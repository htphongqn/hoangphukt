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
            var list = _EmployerRepo.GetAll();
            ddlEmployer.DataValueField = "ID";
            ddlEmployer.DataTextField = "EMP_NAME";
            ddlEmployer.DataSource = list;
            ddlEmployer.DataBind();
            ListItem l = new ListItem("--- Chọn nhân viên ---", "0");
            l.Selected = true;
            ddlEmployer.Items.Insert(0, l);
        }
        private void LoadContract_Canphatphieu()
        {
            string keyword = txtKeyword.Value;
            var l = (from a in db.CONTRACTs
                     join b in db.CONTRACT_DETAILs on a.ID equals b.ID_CONT
                     where a.CONT_STATUS == Cost.HD_CONGOP //hợp đồng còn góp
                     && (a.BILL_STATUS != 1 || a.BILL_STATUS == null) //chưa giao phiếu
                     && (b.CONTD_PAY_PRICE == 0 || b.CONTD_PAY_PRICE == null)//lấy list chưa đóng tiền
                     && ((b.CONTD_DATE_THU.Value - DateTime.Parse(ddlContractDay.SelectedValue)).Days <= 0)  
                     && (b.CONTD_DATE_THU.Value.DayOfWeek == DateTime.Parse(ddlContractDay.SelectedValue).DayOfWeek)
                     && (a.CONT_NO.Contains(keyword) || keyword == null || keyword == "") 
                     select new
                     {                         
                         a.EMP_TN,
                         //b.ID,
                         b.ID_CONT,
                         a.CONT_DEBT_PRICE,
                         CONTD_DATE_THU = ddlContractDay.SelectedValue
                     }).Distinct().ToList();

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
            for (int i = 0; i < arr.Count; i++)
            {
                l.RemoveAt(arr[i]);
            }          

            ASPxGridView1_phatphieu.DataSource = l;
            ASPxGridView1_phatphieu.DataBind();
        }
        protected void lbtnPhatphieu_Click(object sender, EventArgs e)
        {
            List<object> fieldValues = ASPxGridView1_phatphieu.GetSelectedFieldValues(new string[] { "ID_CONT" });
            foreach (var item in fieldValues)
            {
                BILL b = new BILL();
                b.ID_CONT = Utils.CIntDef(item);
                b.ID_EMPLOY = Utils.CIntDef(ddlEmployer.SelectedValue);
                b.BILL_DELI_DATE = DateTime.Now;
                int index = ASPxGridView1_phatphieu.FindVisibleIndexByKeyValue(Utils.CIntDef(item));
                Label lbDatethu = ASPxGridView1_phatphieu.FindRowCellTemplateControl(index, (GridViewDataColumn)ASPxGridView1_phatphieu.Columns["CONTD_DATE_THU"], "lbDatethu") as Label;
                b.CONTD_DATE_THU = DateTime.ParseExact(lbDatethu.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                _BillRepo.Create(b);
                //cập nhật trạng thái phiếu
                CONTRACT contract = _ContractRepo.GetById(Utils.CIntDef(item));
                contract.BILL_STATUS = 1;
                contract.EMP_TN = Utils.CIntDef(ddlEmployer.SelectedValue);
                _ContractRepo.Update(contract);
            }

            LoadContract_Canphatphieu();
        }

        protected void lbtnPhatphieumacdinh_Click(object sender, EventArgs e)
        {
            var l = (from a in db.CONTRACTs
                     join b in db.CONTRACT_DETAILs on a.ID equals b.ID_CONT
                     where a.CONT_STATUS == Cost.HD_CONGOP //hợp đồng còn góp
                     && (a.BILL_STATUS != 1 || a.BILL_STATUS == null) //chưa giao phiếu
                         //&& (b.CONTD_PAY_PRICE == 0 || b.CONTD_PAY_PRICE == null)//lấy list chưa đóng tiền
                     && ((b.CONTD_DATE_THU.Value - DateTime.Parse(ddlContractDay.SelectedValue)).Days <= 0)
                     && (b.CONTD_DATE_THU.Value.DayOfWeek == DateTime.Parse(ddlContractDay.SelectedValue).DayOfWeek)

                     select new
                     {
                         a.EMP_TN,
                         //b.ID,
                         b.ID_CONT,
                         a.CONT_DEBT_PRICE,
                         CONTD_DATE_THU = ddlContractDay.SelectedValue
                     }).Distinct().ToList();

            foreach (var item in l)
            {
                var detail = _ContractDetailRepo.GetListByContractId(Utils.CIntDef(item.ID_CONT));
                decimal pricethu = 0;
                if (detail != null)
                {
                    pricethu = detail.Where(a => a.CONTD_PAY_PRICE != null).Sum(a => a.CONTD_PAY_PRICE.Value);
                    if (item.CONT_DEBT_PRICE <= pricethu)
                    {
                        l.Remove(item);
                    }
                }
            }
            foreach (var item in l)
            {
                if (Utils.CIntDef(item.EMP_TN) > 0)
                {
                    BILL b = new BILL();
                    b.ID_CONT = Utils.CIntDef(item.ID_CONT);
                    b.ID_EMPLOY = Utils.CIntDef(item.EMP_TN);
                    b.BILL_DELI_DATE = DateTime.Now;
                    int index = ASPxGridView1_phatphieu.FindVisibleIndexByKeyValue(Utils.CIntDef(item.ID_CONT));
                    Label lbDatethu = ASPxGridView1_phatphieu.FindRowCellTemplateControl(index, (GridViewDataColumn)ASPxGridView1_phatphieu.Columns["CONTD_DATE_THU"], "lbDatethu") as Label;
                    b.CONTD_DATE_THU = DateTime.ParseExact(lbDatethu.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
               
                    _BillRepo.Create(b);
                    //cập nhật trạng thái phiếu
                    CONTRACT contract = _ContractRepo.GetById(Utils.CIntDef(item.ID_CONT));
                    contract.BILL_STATUS = 1;
                    contract.EMP_TN = Utils.CIntDef(item.EMP_TN);
                    _ContractRepo.Update(contract);
                }
            }

            LoadContract_Canphatphieu();

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
            var contractdetail =db.CONTRACT_DETAILs.Where(a => a.ID_CONT == idc && (a.CONTD_DATE_THU.Value-DateTime.Today).Days > 0).ToList();
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
                var detail = db.CONTRACT_DETAILs.Where(a => a.ID_CONT == idc).OrderByDescending(a => a.ID).Take(1);
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