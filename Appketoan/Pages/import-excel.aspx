<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master.Master" AutoEventWireup="true" CodeBehind="import-excel.aspx.cs" Inherits="Appketoan.Pages.import_excel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHMain" runat="server">
    <asp:FileUpload ID="fileUpload" runat="server" />
    <asp:Button ID="btnImport" runat="server" Text="Import" 
        onclick="btnImport_Click" />
    <asp:Label ID="Lbrow1" runat="server" Text="Label"></asp:Label>
    <asp:Label ID="lbrow2" runat="server" Text="Label"></asp:Label>
</asp:Content>
