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
    public partial class chi_tiet_khach_hang_khong_giao : System.Web.UI.Page
    {
        #region Declare
        private CustomerRepo _CustomerRepo = new CustomerRepo();
        private CustomerNoDeliRepo _CustomerNoDeliRepo = new CustomerNoDeliRepo();
        private CustomerHistoryRepo _CustomerHistoryRepo = new CustomerHistoryRepo();
        private EmployerRepo _EmployerRepo = new EmployerRepo();
        private int id = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            id = Utils.CIntDef(Request.QueryString["id"], 0);
            if (!IsPostBack)
            {
                pickdatefax.returnDate = DateTime.Now;
                LoadEmployee();
                Getinfo();
            }
        }
        private void LoadEmployee()
        {
            var list = _EmployerRepo.GetAllSortName();

            ddlEmployeeBH.DataSource = list;
            ddlEmployeeXM.DataSource = list;
            ddlEmployeeBH.DataBind();
            ddlEmployeeXM.DataBind();
        }
        #region Getinfo
        private void Getinfo()
        {
            try
            {
                var Customer = _CustomerNoDeliRepo.GetById(id);
                if (Customer != null)
                {
                    Txtname.Text = Customer.CUS_FULLNAME;
                    Txtphone.Text = Customer.CUS_PHONE;
                    Txtaddress.Text = Customer.CUS_ADDRESS;
                    Txtproduct.Text = Customer.CUS_PRODUCT;
                    pickdatefax.returnDate = Utils.CDateDef(Customer.CUS_FAX_DATE, DateTime.Now);
                    ddlEmployeeBH.Visible = false;
                    lbEmployeeBH.Visible = true;
                    string[] empBHIds = Utils.CStrDef(Customer.EMP_BH).Split(',');
                    lbEmployeeBH.Text = "";
                    foreach (var BHId in empBHIds)
                    {
                        EMPLOYER emp = _EmployerRepo.GetById(Utils.CIntDef(BHId));
                        if (emp != null)
                        {
                            lbEmployeeBH.Text += emp.EMP_NAME + ",";
                        }
                    }
                    if (lbEmployeeBH.Text.Length > 0)
                        lbEmployeeBH.Text = lbEmployeeBH.Text.Substring(0, lbEmployeeBH.Text.Length - 1);

                    ddlEmployeeXM.Visible = false;
                    lbEmployeeXM.Visible = true;
                    string[] empXMIds = Utils.CStrDef(Customer.EMP_XM).Split(',');
                    lbEmployeeXM.Text = "";
                    foreach (var XMId in empXMIds)
                    {
                        EMPLOYER emp = _EmployerRepo.GetById(Utils.CIntDef(XMId));
                        if (emp != null)
                        {
                            lbEmployeeXM.Text += emp.EMP_NAME + ",";
                        }
                    }
                    if (lbEmployeeXM.Text.Length > 0)
                        lbEmployeeXM.Text = lbEmployeeXM.Text.Substring(0, lbEmployeeXM.Text.Length - 1);
                    txtNoteXM.Text = Customer.NOTE_XM;
                    rdbStatus.SelectedValue = Utils.CStrDef(Utils.CIntDef(Customer.PROCESS_STATUS));
                }
                else
                {
                    ddlEmployeeBH.Visible = true;
                    lbEmployeeBH.Visible = false;

                    ddlEmployeeXM.Visible = true;
                    lbEmployeeXM.Visible = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Savedata
        private void Save(string strLink = "")
        {
            try
            {
                if (id > 0)
                {
                    var Customer = _CustomerNoDeliRepo.GetById(id);
                    Customer.CUS_FULLNAME = Txtname.Text;
                    Customer.CUS_PHONE = Txtphone.Text;
                    Customer.CUS_ADDRESS = Txtaddress.Text;
                    Customer.CUS_PRODUCT = Txtproduct.Text;
                    Customer.CUS_FAX_DATE = pickdatefax.returnDate;
                    Customer.NOTE_XM = txtNoteXM.Text;
                    Customer.PROCESS_STATUS = Utils.CIntDef(rdbStatus.SelectedItem.Value);
                    _CustomerNoDeliRepo.Update(Customer);
                    
                    strLink = string.IsNullOrEmpty(strLink) ? "chi-tiet-khach-hang-khong-giao.aspx?id=" + id : strLink;
                }
                else
                {
                    CUSTOMER_NODELI Customer = new CUSTOMER_NODELI();
                    Customer.CUS_FULLNAME = Txtname.Text;
                    Customer.CUS_PHONE = Txtphone.Text;
                    Customer.CUS_ADDRESS = Txtaddress.Text;
                    Customer.CUS_PRODUCT = Txtproduct.Text;
                    Customer.CUS_FAX_DATE = pickdatefax.returnDate;
                    Customer.EMP_BH = HiddenEmployeeBH.Value;
                    Customer.EMP_XM = HiddenEmployeeXM.Value;
                    Customer.NOTE_XM = txtNoteXM.Text;
                    Customer.PROCESS_STATUS = Utils.CIntDef(rdbStatus.SelectedItem.Value);
                    Customer.USER_ID = Utils.CIntDef(Session["Userid"]);
                    Customer.CUS_CREATE_DATE = DateTime.Now;
                    _CustomerNoDeliRepo.Create(Customer);

                    strLink = string.IsNullOrEmpty(strLink) ? "chi-tiet-khach-hang-khong-giao.aspx?id=" + Customer.ID : strLink;
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
            Save("danh-sach-khach-hang-khong-giao.aspx");
        }

        protected void lbtnSaveNew_Click(object sender, EventArgs e)
        {
            Save("chi-tiet-khach-hang-khong-giao.aspx");
        }
        protected void lbtnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("danh-sach-khach-hang-khong-giao.aspx");
        }
        #endregion

        #region WebMethod
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchFullname(string prefixText, int count)
        {
            var list = new CustomerNoDeliRepo().GetListByContainsFullName(prefixText);
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
            var list = new CustomerNoDeliRepo().GetListByContainsPhone(prefixText);
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
            var list = new CustomerNoDeliRepo().GetListByContainsAddress(prefixText);
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