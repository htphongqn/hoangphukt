<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true"
    CodeBehind="danh-sach-hop-dong.aspx.cs" Inherits="Appketoan.Pages.danh_sach_hop_dong" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMain" runat="server">
    <div id="header">
        <div class="title">
            Danh Sách Hợp Đồng
        </div>
        <div class="toolbar" style="margin-left: 1006px; margin-top: -38px;">
            &nbsp;<a href="chi-tiet-hop-dong.aspx" title="Thêm hợp đồng" class="k-button"><span
                class="p-i-add"></span></a> &nbsp;<asp:LinkButton ID="lbtnDelete" OnClientClick="return confirm('Bạn có chắc muốn xóa ?');"
                    ToolTip="Xóa" CssClass="k-button" runat="server" OnClick="lbtnDelete_Click1"><img alt="Xóa" src="../Images/icon-32-trash.png" /></asp:LinkButton>
        </div>
        <div style="clear: both">
        </div>
    </div>
    <div id="div-search">
    <asp:Panel ID="pnContract" runat="server" DefaultButton="lbtnSearch">
        <table>
            <tbody>
                <tr>
                    <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="lbtnDeleteKeyword" />
                        </Triggers>
                        <ContentTemplate>
                        <input class="k-textbox k-input search-noidung fill-input" width="300" id="txtKeyword"
                            name="txtKeyword" type="text" runat="server" placeholder="Nhập số hợp đồng" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlContractStatus" runat="server" CssClass="k-textbox textbox" Width="180px">
                            <asp:ListItem Text="--Chọn tình trạng hợp đồng--" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Hợp đồng còn góp" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Hợp đồng thanh lý" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Hợp đồng chết" Value="4"></asp:ListItem>
                        </asp:DropDownList>
                    </td>

                    <td>
                        <asp:LinkButton CssClass="k-button" ID="lbtnSearch" ToolTip="Tìm kiếm" runat="server"
                            OnClick="lbtnSearch_Click"><span class="p-i-search"></span></asp:LinkButton>
                    </td>
                    <td>
                        <asp:LinkButton CssClass="k-button" ID="lbtnDeleteKeyword" ToolTip="Xóa tìm kiếm"
                            runat="server" OnClick="lbtnDeleteKeyword_Click"><span class="p-i-clear"></span></asp:LinkButton>
                    </td>
                    <td>
                        <strong><asp:Label ID="lbtotalContract" runat="server"></asp:Label></strong> 
                    </td>
                </tr>
            </tbody>
        </table>
        </asp:Panel>
    </div>
    <table width="100%" cellpadding="3" cellspacing="3" style="background-color: #f4f4f4;
        border: 1px solid #aecaf0;">
        <tr>
            <td>
                <dx:ASPxGridView ID="ASPxGridView_contract" runat="server" AutoGenerateColumns="False"
                    Width="100%" KeyFieldName="ID" Theme="Aqua"
                    onhtmlrowprepared="ASPxGridView_contract_HtmlRowPrepared" >
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="25px">
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataDateColumn  FieldName="CONT_DELI_DATE" Caption="Ngày giao HĐ" VisibleIndex="1">
                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                            </PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                        <%--<dx:GridViewDataTextColumn VisibleIndex="1" Caption="STT" Width="45px">
                            <DataItemTemplate>
                                <%# Container.ItemIndex + 1 %>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>--%>
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
                        <%--<dx:GridViewDataTextColumn VisibleIndex="1" Caption="Trể" Width="65px">
                            <DataItemTemplate>
                                <%# getCountTre(Eval("ID"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>--%>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tên khách hàng"  Width="250px">
                            <DataItemTemplate>
                                <a href="<%# getLinkCus(Eval("ID_CUS")) %>" title="<%# getnameCus(Eval("ID_CUS"))%>">
                                    <%# getTitle(getnameCus(Eval("ID_CUS")),38) %></a>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Địa chỉ" Width="250px">
                            <DataItemTemplate>                            
                                <span title='<%# getnameAdd(Eval("ID_CUS"))%>'>
                            <%# getTitle(getnameAdd(Eval("ID_CUS")), 38)%>
                                </span>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Số điện thoại" Width="200px">
                            <DataItemTemplate>
                            <span title='<%# getPhone(Eval("ID_CUS"))%>'>
                                <%# getTitle(getPhone(Eval("ID_CUS")),25)%></span>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tên sản phẩm" FieldName="CONT_PRODUCT_NAME" Width="200px">
                            <DataItemTemplate>
                            <span title='<%# Eval("CONT_PRODUCT_NAME")%>'>
                                <%# getTitle(Eval("CONT_PRODUCT_NAME"),28)%></span>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tiền hàng" FieldName="CONT_PRODUCT_PRICE">
                            <PropertiesTextEdit DisplayFormatString="{0:0,0 VNĐ}" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tổng tiền" FieldName="CONT_TOTAL_PRICE" Width="110px">
                            <PropertiesTextEdit DisplayFormatString="{0:0,0 VNĐ}" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tiền đầu" FieldName="CONT_DELI_PRICE">
                            <PropertiesTextEdit DisplayFormatString="{0:0,0 VNĐ}" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Số kỳ góp" FieldName="CONT_WEEK_COUNT" Width="70">
                            <DataItemTemplate>
                                <%# Eval("CONT_WEEK_COUNT")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tiền thu mỗi kỳ" Width="100" FieldName="CONT_WEEK_PRICE">
                            <PropertiesTextEdit DisplayFormatString="{0:0,0 VNĐ}" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tiền thu">
                            <DataItemTemplate>
                                <%#getAllthuPirce(Eval("ID")) %>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tiền thất thoát">
                            <DataItemTemplate>
                                <%#getMoneythattoat(Eval("CONT_DEBT_PRICE"), Eval("ID"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Còn lại" Width="110px">
                            <DataItemTemplate>
                                <%#getMoneyconlai(Eval("CONT_DEBT_PRICE"), Eval("ID"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>                        
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Ghi chú" Width="250px">
                            <DataItemTemplate>
                            <span title='<%# Eval("CONT_NOTE")%>'>
                                <%# getTitle(Eval("CONT_NOTE"), 40)%></span>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Theo dõi góp" Width="250px">
                            <DataItemTemplate>
                            <span title='<%# Eval("CONT_NOTE_TRACK")%>'>
                                <%# getTitle(Eval("CONT_NOTE_TRACK"), 40)%></span>
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
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="NVGH">
                            <DataItemTemplate>
                                <%#getEmp(Eval("EMP_GH"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Công ty">
                            <DataItemTemplate>
                                <%#getCompany(Eval("COMPANY"))%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Giấy tờ" Width="250px">
                            <DataItemTemplate>
                            <span title='<%# Eval("CUS_GT")%>'>
                                <%# getTitle(Eval("CUS_GT"),40)%></span>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Ghi chú khi giao hàng"  Width="250px">
                            <DataItemTemplate>
                            <span title='<%# Eval("CONT_NOTE_DELI")%>'>
                                <%# getTitle(Eval("CONT_NOTE_DELI"),40)%></span>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Nội dung xác minh" Width="250px">
                            <DataItemTemplate>
                            <span title='<%# Eval("CONT_NOTE_XM")%>'>
                            <%# getTitle(Eval("CONT_NOTE_XM"), 40)%>
                                </span>
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
                    <SettingsPager PageSize="50" Mode="ShowAllRecords">
                    </SettingsPager>
                </dx:ASPxGridView>


                <div class="navigation">
                    <asp:Literal ID="ltrPage" runat="server"></asp:Literal>
                    <div style="float:right">
                    Trang
                    <asp:DropDownList ID="ddlPage" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="ddlPage_SelectedIndexChanged">
                    </asp:DropDownList>
                    </div>
                </div>

            </td>
        </tr>
    </table>
</asp:Content>
