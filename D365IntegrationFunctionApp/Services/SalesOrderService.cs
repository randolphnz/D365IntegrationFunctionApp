using D365IntegrationFunctionApp.Models;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace D365IntegrationFunctionApp.Services
{
    internal class SalesOrderService
    {
        public static SalesOrder UpdateSalesOrder(SalesOrder order)
        {
            List<Container> containers = order.Containers.ToList();
            foreach (Container container in containers)
            {
                string containerType = container.ContainerType;
                switch(containerType)
                {
                    case "20RF":
                        containerType = "REF20";
                        break;
                    case "40RF":
                        containerType = "REF40";
                        break;
                    case "20HC":
                        containerType = "HC20";
                        break;
                    case "40HC":
                        containerType = "HC40";
                        break;
                }
            }

            return order;
        }

        public static async Task UploadSalesOrderCSV(string sftpHost, string sftpUsername, string sftpPassword, int port, 
            string filePath, string destinationPath, ILogger _logger)
        {
            // Retry logic for SFTP upload
            int maxRetryAttempts = 3;
            int delay = 2000; // milliseconds
            bool uploadSuccess = false;

            for (int attempt = 1; attempt <= maxRetryAttempts; attempt++)
            {
                try
                {
                    // Push encrypted JSON data to SFTP server
                    using (var sftpClient = new SftpClient(sftpHost, port, sftpUsername, sftpPassword))
                    {
                        sftpClient.Connect();

                        using (var fileStream = new FileStream(filePath, FileMode.Open))
                        {
                            sftpClient.UploadFile(fileStream, destinationPath);
                        }

                        sftpClient.Disconnect();
                    }

                    uploadSuccess = true;
                    _logger.LogInformation("Encrypted JSON data successfully pushed to SFTP server.");
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Attempt {attempt} - An error occurred: {ex.Message}");
                    if (attempt == maxRetryAttempts)
                    {
                        throw;
                    }
                    await Task.Delay(delay);
                }
            }

            if (!uploadSuccess)
            {
                _logger.LogError("Failed to upload encrypted JSON data to SFTP server after multiple attempts.");
            }
        }
    }
}
