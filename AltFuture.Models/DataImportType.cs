using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltFuture.Models
{
    public class DataImportType
    {
        [Key]
        public int DataImportTypeId { get; set; }

        [StringLength(100)]
        public string DataImportTypeName { get; set; }
    }
}
