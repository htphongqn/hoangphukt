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
    public partial class phat_phieu_tu_do : System.Web.UI.Page
    {
        #region Declare
        private AppketoanDataContext db = new AppketoanDataContext();
        private EmployerRepo _EmployerRepo = new EmployerRepo();
        private ContractRepo _ContractRepo = new ContractRepo();
        private ContractDetailRepo _ContractDetailRepo = new ContractDetailRepo();
        private ContractHistoryWeekRepo _ContractHistoryWeekRepo = new ContractHistoryWeekRepo();
        private CustomerRepo _CustomerRepo = new CustomerRepo();
        private BillRepo _BillRepo = new BillRepo();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pickdate_deli.returnDate = DateTime.Now;
                showEmployer();
                LoadLandau();                
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
        private void LoadLandau()
        {
            var l = (from a in db.CONTRACTs
                     where a.CONT_STATUS == Cost.HD_CONGOP //hợp đồng còn góp
                     && (a.BILL_STATUS != 1 || a.BILL_STATUS == null) //chưa giao phiếu
                     && (a.EMP_TN == null)
                     && (a.IS_DELETE == false || a.IS_DELETE == null)
                     select new
                     {
                         ID_CONT = a.ID,
                         a.CONT_NO,
                         a.EMP_TN,
                         a.CONT_DEBT_PRICE,
                         CONTD_DATE_THU = DateTime.Today
                     }).OrderByDescending(n => n.CONT_NO).ToList();
            Session["listtudo"] = l;
            ASPxGridView1_phatphieu.DataSource = l;
            ASPxGridView1_phatphieu.DataBind();
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
                     && (a.CONT_NO.Contains(keyword) || keyword == null || keyword == "")
                     && (a.EMP_TN == EmpTNId || EmpTNId == 0)
                     && (a.IS_DELETE == false || a.IS_DELETE == null)
                     select new
                     {                         
                         a.EMP_TN,
                         b.ID_CONT,
                         a.CONT_DEBT_PRICE,
                         CONTD_DATE_THU = DateTime.Today
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
            Session["listtudo"] = l;
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
                                                
                        //cập nhật trạng thái phiếu
                        CONTRACT contract = _ContractRepo.GetById(Utils.CIntDef(item));
                        if (contract.EMP_TN == null)
                        {
                            b.CONTD_DATE_THU = pickdate_deli.returnDate;
                            //tạo details các kỳ thu dựa vào ngày hom nay(ngay giao lan dau) và loại hợp đồng
                            for (int j = 0; j < contract.CONT_WEEK_COUNT; j++)
                            {
                                CONTRACT_DETAIL cd = new CONTRACT_DETAIL();
                                cd.ID_CONT = contract.ID;
                                if (contract.CONT_TYPE == 3)
                                {
                                    cd.CONTD_DATE_THU = b.CONTD_DATE_THU.Value.AddMonths(j);
                                }
                                else if (contract.CONT_TYPE == 2)
                                {
                                    cd.CONTD_DATE_THU = b.CONTD_DATE_THU.Value.AddDays(j * 2 * 7);
                                }
                                else if (contract.CONT_TYPE == 1)
                                {
                                    cd.CONTD_DATE_THU = b.CONTD_DATE_THU.Value.AddDays(j * 7);
                                }                               
                                _ContractDetailRepo.Create(cd);
                            }
                            //ghi lịch sử
                            CONTRACT_HISTORYWEEK cthis = new CONTRACT_HISTORYWEEK();
                            cthis.ID_CONT = contract.ID;
                            cthis.CONTHIS_WEEK = gettypeContrachisWeek(b.CONTD_DATE_THU.Value.DayOfWeek);
                            cthis.CONTHIS_TRANSFER_DATE = pickdate_deli.returnDate;
                            cthis.USER_ID = Utils.CIntDef(Session["Userid"]);
                            _ContractHistoryWeekRepo.Create(cthis);
                        }
                        _BillRepo.Create(b);

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
            Response.Redirect("~/Pages/phat-phieu-tu-do.aspx");
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
            Response.Redirect("~/Pages/phat-phieu-tu-do.aspx");
        }
        public int gettypeContrachisWeek(DayOfWeek sta)
        {
            switch (sta)
            {
                case DayOfWeek.Monday:
                    return 2;
                case DayOfWeek.Tuesday:
                    return 3;
                case DayOfWeek.Wednesday:
                    return 4;
                case DayOfWeek.Thursday:
                    return 5;
                case DayOfWeek.Friday:
                    return 6;
                case DayOfWeek.Saturday:
                    return 7;
                case DayOfWeek.Sunday:
                    return 8;
                default:
                    return 2;
            }
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
            var contractdetail =db.CONTRACT_DETAILs.Where(a => a.ID_CONT == idc && a.CONTD_DATE_THU != null && (a.CONTD_DATE_THU.Value-DateTime.Today).Days > 0).ToList();//nếu ko bị trể
            if (contractdetail != null && contractdetail.Count > 0)
            {
                var contractdetailPre = db.CONTRACT_DETAILs.Where(a => a.ID_CONT == idc && a.CONTD_DATE_THU != null && (a.CONTD_DATE_THU.Value - DateTime.Today).Days <= 0).OrderByDescending(n=>n.CONTD_DATE_THU).ToList();
                if (contractdetailPre != null && contractdetailPre.Count > 0)
                {
                    return DateTime.Parse(Utils.CStrDef(contractdetailPre[0].CONTD_DATE_THU)).ToString("dd/MM/yyyy");//? lấy kỳ trước hiện tại hay kỳ sau đó=>lay gần và nhỏ với ngày htai 
                }
            }
            else
            {
                var detail = db.CONTRACT_DETAILs.Where(a => a.ID_CONT == idc && a.CONTD_DATE_THU != null).OrderByDescending(a => a.CONTD_DATE_THU).Take(1);//trể, lấy ngày thu cuối
                if (detail != null && detail.ToList().Count > 0)
                {
                    return detail.ToList()[0].CONTD_DATE_THU.Value.ToString("dd/MM/yyyy");
                }
            }
            return DateTime.Now.ToString("dd/MM/yyyy");
        }
        protected void ASPxGridView1_phatphieu_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            ASPxGridView1_phatphieu.DataSource = Session["listtudo"];
            ASPxGridView1_phatphieu.DataBind();
        }

        protected void ASPxGridView1_phatphieu_PageIndexChanged(object sender, EventArgs e)
        {
            ASPxGridView1_phatphieu.DataSource = Session["listtudo"];
            ASPxGridView1_phatphieu.DataBind();
        }


    }
}