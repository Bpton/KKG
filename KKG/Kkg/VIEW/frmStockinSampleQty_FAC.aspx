<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmStockinSampleQty_FAC.aspx.cs" Inherits="FACTORY_frmStockinSampleQty_FAC" %>

<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/styles.css" rel="stylesheet" type="text/css" />
    <link href="../css/sprite.css" rel="stylesheet" type="text/css" />
    <link href="../css/style-forms.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.min.js"></script>
</head>
<body bgcolor="#eee">

    <script type="text/javascript" language="javascript">
        function calculate(a) {
            var ReceiveQtyid = 0;
            var ReceiveQty = 0;
            var SampleQtyID = 0;
            var SampleQty = 0;
            var ObservationQtyID = 0;
            var ObservationQty = 0;
            var rowData = a.parentNode.parentNode;
            var rowIndex = rowData.rowIndex - 1;
            var grd = document.getElementById('<%= GvSample.ClientID %>');
            var grid = document.getElementById('GvSample');

            ReceiveQtyid = "GvSample_lblReceivedQty_" + rowIndex;
            ReceiveQty = document.getElementById(ReceiveQtyid).value;

            SampleQtyID = "GvSample_txtsampleQty_" + rowIndex;
            SampleQty = document.getElementById(SampleQtyID).value;
            /*alert(SampleQty);
            alert(ReceiveQty);*/

            ObservationQtyID = "GvSample_txtObservationQty_" + rowIndex;
            ObservationQty = document.getElementById(ObservationQtyID).value;

            if (ReceiveQty < SampleQty) {
                alert("Wastage Qty Can Not be greater Than Production Qty.");
            }
            else {

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
                <legend>Stock in Sample Qty</legend>
                <table cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                    <tr>
                        <td align="left" class="field_input" colspan="2">
                            <div style="height: 215px">
                                <asp:GridView ID="GvSample" runat="server" AutoGenerateColumns="False" EmptyDataText="No files uploaded"
                                    GridLines="Horizontal" CssClass="zebra" RowStyle-Height="22" ShowFooter="true" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="STOCKRECEIVEDID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStockReceivedID" runat="server" Text='<%# Bind("STOCKRECEIVEDID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="POID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPOID" runat="server" Text='<%# Bind("POID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PRODUCTID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductid" runat="server" Text='<%# Bind("PRODUCTID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="PRODUCT NAME" HeaderStyle-Width="250">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductName" runat="server" Text='<%# Bind("PRODUCTNAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="RECEIVED QTY" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="30">
                                            <ItemTemplate>
                                                <asp:Label ID="lblReceivedQty" runat="server" Text='<%# Bind("RECEIVEDQTY") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SAMPLE QTY">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtsampleQty" runat="server" value='<%# Eval("SAMPLEQTY") %>' onkeyup="calculate(this);" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="OBSERVATION QTY">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtObservationQty" runat="server" value='<%# Eval("OBSERVATIONQTY") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="field_input" colspan="2" runat="server" id="TdButtonEvent">
                            <div class="btn_24_blue" id="divSaveButton" runat="server">
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
