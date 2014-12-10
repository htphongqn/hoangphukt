using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using DevExpress.Web.ASPxGridView;
namespace Appketoan.Pages
{
    public partial class danh_sach_quan_tri : System.Web.UI.Page
    {
        #region Declare
        AppketoanDataContext db = new AppketoanDataContext();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Loaduser();
            }
            else
            {
                if (HttpContext.Current.Session["ktoan.listuser"] != null)
                {
                    ASPxGridView1_user.DataSource = HttpContext.Current.Session["ktoan.listuser"];
                    ASPxGridView1_user.DataBind();
                }
            }
        }
        #region Loaddata
        public void Loaduser()
        {
            try
            {
                var list = (from a in db.USERs
                            join b in db.GROUPs on a.GROUP_ID equals b.GROUP_ID
                            where (a.USER_NAME.Contains(txtKeyword.Value) || txtKeyword.Value == "") 
                            select new
                            {
                                a.USER_ID,
                                a.USER_NAME,
                                a.USER_UN,
                                a.USER_EMAIL,
                                a.USER_PHONE,
                                a.USER_ADDRESS,
                                a.USER_ACTIVE,
                                b.GROUP_TYPE
                            }).OrderByDescending(n=>n.USER_ID).ToList();
                if (list.Count > 0)
                {
                    HttpContext.Current.Session["ktoan.listuser"] = list;
                    ASPxGridView1_user.DataSource = list;
                    ASPxGridView1_user.DataBind();
                }
                else
                {
                    HttpContext.Current.Session["ktoan.listuser"] = null;
                    ASPxGridView1_user.DataSource = list;
                    ASPxGridView1_user.DataBind();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        #region Function
        public string getlink(object userid)
        {
            return Utils.CIntDef(userid) > 0 ? "chi-tiet-quan-tri.aspx?userid=" + Utils.CIntDef(userid) : "chi-tiet-quan-tri.aspx";
        }
        public string getDate(object News_PublishDate)
        {
            return string.Format("{0:dd/MM/yyyy}", News_PublishDate);
        }
        public string Getactive(object active)
        {
            return Utils.CIntDef(active) == 1 ? "Kích hoạt" : "Chưa kích hoạt";
        }
        public string Getnhom(object groupid)
        { 
            return Utils.CIntDef(groupid)==1 ? "Nhóm Admin" : "Nhóm Editor";
        }
        #endregion
        #region Buttion
        protected void lbtnDeleteKeyword_Click(object sender, EventArgs e)
        {
            txtKeyword.Value = "";
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            Loaduser();
        }
        protected void lbtnDelete_Click1(object sender, EventArgs e)
        {
            List<object> fieldValues = ASPxGridView1_user.GetSelectedFieldValues(new string[] { "USER_ID" });
            var list = db.USERs.Where(n => fieldValues.Contains(n.USER_ID.ToString()));
            db.USERs.DeleteAllOnSubmit(list);
            db.SubmitChanges();
            Loaduser();

        }
        #endregion
    }
}