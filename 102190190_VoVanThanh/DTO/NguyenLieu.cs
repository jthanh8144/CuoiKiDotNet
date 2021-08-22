using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _102190190_VoVanThanh.DTO
{
    [Table("NguyenLieu")]
    public class NguyenLieu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaNguyenLieu { get; set; }
        public string TenNguyenLieu { get; set; }
        public bool TinhTrang { get; set; }
    }
}
