using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Helpers;
using WebApplication1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        ApplicationDBContext context;
        public LoginController()
        {
            context = new ApplicationDBContext();
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            var userObj = context.Users.Where(u => u.username == user.username
            && u.password == user.password
            && u.usertype == user.usertype).SingleOrDefault();
            if (userObj != null)
            {
                if (user.username.Equals(userObj.username) && user.password.Equals(userObj.password) && user.usertype.Equals(userObj.usertype))
                {
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "user", userObj);
                    //HttpContext.Session.SetString("username", "Bhawna Gunwani");
                    HttpContext.Session.SetString("Role", user.usertype);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Invalid Credentials";
                    return View("Index");
                }

            }
            else
            {
                ViewBag.Error = "Please Enter Your Credentials.";
                return View("Index");
            }

        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }
    }
}
