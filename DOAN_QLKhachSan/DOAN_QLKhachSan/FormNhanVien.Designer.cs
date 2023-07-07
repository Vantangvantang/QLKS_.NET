namespace DOAN_QLKhachSan
{
    partial class FormNhanVien
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_DatPhong = new System.Windows.Forms.Button();
            this.btn_SDDV = new System.Windows.Forms.Button();
            this.btn_KhachHang = new System.Windows.Forms.Button();
            this.btn_ThanhToan = new System.Windows.Forms.Button();
            this.panel_Menu = new System.Windows.Forms.Panel();
            this.panel_Body = new System.Windows.Forms.Panel();
            this.panel_Menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_DatPhong
            // 
            this.btn_DatPhong.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DatPhong.Location = new System.Drawing.Point(12, 12);
            this.btn_DatPhong.Margin = new System.Windows.Forms.Padding(4);
            this.btn_DatPhong.Name = "btn_DatPhong";
            this.btn_DatPhong.Size = new System.Drawing.Size(194, 72);
            this.btn_DatPhong.TabIndex = 0;
            this.btn_DatPhong.Text = "Đặt phòng";
            this.btn_DatPhong.UseVisualStyleBackColor = true;
            this.btn_DatPhong.Click += new System.EventHandler(this.btn_DatPhong_Click);
            // 
            // btn_SDDV
            // 
            this.btn_SDDV.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_SDDV.Location = new System.Drawing.Point(12, 92);
            this.btn_SDDV.Margin = new System.Windows.Forms.Padding(4);
            this.btn_SDDV.Name = "btn_SDDV";
            this.btn_SDDV.Size = new System.Drawing.Size(194, 70);
            this.btn_SDDV.TabIndex = 1;
            this.btn_SDDV.Text = "Sử dụng dịch vụ";
            this.btn_SDDV.UseVisualStyleBackColor = true;
            this.btn_SDDV.Click += new System.EventHandler(this.btn_SDDV_Click);
            // 
            // btn_KhachHang
            // 
            this.btn_KhachHang.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_KhachHang.Location = new System.Drawing.Point(8, 248);
            this.btn_KhachHang.Margin = new System.Windows.Forms.Padding(4);
            this.btn_KhachHang.Name = "btn_KhachHang";
            this.btn_KhachHang.Size = new System.Drawing.Size(198, 72);
            this.btn_KhachHang.TabIndex = 3;
            this.btn_KhachHang.Text = "Khách hàng";
            this.btn_KhachHang.UseVisualStyleBackColor = true;
            this.btn_KhachHang.Click += new System.EventHandler(this.btn_KhachHang_Click);
            // 
            // btn_ThanhToan
            // 
            this.btn_ThanhToan.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ThanhToan.Location = new System.Drawing.Point(8, 170);
            this.btn_ThanhToan.Margin = new System.Windows.Forms.Padding(4);
            this.btn_ThanhToan.Name = "btn_ThanhToan";
            this.btn_ThanhToan.Size = new System.Drawing.Size(198, 70);
            this.btn_ThanhToan.TabIndex = 7;
            this.btn_ThanhToan.Text = "Thanh toán";
            this.btn_ThanhToan.UseVisualStyleBackColor = true;
            this.btn_ThanhToan.Click += new System.EventHandler(this.btn_ThanhToan_Click);
            // 
            // panel_Menu
            // 
            this.panel_Menu.Controls.Add(this.btn_DatPhong);
            this.panel_Menu.Controls.Add(this.btn_ThanhToan);
            this.panel_Menu.Controls.Add(this.btn_SDDV);
            this.panel_Menu.Controls.Add(this.btn_KhachHang);
            this.panel_Menu.Location = new System.Drawing.Point(-3, 1);
            this.panel_Menu.Margin = new System.Windows.Forms.Padding(4);
            this.panel_Menu.Name = "panel_Menu";
            this.panel_Menu.Size = new System.Drawing.Size(206, 1001);
            this.panel_Menu.TabIndex = 8;
            // 
            // panel_Body
            // 
            this.panel_Body.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.panel_Body.BackgroundImage = global::DOAN_QLKhachSan.Properties.Resources.xep_hang_khach_san_2;
            this.panel_Body.Location = new System.Drawing.Point(211, 5);
            this.panel_Body.Margin = new System.Windows.Forms.Padding(4);
            this.panel_Body.Name = "panel_Body";
            this.panel_Body.Size = new System.Drawing.Size(1435, 815);
            this.panel_Body.TabIndex = 9;
            // 
            // FormNhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1604, 833);
            this.Controls.Add(this.panel_Body);
            this.Controls.Add(this.panel_Menu);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormNhanVien";
            this.Text = "Nhân viên";
            this.Load += new System.EventHandler(this.FormNhanVien_Load);
            this.panel_Menu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_DatPhong;
        private System.Windows.Forms.Button btn_SDDV;
        private System.Windows.Forms.Button btn_KhachHang;
        private System.Windows.Forms.Button btn_ThanhToan;
        private System.Windows.Forms.Panel panel_Menu;
        private System.Windows.Forms.Panel panel_Body;
    }
}