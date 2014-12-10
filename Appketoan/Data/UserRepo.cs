using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appketoan.Data
{
    public class UserRepo
    {
        private AppketoanDataContext db = new AppketoanDataContext();


        public virtual USER GetById(int id)
        {
            try
            {
                return this.db.USERs.Single(u => u.USER_ID == id);
            }
            catch
            {
                return null;
            }            
        }
        public virtual List<USER> GetAll()
        {
            return this.db.USERs.ToList();
        }
        public virtual void Create(USER user)
        {
            try
            {
                this.db.USERs.InsertOnSubmit(user);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Update(USER user)
        {
            try
            {
                USER userOld = this.GetById(user.USER_ID);
                userOld = user;
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
                USER user = this.GetById(id);
                this.Remove(user);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Remove(USER user)
        {
            try
            {
                db.USERs.DeleteOnSubmit(user);
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
                USER user = this.GetById(id);
                return this.Delete(user);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(USER user)
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