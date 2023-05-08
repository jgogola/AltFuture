using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltFuture.DataAccessLayer.Models
{
    public class ExchangeApi
    {
        public int ExchangeApiId { get; set; }
        public string? ProfileName { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }

        public bool IsEnabled { get; set; }

        public int AppUserId { get; set; }
        public int ExchangeId { get; set; }
        public Exchange Exchange { get; set; }
    }
}
