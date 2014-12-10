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
    public partial class doanh_thu_trong_thang_nam : System.Web.UI.Page
    {
        #region Declare
        private AppketoanDataContext db = new AppketoanDataContext();
        private clsFormat fm = new clsFormat();
        private UserRepo _UserRepo = new UserRepo();
        private ContractRepo _ContractRepo = new ContractRepo();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadMonth();
                LoadYear();
                lbtnSearch_Click(null, null);
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
        
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            int m = Utils.CIntDef(ddlMonth.SelectedItem.Value);
            int y = Utils.CIntDef(ddlYear.SelectedItem.Value);

            //tien hang
            var cth = db.CONTRACTs.Where(a => a.CONT_DELI_DATE != null && a.CONT_PRODUCT_PRICE != null
                && a.CONT_PRODUCT_PRICE.Value > 0
                && (a.CONT_DELI_DATE.Value.Month == m || m == 0)
                && (a.CONT_DELI_DATE.Value.Year == y || y == 0)).ToList();
            decimal pricehang = 0;
            if (cth != null && cth.Count > 0)
            {
                pricehang = Utils.CDecDef(cth.Sum(a => a.CONT_PRODUCT_PRICE));
            }
            lbPriceHang.Text = fm.FormatMoney(pricehang);

            //tổng tiền
            var ctt = db.CONTRACTs.Where(a => a.CONT_DELI_DATE != null && a.CONT_TOTAL_PRICE != null
                && a.CONT_TOTAL_PRICE.Value > 0
                && (a.CONT_DELI_DATE.Value.Month == m || m == 0)
                && (a.CONT_DELI_DATE.Value.Year == y || y == 0)).ToList();
            decimal pricett = 0;
            if (ctt != null && ctt.Count > 0)
            {
                pricett = Utils.CDecDef(ctt.Sum(a => a.CONT_TOTAL_PRICE));
            }
            lbPriceTong.Text = fm.FormatMoney(pricett);
            //tiền đầu
            var ctd = db.CONTRACTs.Where(a => a.CONT_DELI_DATE != null && a.CONT_DELI_PRICE != null
                && a.CONT_DELI_PRICE.Value > 0
                && (a.CONT_DELI_DATE.Value.Month == m || m == 0)
                && (a.CONT_DELI_DATE.Value.Year == y || y == 0)).ToList();
            decimal pricetd = 0;
            if (ctd != null && ctd.Count > 0)
            {
                pricetd = Utils.CDecDef(ctd.Sum(a => a.CONT_DELI_PRICE));
            }
            lbPriceDau.Text = fm.FormatMoney(pricetd);
            //tien thu
            var l = db.CONTRACT_DETAILs.Where(a => a.CONTD_DATE_THU_TT != null && a.CONTD_PAY_PRICE != null
                && a.CONTD_PAY_PRICE.Value > 0
                && (a.CONTD_DATE_THU_TT.Value.Month == m || m == 0)
                && (a.CONTD_DATE_THU_TT.Value.Year == y || y== 0)).ToList();
            decimal pricethu = 0;
            if (l != null && l.Count > 0)
            {
                pricethu = Utils.CDecDef(l.Sum(a => a.CONTD_PAY_PRICE));
            }
            lbPriceThu.Text = fm.FormatMoney(pricethu);
            //that thoat
            var ctconno = db.CONTRACTs.Where(a => a.CONT_DELI_DATE != null && a.CONT_DEBT_PRICE != null
                && a.CONT_DEBT_PRICE.Value > 0
                && (a.CONT_STATUS == 3 || a.CONT_STATUS == 4)
                && (a.CONT_DELI_DATE.Value.Month == m || m == 0)
                && (a.CONT_DELI_DATE.Value.Year == y || y == 0)).ToList();
            decimal priceconno = 0;
            if (ctconno != null && ctconno.Count > 0)
            {
                priceconno = Utils.CDecDef(ctconno.Sum(a => a.CONT_DEBT_PRICE));
            }
            decimal pricetthu = 0;
            foreach (var item in ctconno)
            {
                var dtthu = db.CONTRACT_DETAILs.Where(a => a.CONTD_DATE_THU_TT != null && a.CONTD_PAY_PRICE != null
                && a.CONTD_PAY_PRICE.Value > 0
                && a.ID_CONT == item.ID
                && (a.CONTD_DATE_THU_TT.Value.Month == m || m == 0)
                && (a.CONTD_DATE_THU_TT.Value.Year == y || y == 0)).ToList();
                if (dtthu != null && dtthu.Count > 0)
                {
                    pricetthu += Utils.CDecDef(dtthu.Sum(a => a.CONTD_PAY_PRICE));
                }
            }
            decimal pricetthoat = 0;
            pricetthoat = priceconno - pricetthu;
            lbPriceThatthoat.Text = fm.FormatMoney(pricetthoat);
            //còn lại
            decimal priceconlai = 0;
            priceconlai = pricett - pricetd;
            priceconlai = priceconlai - pricethu;
            priceconlai = priceconlai - pricetthoat;
            lbPriceConlai.Text = fm.FormatMoney(priceconlai);
        }
    }
}