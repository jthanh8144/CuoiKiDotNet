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

        public List<NguyenLieu> GetListMANL(int ID, string Name)
        {
            List<NguyenLieu> list = new List<NguyenLieu>();
            if (ID == 0 && Name == "")
            {
                var l = from p in db.NguyenLieus select p;
                list = l.ToList();
            }
            else if (ID == 0 && Name != "")
            {
                var l = from p in db.NguyenLieus
                        where (p.TenNguyenLieu.ToUpper().Contains(Name.ToUpper())) 
                        select p;
                list = l.ToList();
            }
            else
            {
                if (Name == "")
                {
                    var l = from p in db.NguyenLieus
                            where p.MaNguyenLieu == ID
                            select p;
                    list = l.ToList();
                }
                else
                {
                    var l = from p in db.NguyenLieus
                            where (p.MaNguyenLieu == ID && p.TenNguyenLieu.ToUpper().Contains(Name.ToUpper()))
                            select p;
                    list = l.ToList();
                }
            }
            return list;
        }
    }
}
