<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="iscan_test._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="iscan.css" rel="stylesheet" type="text/css" />
    <script src="jquery.js" type="text/javascript" language="javascript"></script>
    <script src="iscan.js" type="text/javascript" language="javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    Count: <div id="count">ok</div>
    <br />
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
    <asp:Button ID="Button1" CssClass="btn" runat="server" Text="Increase" />
    </form>
</body>
</html>
