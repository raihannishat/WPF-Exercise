namespace Calculator.Operations;

public class Subtraction : ICalculator
{
    public double Calculate(double x, double y) => x - y;

    public bool ValidOperator(char sign) => sign == '-';
}

