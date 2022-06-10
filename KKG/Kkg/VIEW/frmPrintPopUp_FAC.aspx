<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="frmPrintPopUp_FAC.aspx.cs" Inherits="VIEW_frmPrintPopUp_FAC" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script type="text/javascript">
          function checkcheckbox() {
          debugger          
                    var checkBoxList = document.getElementById("<%=chkPrintButton.ClientID%>");
                    var checkBoxes = checkBoxList.getElementsByTagName("INPUT");
                    if (checkBoxes[3].parentNode.getElementsByTagName("LABEL")[0].innerHTML == 'BackUp Sheet')
                    {
                    if (checkBoxes[0].checked == true || checkBoxes[1].checked == true || checkBoxes[2].checked == true) {
                    checkBoxes[3].checked = false;
                    }
            }


            if (checkBoxes[4].parentNode.getElementsByTagName("LABEL")[0].innerHTML == 'E-Way Bill Details') {
                if (checkBoxes[0].checked == true || checkBoxes[1].checked == true || checkBoxes[2].checked == true || checkBoxes[3].checked == true) {
                    checkBoxes[4].checked = false;
                }
            }

                    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <fieldset>
            <legend>Select Print Copy</legend>
                     
            <table>
              <tr>
                  <td>
                        <asp:CheckBoxList  ID="chkPrintButton" runat="server"  RepeatColumns="2" CellPadding="10"  onclick="checkcheckbox(this)">
                        <asp:ListItem Text="Original For Recipient" Value="1" Selected></asp:ListItem>
                        <asp:ListItem Text="Duplicate For Transporter" Value="2" Selected></asp:ListItem>
                        <asp:ListItem Text="Triplicate For Seller Copy" Value="3" Selected></asp:ListItem>
                        <asp:ListItem Text="Extra Copy" Value="4" Selected></asp:ListItem>
                        </asp:CheckBoxList>
                  </td>
               
              </tr>
                <tr>
                    <td><asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" /></td>

                </tr>

            </table>
       </fieldset>
        
    </div>
    </form>
</body>
</html>