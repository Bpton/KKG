<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmDespatchMM.aspx.cs" Inherits="VIEW_frmDespatchMM" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="~/images/loading123.gif" AlternateText="Processing"
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
                                <h6>DESPATCH DETAILS</h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAdd" runat="server" Text="Add New Record" CssClass="btn_link"
                                        CausesValidation="false" OnClick="btnAdd_Click" />
                                </div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px">
                                                <fieldset>
                                                    <legend>Despatch Entry Details</legend>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr runat="server" id="trAutoDespatchNo">
                                                            <td class="field_title" width="80">
                                                                <asp:Label ID="lblStockRcvdNo" Text="Despatch&nbsp;No." runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input" colspan="5">
                                                                <asp:TextBox ID="txtDespatchNo" runat="server" Width="270" placeholder="Auto Generate No."
                                                                    Enabled="false" Font-Bold="true"> </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="100" class="field_title">
                                                                <asp:Label ID="Label2" Text="Despatch&nbsp;Dt" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="200" class="field_input">
                                                                <asp:TextBox ID="txtDespatchDate" runat="server" Enabled="false" Width="60" placeholder="dd/MM/yyyy"
                                                                    MaxLength="10" ValidationGroup="Save"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/calendar.png"
                                                                    Width="24px" Height="24px" ImageAlign="AbsMiddle" Enabled="true" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderDespatchDate" TargetControlID="txtDespatchDate"
                                                                    PopupButtonID="ImageButton1" runat="server" Format="dd/MM/yyyy" Enabled="true"
                                                                    BehaviorID="CalendarExtenderDespatchDate" CssClass="cal_Theme1" />
                                                            </td>
                                                            <td class="field_title" width="70">
                                                                <asp:Label ID="Label14" Text="Vendor" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" width="200">
                                                                <asp:DropDownList ID="ddlvendor" Width="220" runat="server" AutoPostBack="true" class="chosen-select"
                                                                    data-placeholder="Choose Vebdor" AppendDataBoundItems="True" ValidationGroup="Save"
                                                                    OnSelectedIndexChanged="ddlvendor_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorDepot" runat="server" ControlToValidate="ddlvendor"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="Save"
                                                                    ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td class="field_title" width="70">
                                                                <asp:Label ID="Label41" Text="WorkOrder" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" width="200">
                                                                <asp:DropDownList ID="ddlPo" Width="190" runat="server" AutoPostBack="true" class="chosen-select"
                                                                    data-placeholder="Choose PO" AppendDataBoundItems="True" ValidationGroup="Save"
                                                                    OnSelectedIndexChanged="ddlPo_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlPo"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="Save"
                                                                    ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </td>

                                                            <td class="field_title" width="70">
                                                                <asp:Label ID="lblissueno" Text="IssueNo" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" width="200">
                                                                <asp:ListBox ID="ddlIssue" runat="server" SelectionMode="Multiple" TabIndex="4"
                                                                    ValidationGroup="ADD" AppendDataBoundItems="True" name="options[]" multiple="multiple"
                                                                    OnSelectedIndexChanged="ddlIssue_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                                                                <asp:CustomValidator ID="CvddlIssue" runat="server" ValidateEmptyText="true"
                                                                    ControlToValidate="ddlIssue" ValidationGroup="ADD" ErrorMessage="Required!" Display="Dynamic"
                                                                    ClientValidationFunction="ValidateListBox_Issue" ForeColor="Red"></asp:CustomValidator>
                                                                <asp:RequiredFieldValidator ID="RFVddlIssue" runat="server" ControlToValidate="ddlIssue"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!"
                                                                    ForeColor="Red" InitialValue="0">
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>&nbsp;

                                                            </td>


                                                        </tr>
                                                        <tr>
                                                            <td class="field_title" width="75">
                                                                <asp:Label ID="lblgatepassno" Text="GatePass&nbsp;No" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input" width="70">
                                                                <asp:TextBox ID="txtgatepassno" runat="server" Width="70" placeholder="GATEPASS NO"
                                                                    AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"> </asp:TextBox>
                                                            </td>
                                                            <td class="field_title" width="70">
                                                                <asp:Label ID="lblgatepassdate" Text="GatePass&nbsp;Dt" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input" width="110">
                                                                <asp:TextBox ID="txtgatepassdate" runat="server" Width="70" placeholder="dd/MM/yyyy"
                                                                    Enabled="false" MaxLength="10"> </asp:TextBox>
                                                                <asp:ImageButton ID="Imgbtngatepass" runat="server" ImageUrl="~/images/calendar.png"
                                                                    Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtgatepassdate"
                                                                    PopupButtonID="Imgbtngatepass" runat="server" Format="dd/MM/yyyy" Enabled="true"
                                                                    BehaviorID="CalendarExtender2" CssClass="cal_Theme1" />
                                                            </td>

                                                            <td class="field_title">
                                                                <asp:Label ID="lblTransportMode" Text="Trans. By" runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlTransportMode" Width="100" runat="server" class="chosen-select"
                                                                    data-placeholder="Choose Transport Mode" ValidationGroup="Save">
                                                                    <asp:ListItem Text="By Road" Value="By Road"></asp:ListItem>
                                                                    <asp:ListItem Text="By Rail" Value="By Rail"></asp:ListItem>
                                                                    <asp:ListItem Text="By Air" Value="By Air"></asp:ListItem>
                                                                    <asp:ListItem Text="By Ship" Value="By Ship"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="lblTransporter" Text="Trans." runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlTransporter" Width="150" runat="server" class="chosen-select"
                                                                    data-placeholder="Choose Transporter" AppendDataBoundItems="True" ValidationGroup="Save">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="lblVehicle" Text="Vehicle&nbsp;No." runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtVehicle" runat="server" Width="80" placeholder="Vehicle No"
                                                                    ValidationGroup="Save" AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"> </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">
                                                                <asp:Label ID="lblLRGRNo" Text="LR/GR No" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtLRGRNo" runat="server" Width="70" placeholder="LR/GR No" ValidationGroup="Save"
                                                                    AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"> </asp:TextBox>
                                                            </td>


                                                            <td class="field_title">
                                                                <asp:Label ID="lblLRGRDate" Text="LR/GR Dt" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtLRGRDate" runat="server" Enabled="false" Width="70" placeholder="dd/MM/yyyy"
                                                                    MaxLength="10" ValidationGroup="Save"></asp:TextBox>
                                                                <asp:ImageButton ID="imgbtnLRGRCalendar" runat="server" ImageUrl="~/images/calendar.png"
                                                                    Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderLRGRDate" TargetControlID="txtLRGRDate"
                                                                    PopupButtonID="imgbtnLRGRCalendar" runat="server" Format="dd/MM/yyyy" Enabled="true"
                                                                    BehaviorID="CalendarExtenderLRGRDate" CssClass="cal_Theme1" />
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="lblinvoice" Text="INVOICE NO" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtinvoiceno" runat="server" Width="90" placeholder="INVOICE NO" ValidationGroup="Save"
                                                                    AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"> </asp:TextBox>
                                                            </td>
                                                            <td width="100" class="field_title">
                                                                <asp:Label ID="lblinvoicedate" Text="INVOICE DATE" runat="server"></asp:Label>
                                                            </td>
                                                            <td width="200" class="field_input">
                                                                <asp:TextBox ID="txtinvoicedate" runat="server" Enabled="false" Width="60" placeholder="dd/MM/yyyy"
                                                                    MaxLength="10" ValidationGroup="Save"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButtonInv" runat="server" ImageUrl="~/images/calendar.png"
                                                                    Width="24px" Height="24px" ImageAlign="AbsMiddle" Enabled="true" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderInvoiceDate" TargetControlID="txtinvoicedate"
                                                                    PopupButtonID="ImageButtonInv" runat="server" Format="dd/MM/yyyy" Enabled="true"
                                                                    BehaviorID="CalendarExtenderInvoiceDate" CssClass="cal_Theme1" />
                                                            </td>
                                                            <td class="field_title" width="70">
                                                                <asp:Label ID="Label1" Text="PARTY NAME" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" width="120">
                                                                <asp:DropDownList ID="ddlpartyname" Width="150" runat="server" class="chosen-select"
                                                                    data-placeholder="Choose Party Name" AppendDataBoundItems="True" ValidationGroup="Save">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlvendor"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="Save"
                                                                    ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </td>

                                                        </tr>
                                                        <tr style="display: none;">
                                                            <td class="field_title">
                                                                <asp:Label ID="lblWayBill" Text="WayBill Key" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlWaybill" Width="110" runat="server" class="chosen-select"
                                                                    data-placeholder="Select Waybill Key" AppendDataBoundItems="True">
                                                                </asp:DropDownList>
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
                                            <div style="overflow: scroll;" onscroll="OnScrollDiv(this)">
                                                <asp:GridView ID="grdproductdetails" runat="server" Width="100%" CssClass="zebra"
                                                    AutoGenerateColumns="false" EmptyDataText="No Records Available" ShowFooter="false">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PRODUCTID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblproductid1" runat="server" Text='<%# Bind("PRODUCTID") %>' value='<%# Eval("PRODUCTID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PRODUCTNAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblproductname1" runat="server" Text='<%# Bind("PRODUCTNAME") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="WORKORDER NO ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblWORKORDERNO" runat="server" Text='<%# Bind("WORKORDERNO") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ORDER QTY">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblissueqty1" runat="server" Text='<%# Bind("QTY") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="UOMID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlbluomid1" runat="server" Text='<%# Bind("UOMID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="UOM">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlbluomname1" runat="server" Text='<%# Bind("UOMNAME") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MRP" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblmrp1" runat="server" Text='<%# Bind("MRP") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="JOB ORDER COST">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblrate1" runat="server" Text='<%# Bind("RATE") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="JOB ORDER AMOUNT">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblamount1" runat="server" Text='<%# Bind("AMOUNT") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="BATCHNO" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblbatchno1" runat="server" Text='<%# Bind("BATCHNO") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TOTMRP" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlbltotalmrp1" runat="server" Text='<%# Bind("TOTMRP") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ALLOCATEDQTY" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblallocatedqty1" runat="server" Text='<%# Bind("ALLOCATEDQTY") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="PURCHASE COST">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblPurchaseCost" runat="server" Text='<%# Bind("PRODUCTRATE") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PURCHASE AMOUNT">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblPurchaseAmount" runat="server" Text='<%# Bind("PURCHASEAMT") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                      


                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div class="gridcontent-inner">
                                        <fieldset>
                                            <legend>BOM Details</legend>
                                            <div style="overflow: scroll;" onscroll="OnScrollDiv(this)">
                                                <asp:GridView ID="grdAddDespatch" runat="server" Width="100%" CssClass="zebra" AutoGenerateColumns="false"
                                                    EmptyDataText="No Records Available" ShowFooter="true" OnRowDataBound="grdAddDespatch_RowDataBound" >
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="BULKPRODUCTID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblWorkOrderproductid" runat="server" Text='<%# Bind("BULKPRODUCTID") %>' value='<%# Eval("BULKPRODUCTID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="BULKPRODUCTNAME" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblBulkproductname" runat="server" Text='<%# Bind("BULKPRODUCTNAME") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="PRODUCTID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblproductid" runat="server" Text='<%# Bind("PRODUCTID") %>' value='<%# Eval("PRODUCTID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PRODUCTNAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblproductname" runat="server" Text='<%# Bind("PRODUCTNAME") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="REQUISITION QTY" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblrequisitionqty" runat="server" Text='<%# Bind("REQUISITIONQTY") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="REQUISITION QTY">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" Enabled="false" width="60px" ID="grdtxtrequisitionqty" Text='<%# Bind("REQUISITIONQTY") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                         <asp:TemplateField HeaderText="STOCK QTY" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblstockqty" runat="server" Text='<%# Bind("STOCKQTY") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                         <asp:TemplateField HeaderText="STOCK QTY" >
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdTextstockqty" Enabled="false" width="60px" runat="server" Text='<%# Bind("STOCKQTY") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="ISSUE QTY">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblissueqty" runat="server" Text='<%# Bind("QTY") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DESPATCH QTY">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdtxtpoqty" AutoPostBack="false" runat="server"
                                                                    Style="text-align: right;" Width="70px" Height="20" Text='<%# Eval("QTY") %>'
                                                                    onkeyup="stockqtyvalidation(this);"  onkeypress="return isNumberKeyWithDot(event);"/>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                         <asp:TemplateField HeaderText="RATE">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdRate" AutoPostBack="false" runat="server" 
                                                                    Style="text-align: right;" Width="70px" Height="20" Text='<%# Eval("RATE") %>'
                                                                    onkeyup="CalculateTax(this);"
                                                                    onkeypress="return isNumberKeyWithDot(event);" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="NET(₹)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdTAXABLEAMNT" Enabled="false"  Style="text-align: right;" Width="70px" Height="20" 
                                                                  Text='<%# Eval("AMOUNT") %>'  runat="server"  />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                         <asp:TemplateField HeaderText="CGST(%)">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" Enabled="false"  Style="text-align: right;" Width="70px" Height="20"  ID="grdCGST_PERCENTAGE"  Text='<%# Bind("CGSTPERCENT") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CGST(₹)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdCGSTAMOUNT" Enabled="false"  Style="text-align: right;" Width="70px" Height="20" runat="server" Text='<%# Bind("CGSTAMOUNT") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SGST(%)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdSGST_PERCENTAGE" Enabled="false"  Style="text-align: right;" Width="70px" Height="20" runat="server" Text='<%# Bind("SGSTPERCENT") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SGST(₹)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdSGSTAMOUNT" Enabled="false"  Style="text-align: right;" Width="70px" Height="20" runat="server" Text='<%# Bind("SGSTAMOUNT") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="IGST(%)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdIGST_PERCENTAGE" Enabled="false"  Style="text-align: right;" Width="70px" Height="20" runat="server" Text='<%# Bind("IGSTPERCENT") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="IGST(₹)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdIGSTAMOUNT" Enabled="false"  Style="text-align: right;" Width="70px" Height="20" runat="server" Text='<%# Bind("IGSTAMOUNT") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TOTAL AMOUNT">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdTOTALAMOUNT" runat="server"  Style="text-align: right;" Width="70px" Height="20" Enabled="false" Text='<%# Bind("AFTERTAXAMNT") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         
                                                        <asp:TemplateField HeaderText="UOMID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlbluomid" runat="server" Text='<%# Bind("UOMID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="UOM">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlbluomname" runat="server" Text='<%# Bind("UOMNAME") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MRP" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblmrp" runat="server" Text='<%# Bind("MRP") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                     <%--   <asp:TemplateField HeaderText="RATE" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblrate" runat="server" Text='<%# Bind("RATE") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="AMOUNT" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdtxtamount" AutoPostBack="false" runat="server" Enabled="false"
                                                                    Style="text-align: right;" Width="70px" Height="20" Text='<%# Bind("AMOUNT") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="BATCHNO" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblbatchno" runat="server" Text='<%# Bind("BATCHNO") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TOTMRP" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlbltotalmrp" runat="server" Text='<%# Bind("TOTMRP") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ALLOCATEDQTY" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblallocatedqty" runat="server" Text='<%# Bind("ALLOCATEDQTY") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ISSUEID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblissueid" runat="server" Text='<%# Bind("ISSUEID") %>' value='<%# Eval("ISSUEID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="FROM STORE LOCATION ID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblfromstorelocationid" runat="server" Text='<%# Bind("STORELOCATIONID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="FROM STORE LOCATION">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblfromstorelocation" runat="server" Text='<%# Bind("STORELOCATIONNAME") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div style="padding-top: 10px; padding-right: 50px; float: right; display: none;">
                                        <div class="btn_24_blue">
                                            <span class="icon add_co"></span>
                                            <asp:Button ID="btnorderadd" runat="server" Text="Add" CssClass="btn_link" OnClick="btnorderadd_Click" />
                                        </div>
                                    </div>
                                    <fieldset style="display: none;">
                                        <legend>Despatch Details</legend>
                                        <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="Div2">
                                            <asp:GridView ID="grddespatchrecord" runat="server" Width="100%" CssClass="zebra"
                                                AutoGenerateColumns="false" EmptyDataText="No Records Available" ShowFooter="true">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="GUID" HeaderText="GUID" HeaderStyle-Wrap="false" Visible="false" />
                                                    <asp:BoundField DataField="PRODUCTNAME" HeaderText="PRODUCT" HeaderStyle-Wrap="false" />
                                                    <asp:BoundField DataField="QTY" HeaderText="QTY" HeaderStyle-Wrap="false" />
                                                    <asp:BoundField DataField="UOMNAME" HeaderText="UOM" HeaderStyle-Wrap="false" />
                                                    <asp:BoundField DataField="RATE" HeaderText="RATE" HeaderStyle-Wrap="false" Visible="false" />
                                                    <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT" HeaderStyle-Wrap="false" Visible="false" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </fieldset>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" valign="top" style="padding-left: 10px; width: 75%;">
                                                <fieldset>
                                                    <legend>Remarks & Save</legend>
                                                    <table border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="80" class="innerfield_title">
                                                                <label>
                                                                    Remarks</label>
                                                            </td>
                                                            <td width="320">
                                                                <asp:TextBox ID="txtRemarks" runat="server" placeholder="Remarks if any...." CssClass="mid"
                                                                    TextMode="MultiLine" Style="width: 300px; height: 57px;"> </asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <div class="btn_24_blue" id="btnsubmitdiv" runat="server">
                                                                    <span class="icon disk_co"></span>
                                                                    <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn_link" OnClick="btnSubmit_Click"
                                                                        AccessKey="S" ValidationGroup="Save" />
                                                                </div>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <div class="btn_24_blue">
                                                                    <span class="icon cross_octagon_co"></span>
                                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn_link" CausesValidation="false"
                                                                        OnClick="btnCancel_Click" />
                                                                </div>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <div class="btn_24_green" id="divbtnapprove" runat="server">
                                                                    <span class="icon approve_co"></span>
                                                                    <asp:LinkButton ID="btnApprove" runat="server" Text="Approve" CssClass="btn_link"
                                                                        CausesValidation="false" OnClick="btnApprove_Click" />
                                                                </div>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <div class="btn_24_red" id="divbtnrejection" runat="server">
                                                                    <span class="icon reject_co"></span>
                                                                    <asp:LinkButton ID="btnreject" runat="server" Text="Reject" CssClass="btn_link" CausesValidation="false"
                                                                        OnClick="btnreject_Click" />
                                                                </div>
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                            <td class="field_input" valign="top" style="padding-right: 40px; width: 25%;">
                                                <fieldset>
                                                    <legend>Amount&nbsp;Details</legend>
                                                    <table border="2" cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            <td valign="top" style="width: 50%; padding-right: 20px">
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td class="innerfield_title" style="color: #1e69de;">
                                                                            <label>
                                                                                Gross Amt.</label>
                                                                        </td>
                                                                        <td style="float: right;">
                                                                            <asp:TextBox runat="server" ID="txtTotalGross" placeholder="Gross Amount" BackColor="Transparent"
                                                                                Font-Bold="true" Enabled="false" Style="text-align: right;"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    
                                                                    <tr>
                                                                        <td class="innerfield_title" style="color: #1e69de;">
                                                                            <label>
                                                                                Tax Amt.</label>
                                                                        </td>
                                                                        <td style="float: right;">
                                                                            <asp:TextBox runat="server" ID="txtTaxAmnt" placeholder="Net Amtount" BackColor="Transparent"
                                                                                Font-Bold="true" Enabled="false" Style="text-align: right;"></asp:TextBox>
                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td class="innerfield_title" style="color: #1e69de;">
                                                                            <label>
                                                                                Round Off</label>
                                                                        </td>
                                                                        <td style="float: right;">
                                                                            <asp:TextBox runat="server" ID="txtRoundoff" placeholder="Round Off" BackColor="Transparent"
                                                                                Font-Bold="true" Enabled="false" Style="text-align: right;"></asp:TextBox>
                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td class="innerfield_title" style="color: #1e69de;">
                                                                            <label>
                                                                                Net Amt.</label>
                                                                        </td>
                                                                        <td style="float: right;">
                                                                            <asp:TextBox runat="server" ID="txtFinalAmt" placeholder="Net Amtount" BackColor="Transparent"
                                                                                Font-Bold="true" Enabled="false" Style="text-align: right;"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="display: none;">
                                                                        <td class="innerfield_title" style="color: #1e69de;">
                                                                            <label>
                                                                                Other Charge</label>
                                                                        </td>
                                                                        <td style="float: right;">
                                                                            <asp:TextBox runat="server" ID="txtOtherCharge" CssClass="x-large" placeholder="Other Charge"
                                                                                Style="text-align: right;" Text="0"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="display: none;">
                                                                        <td class="innerfield_title" style="color: #1e69de;">
                                                                            <label>
                                                                                Adj. Amt</label>
                                                                        </td>
                                                                        <td style="float: right;">
                                                                            <asp:TextBox runat="server" ID="txtAdj" CssClass="x-large" placeholder="Adj. Amt"
                                                                                Style="text-align: right;" Text="0"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField runat="server" ID="hdnDespatchID" />
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
                                                        <td width="110">
                                                            <asp:TextBox ID="txtFromDate" runat="server" MaxLength="10" Width="70" Enabled="false"
                                                                placeholder="dd/MM/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopup" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarFromDate" PopupButtonID="imgPopup" runat="server"
                                                                TargetControlID="txtFromDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td width="70">
                                                            <asp:Label ID="Label19" runat="server" Text="To Date"></asp:Label>
                                                        </td>
                                                        <td width="110">
                                                            <asp:TextBox ID="txtToDate" runat="server" MaxLength="10" Width="70" Enabled="false"
                                                                placeholder="dd/mm/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopuptodate" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtenderToDate" PopupButtonID="imgPopuptodate"
                                                                runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td width="110" valign="top">
                                                            <div class="btn_24_blue">
                                                                <span class="icon find_co"></span>
                                                                <asp:Button ID="btnSearchDespatch" runat="server" Text="Search" CssClass="btn_link"
                                                                    ValidationGroup="Search" OnClick="btnSearchDespatch_Click" />
                                                            </div>
                                                        </td>
                                                        <td>&nbsp;
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
                                        <cc1:Grid ID="grdDespatchHeader" runat="server" CallbackMode="true" Serialize="true"
                                            FolderStyle="../GridStyles/premiere_blue" AutoGenerateColumns="false" OnDeleteCommand="DeleteRecordDespatch"
                                            AllowAddingRecords="false" AllowFiltering="true" AllowPageSizeSelection="false"
                                            OnRowDataBound="grdDespatchHeader_RowDataBound" AllowPaging="true" PageSize="30">
                                            <ClientSideEvents OnBeforeClientDelete="OnBeforeDespatchDelete" OnClientDelete="OnDeleteDespatchDetails" />
                                            <FilteringSettings InitialState="Visible" />
                                            <Columns>
                                                <cc1:Column ID="Column12" DataField="Sl" HeaderText="SL" runat="server" Width="60">
                                                    <TemplateSettings TemplateId="slnoTemplateIN" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column19" DataField="STOCKDESPATCHID" HeaderText="STOCKDESPATCHID"
                                                    runat="server" Width="60" Visible="false" />
                                                <cc1:Column ID="Column91" DataField="STOCKDESPATCHNO" HeaderText="DESPATCH NO." runat="server"
                                                    Width="100%">
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
                                                <cc1:Column ID="Column8" DataField="DESPATCHDATE" HeaderText="DATE" runat="server"
                                                    Width="100">
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
                                                <cc1:Column ID="Column15" DataField="VEHICHLENO" HeaderText="VEHICHLE NO" runat="server"
                                                    Width="90" Visible="false" />
                                                <cc1:Column ID="Column16" DataField="LRGRNO" HeaderText="LR/GR NO" runat="server"
                                                    Width="120" Visible="false">
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
                                                    Visible="false" Width="120" Wrap="true">
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
                                                <cc1:Column ID="Column89" DataField="MOTHERDEPOTNAME" HeaderText="FACTORY" runat="server"
                                                    Width="100%" Wrap="true">
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

                                                <cc1:Column ID="Column1" DataField="VENDORNAME" HeaderText="VENDOR NAME" runat="server"
                                                    Width="100%" Wrap="true">
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
                                                    Width="100" />
                                                <cc1:Column ID="Column25" DataField="ISVERIFIEDDESC" HeaderText="STATUS" runat="server"
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
                                                <cc1:Column ID="Column92" AllowEdit="true" AllowDelete="true" HeaderText="EDIT" runat="server"
                                                    Width="60">
                                                    <TemplateSettings TemplateId="editBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="DELETE" AllowEdit="false" AllowDelete="true" Width="95">
                                                    <TemplateSettings TemplateId="deleteBtnTemplateDespatch" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column_Print" AllowEdit="false" AllowDelete="true" HeaderText="PRINT" runat="server"
                                                    Width="60">
                                                    <TemplateSettings TemplateId="PrintBtnTemplate" />
                                                </cc1:Column>
                                            </Columns>
                                            <FilteringSettings MatchingType="AnyFilter" />
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="editBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-edit" title="Edit" id="btnGridEdit_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethod(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="deleteBtnTemplateDespatch">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="grdDespatchHeader.delete_record(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                                <cc1:GridTemplate runat="server" ID="slnoTemplateIN">
                                                    <Template>
                                                        <asp:Label ID="lblslnoin" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
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
                                            <ScrollingSettings ScrollWidth="100%" ScrollHeight="290" />
                                        </cc1:Grid>
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" Style="display: none" OnClick="btnPrint_Click"
                                            CausesValidation="false" />
                                        <asp:Button ID="btngrdedit" runat="server" Text="grdedit" Style="display: none" OnClick="btngrdedit_Click"
                                            CausesValidation="false" />
                                        <asp:HiddenField runat="server" ID="hdnTotalCase" />
                                        <asp:HiddenField runat="server" ID="hdnDespatchNo" />
                                        <asp:HiddenField runat="server" ID="hdnStatus" />
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <cc1:MessageBox ID="MessageBox1" runat="server" />
                </div>


           
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

                    function Tax_MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
                        var tbl = document.getElementById(gridId);
                        if (tbl) {
                            var DivHR = document.getElementById('Tax_DivHeaderRow');
                            var DivMC = document.getElementById('Tax_DivMainContent');
                            var DivFR = document.getElementById('Tax_DivFooterRow');

                            //*** Set divheaderRow Properties ****
                            DivHR.style.height = headerHeight + 'px';
                            //DivHR.style.width = (parseInt(width) - 16) + 'px';
                            DivHR.style.width = '100%';
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

                    function Tax_OnScrollDiv(Scrollablediv) {
                        document.getElementById('Tax_DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
                        document.getElementById('Tax_DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
                    }

                   

                    function CalculateTax(a) {

                        try {
                            var amnt = 0;
                            var gross = 0;
                            var ttax = 0;
                            amnt = CalculateTaxAmnt(a);

                            if (amnt > 0 ||amnt == -1) {
                                calculateFooter(a);
                            }
                        }
                        catch (ex) {
                            alert('1st'+ex);
                        }
                        
                    }

                    function stockqtyvalidation(a) {
                        try {
                            
                           var rowData = a.parentNode.parentNode;
                           var rowIndex = rowData.rowIndex - 1;
                           var grd = document.getElementById('<%= grdAddDespatch.ClientID %>');
                           var grid = document.getElementById('ContentPlaceHolder1_grdAddDespatch');
                           var despatchQtyId = "ContentPlaceHolder1_grdAddDespatch_grdtxtpoqty_" + rowIndex;
                           var stcokQtyId = "ContentPlaceHolder1_grdAddDespatch_grdTextstockqty_" + rowIndex;
                           var requisitionQtyId = "ContentPlaceHolder1_grdAddDespatch_grdtxtrequisitionqty_" + rowIndex;
                           var rateId = "ContentPlaceHolder1_grdAddDespatch_grdRate_" + rowIndex;

                           document.getElementById(rateId).value = 0;

                           despatchQty = (parseFloat(document.getElementById(despatchQtyId).value));
                           stcokQty = (parseFloat(document.getElementById(stcokQtyId).value));
                           requisitionQty = (parseFloat(document.getElementById(requisitionQtyId).value));
                        

                            if (despatchQty > stcokQty) {
                                alert('Stockqty not avilable please check');
                                document.getElementById(despatchQtyId).value = 0;
                                return;
                            }
                            else if (despatchQty > requisitionQty) {
                                alert('Despatchqty must be same as requisitionqty');
                                document.getElementById(despatchQtyId).value = 0;
                                return;
                            }

                        }
                        catch (ex) {
                            alert('error on qty'+ex)
                        }
                    }

                   function CalculateTaxAmnt(a) {
                   
                       try {

                          
                           var taxValue = 0;
                           var rowData = a.parentNode.parentNode;
                           var rowIndex = rowData.rowIndex - 1;
                           var grd = document.getElementById('<%= grdAddDespatch.ClientID %>');
                           var grid = document.getElementById('ContentPlaceHolder1_grdAddDespatch');
                           var despatchQtyId = "ContentPlaceHolder1_grdAddDespatch_grdtxtpoqty_" + rowIndex;
                           var rateId = "ContentPlaceHolder1_grdAddDespatch_grdRate_" + rowIndex;
                           var cgstPerId = "ContentPlaceHolder1_grdAddDespatch_grdCGST_PERCENTAGE_" + rowIndex;
                           var cgstAmntId = "ContentPlaceHolder1_grdAddDespatch_grdCGSTAMOUNT_" + rowIndex;
                           var sgstPerId = "ContentPlaceHolder1_grdAddDespatch_grdSGST_PERCENTAGE_" + rowIndex;
                           var sgstAmntId = "ContentPlaceHolder1_grdAddDespatch_grdSGSTAMOUNT_" + rowIndex;
                           var igstPerId = "ContentPlaceHolder1_grdAddDespatch_grdIGST_PERCENTAGE_" + rowIndex;
                           var igstAmntId = "ContentPlaceHolder1_grdAddDespatch_grdIGSTAMOUNT_" + rowIndex;
                           var taxableAmntId = "ContentPlaceHolder1_grdAddDespatch_grdTAXABLEAMNT_" + rowIndex;
                           var totalAmntId = "ContentPlaceHolder1_grdAddDespatch_grdTOTALAMOUNT_" + rowIndex;

                           var despatchQty = 0;
                           var rate = 0;
                           var cgstTaxPer = 0;
                           var sgstTaxPer = 0;
                           var igstTaxPer = 0;
                           

                           despatchQty = (parseFloat(document.getElementById(despatchQtyId).value));
                           rate = (parseFloat(document.getElementById(rateId).value));
                           cgstTaxPer = (parseFloat(document.getElementById(cgstPerId).value));
                           sgstTaxPer = (parseFloat(document.getElementById(sgstPerId).value));
                           igstTaxPer = (parseFloat(document.getElementById(igstPerId).value));
                          


                           var amnt = 0;
                           var cTaxAmnt = 0;
                           var sTaxAmnt = 0;
                           var iTaxAmnt = 0;
                           var totalAmnt = 0;
                           if (rate > 0) {
                               amnt = parseFloat(despatchQty) * parseFloat(rate);
                               cTaxAmnt = (parseFloat(amnt) * parseFloat(cgstTaxPer)) / 100;
                               sTaxAmnt = (parseFloat(amnt) * parseFloat(sgstTaxPer)) / 100;
                               iTaxAmnt = (parseFloat(amnt) * parseFloat(igstTaxPer)) / 100;

                               if (iTaxAmnt == 0) {
                                   totalAmnt = parseFloat(amnt) + parseFloat(cTaxAmnt) + parseFloat(sTaxAmnt);
                               }
                               else {
                                   totalAmnt = parseFloat(amnt) + parseFloat(iTaxAmnt);
                               }

                               document.getElementById(cgstAmntId).value = parseFloat(cTaxAmnt).toFixed(2);
                               document.getElementById(sgstAmntId).value = parseFloat(sTaxAmnt).toFixed(2);
                               document.getElementById(igstAmntId).value = parseFloat(iTaxAmnt).toFixed(2);
                               document.getElementById(taxableAmntId).value = parseFloat(amnt).toFixed(2);
                               document.getElementById(totalAmntId).value = parseFloat(totalAmnt).toFixed(2);

                               taxValue = amnt;
                           }
                           else {

                           amnt = parseFloat(despatchQty) * parseFloat(0);
                           cTaxAmnt = (parseFloat(amnt) * parseFloat(cgstTaxPer)) / 100;
                           sTaxAmnt = (parseFloat(amnt) * parseFloat(sgstTaxPer)) / 100;
                           iTaxAmnt = (parseFloat(amnt) * parseFloat(igstTaxPer)) / 100;

                           if (iTaxAmnt == 0) {
                               totalAmnt = parseFloat(amnt) + parseFloat(cTaxAmnt) + parseFloat(sTaxAmnt);
                           }
                           else {
                               totalAmnt = parseFloat(amnt) + parseFloat(iTaxAmnt);
                           }

                           document.getElementById(cgstAmntId).value = parseFloat(cTaxAmnt).toFixed(2);
                           document.getElementById(sgstAmntId).value = parseFloat(sTaxAmnt).toFixed(2);
                           document.getElementById(igstAmntId).value = parseFloat(iTaxAmnt).toFixed(2);
                           document.getElementById(taxableAmntId).value = parseFloat(amnt).toFixed(2);
                           document.getElementById(totalAmntId).value = parseFloat(totalAmnt).toFixed(2);
                          
                           taxValue = -1;
                           }
                           

                           return taxValue;
                           
                       }
                       catch (ex) {
                           alert('2nd'+ex);
                       }
                   
                   }

                    function calculateFooter(a) {
                        try {
                            debugger;

                            var rowData = a.parentNode.parentNode;
                            var rowIndex = rowData.rowIndex - 1;
                            var grd = document.getElementById('<%= grdAddDespatch.ClientID %>');
                            var grid = document.getElementById('ContentPlaceHolder1_grdAddDespatch');
                            var footerNetAmnt = 0;

                            var footerRoAmnt = 0;
                            var rate = 0;
                            var footerTaxAmnt = 0;
                            var footerTotalAmnt = 0;
                            var footerNetAmntId = "";
                            var cgstAmnt = "";
                            var sgstAmnt = "";
                            var igstAmnt = "";
                            var amnt = "";

                            var IdtxtTotalGross = "ContentPlaceHolder1_txtTotalGross";
                            var IdtxtTaxAmnt = "ContentPlaceHolder1_txtTaxAmnt";
                            var IdtxtFinalAmt = "ContentPlaceHolder1_txtFinalAmt";
                            var IdtxtRoundoff = "ContentPlaceHolder1_txtRoundoff";

                            

                            for (var i = 0; i < grid.rows.length - 2; i++) {
                                debugger;
                                var rateId = "ContentPlaceHolder1_grdAddDespatch_grdRate_" + rowIndex;
                                rate = (parseFloat(document.getElementById(rateId).value));

                                if (rate > 0) {
                                    footerNetAmntId = "ContentPlaceHolder1_grdAddDespatch_grdTAXABLEAMNT_" + i;
                                    cgstAmnt = "ContentPlaceHolder1_grdAddDespatch_grdCGSTAMOUNT_" + i;
                                    sgstAmnt = "ContentPlaceHolder1_grdAddDespatch_grdSGSTAMOUNT_" + i;
                                    igstAmnt = "ContentPlaceHolder1_grdAddDespatch_grdIGSTAMOUNT_" + i;
                                    amnt = "ContentPlaceHolder1_grdAddDespatch_grdTOTALAMOUNT_" + i;

                                    footerNetAmnt = (parseFloat(footerNetAmnt) + parseFloat(document.getElementById(footerNetAmntId).value)).toFixed(2);
                                    footerTaxAmnt = (parseFloat(footerTaxAmnt) + (parseFloat(document.getElementById(cgstAmnt).value) + parseFloat(document.getElementById(sgstAmnt).value) + parseFloat(document.getElementById(igstAmnt).value))).toFixed(2);
                                    footerTotalAmnt = ((parseFloat(footerTotalAmnt)  + parseFloat(document.getElementById(amnt).value))).toFixed(2);
                                    

                                    document.getElementById(IdtxtTotalGross).value = footerNetAmnt;
                                    document.getElementById(IdtxtTaxAmnt).value = footerTaxAmnt;
                                    document.getElementById(IdtxtFinalAmt).value = footerTotalAmnt;
                                }

                                else if (rate == 0.00) {



                                    footerNetAmntId = "ContentPlaceHolder1_grdAddDespatch_grdTAXABLEAMNT_" + i;
                                    cgstAmnt = "ContentPlaceHolder1_grdAddDespatch_grdCGSTAMOUNT_" + i;
                                    sgstAmnt = "ContentPlaceHolder1_grdAddDespatch_grdSGSTAMOUNT_" + i;
                                    igstAmnt = "ContentPlaceHolder1_grdAddDespatch_grdIGSTAMOUNT_" + i;
                                    amnt = "ContentPlaceHolder1_grdAddDespatch_grdTOTALAMOUNT_" + i;

                                    footerNetAmnt = (parseFloat(footerNetAmnt) + parseFloat(document.getElementById(footerNetAmntId).value)).toFixed(2);
                                    footerTaxAmnt = (parseFloat(footerTaxAmnt) + (parseFloat(document.getElementById(cgstAmnt).value) + parseFloat(document.getElementById(sgstAmnt).value) + parseFloat(document.getElementById(igstAmnt).value))).toFixed(2);
                                    

                                    footerTotalAmnt = ((parseFloat(footerTotalAmnt) +  parseFloat(document.getElementById(cgstAmnt).value) + parseFloat(document.getElementById(sgstAmnt).value) + parseFloat(document.getElementById(igstAmnt).value) + parseFloat(document.getElementById(footerNetAmntId).value))).toFixed(2);


                                    document.getElementById(IdtxtTotalGross).value = footerNetAmnt;
                                    document.getElementById(IdtxtTaxAmnt).value = footerTaxAmnt;
                                    document.getElementById(IdtxtFinalAmt).value = footerTotalAmnt;

                                    

                                }

                            }

                           
                           
                            var roundOff = (parseFloat(Math.round(footerTotalAmnt)) - parseFloat(footerTotalAmnt)).toFixed(2);
                           
                                    if (roundOff > 0) {
                                        document.getElementById(IdtxtRoundoff).value = parseFloat(roundOff).toFixed(2);
                                        document.getElementById(IdtxtFinalAmt).value = (parseFloat(footerTotalAmnt) + parseFloat(roundOff)).toFixed(2);
                                    }
                                    else {
                                        document.getElementById(IdtxtRoundoff).value = parseFloat(roundOff).toFixed(2);
                                        document.getElementById(IdtxtFinalAmt).value = (parseFloat(footerTotalAmnt) - -1 * (parseFloat(roundOff))).toFixed(2);
                                    }

                        }
                        catch (ex) {
                            alert('error last'+ex);
                        }
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

                    function TabchangeTPU() {
                        elem = document.getElementById('<%= ddlPo.ClientID %>');
                        elem.focus();
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
                    var searchTimeout = null;
                    function FilterTextBox_KeyUp() {
                        searchTimeout = window.setTimeout(performSearch, 500);
                    }

                    function performSearch() {
                        var searchValue = document.getElementById('FilterTextBox').value;
                        if (searchValue == FilterTextBox.WatermarkText) {
                            searchValue = '';
                        }
                        grdDespatchHeader.addFilterCriteria('STOCKDESPATCHNO', OboutGridFilterCriteria.Contains, searchValue);
                        grdDespatchHeader.addFilterCriteria('DESPATCHDATE', OboutGridFilterCriteria.Contains, searchValue);
                        grdDespatchHeader.addFilterCriteria('VEHICHLENO', OboutGridFilterCriteria.Contains, searchValue);
                        grdDespatchHeader.addFilterCriteria('LRGRNO', OboutGridFilterCriteria.Contains, searchValue);
                        grdDespatchHeader.addFilterCriteria('TRANSPORTERNAME', OboutGridFilterCriteria.Contains, searchValue);
                        grdDespatchHeader.executeFilter();
                        searchTimeout = null;
                        return false;
                    }

                    function OnBeforeDespatchDelete(record) {
                        record.Error = '';
                        document.getElementById("<%=hdnDespatchID.ClientID %>").value = record.STOCKDESPATCHID;
                        if (confirm("Are you sure you want to delete? "))
                            return true;
                        else
                            return false;
                    }
                    function OnDeleteDespatchDetails(record) {
                        alert(record.Error);
                    }

                    function CallServerMethod(oLink) {
                        var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                        document.getElementById("<%=hdnDespatchID.ClientID %>").value = grdDespatchHeader.Rows[iRecordIndex].Cells[1].Value;
                        document.getElementById("<%=hdnStatus.ClientID %>").value = grdDespatchHeader.Rows[iRecordIndex].Cells[10].Value;
                        document.getElementById("<%=btngrdedit.ClientID %>").click();

                    }
                    function CallServerMethodPrint(oLink) {
                        var iRecordIndex = oLink.id.toString().replace("btnPrint_", "");
                        document.getElementById("<%=hdnDespatchID.ClientID %>").value = grdDespatchHeader.Rows[iRecordIndex].Cells[1].Value;
                        document.getElementById("<%=btnPrint.ClientID %>").click();
                    }
                </script>
                <script type="text/javascript">
                    function disableautocompletion(id) {
                        var TextBoxControl = document.getElementById(id);
                        TextBoxControl.setAttribute("autocomplete", "off");
                    }

                </script>
                <script type="text/javascript">
                    function ValidateListBox_Issue(sender, args) {
                        var options = document.getElementById("<%=ddlIssue.ClientID%>").options;
                        for (var i = 0; i < options.length; i++) {
                            if (options[i].selected == true) {
                                args.IsValid = true;
                                return;
                            }
                        }
                        args.IsValid = false;
                    }
                </script>

                <script type="text/javascript">
                    $(function () {
                        $('#ContentPlaceHolder1_ddlIssue').multiselect({
                            includeSelectAllOption: true
                        });
                        $("#ContentPlaceHolder1_ddlIssue").multiselect('selectAll', false);
                        $("#ContentPlaceHolder1_ddlIssue").multiselect('updateButtonText');
                    });
                </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>