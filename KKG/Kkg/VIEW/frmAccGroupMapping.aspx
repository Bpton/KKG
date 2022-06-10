<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="frmAccGroupMapping.aspx.cs" Inherits="VIEW_frmAccGroupMapping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"  TagPrefix="cc1" %>
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
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("[id*=trvwdebit] input[type=checkbox]").bind("click", function () {
                var table = $(this).closest("table");
                if (table.next().length > 0 && table.next()[0].tagName == "DIV") {
                    //Is Parent CheckBox
                    var childDiv = table.next();
                    var isChecked = $(this).is(":checked");
                    $("input[type=checkbox]", childDiv).each(function () {
                        if (isChecked) {
                            $(this).attr("checked", "checked");
                        } else {
                            $(this).removeAttr("checked");
                        }
                    });
                } else {
                    //Is Child CheckBox
                    var parentDIV = $(this).closest("DIV");
                    if ($("input[type=checkbox]", parentDIV).length == $("input[type=checkbox]:checked", parentDIV).length) {
                        $("input[type=checkbox]", parentDIV.prev()).attr("checked", "checked");
                    } else {
                        $("input[type=checkbox]", parentDIV.prev()).removeAttr("checked");
                    }
                }
            });
        })
        </script>
        <script type="text/javascript">
            $(function () {
                $("[id*=trvwcredit] input[type=checkbox]").bind("click", function () {
                    var table = $(this).closest("table");
                    if (table.next().length > 0 && table.next()[0].tagName == "DIV") {
                        //Is Parent CheckBox
                        var childDiv = table.next();
                        var isChecked = $(this).is(":checked");
                        $("input[type=checkbox]", childDiv).each(function () {
                            if (isChecked) {
                                $(this).attr("checked", "checked");
                            } else {
                                $(this).removeAttr("checked");
                            }
                        });
                    } else {
                        //Is Child CheckBox
                        var parentDIV = $(this).closest("DIV");
                        if ($("input[type=checkbox]", parentDIV).length == $("input[type=checkbox]:checked", parentDIV).length) {
                            $("input[type=checkbox]", parentDIV.prev()).attr("checked", "checked");
                        } else {
                            $("input[type=checkbox]", parentDIV.prev()).removeAttr("checked");
                        }
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
	color:#333;
}
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="pageloaddiv"></div>
    <div id="pnlterritory" runat="server" >
        <table border="0" width="100%" cellspacing="0" cellpadding="0" class="form_container_td">
            <tr>
                <td class="field_input" colspan="2" align="center" >
                    <asp:Label ID="Label1" Text="VOUCHER TYPE&nbsp;<span>*</span>&nbsp;&nbsp;&nbsp;" class="innerfield_title" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                    
                   <asp:DropDownList ID="ddlvouchertype" runat="server" Width="25%" 
                        AppendDataBoundItems="true" class="chosen-select" 
                        data-placeholder="Choose a VoucherType" AutoPostBack="True" 
                        onselectedindexchanged="ddlvouchertype_SelectedIndexChanged" ></asp:DropDownList>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<span class='blink'>* Required</span>" ValidationGroup="add" ForeColor="Red"
                                                                    ControlToValidate="ddlvouchertype"  InitialValue="0"> </asp:RequiredFieldValidator>
                    <br /><br />
                </td>
            </tr>
            <tr runat="server" id="tr1">
                <td class="field_input" colspan="2" >
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td" >
                <tr>
                <td  width="50%" valign="top">
                
                <fieldset style="border: medium dotted #FF00FF;">
                        <legend style="border: thick double #0066FF; color: #FF0000; font-weight: bold">DEBIT REGION</legend>
                        <div style="height:450px;border-radius: 16px;position:relative;overflow:auto;">
                        <table border="0" cellspacing="0" cellpadding="0" class="form_container_td" >
                            <tr>
                                <td valign="top" style="padding-left: 10px;" >
                                   
                                    <asp:TreeView ID="trvwdebit" runat="server" SkinID="SampleTreeView" ExpandDepth="10" ForeColor="Blue"  ShowCheckBoxes="All" 
                                                                    ImageSet="News" NodeIndent="50" ShowLines="True" ToolTip="Group Details" HoverNodeStyle-BackColor="ButtonShadow"
                                                                    HoverNodeStyle-ForeColor="ControlLight">
                                                                    <HoverNodeStyle Font-Underline="True" ForeColor="Aqua" />
                                                                    <NodeStyle Font-Names="Arial, Sans-Serif" Font-Size="13px" NodeSpacing="2px" VerticalPadding="0px" ImageUrl="../images/img-raty/050.png" />
                                                                    <RootNodeStyle ForeColor="Red" Font-Bold="true" ImageUrl="../images/img-raty/checked.png" />
                                                                    <ParentNodeStyle Font-Bold="true" ForeColor="Green" ImageUrl="../images/img-raty/star-on.png" />
                                                                    <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px"  />
                                                                </asp:TreeView>

                                    <%--<asp:HiddenField ID="hdnPid" runat="server" />--%>
                                </td>
                            </tr>
                        </table>
                        </div>
                    </fieldset>
                
                </td>
                <td style="padding-left: 10px;" ></td>
                <td valign="top">
                
                <fieldset style="border: medium dotted #FF00FF;">
                        <legend style="border: thick double #FF0000; color: #009900; font-weight: bold">CREDIT REGION</legend>
                        <div style="height:450px;border-radius: 16px;position:relative;overflow:auto;">
                        <table border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                            <tr>
                                <td valign="top" style="padding-left: 10px;" >
                                  
                                     <asp:TreeView ID="trvwcredit" runat="server" SkinID="SampleTreeView" ExpandDepth="10" ForeColor="Blue" ShowCheckBoxes="All"
                                                                    ImageSet="News" NodeIndent="50" ShowLines="True" ToolTip="Group Details" HoverNodeStyle-BackColor="ButtonShadow"
                                                                    HoverNodeStyle-ForeColor="ControlLight">
                                                                    <HoverNodeStyle Font-Underline="True" ForeColor="Aqua" />
                                                                    <NodeStyle Font-Names="Arial, Sans-Serif" Font-Size="13px" NodeSpacing="2px" VerticalPadding="0px" ImageUrl="../images/img-raty/050.png" />
                                                                    <RootNodeStyle ForeColor="Red" Font-Bold="true" ImageUrl="../images/img-raty/checked.png" />
                                                                    <ParentNodeStyle Font-Bold="true" ForeColor="Green" ImageUrl="../images/img-raty/star-on.png" />
                                                                    <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px"  />
                                                                </asp:TreeView>
                                    <asp:HiddenField ID="hdnPid" runat="server" />
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
                <td align="right" style="width:50%">
                    <br />
                    <asp:Button ID="Add" CssClass="btn_small btn_blue" ToolTip="Save" runat="server" Width="20%" style="border-radius: 8px;"
                        Text="Save" ValidationGroup="add" OnClick="Add_Click" />
                </td>
                <td>
                    <br />
                    <asp:Button ID="Cancel" CausesValidation="false" CssClass="btn_small btn_blue" Width="20%" style="border-radius: 8px;"
                     ToolTip="Cancel" runat="server" Text="Cancel" OnClick="Cancel_close" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>