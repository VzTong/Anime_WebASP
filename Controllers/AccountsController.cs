using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test1.Models;

// haha ten project alf test1
//dổi dùm t đc ko
// tạo project khác
//omg, m làm dùm lun đê kkk
// 50k =>> Bữa m thấy có lỗi mà, làm đê  m vắt khô t đi 

namespace test1.Controllers
{
    public class AccountsController : Controller
    {

        WEB_Anime_ASPEntities _db = new WEB_Anime_ASPEntities();

        //GET: Register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        public ActionResult Register(Models.AccountModel _User)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // check user_name
                    var check = _db.Accounts.FirstOrDefault(s => s.User_Name == _User.User_name);
                    if (check == null)
                    {
                        _User.Password = GetMD5(_User.Password);
                        _db.Configuration.ValidateOnSaveEnabled = false;
                        var account = new Account();
                        account.FullName = _User.Fullname_user;
                        account.Phone = _User.Phone;
                        account.Password = _User.Password;
                        account.User_Name = _User.User_name;
                        _db.Accounts.Add(account);
                        _db.SaveChanges();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.error = "User name already exists";
                        return View();
                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("Something went wrong.");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(string UserName, string password)
        {
            // UserName : ng dùng nhập
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(password);
                var check_pass = _db.Accounts.Where(s => s.Password.Equals(f_password)).ToList();
                if (check_pass.Count() == 0)
                {
                    ViewBag.error = "Incorrect password";
                    return RedirectToAction("Login");
                }

                var data = _db.Accounts.Where(s => s.User_Name.Equals(UserName) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["UserName"] = data.FirstOrDefault().User_Name;
                    Session["id"] = data.FirstOrDefault().Id;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        //Logout

        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Index", "Home");
        }


        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;
        }

    }
}