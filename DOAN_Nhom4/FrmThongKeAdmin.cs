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
    public partial class FrmThongKeAdmin : Form
    {
        LichSuGiaoDichDAO lsgdDAO = new LichSuGiaoDichDAO();
        ThongKeDAO tkDAO = new ThongKeDAO();

        string ngayBatDau = DateTime.Now.Subtract(new TimeSpan(6, 0, 0, 0)).ToString("yyyy-MM-dd");
        string ngayKetThuc = DateTime.Now.ToString("yyyy-MM-dd");
        public FrmThongKeAdmin()
        {
            InitializeComponent();
        }

        private void FrmThongKeAdmin_Load(object sender, EventArgs e)
        {
            HienThiDuLieuLen();
            HienThiLsgd();
            ThongKe.DataSource = tkDAO.ThongKeGiaoDichChuyenTien(ngayBatDau, ngayKetThuc);
            ThongKe.Series["ChuyenTien"].XValueMember = "ThoiGian";
            ThongKe.Series["ChuyenTien"].YValueMembers = "TongTien";
            ThongKe.Legends[1].Enabled = false;
            ThongKe.Legends[2].Enabled = false;
            ThongKe.Legends[3].Enabled = false;
            ThongKe.Legends[4].Enabled = false;
        }

        private void btnChuyenTien_Click(object sender, EventArgs e)
        {
            MacDinhDoThi();
            string s = "ChuyenTien";
            ThongKe.Legends[0].Enabled = true;
            ThongKe.DataSource = tkDAO.ThongKeGiaoDichChuyenTien(ngayBatDau, ngayKetThuc);
            HienThiDuLieuDoThi(s);
        }

        private void btnVienThong_Click(object sender, EventArgs e)
        {
            MacDinhDoThi();
            string s = "VienThong";
            ThongKe.Legends[1].Enabled = true;
            ThongKe.DataSource = tkDAO.ThongKeGiaoDichVienThong(ngayBatDau, ngayKetThuc);
            HienThiDuLieuDoThi(s);
        }

        private void btnTinDung_Click(object sender, EventArgs e)
        {
            MacDinhDoThi();
            string s = "TinDung";
            ThongKe.Legends[2].Enabled = true;
            ThongKe.DataSource = tkDAO.ThongKeGiaoDichTinDung(ngayBatDau, ngayKetThuc);
            HienThiDuLieuDoThi(s);
        }

        private void btnTietKiem_Click(object sender, EventArgs e)
        {
            MacDinhDoThi();
            string s = "TietKiem";
            ThongKe.Legends[3].Enabled = true;
            ThongKe.DataSource = tkDAO.ThongKeGiaoDichTietKiem(ngayBatDau, ngayKetThuc);
            HienThiDuLieuDoThi(s);
        }

        private void btnVay_Click(object sender, EventArgs e)
        {
            MacDinhDoThi();
            string s = "Vay";
            ThongKe.Legends[4].Enabled = true;
            ThongKe.DataSource = tkDAO.ThongKeGiaoDichVayTien(ngayBatDau, ngayKetThuc);
            HienThiDuLieuDoThi(s);
        }

        private void pb_QuayLai_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmAdmin frmad = new FrmAdmin();
            frmad.ShowDialog();
            this.Close();
        }

        public void MacDinhDoThi()
        {
            ThongKe.Series["ChuyenTien"].Enabled = false;
            ThongKe.Series["VienThong"].Enabled = false;
            ThongKe.Series["TinDung"].Enabled = false;
            ThongKe.Series["TietKiem"].Enabled = false;
            ThongKe.Series["Vay"].Enabled = false;
        }

        public void HienThiDuLieuDoThi(string s)
        {
            ThongKe.Series[s].Enabled = true;
            ThongKe.Series[s].XValueMember = "ThoiGian";
            ThongKe.Series[s].YValueMembers = "TongTien";
        }

        private void HienThiLsgd() // Cai dat tieng viet cho cac title cua bang
        {
            gvQLGD.DataSource = lsgdDAO.HienThi();
            gvQLGD.Columns[7].DefaultCellStyle.Format = "dd/MM/yyyy";
            gvQLGD.Columns[0].HeaderText = "Mã giao dịch";
            gvQLGD.Columns[1].HeaderText = "Loại giao dịch";
            gvQLGD.Columns[2].HeaderText = "Ngân hàng gửi";
            gvQLGD.Columns[3].HeaderText = "Số tài khoản gửi";
            gvQLGD.Columns[4].HeaderText = "Ngân hàng nhận";
            gvQLGD.Columns[5].HeaderText = "Số tài khoản nhận";
            gvQLGD.Columns[6].HeaderText = "Số tiền";
            gvQLGD.Columns[7].HeaderText = "Thời gian";
        }

        private void HienThiDuLieuLen() // Hien thi thong ke co ban ve so luong cac tai khoan
        {
            lblHienThiSoLuongTaiKhoang.Text = tkDAO.DemSoLuongTaiKhoang().ToString();
            lblHienThiTinDung.Text = tkDAO.DemSoLuongTinDung().ToString();
            lblHienThiTietKiem.Text = tkDAO.DemSoLuongSoTietKiem().ToString();
            lblHienThiVay.Text = tkDAO.DemSoLuongVay().ToString();
            lblHienThiNoXau.Text = tkDAO.DemSoLuongNoXau().ToString();
        }
    }
}
