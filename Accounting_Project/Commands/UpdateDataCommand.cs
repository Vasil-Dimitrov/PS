using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Accounting_Project.Commands
{
    using Accounting_Project.ViewModels;
using System.Windows.Input;
    class UpdateDataCommand : ICommand
    {
        private MainMenuViewModel viewModel;
        public UpdateDataCommand(MainMenuViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        #region ICommand Members

        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return viewModel.CanUpdateData();
        }

        public void Execute(object parameter)
        {
            viewModel.TryUpdateData();
        }

        #endregion
    }
}
