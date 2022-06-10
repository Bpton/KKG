<%@ page title="" language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true"
     CodeFile="frmPurchaseOrder_KKG.aspx.cs" Inherits="VIEW_frmPurchaseOrder_KKG" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="js/36d/jquery-1.4.2.min.js"></script>

    <script type="text/javascript">
        function pageload(sender, args) {
            //calculation();
        }
    </script>
    <script type="text/javascript">
        function Calpurchaseorderdetails(pcsqty, purchasecost, poqty, basiccostvalue) {
            debugger;
            var tpoqty = parseFloat(document.getElementById(poqty).value);
            var Totalbasiccostvalue = parseFloat(pcsqty * purchasecost * tpoqty).toFixed(2);
            if (isNaN(Totalbasiccostvalue)) {
                Totalbasiccostvalue = 0;
            }

            basiccostvalue.value = Totalbasiccostvalue;
            /*var alltotalmrp = parseFloat(pcsqty * tpoqty * mrp).toFixed(2);
            if (isNaN(alltotalmrp)) {
                alltotalmrp = 0;
            }
            totalmrp.value = alltotalmrp;*/
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

        function OnScrollDivOut(Scrollablediv) {
            document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
            document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">




    <style>
        th {
            background: cornflowerblue !important;
            color: white !important;
            position: sticky !important;
            top: 0;
            box-shadow: 0 2px 2px -1px rgb(255, 106, 0);
            width: 400px;
        }

        th, td {
            padding: 0.25rem;
        }
    </style>



    <script type="text/javascript"> 
        $(document).ready(function () {
            $("form").bind("keypress", function (e) {
                if (e.keyCode == 13) {
                    alert("Enter key is block for this page!!!!");/*AS PER REQUIREMENT*/
                    return false;
                }
            });

        <%--  var grd = document.getElementById('<%= grdpodetailsadd.ClientID %>');--%>
            $('<%= grdpodetailsadd.ClientID %>').click(function () {
                alert('hi');
                if ($(this).css("background-color") == "gray") {
                    $(this).css("background-color", "white");
                }
                else {
                    $(this).css("background-color", "gray");
                }
            });


        });
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


        function HighlightRow(chkB) {

            var IsChecked = chkB.checked;

            if (IsChecked) {

                chkB.parentElement.parentElement.style.backgroundColor = 'PaleGreen';

                chkB.parentElement.parentElement.style.color = 'black';

            } else {

                chkB.parentElement.parentElement.style.backgroundColor = 'PowderBlue';

                chkB.parentElement.parentElement.style.color = 'black';

            }

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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnPOExcel" />
            <asp:PostBackTrigger ControlID="btnExportExcel" />
        </Triggers>

        <ContentTemplate>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">

                            <i class="fa fa-info">Shortcut Key Info</i>
                            <div id="show" runat="server" style="align-content: center">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                   <label style="color: blueviolet">SEARCH RECORD(ALT+A), </label>
                                &nbsp;
                                                                   <label style="color: orangered">ADD NEW RECEORD(ALT+N), </label>
                                &nbsp;
                                                                   <label style="color: blueviolet">SAVE(ALT+S), </label>
                                &nbsp;
                                                                   <label style="color: orangered">ADD PRODUCT(ALT+B),</label>&nbsp;
                                                                   <label style="color: blueviolet">APPROVE(ALT+P),</label>&nbsp;
                                                                   <label style="color: orangered">CONFIRM(ALT+C),</label>
                                <label style="color: blueviolet">REJECT(ALT+R),</label>
                                <label style="color: orangered">CLOSE(ALT+X),</label>&nbsp;
                            </div>

                            <div class="widget_top active">
                                <span class="h_icon_he list_images"></span>
                                <h6>PO Details</h6>
                                <div id="divadd" runat="server" class="btn_30_light" style="float: right;">
                                    <span id="icon" class="icon add_co"></span>
                                    <asp:Button ID="btnnewentry" runat="server" Text="Add New Record" CssClass="btn_link"
                                        OnClick="btnnewentry_Click" CausesValidation="false" AccessKey="N" />
                                </div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="InputTable" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>TPU / FACTORY DETAILS</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td width="70" class="field_title" style="padding-bottom: 10px;">&nbsp;</td>
                                                            <td width="260" style="display: none">
                                                                <asp:RadioButton ID="rdbNoPlanned" runat="server" Text="Without&nbsp;Planning" Checked="true" GroupName="TAX" AutoPostBack="true" OnCheckedChanged="rdbNoPlanned_CheckedChanged" />&nbsp;&nbsp;
                                                                <asp:RadioButton ID="rdbPlanned" runat="server" Text="With&nbsp;Planning" GroupName="TAX" AutoPostBack="true" OnCheckedChanged="rdbPlanned_CheckedChanged" />
                                                            </td>
                                                            <td width="110" class="field_title" style="display: none" id="tdDate" runat="server">
                                                                <asp:Label ID="Label8" runat="server" Text="Date"></asp:Label>
                                                            </td>
                                                            <td width="130" id="tdCalenderFrom" runat="server" style="display: none">
                                                                <asp:TextBox ID="txtReqFromDate" runat="server" Width="70" placeholder="dd/mm/yyyy" MaxLength="10" Enabled="false"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButtonReqFromDate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle" runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender6" PopupButtonID="ImageButtonReqFromDate"
                                                                    runat="server" TargetControlID="txtReqFromDate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                            <td width="130" id="tdCalenderTo" runat="server" style="display: none">
                                                                <asp:TextBox ID="txtReqToDate" runat="server" Width="70" placeholder="dd/mm/yyyy" MaxLength="10" Enabled="false"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButtonReqToDate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle" runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender7" PopupButtonID="ImageButtonReqToDate"
                                                                    runat="server" TargetControlID="txtReqToDate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                            <td width="110" class="field_title" style="display: none" id="tdReqSearch" runat="server">
                                                                <div class="btn_24_blue">
                                                                    <span class="icon find_co"></span>
                                                                    <asp:Button ID="btnReqSearch" runat="server" Text="Search" CssClass="btn_link"
                                                                        OnClick="btnReqSearch_Click" />
                                                                </div>
                                                            </td>
                                                            <td width="110" class="field_title" style="display: none" id="tdLable" runat="server">
                                                                <asp:Label ID="Label6" runat="server" Text="Req No."></asp:Label>
                                                            </td>
                                                            <td width="180" id="tdControl" runat="server" colspan="3" style="display: none">
                                                                <asp:DropDownList ID="ddlReqNo" runat="server" Width="200px" Height="28px"
                                                                    class="chosen-select" data-placeholder="SELECT REQ. NO.">
                                                                </asp:DropDownList>
                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td class="field_title" style="padding-bottom: 10px;" colspan="9">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td width="70" class="field_title" style="padding-bottom: 10px;">
                                                                <asp:Label ID="Label5" runat="server" Text="TPU"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="260">
                                                                <asp:DropDownList ID="ddlTPUName" runat="server" Width="245px" Height="28px" ValidationGroup="POFooter"
                                                                    OnSelectedIndexChanged="ddlTPUName_SelectedIndexChanged" AutoPostBack="true"
                                                                    class="chosen-select" data-placeholder="SELECT TPU NAME">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="CV_TPUNAME" runat="server" ControlToValidate="ddlTPUName"
                                                                    InitialValue="0" SetFocusOnError="true" ErrorMessage="Required!" ForeColor="Red"
                                                                    ValidationGroup="POFooter"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td width="110" class="field_title" style="padding-bottom: 10px; padding-left: 10px;">
                                                                <asp:Label ID="Label29" runat="server" Text="Payment Terms"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="180">
                                                                <asp:TextBox ID="txtPaymentTerms" runat="server" Width="150px" Height="12px" TextMode="MultiLine"
                                                                    CssClass="text-center">
                                                                </asp:TextBox>

                                                            </td>

                                                            <td width="110" class="field_title" style="padding-bottom: 10px; padding-left: 10px;">
                                                                <asp:Label ID="Label14" runat="server" Text="QUOT.REF.NO"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="180">
                                                                <asp:TextBox ID="txtqutrefno" runat="server" Width="150px" Height="12px" CssClass="text-center">
                                                                </asp:TextBox>

                                                            </td>

                                                            <td width="110" class="field_title" style="padding-bottom: 10px;">
                                                                <asp:Label ID="Label1" runat="server" Text="Qut Refno date:"></asp:Label>

                                                            </td>
                                                            <td width="135" style="padding-bottom: 10px;">
                                                                <asp:TextBox ID="txtqutrefdate" runat="server" Width="70" placeholder="dd/mm/yyyy"
                                                                    MaxLength="10" Enabled="false" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Scheduledate"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButtontxtqutrefdate" ImageUrl="../images/calendar.png"
                                                                    ImageAlign="AbsMiddle" runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButtontxtqutrefdate"
                                                                    runat="server" TargetControlID="txtqutrefdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>


                                                            <td width="110" class="field_title" style="padding-bottom: 10px;">
                                                                <asp:Label ID="Label11" runat="server" Text="Entry Date"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="135" style="padding-bottom: 10px;">
                                                                <asp:TextBox ID="txtpodate" runat="server" Width="70" placeholder="dd/mm/yyyy" MaxLength="10"
                                                                    ValidationGroup="datecheckpodetail" Enabled="false" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                <asp:ImageButton ID="imgPopuppodate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="imgPopuppodate"
                                                                    runat="server" TargetControlID="txtpodate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="CV_PODate" runat="server" ControlToValidate="txtpodate"
                                                                    ValidationGroup="datecheckpodetail" SetFocusOnError="true" ErrorMessage="PO Date is required!"
                                                                    Display="None"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server"
                                                                    TargetControlID="CV_PODate" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </td>
                                                            <td width="70" style="padding-bottom: 10px;" class="field_title" id="divponoheader"
                                                                runat="server">
                                                                <asp:Label ID="Label15" runat="server" Text="PO No"></asp:Label>
                                                            </td>
                                                            <td width="350" style="padding-bottom: 10px;" id="divpono" runat="server">
                                                                <asp:TextBox ID="txtpono" Width="100%" runat="server" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr id="trscheduledate" runat="server">
                                                            <td width="130" class="field_title">
                                                                <asp:Label ID="Label12" runat="server" Text="Delivary From Date"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="135">
                                                                <asp:TextBox ID="txtreqfromdateset" runat="server" Width="70" placeholder="dd/mm/yyyy"
                                                                    MaxLength="10" Enabled="false" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Scheduledate"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButtonreqfromdateset" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender8" PopupButtonID="ImageButtonreqfromdateset"
                                                                    runat="server" TargetControlID="txtreqfromdateset" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="rfv_txtreqfromdateset" runat="server" ControlToValidate="txtreqfromdateset"
                                                                    SetFocusOnError="true" ErrorMessage="Required!" Display="None" ValidationGroup="Scheduledate"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                                    TargetControlID="rfv_txtreqfromdateset" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </td>

                                                            <td width="130" class="field_title">
                                                                <asp:Label ID="Label2" runat="server" Text="Delivary To Date"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="135">
                                                                <asp:TextBox ID="txtreqtotodateset" runat="server" Width="70" placeholder="dd/mm/yyyy"
                                                                    MaxLength="10" Enabled="false" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Scheduledate"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButtonreqtotodateset" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender5" PopupButtonID="ImageButtonreqtotodateset"
                                                                    runat="server" TargetControlID="txtreqtotodateset" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                                <asp:RequiredFieldValidator ID="rfv_txtreqtotodateset" runat="server" ControlToValidate="txtreqtotodateset"
                                                                    SetFocusOnError="true" ErrorMessage="Required!" Display="None" ValidationGroup="Scheduledate"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender31" runat="server"
                                                                    TargetControlID="rfv_txtreqtotodateset" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </td>


                                                            <td class="field_title" style="padding-left: 10px;" runat="server" id="tdDuplicatePo">
                                                                <asp:Label ID="Label28" runat="server" Text="CHECK AND SAVE FOR DUPLICATEPO" ForeColor="Blue"></asp:Label>
                                                            </td>
                                                            <td align="left" class="field_input" style="padding-bottom: 20px;" width="10">
                                                                <asp:CheckBox ID="chkDuplicatepo" runat="server" Text=" " OnCheckedChanged="chkDuplicatepo_CheckedChanged"
                                                                    CausesValidation="false" AutoPostBack="true" />
                                                            </td>

                                                            <td width="70" class="field_title" style="display: none">
                                                                <asp:Label ID="Label4" runat="server" Text="PACKSIZE"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="70" style="display: none">
                                                                <asp:DropDownList ID="ddlpacksize" runat="server" Width="150px" Height="28px" ValidationGroup="Scheduledate"
                                                                    class="chosen-select" data-placeholder="SELECT PACKSIZE">
                                                                </asp:DropDownList>


                                                            </td>
                                                            <td colspan="3"><%-- hide for kkg as per requeriment by p.basu on 17-12-2020--%>
                                                                <div class="btn_24_blue" style="display: none">
                                                                    <span class="icon add_co"></span>
                                                                    <asp:Button ID="btnscheduledate" runat="server" Text="Set Date" CssClass="btn_link" ValidationGroup="Scheduledate"
                                                                        OnClick="btnscheduledate_Click" />
                                                                </div>
                                                            </td>
                                                            <%--  <td width="150" class="field_title"  >
                                                               
                                                                  <asp:TextBox runat="server" ID="txtSearch" />
                                                                  <asp:Button runat="server" ID="btnSearchBar" Style="background-color: #c9302c;" CssClass="btn btn-danger" Text="Search" OnClientClick="javascipt: return confirm('are you sure want to search ?');"  />
                                                                  <asp:BulletedList ID="bltLstSearchItems" runat="server">
                                                                  </asp:BulletedList>
                                                                 </td>--%>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="gridcontent-inner" id="divProductDetails" runat="server">
                                        <fieldset>
                                            <legend>PRODUCT DETAILS</legend>
                                            <div style="overflow: hidden; width: 100%;" id="DivHeaderRow">
                                            </div>

                                            <asp:TextBox ID="txtSearchBox" runat="server" Width="200px" Height="20px"></asp:TextBox>
                                            <asp:Button Text="Search" runat="server" Width="50px" Height="20px" ForeColor="Green" BackColor="#ccffcc" OnClick="Search" />
                                            <span class="label label-warning">ADD PRODUCT BEFORE CHANGING THE PAGE</span>

                                            <div style="overflow: scroll; height: 300px;" onscroll="OnScrollDiv(this)" id="DivMainContent">

                                                <asp:GridView ID="grdpodetailsadd" EmptyDataText="There are no records available." ShowHeader="true" CssClass="zebra"
                                                    EmptyDataRowStyle-ForeColor="#cc0000" HeaderStyle-Width="100%" ItemStyle-Width="100%" runat="server" OnRowDataBound="grdpodetailsadd_RowDataBound"
                                                    AutoGenerateColumns="False" BackColor="#f0f5f5" Style="height: 10px; overflow: auto" AllowPaging="true" OnPageIndexChanging="OnPageIndexChanging" PageSize="20">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="CHECK BOX" SortExpression="HIGHLIGHT">
                                                            <EditItemTemplate>
                                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="CheckBox1" runat="server"  onclick="javascript:HighlightRow(this);" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SLNO">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PRODUCTID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblproductid" runat="server" Text='<%# Bind("PRODUCTID") %>' value='<%# Eval("PRODUCTID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CODE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblcode" runat="server" Text='<%# Bind("PRODUCTCODE") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ITEMNAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblproductname" Width="250px" runat="server" Text='<%# Bind("PRODUCTNAME") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PACKSIZE">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="grdlblUOMNAME" AutoPostBack="true" runat="server" Width="100px" OnSelectedIndexChanged="grdlblUOMNAME_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="LASTRATE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblmrp" runat="server" Text='<%# Bind("LASTRATE") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="PURCHASECOST">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdlblpurchasecost" runat="server" Width="50px" Height="20" Text='<%# Bind("PURCHASECOST") %>' Enabled="true" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="UOMID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblUOMID" runat="server" Text='<%# Bind("UOMID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--  <asp:TemplateField HeaderText="PACKSIZE">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="grdlblUOMNAME" runat="server" Width="120px"/>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>


                                                        <asp:TemplateField HeaderText="PACKSIZEID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblpacksizeid" runat="server" Text='<%# Bind("PACKSIZEID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PACKSIZE " Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblpacksizename" runat="server" Text='<%# Bind("PACKSIZENAME") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PCSQTY" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblpcsqty" runat="server" Text='<%# Bind("PCSQTY") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="POQTY">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdtxtpoqty" AutoPostBack="false" runat="server" Enabled="true"
                                                                    Style="text-align: right;" Width="50px" Height="20" Text='<%# Bind("POQTY") %>'
                                                                    value='<%# Eval("POQTY") %>' onkeypress="return isNumberKey(event);" onkeyup="Calculateamount(this);" onmousemove="CalculateDiscPer(this)" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DISC(%)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdTxtDiscPer" AutoPostBack="false" runat="server" Enabled="true"
                                                                    Style="text-align: right;" Width="50px" Height="20" Text='<%# Bind("DISCPER") %>'
                                                                    value='<%# Eval("DISCPER") %>' onkeypress="return isNumberKey(event);" onkeyup="CalculateDiscPer(this);" onmousemove="CalculateDiscPer(this)" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DISCAMNT">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdTxtDiscAmnt" AutoPostBack="false" runat="server" Enabled="false"
                                                                    Style="text-align: right;" Width="50px" Height="20" Text='<%# Bind("DISCAMNT") %>'
                                                                    value='<%# Eval("DISCAMNT") %>' onkeypress="return isNumberKey(event);" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="AMOUNT">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdlblbasiccostvalue" AutoPostBack="false" runat="server" Enabled="false"
                                                                    Style="text-align: right;" Width="50px" Height="20" Text='<%# Bind("BASICVALUE") %>'
                                                                    value='<%# Eval("BASICVALUE") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="CGSTID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdCGSTID" AutoPostBack="false" runat="server" Enabled="false"
                                                                    Style="text-align: right;" Width="50px" Height="20" Text='<%# Bind("CGSTID") %>'
                                                                    value='<%# Eval("CGSTID") %>' onkeypress="return isNumberKey(event);" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="CGST(%)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdCGSTPER" AutoPostBack="false" runat="server" Enabled="false"
                                                                    Style="text-align: right;" Width="50px" Height="20" Text='<%# Bind("CGSTPER") %>'
                                                                    value='<%# Eval("CGSTPER") %>' onkeypress="return isNumberKey(event);" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="CGST(Amnt)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdCGSTAMNT" AutoPostBack="false" runat="server" Enabled="false"
                                                                    Style="text-align: right;" Width="50px" Height="20" Text='<%# Bind("CGSTAMNT") %>'
                                                                    value='<%# Eval("CGSTAMNT") %>' onkeypress="return isNumberKey(event);" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="SGSTID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdSGSTID" AutoPostBack="false" runat="server" Enabled="false"
                                                                    Style="text-align: right;" Width="50px" Height="20" Text='<%# Bind("SGSTID") %>'
                                                                    value='<%# Eval("SGSTID") %>' onkeypress="return isNumberKey(event);" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="SGST(%)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdSGSTPER" AutoPostBack="false" runat="server" Enabled="false"
                                                                    Style="text-align: right;" Width="50px" Height="20" Text='<%# Bind("SGSTPER") %>'
                                                                    value='<%# Eval("SGSTPER") %>' onkeypress="return isNumberKey(event);" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="SGST(Amnt)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdSGSTAMNT" AutoPostBack="false" runat="server" Enabled="false"
                                                                    Style="text-align: right;" Width="50px" Height="20" Text='<%# Bind("SGSTAMNT") %>'
                                                                    value='<%# Eval("SGSTAMNT") %>' onkeypress="return isNumberKey(event);" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="IGSTID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdIGSTID" AutoPostBack="false" runat="server" Enabled="false"
                                                                    Style="text-align: right;" Width="50px" Height="20" Text='<%# Bind("IGSTID") %>'
                                                                    value='<%# Eval("IGSTID") %>' onkeypress="return isNumberKey(event);" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="IGST(%)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdIGSTPER" AutoPostBack="false" runat="server" Enabled="false"
                                                                    Style="text-align: right;" Width="50px" Height="20" Text='<%# Bind("IGSTPER") %>'
                                                                    value='<%# Eval("IGSTPER") %>' onkeypress="return isNumberKey(event);" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="IGST(Amnt)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdIGSTAMNT" AutoPostBack="false" runat="server" Enabled="false"
                                                                    Style="text-align: right;" Width="50px" Height="20" Text='<%# Bind("IGSTAMNT") %>'
                                                                    value='<%# Eval("IGSTAMNT") %>' onkeypress="return isNumberKey(event);" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                         <asp:TemplateField HeaderText="TOTAL(Amnt)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdVALUEWITHTAX" AutoPostBack="false" runat="server" Enabled="false"
                                                                    Style="text-align: right;" Width="50px" Height="20" Text='<%# Bind("VALUEWITHTAX") %>'
                                                                    value='<%# Eval("VALUEWITHTAX") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="DELIVERY FROM DATE" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdtxtdeliverydatefrom" AutoPostBack="false" runat="server" MaxLength="10"
                                                                    Style="text-align: right;" Width="70px" Height="20" Enabled="false" />
                                                                <asp:ImageButton ID="imgPopuppodate4" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="imgPopuppodate4"
                                                                    runat="server" TargetControlID="grdtxtdeliverydatefrom" CssClass="cal_Theme1"
                                                                    Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DELIVERY TO DATE" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdtxtdeliverydateto" AutoPostBack="false" runat="server" MaxLength="10"
                                                                    Style="text-align: right;" Width="70px" Height="20" Enabled="false" />
                                                                <asp:ImageButton ID="imgPopuppodateto" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender34" PopupButtonID="imgPopuppodateto"
                                                                    runat="server" TargetControlID="grdtxtdeliverydateto" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DIVISIONID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblDIVISIONID" runat="server" Text='<%# Bind("DIVISIONID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DIVISIONNAME" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblDIVISIONNAME" runat="server" Text='<%# Bind("DIVISIONNAME") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CATEGORYID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblCATEGORYID" runat="server" Text='<%# Bind("CATEGORYID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CATEGORYNAME" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblCATEGORYNAME" runat="server" Text='<%# Bind("CATEGORYNAME") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="NATUREOFPRODUCTID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblNATUREOFPRODUCTID" runat="server" Text='<%# Bind("NATUREOFPRODUCTID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="NATUREOFPRODUCTNAME" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblNATUREOFPRODUCTNAME" runat="server" Text='<%# Bind("NATUREOFPRODUCTNAME") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>

                                                </asp:GridView>

                                            </div>
                                        </fieldset>
                                    </div>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td"
                                        runat="server" id="tblADD">
                                        <tr>
                                            <td style="padding-left: 13px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon add_co"></span>
                                                    <asp:Button ID="btnadd" runat="server" Text="Add" CssClass="btn_link" OnClick="btnadd_Click" AccessKey="B" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="gridcontent-inner">
                                        <fieldset>
                                            <legend>PO / INDENT DETAILS</legend>
                                            <div style="overflow: scroll; height: 300px;" onscroll="OnScrollDiv(this)" id="DivMainContent1">

                                                <asp:GridView ID="gvPurchaseOrder" EmptyDataText="There are no records available." ShowHeader="true"
                                                    CssClass="zebra" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                                                    PageSize="200" Width="100%" OnRowDataBound="gvPurchaseOrder_RowDataBound">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="CHECK BOX" SortExpression="HIGHLIGHT">
                                                            
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="PurchaseOrderCheckBox" runat="server"  onclick="javascript:HighlightRow(this);" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SLNO">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PRODUCTID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblproductid1" runat="server" Text='<%# Bind("PRODUCTID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CODE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblcode1" runat="server" Text='<%# Bind("PRODUCTCODE") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PRODUCT">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblproductname1" Width="250px" runat="server" Text='<%# Bind("PRODUCTNAME") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PO QTY">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblQTY1" runat="server" Text='<%# Bind("PRODUCTQTY") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DISCPER(%)">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblDiscPer" runat="server" Text='<%# Bind("DISCPER") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DISCAMNT">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblDiscAmnt" runat="server" Text='<%# Bind("DISCAMNT") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PACKSIZE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblpacksizename1" runat="server" Text='<%# Bind("PRODUCTPACKINGSIZE") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="LAST RATE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblLastRate" runat="server" Text='<%# Bind("LASTRATE") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PURCHASE RATE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblpurchasecost1" runat="server" Text='<%# Bind("PRODUCTPRICE") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="AMOUNT">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblAmount1" runat="server" Text='<%# Bind("PRODUCTAMOUNT") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="CGSTID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdCGSTID1" AutoPostBack="false" runat="server" Enabled="false"
                                                                    Style="text-align: right;" Width="50px" Height="20" Text='<%# Bind("CGSTID") %>'
                                                                    value='<%# Eval("CGSTID") %>' onkeypress="return isNumberKey(event);" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="CGST(%)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdCGSTPER1" AutoPostBack="false" runat="server" Enabled="false"
                                                                    Style="text-align: right;" Width="50px" Height="20" Text='<%# Bind("CGSTPER") %>'
                                                                    value='<%# Eval("CGSTPER") %>' onkeypress="return isNumberKey(event);" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="CGST(Amnt)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdCGSTAMNT1" AutoPostBack="false" runat="server" Enabled="false"
                                                                    Style="text-align: right;" Width="50px" Height="20" Text='<%# Bind("CGSTAMNT") %>'
                                                                    value='<%# Eval("CGSTAMNT") %>' onkeypress="return isNumberKey(event);" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="SGSTID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdSGSTID1" AutoPostBack="false" runat="server" Enabled="false"
                                                                    Style="text-align: right;" Width="50px" Height="20" Text='<%# Bind("SGSTID") %>'
                                                                    value='<%# Eval("SGSTID") %>' onkeypress="return isNumberKey(event);" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="SGST(%)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdSGSTPER1" AutoPostBack="false" runat="server" Enabled="false"
                                                                    Style="text-align: right;" Width="50px" Height="20" Text='<%# Bind("SGSTPER") %>'
                                                                    value='<%# Eval("SGSTPER") %>' onkeypress="return isNumberKey(event);" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="SGST(Amnt)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdSGSTAMNT1" AutoPostBack="false" runat="server" Enabled="false"
                                                                    Style="text-align: right;" Width="50px" Height="20" Text='<%# Bind("SGSTAMNT") %>'
                                                                    value='<%# Eval("SGSTAMNT") %>' onkeypress="return isNumberKey(event);" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="IGSTID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdIGSTID1" AutoPostBack="false" runat="server" Enabled="false"
                                                                    Style="text-align: right;" Width="50px" Height="20" Text='<%# Bind("IGSTID") %>'
                                                                    value='<%# Eval("IGSTID") %>' onkeypress="return isNumberKey(event);" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="IGST(%)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdIGSTPER1" AutoPostBack="false" runat="server" Enabled="false"
                                                                    Style="text-align: right;" Width="50px" Height="20" Text='<%# Bind("IGSTPER") %>'
                                                                    value='<%# Eval("IGSTPER") %>' onkeypress="return isNumberKey(event);" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="IGST(Amnt)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdIGSTAMNT1" AutoPostBack="false" runat="server" Enabled="false"
                                                                    Style="text-align: right;" Width="50px" Height="20" Text='<%# Bind("IGSTAMNT") %>'
                                                                    value='<%# Eval("IGSTAMNT") %>' onkeypress="return isNumberKey(event);" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>



                                                         <asp:TemplateField HeaderText="TOTAL(Amnt)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdVALUEWITHTAX1" AutoPostBack="false" runat="server" Enabled="false"
                                                                    Style="text-align: right;" Width="50px" Height="20" Text='<%# Bind("VALUEWITHTAX") %>'
                                                                    value='<%# Eval("VALUEWITHTAX") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>










                                                        <asp:TemplateField HeaderText="MRP" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblMRP1" runat="server" Text='<%# Bind("MRP") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TOTAL MRP" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblTotalMRP1" runat="server" Text='<%# Bind("MRPVALUE") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ASSESMENT(%)" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblASSESMENT1" runat="server" Text='<%# Bind("ASSESMENTPERCENTAGE") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Excise(%)" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblexcise1" runat="server" Text='<%# Bind("Excise") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CST Percentage" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblcst1" runat="server" Text='<%# Bind("CST") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DELIVERY FROM DATE" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblFromDate1" runat="server" Text='<%# Bind("REQUIREDDATE") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DELIVERY TO DATE" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblToDate1" runat="server" Text='<%# Bind("REQUIREDTODATE") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DivisionID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblDivID1" runat="server" Text='<%# Bind("DIVISIONID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DIVISIONNAME" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblDivID2" runat="server" Text='<%# Bind("DIVISIONNAME") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CATEGORYID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblDivID3" runat="server" Text='<%# Bind("CATEGORYID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CATEGORYNAME" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblDivID4" runat="server" Text='<%# Bind("CATEGORYNAME") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="NATUREOFPRODUCTNAME" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblDivID5" runat="server" Text='<%# Bind("NATUREOFPRODUCTNAME") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="NATUREOFPRODUCTID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblDivID6" runat="server" Text='<%# Bind("NATUREOFPRODUCTID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="UOMID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblDivID7" runat="server" Text='<%# Bind("UOMID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="UOMNAME" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblDivID8" runat="server" Text='<%# Bind("UOMNAME") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DELETE">
                                                            <ItemTemplate>
                                                                <asp:Button ID="btn_TempDelete" runat="server" class="action-icons c-delete"
                                                                    OnClientClick="return confirm('Are you sure you want to Delete?')" ToolTip="Delete"
                                                                    OnClick="btn_TempDelete_Click" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>

                                                </asp:GridView>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 12px;">
                                                <fieldset>
                                                    <legend>PO / INDENT AMOUNT DETAILS</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>

                                                            <td width="130">
                                                                <asp:TextBox ID="txtBasicAmnt" runat="server" MaxLength="15" CssClass="x-large"
                                                                    placeholder="Total BasicAmnt" Text="0" Font-Bold="true" Enabled="false" CausesValidation="true"></asp:TextBox>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label23" runat="server" Text="Total BasicAmnt"></asp:Label></span>
                                                            </td>
                                                            <td width="15">&nbsp;
                                                            </td>



                                                            <td width="130">
                                                                <asp:TextBox ID="txtDiscountAmnt" runat="server" MaxLength="15" CssClass="x-large" AutoPostBack="true"
                                                                    placeholder="Discount Amount" Text="0" Font-Bold="true" OnTextChanged="txtDiscountAmnt_TextChanged" CausesValidation="true"></asp:TextBox>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label19" runat="server" Text="Discount Amount"></asp:Label></span>
                                                            </td>
                                                            <td width="15">&nbsp;
                                                            </td>
                                                            <td width="130">
                                                                <asp:TextBox ID="txtItemWiseTotalDisc" runat="server" MaxLength="15" CssClass="x-large" Enabled="false"
                                                                    placeholder="Discount Amount" Text="0" Font-Bold="true" CausesValidation="true"></asp:TextBox>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label30" runat="server" Text="Item WiseDiscount Amount"></asp:Label></span>
                                                            </td>
                                                            <td width="15">&nbsp;

                                                                <td class="field_title" style="padding-left: 10px;" runat="server" id="Td1">
                                                                    <asp:Label ID="Label27" runat="server" Text="Extra Freight Charge" ForeColor="Blue"></asp:Label>
                                                                </td>
                                                                <td align="left" class="field_input" style="padding-bottom: 20px;" width="10">
                                                                    <asp:CheckBox ID="CheckBox2" runat="server" Text=" "
                                                                        CausesValidation="false" AutoPostBack="true" />
                                                                </td>
                                                            </td>
                                                            <td width="130">
                                                                <asp:TextBox ID="txtFreightAmnt" runat="server" MaxLength="15" CssClass="x-large" AutoPostBack="true"
                                                                    placeholder="Freight Charges" Text="0" Font-Bold="true" CausesValidation="true" OnTextChanged="txtFreightAmnt_TextChanged"></asp:TextBox>

                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label20" runat="server" Text="Freight Amount"></asp:Label></span>
                                                            </td>

                                                            <td width="130">
                                                                <asp:TextBox ID="txtOtherCharges" runat="server" MaxLength="15" CssClass="x-large" AutoPostBack="true"
                                                                    placeholder="Other Charges" Text="0" Font-Bold="true" CausesValidation="true" OnTextChanged="txtOtherCharges_TextChanged"></asp:TextBox>

                                                                <span class="label_intro">
                                                                    <asp:Label ID="LabelCharges" runat="server" Text="Charges Amount"></asp:Label></span>
                                                            </td>
                                                            <td width="15">&nbsp;
                                                            </td>
                                                            <td width="130">
                                                                <asp:TextBox ID="txtgrosstotal" runat="server" MaxLength="15" CssClass="x-large"
                                                                    placeholder="Fianl Amount" Text="0" Font-Bold="true" Enabled="false" CausesValidation="true"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="CV_GrossTotal" runat="server" ControlToValidate="txtgrosstotal"
                                                                    SetFocusOnError="true" ErrorMessage="Total Amount is required!" Display="None"
                                                                    ValidationGroup="POFooter"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server"
                                                                    TargetControlID="CV_GrossTotal" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label3" runat="server" Text="Total Amount"></asp:Label></span>
                                                            </td>
                                                            <td width="15">&nbsp;
                                                            </td>
                                                            <td width="130" style="display: none;">
                                                                <asp:TextBox ID="txtTotalMRP" runat="server" MaxLength="15" CssClass="x-large" Text="0"
                                                                    ValidationGroup="POFooter" onkeypress="return isNumberKeyWithDot(event);" placeholder="Total MRP"
                                                                    Enabled="false" Font-Bold="true"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="CV_Discount" runat="server" ControlToValidate="txtTotalMRP"
                                                                    SetFocusOnError="true" ErrorMessage="required!" Display="None" ValidationGroup="POFooter"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                                    TargetControlID="CV_Discount" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label16" runat="server" Text="Total MRP"></asp:Label></span>
                                                            </td>
                                                            <td width="15" style="display: none;">&nbsp;
                                                            </td>
                                                            <td width="130" style="display: none;">
                                                                <asp:TextBox ID="txtadjustment" runat="server" MaxLength="5" CssClass="x-large" placeholder="Adjustment"
                                                                    Text="0" ValidationGroup="POFooter" onChange="adjustmentcalculation()" onkeypress="return isNumberKeyWithDotMinus(event);"
                                                                    AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="CV_Adjustment" runat="server" ControlToValidate="txtadjustment"
                                                                    SetFocusOnError="true" ErrorMessage="Adjustment is required!" Display="None"
                                                                    ValidationGroup="POFooter"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                                                                    TargetControlID="CV_Adjustment" PopupPosition="BottomLeft" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label17" runat="server" Text="Total Adjustment"></asp:Label></span>
                                                            </td>
                                                            <td width="15" style="display: none;">&nbsp;
                                                            </td>
                                                            <td width="130" style="display: none;">
                                                                <asp:TextBox ID="txttotalamount" runat="server" MaxLength="15" CssClass="x-large"
                                                                    placeholder="Total Gross Value" Enabled="false" Text="0" ValidationGroup="POFooter"
                                                                    Font-Bold="true"></asp:TextBox>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label13" runat="server" Text="Total Gross Value"></asp:Label></span>
                                                            </td>
                                                            <td width="15" style="display: none;">&nbsp;
                                                            </td>
                                                            <td width="130">
                                                                <asp:TextBox ID="txtcasepack" runat="server" MaxLength="15" CssClass="x-large" placeholder="Total Case"
                                                                    Enabled="false" onkeypress="return isNumberKeyWithDot(event);" ValidationGroup="POFooter"
                                                                    Font-Bold="true"></asp:TextBox>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label7" runat="server" Text="Total Qty"></asp:Label></span>
                                                            </td>
                                                            <td width="130">
                                                                <asp:TextBox ID="txtpacking" runat="server" MaxLength="15" CssClass="x-large" placeholder="Packing"
                                                                    Text="0" onkeypress="return isNumberKeyWithDot(event);" Enabled="false" ValidationGroup="POFooter"
                                                                    Font-Bold="true" Visible="false"></asp:TextBox>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label21" runat="server" Text="Packing" Visible="false"></asp:Label></span>
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="8" colspan="10"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtsaletax" runat="server" MaxLength="15" CssClass="x-large" Visible="false"
                                                                    placeholder="Tot. CST Value" Text="0" onkeypress="return isNumberKeyWithDot(event);"
                                                                    Enabled="false" ValidationGroup="POFooter" Font-Bold="true"></asp:TextBox>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label25" runat="server" Text="Tot. CST Value" Visible="false"></asp:Label>
                                                                </span>
                                                            </td>
                                                            <td width="15">&nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtexercise" runat="server" MaxLength="15" CssClass="x-large" placeholder="Tot. Excise Value"
                                                                    Text="0" Visible="false" ValidationGroup="POFooter" onkeypress="return isNumberKeyWithDot(event);"
                                                                    Enabled="false" Font-Bold="true"></asp:TextBox>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label22" runat="server" Text="Tot. Excise Value" Visible="false"></asp:Label></span>
                                                            </td>
                                                            <td width="15">&nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtnettotal" runat="server" MaxLength="15" Enabled="false" CssClass="x-large"
                                                                    Text="0" ValidationGroup="POFooter" placeholder="Net Total" Font-Bold="true"
                                                                    ForeColor="Green" Visible="false"></asp:TextBox>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label24" runat="server" Text="Net Total" Visible="false"></asp:Label></span>
                                                            </td>
                                                            <td width="15">&nbsp;
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
                                                            <td width="15">&nbsp;
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>

                                                            <td class="field_title" style="padding-left: 10px;" runat="server" id="Td3">
                                                                <asp:Label ID="Label18" runat="server" Text="PO DOCUMENT UPLOAD" ForeColor="Blue"></asp:Label>
                                                            </td>
                                                            <td align="left" class="field_input" style="padding-bottom: 20px;" width="10">
                                                                <asp:CheckBox ID="chkfileupload" runat="server" Text=" " OnCheckedChanged="chkfileupload_check"
                                                                    CausesValidation="false" AutoPostBack="true" />
                                                            </td>

                                                            <td class="field_input">
                                                                <div class="btn_24_blue" id="divshow" runat="server">
                                                                    <span class="icon exclamation_co"></span>
                                                                    <asp:Button ID="btnshow" runat="server" Text="Documents" CssClass="btn_link"
                                                                        CausesValidation="false" OnClick="btnshow_Click" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td height="8" colspan="10"></td>
                                                        </tr>
                                                    </table>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td width="320">
                                                                <asp:TextBox ID="txtremarks" runat="server" MaxLength="255" TextMode="MultiLine"
                                                                    Width="100%" placeholder="Remarks" class="input_grow"></asp:TextBox>
                                                            </td>
                                                            <td width="15">&nbsp;
                                                            </td>
                                                            <td>
                                                                <div class="btn_24_blue" id="divbtnsave" runat="server">
                                                                    <span class="icon disk_co"></span>
                                                                    <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn_link" ValidationGroup="POFooter"
                                                                        OnClick="btnsave_Click" AccessKey="S" />
                                                                    <asp:HiddenField ID="Hdn_Fld" runat="server" />
                                                                </div>
                                                                <div class="btn_24_blue" id="divbtnHoldsave" runat="server">
                                                                    <span class="icon disk_co"></span>
                                                                    <asp:Button ID="btnHoldsave" runat="server" Text="HOLD" CssClass="btn_link" ValidationGroup="POFooter"
                                                                        OnClick="btnHoldsave_Click" />

                                                                </div>
                                                                <div class="btn_24_blue" id="divbtnapprove" runat="server">
                                                                    <span class="icon disk_co"></span>
                                                                    <asp:Button ID="btnapprove" runat="server" Text="Approve" CssClass="btn_link" ValidationGroup="POFooter"
                                                                        OnClick="btnapprove_Click" AccessKey="P" />

                                                                </div>
                                                                <div class="btn_24_blue" id="divBtnConfirm" runat="server">
                                                                    <span class="icon disk_co"></span>
                                                                    <asp:Button ID="btnConfirm" runat="server" Text="Confirm" CssClass="btn_link" ValidationGroup="POFooter"
                                                                        OnClick="btnConfirm_Click" AccessKey="C" />

                                                                </div>

                                                                <div class="btn_24_red" id="divbtnreject" runat="server">
                                                                    <span class="icon reject_co"></span>
                                                                    <asp:LinkButton ID="btnReject" runat="server" Text="Reject" CssClass="btn_link" CausesValidation="false"
                                                                        OnClick="btnReject_Click" AccessKey="R" />
                                                                </div>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <div class="btn_24_blue">
                                                                    <span class="icon cross_octagon_co"></span>
                                                                    <asp:Button ID="btncancel" runat="server" Text="Close" CssClass="btn_link" OnClick="btncancel_Click"
                                                                        CausesValidation="false" AccessKey="X" />
                                                                </div>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <div class="btn_24_blue" style="display: none;" runat="server" id="div_btnPrint">
                                                                    <span class="icon printer_co"></span>
                                                                    <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn_link" OnClick="btnPrint_Click"
                                                                        CausesValidation="false" Visible="false" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
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
                                                            <asp:Label ID="Label9" runat="server" Text="From Date"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="165">
                                                            <asp:TextBox ID="txtfromdate" runat="server" MaxLength="15" Width="120" Enabled="false"
                                                                placeholder="dd/mm/yyyy" ValidationGroup="datecheck"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopupfromdate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopupfromdate"
                                                                runat="server" TargetControlID="txtfromdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="CV_FromDate" runat="server" ControlToValidate="txtfromdate"
                                                                SetFocusOnError="true" ErrorMessage="From Date is required!" Display="None" ValidationGroup="datecheck"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                                                TargetControlID="CV_FromDate" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <asp:CustomValidator ID="CV_FromDateValidation" runat="server" ClientValidationFunction="ValidateDate"
                                                                ControlToValidate="txtfromdate" SetFocusOnError="true" ErrorMessage="* Invalid Date!"
                                                                Display="None" ValidationGroup="datecheck" />
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server"
                                                                TargetControlID="CV_FromDateValidation" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                        <td width="70">
                                                            <asp:Label ID="Label10" runat="server" Text="To Date"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="165">
                                                            <asp:TextBox ID="txttodate" runat="server" MaxLength="10" Width="69%" placeholder="dd/mm/yyyy"
                                                                ValidationGroup="datecheck" Enabled="false"></asp:TextBox>
                                                            <asp:ImageButton ID="imgpopuptodate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="imgpopuptodate"
                                                                runat="server" TargetControlID="txttodate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="CV_ToDate" runat="server" ControlToValidate="txttodate"
                                                                SetFocusOnError="true" ErrorMessage="To Date is required!" Display="None" ValidationGroup="datecheck"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server"
                                                                TargetControlID="CV_ToDate" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <asp:CustomValidator ID="CV_ToDateValidation" runat="server" ClientValidationFunction="ValidateDate"
                                                                ControlToValidate="txttodate" SetFocusOnError="true" ErrorMessage="* Invalid Date!"
                                                                Display="None" ValidationGroup="datecheck" />
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server"
                                                                TargetControlID="CV_ToDateValidation" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                        <td width="110" class="field_title">
                                                            <asp:Label ID="Label26" runat="server" Text="Type"></asp:Label>
                                                        </td>
                                                        <td width="180" id="tdddlType" runat="server" colspan="3">
                                                            <asp:DropDownList ID="ddlType" runat="server" Width="200px" Height="28px"
                                                                class="chosen-select" data-placeholder="Select Type">
                                                            </asp:DropDownList>
                                                        </td>




                                                        <td>
                                                            <div class="btn_24_blue">
                                                                <span class="icon find_co"></span>
                                                                <asp:Button ID="btngvfill" runat="server" Text="Search" CssClass="btn_link" ValidationGroup="datecheck"
                                                                    OnClick="btngvfill_Click" AccessKey="A" />
                                                            </div>
                                                            <asp:HiddenField ID="hdn_pofield" runat="server" />
                                                            <asp:HiddenField ID="hdn_pofieNo" runat="server" />
                                                            <asp:HiddenField ID="hdn_pofield1" runat="server" />
                                                            <asp:HiddenField ID="hdn_podelete" runat="server" />
                                                            <asp:HiddenField ID="hdn_FromDate" runat="server" />
                                                            <asp:HiddenField ID="hdn_ToDate" runat="server" />
                                                        </td>
                                                        <td>
                                                            <div class="btn_30_dark">
                                                                <span class="icon excel_document"></span>
                                                                <asp:Button ID="btnExportExcel" runat="server" Text="EXCEL" CssClass="btn_link" ValidationGroup="datecheck"
                                                                    OnClick="btnExportExcel_Click" />
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
                                    <div class="gridcontent">
                                        <cc1:Grid ID="gvpodetails" runat="server" CallbackMode="true" Serialize="true" AutoGenerateColumns="false"
                                            AllowAddingRecords="false" PageSize="200" AllowPaging="true" AllowFiltering="true" OnRowDataBound="gvpodetails_RowDataBound"
                                            AllowPageSizeSelection="true" OnDeleteCommand="DeleteRecordPoDetails" FolderStyle="../GridStyles/premiere_blue"
                                            EnableRecordHover="true">
                                            <ClientSideEvents OnBeforeClientDelete="OnBeforepodetailsDelete" OnClientDelete="OnDeletePoDetails" />
                                            <FilteringSettings InitialState="Visible" />
                                            <Columns>
                                                <cc1:Column ID="Column4" DataField="Sl" HeaderText="SL" runat="server" Width="60"
                                                    Wrap="true">
                                                    <TemplateSettings TemplateId="slnoTemplatePO" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column6" DataField="PODATE" HeaderText="PO DATE" runat="server" Width="100"
                                                    Wrap="true">
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
                                                <cc1:Column ID="Column7" DataField="PONO" HeaderText="PO NO" runat="server" Width="220"
                                                    Wrap="true">
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
                                                <cc1:Column ID="Column8" DataField="TPUNAME" HeaderText="TPU NAME" runat="server"
                                                    Width="280" Wrap="true">
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
                                                <cc1:Column DataField="TOTALPRODUCT" HeaderText="TOTAL PRODUCTS" Width="130" Wrap="true">
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
                                                <cc1:Column DataField="TOTALCASEPACK" HeaderText="TOTAL QTY" Width="150" Wrap="true">
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
                                                <cc1:Column ID="Column19" DataField="ORDERTYPENAME" HeaderText="ORDER TYPE" runat="server"
                                                    Width="110" Wrap="true" Visible="false">
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
                                                <cc1:Column ID="Column2" DataField="STATUS" HeaderText="STATUS" runat="server"
                                                    Width="110" Wrap="true">
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
                                                <cc1:Column ID="Column1" DataField="POID" HeaderText="POID" runat="server" Width="100"
                                                    Visible="false">
                                                </cc1:Column>
                                                <cc1:Column ID="Column15" AllowEdit="true" HeaderText="VIEW" runat="server" Width="60"
                                                    Wrap="true">
                                                    <TemplateSettings TemplateId="viewBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column13" AllowEdit="true" AllowDelete="true" HeaderText="EDIT" runat="server"
                                                    Width="80" Wrap="true">
                                                    <TemplateSettings TemplateId="editBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="DELETE" AllowEdit="false" AllowDelete="true" Width="80" Wrap="true">
                                                    <TemplateSettings TemplateId="deleteBtnTemplatePO" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column333" AllowEdit="false" AllowDelete="false" Width="80" HeaderText="PRINT"
                                                    runat="server" Wrap="true">
                                                    <TemplateSettings TemplateId="PrintBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column3330" AllowEdit="false" AllowDelete="false" HeaderText="EXCEL"
                                                    runat="server" Width="80" Wrap="true">
                                                    <TemplateSettings TemplateId="PrintBtnExcel" />
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
                                                <cc1:GridTemplate runat="server" ID="viewBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="h_icon zoom_sl" title="View" id="btnGridView_<%# Container.PageRecordIndex %>"
                                                            onclick="CallViewServerMethod(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="deleteBtnTemplatePO">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="gvpodetails.delete_record(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                                <cc1:GridTemplate runat="server" ID="slnoTemplatePO">
                                                    <Template>
                                                        <asp:Label ID="lblslnoPO" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="PrintBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="filter_btn printer_co" id="btnPOPrint_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethodPrint(this)" title="Print"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="PrintBtnExcel">
                                                    <Template>
                                                        <a href="javascript: //" class="filter_btn excel_document" id="btnPOExcel_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethodExcel(this)" title="Excel"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <ScrollingSettings ScrollWidth="100%" ScrollHeight="290" />
                                        </cc1:Grid>
                                        <asp:Button ID="btngrdedit" runat="server" Text="grdedit" Style="display: none" OnClick="btngrdedit_Click"
                                            CausesValidation="false" />
                                        <asp:Button ID="btngrdview" runat="server" Text="GridView" Style="display: none"
                                            OnClick="btngrdview_Click" CausesValidation="false" />
                                        <asp:Button ID="btnPOPrint" runat="server" Text="Print" Style="display: none" OnClick="btnPOPrint_Click"
                                            CausesValidation="false" />
                                        <asp:Button ID="btnPOExcel" runat="server" Text="Excel" Style="display: none" OnClick="btnPOExcel_Click"
                                            CausesValidation="false" />
                                        <asp:HiddenField ID="hdn_convertionqty" runat="server" />
                                        <asp:HiddenField ID="hdnCST" runat="server" />
                                        <asp:HiddenField ID="hdnExcise" runat="server" />
                                    </div>
                                    <div class="white_content" runat="server">
                                        <rsweb:ReportViewer ID="ReportViewer1" class="gridcontent-inner" runat="server" Height="1000px"
                                            Width="800px" Style="margin-right: 0px">
                                        </rsweb:ReportViewer>
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
                function ValidateDate(sender, args) {
                    var dateString = document.getElementById(sender.controltovalidate).value;
                    var regex = /((0[0-9]|1[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$/;
                    if (regex.test(dateString)) {
                        var parts = dateString.split("/");
                        var dt = new Date(parts[1] + "/" + parts[0] + "/" + parts[2]);
                        args.IsValid = (dt.getDate() == parts[0] && dt.getMonth() + 1 == parts[1] && dt.getFullYear() == parts[2]);
                    } else {
                        args.IsValid = false;
                    }
                }
            </script>
            <script type="text/javascript">

                function calculation() {
                    var totalamount = 0;
                    var nettotal = 0;



                    adjustmentcalculation();

                    var grosstotal = Number(document.getElementById("<%=txtgrosstotal.ClientID %>").value);
                    grosstotal.toFixed(2);
                    var adjustment = Number(document.getElementById("<%=txtadjustment.ClientID %>").value);

                    var exercise = Number(document.getElementById("<%=txtexercise.ClientID %>").value);
                    var salestax = Number(document.getElementById("<%=txtsaletax.ClientID %>").value);

                    //totalamount = grosstotal - discount + packing + exercise + salestax + othercharges;
                    nettotal = grosstotal + adjustment;
                    document.getElementById("<%=txttotalamount.ClientID %>").value = nettotal.toFixed(2);
                    document.getElementById("<%=txtnettotal.ClientID %>").value = nettotal.toFixed(2);
                }
            </script>
            <script type="text/javascript">

                function adjustmentcalculation() {
                    var nettotal = 0;
                    var totalamount = 0;
                    var grosstotal = Number(document.getElementById("<%=txtgrosstotal.ClientID %>").value);
                    grosstotal.toFixed(2);


                    var adjustment = Number(document.getElementById("<%=txtadjustment.ClientID %>").value);

                    //totalamount = grosstotal ;
                    nettotal = grosstotal + adjustment;
                    document.getElementById("<%=txttotalamount.ClientID %>").value = nettotal.toFixed(2);
                    //document.getElementById("<%=txtnettotal.ClientID %>").value = nettotal.toFixed(2);
                }



            </script>
            <script type="text/javascript">
                var searchTimeout = null;
                function FilterTextBox_KeyUp() {
                    searchTimeout = window.setTimeout(performSearch, 500);
                }

                function performSearch() {

                    var searchValue = document.getElementById('FilterTextBox').value;

                    if (searchValue == FilterTextBox.WatermarkText) {
                        searchValue = '';
                    }
                    gvpodetails.addFilterCriteria('PODATE', OboutGridFilterCriteria.Contains, searchValue);
                    gvpodetails.addFilterCriteria('PONO', OboutGridFilterCriteria.Contains, searchValue);
                    gvpodetails.addFilterCriteria('TPUNAME', OboutGridFilterCriteria.Contains, searchValue);
                    gvpodetails.executeFilter();
                    searchTimeout = null;
                    return false;
                }
            </script>

            <script src="../js/quicksearch.js"></script>
            <script type="text/javascript">

                function isNumberKey(evt) {
                    var charCode = (evt.which) ? evt.which : event.keyCode;
                    if ((charCode < 48 || charCode > 57) && (charCode != 8))
                        return false;

                    return true;
                }

                function isNumberKeyWithslash(evt) {
                    var charCode = (evt.which) ? evt.which : event.keyCode;
                    if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 47))
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


                function OnBeforeDelete(record) {
                    record.Error = '';
                    document.getElementById("<%=Hdn_Fld.ClientID %>").value = record.ID;
                    if (confirm("Are you sure you want to delete? "))
                        return true;
                    else
                        return false;
                }
                function OnDelete(record) {
                    alert(record.Error);
                }

                function CallServerMethod(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                    document.getElementById("<%=hdn_pofield.ClientID %>").value = gvpodetails.Rows[iRecordIndex].Cells[8].Value;
                    document.getElementById("<%=hdn_pofield1.ClientID %>").value = gvpodetails.Rows[iRecordIndex].Cells[7].Value;
                    document.getElementById("<%=btngrdedit.ClientID %>").click();

                }

                function CallViewServerMethod(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridView_", "");
                    document.getElementById("<%=hdn_pofield.ClientID %>").value = gvpodetails.Rows[iRecordIndex].Cells[8].Value;
                    document.getElementById("<%=hdn_pofield1.ClientID %>").value = gvpodetails.Rows[iRecordIndex].Cells[7].Value;
                    document.getElementById("<%=btngrdview.ClientID %>").click();

                }

                function CallServerMethodPrint(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnPOPrint_", "");
                    document.getElementById("<%=hdn_pofield.ClientID %>").value = gvpodetails.Rows[iRecordIndex].Cells[8].Value;
                    document.getElementById("<%=btnPOPrint.ClientID %>").click();

                }
                function CallServerMethodExcel(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnPOExcel_", "");
                    document.getElementById("<%=hdn_pofield.ClientID %>").value = gvpodetails.Rows[iRecordIndex].Cells[8].Value;
                    document.getElementById("<%=hdn_pofieNo.ClientID %>").value = gvpodetails.Rows[iRecordIndex].Cells[2].Value;
                    document.getElementById("<%=btnPOExcel.ClientID %>").click();

                }


                function OnBeforepodetailsDelete(record) {
                    record.Error = '';
                    document.getElementById("<%=hdn_pofield.ClientID %>").value = record.PONO;
                    if (confirm("Are you sure you want to delete? "))
                        return true;
                    else
                        return false;
                }
                function OnDeletePoDetails(record) {
                    alert(record.Error);
                }

                function ShowPanel(ctlbtn, ctladd, ctlshow) {
                    if (document.getElementById(ctlbtn)) {
                        document.getElementById(ctladd).style.display = '';
                        document.getElementById(ctlshow).style.display = 'none';

                        return false;
                    }
                }

                function ClosePanel(ctlbtn, ctladd, ctlshow) {
                    if (document.getElementById(ctlbtn)) {
                        document.getElementById(ctlshow).style.display = true;
                        document.getElementById(ctladd).style.display = false;
                        document.getElementById("ctl00_ContentPlaceHolder1_btnAdd").disabled = false;
                        return false;
                    }
                }

                function ShowHideInputs() {

                    if (document.all.InputTable.style.display == "none") {
                        document.all.InputTable.style.display = "inline";
                        document.all.HideInput.value = " Hide ";
                    }
                    Else
                    {
                        document.all.InputTable.style.display = "none";
                        document.all.HideInput.value = " Show ";
                    }

                }
                function Calculateamount(a) {
                    debugger;
                    var qtypcs = 0;
                    var purchasecost = 0;
                    var totoalamount = 0;
                    var rowData = a.parentNode.parentNode;
                    var rowIndex = rowData.rowIndex - 1;
                    var grd = document.getElementById('<%= grdpodetailsadd.ClientID %>');
                    var grid = document.getElementById('ContentPlaceHolder1_grdpodetailsadd');
                    var idtxtinvpcs = "ContentPlaceHolder1_grdpodetailsadd_grdtxtpoqty_" + rowIndex;
                    var idtxtpurchasecos = "ContentPlaceHolder1_grdpodetailsadd_grdlblpurchasecost_" + rowIndex;
                    var idtxtamount = "ContentPlaceHolder1_grdpodetailsadd_grdlblbasiccostvalue_" + rowIndex;

                    var idamountWithTax = "ContentPlaceHolder1_grdpodetailsadd_grdVALUEWITHTAX_" + rowIndex;

                    var idCgstPer = "ContentPlaceHolder1_grdpodetailsadd_grdCGSTPER_" + rowIndex;
                    var idSgstPer="ContentPlaceHolder1_grdpodetailsadd_grdSGSTPER_" + rowIndex;
                    var IdIgstPer = "ContentPlaceHolder1_grdpodetailsadd_grdIGSTPER_" + rowIndex;

                    var idCgstAmnt = "ContentPlaceHolder1_grdpodetailsadd_grdCGSTAMNT_" + rowIndex;
                    var idSgstAmnt="ContentPlaceHolder1_grdpodetailsadd_grdSGSTAMNT_" + rowIndex;
                    var IdIgstAmnt="ContentPlaceHolder1_grdpodetailsadd_grdIGSTAMNT_" + rowIndex;


                    qtypcs = (parseFloat(document.getElementById(idtxtinvpcs).value));
                    if (isNaN(qtypcs)) {
                        qtypcs = 0;
                    }
                    purchasecost = (parseFloat(document.getElementById(idtxtpurchasecos).value));
                    if (purchasecost == "") {
                        purchasecost = 0;
                    }

                    if (isNaN(purchasecost)) {
                        purchasecost = 0;
                    }
                    totoalamount = parseFloat(qtypcs * purchasecost).toFixed(2);

                    if (isNaN(totoalamount)) {

                        totoalamount = 0;
                    }
                   

                    var CgsttaxAmnt = parseFloat(totoalamount * (document.getElementById(idCgstPer).value)) / 100;
                    var SgsttaxAmnt = parseFloat(totoalamount * (document.getElementById(idSgstPer).value)) / 100;
                    var IgsttaxAmnt = parseFloat(totoalamount * (document.getElementById(IdIgstPer).value)) / 100;
                    

                    var totalAmntWithTax = (parseFloat(totoalamount) + parseFloat(CgsttaxAmnt) + parseFloat(SgsttaxAmnt) + parseFloat(IgsttaxAmnt)).toFixed(2);
                    

                    document.getElementById(idtxtamount).value = totoalamount;
                    document.getElementById(idCgstAmnt).value = CgsttaxAmnt;
                    document.getElementById(idSgstAmnt).value = SgsttaxAmnt;
                    document.getElementById(IdIgstAmnt).value = IgsttaxAmnt;
                    document.getElementById(idamountWithTax).value = totalAmntWithTax;


                }
                //new add
                function CalculateDiscPer(a) {
                    debugger;
                    var qtypcs = 0;
                    var discPer = 0;
                    var discAmnt = 0;
                    var purchasecost = 0;
                    var totoalamount = 0;
                    var rowData = a.parentNode.parentNode;
                    var rowIndex = rowData.rowIndex - 1;
                    var grd = document.getElementById('<%= grdpodetailsadd.ClientID %>');
                    var grid = document.getElementById('ContentPlaceHolder1_grdpodetailsadd');
                    var idtxtinvpcs = "ContentPlaceHolder1_grdpodetailsadd_grdtxtpoqty_" + rowIndex;
                    var idtxtpurchasecos = "ContentPlaceHolder1_grdpodetailsadd_grdlblpurchasecost_" + rowIndex;
                    var idtxtamount = "ContentPlaceHolder1_grdpodetailsadd_grdlblbasiccostvalue_" + rowIndex;
                    var idtxtDiscPer = "ContentPlaceHolder1_grdpodetailsadd_grdTxtDiscPer_" + rowIndex;
                    var idtxtDiscAmnt = "ContentPlaceHolder1_grdpodetailsadd_grdTxtDiscAmnt_" + rowIndex;
                    debugger;


                    var idamountWithTax = "ContentPlaceHolder1_grdpodetailsadd_grdVALUEWITHTAX_" + rowIndex;

                    var idCgstPer = "ContentPlaceHolder1_grdpodetailsadd_grdCGSTPER_" + rowIndex;
                    var idSgstPer="ContentPlaceHolder1_grdpodetailsadd_grdSGSTPER_" + rowIndex;
                    var IdIgstPer = "ContentPlaceHolder1_grdpodetailsadd_grdIGSTPER_" + rowIndex;

                    var idCgstAmnt = "ContentPlaceHolder1_grdpodetailsadd_grdCGSTAMNT_" + rowIndex;
                    var idSgstAmnt="ContentPlaceHolder1_grdpodetailsadd_grdSGSTAMNT_" + rowIndex;
                    var IdIgstAmnt="ContentPlaceHolder1_grdpodetailsadd_grdIGSTAMNT_" + rowIndex;

                    qtypcs = (parseFloat(document.getElementById(idtxtinvpcs).value));
                    discPer = (parseFloat(document.getElementById(idtxtDiscPer).value));

                    /*ISNAN HANDELING*/
                    if (isNaN(discPer)) {
                        discPer = 0;
                    }

                    purchasecost = (parseFloat(document.getElementById(idtxtpurchasecos).value));
                    /*ISNAN HANDELING*/
                    if (isNaN(purchasecost)) {
                        purchasecost = 0;
                    }

                    totoalamount = parseFloat(qtypcs * purchasecost).toFixed(2);
                    discAmnt = (totoalamount / 100) * discPer;

                    if (isNaN(discAmnt)) {
                        discAmnt = 0;
                    }


                    totoalamount = parseFloat(totoalamount - discAmnt).toFixed(2);
                    document.getElementById(idtxtamount).value = parseFloat(totoalamount).toFixed(2);
                    document.getElementById(idtxtDiscPer).value = (discPer);
                    document.getElementById(idtxtDiscAmnt).value = parseFloat(discAmnt).toFixed(2);



                     var CgsttaxAmnt = parseFloat(totoalamount * (document.getElementById(idCgstPer).value)) / 100;
                    var SgsttaxAmnt = parseFloat(totoalamount * (document.getElementById(idSgstPer).value)) / 100;
                    var IgsttaxAmnt = parseFloat(totoalamount * (document.getElementById(IdIgstPer).value)) / 100;
                    

                    var totalAmntWithTax = (parseFloat(totoalamount) + parseFloat(CgsttaxAmnt) + parseFloat(SgsttaxAmnt) + parseFloat(IgsttaxAmnt)).toFixed(2);
                   


                    document.getElementById(idCgstAmnt).value = (CgsttaxAmnt).toFixed(2);
                    document.getElementById(idSgstAmnt).value = (SgsttaxAmnt).toFixed(2);
                    document.getElementById(IdIgstAmnt).value = (IgsttaxAmnt).toFixed(2);
                    document.getElementById(idamountWithTax).value = totalAmntWithTax;
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
