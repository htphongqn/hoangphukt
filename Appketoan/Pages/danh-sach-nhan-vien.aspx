<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="danh-sach-nhan-vien.aspx.cs" Inherits="Appketoan.Pages.danh_sach_nhan_vien" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMain" runat="server">
    <div id="header">
        <div class="title">
            Danh Sách Nhân Viên
        </div>
        <div class="toolbar" style="margin-left: 1006px; margin-top: -38px;">
            &nbsp;<a href="chi-tiet-nhan-vien.aspx" title="Thêm nhân viên" class="k-button"><span
                class="p-i-add"></span></a> &nbsp;<asp:LinkButton ID="lbtnDelete" OnClientClick="return confirm('Bạn có chắc muốn xóa ?');"
                    ToolTip="Xóa" CssClass="k-button" runat="server" OnClick="lbtnDelete_Click"><img alt="Xóa" src="../Images/icon-32-trash.png" /></asp:LinkButton>
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
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="lbtnDeleteKeyword" />
                        </Triggers>
                        <ContentTemplate>
                        <input class="k-textbox k-input search-noidung fill-input" width="300" id="txtKeyword"
                            name="txtKeyword" type="text" value="" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
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
                <dx:ASPxGridView ID="ASPxGridView1_employer" runat="server" AutoGenerateColumns="False"
                    Width="100%" KeyFieldName="ID" Theme="Aqua">
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                        </dx:GridViewCommandColumn>
                        <%--<dx:GridViewDataTextColumn VisibleIndex="1" Caption="STT" Width="45px">
                            <DataItemTemplate>
                                <%# Container.ItemIndex + 1 %>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>--%>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tên đầy đủ" FieldName="EMP_NAME">
                            <DataItemTemplate>
                            <a href="<%# getlink(Eval("ID")) %>">
                                    <%# Eval("EMP_NAME")%></a>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Chức vụ" FieldName="EMP_CHUCVU">
                            <CellStyle HorizontalAlign="Left" VerticalAlign="Middle">
                            </CellStyle>
                            <DataItemTemplate>
                                <%# getEmpChucvu(Eval("EMP_CHUCVU"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Số điện thoại" FieldName="EMP_PHONE">
                            <DataItemTemplate>
                                <%# Eval("EMP_PHONE")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Địa chỉ" FieldName="EMP_ADDRESS">
                            <DataItemTemplate>
                                <%# Eval("EMP_ADDRESS")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>  
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Người tạo">
                            <DataItemTemplate>
                                <%#getUserName(Eval("USER_ID"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>                      
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Ngày tạo">
                            <DataItemTemplate>
                                <%# Eval("EMP_DATE", "{0:dd/MM/yyyy}")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsPager PageSize="30">
                    </SettingsPager>
                </dx:ASPxGridView>
            </td>
        </tr>
    </table>
</asp:Content>
