using Model.EF;
using System;
using System.Linq;
using System.Web.Mvc;

namespace HeThongQuanLy.Controllers
{
    public class DichVuGroupController : BaseController
    {
        private HeThong db = new HeThong();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult LoadDichVuGroup()
        {
            var ds = db.NhomDVs.ToList();
            return Json(new { data = ds }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ValidateInput(false)]
        public JsonResult AddDichVuGroup()
        {
            var name = Request.Form["TenDichVuGroup"].ToString();

            NhomDV dv = new NhomDV();
            dv.TenNhom = name;
            dv.TrangThai = true;

            db.NhomDVs.Add(dv);
            db.SaveChanges();
            return Json(new { data = true });
        }

        [HttpPost, ValidateInput(false)]
        public JsonResult EditDichVuGroup()
        {
            var id = int.Parse(Request.Form["ID"].ToString());
            var name = Request.Form["TenDichVuGroup"].ToString();
            var trangThai = Request.Form["trangThai"].ToString();

            NhomDV dv = new NhomDV();
            dv.TenNhom = name;
            if (trangThai == "True")
            {
                dv.TrangThai = true;
            }
            else
            {
                dv.TrangThai = false;
            }
                
            db.NhomDVs.Add(dv);
            db.SaveChanges();
            return Json(new { data = true });
        }

        public JsonResult DeleteDichVuGroup(int id)
        {
            if (ModelState.IsValid)
            {
                var dv = db.NhomDVs.Single(x => x.ID == id);
                db.NhomDVs.Remove(dv);
                db.SaveChanges();
                return Json(new { data = true });
            }
            else
                return Json(new { data = false });
        }

        public ActionResult AddandEditDichVuGroupModal(int id)
        {
            var result = new NhomDV();
            string mode = "Add";
            result = db.NhomDVs.FirstOrDefault(x => x.ID == id);
            if (result == null)
            {
                mode = "Add";
                result = new NhomDV();
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
            var dv = db.NhomDVs.Single(z => z.ID == id);

            dv.TrangThai = !dv.TrangThai;
            db.SaveChanges();
            return Json(new { data = true });
        }
    }
}