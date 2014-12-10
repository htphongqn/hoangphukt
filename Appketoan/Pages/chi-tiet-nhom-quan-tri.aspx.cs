using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using System.Data;
using Appketoan.Components;
using DevExpress.Web.ASPxTreeList;
using System.Web.UI.HtmlControls;
namespace Appketoan.Pages
{
    public partial class chi_tiet_nhom_quan_tri : System.Web.UI.Page
    {
        #region Declare
        AppketoanDataContext db = new AppketoanDataContext();
        int _groupid = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            _groupid = Utils.CIntDef(Request.QueryString["groupid"]);
            if (!IsPostBack)
            {
 
                Loadmenu();
                Getinfo();
                Check_menu();
            }
            else
            {
                
                if (HttpContext.Current.Session["ktoan.listmenu"] != null)
                {
                    ASPxTreeList_menu.DataSource = HttpContext.Current.Session["ktoan.listmenu"];
                    ASPxTreeList_menu.DataBind();
                }
            }
        }
        #region Getinfo
      
        public void Check_menu()
        {
            try
            {
                var list = db.GROUP_MENUs.Where(n => n.GROUP_ID == _groupid).ToList();
                foreach (var i in list)
                {
                    foreach (TreeListNode node in ASPxTreeList_menu.GetAllNodes())
                    {
                        if (i.MENU_ID == Utils.CIntDef(node.Key))
                        {
                            node.Selected = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Loadmenu()
        {
            try
            {
                var list = db.MENU_PARENTs.Where(n => n.MENU_PAR_ACTIVE == 1).ToList();
                if (list.Count > 0)
                {
                    ASPxTreeList_menu.DataSource = list;
                    ASPxTreeList_menu.DataBind();
                    HttpContext.Current.Session["ktoan.listmenu"] = list;
                }
                else
                {
                    HttpContext.Current.Session["ktoan.listmenu"] = null;
                    ASPxTreeList_menu.DataSource = list;
                    ASPxTreeList_menu.DataBind();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public void Getinfo()
        {
            try
            {
                var list = db.GROUPs.Where(n => n.GROUP_ID == _groupid).ToList();
                if (list.Count > 0)
                {
                    Txtname.Text = list[0].GROUP_NAME;
                    Rdtype.SelectedValue = Utils.CStrDef(list[0].GROUP_TYPE);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        #region Savedata
        private void Delete_group_menu()
        {
            try
            {
                var list = db.GROUP_MENUs.Where(n => n.GROUP_ID == _groupid);
                db.GROUP_MENUs.DeleteAllOnSubmit(list);
                db.SubmitChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void Save(string strLink = "")
        {
            try
            {
                Delete_group_menu();
                if (_groupid == 0)
                {
                    GROUP group = new GROUP();
                    group.GROUP_NAME = Txtname.Text;
                    group.GROUP_TYPE = Utils.CIntDef(Rdtype.SelectedValue);
                    db.GROUPs.InsertOnSubmit(group);
                    db.SubmitChanges();
                    var getlink = db.GROUPs.OrderByDescending(n => n.GROUP_ID).Take(1).ToList();
                    if (getlink.Count > 0)
                    {
                        _groupid = getlink[0].GROUP_ID;
                        strLink = string.IsNullOrEmpty(strLink) ? "chi-tiet-nhom-quan-tri.aspx?groupid=" + getlink[0].GROUP_ID : strLink;
                    }
                }
                else
                {
                    var list = db.GROUPs.Where(n => n.GROUP_ID == _groupid).ToList();
                    foreach (var i in list)
                    {
                        i.GROUP_NAME = Txtname.Text;
                        i.GROUP_TYPE = Utils.CIntDef(Rdtype.SelectedValue);
                    }
                    db.SubmitChanges();
                    strLink = string.IsNullOrEmpty(strLink) ? "chi-tiet-nhom-quan-tri.aspx?groupid=" + _groupid : strLink;
                }

                foreach (TreeListNode node in ASPxTreeList_menu.GetSelectedNodes())
                {
                    int menu_id =Utils.CIntDef(node.Key);
                    GROUP_MENU grmenu = new GROUP_MENU();
                    grmenu.GROUP_ID = _groupid;
                    grmenu.MENU_ID = menu_id;
                    db.GROUP_MENUs.InsertOnSubmit(grmenu);
                    db.SubmitChanges();
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
        private void Delete()
        {
            try
            {
                var list = db.GROUPs.Where(n => n.GROUP_ID == _groupid).ToList();
                db.GROUPs.DeleteAllOnSubmit(list);
            }
            catch (Exception)
            {

                throw;
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
                Save("danh-sach-nhom-quan-tri.aspx");
        }

        protected void lbtnSaveNew_Click(object sender, EventArgs e)
        {
                Save("chi-tiet-nhom-quan-tri.aspx");
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            Delete();
            Response.Redirect("danh-sach-nhom-quan-tri.aspx");
        }

        protected void lbtnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("danh-sach-nhom-quan-tri.aspx");
        }
        #endregion

        
        
    }
}