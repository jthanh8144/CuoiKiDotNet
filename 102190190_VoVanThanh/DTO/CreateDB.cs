using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _102190190_VoVanThanh.DTO
{
    class CreateDB :
        DropCreateDatabaseIfModelChanges<Data>
    {
        protected override void Seed(Data context)
        {
            context.MonAns.AddRange(new MonAn[]
            {
                new MonAn { MaMonAn = 1, TenMonAn = "Com ga" },
                new MonAn { MaMonAn = 2, TenMonAn = "Canh xuong" },
                new MonAn { MaMonAn = 3, TenMonAn = "Ca kho" },
            });
            context.NguyenLieus.AddRange(new NguyenLieu[]
            {
                new NguyenLieu { MaNguyenLieu = 1, TenNguyenLieu = "Gao", TinhTrang = true },
                new NguyenLieu { MaNguyenLieu = 2, TenNguyenLieu = "Xuong heo", TinhTrang = false },
                new NguyenLieu { MaNguyenLieu = 3, TenNguyenLieu = "Ca ngu", TinhTrang = true }
            });
            context.MonAn_NguyenLieus.AddRange(new MonAn_NguyenLieu[]
            {
                new MonAn_NguyenLieu { Ma = "Mon01", SoLuong = 10, DonViTinh = "Lon", MaMonAn = 1, MaNguyenLieu = 1 },
                new MonAn_NguyenLieu { Ma = "Mon02", SoLuong = 20, DonViTinh = "Kg", MaMonAn = 2, MaNguyenLieu = 2 },
                new MonAn_NguyenLieu { Ma = "Mon03", SoLuong = 30, DonViTinh = "Con", MaMonAn = 3, MaNguyenLieu = 3 }
            });
        }
    }
}
