using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxTreeList;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Collections;
using Appketoan.Components;
using vpro.functions;

namespace Appketoan.Pages
{
    public partial class changeinfo : System.Web.UI.Page
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
            _iUserID = clsUtility.Utils.CIntDef(Session["Userid"]);
            if (_iUserID > 0)
            {
                txtUserName.ReadOnly = txtEmail.ReadOnly = true;
                ddlGroup.Enabled=ddlloai.Enabled = false;
                
              
            }
            if (!IsPostBack)
            {
                Load_Year(60, 18);
                Load_Group();
                Get_Info();

            }
        }
        #endregion

        #region My Fuction

        private void Load_Group()
        {
            try
            {
                var list = db.GetTable<GROUP>().OrderBy(c => c.GROUP_NAME);
                ddlGroup.DataSource = list;
                ddlGroup.DataTextField = "GROUP_NAME";
                ddlGroup.DataValueField = "GROUP_ID";
                ddlGroup.DataBind();
                ListItem l = new ListItem("---------- Chọn nhóm ---------", "0", true); l.Selected = true;
                ddlGroup.Items.Insert(0, l);

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        private void Load_Year(int YearMax, int YearMin)
        {
            try
            {
                //ddlYear.Items.Clear();
                ArrayList aday = new ArrayList();
                for (int i = DateTime.Now.Year - YearMax; i <= DateTime.Now.Year - YearMin; i++)
                {
                    aday.Add(i);
                }
                //ddlYear.DataSource = aday;
                //ddlYear.DataBind();
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        //private void Load_Group()
        //{
        //    try
        //    {
        //        var list = db.GetTable<AT_EMPLOYEE_GROUP>().Where(c => c.AT_EMP_GROUP_ACTIVE == 1).OrderBy(c => c.AT_EMP_GROUP_NAME);
        //        ddlGroup.DataSource = list;
        //        ddlGroup.DataTextField = "AT_EMP_GROUP_NAME";
        //        ddlGroup.DataValueField = "AT_EMP_GROUP_ID";
        //        ddlGroup.DataBind();
        //        ListItem l = new ListItem("---------- Chọn nhóm ---------", "0", true); l.Selected = true;
        //        ddlGroup.Items.Insert(0, l);

        //    }
        //    catch (Exception ex)
        //    {
        //        clsVproErrorHandler.HandlerError(ex);
        //    }
        //}

        private void Get_Info()
        {
            try
            {
                var G_info = db.GetTable<USER>().Where(a => a.USER_ID == _iUserID);
                if (G_info.ToList().Count > 0)
                {
                    trRepass.Visible = false;
                    trPass.Visible = false;
                    txtUserName.Text = G_info.ToList()[0].USER_UN;
                    txtEmail.Text = G_info.ToList()[0].USER_EMAIL;
                    txtUserFullName.Text = G_info.ToList()[0].USER_NAME;
                    //rblGender.SelectedValue = clsUtility.Utils.CStrDef(G_info.ToList()[0].AT_EMP_GENDER, string.Empty);
                    //txtIdentity.Text = G_info.ToList()[0].USE_OIDENTITY;
                    //txtCode.Text = G_info.ToList()[0].USE_CODE;

                    DateTime _dateBirth = new DateTime();

                    //_dateBirth = clsUtility.Utils.CDateDef(G_info.ToList()[0].AT_EMP_DATEOFBIRTH, DateTime.Now);

                    //ddlYear.SelectedValue = clsUtility.Utils.CStrDef(_dateBirth.Year, string.Empty);

                    // ddlMonth.SelectedValue = clsUtility.Utils.CStrDef(_dateBirth.Month, string.Empty);

                    //ddlDay.SelectedValue = clsUtility.Utils.CStrDef(_dateBirth.Day, string.Empty);

                    txtAddress.Text = clsUtility.Utils.CStrDef(G_info.ToList()[0].USER_ADDRESS, string.Empty);

                    txtPhone.Text = G_info.First().USER_PHONE;

                    rblActive.SelectedValue = clsUtility.Utils.CStrDef(G_info.ToList()[0].USER_ACTIVE, string.Empty);
                    // ddlGroup.SelectedValue = G_info.ToList()[0].USE_TITLE + "";
                    //txtLastUpdate.Text = string.Format("{0:dd/MM/yyyy - hh:mm}", G_info.ToList()[0].AT_EMP_LASTUPDATE);
                    //int chucvu = clsUtility.Utils.CIntDef(G_info.ToList()[0].USE_TITLE, 0);
                    //int loai = clsUtility.Utils.CIntDef(G_info.ToList()[0].USE_TYPE, 0);
                    //if (chucvu==1)
                    //{
                    //    ddlchucvu.Text = "Thủ trưởng";
                    //}
                    //else if (chucvu == 2)
                    //{
                    //    ddlchucvu.Text = "Thủ kho";
                    //}
                    //else if (chucvu == 3)
                    //{
                    //    ddlchucvu.Text = "Người lập biểu";

                    //}
                    //else if (chucvu == 4)
                    //{
                    //    ddlchucvu.Text = "Quản trị hệ thống";
                    //}
                    //else if(loai==1)
                    //{
                    //    ddlloai.Text = "Manager";
                    //}
                    //else if(loai==2)
                    //{
                    //    ddlloai.Text = "Admin";
                    //}
                    //else if (loai == 3)
                    //{
                    //    ddlloai.Text = "Editor";
                    //}
                    ddlGroup.SelectedValue = G_info.ToList()[0].GROUP_ID.ToString();
                    //ddlchucvu.SelectedValue = clsUtility.Utils.CStrDef(clsUtility.Utils.CIntDef(G_info.ToList()[0].USE_TITLE, 0), string.Empty);
                    ddlloai.SelectedValue = G_info.ToList()[0].USER_TYPE.ToString();

                }
            }

            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }

        }

        private void CreateDirectory(int UserID)
        {
            m_pathFile = clsUtility.PathFiles.GetPathUser(UserID);

            if (!Directory.Exists(Server.MapPath(m_pathFile)))
            {
                Directory.CreateDirectory(Server.MapPath(m_pathFile));
            }
            //Session["FileManager"] = m_pathFile;
        }

        private bool ValidCheck()
        {
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                clsUtility.Common.Show("Tên đăng nhập không được rỗng!");
                txtUserName.Focus();
                return false;
            }
            else if (txtUserName.Text.Trim().Length < 4 || txtUserName.Text.Length > 200)
            {
                clsUtility.Common.Show("Tên đăng nhập phải từ 4 ký tự đến 200 ký tự!");
                txtUserName.Focus();
                return false;
            }
            else if (CheckExitsUser())
            {
                clsUtility.Common.Show("Tên đăng nhập này đã có người sử dụng!");
                txtUserName.Focus();
                return false;
            }
            else if (_iUserID == 0)
            {
                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    clsUtility.Common.Show("Mật khẩu không được rỗng!");
                    txtPassword.Focus();
                    return false;
                }
                else if (txtPassword.Text.Trim().Length < 6 || txtPassword.Text.Length > 200)
                {
                    clsUtility.Common.Show("Mật khẩu phải từ 6 ký tự đến 200 ký tự!");
                    txtPassword.Focus();
                    return false;
                }
                else if (txtPassword.Text != txtRePassword.Text)
                {
                    clsUtility.Common.Show("Mật khẩu không trùng khớp!");
                    txtRePassword.Focus();
                    return false;
                }
            }
            else if (string.IsNullOrEmpty(txtEmail.Text))
            {
                clsUtility.Common.Show("Email không được rỗng!");
                txtUserName.Focus();
                return false;
            }
            else if (txtEmail.Text.Length > 200)
            {
                clsUtility.Common.Show("Email không được vượt quá 200 ký tự!");
                txtUserName.Focus();
                return false;
            }
            else if (!clsUtility.Common.IsValidEmail(txtEmail.Text))
            {
                clsUtility.Common.Show("Email không hợp lệ!");
                txtUserName.Focus();
                return false;
            }
            else if (CheckExitsEmail())
            {
                clsUtility.Common.Show("Email này đã có người sử dụng!");
                txtEmail.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtUserFullName.Text))
            {
                clsUtility.Common.Show("Họ tên không được rỗng!");
                txtUserName.Focus();
                return false;
            }
            else if (txtUserFullName.Text.Length > 300)
            {
                clsUtility.Common.Show("Họ tên không được vượt quá 300 ký tự!");
                txtUserName.Focus();
                return false;
            }
            //else if (clsUtility.Utils.CIntDef(ddlDay.SelectedValue, 0) == 0)
            //{
            //    clsUtility.Common.Show("Ngày sinh không được rỗng!");
            //    ddlDay.Focus();
            //    return false;
            //}
            //else if (clsUtility.Utils.CIntDef(ddlMonth.SelectedValue, 0) == 0)
            //{
            //    clsUtility.Common.Show("Tháng sinh không được rỗng!");
            //    ddlDay.Focus();
            //    return false;
            //}
            else if (string.IsNullOrEmpty(txtAddress.Text))
            {
                clsUtility.Common.Show("Địa chỉ không được rỗng!");
                txtAddress.Focus();
                return false;
            }
            else if (clsUtility.Utils.CIntDef(ddlGroup.SelectedValue, 0) == 0)
            {
                clsUtility.Common.Show("Nhóm không được rỗng!");
                ddlGroup.Focus();
                return false;
            }
            return true;
        }

        public void Save_Info(string strLink = "")
        {
            try
            {
                if (_iUserID > 0)
                {
                    var user_update = db.GetTable<USER>().Where(g => g.USER_ID == _iUserID);

                    if (user_update.ToList().Count > 0)
                    {
                        DateTime _dateBirth = new DateTime();
                        //string _sDay = clsUtility.Utils.CStrDef(ddlDay.SelectedValue, string.Empty);
                        //string _sMonth = clsUtility.Utils.CStrDef(ddlMonth.SelectedValue, string.Empty);
                        //string _sYear = clsUtility.Utils.CStrDef(ddlYear.SelectedValue, string.Empty);

                        //_dateBirth = clsUtility.Utils.StrDateToDate(_sDay + "/" + _sMonth + "/" + _sYear, "dd/MM/yyyy");

                        user_update.First().USER_ACTIVE = Convert.ToInt16(rblActive.SelectedValue);
                        user_update.First().USER_ADDRESS = txtAddress.Text;
                        //user_update.First().USE_CODE = txtCode.Text;
                        //user_update.First().TYPE = clsUtility.Utils.CIntDef(rblType.SelectedValue, 0) == 0 ? 0 : 1;
                        // user_update.First().AT_EMP_DATEOFBIRTH = _dateBirth;
                        //user_update.First().AT_EMP_LASTUPDATE = DateTime.Now;
                        user_update.First().GROUP_ID = clsUtility.Utils.CIntDef(ddlGroup.SelectedValue, 0);
                        user_update.First().USER_EMAIL = txtEmail.Text;
                        //user_update.First().AT_EMP_GENDER = clsUtility.Utils.CIntDef(rblGender.SelectedValue);
                        user_update.First().USER_NAME = txtUserFullName.Text;
                        user_update.First().USER_PHONE = txtPhone.Text;
                        user_update.First().USER_UN = txtUserName.Text;
                        // user_update.First().USE_OIDENTITY = txtIdentity.Text;

                        db.SubmitChanges();

                        strLink = string.IsNullOrEmpty(strLink) ? "chi-tiet-nhan-vien.aspx?id=" + _iUserID : strLink;
                    }
                }
                else
                {
                    USER user_insert = new USER();

                    DateTime _dateBirth = new DateTime();
                    //string _sDay = clsUtility.Utils.CStrDef(ddlDay.SelectedValue, string.Empty);
                    //string _sMonth = clsUtility.Utils.CStrDef(ddlMonth.SelectedValue, string.Empty);
                    //string _sYear = clsUtility.Utils.CStrDef(ddlYear.SelectedValue, string.Empty);
                    string _sSalt = clsUtility.Common.CreateSalt();

                    // _dateBirth = clsUtility.Utils.StrDateToDate(_sDay + "/" + _sMonth + "/" + _sYear, "dd/MM/yyyy");

                    user_insert.USER_ACTIVE = Convert.ToInt16(rblActive.SelectedValue);
                    user_insert.USER_ADDRESS = txtAddress.Text;
                    //user_insert.USE_CODE = txtCode.Text;
                    //user_insert.USE_OIDENTITY = txtIdentity.Text;
                    //user_insert.AT_EMP_DATEOFBIRTH = _dateBirth;
                    // user_insert.AT_EMP_LASTUPDATE = DateTime.Now;
                    user_insert.GROUP_ID = clsUtility.Utils.CIntDef(ddlGroup.SelectedValue, 0);
                    user_insert.USER_EMAIL = txtEmail.Text;
                    //user_insert.AT_EMP_GENDER = clsUtility.Utils.CIntDef(rblGender.SelectedValue);
                    user_insert.USER_NAME = txtUserFullName.Text;
                    user_insert.USER_PHONE = txtPhone.Text;
                    user_insert.USER_UN = txtUserName.Text;
                    user_insert.SALT = _sSalt;
                    user_insert.USER_PW = clsUtility.Common.Encrypt(txtPassword.Text, _sSalt);

                    db.USERs.InsertOnSubmit(user_insert);
                    db.SubmitChanges();

                    var _vEmp = db.GetTable<USER>().OrderByDescending(a => a.USER_ID);

                    _iUserID = clsUtility.Utils.CIntDef(_vEmp.First().USER_ID);

                    strLink = string.IsNullOrEmpty(strLink) ? "chi-tiet-nhan-vien.aspx?id=" + _iUserID : strLink;
                }
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                if (!string.IsNullOrEmpty(strLink))
                {
                    Response.Redirect(strLink);
                }
            }
        }

        private void DeleteInfo()
        {
            string strLink = "";
            try
            {

                var G_info = db.GetTable<USER>().Where(g => g.USER_ID == _iUserID);

                db.USERs.DeleteAllOnSubmit(G_info);
                db.SubmitChanges();

                strLink = "danh-sach-nhan-vien.aspx";

            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            finally
            {
                if (!string.IsNullOrEmpty(strLink))
                    Response.Redirect(strLink);
            }
        }

        private void DeleteAllFilesInFolder(string folderpath)
        {
            foreach (var f in System.IO.Directory.GetFiles(folderpath))
                System.IO.File.Delete(f);
        }

        private bool CheckExitsUser()
        {
            try
            {
                var _user = db.GetTable<USER>().Where(u => u.USER_UN == txtUserName.Text.Trim() && u.USER_ID != _iUserID);

                if (_user.ToList().Count > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return true;
            }
        }

        private bool CheckExitsEmail()
        {
            try
            {
                var _user = db.GetTable<USER>().Where(u => u.USER_EMAIL == txtEmail.Text.Trim() && u.USER_ID != _iUserID);

                if (_user.ToList().Count > 0)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                clsVproErrorHandler.HandlerError(ex);
                return true;
            }
        }

        public string getOrder()
        {
            _count = _count + 1;
            return _count.ToString();
        }

        #endregion

        #region Button Handler

        protected void lnkSave_Click(object sender, EventArgs e)
        {
            if (ValidCheck())
            {
                Save_Info();
            }
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            if (ValidCheck())
            {
                Save_Info();
            }
        }

        protected void lbtnSaveClose_Click(object sender, EventArgs e)
        {
            if (ValidCheck())
            {
                Save_Info("danh-sach-nhan-vien.aspx");
            }
        }

        protected void lbtnSaveNew_Click(object sender, EventArgs e)
        {
            if (ValidCheck())
            {
                Save_Info("chi-tiet-nhan-vien.aspx");
            }
        }

        protected void lbtnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("trang-chu.aspx");
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            if (_iUserID > 0)
            {
                DeleteInfo();
            }
            else
            {
                clsUtility.Common.Show("Thao tác không hợp lệ!");
            }
        }

        //protected void lnkResetPass_Click(object sender, EventArgs e)
        //{
        //    var _vReset = db.USERs.Single(a => a.OID == _iUserID);
        //    if (_vReset != null)
        //    {
        //        _vReset.PASSWORD = clsUtility.Common.EncryptPass(clsUtility.Common.TaoChuoiTuDong(10));
        //        db.SubmitChanges();
        //        lblInfo.Text = "Mật khẩu đã được thay đổi ngẫu nhiên!";
        //    }
        //    Get_Info();
        //}

        #endregion
    }
}