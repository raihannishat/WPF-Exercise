using Calculator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.ViewModel
{
    public abstract class BaseViewModel : NotifyPropertyChanged
    {

        private string _result;
        public string Result
        {
            get { return _result; }
            set { _result = value; RaisePropertyChanged(() => Result); }
        }


        private string _number;
        public string Number
        {
            get { return _number; }
            set
            {
                _number = value;
                RaisePropertyChanged(() => Number);
            }
        }

        private string lastOperator;


        private readonly ICalculatorService _calculatorService;
        public ICalculatorService CalculatorService
        {
            get { return _calculatorService; }
        }

        public BaseViewModel(ICalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }

        public void OperatorInputed(string input)
        {
            if (Result == "0")
            {
                Result = Number;
            }
            else
            {
                var calculator = _calculatorService.GetCalculator(lastOperator[0]);

                if (calculator == null)
                {
                    throw new NotImplementedException();
                }

                Result = calculator.Calculate(double.Parse(Result), double.Parse(Number)).ToString();
            }

            Number = "0";
            lastOperator = input;
        }

        public void NumberInputed(string input)
        {
            if (Number == "0")
            {
                Number = input;
            }
            else
            {
                Number += input;
            }
        }
    }
}
