using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _102190190_VoVanThanh.DTO;

namespace _102190190_VoVanThanh.BLL
{
    class BLL_R
    {
        private Data db = new Data();
        public static BLL_R Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_R();
                }
                return _Instance;
            }
            private set
            {

            }
        }
        private static BLL_R _Instance;

        public List<MonAn_NguyenLieu> GetListMANL(int ID, string Name)
        {
            List<MonAn_NguyenLieu> list = new List<MonAn_NguyenLieu>();
            if (ID == 0 && Name == "")
            {
                var l = from p in db.MonAn_NguyenLieus select p;
                list = l.ToList();
            }
            else if (ID == 0 && Name != "")
            {
                var l = from p in db.MonAn_NguyenLieus
                        where ((p.NguyenLieu.TenNguyenLieu + p.SoLuong.ToString() + p.DonViTinh).ToUpper().Contains(Name.ToUpper())) 
                        select p;
                list = l.ToList();
            }
            else
            {
                if (Name == "")
                {
                    var l = from p in db.MonAn_NguyenLieus
                            where p.MaMonAn == ID
                            select p;
                    list = l.ToList();
                }
                else
                {
                    var l = from p in db.MonAn_NguyenLieus
                            where (p.MaMonAn == ID && (p.NguyenLieu.TenNguyenLieu + p.SoLuong.ToString() + p.DonViTinh).ToUpper().Contains(Name.ToUpper()))
                            select p;
                    list = l.ToList();
                }
            }
            return list;
        }

        public List<View> ViewAll(int ID, string Name)
        {
            List<View> list = new List<View>();
            List<MonAn_NguyenLieu> l = GetListMANL(ID, Name);
            for (int i = 0; i < l.Count; i++)
            {
                View v = new View();
                v.STT = i + 1;
                v.TenNguyenLieu = l[i].NguyenLieu.TenNguyenLieu;
                v.SoLuong = l[i].SoLuong;
                v.DonViTinh = l[i].DonViTinh;
                v.TinhTrang = l[i].NguyenLieu.TinhTrang;
                v.Ma = l[i].Ma;
                list.Add(v);
            }
            return list;
        }

        public MonAn_NguyenLieu GetMANLByID(string Ma)
        {
            var m = db.MonAn_NguyenLieus.Where(p => p.Ma == Ma).FirstOrDefault();
            return m;
        }

        public NguyenLieu GetNLByID(int ID)
        {
            var m = db.NguyenLieus.Where(p => p.MaNguyenLieu == ID).FirstOrDefault();
            return m;
        }

        public MonAn GetMAByID(int ID)
        {
            var m = db.MonAns.Where(p => p.MaMonAn == ID).FirstOrDefault();
            return m;
        }

        public void AddMANL(MonAn_NguyenLieu n)
        {
            db.MonAn_NguyenLieus.Add(n);
            db.SaveChanges();
        }

        public void EditMANL(MonAn_NguyenLieu s, bool TinhTrang)
        {
            var m = db.MonAn_NguyenLieus.Where(p => p.Ma == s.Ma).FirstOrDefault();
            m.SoLuong = s.SoLuong;
            m.DonViTinh = s.DonViTinh;
            m.MaMonAn = s.MaMonAn;
            m.MaNguyenLieu = s.MaNguyenLieu;
            GetNLByID(m.MaNguyenLieu).TinhTrang = TinhTrang;
            db.SaveChanges();
        }

        public void DeleteMANL(string ID)
        {
            db.MonAn_NguyenLieus.Remove(GetMANLByID(ID));
            db.SaveChanges();
        }
    }
}
