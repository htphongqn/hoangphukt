﻿using System;
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
            bool b = null == 1;
            string[] empBHIds = "123,23,".Split(',');
            string[] empBH2Ids = "123,".Split(',');
            string[] empBH3Ids = "123,34".Split(',');

            DateTime t = DateTime.Today;
            DateTime n = DateTime.Now;
            DayOfWeek dow = DateTime.Now.DayOfWeek;            
        }
    }
}