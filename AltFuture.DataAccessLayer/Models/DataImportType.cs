using System.ComponentModel.DataAnnotations;

namespace AltFuture.DataAccessLayer.Models
{
    public class DataImportType
    {
        [Key]
        public int DataImportTypeId { get; set; }

        [StringLength(100)]
        public string DataImportTypeName { get; set; }
    }
}
