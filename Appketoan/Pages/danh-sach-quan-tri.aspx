<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true"
    CodeBehind="danh-sach-quan-tri.aspx.cs" Inherits="Appketoan.Pages.danh_sach_quan_tri" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMain" runat="server">
    <div id="header">
        <div class="title">
            Danh Sách Quản Trị
        </div>
        <div class="toolbar" style="margin-left: 1006px; margin-top: -38px;">
            &nbsp;<a href="chi-tiet-quan-tri.aspx" title="Thêm dịch vụ" class="k-button"><span
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
                <dx:ASPxGridView ID="ASPxGridView1_user" runat="server" AutoGenerateColumns="False"
                    Width="100%" KeyFieldName="USER_ID" Theme="Aqua">
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tên đầy đủ" FieldName="USER_NAME">
                            <DataItemTemplate>
                                <a href="<%# getlink(Eval("USER_ID")) %>">
                                    <%# Eval("USER_NAME")%></a>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tên đăng nhập" FieldName="USER_UN">
                            <DataItemTemplate>
                                <%# Eval("USER_UN")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Email" FieldName="USER_EMAIL">
                            <DataItemTemplate>
                                <%# Eval("USER_EMAIL")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Số điện thoại" FieldName="USER_PHONE">
                            <DataItemTemplate>
                                <%# Eval("USER_PHONE")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Địa chỉ" FieldName="USER_ADDRESS">
                            <DataItemTemplate>
                                <%# Eval("USER_ADDRESS")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <%--<dx:GridViewDataTextColumn VisibleIndex="1" Caption="Nhóm">
                            <DataItemTemplate>
                                <%# Getnhom(Eval("GROUP_TYPE"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>--%>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Trạng thái">
                            <DataItemTemplate>
                                <%# Getactive(Eval("USER_ACTIVE"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsPager PageSize="100">
                    </SettingsPager>
                </dx:ASPxGridView>
            </td>
        </tr>
    </table>
</asp:Content>
