﻿using Common.Model;
using Common.ViewModels;
using Data.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implements
{

    public class PhieuChiBusiness
    {
        QLWebDBEntities dbContext;
        private readonly PhieuChiReponsitory _phieuChiRepo;
        private readonly NhanVienReponsitory _nhanVienRepo;
        private NhanVienBusiness _nhanVienBus;

        public PhieuChiBusiness()
        {
            dbContext = new QLWebDBEntities();
            _phieuChiRepo = new PhieuChiReponsitory(dbContext);
            _nhanVienRepo = new NhanVienReponsitory(dbContext);
           
            _nhanVienBus = new NhanVienBusiness();
        }
        public IList<PhieuChiViewModel> SearchDanhSachPhieuChi(String key, string trangthai, DateTime tungay, DateTime denngay, string MaNhanVien)
        {
            IQueryable<PhieuChi> danhSachPhieuChi = _phieuChiRepo.GetAll();
            List<PhieuChiViewModel> all = new List<PhieuChiViewModel>();
            List<PhieuChiViewModel> allForManager = new List<PhieuChiViewModel>();

            //Nếu là thủ kho
            if (_nhanVienBus.layMaChucVu(MaNhanVien) == 7)
            {
                if ((!(tungay == default(DateTime))) && (!(denngay == default(DateTime))))
                {
                    allForManager = (from phieuchi in danhSachPhieuChi
                                     join nhanvien in _nhanVienRepo.GetAll()
                                     on phieuchi.MaNhanVien equals nhanvien.MaNhanVien
                                     where (phieuchi.NgayChi >= tungay.Date && phieuchi.NgayChi <= denngay.Date && nhanvien.MaNhanVien.Equals(MaNhanVien))
                                     select new
                                     {
                                         SoPhieuChi = phieuchi.SoPhieuChi,
                                         NgayChi = phieuchi.NgayChi,
                                         TenNhanVien = nhanvien.TenNhanvien,
                                         TrangThai = phieuchi.TrangThai,
                                         ChuThich = phieuchi.GhiChu,

                                     }).AsEnumerable().Select(x => new PhieuChiViewModel()
                                     {
                                         soPhieuChi = x.SoPhieuChi,
                                         ngayChi = x.NgayChi,
                                         tenNhanVien = x.TenNhanVien,
                                         trangThai = x.TrangThai,
                                         ghiChu = x.ChuThich,
                                     }).OrderByDescending(x => x.soPhieuChi).ToList();
                    return allForManager;
                }
                if (!string.IsNullOrEmpty(key))
                {
                    all = (from phieuchi in danhSachPhieuChi
                           join nhanvien in _nhanVienRepo.GetAll()
                           on phieuchi.MaNhanVien equals nhanvien.MaNhanVien
                           where (nhanvien.UserName.Equals(MaNhanVien) && (
                                     phieuchi.SoPhieuChi.ToString().Contains(key)
                                  || phieuchi.TrangThai.ToString().Equals(trangthai)) && nhanvien.MaNhanVien.Equals(MaNhanVien))
                           select new
                           {
                               soPhieuChi = phieuchi.SoPhieuChi,
                               NgayChi = phieuchi.NgayChi,
                               TenNhanVien = nhanvien.TenNhanvien,
                               TrangThai = phieuchi.TrangThai,
                               ChuThich = phieuchi.GhiChu,

                           }).AsEnumerable().Select(x => new PhieuChiViewModel()
                           {
                               soPhieuChi = x.soPhieuChi,
                               ngayChi = x.NgayChi,
                               tenNhanVien = x.TenNhanVien,
                               trangThai = x.TrangThai,
                               ghiChu = x.ChuThich,
                           }).OrderByDescending(x => x.soPhieuChi).ToList();
                    return all;
                }
                if (!string.IsNullOrEmpty(trangthai))
                {
                    all = (from phieuchi in danhSachPhieuChi
                           join nhanvien in _nhanVienRepo.GetAll()
                           on phieuchi.MaNhanVien equals nhanvien.MaNhanVien
                           where (nhanvien.UserName.Equals(MaNhanVien) && phieuchi.TrangThai.ToString().Equals(trangthai))
                           select new
                           {
                               SoPhieuChi = phieuchi.SoPhieuChi,
                               NgayChi = phieuchi.NgayChi,
                               TenNhanVien = nhanvien.TenNhanvien,
                               TrangThai = phieuchi.TrangThai,
                               ChuThich = phieuchi.GhiChu,

                           }).AsEnumerable().Select(x => new PhieuChiViewModel()
                           {
                               soPhieuChi = x.SoPhieuChi,
                               ngayChi = x.NgayChi,
                               tenNhanVien = x.TenNhanVien,
                               trangThai = x.TrangThai,
                               ghiChu = x.ChuThich,
                           }).OrderByDescending(x => x.soPhieuChi).ToList();
                    return all;
                }

                all = (from phieuchi in danhSachPhieuChi
                       join nhanvien in _nhanVienRepo.GetAll()
                       on phieuchi.MaNhanVien equals nhanvien.MaNhanVien
                       where (nhanvien.UserName.Equals(MaNhanVien) && phieuchi.TrangThai.Equals(true))
                       select new
                       {
                           SoPhieuChi = phieuchi.SoPhieuChi,
                           NgayChi = phieuchi.NgayChi,
                           TenNhanVien = nhanvien.TenNhanvien,
                           TrangThai = phieuchi.TrangThai,
                           ChuThich = phieuchi.GhiChu,

                       }).AsEnumerable().Select(x => new PhieuChiViewModel()
                       {
                           soPhieuChi = x.SoPhieuChi,
                           ngayChi = x.NgayChi,
                           tenNhanVien = x.TenNhanVien,
                           trangThai = x.TrangThai,
                           ghiChu = x.ChuThich,
                       }).OrderByDescending(x => x.soPhieuChi).ToList();
                return all;

            }
            else
            {
                if ((!(tungay == default(DateTime))) && (!(denngay == default(DateTime))))
                {
                    allForManager = (from phieuchi in danhSachPhieuChi
                                     join nhanvien in _nhanVienRepo.GetAll()
                                     on phieuchi.MaNhanVien equals nhanvien.MaNhanVien
                                     where (phieuchi.NgayChi >= tungay.Date && phieuchi.NgayChi <= denngay.Date)
                                     select new
                                     {
                                         SoPhieuChi = phieuchi.SoPhieuChi,
                                         NgayChi = phieuchi.NgayChi,
                                         TenNhanVien = nhanvien.TenNhanvien,
                                         TrangThai = phieuchi.TrangThai,
                                         ChuThich = phieuchi.GhiChu,

                                     }).AsEnumerable().Select(x => new PhieuChiViewModel()
                                     {
                                         soPhieuChi = x.SoPhieuChi,
                                         ngayChi = x.NgayChi,
                                         tenNhanVien = x.TenNhanVien,
                                         trangThai = x.TrangThai,
                                         ghiChu = x.ChuThich,
                                     }).OrderByDescending(x => x.soPhieuChi).ToList();
                    return allForManager;
                }
                if (!string.IsNullOrEmpty(key))
                {
                    allForManager = (from phieuchi in danhSachPhieuChi
                                     join nhanvien in _nhanVienRepo.GetAll()
                                     on phieuchi.MaNhanVien equals nhanvien.MaNhanVien
                                     where (phieuchi.SoPhieuChi.ToString().Contains(key))
                                     select new
                                     {
                                         SoPhieuChi = phieuchi.SoPhieuChi,
                                         NgayChi = phieuchi.NgayChi,
                                         TenNhanVien = nhanvien.TenNhanvien,
                                         TrangThai = phieuchi.TrangThai,
                                         ChuThich = phieuchi.GhiChu,

                                     }).AsEnumerable().Select(x => new PhieuChiViewModel()
                                     {
                                         soPhieuChi = x.SoPhieuChi,
                                         ngayChi = x.NgayChi,
                                         tenNhanVien = x.TenNhanVien,
                                         trangThai = x.TrangThai,
                                         ghiChu = x.ChuThich,
                                     }).OrderByDescending(x => x.soPhieuChi).ToList();
                    return allForManager;
                }
                if (!string.IsNullOrEmpty(trangthai))
                {
                    allForManager = (from phieuchi in danhSachPhieuChi
                                     join nhanvien in _nhanVienRepo.GetAll()
                                     on phieuchi.MaNhanVien equals nhanvien.MaNhanVien
                                     where phieuchi.TrangThai.ToString().Equals(trangthai)
                                     select new
                                     {
                                         SoPhieuChi = phieuchi.SoPhieuChi,
                                         NgayChi = phieuchi.NgayChi,
                                         TenNhanVien = nhanvien.TenNhanvien,
                                         TrangThai = phieuchi.TrangThai,
                                         ChuThich = phieuchi.GhiChu,

                                     }).AsEnumerable().Select(x => new PhieuChiViewModel()
                                     {
                                         soPhieuChi = x.SoPhieuChi,
                                         ngayChi = x.NgayChi,
                                         tenNhanVien = x.TenNhanVien,
                                         trangThai = x.TrangThai,
                                         ghiChu = x.ChuThich,
                                     }).OrderByDescending(x => x.soPhieuChi).ToList();
                    return allForManager;
                }

                allForManager = (from phieuchi in danhSachPhieuChi
                                 join nhanvien in _nhanVienRepo.GetAll()
                                 on phieuchi.MaNhanVien equals nhanvien.MaNhanVien
                                 where phieuchi.TrangThai.Equals(true)
                                 select new
                                 {
                                     SoPhieuChi = phieuchi.SoPhieuChi,
                                     NgayChi = phieuchi.NgayChi,
                                     TenNhanVien = nhanvien.TenNhanvien,
                                     TrangThai = phieuchi.TrangThai,
                                     ChuThich = phieuchi.GhiChu,

                                 }).AsEnumerable().Select(x => new PhieuChiViewModel()
                                 {
                                     soPhieuChi = x.SoPhieuChi,
                                     ngayChi = x.NgayChi,
                                     tenNhanVien = x.TenNhanVien,
                                     trangThai = x.TrangThai,
                                     ghiChu = x.ChuThich,
                                 }).OrderByDescending(x => x.soPhieuChi).ToList();
                return allForManager;
            }
        }
        public int LoadSoPhieuChi()
        {
            var soPhieuChi = from phieuchi in _phieuChiRepo.GetAll()
                                 orderby phieuchi.SoPhieuChi descending
                                 select phieuchi.SoPhieuChi;

            int demSoPhieu = _phieuChiRepo.GetAll().Count();
            if (demSoPhieu == 0)
            {
                return 1;
            }
            return (soPhieuChi.First() + 1);
        }
        public async Task Create(PhieuChiViewModel O)
        {
            PhieuChi order = new PhieuChi
            {
                SoPhieuChi = O.soPhieuChi,
                NgayChi = DateTime.Now,
                MaNhanVien = O.maNhanVien,
                TrangThai = true,
                GhiChu = O.ghiChu,
                NgayChinhSua = DateTime.Now,
            };
           
            await _phieuChiRepo.InsertAsync(order);
        }
        public async Task<object> Find(int ID)
        {
            return await _phieuChiRepo.GetByIdAsync(ID);
        }

        public async Task HuyPhieuChi(object editModel) // chỉnh sửa trạng thái và ngày chỉnh sửa
        {
            PhieuChi editPhieuChi = (PhieuChi)editModel;
            editPhieuChi.TrangThai = false;
            editPhieuChi.NgayChinhSua = DateTime.Now;

            await _phieuChiRepo.EditAsync(editPhieuChi);
        }

        public IEnumerable<PhieuChiViewModel> thongTinPhieuChiTheoMa(int soPhieuChi)
        {
            IQueryable<PhieuChi> danhSachPhieuChi = _phieuChiRepo.GetAll();
            List<PhieuChiViewModel> all = new List<PhieuChiViewModel>();

            all = (from phieuchi in danhSachPhieuChi
                   join nhanvien in _nhanVienRepo.GetAll()
                   on phieuchi.MaNhanVien equals nhanvien.MaNhanVien
                   where (phieuchi.SoPhieuChi.Equals(soPhieuChi))
                   select new
                   {
                       SoPhieuChi = phieuchi.SoPhieuChi,
                       NgayChi = phieuchi.NgayChi,
                       TenNhanVien = nhanvien.TenNhanvien,
                       TrangThai = phieuchi.TrangThai,
                       ChuThich = phieuchi.GhiChu,
                   }).AsEnumerable().Select(x => new PhieuChiViewModel()
                   {
                       soPhieuChi = x.SoPhieuChi,
                       ngayChi = x.NgayChi,
                       tenNhanVien = x.TenNhanVien,
                       trangThai = x.TrangThai,
                       ghiChu = x.ChuThich,
                   }).ToList();
            return all;
        }

    }


}
