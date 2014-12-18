using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Appketoan.Pages
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool b = (null == 1);
            string[] empBHIds = "123,23,".Split(',');
            string[] empBH2Ids = "123,".Split(',');
            string[] empBH3Ids = "123,34".Split(',');

            DateTime t = DateTime.Today;
            DateTime n = DateTime.Now;
            DayOfWeek dow = DateTime.Now.DayOfWeek;
            string thu2 = DayOfWeek.Monday.ToString();

            bool bcona = "112,22,".Contains("112" + ",");
            bool bcona2 = "112,22,".Contains("22" + ",");
            bool bcona3 =  "112,22,".Contains("1122" + ",") ;
            bool bcona4 = "112,22,".Contains("2112" + ",");
            bool bcona5 = "112,22,".Contains("12" + ",");
        }
    }
}