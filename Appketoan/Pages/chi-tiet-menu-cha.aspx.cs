using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
namespace Appketoan.Pages
{
    public partial class chi_tiet_menu : System.Web.UI.Page
    {
        #region Declare
        AppketoanDataContext db = new AppketoanDataContext();
        int _menuid = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            _menuid = Utils.CIntDef(Request.QueryString["menuid"]);
            if (!IsPostBack)
            {
                Loadmenu_parent();
                Getinfo();
            }
        }
        #region Getinfo
        private void Loadmenu_parent()
        {
            var list = db.MENU_PARENTs.Where(n => n.MENU_RANK == 1);
            Drmenu_parent.DataValueField = "MENU_PAR_ID";
            Drmenu_parent.DataTextField = "MENU_NAME";
            Drmenu_parent.DataSource = list;
            Drmenu_parent.DataBind();
            ListItem l = new ListItem("--- Root ---", "0");
            l.Selected = true;
            Drmenu_parent.Items.Insert(0, l);
        }
        public void Getinfo()
        {
            try
            {
                var list = db.MENU_PARENTs.Where(n => n.MENU_PAR_ID == _menuid).ToList();
                if (list.Count > 0)
                {
                    Txtname.Text = list[0].MENU_NAME;
                    txtlinkmenu.Text = list[0].MENU_PARENT_LINK;
                    rblActive.SelectedValue = list[0].MENU_PAR_ACTIVE.ToString();
                    Drmenu_parent.SelectedValue = list[0].MENU_PARENT1.ToString();
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
                int _idroot=Utils.CIntDef(Drmenu_parent.SelectedValue);
                int rank = 1;
                int _idpar = 0;
                var getrank = db.MENU_PARENTs.Where(n => n.MENU_PAR_ID == _idroot).ToList();
                if (getrank.Count > 0)
                {
                    rank +=Utils.CIntDef(getrank[0].MENU_RANK);
                    _idpar = getrank[0].MENU_PAR_ID;
                }

                if (_menuid == 0)
                {
                    MENU_PARENT menu = new MENU_PARENT();
                    menu.MENU_PARENT1 = _idpar;
                    menu.MENU_RANK = rank;
                    menu.MENU_NAME = Txtname.Text;
                    menu.MENU_PARENT_LINK = txtlinkmenu.Text;
                    menu.MENU_PAR_ACTIVE = Utils.CIntDef(rblActive.SelectedValue);
                    db.MENU_PARENTs.InsertOnSubmit(menu);
                    db.SubmitChanges();
                    var getlink = db.MENU_PARENTs.OrderByDescending(n => n.MENU_PAR_ID).Take(1).ToList();
                    if (getlink.Count > 0)
                    {
                        strLink = string.IsNullOrEmpty(strLink) ? "chi-tiet-menu-cha.aspx?menuid=" + getlink[0].MENU_PAR_ID : strLink;
                    }
                }
                else
                {
                    var list = db.MENU_PARENTs.Where(n => n.MENU_PAR_ID == _menuid).ToList();
                    foreach (var i in list)
                    {
                        i.MENU_RANK = rank;
                        i.MENU_PARENT1 = _idpar;
                        i.MENU_NAME = Txtname.Text;
                        i.MENU_PARENT_LINK = txtlinkmenu.Text;
                        i.MENU_PAR_ACTIVE = Utils.CIntDef(rblActive.SelectedValue);
                    }
                    db.SubmitChanges();
                    strLink = string.IsNullOrEmpty(strLink) ? "chi-tiet-menu-cha.aspx?menuid=" + _menuid : strLink;
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
                var list = db.MENU_PARENTs.Where(n => n.MENU_PAR_ID == _menuid).ToList();
                db.MENU_PARENTs.DeleteAllOnSubmit(list);
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
            Save("danh-sach-menu-cha.aspx");
        }

        protected void lbtnSaveNew_Click(object sender, EventArgs e)
        {
            Save("chi-tiet-menu-cha.aspx");
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            Delete();
            Response.Redirect("danh-sach-menu-cha.aspx");
        }

        protected void lbtnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("danh-sach-menu-cha.aspx");
        }
        #endregion
    }
}