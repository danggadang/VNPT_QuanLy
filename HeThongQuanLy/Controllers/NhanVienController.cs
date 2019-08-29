using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;

namespace HeThongQuanLy.Controllers
{
    public class NhanVienController : BaseController
    {
        HeThong db = new HeThong();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult LoadNhanVien()
        {
            var ds = db.NhanViens.Where(x => x.IDNhom == 3).ToList();
            return Json(new { data = ds }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost, ValidateInput(false)]
        public JsonResult AddNhanVien()
        {
            var userName = Request.Form["TenDangNhap"].ToString();
            var pass = Request.Form["MatKhau"].ToString();
            var name = Request.Form["Ten"].ToString();
            var mail = Request.Form["Mail"].ToString();
            var gioiTinh = Request.Form["GioiTinh"].ToString();
            var countform = Request.Form.Count;
            var countfile = Request.Files.Count;
            var anh = Request.Files["Anh"];
            var path = Path.Combine(Server.MapPath("~/Images"), anh.FileName);
            anh.SaveAs(path);

            NhanVien nv = new NhanVien();
            nv.TenDangNhap = userName;
            nv.MatKhau = pass;
            nv.TenNV = name;
            nv.Mail = mail;
            nv.GioiTinh = gioiTinh;
            nv.Anh = anh.FileName;
            nv.IDNhom = 3;
            nv.NgayTao = DateTime.Now;

            db.NhanViens.Add(nv);
            db.SaveChanges();
            return Json(new { data = true });
        }
        [HttpPost, ValidateInput(false)]
        public JsonResult EditNhanVien()
        {
            var id = int.Parse(Request.Form["ID"].ToString());
            NhanVien nv = db.NhanViens.Single(x => x.ID == id);
            var userName = Request.Form["TenDangNhap"].ToString();
            var pass = Request.Form["MatKhau"].ToString();
            var name = Request.Form["Ten"].ToString();
            var mail = Request.Form["Mail"].ToString();
            var gioiTinh = Request.Form["GioiTinh"].ToString();
            var countform = Request.Form.Count;
            var countfile = Request.Files.Count;
            var anh = Request.Files["Anh"];
            if(anh!=null)
            {
                var path = Path.Combine(Server.MapPath("~/Images"), anh.FileName);
                anh.SaveAs(path);
                nv.Anh = anh.FileName;
            }    
            
            nv.TenDangNhap = userName;
            nv.MatKhau = pass;
            nv.TenNV = name;
            nv.Mail = mail;
            nv.GioiTinh = gioiTinh;          
            nv.IDNhom = 3;
            nv.NgaySua = DateTime.Now;

            db.SaveChanges();
            return Json(new { data = true });
        }
        public JsonResult DeleteNhanVien(int id)
        {
            if (ModelState.IsValid)
            {
                var ql = db.NhanViens.Single(x => x.ID == id);
                db.NhanViens.Remove(ql);
                db.SaveChanges();
                return Json(new { data = true });
            }
            else
                return Json(new { data = false });
        }
        public ActionResult AddandEditNhanVienModal(int id)
        {
            var result = new NhanVien();
            string mode = "Add";
            result = db.NhanViens.FirstOrDefault(x => x.ID == id);
            if (result == null)
            {
                mode = "Add";
                result = new NhanVien();
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