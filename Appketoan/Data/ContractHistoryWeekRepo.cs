using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appketoan.Data
{
    public class ContractHistoryWeekRepo
    {
        private AppketoanDataContext db = new AppketoanDataContext();

        public virtual List<CONTRACT_HISTORYWEEK> GetListByContractID(int id)
        {
            return this.db.CONTRACT_HISTORYWEEKs.Where(n => (n.ID_CONT == id)).OrderByDescending(n => n.ID).ToList();
        }
        public virtual CONTRACT_HISTORYWEEK GetById(int id)
        {
            try
            {
                return this.db.CONTRACT_HISTORYWEEKs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }
        public virtual List<CONTRACT_HISTORYWEEK> GetAll()
        {
            return this.db.CONTRACT_HISTORYWEEKs.OrderByDescending(n => n.ID).ToList();
        }
        public virtual void Create(CONTRACT_HISTORYWEEK cus)
        {
            try
            {
                this.db.CONTRACT_HISTORYWEEKs.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Update(CONTRACT_HISTORYWEEK cus)
        {
            try
            {
                CONTRACT_HISTORYWEEK cusOld = this.GetById(cus.ID);
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
                CONTRACT_HISTORYWEEK cus = this.GetById(id);
                this.Remove(cus);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Remove(CONTRACT_HISTORYWEEK cus)
        {
            try
            {
                db.CONTRACT_HISTORYWEEKs.DeleteOnSubmit(cus);
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
                CONTRACT_HISTORYWEEK cus = this.GetById(id);
                return this.Delete(cus);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(CONTRACT_HISTORYWEEK cus)
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