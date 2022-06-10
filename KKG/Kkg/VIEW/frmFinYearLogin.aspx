<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmFinYearLogin.aspx.cs" Inherits="VIEW_frmFinYearLogin" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html>
<head>
    <title>KKG Fin Year Login</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="keywords" content="" />
    <meta name="description" content="" />
    <%--<link rel="shortcut icon" href="../images/favicon.ico" type="image/x-icon" />--%>
    <link href="../css/loginstyle.css" rel="stylesheet" type="text/css" />
    <script src="../js/jquery.blockUI.js" type="text/javascript"></script>
    <script type="text/javascript">
        function BlockUI(elementID) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_beginRequest(function () {
                $("#" + elementID).block({ message: '<table align = "center"><tr><td>' +
     '<img src="../images/loading123.gif"/></td></tr></table>',
                    css: {},
                    overlayCSS: { background: 'transparent'
                    }
                });
            });
            prm.add_endRequest(function () {
                $("#" + elementID).unblock();
            });
        }
        $(document).ready(function () {

            BlockUI("<%=pnlsync.ClientID %>");
            $.blockUI.defaults.css = {};
        });
        function Hidepopup() {
            $find("popup").hide();
            return false;
        }
    </script>
    <script type="text/javascript">
        function detectPopupBlocker() {
            var myTest = window.open("about:blank", "", "directories=no,height=70,width=100,menubar=no,resizable=no,scrollbars=no,status=no,titlebar=no,top=0,location=no");

            if (!myTest) {
                alert("A popup blocker was detected.");
            }
            else {
                myTest.close();
            }
        }
        // window.onload = detectPopupBlocker;
    </script>
    <style type="text/css">
        .modalBackground
        {
            top: 0px;
            left: 0px;
            background-color: rgba(0,0,0,0.5);
            filter: alpha(opacity=60);
            -moz-opacity: 0.5;
            opacity: 0.5;
        }
        .b_medium
        {
            font-size: 13px;
            color: Yellow;
            font-size: large;
            background: #7abcff; /* Old browsers */ /* IE9 SVG, needs conditional override of 'filter' to 'none' */
            background: -moz-linear-gradient(top, #7abcff 0%, #60abf8 44%, #4096ee 100%); /* FF3.6+ */
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%, #7abcff), color-stop(44%, #60abf8), color-stop(100%, #4096ee)); /* Chrome,Safari4+ */
            background: -webkit-linear-gradient(top, #7abcff 0%, #60abf8 44%, #4096ee 100%); /* Chrome10+,Safari5.1+ */
            background: -o-linear-gradient(top, #7abcff 0%, #60abf8 44%, #4096ee 100%); /* Opera 11.10+ */
            background: -ms-linear-gradient(top, #7abcff 0%, #60abf8 44%, #4096ee 100%); /* IE10+ */
            background: linear-gradient(top, #7abcff 0%, #60abf8 44%, #4096ee 100%); /* W3C */
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#7abcff', endColorstr='#4096ee', GradientType=0 ); /* IE6-8 */
            border: #0a87af 1px solid;
        }
    </style>
</head>
<body>
    <!--/start-login-one-->
    <div class="login">
        <div class="ribbon-wrapper h2 ribbon-red">
            <div class="logo">
                <%--<img src="../images/logo_mc.png" alt="McNROE" />--%></div>
            <%--<div class="ribbon-front">
					<h2>User Login</h2>
				</div>                               
				<div class="ribbon-edge-topleft2"></div>
				<div class="ribbon-edge-bottomleft"></div>--%>
        </div>
        <div class="form formloginselect">
            <form action="" method="post" runat="server">
            <div class="select-style">
                <asp:DropDownList ID="ddlFinYear" runat="server">
                </asp:DropDownList>
            </div>
            <div class="select-style">
                <asp:DropDownList ID="ddlBranch" runat="server">
                </asp:DropDownList>
            </div>
            <div class="logerror" style="background-color: #B00000; color: #fff; border-radius: 0.25em;
                padding-left: 0.6875em; align-items: center; margin: 0px 0 3px 0;">
                <asp:Label ID="LblMsg" runat="server"></asp:Label>
            </div>
            <div class="form--login-button">
                <asp:Button ID="btnFinYrSubmit" runat="server" Text="LOGIN" CssClass="button-gray" OnClick="btnFinYrSubmit_Click" />
                <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="600">
                </asp:ScriptManager>
            </div>
            <asp:Panel ID="pnlsync" runat="server" CssClass="modalPopup" Style="display: none;
                border-radius: 16px; border-color: rgb(234, 84, 53);" Width="40%" Height="50%" BackColor="White" 
                BorderWidth="8px" BorderStyle="Solid">
                <div style="background-color: rgb(234, 84, 53); border-style: solid; border-color: Yellow;
                    background-position: center; background-repeat: no-repeat; background-size: cover;
                    height: 10%" align="center">
                    <asp:Label Font-Bold="True" ID="Label26" runat="server" Text="McWORLD Software Updation Alert !"
                        Font-Size="X-Large" ForeColor="Yellow"></asp:Label>
                    <hr />
                </div>

                

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table align="center">
                            <tr>
                                <td style="padding-left: 40px;">
                                    <br />
                                    <b>&nbsp;&nbsp;&nbsp;Update Version is Avaliable Please Click Update...</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="b_medium" OnClick="btnUpdate_Click"
                                        OnClientClick="return confirm('Are you sure you have internet connection?')" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="b_medium" Visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 80px;">
                                    <br />
                                    <br />
                                    <br />
                                    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                        DynamicLayout="true">
                                        <ProgressTemplate>
                                            <asp:Image ID="Image1" ImageUrl="~/images/bandwidth_animation.gif" AlternateText="Processing"
                                                runat="server" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 40px;">
                                    <b><asp:Label ID="lblerrorMsg" runat="server" ForeColor="Red" Text=""></asp:Label></b>
                                    <b><asp:Label ID="lblnetspeed" runat="server" ForeColor="Red" Text=""></asp:Label></b>
                                    <b><asp:Label ID="lblantiwires" runat="server" ForeColor="Red" Text=""></asp:Label></b>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
            <asp:LinkButton ID="lnkfak" runat="server"></asp:LinkButton>
            <ajaxToolkit:ModalPopupExtender ID="popup" runat="server" DropShadow="false" PopupControlID="pnlsync"
                TargetControlID="lnkfak" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>
            </form>
        </div>
    </div>
    <!--start-copyright-->
    <div class="copy-right" style="background-color:#FAEBD7">
        <p>
            &copy; <script>new Date().getFullYear() > 2010 && document.write("" + new Date().getFullYear());</script>
            All rights Reserved | <a href="#" target="_blank">KKG Industries</a><br>
            <span>Best viewed on IE 10.x, Firefox 40.x, Google Chrome 40.x and above.</span></p>
    </div>
    <!--//end-copyright-->
</body>
</html>