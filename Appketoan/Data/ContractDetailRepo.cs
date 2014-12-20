using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using vpro.functions;

namespace Appketoan.Data
{
    public class ContractDetailRepo
    {
        private AppketoanDataContext db = new AppketoanDataContext();

        public virtual CONTRACT_DETAIL GetByContractIdAndDatethu(int id, DateTime datethu)
        {
            try
            {
                return db.CONTRACT_DETAILs.Where(a => a.ID_CONT == id && (a.CONTD_DATE_THU.Value - datethu.Date).Days == 0).OrderBy(a => a.ID).Take(1).Single();
            }
            catch
            {
                return null;
            }

        }
        public virtual CONTRACT_DETAIL GetNoPriceByContractId(int id)
        {
            try
            {
                return GetListByContractId(id).Where(a => (a.CONTD_PAY_PRICE == null || a.CONTD_PAY_PRICE == 0) && a.CONTD_DATE_THU_TT == null).OrderBy(a => a.ID).Take(1).Single();
            }
            catch
            {
                return null;
            }
            
        }
        public virtual CONTRACT_DETAIL GetLastPayByContractId(int id, DateTime date)
        {
            try
            {
                var detail = GetListByContractId(id).Where(a =>(a.CONTD_DATE_THU.Value - date).Days < 0).OrderByDescending(a => a.ID).Take(1).Single();
                return detail;
            }
            catch
            {
                return null;
            }
        }
        public virtual CONTRACT_DETAIL GetNextPayDateConveByContractId(int id, DateTime date)
        {
            try
            {
                //var detail =  GetListByContractId(id).Where(a => a.CONTD_PAY_PRICE != null && a.CONTD_PAY_PRICE != 0).OrderByDescending(a => a.ID).Take(1).Single();
                //if ((detail.CONTD_DATE_THU.Value - DateTime.Today).Days >= 0)
                //{
                //    return detail;
                //}
                //return GetListByContractId(id).Where(a => a.CONTD_PAY_PRICE != null && a.CONTD_PAY_PRICE != 0 && (detail.CONTD_DATE_THU.Value - DateTime.Today).Days < 0).OrderByDescending(a => a.ID).Take(1).Single();
                var detail = GetListByContractId(id).Where(a => (a.CONTD_PAY_PRICE == null || a.CONTD_PAY_PRICE == 0) && (a.CONTD_DATE_THU.Value - date).Days >= 0).OrderBy(a => a.ID).OrderBy(n => n.CONTD_DATE_THU_TT).OrderBy(n => n.CONTD_DATE_THU).Take(1).Single();
                return detail;
            }
            catch
            {
                return null;
            }
        }
        public virtual List<CONTRACT_DETAIL> GetListPayDateConveByContractId(int id, DateTime date)
        {
            return GetListByContractId(id).Where(a => (a.CONTD_PAY_PRICE == null || a.CONTD_PAY_PRICE == 0) && (a.CONTD_DATE_THU.Value - date).Days >= 0).OrderBy(a => a.ID).OrderBy(n=>n.CONTD_DATE_THU_TT).OrderBy(n=>n.CONTD_DATE_THU).ToList();
        }
        public virtual List<CONTRACT_DETAIL> GetListNoPriceByContractId(int id)
        {
            return GetListByContractId(id).Where(a => a.CONTD_PAY_PRICE == null || a.CONTD_PAY_PRICE == 0).OrderBy(a => a.ID).ToList();
        }
        public virtual List<CONTRACT_DETAIL> GetListByContractId(int id)
        {
            return this.db.CONTRACT_DETAILs.Where(a=>a.ID_CONT == id).OrderBy(n => n.ID).OrderBy(n=>n.CONTD_DATE_THU).ToList();
        }
        public virtual CONTRACT_DETAIL GetLastPayPriceByContractId(int id)
        {
            try
            {
                return GetListByContractId(id).Where(a => a.CONTD_PAY_PRICE != null && a.CONTD_PAY_PRICE != 0).OrderBy(a => a.ID).Take(1).Single();
            }
            catch
            {
                return null;
            }
        }
        public virtual bool CheckBillNewById(int id)
        {
            try
            {                
                var list =  GetListByContractId(id).Where(a => a.CONTD_DATE_THU_TT != null).ToList();
                if (list != null && list.Count > 0)//phiếu củ
                    return false;
                return true;//phiếu mới
            }
            catch
            {
                return false;
            }
        }
        public virtual CONTRACT_DETAIL GetById(int id)
        {
            try
            {
                return this.db.CONTRACT_DETAILs.Single(u => u.ID == id);
            }
            catch
            {
                return null;
            }
        }
        public virtual List<CONTRACT_DETAIL> GetAll()
        {
            return this.db.CONTRACT_DETAILs.OrderBy(n => n.ID).ToList();
        }
        public virtual void Create(CONTRACT_DETAIL cus)
        {
            try
            {
                this.db.CONTRACT_DETAILs.InsertOnSubmit(cus);
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Update(CONTRACT_DETAIL cus)
        {
            try
            {
                CONTRACT_DETAIL cusOld = this.GetById(cus.ID);
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
                CONTRACT_DETAIL cus = this.GetById(id);
                this.Remove(cus);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public virtual void Remove(CONTRACT_DETAIL cus)
        {
            try
            {
                db.CONTRACT_DETAILs.DeleteOnSubmit(cus);
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
                CONTRACT_DETAIL cus = this.GetById(id);
                return this.Delete(cus);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public virtual int Delete(CONTRACT_DETAIL cus)
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