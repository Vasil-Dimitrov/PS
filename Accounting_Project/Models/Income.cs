namespace Accounting_Project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Income")]
    public partial class Income
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public decimal amount { get; set; }

        public int category_id { get; set; }

        public int user_id { get; set; }

        public DateTime date { get; set; }

        public virtual IncomeCategory IncomeCategory { get; set; }

        public virtual User User { get; set; }
    }
}
