using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using vpro.functions;
using Appketoan.Components;
using System.Data;
using System.IO;
using System.Reflection;
using Appketoan.Data;
using DevExpress.Web.ASPxGridView;

namespace Appketoan.Pages
{
    public partial class chi_tiet_hop_dong : System.Web.UI.Page
    {
        #region Declare
        private AppketoanDataContext db = new AppketoanDataContext();
        private EmployerRepo _EmployerRepo = new EmployerRepo();
        private ContractRepo _ContractRepo = new ContractRepo();
        private CompanyRepo _CompanyRepo = new CompanyRepo();
        private ContractHistoryRepo _ContractHistoryRepo = new ContractHistoryRepo();
        private ContractHistoryWeekRepo _ContractHistoryWeekRepo = new ContractHistoryWeekRepo();
        private ContractDetailRepo _ContractDetailRepo = new ContractDetailRepo();
        private CustomerRepo _CustomerRepo = new CustomerRepo();
        private BillRepo _BillRepo = new BillRepo();
        private clsFormat fm = new clsFormat();
        private int id, _gtype, iduser, id_detail, id_bill, id_history, activetab, cusid, _count = 1, _count1 = 1;        
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            _gtype = Utils.CIntDef(Session["Grouptype"]);
            id = Utils.CIntDef(Request.QueryString["id"]);
            cusid = Utils.CIntDef(Request.QueryString["cusid"]);
            id_detail = Utils.CIntDef(Request.QueryString["iddetail"]);
            id_bill = Utils.CIntDef(Request.QueryString["idbill"]);
            id_history = Utils.CIntDef(Request.QueryString["idhistory"]);
            iduser = Utils.CIntDef(Session["Userid"]);
            activetab = Utils.CIntDef(Request.QueryString["activetab"]);

            if (!IsPostBack)
            {
                pickdate_deli.returnDate = DateTime.Now;                
                pickdateconvert.returnDate = DateTime.Now;
                loadWeek();
                Load_customer();
                LoadEmployee();
                LoadCompany();
                Showinfo();

                if (id > 0)
                {
                    Loadlist_historyweek();
                    //getInfo_detail();
                    load_list_detail();
                    //bill
                    //showEmployer();
                    showListphieu();
                    //getInfo_phieu();
                    //History
                    Loadlist_history();
                    getInfoHistory();
                }
            }
            else
            {
                if (HttpContext.Current.Session["dienmay.listctdetail"] != null)
                {
                    ASPxGridView_contractdetail.DataSource = HttpContext.Current.Session["dienmay.listctdetail"];
                    ASPxGridView_contractdetail.DataBind();
                }
                //if (HttpContext.Current.Session["dienmay.listphieu"] != null)
                //{
                //    ASPxGridView_phieu.DataSource = HttpContext.Current.Session["dienmay.listphieu"];
                //    ASPxGridView_phieu.DataBind();
                //}
                if (HttpContext.Current.Session["dienmay.listhistory"] != null)
                {
                    ASPxGridView_historyConvert.DataSource = HttpContext.Current.Session["dienmay.listhistory"];
                    ASPxGridView_historyConvert.DataBind();
                }
            }
            if (id > 0)
            {
                ASPxPageControl2.TabPages.FindByText("Phiếu thu").ClientVisible = true;
                ASPxPageControl2.TabPages.FindByText("Thu theo kỳ").ClientVisible = true;
                ASPxPageControl2.TabPages.FindByText("Lịch sử quá trình chuyển đổi").ClientVisible = true;
                if (id_detail > 0 || activetab == 2)
                    ASPxPageControl2.ActiveTabIndex = 2;
                if (id_bill > 0 || activetab == 1)
                    ASPxPageControl2.ActiveTabIndex = 1;
                if (id_history > 0 || activetab == 3)
                    ASPxPageControl2.ActiveTabIndex = 3;
            }
            else
            {
                ASPxPageControl2.TabPages.FindByText("Phiếu thu").ClientVisible = false;
                ASPxPageControl2.TabPages.FindByText("Thu theo kỳ").ClientVisible = false;
                ASPxPageControl2.TabPages.FindByText("Lịch sử quá trình chuyển đổi").ClientVisible = false;

            }
        }
        
        private void loadWeek()
        {
            ddlContractDay.Items.Add(new ListItem("", "0"));
            ddlContractDay.Items.Add(new ListItem("Thứ hai", DayOfWeek.Monday.ToString()));
            ddlContractDay.Items.Add(new ListItem("Thứ ba", DayOfWeek.Tuesday.ToString()));
            ddlContractDay.Items.Add(new ListItem("Thứ tư", DayOfWeek.Wednesday.ToString()));
            ddlContractDay.Items.Add(new ListItem("Thứ năm", DayOfWeek.Thursday.ToString()));
            ddlContractDay.Items.Add(new ListItem("Thứ sáu", DayOfWeek.Friday.ToString()));
            ddlContractDay.Items.Add(new ListItem("Thứ bảy", DayOfWeek.Saturday.ToString()));
            ddlContractDay.Items.Add(new ListItem("Chủ nhật", DayOfWeek.Sunday.ToString()));
        }
        private DayOfWeek GetDayOfWeek(int index)
        {
            DayOfWeek d = DayOfWeek.Monday;
            switch (index)
            {
                case 1:
                    d = DayOfWeek.Monday;
                    break;
                case 2:
                    d = DayOfWeek.Tuesday;
                    break;
                case 3:
                    d = DayOfWeek.Wednesday;
                    break;
                case 4:
                    d = DayOfWeek.Thursday;
                    break;
                case 5:
                    d = DayOfWeek.Friday;
                    break;
                case 6:
                    d = DayOfWeek.Saturday;
                    break;
                case 7:
                    d = DayOfWeek.Sunday;
                    break;
                default:
                    break;
            }
            return d;
        }
        private int OperationTwoWeek(DayOfWeek d1, DayOfWeek d2)//d1 hiện tại, d2 cần chuyển đổi
        {
            int op = 0;
            switch (d1)
            {
                case DayOfWeek.Monday:
                switch (d2)
	            {
                    case DayOfWeek.Monday:
                        op = 0;
                        break;
                    case DayOfWeek.Tuesday:
                        op = 1;
                        break;
                    case DayOfWeek.Wednesday:
                        op = 2;
                        break;
                    case DayOfWeek.Thursday:
                        op = 3;
                        break;
                    case DayOfWeek.Friday:
                        op = 4;
                        break;
                    case DayOfWeek.Saturday:
                        op = 5;
                        break;
                    case DayOfWeek.Sunday:
                        op = 6;
                        break;
                    default:
                        break;
	            }
                break;
                case DayOfWeek.Tuesday:
                switch (d2)
	            {
                    case DayOfWeek.Monday:
                        op = -1;
                        break;
                    case DayOfWeek.Tuesday:
                        op = 0;
                        break;
                    case DayOfWeek.Wednesday:
                        op = 1;
                        break;
                    case DayOfWeek.Thursday:
                        op = 2;
                        break;
                    case DayOfWeek.Friday:
                        op = 3;
                        break;
                    case DayOfWeek.Saturday:
                        op = 4;
                        break;
                    case DayOfWeek.Sunday:
                        op = 5;
                        break;
                    default:
                        break;
	            }
                break;
                case DayOfWeek.Wednesday:
                    switch (d2)
	                {
                        case DayOfWeek.Monday:
                            op = -2;
                            break;
                        case DayOfWeek.Tuesday:
                            op = -1;
                            break;
                        case DayOfWeek.Wednesday:
                            op = 0;
                            break;
                        case DayOfWeek.Thursday:
                            op = 1;
                            break;
                        case DayOfWeek.Friday:
                            op = 2;
                            break;
                        case DayOfWeek.Saturday:
                            op = 3;
                            break;
                        case DayOfWeek.Sunday:
                            op = 4;
                            break;
                        default:
                            break;
	                }
                    break;
                case DayOfWeek.Thursday:
                    switch (d2)
	                {
                        case DayOfWeek.Monday:
                            op = -3;
                            break;
                        case DayOfWeek.Tuesday:
                            op = -2;
                            break;
                        case DayOfWeek.Wednesday:
                            op = -1;
                            break;
                        case DayOfWeek.Thursday:
                            op = 0;
                            break;
                        case DayOfWeek.Friday:
                            op = 1;
                            break;
                        case DayOfWeek.Saturday:
                            op = 2;
                            break;
                        case DayOfWeek.Sunday:
                            op = 3;
                            break;
                        default:
                            break;
	                }
                    break;
                case DayOfWeek.Friday:
                    switch (d2)
	                {
                        case DayOfWeek.Monday:
                            op = -4;
                            break;
                        case DayOfWeek.Tuesday:
                            op = -3;
                            break;
                        case DayOfWeek.Wednesday:
                            op = -2;
                            break;
                        case DayOfWeek.Thursday:
                            op = -1;
                            break;
                        case DayOfWeek.Friday:
                            op = 0;
                            break;
                        case DayOfWeek.Saturday:
                            op = 1;
                            break;
                        case DayOfWeek.Sunday:
                            op = 2;
                            break;
                        default:
                            break;
	                }
                    break;
                case DayOfWeek.Saturday:
                    switch (d2)
	                {
                        case DayOfWeek.Monday:
                            op = -5;
                            break;
                        case DayOfWeek.Tuesday:
                            op = -4;
                            break;
                        case DayOfWeek.Wednesday:
                            op = -3;
                            break;
                        case DayOfWeek.Thursday:
                            op = -2;
                            break;
                        case DayOfWeek.Friday:
                            op = -1;
                            break;
                        case DayOfWeek.Saturday:
                            op = 0;
                            break;
                        case DayOfWeek.Sunday:
                            op = 1;
                            break;
                        default:
                            break;
	                }
                    break;
                case DayOfWeek.Sunday:
                    switch (d2)
	                {
                        case DayOfWeek.Monday:
                            op = -6;
                            break;
                        case DayOfWeek.Tuesday:
                            op = -5;
                            break;
                        case DayOfWeek.Wednesday:
                            op = -4;
                            break;
                        case DayOfWeek.Thursday:
                            op = -3;
                            break;
                        case DayOfWeek.Friday:
                            op = -2;
                            break;
                        case DayOfWeek.Saturday:
                            op = -1;
                            break;
                        case DayOfWeek.Sunday:
                            op = 0;
                            break;
                        default:
                            break;
	                }
                    break;
                default:
                    break;
            }
            return op;
        }
        
        #region Info
        private void Load_customer()
        {
            var list = _CustomerRepo.GetAll();
            Drcustomer.DataValueField = "ID";
            Drcustomer.DataTextField = "CUS_FULLNAME";
            Drcustomer.DataSource = list;
            Drcustomer.DataBind();
            //ListItem l = new ListItem("--- Chọn khách hàng ---", "0");
            //l.Selected = true;
            //Drcustomer.Items.Insert(0, l);
            if (cusid > 0)
                Drcustomer.SelectedValue = cusid.ToString();
        }
        private void LoadEmployee()
        {
            var list = _EmployerRepo.GetAllSortName();
            ddlEmployeeBH.DataSource = list;//tiep thj + nv cty
            var list2 = _EmployerRepo.GetListCtySortName();
            ddlEmployeeXM.DataSource = list2;//nv cty
            ddlEmployeeGH.DataSource = list2;//nv cty

            ddlEmployeeBH.DataBind();
            ddlEmployeeXM.DataBind();
            ddlEmployeeGH.DataBind();
        }
        private void LoadCompany()
        {
            var list = _CompanyRepo.GetAllSortName();
            ddlCompany.DataSource = list;
            ddlCompany.DataBind();
        }
        private void Showinfo()
        {
            var item = _ContractRepo.GetById(id);
            if (item != null)
            {
                txtNocontract.Text = item.CONT_NO;
                Drcustomer.Visible = false;
                lbcustomer.Visible = true;
                Drcustomer.SelectedValue = item.ID_CUS.ToString();
                CUSTOMER cus = _CustomerRepo.GetById(Utils.CIntDef(item.ID_CUS));
                if (cus != null)
                {
                    lbcustomer.Text += cus.CUS_FULLNAME;
                }   
                txtNameproduct.Text = item.CONT_PRODUCT_NAME;
                txtProductprice.Text = fm.FormatMoneyNotext(item.CONT_PRODUCT_PRICE);
                txtToltalprice.Visible = false;
                lbToltalprice.Visible = true;
                txtToltalprice.Text = item.CONT_TOTAL_PRICE.ToString();
                lbToltalprice.Text = fm.FormatMoneyNotext(item.CONT_TOTAL_PRICE);
                txtPrepayprice.Visible = false;
                lbPrepayprice.Visible = true;
                txtPrepayprice.Text = item.CONT_PREPAY_PRICE.ToString();
                lbPrepayprice.Text = fm.FormatMoneyNotext(item.CONT_PREPAY_PRICE);
                pickdate_deli.Visible = false;
                lbpickdate_deli.Visible = true;
                pickdate_deli.returnDate = Utils.CDateDef(item.CONT_DELI_DATE, DateTime.Now);
                lbpickdate_deli.Text = item.CONT_DELI_DATE.Value.ToString("dd/MM/yyyy");
                txtdeli_price_pro.Visible = false;
                lbdeli_price_pro.Visible = true;
                txtdeli_price_pro.Text = item.CONT_DELI_PRICE.ToString();
                lbdeli_price_pro.Text = fm.FormatMoneyNotext(item.CONT_DELI_PRICE);
                txtBebtprice.Visible = false;
                lbBebtprice.Visible = true;
                txtBebtprice.Text = item.CONT_DEBT_PRICE.ToString();
                Session["CONT_DEBT_PRICE"] = item.CONT_DEBT_PRICE;
                lbBebtprice.Text = fm.FormatMoneyNotext(item.CONT_DEBT_PRICE);
                Rdtypecontract.Visible = false;
                lbtypecontract.Visible = true;
                Rdtypecontract.SelectedValue = item.CONT_TYPE.ToString();
                lbtypecontract.Text = item.CONT_TYPE == 1 ? "1 tuần" : (item.CONT_TYPE == 2 ? "2 tuần" : (item.CONT_TYPE == 3 ? "1 tháng" : ""));
                txtPayprice_week.Visible = false;
                lbPayprice_week.Visible = true;
                txtPayprice_week.Text = item.CONT_WEEK_PRICE.ToString();
                lbPayprice_week.Text = fm.FormatMoneyNotext(item.CONT_WEEK_PRICE);
                txtweeKcount.Visible = false;
                lbweeKcount.Visible = true;
                txtweeKcount.Text = item.CONT_WEEK_COUNT.ToString();
                lbweeKcount.Text = item.CONT_WEEK_COUNT.ToString();
                Rdstatuscontract.SelectedValue = item.CONT_STATUS.ToString();
                txtremarkcontract.Text = item.CONT_NOTE;
                txtNoteTrack.Text = item.CONT_NOTE_TRACK;

                //string[] empBHIds = Utils.CStrDef(item.EMP_BH).Split(',');
                //foreach (var BHId in empBHIds)
                //{
                //    for (int i = 0; i < chkEmployeeBH.Items.Count; i++)
                //    {
                //        if (chkEmployeeBH.Items[i].Value == BHId)
                //            chkEmployeeBH.Items[i].Selected = true;
                //    }
                //}
                //btnApplyEmployeeBH_Click(null, null);
                //string[] empXMIds = Utils.CStrDef(item.EMP_XM).Split(',');
                //foreach (var XMId in empXMIds)
                //{
                //    for (int i = 0; i < chkEmployeeXM.Items.Count; i++)
                //    {
                //        if (chkEmployeeXM.Items[i].Value == XMId)
                //            chkEmployeeXM.Items[i].Selected = true;
                //    }
                //}
                //btnApplyEmployeeXM_Click(null, null);
                //string[] empGHIds = Utils.CStrDef(item.EMP_GH).Split(',');
                //foreach (var GHId in empGHIds)
                //{
                //    for (int i = 0; i < chkEmployeeGH.Items.Count; i++)
                //    {
                //        if (chkEmployeeGH.Items[i].Value == GHId)
                //            chkEmployeeGH.Items[i].Selected = true;
                //    }
                //}
                //btnApplyEmployeeGH_Click(null, null);

                ddlEmployeeBH.Visible = false;
                lbEmployeeBH.Visible = true;
                string[] empBHIds = Utils.CStrDef(item.EMP_BH).Split(',');
                lbEmployeeBH.Text = "";
                foreach (var BHId in empBHIds)
                {
                    EMPLOYER emp = _EmployerRepo.GetById(Utils.CIntDef(BHId));
                    if (emp != null)
                    {
                        lbEmployeeBH.Text += emp.EMP_NAME + ",";
                    }                    
                }
                if (lbEmployeeBH.Text.Length > 0)
                    lbEmployeeBH.Text = lbEmployeeBH.Text.Substring(0, lbEmployeeBH.Text.Length - 1);

                ddlEmployeeXM.Visible = false;
                lbEmployeeXM.Visible = true;
                string[] empXMIds = Utils.CStrDef(item.EMP_XM).Split(',');
                lbEmployeeXM.Text = "";
                foreach (var XMId in empXMIds)
                {
                    EMPLOYER emp = _EmployerRepo.GetById(Utils.CIntDef(XMId));
                    if (emp != null)
                    {
                        lbEmployeeXM.Text += emp.EMP_NAME + ",";
                    }
                }
                if (lbEmployeeXM.Text.Length > 0)
                    lbEmployeeXM.Text = lbEmployeeXM.Text.Substring(0, lbEmployeeXM.Text.Length - 1);

                ddlEmployeeGH.Visible = false;
                lbEmployeeGH.Visible = true;
                string[] empGHIds = Utils.CStrDef(item.EMP_GH).Split(',');
                lbEmployeeGH.Text = "";
                foreach (var GHId in empGHIds)
                {
                    EMPLOYER emp = _EmployerRepo.GetById(Utils.CIntDef(GHId));
                    if (emp != null)
                    {
                        lbEmployeeGH.Text += emp.EMP_NAME + ",";
                    }
                }
                if (lbEmployeeGH.Text.Length > 0)
                    lbEmployeeGH.Text = lbEmployeeGH.Text.Substring(0, lbEmployeeGH.Text.Length - 1);

                ddlCompany.Visible = false;
                lbCompany.Visible = true;
                string[] comIds = Utils.CStrDef(item.COMPANY).Split(',');
                lbCompany.Text = "";
                foreach (var comId in comIds)
                {
                    COMPANY com = _CompanyRepo.GetById(Utils.CIntDef(comId));
                    if (com != null)
                    {
                        lbCompany.Text += com.COM_NAME + ",";
                    }
                }
                if (lbCompany.Text.Length > 0)
                    lbCompany.Text = lbCompany.Text.Substring(0, lbCompany.Text.Length - 1);


                txtCus_gt.Text = item.CUS_GT;
                txtNote_deli.Text = item.CONT_NOTE_DELI;
                txtNoteXM.Text = item.CONT_NOTE_XM;
            }
            else
            {
                Drcustomer.Visible = true;
                lbcustomer.Visible = false;

                Rdtypecontract.Visible = true;
                lbtypecontract.Visible = false;

                txtToltalprice.Visible = true;
                lbToltalprice.Visible = false;

                txtPrepayprice.Visible = true;
                lbPrepayprice.Visible = false;

                pickdate_deli.Visible = true;
                lbpickdate_deli.Visible = false;

                txtdeli_price_pro.Visible = true;
                lbdeli_price_pro.Visible = false;

                txtBebtprice.Visible = true;
                lbBebtprice.Visible = false;

                txtPayprice_week.Visible = true;
                lbPayprice_week.Visible = false;

                txtweeKcount.Visible = true;
                lbweeKcount.Visible = false;

                trContractStatus.Visible = false;

                ddlEmployeeBH.Visible = true;
                lbEmployeeBH.Visible = false;

                ddlEmployeeXM.Visible = true;
                lbEmployeeXM.Visible = false;

                ddlEmployeeGH.Visible = true;
                lbEmployeeGH.Visible = false;

                ddlCompany.Visible = true;
                lbCompany.Visible = false;
            }
        }
        #endregion

        #region Employer
        //private string GetEmpBHIds()
        //{
        //    string EmpIds = "";
        //    for (int i = 0; i < chkEmployeeBH.Items.Count; i++)
        //    {
        //        if (chkEmployeeBH.Items[i].Selected)
        //            EmpIds += chkEmployeeBH.Items[i].Value + ",";
        //    }
        //    return EmpIds;
        //}
        //private string GetEmpXMIds()
        //{
        //    string EmpIds = "";
        //    for (int i = 0; i < chkEmployeeXM.Items.Count; i++)
        //    {
        //        if (chkEmployeeXM.Items[i].Selected)
        //            EmpIds += chkEmployeeXM.Items[i].Value + ",";
        //    }
        //    return EmpIds;
        //}
        //private string GetEmpGHIds()
        //{
        //    string EmpIds = "";
        //    for (int i = 0; i < chkEmployeeGH.Items.Count; i++)
        //    {
        //        if (chkEmployeeGH.Items[i].Selected)
        //            EmpIds += chkEmployeeGH.Items[i].Value + ",";
        //    }
        //    return EmpIds;
        //}
        //protected void btnApplyEmployeeBH_Click(object sender, EventArgs e)
        //{
        //    System.Collections.ArrayList arr = new System.Collections.ArrayList();
        //    for (int i = 0; i < chkEmployeeBH.Items.Count; i++)
        //    {
        //        if (chkEmployeeBH.Items[i].Selected)
        //        {
        //            arr.Add(chkEmployeeBH.Items[i].Text);
        //        }
        //    }
        //    if (arr.Count <= 0)
        //    {
        //        txtEmployeeBH.Text = "--- Chọn nhân viên bán hàng ---";
        //    }
        //    //else if (arr.Count == 1)
        //    //{
        //    //    txtEmployeeBH.Text = arr[0].ToString();
        //    //}
        //    //else if (arr.Count > 1)
        //    //{
        //    //    txtEmployeeBH.Text = arr[0].ToString() + ", v.v...";
        //    //}
        //    else
        //    {
        //        txtEmployeeBH.Text = "";
        //        for (int i = 0; i < arr.Count; i++)
        //        {
        //            if (i == arr.Count - 1)
        //            {
        //                txtEmployeeBH.Text += arr[i].ToString();
        //            }
        //            else
        //            {
        //                txtEmployeeBH.Text += arr[i].ToString() + ", ";
        //            }
        //        }                
        //    }
        //}
        //protected void btnCancelEmployeeBH_Click(object sender, EventArgs e)
        //{
        //    //txtEmployeeBH.Text = "--- Chọn nhân viên bán hàng ---";
        //    //for (int i = 0; i < chkEmployeeBH.Items.Count; i++)
        //    //{
        //    //    if (chkEmployeeBH.Items[i].Selected)
        //    //        chkEmployeeBH.Items[i].Selected = false;
        //    //}
        //    LoadEmployee();
        //    Showinfo();
        //}
        //protected void btnApplyEmployeeXM_Click(object sender, EventArgs e)
        //{
        //    System.Collections.ArrayList arr = new System.Collections.ArrayList();
        //    for (int i = 0; i < chkEmployeeXM.Items.Count; i++)
        //    {
        //        if (chkEmployeeXM.Items[i].Selected)
        //        {
        //            arr.Add(chkEmployeeXM.Items[i].Text);
        //        }
        //    }
        //    if (arr.Count <= 0)
        //    {
        //        txtEmployeeXM.Text = "--- Chọn nhân viên xác minh ---";
        //    }
        //    //else if (arr.Count == 1)
        //    //{
        //    //    txtEmployeeXM.Text = arr[0].ToString();
        //    //}
        //    //else if (arr.Count > 1)
        //    //{
        //    //    txtEmployeeXM.Text = arr[0].ToString() + ", v.v...";
        //    //}
        //    else
        //    {
        //        txtEmployeeXM.Text = "";
        //        for (int i = 0; i < arr.Count; i++)
        //        {
        //            if (i == arr.Count - 1)
        //            {
        //                txtEmployeeXM.Text += arr[i].ToString();
        //            }
        //            else
        //            {
        //                txtEmployeeXM.Text += arr[i].ToString() + ", ";
        //            }
        //        }
        //    }
        //}
        //protected void btnCancelEmployeeXM_Click(object sender, EventArgs e)
        //{
        //    //txtEmployeeXM.Text = "--- Chọn nhân viên xác minh ---";
        //    //for (int i = 0; i < chkEmployeeXM.Items.Count; i++)
        //    //{
        //    //    if (chkEmployeeXM.Items[i].Selected)
        //    //        chkEmployeeXM.Items[i].Selected = false;
        //    //}
        //    LoadEmployee();
        //    Showinfo();
        //}
        //protected void btnApplyEmployeeGH_Click(object sender, EventArgs e)
        //{
        //    System.Collections.ArrayList arr = new System.Collections.ArrayList();
        //    for (int i = 0; i < chkEmployeeGH.Items.Count; i++)
        //    {
        //        if (chkEmployeeGH.Items[i].Selected)
        //        {
        //            arr.Add(chkEmployeeGH.Items[i].Text);
        //        }
        //    }
        //    if (arr.Count <= 0)
        //    {
        //        txtEmployeeGH.Text = "--- Chọn nhân viên giao hàng ---";
        //    }
        //    //else if (arr.Count == 1)
        //    //{
        //    //    txtEmployeeGH.Text = arr[0].ToString();
        //    //}
        //    //else if (arr.Count > 1)
        //    //{
        //    //    txtEmployeeGH.Text = arr[0].ToString() + ", v.v...";
        //    //}
        //    else
        //    {
        //        txtEmployeeGH.Text = "";
        //        for (int i = 0; i < arr.Count; i++)
        //        {
        //            if (i == arr.Count - 1)
        //            {
        //                txtEmployeeGH.Text += arr[i].ToString();
        //            }
        //            else
        //            {
        //                txtEmployeeGH.Text += arr[i].ToString() + ", ";
        //            }
        //        }
        //    }
        //}
        //protected void btnCancelEmployeeGH_Click(object sender, EventArgs e)
        //{
        //    //txtEmployeeGH.Text = "--- Chọn nhân viên giao hàng ---";
        //    //for (int i = 0; i < chkEmployeeGH.Items.Count; i++)
        //    //{
        //    //    if (chkEmployeeGH.Items[i].Selected)
        //    //        chkEmployeeGH.Items[i].Selected = false;
        //    //}
        //    LoadEmployee();
        //    Showinfo();
        //}
        #endregion

        #region Function
        private void Savecontract(string strLink = "")
        {
            if (id != 0)
            {
                CONTRACT i = _ContractRepo.GetById(id);
                i.CONT_NO = txtNocontract.Text;
                i.ID_CUS = Utils.CIntDef(Drcustomer.SelectedValue);
                i.CONT_PRODUCT_NAME = txtNameproduct.Text;
                i.CONT_PRODUCT_PRICE = Utils.CDecDef(Utils.CStrDef(txtProductprice.Text).Replace(",", ""));
                //i.CONT_TOTAL_PRICE = Utils.CDecDef(txtToltalprice.Text);
                //i.CONT_PREPAY_PRICE = Utils.CDecDef(txtPrepayprice.Text);
                //i.CONT_DELI_DATE = pickdate_deli.returnDate;
                //i.CONT_DELI_PRICE = Utils.CDecDef(txtdeli_price_pro.Text);
                //i.CONT_DEBT_PRICE = Utils.CDecDef(txtBebtprice.Text);
                //i.CONT_TYPE = Utils.CIntDef(Rdtypecontract.SelectedValue);
                i.CONT_STATUS = Utils.CIntDef(Rdstatuscontract.SelectedValue);
                //i.CONT_WEEK_PRICE = Utils.CDecDef(txtPayprice_week.Text);
                //i.CONT_WEEK_COUNT = Utils.CIntDef(txtweeKcount.Text);
                i.CONT_NOTE = txtremarkcontract.Text;
                i.CONT_NOTE_TRACK = txtNoteTrack.Text;
                //i.EMP_BH = HiddenEmployeeBH.Value;
                //i.EMP_XM = HiddenEmployeeXM.Value;
                //i.EMP_GH = HiddenEmployeeGH.Value;
                i.CUS_GT = txtCus_gt.Text;
                i.CONT_NOTE_DELI = txtNote_deli.Text;
                i.CONT_NOTE_XM = txtNoteXM.Text;
                _ContractRepo.Update(i);

                strLink = string.IsNullOrEmpty(strLink) ? "chi-tiet-hop-dong.aspx?id=" + id : strLink;
            }
            else
            {
                CONTRACT i = new CONTRACT();
                i.CONT_NO = txtNocontract.Text;
                i.ID_CUS = Utils.CIntDef(Drcustomer.SelectedValue);
                i.CONT_PRODUCT_NAME = txtNameproduct.Text;
                i.CONT_PRODUCT_PRICE = Utils.CDecDef(Utils.CStrDef(txtProductprice.Text).Replace(",", ""));
                i.CONT_TOTAL_PRICE = Utils.CDecDef(Utils.CStrDef(txtToltalprice.Text).Replace(",", ""));
                i.CONT_PREPAY_PRICE = Utils.CDecDef(Utils.CStrDef(txtPrepayprice.Text).Replace(",", ""));
                i.CONT_DELI_DATE = pickdate_deli.returnDate;
                i.CONT_DELI_PRICE = Utils.CDecDef(Utils.CStrDef(txtdeli_price_pro.Text).Replace(",", ""));
                i.CONT_DEBT_PRICE = Utils.CDecDef(Utils.CStrDef(txtBebtprice.Text).Replace(",", ""));
                i.CONT_TYPE = Utils.CIntDef(Rdtypecontract.SelectedValue);
                i.CONT_STATUS = Utils.CIntDef(Rdstatuscontract.SelectedValue);
                i.CONT_WEEK_PRICE = Utils.CDecDef(Utils.CStrDef(txtPayprice_week.Text).Replace(",", ""));
                i.CONT_WEEK_COUNT = Utils.CIntDef(txtweeKcount.Text);
                i.CONT_NOTE = txtremarkcontract.Text;
                i.CONT_NOTE = txtremarkcontract.Text;
                i.CONT_NOTE_TRACK = txtNoteTrack.Text;
                i.EMP_BH = HiddenEmployeeBH.Value;
                i.EMP_XM = HiddenEmployeeXM.Value;
                i.EMP_GH = HiddenEmployeeGH.Value;
                i.COMPANY = HiddenCompany.Value;
                i.CUS_GT = txtCus_gt.Text;
                i.CONT_NOTE_DELI = txtNote_deli.Text;
                i.CONT_NOTE_XM = txtNoteXM.Text;
                i.USER_ID = Utils.CIntDef(Session["Userid"]);
                i.CONT_DATE = DateTime.Now;
                i.DATE_STATUS = DateTime.Now;
                _ContractRepo.Create(i);

                ////tạo details các kỳ thu dựa vào ngày giao hàng và loại hợp đồng
                //for (int j = 1; j <= i.CONT_WEEK_COUNT; j++)
                //{
                //    CONTRACT_DETAIL cd = new CONTRACT_DETAIL();
                //    cd.ID_CONT = i.ID;
                //    if (i.CONT_TYPE == 3)
                //    {
                //        cd.CONTD_DATE_THU = i.CONT_DELI_DATE.Value.AddMonths(j);
                //    }
                //    else if (i.CONT_TYPE == 2)
                //    {
                //        cd.CONTD_DATE_THU = i.CONT_DELI_DATE.Value.AddDays(j * 2 * 7);
                //    }
                //    else if (i.CONT_TYPE == 1)
                //    {
                //        cd.CONTD_DATE_THU = i.CONT_DELI_DATE.Value.AddDays(j * 7);
                //    }
                //    cd.CONTD_DEBT_PRICE = i.CONT_DEBT_PRICE - (i.CONT_WEEK_PRICE * j);
                //    _ContractDetailRepo.Create(cd);
                //}
                //tạo lịch sử chuyển đổi đầu tiên
                CONTRACT_HISTORY ch = new CONTRACT_HISTORY();
                ch.ID_CONT = i.ID;
                ch.CONTHIS_TYPE = i.CONT_TYPE;
                ch.CONTHIS_TRANSFER_DATE = DateTime.Now;
                ch.USER_ID = Utils.CIntDef(Session["Userid"]);
                _ContractHistoryRepo.Create(ch);

                strLink = string.IsNullOrEmpty(strLink) ? "chi-tiet-hop-dong.aspx?id=" + i.ID : strLink;
            }
            
            if (!string.IsNullOrEmpty(strLink))
            {
                Response.Redirect(strLink);
            }
        }
        private void Deletecontract()
        {

            var list = db.CONTRACTs.Where(n => n.ID == id).ToList();
            db.CONTRACTs.DeleteAllOnSubmit(list);
            db.SubmitChanges();
        }
        //Details
        private void getInfo_detail()
        {
            CONTRACT contract = _ContractRepo.GetById(id);
            CONTRACT_DETAIL contractdetail = _ContractDetailRepo.GetById(id_detail);
            if (contract !=null && contractdetail !=null)
            {
                //lbPaygop.Text = fm.FormatMoney(contract.CONT_WEEK_PRICE);
                //lbdate_thu.Text = contractdetail.CONTD_DATE_THU.Value.ToString("dd/MM/yyyy");
            }
        }
        private void load_list_detail()
        {
            var list = db.CONTRACT_DETAILs.Where(n => n.ID_CONT == id).OrderBy(n => n.CONTD_DATE_THU);//.OrderBy(n=>n.CONTD_DATE_THU_TT)
            HttpContext.Current.Session["dienmay.listctdetail"] = list;
            ASPxGridView_contractdetail.DataSource = list;
            ASPxGridView_contractdetail.DataBind();
            if(list.ToList().Count > 0)
            {
                trday1.Visible =true;
                trday2.Visible =true;
                trday3.Visible =true;
                trday4.Visible =true;
            }
        }
        private decimal getno()
        {
            var list = db.CONTRACTs.Where(n => n.ID == id).ToList();
            decimal getallprice =Utils.CDecDef(db.CONTRACT_DETAILs.Where(n => n.ID_CONT == id).Sum(n=>n.CONTD_PAY_PRICE));
            decimal getsono = Utils.CDecDef(list[0].CONT_DEBT_PRICE - getallprice);
            return getsono;
        }
        private decimal getNobefore(int _idbefore)
        {
            var list = db.CONTRACT_DETAILs.Where(n => n.ID < _idbefore).OrderByDescending(n => n.ID).Take(1).ToList();
            if (list.Count > 0)
                return Utils.CDecDef(list[0].CONTD_DEBT_PRICE);
            return 0;
        }
        private void getnoUpdateWhenDelete(List<int> listidchose)
        {
            var list = db.CONTRACTs.Where(n => n.ID == id).ToList();
            var updatenoWhendelete=db.CONTRACT_DETAILs.Where(n => n.ID_CONT == id && listidchose.Contains(n.ID));
            int _count = 0;
            foreach(var i in updatenoWhendelete)
            {
                if (_count == 0)
                    i.CONTD_DEBT_PRICE = Utils.CDecDef(list[0].CONT_DEBT_PRICE) - i.CONTD_PAY_PRICE;
                else
                    i.CONTD_DEBT_PRICE = getNobefore(i.ID) - i.CONTD_PAY_PRICE;
               db.SubmitChanges();
               _count++;
            }
        }
        private void updateNo(decimal dochenhlech,int type)
        {
            var list = db.CONTRACT_DETAILs.Where(n => n.ID_CONT == id&&n.ID>id_detail);
            foreach (var i in list)
            {
                if(type==0)
                i.CONTD_DEBT_PRICE = i.CONTD_DEBT_PRICE + dochenhlech;
                else i.CONTD_DEBT_PRICE = i.CONTD_DEBT_PRICE - dochenhlech;
                db.SubmitChanges();
            }
        }
        private void Save_details()
        {
            CONTRACT contract = _ContractRepo.GetById(id);
            List<object> fieldValues = ASPxGridView_contractdetail.GetSelectedFieldValues(new string[] { "ID" });
            foreach (var item in fieldValues)
            {
                CONTRACT_DETAIL contractdetail = _ContractDetailRepo.GetById(Utils.CIntDef(item));
                contractdetail.CONTD_DATE_THU_TT = pickdate_datethuTT.returnDate;
                decimal? price = null;
                if (Utils.CDecDef(Utils.CStrDef(txtPayprice.Text).Replace(",", "")) > 0)
                    price = Utils.CDecDef(Utils.CStrDef(txtPayprice.Text).Replace(",", ""));
                contractdetail.CONTD_PAY_PRICE = price;
                _ContractDetailRepo.Update(contractdetail);
            }

        }
        private void Save_detailsNoprice()
        {
            CONTRACT contract = _ContractRepo.GetById(id);
            List<object> fieldValues = ASPxGridView_contractdetail.GetSelectedFieldValues(new string[] { "ID" });
            foreach (var item in fieldValues)
            {
                CONTRACT_DETAIL contractdetail = _ContractDetailRepo.GetById(Utils.CIntDef(item));
                contractdetail.CONTD_DATE_THU_TT = null;
                contractdetail.CONTD_PAY_PRICE = null;
                _ContractDetailRepo.Update(contractdetail);
            }

        }
        //private void delete_Contractdetail()
        //{
        //    List<object> fieldValues = ASPxGridView_contractdetail.GetSelectedFieldValues(new string[] { "ID" });

        //    var list = db.CONTRACT_DETAILs.Where(n => fieldValues.Contains(n.ID));
        //    db.CONTRACT_DETAILs.DeleteAllOnSubmit(list);
        //    db.SubmitChanges();
        //    var listNodelete = db.CONTRACT_DETAILs.Where(n => !fieldValues.Contains(n.ID)).Select(n=>new{n.ID});
        //    List<int> listid = new List<int>();
        //    foreach (var i in listNodelete)
        //    {
        //        listid.Add(Utils.CIntDef(i.ID));
        //    }
        //    getnoUpdateWhenDelete(listid);
        //    load_list_detail();
        //}
        #endregion
        #region Phieu
        //private void showEmployer()
        //{
        //    var list = db.EMPLOYERs.OrderByDescending(n => n.ID);
        //    Dremployer.DataValueField = "ID";
        //    Dremployer.DataTextField = "EMP_NAME";
        //    Dremployer.DataSource = list;
        //    Dremployer.DataBind();
        //    ListItem l = new ListItem("--- Chọn nhân viên ---", "0");
        //    l.Selected = true;
        //    Dremployer.Items.Insert(0, l);
        //}
        //private void getInfo_phieu()
        //{
        //    var list = db.BILLs.Where(n => n.ID == id_bill).ToList();
        //    if (list.Count > 0)
        //    {
        //        txtdate_deli_phieu.returnDate = Utils.CDateDef(list[0].BILLL_RECEIVER_DATE, DateTime.Now);
        //        //Rdstatus_phieu.SelectedValue = list[0].BILL_STATUS.ToString();
        //        //txtdate_deli_phieu.Text = list[0].BILL_DELI_DATE;
        //        Dremployer.SelectedValue = list[0].ID_EMPLOY.ToString();
        //    }
        //}
        private void showListphieu()
        {
            var list = db.BILLs.Where(n => n.ID_CONT == id);
            HttpContext.Current.Session["dienmay.listphieu"] = list;
            ASPxGridView_phieu.DataSource = list;
            ASPxGridView_phieu.DataBind();
        }

        //private void add_phieu()
        //{
        //    BILL _bill = new BILL();
        //    _bill.ID_CONT = id;
        //    _bill.BILLL_RECEIVER_DATE = null;
        //    _bill.BILL_STATUS = null;
        //    _bill.BILL_DELI_DATE = txtdate_deli_phieu.returnDate;
        //    _bill.ID_EMPLOY = Utils.CIntDef(Dremployer.SelectedValue);
        //    db.BILLs.InsertOnSubmit(_bill);
        //    db.SubmitChanges();
        //    id_bill = _bill.ID;
        //}
        private void save_phieu()
        {
            List<object> fieldValues = ASPxGridView_phieu.GetSelectedFieldValues(new string[] { "ID" });
            foreach (var item in fieldValues)
            {
                int id = Utils.CIntDef(item);
                var i = db.BILLs.FirstOrDefault(n => n.ID == id);
                if (i != null)
                {
                    int index = ASPxGridView_phieu.FindVisibleIndexByKeyValue(id);
                    //TextBox txtdate_receiver_phieu = ASPxGridView_phieu.FindRowCellTemplateControl(index, (GridViewDataColumn)ASPxGridView_phieu.Columns["BILLL_RECEIVER_DATE"], "txtdate_receiver_phieu") as TextBox;
                    CheckBox chkStatusPhieu = ASPxGridView_phieu.FindRowCellTemplateControl(index, (GridViewDataColumn)ASPxGridView_phieu.Columns["BILL_STATUS"], "chkStatusPhieu") as CheckBox;

                    //DateTime? date = null; DateTime temp;
                    //if (DateTime.TryParseExact(txtdate_receiver_phieu.Text, "dd/MM/yyyy",
                    //                           System.Globalization.CultureInfo.InvariantCulture,
                    //                           System.Globalization.DateTimeStyles.None,
                    //                           out temp))
                    //{
                    //    date = temp;
                    //}

                    //i.BILLL_RECEIVER_DATE = date;
                    i.BILL_STATUS = chkStatusPhieu.Checked ? 1 : 0;
                    db.SubmitChanges();
                    
                }
            }
            showListphieu();

            
        }
        private void delete_phieu()
        {
            List<object> fieldValues = ASPxGridView_phieu.GetSelectedFieldValues(new string[] { "ID" });
            var list = db.BILLs.Where(n => fieldValues.Contains(n.ID));
            db.BILLs.DeleteAllOnSubmit(list);
            db.SubmitChanges();
            showListphieu();
        }
        //Function
        public int setOrder()
        {
            return _count++;
        }
        public int setOrder1()
        {
            return _count1++;
        }
        public string getNameemployer(object idem)
        {
            int id = Utils.CIntDef(idem);
            var list = db.EMPLOYERs.Where(n => n.ID == id).ToList();
            if (list.Count > 0) return list[0].EMP_NAME;
            return "";
        }
        public bool getStatusphieu(object sta)
        {
            switch (Utils.CIntDef(sta))
            {
                case 0: return false;
                case 1: return true;
            }
            return true;
        }
        public string getStatusphieuName(object sta)
        {
            if (string.IsNullOrEmpty(Utils.CStrDef(sta)))
            {
                return "";
            }
            switch (Utils.CIntDef(sta))
            {
                case 0: return "Phiếu rớt";
                case 1: return "Phiếu tốt";
            }
            return "";
        }
        public string getLinkphieu(object idphieu)
        {
            return "chi-tiet-hop-dong.aspx?id=" + id + "&idbill=" + idphieu;
        }
        #endregion
        #region History contract
        private void Loadlist_history()
        {
            var list = _ContractHistoryRepo.GetListByContractID(id);
            HttpContext.Current.Session["dienmay.listhistory"] = list;
            ASPxGridView_historyConvert.DataSource = list;
            ASPxGridView_historyConvert.DataBind();
        }
        private void Loadlist_historyweek()
        {
            var list = _ContractHistoryWeekRepo.GetListByContractID(id);
            HttpContext.Current.Session["dienmay.listhistoryWeek"] = list;
            ASPxGridViewDay.DataSource = list;
            ASPxGridViewDay.DataBind();
        }
        private int getTypeContract()
        {
            var list = db.CONTRACTs.Where(n => n.ID == id).ToList();
            if (list.Count > 0)
                return Utils.CIntDef(list[0].CONT_TYPE);
            return 0;
        }
        private void getInfoHistory()
        {
            Rdtypecont_convert.SelectedValue = getTypeContract().ToString();
            if (id_history > 0)
            {
                var list = db.CONTRACT_HISTORies.Where(n => n.ID == id_history).ToList();
                if (list.Count > 0)
                {
                    pickdateconvert.returnDate = Utils.CDateDef(list[0].CONTHIS_TRANSFER_DATE, DateTime.Now);
                    Rdtypecont_convert.SelectedValue = list[0].CONTHIS_TYPE.ToString();
                    
                }
            }
        }
        private void Save_history()
        {
            //check hợp lệ
            CONTRACT_DETAIL contractNextpay = _ContractDetailRepo.GetNextPayDateConveByContractId(id, pickdateconvert.returnDate);
            if (contractNextpay == null)
            {
                MessageBox1.ShowMessage("Ngày chuyển đổi không được lớn hơn danh sách các kỳ chưa thu!", "Thông báo");
                return;
            }
            var contractList = _ContractDetailRepo.GetListByContractId(id);
            if (contractList != null && contractList.Count > 0 && contractList[0].CONTD_DATE_THU.Value >= pickdateconvert.returnDate)
            {
                MessageBox1.ShowMessage("Ngày chuyển đổi phải lớn hơn ngày thu đầu tiên!", "Thông báo");
                return;
            }
            var contract = _ContractRepo.GetById(id);
            if (contract.BILL_STATUS == 1)
            {
                MessageBox1.ShowMessage("Hãy thực hiện nhận phiếu trước khi thực hiện chuyển đổi!", "Thông báo");
                return;
            }
            //check với loại hiên tại
            if (contract.CONT_TYPE == Utils.CIntDef(Rdtypecont_convert.SelectedValue))
            {
                MessageBox1.ShowMessage("Loại hợp đồng trùng với loại hợp đồng hiện tại!","Thông báo");
                return;
            }
            //thực hiện
            //cập nhật lại loại hợp đồng
            if (contract != null)
            {
                contract.CONT_TYPE = Utils.CIntDef(Rdtypecont_convert.SelectedValue);
                _ContractRepo.Update(contract);
            }
            //ghi lịch sử
            CONTRACT_HISTORY cthis = new CONTRACT_HISTORY();
            cthis.ID_CONT = id;
            cthis.CONTHIS_TYPE = Utils.CIntDef(Rdtypecont_convert.SelectedValue);
            cthis.CONTHIS_TRANSFER_DATE = pickdateconvert.returnDate;
            cthis.USER_ID = Utils.CIntDef(Session["Userid"]);
            _ContractHistoryRepo.Create(cthis);
            //cập nhật lại kỳ thu chưa góp so với ngày chuyển đổi
            DateTime dtlastpay = contract.CONT_DELI_DATE.Value;

            var contractLastpay = _ContractDetailRepo.GetLastPayByContractId(id, pickdateconvert.returnDate);//lấy ngày của kỳ trước đó
            if (contractLastpay != null)
            {
                dtlastpay = contractLastpay.CONTD_DATE_THU.Value;
            }
            var contractListpay = _ContractDetailRepo.GetListPayDateConveByContractId(id, pickdateconvert.returnDate);
            int j = 0;
            foreach (var item in contractListpay)
            {
                j++;
                if (cthis.CONTHIS_TYPE == 3)
                {
                    item.CONTD_DATE_THU = dtlastpay.AddMonths(j);
                }
                else if (cthis.CONTHIS_TYPE == 2)
                {
                    item.CONTD_DATE_THU = dtlastpay.AddDays(j * 2 * 7);
                }
                else if (cthis.CONTHIS_TYPE == 1)
                {
                    item.CONTD_DATE_THU = dtlastpay.AddDays(j * 7);
                }
                _ContractDetailRepo.Update(item);
            }

            Response.Redirect("chi-tiet-hop-dong.aspx?id=" + id + "&activetab=3");
        }
        private void Save_historyweek()
        {
            //check hợp lệ
            CONTRACT_DETAIL contractNextpay = _ContractDetailRepo.GetNextPayDateConveByContractId(id, pickconvertday.returnDate);
            if (contractNextpay == null)
            {
                MessageBox1.ShowMessage("Ngày chuyển đổi không được lớn hơn danh sách các kỳ chưa thu!", "Thông báo");
                return;
            }
            var contractList = _ContractDetailRepo.GetListByContractId(id);
            if (contractList != null && contractList.Count > 0 && contractList[0].CONTD_DATE_THU.Value >= pickconvertday.returnDate)
            {
                MessageBox1.ShowMessage("Ngày chuyển đổi phải lớn hơn ngày thu đầu tiên!", "Thông báo");
                return;
            }
            var contract = _ContractRepo.GetById(id);
            if (contract.BILL_STATUS == 1)
            {
                MessageBox1.ShowMessage("Hãy thực hiện nhận phiếu trước khi thực hiện chuyển đổi!", "Thông báo");
                return;
            }
            //check với thứ hiên tại
            DayOfWeek WCurrent = contractNextpay.CONTD_DATE_THU.Value.DayOfWeek;
            DayOfWeek WSelect = GetDayOfWeek(ddlContractDay.SelectedIndex);
            if (WCurrent == WSelect)
            {
                MessageBox1.ShowMessage("Thứ đi thu trùng với thứ hiện tại!", "Thông báo");
                return;
            }
            //thực hiện
            int op = OperationTwoWeek(WCurrent, WSelect);
            contractNextpay.CONTD_DATE_THU = contractNextpay.CONTD_DATE_THU.Value.AddDays(op);
            _ContractDetailRepo.Update(contractNextpay);
            //cập nhật lại kỳ thu chưa góp so với ngày chuyển đổi            
            var contractListpay = _ContractDetailRepo.GetListPayDateConveByContractId(id, pickconvertday.returnDate);            
            for (int i = 1; i < contractListpay.Count; i++)
			{
                if (contract.CONT_TYPE == 3)
                {
                    contractListpay[i].CONTD_DATE_THU = contractNextpay.CONTD_DATE_THU.Value.AddMonths(i);
                }
                else if (contract.CONT_TYPE == 2)
                {
                    contractListpay[i].CONTD_DATE_THU = contractNextpay.CONTD_DATE_THU.Value.AddDays(i * 2 * 7);
                }
                else if (contract.CONT_TYPE == 1)
                {
                    contractListpay[i].CONTD_DATE_THU = contractNextpay.CONTD_DATE_THU.Value.AddDays(i * 7);
                }
                _ContractDetailRepo.Update(contractListpay[i]);
                
            }

            //ghi lịch sử
            CONTRACT_HISTORYWEEK cthis = new CONTRACT_HISTORYWEEK();
            cthis.ID_CONT = id;
            cthis.CONTHIS_WEEK = Utils.CIntDef(ddlContractDay.SelectedIndex + 1);
            cthis.CONTHIS_TRANSFER_DATE = pickconvertday.returnDate;
            cthis.USER_ID = Utils.CIntDef(Session["Userid"]);
            _ContractHistoryWeekRepo.Create(cthis);
            Response.Redirect("chi-tiet-hop-dong.aspx?id=" + id + "&activetab=3");
        }
        private int getTypecontractLast()
        {
            var list = db.CONTRACT_HISTORies.OrderByDescending(n => n.ID).Take(1).ToList();
            if (list.Count > 0)
                return Utils.CIntDef(list[0].CONTHIS_TYPE);
            return 1;
        }
        private void Delete_history()
        {
            List<object> fieldValues = ASPxGridView_historyConvert.GetSelectedFieldValues(new string[] { "ID" });
            var list = db.CONTRACT_HISTORies.Where(n => fieldValues.Contains(n.ID));
            db.CONTRACT_HISTORies.DeleteAllOnSubmit(list);
            db.SubmitChanges();
            var updateTypecontract = db.CONTRACTs.Where(n => n.ID == id).ToList();
            if (updateTypecontract.Count > 0)
            {
                updateTypecontract[0].CONT_TYPE = getTypecontractLast();
                db.SubmitChanges();
            }
            Loadlist_history();
        }
        #region function
        public string getstatusContrachis(object sta)
        {
            switch (Utils.CIntDef(sta))
            {
                case 1: return "1 Tuần";
                case 2: return "2 Tuần";
                case 3: return "1 Tháng";
            }
            return "";
        }
        public string getstatusContrachisWeek(object sta)
        {
            switch (Utils.CIntDef(sta))
            {
                case 2:
                    return "Thứ 2";                    
                case 3:
                    return "Thứ 3";
                case 4:
                    return "Thứ 4";
                case 5:
                    return "Thứ 5";
                case 6:
                    return "Thứ 6";
                case 7:
                    return "Thứ 7";
                case 8:
                    return "Chủ nhật";
                default:
                    break;
            }
            return "";
        }
        
        public string getLinkConvert(object idhis)
        {
            return "chi-tiet-hop-dong.aspx?id=" + id + "&idhistory=" + idhis;
        }
        #endregion
        #endregion
        #region Button_event
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            Savecontract();
        }

        protected void lbtnSaveClose_Click(object sender, EventArgs e)
        {
            Savecontract("danh-sach-hop-dong.aspx");
        }

        protected void lbtnSaveNew_Click(object sender, EventArgs e)
        {
            Savecontract("chi-tiet-hop-dong.aspx");
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            Deletecontract();

        }

        protected void lbtnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("danh-sach-hop-dong.aspx");
        }
        #endregion
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Save_details();
            Response.Redirect("chi-tiet-hop-dong.aspx?id=" + id+"&activetab=2");
            
        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Save_detailsNoprice();
            Response.Redirect("chi-tiet-hop-dong.aspx?id=" + id + "&activetab=2");

        }
        #region function
        public string getDate(object date)
        {
            return string.Format("{0:dd/MM/yyyy}", date);
        }
        public DateTime getDateReceiver(object date)
        {
            return Convert.ToDateTime(date);
        }
        public string formatMoney(object money)
        {
            return fm.FormatMoney(money);
        }
        public string getConnoTT(object pageprice)
        {
            decimal price = Utils.CDecDef(pageprice);
            if(price > 0)
            {
                decimal tt = Utils.CDecDef(Session["CONT_DEBT_PRICE"]);
                decimal cl = tt - price;
                Session["CONT_DEBT_PRICE"] = cl;
                return fm.FormatMoney(cl);
            }
            return "";
        }
        public string getLink(object _iddet)
        {
            return "chi-tiet-hop-dong.aspx?id=" + id + "&iddetail=" + _iddet;
        }
        #endregion

        protected void Lbdelete_detail_Click(object sender, EventArgs e)
        {
            //delete_Contractdetail();
        }

        protected void lbaddphieu_Click(object sender, EventArgs e)
        {
            //add_phieu();
            Response.Redirect("chi-tiet-hop-dong.aspx?id=" + id + "&activetab=1");
            
        }

        protected void lbdelete_phieu_Click(object sender, EventArgs e)
        {
            delete_phieu();
            
        }
        protected void lbsavephieu_Click(object sender, EventArgs e)
        {
            save_phieu();

        }
        protected void lbaddConvert_Click(object sender, EventArgs e)
        {
            Save_history();
        }
        protected void lbDelteconvert_Click(object sender, EventArgs e)
        {
            Delete_history();
            
        }
        protected void lbConvertDay_Click(object sender, EventArgs e)
        {
            Save_historyweek();
        }


    }
}