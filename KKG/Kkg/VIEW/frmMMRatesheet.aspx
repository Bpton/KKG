<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmMMRatesheet.aspx.cs" Inherits="VIEW_frmMMRatesheet" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register assembly="ProudMonkey.Common.Controls" namespace="ProudMonkey.Common.Controls" tagprefix="cc1" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
    <script type="text/javascript">
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

          function ValidateListBox_ddlSunItemType(sender, args) {
         var options = document.getElementById("<%=ddlSunItemType.ClientID%>").options;
         for (var i = 0; i < options.length; i++) {
             if (options[i].selected == true) {
                 args.IsValid = true;
                 return;
             }
         }
         args.IsValid = false;
        }



         $(function () {
            $('#ContentPlaceHolder1_ddlSunItemType').multiselect({
                includeSelectAllOption: true
            });
            $("#ContentPlaceHolder1_ddlSunItemType").multiselect('selectAll', false);
            $("#ContentPlaceHolder1_ddlSunItemType").multiselect('updateButtonText');
        });


    </script>
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="../images/loading123.gif" AlternateText="Processing"
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
                    PRINCIPAL GROUP RATESHEET MASTER</h3>
            </div>--%>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>
                                     Treading Sale Rate Sheet</h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAddprincipleSheet" runat="server" Text="Add New Record" CssClass="btn_link"
                                        OnClick="btnAddprincipleSheet_Click" CausesValidation="false" /></div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                   <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td width="130" class="field_title">
                                                <asp:Label ID="Label1" Text="Business Segment" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td width="170" class="field_input">
                                                <asp:DropDownList ID="ddlBusiness" runat="server" AppendDataBoundItems="true" class="chosen-select" 
                                                            Style="width: 250px;" data-placeholder="Select Business" ValidationGroup="Show" >
                                                        </asp:DropDownList>
                                                        
                                            
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="Show"
                                                            ForeColor="Red" ErrorMessage="Required!" ControlToValidate="ddlBusiness" ValidateEmptyText="false"
                                                            SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                              </td>

                                            <td class="field_title" width="130" style="display:none">
                                                <asp:Label ID="Label2" Text="Party" runat="server"></asp:Label><span class="req">*</span></td>
                                            <td class="field_input" width="170" style="display:none">
                                                 <asp:DropDownList ID="ddlparty" runat="server" AppendDataBoundItems="true" class="chosen-select"
                                                            Style="width: 250px;" data-placeholder="Select Party" 
                                                            ValidationGroup="Show" >
                                                 </asp:DropDownList>                                                        
                                           
                                            <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Show"
                                                            ForeColor="Red" ErrorMessage="Required!" ControlToValidate="ddlparty" ValidateEmptyText="false"
                                                            SetFocusOnError="true" InitialValue="0" > </asp:RequiredFieldValidator>--%>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>


                                        <tr>
                                            <td class="field_title" width="130"><asp:Label ID="lblDiv" Text="Primary Item Type*" runat="server"></asp:Label><span class="req">*</span></td>
                                            <td class="field_input" width="170">
                                                <asp:DropDownList ID="ddlPrimaryItemType" runat="server" AppendDataBoundItems="true" class="chosen-select" ValidationGroup="Show"
                                                            Style="width: 250px;" data-placeholder="Select Brand" AutoPostBack="true" OnSelectedIndexChanged="ddlPrimaryItemType_SelectedIndexChanged">
                                                        </asp:DropDownList>                                                        
                                            
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Show"
                                                            ForeColor="Red" ErrorMessage="Required!" ControlToValidate="ddlPrimaryItemType" ValidateEmptyText="false"
                                                            SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                            </td>
                                       
                                            <td class="field_title" width="130">
                                                <asp:Label ID="lblCat" Text="Sub Item Type" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input" width="170">
                                                 <asp:ListBox ID="ddlSunItemType" runat="server" SelectionMode="Multiple" TabIndex="4"
                                                                    Width="200%" AppendDataBoundItems="True" ValidationGroup="ADD" name="options[]"
                                                                    multiple="multiple">
                                                        </asp:ListBox>
                                                         <asp:CustomValidator ID="cvddlSunItemType" runat="server" ValidateEmptyText="true"
                                                                ControlToValidate="ddlSunItemType" ValidationGroup="ADD" ErrorMessage="Required!"
                                                                Display="Dynamic" ClientValidationFunction="ValidateListBox_ddlSunItemType" ForeColor="Red"></asp:CustomValidator>
                                                        
                                            </td>
                                           
                                        </tr>
                                       <tr>
                                             <td class="field_title">
                                                <asp:Label ID="Label3" Text="CURRENCY" runat="server"></asp:Label>
                                                <span class="req">*</span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlcurrency" runat="server" AppendDataBoundItems="true" class="chosen-select"
                                                    Style="width: 250px;" data-placeholder="Choose a Currency" ValidationGroup="Show" Enabled="false" >
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfv_ddlcurrency" runat="server" ValidationGroup="Show"
                                                    ForeColor="Red" ErrorMessage="Required!" ControlToValidate="ddlcurrency" ValidateEmptyText="false"
                                                    SetFocusOnError="true" InitialValue="0"> </asp:RequiredFieldValidator>
                                               
                                            </td>

                                           <td width="90" class="field_title">
                                                                Entry Date<span class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtEntryDate" runat="server" Width="60" Enabled="false" Font-Bold="true" />
                                                                <asp:ImageButton ID="imgPopuppodate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    Enabled="false" runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderVoucherDate" PopupButtonID="imgPopuppodate"
                                                                    Enabled="false" runat="server" TargetControlID="txtEntryDate" CssClass="cal_Theme1"
                                                                    Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td> 
                                             <td class="field_input">
                                            <div class="btn_24_blue">
                                                <span class="icon exclamation_co"></span>
                                                <asp:Button ID="btnsearch" runat="server" Text="Show" CssClass="btn_link" ValidationGroup="Show" OnClick="btnsearch_Click" />
                                            </div>                                                
                                            </td>
                                       </tr>
                                        <tr>
                                            <td class="field_title" width="130">Rate Sheet</td>
                                            <td class="field_input" colspan="4">
                                                <cc1:Grid ID="gvPrincipleRSMap" runat="server" CallbackMode="true" Serialize="true"
                                        FolderStyle="../GridStyles/premiere_blue" AutoGenerateColumns="false" PageSize="500" AllowPageSizeSelection="false"
                                        AllowAddingRecords="false" AllowPaging="false" AllowFiltering="false" >
                                        <Columns>
                                            <cc1:Column ID="Column8" DataField="ID" ReadOnly="true" HeaderText="PRODUCTID" runat="server"
                                                Visible="false" />
                                            <cc1:Column ID="Column9" DataField="NAME" HeaderText=" PRODUCT NAME" runat="server" Width="350%">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column DataField="" HeaderText="RATE" AllowEdit="true">
                                                <TemplateSettings TemplateId="mrptemplate" />
                                            </cc1:Column>
                                            <cc1:Column DataField="" HeaderText="UNIT" AllowEdit="true" Width="100%">
                                                        <TemplateSettings TemplateId="temppcs" />
                                             </cc1:Column>
                                             <cc1:Column ID="Column11" DataField="UOMID" HeaderText="UOMID" runat="server" Width="180" Visible="false">
                                            </cc1:Column>
                                            <cc1:Column ID="Column12" DataField="UOMNAME" HeaderText="UOM NAME" runat="server" Width="180" >
                                            </cc1:Column>
                                        </Columns>
                                        <Templates>
                                            <cc1:GridTemplate ID="mrptemplate">
                                                <Template>
                                                    <asp:HiddenField runat="server" ID="hdnpid" Value='<%# Container.DataItem["ID"] %>' />
                                                    <asp:HiddenField runat="server" ID="hdnPS" Value='<%# Container.DataItem["NAME"] %>' />
                                                     <asp:HiddenField runat="server" ID="hdnuomid" Value='<%# Container.DataItem["UOMID"] %>' />
                                                      <asp:HiddenField runat="server" ID="hdnuomname" Value='<%# Container.DataItem["UOMNAME"] %>' />
                                                    <asp:TextBox ID="txtmrpcost" runat="server" ToolTip='<%# Container.DataItem["ID"] %>' Width="80px"
                                                        onkeypress="return isNumberKeyWithDot(event);" Text='<%# Container.DataItem["RATE"] %>' />
                                                </Template>
                                            </cc1:GridTemplate>
                                        </Templates>
                                         <Templates>
                                            <cc1:GridTemplate ID="temppcs">
                                                <Template>
                                                    <asp:HiddenField runat="server" ID="hdnpid" Value='<%# Container.DataItem["ID"] %>' />
                                                    <asp:HiddenField runat="server" ID="hdnPS" Value='<%# Container.DataItem["NAME"] %>' />
                                                    <asp:TextBox ID="txtpcs" runat="server" ToolTip='<%# Container.DataItem["ID"] %>' Width="80px"
                                                        onkeypress="return isNumberKeyWithDot(event);" Text='<%# Container.DataItem["PCS"] %>' />
                                                </Template>
                                            </cc1:GridTemplate>
                                        </Templates>
                                        <ScrollingSettings ScrollWidth="80%" ScrollHeight="200" />
                                    </cc1:Grid>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td style="padding:8px 0;">
                                            <div class="btn_24_blue">
                                              <span class="icon disk_co"></span>
                                                <asp:Button ID="btnprincipleSubmit" runat="server" Text="Save" CssClass="btn_link" OnClick="btnprincipleSubmit_Click" ValidationGroup="A"  />
                                            </div>
                                                 &nbsp;&nbsp;&nbsp;&nbsp;
                                            <div class="btn_24_blue">
                                                <span class="icon cross_octagon_co"></span>
                                                <asp:Button ID="btnprincipleCancel" runat="server" Text="Cancel" CssClass="btn_link" OnClick="btnprincipleCancel_Click" CausesValidation="false" />
                                            </div>
                                                <asp:HiddenField ID="Hdn_Fld" runat="server" />
                                            </td>
                                        </tr>                                     
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlDisplay" runat="server">
                                <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>                                            
                                            <td class="field_input" style="padding-left:10px;">
                                                <asp:DropDownList ID="ddlSearchParty" runat="server" AppendDataBoundItems="true"
                                                class="chosen-select" Style="width: 250px;" data-placeholder="Choose a Party"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlSearchParty_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>                                            
                                            <td style="padding:8px 0;">
                                                <div class="gridcontent">
                                    <cc1:Grid ID="gvPrincipleRatesheet" runat="server" CallbackMode="true" Serialize="true"
                                        FolderStyle="../GridStyles/premiere_blue" AutoGenerateColumns="False"  AllowPageSizeSelection="false"
                                        OnDeleteCommand="DeleteRecord" AllowAddingRecords="false" AllowFiltering="true"
                                        EnableRecordHover="true"
                                            OnRowDataBound="gvProduct_RowDataBound"  AllowPaging="true">
                                        <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnClientDelete="OnDelete" />

                                        <FilteringSettings InitialState="Visible" />
                                        <Columns>
                                            <cc1:Column ID="Column1" DataField="ID" ReadOnly="true" HeaderText="ID" runat="server" Visible="false" />
                                            <cc1:Column ID="Column2" DataField="PRODUCTID" HeaderText="PID" runat="server" Visible="false" />
                                            <cc1:Column ID="Column3" DataField="BSID" HeaderText="BSID" runat="server" Visible="false" />
                                            <cc1:Column ID="Column4" DataField="BSNAME" HeaderText="BUSINESS SEGMENT" runat="server" Width="100%"/>
                                            <cc1:Column ID="Column5" DataField="PRODUCTNAME" HeaderText=" PRODUCT NAME" runat="server" Width="280%">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column ID="Column6" DataField="RATE" HeaderText="RATE" runat="server" Width="180">
                                            </cc1:Column>
                                             <cc1:Column ID="Column10" DataField="PCS" HeaderText="UNIT" runat="server" Width="180">
                                            </cc1:Column>
                                             <cc1:Column ID="Column13" DataField="UOMNAME" HeaderText="UOM NAME" runat="server" Width="180" >
                                            </cc1:Column>
                                            <cc1:Column ID="Column14" DataField="ISAPPROVE" HeaderText="STATUS" runat="server" Width="180" Visible="false">
                                            </cc1:Column>
                                            <cc1:Column ID="Column32" DataField="ENTRYDATE" HeaderText="ENTRY DATE" runat="server" Width="180">
                                            </cc1:Column>
                                            <cc1:Column ID="Column7" HeaderText="Edit" AllowEdit="true" AllowDelete="true" runat="server" Visible="false"
                                                Width="80">
                                                <TemplateSettings TemplateId="editBtnTemplate" />
                                            </cc1:Column>
                                             <cc1:Column ID="Column15" AllowEdit="false" AllowDelete="false" HeaderText="CONFIRM" runat="server" Width="85" Wrap="true" >
                                                    
                                                    <TemplateSettings TemplateId="editApproveBtnTemplate" />
                                                </cc1:Column>
                                            <cc1:Column ID="Column16" AllowEdit="false" AllowDelete="false" HeaderText="Delete" runat="server" Width="80">
                                                <TemplateSettings TemplateId="deleteBtnTemplate" />
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
                                            <cc1:GridTemplate runat="server" ID="deleteBtnTemplate">
                                                <Template>
                                                    <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="gvPrincipleRatesheet.delete_record(this)">
                                                    </a>
                                                </Template>
                                            </cc1:GridTemplate>
                                             <cc1:GridTemplate runat="server" ID="editApproveBtnTemplate">
                                         <Template>
                                             <a href="javascript: //" class="filter_btn carousel" id="btnGridApprove_<%# Container.PageRecordIndex %>"
                                                 onclick="CallServerMethodApprove(this)"></a>
                                         </Template>
                                         </cc1:GridTemplate>
                                        </Templates>
                                        <ScrollingSettings ScrollWidth="100%" ScrollHeight="290" />
                                    </cc1:Grid>
                                        
                                    <asp:Button ID="btngridsave" runat="server" Text="Edit" Style="display: none" OnClick="btngridsave_Click"
                                        CausesValidation="false" />
                                         <asp:Button ID="btngrdApprove" runat="server" Text="Approve" Style="display: none"
                                        OnClick="btngrdApprove_Click" CausesValidation="false" />
                                    </asp:Panel>
                                    <asp:HiddenField ID="hdn_pid" runat="server" />
                                    <asp:HiddenField ID="hdn_bsid" runat="server" />
                                 <asp:HiddenField ID="hdn_group" runat="server" />
                                <asp:HiddenField ID="hdn_currency" runat="server" />
                                </div>
                                            </td>
                                        </tr>                           
                                    </table>
                            </div>
                        </div>
                    </div>
                    <cc1:MessageBox ID="MessageBox1" runat="server" />  
                    <span class="clear"></span>
                </div>
                <span class="clear"></span>
            </div>
            <script type="text/javascript">
                function CallServerMethod(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                    document.getElementById("<%=hdn_pid.ClientID %>").value = gvPrincipleRatesheet.Rows[iRecordIndex].Cells[1].Value;
                    document.getElementById("<%=hdn_bsid.ClientID %>").value = gvPrincipleRatesheet.Rows[iRecordIndex].Cells[2].Value;
                    document.getElementById("<%=btngridsave.ClientID %>").click();
                }

                function CallServerMethodApprove(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridApprove_", "");
                    document.getElementById("<%=hdn_pid.ClientID %>").value = gvPrincipleRatesheet.Rows[iRecordIndex].Cells[1].Value;
                    document.getElementById("<%=hdn_bsid.ClientID %>").value = gvPrincipleRatesheet.Rows[iRecordIndex].Cells[2].Value;
                    document.getElementById("<%=hdn_group.ClientID %>").value = gvPrincipleRatesheet.Rows[iRecordIndex].Cells[5].Value;
                    document.getElementById("<%=hdn_currency.ClientID %>").value = gvPrincipleRatesheet.Rows[iRecordIndex].Cells[6].Value;
                    document.getElementById("<%=btngrdApprove.ClientID %>").click();
                }

                function isNumberKeyWithDot(evt) {
                    var charCode = (evt.which) ? evt.which : event.keyCode;
                    if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 46))
                        return false;

                    return true;
                }
            </script>
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