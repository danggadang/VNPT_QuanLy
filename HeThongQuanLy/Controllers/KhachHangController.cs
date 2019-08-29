﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;

namespace HeThongQuanLy.Controllers
{
    public class KhachHangController : BaseController
    {
        HeThong db = new HeThong();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult LoadKhachHang()
        {
            var ds = db.KhachHangs.ToList();
            var nv = db.NhanViens.Where(x => x.IDNhom == 3).ToList();
            var tt = db.TrangThais.ToList();
            return Json(new { data = ds,data1= nv, data2=tt }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost, ValidateInput(false)]
        public JsonResult AddKhachHang()
        {
            var name = Request.Form["TenKhachHang"].ToString();
            var diachi = Request.Form["DiaChi"].ToString();
            var soDienThoai = Request.Form["SoDienThoai"].ToString();
            var mail = Request.Form["Mail"].ToString();
            var idNhanVien= int.Parse(Request.Form["idNhanVien"].ToString());

            KhachHang kh = new KhachHang();
            kh.TenKH = name;
            kh.DiaChi = diachi;
            kh.SoDienThoai = soDienThoai;
            kh.Mail = mail;
            kh.NgayTao = DateTime.Now;
            kh.TrangThai = 1;
            kh.IDNhanVien = idNhanVien;

            db.KhachHangs.Add(kh);
            db.SaveChanges();
            return Json(new { data = true });
        }
        [HttpPost, ValidateInput(false)]
        public JsonResult EditKhachHang()
        {
            var id = int.Parse(Request.Form["ID"].ToString());
            var name = Request.Form["TenKhachHang"].ToString();
            var diachi = Request.Form["DiaChi"].ToString();
            var soDienThoai = Request.Form["SoDienThoai"].ToString();
            var mail = Request.Form["Mail"].ToString();
            var idNhanVien = int.Parse(Request.Form["idNhanVien"].ToString());

            KhachHang kh = db.KhachHangs.Single(x=>x.ID==id);
            kh.TenKH = name;
            kh.DiaChi = diachi;
            kh.SoDienThoai = soDienThoai;
            kh.Mail = mail;
            kh.NgaySua = DateTime.Now;
            kh.IDNhanVien = idNhanVien;

            db.SaveChanges();
            return Json(new { data = true });
        }
        public JsonResult DeleteKhachHang(int id)
        {
            if (ModelState.IsValid)
            {
                var kh = db.KhachHangs.Single(x => x.ID == id);
                db.KhachHangs.Remove(kh);
                db.SaveChanges();
                return Json(new { data = true });
            }
            else
                return Json(new { data = false });
        }
        public JsonResult SaveTrangThai()
        {
            return Json(new {  }, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public JsonResult EditTrangThaiModal(int id)
        {
            var tt = db.TrangThais.ToList();
            List<object> list = new List<object>();
            list.Add(tt);
            list.Add(id);
            list.Add(db.KhachHangs.SingleOrDefault(x=>x.ID==id).TrangThai);
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddandEditKhachHangModal(int id)
        {
            var result = new KhachHang();
            string mode = "Add";
            result = db.KhachHangs.FirstOrDefault(x => x.ID == id);
            if (result == null)
            {
                mode = "Add";
                result = new KhachHang();
            }
            else
            {
                mode = "Edit";
            }
            ViewData["Mode"] = mode;
            ViewData["Obj"] = result;
            return View();
        }
        public JsonResult LoadNhanVien(int id)
        {
            var list = db.NhanViens.Where(x => x.IDNhom == 3).ToList();
            var iD = id;
            return Json(new { data = list,data1=iD }, JsonRequestBehavior.AllowGet);

        }
    }
}