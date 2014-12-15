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
    public partial class danh_sach_nhom_quan_tri : System.Web.UI.Page
    {
        #region Declare
        AppketoanDataContext db = new AppketoanDataContext();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Loadgroup();
            }
            else
            {
                if (HttpContext.Current.Session["ktoan.listgroup"] != null)
                {
                    ASPxGridView1_group.DataSource = HttpContext.Current.Session["ktoan.listgroup"];
                    ASPxGridView1_group.DataBind();
                }
            }
        }
        #region Loaddata
        public void Loadgroup()
        {
            try
            {
                var list = db.GROUPs.Where(n => (n.GROUP_NAME.Contains(txtKeyword.Value)||txtKeyword.Value=="")).ToList();
                if (list.Count > 0)
                {
                    HttpContext.Current.Session["ktoan.listgroup"] = list;
                    ASPxGridView1_group.DataSource = list;
                    ASPxGridView1_group.DataBind();
                }
                else
                {
                    HttpContext.Current.Session["ktoan.listgroup"] = null;
                    ASPxGridView1_group.DataSource = list;
                    ASPxGridView1_group.DataBind();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        #region Function
        public string getlink(object groupid)
        {
            return Utils.CIntDef(groupid) > 0 ? "chi-tiet-nhom-quan-tri.aspx?groupid=" + Utils.CIntDef(groupid) : "chi-tiet-nhom-quan-tri.aspx";
        }
        public string getDate(object News_PublishDate)
        {
            return string.Format("{0:dd/MM/yyyy}", News_PublishDate);
        }
        public string Getactive(object active)
        {
            return Utils.CIntDef(active) == 1 ? "Kích hoạt" : "Chưa kích hoạt";
        }
        public string Getnhom(object grouptype)
        {
            return Utils.CIntDef(grouptype) == 1 ? "Nhóm Admin" : "Nhóm Editor";
        }
        #endregion
        #region Buttion
        protected void lbtnDeleteKeyword_Click(object sender, EventArgs e)
        {
            txtKeyword.Value = "";
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            Loadgroup();
        }
        protected void lbtnDelete_Click1(object sender, EventArgs e)
        {
            List<object> fieldValues = ASPxGridView1_group.GetSelectedFieldValues(new string[] { "GROUP_ID" });
            var list = db.GROUPs.Where(n => fieldValues.Contains(n.GROUP_ID.ToString()));
            db.GROUPs.DeleteAllOnSubmit(list);
            db.SubmitChanges();
            //Loadgroup();
            Response.Redirect("danh-sach-nhom-quan-tri.aspx");
        }
        #endregion
    }
}