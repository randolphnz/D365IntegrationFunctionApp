using Azure.Messaging.ServiceBus;
using D365IntegrationFunctionApp.IServices;
using D365IntegrationFunctionApp.Models;
using D365IntegrationFunctionApp.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace D365IntegrationFunctionApp
{
    public class Function
    {
        private readonly ILogger _logger;
        private readonly ISecretService _secretService;
        private readonly IDataEncryptService _dataEncryptServices;
        private static readonly HttpClient httpClient = new HttpClient();

        public Function(ILoggerFactory loggerFactory, ISecretService secretService, IDataEncryptService dataEncryptServices)
        {
            _logger = loggerFactory.CreateLogger<Function>();
            _secretService = secretService;
            _dataEncryptServices = dataEncryptServices;
        }

        [Function("D365IntegrationFunction")]
        public async Task Run([ServiceBusTrigger("queue", Connection = "ServiceBusConnection")] ServiceBusReceivedMessage message)
        {
            _logger.LogInformation($"Function executed at: {DateTime.Now}");
            try
            {
                // Deserialize json to .net object
                SalesOrder salesOrder = JsonSerializer.Deserialize<SalesOrder>(message.ToString());
                _logger.LogInformation($"Ready for dispatch event json deserialized at: {DateTime.Now}");

                // Container conversion
                salesOrder = SalesOrderService.UpdateSalesOrder(salesOrder);
                _logger.LogInformation($"Container conversion at: {DateTime.Now}");

                // Serialize object to json
                string jsonString = JsonSerializer.Serialize(salesOrder);
                string csv = JsonToCSVService.JsonToCsv(jsonString);
                File.WriteAllText("output.csv", csv);

                string sftpHost = await _secretService.GetSecret("SftpHost", "kvUrl", "kvName");
                _logger.LogInformation($"Sftp host fetched at: {DateTime.Now}");
                string sftpUsername = await _secretService.GetSecret("SftpUsername", "kvUrl", "kvName");
                _logger.LogInformation($"Sftp Username fetched at: {DateTime.Now}");
                string sftpPassword = await _secretService.GetSecret("SftpPassword", "kvUrl", "kvName");
                _logger.LogInformation($"Sftp Password fetched at: {DateTime.Now}");
                string port = await _secretService.GetSecret("SftpDirectory", "kvUrl", "kvName");
                _logger.LogInformation($"Sftp Port fetched at: {DateTime.Now}");
                string password = await _secretService.GetSecret("EncryptionKey", "kvUrl", "kvName");
                _logger.LogInformation($"Password fetched at: {DateTime.Now}");

                // Encrypt csv
                _dataEncryptServices.EncryptCsvFile("output.csv", "output_encrypted.csv", password);
                _logger.LogInformation($"CSV encrypted at: {DateTime.Now}");

                //Upload to SFTP
                await SalesOrderService.UploadSalesOrderCSV(sftpHost, sftpUsername, sftpPassword, int.Parse(port), "output_encrypted.csv", "/bluecorp-incoming", _logger);
                _logger.LogInformation($"Encrypted csv uploaded at: {DateTime.Now}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex.Message}");
            }
        }
    }
}
