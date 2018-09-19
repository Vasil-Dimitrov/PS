namespace Accounting_Project.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AccountingDB : DbContext
    {
        public AccountingDB()
            : base("name=AccountingDB")
        {
        }

        public virtual DbSet<Expense> Expenses { get; set; }
        public virtual DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public virtual DbSet<Income> Incomes { get; set; }
        public virtual DbSet<IncomeCategory> IncomeCategories { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExpenseCategory>()
                .HasMany(e => e.Expenses)
                .WithRequired(e => e.ExpenseCategory)
                .HasForeignKey(e => e.category_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IncomeCategory>()
                .HasMany(e => e.Incomes)
                .WithRequired(e => e.IncomeCategory)
                .HasForeignKey(e => e.category_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.ExpenseCategories)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.user_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.IncomeCategories)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.user_id)
                .WillCascadeOnDelete(false);
        }
    }
}
