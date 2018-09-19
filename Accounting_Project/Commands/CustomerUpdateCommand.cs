namespace Accounting_Project.Commands
{
    using Accounting_Project.ViewModels;
    using System.Windows.Input;
    class CustomerUpdateCommand : ICommand
    {
        private LoginViewModel viewModel;
        public CustomerUpdateCommand(LoginViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        #region ICommand Members

        public event System.EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) {
            return viewModel.CanUpdate;
        }

        public void Execute(object parameter) {
            viewModel.TryLogin();
        }

        #endregion
    }
}
