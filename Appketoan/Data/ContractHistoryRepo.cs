using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appketoan.Data
{
    public class ContractHistoryRepo
    {
        private AppketoanDataContext db = new AppketoanDataContext();

        public virtual List<CONTRACT_HISTORY> GetListByContractID(int id)
        {
            return this.db.CONTRACT_HISTORies.Where(n => (n.ID_CONT == id)).OrderByDescending(n => n.ID).ToList();
        }
        public virtual CONTRACT_HISTORY GetById(int id)
        {
            try
            {
                return this.db.CONTRACT_HISTORies.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }
        public virtual List<CONTRACT_HISTORY> GetAll()
        {
            return this.db.CONTRACT_HISTORies.OrderByDescending(n => n.ID).ToList();
        }
        public virtual void Create(CONTRACT_HISTORY cus)
        {
            try
            {
                this.db.CONTRACT_HISTORies.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Update(CONTRACT_HISTORY cus)
        {
            try
            {
                CONTRACT_HISTORY cusOld = this.GetById(cus.ID);
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
                CONTRACT_HISTORY cus = this.GetById(id);
                this.Remove(cus);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Remove(CONTRACT_HISTORY cus)
        {
            try
            {
                db.CONTRACT_HISTORies.DeleteOnSubmit(cus);
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
                CONTRACT_HISTORY cus = this.GetById(id);
                return this.Delete(cus);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(CONTRACT_HISTORY cus)
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