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
            var dv = db.DichVus.ToList();
            var ndv = db.NhomDVs.ToList();
            return Json(new { data = dv, data1 = ndv }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ValidateInput(false)]
        public JsonResult AddDichVu()
        {
            var name = Request.Form["TenDichVu"].ToString();
            var soTien = Request.Form["SoTien"].ToString();
            var idNhomDichVu = Request.Form["idNhomDichVu"].ToString();

            DichVu dv = new DichVu();
            dv.TenDV = name;
            dv.SoTien = int.Parse(soTien);
            dv.IDNhomDV = int.Parse(idNhomDichVu);
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
            var soTien = Request.Form["SoTien"].ToString();
            var idNhomDichVu = Request.Form["idNhomDichVu"].ToString();

            DichVu dv = db.DichVus.SingleOrDefault(x => x.ID == id);
            dv.TenDV = name;
            dv.SoTien = int.Parse(soTien);
            dv.IDNhomDV = int.Parse(idNhomDichVu);
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
        public JsonResult LoadNhomDichVu(int id)
        {
            var list = db.NhomDVs.ToList();
            var iD = id;
            return Json(new { data = list, data1 = iD }, JsonRequestBehavior.AllowGet);

        }
    }
}