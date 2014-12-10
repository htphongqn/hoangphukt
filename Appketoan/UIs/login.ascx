<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="login.ascx.cs" Inherits="Appketoan.UIs.login" %>
<div id="admin_float">
    <a href="/Pages/trang-chu.aspx">[Trang chủ]</a>
    &nbsp;<asp:HyperLink ID="lnkUserLogin" runat="server" NavigateUrl="~/pages/changeinfo.aspx" ToolTip="Thay đổi thông tin tài khoản" Style="font-weight: bold;"> </asp:HyperLink>
    &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/pages/doi-mat-khau.aspx" ToolTip="Thay đổi mật khẩu">[Đổi mật khẩu]</asp:HyperLink>
    &nbsp;<asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/logout.aspx" ToolTip="Thoát khỏi chương trình" onclick="return confirm('Bạn thật sự muốn thoát?');">[Thoát]</asp:HyperLink>
</div>