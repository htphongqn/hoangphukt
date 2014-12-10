<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="doi-mat-khau.aspx.cs" Inherits="Appketoan.Pages.doi_mat_khau" %>

<%@ Register src="../UIs/login.ascx" tagname="login" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Đổi mật khẩu Atlas</title>
    <link href="../Styles/kendo.BlueOpal.min.css" rel="stylesheet" />
    <link href="../Styles/kendo.common.min.css" rel="stylesheet" />
    <link href="../Styles/site.css" rel="stylesheet" />
    <%--    <link href="/Styles/site1.css" rel="stylesheet" />--%>
    <link href="../Styles/common.css" rel="stylesheet" />
    <link href="../Styles/buttons.css" rel="stylesheet" />
    <script type="text/javascript" src="../Scripts/jquery.min.js"></script>
    <script type="text/javascript" src="../Scripts/kendo.all.min.js"></script>
    <script src="../Scripts/UtilitiesCpanel.js" type="text/javascript"></script>
    <script src="../Scripts/accounting.js" type="text/javascript"></script>
    <style type="text/css">
        #top > #page-header
        {
            border-bottom: 1px solid #94C0D2;
        }
        
        #middle
        {
            background: #f2f2f2 !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
     <div class="page min-width">
        <div id="top">
            <div id="page-header">
                <div id="logo">
                    <div class="title-color">
                        <a href="">
                            <img src="/Images/logo.jpg" style="vertical-align: middle" height="50" alt="logo" /></a>
                        <a>Quản lý khách hàng</a>
                        
                    </div>
                </div>
                <div id="project-name">
                    <uc1:login ID="login1" runat="server" />
                </div>
            </div>
        </div>
        <div id="middle" style="padding-top: 10%; min-height: 289px; height: 420px">
            <div id="main-login">
                <div id="div-login" style="height: 315px;" class="login">
                    <h2>
                        Đổi mật khẩu</h2>
                    <div class="label">
                        <label for="Password">
                            Mật khẩu cũ</label></div>
                    <div class="field" id="div-password">
                        <input class="k-textbox k-input fill-input" id="txtOldPassword" name="Password" type="password"
                            runat="server" />
                    </div>
                    <div class="label">
                        <label for="Password">
                            Mật khẩu mới</label></div>
                    <div class="field" id="div1">
                        <input class="k-textbox k-input fill-input" id="txtNewPassword" name="Password" type="password"
                            runat="server" />
                    </div>
                    <div class="label">
                        <label for="Password">
                            Nhập lại mật khẩu</label></div>
                    <div class="field" id="div2">
                        <input class="k-textbox k-input fill-input" id="txtConfirmNewPassword" name="Password"
                            type="password" runat="server" />
                    </div>
                    <div class="button-container">
                        <asp:LinkButton ID="lbtnLogin" ToolTip="Đổi mật khẩu" CssClass="g-button" runat="server"
                            OnClick="lbtnLogin_Click">Đổi mật khẩu</asp:LinkButton>
                    </div>
                </div>
                <div style="clear: both">
                </div>
            </div>
        </div>
        <div id="footer" class="widget-footer">
            <div>
                <div class="copyright">
                    <b>©</b> Bản quyền 2013 thuộc về ATLAS</div>
                <div class="designed-by">
                    Thiết kể bởi <a href="http://dangcapviet.vn" target="_blank"><span class="company-name">
                        Đẳng Cấp Việt</span></a></div>
                <div style="clear: both">
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
