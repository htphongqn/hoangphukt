using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Appketoan.Data;
using vpro.functions;
using Appketoan.Components;

namespace Appketoan.Pages
{
    public partial class danh_sach_cong_ty : System.Web.UI.Page
    {
        #region Declare
        private CompanyRepo _CompanyRepo = new CompanyRepo();
        private UserRepo _UserRepo = new UserRepo();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCompany();
            }
            else
            {
                ASPxGridView1_COMPANY.DataSource = HttpContext.Current.Session["listCompany"];
                ASPxGridView1_COMPANY.DataBind();
            }
        }

        private void LoadCompany()
        {
            try
            {
                var list = _CompanyRepo.GetListByName(txtKeyword.Value);

                HttpContext.Current.Session["listCompany"] = list;
                ASPxGridView1_COMPANY.DataSource = list;
                ASPxGridView1_COMPANY.DataBind();

            }
            catch //(Exception)
            {

                //throw;
            }
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            LoadCompany();
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            List<object> fieldValues = ASPxGridView1_COMPANY.GetSelectedFieldValues(new string[] { "ID" });
            foreach (var item in fieldValues)
            {
                _CompanyRepo.Remove(Utils.CIntDef(item));
            }
                       
            Response.Redirect("danh-sach-cong-ty.aspx");
        }

        protected void lbtnDeleteKeyword_Click(object sender, EventArgs e)
        {
            txtKeyword.Value = "";
        }

        #region Function
        public string getlink(object id)
        {
            return Utils.CIntDef(id) > 0 ? "chi-tiet-cong-ty.aspx?id=" + Utils.CIntDef(id) : "chi-tiet-cong-ty.aspx";
        }
        public string getDate(object News_PublishDate)
        {
            return string.Format("{0:dd/MM/yyyy}", News_PublishDate);
        }
        public string Getactive(object active)
        {
            return Utils.CIntDef(active) == 1 ? "Kích hoạt" : "Chưa kích hoạt";
        }
        public string getUserName(object idUser)
        {
            int _id = Utils.CIntDef(idUser);
            var user = _UserRepo.GetById(_id);
            if (user != null)
            {
                return user.USER_NAME;
            }
            return "";
        }
        #endregion
    }
}