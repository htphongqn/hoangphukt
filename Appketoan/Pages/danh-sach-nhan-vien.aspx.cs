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
    public partial class danh_sach_nhan_vien : System.Web.UI.Page
    {
        #region Declare
        private EmployerRepo _EmployerRepo = new EmployerRepo();
        private UserRepo _UserRepo = new UserRepo();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadEmployer();
            }
            else
            {
                ASPxGridView1_employer.DataSource = HttpContext.Current.Session["listEmployer"];
                ASPxGridView1_employer.DataBind();
            }
        }

        private void LoadEmployer()
        {
            try
            {
                var list = _EmployerRepo.GetListByName(txtKeyword.Value);

                HttpContext.Current.Session["listEmployer"] = list;
                ASPxGridView1_employer.DataSource = list;
                ASPxGridView1_employer.DataBind();

            }
            catch //(Exception)
            {

                //throw;
            }
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            LoadEmployer();
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            List<object> fieldValues = ASPxGridView1_employer.GetSelectedFieldValues(new string[] { "ID" });
            foreach (var item in fieldValues)
            {
                _EmployerRepo.Remove(Utils.CIntDef(item));
            }
                       
            //LoadEmployer();
            Response.Redirect("danh-sach-nhan-vien.aspx");
        }

        protected void lbtnDeleteKeyword_Click(object sender, EventArgs e)
        {
            txtKeyword.Value = "";
        }

        #region Function
        public string getlink(object id)
        {
            return Utils.CIntDef(id) > 0 ? "chi-tiet-nhan-vien.aspx?id=" + Utils.CIntDef(id) : "chi-tiet-nhan-vien.aspx";
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
        public string getEmpChucvu(object idChucvu)
        {
            int idcv = Utils.CIntDef(idChucvu);
            switch (idcv)
            {
                case Cost.EMP_CONGTY:
                    return Cost.EMP_CONGTY_STR;
                    break;
                case Cost.EMP_TIEPTHI:
                    return Cost.EMP_TIEPTHI_STR;
                    break;
                default:
                    return "";
                    break;
            }
        }
        #endregion
    }
}