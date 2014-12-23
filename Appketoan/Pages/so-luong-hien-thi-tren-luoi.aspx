<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="so-luong-hien-thi-tren-luoi.aspx.cs" Inherits="Appketoan.Pages.so_luong_hien_thi_tren_luoi" %>


<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link rel="stylesheet" href="../chosen/docsupport/style.css">
    <link rel="stylesheet" href="../chosen/docsupport/prism.css">
    <link rel="stylesheet" href="../chosen/chosen.css">
    <style type="text/css" media="all">
        /* fix rtl for demo */
        .chosen-rtl .chosen-drop { left: -9000px; }
    </style>
    <script type="text/javascript">
        function FormatNumber(obj) {
            var strvalue;
            if (eval(obj))
                strvalue = eval(obj).value;
            else
                strvalue = obj;
            var num;
            num = strvalue.toString().replace(/\$|\,/g, '');
            if (isNaN(num))
                num = "";
            sign = (num == (num = Math.abs(num)));
            num = Math.floor(num * 100 + 0.50000000001);
            num = Math.floor(num / 100).toString();
            for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
                num = num.substring(0, num.length - (4 * i + 3)) + ',' +
   num.substring(num.length - (4 * i + 3));
            //return (((sign)?'':'-') + num); 
            eval(obj).value = (((sign) ? '' : '-') + num);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMain" runat="server">
    <div id="header">
        <div class="title">
            Số lượng hiển thị trên lưới
        </div>
        <div class="toolbar" style="padding-bottom: 5px">
            &nbsp;<asp:LinkButton ID="lbtnSave" ToolTip="Lưu thông tin" CssClass="k-button" runat="server"
                OnClick="lbtnSave_Click" ValidationGroup="G2"><img alt="Lưu thông tin" src="../Images/icon-32-save.png" /></asp:LinkButton>            
        </div>
    </div>    
    <table width="100%" cellpadding="3" cellspacing="3" style="background-color: #f4f4f4;
        border: 1px solid #aecaf0">
        <tr>
            <td style="width:180px">
                Danh sách hợp đồng
            </td>
            <td>
                <asp:TextBox ID="txtContract" runat="server" class="text" 
                                                    CssClass="k-textbox textbox" 
                                                    onkeyup="FormatNumber(this);" onblur="FormatNumber(this);"
                                                     MaxLength="3"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Danh sách hợp đồng đã xóa
            </td>
            <td>
                <asp:TextBox ID="txtContractDelete" runat="server" class="text" 
                                                    CssClass="k-textbox textbox" 
                                                    onkeyup="FormatNumber(this);" onblur="FormatNumber(this);"
                                                     MaxLength="3"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:150px">
                Phát phiếu
            </td>
            <td>
                <asp:TextBox ID="txtBillDeli" runat="server" class="text" 
                                                    CssClass="k-textbox textbox" 
                                                    onkeyup="FormatNumber(this);" onblur="FormatNumber(this);"
                                                     MaxLength="3"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:150px">
                Phát phiếu tự do
            </td>
            <td>
                <asp:TextBox ID="txtBillDeliFree" runat="server" class="text" 
                                                    CssClass="k-textbox textbox" 
                                                    onkeyup="FormatNumber(this);" onblur="FormatNumber(this);"
                                                     MaxLength="3"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:150px">
                Nhận phiếu
            </td>
            <td>
                <asp:TextBox ID="txtBillRecei" runat="server" class="text" 
                                                    CssClass="k-textbox textbox" 
                                                    onkeyup="FormatNumber(this);" onblur="FormatNumber(this);"
                                                     MaxLength="3"></asp:TextBox>
            </td>
        </tr>
    </table>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.6.4/jquery.min.js" type="text/javascript"></script>
  <script src="../chosen/chosen.jquery.js" type="text/javascript"></script>
  <script src="../chosen/docsupport/prism.js" type="text/javascript" charset="utf-8"></script>
  <script type="text/javascript">
      var config = {
          '.chosen-select': {},
          '.chosen-select-deselect': { allow_single_deselect: true },
          '.chosen-select-no-single': { disable_search_threshold: 10 },
          '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
          '.chosen-select-width': { width: "95%" }
      }
      for (var selector in config) {
          $(selector).chosen(config[selector]);
      }
  </script>
</asp:Content>
