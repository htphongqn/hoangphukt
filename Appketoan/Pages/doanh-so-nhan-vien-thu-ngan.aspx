<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="doanh-so-nhan-vien-thu-ngan.aspx.cs" Inherits="Appketoan.Pages.doanh_so_nhan_vien_thu_ngan" %>


<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMain" runat="server">
    <div id="header">
        <div class="title">
            Báo cáo doanh số nhân viên thu ngân
        </div>
        <div style="clear: both">
        </div>
    </div>
    <div id="div-search">
        <table width="100%">
            <tbody>
                <tr>
                    <td align="left">
                        <asp:DropDownList ID="ddlContractStatus" runat="server" CssClass="k-textbox textbox" Width="180px">
                            <asp:ListItem Text="--Chọn nhân viên--" Value="0" Selected="True"></asp:ListItem>
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
                <tr>
                    <td></td>
                </tr>
            </tbody>
        </table>
    </div>
    <table width="100%" cellpadding="3" cellspacing="3" style="background-color: #f4f4f4;
        border: 1px solid #aecaf0">
        <tr>
            <td>
                <dx:ASPxGridView ID="ASPxGridView_contract" runat="server" AutoGenerateColumns="False"
                    Width="100%" KeyFieldName="ID" Theme="Aqua" 
                    onbeforecolumnsortinggrouping="ASPxGridView_contract_BeforeColumnSortingGrouping">                    
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="25px" Visible="false">
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Ngày giao HĐ">
                            <DataItemTemplate>
                                <%# getDate(Eval("CONT_DELI_DATE"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="STT" Width="45px">
                            <DataItemTemplate>
                                <%#setOrder()%>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Số HĐ" FieldName="CONT_NO" Width="65px">
                            <DataItemTemplate>
                            <a href="<%#getLink(Eval("ID")) %>">
                                <%# Eval("CONT_NO")%></a>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Loại HĐ" FieldName="CONT_TYPE" Width="65px">
                            <DataItemTemplate>
                                <%#getContractType(Eval("CONT_TYPE"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tình trạng HĐ" FieldName="CONT_STATUS">
                            <DataItemTemplate>
                                <%#getContractStatus(Eval("CONT_STATUS"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tên khách hàng">
                            <DataItemTemplate>
                                <a href="<%#getLinkCus(Eval("ID_CUS")) %>">
                                    <%#getnameCus(Eval("ID_CUS"))%></a>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        
                        
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tên sản phẩm" FieldName="CONT_PRODUCT_NAME">
                            <DataItemTemplate>
                                <%# Eval("CONT_PRODUCT_NAME")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tổng tiền" FieldName="CONT_TOTAL_PRICE">
                            <DataItemTemplate>
                                <%# formatMoney(Eval("CONT_TOTAL_PRICE"))%>
                            </DataItemTemplate>
                            <FooterTemplate>
                                <%# getSumTotal()%>
                            </FooterTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tiền đầu">
                            <DataItemTemplate>
                                <%# formatMoney(Eval("CONT_DELI_PRICE"))%>
                            </DataItemTemplate>
                            <FooterTemplate>
                                <%# getSumDeli()%>
                            </FooterTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Số kỳ góp" FieldName="CONT_WEEK_COUNT" Width="70">
                            <DataItemTemplate>
                                <%# Eval("CONT_WEEK_COUNT")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tiền thu mỗi kỳ" Width="100">
                            <DataItemTemplate>
                                <%# formatMoney(Eval("CONT_WEEK_PRICE"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tiền thu">
                            <DataItemTemplate>
                                <%#getAllthuPirce(Eval("ID")) %>
                            </DataItemTemplate>
                            <FooterTemplate>
                                <%# getSumThu()%>
                            </FooterTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tiền thất thoát">
                            <DataItemTemplate>
                                <%#getMoneythattoat(Eval("CONT_DEBT_PRICE"), Eval("ID"))%>
                            </DataItemTemplate>
                            <FooterTemplate>
                                <%# getSumthattoat()%>
                            </FooterTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Còn lại">
                            <DataItemTemplate>
                                <%#getMoneyconlai(Eval("CONT_DEBT_PRICE"), Eval("ID"))%>
                            </DataItemTemplate>
                            <FooterTemplate>
                                <%# getSumthattoat()%>
                            </FooterTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Người tạo">
                            <DataItemTemplate>
                                <%#getUserName(Eval("USER_ID"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Ghi chú">
                            <DataItemTemplate>
                                <%# Eval("CONT_NOTE")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsPager PageSize="100">
                    </SettingsPager>
                    <Settings ShowFooter="True" />
                </dx:ASPxGridView>
            </td>
        </tr>
    </table>
</asp:Content>
