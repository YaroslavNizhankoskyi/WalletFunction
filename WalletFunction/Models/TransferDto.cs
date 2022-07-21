using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletFunction.Models
{
    internal class TransferDto
    {
        public string Id { get; set; }

        public string WalletId { get; set; }

        public string Category { get; set; }

        public double Amount { get; set; }

        public DateTimeOffset Date { get; set; }
    }
}
