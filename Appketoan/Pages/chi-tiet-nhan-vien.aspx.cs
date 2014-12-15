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
    public partial class chi_tiet_nhan_vien : System.Web.UI.Page
    {
        #region Declare
        private EmployerRepo _EmployerRepo = new EmployerRepo();
        private int id = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            id = Utils.CIntDef(Request.QueryString["id"], 0);
            if (!IsPostBack)
            {
                LoadChucvu();
                Getinfo();
            }
        }
        private void LoadChucvu()
        {
            ddlChucvu.Items.Add(new ListItem(Cost.EMP_CONGTY_STR,Cost.EMP_CONGTY.ToString()));
            ddlChucvu.Items.Add(new ListItem(Cost.EMP_TIEPTHI_STR, Cost.EMP_TIEPTHI.ToString()));
        }
        #region Getinfo
        private void Getinfo()
        {
            try
            {
                var Employer = _EmployerRepo.GetById(id);
                if (Employer != null)
                {
                    Txtname.Text = Employer.EMP_NAME;
                    ddlChucvu.SelectedValue = Utils.CStrDef(Employer.EMP_CHUCVU);
                    Txtphone.Text = Employer.EMP_PHONE;
                    Txtaddress.Text = Employer.EMP_ADDRESS;
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
                    var Employer = _EmployerRepo.GetById(id);
                    Employer.EMP_NAME = Txtname.Text;
                    Employer.EMP_CHUCVU = Utils.CIntDef(ddlChucvu.SelectedItem.Value);
                    Employer.EMP_PHONE = Txtphone.Text;
                    Employer.EMP_ADDRESS = Txtaddress.Text;
                    _EmployerRepo.Update(Employer);

                    strLink = string.IsNullOrEmpty(strLink) ? "chi-tiet-nhan-vien.aspx?id=" + id : strLink;
                }
                else
                {
                    EMPLOYER Employer = new EMPLOYER();
                    Employer.EMP_NAME = Txtname.Text;
                    Employer.EMP_CHUCVU = Utils.CIntDef(ddlChucvu.SelectedItem.Value);
                    Employer.EMP_PHONE = Txtphone.Text;
                    Employer.EMP_ADDRESS = Txtaddress.Text;
                    Employer.USER_ID = Utils.CIntDef(Session["Userid"]);
                    Employer.EMP_DATE = DateTime.Now;
                    _EmployerRepo.Create(Employer);
                    strLink = string.IsNullOrEmpty(strLink) ? "chi-tiet-nhan-vien.aspx?id=" + Employer.ID : strLink;
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
            Save("danh-sach-nhan-vien.aspx");
        }

        protected void lbtnSaveNew_Click(object sender, EventArgs e)
        {
            Save("chi-tiet-nhan-vien.aspx");
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                _EmployerRepo.Remove(id);
            }
            catch (Exception)
            {

                throw;
            }
            Response.Redirect("danh-sach-nhan-vien.aspx");
        }

        protected void lbtnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("danh-sach-nhan-vien.aspx");
        }
        #endregion
    }
}