using System;
using System.Data.Entity;
using System.Linq;

namespace _102190190_VoVanThanh.DTO
{
    public class Data : DbContext
    {
        public Data()
            : base("name=Data")
        {
            Database.SetInitializer<Data>(new CreateDB());
        }

        public virtual DbSet<MonAn> MonAns { get; set; }
        public virtual DbSet<NguyenLieu> NguyenLieus { get; set; }
        public virtual DbSet<MonAn_NguyenLieu> MonAn_NguyenLieus { get; set; }
    }
}