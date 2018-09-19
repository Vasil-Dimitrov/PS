using Accounting_Project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Accounting_Project.Commands
{
    class AddIncomeCommand : ICommand
    {
        private MainMenuViewModel viewModel;
        public AddIncomeCommand(MainMenuViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        #region ICommand Members

        public event System.EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) {
            return true; // return viewModel.CanUpdate;
        }

        public void Execute(object parameter) {
            viewModel.insertIncome();
        }

        #endregion
    }
}
