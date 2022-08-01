using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletFunction.Models
{
    internal class PeriodTransferStats
    {
        public double PeriodIncome { get; set; }

        public List<string> Days { get; set; } = new List<string>();

        public List<double> Incomes { get; set; } = new List<double>();

        public List<TransferDayStat> DaysStats { get; set; } = new List<TransferDayStat>();
    }
}
