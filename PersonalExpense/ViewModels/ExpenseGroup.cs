using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonalExpense.ViewModels
{
    public class ExpenseGroup
    {
      
        public DateTime Date_Added { get; set; }

        public int IncomeCount { get; set; }
    }
}