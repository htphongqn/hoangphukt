using System;
using System.Collections.Generic;
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
    public partial class nhan_phieu : System.Web.UI.Page
    {
        #region Declare
        private AppketoanDataContext db = new AppketoanDataContext();
        private EmployerRepo _EmployerRepo = new EmployerRepo();
        private ContractRepo _ContractRepo = new ContractRepo();
        private ContractDetailRepo _ContractDetailRepo = new ContractDetailRepo();
        private CustomerRepo _CustomerRepo = new CustomerRepo();
        private BillRepo _BillRepo = new BillRepo();
        private int _count = 1;
        private clsFormat fm = new clsFormat();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            _count = 1;
            if (!IsPostBack)
            {
                LoadContract_Cannhanphieu();
                showEmployer();
            }
        }
        private void showEmployer()
        {
            var list = _EmployerRepo.GetAllSortName();
            ddlEmployerSearch.DataValueField = "ID";
            ddlEmployerSearch.DataTextField = "EMP_NAME";
            ddlEmployerSearch.DataSource = list;
            ddlEmployerSearch.DataBind();
            ListItem ls = new ListItem("--- Chọn nhân viên ---", "0");
            ls.Selected = true;
            ddlEmployerSearch.Items.Insert(0, ls);
        }
        private void LoadContract_Cannhanphieu()
        {
            string keyword = txtKeyword.Value;
            int EmpTNId = Utils.CIntDef(ddlEmployerSearch.SelectedValue);
            var l = (from a in db.CONTRACTs
                     join b in db.BILLs on a.ID equals b.ID_CONT
                     where a.BILL_STATUS == 1 //phiếu đã giao
                     && a.CONT_STATUS == Cost.HD_CONGOP //hợp đồng còn góp
                     && (a.CONT_NO.Contains(keyword) || keyword == null || keyword == "")
                     && (a.EMP_TN == EmpTNId || EmpTNId == 0)
                     select new
                     {
                         a.ID,
                     }).Distinct();

            List<BILL> lbill = new List<BILL>();
            foreach (var item in l)
            {
                var lbillByConID = db.BILLs.Where(a => a.ID_CONT == item.ID).OrderByDescending(a => a.ID).Take(1).ToList();// OrderByDescending, Take để lấy bill mới nhất
                if (lbillByConID != null && lbillByConID.Count > 0)
                {
                    lbill.Add(lbillByConID[0]);
                }
            }

            var lcontract_nhanphieu = lbill.OrderByDescending(n => n.ID_CONT);

            ASPxGridView1_nhanphieu.DataSource = lcontract_nhanphieu;
            ASPxGridView1_nhanphieu.DataBind();
        }
        
        protected void lbtnNhanphieu_Click(object sender, EventArgs e)
        {
            try
            {
                List<object> fieldValues = ASPxGridView1_nhanphieu.GetSelectedFieldValues(new string[] { "ID_CONT" });
                foreach (var item in fieldValues)
                {
                    int index = ASPxGridView1_nhanphieu.FindVisibleIndexByKeyValue(Utils.CIntDef(item));
                    HiddenField hddID = ASPxGridView1_nhanphieu.FindRowCellTemplateControl(index, (GridViewDataColumn)ASPxGridView1_nhanphieu.Columns["BILL_STATUS"], "hddID") as HiddenField;
                    BILL b = _BillRepo.GetById(Utils.CIntDef(hddID.Value));
                    if (b != null)
                    {
                        TextBox txtPayprice = ASPxGridView1_nhanphieu.FindRowCellTemplateControl(index, (GridViewDataColumn)ASPxGridView1_nhanphieu.Columns["BILL_STATUS"], "txtPayprice") as TextBox;
                        decimal? price = null;
                        if (txtPayprice != null && Utils.CDecDef(Utils.CStrDef(txtPayprice.Text).Replace(",", "")) > 0)
                            price = Utils.CDecDef(Utils.CStrDef(txtPayprice.Text).Replace(",", ""));
                        b.BILL_STATUS = price != null ? 1 : 0;  //1 Phiếu tốt - 0 Phiếu rớt
                        b.BILLL_RECEIVER_DATE = DateTime.Now;
                        _BillRepo.Update(b);//cập nhật ngày nhận và trạng thái bill

                        CONTRACT_DETAIL contractdetail = _ContractDetailRepo.GetByContractIdAndDatethu(b.ID_CONT.Value, b.CONTD_DATE_THU.Value);
                        if (contractdetail != null)
                        {
                            var checklistdetail = db.CONTRACT_DETAILs.Where(a => a.ID_CONT == contractdetail.ID_CONT && a.CONTD_DATE_THU != null && (a.CONTD_DATE_THU.Value - contractdetail.CONTD_DATE_THU.Value).Days >= 0).ToList();
                            if (checklistdetail != null && checklistdetail.Count > 0)
                            {
                                contractdetail.CONTD_PAY_PRICE = price;
                                contractdetail.CONTD_DATE_THU_TT = DateTime.Now;
                                _ContractDetailRepo.Update(contractdetail);
                            }
                            else
                            {
                                CONTRACT_DETAIL de = new CONTRACT_DETAIL();
                                de.ID_CONT = contractdetail.ID_CONT;
                                de.CONTD_PAY_PRICE = price;
                                de.CONTD_DATE_THU_TT = DateTime.Now;
                                _ContractDetailRepo.Create(de);
                            }
                        }

                        CONTRACT contract = _ContractRepo.GetById(Utils.CIntDef(b.ID_CONT));
                        var detail = _ContractDetailRepo.GetListByContractId(Utils.CIntDef(contract.ID));
                        decimal pricethu = 0;
                        if (detail != null)
                        {
                            pricethu = detail.Where(a => a.CONTD_PAY_PRICE != null).Sum(a => a.CONTD_PAY_PRICE.Value);
                            if (contract.CONT_DEBT_PRICE <= pricethu)
                            {
                                contract.CONT_STATUS = 3;
                            }
                        }
                        //var l_nextrecept = db.CONTRACT_DETAILs.Where(a => a.CONTD_DATE_THU > contractdetail.CONTD_DATE_THU).ToList();//xem còn ngày thu tiếp theo không
                        //var l_noprice = db.CONTRACT_DETAILs.Where(a => a.CONTD_PAY_PRICE == 0 || a.CONTD_PAY_PRICE == null).ToList();//check xem con kỳ nào chưa thanh toán
                        //if (l_nextrecept.Count == 0 && l_noprice.Count == 0)
                        //    contract.CONT_STATUS = 2;//hoàn tất hợp đồng
                        contract.BILL_STATUS = 0;//cập nhật trạng thái phiếu
                        _ContractRepo.Update(contract);

                    }
                }


            }
            catch
            {

            }

            //LoadContract_Cannhanphieu();
            Response.Redirect("~/Pages/nhan-phieu.aspx");
        }
        protected void lbtnNhanphieumacdinh_Click(object sender, EventArgs e)
        {
            try
            {
                ASPxGridView1_nhanphieu.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                LoadContract_Cannhanphieu();

                for (int i = 0; i < ASPxGridView1_nhanphieu.VisibleRowCount; i++)
                {
                    HiddenField hddID = ASPxGridView1_nhanphieu.FindRowCellTemplateControl(i, (GridViewDataColumn)ASPxGridView1_nhanphieu.Columns["BILL_STATUS"], "hddID") as HiddenField;
                    BILL b = _BillRepo.GetById(Utils.CIntDef(hddID.Value));
                    if (b != null)
                    {
                        TextBox txtPayprice = ASPxGridView1_nhanphieu.FindRowCellTemplateControl(i, (GridViewDataColumn)ASPxGridView1_nhanphieu.Columns["BILL_STATUS"], "txtPayprice") as TextBox;
                        decimal? price = null;
                        if (txtPayprice != null && Utils.CDecDef(Utils.CStrDef(txtPayprice.Text).Replace(",", "")) > 0)
                            price = Utils.CDecDef(Utils.CStrDef(txtPayprice.Text).Replace(",", ""));
                        b.BILL_STATUS = price != null ? 1 : 0;  //1 Phiếu tốt - 0 Phiếu rớt
                        b.BILLL_RECEIVER_DATE = DateTime.Now;
                        _BillRepo.Update(b);//cập nhật ngày nhận và trạng thái bill

                        CONTRACT_DETAIL contractdetail = _ContractDetailRepo.GetByContractIdAndDatethu(b.ID_CONT.Value, b.CONTD_DATE_THU.Value);
                        if (contractdetail != null)
                        {
                            var checklistdetail = db.CONTRACT_DETAILs.Where(a => a.ID_CONT == contractdetail.ID_CONT && a.CONTD_DATE_THU != null && (a.CONTD_DATE_THU.Value - contractdetail.CONTD_DATE_THU.Value).Days >= 0).ToList();
                            if (checklistdetail != null && checklistdetail.Count > 0)
                            {
                                contractdetail.CONTD_PAY_PRICE = price;
                                contractdetail.CONTD_DATE_THU_TT = DateTime.Now;
                                _ContractDetailRepo.Update(contractdetail);
                            }
                            else
                            {
                                CONTRACT_DETAIL de = new CONTRACT_DETAIL();
                                de.ID_CONT = contractdetail.ID_CONT;
                                de.CONTD_PAY_PRICE = price;
                                de.CONTD_DATE_THU_TT = DateTime.Now;
                                _ContractDetailRepo.Create(de);
                            }
                        }

                        CONTRACT contract = _ContractRepo.GetById(Utils.CIntDef(b.ID_CONT));
                        var detail = _ContractDetailRepo.GetListByContractId(Utils.CIntDef(contract.ID));
                        decimal pricethu = 0;
                        if (detail != null)
                        {
                            pricethu = detail.Where(a => a.CONTD_PAY_PRICE != null).Sum(a => a.CONTD_PAY_PRICE.Value);
                            if (contract.CONT_DEBT_PRICE <= pricethu)
                            {
                                contract.CONT_STATUS = 3;
                            }
                        }
                        //var l_nextrecept = db.CONTRACT_DETAILs.Where(a => a.CONTD_DATE_THU > contractdetail.CONTD_DATE_THU).ToList();//xem còn ngày thu tiếp theo không
                        //var l_noprice = db.CONTRACT_DETAILs.Where(a => a.CONTD_PAY_PRICE == 0 || a.CONTD_PAY_PRICE == null).ToList();//check xem con kỳ nào chưa thanh toán
                        //if (l_nextrecept.Count == 0 && l_noprice.Count == 0)
                        //    contract.CONT_STATUS = 2;//hoàn tất hợp đồng
                        contract.BILL_STATUS = 0;//cập nhật trạng thái phiếu
                        _ContractRepo.Update(contract);

                    }

                }
            }
            catch
            {

            }
            Response.Redirect("~/Pages/nhan-phieu.aspx");
        }
        protected void lbtnMatphieu_Click(object sender, EventArgs e)
        {
            try
            {
                List<object> fieldValues = ASPxGridView1_nhanphieu.GetSelectedFieldValues(new string[] { "ID_CONT" });
                foreach (var item in fieldValues)
                {
                    int index = ASPxGridView1_nhanphieu.FindVisibleIndexByKeyValue(Utils.CIntDef(item));
                    HiddenField hddID = ASPxGridView1_nhanphieu.FindRowCellTemplateControl(index, (GridViewDataColumn)ASPxGridView1_nhanphieu.Columns["BILL_STATUS"], "hddID") as HiddenField;
                    BILL b = _BillRepo.GetById(Utils.CIntDef(hddID.Value));
                    if (b != null)
                    {
                        //CheckBox chkStatusPhieu = ASPxGridView1_nhanphieu.FindRowCellTemplateControl(index, (GridViewDataColumn)ASPxGridView1_nhanphieu.Columns["BILL_STATUS"], "chkStatusPhieu") as CheckBox;
                        TextBox txtPayprice = ASPxGridView1_nhanphieu.FindRowCellTemplateControl(index, (GridViewDataColumn)ASPxGridView1_nhanphieu.Columns["BILL_STATUS"], "txtPayprice") as TextBox;
                        decimal? price = null;
                        if (txtPayprice != null && Utils.CDecDef(Utils.CStrDef(txtPayprice.Text).Replace(",", "")) > 0)
                            price = Utils.CDecDef(Utils.CStrDef(txtPayprice.Text).Replace(",", ""));
                        b.BILL_STATUS = price != null ? 1 : 0;  //1 Phiếu tốt - 0 Phiếu rớt
                        _BillRepo.Update(b);//cập nhật ngày nhận và trạng thái bill

                        CONTRACT_DETAIL contractdetail = _ContractDetailRepo.GetByContractIdAndDatethu(b.ID_CONT.Value, b.CONTD_DATE_THU.Value);
                        if (contractdetail != null)
                        {
                            var checklistdetail = db.CONTRACT_DETAILs.Where(a => a.ID_CONT == contractdetail.ID_CONT && a.CONTD_DATE_THU != null && (a.CONTD_DATE_THU.Value - contractdetail.CONTD_DATE_THU.Value).Days >= 0).ToList();
                            if (checklistdetail != null && checklistdetail.Count > 0)
                            {
                                contractdetail.CONTD_PAY_PRICE = price;
                                contractdetail.CONTD_DATE_THU_TT = DateTime.Now;
                                _ContractDetailRepo.Update(contractdetail);
                            }
                            else
                            {
                                CONTRACT_DETAIL de = new CONTRACT_DETAIL();
                                de.ID_CONT = contractdetail.ID_CONT;
                                de.CONTD_PAY_PRICE = price;
                                de.CONTD_DATE_THU_TT = DateTime.Now;
                                _ContractDetailRepo.Create(de);
                            }
                        }
                        CONTRACT contract = _ContractRepo.GetById(Utils.CIntDef(b.ID_CONT));
                        //var l_nextrecept = db.CONTRACT_DETAILs.Where(a => a.CONTD_DATE_THU > contractdetail.CONTD_DATE_THU).ToList();//xem còn ngày thu tiếp theo không
                        //var l_noprice = db.CONTRACT_DETAILs.Where(a => a.CONTD_PAY_PRICE == 0 || a.CONTD_PAY_PRICE == null).ToList();//check xem con kỳ nào chưa thanh toán
                        //if (l_nextrecept.Count == 0 && l_noprice.Count == 0)
                        //    contract.CONT_STATUS = 2;//hoàn tất hợp đồng
                        contract.BILL_STATUS = 0;//cập nhật trạng thái phiếu
                        _ContractRepo.Update(contract);

                    }
                }

            }
            catch
            {

            }
            //LoadContract_Cannhanphieu();
            Response.Redirect("~/Pages/nhan-phieu.aspx");
        }

        public int setOrder()
        {
            return _count++;
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
        public string getTienthu(object id)
        {
            int _id = Utils.CIntDef(id);
            var contract = _ContractRepo.GetById(_id);
            if (contract != null)
            {
                return fm.FormatMoneyNotext(contract.CONT_WEEK_PRICE);
            }
            return "0";
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
        public string getLinkEmp(object id)
        {
            return "chi-tiet-nhan-vien.aspx?id=" + id;
        }
        public string getnameEmp(object id)
        {
            int _id = Utils.CIntDef(id);
            var employer = _EmployerRepo.GetById(_id);
            if (employer != null)
            {
                return employer.EMP_NAME;
            }
            return "";
        }

        protected void ASPxGridView1_nhanphieu_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            LoadContract_Cannhanphieu();
        }

        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            LoadContract_Cannhanphieu();
        }

        protected void ASPxGridView1_nhanphieu_PageIndexChanged(object sender, EventArgs e)
        {
            LoadContract_Cannhanphieu();
        }

        

    }
}