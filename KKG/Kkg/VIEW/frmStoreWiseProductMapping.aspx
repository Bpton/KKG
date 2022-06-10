<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmStoreWiseProductMapping.aspx.cs" Inherits="VIEW_frmStoreWiseProductMapping" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
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

    <style>
        .checkbox .btn, .checkbox-inline .btn {
            padding-left: 2em;
            min-width: 8em;
        }

        .checkbox label, .checkbox-inline label {
            text-align: left;
            padding-left: 0.5em;
        }

        .checkbox input[type="checkbox"] {
            float: none;
        }
    </style>

    <style>
        th {
            background: cornflowerblue !important;
            color: white !important;
            position: sticky !important;
            top: 0;
            box-shadow: 0 2px 2px -1px rgb(255, 106, 0);
            width: 400px;
        }

        th, td {
            padding: 0.25rem;
        }
    </style>

    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="../images/103.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="page_title">
                <span class="title_icon"><span class="action-icons"></span></span>
                <h3>STORELOCATION WISE PRODUCT MAPPING</h3>
            </div>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">

                            <asp:Panel ID="pnlAdd" runat="server">


                                <td class="field_input">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 12px;">

                                                 <td class="field_title">
                                                    <asp:Label ID="Label4" runat="server" Text="Mapped"></asp:Label>
                                                    <span class="req">*</span>
                                                </td>
                                                <td width="23%">
                                                   <asp:DropDownList ID="ddlMappedOrNot" AppendDataBoundItems="true" Width="150" class="chosen-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMappedOrNot_SelectedIndexChanged">
                                                       <asp:ListItem Text="YES" Value="Y" Selected="True" />
                                                        <asp:ListItem Text="NO" Value="N" />
                                                  </asp:DropDownList>
                                                </td>

                                                <div id="divHide" runat="server">
                                                <td class="field_title">
                                                    <asp:Label ID="Label1" runat="server" Text="PRIMARY ITEM"></asp:Label>
                                                    <span class="req">*</span>
                                                </td>
                                                <td width="23%">
                                                    <asp:DropDownList ID="ddlPrimaryItem" runat="server" data-placeholder="Select Type" AutoPostBack="true"
                                                        AppendDataBoundItems="True" Width="250" class="chosen-select" OnSelectedIndexChanged="ddlPrimaryItem_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>

                                                
                                                <td class="field_title">
                                                    <asp:Label ID="Label3" runat="server" Text="SUB ITEM"></asp:Label>
                                                    <span class="req">*</span>
                                                </td>
                                                <td width="23%">
                                                    <asp:DropDownList ID="ddlSubItem" runat="server" data-placeholder="Select Type" AutoPostBack="true"
                                                        AppendDataBoundItems="True" Width="250" class="chosen-select" >
                                                    </asp:DropDownList>
                                                </td>
                                                    </div>

                                                <td class="field_title">
                                                    <asp:Label ID="Label2" runat="server" Text="GET PRODUCT"></asp:Label>
                                                    <span class="req">*</span>
                                                </td>
                                                <td width="23%">
                                                    <div class="btn_24_blue" id="Generate" runat="server">
                                                        <span class="icon page_white_gear_co"></span>
                                                        <asp:Button ID="btnGenerate" runat="server" Text="Generate" CssClass="btn_link" OnClick="btnGenerate_Click" />
                                                    </div>
                                                </td>

                                                <td class="field_title">
                                                    <asp:Label ID="lblStoreName" runat="server" Text="STORE LOCATION"></asp:Label>
                                                    <span class="req">*</span>
                                                </td>
                                                <td width="23%">
                                                    <asp:DropDownList ID="ddlStore" runat="server" data-placeholder="Select Store" AutoPostBack="true"
                                                        AppendDataBoundItems="True" Width="250" class="chosen-select">
                                                    </asp:DropDownList>
                                                </td>
                                            </td>
                                        </tr>

                                    </table>

                                    <div class="gridcontent-inner" id="divProductDetails" runat="server">
                                        <fieldset>
                                            <legend>PRODUCT DETAILS</legend>
                                            <div style="overflow: hidden; width: 100%;" id="DivHeaderRow1">
                                            </div>

                                            <%--<asp:TextBox ID="txtSearchBox" runat="server" Width="200px" Height="20px"></asp:TextBox>
                                            <asp:Button Text="Search" runat="server" Width="50px" Height="20px" ForeColor="Green" BackColor="#ccffcc"/>--%>


                                            <div style="overflow: scroll; height: 400px;" onscroll="OnScrollDiv(this)" id="DivMainContent">

                                                <asp:GridView ID="grdStoreProdutMapping" Width="100%" CssClass="reportgridchild" AutoGenerateColumns="false" ShowFooter="true"
                                                    EmptyDataText="No Records Available" runat="server" OnRowDataBound="grdStoreProdutMapping_RowDataBound">
                                                    <Columns>

                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkAllSelect" runat="server" onclick="CheckAllheader(this);" Text=" " />
                                                            </HeaderTemplate>
                                                            <HeaderStyle Width="5px" />
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" Class="checkbox" runat="server" Text=" " Style="padding-left: 15px;"
                                                                    ToolTip='<%# Bind("PRODUCTNAME") %>' onclick="setrowcolor(this);" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="5">
                                                            <ItemTemplate>
                                                                <nav style="position: relative; text-align: center;"><span class="badge black"><%# Container.DataItemIndex + 1 %></span></nav>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="STOREID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSTOREID" runat="server" Text='<%# Bind("STOREID") %>'
                                                                    value='<%# Eval("STOREID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="PRODUCTID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPRODUCTID" runat="server" Text='<%# Bind("PRODUCTID") %>'
                                                                    value='<%# Eval("PRODUCTID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PRIMARY TYPE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDIVNAME" runat="server" Text='<%# Bind("DIVNAME") %>'
                                                                    value='<%# Eval("DIVNAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SUB TYPE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCATNAME" runat="server" Text='<%# Bind("CATNAME") %>'
                                                                    value='<%# Eval("CATNAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CODE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCODE" runat="server" Text='<%# Bind("CODE") %>'
                                                                    value='<%# Eval("CODE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PRODUCTNAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPRODUCTNAME" runat="server" Text='<%# Bind("PRODUCTNAME") %>'
                                                                    value='<%# Eval("PRODUCTNAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MAPPED STORE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSTORENAME" runat="server" Text='<%# Bind("STORENAME") %>'
                                                                    value='<%# Eval("STORENAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <asp:Label ID="lbltotalcount" runat="server" Text="" Font-Bold="true" ForeColor="Gray"
                                        Font-Size="Small"></asp:Label>
                                    <tr>
                                        <div class="btn_24_blue" id="divbtnsave" runat="server">
                                            <span class="icon disk_co"></span>
                                            <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn_link" ValidationGroup="POFooter"
                                                OnClick="btnsave_Click" />
                                            <asp:HiddenField ID="Hdn_Fld" runat="server" />
                                        </div>
                                    </tr>
                                    <tr>
                                        <div class="btn_24_red">
                                            <span class="icon close"></span>
                                            <asp:Button ID="btnClose" runat="server" Text="CLOSE" CssClass="btn_link"
                                                OnClick="btnClose_Click" />

                                        </div>

                                    </tr>



                                </td>
                            </asp:Panel>

                            <asp:Panel ID="pnlDisplay" runat="server">
                                <div class="widget_top active" id="trBtn" runat="server">
                                    <span class="h_icon_he list_images"></span>
                                    <h6>Mapping Details</h6>
                                </div>


                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAdd" runat="server" Text="Add New Record" CssClass="btn_link" OnClick="btnAdd_Click"
                                        CausesValidation="false" />
                                </div>

                                <div class="gridcontent-inner" id="div1" runat="server">
                                    <fieldset>
                                        <legend>MAPPING DETAILS</legend>
                                        <div style="overflow: hidden; width: 100%;" id="DivHeaderRow">
                                        </div>

                                        <%--<asp:TextBox ID="txtSearchBox" runat="server" Width="200px" Height="20px"></asp:TextBox>
                                            <asp:Button Text="Search" runat="server" Width="50px" Height="20px" ForeColor="Green" BackColor="#ccffcc"/>--%>


                                        <div style="overflow: scroll; height: 250px;" onscroll="OnScrollDiv(this)" id="DivMainContent1">

                                            <asp:GridView ID="grdRptStoreWiseProduct" Width="100%" CssClass="reportgridchild" AutoGenerateColumns="false" ShowFooter="true"
                                                EmptyDataText="No Records Available" runat="server">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="5">
                                                        <ItemTemplate>
                                                            <nav style="position: relative; text-align: center;"><span class="badge black"><%# Container.DataItemIndex + 1 %></span></nav>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="STOREID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSTOREID" runat="server" Text='<%# Bind("STOREID") %>'
                                                                value='<%# Eval("STOREID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MAPPED STORE">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSTORENAME" runat="server" Text='<%# Bind("STORENAME") %>'
                                                                value='<%# Eval("STORENAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PRODUCT COUNT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCOUNT" runat="server" Text='<%# Bind("PRODUCTCOUNT") %>'
                                                                value='<%# Eval("PRODUCTCOUNT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="VIEW PRODUCTS" HeaderStyle-Width="40px">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnEdit" runat="server" CssClass="action-icons c-edit"
                                                                OnClick="btnEdit_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </fieldset>
                                </div>

                                <tr>
                                    <div class="btn_30_orange" id="btnHide" runat="server" style="display: none">
                                        <span class="icon close"></span>
                                        <asp:Button ID="btnHideGrid" runat="server" Text="HIDE PRODUCT DETAILS" CssClass="btn_link"
                                            OnClick="btnHideGrid_Click" />

                                    </div>

                                </tr>

                                <div id="divProductStoreDetails" runat="server" style="display: none">
                                    <fieldset>
                                        <legend>MAPPING DETAILS</legend>
                                        <div style="overflow: hidden; width: 100%;" id="DivHeaderRow2">
                                        </div>
                                        <div style="overflow: scroll; height: 400px;" onscroll="OnScrollDiv(this)" id="DivMainContent2">

                                            <asp:GridView ID="grdDetails" Width="100%" CssClass="reportgridchild" AutoGenerateColumns="true" ShowFooter="true"
                                                EmptyDataText="No Records Available" runat="server">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="5">
                                                        <ItemTemplate>
                                                            <nav style="position: relative; text-align: center;"><span class="badge black"><%# Container.DataItemIndex + 1 %></span></nav>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </fieldset>
                                </div>

                            </asp:Panel>

                        </div>
                    </div>
                    <cc1:MessageBox ID="MessageBox1" runat="server" />
                    <span class="clear"></span>
                </div>
                <span class="clear"></span>
            </div>

            <script type="text/javascript" language="javascript">
                function CheckAllheader(Checkbox) {
                    var GridVwHeaderCheckbox = document.getElementById("<%=grdStoreProdutMapping.ClientID %>");
            var lbltotalcount = document.getElementById("<%=lbltotalcount.ClientID %>");
                    var totalcount = 0;
                    if (Checkbox.checked) {
                        for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                            GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                            /*GridVwHeaderCheckbox.rows[i].style.color = '#389638';*/
                            GridVwHeaderCheckbox.rows[i].cells[0].style.border = '1px solid #fff';

                            totalcount = totalcount + 1;
                        }
                    }
                    else {
                        for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                            GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = false;
                            /*GridVwHeaderCheckbox.rows[i].style.color = '#4B555E';*/
                            GridVwHeaderCheckbox.rows[i].cells[0].style.border = '0px solid gray';
                        }
                    }
                    alert(totalcount);
                    if (totalcount > 0) {
                        lbltotalcount.style.display = "";
                        lbltotalcount.innerHTML = 'You have selected : ' + totalcount + ' invoices.';
                    }
                    else {
                        lbltotalcount.style.display = "none";
                    }
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

