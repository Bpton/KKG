<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAdminLogin.aspx.cs" Inherits="VIEW_frmAdminLogin" %>


<!DOCTYPE html>


<html>
<head>
<title>KKG Login</title>
<%--<meta name="viewport" content="width=device-width, initial-scale=1"/>--%>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

<meta name="keywords" content=""/>
<meta name="description" content=""/>

<%--<link rel="shortcut icon" href="../images/favicon.ico" type="image/x-icon"/>--%>

<link href="../css/loginstyle.css" rel="stylesheet" type="text/css"/>

<%--<script type="text/javascript">
    if (parent.frames.length > 0) {
        parent.location.href = self.document.location;
    }
</script>--%>



<script type = "text/javascript" >

    function preventBack() { window.history.forward(); }

    setTimeout("preventBack()", 0);

    window.onunload = function () { null };

</script>


<script type="text/javascript" >

    function clearerrormsg() {
        var errmsg = document.getElementById("<%=LblMsg.ClientID %>");
        if (errmsg.innerHTML != '') {
            errmsg.innerHTML = '';           
        }
    }

</script>

</head>
<body>

<!--/start-login-one-->
		<div class="login">	
			<div class="ribbon-wrapper h2 ribbon-red">
            <div class="logo"><%--<img src="../images/logo_mc.png" alt="Mcnroe" />--%></div> 
				<%--<div class="ribbon-front">
					<h2>User Login <span>(TEST)</span></h2>                    
				</div>                               
				<div class="ribbon-edge-topleft2"></div>
				<div class="ribbon-edge-bottomleft"></div>--%>  
			</div>            
            <div class="form form--login">
			<form action="" method="post" runat="server">
            
        <div class="form__field green">            
          <div class="fa-user"><span class="hidden">Username</span></div>          
          <asp:TextBox ID="txtUserName" runat="server" CssClass="form__input input" value="Username" onclick="clearerrormsg();" TabIndex="1"
          onfocus="if(this.value=='Username'){this.value=''}" 
          onblur="if(this.value==''){this.value='Username'}" ></asp:TextBox>          
        </div>
        <div class="form__field orange">
          <div class="fa-lock"><span class="hidden">Password</span></div>
          <asp:TextBox ID="txtPassword" runat="server" CssClass="form__input input" value="Password" onclick="clearerrormsg();" TabIndex="2"
          onfocus="if(this.value=='Password'){this.value=''}" TextMode="Password" 
          onblur="if(this.value==''){this.value='Password'}"></asp:TextBox>
        </div>
		<div class="logerror" ID="LblMsg" runat="server">
            <asp:Label ID="lblmsg1" runat="server" Text=""></asp:Label>
                <asp:LinkButton ID="LinkButton1" runat="server" Text="Reset" Visible="false" OnClick="LinkButton1_Click"></asp:LinkButton>
         </div>
        <div class="form--login-button">           
           <asp:Button ID="btnSubmit" runat="server" Width="49%" TabIndex="3" Text="LOGIN" CssClass="button-gray" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnreset" runat="server" Width="49%" TabIndex="4" Text="RESET" CssClass="button-gray" OnClick="btnreset_Click" />
        </div>

      </form>
      </div>			
		</div>
<!--start-copyright antique white-->
   		<div class="copy-right" style="background-color:#FAEBD7">
				<p>&copy; <script>new Date().getFullYear() > 2010 && document.write("" + new Date().getFullYear());</script>  All rights  Reserved | <a href="#" target="_blank">KKG Industries</a><br>
                <span>Best viewed on IE 10.x, Firefox 40.x, Google Chrome 40.x and above.</span></p>
		</div>
	<!--//end-copyright-->
</body>
</html>