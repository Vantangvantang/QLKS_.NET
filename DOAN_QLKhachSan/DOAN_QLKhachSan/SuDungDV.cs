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
    public partial class SuDungDV : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-29HM56H\SQLEXPRESS02;Initial Catalog=QUANLY_KHACHSACN_LAN1;Integrated Security=True");
        DataSet ds_QLDV;
        SqlDataAdapter da_SUDUNGDV;
        DataSet ds_MDP;
        SqlDataAdapter da_MDP;
        DataSet ds_DV;
        SqlDataAdapter da_DV;
        public int flag = 0;
        public SuDungDV()
        {
            InitializeComponent();
        }
        public void load_database_gridview()
        {
            ds_QLDV = new DataSet();
            string strSelect = "select * from SUDUNGDV";
            da_SUDUNGDV = new SqlDataAdapter(strSelect, conn);
            da_SUDUNGDV.Fill(ds_QLDV, "SUDUNGDV");
            DataGridView1.DataSource = ds_QLDV.Tables["SUDUNGDV"];
        }
        public void Databingdings(DataTable pDT)
        {
            txt_ma.DataBindings.Clear();
            cbo_maPTP.DataBindings.Clear();
            cbo_soluong.DataBindings.Clear();
            cbo_tendv.DataBindings.Clear();
            txt_ngay.DataBindings.Clear();
            txt_tongt.DataBindings.Clear();
            txt_tinhtrang.DataBindings.Clear();

            txt_ma.DataBindings.Add("Text", pDT, "MA_SD");
            cbo_maPTP.DataBindings.Add("Text", pDT, "MA_PTP");
            cbo_soluong.DataBindings.Add("Text", pDT, "SOLUONG");
            cbo_tendv.DataBindings.Add("Text", pDT, "MA_DV");
            txt_ngay.DataBindings.Add("Text", pDT, "NGAYSUDUNG");
            txt_tongt.DataBindings.Add("Text", pDT, "TONGTIEN_DV");
            txt_tinhtrang.DataBindings.Add("Text", pDT, "TINHTRANG_DV");
        }

        private void btn_them_Click(object sender, EventArgs e)
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

        private void btn_luu_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                SqlCommandBuilder cmb = new SqlCommandBuilder(da_SUDUNGDV);
                da_SUDUNGDV.Update(ds_QLDV, "SUDUNGDV");
                Databingdings(ds_QLDV.Tables["SUDUNGDV"]);
                MessageBox.Show("Thành công");
                load_database_gridview();
                btn_luu.Enabled = false;
                btn_them.Enabled = true;
                flag = 0;

                //load_database_gridview();
            }
        }

        private void SuDungDV_Load(object sender, EventArgs e)
        {
            load_cbo_MDP();
            load_cbo_soluong();
            load_cbo_DV();
            load_database_gridview_DV();
            load_database_gridview();
            DataGridView1.ReadOnly = true;
            DataGridView1.AllowUserToAddRows = false;
            foreach (Control item in DataGridView1.Controls)
            {
                if (item.GetType() == typeof(TextBox) || item.GetType() ==
                typeof(ComboBox) || item.GetType() == typeof(MaskedTextBox))

                    item.Enabled = false;

            }
            btn_xoa.Enabled = btn_luu.Enabled = false;
            Databingdings(ds_QLDV.Tables["SUDUNGDV"]);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        public void load_cbo_MDP()
        {
            ds_MDP = new DataSet();
            string strSelect = "select * from PHIEUTHUEPHONG";
            da_MDP = new SqlDataAdapter(strSelect, conn);
            da_MDP.Fill(ds_MDP, "PHIEUTHUEPHONG");
            cbo_maPTP.DataSource = ds_MDP.Tables["PHIEUTHUEPHONG"];
            cbo_maPTP.DisplayMember = "MA_PTP";
            cbo_maPTP.ValueMember = "MA_PTP";
        }
        public void load_cbo_soluong()
        {
            cbo_soluong.Items.Add(1);
            cbo_soluong.Items.Add(2);
            cbo_soluong.Items.Add(3);
            cbo_soluong.Items.Add(4);
            cbo_soluong.Items.Add(5);
        }
        public void load_cbo_DV()
        {
            ds_DV = new DataSet();
            string strSelect = "select * from DICHVU";
            da_DV = new SqlDataAdapter(strSelect, conn);
            da_DV.Fill(ds_DV, "DICHVU");
            cbo_tendv.DataSource = ds_DV.Tables["DICHVU"];
            cbo_tendv.DisplayMember = "MA_DV";
            cbo_tendv.ValueMember = "MA_DV";
        }
        public void load_database_gridview_DV()
        {
            ds_DV = new DataSet();
            string strSelect = "select * from DICHVU";
            da_DV = new SqlDataAdapter(strSelect, conn);
            da_DV.Fill(ds_DV, "DICHVU");
            DataGridView2.DataSource = ds_DV.Tables["DICHVU"];
        }
    }
}
