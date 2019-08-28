using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;

namespace HeThongQuanLy.Controllers
{
    public class UserController : BaseController
    {
        HeThong db = new HeThong();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShowInfo(int id)
        {
            var info = db.NhanViens.Single(x => x.ID == id);
            ViewData["info"] = info;
            return View();
        }       
        public ActionResult EditModal(int id)
        {
            var info = db.NhanViens.Single(x => x.ID == id);
            ViewData["Info"] = info;
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public JsonResult Edit()
        {
            var id = int.Parse(Request.Form["ID"].ToString());
            var name = Request.Form["Ten"].ToString();
            var mail = Request.Form["Mail"].ToString();
            var gioiTinh = Request.Form["GioiTinh"].ToString();

            NhanVien nv = db.NhanViens.Single(x => x.ID == id);
            nv.TenNV = name;
            nv.Mail = mail;
            nv.GioiTinh = gioiTinh;

            nv.NgaySua = DateTime.Now;
            db.SaveChanges();
            return Json(new { data = true });
        }

    }
}