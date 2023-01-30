using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculator
{
    public class DelegateCommand<T> : ICommand
    {
        private readonly Func<bool> canExecuteMethod;

        private readonly Action<T> executeMethod;

        private List<WeakReference> canExecuteChangedHandlers;

        private bool isAutomaticRequeryDisabled;

        public bool IsAutomaticRequeryDisabled
        {
            get
            {
                return isAutomaticRequeryDisabled;
            }
            set
            {
                if (isAutomaticRequeryDisabled != value)
                {
                    if (value)
                    {
                        CommandManagerHelper.RemoveHandlersFromRequerySuggested(canExecuteChangedHandlers);
                    }
                    else
                    {
                        CommandManagerHelper.AddHandlersToRequerySuggested(canExecuteChangedHandlers);
                    }

                    isAutomaticRequeryDisabled = value;
                }
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (!isAutomaticRequeryDisabled)
                {
                    CommandManager.RequerySuggested += value;
                }

                CommandManagerHelper.AddWeakReferenceHandler(ref canExecuteChangedHandlers, value, 2);
            }
            remove
            {
                if (!isAutomaticRequeryDisabled)
                {
                    CommandManager.RequerySuggested -= value;
                }

                CommandManagerHelper.RemoveWeakReferenceHandler(canExecuteChangedHandlers, value);
            }
        }

        public DelegateCommand(Action<T> executeMethod)
            : this(executeMethod, null, isAutomaticRequeryDisabled: false)
        {
        }

        public DelegateCommand(Action<T> executeMethod, Func<bool> canExecuteMethod)
            : this(executeMethod, canExecuteMethod, isAutomaticRequeryDisabled: false)
        {
        }

        public DelegateCommand(Action<T> executeMethod, Func<bool> canExecuteMethod, bool isAutomaticRequeryDisabled)
        {
            if (executeMethod == null)
            {
                throw new ArgumentNullException("executeMethod");
            }

            this.executeMethod = executeMethod;
            this.canExecuteMethod = canExecuteMethod;
            this.isAutomaticRequeryDisabled = isAutomaticRequeryDisabled;
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        void ICommand.Execute(object parameter)
        {
            Execute(parameter);
        }

        public bool CanExecute()
        {
            if (canExecuteMethod != null)
            {
                return canExecuteMethod();
            }

            return true;
        }

        public void Execute(object parameter)
        {
            if (executeMethod != null)
            {
                executeMethod((T)parameter);
            }
        }

        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }

        protected virtual void OnCanExecuteChanged()
        {
            CommandManagerHelper.CallWeakReferenceHandlers(canExecuteChangedHandlers);
        }
    }
}
