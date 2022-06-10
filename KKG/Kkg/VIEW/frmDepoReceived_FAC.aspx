<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true"
    CodeFile="frmDepoReceived_FAC.aspx.cs" Inherits="FACTORY_frmDepoReceived_FAC" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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

            BlockUI("<%=pnlAddEdit.ClientID %>");
            $.blockUI.defaults.css = {};
        });
        function Hidepopup() {
            $find("popup").hide();
            return false;
        }
    </script>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.show();
            }
        }

        function EndRequestHandler(sender, args) {
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.hide();
            }
        }
    </script>
    <script type="text/javascript">
        function disableautocompletion(id) {
            var TextBoxControl = document.getElementById(id);
            TextBoxControl.setAttribute("autocomplete", "off");
        }
    
    </script>
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="../images/loading123.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
           
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>
                                    Depot Stock Receipt Details</h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnnewentry" runat="server" Text="Add New Record" CssClass="btn_link"
                                        CausesValidation="false" OnClick="btnnewentry_Click" />
                                </div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr id="trreceivedno" runat="server">
                                            <td class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>DEPO RECEIVED DETAILS</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td width="110">
                                                                <asp:Label ID="lblqualitycontrol" runat="server" Text="Received No"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtdeporeceivedno" runat="server" Font-Bold="true" Width="20%" placeholder="Depo Received No"
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>SEARCH INFORMATION</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                                        <tr>
                                                            <td width="110" class="field_title">
                                                                <asp:Label ID="Label2" Text="Received Depot" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="223" class="field_input">
                                                                <asp:DropDownList ID="ddltodepot" runat="server" AppendDataBoundItems="true" width= "220"
                                                                    class="chosen-select">
                                                                    <asp:ListItem Text="--Select Depot Name--" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td width="90" class="field_title">
                                                                <asp:Label ID="Label12" Text="Transfer No" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="223" class="field_input">
                                                                <asp:DropDownList ID="ddltransferno" runat="server" AppendDataBoundItems="true" width= "220"
                                                                    class="chosen-select" ValidationGroup="check" OnSelectedIndexChanged="ddltransferno_SelectedIndexChanged"
                                                                    AutoPostBack="true">
                                                                    <asp:ListItem Text="--Select Transfer No--" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td width="90" class="field_title">
                                                                <asp:Label ID="Label1" runat="server" Text="Received Date"></asp:Label><span class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtreceiveddate" runat="server" Width="120" Enabled="false"
                                                                    placeholder="dd/mm/yyyy" MaxLength="10" ValidationGroup="check" onkeypress="return isNumberKeyWithslash(event); "></asp:TextBox>
                                                                <asp:ImageButton ID="imgPopuppodate" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24"  />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="imgPopuppodate"
                                                                    runat="server" TargetControlID="txtreceiveddate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                                </ajaxToolkit:CalendarExtender>
                                                                
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>TRANSFER INFORMATION</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                                        <tr>
                                                            <td width="100" class="field_title">
                                                                <asp:Label ID="lblfromdepo" Text="From Depot" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="223" class="field_input">
                                                                <asp:DropDownList ID="ddlfromdepot" runat="server" AppendDataBoundItems="true" Width="220"
                                                                    class="chosen-select" Enabled="false">
                                                                    <asp:ListItem Text="--Mother Depot Name--" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td width="100" class="field_title">
                                                                <asp:Label ID="Label10" Text="Challan Date" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="200" class="field_input">
                                                                <asp:TextBox ID="txtchallandate" runat="server" Enabled="false" CssClass="x-large"></asp:TextBox>
                                                            </td>
                                                            <td width="100" class="field_title">
                                                                <asp:Label ID="Label4" Text="E-WayBill No" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtwaybillno" runat="server" Enabled="false" CssClass="x-large" Width="200"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label5" Text="Insurance Company" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlInsurance" runat="server" AppendDataBoundItems="true" Style="width: 100%"
                                                                    class="chosen-select" Enabled="false">
                                                                    <asp:ListItem Text="Insurance Co. Name" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label27" Text="Policy No." runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtinsuranceno" runat="server" Enabled="false" CssClass="x-large"></asp:TextBox>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label16" Text="Mode of Transport" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtmodeoftransport" runat="server" Enabled="false" CssClass="x-large" Width="200"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label3" Text="Transpoter" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txttranspoter" runat="server" Enabled="false" Width="98%" CssClass="x-large"></asp:TextBox>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label6" Text="Vehicle No" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtvehicleno" runat="server" Font-Bold="true" Enabled="false" CssClass="x-large"></asp:TextBox>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label7" Text="LR/GR No" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtlrgrno" runat="server" Enabled="false" CssClass="x-large" Width="200"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label8" Text="LR/GR Date" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtlrgrdate" runat="server" Enabled="false" Width="98%" CssClass="x-large"></asp:TextBox>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label24" Text="GATE ENTRY NO" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtgateno" runat="server" MaxLength="40" CssClass="x-large" 
                                                                AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);" Width="200"></asp:TextBox>
                                                            </td>
                                                                                                                        <td class="field_title">
                                                                <asp:Label ID="Label25" Text="GATE ENTRY DATE" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtgatedate" runat="server" MaxLength="10" Width="162" CssClass="x-large" Enabled="TRUE"
                                                                    placeholder="dd/mm/yyyy" Font-Bold="true" onkeypress="return isNumberKeyWithslash(event);" />
                                                                <asp:ImageButton ID="ImageButton2" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton2"
                                                                    runat="server" TargetControlID="txtgatedate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                            <td class="field_title" Visible="false">
                                                                <asp:Label ID="Label9" Text="GNG No" runat="server" Visible="false" ></asp:Label>
                                                            </td>
                                                            <td class="field_input" Visible="false">
                                                                <asp:TextBox ID="txtchallanno" runat="server" Font-Bold="true" Enabled="false" CssClass="x-large" Visible="false"></asp:TextBox>
                                                            </td>
                                                            <td class="field_title" Visible="false">
                                                                <asp:Label ID="Label11" Text="GNG Date" runat="server" Visible="false"></asp:Label>
                                                            </td>
                                                            <td class="field_input" Visible="false">
                                                                <asp:TextBox ID="txttransferdate" runat="server" Enabled="false" CssClass="x-large" Width="200" Visible="false"></asp:TextBox>
                                                            </td>

                                                        </tr>
                                                        <tr style="display:none";>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label22" Text="F-FORM NO" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtfformno" runat="server" Enabled="false" Width="98%" CssClass="x-large" />
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label23" Text="F-FORM DATE" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtfformdate" runat="server" Enabled="false" CssClass="x-large"></asp:TextBox>
                                                            </td>

                                                        </tr>
                                                        
                                                        <tr>

                                                            <td class="field_title" >
                                                                <asp:Label ID="Label33" Text="Net Amt." runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox runat="server" ID="txtFinalAmt"  Width="222px" placeholder="Net Amt."
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td class="field_title" >
                                                                <asp:Label ID="Label31" Text="Total PCS" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox runat="server" ID="txtTotalPCS" Width="200px" placeholder="Total PCS" Enabled="false"></asp:TextBox>
                                                                
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                             <td width="100" class="field_title">
                                                                <asp:Label ID="Label34" Text="Delivery Date" runat="server" ></asp:Label>
                                                            </td>
                                                            <td width="200" class="field_input">
                                                                <asp:TextBox ID="txtdeliverydate" runat="server" Width="120" Enabled="false" CssClass="x-large" ></asp:TextBox>
                                                            </td>
                                                            </tr>

                                                        <tr style="display:none;">
                                                            <td class="field_title" >
                                                                <asp:Label ID="Label28" Text="Total Case" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox runat="server" ID="txtTotalCase" Width="200px" placeholder="Total Case" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>

                                                         <tr style="display:none">
                                                            <td class="field_title" >
                                                                <asp:Label ID="lblledger" Text="Ledger" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                 <asp:DropDownList ID="ddlledger" Width="200" runat="server" class="chosen-select">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="Reqrdddlledger" runat="server" ControlToValidate="ddlledger"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Approve" InitialValue="0"
                                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>

                                     <div class="gridcontent-inner">
                                     <fieldset>
                                         <legend>RECEIVED DETAILS</legend>
                                          <div style="overflow: hidden; width: 100%;" id="DivHeaderRow">
                                          </div>
                                           <div style="overflow: scroll; margin-bottom: 8px; margin-right:6px;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                           <asp:GridView ID="gvreceived" runat="server" Width="100%" ShowFooter="true" AutoGenerateColumns="true" ViewStateMode="Enabled"  
                                           EmptyDataText="No Records Available" CssClass="zebra">
                                                    <Columns>
                                                     <asp:TemplateField HeaderText="SL">
                                                                    <ItemTemplate>
                                                                         <nav style="height: 16px;z-index: 2; padding-left:10px; position: relative; text-align:center;"><span class="badge green"><%# Container.DataItemIndex + 1 %></span></nav>
                                                                    </ItemTemplate>
                                                                 </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="REJECTION" HeaderStyle-ForeColor="Red" Visible="false">
                                                            <ItemTemplate>
                                                                <nav style="height: 5px;z-index: 2;position: relative; text-align:center;">
                                                                <asp:Label ID="lblrejvalue" runat="server" class="badge red">0</asp:Label>
                                                                </nav>
                                                                <asp:ImageButton ID="btnrejectiondetails" runat="server" value='<%# Eval("GUID") %>' CausesValidation="false" 
                                                                    ToolTip="REJECTION REASON" class="action-icons c-edit" style="margin-left:15px;" OnClick="btnrejectiondetails_Click"></asp:ImageButton>                                                                    
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>                                                                                 
                                                </asp:GridView>
                                                 </div>
                                            <div id="DivFooterRow" style="overflow: hidden">
                                            </div>
                                     </fieldset>
                                     </div>

                                     <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                     <tr>
                                            <td class="field_input" style="padding-left: 10px">
                                                <fieldset>
                                                    <legend>Tax Details</legend>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                         <td width="60" class="innerfield_title">
                                                                <asp:Label ID="Label29" Text="TOT.BASIC AMT." runat="server"></asp:Label>
                                                            </td>
                                                            <td width="150">
                                                                <asp:TextBox runat="server" ID="txtbasicamt" Width="130px" placeholder="TOT.BASIC AMT."
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="95" class="innerfield_title" >
                                                                <asp:Label ID="Label30" Text="TAX AMT." runat="server" ></asp:Label>
                                                            </td>
                                                            <td width="150">
                                                                <asp:TextBox ID="txttaxamt" runat="server" Width="130px" Enabled="false" placeholder="TAX AMT."> </asp:TextBox>
                                                            </td>
                                                            <td width="70" class="innerfield_title">
                                                                <asp:Label ID="Label32" Text="(BASIC + TAX) AMT." runat="server" ></asp:Label>
                                                            </td>
                                                            <td width="150">
                                                                <asp:TextBox ID="txttotal" runat="server" Width="130px" Enabled="false" placeholder="(BASIC + TAX) AMT."> </asp:TextBox>
                                                            </td>                                                           
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td class="field_input" style="padding-left: 10px">
                                                <fieldset>
                                                    <legend>Gross Details</legend>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                         <td width="60" class="innerfield_title">
                                                                <asp:Label ID="lblgrossamt" Text="Gross Amt." runat="server"></asp:Label>
                                                            </td>
                                                            <td width="150">
                                                                <asp:TextBox runat="server" ID="txtTotalGross"  Width="130px" placeholder="Gross Amt."
                                                                    Enabled="false"></asp:TextBox>                                                                   
                                                            </td>
                                                             <td width="60" class="innerfield_title">
                                                                <asp:Label ID="lblroundoff" Text="R/O" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="150">
                                                                <asp:TextBox runat="server" ID="txtRoundoff"  Width="130px" placeholder="R/O"
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>

                                                            <td width="60" class="innerfield_title">
                                                                <asp:Label ID="lblnetamt" Text="Net Amt." runat="server"></asp:Label>
                                                            </td>
                                                            <td width="150">
                                                                <asp:TextBox runat="server" ID="txtnetamt"  Width="130px" placeholder="Net Amt."
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                     </table>   

                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td width="80" class="innerfield_title" style="padding-left:10px;">
                                                <asp:Label ID="lblRemarks" Text="Remarks" runat="server"></asp:Label>
                                            </td>
                                            <td width="320">
                                                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Style="width:300px; height: 80px" MaxLength="255"> </asp:TextBox>
                                            </td>
                                            <td width="15">&nbsp;
                                                
                                            </td>
                                            <td width="100" align="left" class="innerfield_title">
                                                <asp:Label ID="lblCheckerNote" Text="Rejection Note" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCheckerNote" runat="server" TextMode="MultiLine" Style="width:300px; height: 80px"
                                                    Enabled="false"> </asp:TextBox>
                                            </td>                                            
                                        </tr>
                                        <tr>
                                            <td height="8" colspan="5">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left">&nbsp;
                                                
                                            </td>
                                            <td colspan="4">
                                            <div class="btn_24_blue" id="divbtnsave" runat="server">
                                              <span class="icon disk_co"></span>
                                                <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn_link" OnClick="btnsave_Click" />
                                            </div>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <div class="btn_24_blue">
                                                <span class="icon cross_octagon_co"></span>
                                                <asp:Button ID="btncancel" runat="server" CssClass="btn_link" Text="Cancel" CausesValidation="false" OnClick="btncancel_Click" />
                                            </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                            <div class="btn_24_green" id="divbtnapprove" runat="server">
                                                <span class="icon approve_co"></span>
                                                <asp:LinkButton ID="btnApprove" runat="server" Text="Approve" CssClass="btn_link" CausesValidation="false" OnClick="btnApprove_Click" />
                                            </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                            <div class="btn_24_red" id="divbtnreject" runat="server">
                                                <span class="icon reject_co"></span>
                                                <asp:LinkButton ID="btnReject" runat="server" Text="Reject" CssClass="btn_link" CausesValidation="false" OnClick="btnReject_Click" />
                                            </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlDisplay" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_title">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="90">
                                                            <asp:Label ID="Label17" runat="server" Text="From Date"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="165">
                                                            <asp:TextBox ID="txtfromdateins" runat="server" MaxLength="10" Width="120" Enabled="false"
                                                                placeholder="dd/mm/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="datecheckins"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopup" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="Calendar1" PopupButtonID="imgPopup" runat="server"
                                                                TargetControlID="txtfromdateins" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="CV_FromDate_QC" runat="server" ControlToValidate="txtfromdateins"
                                                                SetFocusOnError="true" ErrorMessage="From Date is required!" Display="None" ValidationGroup="datecheckins"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
                                                                TargetControlID="CV_FromDate_QC" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <asp:CustomValidator ID="CV_FromDateValidation_qc" runat="server" ClientValidationFunction="ValidateDate"
                                                                ControlToValidate="txtfromdateins" SetFocusOnError="true" ErrorMessage="* Invalid Date!"
                                                                Display="None" ValidationGroup="datecheckins" />
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender28" runat="server"
                                                                TargetControlID="CV_FromDateValidation_qc" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                        <td width="70">
                                                            <asp:Label ID="Label18" runat="server" Text="To Date"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="165">
                                                            <asp:TextBox ID="txttodateins" runat="server" MaxLength="10" Width="69%" Enabled="false"
                                                                placeholder="dd/mm/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="datecheckins"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="ImageButton1" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButton1"
                                                                runat="server" TargetControlID="txttodateins" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="CV_ToDate_QC" runat="server" ControlToValidate="txttodateins"
                                                                SetFocusOnError="true" ErrorMessage="To Date is required!" Display="None" ValidationGroup="datecheckins"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server"
                                                                TargetControlID="CV_ToDate_QC" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <asp:CustomValidator ID="CV_ToDateValidation_QC" runat="server" ClientValidationFunction="ValidateDate"
                                                                ControlToValidate="txttodateins" SetFocusOnError="true" ErrorMessage="* Invalid Date!"
                                                                Display="None" ValidationGroup="datecheckins" />
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server"
                                                                TargetControlID="CV_ToDateValidation_QC" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                        <td>
                                                        <div class="btn_24_blue">
                                                            <span class="icon find_co"></span>
                                                            <asp:Button ID="btnSTfind" runat="server" Text="Search" CssClass="btn_link" ValidationGroup="datecheckins" OnClick="btnSTfind_Click" />
                                                        </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <ul id="search_box">
                                        <li>
                                            <input name="" id="FilterTextBox" type="text" class="search_input" placeholder="Enter text to search the whole grid"
                                                onkeyup="FilterTextBox_KeyUp();">
                                        </li>
                                        <li>
                                            <input name="" id="OboutButton2" type="submit" value="" class="search_btn" onclick="return performSearch();">
                                        </li>
                                    </ul>
                                    <div class="gridcontent" style="width: 100%; height: 400px; overflow: scroll">
                                        <cc1:Grid ID="gvdepotreceivedDetails" runat="server" CallbackMode="false" Serialize="true"
                                            FolderStyle="../GridStyles/premiere_blue" AutoGenerateColumns="false" AllowPageSizeSelection="true"
                                            OnDeleteCommand="DeleteRecordReceived" AllowAddingRecords="false" AllowFiltering="true"  AllowPaging="true" PageSize="100"
                                            OnRowDataBound="gvdepotreceivedDetails_RowDataBound">
                                            <ClientSideEvents OnBeforeClientDelete="OnBeforeDepotReceivedDelete" OnClientDelete="OnDeleteDepotReceivedDetails" />
                                            <FilteringSettings InitialState="Visible" />
                                            <Columns>
                                                <cc1:Column ID="Column3" DataField="STOCKDEPORECEIVEDID" HeaderText="STOCKDEPORECEIVEDID"
                                                    runat="server"  Wrap="true" Visible="false">
                                                </cc1:Column>
                                                <cc1:Column ID="Column10" DataField="STOCKTRANSFERID" HeaderText="STOCKTRANSFERID"
                                                    runat="server"  Wrap="true" Visible="false">
                                                </cc1:Column>
                                                <cc1:Column ID="Column20" DataField="Sl" HeaderText="SL" runat="server" Width="60">
                                                    <TemplateSettings TemplateId="slnoTemplateIN" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column21" DataField="STOCKDEPORECEIVEDDATE" HeaderText="RECEIPT DATE"
                                                    runat="server" Width="120" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                        <cc1:FilterOption Type="EndsWith" />
                                                        <cc1:FilterOption Type="IsEmpty" />
                                                        <cc1:FilterOption Type="IsNotEmpty" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column22" DataField="STOCKDEPORECEIVEDNO" HeaderText="RECEIPT NO"
                                                    runat="server" Width="150" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                        <cc1:FilterOption Type="EndsWith" />
                                                        <cc1:FilterOption Type="IsEmpty" />
                                                        <cc1:FilterOption Type="IsNotEmpty" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column18" DataField="CHALLANNO" HeaderText="GNG NO" runat="server"
                                                    Width="100" Wrap="true" Visible="false">
<%--                                                    <FilterOptions >
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                        <cc1:FilterOption Type="EndsWith" />
                                                        <cc1:FilterOption Type="IsEmpty" />
                                                        <cc1:FilterOption Type="IsNotEmpty" />
                                                    </FilterOptions>--%>
                                                </cc1:Column>
                                                <cc1:Column ID="Column24" DataField="MOTHERDEPOTNAME" HeaderText="RECEIPT FROM" runat="server"
                                                    Width="150" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                        <cc1:FilterOption Type="EndsWith" />
                                                        <cc1:FilterOption Type="IsEmpty" />
                                                        <cc1:FilterOption Type="IsNotEmpty" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column19" DataField="RECEIVEDDEPOTNAME" HeaderText="RECEIPT FROM"
                                                    runat="server" Width="150" Wrap="true" Visible="false">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                        <cc1:FilterOption Type="EndsWith" />
                                                        <cc1:FilterOption Type="IsEmpty" />
                                                        <cc1:FilterOption Type="IsNotEmpty" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column229" DataField="NEXTLEVELID" HeaderText="NEXTLEVELID" runat="server"
                                                    Visible="false"  Wrap="true" Width="100" />
                                                <cc1:Column ID="Column329" DataField="ISVERIFIED" HeaderText="ISVERIFIED" Visible="false"
                                                    runat="server" Width="100" />
                                                <cc1:Column ID="Column929" DataField="ISVERIFIEDDESC" HeaderText="FINANCIAL STATUS" runat="server"
                                                    Width="100" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                        <cc1:FilterOption Type="EndsWith" />
                                                        <cc1:FilterOption Type="IsEmpty" />
                                                        <cc1:FilterOption Type="IsNotEmpty" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column729" DataField="RECEIVEDDEPOTID" HeaderText="RECEIVEDDEPOTID"
                                                    Visible="false" runat="server"  Wrap="true" Width="100" />
                                                <cc1:Column ID="Column29" DataField="TOTALCASE" HeaderText="TOTAL CASE"
                                                    runat="server" Width="100"  Wrap="true" Visible="false">
                                                </cc1:Column>
                                                <cc1:Column ID="Column30" DataField="TOTALPCS" HeaderText="TOTAL PCS"
                                                    runat="server" Width="100">
                                                </cc1:Column>
                                                <cc1:Column ID="Column936" DataField="NETAMOUNT" HeaderText="NET AMOUNT" runat="server"  Width="120"  />
                                                <cc1:Column ID="Column937" DataField="APPROVAL_PERSON" HeaderText="APPROVAL PERSON" runat="server"  Width="140"  />  
                                                <cc1:Column ID="Column31" AllowEdit="false" AllowDelete="false" HeaderText="PRINT" runat="server"
                                                Width="60" Wrap="true">
                                                <TemplateSettings TemplateId="PrintBtnTemplate" />
                                                </cc1:Column>

                                                <cc1:Column ID="Column23" AllowEdit="true" AllowDelete="true" HeaderText="EDIT" runat="server"
                                                    Width="50">
                                                    <TemplateSettings TemplateId="editBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="DELETE" AllowEdit="false" AllowDelete="true" Width="80">
                                                    <TemplateSettings TemplateId="deleteBtnTemplateDespatch" />
                                                </cc1:Column>
                                            </Columns>
                                            <FilteringSettings MatchingType="AnyFilter" />
                                             <Templates>                                             
                                             <cc1:GridTemplate runat="server" ID="PrintBtnTemplate">
                                                <Template>
                                                    <a href="javascript: //" class="filter_btn printer_co" id="btnPrint_<%# Container.PageRecordIndex %>"
                                                        onclick="CallServerMethodPrint(this)"></a>
                                                </Template>
                                            </cc1:GridTemplate>
                                            
                                            <cc1:GridTemplate runat="server" ID="editBtnTemplate">
                                                  <Template>
                                                   <a href="javascript: //" class="action-icons c-edit" id="btnGridEdit_<%# Container.PageRecordIndex %>"
                                                    onclick="CallServerMethod(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>

                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="slnoTemplateIN">
                                                    <Template>
                                                        <asp:Label ID="lblslnoin" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="deleteBtnTemplateDespatch">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="gvdepotreceivedDetails.delete_record(this)">
                                                        </a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <ScrollingSettings ScrollWidth="100%" ScrollHeight="290" />
                                        </cc1:Grid>
                                        <asp:Button ID="btngrdedit" runat="server" Text="grdedit"  Style="display: none" OnClick="btngrdedit_Click"
                                            CausesValidation="false" />
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" Style="display: none" 
                                        OnClick="btnPrint_Click" CausesValidation="false" OnClientClick="ShowConfirmBox(this,'Are you sure you want to print this voucher?'); return false;"  />
                                    </div>
                                    <asp:HiddenField ID="hdn_receivedid" runat="server" />
                                    <asp:HiddenField ID="hdn_receivedno" runat="server" />
                                    <asp:HiddenField ID="hdn_stocktransferid" runat="server" />
                                    <asp:HiddenField ID="hdn_guid" runat="server" />
                                    <asp:HiddenField ID="hdn_lockreceivedqty" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdnWaybillKey" runat="server" />
                                    <asp:HiddenField ID="hdnproductid" runat="server" />
                                    <asp:HiddenField ID="hdnproductname" runat="server" />
                                    <asp:HiddenField ID="hdnbatchno" runat="server" />
                                    <asp:HiddenField ID="hdnpacksizeid" runat="server" />
                                    <asp:HiddenField ID="hdn_transferid" runat="server" />
                                    <asp:HiddenField ID="hdnproductrate" runat="server" />
                                    <asp:HiddenField ID="hdnTransporterID" runat="server" />
                                    <asp:HiddenField runat="server" ID="hdnDepotID" />
                                    <asp:HiddenField runat="server" ID="hdnSaleOrderID" />
                                    <asp:HiddenField runat="server" ID="hdnSaleOrderNo" />
                                    <asp:HiddenField runat="server" ID="hdnMFDATE" />
                                    <asp:HiddenField runat="server" ID="hdnEXPRDATE" /> 
                                    <asp:HiddenField runat="server" ID="hdnMRP" />                                   
                                </asp:Panel>
                                <asp:Panel ID="pnlAddEdit" runat="server" CssClass="modalPopup" Style="display: none; border-radius: 16px;"
                                    Width="80%" Height="87%" BackColor="White" BorderColor="#257daf"
                                    BorderWidth="8px" BorderStyle="Solid">
                                    
                                    <div style="background-color: #257daf; border-style: solid; border-color: Yellow;
                                        background-position: center; background-repeat: no-repeat; background-size: cover;
                                        height: 6%" align="center">
                                        <asp:Image ID="Image2" runat="server" ImageAlign="Left" class="action-icons c-edit" />
                                        <asp:Label Font-Bold="True" ID="Label13" runat="server" Text="Depot Receipt Rejection Details"
                                            Font-Size="X-Large" ForeColor="Yellow"></asp:Label>
                                        <asp:ImageButton ID="imgrejectionbtn" ImageAlign="Right" ImageUrl="~/images/Cancel.png"
                                            Height="25px" Width="25px" runat="server" OnClick="imgrejectionbtn_click" />
                                        <hr />
                                    </div>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td width="13%" class="field_title">
                                                <asp:Label ID="Label14" Text="Product" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="45%">
                                                            <asp:TextBox ID="txtproductnameonrejection" runat="server" CssClass="full" Enabled="false"
                                                                ForeColor="Black"></asp:TextBox>
                                                        </td>
                                                        <td width="15">&nbsp;
                                                            
                                                        </td>
                                                        <td width="9%" class="innerfield_title">
                                                            <asp:Label ID="Label15" Text="Batch No" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtproductbatchnoonrejection" runat="server" Width="192px" Enabled="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="Label19" Text="Rejection Qty" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="45%">
                                                            <asp:TextBox ID="txtrejectionqty" runat="server" placeholder="Rejection Qty" Width="40%"
                                                                MaxLength="10" ForeColor="Black" ValidationGroup="check" onkeypress="return isNumberKeyWithDot(event);" AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*"
                                                                Font-Bold="true" ForeColor="Red" ControlToValidate="txtrejectionqty" ValidateEmptyText="false"
                                                                ValidationGroup="check" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td width="15">&nbsp;
                                                            
                                                        </td>
                                                        <td width="9%" class="innerfield_title">
                                                            <asp:Label ID="Label20" Text="PackSize" runat="server"></asp:Label><span class="req">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlrejectionpacksize" runat="server" AppendDataBoundItems="true"
                                                                class="common-select" Width="200" ValidationGroup="check">
                                                                <asp:ListItem Text="-- SELECT PACKSIZE --" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                                                                Font-Bold="true" ForeColor="Red" ControlToValidate="ddlrejectionpacksize" ValidateEmptyText="false"
                                                                ValidationGroup="check" SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="Label21" Text="Reason" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="45%">
                                                            <asp:DropDownList ID="ddlrejectionreason" runat="server" AppendDataBoundItems="true"
                                                                class="common-select" Width="450" ValidationGroup="check">
                                                                <asp:ListItem Text="-- SELECT REASON NAME --" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                                                Font-Bold="true" ForeColor="Red" ControlToValidate="ddlrejectionreason" ValidateEmptyText="false"
                                                                ValidationGroup="check" SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td width="30">&nbsp;
                                                            
                                                        </td>
                                                        <td>
                                                         <div class="btn_24_blue">
                                               <span class="icon add_co"></span>
                                                            <asp:Button ID="btnrejectionadd" runat="server" Text="ADD" CssClass="btn_link"
                                                                ValidationGroup="check" OnClick="btnrejectionadd_click" CausesValidation="true" />
                                                                </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="gridcontent-inner">
                                        <fieldset>
                                            <legend>REJECTION DETAILS</legend>
                                            <cc1:Grid ID="gvrejectiondetails" runat="server" CallbackMode="true" AutoGenerateColumns="false"
                                                FolderStyle="../GridStyles/premiere_blue" AllowAddingRecords="false" AllowFiltering="true"
                                                AllowPageSizeSelection="true" PageSize="50" AllowPaging="true" EnableRecordHover="true">
                                                <FilteringSettings InitialState="Visible" />
                                                <Columns>
                                                    <cc1:Column ID="Column8" DataField="GUID" ReadOnly="true" HeaderText="GUID" runat="server"
                                                        Visible="false" Width="10" />
                                                    <cc1:Column ID="Column9" DataField="DEPOTRECEIVEDID" ReadOnly="true" HeaderText="DEPOTRECEIVEDID"
                                                        runat="server" Visible="false" Width="10" />
                                                    <cc1:Column ID="Column13" DataField="STOCKTRANSFERID" ReadOnly="true" HeaderText="STOCKTRANSFERID"
                                                        runat="server" Visible="false" Width="10" />
                                                    <cc1:Column ID="Column14" DataField="PRODUCTID" ReadOnly="true" HeaderText="ID" runat="server"
                                                        Visible="false" Width="10" />
                                                    <cc1:Column ID="Column15" DataField="Sl" HeaderText="SL" runat="server" Width="70"
                                                        Wrap="true">
                                                        <TemplateSettings TemplateId="slnoTemplaterej" />
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column16" DataField="PRODUCTNAME" HeaderText="PRODUCT NAME" runat="server"
                                                        Wrap="true" Width="380">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column25" DataField="BATCHNO" HeaderText="BATCH NO" runat="server"
                                                        Wrap="true" Width="120">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column51" DataField="REJECTIONQTY" HeaderText="QTY" runat="server"
                                                        Width="100" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column26" DataField="PACKINGSIZENAME" HeaderText="PACK SIZE" runat="server"
                                                        Wrap="true" Width="200">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column12" DataField="RATE" HeaderText="RATE" runat="server" Width="100"
                                                        Visible="false">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column35" DataField="TOTALRATE" HeaderText="AMOUNT" runat="server"
                                                        Width="100" Visible="false">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column27" DataField="REASONNAME" HeaderText="REJECTION REASON" runat="server"
                                                        Width="350" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column28" DataField="REASONID" HeaderText="REASONID" runat="server"
                                                        Visible="false" Width="10" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column331" DataField="MFDATE" HeaderText="MFDATE" runat="server"
                                                        Visible="false" Width="10" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column332" DataField="EXPRDATE" HeaderText="EXPRDATE" runat="server"
                                                        Visible="false" Width="10" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column330" DataField="ASSESMENTPERCENTAGE" HeaderText="ASSESMENTPERCENTAGE"
                                                        runat="server" Visible="false" Width="10" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column334" DataField="MRP" HeaderText="MRP" runat="server" Visible="false"
                                                        Width="10" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column367" DataField="WEIGHT" HeaderText="WEIGHT" runat="server"
                                                        Visible="false" Width="10" Wrap="true">
                                                    </cc1:Column>
                                                    <cc1:Column HeaderText="DELETE" AllowEdit="false" AllowDelete="true" Width="80" Wrap="true">
                                                        <TemplateSettings TemplateId="deleteBtnTemplate" />
                                                    </cc1:Column>
                                                </Columns>
                                                <FilteringSettings MatchingType="AnyFilter" />
                                                <Templates>
                                                    <cc1:GridTemplate runat="server" ID="deleteBtnTemplate">
                                                        <Template>
                                                            <%--<a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="gvPurchaseOrder.delete_record(this)"></a>--%>
                                                            <a href="javascript: //" class="action-icons c-delete" id="btnGridDelete_<%# Container.PageRecordIndex %>"
                                                                onclick="CallDeleteServerMethod(this)"></a>
                                                        </Template>
                                                    </cc1:GridTemplate>
                                                    <cc1:GridTemplate runat="server" ID="slnoTemplaterej">
                                                        <Template>
                                                            <asp:Label ID="lblslno" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                        </Template>
                                                    </cc1:GridTemplate>
                                                </Templates>
                                                <ScrollingSettings ScrollWidth="100%" ScrollHeight="110" NumberOfFixedColumns="6" />
                                            </cc1:Grid>
                                            <asp:Button ID="btngrddelete" runat="server" Text="grddelete" Style="display: none"
                                                OnClick="btngrddelete_Click" CausesValidation="false" OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                        </fieldset>
                                    </div>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td  align="center" colspan="2" style="padding-top:10px;">
                                            <div class="btn_24_blue">
                                               <span class="icon disk_co"></span>
                                                <asp:Button ID="btnrejectionsubmit" runat="server" CssClass="btn_link"
                                                    Text="Submit" CausesValidation="false" OnClick="btnrejectionsubmit_click" />
                                                    </div>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                    <div class="btn_24_blue">
                                               <span class="icon cross_octagon_co"></span>
                                                <asp:Button ID="btnrejectioncancel" runat="server" CssClass="btn_link"
                                                    Text="Cancel" CausesValidation="false" OnClientClick="return Hidepopup()" />
                                                    </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
                                <ajaxToolkit:ModalPopupExtender ID="popup" runat="server" DropShadow="false" PopupControlID="pnlAddEdit"
                                    TargetControlID="lnkFake" BackgroundCssClass="modalBackground">
                                </ajaxToolkit:ModalPopupExtender>
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
                                </style>
                            </div>
                        </div>
                    </div>
                    <div id="lightRejectionNote" class="white_content" runat="server">
                        <table width="50%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                            <tr>
                                <td width="10%" class="field_title">
                                    <asp:Label ID="Label26" runat="server" Text="Note"></asp:Label>&nbsp;<span class="req">*</span>
                                </td>
                                <td width="20%" class="field_input">
                                    <asp:TextBox ID="txtRejectionNote" runat="server" CssClass="x_large" TextMode="MultiLine"
                                        Style="height: 119px"> </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="CV_txtRejectionNote" runat="server" ControlToValidate="txtRejectionNote"
                                        ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="RejectionNote"
                                        ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="field_title">&nbsp;
                                    
                                </td>
                                <td class="field_input">
                                    <asp:Button ID="btnRejectionNoteSubmit" runat="server" Text="Submit" CssClass="btn_small btn_blue"
                                        ValidationGroup="RejectionNote" OnClick="btnRejectionNoteSubmit_Click" />&nbsp;&nbsp;<asp:Button
                                            ID="btnRejectionCloseLightbox" runat="server" Text="Close" CssClass="btn_small btn_blue"
                                            OnClick="btnRejectionCloseLightbox_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="fadeRejectionNote" class="black_overlay" runat="server">
                    </div>
                    <cc1:MessageBox ID="MessageBox1" runat="server" />
                </div>
            </div>
            <script type="text/javascript">
                oboutGrid.prototype._resizeColumn = oboutGrid.prototype.resizeColumn;
                oboutGrid.prototype.resizeColumn = function (columnIndex, amountToResize, keepGridWidth) {
                    this._resizeColumn(columnIndex, amountToResize, false);

                    var width = this._getColumnWidth();

                    if (width > 0) {
                        if (this.ScrollWidth == '0px') {
                            this.GridMainContainer.style.width = width + 'px';
                        } else {
                            var scrollWidth = parseInt(this.ScrollWidth);
                            if (width < scrollWidth) {
                                this.GridMainContainer.style.width = width + 'px';
                                this.HorizontalScroller.style.display = 'none';
                            } else {
                                this.HorizontalScroller.firstChild.firstChild.style.width = width + 'px';
                                this.HorizontalScroller.style.display = '';
                            }
                        }
                    }
                }

                oboutGrid.prototype._getColumnWidth = function () {
                    var totalWidth = 0;
                    for (var i = 0; i < this.ColumnsCollection.length; i++) {
                        if (this.ColumnsCollection[i].Visible) {
                            totalWidth += this.ColumnsCollection[i].Width;
                        }
                    }

                    return totalWidth;
                }
            </script>
            <script type="text/javascript">
                function isNumberKeyWithDot(evt) {
                    var charCode = (evt.which) ? evt.which : event.keyCode;
                    if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 46))
                        return false;

                    return true;
                }

                function isNumberKeyWithslash(evt) {
                    var charCode = (evt.which) ? evt.which : event.keyCode;
                    if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 47))
                        return false;

                    return true;
                }       

                var searchTimeout = null;
                function FilterTextBox_KeyUp() {
                    searchTimeout = window.setTimeout(performSearch, 500);
                }

                function performSearch() {
                    var searchValue = document.getElementById('FilterTextBox').value;
                    if (searchValue == FilterTextBox.WatermarkText) {
                        searchValue = '';
                    }
                    gvdepotreceivedDetails.addFilterCriteria('STOCKDEPORECEIVEDNO', OboutGridFilterCriteria.Contains, searchValue);
                    gvdepotreceivedDetails.addFilterCriteria('STOCKDEPORECEIVEDDATE', OboutGridFilterCriteria.Contains, searchValue);
                    gvdepotreceivedDetails.addFilterCriteria('MOTHERDEPOTNAME', OboutGridFilterCriteria.Contains, searchValue);
                    gvdepotreceivedDetails.addFilterCriteria('CHALLANNO', OboutGridFilterCriteria.Contains, searchValue);
                    gvdepotreceivedDetails.executeFilter();
                    searchTimeout = null;
                    return false;
                }

                function CallServerMethod(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                    document.getElementById("<%=hdn_receivedid.ClientID %>").value = gvdepotreceivedDetails.Rows[iRecordIndex].Cells[0].Value;
                    document.getElementById("<%=hdn_receivedno.ClientID %>").value = gvdepotreceivedDetails.Rows[iRecordIndex].Cells[4].Value;
                    document.getElementById("<%=hdn_stocktransferid.ClientID %>").value = gvdepotreceivedDetails.Rows[iRecordIndex].Cells[1].Value;
                    document.getElementById("<%=hdnDepotID.ClientID %>").value = gvdepotreceivedDetails.Rows[iRecordIndex].Cells[11].Value;
                    document.getElementById("<%=btngrdedit.ClientID %>").click();
                }

                function CallServerMethodPrint(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnPrint_", "");
                    document.getElementById("<%=hdn_transferid.ClientID %>").value = gvdepotreceivedDetails.Rows[iRecordIndex].Cells[0].Value;
                    document.getElementById("<%=btnPrint.ClientID %>").click();
                }

                function CallDeleteServerMethod(oLink) {
                    var iRecordIndexdelete = oLink.id.toString().replace("btnGridDelete_", "");
                    document.getElementById("<%=hdn_guid.ClientID %>").value = gvrejectiondetails.Rows[iRecordIndexdelete].Cells[0].Value;
                    document.getElementById("<%=btngrddelete.ClientID %>").click();
                }

                function OnBeforeDepotReceivedDelete(record) {
                    record.Error = '';
                    document.getElementById("<%=hdn_receivedid.ClientID %>").value = record.STOCKDEPORECEIVEDID;
                    if (confirm("Are you sure you want to delete? "))
                        return true;
                    else
                        return false;
                }
                function OnDeleteDepotReceivedDetails(record) {
                    alert(record.Error);
                }
            </script>

            <script language="javascript" type="text/javascript">
                function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
                    var tbl = document.getElementById(gridId);
                    if (tbl) {
                        var DivHR = document.getElementById('DivHeaderRow');
                        var DivMC = document.getElementById('DivMainContent');
                        var DivFR = document.getElementById('DivFooterRow');

                        //*** Set divheaderRow Properties ****
                        DivHR.style.height = headerHeight + 'px';
                        //DivHR.style.width = (parseInt(width) - 16) + 'px';
                        DivHR.style.width = '98%';
                        DivHR.style.position = 'relative';
                        DivHR.style.top = '0px';
                        DivHR.style.zIndex = '10';
                        DivHR.style.verticalAlign = 'top';

                        //*** Set divMainContent Properties ****
                        DivMC.style.width = width + 'px';
                        DivMC.style.height = height + 'px';
                        DivMC.style.position = 'relative';
                        DivMC.style.top = -headerHeight + 'px';
                        DivMC.style.zIndex = '1';

                        //*** Set divFooterRow Properties ****
                        DivFR.style.width = (parseInt(width) - 16) + 'px';
                        DivFR.style.position = 'relative';
                        DivFR.style.top = -headerHeight + 'px';
                        DivFR.style.verticalAlign = 'top';
                        DivFR.style.paddingtop = '2px';

                        if (isFooter) {
                            var tblfr = tbl.cloneNode(true);
                            tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                            var tblBody = document.createElement('tbody');
                            tblfr.style.width = '100%';
                            tblfr.cellSpacing = "0";
                            tblfr.border = "0px";
                            tblfr.rules = "none";
                            //*****In the case of Footer Row *******
                            tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
                            tblfr.appendChild(tblBody);
                            DivFR.appendChild(tblfr);
                        }
                        //****Copy Header in divHeaderRow****
                        DivHR.appendChild(tbl.cloneNode(true));
                    }
                }

                function OnScrollDiv(Scrollablediv) {
                    document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
                    document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
                }

         </script>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
