using System;
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
        [HttpGet]
        public JsonResult LoadKhachHang()
        {
            var ds = db.KhachHangs.ToList();
            return Json(new { data = ds }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost, ValidateInput(false)]
        public JsonResult AddKhachHang()
        {
            //var userName = Request.Form["TenDangNhap"].ToString();
            //var pass = Request.Form["MatKhau"].ToString();
            //var name = Request.Form["Ten"].ToString();
            //var mail = Request.Form["Mail"].ToString();
            //var gioiTinh = Request.Form["GioiTinh"].ToString();
            //var countform = Request.Form.Count;
            //var countfile = Request.Files.Count;
            //var anh = Request.Files["Anh"];
            //var path = Path.Combine(Server.MapPath("~/Images"), anh.FileName);
            //anh.SaveAs(path);

            //KhachHang nv = new KhachHang();
            //nv.TenDangNhap = userName;
            //nv.MatKhau = pass;
            //nv.TenNV = name;
            //nv.Mail = mail;
            //nv.GioiTinh = gioiTinh;
            //nv.Anh = anh.FileName;
            //nv.IDNhom = 3;
            //nv.NgayTao = DateTime.Now;

            //db.KhachHangs.Add(nv);
            //db.SaveChanges();
            return Json(new { data = true });
        }
        [HttpPost, ValidateInput(false)]
        
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
        [HttpGet]
        public JsonResult LoadNhanVienGroup()
        {
            return Json(new { data = db.N.ToList() }, JsonRequestBehavior.AllowGet);
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
    }
}