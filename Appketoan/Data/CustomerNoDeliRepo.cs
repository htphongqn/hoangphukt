using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appketoan.Data
{
    public class CustomerNoDeliRepo
    {
        private AppketoanDataContext db = new AppketoanDataContext();

        public virtual List<CUSTOMER_NODELI> GetListByContainsFullName(string name)
        {
            return this.db.CUSTOMER_NODELIs.Where(n => n.CUS_FULLNAME.Contains(name)).OrderBy(a => a.CUS_FULLNAME).ToList();
        }
        public virtual List<CUSTOMER_NODELI> GetListByContainsPhone(string phone)
        {
            return this.db.CUSTOMER_NODELIs.Where(n => n.CUS_PHONE.Contains(phone)).OrderBy(a => a.CUS_PHONE).ToList();
        }
        public virtual List<CUSTOMER_NODELI> GetListByContainsAddress(string address)
        {
            return this.db.CUSTOMER_NODELIs.Where(n => n.CUS_ADDRESS.Contains(address)).OrderBy(a => a.CUS_ADDRESS).ToList();
        }
        public virtual List<CUSTOMER_NODELI> GetListByNameAndProcess(string name, int process)
        {
            return this.db.CUSTOMER_NODELIs.Where(n => (n.CUS_FULLNAME.Contains(name) || n.CUS_PHONE.Contains(name) || n.CUS_ADDRESS.Contains(name) || name == "")                 
                && (n.PROCESS_STATUS == process)).OrderByDescending(n => n.ID).OrderByDescending(n=>n.CUS_FAX_DATE).ToList();
        }
        public virtual CUSTOMER_NODELI GetById(int id)
        {
            try
            {
                return this.db.CUSTOMER_NODELIs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }
        public virtual List<CUSTOMER_NODELI> GetAll()
        {
            return this.db.CUSTOMER_NODELIs.OrderByDescending(n => n.ID).ToList();
        }
        public virtual void Create(CUSTOMER_NODELI cus)
        {
            try
            {
                this.db.CUSTOMER_NODELIs.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Update(CUSTOMER_NODELI cus)
        {
            try
            {
                CUSTOMER_NODELI cusOld = this.GetById(cus.ID);
                cusOld = cus;
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public virtual void Remove(int id)
        {
            try
            {
                CUSTOMER_NODELI cus = this.GetById(id);
                this.Remove(cus);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Remove(CUSTOMER_NODELI cus)
        {
            try
            {
                db.CUSTOMER_NODELIs.DeleteOnSubmit(cus);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(int id)
        {
            try
            {
                CUSTOMER_NODELI cus = this.GetById(id);
                return this.Delete(cus);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(CUSTOMER_NODELI cus)
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