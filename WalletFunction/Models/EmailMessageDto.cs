using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletFunction.Models
{
    internal class EmailMessageDto
    {
        public int LeftAmount { get; set; }

        public string UserEmail { get; set; }

        public string WalletName { get; set; }
    }
}
