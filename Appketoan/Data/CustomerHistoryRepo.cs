using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appketoan.Data
{
    public class CustomerHistoryRepo
    {
        private AppketoanDataContext db = new AppketoanDataContext();

        public virtual List<CUSTOMER_HISTORY> GetListByCusID(int id)
        {
            return this.db.CUSTOMER_HISTORies.Where(n => (n.ID_CUS == id)).OrderByDescending(n => n.ID).ToList();
        }

        public virtual CUSTOMER_HISTORY GetById(int id)
        {
            try
            {
                return this.db.CUSTOMER_HISTORies.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }
        public virtual List<CUSTOMER_HISTORY> GetAll()
        {
            return this.db.CUSTOMER_HISTORies.OrderByDescending(n => n.ID).ToList();
        }
        public virtual void Create(CUSTOMER_HISTORY cus)
        {
            try
            {
                this.db.CUSTOMER_HISTORies.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Update(CUSTOMER_HISTORY cus)
        {
            try
            {
                CUSTOMER_HISTORY cusOld = this.GetById(cus.ID);
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
                CUSTOMER_HISTORY cus = this.GetById(id);
                this.Remove(cus);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Remove(CUSTOMER_HISTORY cus)
        {
            try
            {
                db.CUSTOMER_HISTORies.DeleteOnSubmit(cus);
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
                CUSTOMER_HISTORY cus = this.GetById(id);
                return this.Delete(cus);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(CUSTOMER_HISTORY cus)
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