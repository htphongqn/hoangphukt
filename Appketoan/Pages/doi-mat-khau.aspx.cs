using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Appketoan.Components;
using System.IO;
using System.Collections;
using vpro.functions;
namespace Appketoan.Pages
{
    public partial class doi_mat_khau : System.Web.UI.Page
    {
        #region Declare
        AppketoanDataContext db = new AppketoanDataContext();
        int _iUserID = 0;
        string m_pathFile = string.Empty;
        int _count = 0;

        #endregion

        #region Page Event
        protected void Page_Load(object sender, EventArgs e)
        {
            

        }
        #endregion

        #region My Functions

        private bool ValidCheck()
        {
            if (string.IsNullOrEmpty(txtNewPassword.Value))
            {
                clsDataUtil.Show("Mật Khẩu không được rỗng!");
                txtNewPassword.Focus();
                return false;
            }
            else if (txtNewPassword.Value.Trim().Length < 5 || txtNewPassword.Value.Length > 100)
            {
                clsDataUtil.Show("Mật khẩu nhiều hơn 5 ký tự !");
                txtNewPassword.Focus();
                return false;
            }
            return true;
        }

        private void SaveInfo()
        {
            try
            {
                int UserID = Utils.CIntDef(Session["Userid"]);

                string SALT = Common.CreateSalt();
                string USER_PW = Common.Encrypt(txtNewPassword.Value, SALT);

                var g_update = db.GetTable<USER>().Where(g => g.USER_ID == UserID);

                if (g_update.ToList().Count > 0)
                {

                    if (!string.IsNullOrEmpty(USER_PW))
                    {
                        g_update.Single().USER_PW = USER_PW;
                        g_update.Single().SALT = SALT;
                    }

                    db.SubmitChanges();


                    //clsUtility.Common.Show("Thông báo : đổi mật khẩu thành công !");
                    string strScript = "<script>";
                    strScript += "alert('Thông báo : đổi mật khẩu thành công !');";
                    Session.Abandon();
                    strScript += "window.location='/Pages/dang-nhap.aspx';";
                    strScript += "</script>";
                    Page.RegisterClientScriptBlock("strScript", strScript);


                    // Response.Redirect("dang-nhap.aspx",false);


                    //Response.Redirect("trang-chu.aspx");
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }


        private bool CheckInfo()
        {
            try
            {
                string strPW = Utils.CStrDef(txtOldPassword.Value, "");
                int UserID = Utils.CIntDef(Session["Userid"]);

                if (UserID > 0)
                {
                    var login = db.GetTable<USER>().Where(u => u.USER_ID == UserID);

                    if (login.ToList().Count > 0)
                    {
                        strPW = Common.Encrypt(strPW, Utils.CStrDef(login.ToList()[0].SALT));
                        if (login.ToList()[0].USER_PW != strPW)
                            return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return false;
            }

        }

        #endregion

        #region Button Handler
        protected void lbtnLogin_Click(object sender, EventArgs e)
        {
            if (txtNewPassword.Value == txtConfirmNewPassword.Value)
            {
                if (ValidCheck())
                {
                    if (CheckInfo())
                    {
                        clsDataUtil.Show("Mật khẩu cũ nhập không chính xác!");
                    }
                    else
                    {
                        SaveInfo();
                    }
                }
            }
            else { clsDataUtil.Show("Mật khẩu Xác nhận không giống mật khẩu mới!"); }
        }
        #endregion
    }
}