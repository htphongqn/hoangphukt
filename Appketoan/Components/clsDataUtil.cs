using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using vpro.functions;

namespace Appketoan.Components
{
    public class clsDataUtil
    {
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
        public static string Duplicate(string partToDuplicate, int howManyTimes)
        {
            string result = "";

            for (int i = 0; i < howManyTimes; i++)
                result += partToDuplicate;

            return result;
        }
        public static void TransformTableWithSpace(ref DataTable source, DataTable dest, DataRelation rel, DataRow parentRow)
        {
            if (parentRow == null)
            {
                foreach (DataRow row in source.Rows)
                {
                    if (!row.HasErrors && (Utils.CIntDef(row["PROP_PARENT_ID"]) <= 0))
                    {
                        row["PROP_NAME"] = (Utils.CIntDef(row["PROP_RANK"]) <= 1 ? "" : Duplicate("------", Utils.CIntDef(row["PROP_RANK"]))) + row["PROP_NAME"];
                        dest.Rows.Add(row.ItemArray);
                        row.RowError = "dirty";
                        if (Utils.CStrDef(row["PROP_NAME"]) != "------- Root -------")
                            TransformTableWithSpace(ref source, dest, rel, row);
                    }
                }
            }
            else
            {
                DataRow[] children = parentRow.GetChildRows(rel);
                if (!parentRow.HasErrors)
                {
                    parentRow["PROP_NAME"] = (Utils.CIntDef(parentRow["PROP_RANK"]) <= 1 ? "" : Duplicate("------", Utils.CIntDef(parentRow["PROP_RANK"]))) + parentRow["PROP_NAME"];
                    dest.Rows.Add(parentRow.ItemArray);
                    parentRow.RowError = "dirty";
                }
                if (children != null && children.Length > 0)
                {
                    foreach (DataRow child in children)
                    {
                        if (!child.HasErrors)
                        {
                            child["PROP_NAME"] = (Utils.CIntDef(child["PROP_RANK"]) <= 1 ? "" : Duplicate("------", Utils.CIntDef(child["PROP_RANK"]))) + child["PROP_NAME"];
                            dest.Rows.Add(child.ItemArray);
                            child.RowError = "dirty";
                            TransformTableWithSpace(ref source, dest, rel, child);
                        }
                    }
                }
            }
        }
    }
}