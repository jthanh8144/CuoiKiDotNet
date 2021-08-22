using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _102190190_VoVanThanh.DTO
{
    [Table("MonAn")]
    public class MonAn
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaMonAn { get; set; }
        public string TenMonAn { get; set; }
    }
}
