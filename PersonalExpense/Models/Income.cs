using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonalExpense.Models
{
    public class Income
    {
        public int IncomeID { get; set; }
        public int Amount { get; set; }
        [Display(Name = "Category")]
        public int CategoryID { get; set; }

        [Display(Name = "Date")]
        public DateTime Date_Added { get; set; }
        public virtual Category Category { get; set; }

        
    }
}