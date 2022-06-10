<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="frmAddCity.aspx.cs" Inherits="VIEW_frmAddCity" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../css/styles.css" rel="stylesheet" type="text/css" />
     <link href="../css/sprite.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
   
 
    <asp:ScriptManager ID="scrmng" runat="server"></asp:ScriptManager>   
    <asp:Label ID="lblmsg" runat="server" Text="" Visible="false"></asp:Label>
    <div><br />
     <table cellpadding="0" cellspacing="0" border="0" class="form_container_td">
        <tr>
        <td width="55" class="field_title"><asp:Label ID="lblCity" Text="City" runat="server"></asp:Label><span class="req">*</span></td>
        <td width="118" class="field_input">
        <asp:TextBox ID="txtcity" runat="server" CssClass="full" ></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtcity"
                            SetFocusOnError="true" ErrorMessage="City is required!" Display="None"></asp:RequiredFieldValidator>
                        <%--<ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                            TargetControlID="RequiredFieldValidator2" PopupPosition="BottomRight" HighlightCssClass="errormassage"
                            WarningIconImageUrl="../images/050.png">
                        </ajaxToolkit:ValidatorCalloutExtender>--%>
         </td>                                      
        <td class="field_input" align="right">
            <div class="btn_24_blue">
                <span class="icon disk_co"></span>
                <asp:Button ID="btncitySubmit" runat="server" Text="Save" CssClass="btn_link" OnClick="btncitySubmit_Click" />
            </div>
        </td>
        </tr>
     </table>                              
                                                                    
      </div>                               
  
    </form>
</body>
</html>
