<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmQCFileUpload_FAC.aspx.cs" Inherits="VIEW_frmQCFileUpload_FAC" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/styles.css" rel="stylesheet" type="text/css" />
    <link href="../css/sprite.css" rel="stylesheet" type="text/css" />
    <link href="../css/style-forms.css" rel="stylesheet" type="text/css" />
</head>
<body bgcolor="#eee">
    <script type="text/javascript">
        function pageLoad(sender, args) {
            function SetFileName(DocumentUpload, Insuranceimage) {
                var arrFileName = document.getElementById(DocumentUpload).value.split('\\');
                document.getElementById(Insuranceimage).value = arrFileName[arrFileName.length - 1];
            }
        }

    </script>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scrmngr" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="../images/loading123.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Label ID="lblmsg" runat="server" Text="" Visible="false"></asp:Label>
    <div id="contentarea">
        <fieldset style="border-color: #0000FF; border-style: double">
        <legend>Quality Control Documents Upload</legend>
                    <table cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                        <tr>
                            <td width="100" class="innerfield_title" runat="server" id="Tdlblupload">
                                <asp:Label ID="lblupload" Text="Upload Files" runat="server"></asp:Label><span class="req">*</span>
                            </td>
                            <td align="left" valign="top" class="field_input" runat="server" id="TdDocumentUpload">
                                <asp:FileUpload ID="DocumentUpload" runat="server" Width="355" />
                                <div class="btn_24_blue">
                                    <span class="icon page_white_get_co"></span>
                                    <asp:Button ID="btnInsuranceDocument" Text="Upload" runat="server" CssClass="btn_link"
                                        OnClick="btnInsuranceDocument_Click" />
                                </div>
                                <asp:HiddenField ID="Insuranceimage" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <%--<td align="left" class="field_title">
                            </td>--%>
                        <td align="left" class="field_input" colspan="2">
                            <div style="height: 300px">
                                <asp:GridView ID="gvFiles" runat="server" AutoGenerateColumns="False" EmptyDataText="No files uploaded"
                                    GridLines="Horizontal" CssClass="trialbalancegrid" RowStyle-Height="22" ShowFooter="true" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="filename" HeaderText="File Name" />
                                        <asp:TemplateField HeaderText="Download">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDownload" ToolTip="Download" CssClass="filter_btn page_white_put_co"
                                                    CommandArgument='<%# Eval("filename") %>' runat="server" OnClick="DownloadFile"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" ToolTip="Delete" CssClass="action-icons c-delete"
                                                    CommandArgument='<%# Eval("filename") %>' runat="server" OnClick="DeleteFile" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>

                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="field_input" colspan="2" runat="server" id="TdButtonEvent">
                            <div class="btn_24_blue">
                                <span class="icon disk_co"></span>
                                <asp:Button ID="btnsave" runat="server" Text="SAVE" OnClick="btnsave_Click" CssClass="btn_link" />
                            </div>
                            &nbsp;&nbsp;&nbsp;
                            <div class="btn_24_blue">
                                <span class="icon cross_octagon_co"></span>
                                <asp:Button ID="btnuploadcancel" runat="server" Text="Close" OnClick="btnuploadcancel_Click" CssClass="btn_link" />
                            </div>
                            <asp:HiddenField ID="Hdn_Fld" runat="server" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <cc1:MessageBox ID="MessageBox1" runat="server" />
    </form>
</body>
</html>