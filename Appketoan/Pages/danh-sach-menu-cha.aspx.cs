using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxTreeList;
namespace Appketoan.Pages
{
    public partial class danh_sach_menu_cap_cha : System.Web.UI.Page
    {
        #region Declare
        AppketoanDataContext db = new AppketoanDataContext();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Loadmenu();
            }
            else
            {
                if (HttpContext.Current.Session["ktoan.listmenucha"] != null)
                {
                    ASPxTreeList_menu.DataSource = HttpContext.Current.Session["ktoan.listmenucha"];
                    ASPxTreeList_menu.DataBind();
                }
            }
        }
        #region Loaddata
        public void Loadmenu()
        {
            try
            {
                var list = db.MENU_PARENTs.Where(n=>n.MENU_NAME == txtKeyword.Value || txtKeyword.Value == "" || txtKeyword.Value == null).
                    OrderByDescending(n=>n.ORDERBY).ToList();
                if (list.Count > 0)
                {
                    HttpContext.Current.Session["ktoan.listmenucha"] = list;
                    ASPxTreeList_menu.DataSource = list;
                    ASPxTreeList_menu.DataBind();
                }
                else
                {
                    HttpContext.Current.Session["ktoan.listmenucha"] = null;
                    ASPxTreeList_menu.DataSource = list;
                    ASPxTreeList_menu.DataBind();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        #region Function
        public string getlink(object serid)
        {
            return Utils.CIntDef(serid) > 0 ? "chi-tiet-menu-cha.aspx?menuid=" + Utils.CIntDef(serid) : "chi-tiet-menu-cha.aspx";
        }
        public string getDate(object News_PublishDate)
        {
            return string.Format("{0:dd/MM/yyyy}", News_PublishDate);
        }
        public string Getactive(object active)
        {
            return Utils.CIntDef(active) == 1 ? "Kích hoạt" : "Chưa kích hoạt";
        }
        public string Gettype(object type)
        {
            return Utils.CIntDef(type) == 1 ? "Chữ ký số" : (Utils.CIntDef(type) == 2 ? "Kế toán" : "Website");
        }

        #endregion
        #region Buttion
        protected void lbtnDeleteKeyword_Click(object sender, EventArgs e)
        {
            txtKeyword.Value = "";
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            Loadmenu();
        }
        protected void lbtnDelete_Click1(object sender, EventArgs e)
        {
            foreach (TreeListNode node in ASPxTreeList_menu.GetSelectedNodes())
            {
                int  _idmenu = Utils.CIntDef(node.Key, 0);
                var list = db.MENU_PARENTs.Where(n =>n.MENU_PAR_ID==_idmenu);
                db.MENU_PARENTs.DeleteAllOnSubmit(list);
                db.SubmitChanges();
            }
            //Loadmenu();
            Response.Redirect("danh-sach-menu-cha.aspx");
        }
        #endregion
    }
}