using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace iCareTPADAL
{
    public class iCareTPARepository
    {
        private iCareTPADBEntities db;

        #region Constructor
        public iCareTPARepository()
        {
            db = new iCareTPADBEntities();
        }
        #endregion

        #region Methods

        #region Users
        public User ValidateUser(string emailId,string password)
        {
            User user = null;
            try
            {
                user = db.Users.Where(x => x.EmailId == emailId && x.Password == password).FirstOrDefault();

            }
            catch(Exception e)
            {
                user = null;
            }
            return user;
        }

        public bool AddUser(User userInfo)
        {
            if(userInfo==null)
            {
                return false;
            }
            try
            {
                db.Users.Add(userInfo);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Hospital
        public bool AddHospital(Hospital hospitalInfo)
        {
            if (hospitalInfo == null)
            {
                return false;
            }
            try
            {
                db.Hospitals.Add(hospitalInfo);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public List<Hospital> GetAllHospitals()
        {
            try
            {
                return db.Hospitals.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Hospital GetHospital(int id)
        {
            Hospital hospital = null;
            try
            {
                hospital = db.Hospitals.Find(id);
            }
            catch (Exception)
            {
                hospital = null;
            }
            return hospital;
        }

        public bool UpdateHospital(Hospital hospitalInfo)
        {
            if(hospitalInfo==null)
            {
                return false;
            }
            try
            {
                db.Entry(hospitalInfo).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Insurer
        public List<Insurer> GetAllInsurers()
        {
            try
            {
                return db.Insurers.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool AddInsurer(Insurer insurerInfo)
        {
            if (insurerInfo == null)
            {
                return false;
            }
            try
            {
                db.Insurers.Add(insurerInfo);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region SearchHospital
        public List<Hospital> SearchHospital(string name)
        {
            List<Hospital> hospitals = null;
            try
            {
                hospitals = db.Hospitals.Where(x=>x.HospitalName==name).ToList();
            }
            catch (Exception)
            {
                hospitals = null;
            }
            return hospitals;
        }

        public List<stpFindHospitalByPincode_Result> SearchHospitalByPincode(int pincode)
        {
            List<stpFindHospitalByPincode_Result> hospitals = null;
            try
            {
                hospitals =db.stpFindHospitalByPincode(pincode).ToList();
            }
            catch (Exception)
            {
                hospitals = null;
            }
            return hospitals;
        }
        #endregion



        #region ExtraMethods Apart from The Case Study PDF for Application completion

        public bool DeleteHospital(int id)
        {
            try
            {
                Hospital hospital = GetHospital(id);
                db.Hospitals.Remove(hospital);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public Insurer GetInsurer(int id)
        {
            Insurer insurer = null;
            try
            {
                insurer = db.Insurers.Find(id);
            }
            catch (Exception)
            {
                insurer = null;
            }
            return insurer;
        }

        public bool UpdateInsurer(Insurer insurerInfo)
        {
            if (insurerInfo == null)
            {
                return false;
            }
            try
            {
                db.Entry(insurerInfo).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool DeleteInsurer(int id)
        {
            try
            {
                Insurer insurer= GetInsurer(id);
                db.Insurers.Remove(insurer);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        #endregion

        #endregion
    }
}
