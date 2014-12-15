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
    public partial class danh_sach_khach_hang_khong_giao : System.Web.UI.Page
    {
        #region Declare
        private CustomerNoDeliRepo _CustomerNoDeliRepo = new CustomerNoDeliRepo();
        private UserRepo _UserRepo = new UserRepo();
        private EmployerRepo _EmployerRepo = new EmployerRepo();
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
                var list = _CustomerNoDeliRepo.GetListByNameAndProcess(txtKeyword.Value, Utils.CIntDef(ddlStatus.SelectedItem.Value));

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
                _CustomerNoDeliRepo.Remove(Utils.CIntDef(item));
            }

            //LoadCustomer();

            Response.Redirect("danh-sach-khach-hang-khong-giao.aspx");
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
            return Utils.CIntDef(id) > 0 ? "chi-tiet-khach-hang-khong-giao.aspx?id=" + Utils.CIntDef(id) : "chi-tiet-khach-hang-khong-giao.aspx";
        }
        public string getlinkHistory(object id)
        {
            return Utils.CIntDef(id) > 0 ? "lich-su-khach-hang.aspx?id=" + Utils.CIntDef(id) : "lich-su-khach-hang.aspx";
        }
        public string getlinkXLCus(object id)
        {
            int idxl = Utils.CIntDef(id);
            CUSTOMER_NODELI cus_nodeli = _CustomerNoDeliRepo.GetById(idxl);
            if (cus_nodeli != null)
            {
                if(Utils.CIntDef(cus_nodeli.PROCESS_STATUS)!=0)
                    return "javascript:void(0)";
                return Utils.CIntDef(id) > 0 ? "chi-tiet-khach-hang.aspx?cusnodeli=" + Utils.CIntDef(id) : "chi-tiet-khach-hang";
            }
            return "javascript:void(0)";
        }
        public string checkCus(object cusid)
        {
            int idc = Utils.CIntDef(cusid);
            if (idc > 0)
            {
                return "KH Củ";
            }
            return "KH mới";
        }
        public string getEmp(object empIds)
        {
            string name = "";
            string[] empIdsArr = Utils.CStrDef(empIds).Split(',');
            foreach (var item in empIdsArr)
            {
                EMPLOYER emp = _EmployerRepo.GetById(Utils.CIntDef(item));
                if (emp != null)
                {
                    name += emp.EMP_NAME + ",";
                }
            }
            if (name.Length > 0)
                name = name.Substring(0, name.Length - 1);
            return name;
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
        public string getStatus(object status)
        {
            int _id = Utils.CIntDef(status);
            if(_id == 0)
            {
                return "Chưa xử lý";
            }
            return "Đã xử lý";
        }
        #endregion
    }
}