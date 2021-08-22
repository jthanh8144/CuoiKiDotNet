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
            cbb_TenNL.SelectedIndex = 0;
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
            cbb_TinhTrang.SelectedIndex = 0;
        }

        private void _102190190_DF_Load(object sender, EventArgs e)
        {
            if (Ma == "MMA")
            {
                cbb_TenNL.SelectedIndex = -1;
            }
            else
            {

            }
        }

        private MonAn_NguyenLieu getInfo()
        {
            MonAn_NguyenLieu s = new MonAn_NguyenLieu();
            s.Ma = tb_Ma.Text;
            s.NguyenLieu = BLL_R.Instance.GetNLByID(((CBBItem)cbb_TenNL.SelectedItem).Value);
            s.SoLuong = Convert.ToInt32(tb_SL.Text);
            s.DonViTinh = ((CBBItem)cbb_DVT.SelectedItem).Text.ToString();
            if (((CBBItem)cbb_TinhTrang.SelectedItem).Value == 0)
            {
                s.NguyenLieu.TinhTrang = true;
            }
            else
            {
                s.NguyenLieu.TinhTrang = false;
            }
            s.MonAn = BLL_R.Instance.GetMAByID(Convert.ToInt32(Ma.Substring(3, Ma.Length - 4)));
            return s;
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
            }
            BLL_R.Instance.AddMANL(getInfo());
            status = true;
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (Ma == "MMA")
            {
                AddMANL();
            }
            else
            {

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
    }
}
