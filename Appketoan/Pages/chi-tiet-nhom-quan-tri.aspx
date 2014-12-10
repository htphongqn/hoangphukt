<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true"
    CodeBehind="chi-tiet-nhom-quan-tri.aspx.cs" Inherits="Appketoan.Pages.chi_tiet_nhom_quan_tri" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
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
    <div id="header" style="padding-bottom: 5px;">
        <div class="title">
            Chi Tiết Nhóm Quản Trị
        </div>
        <div class="toolbar" style="padding-bottom: 5px">
            &nbsp;<asp:LinkButton ID="lbtnSave" ToolTip="Lưu thông tin" CssClass="k-button" runat="server"
                OnClick="lbtnSave_Click" ValidationGroup="G2"><img alt="Lưu thông tin" src="../Images/icon-32-save.png" /></asp:LinkButton>
            &nbsp;<asp:LinkButton ID="lbtnSaveClose" ToolTip="Lưu và đóng" CssClass="k-button"
                runat="server" OnClick="lbtnSaveClose_Click" ValidationGroup="G2"><img alt="Lưu thông tin" src="../Images/icon-32-saveclose.png" /></asp:LinkButton>
            &nbsp;<asp:LinkButton ID="lbtnSaveNew" ToolTip="Lưu và thêm mới" CssClass="k-button"
                runat="server" OnClick="lbtnSaveNew_Click" ValidationGroup="G2"><img alt="Lưu thông tin" src="../Images/icon-32-save-new.png" /></asp:LinkButton>
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
                                                <span style="color: Red;">*</span>&nbsp;Tên nhóm
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="Txtname" runat="server" class="text" CssClass="k-textbox textbox"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Chưa nhập tên nhóm"
                                                    ControlToValidate="Txtname" Display="Dynamic" ForeColor="Red" ValidationGroup="G2"
                                                    CssClass="tlp-error">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="td_left" align="right" valign="top" nowrap="nowrap">
                                                Loại nhóm
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="Rdtype" runat="server" RepeatColumns="5">
                                                    <asp:ListItem Selected="True" Text="Nhóm Admin" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Nhóm Editor" Value="2"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <dx:ASPxTreeList ID="ASPxTreeList_menu" runat="server" AutoGenerateColumns="False"
                                                    Width="100%" KeyFieldName="MENU_PAR_ID" Theme="Aqua" ParentFieldName="MENU_PARENT1">
                                                    <Columns>
                                                        <dx:TreeListTextColumn Caption="MENU_PAR_ID" FieldName="MENU_PAR_ID" VisibleIndex="0"
                                                            Visible="False">
                                                        </dx:TreeListTextColumn>
                                                        <dx:TreeListTextColumn Caption="Tên menu" FieldName="MENU_NAME" VisibleIndex="2"
                                                            Width="200px">
                                                            <DataCellTemplate>
                                                                    <%# Eval("MENU_NAME")%>
                                                            </DataCellTemplate>
                                                        </dx:TreeListTextColumn>
                                                        
                                                    </Columns>
                                                    <SettingsSelection AllowSelectAll="True" Enabled="True" />
                                                    <SettingsBehavior AutoExpandAllNodes="True" />
                                                </dx:ASPxTreeList>
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
