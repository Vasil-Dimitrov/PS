using Accounting_Project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Accounting_Project.Commands
{
    class ExportCommand : ICommand
    {
        private MainMenuViewModel viewModel;
        public ExportCommand(MainMenuViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return viewModel.CanExport();
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            viewModel.GenerateSimplePdf();
        }
    }
}
