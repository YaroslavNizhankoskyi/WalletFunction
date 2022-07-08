using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletFunction.Models
{
    public class WalletDto
    {
        public string Name { get; set; }

        public double Amount { get; set; }

        public Guid UserId { get; set; }
    }
}
