<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmGroupTypeMaster.aspx.cs" Inherits="VIEW_frmGroupTypeMaster" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    </script>
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="../images/loading123.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <%--<div class="page_title">
                <span class="title_icon"><span class="computer_imac"></span></span>
                <h3>Beat Master</h3>
            </div>--%>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>Group Type datails
                                </h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAddnewRecord" runat="server" Text="Add New Record" CssClass="btn_link"
                                        OnClick="btnAddnewRecord_Click" CausesValidation="false" />
                                </div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td width="120" align="left" class="field_title">
                                                <asp:Label ID="lblCode" Text="Code" runat="server"></asp:Label>

                                            </td>
                                            <td align="left" valign="top" class="field_input">
                                                <asp:TextBox ID="txtCode" runat="server" CssClass="mid" MaxLength="20" Enabled="false"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator ID="CV_txtCode" runat="server" Display="None" ErrorMessage=" Code is required!"
                                                    ControlToValidate="txtCode" ValidateEmptyText="false" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender19" runat="server"
                                                    TargetControlID="CV_txtCode" PopupPosition="Right" HighlightCssClass="errormassage"
                                                    WarningIconImageUrl="../images/050.png">
                                                </ajaxToolkit:ValidatorCalloutExtender>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="field_title">
                                                <asp:Label ID="lblName" Text="Name" runat="server"></asp:Label>
                                                <span class="req">*</span>
                                            </td>
                                            <td align="left" class="field_input">
                                                <asp:TextBox ID="txtName" runat="server" CssClass="mid"> </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="CV_Name" runat="server" Display="None" ErrorMessage="Type is required!"
                                                    ControlToValidate="txtName" ValidateEmptyText="false" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                    TargetControlID="CV_Name" PopupPosition="Right" HighlightCssClass="errormassage"
                                                    WarningIconImageUrl="../images/050.png">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="field_title">
                                                <asp:Label ID="lblDescription" Text="Description" runat="server"></asp:Label>
                                                <span class="req">*</span>
                                            </td>
                                            <td align="left" class="field_input">
                                                <asp:TextBox ID="txtDescription" runat="server" CssClass="mid"> </asp:TextBox>
                                                <asp:RequiredFieldValidator ID="CV_Description" runat="server" Display="None" ErrorMessage="Type is required!"
                                                    ControlToValidate="txtDescription" ValidateEmptyText="false" SetFocusOnError="true"> </asp:RequiredFieldValidator>
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
                                            <td></td>
                                            <td style="padding: 8px 0px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnGroupTypeSubmit" runat="server" Text="Save" CssClass="btn_link" OnClick="btnGroupTypeSubmit_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnGroupTypeCancel" runat="server" Text="Cancel" CssClass="btn_link" OnClick="btnGroupTypeCancel_Click"
                                                        CausesValidation="false" />
                                                </div>
                                                <asp:HiddenField ID="Hdn_Fld" runat="server" />

                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                                <asp:Panel ID="pnlDisplay" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td width="60" class="field_title">SEARCH</td>
                                            <td width="360" class="field_input">
                                                <asp:TextBox ID="txtnamesearch" runat="server" placeholder="Search By Group Name......." Width="350"></asp:TextBox>
                                            </td>
                                            <td width="150" class="field_input">
                                                <div class="btn_24_blue">
                                                    <span class="icon find_co"></span>
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn_link" ValidationGroup="Search"
                                                      OnClick="btnSearch_Click"   />
                                                </div>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                   <div style="overflow: hidden; padding-left: 8px; width: 100%;" id="DivHeaderRow_voucher">
                                    </div>
                                    <div style="overflow: scroll; padding-left: 8px; border: 1px solid #ccc;" onscroll="OnScrollDiv_voucher(this)" id="DivMainContent_voucher">
                                        <asp:GridView ID="gvAdvType" runat="server" Width="100%" ShowFooter="true" AutoGenerateColumns="false"
                                            CssClass="reportgrid">
                                            <Columns>
                                                <asp:TemplateField HeaderText="GROUP_TYPEID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGROUP_TYPEID" runat="server" Text='<%# Bind("GROUP_TYPEID") %>' value='<%# Eval("GROUP_TYPEID") %>'
                                                            Visible="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CODE" Visible="TRUE" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGROUP_CODE" runat="server" Text='<%# Bind("GROUP_CODE") %>' value='<%# Eval("GROUP_CODE") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NAME" Visible="TRUE" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGROUP_TYPENAME" runat="server" Text='<%# Bind("GROUP_TYPENAME") %>' value='<%# Eval("GROUP_TYPENAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description" Visible="TRUE" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDESCRIPTIONS" runat="server" Text='<%# Bind("DESCRIPTIONS") %>' value='<%# Eval("DESCRIPTIONS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="STATUS" Visible="TRUE" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSTATUS" runat="server" Text='<%# Bind("STATUS") %>' value='<%# Eval("STATUS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EDIT" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btngrdedit" runat="server" CausesValidation="false" class="action-icons c-edit"
                                                            OnClick="btngrdedit_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DELETE" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btngrddelete" runat="server" CausesValidation="false"
                                                            class="action-icons c-delete" OnClick="btngriddelete_Click"
                                                            OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                   </div>
                                    <div id="DivFooterRow_voucher" style="overflow: hidden">
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
              <script language="javascript" type="text/javascript">
                function MakeStaticHeader_voucher(gridId, height, width, headerHeight, isFooter) {
                    var tbl = document.getElementById(gridId);
                    if (tbl) {
                        var DivHR_inv = document.getElementById('DivHeaderRow_voucher');
                        var DivMC_inv = document.getElementById('DivMainContent_voucher');
                        var DivFR_inv = document.getElementById('DivFooterRow_voucher');

                        //*** Set divheaderRow Properties ****
                        DivHR_inv.style.height = headerHeight + 'px';
                        //DivHR_inv.style.width = (parseInt(width) - 16) + 'px';
                        DivHR_inv.style.width = '98%';
                        DivHR_inv.style.position = 'relative';
                        DivHR_inv.style.top = '0px';
                        DivHR_inv.style.zIndex = '10';
                        DivHR_inv.style.verticalAlign = 'top';

                        //*** Set divMainContent Properties ****
                        DivMC_inv.style.width = width + 'px';
                        DivMC_inv.style.height = height + 'px';
                        DivMC_inv.style.position = 'relative';
                        DivMC_inv.style.top = -headerHeight + 'px';
                        DivMC_inv.style.zIndex = '1';

                        //*** Set divFooterRow Properties ****
                        DivFR_inv.style.width = (parseInt(width) - 16) + 'px';
                        DivFR_inv.style.position = 'relative';
                        DivFR_inv.style.top = -headerHeight + 'px';
                        DivFR_inv.style.verticalAlign = 'top';
                        DivFR_inv.style.paddingtop = '2px';

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
                            DivFR_inv.appendChild(tblfr);
                        }
                        //****Copy Header in divHeaderRow****
                        DivHR_inv.appendChild(tbl.cloneNode(true));
                    }
                }

                function OnScrollDiv_voucher(Scrollablediv) {
                    document.getElementById('DivHeaderRow_voucher').scrollLeft = Scrollablediv.scrollLeft;
                    document.getElementById('DivFooterRow_voucher').scrollLeft = Scrollablediv.scrollLeft;
                }
            </script>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>