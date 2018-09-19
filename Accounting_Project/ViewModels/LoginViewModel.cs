

namespace Accounting_Project.ViewModels
{
    using Accounting_Project.Commands;
    using Accounting_Project.Models;
    using Accounting_Project.Views;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data.Entity;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    internal class LoginViewModel
    {
        private User user;
        private Window view;

        /// <summary>
        /// Initialize a new instance of the LoginViewModel class.
        /// </summary>
        public LoginViewModel(Window view)
        {
            user = new User();
            this.view = view;
            UpdateCommand = new CustomerUpdateCommand(this);
        }

        /// <summary>
        /// Checks whether the login button should be active or not.
        /// </summary>
        public bool CanUpdate
        {
            get {
                if (User == null)
                    return false;
                return (!String.IsNullOrWhiteSpace(User.username) && !String.IsNullOrWhiteSpace(User.password));
            }
        }


        public User User {
            get{return user;}
            set{user = value;}
        }

        public void TryLogin() {
            using(var db = new AccountingDB()) {
                var query = from b in db.Users where b.username == User.username && b.password == User.password select b;
                foreach (var item in query) {
                    MessageBox.Show("Правилна парола!");
                    User = item;
                    var tempView = view;
                    view = new MainMenu(User);
                    tempView.Visibility = Visibility.Collapsed;
                    view.Owner = tempView;
                    view.Visibility = Visibility.Visible;
                    return;
                }
            }
            MessageBox.Show("Потребителят не съществува! \nГрешна парола!");
        }

        /// <summary>
        /// Gets the UpdateCommand for the ViewModel.
        /// </summary>
        public ICommand UpdateCommand
        {
            get;
            private set;
        }
    }
}
