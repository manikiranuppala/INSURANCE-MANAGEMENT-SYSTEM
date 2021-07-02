using iCareTPADAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iCareTPAWebApp.Controllers
{
    public class InsurerController : Controller
    {
        iCareTPARepository repo = new iCareTPARepository();

        [HttpGet]
        public ActionResult Index()
        {
            if (Session["EmailId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            List<Insurer> entityInsurers = repo.GetAllInsurers();
            List<Models.Insurer> modelInsurers = entityInsurers.Select(x =>new Models.Insurer()
            {
                InsureId=x.InsureId,
                InsurerName=x.InsurerName,
                RegistrationNo=(int)x.RegistrationNo,
                HeadOffice=x.HeadOffice
            }).ToList();
            return View(modelInsurers);
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Session["EmailId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.Insurer insurer)
        {
            if(Session["EmailId"]==null)
            {
                return RedirectToAction("Login", "User");
            }
            if(ModelState.IsValid)
            {
                Insurer insurerInfo = new Insurer()
                {
                    InsurerName = insurer.InsurerName,
                    RegistrationNo = insurer.RegistrationNo,
                    HeadOffice = insurer.HeadOffice
                };

                bool result = repo.AddInsurer(insurerInfo);
                if (!result)
                {
                    TempData["ErrorMsg"] = "Adding Insurer Failed";
                    return RedirectToAction("Index");
                }
            }
            TempData["SuccessMsg"] = "Successfully Added Insurer Data";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (Session["EmailId"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            Insurer entityInsurer =repo.GetInsurer((int)id);
            Models.Insurer modelInsurer = new Models.Insurer()
            {
                InsureId=entityInsurer.InsureId,
                InsurerName=entityInsurer.InsurerName,
                RegistrationNo=(int)entityInsurer.RegistrationNo,
                HeadOffice=entityInsurer.HeadOffice
            };
            TempData["InsureId"] = entityInsurer.InsureId;
            return View(modelInsurer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.Insurer insurerInfo)
        {
            if (Session["EmailId"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            Insurer entityInsurer = new Insurer()
            {
                InsureId = Convert.ToInt32(TempData["InsureId"]),
                InsurerName = insurerInfo.InsurerName,
                RegistrationNo = insurerInfo.RegistrationNo,
                HeadOffice = insurerInfo.HeadOffice
            };

            bool result = repo.UpdateInsurer(entityInsurer);
            if (!result)
            {
                TempData["ErrorMsg"] = "Editing Insurer Data Failed";
                return RedirectToAction("Index");
            }
            TempData["SuccessMsg"] = "Successfully Insurer Hospital Data";
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            if (Session["EmailId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            Insurer entityInsurer = repo.GetInsurer((int)id);
            Models.Insurer modelInsurer = new Models.Insurer()
            {
                InsureId = entityInsurer.InsureId,
                InsurerName = entityInsurer.InsurerName,
                RegistrationNo = (int)entityInsurer.RegistrationNo,
                HeadOffice = entityInsurer.HeadOffice
            };
            return View(modelInsurer);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["EmailId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            Insurer entityInsurer = repo.GetInsurer((int)id);
            Models.Insurer modelInsurer = new Models.Insurer()
            {
                InsureId = entityInsurer.InsureId,
                InsurerName = entityInsurer.InsurerName,
                RegistrationNo = (int)entityInsurer.RegistrationNo,
                HeadOffice = entityInsurer.HeadOffice
            };
            return View(modelInsurer);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["EmailId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            bool result = repo.DeleteInsurer(id);
            if (!result)
            {
                TempData["ErrorMsg"] = "Deleting Insurer Failed";
                return RedirectToAction("Index");
            }
            TempData["SuccessMsg"] = "Insurer Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}