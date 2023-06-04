﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DOAN_Nhom4
{
    public partial class FrmThongTinTheHHBVisaPlatinumCashBack : Form
    {
        private NguoiDung kh;
        private TaiKhoanNganHang tknh;
        private Panel pnlNguoiDung;
        public FrmThongTinTheHHBVisaPlatinumCashBack()
        {
            InitializeComponent();
        }

        public FrmThongTinTheHHBVisaPlatinumCashBack(NguoiDung kh, TaiKhoanNganHang tknh, Panel pnlNguoiDung)
        {
            InitializeComponent();
            this.kh = kh;
            this.tknh = tknh;
            this.pnlNguoiDung = pnlNguoiDung;
        }

        private void pbQuayLai_Click(object sender, EventArgs e)
        {
            FrmDangKyTinDung frmDangKyTinDung = new FrmDangKyTinDung(kh, tknh, pnlNguoiDung);
            addForm(frmDangKyTinDung);
        }

        private void pbChuyenTrang_Click(object sender, EventArgs e)
        {
            FrmThongTinTheHBBVisaInfinite frmThongTinTheHBBVisaInfinite = new FrmThongTinTheHBBVisaInfinite(kh, tknh, pnlNguoiDung);
            addForm(frmThongTinTheHBBVisaInfinite);
        }

        private void addForm(Form form)
        {
            pnlNguoiDung.Controls.Clear();
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            form.FormBorderStyle = FormBorderStyle.None;
            pnlNguoiDung.Controls.Add(form);
            pnlNguoiDung.Tag = form;
            form.Show();
        }
    }
}
