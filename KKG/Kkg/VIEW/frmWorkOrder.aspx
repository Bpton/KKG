<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmWorkOrder.aspx.cs" Inherits="VIEW_frmWorkOrder" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <%--<div class="page_title">
                <span class="title_icon"><span class="computer_imac"></span></span>
                <h3>Purchase Order</h3>
            </div>--%>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active">
                                <span class="h_icon_he list_images"></span>
                                <h6>Work Order Details</h6>
                                <div id="divadd" runat="server" class="btn_30_light" style="float: right;">
                                    <span id="icon" class="icon add_co"></span>
                                    <asp:Button ID="btnnewentry" runat="server" Text="Add New Record" CssClass="btn_link"
                                        OnClick="btnnewentry_Click" CausesValidation="false" />
                                </div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="InputTable" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>WORK ORDER DETAILS</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td width="70" class="field_title">
                                                                <asp:Label ID="Label5" runat="server" Text="VENDOR"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="260" colspan="2" class="field_input">
                                                                <asp:DropDownList ID="ddlTPUName" runat="server" Width="240px" ValidationGroup="POFooter"
                                                                    OnSelectedIndexChanged="ddlTPUName_SelectedIndexChanged" AutoPostBack="true"
                                                                    class="chosen-select" data-placeholder="SELECT VENDOR NAME">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="CV_TPUNAME" runat="server" ControlToValidate="ddlTPUName"
                                                                    InitialValue="0" SetFocusOnError="true" ErrorMessage="*" ForeColor="Red" ValidationGroup="POFooter"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td width="90" class="field_title">
                                                                <asp:Label ID="Label14" runat="server" Text="ORDER TYPE"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="150" class="field_input">
                                                                <asp:DropDownList ID="ddlorderfor" runat="server" Width="150px" ValidationGroup="POFooter"
                                                                    class="chosen-select" data-placeholder="SELECT ORDER TYPE">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td width="80" class="field_title">
                                                                <asp:Label ID="Label11" runat="server" Text="Entry Date"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="110" class="field_input">
                                                                <asp:TextBox ID="txtpodate" runat="server" Width="70" placeholder="dd/mm/yyyy" MaxLength="10"
                                                                    ValidationGroup="datecheckpodetail" Enabled="false" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                <asp:ImageButton ID="imgPopuppodate" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="imgPopuppodate"
                                                                    runat="server" TargetControlID="txtpodate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>
                                                            <td width="120" class="field_title" id="divponoheader" runat="server">
                                                                <asp:Label ID="Label15" runat="server" Text="PO No"></asp:Label>
                                                            </td>
                                                            <td width="350" class="field_input" style="padding-bottom: 10px;" id="divpono" runat="server">
                                                                <asp:TextBox ID="txtpono" Width="70%" runat="server" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr id="trscheduledate" runat="server">
                                                            <td class="field_title" style="padding-bottom: 20px;">
                                                                <asp:Label ID="Label1" runat="server" Text="Delivery"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="120px" class="field_input">
                                                                <asp:TextBox ID="txtdelfromdate" runat="server" Width="80" placeholder="dd/mm/yyyy"
                                                                    MaxLength="10" Enabled="false" onkeypress="return isNumberKeyWithslash(event);"
                                                                    ValidationGroup="Scheduledate"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButtontxtdelfromdate" ImageUrl="~/images/calendar.png"
                                                                    ImageAlign="AbsMiddle" runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButtontxtdelfromdate"
                                                                    runat="server" TargetControlID="txtdelfromdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label6" runat="server" Text="FROM DATE"></asp:Label></span>
                                                            </td>
                                                            <td width="120px" class="field_input">
                                                                <asp:TextBox ID="txtdeltodate" runat="server" Width="80" placeholder="dd/mm/yyyy"
                                                                    MaxLength="10" Enabled="false" onkeypress="return isNumberKeyWithslash(event);"
                                                                    ValidationGroup="Scheduledate"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButtondeltodate" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender5" PopupButtonID="ImageButtondeltodate"
                                                                    runat="server" TargetControlID="txtdeltodate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label2" runat="server" Text="TO DATE"></asp:Label></span>
                                                            </td>
                                                            <td width="70" class="field_title" style="display:none">
                                                                <asp:Label ID="Label4" runat="server" Text="UOM"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="150" class="field_input" style="display:none">
                                                                <asp:DropDownList ID="ddlpacksize" runat="server" Width="150px" ValidationGroup="Scheduledate"
                                                                    class="chosen-select" data-placeholder="SELECT UOM">
                                                                </asp:DropDownList>
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlpacksize"
                                                                    InitialValue="0" SetFocusOnError="true" ErrorMessage="Required!" ForeColor="Red"
                                                                   ValidationGroup="Scheduledate"></asp:RequiredFieldValidator>--%>
                                                            </td>
                                                            <td class="field_titleTr">
                                                                <asp:Label ID="lblfactory" runat="server" Text="Factory"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="150">
                                                                <asp:DropDownList ID="ddlfactory" runat="server" AppendDataBoundItems="true" class="chosen-select"
                                                                    Style="width: 130px;" data-placeholder="Choose a Factory">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="Reqddlfactory" runat="server" ControlToValidate="ddlfactory"
                                                                    InitialValue="0" SetFocusOnError="true" ErrorMessage="*" ForeColor="Red" ValidationGroup="POFooter"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td style="display:none">
                                                                <div class="btn_24_blue">
                                                                    <span class="icon add_co"></span>
                                                                    <asp:Button ID="btnscheduledate" runat="server" Text="Set Date/UNIT" CssClass="btn_link"
                                                                        ValidationGroup="Scheduledate" OnClick="btnscheduledate_Click" />
                                                                </div>
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
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
                                            <div style="overflow: scroll; height: 300px;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                                                <asp:GridView ID="grdpodetailsadd" EmptyDataText="There are no records available."
                                                    CssClass="reportgrid" runat="server" OnRowDataBound="grdpodetailsadd_RowDataBound"
                                                    AutoGenerateColumns="False" onchange="MakeStaticHeader('<%= grdpodetailsadd.ClientID %>', 300, '100%' , 30 ,false)"
                                                    Width="100%">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SL">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
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
                                                        <asp:TemplateField HeaderText="CASE PACK" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblcasepack" runat="server" Text='<%# Bind("CASEPACKQTY") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MRP" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblmrp" runat="server" Text='<%# Bind("MRP") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
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

                                                        <asp:TemplateField HeaderText="STOCK QTY">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblStockQty" runat="server" Text='<%# Bind("STOCKQTY") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="PURCHASE COST(Pcs/Box)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdlblpurchasecost" runat="server" Text='<%# Bind("PURCHASECOST") %>' 
                                                                    onkeypress="return isNumberKeyWithDot(event);" Style="text-align: right;" Width="70px" Height="20" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ORDER QTY">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdtxtpoqty" AutoPostBack="false" runat="server" Enabled="true"
                                                                    Style="text-align: right;" Width="70px" Height="20" Text='<%# Bind("POQTY") %>'
                                                                    value='<%# Eval("POQTY") %>' onkeypress="return isNumberKey(event);" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="AMOUNT" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdlblbasiccostvalue" AutoPostBack="false" runat="server" Enabled="false"
                                                                    Style="text-align: right;" Width="70px" Height="20" Text='<%# Bind("BASICVALUE") %>'
                                                                    value='<%# Eval("BASICVALUE") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TOTAL MRP" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdlblmrpvalue" AutoPostBack="false" runat="server" Enabled="false"
                                                                    Style="text-align: right;" Width="70px" Height="20" Text='<%# Bind("MRPVALUE") %>'
                                                                    value='<%# Eval("MRPVALUE") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ASSESMENT(%)" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblassesmentpercent" runat="server" Text='<%# Bind("ASSESMENTPERCENTAGE") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Excise Percentage" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblexcise" runat="server" Text='<%# Bind("Excise") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CST Percentage" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblcst" runat="server" Text='<%# Bind("CST") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DEL. FROM DATE" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdtxtdeliverydatefrom" AutoPostBack="false" runat="server" MaxLength="10"
                                                                    Style="text-align: right;" Width="70px" Height="20" Enabled="false" />
                                                                <asp:ImageButton ID="imgPopuppodate4" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="imgPopuppodate4"
                                                                    runat="server" TargetControlID="grdtxtdeliverydatefrom" CssClass="cal_Theme1"
                                                                    Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DEL. TO DATE" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="grdtxtdeliverydateto" AutoPostBack="false" runat="server" MaxLength="10"
                                                                    Style="text-align: right;" Width="70px" Height="20" Enabled="false" />
                                                                <asp:ImageButton ID="imgPopuppodateto" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
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
                                                        <asp:TemplateField HeaderText="UOMID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblUOMID" runat="server" Text='<%# Bind("UOMID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="UOMNAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="grdlblUOMNAME" runat="server" Text='<%# Bind("UOMNAME") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div id="DivFooterRow" style="overflow: hidden">
                                            </div>
                                        </fieldset>
                                    </div>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td"
                                        runat="server" id="tblADD">
                                        <tr>
                                            <td style="padding-left: 13px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon add_co"></span>
                                                    <asp:Button ID="btnadd" runat="server" Text="Add" CssClass="btn_link" OnClick="btnadd_Click" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="gridcontent-inner">
                                        <fieldset>
                                            <legend>WORK ORDER DETAILS</legend>
                                            <asp:GridView ID="gvPurchaseOrder" EmptyDataText="There are no records available."
                                                CssClass="reportgrid" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                                                PageSize="200" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PRODUCTID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="grdlblproductid1" runat="server" Text='<%# Bind("PRODUCTID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PRODUCT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="grdlblproductname1" runat="server" Text='<%# Bind("PRODUCTNAME") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="STOCK QTY" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="grdlblproductStockQty" runat="server" Text='<%# Bind("STOCKQTY") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="ORDER QTY">
                                                        <ItemTemplate>
                                                            <asp:Label ID="grdlblQTY1" runat="server" Text='<%# Bind("PRODUCTQTY") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="UNIT">
                                                        <ItemTemplate>
                                                            <%-- <asp:Label ID="grdlblpacksizename1" runat="server" Text='<%# Bind("PRODUCTPACKINGSIZE") %>' />--%>
                                                            <asp:Label ID="grdlblpacksizename1" runat="server" Text='<%# Bind("UOMNAME") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="JOB ORDER COST(Pcs)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="grdlblJobOrderCost" runat="server" Text='<%# Bind("PRODUCTPRICE") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="JOB ORDER AMOUNT" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="grdlblJobOrderAmt" runat="server" Text='<%# Bind("AMOUNT") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="PURCHASE COST(Pcs)" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="grdlblpurchasecost1" runat="server" Text='<%# Bind("PRODUCTRATE") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PURCHASE AMOUNT" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="grdlblAmount1" runat="server" Text='<%# Bind("AMOUNT") %>' />
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
                                                    <asp:TemplateField HeaderText="NATUREOFPRODUCTID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="grdlblDivID5" runat="server" Text='<%# Bind("NATUREOFPRODUCTID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="NATUREOFPRODUCTNAME" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="grdlblDivID6" runat="server" Text='<%# Bind("NATUREOFPRODUCTNAME") %>' />
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
                                                            <%-- <asp:ImageButton ID="btn_TempDelete" runat="server" class="action-icons c-delete"
                                                                OnClientClick="return confirm('Are you sure you want to Delete?')" ToolTip="Delete"
                                                                OnClick="btn_TempDelete_Click" />--%>

                                                            <asp:Button ID="btn_TempDelete" runat="server" class="action-icons c-delete"
                                                                OnClientClick="return confirm('Are you sure you want to Delete?')" ToolTip="Delete"
                                                                OnClick="btn_TempDelete_Click" />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </fieldset>
                                    </div>
                                    <%--Add By Rajeev 30-07-2017--%>
                                    <div class="gridcontent-inner">
                                        <fieldset>
                                            <legend>BOM DETAILS</legend>
                                            <asp:GridView ID="GvBomDtls" EmptyDataText="There are no records available." CssClass="reportgrid"
                                                runat="server" AutoGenerateColumns="False" AllowPaging="false" PageSize="200"
                                                Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="PROCESSMATERIALID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="gvlblprocessMaterialID" runat="server" Text='<%# Bind("PROCESSMATERIALID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="PROCESSFRAMEWORKID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="gvlblprocessframeworkID" runat="server" Text='<%# Bind("PROCESSFRAMEWORKID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="PROCESSID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="gvlblprocessID" runat="server" Text='<%# Bind("PROCESSID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="PRODUCTID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="gvlblproductID" runat="server" Text='<%# Bind("PRODUCTID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PRODUCTNAME">
                                                        <ItemTemplate>
                                                            <asp:Label ID="gvlblproductname" runat="server" Text='<%# Bind("PRODUCTNAME") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CATEGORYID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="gvlblCatID" runat="server" Text='<%# Bind("CATEGORYID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CATEGORYNAME">
                                                        <ItemTemplate>
                                                            <asp:Label ID="gvlblCatname" runat="server" Text='<%# Bind("CATEGORYNAME") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="TYPE">
                                                        <ItemTemplate>
                                                            <asp:Label ID="gvlbltype" runat="server" Text='<%# Bind("TYPE") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="UNITVALUE" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="gvlblunitvalue" runat="server" Text='<%# Bind("UNITVALUE") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="UOMID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="gvlbluomid" runat="server" Text='<%# Bind("UOMID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="UNITNAME">
                                                        <ItemTemplate>
                                                            <asp:Label ID="gvlblunitname" runat="server" Text='<%# Bind("UOMNAME") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="STOCK QTY" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="GvStockQty" runat="server" Text='<%# Bind("STOCKQTY") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="QTY">
                                                        <ItemTemplate>
                                                            <asp:Label ID="gvlblqty" runat="server" Text='<%# Bind("QTY") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="WORKORDERPRODUCTID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="gvlblWORKORDERPRODUCTID" runat="server" Text='<%# Bind("WORKORDERPRODUCTID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="REFQTY">
                                                        <ItemTemplate>
                                                            <asp:Label ID="gvlblrefqty" runat="server" Text='<%# Bind("REFQTY") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                    <asp:TemplateField HeaderText="STORELOCATIONID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="gvlblSTORELOCATIONID" runat="server" Text='<%# Bind("STORELOCATIONID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="STORELOCATION NAME">
                                                        <ItemTemplate>
                                                            <asp:Label ID="gvlblSTORELOCATIONAME" runat="server" Text='<%# Bind("STORELOCATIONAME") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="DELETE" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btn_TempDelete" runat="server" class="action-icons c-delete"
                                                                OnClientClick="return confirm('Are you sure you want to Delete?')" ToolTip="Delete"
                                                                OnClick="btn_TempDelete_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </fieldset>
                                    </div>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 12px;">
                                                <fieldset>
                                                    <legend>PURCHASE ORDER AMOUNT DETAILS</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td width="130">
                                                                <asp:TextBox ID="txtgrosstotal" runat="server" MaxLength="15" CssClass="x-large"
                                                                    placeholder="Total Basic Value" Text="0" Font-Bold="true" Enabled="false" CausesValidation="true"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="CV_GrossTotal" runat="server" ControlToValidate="txtgrosstotal"
                                                                    SetFocusOnError="true" ErrorMessage="Total Basic Value is required!" Display="None"
                                                                    ValidationGroup="POFooter"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server"
                                                                    TargetControlID="CV_GrossTotal" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="~/images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label3" runat="server" Text="Total Basic Value"></asp:Label></span>
                                                            </td>
                                                            <td width="15">&nbsp;
                                                            </td>
                                                            <td width="130" style="display:none">
                                                                <asp:TextBox ID="txtTotalMRP" runat="server" MaxLength="15" CssClass="x-large" Text="0"
                                                                    ValidationGroup="POFooter" onkeypress="return isNumberKeyWithDot(event);" placeholder="Total MRP"
                                                                    Enabled="false" Font-Bold="true"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="CV_Discount" runat="server" ControlToValidate="txtTotalMRP"
                                                                    SetFocusOnError="true" ErrorMessage="required!" Display="None" ValidationGroup="POFooter"></asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                                    TargetControlID="CV_Discount" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="~/images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label16" runat="server" Text="Total MRP"></asp:Label></span>
                                                            </td>
                                                            <td width="15">&nbsp;
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
                                                                    WarningIconImageUrl="~/images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label17" runat="server" Text="Total Adjustment"></asp:Label></span>
                                                            </td>
                                                            <td width="15">&nbsp;
                                                            </td>
                                                            <td width="130">
                                                                <asp:TextBox ID="txttotalamount" runat="server" MaxLength="15" CssClass="x-large"
                                                                    placeholder="Total Gross Value" Enabled="false" Text="0" ValidationGroup="POFooter"
                                                                    Font-Bold="true"></asp:TextBox>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label13" runat="server" Text="Total Gross Value"></asp:Label></span>
                                                            </td>
                                                            <td width="15">&nbsp;
                                                            </td>
                                                            <td width="130">
                                                                <asp:TextBox ID="txtcasepack" runat="server" MaxLength="15" CssClass="x-large" placeholder="Total Case"
                                                                    Enabled="false" onkeypress="return isNumberKeyWithDot(event);" ValidationGroup="POFooter"
                                                                    Font-Bold="true"></asp:TextBox>
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label7" runat="server" Text="Total Case"></asp:Label></span>
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
                                                            <td height="8" colspan="10"></td>
                                                        </tr>
                                                    </table>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td width="320">
                                                                <asp:TextBox ID="txtremarks" runat="server" MaxLength="255" TextMode="MultiLine"
                                                                    Height="40" Width="100%" placeholder="Remarks" class="input_grow"></asp:TextBox>
                                                            </td>
                                                            <td width="15">&nbsp;
                                                            </td>
                                                            <td>
                                                                <div class="btn_24_blue" id="divbtnsave" runat="server">
                                                                    <span class="icon disk_co"></span>
                                                                    <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn_link" ValidationGroup="POFooter"
                                                                        OnClick="btnsave_Click" />
                                                                    <asp:HiddenField ID="Hdn_Fld" runat="server" />
                                                                </div>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <div class="btn_24_blue">
                                                                    <span class="icon cross_octagon_co"></span>
                                                                    <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn_link" OnClick="btncancel_Click"
                                                                        CausesValidation="false" />
                                                                </div>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <div class="btn_24_blue" style="display: none;" runat="server" id="div_btnPrint">
                                                                    <span class="icon printer_co"></span>
                                                                    <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn_link" OnClick="btnPrint_Click"
                                                                        CausesValidation="false" />
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
                                                            <asp:ImageButton ID="imgPopupfromdate" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopupfromdate"
                                                                runat="server" TargetControlID="txtfromdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="CV_FromDate" runat="server" ControlToValidate="txtfromdate"
                                                                SetFocusOnError="true" ErrorMessage="From Date is required!" Display="None" ValidationGroup="datecheck"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                                                TargetControlID="CV_FromDate" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="~/images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <asp:CustomValidator ID="CV_FromDateValidation" runat="server" ClientValidationFunction="ValidateDate"
                                                                ControlToValidate="txtfromdate" SetFocusOnError="true" ErrorMessage="* Invalid Date!"
                                                                Display="None" ValidationGroup="datecheck" />
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server"
                                                                TargetControlID="CV_FromDateValidation" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="~/images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                        <td width="70">
                                                            <asp:Label ID="Label10" runat="server" Text="To Date"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="165">
                                                            <asp:TextBox ID="txttodate" runat="server" MaxLength="10" Width="69%" placeholder="dd/mm/yyyy"
                                                                ValidationGroup="datecheck" Enabled="false"></asp:TextBox>
                                                            <asp:ImageButton ID="imgpopuptodate" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="imgpopuptodate"
                                                                runat="server" TargetControlID="txttodate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                            <asp:RequiredFieldValidator ID="CV_ToDate" runat="server" ControlToValidate="txttodate"
                                                                SetFocusOnError="true" ErrorMessage="To Date is required!" Display="None" ValidationGroup="datecheck"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server"
                                                                TargetControlID="CV_ToDate" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="~/images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                            <asp:CustomValidator ID="CV_ToDateValidation" runat="server" ClientValidationFunction="ValidateDate"
                                                                ControlToValidate="txttodate" SetFocusOnError="true" ErrorMessage="* Invalid Date!"
                                                                Display="None" ValidationGroup="datecheck" />
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server"
                                                                TargetControlID="CV_ToDateValidation" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="~/images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                        <td>
                                                            <div class="btn_24_blue">
                                                                <span class="icon find_co"></span>
                                                                <asp:Button ID="btngvfill" runat="server" Text="Search" CssClass="btn_link" ValidationGroup="datecheck"
                                                                    OnClick="btngvfill_Click" />
                                                            </div>
                                                            <asp:HiddenField ID="hdn_pofield" runat="server" />
                                                            <asp:HiddenField ID="hdn_podelete" runat="server" />
                                                            <asp:HiddenField ID="hdn_FromDate" runat="server" />
                                                            <asp:HiddenField ID="hdn_ToDate" runat="server" />
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
                                            AllowAddingRecords="false" PageSize="200" AllowPaging="true" AllowFiltering="true"
                                            AllowPageSizeSelection="true" OnDeleteCommand="DeleteRecordPoDetails" FolderStyle="../GridStyles/premiere_blue"
                                            EnableRecordHover="true">
                                            <ClientSideEvents OnBeforeClientDelete="OnBeforepodetailsDelete" OnClientDelete="OnDeletePoDetails" />
                                            <FilteringSettings InitialState="Visible" />
                                            <Columns>
                                                <cc1:Column ID="Column4" DataField="Sl" HeaderText="SL" runat="server" Width="60"
                                                    Wrap="true">
                                                    <TemplateSettings TemplateId="slnoTemplatePO" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column6" DataField="PODATE" HeaderText="ORDER DATE" runat="server"
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
                                                <cc1:Column ID="Column7" DataField="PONO" HeaderText="WORKORDER NO" runat="server"
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
                                                <cc1:Column ID="Column8" DataField="VENDORNAME" HeaderText="VENDOR" runat="server"
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
                                                <cc1:Column DataField="TOTALCASEPACK" HeaderText="TOTAL CASEPACK" Width="150" Wrap="true"
                                                    Visible="false">
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
                                                    Visible="false" Width="110" Wrap="true">
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
                                            <ScrollingSettings ScrollWidth="100%" ScrollHeight="290" />
                                        </cc1:Grid>
                                        <asp:Button ID="btngrdedit" runat="server" Text="grdedit" Style="display: none" OnClick="btngrdedit_Click"
                                            CausesValidation="false" />
                                        <asp:Button ID="btngrdview" runat="server" Text="GridView" Style="display: none"
                                            OnClick="btngrdview_Click" CausesValidation="false" />
                                        <asp:HiddenField ID="hdn_convertionqty" runat="server" />
                                        <asp:HiddenField ID="hdnCST" runat="server" />
                                        <asp:HiddenField ID="hdnExcise" runat="server" />
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
                    document.getElementById("<%=hdn_pofield.ClientID %>").value = gvpodetails.Rows[iRecordIndex].Cells[7].Value;
                    document.getElementById("<%=btngrdedit.ClientID %>").click();
                }

                function CallViewServerMethod(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridView_", "");
                    document.getElementById("<%=hdn_pofield.ClientID %>").value = gvpodetails.Rows[iRecordIndex].Cells[7].Value;
                    document.getElementById("<%=btngrdview.ClientID %>").click();
                }

                function OnBeforepodetailsDelete(record) {
                    record.Error = '';
                    document.getElementById("<%=hdn_pofield.ClientID %>").value = record.POID;
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
                function Calpurchaseorderdetails(pcsqty, purchasecost, poqty, basiccostvalue, totalmrp, mrp) {

                    var tpoqty = parseFloat(document.getElementById(poqty).value);
                    var Totalbasiccostvalue = parseFloat(pcsqty * purchasecost * tpoqty).toFixed(2);
                    if (isNaN(Totalbasiccostvalue)) {
                        Totalbasiccostvalue = 0;
                    }

                    basiccostvalue.value = Totalbasiccostvalue;
                    var alltotalmrp = parseFloat(pcsqty * tpoqty * mrp).toFixed(2);
                    if (isNaN(alltotalmrp)) {
                        alltotalmrp = 0;
                    }
                    totalmrp.value = alltotalmrp;
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
                    var popup =grdpodetailsadd $find('<%= modalPopup.ClientID %>');
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>