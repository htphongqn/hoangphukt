using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using Appketoan.Data;

namespace Appketoan.Pages
{
    public partial class chi_tiet_khach_hang : System.Web.UI.Page
    {
        #region Declare
        private CustomerRepo _CustomerRepo = new CustomerRepo();
        private CustomerNoDeliRepo _CustomerNoDeliRepo = new CustomerNoDeliRepo();
        private CustomerHistoryRepo _CustomerHistoryRepo = new CustomerHistoryRepo();
        private int id = 0, cusnodeli =0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            id = Utils.CIntDef(Request.QueryString["id"], 0);
            cusnodeli = Utils.CIntDef(Request.QueryString["cusnodeli"], 0);
            if (!IsPostBack)
            {
                if (cusnodeli > 0)
                {
                    CUSTOMER_NODELI cus_nodeli = _CustomerNoDeliRepo.GetById(cusnodeli);
                    if (cus_nodeli != null)
                    {
                        Getinfo2(cus_nodeli);
                    }
                }
                else
                {
                    Getinfo();
                }
            }
        }
        #region Getinfo
        private void Getinfo()
        {
            try
            {
                var Customer = _CustomerRepo.GetById(id);
                if (Customer != null)
                {
                    Txtname.Text = Customer.CUS_FULLNAME;
                    Txtcmnd.Text = Customer.CUS_CMND;
                    Txtphone.Text = Customer.CUS_PHONE;
                    Txtaddress.Text = Customer.CUS_ADDRESS;
                    rdblType.SelectedValue = Utils.CStrDef(Customer.CUS_TYPE);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void Getinfo2(CUSTOMER_NODELI cus_nodeli)
        {
            try
            {
                if (cus_nodeli != null)
                {
                    Txtname.Text = cus_nodeli.CUS_FULLNAME;
                    Txtphone.Text = cus_nodeli.CUS_PHONE;
                    Txtaddress.Text = cus_nodeli.CUS_ADDRESS;
                    //rdblType.SelectedValue = Utils.CStrDef(Customer.CUS_TYPE);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Savedata
        private void SaveHistory(CUSTOMER Customer)
        {
            CUSTOMER_HISTORY CustomerHis = new CUSTOMER_HISTORY();
            CustomerHis.ID_CUS = Customer.ID;
            CustomerHis.CUSHIS_TYPE = Customer.CUS_TYPE;
            CustomerHis.CUSHIS_DATE = DateTime.Now;
            CustomerHis.USER_ID = Utils.CIntDef(Session["Userid"]);
            _CustomerHistoryRepo.Create(CustomerHis);
        }
        private void Save(string strLink = "")
        {
            try
            {
                if (cusnodeli > 0)
                {
                    CUSTOMER_NODELI cus_nodeli = _CustomerNoDeliRepo.GetById(cusnodeli);
                    if (cus_nodeli != null)
                    {
                        id = Utils.CIntDef(cus_nodeli.CUS_ID);
                        cus_nodeli.PROCESS_STATUS = 1;
                        _CustomerNoDeliRepo.Update(cus_nodeli);
                    }
                }
                var Customer = _CustomerRepo.GetById(id);
                if (id > 0 && Customer != null)
                {                    
                    Customer.CUS_FULLNAME = Txtname.Text;
                    Customer.CUS_CMND = Txtcmnd.Text;
                    Customer.CUS_PHONE = Txtphone.Text;
                    Customer.CUS_ADDRESS = Txtaddress.Text;
                    Customer.CUS_UPDATE_DATE = DateTime.Now;

                    if (Customer.CUS_TYPE != Utils.CIntDef(rdblType.SelectedValue))
                    {
                        SaveHistory(Customer);
                        Customer.CUS_TYPE = Utils.CIntDef(rdblType.SelectedValue);
                    }
                    _CustomerRepo.Update(Customer);
                    
                    strLink = string.IsNullOrEmpty(strLink) ? "chi-tiet-khach-hang.aspx?id=" + id : strLink;
                }
                else
                {
                    Customer = new CUSTOMER();
                    Customer.CUS_FULLNAME = Txtname.Text;
                    Customer.CUS_CMND = Txtcmnd.Text;
                    Customer.CUS_PHONE = Txtphone.Text;
                    Customer.CUS_ADDRESS = Txtaddress.Text;
                    Customer.CUS_TYPE = Utils.CIntDef(rdblType.SelectedValue);
                    Customer.USER_ID = Utils.CIntDef(Session["Userid"]);
                    Customer.CUS_CREATE_DATE = DateTime.Now;
                    Customer.CUS_UPDATE_DATE = DateTime.Now;
                    _CustomerRepo.Create(Customer);
                    SaveHistory(Customer);
                    if (cusnodeli > 0)
                    {
                        CUSTOMER_NODELI cus_nodeli = _CustomerNoDeliRepo.GetById(cusnodeli);
                        if (cus_nodeli != null)
                        {
                            cus_nodeli.CUS_ID = Customer.ID;
                            _CustomerNoDeliRepo.Update(cus_nodeli);
                        }
                    }

                    strLink = string.IsNullOrEmpty(strLink) ? "chi-tiet-khach-hang.aspx?id=" + Customer.ID : strLink;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

                if (!string.IsNullOrEmpty(strLink))
                {
                    Response.Redirect(strLink);
                }
            }
        }
        #endregion

        #region Button
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        protected void lbtnSaveClose_Click(object sender, EventArgs e)
        {
            Save("danh-sach-khach-hang.aspx");
        }

        protected void lbtnSaveNew_Click(object sender, EventArgs e)
        {
            Save("chi-tiet-khach-hang.aspx");
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                _CustomerRepo.Remove(id);
            }
            catch (Exception)
            {

                throw;
            }
            Response.Redirect("danh-sach-khach-hang.aspx");
        }

        protected void lbtnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("danh-sach-khach-hang.aspx");
        }
        #endregion

        #region WebMethod
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchFullname(string prefixText, int count)
        {
            var list = new CustomerRepo().GetListByContainsFullName(prefixText);
            List<string> fullnames = new List<string>();
            foreach (var item in list)
            {
                fullnames.Add(item.CUS_FULLNAME);
            }
            return fullnames;
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchPhone(string prefixText, int count)
        {
            var list = new CustomerRepo().GetListByContainsPhone(prefixText);
            List<string> fullnames = new List<string>();
            foreach (var item in list)
            {
                fullnames.Add(item.CUS_PHONE);
            }
            return fullnames;
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchAddress(string prefixText, int count)
        {
            var list = new CustomerRepo().GetListByContainsAddress(prefixText);
            List<string> fullnames = new List<string>();
            foreach (var item in list)
            {
                fullnames.Add(item.CUS_ADDRESS);
            }
            return fullnames;
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchCMND(string prefixText, int count)
        {
            var list = new CustomerRepo().GetListByContainsCMND(prefixText);
            List<string> fullnames = new List<string>();
            foreach (var item in list)
            {
                fullnames.Add(item.CUS_ADDRESS);
            }
            return fullnames;
        }
        #endregion

    }
}