<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="nhan-phieu.aspx.cs" Inherits="Appketoan.Pages.nhan_phieu" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    function FormatNumber(obj) {
        var strvalue;
        if (eval(obj))
            strvalue = eval(obj).value;
        else
            strvalue = obj;
        var num;
        num = strvalue.toString().replace(/\$|\,/g, '');
        if (isNaN(num))
            num = "";
        sign = (num == (num = Math.abs(num)));
        num = Math.floor(num * 100 + 0.50000000001);
        num = Math.floor(num / 100).toString();
        for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
            num = num.substring(0, num.length - (4 * i + 3)) + ',' +
   num.substring(num.length - (4 * i + 3));
        //return (((sign)?'':'-') + num); 
        eval(obj).value = (((sign) ? '' : '-') + num);
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMain" runat="server">
    <div id="header">
        <div class="title">
            Danh Sách Hợp Đồng Cần Nhận Phiếu Thu
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
                <td>
                <asp:LinkButton CssClass="k-button" ID="lbtnSearch" ToolTip="Tìm kiếm" runat="server"
                            OnClick="lbtnSearch_Click"><span class="p-i-search"></span></asp:LinkButton>
                        <asp:LinkButton CssClass="k-button" ID="lbtnNhanphieu" ToolTip="Nhận phiếu" runat="server" ValidationGroup="G2"
                            OnClick="lbtnNhanphieu_Click" OnClientClick="return confirm('Nhận phiếu đã chọn?');">Nhận phiếu</asp:LinkButton>
                            <asp:LinkButton CssClass="k-button" ID="LinkButton1" ToolTip="Mất phiếu" runat="server" ValidationGroup="G2"
                            OnClick="lbtnMatphieu_Click" OnClientClick="return confirm('Mất phiếu đã chọn?');">Mất phiếu</asp:LinkButton>
                    </td>                     
                </tr>
            </tbody>
        </table>
    </div>
    <table width="100%" cellpadding="3" cellspacing="3" style="background-color: #f4f4f4;
        border: 1px solid #aecaf0">
        <tr>
            <td>
                <dx:ASPxGridView ID="ASPxGridView1_nhanphieu" runat="server" AutoGenerateColumns="False"
                    Width="100%" KeyFieldName="ID" Theme="Aqua" 
                    onbeforecolumnsortinggrouping="ASPxGridView1_nhanphieu_BeforeColumnSortingGrouping">
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="25px">
                        </dx:GridViewCommandColumn>
                        <%--<dx:GridViewDataTextColumn VisibleIndex="1" Caption="STT" Width="45px">
                            <DataItemTemplate>
                                <%# Container.ItemIndex + 1 %>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>--%>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tiền thu" Width="200px" FieldName="BILL_STATUS">
                            <HeaderTemplate>
                            Tiền thu
                            <%--<asp:CheckBox ID="CheckBox1" runat="server" Checked="true" Text="Phiếu tốt" />
                                <asp:CheckBox ID="CheckBox2" runat="server"  Text="Phiếu rớt"/>--%>
                            </HeaderTemplate>
                            <DataItemTemplate>
                                <%--<asp:CheckBox ID="chkStatusPhieu" runat="server" Checked="true" />--%>
                                <asp:TextBox ID="txtPayprice" runat="server" Text='<%# getTienthu(Eval("ID_CONT"))%>' class="text" CssClass="k-textbox textbox" onkeyup="FormatNumber(this);" onblur="FormatNumber(this);" Width="100"></asp:TextBox>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Số hợp đồng" FieldName="ID_CONT">
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
                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tên nhân viên" FieldName="ID_CONT">
                            <DataItemTemplate>
                                <a href="<%#getLinkEmp(Eval("ID_EMPLOY")) %>">
                                    <%#getnameEmp(Eval("ID_EMPLOY"))%></a>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <%--<dx:GridViewDataTextColumn VisibleIndex="1" Caption="Kỳ thu" FieldName="EMP_PHONE">
                            <DataItemTemplate>
                                <%# Eval("EMP_PHONE")%>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>--%>
                        
                    </Columns>
                    <SettingsPager PageSize="100">
                    </SettingsPager>
                </dx:ASPxGridView>
            </td>
        </tr>
    </table>
</asp:Content>