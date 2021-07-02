using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iCareTPADAL;

namespace iCareTPAWebApp.Controllers
{
    public class UserController : Controller
    {
        iCareTPARepository repo = new iCareTPARepository();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Models.User userInfo)
        {
            ViewBag.SuccessMsg = ViewBag.SuccessMsg;
            string emailID = userInfo.EmailId;
            string password = userInfo.Password;

            iCareTPADAL.User user = repo.ValidateUser(emailID, password);

            if (user == null)
            {
                ViewBag.ErrorMsg = "Invalid credentials";
                return View();
            }
            else
            {
                Session["EmailId"] = emailID;
                Session["UserId"] = user.UserId;
            }
            return RedirectToAction("Index","Hospital");
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Models.User user)
        {
            if (ModelState.IsValid)
            {
                iCareTPADAL.User userInfo = new User()
                {
                    EmailId = user.EmailId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Password = user.Password
                };
                bool result = repo.AddUser(userInfo);
                if (!result)
                {
                    return View("Error");
                }
                TempData["SuccessMsg"] = "User Registered Successfully";
            }
            return RedirectToAction("Login");
        }
    }
}