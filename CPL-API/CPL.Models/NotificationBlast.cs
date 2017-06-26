using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPL.Models
{
    public class NotificationBlast
    {
        public int NotificationBlastId { get; set; }
        public string CoinTicker { get; set; }
        public string CoinName { get; set; }
        public DateTime SentDate { get; set; }
    }
}
