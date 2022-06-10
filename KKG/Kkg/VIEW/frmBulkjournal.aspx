<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true"
    CodeFile="frmBulkjournal.aspx.cs" Inherits="VIEW_frmBulkjournal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
      function ShowProgressBar() {
        document.getElementById('dvProgressBar').style.visibility = 'visible';
        document.getElementById('tdbtnUpload').style.visibility = 'hidden';
      }

      function HideProgressBar() {
        document.getElementById('dvProgressBar').style.visibility = "hidden";
        document.getElementById('tdbtnUpload').style.visibility = 'visible';
      }
    </script>
    <script type="text/javascript">
        function SetFileName(FileUpload1, txtWayBill) {
            var arrFileName = document.getElementById(FileUpload1).value.split('\\');
            document.getElementById(txtWayBill).value = arrFileName[arrFileName.length - 1];
        }     
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                <h6>BULK JOURNAL
                                </h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon doc_excel_table_co"></span><b>
                                        <asp:Button ID="btngeneraltemp" runat="server" Text="Download Bulk Journal Template"
                                            CssClass="btn_link" CausesValidation="false" OnClick="btngeneraltemp_Click" OnClientClick="ShowConfirmBox(this,'<b>Note : Please do not change any header name & PRODUCTID into sample template & must be upload this template, no others template not allowed into this system.!</br></br> if you are agree than download this sample!</b>',80,500); return false;" />
                                    </b>
                                </div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlDisplay" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <%--<td class="field_title" width="60">
                                                <asp:Label ID="Label31" Text="Beat" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                            </td>
                                            <td class="field_input" width="220">
                                                <asp:DropDownList ID="ddlbeat" Width="200" runat="server" class="form-control chosen-select"
                                                    data-placeholder="Choose Beat" AppendDataBoundItems="True">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorTSI" runat="server" ControlToValidate="ddlbeat"
                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="ADD"
                                                    ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="field_title" width="90">
                                                <asp:Label ID="Label1" Text="Sales Force" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input" width="220">
                                                <asp:DropDownList ID="ddlTSI" Width="200" runat="server" class="form-control chosen-select"
                                                    data-placeholder="Choose Sales Force" AppendDataBoundItems="True">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlTSI"
                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="*" ValidationGroup="ADD"
                                                    ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                            </td>--%>

                                            <td class="field_title" width="90">
                                                            <asp:Label ID="Label2" Text="Journal Date" runat="server"></asp:Label>
                                                        </td>
                                                        <td class="field_input">
                                                            <asp:TextBox ID="txtJournalDate" runat="server" Enabled="false" Width="85" placeholder="dd/MM/yyyy"
                                                                MaxLength="10"></asp:TextBox>
                                                            <asp:ImageButton ID="ImgToDate" runat="server" ImageUrl="~/images/calendar.png" Width="24px"
                                                                Height="24px" ImageAlign="AbsMiddle" />
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImgToDate" runat="server"
                                                                TargetControlID="txtJournalDate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>
                                                        </td>
                                            <td class="field_title" width="110">
                                                <asp:Label ID="Label6" Text="Upload File" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input" width="80">
                                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                            </td>
                                            <td class="field_title" id="tdbtnUpload">
                                                <div class="btn_24_blue">
                                                    <span class="icon upload_b"></span>
                                                    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" CssClass="btn_link"
                                                        ValidationGroup="ADD" OnClientClick="javascript:ShowProgressBar()" />
                                                    <asp:HiddenField ID="txtWayBill" runat="server" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <div align="center">
                                        <div id="dvProgressBar" style="visibility: hidden;">
                                            <img src="../images/loading123.gif" />
                                            <div>Uploading under process.......Please wait ...</div>
                                        </div>
                                        <br style="clear: both" />
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