﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace DOAN_Nhom4
{
    public partial class NapTien : UserControl
    {
        public NapTien()
        {
            InitializeComponent();
        }
        TaiKhoanNganHangDAO tknhDAO = new TaiKhoanNganHangDAO();
        NguoiDungDAO ndDAO = new NguoiDungDAO();
        DBConnection db = new DBConnection();
        LichSuGiaoDichDAO lsgdDAO = new LichSuGiaoDichDAO();

        public void HienThiThongTin()
        {
            NguoiDung ngDung = new NguoiDung();
            TaiKhoanNganHang tk = new TaiKhoanNganHang();
            ngDung = ndDAO.LayKhachHang("SoTK", txtSoTK.Text, "TenNH", "HHB");
            tk = tknhDAO.LayTaiKhoanNganHang("SoTK", txtSoTK.Text, "TenNH", "HHB");
            if (ngDung != null)
            {
                txtTenTK.Text = ngDung.TenTK.ToString();
                txtCCCD.Text = ngDung.Cccd.ToString();
                txtSDT.Text = ngDung.Sdt.ToString();
                lblSoDu.Text = tk.SoDu.ToString("N0") + " VNĐ";
            }
            else
            {
                txtTenTK.Text = "Khong ton tai";
                txtCCCD.Text = "";
                txtSDT.Text = "";
                lblSoDu.Text = "-";
            }
        }

        private void btn_NapTien_Click(object sender, EventArgs e)
        {
            TaiKhoanNganHang tkNap = new TaiKhoanNganHang();
            tkNap = tknhDAO.LayTaiKhoanNganHang("SoTK", txtSoTK.Text, "TenNH", "HHB");
            NguoiDung ndNap = new NguoiDung();
            ndNap = ndDAO.LayKhachHang("SoTK", txtSoTK.Text, "TenNH", "HHB");
            GiaoDich gd = new GiaoDich("Nap Tien", "Tien Mat", "", "", tkNap.TenNH, ndNap.TenTK, tkNap.SoTK, decimal.Parse(txtSoTien.Text), "Nap tien vao tai khoan");
            if (tkNap != null)
            {
                if (gd.SoTien >= 100000)
                {
                    tknhDAO.NapTien(tkNap, gd);
                    lsgdDAO.Them(gd);
                    txtSoTK.Text = "";
                    txtSoTien.Text = "";
                    txtCCCD.Text = "";
                    txtSDT.Text = "";
                    MessageBox.Show("Nạp tiền thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Số tiền không hợp lệ", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("Số tài khoản không đúng", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void btn_HuyBo_Click(object sender, EventArgs e)
        {
            txtSoTK.Text = "";
            txtSoTien.Text = "";
            txtCCCD.Text = "";
            txtSDT.Text = "";
            txtTenTK.Text = "";
            lblSoDu.Text = "-";
        }

        private void txt_SoTK_TextChanged(object sender, EventArgs e)
        {
            HienThiThongTin();
        }
    }
}
