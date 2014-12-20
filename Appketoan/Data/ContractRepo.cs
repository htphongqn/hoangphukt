using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appketoan.Data
{
    public class ContractRepo
    {
        private AppketoanDataContext db = new AppketoanDataContext();

        public virtual List<CONTRACT> GetListByStatus(int statusID)
        {
            return this.db.CONTRACTs.Where(n => (n.CONT_STATUS == statusID)).OrderByDescending(n => n.ID).ToList();
        }
        public virtual CONTRACT GetByIdNo(string idno)
        {
            try
            {
                return this.db.CONTRACTs.Single(u => u.CONT_NO == idno);
            }
            catch
            {
                return null;
            }
        }
        public virtual CONTRACT GetById(int id)
        {
            try
            {
                return this.db.CONTRACTs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }
        public virtual List<CONTRACT> GetAll()
        {
            return this.db.CONTRACTs.OrderByDescending(n => n.ID).ToList();
        }
        public virtual void Create(CONTRACT cus)
        {
            try
            {
                this.db.CONTRACTs.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Update(CONTRACT cus)
        {
            try
            {
                CONTRACT cusOld = this.GetById(cus.ID);
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
                CONTRACT cus = this.GetById(id);
                this.Remove(cus);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Remove(CONTRACT cus)
        {
            try
            {
                db.CONTRACTs.DeleteOnSubmit(cus);
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
                CONTRACT cus = this.GetById(id);
                cus.IS_DELETE = true;
                return this.Delete(cus);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(CONTRACT cus)
        {
            try
            {
                CONTRACT cusOld = this.GetById(cus.ID);
                cusOld = cus;
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