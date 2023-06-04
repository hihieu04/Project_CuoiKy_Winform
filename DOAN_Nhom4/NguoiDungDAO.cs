﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DOAN_Nhom4
{
    internal class NguoiDungDAO
    {
        DBConnection data = new DBConnection();
        public DataTable HienThi()
        {
            string sqlStr = string.Format("SELECT KhachHang.TenNH, KhachHang.SoTK, KhachHang.TenKH, TaiKhoanNganHang.TenDN, TaiKhoanNganHang.MatKhau, TaiKhoanNganHang.SoDu, KhachHang.NgaySinh, KhachHang.CCCD, KhachHang.Email, KhachHang.SDT, TaiKhoanNganHang.NgayDangKy "
                                        + "FROM KhachHang "
                                        + "JOIN TaiKhoanNganHang ON KhachHang.TenNH = TaiKhoanNganHang.TenNH AND KhachHang.SoTK = TaiKhoanNganHang.SoTK "
                                        + "WHERE KhachHang.TenNH = 'HHB'");
            return data.LayDanhSach(sqlStr);
        }
        public void Them(NguoiDung kh)
        {
            string sqlKH = string.Format("INSERT INTO KhachHang(TenNH, SoTK , TenKH, NgaySinh, CCCD, Email, SDT) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')", kh.TenNH, kh.SoTK, kh.TenTK, kh.NgaySinh, kh.Cccd, kh.Email, kh.Sdt);
            data.xuLi(sqlKH);
        }

        public void Xoa(NguoiDung kh)
        {
            string sqlKH = string.Format("DELETE FROM KhachHang WHERE SoTK ='{0}' AND TenNH = '{1}'", kh.SoTK, kh.TenNH);
            data.xuLi(sqlKH);
        }
        public void Sua(NguoiDung kh)
        {
            string sqlKH = string.Format("UPDATE KhachHang SET TenKH = '{0}', NgaySinh = '{1}', CCCD = '{2}', Email = '{3}', SDT = '{4}' WHERE SoTK ='{5}' AND TenNH = '{6}'", kh.TenTK, kh.NgaySinh, kh.Cccd, kh.Email, kh.Sdt, kh.SoTK, kh.TenNH);
            data.xuLi(sqlKH);
        }

        public NguoiDung LayKhachHang(string CotSoTK, string GTSoTK, string CotTenNH, string GTTenNH)
        {
            NguoiDung khachHang = new NguoiDung();
            string sql = string.Format("SELECT * FROM KhachHang WHERE {0} = '{1}' AND {2} = '{3}'", CotSoTK, GTSoTK, CotTenNH, GTTenNH);
            khachHang = data.Xuli(sql);
            return khachHang;
        }

        public bool IsEmpty(NguoiDung kh)
        {
            if (kh.SoTK != "" && kh.TenTK != "" && kh.Cccd != "" && kh.Sdt != "" && kh.Email != "")
                return false;
            return true;
        }
        public bool IsPhone(NguoiDung kh)
        {
            return data.IsPhone(kh.Sdt);
        }
        public bool IsEmail(NguoiDung kh)
        {
            return data.IsEmail(kh.Email);
        }
    }
}
