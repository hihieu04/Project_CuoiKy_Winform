﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DOAN_Nhom4
{
    public partial class RutTien : UserControl
    {
        public RutTien()
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

        private void txt_SoTK_TextChanged(object sender, EventArgs e)
        {
            HienThiThongTin();
        }

        private void btn_RutTien_Click(object sender, EventArgs e)
        {
            TaiKhoanNganHang tkRut = new TaiKhoanNganHang();
            tkRut = tknhDAO.LayTaiKhoanNganHang("SoTK", txtSoTK.Text, "TenNH", "HHB");
            NguoiDung ndRut = new NguoiDung();
            ndRut = ndDAO.LayKhachHang("SoTK", txtSoTK.Text, "TenNH", "HHB");
            GiaoDich gd = new GiaoDich("Rut Tien", tkRut.TenNH, ndRut.TenTK, tkRut.SoTK, "Tien Mat", "", "", decimal.Parse(txtSoTien.Text), "Rut tien ra khoi tai khoan");
            if (tkRut != null)
            {
                if (gd.SoTien >= 100000 && gd.SoTien <= tkRut.SoDu)
                {
                    tknhDAO.RutTien(tkRut, gd);
                    lsgdDAO.Them(gd);
                    txtSoTK.Text = "";
                    txtSoTien.Text = "";
                    txtCCCD.Text = "";
                    txtSDT.Text = "";
                    MessageBox.Show("Rút tiền thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
