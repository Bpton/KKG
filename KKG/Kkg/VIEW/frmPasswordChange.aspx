<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmPasswordChange.aspx.cs" Inherits="VIEW_frmPasswordChange" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        body
        {
            background: #f77462;
            font-family: Lato, sans-serif;
        }
        .accordion
        {
            background: #5ab2ca;
            width: 100%;
            display: block;
            list-style-type: none;
            overflow: hidden;
            height: 204px;
            font-size: 0;
            border-top-left-radius: 10px;
            border-bottom-left-radius: 10px;
            background-image: url(../images/enstitu_login.png);
        }
        .tabs
        {
            display: inline-block;
            background-color: #6dc5dd;
            border-right: #5ab2ca 1px solid;
            border-bottom-left-radius: 10px;
            border-top-left-radius: 10px;
            width: 20px;
            height: 204px;
            overflow: hidden;
            position: relative;
            margin: 0;
            font-size: 16px;
            -moz-transition: all 0.4s ease-in-out 0.1s;
            -o-transition: all 0.4s ease-in-out 0.1s;
            -webkit-transition: all 0.4s ease-in-out;
            -webkit-transition-delay: 0.1s;
            transition: all 0.4s ease-in-out 0.1s;
        }
        .tabs:hover
        {
            width: 350px;
        }
        .tabs .paragraph
        {
            position: relative;
            margin-left: 80px;
            padding: 10px 0 0 10px;
            height: 204px;
            background: #fff;
        }
        .tabs .paragraph h1
        {
            font-size: 2.5em;
            margin-bottom: 10px;
        }
        .tabs .paragraph p
        {
            font-size: 0.82em;
            line-height: 1.5em;
            padding-right: 30px;
        }
    </style>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div id="content">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>
                                    CHANGE PASSWORD</h6>
                                <div class="widget_content">
                                    <asp:Panel ID="pnlAdd" runat="server" align="center">
                                        <table width="30%" border="0" cellspacing="0" cellpadding="0" class="form_container_td"
                                            style="margin-top: 100px;">
                                            <tr>
                                                <td style="padding-top: 10px;">
                                                    <div>
                                                        <ul class="accordion">
                                                            <li class="tabs" style="background-image: url('../images/arrowhl.png'); background-repeat: no-repeat;
                                                                background-position: center;">
                                                                <div class="paragraph">
                                                                    <h2 style="height:40px;">User Information</h2>
                                                                    <p style="height:8px;">Name :&nbsp;<asp:Label ID="lblname" runat="server" Text="" Font-Bold="True"></asp:Label></p>
                                                                    <p style="height:8px;">User Type :&nbsp;<asp:Label ID="lblusertype" runat="server" Text="" Font-Bold="True"></asp:Label></p>
                                                                    <p style="height:8px;">UserName :&nbsp;<asp:Label ID="lblusername" runat="server" Text="" Font-Bold="True"></asp:Label></p>
                                                                    <p style="height:8px;">Last Change Password :&nbsp;<asp:Label ID="lbllastpasswordchange" runat="server" Text="" Font-Bold="True"></asp:Label></p>
                                                                </div>
                                                            </li>
                                                        </ul>
                                                    </div>
                                                </td>
                                                <td>
                                                    <fieldset style="background-position: left bottom; border-radius: 0px; border-bottom-right-radius: 10px;
                                                        border-top-right-radius: 10px; border-color: #6dc5dd; border-style: solid; background-color: rgb(158, 188, 218);
                                                        background-image: url('../images/enstitu_login.png'); background-repeat: no-repeat;">
                                                        <legend>Password Details</legend>
                                                        <table width="30%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                                            <tr>
                                                                <td style="padding-bottom: 15px">
                                                                    <asp:Label ID="Label1" runat="server" Text="Current password" Width="150px" Font-Bold="True"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_cpassword" runat="server" TextMode="Password" Width="200px" Font-Bold="true" placeholder="Enter current password" MaxLength="20"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_cpassword"
                                                                        ErrorMessage="* Please enter Current password" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding-bottom: 15px">
                                                                    <asp:Label ID="Label2" runat="server" Text="New password" Width="150px" Font-Bold="True"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_npassword" runat="server" TextMode="Password" Width="200px" Font-Bold="true" placeholder="Enter new password" MaxLength="20"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_npassword"
                                                                        ErrorMessage="* Please enter New password" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding-bottom: 30px">
                                                                    <asp:Label ID="Label3" runat="server" Text="Confirm password" Width="150px" Font-Bold="True"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_ccpassword" runat="server" TextMode="Password" Width="200px" Font-Bold="true" placeholder="Re-enter new password" MaxLength="20"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_ccpassword"
                                                                        ErrorMessage="* Please enter Confirm  password" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txt_npassword"
                                                                        ControlToValidate="txt_ccpassword" ErrorMessage="* Password Mismatch" ForeColor="red"></asp:CompareValidator><br />
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    &nbsp;&nbsp;&nbsp;
                                                                    <div class="btn_24_blue">
                                                                        <span class="icon disk_co"></span>
                                                                        <asp:Button ID="btn_update" runat="server" Font-Bold="True" CssClass="btn_link" OnClick="btn_update_Click"
                                                                            Text="Update" />
                                                                    </div>
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    <div class="btn_24_blue">
                                                                        <span class="icon cross_octagon_co"></span>
                                                                        <asp:Button ID="btnreset" runat="server" Font-Bold="True" CausesValidation="false"
                                                                            CssClass="btn_link" OnClick="btn_reset_Click" Text="Reset" />
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                        </table>
                                        <cc1:MessageBox ID="MessageBox1" runat="server" />
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>