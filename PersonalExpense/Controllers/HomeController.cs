using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonalExpense.DAL;
using PersonalExpense.ViewModels;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web.Helpers;
using System.IO;
using System.Web.UI.DataVisualization.Charting;
using System.Data;

namespace PersonalExpense.Controllers
{
    public class HomeController : Controller
    {
        private PersonalExpenseContext db = new PersonalExpenseContext();
        public ActionResult Index()
        {
            //IQueryable<ExpenseGroup> data = from income in db.Incomes
            //                                group income by income.Date_Added into dateGroup
            //                                select new ExpenseGroup()
            //                                       {
            //                                           Date_Added = dateGroup.Key,
            //                                           IncomeCount = dateGroup.Count()
            //                                       };

            IQueryable<ExpenseGroup> data = from income in db.Incomes
                                            group income by income.Date_Added into dateGroup
                                            select new ExpenseGroup()
                                                   {
                                                       Date_Added = dateGroup.Key,
                                                       IncomeCount = dateGroup.Sum(a =>a.Amount)
                                                   };
            using (var context = new PersonalExpenseContext())
            {
                ViewBag.Total = context.Incomes.Sum(x => x.Amount);

                ViewBag.TotalE = context.Expenses.Sum(x => x.Amount);
                ViewBag.FinalTotal = context.Incomes.Sum(x => x.Amount) - context.Expenses.Sum(x => x.Amount);
            }
            return View(data);
        }
        //public ActionResult Chart()
        //{
        //    Decimal TotalIncome;
        //    Decimal TotalExpense;
        //    using (var context = new PersonalExpenseContext())
        //    {
        //        TotalIncome = context.Incomes.Sum(x => x.Amount);
        //        TotalExpense = context.Expenses.Sum(x => x.Amount);
        //    }
        //    new System.Web.Helpers.Chart(width: 800, height: 200).AddSeries(
        //        chartType: "column",
        //        xValue: new[] { "Total Income", "Total Expense" },
        //        yValues: new[] { TotalIncome, TotalExpense }).Write("png");
        //    return null;

        //}

        public ActionResult Chart2()
        {
           
            int jan = january_count();
            int feb = february_count();
            int mar = march_count();
            int apr = april_count();
            int may = may_count();
            int jun = june_count();
            int jul = july_count();
            int aug = august_count();
            int sept = september_count();
            int oct = october_count();
            int nov = november_count();
            int dec = december_count();
            // Expense ////////////////////////////
            int jan2 = january_count2();
            int feb2 = february_count2();
            int mar2 = march_count2();
            int apr2 = april_count2();
            int may2 = may_count2();
            int jun2 = june_count2();
            int jul2 = july_count2();
            int aug2 = august_count2();
            int sept2 = september_count2();
            int oct2 = october_count2();
            int nov2 = november_count2();
            int dec2 = december_count2();
            // year////////////////
            //int year = year_count();
            //int year1 = year1_count();
            //int year2 = year2_count();

            using (var context = new PersonalExpenseContext())
            {
                //    TotalIncome = context.Incomes.Sum(x => x.Amount);
                //    TotalExpense = context.Expenses.Sum(x => x.Amount);

                //                jan = context.Incomes.SqlQuery(@"select [Amount] From
                //                                             [PEM].[dbo].[Income]
                //                                             where datepart(month,[Date_Added]) = '1'").Count();
            }



            var dataChart = new System.Web.Helpers.Chart(width: 800, height: 400, theme: ChartTheme.Blue).AddTitle("Monthly Total").AddSeries(
                chartType: "column",
                xValue: new[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" },
                yValues: new[] { jan, feb, mar, apr, may, jun, jul, aug, sept, oct, nov, dec },
                name: "Incomes")
                .AddLegend()
                .AddSeries(
                chartType: "column",
                xValue: new[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" },
                yValues: new[] { jan2, feb2, mar2, apr2, may2, jun2, jul2, aug2, sept2, oct2, nov2, dec2 },
                name: "Expenses")
                .AddLegend()
                .Write("png");
            return null;

        }
        public ActionResult Chart1()
        {
            // year////////////////
            int year = year_count();
            int year1 = year1_count();
            int year2 = year2_count();
            //int year3 = year3_count();
            //int year4 = year4_count();
            //int year5 = year5_count();
            //int year6 = year6_count();

            using (var context = new PersonalExpenseContext())
            {
                //    TotalIncome = context.Incomes.Sum(x => x.Amount);
                //    TotalExpense = context.Expenses.Sum(x => x.Amount);

                //                jan = context.Incomes.SqlQuery(@"select [Amount] From
                //                                             [PEM].[dbo].[Income]
                //                                             where datepart(month,[Date_Added]) = '1'").Count();
            }


            var chart1 = new System.Web.UI.DataVisualization.Charting.Chart();
            chart1.Width = 800;
            chart1.Height = 400;
            chart1.ChartAreas.Add("xAxis").BackColor = System.Drawing.Color.FromArgb(64, System.Drawing.Color.White);
            chart1.Series.Add("xAxis");
            chart1.Series["xAxis"].Points.AddXY("2015", year);
            chart1.Series["xAxis"].Points.AddXY("2016", year1);
            chart1.Series["xAxis"].Points.AddXY("2017", year2);
            //chart1.Series["xAxis"].Points.AddXY("2017", year3);
            //chart1.Series["xAxis"].Points.AddXY(year4);
            //chart1.Series["xAxis"].Points.AddXY(year5);
            //chart1.Series["xAxis"].Points.AddXY(year6);
            chart1.Series["xAxis"].IsValueShownAsLabel = true;
            MemoryStream ms = new MemoryStream();
            chart1.SaveImage(ms, ChartImageFormat.Png);
            return File(ms.ToArray(), "image/png");
            //return null;

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public int january_count()
        {
            string connnectionString = @"Server=.\SQLEXPRESS;Database=PEM;Integrated Security=True";
            string query = @"select sum ([Amount]) From
                                             [PEM].[dbo].[Income]
                                            where datepart(month,[Date_Added]) = '1'";

            int jan;
            using (SqlConnection con = new SqlConnection(connnectionString))
            {
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    con.Open();
                    jan = Convert.ToInt32(com.ExecuteScalar());
                }
            }

            return jan;
        }
        public int february_count()
        {
            string connnectionString = @"Server=.\SQLEXPRESS;Database=PEM;Integrated Security=True";
            string query = @"select sum ([Amount]) From
                                             [PEM].[dbo].[Income]
                                            where datepart(month,[Date_Added]) = '2'";

            int feb;
            using (SqlConnection con = new SqlConnection(connnectionString))
            {
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    con.Open();
                    feb = Convert.ToInt32(com.ExecuteScalar());
                }
            }

            return feb;
        }
        public int march_count()
        {
            string connnectionString = @"Server=.\SQLEXPRESS;Database=PEM;Integrated Security=True";
            string query = @"select sum ([Amount]) From
                                             [PEM].[dbo].[Income]
                                            where datepart(month,[Date_Added]) = '3'";

            int mar;
            using (SqlConnection con = new SqlConnection(connnectionString))
            {
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    con.Open();
                    mar = Convert.ToInt32(com.ExecuteScalar());
                }
            }

            return mar;
        }
        public int april_count()
        {
            string connnectionString = @"Server=.\SQLEXPRESS;Database=PEM;Integrated Security=True";
            string query = @"select sum ([Amount]) From
                                             [PEM].[dbo].[Income]
                                            where datepart(month,[Date_Added]) = '4'";

            int apr;
            using (SqlConnection con = new SqlConnection(connnectionString))
            {
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    con.Open();
                    apr = Convert.ToInt32(com.ExecuteScalar());
                }
            }

            return apr;
        }
        public int may_count()
        {
            string connnectionString = @"Server=.\SQLEXPRESS;Database=PEM;Integrated Security=True";
            string query = @"select sum ([Amount]) From
                                             [PEM].[dbo].[Income]
                                            where datepart(month,[Date_Added]) = '5'";

            int may;
            using (SqlConnection con = new SqlConnection(connnectionString))
            {
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    con.Open();
                    may = Convert.ToInt32(com.ExecuteScalar());
                }
            }

            return may;
        }
        public int june_count()
        {
            string connnectionString = @"Server=.\SQLEXPRESS;Database=PEM;Integrated Security=True";
            string query = @"select sum ([Amount]) From
                                             [PEM].[dbo].[Income]
                                            where datepart(month,[Date_Added]) = '6'";

            int jun;
            using (SqlConnection con = new SqlConnection(connnectionString))
            {
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    con.Open();
                    jun = Convert.ToInt32(com.ExecuteScalar());
                }
            }

            return jun;
        }
        public int july_count()
        {
            string connnectionString = @"Server=.\SQLEXPRESS;Database=PEM;Integrated Security=True";
            string query = @"select sum ([Amount]) From
                                             [PEM].[dbo].[Income]
                                            where datepart(month,[Date_Added]) = '7'";

            int jul;
            using (SqlConnection con = new SqlConnection(connnectionString))
            {
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    con.Open();
                    jul = Convert.ToInt32(com.ExecuteScalar());
                }
            }

            return jul;
        }
        public int august_count()
        {
            string connnectionString = @"Server=.\SQLEXPRESS;Database=PEM;Integrated Security=True";
            string query = @"select sum ([Amount]) From
                                             [PEM].[dbo].[Income]
                                            where datepart(month,[Date_Added]) = '8'";

            int aug;
            using (SqlConnection con = new SqlConnection(connnectionString))
            {
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    con.Open();
                    aug = Convert.ToInt32(com.ExecuteScalar());
                }
            }

            return aug;
        }
        public int september_count()
        {
            string connnectionString = @"Server=.\SQLEXPRESS;Database=PEM;Integrated Security=True";
            string query = @"select sum ([Amount]) From 
                                             [PEM].[dbo].[Income]
                                            where datepart(month,[Date_Added]) = '9'";

            int sept;
            using (SqlConnection con = new SqlConnection(connnectionString))
            {
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    con.Open();
                    sept = Convert.ToInt32(com.ExecuteScalar());
                }
            }

            return sept;
        }
        public int october_count()
        {
            string connnectionString = @"Server=.\SQLEXPRESS;Database=PEM;Integrated Security=True";
            string query = @"select sum ([Amount]) From
                                             [PEM].[dbo].[Income]
                                            where datepart(month,[Date_Added]) = '10'";

            int oct;
            using (SqlConnection con = new SqlConnection(connnectionString))
            {
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    con.Open();
                    oct = Convert.ToInt32(com.ExecuteScalar());
                }
            }

            return oct;
        }
        public int november_count()
        {
            string connnectionString = @"Server=.\SQLEXPRESS;Database=PEM;Integrated Security=True";
            string query = @"select sum ([Amount]) From
                                             [PEM].[dbo].[Income]
                                            where datepart(month,[Date_Added]) = '11'";

            int nov;
            using (SqlConnection con = new SqlConnection(connnectionString))
            {
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    con.Open();
                    nov = Convert.ToInt32(com.ExecuteScalar());
                }
            }

            return nov;
        }
        public int december_count()
        {
            string connnectionString = @"Server=.\SQLEXPRESS;Database=PEM;Integrated Security=True";
            string query = @"select sum ([Amount]) From
                                             [PEM].[dbo].[Income]
                                            where datepart(month,[Date_Added]) = '12'";

            int dec;
            using (SqlConnection con = new SqlConnection(connnectionString))
            {
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    con.Open();
                    dec = Convert.ToInt32(com.ExecuteScalar());
                }
            }

            return dec;
        }
        //Expense /////////////////////////////////////////////////////////////////
        public int january_count2()
        {
            string connnectionString = @"Server=.\SQLEXPRESS;Database=PEM;Integrated Security=True";
            string query = @"select sum ([Amount]) From
                                             [PEM].[dbo].[Expense]
                                            where datepart(month,[Date_Added]) = '1'";

            int jan2;
            using (SqlConnection con = new SqlConnection(connnectionString))
            {
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    con.Open();
                    jan2 = Convert.ToInt32(com.ExecuteScalar());
                }
            }

            return jan2;
        }
        public int february_count2()
        {
            string connnectionString = @"Server=.\SQLEXPRESS;Database=PEM;Integrated Security=True";
            string query = @"select sum ([Amount]) From
                                             [PEM].[dbo].[Expense]
                                            where datepart(month,[Date_Added]) = '2'";

            int feb2;
            using (SqlConnection con = new SqlConnection(connnectionString))
            {
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    con.Open();
                    feb2 = Convert.ToInt32(com.ExecuteScalar());

                }
            }

            return feb2;
        }
        public int march_count2()
        {
            string connnectionString = @"Server=.\SQLEXPRESS;Database=PEM;Integrated Security=True";
            string query = @"select sum ([Amount]) From
                                             [PEM].[dbo].[Expense]
                                            where datepart(month,[Date_Added]) = '3'";

            int mar2;
            using (SqlConnection con = new SqlConnection(connnectionString))
            {
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    con.Open();
                    mar2 = Convert.ToInt32(com.ExecuteScalar());
                }
            }

            return mar2;
        }
        public int april_count2()
        {
            string connnectionString = @"Server=.\SQLEXPRESS;Database=PEM;Integrated Security=True";
            string query = @"select sum ([Amount]) From
                                             [PEM].[dbo].[Expense]
                                            where datepart(month,[Date_Added]) = '4'";

            int apr2;
            using (SqlConnection con = new SqlConnection(connnectionString))
            {
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    con.Open();
                    apr2 = Convert.ToInt32(com.ExecuteScalar());
                }
            }

            return apr2;
        }
        public int may_count2()
        {
            string connnectionString = @"Server=.\SQLEXPRESS;Database=PEM;Integrated Security=True";
            string query = @"select sum ([Amount]) From
                                             [PEM].[dbo].[Expense]
                                            where datepart(month,[Date_Added]) = '5'";

            int may2;
            using (SqlConnection con = new SqlConnection(connnectionString))
            {
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    con.Open();
                    may2 = Convert.ToInt32(com.ExecuteScalar());
                }
            }

            return may2;
        }
        public int june_count2()
        {
            string connnectionString = @"Server=.\SQLEXPRESS;Database=PEM;Integrated Security=True";
            string query = @"select sum ([Amount]) From
                                             [PEM].[dbo].[Expense]
                                            where datepart(month,[Date_Added]) = '6'";

            int jun2;
            using (SqlConnection con = new SqlConnection(connnectionString))
            {
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    con.Open();
                    jun2 = Convert.ToInt32(com.ExecuteScalar());
                }
            }

            return jun2;
        }
        public int july_count2()
        {
            string connnectionString = @"Server=.\SQLEXPRESS;Database=PEM;Integrated Security=True";
            string query = @"select sum ([Amount]) From
                                             [PEM].[dbo].[Expense]
                                            where datepart(month,[Date_Added]) = '7'";

            int jul2;
            using (SqlConnection con = new SqlConnection(connnectionString))
            {
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    con.Open();
                    jul2 = Convert.ToInt32(com.ExecuteScalar());
                }
            }

            return jul2;
        }
        public int august_count2()
        {
            string connnectionString = @"Server=.\SQLEXPRESS;Database=PEM;Integrated Security=True";
            string query = @"select sum ([Amount]) From
                                             [PEM].[dbo].[Expense]
                                            where datepart(month,[Date_Added]) = '8'";

            int aug2;
            using (SqlConnection con = new SqlConnection(connnectionString))
            {
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    con.Open();
                    aug2 = Convert.ToInt32(com.ExecuteScalar());
                }
            }

            return aug2;
        }
        public int september_count2()
        {
            string connnectionString = @"Server=.\SQLEXPRESS;Database=PEM;Integrated Security=True";
            string query = @"select sum ([Amount]) From
                                             [PEM].[dbo].[Expense]
                                            where datepart(month,[Date_Added]) = '9'";

            int sept2;
            using (SqlConnection con = new SqlConnection(connnectionString))
            {
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    con.Open();
                    sept2 = Convert.ToInt32(com.ExecuteScalar());
                }
            }

            return sept2;
        }
        public int october_count2()
        {
            string connnectionString = @"Server=.\SQLEXPRESS;Database=PEM;Integrated Security=True";
            string query = @"select sum ([Amount]) From
                                             [PEM].[dbo].[Expense]
                                            where datepart(month,[Date_Added]) = '10'";

            int oct2;
            using (SqlConnection con = new SqlConnection(connnectionString))
            {
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    con.Open();
                    oct2 = Convert.ToInt32(com.ExecuteScalar());
                }
            }

            return oct2;
        }
        public int november_count2()
        {
            string connnectionString = @"Server=.\SQLEXPRESS;Database=PEM;Integrated Security=True";
            string query = @"select sum ([Amount]) From
                                             [PEM].[dbo].[Expense]
                                            where datepart(month,[Date_Added]) = '11'";

            int nov2;
            using (SqlConnection con = new SqlConnection(connnectionString))
            {
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    con.Open();
                    nov2 = Convert.ToInt32(com.ExecuteScalar());
                }
            }

            return nov2;
        }
        public int december_count2()
        {
            string connnectionString = @"Server=.\SQLEXPRESS;Database=PEM;Integrated Security=True";
            string query = @"select sum ([Amount]) From
                                             [PEM].[dbo].[Expense]
                                            where datepart(month,[Date_Added]) = '12'";

            int dec2;
            using (SqlConnection con = new SqlConnection(connnectionString))
            {
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    con.Open();
                    dec2 = Convert.ToInt32(com.ExecuteScalar());
                }
            }

            return dec2;
        }
        public int year_count()
        {
            string connnectionString = @"Server=.\SQLEXPRESS;Database=PEM;Integrated Security=True";
            string query = @"SELECT SUM([Amount])
                            FROM [PEM].[dbo].[Income]
                            Where YEAR ([Date_Added]) = '2015'";

            int year;
            using (SqlConnection con = new SqlConnection(connnectionString))
            {
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    con.Open();
                    year = Convert.ToInt32(com.ExecuteScalar());
                }
            }

            return year;
        }
        public int year1_count()
        {
            string connnectionString = @"Server=.\SQLEXPRESS;Database=PEM;Integrated Security=True";
            string query = @"SELECT SUM([Amount])
                            FROM [PEM].[dbo].[Income]
                            Where YEAR ([Date_Added]) = '2016'";

            int year1;
            using (SqlConnection con = new SqlConnection(connnectionString))
            {
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    con.Open();
                    year1 = Convert.ToInt32(com.ExecuteScalar());
                }
            }

            return year1;
        }
        public int year2_count()
        {
            string connnectionString = @"Server=.\SQLEXPRESS;Database=PEM;Integrated Security=True";
            string query = @"SELECT SUM([Amount])
                            FROM [PEM].[dbo].[Income]
                            Where YEAR ([Date_Added]) = '2017'";

            int year2;
            using (SqlConnection con = new SqlConnection(connnectionString))
            {
                using (SqlCommand com = new SqlCommand(query, con))
                {
                    con.Open();
                    year2 = Convert.ToInt32(com.ExecuteScalar());
                }
            }

            return year2;
        }
//        public int year3_count()
//        {
//            string connnectionString = @"Server=.\SQLEXPRESS;Database=PEM;Integrated Security=True";
//            string query = @"SELECT SUM([Amount])
//                            FROM [PEM].[dbo].[Expense]
//                            Where YEAR ([Date_Added]) = '2017'";

//            int year3;
//            using (SqlConnection con = new SqlConnection(connnectionString))
//            {
//                using (SqlCommand com = new SqlCommand(query, con))
//                {
//                    con.Open();
//                    year3 = Convert.ToInt32(com.ExecuteScalar());
//                }
//            }

//            return year3;
//        }
//        public int year4_count()
//        {
//            string connnectionString = @"Server=.\SQLEXPRESS;Database=PEM;Integrated Security=True";
//            string query = @"SELECT SUM([Amount])
//                            FROM [PEM].[dbo].[Expense]
//                            Where YEAR ([Date_Added]) = '2015'";

//            int year4;
//            using (SqlConnection con = new SqlConnection(connnectionString))
//            {
//                using (SqlCommand com = new SqlCommand(query, con))
//                {
//                    con.Open();
//                    year4 = Convert.ToInt32(com.ExecuteScalar());
//                }
//            }

//            return year4;
//        }
//        public int year5_count()
//        {
//            string connnectionString = @"Server=.\SQLEXPRESS;Database=PEM;Integrated Security=True";
//            string query = @"SELECT SUM([Amount])
//                            FROM [PEM].[dbo].[Expense]
//                            Where YEAR ([Date_Added]) = '2016'";

//            int year5;
//            using (SqlConnection con = new SqlConnection(connnectionString))
//            {
//                using (SqlCommand com = new SqlCommand(query, con))
//                {
//                    con.Open();
//                    year5 = Convert.ToInt32(com.ExecuteScalar());
//                }
//            }

//            return year5;
//        }
//        public int year6_count()
//        {
//            string connnectionString = @"Server=.\SQLEXPRESS;Database=PEM;Integrated Security=True";
//            string query = @"SELECT SUM([Amount])
//                            FROM [PEM].[dbo].[Expense]
//                            Where YEAR ([Date_Added]) = '2017'";

//            int year6;
//            using (SqlConnection con = new SqlConnection(connnectionString))
//            {
//                using (SqlCommand com = new SqlCommand(query, con))
//                {
//                    con.Open();
//                    year6 = Convert.ToInt32(com.ExecuteScalar());
//                }
//            }

//            return year6;
//        }
   }
}