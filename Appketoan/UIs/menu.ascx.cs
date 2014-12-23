using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;

namespace Appketoan.UIs
{
    public partial class menu : System.Web.UI.UserControl
    {
        #region Declare
        AppketoanDataContext db = new AppketoanDataContext();
        int _groupid = 0;
        int _gtype = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            _groupid = Utils.CIntDef(Session["Groupid"]);
            _gtype = Utils.CIntDef(Session["Grouptype"]);
            if (!IsPostBack)
                Loadmenu_parent();
        }
        #region Loaddata
        public List<string> listmenuid_cap1()
        {
            try
            {
                List<string> l = new List<string>();
                var list = (from a in db.MENU_PARENTs
                            join b in db.GROUP_MENUs on a.MENU_PAR_ID equals b.MENU_ID
                            where b.GROUP_ID == _groupid && _gtype != 1
                            select new { a.MENU_PAR_ID }).Distinct();
                foreach (var i in list)
                {
                    l.Add(i.MENU_PAR_ID.ToString());
                }
                return l;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Loadmenu_parent()
        {
            try
            {
                var list = db.MENU_PARENTs.Where(n => n.MENU_PAR_ACTIVE == 1 && n.MENU_RANK==1 && (_gtype != 1 ? listmenuid_cap1().Contains(n.MENU_PAR_ID.ToString()) : _gtype == 1)).OrderByDescending(n => n.ORDERBY).ToList();
                if (list.Count > 0)
                {
                    Rpmenu.DataSource = list;
                    Rpmenu.DataBind();
                    Rpmenuchild.DataSource = list;
                    Rpmenuchild.DataBind();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
       
        public IQueryable menuchild(object par_id)
        {
            int id = Utils.CIntDef(par_id);
            var list = db.MENU_PARENTs.Where(n => n.MENU_PARENT1 == id).OrderByDescending(n=>n.ORDERBY);
            return list.ToList().Count > 0 ? list : null;
            
        }
        #endregion
        #region Function
        public string Getlink(object link)
        {
            return !string.IsNullOrEmpty(Utils.CStrDef(link)) ? "/Pages/" + Utils.CStrDef(link) : "";
        }
        #endregion
    }
}