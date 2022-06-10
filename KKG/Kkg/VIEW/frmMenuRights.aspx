<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMenuRights.aspx.cs" Inherits="VIEW_frmMenuRights" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../css/styles.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function closeWindow() {
            if (false == popupWindow.closed) {
                popupWindow.close();
            }
        }
    </script>
    <script src="../js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("[id*=trmenulist] input[type=checkbox]").bind("click", function () {
                var table = $(this).closest("table");
                if (table.next().length > 0 && table.next()[0].tagName == "DIV") {
                    //Is Parent CheckBox
                    var childDiv = table.next();
                    var isChecked = $(this).is(":checked");
                    $("input[type=checkbox]", childDiv).each(function () {
                        if (isChecked) {
                            $(this).attr("checked", "checked");
                        }
                        else {
                            $(this).removeAttr("checked");
                        }
                    });
                } else {
                    //Is Child CheckBox
                    var parentDIV = $(this).closest("DIV");
                    if ($("input[type=checkbox]", parentDIV).length == $("input[type=checkbox]:checked", parentDIV).length) {
                        $("input[type=checkbox]", parentDIV.prev()).attr("checked", "checked");
                    }
                    //                    else {
                    //                        $("input[type=checkbox]", parentDIV.prev()).removeAttr("checked");
                    //                    }
                }
            });
        })
    </script>
    <script type="text/javascript">
        $(window).load(function () {
            $("#pageloaddiv").fadeOut(3000);
        });
    </script>
    <style type="text/css">
        /*#pageloaddiv {
position: fixed;
left: 0px;
top: 0px;
width: 100%;
height: 100%;
z-index: 1000;
background: url('../images/loading123.gif') no-repeat center center;
}*/
        body {
            font-size: 12px;
            font-family: Arial, sans-serif;
            color: #333;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="pageloaddiv"></div>

        <div id="pnlterritory" runat="server">
            <table border="0" width="100%" cellspacing="0" cellpadding="0" class="form_container_td">
                <tr>
                    <td class="field_input" colspan="2" align="center">
                        <asp:Label ID="Label1" Text="USER TYPE&nbsp;<span>*</span>&nbsp;&nbsp;&nbsp;" class="innerfield_title"
                            runat="server" Font-Bold="True" ForeColor="black" Style="padding-left: 22%;"></asp:Label>
                        <asp:DropDownList ID="ddlusertype" runat="server" Width="25%" AppendDataBoundItems="true"
                            class="chosen-select" data-placeholder="Choose a UserType" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlusertype_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<span class='blink'>* Required</span>"
                            ValidationGroup="add" ForeColor="red" ControlToValidate="ddlusertype" InitialValue="0"> </asp:RequiredFieldValidator>
                        <asp:LinkButton ID="lnklogin" runat="server" OnClick="lnklogin_Click" ForeColor="Red" Style="font-size: medium;">Back To Login</asp:LinkButton>
                        <asp:LinkButton ID="lnkmenu" runat="server" OnClick="lnkmenu_Click" ForeColor="Green" Style="font-size: medium; padding-left: 20px;">Go to Menu Creation</asp:LinkButton>
                        <br />
                        <br />
                    </td>
                </tr>
                <tr runat="server" id="tr1">
                    <td class="field_input" colspan="2" style="padding-left: 20%;" align="center">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                            <tr>
                                <td width="50%">
                                    <fieldset style="width: 70%;">
                                        <legend style="color: #FF0000; font-weight: bold">MENU PERMISSION</legend>
                                        <div style="height: 490px; width: 100%; align: center; border-radius: 16px; position: relative; overflow: auto; background-color: rgba(240, 240, 240, 0.5);">
                                            <table border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                                <tr>
                                                    <td valign="top" style="padding-left: 10px;">
                                                        <asp:TreeView ID="trmenulist" runat="server" ExpandDepth="1" Font-Underline="false" AutoGenerateDataBindings="false"
                                                            ForeColor="Blue" ShowCheckBoxes="All" ImageSet="News" NodeIndent="50" ShowLines="True"
                                                            ToolTip="Menu Details" HoverNodeStyle-BackColor="ButtonShadow" HoverNodeStyle-ForeColor="ButtonShadow">
                                                            <HoverNodeStyle Font-Underline="false" ForeColor="Black" Font-Bold="true" />
                                                            <NodeStyle Font-Underline="false" Font-Names="Arial, Sans-Serif" Font-Size="13px" NodeSpacing="3px" VerticalPadding="0px" ImageUrl="../images/img-raty/Article_IconNew.gif" />
                                                            <RootNodeStyle Font-Underline="false" ForeColor="#e31281" Font-Bold="true" ImageUrl="../images/img-raty/card.jpeg" />
                                                            <ParentNodeStyle Font-Underline="false" Font-Bold="true" ForeColor="Green" ImageUrl="../images/img-raty/star-on.png" />
                                                            <SelectedNodeStyle Font-Underline="false" HorizontalPadding="0px" VerticalPadding="0px" />
                                                        </asp:TreeView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </fieldset>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="width: 50%">
                        <br />
                        <asp:Button ID="Add" CssClass="btn_small btn_blue" ToolTip="Save" runat="server"
                            Width="20%" Style="border-radius: 8px;" Text="Save" ValidationGroup="add" OnClick="Add_Click" />
                    </td>
                    <td>
                        <br />
                        <asp:Button ID="Cancel" CausesValidation="false" CssClass="btn_small btn_blue" Width="20%"
                            Style="border-radius: 8px;" ToolTip="Cancel" runat="server" Text="Cancel" OnClick="Cancel_close" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="divcredential" runat="server" style="display: none; vertical-align: middle; text-align: center;" align="center">
            <asp:Image ID="Image2" runat="server" ImageUrl="~/images/nocredential.jpeg" ImageAlign="AbsMiddle" Height="600px" Width="100%" ToolTip="No Credential" />
        </div>
    </form>
</body>
</html>
