using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HeThongQuanLy.Models;
using HeThongQuanLy.Common;
using Model.EF;
using System.Web.Security;

namespace HeThongQuanLy.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View(); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                string userName = fc["userName"].ToString();
                string password = fc["password"].ToString();
                var result = LoginState(userName, password, true);

                if (result == 1)
                {

                    NhanVien user = db.NhanViens.SingleOrDefault(x => x.TenDangNhap == userName);
                    var name = db.NhanViens.SingleOrDefault(x => x.TenDangNhap == userName).TenNV;
                    //hinh = db.GiangViens.SingleOrDefault(z => z.IDTaiKhoan == taikhoan.ID).Anh;
                    //Session["Anh"] = hinh;
                    Session["GroupID"] = user.IDNhom;
                    Session["Name"]=name;
                    FormsAuthentication.SetAuthCookie(name, false);//?
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Không có tài khoản");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản không được kích hoạt");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Sai mật khẩu");
                }
                else if (result == -3)
                {
                    ModelState.AddModelError("", "Không có quyền đăng nhập vào Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Tài khoản không đúng");
                }
            }
            return View("Index");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
            
        }
        HeThong db = new HeThong();
        public int LoginState(string userName, string passWord, bool AdminLogin = false)
        {
            var result = db.NhanViens.SingleOrDefault(x => x.TenDangNhap == userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (AdminLogin == true)
                {
                    if (result.IDNhom == 1 || result.IDNhom == 2 || result.IDNhom == 3)
                    {
                        //if (result.Status == false)
                        //{
                        //    return -1;
                        //}
                        //else
                        //{
                            if (result.MatKhau == passWord)
                                return 1;
                            else
                                return -2;
                        //}
                    }
                    else
                    {
                        return -3;
                    }
                }
                else
                {
                    //if (result.Status == false)
                    //{
                    //    return -1;
                    //}
                    //else
                    //{
                        if (result.MatKhau == passWord)
                            return 1;
                        else
                            return -2;
                    //}
                }
            }
        }

        public NhanVien GetByUserName(string userName)
        {
            return db.NhanViens.Single(x => x.TenDangNhap == userName);
        }
    }
}