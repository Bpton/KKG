<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmFinalChequeBookletCancel.aspx.cs" Inherits="VIEW_frmFinalChequeBookletCancel" %>

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
        <ContentTemplate>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>
                                    CHEQUE BOOK DETAILS </h6>
                            </div>
                            <div class="widget_content">
                               <%-- <asp:Panel ID="pnlADD" runat="server">--%>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                           <td class="field_title" width="120">
                                              <asp:Label ID="Label7" runat="server" Text="Bank Name"></asp:Label>
                                           </td>
                                            <td class="field_input" width="150">
                                                <asp:DropDownList ID="ddlBankName" runat="server" AppendDataBoundItems="true" Width="200" Height="28"
                                                  class="chosen-select" data-placeholder="Choose" ValidationGroup="ADD" AutoPostBack="true" OnSelectedIndexChanged="ddlBankName_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="field_title" width="120">
                                              <asp:Label ID="Label1" runat="server" Text="Booklet No."></asp:Label>
                                           </td>
                                           <td class="field_input" width="200">
                                           <asp:DropDownList ID="ddlBookletNo" runat="server" AppendDataBoundItems="true" Width="170" Height="28"
                                            class="chosen-select" data-placeholder="Choose" ValidationGroup="ADD">
                                             </asp:DropDownList>
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
                                 <%--   <div class="gridcontent-inner" id="trvoucher" runat="server">--%>
                                        <fieldset>
                                            <legend>CHEQUE DETAILS</legend>
                                            <div style="overflow: scroll; height: 350px; width: 100%;">
                                               <asp:GridView ID="gvUnlockVoucher" runat="server" Width="100%" CssClass="reportgrid"
                                                    AutoGenerateColumns="false" ShowFooter="false" AlternatingRowStyle-BackColor="AliceBlue"
                                                    OnRowDataBound="gvUnlockVoucher_RowDataBound"
                                                    EmptyDataText="No Records Available">
                                                    <Columns>
                                                           <asp:TemplateField HeaderText="SL" ItemStyle-Wrap="false" ItemStyle-Width="10px">
                                                            <ItemTemplate>
                                                                <nav style="position: relative;"><span class="badge black"><%# Container.DataItemIndex + 1 %></span></nav>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                              
                                                            </HeaderTemplate>
                                                            <HeaderStyle Width="25px" />
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelectvou" runat="server" Text=" " class="round" Style="padding-left: 25px;"
                                                                    ToolTip='<%# Bind("AutoBookletNo") %>' onclick="setrowcolorvou(this);" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                     <%--   <asp:TemplateField HeaderText="AccEntryID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAccEntryID" runat="server" Text='<%# Bind("AccEntryID") %>' value='<%# Eval("AccEntryID") %>'
                                                                    Visible="false"></asp:Label>
                                                                 <asp:Label ID="lblISTRANSFERTOHO" runat="server" Text='<%# Bind("ISTRANSFERTOHO") %>' value='<%# Eval("ISTRANSFERTOHO") %>'
                                                                    Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                     
                                                       
                                                        <asp:TemplateField HeaderText="Auto Booklet No" ItemStyle-Width="160" ItemStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LBLAutoBookletNo" runat="server" Text='<%# Bind("AutoBookletNo") %>' ToolTip='<%# Bind("AutoBookletNo") %>'
                                                                    value='<%# Eval("AutoBookletNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bank Name" ItemStyle-Width="160" ItemStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBankName" runat="server" Text='<%# Bind("BankName") %>' ToolTip='<%# Bind("BankName") %>'
                                                                    value='<%# Eval("BankName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Party Name" DataField="PartyLedger" ItemStyle-Width="15%" />
                                                         <asp:TemplateField HeaderText="Booklet NO" ItemStyle-Width="160" ItemStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBOOKLETNO" runat="server" Text='<%# Bind("BOOKLETNO") %>' ToolTip='<%# Bind("BOOKLETNO") %>'
                                                                    value='<%# Eval("BOOKLETNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                         <asp:TemplateField HeaderText="Cheqe Number" ItemStyle-Width="160" ItemStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCheqNumber" runat="server" Text='<%# Bind("CheqNumber") %>' ToolTip='<%# Bind("CheqNumber") %>'
                                                                    value='<%# Eval("CheqNumber") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                      
                                                        <asp:BoundField HeaderText="Cheque Date" DataField="ChequeDate" HeaderStyle-Wrap="false" ItemStyle-Width="10%"/>
                                                        <asp:BoundField HeaderText="Entry Date" DataField="EntryDate" ItemStyle-Width="70"
                                                            ItemStyle-Wrap="false" ItemStyle-ForeColor="Blue" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField HeaderText="Voucher No" DataField="VoucherNo" HeaderStyle-Wrap="false" ItemStyle-Width="10%"/>
                                                        <asp:BoundField HeaderText="Amount" DataField="amount" HeaderStyle-Wrap="false" ItemStyle-Width="10%"/>
                                                           <asp:TemplateField HeaderText="REMARKS" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtremarks" runat="server" Style="text-align: left;" BackColor="White" BorderStyle="Solid"
                                                                            Width="220" Height="19" Enabled="true" MaxLength="60"/>
                                                                        
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                        
                                                                                                       
                                                     
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </fieldset>
                                  <%--  </div>--%>

                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px; width:50%;">
                                                <div class="btn_24_green" id="divbtnapprove" runat="server">
                                                    <span class="icon approve_co"></span>
                                                    <asp:LinkButton ID="btnApprove" runat="server" Text="APPROVED" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btnApprove_Click" OnClientClick="return confirm('Are you sure you want to APPROVED above Cheque?')" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                               <%-- <div class="btn_24_red" id="divbtnreject" runat="server">
                                                    <span class="icon reject_co"></span>
                                                    <asp:LinkButton ID="btnReject" runat="server" Text="Disposal" CssClass="btn_link" CausesValidation="false"
                                                        OnClick="btnReject_Click" OnClientClick="return confirm('Are you sure you want to Disposal above cheque?')" />
                                                </div>--%>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnCancel" runat="server" Text="Back" CssClass="btn_link" CausesValidation="false"
                                                        OnClick="btnCancel_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lbltotalcount" runat="server" Text="" Font-Bold="true" ForeColor="Gray"
                                                    Font-Size="Small"></asp:Label>
                                            </td>
                                        
                                        </tr>
                                    </table>
                              <%--  </asp:Panel>--%>
                            </div>
                        </div>
                    </div>
                    <cc1:MessageBox ID="MessageBox1" runat="server" />
                    <span class="clear"></span>
                </div>
                <span class="clear"></span>
            </div>
            
           
         
           
 
            
          
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
          
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



