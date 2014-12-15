<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="danh-sach-khach-hang-khong-giao.aspx.cs" Inherits="Appketoan.Pages.danh_sach_khach_hang_khong_giao" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMain" runat="server">
<div id="header">
        <div class="title">
            Danh Sách Khách Hàng Không Giao
        </div>
        <div class="toolbar" style="margin-left: 1006px; margin-top: -38px;">
            &nbsp;<a href="chi-tiet-khach-hang-khong-giao.aspx" title="Thêm khách hàng không giao" class="k-button"><span
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
                        <label>
                            Tình trạng
                        </label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="k-textbox textbox">
                            <asp:ListItem Value="0" Text="Chưa xử lý"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Đã xử lý"></asp:ListItem>
                        </asp:DropDownList>
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
                <dx:ASPxGridView ID="ASPxGridView1_Customer" runat="server" AutoGenerateColumns="False"
                    Width="100%" KeyFieldName="ID" Theme="Aqua">
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="45px">
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Ngày FAX HĐ">
                            <DataItemTemplate>
                                <%# getDate(Eval("CUS_FAX_DATE"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Xử lý" Width="60px">
                            <DataItemTemplate>
                                <a href="<%# getlinkXLCus(Eval("ID")) %>" onclick="return confirm('Bạn có chắc chắn cập nhật khách hàng để thêm mới hợp đồng?')">
                                    Cập nhật</a>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tên đầy đủ" FieldName="CUS_FULLNAME" Width="250px">
                            <DataItemTemplate>
                               <a href="<%# getlink(Eval("ID")) %>" title='<%# Eval("CUS_FULLNAME")%>'>
                                    <%# getTitle(Eval("CUS_FULLNAME"), 40)%></a>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn> 
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Khách hàng">
                            <DataItemTemplate>
                                <%#checkCus(Eval("Cus_ID"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>                                                 
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Số điện thoại" FieldName="CUS_PHONE" Width="200px">
                            <DataItemTemplate>
                            <span title='<%# Eval("CUS_PHONE")%>'>
                                <%# getTitle(Eval("CUS_PHONE"), 25)%></span>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>                      
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Địa chỉ" FieldName="CUS_ADDRESS" Width="250px">
                            <DataItemTemplate>                            
                                <span title='<%# Eval("CUS_ADDRESS") %>'>
                            <%# getTitle(Eval("CUS_ADDRESS"), 38)%>
                                </span>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>   
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Sản phẩm" FieldName="CUS_PRODUCT" Width="200px">
                            <DataItemTemplate>
                            <span title='<%# Eval("CUS_PRODUCT")%>'>
                                <%# getTitle(Eval("CUS_PRODUCT"),28)%></span>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="NVBH">
                            <DataItemTemplate>
                                <%#getEmp(Eval("EMP_BH"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="NVXM">
                            <DataItemTemplate>
                                <%#getEmp(Eval("EMP_XM"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Biên bản XM" FieldName="NOTE_XM" Width="200px">
                            <DataItemTemplate>
                            <span title='<%# Eval("NOTE_XM")%>'>
                                <%# getTitle(Eval("NOTE_XM"), 28)%></span>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>                                           
                           
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tình trạng">
                            <DataItemTemplate>
                                <%#getStatus(Eval("PROCESS_STATUS"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>    
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Người tạo">
                            <DataItemTemplate>
                                <%#getUserName(Eval("USER_ID"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>                    
                    </Columns>
                    <Settings ShowHorizontalScrollBar="true" />
                    <Settings VerticalScrollableHeight="350" />
                    <Settings ShowVerticalScrollBar="true" />
                    <SettingsPager PageSize="30">
                    </SettingsPager>
                </dx:ASPxGridView>
            </td>
        </tr>
    </table>
</asp:Content>
