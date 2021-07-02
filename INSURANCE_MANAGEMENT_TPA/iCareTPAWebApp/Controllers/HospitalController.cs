using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iCareTPADAL;

namespace iCareTPAWebApp.Controllers
{
    public class HospitalController : Controller
    {
        iCareTPARepository repo = new iCareTPARepository();

        [HttpGet]
        public ActionResult Index()
        {
            if (Session["EmailId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            List<Hospital> entityHospitals = repo.GetAllHospitals();
            List<Insurer> entityInsurers = repo.GetAllInsurers();
            List<Models.Hospital> modelHospitals = entityHospitals.Select(x => new Models.Hospital()
            {
                HospitalId=x.HospitalId,
                HospitalName = x.HospitalName,
                Address = x.Address,
                City = x.City,
                State = x.State,
                Pincode = x.Pincode,
                InsurerName = entityInsurers.Where(y => y.InsureId == x.InsurerId).Select(z => z.InsurerName).FirstOrDefault()
            }).ToList();
            return View(modelHospitals);
        }

        [HttpGet]
        public ActionResult Create()
        {
            if(Session["EmailId"]==null)
            {
                return RedirectToAction("Login", "User");
            }
            List<Insurer> entityInsurers = repo.GetAllInsurers();
            List<SelectListItem> insurerSelectList = entityInsurers.Select(x => new SelectListItem()
                {
                Text = x.InsurerName,
                Value = x.InsureId.ToString()
            }).ToList();
            ViewBag.insurerList = insurerSelectList;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.Hospital hospital)
        {
            if (Session["EmailId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            if(ModelState.IsValid)
            {
                Hospital hospitalInfo = new Hospital()
                {
                    HospitalName=hospital.HospitalName,
                    Address=hospital.Address,
                    City=hospital.City,
                    State=hospital.State,
                    Pincode=hospital.Pincode,
                    InsurerId=Int32.Parse(hospital.InsurerName)
                };

                bool result = repo.AddHospital(hospitalInfo);
                if(!result)
                {
                    TempData["ErrorMsg"] = "Adding Hospital Failed";
                    return RedirectToAction("Index");
                }
            }
            TempData["SuccessMsg"] = "Successfully Added Hospital";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (Session["EmailId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            Hospital entityHospital = repo.GetHospital((int)id);
            List<Insurer> entityInsurers = repo.GetAllInsurers();

            Models.Hospital modelsHospital = new Models.Hospital()
            {
                HospitalId = entityHospital.HospitalId,
                HospitalName = entityHospital.HospitalName,
                Address = entityHospital.Address,
                City = entityHospital.City,
                State = entityHospital.State,
                Pincode = entityHospital.Pincode,
                InsurerName = entityHospital.InsurerId.ToString() //entityInsurers.Where(y => y.InsureId == entityHospital.InsurerId).Select(z => z.InsurerName).FirstOrDefault()
            };

            List<SelectListItem> insurerSelectList = entityInsurers.Select(x => new SelectListItem()
            {
                Text = x.InsurerName,
                Value = x.InsureId.ToString()
            }).ToList();
            var a = insurerSelectList.Where(y => Int32.Parse(y.Value) == entityHospital.InsurerId).FirstOrDefault();
            a.Selected = true;
            ViewBag.insurerList = insurerSelectList;
            TempData["HospitalId"] = entityHospital.HospitalId;
            return View(modelsHospital);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.Hospital hospitalInfo)
        {
            if (Session["EmailId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            Hospital entityHospital = new Hospital()
            {
                HospitalId= Int32.Parse(TempData["HospitalId"].ToString()),
                HospitalName = hospitalInfo.HospitalName,
                Address = hospitalInfo.Address,
                City = hospitalInfo.City,
                State = hospitalInfo.State,
                Pincode = hospitalInfo.Pincode,
                InsurerId = Int32.Parse(hospitalInfo.InsurerName)
            };
            bool result = repo.UpdateHospital(entityHospital);
            if(!result)
            {
                TempData["ErrorMsg"] = "Editing Hospital Data Failed";
                return RedirectToAction("Index");
            }
            TempData["SuccessMsg"] = "Successfully Updated Hospital Data";
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (Session["EmailId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            Hospital entityHospital = repo.GetHospital((int)id);
            List<Insurer> entityInsurers = repo.GetAllInsurers();
            Models.Hospital modelsHospital = new Models.Hospital()
            {
                HospitalId = entityHospital.HospitalId,
                HospitalName = entityHospital.HospitalName,
                Address = entityHospital.Address,
                City = entityHospital.City,
                State = entityHospital.State,
                Pincode = entityHospital.Pincode,
                InsurerName = entityInsurers.Where(y => y.InsureId == entityHospital.InsurerId).Select(z => z.InsurerName).FirstOrDefault()
            };
            return View(modelsHospital);
        }

        [HttpGet]
        public ActionResult SearchHospitals()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult SearchHospitals(Models.Hospital hospitalInfo)
        {
            List<Hospital> entityHospitals = repo.SearchHospital(hospitalInfo.HospitalName);
            if(entityHospitals.Count<=0)
            {
                TempData["ErrorMsg"] = "No Hospital Found with this Name";
                return View();
            }
            List<Models.Hospital> modelHospitals = entityHospitals.Select(x => new Models.Hospital()
            {
                HospitalName = x.HospitalName,
                Address = x.Address,
                City = x.City,
                State = x.State,
                Pincode = x.Pincode
            }).ToList();
            return View("SearchByNameResult",modelHospitals);
        }

        [HttpGet]
        public ActionResult SearchHospitalsByPincode()
        {

            return View();
        }

        [HttpPost]
        public ActionResult SearchHospitalsByPincode(Models.Hospital hospitalInfo)
        {
            List<stpFindHospitalByPincode_Result> entityHospitals = repo.SearchHospitalByPincode(hospitalInfo.Pincode);
            if (entityHospitals.Count <= 0)
            {
                TempData["ErrorMsg"] = "No Hospital Found with this Name";
                return View();
            }
            List<Insurer> entityInsurers = repo.GetAllInsurers();
            List<Models.Hospital> modelHospitals = entityHospitals.Select(x => new Models.Hospital()
            {
                HospitalName = x.HospitalName,
                Address = x.Address,
                City = x.City,
                State = x.State,
                InsurerName= entityInsurers.Where(y => y.InsureId == x.InsurerId).Select(z => z.InsurerName).FirstOrDefault()
            }).ToList();
            return View("SearchByPincodeResult", modelHospitals);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (Session["EmailId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            Hospital entityHospital = repo.GetHospital((int)id);
            List<Insurer> entityInsurers = repo.GetAllInsurers();

            Models.Hospital modelHospital = new Models.Hospital()
            {
                HospitalId = entityHospital.HospitalId,
                HospitalName = entityHospital.HospitalName,
                Address = entityHospital.Address,
                City = entityHospital.City,
                State = entityHospital.State,
                Pincode = entityHospital.Pincode,
                InsurerName = entityInsurers.Where(y => y.InsureId == entityHospital.InsurerId).Select(z => z.InsurerName).FirstOrDefault()
            };
            return View(modelHospital);
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
            bool result = repo.DeleteHospital(id);
            if (!result)
            {
                TempData["ErrorMsg"] = "Deleting Hospital Failed";
                return RedirectToAction("Index");
            }
            TempData["SuccessMsg"] = "Hospital Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}