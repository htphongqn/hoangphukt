using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.Data.Common;

namespace Appketoan.Pages
{
    public partial class import_excel : System.Web.UI.Page
    {
        #region Declare
        AppketoanDataContext db = new AppketoanDataContext();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private DataTable getDataexcel(string SourceFilePath)
        {

            DataTable dtExcel = new DataTable();
            // Connection String to Excel Workbook
            string excelConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", SourceFilePath);
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = excelConnectionString;
            connection.Open();
            OleDbCommand command = new OleDbCommand("select * from [Sheet1$]", connection);
            OleDbDataAdapter data = new OleDbDataAdapter(command);
            data.Fill(dtExcel);
            return dtExcel;
            
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            string path = string.Concat(Server.MapPath("~/Data/" + fileUpload.FileName));
            fileUpload.SaveAs(path);
            DataTable dt = getDataexcel(path);
            string row1 = "";
            int i = 0;
            foreach (DataColumn col in dt.Columns)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (!String.IsNullOrEmpty(row[col].ToString()))
                    {
                        row1 += row[col].ToString()+"<br/>";
                    }
                }
                if (i > 2) break;

            }
            Lbrow1.Text = row1;
           
           
        }
    }
}