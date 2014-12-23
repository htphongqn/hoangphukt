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
    public partial class so_luong_hien_thi_tren_luoi : System.Web.UI.Page
    {
        #region Declare
        private AppketoanDataContext db = new AppketoanDataContext();
        private clsFormat fm = new clsFormat();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showQuantity();
            }
            
        }
        private void showQuantity()
        {
            var Q_Contract = db.QUANTITY_IN_LISTs.Where(q=>q.CODE == Cost.CONTRACT).ToList();
            if (Q_Contract != null && Q_Contract.Count > 0)
            {
                txtContract.Text = Utils.CStrDef(Q_Contract[0].QUANTITY);
            }
            var Q_ContractDelete = db.QUANTITY_IN_LISTs.Where(q => q.CODE == Cost.CONTRACTDELETE).ToList();
            if (Q_ContractDelete != null && Q_ContractDelete.Count > 0)
            {
                txtContractDelete.Text = Utils.CStrDef(Q_ContractDelete[0].QUANTITY);
            }
            var Q_BILLDELI = db.QUANTITY_IN_LISTs.Where(q => q.CODE == Cost.BILLDELI).ToList();
            if (Q_BILLDELI != null && Q_BILLDELI.Count > 0)
            {
                txtBillDeli.Text = Utils.CStrDef(Q_BILLDELI[0].QUANTITY);
            }
            var Q_BILLDELIFREE = db.QUANTITY_IN_LISTs.Where(q => q.CODE == Cost.BILLDELIFREE).ToList();
            if (Q_BILLDELIFREE != null && Q_BILLDELIFREE.Count > 0)
            {
                txtBillDeliFree.Text = Utils.CStrDef(Q_BILLDELIFREE[0].QUANTITY);
            }
            var Q_BILLRECEI = db.QUANTITY_IN_LISTs.Where(q => q.CODE == Cost.BILLRECEI).ToList();
            if (Q_BILLRECEI != null && Q_BILLRECEI.Count > 0)
            {
                txtBillRecei.Text = Utils.CStrDef(Q_BILLRECEI[0].QUANTITY);
            }
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            QUANTITY_IN_LIST Q_Contract = db.QUANTITY_IN_LISTs.Single(q => q.CODE == Cost.CONTRACT);
            Q_Contract.QUANTITY = Utils.CIntDef(Utils.CStrDef(txtContract.Text).Replace(",", ""));
            db.SubmitChanges();

            QUANTITY_IN_LIST Q_ContractDelete = db.QUANTITY_IN_LISTs.Single(q => q.CODE == Cost.CONTRACTDELETE);
            Q_ContractDelete.QUANTITY = Utils.CIntDef(Utils.CStrDef(txtContractDelete.Text).Replace(",", ""));
            db.SubmitChanges();

            QUANTITY_IN_LIST Q_BILLDELI = db.QUANTITY_IN_LISTs.Single(q => q.CODE == Cost.BILLDELI);
            Q_BILLDELI.QUANTITY = Utils.CIntDef(Utils.CStrDef(txtBillDeli.Text).Replace(",", ""));
            db.SubmitChanges();

            QUANTITY_IN_LIST Q_BILLRECEI = db.QUANTITY_IN_LISTs.Single(q => q.CODE == Cost.BILLRECEI);
            Q_BILLRECEI.QUANTITY = Utils.CIntDef(Utils.CStrDef(txtBillRecei.Text).Replace(",", ""));
            db.SubmitChanges();

            QUANTITY_IN_LIST Q_BILLDELIFREE = db.QUANTITY_IN_LISTs.Single(q => q.CODE == Cost.BILLDELIFREE);
            Q_BILLDELIFREE.QUANTITY = Utils.CIntDef(Utils.CStrDef(txtBillDeliFree.Text).Replace(",", ""));
            db.SubmitChanges();

            Response.Redirect("~/Pages/so-luong-hien-thi-tren-luoi.aspx");
        }
    }
}