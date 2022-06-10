<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmGateEntry.aspx.cs" Inherits="VIEW_Default" %>


<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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

         function ValidateListBox_ddlbsegment(sender, args) {
         var options = document.getElementById("<%=ddlPono.ClientID%>").options;
         for (var i = 0; i < options.length; i++) {
             if (options[i].selected == true) {
                 args.IsValid = true;
                 return;
             }
         }
         args.IsValid = false;
         }

        $(function () {
            $('#ContentPlaceHolder1_ddlPono').multiselect({
                includeSelectAllOption: true
            });
        });
    
    </script>

    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="../images/103.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="page_title">
                <span class="title_icon"><span class="computer_imac"></span></span>
                <h3>Gate Entry</h3>
            </div>

            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">

                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>District Details</h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAdd" runat="server" Text="Add New Record" CssClass="btn_link" OnClick="btnAdd_Click"
                                        CausesValidation="false" />
                                </div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <div id="entryNumber" runat="server" style="display:none">
                                                         <tr>
                                                        <td class="field_title">
                                                            <asp:Label ID="Label5" runat="server" Text="Enrty Number"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td class="field_input" width="30%">
                                                            <asp:TextBox ID="txtEnrtyNumber" runat="server" Width="500px"  Enabled="false" CssClass="mid" ToolTip="Enter Po Number"> </asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    </div>
                                                    <tr>
                                                        <td width="135" class="field_title">
                                                                <asp:Label ID="lblEntryType" Text="Entry Type" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="160" class="field_input">
                                                                <asp:DropDownList ID="ddlEntryType" runat="server" class="chosen-select" AutoPostBack="true"
                                                                    data-placeholder="Entry Type" AppendDataBoundItems="True" Width="150px" ValidationGroup="Save" 
                                                                    OnSelectedIndexChanged="ddlEntryType_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0" Text="select type"></asp:ListItem>
                                                                    <asp:ListItem Value="1" Selected="True" Text="NORMAL"></asp:ListItem>
                                                                    <asp:ListItem Value="2"  Text="CONSUMABLE"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                    </tr>
                                                   
                                                    <tr>

                                                       <td class="field_title">
                                                                <asp:Label ID="Label6" Text="Gate Entry Date" runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td width="23%">
                                                                <asp:TextBox ID="txtEntryDate" runat="server" Enabled="false" Width="120" placeholder="MM/dd/yyyy"
                                                                    MaxLength="10" ValidationGroup="Save"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/calendar.png"
                                                                    Width="24px" Height="24px" ImageAlign="AbsMiddle"/>
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderEntryDate" 
                                                                    TargetControlID="txtEntryDate"
                                                                    PopupButtonID="ImageButton2" runat="server" Format="dd/MM/yyyy"
                                                                    BehaviorID="CalendarExtenderEntryDate" CssClass="cal_Theme1" />
                                                            </td>
                                                        <td class="field_title">
                                                            <asp:Label ID="lblVendorName" runat="server" Text="VendorName"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="23%">
                                                            <asp:DropDownList ID="ddlVendor" runat="server" data-placeholder="Select Vendor" AutoPostBack="true"
                                                                AppendDataBoundItems="True" Width="250" class="chosen-select" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="field_title" id="tdLblPo" runat="server">
                                                            <asp:Label ID="lblPono" runat="server" Text="Po no"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="23%" id="tdDdlPo" runat="server">
                                                            <asp:ListBox ID="ddlPono" runat="server"   SelectionMode="Multiple" ValidationGroup="ADD"
                                                                AppendDataBoundItems="True" TabIndex="4" Width="150px">
                                                            </asp:ListBox>
                                                        </td>
                                                  </tr>
                                                    <tr>
                                                        <td class="field_title">
                                                            <asp:Label ID="Label1" runat="server" Text="Bill Number"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td class="field_input">
                                                            <asp:TextBox ID="txtBillNumber" Width="160px" runat="server" CssClass="mid"  ToolTip="Enter Bill Number"> </asp:TextBox>
                                                        </td>

                                                        <td class="field_title">
                                                            <asp:Label ID="Label3" runat="server" Text="Bill Date"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td class="field_input" width="95">
                                                            <asp:TextBox ID="txtBilldate" runat="server" MaxLength="10" Width="60" Enabled="false"
                                                                placeholder="dd/MM/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopup" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarFromDate" PopupButtonID="imgPopup" runat="server"
                                                                TargetControlID="txtBilldate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>

                                                        <td class="field_title">
                                                            <asp:Label ID="Label2" runat="server" Text="Total Value"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td class="field_input">
                                                            <asp:TextBox ID="txtTotalvalue" runat="server" CssClass="mid" onkeypress="return isNumberKeyWithDot(event);" ToolTip="Enter Total Value"> </asp:TextBox>

                                                        </td>
                                                        <tr>
                                                            <td class="field_title">
                                                            <asp:Label ID="Label4" runat="server" Text="Remarks"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td class="field_input">
                                                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="mid" TextMode="MultiLine" > </asp:TextBox>

                                                        </td>

                                                        </tr>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding: 8px 0" id="save" runat="server">
                                                            <div class="btn_24_blue">
                                                                <span class="icon disk_co"></span>
                                                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn_link" OnClick="btnSave_Click" />
                                                            </div>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            </td>
                                                        <td>
                                             <div class="btn_24_blue">
                                                 <span class="icon cross_octagon_co"></span>
                                                 <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn_link" OnClick="btnCancel_Click" CausesValidation="false" />
                                             </div>
                                                            <asp:HiddenField ID="Hdn_Fld" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
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
                                                            <asp:Label ID="Label16" runat="server" Text="From Date"></asp:Label>
                                                        </td>
                                                        <td width="165">
                                                            <asp:TextBox ID="txtFromDate" runat="server" MaxLength="10" Width="80" Enabled="false"
                                                                placeholder="dd/MM/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="ImageButton1" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server"
                                                                TargetControlID="txtFromDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td width="70">
                                                            <asp:Label ID="Label19" runat="server" Text="To Date"></asp:Label>
                                                        </td>
                                                        <td width="165">
                                                            <asp:TextBox ID="txtToDate" runat="server" MaxLength="10" Width="80" Enabled="false"
                                                                placeholder="dd/mm/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopuptodate" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderToDate" PopupButtonID="imgPopuptodate"
                                                                runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                         <td width="60">
                                                            <asp:Label ID="Label120" runat="server" Text="Status"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlstatus" Width="100" runat="server" class="chosen-select"
                                                                 data-placeholder="Choose Waybill Filter">
                                                                <asp:ListItem Text="Select All" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Pending" Selected="True" Value="N"></asp:ListItem>
                                                                <asp:ListItem Text="Confirm" Value="Y"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td width="110" valign="top">
                                                            <div class="btn_24_blue">
                                                                <span class="icon find_co"></span>
                                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn_link"
                                                                    ValidationGroup="Search" OnClick="btnSearch_Click" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>


                                    <div style="overflow: scroll; height: 350px; width: 100%;">
                                            <asp:GridView ID="grvGateEntry" runat="server" Width="100%" CssClass="reportgrid"
                                                AutoGenerateColumns="false" ShowFooter="true" AlternatingRowStyle-BackColor="AliceBlue"
                                                EmptyDataText="No Records Available" OnRowDataBound="grvGateEntry_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField>
                                                            <ItemTemplate>
                                                    <asp:Button ID="btnEntryedit" runat="server" Text="EDIT" OnClick="btnEntryedit_Click" 
                                                        class="action-icons c-edit" CausesValidation="false" />
                                                          </ItemTemplate>
                                                                <HeaderStyle Width="5px" />
                                                            </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="5">
                                                        <ItemTemplate>
                                                            <nav style="position: relative; text-align: center;"><span class="badge black"><%# Container.DataItemIndex + 1 %></span></nav>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="ENTRYID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblENTRYID" runat="server" Text='<%# Bind("ENTRYID") %>'
                                                                value='<%# Eval("ENTRYID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ENTNUMBER">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblENTNUMBER" runat="server" Text='<%# Bind("ENTNUMBER") %>'
                                                                value='<%# Eval("ENTNUMBER") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                     <asp:TemplateField HeaderText="ENTRYDATE">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblENTRYDATE" runat="server" Text='<%# Bind("ENTRYDATE") %>'
                                                                value='<%# Eval("ENTRYDATE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="VENDORNAME">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVENDORNAME" runat="server" Text='<%# Bind("VENDORNAME") %>'
                                                                value='<%# Eval("VENDORNAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="POID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPOID" runat="server" Text='<%# Bind("POID") %>'
                                                                value='<%# Eval("POID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField> <asp:TemplateField HeaderText="PONO">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPONO" runat="server" Text='<%# Bind("PONO") %>'
                                                                value='<%# Eval("PONO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="BILLNUMBER">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBILLNUMBER" runat="server" Text='<%# Bind("BILLNO") %>'
                                                                value='<%# Eval("BILLNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="GRNNO">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGRNNO" runat="server" Text='<%# Bind("GRNNO") %>'
                                                                value='<%# Eval("GRNNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TOTOALVALUE">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTOTOALVALUE" runat="server" Text='<%# Bind("TOTOALVALUE") %>'
                                                                value='<%# Eval("TOTOALVALUE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="STATUS">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSTATUS" runat="server" Text='<%# Bind("STATUS") %>'
                                                                value='<%# Eval("STATUS") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DELETE" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btngrddelete" runat="server" CausesValidation="false"
                                                            class="action-icons c-delete" OnClick="btngrddelete_Click"
                                                            OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                </asp:Panel>

                            </div>
                        </div>
                    </div>
                    <cc1:MessageBox ID="MessageBox1" runat="server" />
                    <span class="clear"></span>
                </div>
                <span class="clear"></span>
            </div>

            <script type="text/javascript">
                
                    function isNumberKeyWithDot(evt) {
                        var charCode = (evt.which) ? evt.which : event.keyCode;
                        if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 46) && (charCode != 45))
                            return false;

                        return true;
                    }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

