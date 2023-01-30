namespace Calculator.Contractor;

public interface ICalculator
{
    double Calculate(double x, double y);
    bool ValidOperator(char sign);
}