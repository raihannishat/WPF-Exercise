namespace Calculator.Operations;

public class Addition : ICalculator
{
    public double Calculate(double x, double y) => x + y;

    public bool ValidOperator(char sign) => sign == '+';
}
