<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="frmBackDateSettings.aspx.cs" Inherits="VIEW_frmBackDateSettings" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register assembly="ProudMonkey.Common.Controls" namespace="ProudMonkey.Common.Controls" tagprefix="cc1" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

     <script type="text/javascript">

        $(function () {
            $('#ContentPlaceHolder1_ddldepot').multiselect({
                includeSelectAllOption: true
            });
            $("#ContentPlaceHolder1_ddldepot").multiselect('selectAll', false);
            $("#ContentPlaceHolder1_ddldepot").multiselect('updateButtonText');

        });

          $(function () {
            $('#ContentPlaceHolder1_ddlparty').multiselect({
                includeSelectAllOption: true
            });
            $("#ContentPlaceHolder1_ddlparty").multiselect('selectAll', false);
            $("#ContentPlaceHolder1_ddlparty").multiselect('updateButtonText');
        });  

        
       
    </script>
    <script type="text/javascript">
        function ValidateListBox(sender, args) {
            var options = document.getElementById("<%=ddldepot.ClientID%>").options;
            for (var i = 0; i < options.length; i++) {
                if (options[i].selected == true) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }
   </script>
    <%--<script type="text/javascript">
        function ValidateListBox(sender, args) {
            var options = document.getElementById("<%=ddlbsname.ClientID%>").options;
            for (var i = 0; i < options.length; i++) {
                if (options[i].selected == true) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }
   </script>--%>
    <script type="text/javascript" language="javascript">
    function numericFilter(txb) {
   txb.value = txb.value.replace(/[^\0-9]/ig, "");
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
                    City Master</h3>
            </div>--%>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>
                                    Back Date Entry Settings Details</h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAddSettings" runat="server" Text="Add New Date" CssClass="btn_link" OnClick="btnAddSettings_Click"
                                        CausesValidation="false" /></div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                 <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        
                                     
                                       <tr>
                                            <td width="10%" class="field_title">From Date <span class="req">*</span></td>
                                            <td class="field_input" width="15%">
                                                                <asp:TextBox ID="txtFromDate" runat="server" Width="80%" Enabled="false" Font-Bold="true" />
                                                                <asp:ImageButton ID="imgFromDate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    Enabled="True" runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderFromDate" PopupButtonID="imgFromDate"
                                                                    Enabled="True" runat="server" TargetControlID="txtFromDate" CssClass="cal_Theme1"
                                                                    Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>

                                            </td>
                                           <td width="10%" class="field_title">To Date <span class="req">*</span></td>
                                            <td class="field_input" width="15%">
                                                                <asp:TextBox ID="txtToDate" runat="server" Width="80%" Enabled="false" Font-Bold="true" />
                                                                <asp:ImageButton ID="imgToDate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    Enabled="True" runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderToDate" PopupButtonID="imgToDate"
                                                                    Enabled="True" runat="server" TargetControlID="txtToDate" CssClass="cal_Theme1"
                                                                    Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>

                                            </td>
                                           <td width="10%" class="field_title">Allow Date <span class="req">*</span></td>
                                            <td class="field_input" width="15%">
                                                                <asp:TextBox ID="txtAllowDate" runat="server" Width="80%" Enabled="false" Font-Bold="true" />
                                                                <asp:ImageButton ID="imgAllowDate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    Enabled="True" runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderAllowDate" PopupButtonID="imgAllowDate"
                                                                    Enabled="True" runat="server" TargetControlID="txtAllowDate" CssClass="cal_Theme1"
                                                                    Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>

                                            </td>
                                            <td width="80" class="field_title" align="left">
                                                <asp:Label ID="Label6" Text="Year" runat="server"></asp:Label>
                                                <span class="req">*</span>
                                            </td>
                                            <td width="100" class="field_input" align="left">
                                                <asp:DropDownList ID="ddlYear" Width="150" runat="server" class="chosen-select" AppendDataBoundItems="True">
                                                </asp:DropDownList>
                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlYear"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="A"
                                                                    ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                            </td>
                                           </tr>
                                          <tr>
                                            
                                            <td width="110" class="field_title">
                                                <asp:Label ID="Label3" Text="Depot" runat="server"></asp:Label>
                                                <span class="req">*</span>
                                            </td>
                                            <td width="180" class="field_input">
                                                <%--<asp:ListBox ID="ddldepot" runat="server" SelectionMode="Multiple" TabIndex="4"
                                                            OnSelectedIndexChanged="ddldepot_OnSelectedIndexChanged" AutoPostBack="true"
                                                                Width="100%" AppendDataBoundItems="True"  name="options[]"
                                                                multiple="multiple"></asp:ListBox>--%>
                                                <asp:DropDownList ID="ddldepot" Width="170" runat="server" class="chosen-select" OnSelectedIndexChanged="ddldepot_OnSelectedIndexChanged" 
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                                           <asp:CustomValidator ID="CV_DDLDEPOT" runat="server" ValidateEmptyText="true"
                                                                    ControlToValidate="ddldepot" ValidationGroup="A" ErrorMessage="Required!" Display="Dynamic"
                                                                    ClientValidationFunction="ValidateListBox" ForeColor="Red" InitialValue="0"> </asp:CustomValidator>
                                                        </td>

              
                                           
                                            <td width="70" class="field_title" align="left">
                                                <asp:Label ID="Label7" Text="User" runat="server"></asp:Label>
                                                <span class="req">*</span>

                                            </td>
                                            <td width="170" class="field_input">
                                                <asp:DropDownList ID="ddlUser" Width="170" runat="server" class="chosen-select" AppendDataBoundItems="True">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlUser"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" 
                                                                    ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>

                                            </td>
                                            <td width="70" class="field_title" align="left">
                                                <asp:Label ID="Label1" Text="Depot" runat="server" Visible="false"></asp:Label>

                                            </td>
                                            
                                       
                                           
                                            <td style="padding:8px 0">
                                            <div class="btn_24_blue">
                                              <span class="icon disk_co"></span>
                                                <asp:Button ID="btnSaveCity" runat="server" Text="Save" CssClass="btn_link" OnClick="btnSaveCity_Click" ValidationGroup="A"/>
                                             </div>
                                             &nbsp;&nbsp;&nbsp;&nbsp;
                                             <div class="btn_24_blue">
                                                <span class="icon cross_octagon_co"></span>
                                                <asp:Button ID="btnCancelCity" runat="server" Text="Cancel" CssClass="btn_link" OnClick="btnCancelCity_Click" CausesValidation="false" />
                                             </div>
                                                <asp:HiddenField ID="Hdn_Fld" runat="server" />
                                            </td>
                                        </tr>
                                        </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlDisplay" runat="server">
                                <tr>
                                <td>
                             
                                     </td>
                                                </tr>
                                    <ul id="search_box">
                                        <li>
                                            <input name="" id="FilterTextBox" type="text" class="search_input" placeholder="Enter text to search the whole grid"
                                                onkeyup="FilterTextBox_KeyUp();" />
                                        </li>
                                        <li>
                                            <input name="" id="OboutButton2" type="submit" value="" class="search_btn" onclick="return performSearch();">
                                        </li>
                                    </ul>
                                    <div class="gridcontent">
                                    <cc1:Grid ID="gvCity" runat="server" CallbackMode="false" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                        AutoGenerateColumns="false" AllowPageSizeSelection="true" OnDeleteCommand="DeleteRecord" FolderExports="resources/exports" 
                                        AllowAddingRecords="false" AllowFiltering="true" PageSize="50" >
                                        <ExportingSettings ExportColumnsFooter="true" ExportGroupFooter="true" ExportGroupHeader="true" FileName="CityDetails" ExportAllPages="true" ExportDetails="true"/>
                                        <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnClientDelete="OnDelete" />
                                        <FilteringSettings InitialState="Visible" />
                                        <Columns>
                                            <cc1:Column ID="Column1" DataField="ID" ReadOnly="true" HeaderText="ID"
                                                runat="server" Visible="false" />
                                            <cc1:Column ID="Column2" DataField="AllowDate" HeaderText="Allow Date" runat="server"
                                                Width="200px">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>  
                                            <cc1:Column ID="Column4" DataField="FromDate" HeaderText="From Date" runat="server"
                                                Width="200px">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>  
                                            <cc1:Column ID="Column5" DataField="ToDate" HeaderText="To Date" runat="server"
                                                Width="200px">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>  
                                             <cc1:Column ID="Column3" DataField="USERNAME" HeaderText="USER NAME" runat="server"
                                                Width="200px">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>  
                                         
                                            <cc1:Column HeaderText="Delete" AllowEdit="false" AllowDelete="true" Width="80" >
                                                <TemplateSettings TemplateId="deleteBtnTemplate" />
                                            </cc1:Column>
                                        </Columns>
                                        <FilteringSettings MatchingType="AnyFilter" />                                       
                                        <Templates>
                                            <cc1:GridTemplate runat="server" ID="deleteBtnTemplate">
                                                <Template>
                                                    <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="gvCity.delete_record(this)">
                                                    </a>
                                                </Template>
                                            </cc1:GridTemplate>
                                        </Templates>
                                        <ScrollingSettings ScrollWidth="100%" ScrollHeight="290" />
                                    </cc1:Grid>
                       
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
                        document.getElementById("<%=Hdn_Fld.ClientID %>").value = record.USERID;
                        if (confirm("Are you sure you want to delete? "))
                            return true;
                        else
                            return false;
                    }
                    function OnDelete(record) {
                        alert(record.Error);
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

                        gvCity.addFilterCriteria('NAME', OboutGridFilterCriteria.Contains, searchValue);
                        gvCity.addFilterCriteria('SingleTracsAmt', OboutGridFilterCriteria.Contains, searchValue);
                        gvCity.addFilterCriteria('MultiTransAmt', OboutGridFilterCriteria.Contains, searchValue);
                        gvCity.executeFilter();
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>




