﻿using System;
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
    public partial class doanh_so_nhan_vien_thu_ngan : System.Web.UI.Page
    {
        #region Declare
        private AppketoanDataContext db = new AppketoanDataContext();
        private clsFormat fm = new clsFormat();
        private int _count = 1;
        private UserRepo _UserRepo = new UserRepo();
        private ContractRepo _ContractRepo = new ContractRepo();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            _count = 1;
            if (!IsPostBack)
            {
                LoadMonth();
                LoadYear();
                Load_listcontract();
            }
            else
            {
                ASPxGridView_contract.DataSource = HttpContext.Current.Session["ktoan.listcontract"];
                ASPxGridView_contract.DataBind();
            }
            
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
        private void Load_listcontract()
        {
            int idstatus = Utils.CIntDef(ddlContractStatus.SelectedValue);
            int idmonth = Utils.CIntDef(ddlMonth.SelectedValue);
            int idyear = Utils.CIntDef(ddlYear.SelectedValue);
            var list = db.CONTRACTs.Where(n => (n.CONT_STATUS == idstatus || idstatus == 0)
                && (n.DATE_STATUS.Value.Month == idmonth || idmonth == 0)
                && (n.DATE_STATUS.Value.Year == idyear || idyear == 0)
                ).OrderBy(n => n.ID).ToList();
                                            

            if (list.Count > 0)
            {
                HttpContext.Current.Session["ktoan.listcontract"] = list;
                ASPxGridView_contract.DataSource = list;
                ASPxGridView_contract.DataBind();
            }
            else
            {
                HttpContext.Current.Session["ktoan.listcontract"] = null;
                ASPxGridView_contract.DataSource = list;
                ASPxGridView_contract.DataBind();
            }
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            Load_listcontract();
        }
        #region function
        public int setOrder()
        {
            return _count++;
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

        protected void ASPxGridView_contract_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            _count = 1;
        }
    }
}