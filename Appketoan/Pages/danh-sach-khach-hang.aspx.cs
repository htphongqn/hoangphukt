using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using Appketoan.Data;
using Appketoan.Components;

namespace Appketoan.Pages
{
    public partial class danh_sach_khach_hang : System.Web.UI.Page
    {
        #region Declare
        private CustomerRepo _CustomerRepo = new CustomerRepo();
        private UserRepo _UserRepo = new UserRepo();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCustomer();
            }
            else
            {
                ASPxGridView1_Customer.DataSource = HttpContext.Current.Session["listCustomer"];
                ASPxGridView1_Customer.DataBind();
            }
        }

        private void LoadCustomer()
        {
            try
            {
                var list = _CustomerRepo.GetListByName(txtKeyword.Value);

                HttpContext.Current.Session["listCustomer"] = list;
                ASPxGridView1_Customer.DataSource = list;
                ASPxGridView1_Customer.DataBind();

            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            LoadCustomer();
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            List<object> fieldValues = ASPxGridView1_Customer.GetSelectedFieldValues(new string[] { "ID" });
            foreach (var item in fieldValues)
            {
                _CustomerRepo.Remove(Utils.CIntDef(item));
            }

            LoadCustomer();
        }

        protected void lbtnDeleteKeyword_Click(object sender, EventArgs e)
        {
            txtKeyword.Value = "";
        }

        #region Function
        public string getTitle(object title, int length)
        {
            string str = Utils.CStrDef(title);
            if (str.Length > length)
                str = str.Substring(0, length - 3) + "...";
            return str;
        }
        public string getlink(object id)
        {
            return Utils.CIntDef(id) > 0 ? "chi-tiet-khach-hang.aspx?id=" + Utils.CIntDef(id) : "chi-tiet-khach-hang.aspx";
        }
        public string getlinkHistory(object id)
        {
            return Utils.CIntDef(id) > 0 ? "lich-su-khach-hang.aspx?id=" + Utils.CIntDef(id) : "lich-su-khach-hang.aspx";
        }
        public string getlinkInsertContract(object id)
        {
            return Utils.CIntDef(id) > 0 ? "chi-tiet-hop-dong.aspx?cusid=" + Utils.CIntDef(id) : "chi-tiet-hop-dong.aspx";
        }
        public string getlinkViewContract(object id)
        {
            return Utils.CIntDef(id) > 0 ? "danh-sach-hop-dong.aspx?cusid=" + Utils.CIntDef(id) : "danh-sach-hop-dong.aspx";
        }
        public string getDate(object News_PublishDate)
        {
            return string.Format("{0:dd/MM/yyyy}", News_PublishDate);
        }
        public string GetType(object type)
        {
            return Utils.CIntDef(type) == Cost.CUSTOMER_GOOD ? "<span style='color:blue'>Tốt</span>" : (Utils.CIntDef(type) == Cost.CUSTOMER_HANDLING ? "<span style='color:aqua'>Xử lý</span>" : "<span style='color:red'>Xấu</span>");
        }
        public string Getactive(object active)
        {
            return Utils.CIntDef(active) == 1 ? "Kích hoạt" : "Chưa kích hoạt";
        }
        public string getUserName(object idUser)
        {
            int _id = Utils.CIntDef(idUser);
            var user = _UserRepo.GetById(_id);
            if (user != null)
            {
                return user.USER_NAME;
            }
            return "";
        }
        #endregion
    }
}