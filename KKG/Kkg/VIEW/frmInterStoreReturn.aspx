<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmInterStoreReturn.aspx.cs" Inherits="VIEW_frmInterStoreReturn" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_Net" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                <div id="contentarea">
                    <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>Stock Journal Details</h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAdd" runat="server" Text="Add New Record" CssClass="btn_link"
                                        CausesValidation="false" OnClick="btnAdd_Click"  />
                                </div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px">
                                                <fieldset>
                                                    <legend>Product Details</legend>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr id="tradjunmentheader" runat="server">
                                                            <td class="field_title" width="70">
                                                                <asp:Label ID="lblqualitycontrol" runat="server" Text="Journal No"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtadjustmentno" runat="server" placeholder="Auto Generate Return No"
                                                                    Enabled="false" Style="width: 230px;"></asp:TextBox><br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="70" class="field_title" style="padding-bottom: 24px;">
                                                                <asp:Label ID="lblDeptName" Text="FACTORY" runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td width="240" class="field_input" style="padding-bottom: 24px;">
                                                                <asp:DropDownList ID="ddlDeptName" Width="230" runat="server" class="chosen-select"
                                                                    data-placeholder="Choose Dept Name" AppendDataBoundItems="True" ValidationGroup="ADD">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfv_ddlDeptName" runat="server" ControlToValidate="ddlDeptName"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="ADD"
                                                                    ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td width="110" class="field_input">
                                                                <asp:TextBox ID="txtReturnDate" runat="server" Width="65" placeholder="dd/mm/yyyy"
                                                                    MaxLength="10" Enabled="false"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/calendar.png"
                                                                    Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtReturnDate"
                                                                    PopupButtonID="ImageButton1" runat="server" Format="dd/MM/yyyy" Enabled="true"
                                                                    BehaviorID="CalendarExtender1" CssClass="cal_Theme1" />
                                                                <span class="label_intro">
                                                                    <asp:Label ID="Label3" runat="server" Text="RETURN DATE"></asp:Label></span>
                                                            </td>
                                                           
                                                            <td>&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                              <td class="field_title">
                                                                <asp:Label ID="lblfromstorelocation" Text="From Loaction" runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlfromstorelocation" runat="server" AppendDataBoundItems="true"
                                                                    Width="170" class="chosen-select" ValidationGroup="ADD" data-placeholder="From Store Loacation">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                                                    ValidationGroup="ADD" Font-Bold="true" ForeColor="Red" ControlToValidate="ddlfromstorelocation"
                                                                    SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                            </td>
                                                            <td class="field_title" width="70">
                                                                <asp:Label ID="lblProduct" Text="Product" runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input" colspan="2">
                                                                <asp:DropDownList ID="ddlProduct" Width="350" class="chosen-select" runat="server"
                                                                    ValidationGroup="ADD" AutoPostBack="true" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfv_ddlProduct" runat="server" ControlToValidate="ddlProduct"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="ADD"
                                                                    ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="lblPackingSize" runat="server" Text="UOM"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlPackingSize" Width="150" runat="server" class="chosen-select"
                                                                    ValidationGroup="ADD">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfv_ddlPackingSize" runat="server" ControlToValidate="ddlPackingSize"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="ADD"
                                                                    ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label7" Text="To Loaction" runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlToLocation" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                                                    Width="170" class="chosen-select" ValidationGroup="ADD" data-placeholder="To Store Loacation" OnSelectedIndexChanged="ddlToLocation_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                                                    ValidationGroup="ADD" Font-Bold="true" ForeColor="Red" ControlToValidate="ddlTolocation"
                                                                    SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title" colspan="7">
                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td valign="top" style="padding-top: 11px;" width="70">
                                                                            <asp:Label ID="lblStock" Text="DETAILS" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                                        </td>
                                                                        <td valign="top" width="80">
                                                                            <asp:TextBox ID="txtStockqty" runat="server" Width="70" placeholder="JOURNAL QTY"
                                                                                ValidationGroup="ADD" Text="0" MaxLength="10" onkeypress="return isNumberKeyWithDotMinus(event);"> </asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="rfv_txtStockqty" runat="server" Display="None" ErrorMessage="Return qty is required!"
                                                                                ControlToValidate="txtStockqty" ValidateEmptyText="false" SetFocusOnError="true "
                                                                                ValidationGroup="ADD"> </asp:RequiredFieldValidator>
                                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                                                TargetControlID="rfv_txtStockqty" PopupPosition="BottomLeft" HighlightCssClass="errormassage"
                                                                                WarningIconImageUrl="~/images/050.png">
                                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                                            <span class="label_intro">
                                                                                <asp:Label ID="Label1" runat="server" Text="Return Qty"></asp:Label></span>
                                                                        </td>

                                                                         <td valign="top" width="160">
                                                                            <asp:TextBox ID="txtTotockInHand" runat="server" Width="70" MaxLength="10" Enabled="false"
                                                                                placeholder="STOCKINHAND" Text="0"></asp:TextBox>
                                                                            <span class="label_intro">
                                                                                <asp:Label ID="lblToStockInHand" runat="server" Text="From location Stock In Hand" ForeColor="Blue"></asp:Label>
                                                                            </span>
                                                                        </td>
                                                                        <td valign="top" width="160">
                                                                            <asp:TextBox ID="txtstockinhand" runat="server" Width="70" MaxLength="10" Enabled="false"
                                                                                placeholder="STOCKINHAND" Text="0"></asp:TextBox>
                                                                            <span class="label_intro">
                                                                                <asp:Label ID="lblstockinhand" runat="server" Text="To location Stock In Hand" ForeColor="Blue"></asp:Label>
                                                                            </span>
                                                                        </td>
                                                                       
                                                                        <td valign="top">
                                                                            <%--ValidationGroup="ADD"--%>
                                                                            <asp:DropDownList ID="ddlreason" runat="server" AppendDataBoundItems="true" Style="width: 200px;"
                                                                                class="chosen-select">
                                                                            </asp:DropDownList>
                                                                            <span class="label_intro">
                                                                                <asp:Label ID="Label8" runat="server" Text="REASON"></asp:Label></span>
                                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*Required"
                                                                                Font-Bold="true" ForeColor="Red" ControlToValidate="ddlreason" ValidateEmptyText="false"
                                                                                ValidationGroup="ADD" SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>--%>
                                                                        </td>
                                                                        <td valign="top" style="padding-right: 8px;">
                                                                            <div class="btn_24_blue" ID="divadd" runat="server">
                                                                                <span class="icon blue_block"></span>
                                                                                <asp:Button ID="btnAddStock" runat="server" Text="ADD" CssClass="btn_link" ValidationGroup="ADD"
                                                                                    OnClick="btnAddStock_Click" />
                                                                                <asp:HiddenField ID="hdn_adjustmentid" runat="server" />
                                                                                <asp:HiddenField ID="hdn_mfgdate" runat="server" />
                                                                                <asp:HiddenField ID="hdn_exprdate" runat="server" />
                                                                            </div>
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
                                    <div class="gridcontent-inner">
                                        <fieldset>
                                            <legend>Jornal Stock Details</legend>
                                            <asp:GridView ID="grdReturn" runat="server" AutoGenerateColumns="false" CssClass="trialbalancegrid">
                                                <Columns>
                                                        <asp:TemplateField HeaderText="GUID" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblGUID" runat="server" Text='<%# Bind("GUID") %>'
                                                                                value='<%# Eval("GUID") %>' Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="PRODUCTID" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPRODUCTID" runat="server" Text='<%# Bind("PRODUCTID") %>'
                                                                                value='<%# Eval("PRODUCTID") %>' Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="PRODUCT NAME" DataField="PRODUCTNAME" ItemStyle-Width="350px" />
                                                     <asp:BoundField HeaderText="BATCHNO" DataField="BATCHNO" Visible="false" />
                                                    <asp:BoundField HeaderText="" DataField="PACKINGSIZEID" Visible="false"/>
                                                    <asp:BoundField HeaderText="UOM" DataField="PACKINGSIZENAME" ItemStyle-Width="30px" />
                                                    <asp:BoundField HeaderText="ADJUSTMENTQTY" DataField="ADJUSTMENTQTY" ItemStyle-Width="30px" />
                                                    <asp:BoundField HeaderText="" DataField="PRICE" Visible="false"/>
                                                    <asp:BoundField HeaderText="" DataField="AMOUNT" Visible="false" />
                                                    <asp:BoundField HeaderText="" DataField="REASONID" Visible="false" />
                                                     <asp:BoundField HeaderText="REASON NAME" DataField="REASONNAME" ItemStyle-Width="250px" />
                                                     <asp:BoundField HeaderText="" DataField="STORELOCATIONID" Visible="false" />
                                                     <asp:BoundField HeaderText="STORELOCATION NAME" DataField="STORELOCATIONNAME" ItemStyle-Width="250px" />
                                                      <asp:TemplateField ItemStyle-Width="5" ItemStyle-HorizontalAlign="Center" HeaderText="Delete">
                                                           <ItemTemplate>
                                                              <asp:Button ID="btndebitgrddelete" runat="server" Text="" OnClick="btngriddelete_Click"
                                                               ToolTip="Ledger Delete" CssClass="action-icons c-delete" CausesValidation="false" 
                                                               OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                                            </ItemTemplate>
                                                     </asp:TemplateField>
                                                    
                                                </Columns>

                                            </asp:GridView>
                                            <asp:HiddenField ID="Hdn_Fld" runat="server" />

                                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                                <tr>
                                                    <td class="field_title" width="80">
                                                        <asp:Label ID="Label9" runat="server" Text="TOT.QTY(+ve)"></asp:Label>
                                                    </td>
                                                    <td class="field_input" width="60">
                                                        <asp:TextBox ID="txtPosPCS" runat="server" Width="60" Enabled="false" MaxLength="255"> </asp:TextBox>
                                                    </td>
                                                    <td class="field_title" width="80">
                                                        <asp:Label ID="Label10" runat="server" Text="TOT.QTY(-ve)"></asp:Label>
                                                    </td>
                                                    <td class="field_input" width="60">
                                                        <asp:TextBox ID="txtNegPCS" runat="server" Width="60" Enabled="false" MaxLength="255"> </asp:TextBox>
                                                    </td>
                                                    <td>&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </div>
                                    <div class="gridcontent-inner">
                                        <fieldset>
                                            <legend>Remarks & Save</legend>
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                                <tr>
                                                    <td class="field_title" width="70" style="padding-left: 10px">Remarks
                                                    </td>
                                                    <td class="field_input" width="26%">
                                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="mid" TextMode="MultiLine" Style="width: 90%; height: 80px"
                                                            MaxLength="255"> </asp:TextBox>
                                                    </td>
                                                    <td class="field_input">
                                                        <div class="btn_24_blue" id="divbtnSubmit" runat="server">
                                                            <span class="icon disk_co"></span>
                                                            <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn_link" ValidationGroup="Save"
                                                                OnClick="btnSubmit_Click"/>
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
                                                                 CausesValidation="false" OnClick="btnApprove_Click"/>
                                                         </div>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_red" id="divbtnreject" runat="server">
                                                    <span class="icon reject_co"></span>
                                                    <asp:LinkButton ID="btnReject" runat="server" Text="Reject" CssClass="btn_link" CausesValidation="false"
                                                        OnClick="btnReject_Click" />
                                                </div>


                                                        <asp:HiddenField runat="server" ID="hdnDepotID" />
                                                        <asp:HiddenField runat="server" ID="hdnbrandid" />
                                                        <asp:HiddenField runat="server" ID="hdncatagoryid" />
                                                        <asp:HiddenField runat="server" ID="hdnlocationid" />
                                                        <asp:HiddenField runat="server" ID="Hdn_Print" />
                                                        <asp:HiddenField runat="server" ID="hdn_statusId" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </div>
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
                                                        <td width="110">
                                                            <asp:TextBox ID="txtfromdateins" runat="server" MaxLength="10" Width="70" Enabled="false"
                                                                placeholder="dd/mm/yyyy" ValidationGroup="datecheckins" Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="imgPopup" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="Calendar1" PopupButtonID="imgPopup" runat="server"
                                                                TargetControlID="txtfromdateins" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td width="70">
                                                            <asp:Label ID="Label18" runat="server" Text="To Date"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="110">
                                                            <asp:TextBox ID="txttodateins" runat="server" MaxLength="10" Width="70" Enabled="false"
                                                                placeholder="dd/mm/yyyy" ValidationGroup="datecheckins" Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="ImageButton2" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButton2"
                                                                runat="server" TargetControlID="txttodateins" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td>
                                                            <div class="btn_24_blue">
                                                                <span class="icon find_co"></span>
                                                                <asp:Button ID="btnSTfind" runat="server" Text="Search" CssClass="btn_link" ValidationGroup="datecheckins"
                                                                    OnClick="btnSTfind_Click" />
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
                                  

                                    <div class="gridcontent" style="padding-left: 8px;">
                                        <asp:GridView id="gvOpenStock" runat="server" CssClass="reportgrid" AutoGenerateColumns="false"
                                            EmptyDataText="There are no debit records available." Width="100%">
                                           <Columns>
                                                 <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5">
                                                <ItemTemplate>
                                                  <nav style="position: relative; text-align: left;"><span class="badge black"><%# Container.DataItemIndex + 1 %></span></nav>
                                             </ItemTemplate>
                                              </asp:TemplateField>
                                               <asp:TemplateField HeaderText="ADJUSTMENTID" Visible="false">
                                                     <ItemTemplate>
                                                        <asp:Label ID="lbldrguid" runat="server" Text='<%# Bind("ADJUSTMENTID") %>' value='<%# Eval("ADJUSTMENTID") %>' />
                                                 </ItemTemplate>
                                               </asp:TemplateField>
                                               <asp:BoundField DataField="ADJUSTMENTDATE" HeaderText="DATE" />
                                               <asp:BoundField DataField="ADJUSTMENTNO" HeaderText="RETURN NO" />
                                               <asp:BoundField DataField="ADJUSTMENT_FROMMENU" HeaderText="TYPE" />
                                               <asp:BoundField DataField="USERNAME" HeaderText="ENTRY USER" />
                                               
                                                <asp:TemplateField HeaderText="STATUS" Visible="true">
                                                     <ItemTemplate>
                                                        <asp:Label ID="lblSTATUS" runat="server" Text='<%# Bind("ISVERIFIEDDESC") %>' value='<%# Eval("ISVERIFIEDDESC") %>' />
                                                 </ItemTemplate>
                                               </asp:TemplateField>
                                               <asp:TemplateField ItemStyle-Width="5" ItemStyle-HorizontalAlign="Center"  HeaderText="Edit">
                                                        <ItemTemplate>
                                                          <asp:Button ID="btngridedit" runat="server" Text="" OnClick="btngridedit_Click"
                                                                  ToolTip="Edit" CssClass="action-icons c-edit" CausesValidation="false" AccessKey="H"
                                                                   />
                                                          </ItemTemplate>
                                                        </asp:TemplateField>
                                               <asp:TemplateField ItemStyle-Width="5" ItemStyle-HorizontalAlign="Center" HeaderText="Print">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnPrint" runat="server" Text="" OnClick="btnPrint_Click"
                                                                ToolTip="Delete" class="filter_btn printer_co" CausesValidation="false" AccessKey="H"
                                                               />
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
                </div>
            </div>

             </ContentTemplate>
         </asp:UpdatePanel>
</asp:Content>

