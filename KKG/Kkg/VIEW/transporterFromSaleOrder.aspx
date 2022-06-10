<%@ Page Language="C#" AutoEventWireup="true" CodeFile="transporterFromSaleOrder.aspx.cs" Inherits="VIEW_transporterFromSaleOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <label>Transporter Name</label>
            <asp:TextBox id="txtTransporterName" runat="server"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="btnsubmit" Text="Save" OnClick="btnsubmit_Click" runat="server" />
        
            <asp:Button ID="btnClose" Text="Close" OnClick="btnClose_Click" runat="server" />
        </div>
        <asp:label id="lblShowMessage" runat="server" ></asp:label>
    </form>
</body>
</html>
