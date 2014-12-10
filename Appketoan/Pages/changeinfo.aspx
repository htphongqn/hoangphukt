<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true"
    CodeBehind="changeinfo.aspx.cs" Inherits="Appketoan.Pages.changeinfo" %>

<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx1" %>
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
    <%-- <script type="text/javascript">
        window.setTimeout(function () {
            var passFields = document.querySelectorAll("input[type='password']");
            if (!passFields.length) return;
            for (var i = 0; i < passFields.length; i++) {
                passFields[i].addEventListener("mouseover", function () {
                    this.type = "text";
                }, false);
                passFields[i].addEventListener("mouseout", function () {
                    this.type = "password";
                }, false);
            }
        }, 1000)
    </script>--%>
    <div id="header" style="padding-bottom: 5px;">
        <div class="title">
            Thông tin tài khoản
        </div>
        <div class="toolbar" style="padding-bottom: 5px">
            &nbsp;<asp:LinkButton ID="lbtnSave" ToolTip="Lưu thông tin" CssClass="k-button" runat="server"
                OnClick="lbtnSave_Click"><img alt="Lưu thông tin" src="../Images/icon-32-save.png" /></asp:LinkButton>
            &nbsp;<asp:LinkButton ID="lbtnSaveClose" ToolTip="Lưu và đóng" CssClass="k-button"
                runat="server" OnClick="lbtnSaveClose_Click"><img alt="Lưu thông tin" src="../Images/icon-32-saveclose.png" /></asp:LinkButton>
            &nbsp;<asp:LinkButton ID="lbtnSaveNew" ToolTip="Lưu và thêm mới" CssClass="k-button"
                runat="server" OnClick="lbtnSaveNew_Click"><img alt="Lưu thông tin" src="../Images/icon-32-save-new.png" /></asp:LinkButton>
            &nbsp;<asp:LinkButton ID="lbtnDelete" ToolTip="Xóa" CssClass="k-button" runat="server"
                OnClick="lbtnDelete_Click"><img alt="Xóa" src="../Images/icon-32-trash.png" /></asp:LinkButton>
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
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Tên đăng nhập
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtUserName" runat="server" CssClass="k-textbox textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="trPass" runat="server">
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Mật khẩu
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="k-textbox textbox" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="trRepass" runat="server">
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Nhập lại mật khẩu
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtRePassword" TextMode="Password" CssClass="k-textbox textbox"
                                                    runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Email
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtEmail" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Họ tên
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtUserFullName" CssClass="k-textbox textbox" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Mã người dùng
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtCode" CssClass="k-textbox textbox" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span> &nbsp;Địa chỉ
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtAddress" runat="server" CssClass="k-textbox textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Điện thoại
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtPhone" runat="server" class="text" onkeyup="this.value=formatNumeric(this.value);"
                                                    onblur="this.value=formatNumeric(this.value);" CssClass="k-textbox textbox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Nhóm
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlGroup" runat="server" CssClass="k-textbox textbox">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Chức vụ
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlchucvu" runat="server" CssClass="k-textbox textbox">
                                                    <asp:ListItem Text="Thủ trưởng" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Thủ kho" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Người lập biểu" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="Quản trị hệ thống" Value="4"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                <span style="color: Red;">*</span>&nbsp;Loại
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlloai" runat="server" CssClass="k-textbox textbox">
                                                    <asp:ListItem Text="Manager" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Admin" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Editor" Value="3"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Kích hoạt
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="rblActive" runat="server" RepeatColumns="5">
                                                    <asp:ListItem Selected="True" Text="Có" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Không" Value="0"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                            </td>
                                            <td align="left" nowrap="nowrap">
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
                    <ContentStyle>
                        <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
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
</asp:Content>
