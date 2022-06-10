<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmAccLedgerInfo.aspx.cs" Inherits="VIEW_frmAccLedgerInfo" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="../images/loading123.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>
                                    ACCOUNT INFORMATION</h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="Btnadd" runat="server" Text="Add New Record" CssClass="btn_link"
                                        CausesValidation="false" OnClick="Btnadd_Click" /></div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>Ledger Details</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                                        <tr id="trledgerhead" runat="server">
                                                            <td width="60" class="field_title">
                                                                Name&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td width="230" class="field_input">
                                                                <asp:TextBox ID="txtName" runat="server" CssClass="full" MaxLength="200" Font-Bold="true" />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorLoanAmt" runat="server" ControlToValidate="txtName"
                                                                    CssClass="twitterStyleTextbox" Display="Dynamic" ErrorMessage="Enter Name" ForeColor="#F8F8F8"
                                                                    ValidationGroup="1">*</asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtenderLoanAmt" runat="server"
                                                                    TargetControlID="RequiredFieldValidatorLoanAmt" />
                                                            </td>
                                                            <td width="60" class="field_title">
                                                                Group&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td width="200" class="field_input">
                                                                <asp:DropDownList ID="ddlgroup" runat="server" class="chosen-select" Width="200">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlgroup"
                                                                    CssClass="dropup" Display="Dynamic" InitialValue="0" ErrorMessage="Select Group"
                                                                    ForeColor="#F8F8F8" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                                    TargetControlID="RequiredFieldValidator1" />
                                                            </td>
                                                            <td width="40" class="field_title">
                                                                Tax
                                                            </td>
                                                            <td class="field_input" width="100">
                                                                <asp:ListBox ID="ddltax" runat="server" SelectionMode="Multiple" TabIndex="1"></asp:ListBox>
                                                            </td>
                                                            <td width="80" class="field_title">
                                                                Cost Center&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" width="40">
                                                                <asp:CheckBox ID="chkcostcenter" runat="server" Text=" " />
                                                            </td>
                                                            <td width="80" class="field_title">
                                                                Negative Bal.&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" width="40">
                                                                <asp:CheckBox ID="chknegativebal" runat="server" Text=" " />
                                                            </td>
                                                        </tr>
                                                        <tr id="trRdBtn" runat="server">
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td class="field_input" colspan="7">
                                                                <cc1:Grid ID="Grdboard" runat="server" CallbackMode="true" AllowAddingRecords="false"
                                                                    AllowSorting="false" EnableRecordHover="true" AutoGenerateColumns="false" PageSize="500"
                                                                    AllowPageSizeSelection="false" AllowPaging="false" AllowFiltering="false" Serialize="true"
                                                                    FolderStyle="../GridStyles/premiere_blue" OnRowDataBound="Grdboard_RowDataBound">
                                                                    <Columns>
                                                                        <cc1:Column ID="Column11" DataField="REGIONID" ReadOnly="true" HeaderText="REGIONID"
                                                                            runat="server" Visible="false">
                                                                            <%--<TemplateSettings TemplateId="brcode" />--%>
                                                                        </cc1:Column>
                                                                        <cc1:Column ID="Column7" DataField="Sl" HeaderText="SL" runat="server" Width="60">
                                                                            <TemplateSettings TemplateId="slnoTemplate1" />
                                                                        </cc1:Column>
                                                                        <cc1:Column ID="Column8" DataField="REGIONNAME" HeaderText="REGION" runat="server"
                                                                            Width="310" Wrap="true">
                                                                        </cc1:Column>
                                                                        <cc1:Column ID="Column12" DataField="" HeaderText="DEBIT AMOUNT" runat="server" AllowEdit="true"
                                                                            Wrap="true">
                                                                            <TemplateSettings TemplateId="Debitamount" />
                                                                        </cc1:Column>
                                                                        <cc1:Column ID="Column36" DataField="" HeaderText="CREDIT AMOUNT" runat="server"
                                                                            AllowEdit="true" Wrap="true">
                                                                            <TemplateSettings TemplateId="Creditamount" />
                                                                        </cc1:Column>
                                                                    </Columns>
                                                                    <Templates>
                                                                        <%--<cc1:GridTemplate runat="server" ID="brcode">
                                                            <Template>
                                                                <asp:Label ID="lblBrId" runat="server" Text='<%# Container.DataItem["BRID"] %>' Value='<%# Container.DataItem["BRID"] %>'></asp:Label>
                                                            </Template>
                                                        </cc1:GridTemplate>--%>
                                                                        <cc1:GridTemplate runat="server" ID="slnoTemplate1">
                                                                            <Template>
                                                                                <asp:Label ID="lblslno" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                                            </Template>
                                                                        </cc1:GridTemplate>
                                                                        <cc1:GridTemplate runat="server" ID="Debitamount">
                                                                            <Template>
                                                                                <asp:TextBox runat="server" ID="txtDrAmount" Text='<%# Container.DataItem["DEBITAMOUNT"] %>'
                                                                                    MaxLength="20" Enabled="true" Width="100" onkeypress="return isNumberKeyWithDot(event);" />
                                                                            </Template>
                                                                        </cc1:GridTemplate>
                                                                        <cc1:GridTemplate runat="server" ID="Creditamount">
                                                                            <Template>
                                                                                <asp:TextBox runat="server" ID="txtCrAmount" Text='<%# Container.DataItem["CREDITAMOUNT"] %>'
                                                                                    MaxLength="20" Enabled="true" Width="100" onkeypress="return isNumberKeyWithDot(event);" />
                                                                            </Template>
                                                                        </cc1:GridTemplate>
                                                                    </Templates>
                                                                    <ScrollingSettings ScrollHeight="220" />
                                                                </cc1:Grid>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>OutStanding Invoice Details</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                                        <tr>
                                                            <td width="300" class="field_input">
                                                                <asp:DropDownList ID="ddlbranch" runat="server" class="chosen-select" Width="290"
                                                                    ValidationGroup="ADD">
                                                                </asp:DropDownList>
                                                                <span class="label_intro">BRANCH
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="ddlbranch"
                                                                        ErrorMessage="Required!" ForeColor="Red" InitialValue="0" ValidationGroup="ADD"></asp:RequiredFieldValidator></span>
                                                            </td>
                                                            <td class="field_input" width="100">
                                                                <asp:DropDownList ID="ddlvouchertype" runat="server" class="chosen-select" Width="100"
                                                                    ValidationGroup="ADD">
                                                                </asp:DropDownList>
                                                                <span class="label_intro">VOUCHER TYPE
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlvouchertype"
                                                                        InitialValue="0" ErrorMessage="*" ForeColor="Red" ValidationGroup="ADD"></asp:RequiredFieldValidator></span>
                                                            </td>
                                                            <td width="150" class="field_input">
                                                                <asp:TextBox ID="txtinvoice" runat="server" MaxLength="50" Font-Bold="true" ValidationGroup="ADD" />
                                                                <span class="label_intro">INVOICE NO.
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtinvoice"
                                                                        ErrorMessage="Required!" ForeColor="Red" ValidationGroup="ADD"></asp:RequiredFieldValidator></span>
                                                            </td>
                                                            <td class="field_input" width="100">
                                                                <asp:TextBox ID="txtdate" runat="server" Enabled="false" Width="60" placeholder="dd/MM/yyyy"
                                                                    MaxLength="10"></asp:TextBox>
                                                                <asp:ImageButton ID="Imgfrom" runat="server" ImageUrl="~/images/calendar.png" Width="24px"
                                                                    Height="24px" ImageAlign="AbsMiddle" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="Imgfrom" runat="server"
                                                                    TargetControlID="txtdate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                                <span class="label_intro">DATE</span>
                                                            </td>
                                                            <td class="field_input" width="70">
                                                                <asp:TextBox ID="txtamount" runat="server" Width="70" onkeypress="return isNumberKeyWithDot(event);"
                                                                    ValidationGroup="ADD"> </asp:TextBox>
                                                                <span class="label_intro">AMOUNT
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="txtamount"
                                                                        ErrorMessage="*" ForeColor="Red" ValidationGroup="ADD"></asp:RequiredFieldValidator></span>
                                                            </td>
                                                            <td class="field_input" valign="top">
                                                                <div class="btn_24_blue">
                                                                    <span class="icon add_co"></span>
                                                                    <asp:Button ID="btnADDGrid" runat="server" CssClass="btn_link" Text="Add" ValidationGroup="ADD"
                                                                        OnClick="btnADDGrid_Click" />
                                                                </div>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_input" colspan="7">
                                                                <div class="gridcontent-inner">
                                                                    <div style="overflow: hidden; width: 80%;" id="DivHeaderRowOut">
                                                                    </div>
                                                                    <div style="overflow: scroll; margin-bottom: 8px;" onscroll="OnScrollDivOut(this)"
                                                                        id="DivMainContentOut">
                                                                        <asp:GridView ID="gvadd" runat="server" Width="100%" ShowFooter="true" AutoGenerateColumns="false"
                                                                            CssClass="zebra">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="SL" ItemStyle-Wrap="false" ItemStyle-Width="10px"
                                                                                    HeaderStyle-ForeColor="White">
                                                                                    <ItemTemplate>
                                                                                        <nav style="position: relative;"><span class="badge black"><%# Container.DataItemIndex + 1 %></span></nav>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="AccEntryID" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl1" runat="server" Text='<%# Bind("AccEntryID") %>' value='<%# Eval("AccEntryID") %>'
                                                                                            Visible="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="BranchID" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl2" runat="server" Text='<%# Bind("BranchID") %>' value='<%# Eval("BranchID") %>'
                                                                                            Visible="false" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="DEPOT" HeaderStyle-ForeColor="White">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl3" runat="server" Text='<%# Bind("BranchName") %>' value='<%# Eval("BranchName") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="INVOICE NO" HeaderStyle-Wrap="true" ItemStyle-Wrap="true"
                                                                                    HeaderStyle-ForeColor="White">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblpacksizeid" runat="server" Text='<%# Bind("InvoiceNo") %>' value='<%# Eval("InvoiceNo") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="INVOICE DATE" HeaderStyle-Wrap="true" ItemStyle-Wrap="true"
                                                                                    HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl13" runat="server" Text='<%# Bind("InvoiceDate") %>' value='<%# Eval("InvoiceDate") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="INVOICE AMOUNT" HeaderStyle-Wrap="true" ItemStyle-Wrap="true"
                                                                                    HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl14" runat="server" Text='<%# Bind("InvoiceAmt") %>' value='<%# Eval("InvoiceAmt") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="VoucherTypeID" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl15" runat="server" Text='<%# Bind("VoucherTypeID") %>' value='<%# Eval("VoucherTypeID") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="VOUCHER TYPE" ItemStyle-Width="100px" HeaderStyle-ForeColor="White">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl125" runat="server" Text='<%# Bind("VoucherTypeName") %>' value='<%# Eval("VoucherTypeName") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="DELETE" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px"
                                                                                    HeaderStyle-ForeColor="White">
                                                                                    <ItemTemplate>
                                                                                        <asp:Button ID="btn_TempDelete" runat="server" CssClass="action-icons c-delete" OnClientClick="return confirm('Are you sure you want to Delete?')"
                                                                                            ToolTip="Delete" OnClick="btn_TempDelete_Click" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                    <div id="DivFooterRowOut" style="overflow: hidden">
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" style="padding-top: 8px; padding-left: 10px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="1" CssClass="btn_link"
                                                        OnClick="btnSave_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn_link" OnClick="btnClose_Click" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlledgerDepotMapping" runat="server">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                        <tr>
                                            <td class="field_title" width="120">
                                                <asp:Label ID="Label28" Text="LEDGER NAME" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtledgernameMapping" runat="server" Width="400" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>DEPOT MAPPING</legend>
                                                    <div style="overflow: hidden;" id="DivHeaderRow">
                                                    </div>
                                                    <div id="DivMainContent" style="overflow: scroll; height: 300px; width: 50%;" onscroll="OnScrollDiv(this);">
                                                        <asp:GridView ID="gvdepot" runat="server" Width="100%" CssClass="zebra" AutoGenerateColumns="false"
                                                            EmptyDataText="No Records Available">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="BRID" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBRID" runat="server" Text='<%# Bind("BRID") %>' value='<%# Eval("BRID") %>'
                                                                            Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="REGION" DataField="BRNAME" ItemStyle-Width="280px" />
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkAllSelect" runat="server" onclick="CheckAllheader(this);" Text=" " />
                                                                    </HeaderTemplate>
                                                                    <HeaderStyle Width="10px" />
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelect" runat="server" Text=" " Style="padding-left: 50px;" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                    <div id="DivFooterRow" style="overflow: hidden;">
                                                    </div>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="padding: 8px 10px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnDepotSubmit" runat="server" Text="Save" CssClass="btn_link" CausesValidation="false"
                                                        OnClick="btnDepotSubmit_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnDepotCancel" runat="server" Text="Cancel" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btnDepotCancel_Click" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlDisplay" runat="server">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                        <tr bgcolor="#66CCFF">
                                            <td class="field_input">
                                                <asp:RadioButton ID="rdGridView" runat="server" Text="GridView" GroupName="View"
                                                    Checked="true" AutoPostBack="True" OnCheckedChanged="rdGridView_CheckedChanged"
                                                    Width="20%" Font-Bold="True" />
                                                <asp:RadioButton ID="rdTreeView" runat="server" Text="TreeView" GroupName="View"
                                                    AutoPostBack="True" OnCheckedChanged="rdTreeView_CheckedChanged" Font-Bold="True" />
                                            </td>
                                        </tr>
                                        <tr id="trTree" runat="server">
                                            <td align="left" class="field_input">
                                                <div id="TreeDiv" style="height: 450px; overflow: scroll;">
                                                    <asp:TreeView ID="TrvGroup" runat="server" SkinID="SampleTreeView" ExpandDepth="10"
                                                        ForeColor="Blue" ImageSet="News" NodeIndent="50" ShowLines="True" ToolTip="Group Details"
                                                        HoverNodeStyle-BackColor="ButtonShadow" HoverNodeStyle-ForeColor="ControlLight"
                                                        HorizontalPadding="8px">
                                                        <HoverNodeStyle Font-Underline="false" ForeColor="Black" />
                                                        <NodeStyle Font-Names="Arial, Sans-Serif" Font-Size="13px" NodeSpacing="2px" VerticalPadding="0px"
                                                            ImageUrl="../images/img-raty/card.jpeg" />
                                                        <RootNodeStyle ForeColor="Green" Font-Bold="true" ImageUrl="../images/img-raty/icon-generalLedger.png"
                                                            HorizontalPadding="8px" />
                                                        <ParentNodeStyle Font-Bold="true" ForeColor="Green" ImageUrl="../images/img-raty/card.jpeg"
                                                            HorizontalPadding="8px" />
                                                        <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px" />
                                                        <LeafNodeStyle HorizontalPadding="8px" ImageUrl="../images/img-raty/Article_IconNew.gif" />
                                                    </asp:TreeView>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="trGrid" runat="server">
                                        <ul id="search_box">
                                            <li>
                                                <input name="" id="FilterTextBox" type="text" class="search_input" placeholder="Enter text to search the whole grid"
                                                    onkeyup="FilterTextBox_KeyUp();">
                                            </li>
                                            <li>
                                                <input name="" id="OboutButton2" type="submit" value="" class="search_btn" onclick="return performSearch();">
                                            </li>
                                            <li>&nbsp;&nbsp;&nbsp;&nbsp;</li>
                                            <li>
                                                <div class="btn_24_blue">
                                                    <span class="icon doc_excel_table_co"></span><a href="#" onclick="exportToExcel();">
                                                        <asp:Button ID="btnExport" runat="server" Text="Export Excel" CssClass="btn_link" /></a>
                                                </div>
                                            </li>
                                        </ul>
                                        <div class="gridcontent">
                                            <cc1:Grid ID="grdView" runat="server" CallbackMode="true" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                                AutoGenerateColumns="false" EnableRecordHover="true" AllowAddingRecords="false"
                                                OnExported="grdView_Exported" OnExporting="grdView_Exporting" AllowPaging="true"
                                                AllowFiltering="true" AllowMultiRecordSelection="false" AllowPageSizeSelection="true"
                                                PageSize="500">
                                                <ExportingSettings ExportColumnsFooter="true" ExportGroupFooter="true" ExportGroupHeader="true"
                                                    AppendTimeStamp="false" FileName="LedgerDetails" ExportAllPages="true" ExportDetails="true" />
                                                <FilteringSettings InitialState="Visible" />
                                                <Columns>
                                                    <cc1:Column ID="Column1" DataField="Id" ReadOnly="true" HeaderText="ID" runat="server"
                                                        Visible="false" />
                                                    <cc1:Column ID="Column5" DataField="Sl" HeaderText="SL" runat="server" Width="10%">
                                                        <TemplateSettings TemplateId="slnoTemplate" />
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column2" DataField="name" HeaderText="LEDGER" runat="server" Width="40%"
                                                        Wrap="true">
                                                        <FilterOptions>
                                                            <cc1:FilterOption Type="NoFilter" />
                                                            <cc1:FilterOption Type="Contains" />
                                                        </FilterOptions>
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column6" DataField="grpName" HeaderText="GROUP" runat="server" Wrap="true"
                                                        Width="30%">
                                                        <FilterOptions>
                                                            <cc1:FilterOption Type="NoFilter" />
                                                            <cc1:FilterOption Type="Contains" />
                                                        </FilterOptions>
                                                    </cc1:Column>
                                                    <cc1:Column DataField="REGION" HeaderText="MAPPED REGION" runat="server" Wrap="true"
                                                        Width="65%">
                                                        <FilterOptions>
                                                            <cc1:FilterOption Type="NoFilter" />
                                                            <cc1:FilterOption Type="Contains" />
                                                        </FilterOptions>
                                                    </cc1:Column>
                                                    <cc1:Column ID="Column3" DataField="costcenter" HeaderText="COSTCENTER APLICABLE"
                                                        Width="110" Align="center" runat="server" Wrap="true">
                                                        <FilterOptions>
                                                            <cc1:FilterOption Type="NoFilter" />
                                                            <cc1:FilterOption Type="Contains" />
                                                        </FilterOptions>
                                                    </cc1:Column>
                                                    <cc1:Column HeaderText="REGION MAPPING" AllowEdit="false" AllowDelete="true" Width="70"
                                                        Align="center" Wrap="true">
                                                        <TemplateSettings TemplateId="LedgerDepotMapping" />
                                                    </cc1:Column>
                                                    <cc1:Column HeaderText="EDIT" AllowEdit="true" AllowDelete="false" Width="50" Wrap="true"
                                                        Align="center">
                                                        <TemplateSettings TemplateId="editaccount" />
                                                    </cc1:Column>
                                                    <cc1:Column HeaderText="DELETE" AllowEdit="true" AllowDelete="false" Width="70" Align="center"
                                                        Wrap="true">
                                                        <TemplateSettings TemplateId="deleteaccount" />
                                                    </cc1:Column>
                                                </Columns>
                                                <FilteringSettings MatchingType="AnyFilter" />
                                                <Templates>
                                                    <cc1:GridTemplate runat="server" ID="slnoTemplate">
                                                        <Template>
                                                            <nav style="position: relative;"><span class="badge black"><asp:Label ID="lblslno" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label></span></nav>
                                                        </Template>
                                                    </cc1:GridTemplate>
                                                    <cc1:GridTemplate runat="server" ID="editaccount">
                                                        <Template>
                                                            <a href="javascript: //" class="action-icons c-edit" id="btnGridEdit_<%# Container.PageRecordIndex %>"
                                                                onclick="CallServerMethod(this)"></a>
                                                        </Template>
                                                    </cc1:GridTemplate>
                                                    <cc1:GridTemplate runat="server" ID="deleteaccount">
                                                        <Template>
                                                            <a href="javascript: //" class="action-icons c-delete" id="btnGridDelete_<%# Container.PageRecordIndex %>"
                                                                onclick="CallDeleteServerMethod(this)"></a>
                                                        </Template>
                                                    </cc1:GridTemplate>
                                                    <cc1:GridTemplate runat="server" ID="LedgerDepotMapping">
                                                        <Template>
                                                            <a href="javascript: //" class="filter_btn switch_co" title="Ledger Depot Mapping "
                                                                onclick="OpenLedgerDepotMapping('<%# Container.DataItem["name"] %>','<%# Container.DataItem["Id"] %>')" />
                                                        </Template>
                                                    </cc1:GridTemplate>
                                                </Templates>
                                                <ScrollingSettings ScrollWidth="100%" ScrollHeight="290" />
                                            </cc1:Grid>
                                            <asp:Button ID="btn_Edit" runat="server" Text="Edit" Style="display: none" CausesValidation="false"
                                                OnClick="btn_Edit_Click" />
                                            <asp:Button ID="btn_Delete" runat="server" Text="Delete" Style="display: none" CausesValidation="false"
                                                OnClick="btn_Delete_Click" OnClientClick="return confirm('Are you sure you want to Delete?')" />
                                            <asp:Button ID="btnDepotMapping" runat="server" Text="Depot Mapping" Style="display: none"
                                                OnClick="btnDepotMapping_Click" CausesValidation="false" />
                                            <asp:HiddenField ID="hdn_accid" runat="server" />
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <cc1:MessageBox ID="MessageBox1" runat="server" />
                    <span class="clear"></span>
                </div>
                <span class="clear"></span>
                <script type="text/javascript">
                    function exportToExcel() {
                        grdView.exportToExcel();
                    }         
                </script>
                <script type="text/javascript">

                    function CallServerMethod(oLink) {
                        var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                        document.getElementById("<%=hdn_accid.ClientID %>").value = grdView.Rows[iRecordIndex].Cells[0].Value;
                        document.getElementById("<%=btn_Edit.ClientID %>").click();

                    }

                    function CallDeleteServerMethod(oLink) {
                        var iRecordIndex = oLink.id.toString().replace("btnGridDelete_", "");
                        document.getElementById("<%=hdn_accid.ClientID %>").value = grdView.Rows[iRecordIndex].Cells[0].Value;
                        document.getElementById("<%=btn_Delete.ClientID %>").click();

                    }

                    function OpenLedgerDepotMapping(name, id) {
                        document.getElementById("<%=hdn_accid.ClientID %>").value = id;
                        document.getElementById("<%=txtledgernameMapping.ClientID %>").value = name;
                        document.getElementById("<%=btnDepotMapping.ClientID %>").click();
                    }

                    function isNumberKeyWithDot(evt) {
                        var charCode = (evt.which) ? evt.which : event.keyCode;
                        if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 46))
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

                        grdView.addFilterCriteria('grpName', OboutGridFilterCriteria.Contains, searchValue);
                        grdView.addFilterCriteria('name', OboutGridFilterCriteria.Contains, searchValue);

                        grdView.executeFilter();
                        searchTimeout = null;


                        return false;
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
                    $(function () {
                        $('#ContentPlaceHolder1_ddltax').multiselect({
                            includeSelectAllOption: true
                        });
                    });
                </script>
                <script type="text/javascript" language="javascript">
                    function CheckAllheader(Checkbox) {
                        var GridVwHeaderCheckbox = document.getElementById("<%=gvdepot.ClientID %>");
                        for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                            GridVwHeaderCheckbox.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                        }
                    }
                </script>
                <script language="javascript" type="text/javascript">
                    function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
                        debugger;
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
                <script language="javascript" type="text/javascript">
                    function MakeStaticHeaderOutStanding(gridId, height, width, headerHeight, isFooter) {
                        debugger;
                        var tbl = document.getElementById(gridId);
                        if (tbl) {
                            var DivHR = document.getElementById('DivHeaderRowOut');
                            var DivMC = document.getElementById('DivMainContentOut');
                            var DivFR = document.getElementById('DivFooterRowOut');


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
                        document.getElementById('DivHeaderRowOut').scrollLeft = Scrollablediv.scrollLeft;
                        document.getElementById('DivFooterRowOut').scrollLeft = Scrollablediv.scrollLeft;
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
                <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
                <script type="text/javascript" src="../js/bootstrap.js"></script>
                <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>