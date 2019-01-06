﻿using Business.Implements;
using Common.Functions;
using Common.Model;
using Common.ViewModels;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using PagedList;
using Spire.Pdf;
using Spire.Xls;
using Spire.Xls.Converter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace QLWeb.Areas.Admin.Controllers
{
    public class BaoCaoTonKhoController : BaseController
    {
        readonly BaoCaoTonKhoBusiness _baoCaoTonKhoBus = new BaoCaoTonKhoBusiness();
        readonly PhieuKiemKhoBusiness _phieuKiemKhoBus = new PhieuKiemKhoBusiness();
        readonly HangHoaBusiness _hangHoaBus = new HangHoaBusiness();
        readonly NhanVienBusiness _nhanVienBus = new NhanVienBusiness();
        //
        // GET: /Admin/KiemKho/

        public ActionResult Index()
        {
            

            return View();
        }

        public ActionResult Create()
        {
            ViewBag.maNhanVien = _nhanVienBus.LoadMaNhanVien(HomeController.userName);
            ViewBag.tenNhanVien = _nhanVienBus.LoadTenNhanVien(HomeController.userName);
            ViewBag.soPhieuKiemKhoTuTang = _phieuKiemKhoBus.LoadSoPhieuKiemKho();
            ViewBag.danhSachHangHoa = new SelectList(_hangHoaBus.LoadSanhSachHangHoaKho(), "Value", "Text");
            return View();
        }

        public JsonResult ListName(string q)
        {
            var data = _hangHoaBus.ListName(q);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DanhSachBaoCaoTonKho(string thang , string nam)
        {
            var res = _baoCaoTonKhoBus.GetListBaoCao(Convert.ToInt32(thang), Convert.ToInt32(nam));
            HttpContext.Session["BaoCaoTonKho"] = res;
            return View(res);
        }
        public ActionResult Delete(int id)
        {
            return View();
        }

        public ActionResult Detail()
        {
            return View();
        }
        public ActionResult XuatExcel()
        {
            var models = (List<BaoCaoTonKhoViewModel>)HttpContext.Session["BaoCaoTonKho"];
            if (models == null) return View("Index");
            using (ExcelPackage pck = new ExcelPackage(new FileInfo(Server.MapPath("~/Templates/BaoCaoTonKho.xlsx"))))
            {
                var ws = pck.Workbook.Worksheets[1];

                for (var i = 0; i < models.Count; i++)
                {
                    ws.Cells["A" + (i + 2)].Value = (i + 1);
                    ws.Cells["A" + (i + 2)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells["A" + (i + 2)].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);

                    ws.Cells["B" + (i + 2)].Value = models[i].thang;
                    ws.Cells["B" + (i + 2)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells["B" + (i + 2)].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);

                    ws.Cells["C" + (i + 2)].Value = models[i].nam;
                    ws.Cells["C" + (i + 2)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells["C" + (i + 2)].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);

                    ws.Cells["D" + (i + 2)].Value = models[i].maHangHoa;
                    ws.Cells["D" + (i + 2)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells["D" + (i + 2)].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);

                    ws.Cells["E" + (i + 2)].Value = models[i].tenHangHoa;
                    ws.Cells["E" + (i + 2)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells["E" + (i + 2)].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);

                    ws.Cells["F" + (i + 2)].Value = models[i].soLuongTonDau;
                    ws.Cells["F" + (i + 2)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells["F" + (i + 2)].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);

                    ws.Cells["G" + (i + 2)].Value = models[i].soLuongNhap;
                    ws.Cells["G" + (i + 2)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells["G" + (i + 2)].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);

                    ws.Cells["H" + (i + 2)].Value = models[i].soLuongXuat;
                    ws.Cells["H" + (i + 2)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells["H" + (i + 2)].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);

                    ws.Cells["I" + (i + 2)].Value = models[i].soLuongTonCuoi;
                    ws.Cells["I" + (i + 2)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells["I" + (i + 2)].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);

                    ws.Cells["J" + (i + 2)].Value = models[i].donViTinh;
                    ws.Cells["J" + (i + 2)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells["J" + (i + 2)].Style.Border.BorderAround(ExcelBorderStyle.Thin, System.Drawing.Color.Black);
                }
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AppendHeader("content-disposition",
                                   $"attachment;  filename=BaoCaoTonKho_{DateTime.Now}.xlsx");
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                var ms = new MemoryStream(pck.GetAsByteArray());
                HttpContext.Session["FileBaoCaoTonKho"] = ms;
                return File(ms, Response.ContentType);
            }
        }
        public MemoryStream XuatPdf()
        {
            var stream = (MemoryStream)HttpContext.Session["FileBaoCaoTonKho"];
            MemoryStream pdfMemoryStream = new MemoryStream();
            
            Workbook workbook = new Workbook();
            workbook.LoadFromStream(stream, ExcelVersion.Version2013);
            PdfConverter pdfConverter = new PdfConverter(workbook);
            PdfConverterSettings settings = new PdfConverterSettings();
            PdfDocument pdfDocument = pdfConverter.Convert(settings);
            pdfDocument.SaveToStream(pdfMemoryStream);

            return pdfMemoryStream;
        }
        public ActionResult ThongTinPhieuKiemKho(int id)
        {
            ViewBag.chiTietPhieuKiemKho = _phieuKiemKhoBus.thongTinChiTietPhieuKiemKhoTheoMa(id).ToList();
            ViewBag.phieuKiemKho = _phieuKiemKhoBus.thongTinPhieuKiemKhoTheoMa(id).ToList();
            return View();
        }

    }
}