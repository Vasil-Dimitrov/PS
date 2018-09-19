namespace Accounting_Project.ViewModels
{
    using Accounting_Project.Commands;
    using Accounting_Project.Models;
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Input;
    using System.Linq;
    using System.Collections;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using iTextSharp.text;
    using iTextSharp.text.pdf;
    using System.IO;
    using System.Data.SqlClient;
    internal class MainMenuViewModel : INotifyPropertyChanged 
    {
        private User user;
        private Window view;
        public string FromDateStr { get; set; }
        public string ToDateStr { get; set; }
        public ICommand UpdateData { get; set; }
        public ICommand ExportCommand { get; set; }
        public ICommand AddIncome { get; set; }
        public decimal totalProfits {get; set; }
        public decimal totalExpenses { get; set; }
        public decimal fullBalance { get; set; }


         /// <summary>
        /// Initialize a new instance of the LoginViewModel class.
        /// </summary>
        public MainMenuViewModel(Window view, User user)
        {
            this.user = user;
            this.view = view;
            this.totalProfits = 0;
            this.totalExpenses = 0;
            UpdateData = new UpdateDataCommand(this);
            ExportCommand = new ExportCommand(this);
            AddIncome = new AddIncomeCommand(this);


            using (var db = new AccountingDB())
            {
                decimal allIncome = 0;
                decimal allExpense = 0;
                var expenseQuery = from b in db.Expenses where b.user_id == this.user.Id select b;
                foreach (Expense eq in expenseQuery) {
                    allExpense += eq.amount;
                }
                var incomeQuery = from b in db.Incomes where b.user_id == this.user.Id select b;
                foreach (Income ic in incomeQuery)
                {
                    allIncome = ic.amount;
                }

                fullBalance = allIncome - allExpense;
            }
        }

        public string Username {
            get {
                return "Потребител: " + this.user.username;
            }
        }

        public string Sum {
            get {
                return "Налични: " + this.fullBalance;
            }
        }

        public string ProfitsTotal
        {
            get
            {
                return "Приходи за избрания период: " + totalProfits;
            }
            set
            {
                OnPropertyChanged();
            }
        }

        public string ExpensesTotal
        {
            get
            {
                return "Разходи за избрания период: " + totalExpenses;
            }
            set
            {
                OnPropertyChanged();
            }
        }

        public string Balance
        {
            get
            {
                return "Баланс: " + (totalProfits - totalExpenses);
            }
            set
            {
                OnPropertyChanged();
            }
        }

        public bool CanUpdateData()
        {
            return (!String.IsNullOrWhiteSpace(FromDateStr) && !String.IsNullOrWhiteSpace(ToDateStr));
        }

        public void TryUpdateData()
        {
            CultureInfo culture = new CultureInfo("bg");
            DateTime fromDate;
            DateTime toDate;
            try
            {
                fromDate = Convert.ToDateTime(FromDateStr, culture);
                toDate = Convert.ToDateTime(ToDateStr, culture);
            }
            catch (Exception e)
            {
                MessageBox.Show("Въведете коректни дати във формат DD.MM.YYYY HH-MM-SS");
                return;
            }

            ArrayList expenses = new ArrayList();
            ArrayList incomes = new ArrayList();
            totalProfits = 0;
            totalExpenses = 0;


            int expenseMultiplier = 0;
            int incomeMultiplier = 0;
            using (var db = new AccountingDB())
            {
                decimal maxSum = 0;
                var expenseQuery = from b in db.Expenses where b.date >= fromDate && b.date <= toDate select b;
                foreach (Expense item in expenseQuery)
                {
                    expenses.Add(item);
                    if (maxSum < item.amount)
                    {
                        maxSum = item.amount;
                    }
                }
                expenseMultiplier = Convert.ToInt32(180 / maxSum);
                if (expenseMultiplier < 1) expenseMultiplier = 1;

                maxSum = 0;
                var incomeQuery = from b in db.Incomes where b.date >= fromDate && b.date <= toDate select b;
                foreach (Income item in incomeQuery)
                {
                    incomes.Add(item);
                    if (maxSum < item.amount)
                    {
                        maxSum = item.amount;
                    }
                }
                incomeMultiplier = Convert.ToInt32(180 / maxSum);
                if (incomeMultiplier < 1) incomeMultiplier = 1;
            }

            emptyRectangleHeights();

            int index = 0;
            for (int i = 0; i < expenses.Count; i++)
            {
                index++;
                Expense tempExp = ((Expense)expenses[i]);
                totalExpenses += tempExp.amount;
                for (int j = i; j < index * expenses.Count / 10; j++)
                {
                    switch (index)
                    {
                        case 1:
                            ExpenseRectangle1 += tempExp.amount * expenseMultiplier;
                            break;
                        case 2:
                            ExpenseRectangle2 += tempExp.amount * expenseMultiplier;
                            break;
                        case 3:
                            ExpenseRectangle3 += tempExp.amount * expenseMultiplier;
                            break;
                        case 4:
                            ExpenseRectangle4 += tempExp.amount * expenseMultiplier;
                            break;
                        case 5:
                            ExpenseRectangle5 += tempExp.amount * expenseMultiplier;
                            break;
                        case 6:
                            ExpenseRectangle6 += tempExp.amount * expenseMultiplier;
                            break;
                        case 7:
                            ExpenseRectangle7 += tempExp.amount * expenseMultiplier;
                            break;
                        case 8:
                            ExpenseRectangle8 += tempExp.amount * expenseMultiplier;
                            break;
                        case 9:
                            ExpenseRectangle9 += tempExp.amount * expenseMultiplier;
                            break;
                        case 10:
                            ExpenseRectangle10 += tempExp.amount * expenseMultiplier;
                            break;
                    }
                }
            }

            index = 0;
            for (int i = 0; i < incomes.Count; i++)
            {
                index++;
                Income tempIncome = ((Income)incomes[i]);
                totalProfits += tempIncome.amount;
                for (int j = i; j < index * incomes.Count / 10; j++)
                {
                    switch (index)
                    {
                        case 1:
                            IncomeRectangle1 += tempIncome.amount * incomeMultiplier;
                            break;
                        case 2:
                            IncomeRectangle2 += tempIncome.amount * incomeMultiplier;
                            break;
                        case 3:
                            IncomeRectangle3 += tempIncome.amount * incomeMultiplier;
                            break;
                        case 4:
                            IncomeRectangle4 += tempIncome.amount * incomeMultiplier;
                            break;
                        case 5:
                            IncomeRectangle5 += tempIncome.amount * incomeMultiplier;
                            break;
                        case 6:
                            IncomeRectangle6 += tempIncome.amount * incomeMultiplier;
                            break;
                        case 7:
                            IncomeRectangle7 += tempIncome.amount * incomeMultiplier;
                            break;
                        case 8:
                            IncomeRectangle8 += tempIncome.amount * incomeMultiplier;
                            break;
                        case 9:
                            IncomeRectangle9 += tempIncome.amount * incomeMultiplier;
                            break;
                        case 10:
                            IncomeRectangle10 += tempIncome.amount * incomeMultiplier;
                            break;
                    }
                }
            }
            markValuesForUpdate();
        }

        public void emptyRectangleHeights()
        {
            ExpenseRectangle1 = 0;
            ExpenseRectangle2 = 0;
            ExpenseRectangle3 = 0;
            ExpenseRectangle4 = 0;
            ExpenseRectangle5 = 0;
            ExpenseRectangle6 = 0;
            ExpenseRectangle7 = 0;
            ExpenseRectangle8 = 0;
            ExpenseRectangle9 = 0;
            ExpenseRectangle10 = 0;

            IncomeRectangle1 = 0;
            IncomeRectangle2 = 0;
            IncomeRectangle3 = 0;
            IncomeRectangle4 = 0;
            IncomeRectangle5 = 0;
            IncomeRectangle6 = 0;
            IncomeRectangle7 = 0;
            IncomeRectangle8 = 0;
            IncomeRectangle9 = 0;
            IncomeRectangle10 = 0;
        }

        public void markValuesForUpdate()
        {
            ExpensesTotal = "update";
            ProfitsTotal = "update";
            Balance = "update";
        }

        public bool CanExport()
        {
            return (!String.IsNullOrWhiteSpace(totalExpenses.ToString("0.00")) && !String.IsNullOrWhiteSpace(totalProfits.ToString("0.00")));
        }

        public void GenerateSimplePdf()
        {
            Document doc = new Document(PageSize.A4);
            var output = new FileStream(("C:\\Users\\king\\Desktop\\Accounting period.pdf"), FileMode.Create);
            var writer = PdfWriter.GetInstance(doc, output);
            //PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("Accounting period.pdf", FileMode.Create));
            doc.Open();

            Paragraph paragraph = new Paragraph("Report for profit/loses during the period: " + this.FromDateStr + " - " + this.ToDateStr + "\n\n");
            doc.Add(paragraph);

            PdfPTable table1 = new PdfPTable(2);
            table1.DefaultCell.Border = 0;
            table1.WidthPercentage = 80;


            PdfPCell cell11 = new PdfPCell();
            cell11.Colspan = 1;
            cell11.AddElement(new Paragraph("User"));
            cell11.VerticalAlignment = Element.ALIGN_LEFT;

            PdfPCell cell12 = new PdfPCell();
            cell12.VerticalAlignment = Element.ALIGN_CENTER;
            cell12.AddElement(new Paragraph(this.user.username));

            table1.AddCell(cell11);
            table1.AddCell(cell12);

            // row 2
            PdfPCell cell21 = new PdfPCell();
            cell21.Colspan = 1;
            cell21.AddElement(new Paragraph("All time balance"));
            cell21.VerticalAlignment = Element.ALIGN_LEFT;

            PdfPCell cell22 = new PdfPCell();
            cell22.VerticalAlignment = Element.ALIGN_CENTER;
            cell22.AddElement(new Paragraph(this.fullBalance + " BGN"));

            table1.AddCell(cell21);
            table1.AddCell(cell22);

            // row 3
            PdfPCell cell31 = new PdfPCell();
            cell31.Colspan = 1;
            cell31.AddElement(new Paragraph("Profits for period"));
            cell31.VerticalAlignment = Element.ALIGN_LEFT;

            PdfPCell cell32 = new PdfPCell();
            cell32.VerticalAlignment = Element.ALIGN_CENTER;
            cell32.AddElement(new Paragraph(this.totalProfits + " BGN"));

            table1.AddCell(cell31);
            table1.AddCell(cell32);

            // row 4
            PdfPCell cell41 = new PdfPCell();
            cell41.Colspan = 1;
            cell41.AddElement(new Paragraph("Expenses for period"));
            cell41.VerticalAlignment = Element.ALIGN_LEFT;

            PdfPCell cell42 = new PdfPCell();
            cell42.VerticalAlignment = Element.ALIGN_CENTER;
            cell42.AddElement(new Paragraph(this.totalExpenses + " BGN"));

            table1.AddCell(cell41);
            table1.AddCell(cell42);

            // row 5
            PdfPCell cell51 = new PdfPCell();
            cell51.Colspan = 1;
            cell51.AddElement(new Paragraph("Balance or period"));
            cell51.VerticalAlignment = Element.ALIGN_LEFT;

            PdfPCell cell52 = new PdfPCell();
            cell52.VerticalAlignment = Element.ALIGN_CENTER;
            cell52.AddElement(new Paragraph((this.totalProfits - this.totalExpenses) + " BGN"));

            table1.AddCell(cell51);
            table1.AddCell(cell52);

            doc.Add(table1);

            doc.Close();

            MessageBox.Show("Експорт на PDF успешен!");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if(propertyName != null)
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private decimal _ExpenseRectangle1;
        public decimal ExpenseRectangle1
        {
            get
            {
                return _ExpenseRectangle1;
            }
            set
            {
                _ExpenseRectangle1 = value;
                OnPropertyChanged();
            }
        }

        public void insertIncome()
        {
            try
            {
                using (var db = new AccountingDB())
                {
                    Income newIncome = new Income();
                    newIncome.amount = 5;
                    newIncome.category_id = 1;
                    newIncome.user_id = 1;

                    CultureInfo culture = new CultureInfo("bg");
                    newIncome.date = Convert.ToDateTime("19.09.2018", culture);

                    var userQuery = from b in db.Users where b.Id == newIncome.user_id select b;
                    foreach (User user in userQuery)
                    {
                        newIncome.User = user;
                    }

                    var categoryQuery = from b in db.IncomeCategories where b.Id == newIncome.category_id select b;
                    foreach (IncomeCategory category in categoryQuery)
                    {
                        newIncome.IncomeCategory = category;
                    }

                    //db.Incomes.Add(newIncome);
                    db.Incomes.Attach(newIncome);
                    db.SaveChanges();
                }
                MessageBox.Show("Успешно добавени 10лв приходи");
            }
            catch (Exception)
            {
                MessageBox.Show("Възникна проблем при опит за добавяне на приход");
            }

        }

        


        private decimal _ExpenseRectangle2;
        public decimal ExpenseRectangle2
        {
            get
            {
                return _ExpenseRectangle2;
            }
            set
            {
                _ExpenseRectangle2 = value;
                OnPropertyChanged();
            }
        }


        private decimal _ExpenseRectangle3;
        public decimal ExpenseRectangle3
        {
            get
            {
                return _ExpenseRectangle3;
            }
            set
            {
                _ExpenseRectangle3 = value;
                OnPropertyChanged();
            }
        }


        private decimal _ExpenseRectangle4;
        public decimal ExpenseRectangle4
        {
            get
            {
                return _ExpenseRectangle4;
            }
            set
            {
                _ExpenseRectangle4 = value;
                OnPropertyChanged();
            }
        }


        private decimal _ExpenseRectangle5;
        public decimal ExpenseRectangle5
        {
            get
            {
                return _ExpenseRectangle5;
            }
            set
            {
                _ExpenseRectangle5 = value;
                OnPropertyChanged();
            }
        }


        private decimal _ExpenseRectangle6;
        public decimal ExpenseRectangle6
        {
            get
            {
                return _ExpenseRectangle6;
            }
            set
            {
                _ExpenseRectangle6 = value;
                OnPropertyChanged();
            }
        }


        private decimal _ExpenseRectangle7;
        public decimal ExpenseRectangle7
        {
            get
            {
                return _ExpenseRectangle7;
            }
            set
            {
                _ExpenseRectangle7 = value;
                OnPropertyChanged();
            }
        }


        private decimal _ExpenseRectangle8;
        public decimal ExpenseRectangle8
        {
            get
            {
                return _ExpenseRectangle8;
            }
            set
            {
                _ExpenseRectangle8 = value;
                OnPropertyChanged();
            }
        }


        private decimal _ExpenseRectangle9;
        public decimal ExpenseRectangle9
        {
            get
            {
                return _ExpenseRectangle9;
            }
            set
            {
                _ExpenseRectangle9 = value;
                OnPropertyChanged();
            }
        }


        private decimal _ExpenseRectangle10;
        public decimal ExpenseRectangle10
        {
            get
            {
                return _ExpenseRectangle10;
            }
            set
            {
                _ExpenseRectangle10 = value;
                OnPropertyChanged();
            }
        }

        // INCOME

              private decimal _IncomeRectangle1;
        public decimal IncomeRectangle1
        {
            get
            {
                return _IncomeRectangle1;
            }
            set
            {
                _IncomeRectangle1 = value;
                OnPropertyChanged();
            }
        }


        private decimal _IncomeRectangle2;
        public decimal IncomeRectangle2
        {
            get
            {
                return _IncomeRectangle2;
            }
            set
            {
                _IncomeRectangle2 = value;
                OnPropertyChanged();
            }
        }


        private decimal _IncomeRectangle3;
        public decimal IncomeRectangle3
        {
            get
            {
                return _IncomeRectangle3;
            }
            set
            {
                _IncomeRectangle3 = value;
                OnPropertyChanged();
            }
        }


        private decimal _IncomeRectangle4;
        public decimal IncomeRectangle4
        {
            get
            {
                return _IncomeRectangle4;
            }
            set
            {
                _IncomeRectangle4 = value;
                OnPropertyChanged();
            }
        }


        private decimal _IncomeRectangle5;
        public decimal IncomeRectangle5
        {
            get
            {
                return _IncomeRectangle5;
            }
            set
            {
                _IncomeRectangle5 = value;
                OnPropertyChanged();
            }
        }


        private decimal _IncomeRectangle6;
        public decimal IncomeRectangle6
        {
            get
            {
                return _IncomeRectangle6;
            }
            set
            {
                _IncomeRectangle6 = value;
                OnPropertyChanged();
            }
        }


        private decimal _IncomeRectangle7;
        public decimal IncomeRectangle7
        {
            get
            {
                return _IncomeRectangle7;
            }
            set
            {
                _IncomeRectangle7 = value;
                OnPropertyChanged();
            }
        }


        private decimal _IncomeRectangle8;
        public decimal IncomeRectangle8
        {
            get
            {
                return _IncomeRectangle8;
            }
            set
            {
                _IncomeRectangle8 = value;
                OnPropertyChanged();
            }
        }


        private decimal _IncomeRectangle9;
        public decimal IncomeRectangle9
        {
            get
            {
                return _IncomeRectangle9;
            }
            set
            {
                _IncomeRectangle9 = value;
                OnPropertyChanged();
            }
        }


        private decimal _IncomeRectangle10;
        public decimal IncomeRectangle10
        {
            get
            {
                return _IncomeRectangle10;
            }
            set
            {
                _IncomeRectangle10 = value;
                OnPropertyChanged();
            }
        }
    }






    }


