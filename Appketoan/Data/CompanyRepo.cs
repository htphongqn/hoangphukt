using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appketoan.Data
{
    public class CompanyRepo
    {
        private AppketoanDataContext db = new AppketoanDataContext();

        public virtual List<COMPANY> GetAllSortName()
        {
            return this.db.COMPANies.OrderBy(n => n.COM_NAME).ToList();
        }
        public virtual COMPANY GetByNam(string name)
        {
            try
            {
                return this.db.COMPANies.Single(u => u.COM_NAME == name);
            }
            catch
            {
                return null;
            }
        }
        public virtual List<COMPANY> GetListByName(string name)
        {
            return this.db.COMPANies.Where(n => (n.COM_NAME.Contains(name) || name == "")).OrderBy(n => n.COM_NAME).ToList();
        }
        public virtual COMPANY GetById(int id)
        {
            try
            {
                return this.db.COMPANies.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }
        public virtual List<COMPANY> GetAll()
        {
            return this.db.COMPANies.ToList();
        }
        public virtual void Create(COMPANY b)
        {
            try
            {
                this.db.COMPANies.InsertOnSubmit(b);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Update(COMPANY b)
        {
            try
            {
                COMPANY bOld = this.GetById(b.ID);
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
                COMPANY b = this.GetById(id);
                this.Remove(b);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Remove(COMPANY b)
        {
            try
            {
                db.COMPANies.DeleteOnSubmit(b);
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
                COMPANY b = this.GetById(id);
                return this.Delete(b);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(COMPANY b)
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