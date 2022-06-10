<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmTransporterUpload.aspx.cs" Inherits="VIEW_frmTransporterUpload" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        //        function pageLoad(sender, args) 
        function SetFileName(FileUpload1, txtWayBill) {
            var arrFileName = document.getElementById(FileUpload1).value.split('\\');
            document.getElementById(txtWayBill).value = arrFileName[arrFileName.length - 1];
        }
        //        }       

    </script>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <Triggers>
    <asp:PostBackTrigger ControlID="btnUpload" />
     <asp:PostBackTrigger ControlID="btngeneraltemp" />
    </Triggers>
        <ContentTemplate>
            <%--<div class="page_title">
                <span class="title_icon"><span class="computer_imac"></span></span>
                <h3>
                    Bank Master</h3>
            </div>--%>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>
                                    Transporter Upload
                                </h6>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlDisplay" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                    <tr>
                                      <td class="field_input" colspan="3" >
                                                 <div class="btn_24_blue">
                                                <span class="icon doc_excel_table_co"></span>
                                               <asp:Button ID="btngeneraltemp" runat="server"  Text="Generate Template" CssClass="btn_link" CausesValidation="false"
                                               OnClick="btngeneraltemp_Click"/>
                                            </div> 
                                            </td>
                                            </tr>

								    <tr>     
											
											  <td width="120" class="field_title">
												<asp:Label ID="Label6" Text="Upload File" runat="server"></asp:Label><span class="req">*</span>
											</td>
											<td class="field_input"  width="40">
											   <asp:FileUpload ID="FileUpload1" runat="server" />
											</td>
											<td  class="field_title" >
											  <div class="btn_24_blue">
													<span class="icon disk_co"></span>
												<asp:Button ID="btnUpload" runat="server" Text="Save" OnClick="btnUpload_Click" CssClass="btn_link" ValidationGroup="ADD" />
											   
												</div>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>