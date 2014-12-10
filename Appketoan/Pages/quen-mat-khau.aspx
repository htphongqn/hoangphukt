<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="quen-mat-khau.aspx.cs" Inherits="Appketoan.Pages.quen_mat_khau" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Đăng Nhập Atlas</title>
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
                </div>
            </div>
        </div>
        <div id="middle" style="padding-top: 10%; min-height: 289px; height: 420px">
            <div id="main-forgotpass">
                <div id="div-forgotpass" class="forgotpass">
                    <h2>
                        Khôi Phục Mật Khẩu</h2>
                    <div class="info">
                        <div class="label">
                            <label for="Email">
                                Địa Chỉ Email</label></div>
                        <div class="field">
                            <input class="k-textbox k-input fill-input bold" id="txtEmail" name="UserEmail"
                                type="text" value="" runat="server" /></div>
                    </div>
                    <div class="label">
                        <label for="Code">
                            Nhập Mã Chuỗi</label></div>
                    <div class="field" id="div-password">
                        <input class="k-textbox k-input fill-input" id="txtCode" name="Code" type="text" runat="server"/>
                    </div>

                     <div class="row" style="margin-left: 80px; margin-top: -20px;>
                            <label class="column left_col">
                                &nbsp;</label>
                            <div class="column right_col">
                                <cc1:CaptchaControl ID="CaptchaControl1" runat="server" CaptchaLength="5" CaptchaBackgroundNoise="None"
                                    CaptchaHeight="35" CaptchaWidth="150" Style="margin-left: 5px;" FontColor="Cyan"
                                    NoiseColor="Aqua" />
                            </div>
                        </div>
                     <div class="button-container">
                        <asp:LinkButton ID="lbtnforgotpass" ToolTip="Đăng nhập" CssClass="g-button" 
                            runat="server" onclick="lbtnforgotpass_Click" >Gửi Thông Tin</asp:LinkButton>
                        <div class="forgotpass1" style="margin-top: 20px;">
                            <img width="9" height="7" src="/Images/MuiTen.jpg"  style="margin-right: 10px;"></img>
                            <a href="/pages/dang-nhap.aspx" class="a" style="text-decoration: none;">Đăng Nhập </a>
                        </div>
                 
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
