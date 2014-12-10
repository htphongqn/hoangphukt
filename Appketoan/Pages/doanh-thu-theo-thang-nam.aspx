<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="doanh-thu-theo-thang-nam.aspx.cs" Inherits="Appketoan.Pages.doanh_thu_trong_thang_nam" %>


<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
</asp:Content>
