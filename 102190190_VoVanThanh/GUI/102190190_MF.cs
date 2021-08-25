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
            SetCBBSort();
        }

        private void SetCBBMonAn()
        {
            Data db = new Data();
            cbb_MonAn.Items.Add(new CBBItem { Value = 0, Text = "All" });
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

        private void SetCBBSort()
        {
            Data db = new Data();
            List<string> l = new List<string>();
            l.AddRange(new string[] { "TenNguyenLieu", "SoLuong", "DonViTinh", "TinhTrang"});
            int j = 0;
            foreach (var i in l)
            {
                cbb_Sort.Items.Add(new CBBItem
                {
                    Value = j++,
                    Text = i
                });
            }
            cbb_Sort.SelectedIndex = 0;
        }

        private void ShowDGV()
        {
            var l = from p in BLL_R.Instance.ViewAll(((CBBItem)cbb_MonAn.SelectedItem).Value, tb_Search.Text)
                    select p;
            dataGridView1.DataSource = l.ToList();
            dataGridView1.Columns["Ma"].Visible = false;
        }

        private void cbb_MonAn_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowDGV();
        }

        private void tb_Search_TextChanged(object sender, EventArgs e)
        {
            ShowDGV();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            _102190190_DF f = new _102190190_DF();
            if (cbb_MonAn.SelectedIndex == 0)
            {
                MessageBox.Show("Please choose one food!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                f.aoe("MMA: " + cbb_MonAn.SelectedIndex.ToString());
                f.reload = ShowDGV;
                f.Show();
            }
        }
        private void btn_Edit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Select one row to edit!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DataGridViewSelectedRowCollection r = dataGridView1.SelectedRows;
            if (r.Count == 1)
            {
                _102190190_DF f = new _102190190_DF();
                f.aoe("MMN: " + r[0].Cells["Ma"].Value.ToString());
                f.reload = ShowDGV;
                f.Show();
            }
            else
            {
                MessageBox.Show("Cannot edit many row!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Del_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection r = dataGridView1.SelectedRows;
            if (r.Count == 0)
            {
                MessageBox.Show("No rows are selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (DataGridViewRow i in r)
            {
                if (i.Cells["TinhTrang"].Value.ToString() == "True")
                {
                    MessageBox.Show("This item cannot be deleted!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    BLL_R.Instance.DeleteMANL(i.Cells["Ma"].Value.ToString());
                }
            }
            ShowDGV();
        }

        private void btn_Sort_Click(object sender, EventArgs e)
        {
            switch(((CBBItem)cbb_Sort.SelectedItem).Value)
            {
                case 0:
                    var l = from p in BLL_R.Instance.ViewAll(((CBBItem)cbb_MonAn.SelectedItem).Value, tb_Search.Text)
                            orderby p.TenNguyenLieu
                            select p;
                    dataGridView1.DataSource = l.ToList();
                    break;
                case 1:
                    l = from p in BLL_R.Instance.ViewAll(((CBBItem)cbb_MonAn.SelectedItem).Value, tb_Search.Text)
                            orderby p.SoLuong
                            select p;
                    dataGridView1.DataSource = l.ToList();
                    break;
                case 2:
                    l = from p in BLL_R.Instance.ViewAll(((CBBItem)cbb_MonAn.SelectedItem).Value, tb_Search.Text)
                        orderby p.DonViTinh
                            select p;
                    dataGridView1.DataSource = l.ToList();
                    break;
                case 3:
                    l = from p in BLL_R.Instance.ViewAll(((CBBItem)cbb_MonAn.SelectedItem).Value, tb_Search.Text)
                        orderby p.TinhTrang
                        select p;
                    dataGridView1.DataSource = l.ToList();
                    break;
            }
            dataGridView1.Columns["Ma"].Visible = false;
        }

        
    }
}
