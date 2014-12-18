using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using Appketoan.Data;
using Appketoan.Components;
namespace Appketoan.Pages
{
    public partial class chi_tiet_cong_ty : System.Web.UI.Page
    {
        #region Declare
        private CompanyRepo _CompanyRepo = new CompanyRepo();
        private int id = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            id = Utils.CIntDef(Request.QueryString["id"], 0);
            if (!IsPostBack)
            {
                Getinfo();
            }
        }
        #region Getinfo
        private void Getinfo()
        {
            try
            {
                var Company = _CompanyRepo.GetById(id);
                if (Company != null)
                {
                    Txtname.Text = Company.COM_NAME;
                    Txtphone.Text = Company.COM_PHONE;
                    Txtaddress.Text = Company.COM_ADDRESS;
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
                    var COMPANY = _CompanyRepo.GetById(id);
                    COMPANY.COM_NAME = Txtname.Text;
                    COMPANY.COM_PHONE = Txtphone.Text;
                    COMPANY.COM_ADDRESS = Txtaddress.Text;
                    _CompanyRepo.Update(COMPANY);

                    strLink = string.IsNullOrEmpty(strLink) ? "chi-tiet-cong-ty.aspx?id=" + id : strLink;
                }
                else
                {
                    COMPANY _COMPANY = new COMPANY();
                    _COMPANY.COM_NAME = Txtname.Text;
                    _COMPANY.COM_PHONE = Txtphone.Text;
                    _COMPANY.COM_ADDRESS = Txtaddress.Text;
                    _COMPANY.USER_ID = Utils.CIntDef(Session["Userid"]);
                    _COMPANY.COM_DATE = DateTime.Now;
                    _CompanyRepo.Create(_COMPANY);
                    strLink = string.IsNullOrEmpty(strLink) ? "chi-tiet-cong-ty.aspx?id=" + _COMPANY.ID : strLink;
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
            Save("danh-sach-cong-ty.aspx");
        }

        protected void lbtnSaveNew_Click(object sender, EventArgs e)
        {
            Save("chi-tiet-cong-ty.aspx");
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                _CompanyRepo.Remove(id);
            }
            catch (Exception)
            {

                throw;
            }
            Response.Redirect("danh-sach-cong-ty.aspx");
        }

        protected void lbtnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("danh-sach-cong-ty.aspx");
        }
        #endregion
    }
}