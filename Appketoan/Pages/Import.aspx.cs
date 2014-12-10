using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using vpro.functions;
using Appketoan.Components;
using Appketoan.Data;
using System.Collections;

namespace Appketoan.Pages
{
    public partial class Import : System.Web.UI.Page
    {
        private AppketoanDataContext db = new AppketoanDataContext();
        private clsFormat fm = new clsFormat();
        private UserRepo _UserRepo = new UserRepo();
        private CustomerRepo _CustomerRepo = new CustomerRepo();
        private CustomerHistoryRepo _CustomerHistoryRepo = new CustomerHistoryRepo();
        private EmployerRepo _EmployerRepo = new EmployerRepo();
        private ContractRepo _ContractRepo = new ContractRepo();
        private ContractDetailRepo _ContractDetailRepo = new ContractDetailRepo();
        private ContractHistoryRepo _ContractHistoryRepo = new ContractHistoryRepo();
        private BillRepo _BillRepo = new BillRepo();
        protected void Page_Load(object sender, EventArgs e)
        {
            //DateTime a= DateTime.FromOADate(0);
            //DateTime a2 =  DateTime.FromOADate(1);
        }
        protected void bt_import_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            OleDbConnection conn = new OleDbConnection();

            OleDbCommand cmd = new OleDbCommand();
            string path = @"D:\PhongHT\Yeucau\Hoangphu\du-lieu.xls";//duong dan toi file excel
            string Strconn = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path;
            Strconn += ";Extended Properties=Excel 8.0";
            conn.ConnectionString = Strconn;
            cmd.CommandText = "Select * from [TONG KET$]";//ten sheet
            cmd.Connection = conn;

            cmd.Connection = conn;
            OleDbDataAdapter dap = new OleDbDataAdapter(cmd);
            dap.Fill(table);
            //int countrow = 0;
            for (int i = 2; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                decimal tien_con_gop = Utils.CDecDef(row[14]);
                string thanhly = Utils.CStrDef(row[16]);
                //lấy hd còn góp
                if (tien_con_gop > 0 && thanhly.Length == 0)
                {
                    CONTRACT c = new CONTRACT();
                    c.CONT_DELI_DATE = Utils.CDateDef(row[0], DateTime.MinValue);
                    c.CONT_NO = Utils.CStrDef(row[2]);
                    c.CONT_STATUS = 2;
                    c.CONT_TYPE = 1;
                    string cusName = Utils.CStrDef(row[3]);
                    string cusAddress = Utils.CStrDef(row[4]);
                    string cusPhone = Utils.CStrDef(row[5]);
                    CUSTOMER cus = _CustomerRepo.GetByNameandAddress(cusName, cusAddress);
                    if (cus == null)
                    {
                        cus = new CUSTOMER();
                        cus.CUS_FULLNAME = cusName;
                        cus.CUS_ADDRESS = cusAddress;
                        cus.CUS_PHONE = cusPhone;
                        cus.CUS_TYPE = 1;
                        cus.CUS_CREATE_DATE = DateTime.Now;
                        _CustomerRepo.Create(cus);
                        SaveHistory(cus);
                    }
                    c.ID_CUS = cus.ID;
                    c.CONT_PRODUCT_NAME = Utils.CStrDef(row[6]);
                    c.CONT_PRODUCT_PRICE = Utils.CDecDef(row[26])*1000;
                    c.CONT_TOTAL_PRICE = Utils.CDecDef(row[8]) * 1000;
                    c.CONT_DELI_PRICE = Utils.CDecDef(row[9]) * 1000;
                    c.CONT_WEEK_PRICE = Utils.CDecDef(row[10]) * 1000;
                    c.CONT_WEEK_COUNT = Utils.CIntDef(row[11]);
                    c.CONT_NOTE = Utils.CStrDef(row[15]);
                    c.CONT_NOTE_TRACK = Utils.CStrDef(row[17]);
                    
                    string empIds = "";
                    string [] empNamestr = Utils.CStrDef(row[18]).Split('+','-');
                    foreach (var item in empNamestr)
                    {
                        EMPLOYER emp = _EmployerRepo.GetByNam(item);
                        if (emp == null)
                        {
                            emp = new EMPLOYER();
                            emp.EMP_NAME = item;
                            emp.EMP_DATE = DateTime.Now;
                            _EmployerRepo.Create(emp);
                        }
                        empIds += emp.ID + ",";
                    }
                    c.EMP_BH = empIds;

                    string empXMIds = "";
                    string[] empXMNamestr = Utils.CStrDef(row[19]).Split('+', '-');
                    foreach (var item in empXMNamestr)
                    {
                        EMPLOYER emp = _EmployerRepo.GetByNam(item);
                        if (emp == null)
                        {
                            emp = new EMPLOYER();
                            emp.EMP_NAME = item;
                            emp.EMP_DATE = DateTime.Now;
                            _EmployerRepo.Create(emp);
                        }
                        empXMIds += emp.ID + ",";
                    }
                    c.EMP_XM = empXMIds;

                    string empGHIds = "";
                    string[] empGHNamestr = Utils.CStrDef(row[20]).Split('+', '-');
                    foreach (var item in empXMNamestr)
                    {
                        EMPLOYER emp = _EmployerRepo.GetByNam(item);
                        if (emp == null)
                        {
                            emp = new EMPLOYER();
                            emp.EMP_NAME = item;
                            emp.EMP_DATE = DateTime.Now;
                            _EmployerRepo.Create(emp);
                        }
                        empGHIds += emp.ID + ",";
                    }
                    c.EMP_GH = empGHIds;

                    c.CUS_GT = Utils.CStrDef(row[22]);
                    c.CONT_NOTE_DELI = Utils.CStrDef(row[23]);
                    c.CONT_NOTE_XM = Utils.CStrDef(row[24]);

                    _ContractRepo.Create(c);

                    //tạo details các kỳ thu dựa vào ngày giao hàng và loại hợp đồng
                    for (int j = 1; j <= c.CONT_WEEK_COUNT; j++)
                    {
                        CONTRACT_DETAIL cd = new CONTRACT_DETAIL();
                        cd.ID_CONT = c.ID;
                        if (c.CONT_TYPE == 3)
                        {
                            cd.CONTD_DATE_THU = c.CONT_DELI_DATE.Value.AddMonths(j);
                        }
                        else if (c.CONT_TYPE == 2)
                        {
                            cd.CONTD_DATE_THU = c.CONT_DELI_DATE.Value.AddDays(j * 2 * 7);
                        }
                        else if (c.CONT_TYPE == 1)
                        {
                            cd.CONTD_DATE_THU = c.CONT_DELI_DATE.Value.AddDays(j * 7);
                        }
                        //cd.CONTD_DEBT_PRICE = c.CONT_DEBT_PRICE - (c.CONT_WEEK_PRICE * j);
                        _ContractDetailRepo.Create(cd);
                    }
                    //tạo lịch sử chuyển đổi đầu tiên
                    CONTRACT_HISTORY ch = new CONTRACT_HISTORY();
                    ch.ID_CONT = c.ID;
                    ch.CONTHIS_TYPE = c.CONT_TYPE;
                    ch.CONTHIS_TRANSFER_DATE = DateTime.Now;
                    //ch.USER_ID = Utils.CIntDef(Session["Userid"]);
                    _ContractHistoryRepo.Create(ch);

                    //countrow++;
                }
            }

            //countrow = countrow;// = 1066
            
        }
        private void SaveHistory(CUSTOMER Customer)
        {
            CUSTOMER_HISTORY CustomerHis = new CUSTOMER_HISTORY();
            CustomerHis.ID_CUS = Customer.ID;
            CustomerHis.CUSHIS_TYPE = Customer.CUS_TYPE;
            CustomerHis.CUSHIS_DATE = DateTime.Now;
            _CustomerHistoryRepo.Create(CustomerHis);
        }

        protected void bt_import_detail_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            OleDbConnection conn = new OleDbConnection();
            OleDbCommand cmd = new OleDbCommand();
            string path = @"D:\PhongHT\Yeucau\Hoangphu\quan-li.xls";//duong dan toi file excel
            string Strconn = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path;
            Strconn += ";Extended Properties=Excel 8.0";
            conn.ConnectionString = Strconn;
            cmd.CommandText = "Select * from [Tra$]";//ten sheet
            cmd.Connection = conn;
            OleDbDataAdapter dap = new OleDbDataAdapter(cmd);
            dap.Fill(table);//65535

            DataTable table2 = new DataTable();
            cmd.CommandText = "Select * from [Phieu tra ko tien$]";//ten sheet
            cmd.Connection = conn;
            dap = new OleDbDataAdapter(cmd);
            dap.Fill(table2);
            //int countrow = 0;

            var rows = from row in table.AsEnumerable()
                       where row.Field<string>("F1") != "" && row.Field<string>("F1") != null
                       select row;
            table = rows.CopyToDataTable();
            var rows2 = from row2 in table2.AsEnumerable()
                       where row2.Field<string>("F2") != "" && row2.Field<string>("F2") != null
                       select row2;
            table2 = rows2.CopyToDataTable();

            int count = table.Rows.Count;//41000
            int count2 = table2.Rows.Count;//4069
            for (int i = 1; i < count2; i++)
            {
                DataRow rowsArray = table.NewRow();
                rowsArray[0] = table2.Rows[i][1];//so phieu
                rowsArray[6] = table2.Rows[i][4];//ngày trả
                rowsArray[2] = "0";//tien gop
                rowsArray[3] = table2.Rows[i][2];//thu ngan
                table.Rows.Add(rowsArray);
            }
            DataView dv = table.DefaultView;
            dv.Sort = "F7 asc";
            DataTable sortedDT = dv.ToTable();

            var c = db.CONTRACTs.ToList();
            for (int i = 0; i < c.Count; i++)
            {
                var results = from myRow in sortedDT.AsEnumerable()
                where myRow.Field<string>("F1") == c[i].CONT_NO
                select myRow;
                DataTable dt = results.Any() ? results.CopyToDataTable() : null;   

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataView dv1 = dt.DefaultView;
                    dv1.Sort = "F7 asc";
                    dt = dv1.ToTable();
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        //insert bill
                        BILL b = new BILL();
                        var cc =_ContractRepo.GetByIdNo(Utils.CStrDef(dt.Rows[j][0]));
                        b.ID_CONT = cc.ID;
                        decimal tiengop = 0;
                        tiengop = Utils.CDecDef(dt.Rows[j][2]) * 1000;
                        b.BILL_STATUS = tiengop > 0 ? 1 : 0;
                        string namethungan = Utils.CStrDef(dt.Rows[j][3]);
                        EMPLOYER emp = _EmployerRepo.GetByNam(namethungan);
                        if (emp == null)
                        {
                            emp = new EMPLOYER();
                            emp.EMP_NAME = namethungan;
                            emp.EMP_DATE = DateTime.Now;
                            _EmployerRepo.Create(emp);
                        }
                        b.ID_EMPLOY = emp.ID;
                        //b.BILL_DELI_DATE = Utils.CDateDef(Utils.CStrDef(dt.Rows[j][6]), DateTime.MinValue);
                        b.BILL_DELI_DATE = DateTime.FromOADate(Utils.CDblDef(dt.Rows[j][6]));
                        if (b.BILL_DELI_DATE == DateTime.MinValue)
                            b.BILL_DELI_DATE = null;
                        b.BILLL_RECEIVER_DATE = b.BILL_DELI_DATE;
                        _BillRepo.Create(b);

                        //update or insert chi tiet hop dong
                        var cdetail = _ContractDetailRepo.GetNoPriceByContractId(b.ID_CONT.Value);
                        if (cdetail != null)
                        {
                            cdetail.CONTD_DATE_THU_TT = b.BILLL_RECEIVER_DATE;
                            cdetail.CONTD_PAY_PRICE = tiengop;
                            if (cdetail.CONTD_PAY_PRICE == 0)
                                cdetail.CONTD_PAY_PRICE = null;
                            _ContractDetailRepo.Update(cdetail);
                        }
                        else
                        {
                            CONTRACT_DETAIL cde = new CONTRACT_DETAIL();
                            cde.ID_CONT = b.ID_CONT;
                            cde.CONTD_DATE_THU_TT = b.BILLL_RECEIVER_DATE;
                            cde.CONTD_PAY_PRICE = tiengop;
                            if (cde.CONTD_PAY_PRICE == 0)
                                cde.CONTD_PAY_PRICE = null;
                            _ContractDetailRepo.Create(cde);
                        }                       

                        //cập nhật nhân viên thu ngan
                        if (j == dt.Rows.Count - 1)
                        {
                            var cl = _ContractRepo.GetById(b.ID_CONT.Value);
                            if (cl != null)
                            {
                                cl.EMP_TN = emp.ID;
                                _ContractRepo.Update(cl);
                            }
                        }
                    }

                }
            }
        }
    }
}