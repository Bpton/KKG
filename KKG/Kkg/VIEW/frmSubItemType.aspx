<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmSubItemType.aspx.cs" Inherits="VIEW_frmSubItemType" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register assembly="ProudMonkey.Common.Controls" namespace="ProudMonkey.Common.Controls" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
         <script type="text/javascript">
        function CHKHSNCode() {      
            var txtHSNCode1 = "ContentPlaceHolder1_txthsn";            
            var txtHSNCode = document.getElementById(txtHSNCode1).value;
            var txtHSNCodeL = txtHSNCode.length;
             var j = 0;
          for (var i = 0; i < txtHSNCodeL; i++) {  
            
               var n1 = txtHSNCode.charCodeAt(i);                                   
              if (n1 >= 48 && n1 <= 57) {
                    if (n1 != 48) {
                    j = 1;
                }            
               }
               else {
                    alert('Invalid HSN Code');
                  document.getElementById(txtHSNCode1).value = "";
                   return false;
               }
            }
            if (j == 0)
            {
                 alert('Invalid HSN Code');
                  document.getElementById(txtHSNCode1).value = "";
                   return false;
            }
           
       return true;
    }
  </script>

      <script type="text/javascript">
        function isNumberKey(evt) {
                        var charCode = (evt.which) ? evt.which : event.keyCode;
                        if (charCode < 48 || charCode > 57)
                            return false;

                        return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                                <h6>Sub Item Type Details</h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAddDivision" runat="server" Text="Add New Record" CssClass="btn_link"
                                        OnClick="btnAddDivision_Click" CausesValidation="false" /></div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td width="120" class="field_title">
                                                <asp:Label ID="lblitemowner" Text="Item Owner" runat="server"></asp:Label>
                                                <span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <asp:DropDownList ID="ddlitemowner" runat="server" AppendDataBoundItems="true" Width="200" Height="28" class="chosen-select"  data-placeholder="Choose a type" ValidationGroup="submitvalidation">
                                                    <%--<asp:ListItem Value="0" Text="-Select Owner-"></asp:ListItem>--%>
                                                    <asp:ListItem Value="M" Text="KKG"></asp:ListItem>
                                                    <%--<asp:ListItem Value="R" Text="Riya"></asp:ListItem>
                                                    <asp:ListItem Value="B" Text="Both"></asp:ListItem>--%>
                                                </asp:DropDownList>
                                                 <asp:RequiredFieldValidator ID="Reqitemowner" runat="server" ErrorMessage="Required!" EnableClientScript="true" ForeColor="Red"
                                                    ControlToValidate="ddlitemowner" ValidateEmptyText="false" SetFocusOnError="true" InitialValue="0" ValidationGroup="SAVE">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                            </tr>                                        
                                        <tr>
                                            <td width="120" class="field_title"><asp:Label ID="Label2" Text="Primary Item Type" runat="server"></asp:Label><span class="req">*</span></td>
                                            <td class="field_input">
                                                <asp:DropDownList ID="ddlPrimaryItemType" Width="200" runat="server" class="chosen-select" data-placeholder="Select Item Type">                                                
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPrimaryItemType" runat="server" ControlToValidate="ddlPrimaryItemType" ValidateEmptyText="false" 
                                                            SetFocusOnError="true" ErrorMessage="Required!"  ValidationGroup="SAVE" ForeColor="Red" InitialValue="0" ></asp:RequiredFieldValidator>
                                                        
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td width="120" class="field_title"><asp:Label ID="lblDIVCode" Text="Sub Item Type Code" runat="server"></asp:Label><span class="req">*</span></td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtTypeCode" runat="server" CssClass="mid" MaxLength="3" Enabled="true"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="CV_Typecode" runat="server" ErrorMessage="Code is required!"
                                                            ControlToValidate="txtTypeCode" ValidateEmptyText="false" Display="None" ValidationGroup="SAVE"> </asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                                            TargetControlID="CV_Typecode" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                                        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title"><asp:Label ID="lblDIVName" Text="Sub Item Type Name" runat="server"></asp:Label><span class="req">*</span></td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtItemTypeName" runat="server" CssClass="mid"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="CV_TypeName" runat="server" ErrorMessage="Name is required!"
                                                            ControlToValidate="txtItemTypeName" ValidateEmptyText="false" Display="None" ValidationGroup="SAVE"> </asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                            TargetControlID="CV_TypeName" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>                                            
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title"><asp:Label ID="lblDescription" Text="Description" runat="server"></asp:Label><span class="req">*</span></td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtDescription" runat="server" Width="30%" TextMode="MultiLine" ></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="CV_Description" runat="server" ErrorMessage="Description is required!"
                                                            ControlToValidate="txtDescription" ValidateEmptyText="false" Display="None" ValidationGroup="SAVE"> </asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                            TargetControlID="CV_Description" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <%--Add By Rajeev(01-07-2017)--%>
                                        <tr>                                        
                                        <td class="field_title">
                                        <asp:Label ID="lblhsn" Text="HSN" runat="server"></asp:Label><span class="req">*
                                        </td>
                                        <td class="field_input">
                                        <asp:TextBox ID="txthsn" runat="server" CssClass="mid" 
                                             onchange="CHKHSNCode();"
                                                onkeypress="return isNumberKey(event);" MaxLength="8"
                                            ></asp:TextBox>                                                        
                                        </td>     
                                          <asp:RequiredFieldValidator ID="CV_HSN" runat="server" Display="None" ErrorMessage="Hsn is required!"
                                                            ControlToValidate="txthsn" ValidateEmptyText="false" OnServerValidate="CV_HSN_ServerValidate"  ValidationGroup="SAVE"> </asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                                            TargetControlID="CV_HSN" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>                                   
                                        </tr>
                                         <tr>
                                             <td width="120" class="field_title">
                                                 <asp:Label ID="lblfacmap" Text="Factory Map" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input">
                                                <asp:DropDownList ID="ddlfacmap" runat="server" AppendDataBoundItems="true" Width="200" Height="28" class="chosen-select"  data-placeholder="Choose a type" ValidationGroup="submitvalidation">                                                                                            
                                                </asp:DropDownList>                                                
                                            </td>
                                            </tr>                 


                                        <%--End--%>
                                        <tr>
                                            <td class="field_title"><asp:Label ID="Label1" Text="Active" runat="server"></asp:Label></td>
                                            <td class="field_input"><asp:CheckBox ID="chkActive" runat="server" Text=" " /></td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td style="padding:8px 0;">
                                            <div class="btn_24_blue">
                                              <span class="icon disk_co"></span>
                                                <asp:Button ID="btnDIVSubmit" runat="server" Text="Save" CssClass="btn_link" OnClick="btnDIVSubmit_Click" ValidationGroup="SAVE"/>
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                <span class="icon cross_octagon_co"></span>
                                                <asp:Button ID="btnDIVCancel" runat="server" Text="Cancel" CssClass="btn_link" OnClick="btnDIVCancel_Click" CausesValidation="false" />
                                                </div>
                                                        <asp:HiddenField ID="Hdn_Fld" runat="server" />
                                                        <asp:HiddenField ID="hdnPredefine" runat="server" />
                                            </td>
                                        </tr>                                   
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlDisplay" runat="server">
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
                                    <cc1:Grid ID="gvDivision" runat="server" CallbackMode="true" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                        AutoGenerateColumns="false" AllowPageSizeSelection="false" OnDeleteCommand="DeleteRecord"
                                        AllowAddingRecords="false" AllowFiltering="true" 
                                            onrowdatabound="gvDivision_RowDataBound">
                                        <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnClientDelete="OnDelete" />
                                        <FilteringSettings InitialState="Visible" />
                                        <Columns>
                                            <cc1:Column ID="Column1" DataField="SUBTYPEID" ReadOnly="true" HeaderText="SUBTYPEID" runat="server" Visible="false" />
                                             <cc1:Column ID="Column7" DataField="ITEMOWNER" HeaderText="ITEM OWNER" runat="server" Width="80">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="EqualTo" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="DoesNotContain" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column ID="Column9" DataField="PRIMARYITEMTYPEID" ReadOnly="true" HeaderText="PRIMARYITEMTYPEID" runat="server" Visible="false" />
                                            <cc1:Column ID="Column10" DataField="ITEMDESC" HeaderText=" PRIMARY ITEM TYPE" runat="server" Width="180">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="EqualTo" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="DoesNotContain" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>
                                           <%-- <cc1:Column ID="Column12" DataField="ALTERNATECODE" HeaderText="ALTERNATE CODE" runat="server" Width="80">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="EqualTo" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="DoesNotContain" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>--%>
                                            <cc1:Column ID="Column2" DataField="SUBITEMCODE" HeaderText=" SUB ITEM TYPE CODE" runat="server" Width="180">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="EqualTo" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="DoesNotContain" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column ID="Column3" DataField="SUBITEMNAME" HeaderText=" SUB ITEM TYPE NAME" runat="server" Width="190">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="EqualTo" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="DoesNotContain" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column ID="Column4" DataField="SUBITEMDESC" HeaderText="SUB ITEM TYPE DESCRIPTION" runat="server" Width="280">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="EqualTo" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="DoesNotContain" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>
                                            
                                            <cc1:Column ID="Column5" DataField="STATUS" HeaderText="STATUS" runat="server" Width="100">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                </FilterOptions>
                                            </cc1:Column>

                                            <cc1:Column ID="Column11" DataField="BRID" HeaderText="BRID" runat="server" Width="50" Visible="false">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                </FilterOptions>
                                            </cc1:Column>

                                            <cc1:Column ID="Column8" DataField="BRPREFIX" HeaderText="BR NAME" runat="server" Width="100">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="EqualTo" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="DoesNotContain" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>
                                            
                                            <cc1:Column ID="Column6" AllowEdit="true" AllowDelete="true" HeaderText="EDIT" runat="server"
                                                Width="80">
                                                <TemplateSettings TemplateId="editBtnTemplate" />
                                            </cc1:Column>
                                            <cc1:Column HeaderText="DELETE" AllowEdit="false" AllowDelete="true" Width="80" Visible="false">
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
                                                    <cc1:GridTemplate runat="server" ID="deleteBtnTemplate" >
                                                    <Template>
                                                    <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="gvDivision.delete_record(this)"  >
                                                    </a>                                             
                                                    </Template>
                                                    </cc1:GridTemplate>
                                        </Templates>
                                        <%--<ScrollingSettings ScrollWidth="100%" ScrollHeight="290" />--%>
                                    </cc1:Grid>
                                    <asp:Button ID="btngrdedit" runat="server" Text="grdedit" Style="display: none" OnClick="btngrdedit_Click" CausesValidation="false" />
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
                    function OnBeforeDelete(record) {
                        record.Error = '';

                        document.getElementById("<%=Hdn_Fld.ClientID %>").value = record.SUBTYPEID;
                        if (confirm("Are you sure you want to delete? "))
                            return true;
                        else
                            return false;
                    }
                    function OnDelete(record) {
                        alert(record.Error);
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
                        gvDivision.addFilterCriteria('SUBITEMNAME', OboutGridFilterCriteria.Contains, searchValue);
                        gvDivision.addFilterCriteria('SUBITEMDESC', OboutGridFilterCriteria.Contains, searchValue);
                        gvDivision.addFilterCriteria('SUBITEMCODE', OboutGridFilterCriteria.Contains, searchValue);
                        gvDivision.executeFilter();
                        searchTimeout = null;
                        return false;
                    }
                </script>
                <script type="text/javascript">


                    function CallServerMethod(oLink) {                        
                        var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                        document.getElementById("<%=Hdn_Fld.ClientID %>").value = gvDivision.Rows[iRecordIndex].Cells[0].Value;
                        document.getElementById("<%=btngrdedit.ClientID %>").click();

                    }

                    
                </script>

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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>