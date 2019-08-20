using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;

namespace HeThongQuanLy.Controllers
{
    public class SanPhamController : BaseController
    {
        HeThong db = new HeThong();
        public ActionResult Index()
        {
            return View();
        }
        //[HttpPost, ValidateInput(false)]
        //public JsonResult AddSanPham()
        //{
        //    var countform = Request.Form.Count;
        //    var countfile = Request.Files.Count;
        //    var name = Request.Form["Ten"].ToString();
        //    var gia = Request.Form["Gia"].ToString();
        //    var anh = Request.Files["Anh"];
        //    var motaRaw = Request.Form["Mota"];
        //    var mota = HttpUtility.UrlDecode(motaRaw);
        //    var path = Path.Combine(Server.MapPath("~/Image"), anh.FileName);

        //    anh.SaveAs(path);

        //    Course course = new Course();
        //    course.Name = name;
        //    course.Image = anh.FileName;
        //    course.Price = decimal.Parse(gia);

        //    course.Description = mota;
        //    db.Courses.Add(course);
        //    db.SaveChanges();
        //    return Json(new { data = true });
        //}
        //[HttpPost, ValidateInput(false)]
        //public JsonResult EditSanPham()
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
        //public JsonResult DeleteSanPham(int id)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var course = db.DichVus.Single(x => x.ID == id);
        //        db.Courses.Remove(course);
        //        db.SaveChanges();
        //        return Json(new { data = true });
        //    }
        //    else
        //        return Json(new { data = false });
        //}
    }
}