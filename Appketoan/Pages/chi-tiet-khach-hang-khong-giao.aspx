<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="chi-tiet-khach-hang-khong-giao.aspx.cs" Inherits="Appketoan.Pages.chi_tiet_khach_hang_khong_giao" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Src="../Calendar/pickerAndCalendar.ascx" TagName="pickerAndCalendar"
    TagPrefix="uc1" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMain" runat="server">
<style type="text/css">
        .td_left
        {
            width: 30%;
            padding-right: 50px;
        }
        .textbox
        {
            width: 400px;
        }
    </style>
    <link rel="stylesheet" href="../chosen/docsupport/style.css">
    <link rel="stylesheet" href="../chosen/docsupport/prism.css">
    <link rel="stylesheet" href="../chosen/chosen.css">
    <style type="text/css" media="all">
        /* fix rtl for demo */
        .chosen-rtl .chosen-drop { left: -9000px; }
    </style>

    <div id="header" style="padding-bottom: 5px;">
        <div class="title">
            Chi Tiết Khách Hàng
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
                                    <table width="100%" border="0" cellspacing="1" cellpadding="3">
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
                                                <span style="color: Red;">*</span>&nbsp;Ngày FAX HĐ
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <uc1:pickerAndCalendar ID="pickdatefax" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Họ tên
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="Txtname" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ServiceMethod="SearchFullname" 
                                                    MinimumPrefixLength="1"
                                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" 
                                                    TargetControlID="Txtname"
                                                    ID="AutoCompleteExtender1" runat="server" FirstRowSelected = "false">
                                                </cc1:AutoCompleteExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Chưa nhập họ tên"
                                                    ControlToValidate="Txtname" Display="Dynamic" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        
                                       
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Số điện thoại
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="Txtphone" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ServiceMethod="SearchPhone" 
                                                    MinimumPrefixLength="3"
                                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" 
                                                    TargetControlID="Txtphone"
                                                    ID="AutoCompleteExtender2" runat="server" FirstRowSelected = "false">
                                                </cc1:AutoCompleteExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Chưa nhập số điện thoại"
                                                    ControlToValidate="Txtphone" Display="Dynamic" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                &nbsp;Địa chỉ
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="Txtaddress" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ServiceMethod="SearchAddress" 
                                                    MinimumPrefixLength="3"
                                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" 
                                                    TargetControlID="Txtaddress"
                                                    ID="AutoCompleteExtender3" runat="server" FirstRowSelected = "false">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;"></span>&nbsp;Sản phẩm
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="Txtproduct" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;"></span>&nbsp;NVBH
                                            </td>
                                            <td align="left" nowrap="nowrap">
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
                                                <asp:DropDownList ID="ddlEmployeeXM" runat="server" CssClass="chosen-select k-textbox textbox" DataTextField="Emp_Name" DataValueField="Id" data-placeholder="--- Chọn nhân viên xác minh ---" class="chosen-select" multiple>
                                                </asp:DropDownList>
                                                <asp:Label ID="lbEmployeeXM" runat="server"></asp:Label>   
                                                <asp:HiddenField ID="HiddenEmployeeXM" runat="server" />  
                            
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Biên bản xác minh
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtNoteXM" runat="server" TextMode="MultiLine" Rows="5" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Tình trạng
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:RadioButtonList ID="rdbStatus" runat="server" RepeatColumns="2">
                                                <asp:ListItem Value="0" Text="Chưa xử lý" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Đã xử lý"></asp:ListItem>
                                                </asp:RadioButtonList>
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
                &nbsp;
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

    });

  </script>

</asp:Content>
