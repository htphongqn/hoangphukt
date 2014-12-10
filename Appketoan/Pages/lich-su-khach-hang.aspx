<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="lich-su-khach-hang.aspx.cs" Inherits="Appketoan.Pages.lich_su_khach_hang" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMain" runat="server">
    <div id="header">
        <div class="title">
            Lịch Sử Khách Hàng
        </div>
        <div class="toolbar" style="margin-left: 1006px; margin-top: -38px;">
            <asp:LinkButton ID="lbtnDelete" OnClientClick="return confirm('Bạn có chắc muốn xóa ?');"
                    ToolTip="Xóa" CssClass="k-button" runat="server" OnClick="lbtnDelete_Click"><img alt="Xóa" src="../Images/icon-32-trash.png" /></asp:LinkButton>
        </div>
        <div style="clear: both">
        </div>
    </div>
    
    <table width="100%" cellpadding="3" cellspacing="3" style="background-color: #f4f4f4;
        border: 1px solid #aecaf0">
        <tr>
            <td>
                <dx:ASPxGridView ID="ASPxGridView1_Customer" runat="server" AutoGenerateColumns="False"
                    Width="100%" KeyFieldName="ID" Theme="Aqua">
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tên đầy đủ" FieldName="ID_CUS">
                            <DataItemTemplate>
                                    <%# getCustomerName(Eval("ID_CUS")) %>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Loại">
                            <DataItemTemplate>
                                <%# GetType(Eval("CUSHIS_TYPE"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Ngày tạo">
                            <DataItemTemplate>
                                <%# getDate(Eval("CUSHIS_DATE"))%>
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

