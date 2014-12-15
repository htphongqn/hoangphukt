<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true"
    CodeBehind="chi-tiet-hop-dong.aspx.cs" Inherits="Appketoan.Pages.chi_tiet_hop_dong" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Src="../Calendar/pickerAndCalendar.ascx" TagName="pickerAndCalendar"
    TagPrefix="uc1" %>
<%@ Register src="../UIs/MessageBox.ascx" tagname="MessageBox" tagprefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMain" runat="server">
    <style type="text/css">
        #tblInfo td
        {
            padding:5px;
        }
        .td_left
        {
            width: 30%;
            padding-right: 50px;
        }
            
        .textbox
        {
            width: 400px;
            top: 0px;
            left: 0px;
            padding:3px;
        }
        .PnlDesign
        {
            /*
            min-width: 156px;
            max-height:150px;*/
            border: solid 1px #94C0D2;
            height: 200px;
            width: 400px;            
            overflow-y:scroll;          
            background-color: #ffffff;
        }
        .txtbox
        {
            min-width: 150px;
            background-image: url('../Images/drpdwn.png');
            background-position: right center;
            background-repeat: no-repeat;
            cursor: pointer;
            cursor: hand;
        }
    </style>
    <script type="text/javascript">

        function getno() {
            var getTotalprice = document.getElementById("<%=txtToltalprice.ClientID %>");
            var getDeliprice = document.getElementById("<%=txtdeli_price_pro.ClientID %>");
            var getno = document.getElementById("<%=txtBebtprice.ClientID %>");
            var total = getTotalprice.value.toString().replace(/\$|\,/g, '');
            var deli = getDeliprice.value.toString().replace(/\$|\,/g, '');
            //getno.value = parseInt((total - deli) / 1000) * 1000;
            getno.value = Math.round((total - deli) / 100) * 100;
            var num;
            num = getno.value.toString().replace(/\$|\,/g, '');
            if (isNaN(num))
                num = "";
            sign = (num == (num = Math.abs(num)));
            num = Math.floor(num * 100 + 0.50000000001);
            num = Math.floor(num / 100).toString();
            for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
                num = num.substring(0, num.length - (4 * i + 3)) + ',' +
   num.substring(num.length - (4 * i + 3));
            //return (((sign)?'':'-') + num); 
            getno.value = (((sign) ? '' : '-') + num);
        }
        function getgopmoiky() {
            var getconno = document.getElementById("<%=txtBebtprice.ClientID %>");
            var getkygop = document.getElementById("<%=txtweeKcount.ClientID %>");
            var getmoiky = document.getElementById("<%=txtPayprice_week.ClientID %>");
            var conno = getconno.value.toString().replace(/\$|\,/g, '');
            var moiky = getmoiky.value.toString().replace(/\$|\,/g, '');
            //getmoiky.value = parseInt((conno / kygop) / 1000) * 1000;
            if (moiky > 0)
                getkygop.value = Math.round(conno / moiky);
            else
                getkygop.value = 0;
            var num;
            num = getkygop.value.toString().replace(/\$|\,/g, '');
            if (isNaN(num))
                num = "";
            sign = (num == (num = Math.abs(num)));
            num = Math.floor(num * 100 + 0.50000000001);
            num = Math.floor(num / 100).toString();
            for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++)
                num = num.substring(0, num.length - (4 * i + 3)) + ',' +
   num.substring(num.length - (4 * i + 3));
            //return (((sign)?'':'-') + num); 
            getkygop.value = (((sign) ? '' : '-') + num);
        }
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

    <link rel="stylesheet" href="../chosen/docsupport/style.css">
    <link rel="stylesheet" href="../chosen/docsupport/prism.css">
    <link rel="stylesheet" href="../chosen/chosen.css">
    <style type="text/css" media="all">
        /* fix rtl for demo */
        .chosen-rtl .chosen-drop { left: -9000px; }
    </style>

   <div id="header" style="padding-bottom: 5px;">
        <div class="title">
            Chi Tiết Hợp Đồng
        </div>
        <div class="toolbar" style="padding-bottom: 5px">
            &nbsp;<asp:LinkButton ID="lbtnSave" ToolTip="Lưu thông tin" CssClass="k-button" runat="server"
                OnClick="lbtnSave_Click" ValidationGroup="G2"><img alt="Lưu thông tin" src="../Images/icon-32-save.png" /></asp:LinkButton>
            &nbsp;<asp:LinkButton ID="lbtnSaveClose" ToolTip="Lưu và đóng" CssClass="k-button"
                runat="server" OnClick="lbtnSaveClose_Click" ValidationGroup="G2"><img alt="Lưu thông tin" src="../Images/icon-32-saveclose.png" /></asp:LinkButton>
            &nbsp;<asp:LinkButton ID="lbtnSaveNew" ToolTip="Lưu và thêm mới" CssClass="k-button"
                runat="server" OnClick="lbtnSaveNew_Click" ValidationGroup="G2"><img alt="Lưu thông tin" src="../Images/icon-32-save-new.png" /></asp:LinkButton>
            &nbsp;<%--<asp:LinkButton ID="lbtnDelete" ToolTip="Xóa" CssClass="k-button" runat="server"
                OnClick="lbtnDelete_Click"><img alt="Xóa" src="../Images/icon-32-trash.png" /></asp:LinkButton>--%>
            &nbsp;<asp:LinkButton ID="lbtnClose" ToolTip="Đóng" CssClass="k-button" runat="server"
                OnClick="lbtnClose_Click"><img alt="Đóng" src="../Images/icon-32-cancel.png" /></asp:LinkButton>
        </div>
    </div>
    <table width="100%" cellpadding="3" cellspacing="3" style="background-color: #f4f4f4;
        border: 1px solid #aecaf0">
        <tr>
            <td>
                <dx:ASPxPageControl ID="ASPxPageControl2" runat="server" ActiveTabIndex="0" CssFilePath="~/App_Themes/Aqua/{0}/styles.css"
                    CssPostfix="Aqua" SpriteCssFilePath="~/App_Themes/Aqua/{0}/sprite.css" TabSpacing="3px"
                    Height="100%" Width="100%">
                    <TabPages>
                        <dx:TabPage Text="Thông tin chung">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl1" runat="server" SupportsDisabledAttribute="True">
                                    <table width="100%" border="0" id="tblInfo">
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" ShowMessageBox="True"
                                                    ShowSummary="False" ValidationGroup="G2" />
                                                <asp:Label ID="Lberrors" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Số hợp đồng
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtNocontract" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Chưa nhập số hợp đồng"
                                                    ControlToValidate="txtNocontract" Display="Dynamic" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Loại hợp đồng
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:RadioButtonList ID="Rdtypecontract" runat="server" RepeatColumns="3">
                                                    <asp:ListItem Value="1" Selected="True" Text="1 tuần"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="2 tuần"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="1 tháng"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                <asp:Label ID="lbtypecontract" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Khách hàng
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:DropDownList ID="Drcustomer" runat="server" Width="400" CssClass="chosen-select k-textbox textbox" class="chosen-select">
                                                </asp:DropDownList>
                                                <asp:Label ID="lbcustomer" runat="server"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Chưa chọn khách hàng"
                    ControlToValidate="Drcustomer" Display="Dynamic" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error" InitialValue="0">*</asp:RequiredFieldValidator>
                                                <%--<a href="chi-tiet-khach-hang.aspx" title="Thêm khách hàng" class="k-button"><span
                class="p-i-add"></span></a>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Sản phẩm góp
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtNameproduct" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Chưa nhập tên sản phẩm"
                                                    ControlToValidate="txtNameproduct" Display="Dynamic" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Tiền hàng
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtProductprice" runat="server" class="text" CssClass="k-textbox textbox" onkeyup="FormatNumber(this);" onblur="FormatNumber(this);"></asp:TextBox>
                                                <asp:Label ID="lbProductprice" runat="server"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Chưa nhập tiền hàng"
                                                    ControlToValidate="txtProductprice" Display="Dynamic" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Tổng giá trị
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtToltalprice" runat="server" class="text" CssClass="k-textbox textbox" onkeyup="FormatNumber(this);" onblur="FormatNumber(this);"></asp:TextBox>
                                                <asp:Label ID="lbToltalprice" runat="server"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Chưa nhập tổng giá trị sản phẩm"
                                                    ControlToValidate="txtToltalprice" Display="Dynamic" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Quy định trả trước
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtPrepayprice" runat="server" class="text" CssClass="k-textbox textbox" onkeyup="FormatNumber(this);" onblur="FormatNumber(this);"></asp:TextBox>
                                                <asp:Label ID="lbPrepayprice" runat="server"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Chưa nhập tiền trả trước"
                                                    ControlToValidate="txtPrepayprice" Display="Dynamic" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Ngày giao hàng
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <uc1:pickerAndCalendar ID="pickdate_deli" runat="server" />
                                                <asp:Label ID="lbpickdate_deli" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Tiền đã nhận khi giao hàng
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtdeli_price_pro" runat="server" class="text" CssClass="k-textbox textbox" onkeyup="FormatNumber(this);getno();" onblur="FormatNumber(this);getno();"
                                                    onchange="getno();"></asp:TextBox>
                                                    <asp:Label ID="lbdeli_price_pro" runat="server"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Chưa nhập tiền đã nhận khi giao hàng"
                                                    ControlToValidate="txtdeli_price_pro" Display="Dynamic" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Còn nợ
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtBebtprice" runat="server" class="text" CssClass="k-textbox textbox" onkeyup="FormatNumber(this);" onblur="FormatNumber(this);"></asp:TextBox>
                                                <asp:Label ID="lbBebtprice" runat="server"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Chưa nhập tiền nợ"
                                                    ControlToValidate="txtBebtprice" Display="Dynamic" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Tiền góp mỗi kỳ
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtPayprice_week" runat="server" class="text" CssClass="k-textbox textbox" onkeyup="FormatNumber(this);getgopmoiky();" onblur="FormatNumber(this);getgopmoiky();" onchange="getgopmoiky();"></asp:TextBox>
                                                <asp:Label ID="lbPayprice_week" runat="server"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Chưa nhập tiền mỗi tuần/tháng"
                                                    ControlToValidate="txtPayprice_week" Display="Dynamic" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Số kỳ góp
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtweeKcount" runat="server" class="text" 
                                                    CssClass="k-textbox textbox" 
                                                    onkeyup="FormatNumber(this);" onblur="FormatNumber(this);"
                                                     MaxLength="2"></asp:TextBox>
                                                    <asp:Label ID="lbweeKcount" runat="server"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Chưa nhập số tuần/tháng góp"
                                                    ControlToValidate="txtweeKcount" Display="Dynamic" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        
                                        <tr id="trContractStatus" runat="server">
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Tình trạng hợp đồng
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:RadioButtonList ID="Rdstatuscontract" runat="server" RepeatColumns="3">
                                                    <asp:ListItem Value="2" Selected="True" Text="Còn góp"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Thanh lý"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="Dựt nợ"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Ghi chú
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtremarkcontract" runat="server" TextMode="MultiLine" Rows="5" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Theo dõi góp
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtNoteTrack" runat="server" TextMode="MultiLine" Rows="5" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;"></span>&nbsp;NVBH
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <%--<asp:DropDownList ID="ddlNVBH" runat="server" CssClass="k-textbox textbox">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Chưa chọn NVBH"
                    ControlToValidate="ddlNVBH" Display="Dynamic" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error" InitialValue="0">*</asp:RequiredFieldValidator>--%>

                                            <%--<asp:TextBox ID="txtEmployeeBH" Text="--- Chọn nhân viên bán hàng ---" runat="server" class="text" CssClass="k-textbox textbox txtbox txtTable"></asp:TextBox>
                                            <asp:Panel ID="pnlEmployeeBH" runat="server" CssClass="PnlDesign txtTable">
                                                <asp:CheckBoxList ID="chkEmployeeBH" runat="server" DataTextField="Emp_Name" 
                                                    DataValueField="Id" RepeatColumns="3">
                                                </asp:CheckBoxList>
                                                <div>
                                                <asp:Button ID="btnApplyEmployeeBH" runat="server" Text="Đồng ý" onclick="btnApplyEmployeeBH_Click" />
                                                <asp:Button ID="btnCancelEmployeeBH" runat="server" Text="Đóng" onclick="btnCancelEmployeeBH_Click" /></div>
                                            </asp:Panel>
                                            <asp:PopupControlExtender ID="txtEmployeeBH_PopupControlExtender" 
                                                runat="server" PopupControlID="pnlEmployeeBH" Position="Bottom" 
                                                TargetControlID="txtEmployeeBH"></asp:PopupControlExtender>--%>
                                                <asp:DropDownList ID="ddlEmployeeBH" runat="server" CssClass="chosen-select k-textbox textbox" DataTextField="Emp_Name" DataValueField="Id" data-placeholder="--- Chọn nhân viên bán hàng ---" class="chosen-select" multiple>
                                                </asp:DropDownList>
                                                <asp:Label ID="lbEmployeeBH" runat="server"></asp:Label>   
                                                <asp:HiddenField ID="HiddenEmployeeBH" runat="server" /> 
                            
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;"></span>&nbsp;NVXM
                                            </td>
                                            <td align="left" nowrap="nowrap">
<%--                                                <asp:DropDownList ID="ddlNVXM" runat="server" CssClass="k-textbox textbox">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Chưa chọn NVXM"
                    ControlToValidate="ddlNVXM" Display="Dynamic" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error" InitialValue="0">*</asp:RequiredFieldValidator>--%>

                                            <%--<asp:TextBox ID="txtEmployeeXM" Text="--- Chọn nhân viên xác minh ---" runat="server" class="text" CssClass="k-textbox textbox txtbox txtTable"></asp:TextBox>
                                            <asp:Panel ID="pnlEmployeeXM" runat="server" CssClass="PnlDesign txtTable">
                                                <asp:CheckBoxList ID="chkEmployeeXM" runat="server" DataTextField="Emp_Name" 
                                                    DataValueField="Id" RepeatColumns="3">
                                                </asp:CheckBoxList>
                                                <div>
                                                <asp:Button ID="btnApplyEmployeeXM" runat="server" Text="Đồng ý" onclick="btnApplyEmployeeXM_Click" />
                                                <asp:Button ID="btnCancelEmployeeXM" runat="server" Text="Đóng" onclick="btnCancelEmployeeXM_Click" /></div>
                                            </asp:Panel>
                                            <asp:PopupControlExtender ID="txtEmployeeXM_PopupControlExtender" 
                                                runat="server" PopupControlID="pnlEmployeeXM" Position="Bottom" 
                                                TargetControlID="txtEmployeeXM"></asp:PopupControlExtender> --%>
                                                <asp:DropDownList ID="ddlEmployeeXM" runat="server" CssClass="chosen-select k-textbox textbox" DataTextField="Emp_Name" DataValueField="Id" data-placeholder="--- Chọn nhân viên xác minh ---" class="chosen-select" multiple>
                                                </asp:DropDownList>
                                                <asp:Label ID="lbEmployeeXM" runat="server"></asp:Label>   
                                                <asp:HiddenField ID="HiddenEmployeeXM" runat="server" />  
                            
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;"></span>&nbsp;NVGH
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <%--<asp:DropDownList ID="ddlNVGH" runat="server" CssClass="k-textbox textbox">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Chưa chọn NVGH"
                    ControlToValidate="ddlNVGH" Display="Dynamic" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error" InitialValue="0">*</asp:RequiredFieldValidator>--%>

                                            <%--<asp:TextBox ID="txtEmployeeGH" Text="--- Chọn nhân viên giao hàng ---" runat="server" class="text" CssClass="k-textbox textbox txtbox txtTable"></asp:TextBox>
                                                <asp:Panel ID="pnlEmployeeGH" runat="server" CssClass="PnlDesign txtTable">
                                                    <asp:CheckBoxList ID="chkEmployeeGH" runat="server" DataTextField="Emp_Name" 
                                                        DataValueField="Id" RepeatColumns="3">
                                                    </asp:CheckBoxList>
                                                    <div>
                                                    <asp:Button ID="btnApplyEmployeeGH" runat="server" Text="Đồng ý" onclick="btnApplyEmployeeGH_Click" />
                                                    <asp:Button ID="btnCancelEmployeeGH" runat="server" Text="Đóng" onclick="btnCancelEmployeeGH_Click" /></div>
                                                </asp:Panel>
                                                
                                                <asp:PopupControlExtender ID="txtEmployeeGH_PopupControlExtender" 
                                                    runat="server" PopupControlID="pnlEmployeeGH" Position="Bottom" 
                                                    TargetControlID="txtEmployeeGH"></asp:PopupControlExtender> --%>

                                                    <asp:DropDownList ID="ddlEmployeeGH" runat="server" CssClass="chosen-select k-textbox textbox" DataTextField="Emp_Name" DataValueField="Id" data-placeholder="--- Chọn nhân viên giao hàng ---" class="chosen-select" multiple>
                                                </asp:DropDownList>
                                                <asp:Label ID="lbEmployeeGH" runat="server"></asp:Label>   
                                                <asp:HiddenField ID="HiddenEmployeeGH" runat="server" />  
                         
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Giấy tờ
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtCus_gt" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Ghi chú khi giao hàng
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtNote_deli" runat="server" TextMode="MultiLine" Rows="5" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Nội dung xác minh
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtNoteXM" runat="server" TextMode="MultiLine" Rows="5" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="Phiếu thu">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl2" runat="server">
                                    <table width="100%" border="0" cellspacing="1" cellpadding="3">
                                        <%--<tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ForeColor="Red" ShowMessageBox="True"
                                                    ShowSummary="False" ValidationGroup="G4" />
                                                <asp:Label ID="Label2" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Ngày giao phiếu
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <uc1:pickerAndCalendar ID="txtdate_deli_phieu" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Nhân viên
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:DropDownList ID="Dremployer" runat="server" CssClass="k-textbox textbox">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td>
                                             <%--<asp:LinkButton ID="lbaddphieu" runat="server" ToolTip="Giao phiếu" class="k-button" ValidationGroup="G4" OnClick="lbaddphieu_Click"><span class="p-i-add"></span></asp:LinkButton>--%>
                                        <%--<asp:LinkButton ID="lbsavephieu" ToolTip="Cập nhật" CssClass="k-button" runat="server"
                                                    OnClick="lbsavephieu_Click"><img alt="Lưu thông tin" src="../Images/icon-32-save.png" /></asp:LinkButton>--%>
                                                <%--<asp:LinkButton ID="lbdelete_phieu" ToolTip="Xóa" CssClass="k-button" runat="server"
                                                    OnClick="lbdelete_phieu_Click"><img alt="Xóa" src="../Images/icon-32-trash.png" /></asp:LinkButton>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxGridView ID="ASPxGridView_phieu" runat="server" AutoGenerateColumns="False"
                                                    Width="100%" KeyFieldName="ID" Theme="Aqua">
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Visible="false">
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="STT" Width="45px">
                                                            <DataItemTemplate>
                                                                <%# Container.ItemIndex + 1 %>
                                                            </DataItemTemplate>
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tên nhân viên">
                                                            <DataItemTemplate>
                                                                <%# getNameemployer(Eval("ID_EMPLOY"))%>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Ngày phát phiếu">
                                                            <DataItemTemplate>
                                                                <%# getDate(Eval("BILL_DELI_DATE"))%>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn VisibleIndex="1" FieldName="BILLL_RECEIVER_DATE" Caption="Ngày nhận phiếu">
                                                            <DataItemTemplate>
                                                                    <%--<asp:TextBox ID="txtdate_receiver_phieu" runat="server" Width="100" Text='<%# getDate(Eval("BILLL_RECEIVER_DATE"))%>'></asp:TextBox>--%>
                                                <%# getDate(Eval("BILLL_RECEIVER_DATE"))%>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn VisibleIndex="1" FieldName="CONTD_DATE_THU" Caption="Ngày thu">
                                                            <DataItemTemplate>
                                                <%# getDate(Eval("CONTD_DATE_THU"))%>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn VisibleIndex="1" FieldName="BILL_STATUS"  Caption="Trạng thái phiếu">
                                                        <%--<HeaderTemplate>
                                                        Tình trạng(<asp:CheckBox ID="CheckBox1" runat="server" Checked="true" Text="Phiếu tốt" />
                                                            <asp:CheckBox ID="CheckBox2" runat="server"  Text="Phiếu rớt"/>)
                                                        </HeaderTemplate>--%>
                                                            <DataItemTemplate>
                                                                <%--<asp:CheckBox ID="chkStatusPhieu" runat="server" Checked='<%# getStatusphieu(Eval("BILL_STATUS"))%>' />--%>
                                                                <%# getStatusphieuName(Eval("BILL_STATUS"))%>
                                                            </DataItemTemplate>
                                                            <CellStyle HorizontalAlign="Left">
                                                            </CellStyle>
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsPager PageSize="100">
                                                    </SettingsPager>
                                                </dx:ASPxGridView>
                                            </td>
                                        </tr>
                                    </table>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="Thu theo kỳ">
                            <ContentCollection>
                                <dx:ContentControl ID="tab_ser" runat="server">
                                    <table width="100%" border="0" cellspacing="1" cellpadding="3">
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ForeColor="Red" ShowMessageBox="True"
                                                    ShowSummary="False" ValidationGroup="G3" />
                                                <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <%--<span style="color: Red;">*</span>&nbsp;Ngày thu--%>
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <%--<uc1:pickerAndCalendar ID="pickdate_thu" runat="server" />--%>
                                                <%--<asp:Label ID="lbdate_thu" runat="server" Text="dd/mm/yyyy"></asp:Label>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <%--<span style="color: Red;">*</span>&nbsp;Tiền góp--%>
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                            <%--<asp:Label ID="lbPaygop" runat="server" Text="00 VNĐ"></asp:Label>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:LinkButton ID="lkbtnSavethu" ToolTip="Nhận tiền không cần phát phiếu thu" Text="Nhận tiền" CssClass="k-button" runat="server" CausesValidation="false" OnClick="LinkButton2_Click"></asp:LinkButton>
                                                    <asp:LinkButton ID="lkbtnSavenothu" ToolTip="Chưa nhận tiền" Text="Chưa nhận tiền" CssClass="k-button" runat="server"  OnClick="LinkButton3_Click"></asp:LinkButton>
                                               
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <dx:ASPxGridView ID="ASPxGridView_contractdetail" runat="server" AutoGenerateColumns="False"
                                                    Width="100%" KeyFieldName="ID" Theme="Aqua">
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ShowSelectCheckbox="true" VisibleIndex="0" Width="45px">
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="STT" Width="45px">
                                                            <DataItemTemplate>
                                                                <%# Container.ItemIndex + 1 %>
                                                            </DataItemTemplate>
                                                            <CellStyle HorizontalAlign="Center">
                                                            </CellStyle>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Ngày thu">
                                                            <DataItemTemplate>
                                                                <%--<a href="<%# getLink(Eval("ID")) %>">--%>
                                                                    <%# getDate(Eval("CONTD_DATE_THU"))%>
                                                                    <%--</a>--%>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Ngày thu thực tế">
                                                            <DataItemTemplate>
                                                                    <%# getDate(Eval("CONTD_DATE_THU_TT"))%>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tiền thu">
                                                            <DataItemTemplate>
                                                                <%# formatMoney(Eval("CONTD_PAY_PRICE"))%>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Còn nợ">
                                                            <DataItemTemplate>
                                                                <%# getConnoTT(Eval("CONTD_PAY_PRICE"))%>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <%--<dx:GridViewDataTextColumn VisibleIndex="1" Caption="Còn nợ">
                                                            <DataItemTemplate>
                                                                <%# formatMoney(Eval("CONTD_DEBT_PRICE"))%>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>--%>                                                        
                                                    </Columns>
                                                    <SettingsPager PageSize="100">
                                                    </SettingsPager>                                                    
                                                </dx:ASPxGridView>
                                            </td>
                                        </tr>
                                    </table>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="Lịch sử quá trình chuyển đổi">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl3" runat="server">
                                    <table width="100%" border="0" cellspacing="1" cellpadding="3">
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:ValidationSummary ID="ValidationSummary4" runat="server" ForeColor="Red" ShowMessageBox="True"
                                                    ShowSummary="False" ValidationGroup="G5" />
                                                <asp:Label ID="Label3" runat="server" Text="" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Ngày chuyển đổi
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <uc1:pickerAndCalendar ID="pickdateconvert" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Loại hợp đồng
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:RadioButtonList ID="Rdtypecont_convert" runat="server" RepeatColumns="3">
                                                    <asp:ListItem Value="1" Selected="True" Text="1 tuần"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="2 tuần"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="1 tháng"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lbaddConvert" ToolTip="Lưu và thêm mới" CssClass="k-button" runat="server"
                                                    ValidationGroup="G5" OnClick="lbaddConvert_Click"><img alt="Lưu thông tin" src="../Images/icon-32-save-new.png" /></asp:LinkButton>
                                                <%--<asp:LinkButton ID="lbDelteconvert" ToolTip="Xóa" CssClass="k-button" runat="server"
                                                    OnClick="lbDelteconvert_Click"><img alt="Xóa" src="../Images/icon-32-trash.png" /></asp:LinkButton>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <dx:ASPxGridView ID="ASPxGridView_historyConvert" runat="server" AutoGenerateColumns="False"
                                                    Width="100%" KeyFieldName="ID" Theme="Aqua">
                                                    <Columns>
                                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Visible="false">
                                                        </dx:GridViewCommandColumn>
                                                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Ngày chuyển đổi" Width="200px">
                                                            <DataItemTemplate>
                                                                <%--<a href="<%# getLinkConvert(Eval("ID")) %>">--%>
                                                                    <%# getDate(Eval("CONTHIS_TRANSFER_DATE"))%>
                                                                    <%--</a>--%>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Loại hợp đồng">
                                                            <DataItemTemplate>
                                                                <%# getstatusContrachis(Eval("CONTHIS_TYPE"))%>
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataTextColumn>
                                                    </Columns>
                                                    <SettingsPager PageSize="100">
                                                    </SettingsPager>
                                                </dx:ASPxGridView>
                                            </td>
                                        </tr>
                                    </table>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>
                    <LoadingPanelImage Url="~/App_Themes/Aqua/Web/Loading.gif">
                    </LoadingPanelImage>
                    <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />

<Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px"></Paddings>

                    <ContentStyle>
                        <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
<Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px"></Border>
                    </ContentStyle>
                </dx:ASPxPageControl>
            </td>
        </tr>
        <tr>
            <td>
                
            </td>
        </tr>
    </table>
    <uc2:MessageBox ID="MessageBox1" runat="server"></uc2:MessageBox>

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
<script type="text/javascript">
    $(document).ready(function () {
        $("#<%=ddlEmployeeBH.ClientID %>").change(function () {
            var arr = $(this).val();
            //    console.log(arr)
            var empBH = document.getElementById("<%=HiddenEmployeeBH.ClientID %>");
            empBH.value = arr;
            //alert(empGH.value);
        })
        $("#<%=ddlEmployeeXM.ClientID %>").change(function () {
            var arr = $(this).val();
            //    console.log(arr)
            var empXM = document.getElementById("<%=HiddenEmployeeXM.ClientID %>");
            empXM.value = arr;
            //alert(empGH.value);
        })
        $("#<%=ddlEmployeeGH.ClientID %>").change(function () {
            var arr = $(this).val();
            //    console.log(arr)
            var empGH = document.getElementById("<%=HiddenEmployeeGH.ClientID %>");
            empGH.value = arr;
            //alert(empGH.value);
        })

    });

  </script>
</asp:Content>
