using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _102190190_VoVanThanh.DTO;
using _102190190_VoVanThanh.BLL;

namespace _102190190_VoVanThanh.GUI
{
    public partial class _102190190_DF : Form
    {
        public delegate void AddOrEdit(string Ma);
        public AddOrEdit aoe;
        public delegate void ReLoadData();
        public ReLoadData reload;

        private static string Ma = "";
        private static bool status = true;

        public void getEdit(string Ma)
        {
            _102190190_DF.Ma = Ma;
        }

        public _102190190_DF()
        {
            aoe = getEdit;
            InitializeComponent();
            SetCBBNguyenLieu();
            SetCBBDVT();
            SetCBBTinhTrang();
            cbb_TinhTrang.Enabled = false;
        }

        private void SetCBBNguyenLieu()
        {
            Data db = new Data();
            foreach (NguyenLieu i in db.NguyenLieus)
            {
                cbb_TenNL.Items.Add(new CBBItem
                {
                    Value = i.MaNguyenLieu,
                    Text = i.TenNguyenLieu
                });
            }
        }

        private void SetCBBDVT()
        {
            Data db = new Data();
            List<string> dvt = new List<string>();
            foreach (MonAn_NguyenLieu i in db.MonAn_NguyenLieus)
            {
                if (Array.IndexOf(dvt.ToArray(), i.DonViTinh) <= -1)
                {
                    dvt.Add(i.DonViTinh);
                }
            }
            for (int j = 0; j < dvt.Count; j++)
            {
                cbb_DVT.Items.Add(new CBBItem
                {
                    Value = j,
                    Text = dvt[j].ToString()
                });
            }
            cbb_DVT.SelectedIndex = 0;
        }

        private void SetCBBTinhTrang()
        {
            Data db = new Data();
            cbb_TinhTrang.Items.AddRange(new CBBItem[]
            {
                new CBBItem { Value = 0, Text = "Đã nhập hàng" },
                new CBBItem { Value = 1, Text = "Chưa nhập hàng" }
            });
        }

        private void AddMANL()
        {
            Data db = new Data();
            if (tb_Ma.Text == "")
            {
                MessageBox.Show("Vui lòng nhập Mã!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                status = false;
                return;
            }
            foreach (MonAn_NguyenLieu i in db.MonAn_NguyenLieus)
            {
                if (tb_Ma.Text == i.Ma.ToString())
                {
                    MessageBox.Show("Mã đã tồn tại!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    status = false;
                    return;
                }
                if (i.MaMonAn == Convert.ToInt32(Ma.Substring(5)) 
                    && i.MaNguyenLieu == ((CBBItem)cbb_TenNL.SelectedItem).Value)
                {
                    MessageBox.Show("Nguyên liệu đã tồn tại trong món ăn!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    status = false;
                    return;
                }
            }
            string SoLuong = tb_SL.Text;
            for (int i = 0; i < SoLuong.Length; i++)
            {
                if (SoLuong[i] < '0' || SoLuong[i] > '9')
                {
                    MessageBox.Show("Id sản phẩm không chứa kí tự", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    status = false;
                    return;
                }
            }
            MonAn_NguyenLieu s = new MonAn_NguyenLieu();
            s.Ma = tb_Ma.Text;
            s.SoLuong = Convert.ToInt32(SoLuong);
            s.DonViTinh = ((CBBItem)cbb_DVT.SelectedItem).Text.ToString();
            s.MaNguyenLieu = ((CBBItem)cbb_TenNL.SelectedItem).Value;
            s.MaMonAn = Convert.ToInt32(Ma.Substring(5));
            BLL_R.Instance.AddMANL(s);
            status = true;
        }

        private void EditMANL()
        {
            Data db = new Data();
            foreach (MonAn_NguyenLieu i in db.MonAn_NguyenLieus)
            {
                //Convert.ToInt32(
                if (i.MaMonAn == BLL_R.Instance.GetMANLByID(Ma.Substring(5)).MaMonAn
                    && i.MaNguyenLieu == ((CBBItem)cbb_TenNL.SelectedItem).Value)
                {
                    MessageBox.Show("Nguyên liệu đã tồn tại trong món ăn!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    status = false;
                    return;
                }
            }
            string SoLuong = tb_SL.Text;
            for (int i = 0; i < SoLuong.Length; i++)
            {
                if (SoLuong[i] < '0' || SoLuong[i] > '9')
                {
                    MessageBox.Show("Id sản phẩm không chứa kí tự", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    status = false;
                    return;
                }
            }
            MonAn_NguyenLieu s = new MonAn_NguyenLieu();
            s.Ma = tb_Ma.Text;
            s.SoLuong = Convert.ToInt32(SoLuong);
            s.DonViTinh = ((CBBItem)cbb_DVT.SelectedItem).Text.ToString();
            s.MaNguyenLieu = ((CBBItem)cbb_TenNL.SelectedItem).Value;
            s.MaMonAn = BLL_R.Instance.GetMANLByID(tb_Ma.Text).MaMonAn;
            if (((CBBItem)cbb_TinhTrang.SelectedItem).Value == 0)
            {
                BLL_R.Instance.EditMANL(s, true);
            }
            else
            {
                BLL_R.Instance.EditMANL(s, false);
            }
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (Ma.Substring(0, 3) == "MMA")
            {
                AddMANL();
            }
            else
            {
                EditMANL();
            }
            if (status == true)
            {
                reload();
                Dispose();
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void _102190190_DF_Load(object sender, EventArgs e)
        {
            if (Ma.Substring(0, 3) == "MMN")
            {
                MonAn_NguyenLieu m = BLL_R.Instance.GetMANLByID(Ma.Substring(5));
                cbb_TinhTrang.Enabled = true;
                tb_Ma.Enabled = false;
                tb_Ma.Text = m.Ma;
                for (int i = 0; i < cbb_TenNL.Items.Count; i++)
                {
                    if (m.MaNguyenLieu == ((CBBItem)cbb_TenNL.Items[i]).Value)
                    {
                        cbb_TenNL.SelectedIndex = i;
                        break;
                    }
                }
                tb_SL.Text = m.SoLuong.ToString();
                for (int i = 0; i < cbb_DVT.Items.Count; i++)
                {
                    if (m.DonViTinh == ((CBBItem)cbb_DVT.Items[i]).Text)
                    {
                        cbb_DVT.SelectedIndex = i;
                        break;
                    }
                }
                bool stt = BLL_R.Instance.GetNLByID(m.MaNguyenLieu).TinhTrang;
                if (Convert.ToInt32(stt) == 1)
                {
                    cbb_TinhTrang.SelectedIndex = 0;
                }
                else
                {
                    cbb_TinhTrang.SelectedIndex = 1;
                }
            }
        }
    }
}
