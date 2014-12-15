using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appketoan.Data
{
    public class EmployerRepo
    {
        private AppketoanDataContext db = new AppketoanDataContext();

        public virtual EMPLOYER GetByNam(string name)
        {
            try
            {
                return this.db.EMPLOYERs.Single(u => u.EMP_NAME == name);
            }
            catch
            {
                return null;
            }
        }
        public virtual List<EMPLOYER> GetListByName(string name)
        {
            return this.db.EMPLOYERs.Where(n => (n.EMP_NAME.Contains(name) || name == "")).OrderBy(n => n.EMP_NAME).ToList();
        }
        public virtual EMPLOYER GetById(int id)
        {
            try
            {
                return this.db.EMPLOYERs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }
        public virtual List<EMPLOYER> GetAll()
        {
            return this.db.EMPLOYERs.OrderByDescending(n => n.ID).ToList();
        }
        public virtual List<EMPLOYER> GetAllSortName()
        {
            return this.db.EMPLOYERs.OrderBy(n => n.EMP_NAME).ToList();
        }
        public virtual void Create(EMPLOYER cus)
        {
            try
            {
                this.db.EMPLOYERs.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Update(EMPLOYER cus)
        {
            try
            {
                EMPLOYER cusOld = this.GetById(cus.ID);
                cusOld = cus;
                db.SubmitChanges();
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }


        public virtual void Remove(int id)
        {
            try
            {
                EMPLOYER cus = this.GetById(id);
                this.Remove(cus);
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual void Remove(EMPLOYER cus)
        {
            try
            {
                db.EMPLOYERs.DeleteOnSubmit(cus);
                db.SubmitChanges();
            }
            catch //(Exception e)
            {
                //throw new Exception(e.Message);
            }
        }
        public virtual int Delete(int id)
        {
            try
            {
                EMPLOYER cus = this.GetById(id);
                return this.Delete(cus);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(EMPLOYER cus)
        {
            try
            {
                //user.IsDelete = true;
                db.SubmitChanges();
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}