using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooWooTech.AssetsTrade.Models
{
    [Table("UserManager")]
    public class User
    {
        [Key]
        public string ID { get; set; }

        [MaxLength(20)]
        public string Username { get; set; }

        [MaxLength(20)]
        public string Password { get; set; }
    }
}
