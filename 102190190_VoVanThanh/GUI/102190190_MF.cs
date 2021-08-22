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
    public partial class _102190190_MF : Form
    {
        public _102190190_MF()
        {
            InitializeComponent();
            SetCBBMonAn();
        }

        private void SetCBBMonAn()
        {
            Data db = new Data();
            cbb_MonAn.Items.Add(new CBBItem { Value = 0, Text = "Tất cả" });
            foreach (MonAn i in db.MonAns)
            {
                cbb_MonAn.Items.Add(new CBBItem
                {
                    Value = i.MaMonAn,
                    Text = i.TenMonAn
                });
            }
            cbb_MonAn.SelectedIndex = 0;
        }


    }
}
