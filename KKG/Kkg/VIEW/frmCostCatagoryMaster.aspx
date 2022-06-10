<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmCostCatagoryMaster.aspx.cs" Inherits="VIEW_frmCostCatagoryMaster" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register assembly="ProudMonkey.Common.Controls" namespace="ProudMonkey.Common.Controls" tagprefix="cc1" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                    Category Master</h3>
            </div>--%>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>Cost Category Details</h6>
                              <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAddCatagory" runat="server" Text="Add New Record" CssClass="btn_link"
                                        OnClick="btnAddCatagory_Click" CausesValidation="false" /></div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td width="120" class="field_title">
                                                <asp:Label ID="lblcostcatcode" Text="Code" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtcostcatcode" runat="server" CssClass="mid" MaxLength="20"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="CV_catcode" runat="server" Display="None" ErrorMessage="Required!"
                                                            ControlToValidate="txtcostcatcode" ValidateEmptyText="false" OnServerValidate="CV_catcode_ServerValidate"
                                                            SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                                            TargetControlID="CV_catcode" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="lblcostcatname" Text="Name" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtcostcatname" runat="server" CssClass="mid" MaxLength="50"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="CV_catname" runat="server" Display="None" ErrorMessage="Required!"
                                                            ControlToValidate="txtcostcatname" ValidateEmptyText="false" OnServerValidate="CV_catname_ServerValidate" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                            TargetControlID="CV_catname" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="lblcostcatdescription" Text="Description" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtcostDescription" runat="server" CssClass="mid" onfocus="if(this.value==''){this.value=''}" MaxLength="50">   </asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="CV_Description" runat="server" Display="None" ErrorMessage="Description is required!"
                                                            ControlToValidate="txtcostDescription" ValidateEmptyText="false" OnServerValidate="CV_Description_ServerValidate" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                            TargetControlID="CV_Description" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_title">
                                                <asp:Label ID="Label1" Text="Active" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input">
                                                <asp:CheckBox ID="chkActive" runat="server" Text=" " />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td style="padding:8px 0px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btncatsubmit" runat="server" Text="Save" CssClass="btn_link" OnClick="btncatsubmit_click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btncatcancel" runat="server" Text="Cancel" CssClass="btn_link" OnClick="btncatcancel_click" CausesValidation="false" />
                                                </div>
                                                        <asp:HiddenField ID="Hdn_Fld" runat="server" />
                                            </td>
                                        </tr>                                   
                                    </table>
                                </asp:Panel>
                               
                               <asp:Panel ID="pnlCostCentreMapping" runat="server">
                               
                                     <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                            <tr>
                                                <td class="field_title" width="11%">
                                                    <asp:Label ID="lblcostcategory" runat="server" Text="COST CATEGORY" Enabled="false"></asp:Label>
                                                </td>
                                                <td class="field_input">
                                                    <asp:TextBox ID="txtcostcategory" runat="server" CssClass="required large" Enabled="false"
                                                        Width="250"></asp:TextBox>
                                                </td>
                                            </tr>
                                              <tr>
                                            <td colspan="2" class="field_input" style="padding-left: 10px;">
                                             <ul>
                                                        <li class="left">
                                                <fieldset>
                                                    <legend>COST CENTRE MAPPING</legend>
                                                    <div style="margin:0 auto; width:400px;">  
                                                     <div style="overflow: hidden;" id="DivHeaderRow">
                                                 </div>   
                                                    <div   id="DivMainContent"   style="overflow: scroll; height: 300px;" onscroll="OnScrollDiv(this);">
                                                       
                                                          <asp:GridView ID="gvCostCentreMapping" runat="server" Width="100%"  CssClass="zebra"
                                                    AutoGenerateColumns="false" EmptyDataText="No Records Available">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="COSTCENTREID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCOSTCENTREID" runat="server" Text='<%# Bind("COSTCENTREID") %>' value='<%# Eval("COSTCENTREID") %>'
                                                                    Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:BoundField HeaderText="COST CENTRE NAME" DataField="COSTCENTRENAME" ItemStyle-Width="200px"   />
                                                         
                                                         <asp:TemplateField>
                                                           <ItemTemplate>
                                                             <asp:CheckBox ID="chkSelect" runat="server" Text=" "   />
                                                           </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    </asp:GridView>
                                                    </div>
                                                     <div id="DivFooterRow" style="overflow:hidden;">
                                                     </div>
                                                     </div>
                                                 </fieldset>
                                                 </li>
                                                 </ul>
                                            </td>
                                        </tr>

                                          <tr>
                                            <td colspan="2" style="padding: 8px 10px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnCostCentreMappingSubmit" runat="server" Text="Save" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btnCostCentreMappingSubmit_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnCostCentreMappingCancel" runat="server" Text="Cancel" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btnCostCentreMappingCancel_Click" />
                                                </div>
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
                                    <cc1:Grid ID="grdcostcat" runat="server" CallbackMode="true" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                        AutoGenerateColumns="false" AllowPageSizeSelection="false" OnDeleteCommand="DeleteRecord"
                                        AllowAddingRecords="false" AllowFiltering="true" 
                                            onrowdatabound="grdcostcat_RowDataBound">
                                        <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnClientDelete="OnDelete" />
                                        <FilteringSettings InitialState="Visible" />
                                        <Columns>
                                            <cc1:Column DataField="COSTCATID" ReadOnly="true" HeaderText="COSTCATID" runat="server" Visible="false" />
                                            <cc1:Column DataField="COSTCATCODE" HeaderText="CODE" runat="server" Width="240">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="EqualTo" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="DoesNotContain" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column DataField="COSTCATNAME" HeaderText="NAME" runat="server"
                                                Width="240">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="EqualTo" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="DoesNotContain" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column DataField="COSTCATDESRIPTION" HeaderText="DESCRIPTION" runat="server" Width="280">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="EqualTo" />
                                                    <cc1:FilterOption Type="Contains" />
                                                    <cc1:FilterOption Type="DoesNotContain" />
                                                    <cc1:FilterOption Type="StartsWith" />
                                                     </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column  DataField="ACTIVE" HeaderText="ACTIVE" SortOrder="Asc" runat="server"
                                                Width="120">
                                                <FilterOptions>
                                                    <cc1:FilterOption Type="NoFilter" />
                                                    <cc1:FilterOption Type="Contains" />
                                                </FilterOptions>
                                            </cc1:Column>
                                            <cc1:Column DataField="CBU" HeaderText="CBU" SortOrder="Asc" runat="server" Width="140"
                                                Visible="false">
                                            </cc1:Column>
                                            <cc1:Column DataField="DTOC" HeaderText="DTOC" runat="server" Width="140" Visible="false">
                                            </cc1:Column>
                                            <cc1:Column AllowEdit="true" AllowDelete="true" HeaderText="Edit" runat="server"
                                                Width="80">
                                                <TemplateSettings TemplateId="editBtnTemplate" />
                                            </cc1:Column>
                                            <cc1:Column HeaderText="Delete" AllowEdit="false" AllowDelete="true" Width="80">
                                                <TemplateSettings TemplateId="deleteBtnTemplate" />
                                            </cc1:Column>
                                             <cc1:Column HeaderText="Cost Centre Mapping" AllowEdit="false" AllowDelete="true" Width="80" Wrap="true">
                                                <TemplateSettings TemplateId="CostCentreMappingTemplate" />
                                            </cc1:Column>
                                        </Columns>
                                        <FilteringSettings MatchingType="AnyFilter" />
                                         <Templates>
                                                    <cc1:GridTemplate runat="server" ID="editBtnTemplate" >
                                                    <Template>
                                                    <a href="javascript: //" class="action-icons c-edit" title="Edit" id="btnGridEdit_<%# Container.PageRecordIndex %>"
                                                        onclick="CallServerMethod(this)"  >
                                                    </a>                                             
                                                    </Template>
                                                    </cc1:GridTemplate>
                                        </Templates>
                                         <Templates>
                                                    <cc1:GridTemplate runat="server" ID="deleteBtnTemplate" >
                                                    <Template>
                                                    <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="grdcostcat.delete_record(this)"  >
                                                    </a>                                             
                                                    </Template>
                                                    </cc1:GridTemplate>
                                        </Templates>
                                          <Templates>
                                                <cc1:GridTemplate runat="server" ID="CostCentreMappingTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="h_icon map" title="Brand Mapping "
                                                            onclick="OpenCostcentreMapping('<%# Container.DataItem["COSTCATNAME"] %>','<%# Container.DataItem["COSTCATID"] %>')" />
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>

                                        <ScrollingSettings ScrollWidth="100%" ScrollHeight="290" />
                                    </cc1:Grid>
                                     <asp:Button ID="btngridedit" runat="server" Text="GridSave" Style="display: none"
                                            OnClick="btngridedit_Click" CausesValidation="false" />
                                      <asp:Button ID="btnCostCentremapping" runat="server" Text="Centre Mapping " Style="display: none"
                                            OnClick="btnCostCentremapping_Click" CausesValidation="false" />
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
                        document.getElementById("<%=Hdn_Fld.ClientID %>").value = record.COSTCATID;
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
                        //         var searchValue = FilterTextBox.value();
                        if (searchValue == FilterTextBox.WatermarkText) {
                            searchValue = '';
                        }

                        grdcostcat.addFilterCriteria('COSTCATCODE', OboutGridFilterCriteria.Contains, searchValue);
                        grdcostcat.addFilterCriteria('COSTCATNAME', OboutGridFilterCriteria.Contains, searchValue);
                        grdcostcat.addFilterCriteria('COSTCATDESRIPTION', OboutGridFilterCriteria.Contains, searchValue);
                        grdcostcat.executeFilter();

                        searchTimeout = null;

                        return false;
                    }
                </script>
                <script type="text/javascript">
                    function CallServerMethod(oLink) {
                        
                        var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                        document.getElementById("<%=Hdn_Fld.ClientID %>").value = grdcostcat.Rows[iRecordIndex].Cells[0].Value;
                        document.getElementById("<%=btngridedit.ClientID %>").click();
                    }

                    function OpenCostcentreMapping(name, id) {
                        document.getElementById("<%=Hdn_Fld.ClientID %>").value = id;
                        document.getElementById("<%=txtcostcategory.ClientID %>").value = name;
                        document.getElementById("<%=btnCostCentremapping.ClientID %>").click();
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

