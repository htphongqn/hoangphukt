using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appketoan.Data
{
    public class BillRepo
    {
        private AppketoanDataContext db = new AppketoanDataContext();

        public virtual List<BILL> GetListByContractID(int ContractID)
        {
            return this.db.BILLs.Where(a => a.ID_CONT == ContractID).ToList();
        }
        public virtual BILL GetById(int id)
        {
            try
            {
                return this.db.BILLs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }
        public virtual List<BILL> GetAll()
        {
            return this.db.BILLs.ToList();
        }
        public virtual void Create(BILL b)
        {
            try
            {
                this.db.BILLs.InsertOnSubmit(b);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Update(BILL b)
        {
            try
            {
                BILL bOld = this.GetById(b.ID);
                bOld = b;
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
                BILL b = this.GetById(id);
                this.Remove(b);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Remove(BILL b)
        {
            try
            {
                db.BILLs.DeleteOnSubmit(b);
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
                BILL b = this.GetById(id);
                return this.Delete(b);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(BILL b)
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