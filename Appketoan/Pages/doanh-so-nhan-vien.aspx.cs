using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Appketoan.Data;
using vpro.functions;
using Appketoan.Components;

namespace Appketoan.Pages
{
    public partial class doanh_so_nhan_vien : System.Web.UI.Page
    {
        #region Declare
        private AppketoanDataContext db = new AppketoanDataContext();
        private clsFormat fm = new clsFormat();
        private UserRepo _UserRepo = new UserRepo();
        private ContractRepo _ContractRepo = new ContractRepo();
        private EmployerRepo _EmployerRepo = new EmployerRepo();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDay();
                LoadMonth();
                LoadYear();
                showEmployer();
                lbtnSearch_Click(null, null);
            }
            
        }
        private void showEmployer()
        {
            var list = _EmployerRepo.GetAllSortName();
            ddlEmployer.DataValueField = "ID";
            ddlEmployer.DataTextField = "EMP_NAME";
            ddlEmployer.DataSource = list;
            ddlEmployer.DataBind();
            ListItem l = new ListItem("--- Chọn nhân viên ---", "0");
            l.Selected = true;
            ddlEmployer.Items.Insert(0, l);
        }
        private void LoadDay()
        {
            ddlDay.Items.Add(new ListItem("--Ngày--", "0"));
            for (int i = 1; i <= 31; i++)
            {
                ddlDay.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            ddlDay.SelectedValue = DateTime.Now.Day.ToString();
        }
        private void LoadMonth()
        {
            ddlMonth.Items.Add(new ListItem("--Tháng--", "0"));
            for (int i = 1; i <= 12; i++)
            {
                ddlMonth.Items.Add(new ListItem(i.ToString(),i.ToString()));
            }
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
        }
        private void LoadYear()
        {
            ddlYear.Items.Add(new ListItem("--Năm--", "0"));

            for (int i = DateTime.Now.Year; i >= DateTime.Now.Year - 10; i--)
            {
                ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            ddlYear.SelectedValue = DateTime.Now.Year.ToString();
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            string stremploye = Utils.CStrDef(ddlEmployer.SelectedValue);
            int idday = Utils.CIntDef(ddlDay.SelectedValue);
            int idmonth = Utils.CIntDef(ddlMonth.SelectedValue);
            int idyear = Utils.CIntDef(ddlYear.SelectedValue);
            //bán hàng
            var list = db.CONTRACTs.Where(n =>            
                (n.CONT_DELI_DATE.Value.Day == idday || idday == 0)
                && (n.CONT_DELI_DATE.Value.Month == idmonth || idmonth == 0)
                && (n.CONT_DELI_DATE.Value.Year == idyear || idyear == 0)
                ).ToList();
            int i = list.Count;
            if (stremploye != "" && stremploye != "0")
            {
                i = 0;
                foreach (var item in list)
                {
                    string[] strs = item.EMP_BH.Split(',');
                    foreach (var str in strs)
                    {
                        if (str == stremploye)
                            i++;
                    }
                }
            }
            lbBanhang.Text = i + " Hợp đồng";
            //xác minh
            var listxm = db.CONTRACTs.Where(n => 
                (n.CONT_DELI_DATE.Value.Day == idday || idday == 0)
                && (n.CONT_DELI_DATE.Value.Month == idmonth || idmonth == 0)
                && (n.CONT_DELI_DATE.Value.Year == idyear || idyear == 0)
                ).ToList();
            i = listxm.Count;
            if (stremploye != "" && stremploye != "0")
            {
                i = 0;
                foreach (var item in listxm)
                {
                    string[] strs = item.EMP_XM.Split(',');
                    foreach (var str in strs)
                    {
                        if (str == stremploye)
                            i++;
                    }
                }
            }
            lbXacminh.Text = i + " Hợp đồng";
            //giao hàng
            var listgh = db.CONTRACTs.Where(n => 
                (n.CONT_DELI_DATE.Value.Day == idday || idday == 0)
                && (n.CONT_DELI_DATE.Value.Month == idmonth || idmonth == 0)
                && (n.CONT_DELI_DATE.Value.Year == idyear || idyear == 0)
                ).ToList();
            i = listgh.Count;
            if (stremploye != "" && stremploye != "0")
            {
                i = 0;
                foreach (var item in listgh)
                {
                    string[] strs = item.EMP_GH.Split(',');
                    foreach (var str in strs)
                    {
                        if (str == stremploye)
                            i++;
                    }
                }
            }
            lbGiaohang.Text = i + " Hợp đồng";
            //thu ngân
            int idEmp =Utils.CIntDef(stremploye);
            var listtn = db.BILLs.Where(n => (n.ID_EMPLOY == idEmp || idEmp == 0)
                && (n.BILLL_RECEIVER_DATE.Value.Day == idday || idday == 0)
                && (n.BILLL_RECEIVER_DATE.Value.Month == idmonth || idmonth == 0)
                && (n.BILLL_RECEIVER_DATE.Value.Year == idyear || idyear == 0)
                ).ToList();
            lbThungan.Text = listtn.Count + " Hợp đồng";
        }
        #region function
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
        public string getnameCus(object idcus)
        {
            int _id = Utils.CIntDef(idcus);
            var list = db.CUSTOMERs.Where(n => n.ID == _id).ToList();
            if (list.Count > 0)
            {
                return list[0].CUS_FULLNAME;
            }
            return "";
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
        public string getnameAdd(object idcus)
        {
            int _id = Utils.CIntDef(idcus);
            var list = db.CUSTOMERs.Where(n => n.ID == _id).ToList();
            if (list.Count > 0)
            {
                return list[0].CUS_ADDRESS;
            }
            return "";
        }
        public string getPhone(object idcus)
        {
            int _id = Utils.CIntDef(idcus);
            var list = db.CUSTOMERs.Where(n => n.ID == _id).ToList();
            if (list.Count > 0)
            {
                return list[0].CUS_PHONE;
            }
            return "";
        }
        public string getLink(object id)
        {
            return "chi-tiet-hop-dong.aspx?id=" + id;
        }
        public string getLinkCus(object id)
        {
            return "chi-tiet-khach-hang.aspx?id=" + id;
        }
        public string getContractType(object type)
        {
            int _idac = Utils.CIntDef(type);
            switch (_idac)
            {
                case 1: return "1 tuần";
                case 2: return "1 tuần";
                case 3: return "1 tháng";
            }
            return "";
        }
        public string getContractStatus(object status)
        {
            int _idac = Utils.CIntDef(status);
            switch (_idac)
            {
                case 2: return "Còn góp";
                case 3: return "Thanh lý";
                case 4: return "Dựt nợ";
            }
            return "";
        }
        public string getDate(object date)
        {
            return string.Format("{0:dd/MM/yyyy}", date);
        }
        public string formatMoney(object money)
        {
            return fm.FormatMoney(money);
        }
        private decimal getAllthu(object _idct)
        {
            int _id = Utils.CIntDef(_idct);
            decimal total = Utils.CDecDef(db.CONTRACT_DETAILs.Where(n => n.ID_CONT == _id).Sum(n => n.CONTD_PAY_PRICE));
            return total;
        }
        public decimal getAllthattoat(object CONT_DEBT_PRICE, object _idct)
        {
            decimal _total = Utils.CDecDef(CONT_DEBT_PRICE) - getAllthu(_idct);
            return _total;
        }
        public string getAllthuPirce(object _idct)
        {
            return fm.FormatMoney(getAllthu(_idct));
        }
        public string getMoneythattoat(object CONT_DEBT_PRICE, object _idct)
        {
            var c = _ContractRepo.GetById(Utils.CIntDef(_idct));
            if (c.CONT_STATUS == 3 || c.CONT_STATUS == 4)
            {
                decimal _total = Utils.CDecDef(CONT_DEBT_PRICE) - getAllthu(_idct);
                return fm.FormatMoney(_total);
            }
            return "";
        }
        public string getMoneyconlai(object CONT_DEBT_PRICE, object _idct)
        {
            decimal _total = Utils.CDecDef(CONT_DEBT_PRICE) - getAllthu(_idct);
            return fm.FormatMoney(_total);
        }

        public string getSumTotal()
        {
            if (HttpContext.Current.Session["ktoan.listcontract"] != null)
            {
                var l = (List<CONTRACT>)HttpContext.Current.Session["ktoan.listcontract"];
                var sumtotal = l.Sum(n => n.CONT_TOTAL_PRICE);
                return fm.FormatMoney(sumtotal);
            }
            return fm.FormatMoney(0);
        }
        public string getSumDeli()
        {
            if (HttpContext.Current.Session["ktoan.listcontract"] != null)
            {
                var l = (List<CONTRACT>)HttpContext.Current.Session["ktoan.listcontract"];
                var sumtotal = l.Sum(n => n.CONT_DELI_PRICE);
                return fm.FormatMoney(sumtotal);
            }
            return fm.FormatMoney(0);
        }
        public string getSumThu()
        {
            if (HttpContext.Current.Session["ktoan.listcontract"] != null)
            {
                var l = (List<CONTRACT>)HttpContext.Current.Session["ktoan.listcontract"];
                decimal c = 0;
                foreach (var item in l)
                {
                    c += getAllthu(item.ID);
                }
                return fm.FormatMoney(c);
            }
            return fm.FormatMoney(0);
        }
        public string getSumthattoat()
        {
            if (HttpContext.Current.Session["ktoan.listcontract"] != null)
            {
                var l = (List<CONTRACT>)HttpContext.Current.Session["ktoan.listcontract"];
                l = l.Where(a => a.CONT_STATUS == 3 || a.CONT_STATUS == 4).ToList();
                decimal c = 0;
                foreach (var item in l)
                {
                    c += getAllthattoat(item.CONT_DEBT_PRICE, item.ID);
                }
                return fm.FormatMoney(c);
            }
            return fm.FormatMoney(0);
        }
        #endregion
    }
}