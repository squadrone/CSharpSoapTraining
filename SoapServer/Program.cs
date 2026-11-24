using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;

namespace SoapServer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.UseUrls("http://localhost:8080");
            builder.Services.AddServiceModelServices();
            builder.Services.AddServiceModelMetadata();

            var app = builder.Build();

            app.UseServiceModel(serviceBuilder =>
            {
                serviceBuilder.AddService<CalculatorService>();
                serviceBuilder.AddServiceEndpoint<CalculatorService, ICalculatorService>(new BasicHttpBinding(), "/CalculatorService/basicHttp");
            });

            // Enable WSDL Metadata
            var metaDataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();
            metaDataBehavior.HttpGetEnabled = true;

            Console.WriteLine("SOAP Server Started!");
            Console.WriteLine(
                "Endpoint: http://localhost:8080/CalculatorService/basicHttp");

            Console.WriteLine(
                "WSDL: http://localhost:8080/CalculatorService/basicHttp?wsdl");

            await app.RunAsync();
        }
    }
}
