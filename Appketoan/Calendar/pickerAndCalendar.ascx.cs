using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using Appketoan.Components;
using vpro.functions;

namespace vpro.eshop.cpanel.Calendar
{
    public partial class pickerAndCalendar : System.Web.UI.UserControl
    {

        #region Properties
        public DateTime returnDate 
        {
	        get { return Picker.SelectedDate; }
	        set {
                value = Utils.CDateDef(string.Format("{0:MM/dd/yyyy}",value), DateTime.Now);
		        Picker.SelectedDate = value;
                Calendar.SelectedDate = value;
                Calendar.VisibleDate = value;
	        }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            object strDate = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            System.DateTime initDate = Picker.SelectedDate;
            string strCurDay = initDate.Month + "/" + initDate.Day + "/" + initDate.Year;
            Calendar.CultureId = 1066;
            if (!(IsDate(strCurDay.ToString())) | (strCurDay == "1/1/1"))
            {
                Picker.SelectedDate = DateTime.Now;
                Calendar.SelectedDate = DateTime.Now;
                Calendar.VisibleDate = DateTime.Now;
            }

        }

        public bool IsDate(string sdate)
        {
            DateTime dt;
            bool isDate = true;

            try
            {
                dt = DateTime.Parse(sdate);
            }
            catch
            {
                isDate = false;
            }

            return isDate;
        }
    }
}