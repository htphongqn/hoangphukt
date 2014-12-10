using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appketoan.Data
{
    public class CustomerRepo
    {
        private AppketoanDataContext db = new AppketoanDataContext();

        public virtual List<CUSTOMER> GetListByContainsFullName(string name)
        {
            return this.db.CUSTOMERs.Where(n => n.CUS_FULLNAME.Contains(name)).OrderBy(a => a.CUS_FULLNAME).ToList();
        }
        public virtual List<CUSTOMER> GetListByContainsPhone(string phone)
        {
            return this.db.CUSTOMERs.Where(n => n.CUS_PHONE.Contains(phone)).OrderBy(a => a.CUS_PHONE).ToList();
        }
        public virtual List<CUSTOMER> GetListByContainsAddress(string address)
        {
            return this.db.CUSTOMERs.Where(n => n.CUS_ADDRESS.Contains(address)).OrderBy(a => a.CUS_ADDRESS).ToList();
        }
        public virtual List<CUSTOMER> GetListByContainsCMND(string cmnd)
        {
            return this.db.CUSTOMERs.Where(n => n.CUS_CMND.Contains(cmnd)).OrderBy(a => a.CUS_CMND).ToList();
        }
        public virtual CUSTOMER GetByNameandAddress(string name, string address)
        {
            try
            {
                return this.db.CUSTOMERs.Single(u => u.CUS_FULLNAME == name && u.CUS_ADDRESS == address);
            }
            catch
            {
                return null;
            }
        }
        public virtual List<CUSTOMER> GetListByName(string name)
        {
            return this.db.CUSTOMERs.Where(n => (n.CUS_FULLNAME.Contains(name) || n.CUS_PHONE.Contains(name) || n.CUS_ADDRESS.Contains(name) || n.CUS_CMND.Contains(name) || name == "")).OrderByDescending(n => n.ID).OrderByDescending(n => n.CUS_UPDATE_DATE).ToList();
        }
        public virtual CUSTOMER GetById(int id)
        {
            try
            {
                return this.db.CUSTOMERs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }
        public virtual List<CUSTOMER> GetAll()
        {
            return this.db.CUSTOMERs.OrderByDescending(n => n.ID).ToList();
        }
        public virtual void Create(CUSTOMER cus)
        {
            try
            {
                this.db.CUSTOMERs.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Update(CUSTOMER cus)
        {
            try
            {
                CUSTOMER cusOld = this.GetById(cus.ID);
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
                CUSTOMER cus = this.GetById(id);
                this.Remove(cus);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Remove(CUSTOMER cus)
        {
            try
            {
                db.CUSTOMERs.DeleteOnSubmit(cus);
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
                CUSTOMER cus = this.GetById(id);
                return this.Delete(cus);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(CUSTOMER cus)
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