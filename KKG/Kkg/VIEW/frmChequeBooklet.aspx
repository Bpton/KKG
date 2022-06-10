<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmChequeBooklet.aspx.cs" Inherits="VIEW_frmChequeBooklet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    <script type="text/javascript" src="../js/jquery-ui.min.js"></script>
    <link href="../css/jquery-ui.min.css" rel="stylesheet" />
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
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">

        <ContentTemplate>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>CHEQUE BOOKLET
                                </h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAdd" runat="server" Text="Click to show voucher details" CssClass="btn_link"
                                        AccessKey="A" CausesValidation="false" OnClick="btnAdd_Click" />
                                </div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlDisplay" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                             <td class="field_title" width="30">Date
                                            </td>
                                            <td class="field_input" width="100">
                                                <asp:TextBox ID="txtstart" runat="server" MaxLength="10" Width="65" EnableViewState="true"
                                                    placeholder="dd/MM/yyyy"
                                                    Font-Bold="true" Enabled="false">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="imgbtn" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle" runat="server" Height="24" />
                                                <ajaxToolkit:CalendarExtender ID="Calendarstart" PopupButtonID="imgbtn" runat="server"
                                                    TargetControlID="txtstart" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                            <td class="field_title" width="40">Branch
                                            </td>
                                            <td class="field_input" width="140">
                                                <asp:DropDownList ID="ddldept" runat="server" class="chosen-select" Width="150"  AutoPostBack="true" OnSelectedIndexChanged="ddldept_SelectedIndexChanged">
                                                   </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                                    ControlToValidate="ddldept" ValidateEmptyText="false" SetFocusOnError="true"
                                                    InitialValue="0" ForeColor="Red" ValidationGroup="add">
                                                </asp:RequiredFieldValidator>
                                            </td>
                                            <td class="field_title" width="50">BANK NAME
                                            </td>
                                            <td class="field_input" width="140">
                                                <asp:DropDownList ID="ddlbank" runat="server" class="chosen-select" Width="150" OnSelectedIndexChanged="ddlbank_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfv_state" runat="server" ErrorMessage="*"
                                                    ControlToValidate="ddlbank" ValidateEmptyText="false" SetFocusOnError="true"
                                                    InitialValue="0" ForeColor="Red" ValidationGroup="add">
                                                </asp:RequiredFieldValidator>
                                            </td>

                                            <td>&nbsp;</td>
                                            <td class="field_title" width="50">Booklet No.
                                            </td>
                                            <td class="field_input" width="140">
                                                <asp:TextBox ID="txtBookletNo" runat="server" Width="120">
                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="CV_Name" runat="server" Display="None" ErrorMessage="*"
                                                    ControlToValidate="txtBookletNo" ValidateEmptyText="false" SetFocusOnError="true" ValidationGroup="add"> </asp:RequiredFieldValidator>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td class="field_title" width="50">Cheq From No.
                                            </td>
                                            <td class="field_input" width="120">
                                                <asp:TextBox ID="txtfromcheqno" runat="server" Width="100" onkeypress="javascript:return isNumber(event)" MaxLength="6">
                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None" ErrorMessage="*"
                                                    ControlToValidate="txtfromcheqno" ValidateEmptyText="false" SetFocusOnError="true" ValidationGroup="add"> </asp:RequiredFieldValidator>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td class="field_title" width="50">Cheq To No.
                                            </td>
                                            <td class="field_input" width="100">
                                                <asp:TextBox ID="txttocheqno" runat="server" Width="100" onkeypress="javascript:return isNumber(event)" MaxLength="6">
                                                </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None" ErrorMessage="*"
                                                    ControlToValidate="txttocheqno" ValidateEmptyText="false" SetFocusOnError="true" ValidationGroup="add"> </asp:RequiredFieldValidator>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnADDGrid" runat="server" Text="Save" CssClass="btn_link" OnClick="btnADDGrid_Click" ValidationGroup="add" OnClientClick="return validate();"/>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <div style="overflow: scroll; height: 350px;">
                                        <asp:GridView ID="grdDSR" runat="server" AutoGenerateColumns="false"
                                            Width="100%" Visible="true" ShowHeader="true" GridLines="Horizontal"
                                            CssClass="reportgrid">
                                            <Columns>
                                                 <asp:BoundField HeaderText="BANKID" DataField="BANKID" ItemStyle-Width="40%" Visible="false"/>
                                                <asp:BoundField HeaderText="BANK" DataField="BANKNAME" ItemStyle-Width="25%" />
                                               <asp:TemplateField HeaderText="BRANCH" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldepartmentid" runat="server" Text='<%# Eval("DEPARTMENTID") %>'></asp:Label>
                                                         <asp:Label ID="lbbookletno" runat="server" Text='<%# Eval("BOOKLETNO") %>' Width="250px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BRANCH" ItemStyle-HorizontalAlign="Left" Visible="true" HeaderStyle-Width="200">
                                                    <ItemTemplate >
                                                        <asp:Label ID="lbldepartment" runat="server" Text='<%# Eval("DEPARTMENT") %>' Width="250px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BOOKLET NO" ItemStyle-HorizontalAlign="Left" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbookletno" runat="server" Text='<%# Eval("BOOKLETNO_AUTO") %>' Width="250px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="CHEQUE FROM NO" DataField="CHEQUEFROMNO" ItemStyle-Width="15%" />
                                                <asp:BoundField HeaderText="CHEQUE TO NO" DataField="CHEQUETONO" ItemStyle-Width="15%" />
                                                <%--<asp:TemplateField HeaderText="EDIT" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btngrdedit" runat="server" CssClass="action-icons c-edit" ToolTip="Edit"
                                                            OnClick="btngrdedit_Click" CausesValidation="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="CANCEL" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btngrddelete" runat="server" CssClass="action-icons c-delete" OnClientClick="return confirm('Are you sure you want to Cancel?')"
                                                            ToolTip="Delete" OnClick="btngrddelete_Click" CausesValidation="false" />
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="isactive" DataField="ISACTIVE" ItemStyle-Width="10%"  runat="server"/>
                                            </Columns>

                                        </asp:GridView>
                                        <asp:HiddenField runat="server" ID="hdncancelID" />
                                    </div>

                                <%--    <div class="btn_24_blue">--%>
                                       <%-- <span class="icon disk_co"></span>--%>
                                        <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn_link" Visible="false" OnClick="btnsave_Click" />
                                   <%-- </div>--%>
                                </asp:Panel>

                            <div><cc1:MessageBox ID="MessageBox1" runat="server" /></div>
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <%--<legend>Invoice Details</legend>--%>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" style="border: none;">
                                                        <tr>
                                                            <td class="field_title" width="40">Branch Name&nbsp;<span class="req">*</span>
                                                            </td>
                                                             <td class="field_input" width="200">
                                                                <asp:DropDownList ID="ddlbranch" Width="190" runat="server" class="form-control chosen-select" ForeColor="#444"
                                                                    data-placeholder="Choose Branch" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlbranch_SelectedIndexChanged"
                                                                    AutoPostBack="true">
                                                                </asp:DropDownList>
                                                               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidatorTSI" runat="server" ControlToValidate="ddlbank1"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="Save"
                                                                    ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                            </td>
                                                            <td class="field_title" width="40">Bank Name&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" width="200">
                                                                <asp:DropDownList ID="ddlbank1" Width="190" runat="server" class="form-control chosen-select" ForeColor="#444"
                                                                    data-placeholder="Choose Bank" AppendDataBoundItems="True" 
                                                                    AutoPostBack="true">
                                                                </asp:DropDownList>
                                                               <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidatorTSI" runat="server" ControlToValidate="ddlbank1"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="Save"
                                                                    ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                            </td>
                                                            <td class="field_title" width="50">Booklet no.&nbsp;<span class="req">*</span>
                                                            </td>
                                                            <td class="field_input" width="210">
                                                                <asp:DropDownList ID="ddlbooklet1" runat="server" class="chosen-select" data-placeholder="Select" ForeColor="#444"
                                                                    AppendDataBoundItems="True" Width="200"  AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlbooklet1_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                              <%--  <asp:RequiredFieldValidator ID="CV_ddlbooklet1" runat="server" ControlToValidate="ddlbooklet1"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="Save"
                                                                    ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                                            </td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                    <div style="overflow: scroll; height: 350px;">
                                                        <asp:GridView ID="VouterGrdview" runat="server" AutoGenerateColumns="false"
                                                            Width="100%" Visible="true" ShowHeader="true" GridLines="Horizontal"
                                                            CssClass="reportgrid">
                                                            <Columns>
                                                            
                                                    <asp:TemplateField HeaderText="Sl.No." ItemStyle-HorizontalAlign="Left" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="AUTO BOOKLET NO." ItemStyle-HorizontalAlign="Left" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAutoBookletNo" runat="server" Text='<%# Eval("AutoBookletNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Bank Name" DataField="Ledger" ItemStyle-Width="40%" />
                                                   <asp:BoundField HeaderText="Party Name" DataField="PartyLedger" ItemStyle-Width="40%" />

                                                      <asp:TemplateField HeaderText="ChequeNo" ItemStyle-HorizontalAlign="Left"  ItemStyle-Width="20%">
                                                         <ItemTemplate>
                                                             <asp:Label ID="lblbank" runat="server" Text='<%# Eval("CheqNumber") %>'></asp:Label>
                                                        </ItemTemplate>
                                                      </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="BRANCH" ItemStyle-HorizontalAlign="Left" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbranch" runat="server" Text='<%# Eval("DEPARTMENT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="BOOKLETNO" ItemStyle-HorizontalAlign="Left" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbookletno1" runat="server" Text='<%# Eval("BOOKLETNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                
                                                              
                                                                <asp:BoundField HeaderText="ChequeDate" DataField="ChequeDate" ItemStyle-Width="20%" />
                                                                 <asp:BoundField HeaderText="Cheque Realised Date" DataField="ChequeRealisedDate" ItemStyle-Width="20%" />
                                                                <asp:BoundField HeaderText="Entry Date" DataField="EntryDate" ItemStyle-Width="20%" />
                                                                <asp:BoundField HeaderText="VoucherNo" DataField="VoucherNo" ItemStyle-Width="10%" />
                                                                <asp:BoundField HeaderText="amount" DataField="amount" ItemStyle-Width="10%" />
                                                      
                                                             <%--   <asp:TemplateField HeaderText="STATUS" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="ddlchqcancel" Width="120" runat="server" ForeColor="#444" OnSelectedIndexChanged="ddlchqcancel_SelectedIndexChanged" AutoPostBack="true">
                                                                            <asp:ListItem Text="None" Value="NO"></asp:ListItem>
                                                                            <asp:ListItem Text="Cancel" Value="N"></asp:ListItem>
                                                                            <asp:ListItem Text="Disposal" Value="Y"></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                            <%--    <asp:TemplateField HeaderText="REMARKS" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtremarks" runat="server" Style="text-align: left;" BackColor="White" BorderStyle="Solid"
                                                                            Width="120" Height="19" Enabled="false" MaxLength="60"/>
                                                                        
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>

                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                 <%--   <div class="btn_24_blue">--%>
                                                  <%--      <span class="icon disk_co"></span>--%>
                                                    <%--    <asp:Button ID="Btnsaveclick" runat="server" Text="Save" CssClass="btn_link" OnClick="btnsaveitem_Click"/>--%>
                                                        <asp:Button ID="btnSearchSave" runat="server" Text="Save" CssClass="btn_link" Visible="false" OnClick="btnSearchSave_Click" />

                                                       
                                                 <%--   </div>--%>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <div class="btn_24_blue">
                                                        <span class="icon disk_co"></span>
                                                          <asp:Button ID="Btncancelclick" runat="server" Text="Cancel" CssClass="btn_link" OnClick="btncancelitem_Click"/>
                                                    </div>
                                </asp:Panel>

                            </div>
                        </div>
                    </div>
                    <span class="clear"></span>
                </div>
                
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;

            return true;

        }
      
    </script>
    <script type="text/javascript">
        function validate()
        {
            
           
            
            if (parseInt(document.getElementById('<%= txtfromcheqno.ClientID %>').value) > parseInt(document.getElementById('<%= txttocheqno.ClientID %>').value))
            {
                alert("From number must be smaller than to number");
                
                return false;
            }
        else if (parseInt(document.getElementById('<%= txtfromcheqno.ClientID %>').value) == parseInt(document.getElementById('<%= txttocheqno.ClientID %>').value))
             { 
                alert("From number and to number won't similer");
                
                return false;
             }

      
           else 
                   
             {
            return true;
             }

           
        }

      
  
             
            </script>
  
</asp:Content>

