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
    public partial class FrmTienIchTinDung : Form
    {
        private NguoiDung kh;
        private TaiKhoanNganHang tknh;
        private Panel pnlNguoiDung;
        private ThongTinTinDung tttd;
        private ThongTinTinDungDAO tttdDAO = new ThongTinTinDungDAO();
        public FrmTienIchTinDung()
        {
            InitializeComponent();
        }

        public FrmTienIchTinDung(NguoiDung kh, TaiKhoanNganHang tknh, Panel pnlNguoiDung)
        {
            InitializeComponent();
            this.kh = kh;
            this.tknh = tknh;
            this.pnlNguoiDung = pnlNguoiDung;
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            tttd = tttdDAO.LayThongTinTinDung("CCCD", kh.Cccd);
            if (tttd == null)
            {
                FrmDangKyTinDung frmDangKyTinDung = new FrmDangKyTinDung(kh, tknh, pnlNguoiDung);
                Utility.addForm(frmDangKyTinDung, pnlNguoiDung);
            }
            else
            {
                MessageBox.Show("Bạn đã có tài khoản tín dụng!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThongTinThe_Click(object sender, EventArgs e)
        {
            tttd = tttdDAO.LayThongTinTinDung("CCCD", kh.Cccd);
            if (tttd != null)
            {
                FrmThongTinNguoiDungTinDung frmThongTinTaiKhoangTinDung = new FrmThongTinNguoiDungTinDung(kh, tknh, pnlNguoiDung, tttd);
                Utility.addForm(frmThongTinTaiKhoangTinDung, pnlNguoiDung);
            }
            else
                MessageBox.Show("Bạn không có tài khoản tín dụng nào!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
        }
    }
}
