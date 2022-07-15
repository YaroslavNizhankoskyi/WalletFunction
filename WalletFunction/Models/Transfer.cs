using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletFunction.Models
{
    internal class Transfer
    {
        public Guid Id { get; set; }

        public Guid WalletId { get; set; }

        public string Category { get; set; }

        public double Amount { get; set; }
    }
}

