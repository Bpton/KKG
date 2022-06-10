<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmsession_expired.aspx.cs" Inherits="VIEW_frmsession_expired" %>

<!doctype html>
<html>
<head>
    <meta charset="UTF-8">
    <title>Session Expired</title>
    <link href="../css/session_expired.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        if (parent.frames.length > 0) {
            parent.location.href = self.document.location;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="form">
        <header>Session Expired</header>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tbody>
                <tr>
                    <td width="153" align="right">
                        <img src="../images/appointment-missedclock.png" alt="" />
                        <%--<img src="../images/animated-clock-image-0074.gif" alt="" />--%>
                        <%--<img src="../images/animated-clock-image-0054.gif" alt="" />--%>
                    </td>
                    <td>
                        <p>
                            Your session has expired.</p>
                        <p>
                            please <a href="frmAdminLogin.aspx">click here</a> go to login .</p>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>
</body>
</html>
