namespace Calculator.Services;

public class CalculatorService : ICalculatorService
{
    private readonly IList<ICalculator> _calculators;
    private readonly Type _calculatorType;
    private readonly Assembly _assembly;
    private readonly Type[] _implementedTypes;
    private readonly IEnumerable<Type> _operations;

    public CalculatorService()
    {
        _calculators = new List<ICalculator>();
        _calculatorType = typeof(ICalculator);
        _assembly = Assembly.GetExecutingAssembly();
        _implementedTypes = _assembly.GetTypes();
        _operations = _implementedTypes.Where(t => t.GetInterfaces().Contains(_calculatorType));

        LoadOperations();
    }

    public ICalculator GetCalculator(char sign)
    {
        ICalculator myObject = null!;

        foreach (var item in _calculators)
        {
            myObject = item.ValidOperator(sign) == true ? item : null!;

            if (myObject != null)
            {
                return myObject;
            }
        }

        return null!;
    }

    private void LoadOperations()
    {
        foreach (var item in _operations)
        {
            ICalculator calculator = (ICalculator)Activator.CreateInstance(item)!;
            _calculators.Add(calculator);
        }
    }
}
