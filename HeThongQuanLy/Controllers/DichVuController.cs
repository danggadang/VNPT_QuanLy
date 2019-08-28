using Model.EF;
using System;
using System.Linq;
using System.Web.Mvc;

namespace HeThongQuanLy.Controllers
{
    public class DichVuController : BaseController
    {
        private HeThong db = new HeThong();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult LoadDichVu()
        {
            var ds = db.DichVus.ToList();
            return Json(new { data = ds }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ValidateInput(false)]
        public JsonResult AddDichVu()
        {
            var name = Request.Form["TenDichVu"].ToString();

            DichVu dv = new DichVu();
            dv.TenDV = name;
            dv.TrangThai = true;
            dv.NgayTao = DateTime.Now;

            db.DichVus.Add(dv);
            db.SaveChanges();
            return Json(new { data = true });
        }

        [HttpPost, ValidateInput(false)]
        public JsonResult EditDichVu()
        {
            var id = int.Parse(Request.Form["ID"].ToString());
            var name = Request.Form["TenDichVu"].ToString();
            var trangThai = Request.Form["TrangThai"].ToString();

            DichVu dv = db.DichVus.SingleOrDefault(x => x.ID == id);
            dv.TenDV = name;
            if (trangThai == "1")
            {
                dv.TrangThai = true;
            }
            else
            {
                dv.TrangThai = false;
            }
            dv.NgaySua = DateTime.Now;

            db.SaveChanges();
            return Json(new { data = true });
        }

        public JsonResult DeleteDichVu(int id)
        {
            if (ModelState.IsValid)
            {
                var ql = db.DichVus.Single(x => x.ID == id);
                db.DichVus.Remove(ql);
                db.SaveChanges();
                return Json(new { data = true });
            }
            else
                return Json(new { data = false });
        }

        public ActionResult AddandEditDichVuModal(int id)
        {
            var result = new DichVu();
            string mode = "Add";
            result = db.DichVus.FirstOrDefault(x => x.ID == id);
            if (result == null)
            {
                mode = "Add";
                result = new DichVu();
            }
            else
            {
                mode = "Edit";
            }
            ViewData["Mode"] = mode;
            ViewData["Obj"] = result;
            return View();
        }
        [HttpPost]
        public JsonResult Status(int id)
        {
            var dv = db.DichVus.Single(z => z.ID == id);
            
            dv.TrangThai = !dv.TrangThai;
            db.SaveChanges();
            return Json(new { data = true });
        }
    }
}