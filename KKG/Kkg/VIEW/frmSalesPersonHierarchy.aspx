<%@ Page Title="Distribution Channel Hierarchy" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true"
    CodeFile="frmSalesPersonHierarchy.aspx.cs" Inherits="VIEW_frmSalesPersonHierarchy" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register assembly="ProudMonkey.Common.Controls" namespace="ProudMonkey.Common.Controls" tagprefix="cc1" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <%--  <script type="text/javascript">


        function openMapping(UTID,UTNAME) {

            document.getElementById("<%=hdnUTID.ClientID %>").value = UTID;
            document.getElementById("<%=txtPname.ClientID %>").value = UTNAME;
            document.getElementById("<%=hdnMode.ClientID %>").value = "M";
            document.getElementById("<%=btngrdmapping.ClientID %>").click();
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
    </script>--%>
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
            <%--<div class="page_title">
                <span class="title_icon"><span class="computer_imac"></span></span>
                <h3>
                    Distribution Channel Hierarchy
                </h3>
            </div>--%>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>Distribution Channel Hierarchy Details</h6>
                              <%--  <div class="btn_30_light" style="float: right;">
                                    <span id="icon" class="icon add_co"></span>--%>
                                    <%--<asp:Button ID="btnAddMenu" runat="server" Text="Add New Record" CssClass="btn_link"
                                        OnClick="btnAddMenu_Click" CausesValidation="false" />--%></div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>                                          
                                             <td width="130" class="field_title">
                                                <asp:Label ID="lblBusniessSegment" Text="BUSINESS SEGMENTS" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <asp:DropDownList ID="ddlBusinessSegment" runat="server" 
                                                    AppendDataBoundItems="true" class="chosen-select"
                                                    Style="width: 250px;" data-placeholder="Choose a Vendor" 
                                                    AutoPostBack="True" onselectedindexchanged="ddlBusinessSegment_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfv_ddlBusinessSegment" runat="server"  ForeColor="Red"
                                                    ErrorMessage="Required!" ControlToValidate="ddlBusinessSegment" ValidateEmptyText="false"
                                                    SetFocusOnError="true" InitialValue="0" > </asp:RequiredFieldValidator>
                                            </td>
                                            </tr>
                                        <tr>
                                            <%--<td width="130" class="field_title">
                                                &nbsp;
                                            </td> --%>                                           
                                            <td class="field_input" colspan="2">
                                                <div class="gridcontent-inner"> 
                                                    <cc1:Grid ID="gvSalesPersonHierarchy" runat="server" CallbackMode="true" 
                                                        Serialize="true" AutoGenerateColumns="false" AllowPaging="false"
                                                    AllowAddingRecords="false" AllowFiltering="true" AllowPageSizeSelection="true"
                                                        FolderStyle="../GridStyles/premiere_blue" >
                                                <Columns>
                                                    <cc1:Column  DataField="UTID" ReadOnly="true" HeaderText="ID" runat="server"
                                                        Visible="false" />
                                                    <cc1:Column  DataField="UTNAME" HeaderText=" NAME" runat="server" Width="250">
                                                        <FilterOptions>
                                                            <cc1:FilterOption Type="NoFilter" />
                                                            <cc1:FilterOption Type="Contains" />
                                                            <cc1:FilterOption Type="StartsWith" />
                                                        </FilterOptions>
                                                    </cc1:Column>
                                                    <cc1:Column DataField="" HeaderText="LEVEL" AllowEdit="true">
                                                        <TemplateSettings TemplateId="rmcosetemplate" />
                                                    </cc1:Column>
                                                </Columns>                            
                                                <Templates>
                                                    <cc1:GridTemplate ID="rmcosetemplate">
                                                        <Template>
                                                            <asp:HiddenField runat="server" ID="hdnUTID" Value='<%# Container.DataItem["UTID"] %>' />
                                                            <asp:HiddenField runat="server" ID="HiddenField1" Value='<%# Container.DataItem["UTNAME"] %>' />

                                                            <asp:TextBox ID="txtLevel" runat="server"  onkeypress="return isNumberKey(event);" Text='<%# Container.DataItem["LEVEL"] %>' />
                                                       
                                                             </Template>
                                                    </cc1:GridTemplate>
                                                </Templates>
                                              
                                                <ScrollingSettings ScrollWidth="85%" ScrollHeight="250" />
                                            </cc1:Grid>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>                                                 
                                                 <td colspan="2" style="padding:8px 0;">
                                                 <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                     <asp:Button ID="btnSubmit" runat="server" CssClass="btn_link" Text="Save" onclick="btnSubmit_Click" />
                                                 </div> 
                                                    <%-- <asp:HiddenField ID="Hdn_Fld" runat="server" />--%>
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
                    var searchTimeout = null;
                    function FilterTextBox_KeyUp() {
                        searchTimeout = window.setTimeout(performSearch, 500);
                    }
                   
                </script>
             <%--   <script type="text/javascript">

                    function CallServerMethod(oLink) {
                        var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                        document.getElementById("<%=Hdn_Fld.ClientID %>").value = gvOrgChart.Rows[iRecordIndex].Cells[0].Value;
                        document.getElementById("<%=btngrdedit.ClientID %>").click();

                    }
                    function attachFlyoutToLink(oLink, Action) {
                        var ele = document.getElementById("<%=pnlDisplay.ClientID%>");
                        if (ele.style.display == "") {
                            ele.style.display = "none";
                            document.getElementById("<%=pnlAdd.ClientID%>").style.display = "";
                        }
                        clearFlyout();
                        populateEditControls(oLink.id.toString().replace("btnGridEdit_", ""));
                    }
                    function clearFlyout() {
                        document.getElementById("<%=txtPageName.ClientID %>").value = '';
                        document.getElementById("<%=ddlParentPageName.ClientID %>").value = '';
                        document.getElementById("<%=Hdn_Fld.ClientID %>").value = '';
                        document.getElementById("<%=txtCode.ClientID%>").value = '';
                        document.getElementById("<%=txtName.ClientID%>").value = '';
                    }
                    function populateEditControls(iRecordIndex) {
                        document.getElementById("<%=txtPageName.ClientID%>").value = gvOrgChart.Rows[iRecordIndex].Cells[4].Value;
                        document.getElementById("<%=txtCode.ClientID%>").value = gvOrgChart.Rows[iRecordIndex].Cells[3].Value;
                        document.getElementById("<%=txtName.ClientID%>").value = gvOrgChart.Rows[iRecordIndex].Cells[2].Value;

                        document.getElementById("<%=Hdn_Fld.ClientID %>").value = gvOrgChart.Rows[iRecordIndex].Cells[0].Value;

                        var activevalue = gvOrgChart.Rows[iRecordIndex].Cells[6].Value;
                        if (activevalue == "Y") {
                            document.getElementById("<%=chkActive.ClientID%>").checked = true;
                        }
                        else {
                            document.getElementById("<%=chkActive.ClientID%>").checked = false;
                        }

                        var val;
                        val = gvOrgChart.Rows[iRecordIndex].Cells[1].Value;
                        selectValue(val);
                    }
                    function selectValue(val) {
                        var lc = document.getElementById("<%=ddlParentPageName.ClientID%>");
                        for (i = 0; i < lc.length; i++) {
                            if (lc.options[i].innerHTML == val) {
                                lc.selectedIndex = i;
                                return;
                            }
                        }
                    }
                </script>--%>
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

                     
                    function isNumberKey(evt) {
                        var charCode = (evt.which) ? evt.which : event.keyCode;
                        if (charCode < 48 || charCode > 57)
                            return false;
                        return true;
                    }    
              
            </script>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
