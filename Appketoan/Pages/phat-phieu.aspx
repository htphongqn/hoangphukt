<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="phat-phieu.aspx.cs" Inherits="Appketoan.Pages.phat_phieu" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">
         function pageLoad() {
             $(document).ready(function () {
                 $(".tablesorter").tablesorter().tablesorterPager({ container: $("#pager") }); ;
             });

         }
      </script>
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
            Danh Sách Hợp Đồng Cần Phát Phiếu Thu
        </div>
        <div style="clear: both">
        </div>
    </div>
    <div id="div-search">
        <table width="100%">
            <tbody>
                <tr>
                <td style="width:130px">
                <input class="k-textbox k-input search-noidung fill-input" width="130px" id="txtKeyword"
                            name="txtKeyword" type="text" runat="server" placeholder="Nhập số hợp đồng" />
                </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlEmployerSearch" runat="server" CssClass="chosen-select k-textbox textbox" Width="170px">
                    </asp:DropDownList>
                        <asp:DropDownList ID="ddlContractDay" runat="server" CssClass="k-textbox textbox">
                        </asp:DropDownList>
                    
                        <asp:LinkButton CssClass="k-button" ID="lbtnSearch" ToolTip="Tìm kiếm" runat="server"
                            OnClick="lbtnSearch_Click"><span class="p-i-search"></span></asp:LinkButton>
                    </td>
                     <td align="right">
                     <div style="text-align:left;width:395px">
                    <asp:DropDownList ID="ddlEmployer" runat="server" CssClass="chosen-select k-textbox textbox" Width="170px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                    ControlToValidate="ddlEmployer" Display="Dynamic" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error" InitialValue="0">*</asp:RequiredFieldValidator>
                    <asp:LinkButton CssClass="k-button" ID="lbtnPhatphieu" ToolTip="Phát phiếu" runat="server" ValidationGroup="G2" OnClientClick="return confirm('Phát phiếu đã chọn cho nhân viên?');"
                            OnClick="lbtnPhatphieu_Click">Phát phiếu</asp:LinkButton>
                <asp:LinkButton CssClass="k-button" ID="lbtnPhatphieumacdinh" 
                             ToolTip="Phát phiếu mặc định" runat="server" onclick="lbtnPhatphieumacdinh_Click" OnClientClick="return confirm('Phát tất cả các phiếu mặc định cho nhân viên?');"
                            >Phát phiếu mặc định</asp:LinkButton>
                    </div>
                    
                
                </td>
                </tr>
            </tbody>
        </table>
    </div>
    <table width="100%" cellpadding="3" cellspacing="3" style="background-color: #f4f4f4;
        border: 1px solid #aecaf0">
        <tr>
            <td>


                <dx:ASPxGridView ID="ASPxGridView1_phatphieu" runat="server" AutoGenerateColumns="False"
                    Width="100%" Theme="Aqua" 
                    KeyFieldName="ID_CONT"
                    onbeforecolumnsortinggrouping="ASPxGridView1_phatphieu_BeforeColumnSortingGrouping" 
                    onpageindexchanged="ASPxGridView1_phatphieu_PageIndexChanged">
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="45px">
                        </dx:GridViewCommandColumn>
                        <%--<dx:GridViewDataTextColumn VisibleIndex="1" Caption="STT" Width="45px">
                            <DataItemTemplate>
                                <%# Container.ItemIndex + 1 %>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>--%>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Số hợp đồng" FieldName="ID_CONT" Width="125px">
                            <DataItemTemplate>
                                    <a href="<%#getLink(Eval("ID_CONT")) %>">
                                <%# getConNo(Eval("ID_CONT"))%></a>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tên khách hàng" FieldName="ID_CONT">
                            <DataItemTemplate>
                                <a href="<%#getLinkCus(Eval("ID_CONT")) %>">
                                    <%#getnameCus(Eval("ID_CONT"))%></a>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Địa chỉ" FieldName="ID_CONT">
                            <DataItemTemplate>
                                    <%#getaddressCus(Eval("ID_CONT"))%>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Nhân viên thu ngân" FieldName="EMP_TN">
                            <DataItemTemplate>
                                    <%#getnameEmpTN(Eval("EMP_TN"))%>
                                    <asp:HiddenField ID= "hddEmp_TN" runat="server" Value='<%# Eval("EMP_TN")%>' />
                                    <asp:HiddenField ID= "hddConID" runat="server" Value='<%# Eval("ID_CONT")%>' />
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn FieldName="CONTD_DATE_THU" Caption="Ngày thu" VisibleIndex="1">
                            <%--<PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                            </PropertiesDateEdit>--%>
                            <DataItemTemplate>
                            <asp:Label ID="lbDatethu" runat="server" Text='<%# getDateThu(Eval("ID_CONT"),Eval("CONTD_DATE_THU"))%>'></asp:Label>
                            </DataItemTemplate>
                        </dx:GridViewDataDateColumn>
                        <%--<dx:GridViewDataTextColumn VisibleIndex="1" Caption="Kỳ thu" FieldName="EMP_PHONE">
                            <DataItemTemplate>
                                <%# Eval("EMP_PHONE")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>--%>
                        
                    </Columns>
                    <%--<Settings ShowHorizontalScrollBar="true" />--%>
                    <Settings VerticalScrollableHeight="350" />
                    <Settings ShowVerticalScrollBar="true" />
                    <SettingsPager PageSize="30">
                    </SettingsPager>
                </dx:ASPxGridView>
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