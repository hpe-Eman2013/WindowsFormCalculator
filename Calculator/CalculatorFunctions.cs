using System;

namespace Calculator
{
    public delegate double CalculateValue(double first, double second);
    public class CalculatorFunctions
    {
        
        public double Add(double digit1, double digit2) {return digit1 + digit2;}
        public double Subtract(double digit1, double digit2) {return digit1 - digit2;}
        public double Multiplication(double digit1, double digit2) {return digit1 * digit2;}
        public double Division(double digit1, double digit2) {return digit1 / digit2;}
        public double PlusMinus(double digit) { return digit * -1; }
        public double CaratValue(double digit) { return 1 / digit; }
        public double SquareRoot(double digit) { return Math.Sqrt(digit); }
        public double CalculatePercentage(double digit) { return digit / 100; }
        public double PowerOf(double digit1, double digit2) {return Math.Pow(digit1, digit2);}
        public double Percentage(double digit1, double digit2) { return (digit1 * CalculatePercentage(digit2));}
        public double PerformCalculation(CalculateValue calculate, double first, double second)
        {
            return calculate(first, second);
        }
    }
}
