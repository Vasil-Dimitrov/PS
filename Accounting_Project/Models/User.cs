namespace Accounting_Project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        private string _username;
        private string _password;
        public User()
        {
            ExpenseCategories = new HashSet<ExpenseCategory>();
            IncomeCategories = new HashSet<IncomeCategory>();
        }

        public User(string username, string password)
        {
            _username = username;
            _password = password;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string username { get { return _username; } set { _username = value; } }

        [Required]
        [StringLength(50)]
        public string password { get { return _password; } set { _password = value; } }

        [Required]
        [StringLength(50)]
        public string email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExpenseCategory> ExpenseCategories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IncomeCategory> IncomeCategories { get; set; }
    }
}
