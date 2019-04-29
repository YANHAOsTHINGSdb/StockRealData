using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooWooTech.AssetsTrade.Models
{
    [Table("ApiHost")]
    public class ApiHost
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string IPAddress { get; set; }

        public int Port { get; set; }

        public string Service { get; set; }

        [NotMapped]
        public long Ping { get; set; }
    }
}
