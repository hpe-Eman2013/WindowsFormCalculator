using System;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var calculate = new CalculatorFunctions();
            Console.WriteLine(calculate.CaratValue(8));
            Console.Read();
        }
    }
}
