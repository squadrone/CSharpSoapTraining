using CoreWCF;

namespace SoapServer
{
    [ServiceContract(Name = "CalculatorService", Namespace = "urn:training:calculator")]
    public interface ICalculatorService
    {
        [OperationContract]
        decimal Add(decimal a, decimal b);
        [OperationContract]
        decimal Subtract(decimal a, decimal b);
        [OperationContract]
        decimal Multiply(decimal a, decimal b);
        [OperationContract]
        decimal Divide(decimal a, decimal b);
    }
}
