using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace User_Control
{
    public partial class MessageBox : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnOk.OnClientClick = String.Format("fnClickOK('{0}','{1}')", btnOk.UniqueID, "");
            btnOk1.OnClientClick = String.Format("fnClickOK('{0}','{1}')", btnOk1.UniqueID, "");
        }

        public void ShowMessage(string Message)
        {
            lblMessage.Text = Message;
            lblCaption.Text = "MessageBox";
            //tdCaption.Visible = false;
            mpext.Show();
        }

        public void ShowMessage(string Message, string Caption)
        {
            lblMessage.Text = Message;
            lblCaption.Text = Caption;
            //tdCaption.Visible = true;
            mpext.Show();
        }

        public void ShowMessage(string Message, string Caption, string Url)
        {
            lblMessage.Text = Message;
            lblCaption.Text = Caption;
            //tdCaption.Visible = true;
            btnOk.PostBackUrl = Url;
            mpext.Show();
        }

        private void Hide()
        {
            lblMessage.Text = "";
            lblCaption.Text = "";
            mpext.Hide();
        }

        public void btnOk1_Click(object sender, EventArgs e)
        {
            OnOk1ButtonPressed(e);
        }

        public delegate void Ok1ButtonPressedHandler(object sender, EventArgs args);
        public event Ok1ButtonPressedHandler Ok1ButtonPressed;
        protected virtual void OnOk1ButtonPressed(EventArgs e)
        {
            if (Ok1ButtonPressed != null)
                Ok1ButtonPressed(btnOk1, e);
        }


        public void btnOk_Click(object sender, EventArgs e)
        {
            OnOkButtonPressed(e);
        }

        public delegate void OkButtonPressedHandler(object sender, EventArgs args);
        public event OkButtonPressedHandler OkButtonPressed;
        protected virtual void OnOkButtonPressed(EventArgs e)
        {
            if (OkButtonPressed != null)
                OkButtonPressed(btnOk, e);
        }
    }
}