using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections;
using Appketoan.Components;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Web.UI.HtmlControls;
using vpro.functions;

namespace Appketoan.Pages
{
    public partial class quen_mat_khau : System.Web.UI.Page
    {
        #region Declare
        AppketoanDataContext db = new AppketoanDataContext();
        clsUtility fm = new clsUtility();
        #endregion

        #region Page Event
        protected void Page_Load(object sender, EventArgs e)
        {
            txtEmail.Attributes.Add("onKeyPress", Appketoan.Components.clsUtility.Common.getSubmitScript(lbtnforgotpass.ClientID));
            txtCode.Attributes.Add("onKeyPress", Appketoan.Components.clsUtility.Common.getSubmitScript(lbtnforgotpass.ClientID));
           // txtConfirmNewPassword.Attributes.Add("onKeyPress", vpro.salv.vn.store.Components.clsUtility.Common.getSubmitScript(lbtnLogin.ClientID));

        }
        #endregion

        #region My Function

        public string Create_Random(int dodai)
        {
            string _allowedChars = "abcdefghijk0123456789mnopqrstuvwxyz";
            Random randNum = new Random();
            char[] chars = new char[dodai];
            int allowedCharCount = _allowedChars.Length;

            for (int i = 0; i < dodai; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }

        public bool IsValidEmail(string strIn)
        {
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        private bool CheckError()
        {

            CaptchaControl1.ValidateCaptcha(txtCode.Value);
            if (string.IsNullOrEmpty(txtEmail.Value))
            {
                clsUtility.Common.Show("Email không được rỗng!");
                txtEmail.Focus();
                return false;
            }
            else if (!IsValidEmail(txtEmail.Value))
            {
                clsUtility.Common.Show("Email không hợp lệ!");
                txtEmail.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtCode.Value))
            {
                clsUtility.Common.Show("Mã bảo mật không được rỗng!");
                txtCode.Focus();
                return false;
            }
            else if (!CaptchaControl1.UserValidated)
            {
                clsUtility.Common.Show("Mã bảo mật không đúng");
                txtCode.Focus();
                return false;
            }
            return true;
        }
        private void Save_Info()
        {
            try
            {
                if (CheckError())
                {
                    var _vChangePass = db.GetTable<USER>().Where(a => a.USER_EMAIL == txtEmail.Value.Trim());
                    if (_vChangePass.ToList().Count > 0)
                    {
                        string _sNewPassword = Create_Random(6);
                        _vChangePass.First().USER_PW = clsUtility.Common.Encrypt(_sNewPassword, _vChangePass.First().SALT);
                        db.SubmitChanges();

                        string strEmailBody = "";

                        strEmailBody = "<html><body>";
                        strEmailBody += "Xin chào, " + _vChangePass.First().USER_NAME + "!<br />";
                        strEmailBody += "Mật khẩu mới của bạn tại atlaslogistics là: " + _sNewPassword + "<br />";
                        strEmailBody += "Sau khi đăng nhập lại vui lòng đổi lại mật khẩu để việc đăng nhập trở nên thuận tiện hơn<br />";
                        strEmailBody += "</body></html>";

                        SendEmailSMTP("Mật khẩu mới của bạn tại atlaslogistics", txtEmail.Value, "", "", strEmailBody, true, false);

                        Response.Write("<script LANGUAGE='JavaScript' >alert('Thông báo: Mật khẩu mới đã được gửi vào mail của bạn. Sau khi đăng nhập lại vui lòng đổi lại mật khẩu để việc đăng nhập trở nên thuận tiện hơn!');document.location='" + ResolveClientUrl("/Pages/dang-nhap.aspx") + "';</script>");

                    }
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        #endregion

        #region Button Handler

        public void SendEmailSMTP(string strSubject, string toAddress, string ccAddress, string bccAddress, string body, bool isHtml, bool isSSL)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(clsUtility.Utils.CStrDef(System.Configuration.ConfigurationManager.AppSettings["Email"]), clsUtility.Utils.CStrDef(System.Configuration.ConfigurationManager.AppSettings["EmailDisplayName"]));
                    mail.To.Add(toAddress);
                    if (ccAddress != "")
                    {
                        mail.CC.Add(ccAddress);
                    }
                    if (bccAddress != "")
                    {
                        mail.Bcc.Add(bccAddress);
                    }
                    mail.Subject = strSubject;

                    string str = body;
                    mail.Body = str;
                    mail.IsBodyHtml = isHtml;
                    SmtpClient client = new SmtpClient();
                    client.EnableSsl = isSSL;
                    client.Host = clsUtility.Utils.CStrDef(System.Configuration.ConfigurationManager.AppSettings["EmailHost"]);
                    client.Port = clsUtility.Utils.CIntDef(System.Configuration.ConfigurationManager.AppSettings["EmailPort"]);
                    client.Credentials = new System.Net.NetworkCredential(clsUtility.Utils.CStrDef(System.Configuration.ConfigurationManager.AppSettings["Email"]), clsUtility.Utils.CStrDef(System.Configuration.ConfigurationManager.AppSettings["EmailPassword"]));

                    client.Send(mail);
                }
            }
            catch (SmtpException ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        protected void lbtnforgotpass_Click(object sender, EventArgs e)
        {
            try
            {
                Save_Info();
            }
            catch
            {
                clsUtility.Common.Show("gui mail khong thanh cong!");
            }

        }

        #endregion
    }
}