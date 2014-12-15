using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appketoan.Components
{
    public class Cost
    {
        //  CUS_TYPE
        public const int CUSTOMER_GOOD = 1;
        public const int CUSTOMER_HANDLING = 2;
        public const int CUSTOMER_BAD = 3;

        //  CONT_STATUS
        public const int HD_CANGIAO = 1;
        public const int HD_CONGOP = 2;
        public const int HD_THANHLY = 3;
        public const int HD_DUTNO = 4;

        //EMP_TYPE
        public const int EMP_TIEPTHI = 1;
        public const int EMP_CONGTY = 2;
        public const string EMP_TIEPTHI_STR = "Tiếp thị";
        public const string EMP_CONGTY_STR = "Công ty";
    }
}