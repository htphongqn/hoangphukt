using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;

namespace Appketoan.Pages
{
    public partial class chi_tiet_quan_tri : System.Web.UI.Page
    {
        #region Declare
        AppketoanDataContext db = new AppketoanDataContext();
        int _userid = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            _userid = Utils.CIntDef(Request.QueryString["userid"]);
            if (!IsPostBack)
            {
                //Loadgroup();
                Getinfo();
            }
        }
        #region Getinfo
        //public void Loadgroup()
        //{
        //    try
        //    {
        //        var list = db.GROUPs.ToList();
        //        Drgroup.DataValueField = "GROUP_ID";
        //        Drgroup.DataTextField = "GROUP_NAME";
        //        Drgroup.DataSource = list;
        //        Drgroup.DataBind();
        //    }
        //    catch (Exception)
        //    {
                
        //        throw;
        //    }
        //}
        public void Getinfo()
        {
            try
            {
                var list = db.USERs.Where(n => n.USER_ID == _userid).ToList();
                if (list.Count > 0)
                {
                    Txtname.Text = list[0].USER_NAME;
                    Txtusername.Text = list[0].USER_UN;
                    Txtemail.Text = list[0].USER_EMAIL;
                    Txtphone.Text = list[0].USER_PHONE;
                    Txtaddress.Text = list[0].USER_ADDRESS;
                    //Drgroup.SelectedValue = list[0].GROUP_ID.ToString();
                    rblActive.SelectedValue = list[0].USER_ACTIVE.ToString();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
        #region Savedata
        private bool checkexist_user(string user)
        {
            try
            {
                var list = db.USERs.Where(n => n.USER_UN == user && n.USER_ID != _userid).ToList();
                if (list.Count > 0)
                    return true;
                return false;
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
                string SALT = "";
                string USER_PW = "";
                if (!string.IsNullOrEmpty(Txtpass.Text))
                {
                    if (Txtpass.Text != Txtrepass.Text)
                    {
                        Lberrors.Text = "2 mật khẩu không giống nhau";
                    }
                    else
                    {
                        SALT = Common.CreateSalt();
                        USER_PW = Common.Encrypt(Txtpass.Text, SALT);
                    }
                }
                if (_userid == 0)
                {
                    if (Txtpass.Text == Txtrepass.Text)
                    {
                        USER user = new USER();
                        user.USER_NAME = Txtname.Text;
                        user.USER_UN = Txtusername.Text;
                        user.SALT = SALT;
                        user.USER_PW = USER_PW;
                        user.USER_EMAIL = Txtemail.Text;
                        user.USER_PHONE = Txtphone.Text;
                        user.USER_ADDRESS = Txtaddress.Text;
                        user.GROUP_ID = 1;// Utils.CIntDef(Drgroup.SelectedValue);
                        user.USER_ACTIVE = Utils.CIntDef(rblActive.SelectedValue);
                        user.USER_DATE = DateTime.Now;
                        db.USERs.InsertOnSubmit(user);
                        db.SubmitChanges();
                        var getlink = db.USERs.OrderByDescending(n => n.USER_ID).Take(1).ToList();
                        if (getlink.Count > 0)
                        {
                            strLink = string.IsNullOrEmpty(strLink) ? "chi-tiet-quan-tri.aspx?userid=" + getlink[0].USER_ID : strLink;
                        }
                    }
                    else
                    {
                        Lberrors.Text = "2 mật khẩu không giống nhau";
                    }
                }
                else
                {
                    var list = db.USERs.Where(n => n.USER_ID == _userid).ToList();
                    foreach (var i in list)
                    {
                        i.USER_NAME = Txtname.Text;
                        i.USER_UN = Txtusername.Text;
                        i.USER_EMAIL = Txtemail.Text;
                        i.USER_PHONE = Txtphone.Text;
                        i.USER_ADDRESS = Txtaddress.Text;
                        i.GROUP_ID = 1;// Utils.CIntDef(Drgroup.SelectedValue);
                        i.USER_ACTIVE = Utils.CIntDef(rblActive.SelectedValue);
                        if (!string.IsNullOrEmpty(USER_PW))
                        {
                            i.SALT = SALT;
                            i.USER_PW = USER_PW;
                        }
                    }
                    db.SubmitChanges();
                    strLink = string.IsNullOrEmpty(strLink) ? "chi-tiet-quan-tri.aspx?userid=" + _userid : strLink;
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
                var list = db.USERs.Where(n => n.USER_ID == _userid).ToList();
                db.USERs.DeleteAllOnSubmit(list);
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
            if(checkexist_user(Txtusername.Text))
            {
                Lberrors.Text = "Tên đăng nhập đã tồn tại";
            }
            else
            Save();
        }

        protected void lbtnSaveClose_Click(object sender, EventArgs e)
        {
            if (checkexist_user(Txtusername.Text))
            {
                Lberrors.Text = "Tên đăng nhập đã tồn tại";
            }
            else
            Save("danh-sach-quan-tri.aspx");
        }

        protected void lbtnSaveNew_Click(object sender, EventArgs e)
        {
            if (checkexist_user(Txtusername.Text))
            {
                Lberrors.Text = "Tên đăng nhập đã tồn tại";
            }
            else
            Save("chi-tiet-quan-tri.aspx");
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            Delete();
            Response.Redirect("danh-sach-quan-tri.aspx");
        }

        protected void lbtnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("danh-sach-quan-tri.aspx");
        }
        #endregion
    }
}