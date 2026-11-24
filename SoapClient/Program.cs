using System.ServiceModel;

namespace SoapClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string endpointUrl = "http://localhost:8080/CalculatorService/basicHttp";

            var binding = new BasicHttpBinding(BasicHttpSecurityMode.None)
            {
                OpenTimeout = TimeSpan.FromSeconds(10),
                CloseTimeout = TimeSpan.FromSeconds(10),
                SendTimeout = TimeSpan.FromSeconds(10),
                ReceiveTimeout = TimeSpan.FromSeconds(10),
                MaxReceivedMessageSize = 64 * 1024
            };

            var endpoint = new EndpointAddress(endpointUrl);
            var factory = new ChannelFactory<ICalculatorService>(binding, endpoint);

            IClientChannel? channel = null;

            try
            {
                factory.Open();

                ICalculatorService proxy = factory.CreateChannel();

                channel = (IClientChannel)proxy;
                channel.Open();

                Console.WriteLine("Connected to SOAP service!\n");

                // Call SOAP operations
                Console.WriteLine($"10 + 5 = {proxy.Add(10m, 5m)}");

                Console.WriteLine($"10 - 5 = {proxy.Subtract(10m, 5m)}");

                Console.WriteLine($"10 * 5 = {proxy.Multiply(10m, 5m)}");

                Console.WriteLine($"10 / 5 = {proxy.Divide(10m, 5m)}");

                // Test SOAP Fault handling
                try
                {
                    proxy.Divide(10m, 0m);
                }
                catch (FaultException fault)
                {
                    Console.WriteLine($"\nSOAP Fault received: {fault.Message}");
                }

                channel.Close();
                factory.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                channel?.Abort();
                factory.Abort();

                Console.WriteLine($"SOAP communication failed: {ex.Message}");
            }
        }
    }
}
