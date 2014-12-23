<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Import.aspx.cs" Inherits="Appketoan.Pages.Import" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:FileUpload ID="file_Upload" runat="server" Width="251px" />
                                <asp:Button ID="bt_import" runat="server"  onclick="bt_import_Click" 
                                    Text="Import" />
    </div>
    <div>
    <asp:FileUpload ID="FileUpload1" runat="server" Width="251px" />
                                <asp:Button ID="Button1" runat="server"  onclick="bt_import_detail_Click" 
                                    Text="Import Detail" />
    </div>
     <div>
    <asp:FileUpload ID="FileUpload2" runat="server" Width="251px" />
                                <asp:Button ID="Button2" runat="server" Enabled="false"
                                    Text="Update Tien Hang" onclick="Button2_Click" />
    </div>
    </form>
</body>
</html>
