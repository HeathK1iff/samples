namespace Samples.Singleton
{
    public class Calculator : ICalculator
    {
        private static Calculator _instance = new();
        private Calculator() { }

        public static Calculator Instance = new Calculator();

        public double Plus(double x1, double x2)
        {
            return x1 + x2;
        }
    }
}