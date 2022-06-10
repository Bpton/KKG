<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true"
    CodeFile="frmDepotTransferRateSheet_RMPM.aspx.cs" Inherits="VIEW_frmDepotTransferRateSheet_RMPM" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function OnBeforeDelete(record) {
            record.Error = '';
            document.getElementById("<%=Hdn_Fld.ClientID %>").value = record.PRODUCTID;
            if (confirm("Are you sure you want to delete? "))
                return true;
            else
                return false;
        }
        function OnDelete(record) {
            alert(record.Error);
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
    </script>
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="~/images/loading123.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <%--<div class="page_title">
                <span class="title_icon"><span class="computer_imac"></span></span>
                <h3>
                DEPOT TRANSFER RATESHEET MASTER</h3>
            </div>--%>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>
                                    Depot Transfer RateSheet Details(RM-PM)</h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAddRateSheet" runat="server" Text="Add New Record" CssClass="btn_link"
                                        OnClick="btnAddRateSheet_Click" CausesValidation="false" /></div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td width="125" class="field_title" style="padding-bottom: 16px;">
                                                <asp:Label ID="lblprimaryitem" Text="PRIMARY ITEM" runat="server"></asp:Label>
                                                <span class="req">*</span>
                                            </td>
                                            <td width="190" class="field_input" style="padding-left: 10px;">
                                                <asp:DropDownList ID="ddlprimaryitem" runat="server" AppendDataBoundItems="true"
                                                    class="chosen-select" Style="width: 190px;" data-placeholder="Select Brand" OnSelectedIndexChanged="ddlprimaryitem_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlprimaryitem" runat="server" ValidationGroup="A"
                                                    ForeColor="Red" ErrorMessage="Required!" ControlToValidate="ddlprimaryitem" ValidateEmptyText="false"
                                                    SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                            </td>
                                            <td class="field_title" width="80" style="padding-bottom: 16px;">
                                                <asp:Label ID="lblsubitem" Text="Sub item" runat="server"></asp:Label>
                                                <span class="req">*</span> </label>
                                            </td>
                                            <td class="field_input" width="210px" style="padding-bottom: 16px;">
                                                <asp:DropDownList ID="ddlsubitem" runat="server" AppendDataBoundItems="true" class="chosen-select"
                                                    Style="width: 220px;" data-placeholder="Select Sub Item">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlsubitem" runat="server" ValidationGroup="A"
                                                    ForeColor="Red" ErrorMessage="Required!" ControlToValidate="ddlsubitem" ValidateEmptyText="false"
                                                    SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                            </td>
                                            <td class="field_input" style="padding-bottom: 16px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon exclamation_co"></span>
                                                    <asp:Button ID="btnsearch" runat="server" Text="Show" CssClass="btn_link" OnClick="btnsearch_Click" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="130" class="field_title">
                                                <asp:Label ID="Label16" runat="server" Text="From Date"></asp:Label>
                                                <span class="req">*</span>
                                            </td>
                                            <td width="255" class="field_input">
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
                                            <td width="130" class="field_title">
                                                <asp:Label ID="Label19" runat="server" Text="To Date"></asp:Label>
                                                <span class="req">*</span>
                                            </td>
                                            <td width="255" class="field_input">
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
                                        </tr>
                                        <tr>
                                            <td class="field_title">
                                                Depot Transfer Rate
                                            </td>
                                            <td class="field_input" colspan="4" style="padding-right: 20px;">
                                                <div class="gridcontent-inner">
                                                    <cc1:Grid ID="gvCustomerRSMap" runat="server" CallbackMode="true" Serialize="true"
                                                        FolderStyle="../GridStyles/premiere_blue" EnableRecordHover="true" AutoGenerateColumns="false"
                                                        AllowPageSizeSelection="false" PageSize="500" AllowPaging="false" AllowAddingRecords="false"
                                                        AllowSorting="false" AllowFiltering="false">
                                                        <Columns>
                                                        <cc1:Column ID="Column16"  DataField="ID" ReadOnly="true" HeaderText="ID" runat="server" Width="80" Wrap="true">                                      
                                                            <TemplateSettings TemplateID="CheckTemplate" HeaderTemplateId="HeaderTemplate" />
                                                        </cc1:Column>
                                                           <%-- <cc1:Column ID="Column6" DataField="ID" ReadOnly="true" HeaderText="PRODUCTID" runat="server"
                                                                Visible="false" />--%>
                                                            <cc1:Column ID="Column7" DataField="NAME" HeaderText=" PRODUCT NAME" runat="server"
                                                                Width="420" Wrap="true">
                                                                <FilterOptions>
                                                                    <cc1:FilterOption Type="NoFilter" />
                                                                    <cc1:FilterOption Type="Contains" />
                                                                    <cc1:FilterOption Type="StartsWith" />
                                                                </FilterOptions>
                                                            </cc1:Column>
                                                            <cc1:Column DataField="" HeaderText="RATE" AllowEdit="true" Wrap="true">
                                                                <TemplateSettings TemplateId="mrptemplate" />
                                                            </cc1:Column>
                                                        </Columns>
                                                        <Templates>
                                                           
                                                             <cc1:GridTemplate runat="server" ID="CheckTemplate">
                                                            <Template>
                                                             <asp:HiddenField runat="server" ID="hdnPRODUCTID" Value='<%# Container.DataItem["ID"] %>' />                               
                                                            <asp:CheckBox ID="ChkID" runat="server" Text=" " ToolTip="<%# Container.Value %>" style="padding-left: 5px;" />
                                                            </Template>
                                                            </cc1:GridTemplate>
                                                          
                                                        </Templates>
                                                        <Templates>
                                                            <cc1:GridTemplate ID="mrptemplate">
                                                                <Template>
                                                                    <asp:HiddenField runat="server" ID="hdnPS" Value='<%# Container.DataItem["NAME"] %>' />
                                                                    <asp:TextBox ID="txtmrpcost" runat="server" ToolTip='<%# Container.DataItem["ID"] %>'
                                                                        Width="90px" onkeypress="return isNumberKey(this,event);" Text='<%# Container.DataItem["RATE"] %>' />
                                                                </Template>
                                                            </cc1:GridTemplate>
                                                        </Templates>
                                                        <ScrollingSettings ScrollHeight="290" />
                                                    </cc1:Grid>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td colspan="4" style="padding-left: 10px; padding-top: 8px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnCustomerSubmit" runat="server" Text="Save" CssClass="btn_link"
                                                        OnClick="btnCustomerSubmit_Click" ValidationGroup="A" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnCustomerCancel" runat="server" Text="Cancel" CssClass="btn_link"
                                                        OnClick="btnCustomerCancel_Click" CausesValidation="false" />
                                                </div>
                                                <asp:HiddenField ID="Hdn_Fld" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlDisplay" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td style="width: 100px;" class="field_title">
                                                <asp:Label ID="lblsearchprimaryitem" Text="Primary Sub Item" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input" style="width: 190px;">
                                                <asp:DropDownList ID="ddlSearchprimaryitem" runat="server" Width="180px" AppendDataBoundItems="true"
                                                    class="chosen-select" data-placeholder="Choose a Brand" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchprimaryitem_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="field_title" style="width: 70px;">
                                                <asp:Label ID="Label2" Text="Category" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input">
                                                <asp:DropDownList ID="ddlSearchsubitem" runat="server" AppendDataBoundItems="true"
                                                    class="chosen-select" Width="210px" data-placeholder="Choose a Category" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlSearchsubitem_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="padding: 8px 0 8px 0px;">
                                                <div class="gridcontent">
                                                    <cc1:Grid ID="gvCustomerRatesheet" runat="server" CallbackMode="true" Serialize="true"
                                                        FolderStyle="../GridStyles/premiere_blue" AutoGenerateColumns="False" AllowPageSizeSelection="true"
                                                        OnDeleteCommand="DeleteRecord" PageSize="500" EnableRecordHover="true" AllowAddingRecords="false"
                                                        AllowFiltering="true">
                                                        <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnClientDelete="OnDelete" />
                                                        <FilteringSettings InitialState="Visible" />
                                                        <Columns>
                                                            <cc1:Column ID="Column12" DataField="Sl" HeaderText="SL" runat="server" Width="80"
                                                                Wrap="true">
                                                                <TemplateSettings TemplateId="slnoTemplate" />
                                                            </cc1:Column>
                                                            <cc1:Column ID="Column1" DataField="PRODUCTNAME" HeaderText=" PRODUCT NAME" runat="server"
                                                                Width="450" Wrap="true">
                                                                <FilterOptions>
                                                                    <cc1:FilterOption Type="NoFilter" />
                                                                    <cc1:FilterOption Type="Contains" />
                                                                    <cc1:FilterOption Type="StartsWith" />
                                                                </FilterOptions>
                                                            </cc1:Column>
                                                            <cc1:Column ID="Column2" DataField="RATE" HeaderText="TRANSFER RATE(Rs.)" runat="server"
                                                                Width="150">
                                                            </cc1:Column>
                                                            <cc1:Column ID="Column3" DataField="PRODUCTID" HeaderText="PID" runat="server" Visible="false" />
                                                            <%-- <cc1:Column ID="Column5"  HeaderText="Edit" AllowEdit="true" AllowDelete="true" runat="server" Width="80">
                                                <TemplateSettings TemplateId="editBtnTemplate" />
                                            </cc1:Column>--%>
                                                             <cc1:Column ID="Column4" DataField="FROMDATE" HeaderText="FROM DATE" runat="server" Width="150">
                                                            </cc1:Column>  
                                                             <cc1:Column ID="Column5" DataField="TODATE" HeaderText="TO DATE" runat="server" Width="150">
                                                            </cc1:Column>  
                                                 
                                                            <cc1:Column HeaderText="DELETE" AllowEdit="false" AllowDelete="true" Width="80">
                                                                <TemplateSettings TemplateId="deleteBtnTemplate" />
                                                            </cc1:Column>
                                                        </Columns>
                                                        <FilteringSettings MatchingType="AnyFilter" />
                                                        <Templates>
                                                            <%--<cc1:GridTemplate runat="server" ID="editBtnTemplate">
                                                <Template>
                                                    <a href="javascript: //" class="action-icons c-edit" id="btnGridEdit_<%# Container.PageRecordIndex %>"
                                                        onclick="CallServerMethod(this)"></a>
                                                </Template>
                                            </cc1:GridTemplate>--%>
                                                        </Templates>
                                                        <Templates>
                                                            <cc1:GridTemplate runat="server" ID="deleteBtnTemplate">
                                                                <Template>
                                                                    <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="gvCustomerRatesheet.delete_record(this)">
                                                                    </a>
                                                                </Template>
                                                            </cc1:GridTemplate>
                                                            <cc1:GridTemplate runat="server" ID="slnoTemplate">
                                                                <Template>
                                                                    <asp:Label ID="lblslno" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                                </Template>
                                                            </cc1:GridTemplate>
                                                        </Templates>
                                                        <ScrollingSettings ScrollHeight="290" />
                                                    </cc1:Grid>
                                                    <%--<asp:Button ID="btngridsave" runat="server" Text="Edit" Style="display: none" OnClick="btngridsave_Click" CausesValidation="false" />--%>
                                                    <asp:HiddenField ID="hdn_pid" runat="server" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
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

                function isNumberKey(e1, evt) {
                    var charCode = (event.which) ? event.which : event.keyCode;

                    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57))
                        return false;

                    //if dot sign entered more than once then don't allow to enter dot sign again. 46 is the code for dot sign
                    if (charCode == 46 && (elementRef.value.indexOf('.') >= 0))
                        return false;
                    else if (charCode == 46 && (elementRef.value.indexOf('.') <= 0)) //allow to enter dot sign if not entered.
                        return true;
                    if (el.value.indexOf(".") !== -1) {
                        var range = document.selection.createRange();

                        if (range.text != "") {
                        }
                        else {
                            var number = el.value.split('.');
                            if (number.length == 2 && number[1].length > 1)
                                return false;
                        }
                    }
                    return true;
                }
            </script>
            <script type="text/javascript">
                function CallServerMethod(oLink) {

                    document.getElementById("<%=hdn_pid.ClientID %>").value = gvCustomerRatesheet.Rows[iRecordIndex].Cells[3].Value;
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
