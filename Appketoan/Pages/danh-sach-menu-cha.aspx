<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true"
    CodeBehind="danh-sach-menu-cha.aspx.cs" Inherits="Appketoan.Pages.danh_sach_menu_cap_cha" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMain" runat="server">
    <div id="header">
        <div class="title">
            Danh Sách Menu Cấp cha
        </div>
        <div class="toolbar" style="margin-left: 1006px; margin-top: -38px;">
            &nbsp;<a href="chi-tiet-menu-cha.aspx" title="Thêm dịch vụ" class="k-button"><span
                class="p-i-add"></span></a> &nbsp;<asp:LinkButton ID="lbtnDelete" OnClientClick="return confirm('Bạn có chắc muốn xóa ?');"
                    ToolTip="Xóa dịch vụ" CssClass="k-button" runat="server" OnClick="lbtnDelete_Click1"><img alt="Xóa" src="../Images/icon-32-trash.png" /></asp:LinkButton>
        </div>
        <div style="clear: both">
        </div>
    </div>
    <div id="div-search">
        <table>
            <tbody>
                <tr>
                    <td>
                        <label>
                            Từ khóa
                        </label>
                    </td>
                    <td>
                        <input class="k-textbox k-input search-noidung fill-input" width="300" id="txtKeyword"
                            name="txtKeyword" type="text" value="" runat="server" />
                    </td>
                    <td>
                        <asp:LinkButton CssClass="k-button" ID="lbtnSearch" ToolTip="Tìm kiếm" runat="server"
                            OnClick="lbtnSearch_Click"><span class="p-i-search"></span></asp:LinkButton>
                    </td>
                    <td>
                        <asp:LinkButton CssClass="k-button" ID="lbtnDeleteKeyword" ToolTip="Xóa tìm kiếm"
                            runat="server" OnClick="lbtnDeleteKeyword_Click"><span class="p-i-clear"></span></asp:LinkButton>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <table width="100%" cellpadding="3" cellspacing="3" style="background-color: #f4f4f4;
        border: 1px solid #aecaf0">
        <tr>
            <td>
                <dx:ASPxTreeList ID="ASPxTreeList_menu" runat="server" AutoGenerateColumns="False"
                    Width="100%" KeyFieldName="MENU_PAR_ID" Theme="Aqua" ParentFieldName="MENU_PARENT1">
                    <Columns>
                        <dx:TreeListTextColumn Caption="MENU_PAR_ID" FieldName="MENU_PAR_ID" VisibleIndex="0" Visible="False">
                        </dx:TreeListTextColumn>
                        <dx:TreeListTextColumn Caption="Tên menu" FieldName="MENU_NAME" VisibleIndex="2"
                            Width="200px">
                            <DataCellTemplate>
                                <a href="<%# getlink(Eval("MENU_PAR_ID")) %>">
                                    <%# Eval("MENU_NAME")%></a>
                            </DataCellTemplate>
                        </dx:TreeListTextColumn>
                        <dx:TreeListTextColumn Caption="Trạng thái" VisibleIndex="2"
                            Width="200px">
                            <DataCellTemplate>
                                <%# Getactive(Eval("MENU_PAR_ACTIVE"))%>
                            </DataCellTemplate>
                        </dx:TreeListTextColumn>
                    </Columns>
                    <SettingsSelection AllowSelectAll="True" Enabled="True" />
                </dx:ASPxTreeList>
               
            </td>
        </tr>
    </table>
</asp:Content>
