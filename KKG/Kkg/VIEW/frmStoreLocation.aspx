<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmStoreLocation.aspx.cs" Inherits="VIEW_frmStoreLocation" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
	TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript">
        function ShowProgressBar() {
            document.getElementById('dvProgressBar').style.visibility = 'visible';
            document.getElementById('tdbtnUpload').style.visibility = 'hidden';
        }
        $(document).ready(function () {

            BlockUI("<%=pnlDisplay.ClientID %>");
            $.blockUI.defaults.css = {};
        });
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

        function HideProgressBar() {
            document.getElementById('dvProgressBar').style.visibility = "hidden";
            document.getElementById('tdbtnUpload').style.visibility = 'visible';
        }
    </script>
    <script type="text/javascript">

        function SetFileName(PageUrlUpload, txtPageUrl) {
            var arrFileName = document.getElementById(PageUrlUpload).value.split('\\');
            document.getElementById(txtPageUrl).value = arrFileName[arrFileName.length - 1];
        }

        function isNumberKeyWithslash(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 47))
                return false;

            return true;
        }
    </script>
    <style>
        table
        {
    max-width:none;
    background-color:transparent;
    border-collapse:collapse;
    border-spacing:0;
    background-color:aliceblue;
        }

.table
{
    width:auto;
    height:auto;
    margin-bottom:20px;
    background-position-x:center;
    border:groove;
}

.table th,.table td
{
    width:auto;
    height:auto;
    padding:8px;
    line-height:20px;
    text-align:left;
    vertical-align:top;
    border-top:1px solid #dddddd;
    font-family:'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif;
    font-size: 16px;
    
}

.table th
{
    width:auto;
    height:auto;
    font-weight:bold;
    border:solid;
}

.table thead th
{
    vertical-align:bottom;
   
}
.button {
  background-color: #4CAF50; /* Green */
  border: none;
  color: white;
  padding: 16px 32px;
  text-align: center;
  text-decoration: none;
  display: inline-block;
  font-size: 16px;
  margin: 4px 2px;
  transition-duration: 0.4s;
  cursor: pointer;
}
.button1 {
  background-color: greenyellow;
  color: black;
  border: 2px solid #4CAF50;
  border-radius: 50%;
}
.button2 {
  background-color: lightcoral;
  color: black;
  border: 2px solid #4CAF50;
  border-radius: 50%;
}


.btn3 {
    background-color: cornflowerblue; /* Green */
    color: black;
    text-align: center;
    text-decoration: none;
    font-size: 16px;
    cursor: pointer;
    transition-duration: 0.4s;
    border: 4px solid #4CAF50;
    border-radius: 8%;
 }

 .btn3:hover {
    box-shadow: 0 4px 6px 0 rgba(0,0,0,0.17),0 6px 8px 0 rgba(0,0,0,0.18);
    background-color: #9999FF;
 }
  
    </style>
   <link rel="stylesheet" type="text/css" href="css/style.css">
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
                                <div class="widget_top active" id="Div1" runat="server">
                                    <span class="h_icon_he list_images"></span>
                                    <h6>Store Location
                                    </h6>
                                    <div class="btn3" style="float: right;" id="btnaddhide" runat="server">
                                        <span class="icon add_co"></span>
                                        <asp:Button ID="btnAddnewRecord" runat="server" Text="Add New Record" CssClass="btn_link"
                                            OnClick="btnAddnewRecord_Click" CausesValidation="false" />
                                    </div>

                                 

                                </div>
                                <div class="widget_content">
                                    <asp:Panel ID="pnlAdd" runat="server">
                                        <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                           
                                          <tr>

                                               <h5>Store Location Name:</h5>
                                                     <asp:TextBox ID="txtstorelocation" runat="server" style=" top:50px;left: 170px; position: absolute; height: 20px; width: 150px"></asp:TextBox>
                                                    <br><br>
                                       
                                                   
                                                      
                                            </tr>   
                                           
                                              <div style="text-align: left " > 
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="90px" Height="20" BorderColor="blue" BackgroundColor="blue" OnClick="SubmitButton_Click" class="button1" />
                                                    <asp:Button ID="btnClear" runat="server"  Text="Clear" Width="89px" Height="20" BorderColor="red" BackgroundColor="red"  OnClick="btncancel_Click" CssClass="button2" />
                                                </div>
                                        </table>
     
                                    </asp:Panel>
                                    <asp:HiddenField runat="server" ID="hdnstorelocation" />
                                    

                                    <asp:Panel ID="pnlDisplay" CssClass="modalPopup" runat="server">
                                        <asp:HiddenField ID="Hdn_Fld" runat="server" />
                                        <div style="overflow: scroll; height: 350px; width: 100%;">
                                            <asp:GridView ID="grvstorelocation" runat="server" Width="100%" CssClass="table table-bordered table-striped"
                                                AutoGenerateColumns="false" ShowFooter="false" AlternatingRowStyle-BackColor="WhiteSmoke"
                                                EmptyDataText="No Records Available" >
                                                <Columns>
                                                    
                                                    <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="5">
                                                        <ItemTemplate>
                                                            <nav style="position: relative; text-align: center;"><span class="badge black"><%# Container.DataItemIndex + 1 %></span></nav>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Task id" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltaskid" runat="server" Text='<%# Bind("ID") %>' value='<%# Eval("ID") %>' visable="false"/>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Store Location Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblENTERYDATE" runat="server" Text='<%# Bind("NAME") %>'
                                                                value='<%# Eval("NAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                    
                                                    <asp:TemplateField HeaderText="DELETE" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btngrddelete" runat="server" CausesValidation="false"
                                                            class="action-icons c-delete" OnClick="btngrddelete_Click"
                                                            OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
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
    <script type="text/javascript">

      


    </script>
</asp:Content>
 
