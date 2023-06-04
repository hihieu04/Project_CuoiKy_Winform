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
    public partial class FrmTienIchVay : Form
    {
        private NguoiDung nguoiDung;
        private TaiKhoanNganHang tknh;
        private Panel pnlNguoiDung;
        ThongTinNguoiDungVayDAO ttNgDungDAO = new ThongTinNguoiDungVayDAO();
        KHNoXauDAO KHNoXauDAO = new KHNoXauDAO();
        public FrmTienIchVay()
        {
            InitializeComponent();
        }
        public FrmTienIchVay(NguoiDung nguoiDung, TaiKhoanNganHang tknh, Panel pnlNguoiDung)
        {
            InitializeComponent();
            this.nguoiDung = nguoiDung;
            this.tknh = tknh;
            this.pnlNguoiDung = pnlNguoiDung;
        }

        private void btn_DangKy_Click(object sender, EventArgs e)
        {
            KHNoXau kh = KHNoXauDAO.IsNull(tknh);
            ThongTinNguoiDungVay ttNgDung = ttNgDungDAO.TKValid(nguoiDung.soTK);
            if (kh == null) //kiem tra co no xau nao khong
            {
                if (ttNgDung == null) //kiem tra co khoan vay nao khong
                {
                    FrmVay frmVay = new FrmVay(nguoiDung, tknh, pnlNguoiDung);
                    Utility.addForm(frmVay, pnlNguoiDung);
                }
                else
                    MessageBox.Show("Bạn đang có khoản vay!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }   
            else
            {
                DateTime startDate = kh.NgayNo;
                DateTime endDate = DateTime.Now;
                TimeSpan span = endDate - startDate;
                double totalDays = span.TotalDays;
                if (totalDays >= 1825) //thoi gian no xau >= 5 nam thi duoc xoa no xau
                {
                    KHNoXauDAO.Xoa(kh);
                    FrmVay frmVay = new FrmVay(nguoiDung, tknh, pnlNguoiDung);
                    Utility.addForm(frmVay, pnlNguoiDung);
                }
                else
                    MessageBox.Show("Bạn đang có nợ xấu!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_XemKhoanVay_Click(object sender, EventArgs e)
        {
            ThongTinNguoiDungVay ttNgDung = ttNgDungDAO.TKValid(nguoiDung.SoTK);           
            if (ttNgDung != null) //kiem tra xem co khoan vay hay chua
            {
                DateTime startDate = ttNgDung.NgayDenHan;
                DateTime endDate = DateTime.Now;
                TimeSpan span = endDate - startDate;
                double totalDays = span.TotalDays;
                if (totalDays > 90) //qua han thanh toan 3 thang => cap nhat no xau va xoa khoan vay
                {
                    KHNoXau kh = new KHNoXau();
                    kh.TenNH = nguoiDung.TenNH;
                    kh.SoTK = tknh.SoTK;
                    kh.TenTK = nguoiDung.tenTK;
                    kh.Cccd = nguoiDung.Cccd;
                    kh.NgayNo = DateTime.Now;
                    if(KHNoXauDAO.IsNull(tknh) == null)
                        KHNoXauDAO.Them(kh);
                    ttNgDungDAO.Xoa(ttNgDung);
                    MessageBox.Show("Bạn trở thành nợ xấu vì không thanh toán khoản vay quá 90 ngày", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    FrmTienIch frmtienich = new FrmTienIch(nguoiDung, tknh, pnlNguoiDung);
                    Utility.addForm(frmtienich, pnlNguoiDung);
                }
                else
                {
                    FrmThongTinTaiKhoanVay frmThongTinTaiKhoanVay = new FrmThongTinTaiKhoanVay(nguoiDung, tknh, ttNgDung, pnlNguoiDung);
                    Utility.addForm(frmThongTinTaiKhoanVay, pnlNguoiDung);
                }     
            }
            else
                MessageBox.Show("Bạn không có khoản vay nào!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
