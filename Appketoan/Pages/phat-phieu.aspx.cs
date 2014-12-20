using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Appketoan.Data;
using vpro.functions;
using Appketoan.Components;
using DevExpress.Web.ASPxGridView;

namespace Appketoan.Pages
{
    public partial class phat_phieu : System.Web.UI.Page
    {
        #region Declare
        private AppketoanDataContext db = new AppketoanDataContext();
        private EmployerRepo _EmployerRepo = new EmployerRepo();
        private ContractRepo _ContractRepo = new ContractRepo();
        private ContractDetailRepo _ContractDetailRepo = new ContractDetailRepo();
        private CustomerRepo _CustomerRepo = new CustomerRepo();
        private BillRepo _BillRepo = new BillRepo();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pickdate_deli.returnDate = DateTime.Now;
                showEmployer();
                loadDateInWeek();
                LoadContract_Canphatphieu();                
            }
        }
        private void loadDateInWeek()
        {
            switch (DateTime.Today.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    ddlContractDay.Items.Add(new ListItem("Thứ hai", DateTime.Today.AddDays(0).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Thứ ba", DateTime.Today.AddDays(1).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Thứ tư", DateTime.Today.AddDays(2).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Thứ năm", DateTime.Today.AddDays(3).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Thứ sáu", DateTime.Today.AddDays(4).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Thứ bảy", DateTime.Today.AddDays(5).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Chủ nhật", DateTime.Today.AddDays(6).ToString("yyyy/MM/dd")));
                    ddlContractDay.SelectedValue = DateTime.Today.AddDays(0).ToString("yyyy/MM/dd");
                    break;
                case DayOfWeek.Tuesday:
                    ddlContractDay.Items.Add(new ListItem("Thứ hai", DateTime.Today.AddDays(-1).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Thứ ba", DateTime.Today.AddDays(0).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Thứ tư", DateTime.Today.AddDays(1).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Thứ năm", DateTime.Today.AddDays(2).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Thứ sáu", DateTime.Today.AddDays(3).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Thứ bảy", DateTime.Today.AddDays(4).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Chủ nhật", DateTime.Today.AddDays(5).ToString("yyyy/MM/dd")));
                    ddlContractDay.SelectedValue = DateTime.Today.AddDays(0).ToString("yyyy/MM/dd");
                    break;
                case DayOfWeek.Wednesday:
                    ddlContractDay.Items.Add(new ListItem("Thứ hai", DateTime.Today.AddDays(-2).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Thứ ba", DateTime.Today.AddDays(-1).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Thứ tư", DateTime.Today.AddDays(0).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Thứ năm", DateTime.Today.AddDays(1).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Thứ sáu", DateTime.Today.AddDays(2).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Thứ bảy", DateTime.Today.AddDays(3).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Chủ nhật", DateTime.Today.AddDays(4).ToString("yyyy/MM/dd")));
                    ddlContractDay.SelectedValue = DateTime.Today.AddDays(0).ToString("yyyy/MM/dd");
                    break;
                case DayOfWeek.Thursday:
                    ddlContractDay.Items.Add(new ListItem("Thứ hai", DateTime.Today.AddDays(-3).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Thứ ba", DateTime.Today.AddDays(-2).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Thứ tư", DateTime.Today.AddDays(-1).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Thứ năm", DateTime.Today.AddDays(0).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Thứ sáu", DateTime.Today.AddDays(1).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Thứ bảy", DateTime.Today.AddDays(2).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Chủ nhật", DateTime.Today.AddDays(3).ToString("yyyy/MM/dd")));
                    ddlContractDay.SelectedValue = DateTime.Today.AddDays(0).ToString("yyyy/MM/dd");
                    break;
                case DayOfWeek.Friday:
                    ddlContractDay.Items.Add(new ListItem("Thứ hai", DateTime.Today.AddDays(-4).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Thứ ba", DateTime.Today.AddDays(-3).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Thứ tư", DateTime.Today.AddDays(-2).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Thứ năm", DateTime.Today.AddDays(-1).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Thứ sáu", DateTime.Today.AddDays(0).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Thứ bảy", DateTime.Today.AddDays(1).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Chủ nhật", DateTime.Today.AddDays(2).ToString("yyyy/MM/dd")));
                    ddlContractDay.SelectedValue = DateTime.Today.AddDays(0).ToString("yyyy/MM/dd");
                    break;
                case DayOfWeek.Saturday:
                    ddlContractDay.Items.Add(new ListItem("Thứ hai", DateTime.Today.AddDays(-5).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Thứ ba", DateTime.Today.AddDays(-4).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Thứ tư", DateTime.Today.AddDays(-3).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Thứ năm", DateTime.Today.AddDays(-2).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Thứ sáu", DateTime.Today.AddDays(-1).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Thứ bảy", DateTime.Today.AddDays(0).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Chủ nhật", DateTime.Today.AddDays(1).ToString("yyyy/MM/dd")));
                    ddlContractDay.SelectedValue = DateTime.Today.AddDays(0).ToString("yyyy/MM/dd");
                    break;
                case DayOfWeek.Sunday:
                    ddlContractDay.Items.Add(new ListItem("Thứ hai", DateTime.Today.AddDays(-6).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Thứ ba", DateTime.Today.AddDays(-5).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Thứ tư", DateTime.Today.AddDays(-4).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Thứ năm", DateTime.Today.AddDays(-3).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Thứ sáu", DateTime.Today.AddDays(-2).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Thứ bảy", DateTime.Today.AddDays(-1).ToString("yyyy/MM/dd")));
                    ddlContractDay.Items.Add(new ListItem("Chủ nhật", DateTime.Today.AddDays(0).ToString("yyyy/MM/dd")));
                    ddlContractDay.SelectedValue = DateTime.Today.AddDays(0).ToString("yyyy/MM/dd");
                    break;
                default:
                    break;
            }
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            LoadContract_Canphatphieu();
        }
        private void showEmployer()
        {
            var list = _EmployerRepo.GetListCtySortName();
            ddlEmployer.DataValueField = "ID";
            ddlEmployer.DataTextField = "EMP_NAME";
            ddlEmployer.DataSource = list;
            ddlEmployer.DataBind();
            ListItem l = new ListItem("--- Chọn nhân viên ---", "0");
            l.Selected = true;
            ddlEmployer.Items.Insert(0, l);

            ddlEmployerSearch.DataValueField = "ID";
            ddlEmployerSearch.DataTextField = "EMP_NAME";
            ddlEmployerSearch.DataSource = list;
            ddlEmployerSearch.DataBind();
            ListItem ls = new ListItem("--- Chọn nhân viên ---", "0");
            ls.Selected = true;
            ddlEmployerSearch.Items.Insert(0, ls);
        }
        private void LoadContract_Canphatphieu()
        {
            string keyword = txtKeyword.Value;
            int EmpTNId = Utils.CIntDef(ddlEmployerSearch.SelectedValue);
            var l = (from a in db.CONTRACTs
                     join b in db.CONTRACT_DETAILs on a.ID equals b.ID_CONT
                     where a.CONT_STATUS == Cost.HD_CONGOP //hợp đồng còn góp
                     && (a.BILL_STATUS != 1 || a.BILL_STATUS == null) //chưa giao phiếu
                     && (b.CONTD_PAY_PRICE == 0 || b.CONTD_PAY_PRICE == null)//lấy list chưa đóng tiền
                     && (a.IS_DELETE == false || a.IS_DELETE == null)
                     && a.EMP_TN != null
                     && b.CONTD_DATE_THU != null
                     && ((b.CONTD_DATE_THU.Value - DateTime.Parse(ddlContractDay.SelectedValue)).Days <= 0)  
                     && (b.CONTD_DATE_THU.Value.DayOfWeek == DateTime.Parse(ddlContractDay.SelectedValue).DayOfWeek)
                     && (a.CONT_NO.Contains(keyword) || keyword == null || keyword == "")
                     && (a.EMP_TN == EmpTNId || EmpTNId == 0)
                     select new
                     {                         
                         a.EMP_TN,
                         //b.ID,
                         b.ID_CONT,
                         a.CONT_DEBT_PRICE,
                         CONTD_DATE_THU = ddlContractDay.SelectedValue
                     }).Distinct().OrderByDescending(n=>n.ID_CONT).ToList();

            int z = 0; List<int> arr = new List<int>();
            foreach (var item in l)
            {
                var detail = _ContractDetailRepo.GetListByContractId(Utils.CIntDef(item.ID_CONT));
                decimal pricethu = 0;
                if (detail != null)
                {
                    pricethu = detail.Where(a=>a.CONTD_PAY_PRICE != null).Sum(a => a.CONTD_PAY_PRICE.Value);
                    if (item.CONT_DEBT_PRICE <= pricethu)
                    {
                        //l.Remove(item);
                        arr.Add(z);

                    }
                }
                z++;
            }
            for (int i = arr.Count- 1; i >= 0; i--)
            {
                l.RemoveAt(arr[i]);
            }

            ASPxGridView1_phatphieu.DataSource = l;
            ASPxGridView1_phatphieu.DataBind();
        }
        protected void lbtnPhatphieu_Click(object sender, EventArgs e)
        {
            try
            {
                List<object> fieldValues = ASPxGridView1_phatphieu.GetSelectedFieldValues(new string[] { "ID_CONT" });
                foreach (var item in fieldValues)
                {
                    BILL b = new BILL();
                    b.ID_CONT = Utils.CIntDef(item);
                    b.ID_EMPLOY = Utils.CIntDef(ddlEmployer.SelectedValue);
                    b.BILL_DELI_DATE = pickdate_deli.returnDate;
                    int index = ASPxGridView1_phatphieu.FindVisibleIndexByKeyValue(Utils.CIntDef(item));
                    Label lbDatethu = ASPxGridView1_phatphieu.FindRowCellTemplateControl(index, (GridViewDataColumn)ASPxGridView1_phatphieu.Columns["CONTD_DATE_THU"], "lbDatethu") as Label;
                    if (lbDatethu != null)
                    {
                        b.CONTD_DATE_THU = DateTime.ParseExact(lbDatethu.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        _BillRepo.Create(b);
                        //cập nhật trạng thái phiếu
                        CONTRACT contract = _ContractRepo.GetById(Utils.CIntDef(item));
                        contract.BILL_STATUS = 1;
                        contract.EMP_TN = Utils.CIntDef(ddlEmployer.SelectedValue);
                        _ContractRepo.Update(contract);
                    }
                }
            }
            catch
            {


            }
            //LoadContract_Canphatphieu();
            Response.Redirect("~/Pages/phat-phieu.aspx");
        }

        protected void lbtnPhatphieudachon_Click(object sender, EventArgs e)
        {
            try
            {
                List<object> fieldValues = ASPxGridView1_phatphieu.GetSelectedFieldValues(new string[] { "ID_CONT" });
                foreach (var item in fieldValues)
                {
                    int index = ASPxGridView1_phatphieu.FindVisibleIndexByKeyValue(Utils.CIntDef(item));
                    HiddenField hddEmp_TN = ASPxGridView1_phatphieu.FindRowCellTemplateControl(index, (GridViewDataColumn)ASPxGridView1_phatphieu.Columns["EMP_TN"], "hddEmp_TN") as HiddenField;
                    if (Utils.CIntDef(hddEmp_TN.Value) > 0)
                    {
                        BILL b = new BILL();
                        b.ID_CONT = Utils.CIntDef(item);
                        b.ID_EMPLOY = Utils.CIntDef(hddEmp_TN.Value);
                        b.BILL_DELI_DATE = pickdate_deli.returnDate;
                        Label lbDatethu = ASPxGridView1_phatphieu.FindRowCellTemplateControl(index, (GridViewDataColumn)ASPxGridView1_phatphieu.Columns["CONTD_DATE_THU"], "lbDatethu") as Label;
                        if (lbDatethu != null)
                        {
                            b.CONTD_DATE_THU = DateTime.ParseExact(lbDatethu.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            _BillRepo.Create(b);
                            //cập nhật trạng thái phiếu
                            CONTRACT contract = _ContractRepo.GetById(Utils.CIntDef(item));
                            contract.BILL_STATUS = 1;
                            contract.EMP_TN = Utils.CIntDef(hddEmp_TN.Value);
                            _ContractRepo.Update(contract);
                        }
                    }
                }
            }
            catch
            {

            }
            //LoadContract_Canphatphieu();
            Response.Redirect("~/Pages/phat-phieu.aspx");
        }
        protected void lbtnPhatphieumacdinh_Click(object sender, EventArgs e)
        {
            #region a
            //string keyword = txtKeyword.Value;
            //var l = (from a in db.CONTRACTs
            //         join b in db.CONTRACT_DETAILs on a.ID equals b.ID_CONT
            //         where a.CONT_STATUS == Cost.HD_CONGOP //hợp đồng còn góp
            //         && (a.BILL_STATUS != 1 || a.BILL_STATUS == null) //chưa giao phiếu
            //         && (b.CONTD_PAY_PRICE == 0 || b.CONTD_PAY_PRICE == null)//lấy list chưa đóng tiền
            //         && ((b.CONTD_DATE_THU.Value - DateTime.Parse(ddlContractDay.SelectedValue)).Days <= 0)
            //         && (b.CONTD_DATE_THU.Value.DayOfWeek == DateTime.Parse(ddlContractDay.SelectedValue).DayOfWeek)
            //         && (a.CONT_NO.Contains(keyword) || keyword == null || keyword == "")
            //         select new
            //         {
            //             a.EMP_TN,
            //             //b.ID,
            //             b.ID_CONT,
            //             a.CONT_DEBT_PRICE,
            //             CONTD_DATE_THU = ddlContractDay.SelectedValue
            //         }).Distinct().OrderByDescending(n => n.ID_CONT).ToList();

            //int z = 0; List<int> arr = new List<int>();
            //foreach (var item in l)
            //{
            //    var detail = _ContractDetailRepo.GetListByContractId(Utils.CIntDef(item.ID_CONT));
            //    decimal pricethu = 0;
            //    if (detail != null)
            //    {
            //        pricethu = detail.Where(a => a.CONTD_PAY_PRICE != null).Sum(a => a.CONTD_PAY_PRICE.Value);
            //        if (item.CONT_DEBT_PRICE <= pricethu)
            //        {
            //            //l.Remove(item);
            //            arr.Add(z);
            //        }
            //    }
            //    z++;
            //}
            //for (int i = 0; i < arr.Count; i++)
            //{
            //    l.RemoveAt(arr[i]);
            //}   
            //foreach (var item in l)
            //{
            //    if (Utils.CIntDef(item.EMP_TN) > 0)
            //    {
            //        BILL b = new BILL();
            //        b.ID_CONT = Utils.CIntDef(item.ID_CONT);
            //        b.ID_EMPLOY = Utils.CIntDef(item.EMP_TN);
            //        b.BILL_DELI_DATE = DateTime.Now;
            //        int index = ASPxGridView1_phatphieu.FindVisibleIndexByKeyValue(Utils.CIntDef(item.ID_CONT));
            //        Label lbDatethu = ASPxGridView1_phatphieu.FindRowCellTemplateControl(index, (GridViewDataColumn)ASPxGridView1_phatphieu.Columns["CONTD_DATE_THU"], "lbDatethu") as Label;
            //        if (lbDatethu != null)
            //        {
            //            b.CONTD_DATE_THU = DateTime.ParseExact(lbDatethu.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            //            _BillRepo.Create(b);
            //            //cập nhật trạng thái phiếu
            //            CONTRACT contract = _ContractRepo.GetById(Utils.CIntDef(item.ID_CONT));
            //            contract.BILL_STATUS = 1;
            //            contract.EMP_TN = Utils.CIntDef(item.EMP_TN);
            //            _ContractRepo.Update(contract);
            //        }
            //    }
            //}
            #endregion
            try
            {
                ASPxGridView1_phatphieu.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                LoadContract_Canphatphieu();

                for (int i = 0; i < ASPxGridView1_phatphieu.VisibleRowCount; i++)
                {
                    HiddenField hddEmp_TN = ASPxGridView1_phatphieu.FindRowCellTemplateControl(i, (GridViewDataColumn)ASPxGridView1_phatphieu.Columns["EMP_TN"], "hddEmp_TN") as HiddenField;
                    HiddenField hddConID = ASPxGridView1_phatphieu.FindRowCellTemplateControl(i, (GridViewDataColumn)ASPxGridView1_phatphieu.Columns["EMP_TN"], "hddConID") as HiddenField;
                    if (Utils.CIntDef(hddEmp_TN.Value) > 0 && Utils.CIntDef(hddConID.Value) > 0)
                    {
                        BILL b = new BILL();
                        b.ID_CONT = Utils.CIntDef(hddConID.Value);
                        b.ID_EMPLOY = Utils.CIntDef(hddEmp_TN.Value);
                        b.BILL_DELI_DATE = pickdate_deli.returnDate;
                        //int index = ASPxGridView1_phatphieu.FindVisibleIndexByKeyValue(Utils.CIntDef(item.ID_CONT));
                        Label lbDatethu = ASPxGridView1_phatphieu.FindRowCellTemplateControl(i, (GridViewDataColumn)ASPxGridView1_phatphieu.Columns["CONTD_DATE_THU"], "lbDatethu") as Label;
                        if (lbDatethu != null)
                        {
                            b.CONTD_DATE_THU = DateTime.ParseExact(lbDatethu.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                            _BillRepo.Create(b);
                            //cập nhật trạng thái phiếu
                            CONTRACT contract = _ContractRepo.GetById(Utils.CIntDef(hddConID.Value));
                            contract.BILL_STATUS = 1;
                            contract.EMP_TN = Utils.CIntDef(hddEmp_TN.Value);
                            _ContractRepo.Update(contract);
                        }
                    }
                }
                ASPxGridView1_phatphieu.SettingsPager.Mode = GridViewPagerMode.ShowPager;
                //LoadContract_Canphatphieu();

            }
            catch
            {

            }
            Response.Redirect("~/Pages/phat-phieu.aspx");

        }

        public string getLink(object id)
        {
            return "chi-tiet-hop-dong.aspx?id=" + id;
        }
        public string getConNo(object id)
        {
            int _id = Utils.CIntDef(id);
            var contract = _ContractRepo.GetById(_id);
            if (contract != null)
            {
                return contract.CONT_NO;
            }
            return "";
        }
        public string getLinkCus(object id)
        {
            int _id = Utils.CIntDef(id);
            var contract = _ContractRepo.GetById(_id);
            if (contract != null)
            {
                return "chi-tiet-khach-hang.aspx?id=" + contract.ID_CUS;
            }
            return "";
            
        }
        public string getnameCus(object id)
        {
            int _id = Utils.CIntDef(id);
            var contract = _ContractRepo.GetById(_id);
            if (contract != null)
            {
                var cus = _CustomerRepo.GetById(Utils.CIntDef(contract.ID_CUS));
                if (cus != null)
                {
                    return cus.CUS_FULLNAME;
                }
            }
            return "";
        }
        public string getaddressCus(object id)
        {
            int _id = Utils.CIntDef(id);
            var contract = _ContractRepo.GetById(_id);
            if (contract != null)
            {
                var cus = _CustomerRepo.GetById(Utils.CIntDef(contract.ID_CUS));
                if (cus != null)
                {
                    return cus.CUS_ADDRESS;
                }
            }
            return "";
        }
        public string getnameEmpTN(object id)
        {
            int _id = Utils.CIntDef(id);
            var emp = _EmployerRepo.GetById(_id);
            if (emp != null)
            {
                return emp.EMP_NAME;
             }
            return "";
        }
        public string getDateThu(object id, object ngaythu)
        {
            int idc = Utils.CIntDef(id);
            var contractdetail =db.CONTRACT_DETAILs.Where(a => a.ID_CONT == idc && a.CONTD_DATE_THU != null && (a.CONTD_DATE_THU.Value-DateTime.Today).Days > 0).ToList();
            if (contractdetail != null && contractdetail.Count > 0)
            {
                //var detail = db.CONTRACT_DETAILs.Where(a => a.ID_CONT == idc).OrderBy(a => a.ID).Take(1).ToList();
                //if (detail != null && detail.ToList().Count > 0 && (detail[0].CONTD_DATE_THU.Value - DateTime.Parse(Utils.CStrDef(ngaythu))).Days >= 0)
                //{
                //    return detail[0].CONTD_DATE_THU.Value.ToString("dd/MM/yyyy");
                //}
                return DateTime.Parse(Utils.CStrDef(ngaythu)).ToString("dd/MM/yyyy");
            }
            else
            {
                var detail = db.CONTRACT_DETAILs.Where(a => a.ID_CONT == idc && a.CONTD_DATE_THU != null).OrderByDescending(a => a.ID).Take(1);
                if (detail != null && detail.ToList().Count > 0)
                {
                    return detail.ToList()[0].CONTD_DATE_THU.Value.ToString("dd/MM/yyyy");
                }
            }
            return "";
        }
        protected void ASPxGridView1_phatphieu_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            LoadContract_Canphatphieu();
        }

        protected void ASPxGridView1_phatphieu_PageIndexChanged(object sender, EventArgs e)
        {
            LoadContract_Canphatphieu();
        }


    }
}