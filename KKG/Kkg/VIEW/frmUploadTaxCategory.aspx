<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmUploadTaxCategory.aspx.cs" Inherits="VIEW_frmUploadTaxCategory" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
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
            <asp:Image ID="Image1" ImageUrl="../images/103.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
        <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
             <asp:PostBackTrigger ControlID="btngeneraltemp" />
        </Triggers>
         <ContentTemplate>
               
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>
                                    Tax Upload Category Wise</h6>
                            </div>

                              <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                    <tr>
                                       <td class="field_input"  colspan="4">
                                                 <div class="btn_24_blue">
                                                <span class="icon doc_excel_table_co"></span>
                                               <asp:Button ID="btngeneraltemp" runat="server"  Text="Generate Template" CssClass="btn_link" 
                                               OnClick="btngeneraltemp_Click"/>
                                            </div>      
                                            </td>

                                    </tr>
                                        <tr>
                                            <td class="field_title" width="100px">
                                                <asp:Label ID="lbl1" Text="FROM DATE" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input" width="120px">
                                                <asp:TextBox ID="txtFromdate" runat="server" Width="70" MaxLength="10" Enabled="true"></asp:TextBox>
                                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/calendar.png"
                                                    Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFromdate"
                                                    PopupButtonID="ImageButton1" runat="server" Format="dd/MM/yyyy" Enabled="true"
                                                    BehaviorID="CalendarExtender1" CssClass="cal_Theme1" />
                                            </td>

                                             <td class="field_title" width="100px">
                                                <asp:Label ID="Label1" Text="TO DATE" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input" width="120px">
                                                <asp:TextBox ID="txtTodate" runat="server" Width="70" MaxLength="10" Enabled="true"></asp:TextBox>
                                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/calendar.png"
                                                    Width="24px" Height="24px" ImageAlign="AbsMiddle" />
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtTodate"
                                                    PopupButtonID="ImageButton2" runat="server" Format="dd/MM/yyyy" Enabled="true"
                                                    BehaviorID="CalendarExtender2" CssClass="cal_Theme1" />
                                            </td>
                                    </tr>
                                         <tr>
                                            <td class="field_title">
                                                <asp:Label ID="Label2" Text="Upload File" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input" >
                                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                            </td>
                                           
                                             <td class="field_input"  style="padding-left:21px">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnUpload" runat="server" Text="SAVE" CssClass="btn_link" ValidationGroup="ADD"
                                                        OnClick="btnUpload_Click" />
                                                </div>
                                            </td>

                                        </tr>



                                 </asp:Panel>
                             </div>
                         </div>
                    </div>
                     <cc1:MessageBox ID="MessageBox1" runat="server" />
                    <span class="clear"></span>
                </div>
             </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

