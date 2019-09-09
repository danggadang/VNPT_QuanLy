using Model.EF;
using System;
using System.Linq;
using System.Web.Mvc;

namespace HeThongQuanLy.Controllers
{
    public class HoaDonController : BaseController
    {
        private HeThong db = new HeThong();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult LoadHoaDon()
        {
            var hd = db.HoaDons.ToList();
            var kh = db.KhachHangs.ToList();
            var dv = db.DichVus.ToList();
            var nv = db.NhanViens.Where(x => x.IDNhom == 3).ToList();
            return Json(new { data = hd, data1 = kh, data2 = dv, data3 = nv }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult LoadThongTin(int id)
        {
            var hd = db.HoaDons.Where(x => x.ID == id).ToList();
            var kh = db.KhachHangs.ToList();
            var dv = db.DichVus.ToList();
            var nv = db.NhanViens.Where(x => x.IDNhom == 3).ToList();
            return Json(new { data = hd, data1 = kh, data2 = dv, data3 = nv }, JsonRequestBehavior.AllowGet);
        }
       
        [HttpPost, ValidateInput(false)]
        public JsonResult AddHoaDon()
        {
            var soLuong = int.Parse(Request.Form["SoLuong"].ToString());
            var idNhanVien = int.Parse(Request.Form["idNhanVien"].ToString());
            var idKhachHang = int.Parse(Request.Form["idKhachHang"].ToString());
            var idDichVu = int.Parse(Request.Form["idDichVu"].ToString());

            var giaTien = db.DichVus.SingleOrDefault(x => x.ID == idDichVu).SoTien;
            var nhanVien = db.NhanViens.SingleOrDefault(x => x.ID == idNhanVien);
            var dichVu = db.DichVus.SingleOrDefault(x => x.ID == idDichVu);
            var khachHang = db.KhachHangs.SingleOrDefault(x => x.ID == idKhachHang);

            HoaDon dv = new HoaDon();
            var tongTien = giaTien * soLuong;
            dv.IDDichVu = idDichVu;
            dv.IDNhanVien = idNhanVien;
            dv.IDKhachHang = idKhachHang;
            dv.SoLuong = soLuong;
            dv.TongTien = int.Parse(tongTien.ToString());

            ChiTietHoaDon ct = new ChiTietHoaDon();
            ct.MaKH = khachHang.ID;
            ct.TenKH = khachHang.TenKH;
            ct.SDT = khachHang.SoDienThoai;
            ct.DiaChi = khachHang.DiaChi;
            ct.IDDV = idDichVu;
            ct.IDNhanVien = idNhanVien;
            ct.TongTien = int.Parse(tongTien.ToString());
            ct.SoLuong = soLuong;
            ct.Mail = khachHang.Mail;            
            ct.NgayTao = DateTime.Now;
            ct.TenDV = dichVu.TenDV;
            db.ChiTietHoaDons.Add(ct);
            db.SaveChanges();
            dv.IDHoaDon = ct.ID;

            db.HoaDons.Add(dv);
            db.SaveChanges();
            return Json(new { data = true });
        }

        public JsonResult EditHoaDon()
        {
            var id = int.Parse(Request.Form["ID"].ToString());
            var soLuong = int.Parse(Request.Form["SoLuong"].ToString());
            var idNhanVien = int.Parse(Request.Form["idNhanVien"].ToString());
            var idKhachHang = int.Parse(Request.Form["idKhachHang"].ToString());
            var idDichVu = int.Parse(Request.Form["idDichVu"].ToString());

            var giaTien = db.DichVus.SingleOrDefault(x => x.ID == idDichVu).SoTien;
            var nhanVien = db.NhanViens.SingleOrDefault(x => x.ID == idNhanVien);
            var dichVu = db.DichVus.SingleOrDefault(x => x.ID == idDichVu);
            var khachHang = db.KhachHangs.SingleOrDefault(x => x.ID == idKhachHang);

            HoaDon dv = db.HoaDons.SingleOrDefault(x => x.ID == id);
            var tongTien = giaTien * soLuong;
            dv.IDDichVu = idDichVu;
            dv.IDNhanVien = idNhanVien;
            dv.IDKhachHang = idKhachHang;
            dv.SoLuong = soLuong;
            dv.TongTien = int.Parse(tongTien.ToString());

            ChiTietHoaDon ct = db.ChiTietHoaDons.SingleOrDefault(x => x.ID == dv.IDHoaDon);
            ct.MaKH = khachHang.ID;
            ct.TenKH = khachHang.TenKH;
            ct.SDT = khachHang.SoDienThoai;
            ct.DiaChi = khachHang.DiaChi;
            ct.IDDV = idDichVu;
            ct.IDNhanVien = idNhanVien;
            ct.TongTien = int.Parse(tongTien.ToString());
            ct.SoLuong = soLuong;
            ct.Mail = khachHang.Mail;
            ct.NgayTao = DateTime.Now;
            ct.TenDV = dichVu.TenDV;

            db.SaveChanges();
            return Json(new { data = true });
        }

        public JsonResult DeleteHoaDon(int id)
        {
            var hd = db.HoaDons.SingleOrDefault(x => x.ID == id);
            var ct = db.ChiTietHoaDons.SingleOrDefault(x => x.ID == id);
            db.HoaDons.Remove(hd);
            db.ChiTietHoaDons.Remove(ct);
            db.SaveChanges();
            return Json(new { data=true});
        }

        [HttpGet]
        public JsonResult ChiTiet(int id)
        {
            var hd = db.HoaDons.SingleOrDefault(x => x.ID == id);
            var ct = db.ChiTietHoaDons.SingleOrDefault(x => x.ID == hd.IDHoaDon);
            var nv = db.NhanViens.SingleOrDefault(x => x.ID == ct.IDNhanVien).TenNV;
            return Json(new { data = ct, data1 = nv }, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult AddandEditHoaDonModal(int id)
        {
            var result = new HoaDon();
            string mode = "Add";
            result = db.HoaDons.FirstOrDefault(x => x.ID == id);
            if (result == null)
            {
                mode = "Add";
                result = new HoaDon();
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