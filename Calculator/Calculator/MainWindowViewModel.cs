using Calculator.Services;
using Calculator.View;
using Calculator.ViewModel;
using System.Windows;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Calculator
{
    public class MainWindowViewModel : NotifyPropertyChanged
    {
        private BaseViewModel _viewModel;

        public BaseViewModel ViewModel
        {
            get { return _viewModel; }
            
            set 
            { 
                _viewModel = value; 
                RaisePropertyChanged(() => ViewModel);
            }
        }

        private UserControl _view;

        public UserControl View
        {
            get { return _view; }
            
            set 
            { 
                _view = value;
                RaisePropertyChanged(() => View);
            }
        }

        public MainWindowViewModel()
        {
            ViewModel = new BasicCalculatorViewModel(new CalculatorService());
            View = new BasicCalculatorView();
            View.DataContext = ViewModel;
        }

        private ICommand _exitCommand;

        public ICommand ExitCommand
        {
            get
            {
                return _exitCommand ?? (_exitCommand = new DelegateCommand<string>(ExcuteExitCommand));
            }
        }

        private ICommand _updateCommand;

        public ICommand UpdateCommand
        {
            get
            {
                return _updateCommand ?? (_updateCommand = new DelegateCommand<string>(ExcuteUpdateCommand));
            }
        }

        private void ExcuteUpdateCommand(string view)
        {
            switch (view)
            {
                case "Basic":
                    UpdateBasicView();
                    break;
                case "Scientific":
                    UpdateScientificView();
                    break;
                default:
                    UpdateBasicView();
                    break;
            }
        }

        private void ExcuteExitCommand(string obj)
        {
            Application.Current.Shutdown();
        }

        private void UpdateScientificView()
        {
            ViewModel = new ScientificCalculatorViewModel(new CalculatorService());
            View = new ScientificCalculatorView();
            View.DataContext = ViewModel;
        }

        private void UpdateBasicView()
        {
            ViewModel = new BasicCalculatorViewModel(new CalculatorService());
            View = new BasicCalculatorView();
            View.DataContext = ViewModel;
        }
    }
}
