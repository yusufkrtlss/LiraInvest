using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class IncomeStatement
    {
        [Key]
        public int Id { get; set; }
        public string Symbol  { get; set; }
        public DateTime Date  { get; set; }
        public double TotalRevenue { get; set; }
        public double NetInterestIncome { get; set; }
        public double GrossProfit { get; set; }
        public double NormalizedEBITDA { get; set; }
        public double NormalizedIncome { get; set; }
        public double OperatingRevenue { get; set; }
        public double TotalAssets { get; set; }
        public double TotalDebt { get; set; }
        public double TotalExpenses { get; set; }
        public double CostOfRevenue { get; set; }
        public double CurrentAssets { get; set; }
        public double CurrentDebt { get; set; }
        public double EBIT { get; set; }
        public double NetDebt { get; set; }
        public double NetIncome { get; set; }
        public double NetPPE { get; set; }
        public double LongTermDebt { get; set; }
        public double FreeCashFlow { get; set; }

    }
}
