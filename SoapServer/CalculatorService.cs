using CoreWCF;

namespace SoapServer
{
    public class CalculatorService : ICalculatorService
    {
        public decimal Add(decimal a, decimal b)
        {
            Console.WriteLine($"Add({a}, {b})");
            return a + b;
        }

        public decimal Divide(decimal a, decimal b)
        {
            Console.WriteLine($"Divide({a}, {b})");

            if (b == 0)
            {
                throw new FaultException("The divisor cannot be zero.");
            }

            return a / b;
        }

        public decimal Multiply(decimal a, decimal b)
        {
            Console.WriteLine($"Multiply({a}, {b})");
            return a * b;
        }

        public decimal Subtract(decimal a, decimal b)
        {
            Console.WriteLine($"Subtract({a}, {b})");
            return a - b;
        }
    }
}
