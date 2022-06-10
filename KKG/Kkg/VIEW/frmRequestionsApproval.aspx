<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmRequestionsApproval.aspx.cs" Inherits="VIEW_frmRequestionsApproval" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <style>
        th {
            background: cornflowerblue !important;
            color: white !important;
            position: sticky !important;
            top: 0;
            box-shadow: 0 2px 2px -1px rgb(255, 106, 0);
            width: 400px;
        }

        th, td {
            padding: 0.25rem;
        }
    </style>

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
            <asp:Image ID="Image1" ImageUrl="../images/103.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="page_title">
                <span class="title_icon"><span class="computer_imac"></span></span>
                <h3>Requisition Approve</h3>
            </div>
            <asp:HiddenField ID="bulkRequistionId" runat="server"  />
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                               <asp:Panel ID="pnlDisplay" runat="server">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                             
                                    <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                        <span class="icon add_co"></span>
                                        <asp:Button ID="btnAdd" runat="server" Text="Add New Record" CssClass="btn_link" OnClick="btnAdd_Click"
                                            CausesValidation="false" />
                                    </div>
                               
                                <h6>REQUESTION Details</h6>
                            </div>
                                    <div class="widget_content">
                                         <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                     <tr>
                                           <td class="field_input">
                                                        <td class="field_title">FROM DATE
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="165">
                                                            <asp:TextBox ID="txtSearchFromDate" runat="server" MaxLength="10" Width="80" Enabled="false"
                                                                placeholder="dd/MM/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="ImageSearch2" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarImageSearch2Extender2" PopupButtonID="ImageSearch2" runat="server"
                                                                TargetControlID="txtSearchFromDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td class="field_title">TO DATE
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="165">
                                                            <asp:TextBox ID="txtSearchToDate" runat="server" MaxLength="10" Width="80" Enabled="false"
                                                                placeholder="dd/mm/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="ImageSearchButton3" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarSearchExtender3" PopupButtonID="ImageSearchButton3"
                                                                runat="server" TargetControlID="txtSearchToDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>

                                                        <td width="110" valign="top">
                                                            <div class="btn_24_blue">
                                                                <span class="icon find_co"></span>
                                                                <asp:Button ID="btnFind" runat="server" Text="Search" CssClass="btn_link"
                                                                    ValidationGroup="Search" OnClick="btnFind_Click" />
                                                            </div>
                                                        </td>
                                         </td>
                                                    </tr>
                                             </table>
                                        </div>
                                        <asp:GridView ID="grdLoadRequistion" runat="server" Width="90%" CssClass="zebra"
                                                        AutoGenerateColumns="false" ShowFooter="true" AlternatingRowStyle-BackColor="AliceBlue"
                                                        EmptyDataText="No Records Available">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL NO" SortExpression="HIGHLIGHT" >
                                                                <ItemTemplate>
                                                                      <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                 </asp:TemplateField>
                                                <asp:TemplateField HeaderText="REQUISTIONID" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblREQUISTIONID" runat="server" Text='<%# Bind("REQUISTIONID") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="BULKPRIMARYITEMNAME" >
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBULKPRIMARYITEMNAME" Width="300px" runat="server" Text='<%# Bind("BULKPRIMARYITEMNAME") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField> 
                                                <asp:TemplateField HeaderText="BULKREQUISTIONNUM" >
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBULKREQUISTIONNUM" runat="server" Width="150px" Text='<%# Bind("REQUISTIONNUM") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CREATEDDATE" >
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCREATEDDATE" runat="server" Width="80px" Text='<%# Bind("CREATEDDATE") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                   <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnGridEdit" runat="server" OnClick="btnGridEdit_Click" class="action-icons c-edit " />

                                                                </ItemTemplate>
                                                            </asp:TemplateField>




                                                </Columns>

                                        </asp:GridView>


                                    </asp:Panel>

                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                     <div id="entryNumber" runat="server" style="display:none">
                                                         <tr>
                                                        <td class="field_title">
                                                            <asp:Label ID="Label5" runat="server" Text="Enrty Number"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td class="field_input" width="30%">
                                                            <asp:TextBox ID="txtEnrtyNumber" Width="350px" runat="server"  Enabled="false" CssClass="mid" ToolTip="Enter Po Number"> </asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    </div>
                                                    <tr>
                                                        <td class="field_title">DEPARTMENT
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="23%">
                                                            <asp:DropDownList ID="ddlDepartMent" runat="server" data-placeholder="Select DEPARTMENT" AutoPostBack="true"
                                                                AppendDataBoundItems="True" Width="250" class="chosen-select">
                                                            </asp:DropDownList>
                                                        </td>

                                                         <td class="field_title">TO DEPARTMENT
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="23%">
                                                            <asp:DropDownList ID="ddlToDepartment" runat="server" data-placeholder="Select DEPARTMENT" AutoPostBack="true"
                                                                AppendDataBoundItems="True" Width="250" class="chosen-select" OnSelectedIndexChanged="ddlToDepartment_SelectedIndexChanged" >
                                                            </asp:DropDownList>
                                                        </td>

                                                        <td class="field_title">TO STORELOCATION
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="23%">
                                                            <asp:DropDownList ID="ddlStoreLocation" runat="server" data-placeholder="Select STORELOCATION" AutoPostBack="true"
                                                                AppendDataBoundItems="True" Width="250" class="chosen-select" OnSelectedIndexChanged="ddlStoreLocation_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                    <tr>
                                                        <td class="field_title">FROM DATE
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="165">
                                                            <asp:TextBox ID="txtFromDate" runat="server" MaxLength="10" Width="80" Enabled="false"
                                                                placeholder="dd/MM/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="ImageButton1" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton1" runat="server"
                                                                TargetControlID="txtFromDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                                        <td class="field_title">TO DATE
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="165">
                                                            <asp:TextBox ID="txtToDate" runat="server" MaxLength="10" Width="80" Enabled="false"
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
                                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn_link"
                                                                    ValidationGroup="Search" OnClick="btnSearch_Click" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                        </tr>
                                        </tr>
                                    </table>


                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td>
                                                <div style="overflow: scroll; height: 200px; width: 90%;">
                                                    <asp:GridView ID="grdRequisition" runat="server" Width="90%" CssClass="zebra"
                                                        AutoGenerateColumns="false" ShowFooter="true" AlternatingRowStyle-BackColor="AliceBlue"
                                                        EmptyDataText="No Records Available">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="CHECK BOX" SortExpression="HIGHLIGHT" >
                                                                <EditItemTemplate>
                                                                    <asp:CheckBox ID="PurchaseOrderCheckBox" runat="server" Width="5px" />
                                                                </EditItemTemplate>
                                                                <ItemTemplate>
                                                                  <asp:CheckBox ID="PurchaseOrderCheckBox" runat="server" Width="25px" />
                                                                      <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                            <asp:TemplateField HeaderText="REQUISITIONID" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblREQUISITIONID" runat="server" Text='<%# Bind("REQUISITIONID") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MATERIALID" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMATERIALID" runat="server" Text='<%# Bind("MATERIALID") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MATERIALNAME" >
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMATERIALNAME" runat="server" Text='<%# Bind("MATERIALNAME") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="PRORDERNUM" >
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPRORDERNUM" runat="server" Text='<%# Bind("PRORDERNUM") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="REQUISITIONDATE">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblREQUISITIONDATE" runat="server" Text='<%# Bind("REQUISITIONDATE") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                           
                                                            <asp:TemplateField HeaderText="REQUISITIONNO" HeaderStyle-ForeColor="#ff0066">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblREQUISITIONNO" Width="250px" runat="server" Text='<%# Bind("REQUISITIONNO") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="DEPTNAME">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDEPTNAME" Width="60px" runat="server" Text='<%# Bind("DEPTNAME") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>

                                                <div class="btn_30_light" style="float: left;" id="Div1" runat="server">
                                                    <span class="icon add_co"></span>
                                                    <asp:Button ID="btnRequisitionDetails" runat="server" Text="Add Details" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btnRequisitionDetails_Click" />
                                                </div>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <div style="overflow: scroll; height: 200px; width: 90%;">

                                                    <asp:GridView ID="grdRequistionDetails" runat="server" Width="90%" CssClass="zebra"
                                                        AutoGenerateColumns="false" ShowFooter="true" AlternatingRowStyle-BackColor="AliceBlue"
                                                        EmptyDataText="No Records Available">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SLNO">
                                                                <ItemTemplate>
                                                                    <%# Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                          
                                                            <asp:TemplateField HeaderText="MATERIALID" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMATERIALID" runat="server" Text='<%# Bind("MATERIALID") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MATERIALNAME">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMATERIALNAME" Width="250px" runat="server" Text='<%# Bind("MATERIALNAME") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="UOMID" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUOMID" runat="server" Text='<%# Bind("UOMID") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="UOMNAME">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUOMNAME" Width="50px" runat="server" Text='<%# Bind("UOMNAME") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="QTY">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQTY" Width="50px" runat="server" Text='<%# Bind("QTY") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="BUFFERQTY">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBUFFERQTY" Width="50px" runat="server" Text='<%# Bind("BUFFERQTY") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="NETQTY">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNETQTY" Width="50px" runat="server" Text='<%# Bind("NETQTY") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="STOCKQTY">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSTOCKQTY" Width="50px" runat="server" Text='<%# Bind("STOCKQTY") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                             <asp:TemplateField HeaderText="REQUIREDQTY">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtREQUIREDQTY" Width="50px" runat="server" Text='<%# Bind("REQUIREDQTY") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="STORELOCATION_STOCKQTY">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtSTORELOCATION_STOCKQTY" Width="50px" Enabled="false" runat="server" Text='<%# Bind("STORELOCATION_STOCKQTY") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="STORELOCATION WISE STOCK" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnViewStock" runat="server" OnClick="btnViewStock_Click" class="action-icons c-edit " />

                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>

                                                <div id="divStoreLocationWiseStock" runat="server" style="display: none">
                                                    <td style="padding-top: 10px;">
                                                        <asp:GridView ID="grdStoreWiseStock" runat="server" AutoGenerateColumns="true" EmptyDataRowStyle-Font-Bold="true"
                                                            EmptyDataRowStyle-VerticalAlign="Middle" CssClass="record_b" Width="150px" Height="75px"
                                                            EmptyDataRowStyle-Font-Size="Medium">
                                                        </asp:GridView>
                                                    </td>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="btn_24_green" style="float: none;">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btnSubmit_Click" />
                                                </div>
                                    <td>&nbsp;</td>
                                    <div class="btn_24_red" style="float: none;">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnCancel" runat="server" Text="Close" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btnCancel_Click" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

