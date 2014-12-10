<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="menu.ascx.cs" Inherits="Appketoan.UIs.menu" %>
<div id="tabs-container">
    <ul class="tabs">
        <!--Top pos 1-->
        <li class="image"><a class="homepage" href="../Pages/trang-chu.aspx">
            <img src="/Images/home.png" alt="" /></a></li>
        <asp:Repeater ID="Rpmenu" runat="server">
            <ItemTemplate>
                <li><a>
                    <%# Eval("MENU_NAME")%></a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
<!-- defining top menu -->
<div id="nav-container" class="">
    <!--Pos 1-->
    <ul class="nav" style="display: none;">
    </ul>
    <asp:Repeater ID="Rpmenuchild" runat="server">
        <ItemTemplate>
            <ul class="nav" style="display: none;">
                <asp:Repeater ID="Repeater1" runat="server" DataSource='<%#menuchild(Eval("MENU_PAR_ID")) %>'>
                    <ItemTemplate>
                        <li><a href="<%# Getlink(Eval("MENU_PARENT_LINK")) %>">
                            <%# Eval("MENU_NAME")%></a> </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </ItemTemplate>
    </asp:Repeater>
    <!--End pos 3-->
    <!--Pos 4-->
</div>
<div style="clear: both">
</div>
