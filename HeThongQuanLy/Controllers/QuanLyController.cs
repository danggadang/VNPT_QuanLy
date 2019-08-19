using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;

namespace HeThongQuanLy.Controllers
{
    public class QuanLyController : Controller
    {
        HeThong db = new HeThong();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public JsonResult AddQuanLy()
        {
            var userName = Request.Form["userName"].ToString();
            var pass = Request.Form["pass"].ToString();
            var gioiTinh = Request.Form["gioiTinh"].ToString();
            var name = Request.Form["Ten"].ToString();
            var gia = Request.Form["Gia"].ToString();
            var countform = Request.Form.Count;
            var countfile = Request.Files.Count;
            var anh = Request.Files["Anh"];
            var path = Path.Combine(Server.MapPath("~/Image"), anh.FileName);
            anh.SaveAs(path);
            NhanVien nv = new NhanVien();
            nv.TenDangNhap = userName;
            nv.MatKhau = pass;
            nv.TenNV = name;
            nv.Anh = anh.FileName;
            nv.GioiTinh = gioiTinh;
            nv.IDNhom = 2;
            nv.NgayTao = DateTime.Now;
            db.NhanViens.Add(nv);
            db.SaveChanges();
            return Json(new { data = true });
        }
        //[HttpPost, ValidateInput(false)]
        //public JsonResult EditQuanLy()
        //{
        //    var countform = Request.Form.Count;
        //    var countfile = Request.Files.Count;
        //    var name = Request.Form["Ten"].ToString();
        //    var gia = Request.Form["Gia"].ToString();
        //    var anh = Request.Files["Anh"];
        //    var motaRaw = Request.Form["Mota"];
        //    var mota = HttpUtility.UrlDecode(motaRaw);
        //    var path = Path.Combine(Server.MapPath("~/Image"), anh.FileName);
        //    var id = int.Parse(Request.Form["ID"]);
        //    anh.SaveAs(path);

        //    Course course = db.Courses.Single(x => x.ID == id);
        //    course.Name = name;
        //    course.Image = anh.FileName;
        //    course.Price = decimal.Parse(gia);

        //    course.Description = mota;
        //    db.SaveChanges();
        //    return Json(new { data = true });
        //}
        public JsonResult DeleteQuanLy(int id)
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
        [HttpGet]
        public JsonResult LoadQuanLy()
        {
            var ds = db.NhanViens.Where(x => x.IDNhom == 2).ToList();
            return Json(new { data = ds }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddandEditQuanLyModal(int id)
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