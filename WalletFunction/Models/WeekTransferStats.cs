using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletFunction.Models
{
    internal class WeekTransferStats
    {
        public double WeeklyIncome { get; set; }

        public List<TransferDayStat> WeekStats { get; set; } = new List<TransferDayStat>();
    }
}
