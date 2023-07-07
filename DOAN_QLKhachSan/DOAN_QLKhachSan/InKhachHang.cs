//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace DOAN_QLKhachSan
//{
//    public partial class InKhachHang : Form
//    {
//        public InKhachHang()
//        {
//            InitializeComponent();
//        }

//        private void InKhachHang_Load(object sender, EventArgs e)
//        {
//            CrystalReport1 rpt = new CrystalReport1();
//            crystalReportViewer1.ReportSource = rpt;
//            rpt.SetDatabaseLogon("sa", "123", @"LAPTOP-Q2F990SF\MSSQLSERVER01", "QUANLY_KHACHSACN_LAN1");
//            crystalReportViewer1.DisplayStatusBar = false;
//            crystalReportViewer1.DisplayToolbar = true;
//            crystalReportViewer1.Refresh();
//        }
//    }
//}
