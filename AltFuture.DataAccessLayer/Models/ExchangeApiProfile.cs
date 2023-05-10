

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AltFuture.DataAccessLayer.Models
{
    public class ExchangeApiProfile
    {
        [Key]
        public int ExchangeApiProfileId { get; set; }

        [ForeignKey(nameof(AppUser))]
        public int AppUserId { get; set; }

        [ForeignKey(nameof(Exchange))]
        public int ExchangeId { get; set; }

        public string ProfileName { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }

        [DefaultValue(true)]
        public bool IsEnabled { get; set; } = true;
        public DateTime? LastSyncDate { get; set; }

        public Exchange Exchange { get; set; }
    }
}
