<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true"
    CodeBehind="trang-chu.aspx.cs" Inherits="Appketoan.Pages.trang_chu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMain" runat="server">
    <div class="menu">
        <div class="menu_parent">
            <asp:Repeater ID="Rpmenu" runat="server">
                <ItemTemplate>
                    <div class="menu_child">
                        <div class="menu_images">
                            <img alt="" src="../Images/Folder1.png" width="70" height="70" /></div>
                        <div class="menu_title">
                            <%# Eval("MENU_NAME") %></div>
                        <asp:Repeater ID="Repeater1" runat="server" DataSource='<%#menuchild(Eval("MENU_PAR_ID")) %>'>
                            <HeaderTemplate>
                                <div class="menu_title_level2">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li><a href='<%# Getlink(Eval("MENU_PARENT_LINK")) %>'>
                                    <%# Eval("MENU_NAME") %></a> </li>
                            </ItemTemplate>
                            <FooterTemplate>
                                </div></FooterTemplate>
                        </asp:Repeater>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
