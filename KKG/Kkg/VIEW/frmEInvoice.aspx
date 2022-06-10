<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true"
    CodeFile="frmEInvoice.aspx.cs" Inherits="VIEW_frmEInvoice" %>

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
         <Triggers>
        
             <asp:PostBackTrigger ControlID="btnApprove" />

               <asp:PostBackTrigger ControlID="btnupdate" />
             
        </Triggers>
        <ContentTemplate>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>
                                    E-Invoice Approval</h6>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlADD" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_title" width="50">
                                                <asp:Label ID="Label1" runat="server" Text="Depot"></asp:Label>
                                                <span class="req">*</span>
                                            </td>
                                            <td class="field_input" width="130">
                                                <asp:DropDownList ID="ddlDepot" Width="120" runat="server" class="chosen-select"
                                                    data-placeholder="Choose Depot" AppendDataBoundItems="True" ValidationGroup="Save" 
                                                    OnSelectedIndexChanged="ddlDepot_SelectedIndexChanged"
                                                    >
                                                </asp:DropDownList>
                                            </td>
                                            <td class="field_title" width="55" runat="server" visible="false">
                                                
                                            </td>
                                            <td class="field_input" id="td1" runat="server" width="130" visible="false">
                                                
                                            </td>
                                            <td id="tdgroup" runat="server" class="field_title" width="55" visible="false">
                                                
                                            </td>
                                            <td class="field_input" id="tdBusinessSegment" runat="server" width="210" visible="false">
                                               
                                            </td>
                                            <td class="field_title" width="80">
                                                <asp:Label ID="Label16" runat="server" Text="(From-To)"></asp:Label>
                                                <span class="req">*</span>
                                            </td>
                                            <td class="field_input" width="95">
                                                <asp:TextBox ID="txtFromDate" runat="server" MaxLength="10" Width="60" Enabled="false" 
                                                    placeholder="dd/MM/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                    Font-Bold="true"></asp:TextBox>
                                                <asp:ImageButton ID="imgPopup" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                    runat="server" Height="24" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarFromDate" PopupButtonID="imgPopup" runat="server"
                                                    TargetControlID="txtFromDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td class="field_input" width="95">
                                                <asp:TextBox ID="txtToDate" runat="server" MaxLength="10" Width="60" Enabled="false" 
                                                    placeholder="dd/mm/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                    Font-Bold="true"></asp:TextBox>
                                                <asp:ImageButton ID="imgPopuptodate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                    runat="server" Height="24" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtenderToDate" PopupButtonID="imgPopuptodate"
                                                    runat="server" TargetControlID="txtToDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td class="field_input" width="110">
                                                <div class="btn_24_blue">
                                                    <span class="icon find_co"></span>
                                                    <asp:Button ID="btnSearchInvoice" runat="server" Text="Search" CssClass="btn_link"
                                                        ValidationGroup="Save" OnClick="btnSearchInvoice_Click" />
                                                </div>
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="gridcontent-inner" id="trinvoice" runat="server">
                                        <fieldset>
                                            <legend>INVOICE DETAILS</legend>
                                            <div style="overflow: scroll; height: 350px; width: 100%;">
                                                <asp:GridView ID="gvUnlockInvoice" runat="server" Width="100%" CssClass="reportgrid"
                                                    AutoGenerateColumns="false" ShowFooter="false" AlternatingRowStyle-BackColor="AliceBlue"
                                                    EmptyDataText="No Records Available">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkAllSelect" runat="server" onclick="CheckAllheader(this);" Text=" " />
                                                            </HeaderTemplate>
                                                            <HeaderStyle Width="25px" />
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" Text=" "  class="round" Style="float:left; padding-right:25px;" ToolTip='<%# Bind("SALEINVOICEID") %>' onclick="setrowcolor(this);" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SALEINVOICEID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSALEINVOICEID" runat="server" Text='<%# Bind("SALEINVOICEID") %>'
                                                                    value='<%# Eval("SALEINVOICEID") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sl.No." ItemStyle-Wrap="false" ItemStyle-Width="10px">
                                                            <ItemTemplate>
                                                                <nav style="position: relative;"><span class="badge black"><%# Container.DataItemIndex + 1 %></span></nav>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invoice No" ItemStyle-Width="160" ItemStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSALEINVOICENO" runat="server" Text='<%# Bind("INVOICENO") %>'
                                                                    ToolTip='<%# Bind("INVOICENO") %>' value='<%# Eval("INVOICENO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date" ItemStyle-Width="60" ItemStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSALEINVOICEDATE" runat="server" Text='<%# Bind("INVOICEDATE") %>'
                                                                    ToolTip='<%# Bind("INVOICEDATE") %>' value='<%# Eval("INVOICEDATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Customer" ItemStyle-Width="150" ItemStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCustomer" runat="server" Text='<%# Bind("CUSTOMER") %>'
                                                                    ToolTip='<%# Bind("CUSTOMER") %>' value='<%# Eval("CUSTOMER") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="PAN" ItemStyle-Width="60" ItemStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPan" runat="server" Text='<%# Bind("PAN") %>'
                                                                    ToolTip='<%# Bind("PAN") %>' value='<%# Eval("PAN") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="GST No" ItemStyle-Width="70" ItemStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGST" runat="server" Text='<%# Bind("GSTNO") %>'
                                                                    ToolTip='<%# Bind("GSTNO") %>' value='<%# Eval("GSTNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Pin No" ItemStyle-Width="50" ItemStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPin" runat="server" Text='<%# Bind("PIN") %>'
                                                                    ToolTip='<%# Bind("PIN") %>' value='<%# Eval("PIN") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                       
                                                        
                                                        <asp:TemplateField HeaderText="Basic Amount" ItemStyle-Width="60" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBILLTYPE" runat="server" Text='<%# Bind("AMOUNT") %>' ToolTip='<%# Bind("AMOUNT") %>'
                                                                    value='<%# Eval("AMOUNT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="SGST" DataField="TotalSGSTValue" HeaderStyle-Wrap="false"
                                                            ItemStyle-Width="80" ItemStyle-Wrap="false"  ItemStyle-HorizontalAlign="Right"/>
                                                        
                                                        <asp:BoundField HeaderText="CGST" DataField="TotalCGSTValue" HeaderStyle-Wrap="false"
                                                           ItemStyle-Width="80" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" />
                                                        
                                                        <asp:BoundField HeaderText="IGST" DataField="TotalIGSTValue" HeaderStyle-Wrap="false"
                                                           ItemStyle-Width="80" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" />
                                                       
                                                        <asp:BoundField HeaderText="Round Off" DataField="ROUNDOFF" ItemStyle-Width="60"
                                                            ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false" />

                                                        <asp:BoundField HeaderText="Net Amt" DataField="NETAMOUNT" ItemStyle-Width="80"
                                                            ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </fieldset>
                                    </div>
                                   

                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px; width:50%;">
                                                <div class="btn_24_green" id="divbtnapprove" runat="server">
                                                    <span class="icon approve_co"></span>
                                                    <asp:LinkButton ID="btnApprove" runat="server" Text="Proceed" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btnApprove_Click" OnClientClick="return confirm('Are you sure you want to make E-Invoice?')" />

                                                   


                                                </div>
                                               
                                                
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lbltotalcount" runat="server" Text="" Font-Bold="true" ForeColor="Gray"
                                                    Font-Size="Small"></asp:Label>
                                            </td>
                                            <td class="field_input" style="padding-left: 10px; width:50%;">
                                                <div >

                                                 
                                                 <asp:FileUpload ID="uploadfile" runat="server" />
                                                    <br />
                                                    <asp:LinkButton ID="btnupdate" runat="server" Text="Upload" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btnupdate_Click" OnClientClick="return confirm('Are you sure you want to update E-Invoice?')" />
                                                     </div>

                                            </td>
                                            <td class="field_input" style="padding-left: 10px; width:50%;">
                                                  

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
            <script type="text/javascript" language="javascript">
                function CheckAllheader(Checkbox) {
                    var GridVwHeaderCheckbox = document.getElementById("<%=gvUnlockInvoice.ClientID %>");
                    var lbltotalcount = document.getElementById("<%=lbltotalcount.ClientID %>");
                    var totalcount = 0;
                    if (Checkbox.checked) {
                        for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                            GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                            /*GridVwHeaderCheckbox.rows[i].style.color = '#389638';
                            GridVwHeaderCheckbox.rows[i].cells[0].style.border = '1px solid #fff';*/

                            totalcount = totalcount + 1;
                        }
                    }
                    else {
                        for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                            GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = false;
                            /*GridVwHeaderCheckbox.rows[i].style.color = '#4B555E';
                            GridVwHeaderCheckbox.rows[i].cells[0].style.border = '0px solid gray';*/
                        }
                    }
                    if (totalcount > 0) {
                        lbltotalcount.style.display = "";
                        lbltotalcount.innerHTML = 'You have selected : ' + totalcount + ' invoices.';
                    }
                    else {
                        lbltotalcount.style.display = "none";
                    }
                }
            </script>
            <script type="text/javascript">
                function setrowcolor(chb) {
                    if (chb.checked) {
                        /*chb.parentNode.parentNode.style.color = '#389638';
                        chb.parentNode.parentNode.style.border = '1px solid #fff';*/
                    }
                    else {
                        /*chb.parentNode.parentNode.style.color = '#4B555E';
                        chb.parentNode.parentNode.style.border = '0px solid gray';*/
                    }

                    var GridVwHeaderCheckbox = document.getElementById("<%=gvUnlockInvoice.ClientID %>");
                    var lbltotalcount = document.getElementById("<%=lbltotalcount.ClientID %>");
                    var totalcount = 0;

                    for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                        if (GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked == true) {
                            totalcount = totalcount + 1;
                        }
                    }
                    if (totalcount > 0) {
                        lbltotalcount.style.display = "";
                        lbltotalcount.innerHTML = 'You have selected : ' + totalcount + ' invoices.';
                    }
                    else {
                        lbltotalcount.style.display = "none";
                    }
                }
            </script>
            
            
            
            <script type="text/javascript">
                function CheckAllheaderinvHO(Checkbox) {
                    var GridVwHeaderCheckbox = document.getElementById("<%=gvUnlockInvoice.ClientID %>");
                    if (Checkbox.checked) {
                        for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                            GridVwHeaderCheckbox.rows[i].cells[2].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                            /*GridVwHeaderCheckbox.rows[i].style.color = '#389638';
                            GridVwHeaderCheckbox.rows[i].cells[2].style.border = '1px solid #fff';*/
                        }
                    }
                    else {
                        for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                            GridVwHeaderCheckbox.rows[i].cells[2].getElementsByTagName("INPUT")[0].checked = false;
                            /*GridVwHeaderCheckbox.rows[i].style.color = '#4B555E';
                            GridVwHeaderCheckbox.rows[i].cells[2].style.border = '0px solid gray';*/
                        }
                    }
                }
            </script>
            <script type="text/javascript">
                function CheckAllheaderinvREVERSE(Checkbox) {
                    var GridVwHeaderCheckbox = document.getElementById("<%=gvUnlockInvoice.ClientID %>");
                    if (Checkbox.checked) {
                        for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                            GridVwHeaderCheckbox.rows[i].cells[3].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                            /*GridVwHeaderCheckbox.rows[i].style.color = '#389638';
                            GridVwHeaderCheckbox.rows[i].cells[3].style.border = '1px solid #fff';*/
                        }
                    }
                    else {
                        for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                            GridVwHeaderCheckbox.rows[i].cells[3].getElementsByTagName("INPUT")[0].checked = false;
                            /*GridVwHeaderCheckbox.rows[i].style.color = '#4B555E';
                            GridVwHeaderCheckbox.rows[i].cells[3].style.border = '0px solid gray';*/
                        }
                    }
                }
            </script>
            <style type="text/css">
                .round
                {
                    position: relative;
                }
                
                .round label
                {
                    background-color: #fff;
                    border: 1px solid #ccc;
                    border-radius: 50%;
                    height: 16px;
                    left: 10px;
                    position: absolute;
                    bottom: 10;
                    width: 16px;
                }
                
                .round label:after
                {
                    border: 2px solid #fff;
                    border-top: none;
                    border-right: none;
                    content: "";
                    height: 6px;
                    left: 2px;
                    bottom: 3px;
                    opacity: 0;
                    position: absolute;
                    top: 2px;
                    transform: rotate(-45deg);
                    width: 10px;
                }
                
                .round input[type="checkbox"]
                {
                    visibility: hidden;
                }
                
                .round input[type="checkbox"]:checked + label
                {
                    background-color: #66bb6a;
                    border-color: #66bb6a;
                }
                
                .round input[type="checkbox"]:checked + label:after
                {
                    opacity: 1;
                }
                
                
                body
                {
                    background-color: #f1f2f3;
                    -webkit-box-align: center;
                    -ms-flex-align: center;
                    align-items: center;
                }
                
                .container
                {
                    margin: 0 auto;
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
