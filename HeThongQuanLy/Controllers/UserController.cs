using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using HeThongQuanLy.Common;

namespace HeThongQuanLy.Controllers
{
    public class UserController : BaseController
    {
        HeThong db = new HeThong();
        public ActionResult Index()
        {
            //if (Session["GroupID"].ToString() != "1" || Session["GroupID"].ToString() != "2" || Session["GroupID"].ToString() != "3")
            //{
            //    Response.StatusCode = 404;
            //    return null;
            //}
            return View();
        }
        public JsonResult GetUserList()
        {
            var listUser = db.NhanViens.ToList();
            return Json(new { data = listUser });
        }

        //public JsonResult AddUser(string userN, string pass, string name, string email)
        //{
        //    if (ModelState.IsValid)
        //    {
                //NhanVien user = new User();
                //user.UserName = userN;
                //user.Password = pass;
                //user.Name = name;
                //user.Email = email;
                //user.GroupID = "1";
                //user.Status = true;
                //db.NhanViens.Add(user);
                //db.SaveChanges();
                //return Json(new { data = true });
        //    }
        //    else
        //    {
        //        return Json(new { data = true });
        //    }
        //}
        //public JsonResult EditUser(int id, string userN, string pass, string name, string email, string group)
        //{
            //if (ModelState.IsValid)
            //{
            //    NhanVien user = db.NhanViens.Single(x => x.ID == id);
            //    user.UserName = userN;
            //    user.Password = pass;
            //    user.Name = name;
            //    user.Email = email;
            //    user.GroupID = group;
            //    user.Status = true;
            //    try
            //    {
            //        db.SaveChanges();
            //        return Json(new { data = true });
            //    }
            //    catch (Exception)
            //    {
            //        return Json(new { data = false });
            //    }
            //}
            //else
            //{
            //    return Json(new { data = true });
            //}
        //}
        public JsonResult DeleteUser(int id)
        {
            NhanVien hs = db.NhanViens.Single(x => x.ID == id);
            if (hs != null)
            {
                db.NhanViens.Remove(hs);
                db.SaveChanges();
                return Json(new { data = true });
            }
            else
                return Json(new { data = false });
        }
        public ActionResult AddandEditUserModal(int id)
        {
            NhanVien result = new NhanVien();
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
        [HttpGet]
        public JsonResult LoadGroup()
        {
            return Json(new { data = db.Nhoms.ToList() }, JsonRequestBehavior.AllowGet);
        }
    }
}