<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmJobOrderApproval.aspx.cs"
    Inherits="VIEW_frmJobOrderApproval" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                                    Jov Order Details</h6>
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
                                                    data-placeholder="Choose Transporter" AppendDataBoundItems="True" ValidationGroup="Save">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfv_ddlDepot" runat="server" ControlToValidate="ddlDepot"
                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="Save"
                                                    ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="field_title" width="50">
                                                <asp:Label ID="Label3" runat="server" Text="Vendor Name"></asp:Label>
                                            </td>
                                            <td width="140" class="field_input" id="td1" runat="server">
                                                <asp:DropDownList ID="ddlVendor" Width="140" runat="server" class="chosen-select"
                                                    data-placeholder="Choose Module" AppendDataBoundItems="True" >
                                                </asp:DropDownList>
                                            </td>
                                            <td class="field_title" width="105">
                                                <asp:Label ID="Label16" runat="server" Text="(From-To) Date"></asp:Label>
                                            </td>
                                            <td class="field_input" width="110">
                                                <asp:TextBox ID="txtFromDate" runat="server" MaxLength="10" Width="60" Enabled="false"
                                                    placeholder="dd/MM/yyyy" onkeypress="return isNumberKeyWithslash(event);" ValidationGroup="Search"
                                                    Font-Bold="true"></asp:TextBox>
                                                <asp:ImageButton ID="imgPopup" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                    runat="server" Height="24" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarFromDate" PopupButtonID="imgPopup" runat="server"
                                                    TargetControlID="txtFromDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td class="field_input" width="110">
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
                                        <tr id="trinvoice" runat="server">
                                            <td colspan="11" class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>INVOICE DETAILS</legend>
                                                    <div style="margin: 0 auto; width: 100%;">
                                                        <div style="overflow: hidden;" id="DivHeaderRow">
                                                        </div>
                                                        <div id="DivMainContent" style="overflow: scroll; height: 300px;" onscroll="OnScrollDiv(this);">
                                                            <asp:GridView ID="gvUnlockInvoice" runat="server" Width="100%" CssClass="reportgridchild" AutoGenerateColumns="false"
                                                                EmptyDataText="No Records Available">
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            <asp:CheckBox ID="chkAllSelect" runat="server" onclick="CheckAllheader(this);" Text=" " />
                                                                        </HeaderTemplate>
                                                                        <HeaderStyle Width="5px" />
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkSelect" runat="server" Text=" " class="round" Style="padding-left: 15px;"
                                                                            ToolTip='<%# Bind("DISTRIBUTORNAME") %>' onclick="setrowcolor(this);" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="SL" ItemStyle-Wrap="false" ItemStyle-Width="10px">
                                                                    <ItemTemplate>
                                                                        <nav style="position: relative;"><span class="badge black"><%# Container.DataItemIndex + 1 %></span></nav>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="SALEINVOICEID" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSALEINVOICEID" runat="server" Text='<%# Bind("SALEINVOICEID") %>'
                                                                                value='<%# Eval("SALEINVOICEID") %>' Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    
                                                                    <asp:TemplateField HeaderText="BILL NO">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSALEINVOICENO" runat="server" Text='<%# Bind("SALEINVOICENO") %>'
                                                                                value='<%# Eval("SALEINVOICENO") %>' Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    
                                                                    <asp:TemplateField HeaderText="INVOICE NO">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblINVOICENO" runat="server" Text='<%# Bind("INVOICENO") %>'
                                                                                value='<%# Eval("INVOICENO") %>' Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    
                                                                    <asp:TemplateField HeaderText="INVOICE DATE">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblINVOICEDATE" runat="server" Text='<%# Bind("INVOICEDATE") %>'
                                                                                value='<%# Eval("INVOICEDATE") %>' Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    
                                                                    <asp:TemplateField HeaderText="SALEINVOICE DATE">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSALEINVOICEDATE" runat="server" Text='<%# Bind("SALEINVOICEDATE") %>'
                                                                                value='<%# Eval("SALEINVOICEDATE") %>' Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    
                                                                    <asp:TemplateField HeaderText="PARTY">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDISTRIBUTORNAME" runat="server" Text='<%# Bind("DISTRIBUTORNAME") %>'
                                                                                value='<%# Eval("DISTRIBUTORNAME") %>' Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    
                                                                    <asp:TemplateField HeaderText="BASICVALUE">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblBASICVALUE" runat="server" Text='<%# Bind("BASICVALUE") %>'
                                                                                value='<%# Eval("BASICVALUE") %>' Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    
                                                                    <asp:TemplateField HeaderText="TOTALTAXAMNT">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTOTALTAXAMNT" runat="server" Text='<%# Bind("TOTALTAXAMNT") %>'
                                                                                value='<%# Eval("TOTALTAXAMNT") %>' Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField> 
                                                                    
                                                                    <asp:TemplateField HeaderText="NETAMOUNT">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblNETAMOUNT" runat="server" Text='<%# Bind("NETAMOUNT") %>'
                                                                                value='<%# Eval("NETAMOUNT") %>' Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                   
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                        <div id="DivFooterRow" style="overflow: hidden;">
                                                        </div>
                                                    </div>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="field_input" colspan="11" style="padding-left: 10px;">
                                                <div class="btn_24_green">
                                                    <span class="icon approve_co"></span>
                                                    <asp:Button ID="btnsave" runat="server" Text="APPROVE" cssclass="btn-primary"
                                                        OnClick="btnsave_Click" OnClientClick="return confirm('Are you sure you want to approve above invoices?')" />
                                                </div>
                                                 &nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lbltotalcount" runat="server" Text="" Font-Bold="true" ForeColor="Gray"
                                                    Font-Size="Small"></asp:Label>
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
                            /*GridVwHeaderCheckbox.rows[i].style.color = '#389638';*/
                            GridVwHeaderCheckbox.rows[i].cells[0].style.border = '1px solid #fff';

                            totalcount = totalcount + 1;
                        }
                    }
                    else {
                        for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                            GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = false;
                            /*GridVwHeaderCheckbox.rows[i].style.color = '#4B555E';*/
                            GridVwHeaderCheckbox.rows[i].cells[0].style.border = '0px solid gray';
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
                        /*chb.parentNode.parentNode.style.color = '#389638';*/
                        chb.parentNode.parentNode.style.border = '1px solid #fff';
                    }
                    else {
                        /*chb.parentNode.parentNode.style.color = '#4B555E';*/
                        chb.parentNode.parentNode.style.border = '0px solid gray';
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

