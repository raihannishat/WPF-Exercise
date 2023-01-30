namespace Calculator.Services;

public interface ICalculatorService
{
    ICalculator GetCalculator(char sign);
}