<%@ Page Title="Gross Return" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master"
    AutoEventWireup="true" CodeFile="frmGrossReturn.aspx.cs" Inherits="VIEW_frmGrossReturn" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
        
        function Hidepopup() {
            $find("popup").hide();
            return false;
        }
    </script>
    <script type="text/javascript">
        window.onload = function () {
            oboutGrid.prototype.selectRecordByClick = function () {
                return;
            }
            oboutGrid.prototype.showSelectionArea = function () {
                return;
            }
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((charCode < 48 || charCode > 57) && (charCode != 8))
                return false;

            return true;
        }

        function isNumberKeyWithDot(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 46))
                return false;

            return true;
        }

        function isNumberKeyWithDotMinus(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 46) && (charCode != 45))
                return false;

            return true;
        }

        function isNumberKeyWithslash(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 47))
                return false;

            return true;
        }
        function isNumberKeyWithSlashHyphenAtZ(evt) {
                    var charCode = (evt.which) ? evt.which : event.keyCode;
                    if ((charCode < 47 || charCode > 57) && (charCode != 45) && (charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122))
                        return false;

                    return true;
                }
            </script>

            <script type="text/javascript">
        function CHKInvoiceNo() {
            var txtInvoiceNo1 = "ContentPlaceHolder1_txtInvNo";            
            var txtInvoiceNo = document.getElementById(txtInvoiceNo1).value;
            var txtInvoiceNoL = txtInvoiceNo.length;  
          for (var i = 0; i < txtInvoiceNoL; i++) {  
            
               var n1 = txtInvoiceNo.charCodeAt(i);                                   
              if ((n1 >= 47 && n1 <= 57) || (n1 >= 65 && n1 <= 90) || (n1 >= 97 && n1 <= 122) || n1==45) {

               }
               else {
                    alert('Invalid Invoice No.');
                    document.getElementById(txtInvoiceNo1).value = "";
                   return false;
               }
            }
            var j = 0;
            for (var i = 0; i < txtInvoiceNoL; i++) {  
            
               var n1 = txtInvoiceNo.charCodeAt(i);                                   
                if (n1 != 48) {
                    j = 1;
                }              
            }
            if (j == 0)
            {
                alert('Invalid Invoice No.');
                document.getElementById(txtInvoiceNo1).value = "";
                return false;
            }

           
       return true;
    }
    </script>
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



        var searchTimeout = null;
        function FilterTextBox_KeyUp() {
            searchTimeout = window.setTimeout(performSearch, 500);
        }

        function performSearch() {
            var searchValue = document.getElementById('FilterTextBox').value;
            if (searchValue == FilterTextBox.WatermarkText) {
                searchValue = '';
            }
            grdDespatchHeader.addFilterCriteria('SALEINVOICENO', OboutGridFilterCriteria.Contains, searchValue);
            grdDespatchHeader.addFilterCriteria('SALEINVOICEDATE', OboutGridFilterCriteria.Contains, searchValue);
            grdDespatchHeader.addFilterCriteria('TRANSPORTERNAME', OboutGridFilterCriteria.Contains, searchValue);
            grdDespatchHeader.addFilterCriteria('DISTRIBUTORNAME', OboutGridFilterCriteria.Contains, searchValue);
            grdDespatchHeader.addFilterCriteria('GATEPASSNO', OboutGridFilterCriteria.Contains, searchValue);
            grdDespatchHeader.executeFilter();
            searchTimeout = null;
            return false;
        }

        function OnBeforeInvoiceDelete(record) {
            record.Error = '';
            document.getElementById("<%=hdnDespatchID.ClientID %>").value = record.SALERETURNID;
            if (confirm("Are you sure you want to cancel? "))
                return true;
            else
                return false;
        }
        function OnDeleteInvoiceDetails(record) {
            alert(record.Error);
        }

        function CallServerMethod(oLink) {
            var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
            document.getElementById("<%=hdnDespatchID.ClientID %>").value = grdDespatchHeader.Rows[iRecordIndex].Cells[1].Value;
            document.getElementById("<%=btngrdedit.ClientID %>").click();

        }
        function CallServerMethodPrint(oLink) {
            var iRecordIndex = oLink.id.toString().replace("btnPrint_", "");
            document.getElementById("<%=hdnDespatchID.ClientID %>").value = grdDespatchHeader.Rows[iRecordIndex].Cells[1].Value;
            document.getElementById("<%=btnPrint.ClientID %>").click();

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
            <div id="content">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>
                                    RETURN DETAILS</h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAdd" runat="server" Text="Add New Record" CssClass="btn_link"
                                        CausesValidation="false" OnClick="btnAdd_Click" /></div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px">
                                                <fieldset>
                                                    <legend>Return Details</legend>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr runat="server" id="trAutoInvoiceNo">
                                                            <td class="field_title">
                                                                <asp:Label ID="lblStockRcvdNo" Text="Sale Return No." runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input" colspan="5">
                                                                <asp:TextBox ID="txtSaleInvoiceNo" runat="server" CssClass="large" placeholder="Auto Generate No."
                                                                    Enabled="false" Font-Bold="true"> </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                            </td>
                                            <td width="98" class="field_title">
                                                <asp:Label ID="Label2" Text="Return Date" runat="server"></asp:Label>&nbsp;<span
                                                    class="req">*</span>
                                            </td>
                                            <td width="165" class="field_input">
                                                <asp:TextBox ID="txtInvoiceDate" runat="server" Enabled="false" Width="120" placeholder="dd/MM/yyyy"
                                                    MaxLength="10" ValidationGroup="Save"></asp:TextBox>
                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/calendar.png"
                                                    Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorInvoiceDate" runat="server"
                                                    ControlToValidate="txtInvoiceDate" ValidateEmptyText="false" SetFocusOnError="true"
                                                    ErrorMessage="Required!" ValidationGroup="Save" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderInvoiceDate" TargetControlID="txtInvoiceDate"
                                                    PopupButtonID="ImageButton1" runat="server" Format="dd/MM/yyyy" Enabled="true"
                                                    BehaviorID="CalendarExtenderDespatchDate" CssClass="cal_Theme1" />
                                            </td>
                                            <td width="70" valign="top" style="padding-top: 15px;" class="field_title">
                                                <asp:Label ID="Label14" Text="Depot" runat="server"></asp:Label>&nbsp;<span
                                                    class="req">*</span>
                                            </td>
                                            <td class="field_input" width="240">
                                                <asp:DropDownList ID="ddlDepot" Width="240" runat="server" AutoPostBack="true" class="chosen-select"
                                                    data-placeholder="Choose Depot" AppendDataBoundItems="True" ValidationGroup="Save"
                                                    OnSelectedIndexChanged="ddlDepot_SelectedIndexChanged" Enabled="false">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorDepot" runat="server" ControlToValidate="ddlDepot"
                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Save"
                                                    ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDepot"
                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Save"
                                                    ForeColor="Red" InitialValue="-1"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDepot"
                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Save"
                                                    ForeColor="Red" InitialValue="-3"></asp:RequiredFieldValidator>
                                            </td>
                                            <td width="92" class="field_title">
                                                <asp:Label ID="lblTransporter" Text="Transporter" runat="server"></asp:Label>&nbsp;<span
                                                    class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <asp:DropDownList ID="ddlTransporter" Width="210" runat="server" class="chosen-select"
                                                    data-placeholder="Choose Transporter" AppendDataBoundItems="True" ValidationGroup="Save">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorTransporter" runat="server"
                                                    ControlToValidate="ddlTransporter" ValidateEmptyText="false" SetFocusOnError="true"
                                                    ErrorMessage="Required!" ValidationGroup="Save" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr style="display:none;">
                                            <td class="field_input" colspan="6" style="padding-left: 10px">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="90" class="innerfield_title">
                                                            <asp:Label ID="lblVehicle" Text="Vehicle No." runat="server"></asp:Label>
                                                        </td>
                                                        <td width="153">
                                                            <asp:TextBox ID="txtVehicle" runat="server" Width="130" placeholder="Vehicle No"> </asp:TextBox>
                                                        </td>
                                                        <td class="innerfield_title" width="130">
                                                            <asp:Label ID="lblTransportMode" Text="Mode&nbsp;of&nbsp;Transport" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="180">
                                                            <asp:DropDownList ID="ddlTransportMode" Width="150" runat="server" class="chosen-select"
                                                                data-placeholder="Select Transport Mode">
                                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="By Road" Value="By Road"></asp:ListItem>
                                                                <asp:ListItem Text="By Rail" Value="By Rail"></asp:ListItem>
                                                                <asp:ListItem Text="By Air" Value="By Air"></asp:ListItem>
                                                                <asp:ListItem Text="By Ship" Value="By Ship"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="innerfield_title" width="100">
                                                            <asp:Label ID="Label13" Text="Payment Mode" runat="server"></asp:Label>&nbsp;<span
                                                                class="req">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlPaymentMode" Width="120" runat="server" class="chosen-select"
                                                                data-placeholder="Choose Payment Mode" >
                                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Advance" Value="Cash"></asp:ListItem>
                                                                <asp:ListItem Text="Credit" Value="Credit"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr style="display:none;">
                                            <td class="field_title">
                                                <asp:Label ID="lblLRGRNo" Text="LR/GR No" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input" colspan="5">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="150">
                                                            <asp:TextBox ID="txtLRGRNo" runat="server" Width="130" placeholder="LR/GR No"> </asp:TextBox>
                                                        </td>
                                                        <td width="75" class="innerfield_title">
                                                            <asp:Label ID="lblLRGRDate" Text="LR/GR Date" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="165">
                                                            <asp:TextBox ID="txtLRGRDate" runat="server" Enabled="false" Width="105" placeholder="dd/MM/yyyy"
                                                                MaxLength="10"></asp:TextBox>
                                                            <asp:ImageButton ID="imgbtnLRGRCalendar" runat="server" ImageUrl="~/images/calendar.png"
                                                                Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderLRGRDate" TargetControlID="txtLRGRDate"
                                                                PopupButtonID="imgbtnLRGRCalendar" runat="server" Format="dd/MM/yyyy" Enabled="true"
                                                                BehaviorID="CalendarExtenderLRGRDate" CssClass="cal_Theme1" />
                                                        </td>
                                                        <td width="98" class="innerfield_title">
                                                            <asp:Label ID="Label10" Text="GATE PASS NO" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="145">
                                                            <asp:TextBox ID="txtGetpass" runat="server" Width="130" placeholder="GATE PASS NO"> </asp:TextBox>
                                                        </td>
                                                        <td width="100" class="innerfield_title">
                                                            <asp:Label ID="Label11" Text="Gate Pass Date" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtGetPassDate" runat="server" Enabled="false" Width="105" placeholder="dd/MM/yyyy"
                                                                MaxLength="10"></asp:TextBox>
                                                            <asp:ImageButton ID="ImageButtonGetPass" runat="server" ImageUrl="~/images/calendar.png"
                                                                Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderGetPassDate" TargetControlID="txtGetPassDate"
                                                                PopupButtonID="ImageButtonGetPass" runat="server" Format="dd/MM/yyyy" Enabled="true"
                                                                BehaviorID="CalendarExtenderGetPassDate" CssClass="cal_Theme1" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="Label26" Text="BUSINESS SEGMENT" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                            </td>
                                            <td class="field_input" colspan="5">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="230">
                                                            <asp:DropDownList ID="ddlBS" runat="server" class="chosen-select" data-placeholder="Select Business Segment"
                                                                AppendDataBoundItems="True" Width="215px" ValidationGroup="A" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlBS_SelectedIndexChanged">
                                                                <asp:ListItem Text="Select Business Segment" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorBS" runat="server" ControlToValidate="ddlBS"
                                                                ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Save"
                                                                ForeColor="Red" InitialValue="0">
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td width="75" class="innerfield_title">
                                                            <asp:Label ID="Label5" Text="Group" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                        </td>
                                                        <td width="230">
                                                            <asp:DropDownList ID="ddlgroup" runat="server" class="chosen-select" data-placeholder="Select Group"
                                                                AppendDataBoundItems="True" Width="215px" ValidationGroup="A" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlgroup_SelectedIndexChanged">
                                                                <asp:ListItem Text="Select Group Name" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlgroup"
                                                                ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Save"
                                                                ForeColor="Red" InitialValue="0">
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                        <td width="80" class="innerfield_title"   >
                                                            <asp:Label ID="lblTPU" Text="CUSTOMER" runat="server" Font-Bold="true"></asp:Label>&nbsp;<span class="req">*</span>
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:DropDownList ID="ddlDistributor" runat="server" class="chosen-select" data-placeholder="Select Customer"
                                                                AppendDataBoundItems="True" Width="215px" ValidationGroup="A" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlDistributor_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="CV_ddlDistributor" runat="server" ControlToValidate="ddlDistributor"
                                                                ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Save"
                                                                ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                            <asp:RadioButton ID="rdbTax" runat="server" Text="With&nbsp;Tax" Checked="true" GroupName="TAX" AutoPostBack="true" Visible="false" oncheckedchanged="rdbTax_CheckedChanged"/>&nbsp;&nbsp;&nbsp;
                                                            <asp:RadioButton ID="rdbNoTax" runat="server" Text="Without&nbsp;Tax" GroupName="TAX" AutoPostBack="true" Visible="false" oncheckedchanged="rdbNoTax_CheckedChanged"/>&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="lblWayBill" Text="WayBill No." runat="server" Visible="false"></asp:Label>
                                                <asp:Label ID="lblInvNo" Text="Invoice No." runat="server" ></asp:Label>
                                            </td>
                                            <td class="field_input" colspan="5">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="230">
                                                            <asp:DropDownList ID="ddlWaybill" Width="215" runat="server" class="chosen-select"
                                                                data-placeholder="Select Waybill" AppendDataBoundItems="True" Visible="false">
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtInvNo" runat="server" Width="210" placeholder="INVOICE NO." onchange="CHKInvoiceNo();" Enabled="true" onkeypress="return isNumberKeyWithSlashHyphenAtZ(event);"> </asp:TextBox>
                                                        </td>
                                                        <td width="75" class="innerfield_title">
                                                            <asp:Label ID="Label28" Text="Invoice Date" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="230">
                                                            <asp:TextBox ID="txtInvDate" runat="server" Width="210" placeholder="INVOICE DATE"> </asp:TextBox>
                                                        </td>
                                                        <td width="90" class="innerfield_title">
                                                            &nbsp;
                                                        </td>
                                                        <td width="134">
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    </fieldset> </td> </tr>
                                    <tr>
                                        <td class="field_input" style="padding-left: 10px">
                                            <fieldset>
                                                <legend id="LgndPoDetails" runat="server">Sale Order Details</legend>
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td class="field_title">
                                                            <asp:Label ID="Label17" Text="Category" runat="server"></asp:Label>
                                                        </td>
                                                        <td class="field_input" colspan="3">
                                                            <asp:DropDownList ID="ddlCategory" runat="server" class="chosen-select" data-placeholder="Select Category"
                                                                AppendDataBoundItems="True" AutoPostBack="true" ValidationGroup="A" Width="215"
                                                                OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="field_title" style="display: none">
                                                            &nbsp;
                                                        </td>
                                                        <td class="field_input" style="display: none">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td class="field_title">
                                                            <asp:Label ID="Label1" Text="Product" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                        </td>
                                                        <td class="field_input" colspan="5">
                                                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td width="425">
                                                                        <asp:DropDownList ID="ddlProduct" runat="server" class="chosen-select" data-placeholder="Select Item"
                                                                            AppendDataBoundItems="True" Width="420px" ValidationGroup="A" AutoPostBack="true"
                                                                            OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlProduct"
                                                                            ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="A"
                                                                            ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td width="80" class="innerfield_title">
                                                                        <asp:Label ID="Label3" Text="Packsize" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlPacksize" runat="server" class="chosen-select" data-placeholder="Select Packsize"
                                                                            AppendDataBoundItems="True" AutoPostBack="true" Width="150px" ValidationGroup="A" >
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlPacksize"
                                                                            ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="A"
                                                                            ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td colspan="6">
                                                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                                <tr style="display:none;">
                                                                    <td width="100" class="field_title">
                                                                        <asp:Label ID="Label6" Text="Order Date" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                                    </td>
                                                                    <td width="125" class="field_input">
                                                                        <asp:TextBox runat="server" ID="txtOrderDate" Width="120" placeholder="Order Date"
                                                                            Enabled="false"></asp:TextBox>
                                                                        <asp:ImageButton ID="ImageButton21" runat="server" ImageUrl="~/images/calendar.png"
                                                                            Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator66" runat="server" ControlToValidate="txtOrderDate"
                                                                            ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="A"
                                                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender111" TargetControlID="txtOrderDate"
                                                                            PopupButtonID="ImageButton21" runat="server" Format="dd/MM/yyyy" Enabled="true"
                                                                            BehaviorID="CalendarExtenderOrderDate" CssClass="cal_Theme1" />
                                                                    </td>
                                                                    <td width="95" class="field_title">
                                                                        <asp:Label ID="Label7" Text="Assesment(%)" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td width="130" class="field_input">
                                                                        <asp:TextBox runat="server" ID="txtAssementPercentage" Width="130" placeholder="Assesment(%)"
                                                                            Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                    <td width="100" class="field_title">
                                                                        <asp:Label ID="lblPOQTY" Text="Stock Qty" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td width="140" class="field_input">
                                                                        <asp:TextBox runat="server" ID="txtStockQty" Width="130" placeholder="Stock Qty"
                                                                            Enabled="false"></asp:TextBox>
                                                                    </td>
                                                                    
                                                                </tr>

                                                                <tr style="display:none">
                                                                    <td width="100" class="field_title">
                                                                        <asp:Label ID="Label4" Text="Batch No" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td  class="field_input">
                                                                        <asp:TextBox runat="server" ID="txtBatchNo" Width="120" placeholder="Batch No"
                                                                            ></asp:TextBox>
                                                                        
                                                                       
                                                                    </td>
                                                                    <td  class="field_title">
                                                                        <asp:Label ID="Label15" Text="MFG Date" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td  class="field_input">
                                                                        <asp:TextBox runat="server" ID="txtMFDate" Width="110" placeholder="dd/MM/yyyy"
                                                                           OnTextChanged="txtMFDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                         <asp:ImageButton ID="ImageButtonMFDate" runat="server" ImageUrl="~/images/calendar.png"
                                                                            Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtenderMFDate" TargetControlID="txtMFDate"
                                                                            PopupButtonID="ImageButtonMFDate" runat="server" Format="dd/MM/yyyy" Enabled="true"
                                                                            BehaviorID="CalendarExtenderMFDate" CssClass="cal_Theme1" />
                                                                    </td>
                                                                    <td class="field_title">
                                                                        <asp:Label ID="Label20" Text="EXPR Date" runat="server"></asp:Label>
                                                                    </td>
                                                                    <td  class="field_input">
                                                                        <asp:TextBox runat="server" ID="txtEXPRDate" Width="110" placeholder="dd/MM/yyyy"
                                                                            ></asp:TextBox>
                                                                        <asp:ImageButton ID="ImageButtonExprDate" runat="server" ImageUrl="~/images/calendar.png"
                                                                            Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                                        
                                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtenderExpr" TargetControlID="txtEXPRDate"
                                                                            PopupButtonID="ImageButtonExprDate" runat="server" Format="dd/MM/yyyy" Enabled="true"
                                                                            BehaviorID="CalendarExtenderExpr" CssClass="cal_Theme1" />
                                                                    </td>
                                                                    
                                                                </tr>

                                                                <tr>
                                                                    <td class="field_title" width="100">
                                                                        <asp:Label ID="Label9" Text="MRP" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                                    </td>
                                                                    <td class="field_input">
                                                                        <asp:TextBox runat="server" ID="txtMRP" Width="130" placeholder="MRP" Enabled="true" onkeypress="return isNumberKeyWithDot(event);"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtMRP"
                                                                            ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="A"
                                                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        <asp:HiddenField runat="server" ID="hdn_PackSizeQC" />
                                                                        <asp:HiddenField runat="server" ID="hdn_bsid" />
                                                                        <asp:HiddenField runat="server" ID="hdn_bsname" />
                                                                        <asp:HiddenField runat="server" ID="hdn_SaleOrderID" />
                                                                    </td>
                                                                    <td class="field_title">
                                                                        <asp:Label ID="Label8" Text="RATE" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                                    </td>
                                                                    <td class="field_input">
                                                                        <asp:TextBox runat="server" ID="txtBaseCost" Width="130" placeholder="RATE" Enabled="true" onkeypress="return isNumberKeyWithDot(event);"></asp:TextBox>
                                                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtBaseCost"
                                                                            ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="A"
                                                                            ForeColor="Red"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td class="field_title">
                                                                        <asp:Label ID="Label12" Text="Returned&nbsp;Qty" runat="server"></asp:Label>&nbsp;<span
                                                                            class="req">*</span>
                                                                    </td>
                                                                    <td class="field_input" >
                                                                        <asp:TextBox runat="server" ID="txtDeliveredQty" Width="65" placeholder="CASE" 
                                                                            onkeypress="return isNumberKey(event);" Visible="false" ></asp:TextBox>
                                                                        
                                                                        <asp:TextBox runat="server" ID="txtDeliveredQtyPCS" Width="65"  onkeypress="return isNumberKey(event);" placeholder="PCS"></asp:TextBox>
                                                                        <div class="btn_24_blue">
                                                                            <span class="icon add_co"></span>
                                                                            <asp:Button ID="btnADDGrid" runat="server" Text="Add" CssClass="btn_link" OnClick="btnADDGrid_Click"
                                                                                ValidationGroup="A"  />
                                                                        </div>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDeliveredQty" runat="server"
                                                                            ControlToValidate="txtDeliveredQty" ValidateEmptyText="false" SetFocusOnError="true"
                                                                            ErrorMessage="Required!" ValidationGroup="A" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorDeliveredQtyPCS" runat="server"
                                                                            ControlToValidate="txtDeliveredQtyPCS" ValidateEmptyText="false" SetFocusOnError="true"
                                                                            ErrorMessage="Required!" ValidationGroup="A" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </td>
                                    </tr>
                                    </table>
                                    <div class="gridcontent-inner">
                                        <fieldset>
                                            <legend>Product Details</legend>
                                            <div style="overflow: hidden; width: 100%;" id="DivHeaderRow">
                                            </div>
                                            <div style="overflow: scroll; margin-bottom: 8px; margin-right:6px;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                                <asp:GridView ID="grdAddDespatch" runat="server" Width="100%" ShowFooter="true" AutoGenerateColumns="true"
                                                    EmptyDataText="No Records Available" CssClass="reportgrid">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="DELETE" ItemStyle-Width="20" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <nav style="height: 0px;z-index: 2; padding-left:50px; position: relative; text-align:center;"><span class="badge green"><%# Container.DataItemIndex + 1 %></span></nav>
                                                                <asp:Button ID="btn_TempDelete" runat="server" CssClass="action-icons c-delete"
                                                                    OnClientClick="return confirm('Are you sure you want to Delete?')" ToolTip="Delete"
                                                                    OnClick="btn_TempDelete_Click" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:HiddenField ID="hdndtDespatchDelete" runat="server" />
                                                <asp:HiddenField ID="hdndtPOIDDelete" runat="server" />
                                                <asp:HiddenField ID="hdndtPRODUCTIDDelete" runat="server" />
                                                <asp:HiddenField ID="hdndtBATCHDelete" runat="server" />
                                            </div>
                                            <div id="DivFooterRow" style="overflow: hidden">
                                            </div>
                                            
                                        </fieldset>
                                    </div>
                                    
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left:10px">
                                                <fieldset>
                                                    <legend>Item Wise Amount And Tax</legend>
                                                    <table>
                                                        <tr>
                                                            <td width="100" class="field_title">
                                                                <asp:Label ID="Label21" Text="Tot.Basic Amt." runat="server"></asp:Label>
                                                            </td>
                                                            <td width="125" class="field_input">
                                                                <asp:TextBox runat="server" ID="txtAmount" CssClass="x_large" placeholder="Tot.Basic Amt."
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="90" class="field_title" style="display:none;">
                                                                <asp:Label ID="Label311" Text="Tot.MRP" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="125" class="field_input" style="display:none;">
                                                                <asp:TextBox runat="server" ID="txtTotMRP" CssClass="x_large" placeholder="Tot.MRP"
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="90" class="field_title">
                                                                <asp:Label ID="Label22" Text="Tot.Tax&nbsp;Amt." runat="server"></asp:Label>
                                                            </td>
                                                            <td width="125" class="field_input">
                                                                <asp:TextBox runat="server" ID="txtTotTax" CssClass="x_large" placeholder="Tot.Tax&nbsp;Amt."
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="100" class="field_title">
                                                                <asp:Label ID="Label23" Text="(Basic&nbsp;+&nbsp;Tax)&nbsp;Amt." runat="server"></asp:Label>
                                                            </td>
                                                            <td width="130" class="field_input">
                                                                <asp:TextBox runat="server" ID="txtNetAmt" CssClass="x_large" placeholder="(Basic&nbsp;+&nbsp;Tax)&nbsp;Amt."
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                            
                                                        </tr>

                                                        <tr style="display:none;">
                                                            <td width="100" class="field_title">
                                                                <asp:Label ID="Label38" Text="Scheme&nbsp;Amt." runat="server"></asp:Label>
                                                            </td>
                                                            <td width="125" class="field_input">
                                                                <asp:TextBox runat="server" ID="txtSchemeAmt" CssClass="x_large" placeholder="Scheme&nbsp;Amt."
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="90" class="field_title">
                                                                <asp:Label ID="Label18" Text="Tot.&nbsp;Special&nbsp;Disc." runat="server"></asp:Label>
                                                            </td>
                                                            <td width="125" class="field_input">
                                                               <asp:TextBox runat="server" ID="txtTotDisc" CssClass="x_large" placeholder="Tot.&nbsp;Special&nbsp;Disc."
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="90" class="field_title">
                                                                <asp:Label ID="Label36" Text="SS Margin(%)" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="125" class="field_input">
                                                                <asp:TextBox runat="server" ID="txtSSMarginPercentage" CssClass="x_large" placeholder="SS Margin(%)"
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="100" class="field_title">
                                                                <asp:Label ID="Label37" Text="SS Margin Amt." runat="server"></asp:Label>
                                                            </td>
                                                            <td width="130" class="field_input">
                                                                <asp:TextBox runat="server" ID="txtSSMarginAmt" CssClass="x_large" placeholder="SS Margin Amt."
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                            
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr style="display:none;">
                                            <td class="field_input" style="padding-left: 10px">
                                                <table width="90%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td>
                                                            <fieldset>
                                                                <legend>Terms & Conditions</legend>
                                                                <div class="gridcontent-shortstock">
                                                                    <cc1:Grid ID="grdTerms" runat="server" CallbackMode="true" Serialize="true" AutoGenerateColumns="false"
                                                                        FolderStyle="../GridStyles/premiere_blue" AllowAddingRecords="false" AllowFiltering="true"
                                                                        AllowSorting="false" OnRowDataBound="grdTerms_RowDataBound" AllowPageSizeSelection="false">
                                                                        <FilteringSettings InitialState="Visible" />
                                                                        <Columns>
                                                                            <cc1:Column ID="Column3" DataField="SLNO" HeaderText="Sl No" runat="server" Width="60">
                                                                                <TemplateSettings TemplateId="tplTermsNumbering" />
                                                                            </cc1:Column>
                                                                            <cc1:Column ID="Column10" DataField="DESCRIPTION" ReadOnly="true" HeaderText="TERMS & Conditions"
                                                                                runat="server" Width="320" Wrap="true" />
                                                                            <cc1:Column ID="Column5" DataField="ID" ReadOnly="true" HeaderText="TERMSID" runat="server"
                                                                                Width="60">
                                                                                <TemplateSettings TemplateId="CheckTemplateTERMS" HeaderTemplateId="HeaderTemplateTERMS" />
                                                                            </cc1:Column>
                                                                        </Columns>
                                                                        <FilteringSettings MatchingType="AnyFilter" />
                                                                        <Templates>
                                                                            <cc1:GridTemplate runat="server" ID="tplTermsNumbering">
                                                                                <Template>
                                                                                    <asp:Label ID="lblslnoTax" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                                                </Template>
                                                                            </cc1:GridTemplate>
                                                                            <cc1:GridTemplate runat="server" ID="CheckTemplateTERMS">
                                                                                <Template>
                                                                                    <asp:HiddenField runat="server" ID="hdnTERMSName" Value='<%# Container.DataItem["ID"] %>' />
                                                                                    <asp:CheckBox ID="ChkIDTERMS" runat="server" Text=" " ToolTip="<%# Container.Value %>" />
                                                                                </Template>
                                                                            </cc1:GridTemplate>
                                                                        </Templates>
                                                                        <ScrollingSettings ScrollWidth="100%" ScrollHeight="180" />
                                                                    </cc1:Grid>
                                                                </div>
                                                            </fieldset>
                                                        </td>
                                                        <td width="20px">
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <fieldset>
                                                                <legend>Gross Tax Details</legend>
                                                                <div class="gridcontent-shortstock">
                                                                    <cc1:Grid ID="grdTax" runat="server" CallbackMode="false" Serialize="true" AutoGenerateColumns="false"
                                                                        FolderStyle="../GridStyles/premiere_blue" AllowAddingRecords="false" AllowFiltering="true"
                                                                        AllowSorting="false"  AllowPageSizeSelection="false">
                                                                        <FilteringSettings InitialState="Visible" />
                                                                        <Columns>
                                                                            <cc1:Column ID="Column2" DataField="SLNO" HeaderText="Sl No" runat="server" Width="50">
                                                                                <TemplateSettings TemplateId="tplTaxNumbering" />
                                                                            </cc1:Column>
                                                                            <cc1:Column ID="Column53" DataField="NAME" ReadOnly="true" HeaderText="TAX NAME"
                                                                                runat="server" Width="150" Wrap="true" />
                                                                            <cc1:Column ID="Column54" DataField="PERCENTAGE" ReadOnly="true" HeaderText="TAX(%)"
                                                                                runat="server" Width="150" />
                                                                            <cc1:Column ID="Column111" DataField="TAXVALUE" ReadOnly="true" HeaderText="Value"
                                                                                runat="server" Width="150" />
                                                                        </Columns>
                                                                        <FilteringSettings MatchingType="AnyFilter" />
                                                                        <Templates>
                                                                            <cc1:GridTemplate runat="server" ID="tplTaxNumbering">
                                                                                <Template>
                                                                                    <asp:Label ID="lblslnoTax" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                                                </Template>
                                                                            </cc1:GridTemplate>
                                                                        </Templates>
                                                                        <ScrollingSettings ScrollWidth="100%" ScrollHeight="180" />
                                                                    </cc1:Grid>
                                                                </div>
                                                            </fieldset>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px">
                                                <fieldset>
                                                    <legend>Gross&nbsp;Amount&nbsp;Details</legend>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="85" class="field_title">
                                                                <asp:Label ID="Label24" Text="Gross Amt." runat="server"></asp:Label>
                                                            </td>
                                                            <td width="100" class="field_input">
                                                                <asp:TextBox runat="server" ID="txtTotalGross" CssClass="x-large" placeholder="Gross Amt."
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                            
                                                            <td width="55" class="field_title">
                                                                <asp:Label ID="Label27" Text="R/O" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="100" class="field_input">
                                                                <asp:TextBox runat="server" ID="txtRoundoff" CssClass="x-large" placeholder="R/O"
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                            
                                                            <td width="65" class="field_title">
                                                                <asp:Label ID="Label33" Text="Net Amt." runat="server"></asp:Label>
                                                            </td>
                                                            <td width="100" class="field_input">
                                                                <asp:TextBox runat="server" ID="txtFinalAmt" CssClass="x_large" placeholder="Net Amt."
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                            
                                                            <td width="65" class="field_title">
                                                                <asp:Label ID="Label35" Text="Tot.Pcs." runat="server"></asp:Label>
                                                            </td>
                                                            <td width="100" class="field_input">
                                                                <asp:TextBox runat="server" ID="txtTotPCS" CssClass="x_large" placeholder="Tot.Pcs."
                                                                    Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td width="65" class="field_title">
                                                                <asp:Label ID="Label25" Text="Tot.Case" runat="server" Visible="false"></asp:Label>
                                                            </td>
                                                            <td width="100" class="field_input">
                                                                <asp:TextBox runat="server" ID="txtTotCase" CssClass="x_large" placeholder="Tot.Case" Enabled="true" Visible="false"></asp:TextBox>
                                                                    
                                                            </td>
                                                        </tr>
                                                        
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 8px 10px">
                                                <table border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td width="80" class="innerfield_title">
                                                            <asp:Label ID="lblRemarks" Text="Remarks" runat="server"></asp:Label>
                                                        </td>
                                                        <td width="320">
                                                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="mid" TextMode="MultiLine" Style="width: 300px;
                                                                height: 80px" MaxLength="255"> </asp:TextBox>
                                                        </td>
                                                        <td width="5">
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <td width="100" align="left" class="innerfield_title">
                                                                <asp:Label ID="lblCheckerNote" Text="Rejection Note" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="310">
                                                                <asp:TextBox ID="txtCheckerNote" runat="server" TextMode="MultiLine" Enabled="false"
                                                                    Style="width: 290px; height: 80px" MaxLength="255"> </asp:TextBox>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="8" colspan="6">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            &nbsp;
                                                        </td>
                                                        <td colspan="5">
                                                            <div class="btn_24_blue" id="btnsubmitdiv" runat="server">
                                                                <span class="icon disk_co"></span>
                                                                <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn_link" OnClick="btnSubmit_Click"
                                                                    ValidationGroup="Save" />
                                                            </div>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <div class="btn_24_blue">
                                                                <span class="icon cross_octagon_co"></span>
                                                                <asp:Button ID="btnCancel" runat="server" Text="Close" CssClass="btn_link" CausesValidation="false"
                                                                    OnClick="btnCancel_Click" />
                                                            </div>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <div class="btn_24_green" id="divbtnapprove" runat="server">
                                                                <span class="icon approve_co"></span>
                                                                <asp:LinkButton ID="btnApprove" runat="server" Text="Approve" CssClass="btn_link"
                                                                    CausesValidation="false" OnClick="btnApprove_Click" />
                                                            </div>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <div class="btn_24_red" id="divbtnreject" runat="server">
                                                                <span class="icon reject_co"></span>
                                                                <asp:LinkButton ID="btnReject" runat="server" Text="Reject" CssClass="btn_link" CausesValidation="false"
                                                                     />
                                                            </div>
                                                            <asp:HiddenField runat="server" ID="hdnDespatchID" />
                                                            <asp:HiddenField runat="server" ID="hdnWaybillNo" />
                                                            <asp:HiddenField runat="server" ID="hdnCustomerID" />
                                                            <asp:HiddenField runat="server" ID="hdnCFormNo" />
                                                            <asp:HiddenField runat="server" ID="hdnCFormDate" />
                                                            <asp:HiddenField runat="server" ID="Hdn_Print" />
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
                                                        <td valign="top" style="padding-top: 10px;" width="90">
                                                            <asp:Label ID="Label16" runat="server" Text="From Date"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="165">
                                                            <asp:TextBox ID="txtFromDate" runat="server" MaxLength="10" Width="120" Enabled="false"
                                                                placeholder="dd/MM/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopup" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarFromDate" PopupButtonID="imgPopup" runat="server"
                                                                TargetControlID="txtFromDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorFromDate" runat="server" ControlToValidate="txtFromDate"
                                                                SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Search" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td valign="top" style="padding-top: 10px;" width="70">
                                                            <asp:Label ID="Label19" runat="server" Text="To Date"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="165">
                                                            <asp:TextBox ID="txtToDate" runat="server" MaxLength="10" Width="120" Enabled="false"
                                                                placeholder="dd/mm/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopuptodate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderToDate" PopupButtonID="imgPopuptodate"
                                                                runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorToDate" runat="server" ControlToValidate="txtToDate"
                                                                SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Search" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td valign="top" width="110">
                                                            <div class="btn_24_blue">
                                                                <span class="icon find_co"></span>
                                                                <asp:Button ID="btnSearchInvoice" runat="server" Text="Search" CssClass="btn_link"
                                                                    ValidationGroup="Search" OnClick="btnSearchInvoice_Click" />
                                                            </div>
                                                        </td>
                                                        <td valign="top" width="60" style="padding-top: 10px;">
                                                            <asp:Label ID="Label120" runat="server" Text="Filter" Visible="false"></asp:Label>
                                                        </td>
                                                        <td valign="top">
                                                            <asp:DropDownList ID="ddlWaybillFilter" Width="150" runat="server" class="chosen-select"
                                                                data-placeholder="Choose Waybill Filter Mode" AutoPostBack="true" OnSelectedIndexChanged="ddlWaybillFilter_SelectedIndexChanged"
                                                                Visible="false">
                                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Without Waybill" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="With Waybill" Value="2"></asp:ListItem>
                                                            </asp:DropDownList>
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
                                    <div class="gridcontent-inner" style="padding-left: 8px;">
                                        <cc1:Grid ID="grdDespatchHeader" runat="server" CallbackMode="false" Serialize="true"
                                            FolderStyle="../GridStyles/premiere_blue" AutoGenerateColumns="false" OnDeleteCommand="DeleteRecordInvoice"
                                            OnRowDataBound="grdDespatchHeader_RowDataBound" AllowAddingRecords="false" AllowFiltering="true"
                                            AllowPageSizeSelection="true" PageSize="50">
                                            <ClientSideEvents OnBeforeClientDelete="OnBeforeInvoiceDelete" OnClientDelete="OnDeleteInvoiceDetails" />
                                            <FilteringSettings InitialState="Visible" />
                                            <Columns>
                                                <cc1:Column ID="Column12" DataField="Sl" HeaderText="SL" runat="server" Width="60">
                                                    <TemplateSettings TemplateId="slnoTemplateIN" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column19" DataField="SALERETURNID" HeaderText="SALERETURNID" runat="server"
                                                    Width="60" Visible="false" />
                                                <cc1:Column ID="Column91" DataField="SALERETURNNO" HeaderText="RETURN NO." runat="server"
                                                    Width="250">
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
                                                <cc1:Column ID="Column8" DataField="SALERETURNDATE" HeaderText="DATE" runat="server"
                                                    Width="120">
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
                                                <cc1:Column ID="Column17" DataField="TRANSPORTERNAME" HeaderText="TRANSPORTER" runat="server"
                                                    Width="170" Wrap="true" Visible="false">
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
                                                <cc1:Column ID="Column89" DataField="DEPOTNAME" HeaderText="DEPOT" runat="server"
                                                    Width="150" Wrap="true" Visible="false">
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
                                                <cc1:Column ID="Column79" DataField="FINYEAR" HeaderText="FINYEAR" runat="server"
                                                    Width="100" Visible="false" />
                                                <cc1:Column ID="Column199" DataField="DISTRIBUTORNAME" HeaderText="CUSTOMERNAME"
                                                    runat="server" Width="250" />
                                                <cc1:Column ID="Column24" DataField="ISVERIFIED" HeaderText="ISVERIFIED" Visible="false"
                                                    runat="server" Width="100" />
                                                <cc1:Column ID="Column25" DataField="ISVERIFIEDDESC" HeaderText="FINANCIAL STATUS"
                                                    runat="server" Width="140" Wrap="true">
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
                                                <cc1:Column ID="Column37" DataField="DAYENDTAG" HeaderText="DAY-END STATUS" runat="server"
                                                    Width="120" Wrap="false">
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
                                                <cc1:Column ID="Column311" DataField="GETPASSNO" HeaderText="GETPASSNO" runat="server"
                                                    Width="130" Wrap="true" Visible="false">
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

                                                <cc1:Column ID="Column1" DataField="NETAMOUNT" HeaderText="NET AMOUNT" runat="server"
                                                    Width="130" Wrap="true">
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

                                                <cc1:Column ID="Column92" AllowEdit="true" AllowDelete="true" HeaderText="EDIT" runat="server"
                                                    Width="70">
                                                    <TemplateSettings TemplateId="editBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="CANCEL" AllowEdit="false" AllowDelete="true" Width="95">
                                                    <TemplateSettings TemplateId="deleteBtnTemplateInvoice" />
                                                </cc1:Column>
                                                 <cc1:Column ID="Column7" AllowEdit="false" AllowDelete="false" HeaderText="Print"
                                                    runat="server" Width="80" Wrap="true">
                                                    <TemplateSettings TemplateId="PrintBtnTemplate" />
                                                </cc1:Column>
                                            </Columns>
                                            <FilteringSettings MatchingType="AnyFilter" />
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="editBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-edit" id="btnGridEdit_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethod(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                              <Templates>
                                                <cc1:GridTemplate runat="server" ID="PrintBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="filter_btn printer_co" id="btnPrint_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethodPrint(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            
                                            
                                            
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="deleteBtnTemplateInvoice">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-delete" title="Cancel" onclick="grdDespatchHeader.delete_record(this)">
                                                        </a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                                <cc1:GridTemplate runat="server" ID="slnoTemplateIN">
                                                    <Template>
                                                        <asp:Label ID="lblslnoin" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <ScrollingSettings ScrollWidth="100%" ScrollHeight="290" NumberOfFixedColumns="3" />
                                        </cc1:Grid>
                                        <asp:Button ID="btngrdedit" runat="server" Text="grdedit" Style="display: none" OnClick="btngrdedit_Click"
                                            CausesValidation="false" />
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" Style="display: none" OnClick="btnPrint_Click"
                                           CausesValidation="false" />
                                        
                                        
                                    </div>
                                </asp:Panel>
                                
                                
                            </div>
                        </div>
                        
                        
                    </div>
                   
                   
                </div>
                <cc1:MessageBox ID="MessageBox1" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
