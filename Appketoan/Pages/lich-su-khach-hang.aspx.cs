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
    public partial class lich_su_khach_hang : System.Web.UI.Page
    {
        #region Declare
        private CustomerHistoryRepo _CustomerRepo = new CustomerHistoryRepo();
        private int id = 0;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Utils.CIntDef(Request.QueryString["id"], 0);
            if (!IsPostBack)
            {
                LoadCustomer();
            }
            else
            {
                ASPxGridView1_Customer.DataSource = HttpContext.Current.Session["listCustomerHis"];
                ASPxGridView1_Customer.DataBind();
            }
        }

        private void LoadCustomer()
        {
            try
            {
                var list = _CustomerRepo.GetListByCusID(id);

                HttpContext.Current.Session["listCustomerHis"] = list;
                ASPxGridView1_Customer.DataSource = list;
                ASPxGridView1_Customer.DataBind();

            }
            catch (Exception)
            {

                throw;
            }
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
        #region Function
        public string getCustomerName(object id)
        {
            CustomerRepo cusRepo =new CustomerRepo();
            CUSTOMER cus = cusRepo.GetById(Utils.CIntDef(id));
            if (cus != null)
            {
                return cus.CUS_FULLNAME;
            }
            return "";
        }
        public string getDate(object News_PublishDate)
        {
            return string.Format("{0:dd/MM/yyyy}", News_PublishDate);
        }
        public string GetType(object type)
        {
            return Utils.CIntDef(type) == Cost.CUSTOMER_GOOD ? "<span style='color:blue'>Tốt</span>" : (Utils.CIntDef(type) == Cost.CUSTOMER_HANDLING ? "<span style='color:aqua'>Xử lý</span>" : "<span style='color:red'>Xấu</span>");
        }

        #endregion
    }
}