using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DOAN_QLKhachSan
{
    public partial class QLPhong : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-29HM56H\SQLEXPRESS02;Initial Catalog=QUANLY_KHACHSACN_LAN1;Integrated Security=True");
        DataSet ds_QLPhong = new DataSet();
        SqlDataAdapter da_Phong;
        public int flag = 0;
        public int flagl = 0;
        public QLPhong()
        {
            InitializeComponent();
            string strSelect = "select * from PHONG";
            da_Phong = new SqlDataAdapter(strSelect, conn);
        }

        private void QLPhong_Load(object sender, EventArgs e)
        {
            load_database_gridviewLP();
            load_database_gridview();
            load_cbo_loai();
            foreach (Control item in DataGridView1.Controls)
            {
                if (item.GetType() == typeof(TextBox) || item.GetType() ==
                typeof(ComboBox) || item.GetType() == typeof(MaskedTextBox))

                    item.Enabled = false;

            }
            foreach (Control item2 in DataGridView2.Controls)
            {
                if (item2.GetType() == typeof(TextBox) || item2.GetType() ==
                typeof(ComboBox) || item2.GetType() == typeof(MaskedTextBox))

                    item2.Enabled = false;

            }
            Databingdings(ds_QLPhong.Tables["PHONG"]);
            Databingdings_Loai(ds_QLLoai.Tables["LOAIPHONG"]);
            btn_xoa.Enabled = btn_luu.Enabled = false;
        }
        /*---------------Phòng--------------------*/
        public void load_database_gridview()
        {
            da_Phong.Fill(ds_QLPhong, "PHONG");
            DataGridView1.DataSource = ds_QLPhong.Tables["PHONG"];
        }
        public void load_cbo_loai()
        {
            da_Loai.Fill(ds_QLLoai, "LOAIPHONG");
            cbo_loai.DataSource = ds_QLLoai.Tables["LOAIPHONG"];
            cbo_loai.DisplayMember = "TENLOAI_P";
            cbo_loai.ValueMember = "MALOAI_P";
        }
        public void Databingdings(DataTable pDT)
        {
            txt_mp.DataBindings.Clear();
            txt_tenP.DataBindings.Clear();
            txt_tt.DataBindings.Clear();
            txt_sok.DataBindings.Clear();
            cbo_loai.DataBindings.Clear();

            txt_mp.DataBindings.Add("Text", pDT, "MA_P");
            txt_tenP.DataBindings.Add("Text", pDT, "TENPHONG");
            txt_tt.DataBindings.Add("Text", pDT, "TINHTRANG_P");
            txt_sok.DataBindings.Add("Text", pDT, "SOKHACHTOIDA");
            cbo_loai.DataBindings.Add("Text", pDT, "MALOAI_P");
        }

        private void btn_them_Click_1(object sender, EventArgs e)
        {
            btn_luu.Enabled = true;
            btn_them.Enabled = false;
            DataGridView1.ReadOnly = false;
            DataGridView1.AllowUserToAddRows = true;
            for (int i = 0; i < DataGridView1.Rows.Count - 1; i++)
            {
                DataGridView1.Rows[i].ReadOnly = true;
            }
            DataGridView1.FirstDisplayedScrollingRowIndex = DataGridView1.Rows.Count - 1;
            flag = 1;
        }

        private void btn_luu_Click_1(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                SqlCommandBuilder cmb = new SqlCommandBuilder(da_Phong);
                da_Phong.Update(ds_QLPhong, "PHONG");
                Databingdings(ds_QLPhong.Tables["PHONG"]);
                MessageBox.Show("Thành công");
                btn_luu.Enabled = false;
                btn_them.Enabled = true;
                flag = 0;
            }
        }
        /*---------------Loại phòng--------------------*/
        DataSet ds_QLLoai;
        SqlDataAdapter da_Loai;
        public void load_database_gridviewLP()
        {
            ds_QLLoai = new DataSet();
            string strSelect1 = "select * from LOAIPHONG";
            da_Loai = new SqlDataAdapter(strSelect1, conn);
            da_Loai.Fill(ds_QLLoai, "LOAIPHONG");
            DataGridView2.DataSource = ds_QLLoai.Tables["LOAIPHONG"];
            Databingdings_Loai(ds_QLLoai.Tables["LOAIPHONG"]);
        }
        public void Databingdings_Loai(DataTable pDT)
        {
            txt_maloai.DataBindings.Clear();
            txt_tenloai.DataBindings.Clear();
            txt_gia.DataBindings.Clear();


            txt_maloai.DataBindings.Add("Text", pDT, "MALOAI_P");
            txt_tenloai.DataBindings.Add("Text", pDT, "TENLOAI_P");
            txt_gia.DataBindings.Add("Text", pDT, "GIA");
        }

        private void btn_themloai_Click(object sender, EventArgs e)
        {
            btn_luuloai.Enabled = true;
            btn_themloai.Enabled = false;
            DataGridView2.ReadOnly = false;
            DataGridView2.AllowUserToAddRows = true;
            for (int i = 0; i < DataGridView2.Rows.Count - 1; i++)
            {
                DataGridView2.Rows[i].ReadOnly = true;
            }
            DataGridView2.FirstDisplayedScrollingRowIndex = DataGridView2.Rows.Count - 1;
            flagl = 1;
        }

        private void btn_luuloai_Click(object sender, EventArgs e)
        {
            if (flagl == 1)
            {
                SqlCommandBuilder cmb = new SqlCommandBuilder(da_Loai);
                da_Loai.Update(ds_QLLoai, "LOAIPHONG");
                Databingdings_Loai(ds_QLLoai.Tables["LOAIPHONG"]);
                MessageBox.Show("Thành công");
                btn_luuloai.Enabled = false;
                btn_themloai.Enabled = true;
                flagl = 0;
            }
        }

        private void btn_xoaloai_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string deleteString = "DELETE LOAIPHONG WHERE MALOAI_P='" + txt_maloai.Text + "'";
                SqlCommand cmd = new SqlCommand(deleteString, conn);
                cmd.ExecuteNonQuery();

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                load_database_gridviewLP();
                MessageBox.Show("Thành Công");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
