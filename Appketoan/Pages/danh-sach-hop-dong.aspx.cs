using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using Appketoan.Components;
using Appketoan.Data;
using System.Drawing;
using System.Data;

namespace Appketoan.Pages
{
    public partial class danh_sach_hop_dong : System.Web.UI.Page
    {
        #region Declare
        private AppketoanDataContext db = new AppketoanDataContext();
        private clsFormat fm = new clsFormat();
        private int iduser, cusid, status;
        private int _page = 0;
        private string num = "";
        private UserRepo _UserRepo = new UserRepo();
        private ContractRepo _ContractRepo = new ContractRepo();
        private EmployerRepo _EmployerRepo = new EmployerRepo();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            iduser = Utils.CIntDef(Session["Userid"]);
            num = Utils.CStrDef(Request.QueryString["num"]);//so hợp đồng
            status = Utils.CIntDef(Request.QueryString["status"]);//tình trạng hợp đồng
            cusid = Utils.CIntDef(Request.QueryString["cusid"]);
            _page = Utils.CIntDef(Request.QueryString["page"]);
            if (!IsPostBack)
            {
                Load_listcontract();
                txtKeyword.Value = num;
                ddlContractStatus.SelectedValue = status.ToString();
            }
            else
            {
                //if (HttpContext.Current.Session["ktoan.listcontract"] != null)
                //{
                //    ASPxGridView_contract.DataSource = HttpContext.Current.Session["ktoan.listcontract"];
                //    ASPxGridView_contract.DataBind();
                //}
            }
        }
        #region Loaddata
        private void Load_listcontract()
        {            
            var list = db.CONTRACTs.Where(n => (n.CONT_NO.Contains(num) || num == "")
                                        && (n.CONT_STATUS == status || status == 0)
                                        && (n.ID_CUS == cusid || cusid == 0)
                                        && (n.IS_DELETE == false || n.IS_DELETE == null)
                                            ).OrderByDescending(n => n.ID).ToList();

            //if (list.Count > 0)
            //{
            //    HttpContext.Current.Session["ktoan.listcontract"] = list;
            //    ASPxGridView_contract.DataSource = list;
            //    ASPxGridView_contract.DataBind();
            //}
            //else
            //{
            //    HttpContext.Current.Session["ktoan.listcontract"] = null;
            //    ASPxGridView_contract.DataSource = null;
            //    ASPxGridView_contract.DataBind();
            //}

            int sotin = 20;
            if (list.Count > 0)
            {
                if (_page != 0)
                {
                    ASPxGridView_contract.DataSource = list.Skip(sotin * _page - sotin).Take(sotin);
                    ASPxGridView_contract.DataBind();
                }
                else
                {
                    ASPxGridView_contract.DataSource = list.Take(sotin);
                    ASPxGridView_contract.DataBind();
                }
                int count = list.Count;

                ltrPage.Text = result(count, sotin, _page);

            }
        }
        public string result(int tongsotin, int sotin, int _page)
        {
            string _re = string.Empty;
            int kiemtradu = tongsotin % sotin;
            int _sotrang;
            if (_page == 0)
            {
                _page = 1;
            }
            if (kiemtradu != 0)
            {
                _sotrang = (tongsotin / sotin) + 1;
            }
            else
            {
                _sotrang = (tongsotin / sotin);
            }
            LoadDDLPage(_sotrang);
            lbtotalContract.Text = (sotin * _page - sotin + 1) + " đến " + (sotin * _page) + " của " + tongsotin + " Hợp đồng";
            if (_sotrang == 1)
            {
                _re = "";
                ddlPage.Visible = false;
            }
            else
            {
                int s = 1;
                if (_sotrang > 10)
                {
                    if (_page >= 10 && _page < _sotrang)
                    {
                        _sotrang = _page + 1;
                        s = _page - 10 + 2;
                    }
                    else if (_page == _sotrang)
                    {
                        _sotrang = _page;
                        s = _page - 10 + 1;
                    }
                    else _sotrang = 10;
                }
                for (int i = s; i <= _sotrang; i++)
                {
                    if (_page == i)
                    {
                        _re += "<b>" + i + "</b>";
                        ddlPage.SelectedValue = i.ToString();
                    }
                    else
                    {
                        if (i == _sotrang && _page >= 10)
                        {
                            _re += "<a href='/Pages/danh-sach-hop-dong.aspx?page=" + (_page + 1);
                            _re += num.Length > 0 ? "&num=" + num : "";
                            _re += status > 0 ? "&status=" + status : "";
                            _re += cusid > 0 ? "&cusid=" + cusid : "";
                            _re += "'> >> </a>";
                        }
                        else if (i == s && _page >= 10)
                        {
                            _re += "<a href='/Pages/danh-sach-hop-dong.aspx?page=" + (_page - 1);
                            _re += num.Length > 0 ? "&num=" + num : "";
                            _re += status > 0 ? "&status=" + status : "";
                            _re += cusid > 0 ? "&cusid=" + cusid : "";
                            _re += "'> << </a>";
                        }
                        else
                        {
                            _re += "<a href='/Pages/danh-sach-hop-dong.aspx?page=" + i;
                            _re += num.Length > 0 ? "&num=" + num : "";
                            _re += status > 0 ? "&status=" + status : "";
                            _re += cusid > 0 ? "&cusid=" + cusid : "";
                            _re += "'>" + i + "</a>";
                        }
                    }
                }
            }
            return _re;
        }
        private void LoadDDLPage(int total)
        {
            for (int i = 1; i <= total; i++)
            {
                ddlPage.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

        }
        protected void ASPxGridView_contract_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
        {
            var c = _ContractRepo.GetById(Utils.CIntDef(e.KeyValue));
            if ( c!= null && c.CONT_STATUS == 2)
            {
                //var l = db.CONTRACT_DETAILs.Where(n => n.ID_CONT == c.ID
                //&& (n.CONTD_PAY_PRICE == null || n.CONTD_PAY_PRICE == 0)
                //&& n.CONTD_DATE_THU < DateTime.Now.Date
                //).ToList();
                //if (l != null && l.Count > 0)
                //{
                //    e.Row.ForeColor = Color.Red;
                //    e.Row.BackColor = Color.Yellow;
                //}

                var de = db.CONTRACT_DETAILs.Where(n => n.ID_CONT == c.ID
                && n.CONTD_DATE_THU < DateTime.Now.Date
                ).OrderByDescending(n=>n.CONTD_DATE_THU).Take(2).ToList();//lấy 2 cái gần nhất
                if (de != null && de.Count >= 2)
                {
                    var de2 = de.Where(n => (n.CONTD_PAY_PRICE == null || n.CONTD_PAY_PRICE == 0)).ToList();
                    if (de2 != null && de2.Count == 2)//trể 2 kỳ liên tiếp
                    {
                        e.Row.ForeColor = Color.White;
                        e.Row.BackColor = Color.Red;
                    }
                    else if (de2 != null && de2.Count == 1)
                    {
                        if (de[0].ID == de2[0].ID)
                        {
                            e.Row.ForeColor = Color.Red;
                            e.Row.BackColor = Color.Yellow;
                        }
                    }
                }
                
            }
        }
        #endregion

        #region Delete
        private void delete_Contract()
        {
            List<object> fieldValues = ASPxGridView_contract.GetSelectedFieldValues(new string[] { "ID" });
            //var list = db.CONTRACTs.Where(n => fieldValues.Contains(n.ID));
            foreach (var item in fieldValues)
            {
                CONTRACT i = _ContractRepo.GetById(Utils.CIntDef(item));
                i.IS_DELETE = true;
                _ContractRepo.Update(i);
            }
            //db.CONTRACTs.DeleteAllOnSubmit(list);
            //db.SubmitChanges();
            Load_listcontract();
        }
        #endregion

        #region function      
       

        public string getTitle(object title, int length)
        {
            string str = Utils.CStrDef(title);
            if (str.Length > length)
                str = str.Substring(0, length - 3)+"...";
            return str;
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
        public string getCountTre(object _idct)
        {
            int count = getCountContractTre(_idct);
            if (count > 0)
                return count + " kỳ";
            return "";
        }
        private int getCountContractTre(object _idct)
        {
            int _id = Utils.CIntDef(_idct);
            var c = _ContractRepo.GetById(_id);
            if (c.CONT_STATUS == 2)
            {
                var l = db.CONTRACT_DETAILs.Where(n => n.ID_CONT == _id
                && (n.CONTD_PAY_PRICE == null || n.CONTD_PAY_PRICE == 0)
                && n.CONTD_DATE_THU < DateTime.Now.Date
                ).ToList();
                if (l != null && l.Count > 0)
                {
                    return l.Count;
                }
            }
            return 0;
        }
        private decimal getAllthu(object _idct)
        {
            int _id = Utils.CIntDef(_idct);
            decimal total = Utils.CDecDef(db.CONTRACT_DETAILs.Where(n => n.ID_CONT == _id).Sum(n => n.CONTD_PAY_PRICE));
            return total;
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
        #endregion

        protected void lbtnDeleteKeyword_Click(object sender, EventArgs e)
        {
            txtKeyword.Value = "";
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            string _re = "";
            num = txtKeyword.Value;
            status = Utils.CIntDef(ddlContractStatus.SelectedValue);
            _re += "~/Pages/danh-sach-hop-dong.aspx?page=" + 1;
            _re += num.Length > 0 ? "&num=" + num : "";
            _re += status > 0 ? "&status=" + status : "";
            _re += cusid > 0 ? "&cusid=" + cusid : "";
            Response.Redirect(_re);
        }

        protected void lbtnDelete_Click1(object sender, EventArgs e)
        {
            delete_Contract();
        }

        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            string _re = "";
            _page = Utils.CIntDef(ddlPage.SelectedValue);
            _re += "~/Pages/danh-sach-hop-dong.aspx?page=" + _page;
            _re += num.Length > 0 ? "&num=" + num : "";
            _re += status > 0 ? "&status=" + status : "";
            _re += cusid > 0 ? "&cusid=" + cusid : "";
            Response.Redirect(_re);
        }

    }
}