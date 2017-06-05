using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalExpense.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Title { get; set; }
        public string Unit { get; set; }

        public virtual ICollection<Income> Incomes { get; set; }
        public virtual ICollection<Expense> Expenses { get; set; }
    }
}