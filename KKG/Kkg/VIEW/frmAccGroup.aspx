<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmAccGroup.aspx.cs" Inherits="VIEW_frmAccGroup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .treeNode
        {
            color: blue;
            font: 13px Arial, Sans-Serif;
            width: 30px;
        }
        
        .rootNode
        {
            font-size: 13px;
            color: Green;
            width: 30px;
        }
        
        .leafNode
        {
            padding: 4px;
            color: Green;
            width: 30px;
        }
        
        .selectNode
        {
            font-weight: bold;
            color: purple;
        }
        
        .mainTv
        {
            display: table;
        }
    </style>
    
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
         <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((charCode < 48 || charCode > 57) && (charCode != 8))
                return false;

            return true;
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <%--<div class="page_title">
                <span class="title_icon"><span class="computer_imac"></span></span>
                <h3>
                    ACCOUNT GROUP ENTRY</h3>
            </div>--%>
            <div id="content">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn1" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>
                                    ACCOUNT GROUP DETAILS</h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAdd" runat="server" Text="Add New Record" CssClass="btn_link"
                                        CausesValidation="false" OnClick="Btnadd_Click" /></div>
                            </div>
                            <div class="widget_content" id="divView" runat="server">
                                <table border="0" cellspacing="0" cellpadding="0" width="100%" class="form_container_td">
                                    <tr>
                                        <td style="padding:10px 10px 0px 10px">
                                            <fieldset>
                                                <legend>Group Details</legend>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                                    <tr>
                                                        <td colspan="2" class="field_input">
                                                            <asp:RadioButton ID="rdTreeView" runat="server" Text="TreeView" Checked="true" GroupName="View"
                                                                AutoPostBack="True" OnCheckedChanged="rdTreeView_CheckedChanged" Width="20%" />
                                                            <asp:RadioButton ID="rdGridView" runat="server" Text="GridView" GroupName="View"
                                                                AutoPostBack="True" OnCheckedChanged="rdGridView_CheckedChanged" />
                                                        </td>
                                                    </tr>
                                                    <tr id="trTree" runat="server">
                                                        <td align="left" class="field_input" colspan="2">
                                                            <div id="TreeDiv">
                                                                <asp:TreeView ID="TrvGroup" runat="server" SkinID="SampleTreeView" ExpandDepth="10" ForeColor="Blue" 
                                                                    ImageSet="News" NodeIndent="50" ShowLines="True" ToolTip="Group Details" HoverNodeStyle-BackColor="ButtonShadow"
                                                                    HoverNodeStyle-ForeColor="ControlLight"  HorizontalPadding="8px" ShowExpandCollapse="False" AutoGenerateDataBindings="True">
                                                                    <HoverNodeStyle Font-Underline="True" ForeColor="Aqua" />
                                                                    <NodeStyle Font-Names="Arial, Sans-Serif" Font-Size="13px" NodeSpacing="2px" VerticalPadding="0px" ImageUrl="../images/img-raty/card.jpeg" />
                                                                    <RootNodeStyle ForeColor="Green" Font-Bold="true" ImageUrl="../images/img-raty/icon-generalLedger.png" HorizontalPadding="8px" />
                                                                    <ParentNodeStyle Font-Bold="true" ForeColor="Green" ImageUrl="../images/img-raty/card.jpeg" HorizontalPadding="8px" />
                                                                    <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px"  VerticalPadding="0px"  />
                                                                    <LeafNodeStyle HorizontalPadding="8px" ImageUrl="../images/img-raty/Article_IconNew.gif" />
                                                                    
                                                                </asp:TreeView>
                                                            </div>
                                                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                                <tr>
                                                                    <td>
                                                                        <table width="100%" cellpadding="0" cellspacing="0" border="0" id="tblSearch" runat="server">
                                                                            <tr>
                                                                                <td width="120" class="innerfield_title">
                                                                                    <asp:Label ID="lblSearch" runat="server" Text="Group To Search"></asp:Label>
                                                                                </td>
                                                                                <td width="250">
                                                                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="full"></asp:TextBox>
                                                                                </td>
                                                                                <td width="15">
                                                                                    &nbsp;
                                                                                </td>
                                                                                <td>
                                                                                <div class="btn_24_blue" >
                                                                                    <span class="icon find_co"></span>
                                                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn_link" OnClick="btnSearch_Click" />
                                                                                </div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td height="8">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <div class="Grid" id="divgvgroup" runat="server">
                                                                            <div class="head">
                                                                                <table width="660" cellspacing="0" cellpadding="0" border="0" id="tblHeader">
                                                                                    <tr>
                                                                                        <td width="22%">
                                                                                            Parent Group
                                                                                        </td>
                                                                                        <td width="24%">
                                                                                            Primary Group
                                                                                        </td>
                                                                                        <td width="22%">
                                                                                            Child Group
                                                                                        </td>
                                                                                        <td width="9%">
                                                                                        Sequence
                                                                                        </td>
                                                                                        <td width="18%">
                                                                                            Is Primary
                                                                                        </td>
                                                                                        <td width="5%">
                                                                                            Edit
                                                                                        </td>
                                                                                        <td>
                                                                                            Delete
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                            <div class="body">
                                                                                <asp:GridView ID="gvGroup" runat="server" AutoGenerateColumns="False" DataKeyNames="Code"
                                                                                    Width="100%" HorizontalAlign="Left" OnRowEditing="gvGroup_RowEditing" OnRowDeleting="gvGroup_RowDeleting"
                                                                                    Visible="true" ShowHeader="false" GridLines="Both">
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Id" Visible="False">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblParentId" runat="server" Text='<%#Bind("parentId") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="ParentId" Visible="False">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblId" runat="server" Text='<%#Bind("Id") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Code" Visible="False">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblCode" runat="server" Text='<%# Bind("Code") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Parent Group">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblParentGrp" runat="server" Text='<%# Bind("ParentGroup") %>' Width="100px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Primary Group">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblPrimaryGrp" runat="server" Text='<%# Bind("PrimaryGroup") %>' Width="100px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Child Group">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblChildGrp" runat="server" Text='<%# Bind("ChildGroup") %>' Width="100"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                                        </asp:TemplateField>
                                                                                         <asp:TemplateField HeaderText="Sequence" ItemStyle-HorizontalAlign="Center" >
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblsequence" runat="server" Text='<%# Bind("sequence") %>' Width="70px"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Is Primary" ItemStyle-HorizontalAlign="Center">
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chkisPrimary" Text=" " Checked='<%#Convert.ToBoolean(Eval("IsPrimary"))%>'
                                                                                                    runat="server" Width="80px" />
                                                                                            </ItemTemplate>
                                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                         
                                                                                        <asp:TemplateField HeaderText="Edit">
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="btnEdit" runat="server" Text="Edit" CssClass="action-icons c-edit"
                                                                                                    CommandName="Edit" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Delete">
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="btnDelete" runat="server" Text="Delete" CssClass="action-icons c-delete"
                                                                                                    CommandName="Delete" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                    <%--<HeaderStyle CssClass="gv_HeaderStyle" />
                                                            <RowStyle CssClass="gv_RowStyle" />--%>
                                                                                    <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="#3399FF" />
                                                                                </asp:GridView>
                                                                            </div>
                                                                            <div class="clear">
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="widget_content" id="diventry" runat="server">
                                <%--<asp:Panel ID="pnlAdd" runat="server">--%>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                    <tr>
                                        <td class="field_input" style="padding-left: 10px">
                                            <fieldset>
                                                <legend>Group Details</legend>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td width="100" class="field_title">
                                                            <asp:Label ID="Label17" Text=" Name" runat="server"></asp:Label><span class="req">*</span>
                                                        </td>
                                                        <td class="field_input">
                                                            <asp:TextBox ID="txtName" runat="server" CssClass="mid" MaxLength="50" Font-Bold="true" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorLoanAmt" runat="server" ControlToValidate="txtName"
                                                                CssClass="twitterStyleTextbox" Display="Dynamic" ErrorMessage="Enter Name" ForeColor="#F8F8F8"
                                                                ValidationGroup="1">*</asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtenderLoanAmt" runat="server"
                                                                TargetControlID="RequiredFieldValidatorLoanAmt" />
                                                            
                                                        </td>
                                                        <td class="field_title" width="100">
                                                               <asp:Label ID="Label1" Text=" Seqence" runat="server"></asp:Label><span class="req">*</span>
                                                            </td>
                                                           <td class="field_input">
                                                                <asp:TextBox ID="txtsequence" runat="server" CssClass="mid" MaxLength="50" Font-Bold="true" onkeypress="return isNumberKey(event);" />
                                                                <asp:RequiredFieldValidator ID="rfv_txtsequence" runat="server" ControlToValidate="txtsequence"
                                                                CssClass="twitterStyleTextbox" Display="Dynamic" ErrorMessage="Enter Sequence" ForeColor="#F8F8F8"
                                                                ValidationGroup="1">*</asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtendertxtsequence" runat="server"
                                                                TargetControlID="rfv_txtsequence" />
                                                           </td>
                                                    </tr>
                                                    <tr id="trRdBtn" runat="server">
                                                        <td class="field_title">
                                                            <asp:Label ID="Label18" Text="  Primary" runat="server"></asp:Label><span class="req">*</span>
                                                        </td>
                                                        <td class="field_input" colspan="3">
                                                            <asp:CheckBox ID="chkpry" runat="server" AutoPostBack="True" Text=" " OnCheckedChanged="chkpry_CheckedChanged" />
                                                            &nbsp;
                                                            <asp:DropDownList ID="ddlgroup" runat="server" AppendDataBoundItems="true" ValidationGroup="1"
                                                                Width="250" Height="28" class="chosen-select" data-placeholder="Choose a Division">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="CV_PRODUCTNAME" runat="server" ControlToValidate="ddlgroup"
                                                                ValidationGroup="1" InitialValue="0" SetFocusOnError="true" ErrorMessage="Select Group!"
                                                                Display="None"></asp:RequiredFieldValidator>
                                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                                TargetControlID="CV_PRODUCTNAME" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                WarningIconImageUrl="../images/050.png">
                                                            </ajaxToolkit:ValidatorCalloutExtender>
                                                        </td>
                                                    </tr>
                                                    <tr id="trSave" runat="server">
                                                        <td class="field_title">
                                                            &nbsp;
                                                        </td>
                                                        <td class="field_input" colspan="3">
                                                        <div class="btn_24_blue">
                                                            <span class="icon disk_co"></span>
                                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn_link" OnClick="btnSave_Click" ValidationGroup="1" />
                                                         </div>   
                                                          &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <%--OnClick="btnSave_Click"  onclick="btnClose_Click"--%>
                                                            <div class="btn_24_blue">
                                                                <span class="icon cross_octagon_co"></span>
                                                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn_link" OnClick="btnClose_Click" />
                                                            </div>
                                                            <%--onclick="btnEdit_Click" --%>
                                                            <%--<ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmText="You Want To Save?"
                                                                TargetControlID="btnSave" Enabled="True">
                                                            </ajaxToolkit:ConfirmButtonExtender>--%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </td>
                                    </tr>
                                </table>
                                <%-- </asp:Panel>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <cc1:MessageBox ID="MessageBox1" runat="server" />
            <%--<ul id="myMenu" class="contextMenu">
			        <li class="edit"><a href="#edit">Edit</a></li>	
			        <li class="delete"><a href="#delete">Delete</a></li>
		        </ul>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>