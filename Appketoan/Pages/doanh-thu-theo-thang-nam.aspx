﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="doanh-thu-theo-thang-nam.aspx.cs" Inherits="Appketoan.Pages.doanh_thu_trong_thang_nam" %>


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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMain" runat="server">
    <div id="header">
        <div class="title">
            Báo cáo doanh thu theo tháng/năm
        </div>
        <div style="clear: both">
        </div>
    </div>
    <div id="div-search">
        <table width="100%">
            <tbody>
                <tr>
                    <td align="left">
                    <asp:DropDownList ID="ddlEmployer" runat="server" CssClass="chosen-select k-textbox textbox" Width="170px">
                    </asp:DropDownList>
                        <asp:DropDownList ID="ddlDay" runat="server" CssClass="k-textbox textbox" Width="70px">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="k-textbox textbox" Width="70px">
                        </asp:DropDownList>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                    ControlToValidate="ddlMonth" Display="Dynamic" ForeColor="Red" ValidationGroup="G21"
                                                    CssClass="tlp-error" InitialValue="0">*</asp:RequiredFieldValidator>--%>
                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="k-textbox textbox" Width="60px">
                        </asp:DropDownList>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                    ControlToValidate="ddlYear" Display="Dynamic" ForeColor="Red" ValidationGroup="G21"
                                                    CssClass="tlp-error" InitialValue="0">*</asp:RequiredFieldValidator>--%>
                        <asp:LinkButton CssClass="k-button" ID="lbtnSearch" ToolTip="Tìm kiếm" runat="server" ValidationGroup="G21"
                            OnClick="lbtnSearch_Click"><span class="p-i-search"></span></asp:LinkButton>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <table width="100%" cellpadding="3" cellspacing="3" style="background-color: #f4f4f4;
        border: 1px solid #aecaf0">
        <tr>
            <td>
            Tiền hàng
            </td>
            <td>
            <strong><asp:Label ID="lbPriceHang" runat="server"></asp:Label></strong> 
            </td>
        </tr>
        <tr>
            <td>
            Tổng tiền
            </td>
            <td>
             <strong><asp:Label ID="lbPriceTong" runat="server"></asp:Label></strong> 
            </td>
        </tr>
        <tr>
            <td style="width:150px">
            Tiền đầu
            </td>
            <td>
             <strong><asp:Label ID="lbPriceDau" runat="server"></asp:Label></strong> 
            </td>
        </tr>
        <tr>
            <td style="width:150px">
            Tiền thu
            </td>
            <td>
             <strong><asp:Label ID="lbPriceThu" runat="server"></asp:Label></strong> 
            </td>
        </tr>
        <tr>
            <td style="width:150px">
            Thất thoát
            </td>
            <td>
             <strong><asp:Label ID="lbPriceThatthoat" runat="server"></asp:Label></strong> 
            </td>
        </tr>
        <tr>
            <td>
            Tiền còn lại
            </td>
            <td>
             <strong><asp:Label ID="lbPriceConlai" runat="server"></asp:Label></strong> 
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
