using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Data;
using System.Reflection;


namespace Appketoan.Components
{
    public class clsUtility
    {
        public static class Utils
        {
            public static int CIntDef(object Expression, int DefaultValue = 0)
            {
                try
                {
                    return System.Convert.ToInt32(Expression);
                }
                catch (Exception)
                {
                    return DefaultValue;
                }
            }

            public static Int16 CInt16Def(object Expression, Int16 DefaultValue = 0)
            {
                try
                {
                    return System.Convert.ToInt16(Expression);
                }
                catch (Exception)
                {
                    return DefaultValue;
                }
            }

            public static string CStrGuid(object Expression, string DefaultValue = "")
            {
                try
                {
                    Guid g;
                    g = new Guid(Expression.ToString());
                    return Expression.ToString();
                }
                catch (Exception)
                {
                    return DefaultValue;
                }
            }

            public static long CLngDef(object Expression, int DefaultValue = 0)
            {
                try
                {
                    return System.Convert.ToInt32(Expression);
                }
                catch (Exception)
                {
                    return DefaultValue;
                }
            }

            public static bool CBoolDef(object Experssion, bool DefaultValue = false)
            {
                try
                {
                    return System.Convert.ToBoolean(Experssion);
                }
                catch (Exception)
                {
                    return DefaultValue;
                }
            }

            public static decimal CDecDef(object Expression, decimal DefaultValue = 0)
            {
                try
                {
                    return System.Convert.ToDecimal(Expression);
                }
                catch (Exception)
                {
                    return DefaultValue;
                }
            }

            public static double CDblDef(object Expression, double DefaultValue = 0)
            {
                try
                {
                    return System.Convert.ToDouble(Expression);
                }
                catch (Exception)
                {
                    return DefaultValue;
                }
            }

            public static DateTime CDateDef(object Expression, DateTime DefaultValue)
            {
                try
                {
                    if (Expression == null)
                        return DefaultValue;

                    return System.Convert.ToDateTime(Expression);
                }
                catch (Exception)
                {
                    return DefaultValue;
                }
            }

            public static string CStrDef(object Expression, string DefaultValue = "")
            {
                try
                {
                    return Expression.ToString().Trim();
                }
                catch (Exception)
                {
                    return DefaultValue;
                }
            }

            // Chuyển định dạng chuỗi ngày thành định dạng ngày
            public static DateTime StrDateToDate(string dateInput, string formatInput)
            {
                var formatInfo = new DateTimeFormatInfo { ShortDatePattern = formatInput };
                return DateTime.Parse(dateInput, formatInfo);
            }

            // Chuyển định dạng ngày thành định dạng chuỗi ngày
            public static string DateToStrDate(object dateInput, string formatOutput)
            {
                //return dateInput.ToString(formatOutput);
                return string.Format(formatOutput, dateInput);
            }

            // Chuyển định dạng chuỗi ngày thành định dạng chuỗi ngày
            public static string StrDateToStrDate(string dateInput, string formatInput, string formatOutput)
            {
                var date = StrDateToDate(dateInput, formatInput);
                return DateToStrDate(date, formatOutput);
            }

            public static string ClearUnicode(string SourceString)
            {

                SourceString = Regex.Replace(SourceString, "[ÂĂÀÁẠẢÃÂẦẤẬẨẪẰẮẶẲẴàáạảãâầấậẩẫăằắặẳẵ]", "a");
                SourceString = Regex.Replace(SourceString, "[ÈÉẸẺẼÊỀẾỆỂỄèéẹẻẽêềếệểễ]", "e");
                SourceString = Regex.Replace(SourceString, "[IÌÍỈĨỊìíịỉĩ]", "i");
                SourceString = Regex.Replace(SourceString, "[ÒÓỌỎÕÔỒỐỔỖỘƠỜỚỞỠỢòóọỏõôồốộổỗơờớợởỡ]", "o");
                SourceString = Regex.Replace(SourceString, "[ÙÚỦŨỤƯỪỨỬỮỰùúụủũưừứựửữ]", "u");
                SourceString = Regex.Replace(SourceString, "[ỲÝỶỸỴỳýỵỷỹ]", "y");
                SourceString = Regex.Replace(SourceString, "[đĐ]", "d");

                return SourceString;
            }

        }

        public static class Common
        {
            public static DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
            {
                DataTable dtReturn = new DataTable();
                PropertyInfo[] oProps = null;
                if (varlist == null) return dtReturn;
                foreach (T rec in varlist)
                {
                    if (oProps == null)
                    {
                        oProps = ((Type)rec.GetType()).GetProperties();
                        foreach (PropertyInfo pi in oProps)
                        {
                            Type colType = pi.PropertyType;
                            if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                            == typeof(Nullable<>)))
                            {
                                colType = colType.GetGenericArguments()[0];
                            }
                            dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                        }
                    }
                    DataRow dr = dtReturn.NewRow();
                    foreach (PropertyInfo pi in oProps)
                    {
                        dr[pi.Name] = pi.GetValue(rec, null) ?? DBNull.Value;
                    }
                    dtReturn.Rows.Add(dr);
                }
                return dtReturn;
            }

            public static void TransformTableWithSpace(ref DataTable source, DataTable dest, DataRelation rel, DataRow parentRow)
            {
                if (parentRow == null)
                {
                    foreach (DataRow row in source.Rows)
                    {
                        if (!row.HasErrors && (Utils.CDecDef(row["OIDPARENT"], 0) <= 0))
                        {
                            row["NAME"] = (Utils.CIntDef(row["RANK"], 0) <= 1 ? "" : Duplicate("-----", Utils.CIntDef(row["RANK"], 0))) + row["NAME"];
                            dest.Rows.Add(row.ItemArray);
                            row.RowError = "dirty";
                            if (Utils.CStrDef(row["NAME"], "") != "----- Root -----")
                                TransformTableWithSpace(ref source, dest, rel, row);
                        }
                    }
                }
                else
                {
                    DataRow[] children = parentRow.GetChildRows(rel);
                    if (!parentRow.HasErrors)
                    {
                        parentRow["NAME"] = (Utils.CIntDef(parentRow["RANK"], 0) <= 1 ? "" : Duplicate("----------", Utils.CIntDef(parentRow["RANK"], 0))) + parentRow["NAME"];
                        dest.Rows.Add(parentRow.ItemArray);
                        parentRow.RowError = "dirty";
                    }
                    if (children != null && children.Length > 0)
                    {
                        foreach (DataRow child in children)
                        {
                            if (!child.HasErrors)
                            {
                                child["NAME"] = (Utils.CIntDef(child["RANK"], 0) <= 1 ? "" : Duplicate("----------", Utils.CIntDef(child["RANK"], 0))) + child["NAME"];
                                dest.Rows.Add(child.ItemArray);
                                child.RowError = "dirty";
                                TransformTableWithSpace(ref source, dest, rel, child);
                            }
                        }
                    }
                }
            }

            public static string Duplicate(string partToDuplicate, int howManyTimes)
            {
                string result = "";
                for (int i = 0; i < howManyTimes - 1; i++)
                    result += partToDuplicate;
                return result;
            }

            public static string Encrypt(string cleanString, string salt)
            {
                System.Text.Encoding encoding;
                byte[] clearBytes = null;
                byte[] hashedBytes = null;
                encoding = System.Text.Encoding.GetEncoding("unicode");
                clearBytes = encoding.GetBytes(salt.ToLower().Trim() + cleanString.Trim());
                hashedBytes = MD5hash(clearBytes);
                return BitConverter.ToString(hashedBytes);
            }

            public static string Decrypt(string toDecrypt, string salt)
            {
                String key = salt;
                byte[] keyArray;
                byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(
                 toEncryptArray, 0, toEncryptArray.Length);
                return UTF8Encoding.UTF8.GetString(resultArray);
            }

            public static string Generate_Random_String(int Length)
            {
                string _allowedChars = "abcdefghijk0123456789mnopASXZDCQWERFBTGNYHMUJMIKOLPqrstuvwxyz";
                Random randNum = new Random();
                char[] chars = new char[Length];
                int allowedCharCount = _allowedChars.Length;

                for (int i = 0; i < Length; i++)
                {
                    chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
                }
                return new string(chars);
            }

            public static string CreateSalt()
            {
                byte[] bytSalt = new byte[9];
                System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
                rng.GetBytes(bytSalt);
                return Convert.ToBase64String(bytSalt);
            }

            public static byte[] MD5hash(byte[] data)
            {
                System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] result = md5.ComputeHash(data);
                return result;
            }

            public static string getOnClickScript(string ctrlName)
            {
                string strScript = "";
                strScript = "if(event.which || event.keyCode || event.charCode){if ((event.charCode == 13) || (event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + ctrlName + "').focus();return false;}} else {return true}; ";
                return strScript;
            }

            public static string getSubmitScript(string btnName)
            {
                string strScript = "";
                strScript = "if(event.which || event.keyCode || event.charCode){if ((event.charCode == 13) || (event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnName + "').click();return false;}} else {return true}; ";
                return strScript;
            }

            public static string getPriceValue(object value)
            {
                return Utils.CDecDef(value, 0) == 0 ? "0" : string.Format("{0:#,#}", value);
            }

            public static bool IsValidEmail(string strIn)
            {
                return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            }

            public static void DeleteAllFilesInFolder(string folderpath)
            {
                foreach (var f in System.IO.Directory.GetFiles(folderpath))
                    System.IO.File.Delete(f);
            }

            public static object ParseUrl(int ObjId, string ObjName)
            {
                string strCatUrl = System.Text.RegularExpressions.Regex.Replace(System.Text.RegularExpressions.Regex.Replace(ClearUnicode(ObjName.ToLower()), "[^a-zA-Z0-9\\s]+", string.Empty), "[\\s]+", "-") + "-" + ObjId.ToString();
                return strCatUrl + ".aspx";
            }

            public static string ClearUnicode(string SourceString)
            {

                SourceString = Regex.Replace(SourceString, "[àáạảãâầấậẩẫăằắặẳẵ]", "a");
                SourceString = Regex.Replace(SourceString, "[èéẹẻẽêềếệểễ]", "e");
                SourceString = Regex.Replace(SourceString, "[ìíịỉĩ]", "i");
                SourceString = Regex.Replace(SourceString, "[òóọỏõôồốộổỗơờớợởỡ]", "o");
                SourceString = Regex.Replace(SourceString, "[ùúụủũưừứựửữ]", "u");
                SourceString = Regex.Replace(SourceString, "[ỳýỵỷỹ]", "y");
                SourceString = Regex.Replace(SourceString, "[đ]", "d");

                return SourceString;

            }

            public static string StripHtml(string html, bool allowHarmlessTags = false)
            {
                if (html == null || html == string.Empty)
                {
                    return string.Empty;
                }
                if (allowHarmlessTags)
                {
                    return System.Text.RegularExpressions.Regex.Replace(html, "", string.Empty);
                }
                return System.Text.RegularExpressions.Regex.Replace(html, "<[^>]*>", string.Empty);
            }

            public static void informStrError(string str)
            {
                string strPopup = "<script language='javascript'>" + " alert('" + str + "'); </script>";
                HttpContext.Current.Response.Write(strPopup);
            }

            public static void Show(string message)
            {
                string cleanMessage = message.Replace("'", "\'");
                Page page = HttpContext.Current.CurrentHandler as Page;
                string script = string.Format("alert('{0}');", cleanMessage);
                if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
                {
                    page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alert", script, true /* addScriptTags */);
                }
            }

        }

        public static class PathFiles
        {
            public static string GetPathObject()
            {
                return System.Configuration.ConfigurationManager.AppSettings["Subweb"] + "/data";
            }

            public static string GetPathNews(int news_id)
            {
                return GetPathObject() + "/news/" + news_id.ToString() + "/";
            }

            public static string GetPathAds(int news_id)
            {
                return GetPathObject() + "/ads/" + news_id.ToString() + "/";
            }

            public static string GetPathAdsAdvertise(int advertise_id)
            {
                return GetPathObject() + "/ads_advertise/" + advertise_id.ToString() + "/";
            }

            public static string GetPathNewsCompany(int news_id)
            {
                return GetPathObject() + "/news_company/" + news_id.ToString() + "/";
            }

            public static string GetPathNewsBussinessman(int news_id)
            {
                return GetPathObject() + "/news_bussninessman/" + news_id.ToString() + "/";
            }

            public static string GetPathProducts(int pro_id)
            {
                return GetPathObject() + "/products/" + pro_id.ToString() + "/";
            }

            public static string GetPathCategory(int cat_id)
            {
                return GetPathObject() + "/categories/" + cat_id.ToString() + "/";
            }

            public static string GetPathAdItems(int ad_id)
            {
                return GetPathObject() + "/aditems/" + ad_id.ToString() + "/";
            }

            public static string GetPathExt(int ad_id)
            {
                return GetPathObject() + "/ext_files/" + ad_id.ToString() + "/";
            }

            public static string GetPathFooter()
            {
                return GetPathObject() + "/footer/";
            }

            public static string GetPathContact()
            {
                return GetPathObject() + "/contact/";
            }

            public static string GetPathConfigs()
            {
                return GetPathObject() + "/configs/";
            }

            public static string GetPathBanner(int banner_id)
            {
                return GetPathObject() + "/configs/" + banner_id.ToString() + "/";
            }

            public static string GetPathUser(int obj_id)
            {
                return GetPathObject() + "/user/" + obj_id.ToString() + "/";
            }

            public static string GetPathShop(int obj_id)
            {
                return GetPathObject() + "/shops/" + obj_id.ToString() + "/";
            }

            public static string GetPathShopFooter(int obj_id)
            {
                return GetPathObject() + "/shops/" + obj_id.ToString() + "/footer/";
            }

            public static string GetPathShopConfigs(int obj_id)
            {
                return GetPathObject() + "/shops/" + obj_id.ToString() + "/configs/";
            }

            public static string GetPathShopAvatar(int obj_id)
            {
                return GetPathObject() + "/shops/" + obj_id.ToString() + "/avatar/";
            }

            public static string GetPathShopAd(int shop_id, int ad_id)
            {
                return GetPathObject() + "/shops/" + shop_id.ToString() + "/aditems/" + ad_id.ToString() + "/";
            }

            public static string GetPathShopNews(int shop_id, int news_id)
            {
                return GetPathObject() + "/shops/" + shop_id.ToString() + "/news/" + news_id.ToString() + "/";
            }

            public static string GetPathShopProduct(int shop_id, int pro_id)
            {
                return GetPathObject() + "/shops/" + shop_id.ToString() + "/products/" + pro_id.ToString() + "/";
            }

            public static string GetPathCompanyConfigs(decimal obj_id)
            {
                return GetPathObject() + "/companies/" + obj_id.ToString() + "/configs/";
            }

            public static string GetPathCompanyPartner(int obj_id, int part_id)
            {
                return GetPathObject() + "/companies/" + obj_id.ToString() + "/partners/" + part_id.ToString() + "/";
            }

            public static string GetPathCompanyBanner(int obj_id)
            {
                return GetPathObject() + "/companies/" + obj_id.ToString() + "/banner/";
            }

            public static string GetPathBussinessmanBanner(int obj_id)
            {
                return GetPathObject() + "/bussinessmen/" + obj_id.ToString() + "/banner/";
            }

            public static string GetPathBussinessmanConfigs(int obj_id)
            {
                return GetPathObject() + "/bussinessmen/" + obj_id.ToString() + "/configs/";
            }

            public static string GetPathBussinessman(int obj_id)
            {
                return GetPathObject() + "/bussinessmen/" + obj_id.ToString() + "/";
            }

            public static string GetPathDeal(int deal_id)
            {
                return GetPathObject() + "/deals/" + deal_id.ToString() + "/";
            }

            public static string GetPathInterface(decimal inf_id)
            {
                return GetPathObject() + "/interfaces/" + inf_id.ToString() + "/";
            }

        }
    }
}