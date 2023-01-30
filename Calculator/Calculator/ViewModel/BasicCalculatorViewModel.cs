using Calculator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculator.ViewModel
{
    public class BasicCalculatorViewModel : BaseViewModel
    {
        public BasicCalculatorViewModel(ICalculatorService calculatorService) : base(calculatorService)
        {
            Result = "0";
            Number = "0";
        }

        private ICommand _buttonCommand;

        private ICommand _operatorCommand;

        public ICommand OperatorCommand
        {
            get
            {
                return _operatorCommand ?? (_operatorCommand = new DelegateCommand<string>(ExcuteOperatorCommand));
            }
        }

        private void ExcuteOperatorCommand(string input)
        {
            OperatorInputed(input);
        }

        private void ExcuteNumberCommand(string input)
        {
            NumberInputed(input);
        }

        public ICommand ButtonCommand
        {
            get
            {
                return _buttonCommand ?? (_buttonCommand = new DelegateCommand<string>(ExcuteNumberCommand));
            }
        }
    }
}
