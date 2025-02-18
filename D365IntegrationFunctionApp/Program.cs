using D365IntegrationFunctionApp.IServices;
using D365IntegrationFunctionApp.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace D365IntegrationFunctionApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FunctionsDebugger.Enable();

            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices(services =>
                {
                    services.AddApplicationInsightsTelemetryWorkerService();
                    services.ConfigureFunctionsApplicationInsights();
                    services.AddSingleton<ISecretService, SecretService>();
                    services.AddSingleton<IDataEncryptService, DataEncryptService>();
                })
                .Build();

            host.Run();
        }
    }
}
