<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmProductMaster.aspx.cs" Inherits="VIEW_frmProductMaster" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
        <Triggers>
            <asp:PostBackTrigger ControlID="btngeneratetemp" />
            <asp:PostBackTrigger ControlID="btnExcel" />
        </Triggers>
        <ContentTemplate>

            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>Product Details</h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAdd" runat="server" Text="Add New Record" CssClass="btn_link"
                                        OnClick="btnAdd_Click" CausesValidation="false" />
                                </div>
                                <div class="btn_30_light" style="float: right;" id="Div1" runat="server">
                                    <span class="icon doc_excel_table_co"></span>
                                    <asp:Button ID="btnExcel" runat="server" OnClick="btnExcel_Click" Text="Download Product List" CssClass="btn_link"
                                        CausesValidation="false" />
                                </div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td width="90" class="field_title">
                                                <asp:Label ID="lblName" Text="Name" runat="server"></asp:Label>
                                            </td>
                                            <td width="320" class="field_input">
                                                <asp:TextBox ID="txtName" runat="server" Width="100%" placeholder="Brand Category Fragrance UnitValue Uom"
                                                    Enabled="false"></asp:TextBox>
                                            </td>
                                            <td width="80" class="field_input">
                                                <asp:TextBox ID="txtname2" runat="server" CssClass="full" placeholder=""></asp:TextBox>
                                            </td>
                                            <td width="90" class="field_title">
                                                <asp:Label Text="Short Name" runat="server" ID="lblShortName"></asp:Label>
                                            </td>

                                            <td width="205" class="field_input">
                                                <asp:TextBox ID="txtShortName" runat="server" Width="250" ValidationGroup="submitvalidation"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorShortName" runat="server" ErrorMessage="*"
                                                    ForeColor="Red" ControlToValidate="txtShortName" ValidateEmptyText="false" SetFocusOnError="true"
                                                    ValidationGroup="submitvalidation"> </asp:RequiredFieldValidator>
                                            </td>
                                            
                                            <td width="40" class="field_title">
                                                <asp:Label ID="lblCode" Text="Code" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input" width="135">
                                                <asp:TextBox ID="txtCode" runat="server" Width="200" Enabled="true"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="CV_Code" runat="server" ErrorMessage="Product Code is required!"
                                                    ForeColor="Red" Display="None" ControlToValidate="txtCode" ValidateEmptyText="false"
                                                    SetFocusOnError="true" ValidationGroup="submitvalidation"> </asp:RequiredFieldValidator>
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                                    TargetControlID="CV_Code" PopupPosition="Right" HighlightCssClass="errormassage"
                                                    WarningIconImageUrl="../images/050.png">
                                                </ajaxToolkit:ValidatorCalloutExtender>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="divproductinfo" runat="server">
                                        <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                            <tr>
                                                <td class="field_title">
                                                    <asp:Label ID="lblUOM" Text="UOM" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                </td>
                                                <td class="field_input">
                                                    <asp:DropDownList ID="ddlUOM" runat="server" AppendDataBoundItems="true" Width="198"
                                                        Height="28" class="chosen-select" data-placeholder="Choose a UOM" OnSelectedIndexChanged="ddlUOM_SelectedIndexChanged"
                                                        AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                                        ForeColor="Red" ControlToValidate="ddlUOM" ValidateEmptyText="false" SetFocusOnError="true"
                                                        InitialValue="0" ValidationGroup="submitvalidation"> </asp:RequiredFieldValidator>
                                                    <asp:Button ID="btnRefreshUOM" runat="server" CssClass="h_icon arrow_refresh_co"
                                                        ToolTip="Refresh" OnClick="btnRefreshUOM_Click" CausesValidation="false" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                                                        TargetControlID="RequiredFieldValidator4" PopupPosition="Right" HighlightCssClass="errormassage"
                                                        WarningIconImageUrl="../images/050.png">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                </td>

                                                <td class="field_title">
                                                    <asp:Label ID="lblNature" Text="Main Group" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                </td>
                                                <td class="field_input">
                                                    <asp:DropDownList ID="ddlNature" runat="server" AppendDataBoundItems="true" Width="198"
                                                        Height="28" class="chosen-select" data-placeholder="Choose a Nature">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                                        ForeColor="Red" ControlToValidate="ddlNature" ValidateEmptyText="false" SetFocusOnError="true"
                                                        InitialValue="0" ValidationGroup="submitvalidation"> </asp:RequiredFieldValidator>
                                                    <asp:Button ID="btnRefreshNature" runat="server" CssClass="h_icon arrow_refresh_co"
                                                        ToolTip="Refresh" OnClick="btnRefreshNature_Click" CausesValidation="false" />
                                                </td>

                                                <td class="field_title">
                                                    <asp:Label ID="lblDiv" Text="Brand (stage 1)" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                </td>
                                                <td class="field_input">
                                                    <asp:DropDownList ID="ddlDivision" runat="server" AppendDataBoundItems="true" Width="198"
                                                        Height="28" class="chosen-select" data-placeholder="Choose a Brand" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"
                                                        AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                                        Font-Bold="true" ForeColor="Red" ControlToValidate="ddlDivision" ValidateEmptyText="false"
                                                        SetFocusOnError="true" InitialValue="0" ValidationGroup="submitvalidation"> </asp:RequiredFieldValidator>
                                                    <asp:Button ID="btnRefresh" runat="server" CssClass="h_icon arrow_refresh_co" ToolTip="Refresh"
                                                        OnClick="btnRefresh_Click" CausesValidation="false" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="120" class="field_title">
                                                    <asp:Label ID="lblCat" Text="Category (stage 2)" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                </td>
                                                <td width="230" class="field_input">
                                                    <asp:DropDownList ID="ddlCategory" runat="server" AppendDataBoundItems="true" Width="198"
                                                        Height="28" class="chosen-select" data-placeholder="Choose a Category" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"
                                                        AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                                        EnableClientScript="true" ForeColor="Red" ControlToValidate="ddlCategory" ValidateEmptyText="false"
                                                        SetFocusOnError="true" InitialValue="0" ValidationGroup="submitvalidation"> </asp:RequiredFieldValidator>
                                                    <asp:Button ID="btnRefreshCategory" runat="server" CssClass="h_icon arrow_refresh_co"
                                                        ToolTip="Refresh" OnClick="btnRefreshCategory_Click" CausesValidation="false" />
                                                </td>

                                                <td class="field_title">
                                                    <asp:Label ID="Label2" Text="TYPE (stage 3)" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                </td>
                                                <td class="field_input">
                                                    <asp:DropDownList ID="ddlitemtype" runat="server" AppendDataBoundItems="true" Width="198"
                                                        class="chosen-select" data-placeholder="Choose a Type">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="tfv_ddlitemtype" runat="server" ErrorMessage="*"
                                                        ForeColor="Red" ControlToValidate="ddlitemtype" ValidateEmptyText="false" SetFocusOnError="true"
                                                        InitialValue="0" ValidationGroup="submitvalidation"> </asp:RequiredFieldValidator>
                                                </td>

                                                <td class="field_title">
                                                    <asp:Label ID="lblUValue" Text="Sweep (stage 4)" runat="server"></asp:Label>&nbsp;<span
                                                        class="req">*</span>
                                                </td>
                                                <td class="field_input">
                                                    <asp:TextBox ID="txtUValue" runat="server" Style="width: 190px;" MaxLength="20" OnTextChanged="txtUValue_TextChanged"
                                                        AutoPostBack="true" onkeypress="return validateFloatKeyPress(this,event);"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Unit value is required!"
                                                        ForeColor="Red" Display="None" ControlToValidate="txtUValue" ValidateEmptyText="false"
                                                        SetFocusOnError="true" ValidationGroup="submitvalidation"> </asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                        TargetControlID="RequiredFieldValidator5" PopupPosition="Right" HighlightCssClass="errormassage"
                                                        WarningIconImageUrl="../images/050.png">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="field_title">
                                                    <asp:Label ID="lblFragrance" Text="color (stage 5)" runat="server"></asp:Label>&nbsp;<span
                                                        class="req">*</span>
                                                </td>
                                                <td class="field_input">
                                                    <asp:DropDownList ID="ddlFragrance" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                                        class="chosen-select" Width="198" Height="28" data-placeholder="Choose a Fragrance"
                                                        OnSelectedIndexChanged="ddlFragrance_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*"
                                                        ForeColor="Red" ControlToValidate="ddlFragrance" ValidateEmptyText="false" SetFocusOnError="true"
                                                        InitialValue="0" ValidationGroup="submitvalidation"> </asp:RequiredFieldValidator>
                                                    <asp:Button ID="btnRefreshFragrance" runat="server" CssClass="h_icon arrow_refresh_co"
                                                        ToolTip="Refresh" OnClick="btnRefreshFragrance_Click" CausesValidation="false" />
                                                </td>

                                                <td class="field_title">
                                                    <asp:Label ID="lblBestbefore" Text="Best before use" runat="server"></asp:Label>&nbsp;<span
                                                        class="req">*</span>
                                                </td>
                                                <td class="field_input">
                                                    <asp:TextBox ID="txtBestbefore" runat="server" Style="width: 190px;" MaxLength="30"
                                                        onkeypress="return validateFloatKeyPress(this,event);"></asp:TextBox>
                                                    <asp:Label ID="Label4" runat="server" Text="&nbsp;Days" Font-Bold="True"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="No. of days is required!"
                                                        ControlToValidate="txtBestbefore" ValidateEmptyText="false" Display="None" SetFocusOnError="true"
                                                        ValidationGroup="submitvalidation"> </asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server"
                                                        TargetControlID="RequiredFieldValidator7" PopupPosition="Right" HighlightCssClass="errormassage"
                                                        WarningIconImageUrl="../images/050.png">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                </td>

                                                <td class="field_title">
                                                    <asp:Label ID="Label16" Text="BARCODE" runat="server"></asp:Label>
                                                </td>
                                                <td class="field_input">
                                                    <asp:TextBox ID="txtbarcode" runat="server" MaxLength="20" Style="width: 190px;" onkeypress="return isNumberKeyWithDot(event);"></asp:TextBox>
                                                    <asp:Label ID="lblPRODUCTOWNER" runat="server" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="100" class="field_title">
                                                    <asp:Label ID="lblMRP" Text="Mrp" runat="server"></asp:Label>&nbsp;<span class="req">*</span>
                                                </td>
                                                <td class="field_input">
                                                    <asp:TextBox ID="txtMRP" runat="server" Style="width: 190px;" onkeypress="return validateFloatKeyPress(this,event);"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="cv_txtMRP" runat="server" ErrorMessage="MRP is required!"
                                                        ControlToValidate="txtMRP" ValidateEmptyText="false" Display="None" SetFocusOnError="true"
                                                        ValidationGroup="submitvalidation"> </asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server"
                                                        TargetControlID="cv_txtMRP" PopupPosition="Right" HighlightCssClass="errormassage"
                                                        WarningIconImageUrl="../images/050.png">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                                  <td width="100" class="field_title"  >
                                                                            <asp:Label ID="Label9" Text="StoreLocation" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td class="field_input">
                                                                            <asp:DropDownList ID="ddlstorelocation" runat="server" Width="200px" AutoPostBack="true"
                                                                                AppendDataBoundItems="True" class="chosen-select" data-placeholder="Select Location">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                <td  width="100" class="field_title">
                                                    <asp:Label ID="lblActive" Text="Active" runat="server"></asp:Label>
                                                </td>
                                                <td width="100" class="field_input">
                                                    <asp:CheckBox ID="ChkActive" runat="server" Text=" " />
                                                </td>


                                                 <td  width="50" class="field_title">
                                                    <asp:Label ID="Label10" Text="service" runat="server"></asp:Label>
                                                </td>
                                                <td class="field_input">
                                                    <asp:CheckBox ID="chkservice" runat="server" Text=" " />
                                                </td>

                                                <td width="100" class="field_title">
                                                    <asp:Label ID="Label15" Text="Power SKU" runat="server"></asp:Label>
                                                </td>
                                                <td class="field_input">
                                                    <asp:CheckBox ID="chkpowersku" runat="server" Text=" " />
                                                </td>

                                            </tr>
                                        </table>
                                    </div>

                                    <div id="divsecondary" runat="server">
                                        <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                            <tr style="display: none">
                                                <td width="100" class="field_title">
                                                    <asp:Label ID="Label17" Text="Singapore dollar(Rs.)" runat="server"></asp:Label>
                                                </td>
                                                <td class="field_input">
                                                    <asp:TextBox ID="txtSingDollar" runat="server" Style="width: 190px;" onkeypress="return validateFloatKeyPress(this,event);"></asp:TextBox>
                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="MRP is required!"
                                                        ControlToValidate="txtMRP" ValidateEmptyText="false" Display="None" SetFocusOnError="true"
                                                        ValidationGroup="submitvalidation"> </asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server"
                                                        TargetControlID="cv_txtMRP" PopupPosition="Right" HighlightCssClass="errormassage"
                                                        WarningIconImageUrl="../images/050.png">
                                                    </ajaxToolkit:ValidatorCalloutExtender>--%>
                                                </td>
                                                <td width="100" class="field_title">
                                                    <asp:Label ID="Label19" Text="Nepalese Rupee(Rs.)" runat="server"></asp:Label>
                                                </td>
                                                <td class="field_input">
                                                    <asp:TextBox ID="txtNepRupee" runat="server" Style="width: 190px;" onkeypress="return validateFloatKeyPress(this,event);"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="MRP is required!"
                                                        ControlToValidate="txtMRP" ValidateEmptyText="false" Display="None" SetFocusOnError="true"
                                                        ValidationGroup="submitvalidation"> </asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server"
                                                        TargetControlID="cv_txtMRP" PopupPosition="Right" HighlightCssClass="errormassage"
                                                        WarningIconImageUrl="../images/050.png">
                                                    </ajaxToolkit:ValidatorCalloutExtender>--%>
                                                </td>
                                                <td width="100" class="field_title">
                                                    <asp:Label ID="Label20" Text="Bangladeshi taka(Rs.)" runat="server"></asp:Label>
                                                </td>
                                                <td class="field_input">
                                                    <asp:TextBox ID="txtBangTaka" runat="server" Style="width: 190px;" onkeypress="return validateFloatKeyPress(this,event);"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="MRP is required!"
                                                        ControlToValidate="txtMRP" ValidateEmptyText="false" Display="None" SetFocusOnError="true"
                                                        ValidationGroup="submitvalidation"> </asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server"
                                                        TargetControlID="cv_txtMRP" PopupPosition="Right" HighlightCssClass="errormassage"
                                                        WarningIconImageUrl="../images/050.png">
                                                    </ajaxToolkit:ValidatorCalloutExtender>--%>
                                                </td>
                                            </tr>
                                            <tr style="display: none">
                                                <td width="90" class="field_title">
                                                    <asp:Label ID="lblbrandcategory" Text="Brand Category Name" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtbrandcategory" runat="server" Style="width: 190px;" MaxLength="30"
                                                        onkeypress="return onlyAlphabets(this,event);"></asp:TextBox>
                                                    <asp:Label ID="Label18" runat="server" Text="&nbsp;" Font-Bold="True"></asp:Label>

                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                                        TargetControlID="RequiredFieldValidator7" PopupPosition="Right" HighlightCssClass="errormassage"
                                                        WarningIconImageUrl="../images/050.png">
                                                    </ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                                <td class="field_title">
                                                    <asp:Label ID="Label22" Text="PRIMARY PRODUCT NAME" runat="server"></asp:Label>
                                                </td>
                                                <td class="field_input">
                                                    <asp:TextBox ID="txtprimaryproductname" runat="server" MaxLength="100" Style="width: 190px;"></asp:TextBox>

                                                </td>
                                            </tr>

                                            <tr style="display: none">
                                                <td class="field_title">
                                                    <asp:Label ID="lblAssessablepercentage" Text="Assessable" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td class="field_input">
                                                    <asp:TextBox ID="txtAssessablepercentage" runat="server" Text="0" Style="width: 190px;"
                                                        MaxLength="30" onkeypress="return validateFloatKeyPress(this,event);"></asp:TextBox>
                                                    <asp:Label ID="Label5" runat="server" Text="&nbsp;%" Font-Bold="True"></asp:Label>

                                                </td>
                                                <td class="field_title">
                                                    <asp:Label ID="lblMinstooklevel" Text="Min Stock Level" runat="server"></asp:Label>&nbsp;<span
                                                        class="req">*</span>
                                                </td>
                                                <td class="field_input">
                                                    <asp:TextBox ID="txtMinstooklevel" runat="server" Style="width: 190px;" MaxLength="30"
                                                        onkeypress="return validateFloatKeyPress(this,event);"></asp:TextBox>
                                                    <asp:Label ID="Label3" runat="server" Text="&nbsp;Pcs" Font-Bold="True"></asp:Label>
                                                    <%--<asp:RequiredFieldValidator ID="CV_txtMinstooklevel" runat="server" ErrorMessage="MinimumStockLevel is required!"
                                                        ControlToValidate="txtMinstooklevel" ValidateEmptyText="false" Display="None"
                                                        SetFocusOnError="true" ValidationGroup="submitvalidation"> </asp:RequiredFieldValidator>
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server"
                                                        TargetControlID="CV_txtMinstooklevel" PopupPosition="Right" HighlightCssClass="errormassage"
                                                        WarningIconImageUrl="../images/050.png">
                                                    </ajaxToolkit:ValidatorCalloutExtender>--%>
                                                </td>
                                                <td class="field_title">
                                                    <asp:Label ID="Label6" Text="CSD Index No." runat="server"></asp:Label>
                                                </td>
                                                <td class="field_input" colspan="2">
                                                    <asp:TextBox ID="txtindexno" runat="server" MaxLength="100" Style="width: 190px;"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="field_title" style="display: none">
                                                    <asp:Label ID="lblRefundable" Text="Returnable" runat="server"></asp:Label>
                                                </td>
                                                <td class="field_input" style="display: none">
                                                    <asp:CheckBox ID="chkRefundable" runat="server" Text=" " />
                                                </td>
                                            </tr>                                            
                                            <tr>
                                                <td class="field_title" style="display: none">
                                                    <asp:Label ID="Label40" Text="Billing Depot(GT)" runat="server"></asp:Label>
                                                </td>
                                                <td class="field_input" style="display: none">
                                                    <asp:DropDownList ID="ddlBillingDepotType" Width="200" runat="server" class="chosen-select"
                                                        data-placeholder="Choose Billing Depot" AppendDataBoundItems="True"
                                                        OnSelectedIndexChanged="ddlBillingDepotType_SelectedIndexChanged"
                                                        AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </td>

                                            </tr>
                                            <tr id="trDepotMap" runat="server">
                                                <td>
                                                    <asp:Label ID="lbltotalcount" runat="server" Text="" Font-Bold="true" ForeColor="Gray"
                                                        Font-Size="Small" Visible="false"></asp:Label></td>
                                                <td>
                                                    <div class="gridcontent-inner" id="trinvoice" runat="server" style="width: 100%">
                                                        <fieldset>
                                                            <legend>Depot List</legend>
                                                            <asp:GridView ID="gvBrandTypeMap" runat="server" Width="100%" CssClass="reportgrid"
                                                                AutoGenerateColumns="false" ShowFooter="true" AlternatingRowStyle-BackColor="AliceBlue"
                                                                EmptyDataText="No Records Available" OnRowDataBound="OnRowDataBound">
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            <asp:CheckBox ID="chkAllSelect" runat="server" onclick="CheckAllheader(this);" Text=" " />
                                                                        </HeaderTemplate>
                                                                        <HeaderStyle Width="2px" />
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkSelect" runat="server" Text=" " class="round" Style="float: left; padding-right: 1px;" ToolTip='<%# Bind("DEPOTID") %>' onclick="setrowcolor(this);" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="DEPOTID" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDEPOTID" runat="server" Text='<%# Bind("DEPOTID") %>'
                                                                                value='<%# Eval("DEPOTID") %>' Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Sl.No." ItemStyle-Wrap="false" ItemStyle-Width="2px" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <nav style="position: relative;"><span class="badge black"><%# Container.DataItemIndex + 1 %></span></nav>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Depot" ItemStyle-Width="80" ItemStyle-Wrap="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDEPOTNAME" runat="server" Text='<%# Bind("DEPOT") %>'
                                                                                value='<%# Eval("DEPOT") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="TAG" ItemStyle-Width="60" ItemStyle-Wrap="false" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblTAG" runat="server" Text='<%# Bind("TAG") %>'
                                                                                value='<%# Eval("TAG") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>

                                                        </fieldset>
                                                    </div>
                                                </td>
                                            </tr>

                                        </table>
                                    </div>

                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr id="trProductsubmit" runat="server">
                                            <td width="90" class="field_title">&nbsp;
                                            </td>
                                            <td style="padding: 8px 0;">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn_link" ValidationGroup="submitvalidation"
                                                        OnClick="btnSubmit_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn_link" OnClick="btnCancel_Click"
                                                        CausesValidation="false" />
                                                </div>
                                                <asp:HiddenField ID="Hdn_Fld" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlBS" runat="server">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                        <tr>
                                            <td width="120" class="field_title">
                                                <asp:Label ID="lblPO" Text="PRODUCT NAME" runat="server"></asp:Label><span class="req">*</span>
                                            </td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtPname" runat="server" CssClass="required large" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trBSTPU">
                                            <td class="field_input" style="padding-left: 10px;" colspan="2">
                                                <fieldset>
                                                    <legend>BUSINESS SEGMENT & TPU/VENDOR & FACTORY MAPPING</legend>
                                                    <table border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                                        <tr>
                                                            <td valign="top">
                                                                <cc1:Grid ID="gvPBMap" runat="server" FolderStyle="../GridStyles/premiere_blue" AutoGenerateColumns="false"
                                                                    AllowSorting="false" AllowPageSizeSelection="false" AllowPaging="false" AllowFiltering="false"
                                                                    PageSize="500" Serialize="true" AllowAddingRecords="false" OnRowDataBound="gvPBMap_RowDataBound">
                                                                    <FilteringSettings InitialState="Visible" />
                                                                    <Columns>
                                                                        <cc1:Column DataField="BSNAME" HeaderText="BUSINESS SEGMENT MAPPING " runat="server"
                                                                            Width="150" Wrap="true">
                                                                            <FilterOptions>
                                                                                <cc1:FilterOption Type="NoFilter" />
                                                                                <cc1:FilterOption Type="Contains" />
                                                                            </FilterOptions>
                                                                        </cc1:Column>
                                                                        <cc1:Column DataField="BSID" ReadOnly="true" HeaderText="ID" runat="server" Width="40"
                                                                            Wrap="true">
                                                                            <TemplateSettings TemplateId="CheckTemplate" HeaderTemplateId="HeaderTemplate" />
                                                                        </cc1:Column>
                                                                    </Columns>
                                                                    <FilteringSettings MatchingType="AnyFilter" />
                                                                    <Templates>

                                                                        <cc1:GridTemplate runat="server" ID="CheckTemplate">
                                                                            <Template>
                                                                                <asp:HiddenField runat="server" ID="BsName" Value='<%# Container.DataItem["BSNAME"] %>' />
                                                                                <asp:CheckBox ID="ChkID" runat="server" Text=" " ToolTip="<%# Container.Value %>"
                                                                                    Style="padding-left: 5px;" />
                                                                            </Template>
                                                                        </cc1:GridTemplate>
                                                                    </Templates>
                                                                    <ScrollingSettings ScrollWidth="100%" ScrollHeight="250" />
                                                                </cc1:Grid>
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
                                                            <td valign="top">
                                                                
                                                                    <cc1:Grid ID="gvPTPUMap" runat="server" FolderStyle="../GridStyles/premiere_blue" AutoGenerateColumns="false"
                                                                    AllowSorting="false" AllowPageSizeSelection="false" AllowPaging="false" AllowFiltering="false"
                                                                    PageSize="500" Serialize="true" AllowAddingRecords="false" OnRowDataBound="gvPTPUMap_RowDataBound">
                                                                    <FilteringSettings InitialState="Visible" />
                                                                    <Columns>
                                                                        <cc1:Column ID="Column3" DataField="VENDORNAME" HeaderText="TPU/VENDOR MAPPING" runat="server"
                                                                            Width="250" Wrap="true">
                                                                            <FilterOptions>
                                                                                <cc1:FilterOption Type="NoFilter" />
                                                                                <cc1:FilterOption Type="Contains" />
                                                                            </FilterOptions>
                                                                        </cc1:Column>
                                                                        <cc1:Column ID="Column4" DataField="VENDORID" ReadOnly="true" HeaderText="ID" runat="server"
                                                                            Width="40" Wrap="true">
                                                                            <TemplateSettings TemplateId="CheckTemplateTPU" HeaderTemplateId="HeaderTemplateTPU" />
                                                                        </cc1:Column>
                                                                    </Columns>
                                                                    <FilteringSettings MatchingType="AnyFilter" />
                                                                    <Templates>

                                                                        <cc1:GridTemplate runat="server" ID="CheckTemplateTPU">
                                                                            <Template>
                                                                                <asp:HiddenField runat="server" ID="hdnTPUName" Value='<%# Container.DataItem["VENDORNAME"] %>' />
                                                                                <asp:CheckBox ID="ChkIDTPU" runat="server" Text=" " ToolTip="<%# Container.Value %>" />
                                                                            </Template>
                                                                        </cc1:GridTemplate>
                                                                    </Templates>
                                                                    <ScrollingSettings ScrollWidth="100%" ScrollHeight="250" />
                                                                </cc1:Grid>
                                                            </td>

                                                            <td>&nbsp;
                                                            </td>
                                                            <td valign="top">
                                                                <cc1:Grid ID="gvFactory" runat="server" FolderStyle="../GridStyles/premiere_blue"
                                                                    AutoGenerateColumns="false" AllowSorting="false" AllowPageSizeSelection="false"
                                                                    AllowPaging="false" AllowFiltering="false" Serialize="true" AllowAddingRecords="false"
                                                                    OnRowDataBound="gvFactory_RowDataBound" PageSize="500">
                                                                    <FilteringSettings InitialState="Visible" />
                                                                    <Columns>
                                                                        <cc1:Column ID="Column7" DataField="VENDORNAME" HeaderText="FACTORY MAPPING" runat="server"
                                                                            Width="250" Wrap="true">
                                                                            <FilterOptions>
                                                                                <cc1:FilterOption Type="NoFilter" />
                                                                                <cc1:FilterOption Type="Contains" />
                                                                            </FilterOptions>
                                                                        </cc1:Column>
                                                                        <cc1:Column ID="Column8" DataField="VENDORID" ReadOnly="true" HeaderText="ID" runat="server"
                                                                            Width="40" Wrap="true">
                                                                            <TemplateSettings TemplateId="CheckTemplateFactory" HeaderTemplateId="HeaderTemplateFactory" />
                                                                        </cc1:Column>
                                                                    </Columns>
                                                                    <FilteringSettings MatchingType="AnyFilter" />
                                                                    <Templates>
                                                                        <cc1:GridTemplate runat="server" ID="CheckTemplateFactory">
                                                                            <Template>
                                                                                <asp:HiddenField runat="server" ID="hdnFactoryName" Value='<%# Container.DataItem["VENDORNAME"] %>' />
                                                                                <asp:CheckBox ID="ChkIDFactory" runat="server" Text=" " ToolTip="<%# Container.Value %>" />
                                                                            </Template>
                                                                        </cc1:GridTemplate>
                                                                    </Templates>
                                                                    <ScrollingSettings ScrollWidth="100%" ScrollHeight="250" />
                                                                </cc1:Grid>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trbtnBSTPU">
                                            <td colspan="2" class="field_input" style="padding-left: 10px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnBSTPUSave" runat="server" Text="Save" CssClass="btn_link" CausesValidation="false"
                                                        OnClick="btnBSTPUSave_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnBSTPUCancel" runat="server" Text="Close" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btnBSTPUCancel_Click" />
                                                </div>
                                            </td>
                                        </tr>

                                        <tr runat="server" id="trPackSize">
                                            <td colspan="2" class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>Packing Size Mapping Details</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                                        <tr>
                                                            <td width="90" class="field_input">
                                                                <asp:TextBox ID="txtUnitvalue" runat="server" onkeypress="return isNumberKeyWithDot(event);"
                                                                    MaxLength="10" ValidationGroup="Mapping" Width="80px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="CV_txtUnitvalue" runat="server" ControlToValidate="txtUnitvalue"
                                                                    Display="None" ErrorMessage="Unit value is required!" SetFocusOnError="true"
                                                                    ValidateEmptyText="false" ValidationGroup="Mapping"> </asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server"
                                                                    HighlightCssClass="errormassage" PopupPosition="BottomLeft" TargetControlID="CV_txtUnitvalue"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                                <br />
                                                                <span class=" label_intro">Unit&nbsp;Capacity <span class="req">*</span></span>
                                                            </td>
                                                            <td width="170" class="field_input">
                                                                <asp:DropDownList ID="ddlPackigsize1" runat="server" AppendDataBoundItems="true"
                                                                    class="chosen-select" data-placeholder="Choose a Packing Size" Style="width: 150px;">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="CV_ddlPackigsize1" runat="server" ControlToValidate="ddlPackigsize1"
                                                                    Display="None" ErrorMessage="PackigSize To is required!" InitialValue="0" SetFocusOnError="true"
                                                                    ValidateEmptyText="false" ValidationGroup="Mapping"> </asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server"
                                                                    HighlightCssClass="errormassage" PopupPosition="BottomLeft" TargetControlID="CV_ddlPackigsize1"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                                <span class=" label_intro">Packing Size From <span class="req">*</span></span>
                                                            </td>
                                                            <td width="70" valign="top" style="padding-top: 15px;" class="field_title">Make 1
                                                            </td>
                                                            <td width="170" class="field_input">
                                                                <asp:DropDownList ID="ddlPackigsize" runat="server" AppendDataBoundItems="true" class="chosen-select"
                                                                    Style="width: 150px;" data-placeholder="Choose a Packing Size" ValidationGroup="Mapping">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="CV_ddlPackigsize" runat="server" Display="None" ErrorMessage="PackigSize From is required!"
                                                                    ControlToValidate="ddlPackigsize" ValidateEmptyText="false" SetFocusOnError="true"
                                                                    InitialValue="0" ValidationGroup="Mapping"> </asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server"
                                                                    TargetControlID="CV_ddlPackigsize" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                                <br />
                                                                <span class=" label_intro">Packing Size To <span class="req">*</span></span>
                                                            </td>
                                                            <td width="90" class="field_input">
                                                                <asp:TextBox ID="txtGrossWeight" runat="server" onkeypress="return isNumberKeyWithDot(event);"
                                                                    MaxLength="10" ValidationGroup="Mapping" Width="80px" Text="0"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorGrossWeight" runat="server"
                                                                    ControlToValidate="txtGrossWeight" Display="None" ErrorMessage="required!" SetFocusOnError="true"
                                                                    ValidateEmptyText="false" ValidationGroup="Mapping"> </asp:RequiredFieldValidator>
                                                                <br />
                                                                <span class=" label_intro">Gross&nbsp;Weight <span class="req">*</span></span>
                                                            </td>
                                                            <td width="140" class="field_input">
                                                                <asp:DropDownList ID="ddlUOMPacksize" runat="server" AppendDataBoundItems="true"
                                                                    class="chosen-select" Style="width: 120px;" data-placeholder="Choose UOM" ValidationGroup="Mapping">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorUOM" runat="server" Display="None"
                                                                    ErrorMessage="required!" ControlToValidate="ddlUOMPacksize" ValidateEmptyText="false"
                                                                    SetFocusOnError="true" InitialValue="0" ValidationGroup="Mapping"> </asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtenderUOM" runat="server"
                                                                    TargetControlID="CV_ddlPackigsize" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                                <br />
                                                                <span class=" label_intro">UOM <span class="req">*</span></span>
                                                            </td>
                                                            <td valign="top" class="field_input">
                                                                <div class="btn_24_blue">
                                                                    <span class="icon add_co"></span>
                                                                    <asp:Button ID="btnADDGrid" runat="server" CssClass="btn_link" OnClick="btnADDGrid_Click"
                                                                        Text="Add" ValidationGroup="Mapping" />
                                                                </div>
                                                            </td>
                                                            <td>&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_input" colspan="8">
                                                                <cc1:Grid ID="gvPackingsizeMappingAdd" runat="server" AllowAddingRecords="false"
                                                                    AllowColumnResizing="true" AllowFiltering="false" AllowPageSizeSelection="false"
                                                                    AllowSorting="false" AutoGenerateColumns="false" CallbackMode="true" EnableRecordHover="true"
                                                                    PageSize="200" FolderStyle="../GridStyles/premiere_blue" Serialize="true" AllowPaging="false">
                                                                    <FilteringSettings InitialState="Visible" />
                                                                    <Columns>
                                                                        <cc1:Column ID="Column1" runat="server" DataField="SLNO" HeaderText="Sl No" Width="70"
                                                                            Visible="false">
                                                                        </cc1:Column>
                                                                        <cc1:Column ID="Column14" runat="server" DataField="PACKSIZEID_FROM" HeaderText="PACKSIZEName_FROM"
                                                                            ReadOnly="true" Visible="false" Width="60" />
                                                                        <cc1:Column ID="Column17" runat="server" DataField="PACKSIZEID_TO" HeaderText="PACKSIZEID_TO"
                                                                            ReadOnly="true" Visible="false" Width="60" />
                                                                        <cc1:Column ID="Column5" runat="server" DataField="CONVERSIONQTY" HeaderText="Unit&nbsp;Capacity"
                                                                            Width="100">
                                                                        </cc1:Column>
                                                                        <cc1:Column ID="Column10" runat="server" DataField="PACKSIZEName_FROM" HeaderText="Packing Size From"
                                                                            Width="200">
                                                                        </cc1:Column>
                                                                        <cc1:Column ID="Column6" runat="server" DataField="PACKSIZEName_TO" HeaderText="Packing Size To"
                                                                            Width="200">
                                                                        </cc1:Column>
                                                                        <cc1:Column ID="Column9" runat="server" DataField="GROSSWEIGHT" HeaderText="Gross Weight"
                                                                            Width="100">
                                                                        </cc1:Column>
                                                                        <cc1:Column ID="Column11" runat="server" DataField="UOMID" HeaderText="UOMID" Width="60"
                                                                            Visible="false">
                                                                        </cc1:Column>
                                                                        <cc1:Column ID="Column12" runat="server" DataField="UOMNAME" HeaderText="UOM" Width="100">
                                                                        </cc1:Column>
                                                                        <cc1:Column AllowDelete="true" AllowEdit="false" HeaderText="Delete" Width="65">
                                                                            <TemplateSettings TemplateId="deleteBtnTemplateMap" />
                                                                        </cc1:Column>
                                                                    </Columns>
                                                                    <FilteringSettings MatchingType="AnyFilter" />
                                                                    <Templates>
                                                                        <cc1:GridTemplate ID="deleteBtnTemplateMap" runat="server">
                                                                            <Template>
                                                                                <a href="javascript: //" id="btnGridDelete_<%# Container.PageRecordIndex %>" class="action-icons c-delete"
                                                                                    onclick="CallDeleteServerMethod(this)"></a>
                                                                            </Template>
                                                                        </cc1:GridTemplate>
                                                                    </Templates>
                                                                    <ScrollingSettings ScrollHeight="250" />
                                                                </cc1:Grid>
                                                                <asp:Button ID="btngrddelete" runat="server" CausesValidation="false" OnClick="btngrddelete_Click"
                                                                    OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                                                    Style="display: none" Text="grddelete" />
                                                                <asp:HiddenField ID="hdnMappingDelete" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trbtnPacksize">
                                            <td colspan="2" class="field_input" style="padding-left: 10px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnPackSizeSubmit" runat="server" Text="Save" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btnPackSizeSubmit_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnPackSizeCancel" runat="server" Text="Close" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btnPackSizeCancel_Click" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trDepot">
                                            <td colspan="2" class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>Depotwise Re-order Lavel</legend>
                                                    <table cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td width="24%" class="field_input">
                                                                <asp:DropDownList ID="ddlDeopt" runat="server" AppendDataBoundItems="true" class="chosen-select"
                                                                    data-placeholder="Choose a Depot" Style="width: 220px;">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlDeopt"
                                                                    Display="None" ErrorMessage="Depot To is required!" InitialValue="0" SetFocusOnError="true"
                                                                    ValidateEmptyText="false" ValidationGroup="MappingReorder"> </asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server"
                                                                    HighlightCssClass="errormassage" PopupPosition="BottomLeft" TargetControlID="RequiredFieldValidator9"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                                <br />
                                                                <span class=" label_intro">Depot <span class="req">*</span></span>
                                                            </td>
                                                            <td width="11%" class="field_input">
                                                                <asp:TextBox ID="txtRorder" runat="server" CssClass="large" ValidationGroup="Mapping"
                                                                    Width="80px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtRorder"
                                                                    Display="None" ErrorMessage="Re-order is required!" SetFocusOnError="true" ValidateEmptyText="false"
                                                                    ValidationGroup="MappingReorder"> </asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                                    HighlightCssClass="errormassage" PopupPosition="BottomLeft" TargetControlID="RequiredFieldValidator8"
                                                                    WarningIconImageUrl="../images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                                <br />
                                                                <span class=" label_intro">Re-order Lavel <span class="req">*</span></span>
                                                            </td>
                                                            <td valign="top" class="field_input">
                                                                <div class="btn_24_blue">
                                                                    <span class="icon add_co"></span>
                                                                    <asp:Button ID="btnAddReorder" runat="server" CssClass="btn_link" OnClick="btnADDgvReorder_Click"
                                                                        Text="Add" ValidationGroup="MappingReorder" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="5" class="field_input">
                                                                <cc1:Grid ID="gvReorder" runat="server" AllowAddingRecords="false" AllowColumnResizing="true"
                                                                    AllowFiltering="false" AllowPageSizeSelection="false" PageSize="200" AllowSorting="false"
                                                                    AutoGenerateColumns="false" CallbackMode="true" EnableRecordHover="true" AllowPaging="false"
                                                                    FolderStyle="../GridStyles/premiere_blue" Serialize="true">
                                                                    <FilteringSettings InitialState="Visible" />
                                                                    <Columns>
                                                                        <cc1:Column runat="server" DataField="SLNO" HeaderText="Sl No" Width="70">
                                                                        </cc1:Column>
                                                                        <cc1:Column ID="Column2" runat="server" DataField="DEPOTID" Visible="false" HeaderText="DEPOT id"
                                                                            ReadOnly="true" Width="200" />
                                                                        <cc1:Column runat="server" DataField="DEPOTNAME" HeaderText="DEPOT" ReadOnly="true"
                                                                            Width="200" />
                                                                        <cc1:Column runat="server" DataField="REORDERLAVEL" HeaderText="RE-ORDER LAVEL" Width="200">
                                                                        </cc1:Column>
                                                                        <cc1:Column AllowDelete="true" AllowEdit="false" HeaderText="Delete" Width="65">
                                                                            <TemplateSettings TemplateId="deleteBtnReorderTemplateMap" />
                                                                        </cc1:Column>
                                                                    </Columns>
                                                                    <FilteringSettings MatchingType="AnyFilter" />
                                                                    <Templates>
                                                                        <cc1:GridTemplate ID="deleteBtnReorderTemplateMap" runat="server">
                                                                            <Template>
                                                                                <a id="btnGridReorderDelete_<%# Container.PageRecordIndex %>" class="action-icons c-delete"
                                                                                    href="javascript: //" onclick="CallDeleteServerMethodForReorder(this)"></a>
                                                                            </Template>
                                                                        </cc1:GridTemplate>
                                                                    </Templates>
                                                                    <ScrollingSettings ScrollWidth="100%" ScrollHeight="250" />
                                                                </cc1:Grid>
                                                                <asp:Button ID="btngridreorder" runat="server" CausesValidation="false" OnClick="btngridreorder_Click"
                                                                    OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                                                    Style="display: none" Text="grddelete" />
                                                                <asp:HiddenField ID="hdnReorder" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trbtnPackSizeDepot">
                                            <td colspan="2" class="field_input" style="padding-left: 10px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnPBMapSubmit" runat="server" Text="Submit" CssClass="btn_link"
                                                        OnClick="btnPBMapSubmit_Click" CausesValidation="false" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnPBMapCancel" runat="server" Text="Cancel" CssClass="btn_link"
                                                        OnClick="btnPBMapCancel_Click" CausesValidation="false" />
                                                </div>
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trGroup">
                                            <td class="field_input" style="padding-left: 10px;" colspan="2">
                                                <fieldset>
                                                    <legend>GROUP MAPPING</legend>
                                                    <table border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                                        <tr>
                                                            <td class="field_title" width="130">
                                                                <asp:Label ID="Label1" Text="BUSINESS SEGMENT" runat="server"></asp:Label>&nbsp;<span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlbusinesssegment" runat="server" AppendDataBoundItems="true"
                                                                    Width="198" Height="28" class="chosen-select" data-placeholder="Choose a Business Segment"
                                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlbusinesssegment_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 8px 0px;" colspan="2">
                                                                <div class="gridcontent-short">
                                                                    <ul>
                                                                        <li class="left">
                                                                            <cc1:Grid ID="gvgroup" runat="server" FolderStyle="../GridStyles/grand_gray" AutoGenerateColumns="false"
                                                                                AllowSorting="false" AllowPageSizeSelection="false" PageSize="200" AllowPaging="false"
                                                                                AllowFiltering="false" Serialize="true" AllowAddingRecords="false">
                                                                                <FilteringSettings InitialState="Visible" />
                                                                                <Columns>
                                                                                    <cc1:Column ID="Column13" DataField="DIS_CATNAME" HeaderText="GROUP MAPPING" runat="server"
                                                                                        Width="450px">
                                                                                        <FilterOptions>
                                                                                            <cc1:FilterOption Type="NoFilter" />
                                                                                            <cc1:FilterOption Type="Contains" />
                                                                                        </FilterOptions>
                                                                                    </cc1:Column>
                                                                                    <cc1:Column ID="Column15" DataField="DIS_CATID" ReadOnly="true" HeaderText="ID" runat="server"
                                                                                        Width="50">
                                                                                        <TemplateSettings TemplateId="CheckTemplateGroup" HeaderTemplateId="HeaderTemplate" />
                                                                                    </cc1:Column>
                                                                                </Columns>
                                                                                <FilteringSettings MatchingType="AnyFilter" />
                                                                                <Templates>
                                                                                    <cc1:GridTemplate runat="server" ID="GridTemplate1">
                                                                                        <Template>
                                                                                            <asp:HiddenField runat="server" ID="GROUPNAME" Value='<%# Container.DataItem["DIS_CATNAME"] %>' />
                                                                                            <asp:CheckBox ID="Chkgroup" runat="server" Text=" " ToolTip="<%# Container.Value %>" />
                                                                                        </Template>
                                                                                    </cc1:GridTemplate>
                                                                                </Templates>
                                                                                <ScrollingSettings ScrollWidth="100%" ScrollHeight="180" />
                                                                            </cc1:Grid>
                                                                        </li>
                                                                        <li class="middle">
                                                                            <div class="btn_24_blue" style="padding-left: 10px;">
                                                                                <span class="icon add_co"></span>
                                                                                <asp:Button ID="btngroupadd" runat="server" ValidationGroup="ADD" Text=" " OnClick="btngroupadd_Click"
                                                                                    CssClass="btnsw_link" />
                                                                            </div>
                                                                        </li>
                                                                        <li class="right">
                                                                            <cc1:Grid ID="gvgroupadd" runat="server" Serialize="true" AllowAddingRecords="false"
                                                                                AutoGenerateColumns="false" AllowSorting="true" AllowPageSizeSelection="false"
                                                                                FolderStyle="../GridStyles/grand_gray" AllowPaging="false" PageSize="200">
                                                                                <Columns>
                                                                                    <cc1:Column ID="Column18" DataField="SlNo" HeaderText="SL" runat="server" Width="60">
                                                                                        <TemplateSettings TemplateId="slnoTemplateFinal" />
                                                                                    </cc1:Column>
                                                                                    <cc1:Column ID="Column16" DataField="GUID" HeaderText="GUID" runat="server" Visible="false" />
                                                                                    <cc1:Column ID="Column19" DataField="PRODUCTID" HeaderText="PRODUCTID NAME" Visible="false"
                                                                                        runat="server">
                                                                                    </cc1:Column>
                                                                                    <cc1:Column ID="Column20" DataField="PRODUCTNAME" HeaderText="PRODUCTNAME" Width="300"
                                                                                        runat="server">
                                                                                    </cc1:Column>
                                                                                    <cc1:Column ID="Column21" DataField="BSID" HeaderText="BSID" runat="server" Visible="false">
                                                                                    </cc1:Column>
                                                                                    <cc1:Column ID="Column22" DataField="BSNAME" HeaderText="BSNAME" runat="server">
                                                                                    </cc1:Column>
                                                                                    <cc1:Column ID="Column23" DataField="GROUPID" HeaderText="GROUPID" runat="server"
                                                                                        Visible="false">
                                                                                    </cc1:Column>
                                                                                    <cc1:Column ID="Column24" DataField="GROUPNAME" HeaderText="GROUPNAME" runat="server">
                                                                                    </cc1:Column>
                                                                                    <cc1:Column HeaderText="Delete" AllowEdit="false" AllowDelete="true" Width="80">
                                                                                        <TemplateSettings TemplateId="deleteBtnTemplategroup" />
                                                                                    </cc1:Column>
                                                                                </Columns>
                                                                                <Templates>
                                                                                    <cc1:GridTemplate runat="server" ID="deleteBtnTemplategroup">
                                                                                        <Template>
                                                                                            <a href="javascript: //" class="action-icons c-delete" id="btnGridDelete_<%# Container.PageRecordIndex %>"
                                                                                                onclick="CallGroupServerMethod(this)"></a>
                                                                                        </Template>
                                                                                    </cc1:GridTemplate>
                                                                                    <cc1:GridTemplate runat="server" ID="slnoTemplateFinal">
                                                                                        <Template>
                                                                                            <asp:Label ID="lblslnoTaxFinal" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                                                        </Template>
                                                                                    </cc1:GridTemplate>
                                                                                </Templates>
                                                                                <ScrollingSettings ScrollWidth="100%" ScrollHeight="180" />
                                                                            </cc1:Grid>
                                                                            <asp:Button ID="btngroupdelete" runat="server" CausesValidation="false" Text="grddelete"
                                                                                Style="display: none" OnClick="btngroupdelete_Click" OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                                                                            <asp:HiddenField ID="hdngroupDelete" runat="server" />
                                                                        </li>
                                                                    </ul>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr runat="server" id="trGroupbtn">
                                            <td class="field_title" colspan="2">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btngroupsubmit" runat="server" Text="Save" CssClass="btn_link" CausesValidation="false"
                                                        OnClick="btngroupsubmit_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btngroupcancel" runat="server" Text="Cancel" CssClass="btn_link"
                                                        CausesValidation="false" OnClick="btngroupcancel_Click" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlDistributor" runat="server">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                        <tr>
                                            <td class="field_title" width="120">
                                                <asp:Label ID="lbldisproductname" Text="PRODUCT NAME" runat="server"></asp:Label>
                                            </td>
                                            <td class="field_input">
                                                <asp:TextBox ID="txtdistproductname" runat="server" CssClass="large" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;
                                            </td>
                                            <td class="field_input">
                                                <fieldset style="width: 50%;">
                                                    <legend>Distributor Details</legend>
                                                    <cc1:Grid ID="grddistributor" runat="server" Serialize="true" AllowAddingRecords="false"
                                                        AutoGenerateColumns="false" AllowSorting="false" AllowPageSizeSelection="false"
                                                        EnableRecordHover="true" PageSize="500" FolderStyle="../GridStyles/premiere_blue"
                                                        AllowPaging="false" AllowFiltering="false" OnRowDataBound="grddistributor_RowDataBound">
                                                        <Columns>
                                                            <cc1:Column ID="Column26" DataField="USERNAME" runat="server" HeaderText="NAME" Wrap="true"
                                                                Width="80%">
                                                            </cc1:Column>
                                                            <cc1:Column ID="Column27" DataField="USERID" runat="server" HeaderText="ID" ReadOnly="true"
                                                                Wrap="true" Width="20%">
                                                                <TemplateSettings TemplateId="chkTempDistributor" HeaderTemplateId="TypeHeaderChkbox" />
                                                            </cc1:Column>
                                                        </Columns>
                                                        <Templates>
                                                            <cc1:GridTemplate runat="server" ID="TypeHeaderChkbox">
                                                                <Template>
                                                                    <asp:CheckBox ID="chkboxSelectAll" runat="server" Text=" " onclick="toggleTypeSelection(this);" />
                                                                </Template>
                                                            </cc1:GridTemplate>
                                                            <cc1:GridTemplate runat="server" ID="chkTempDistributor">
                                                                <Template>
                                                                    <asp:HiddenField runat="server" ID="hdnDistributorname" Value='<%# Container.DataItem["USERNAME"] %>' />
                                                                    <asp:CheckBox ID="ChkIDDistid" runat="server" Text=" " ToolTip="<%# Container.Value %>"
                                                                        onclick="toggleTypeheaderSelectionchecked(this);" />
                                                                </Template>
                                                            </cc1:GridTemplate>
                                                        </Templates>
                                                        <ScrollingSettings ScrollHeight="220" ScrollWidth="100%" />
                                                    </cc1:Grid>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;
                                            </td>
                                            <td class="field_input" style="padding-left: 8px;">
                                                <div class="btn_24_blue">
                                                    <span class="icon disk_co"></span>
                                                    <asp:Button ID="btnDistributorSubmit" runat="server" Text="Submit" CausesValidation="false"
                                                        CssClass="btn_link" OnClick="btnDistributorSubmit_Click" />
                                                </div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <div class="btn_24_blue">
                                                    <span class="icon cross_octagon_co"></span>
                                                    <asp:Button ID="btnDistributorCancel" runat="server" Text="Cancel" CausesValidation="false"
                                                        CssClass="btn_link" OnClick="btnDistributorCancel_Click" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlDisplay" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <td width="110" class="field_title">
                                            <asp:Label ID="Label7" Text="Brand (stage 1)" runat="server"></asp:Label>
                                        </td>
                                        <td width="180" class="field_input">
                                            <asp:DropDownList ID="ddlsearchbrand" runat="server" AppendDataBoundItems="true"
                                                Width="170" Height="28" class="chosen-select" data-placeholder="Choose a Brand"
                                                OnSelectedIndexChanged="ddlsearchbrand_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="110" class="field_title">
                                            <asp:Label ID="Label8" Text="Category (stage 2)" runat="server"></asp:Label>
                                        </td>
                                        <td width="240" class="field_input">
                                            <asp:DropDownList ID="ddlsearchCategory" runat="server" AppendDataBoundItems="true"
                                                Width="230" Height="28" class="chosen-select" data-placeholder="Choose a Category"
                                                OnSelectedIndexChanged="ddlsearchCategory_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td class="field_input" id="excel" runat="server">
                                            <div class="btn_24_blue">
                                                <span class="icon doc_excel_table_co"></span><a href="#" onclick="exportToExcel();">
                                                    <asp:Button ID="btnExport" runat="server" Text="Export Excel" CssClass="btn_link" /></a>
                                            </div>
                                        </td>
                                        <td class="field_input" style="display: none">
                                            <div class="btn_24_blue">
                                                <span class="icon doc_excel_table_co"></span>
                                                <asp:Button ID="btngeneratetemp" runat="server" Text="Generate Template" CssClass="btn_link"
                                                    OnClick="btngeneratetemp_Click" CausesValidation="false" />
                                            </div>
                                        </td>
                                    </table>
                                    <ul id="search_box">
                                        <li>
                                            <input name="" id="FilterTextBox" type="text" class="search_input" placeholder="Enter text to search the whole grid"
                                                onkeyup="FilterTextBox_KeyUp();">
                                        </li>
                                        <li>
                                            <input name="" id="OboutButton2" type="submit" value="" class="search_btn" onclick="return performSearch();">
                                        </li>
                                    </ul>
                                    <div class="gridcontent">
                                        <cc1:Grid ID="gvProduct" runat="server" CallbackMode="true" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                            AutoGenerateColumns="false" PageSize="500" OnDeleteCommand="DeleteRecord" EnableRecordHover="true"
                                            OnRowDataBound="gvProduct_RowDataBound" AllowAddingRecords="false" AllowPaging="true"
                                            AllowFiltering="true" AllowPageSizeSelection="true" AllowGrouping="false" OnExported="gvProduct_Exported"
                                            OnExporting="gvProduct_Exporting">
                                            <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnClientDelete="OnDelete" />
                                            <ExportingSettings ExportColumnsFooter="true" ExportGroupFooter="true" ExportGroupHeader="true" AppendTimeStamp="false"
                                                FileName="ProductDetails" ExportAllPages="true" ExportDetails="true" />
                                            <FilteringSettings InitialState="Visible" />
                                            <Columns>

                                                <cc1:Column DataField="ID" ReadOnly="true" HeaderText="ID" runat="server" Visible="true" />
                                                <cc1:Column ID="Column32" DataField="SlNo" HeaderText="SL" runat="server" Width="50px">
                                                    <TemplateSettings TemplateId="slno" />
                                                </cc1:Column>
                                                <cc1:Column DataField="PRODUCTALIAS" HeaderText="PRODUCT" runat="server" Width="300"
                                                    Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column25" DataField="CODE" HeaderText="CODE" runat="server" Width="130"
                                                    Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column DataField="DIVNAME" HeaderText="BRAND (STAGE 1)" runat="server" Width="140" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column30" DataField="CATNAME" HeaderText="CATAGORY (STAGE 2)" runat="server"
                                                    Width="150" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column DataField="FRGNAME" HeaderText="Fragrance" runat="server" Width="100"
                                                    Visible="false" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column29" DataField="UOMNAME" HeaderText="UNIT" runat="server" Width="80"
                                                    Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column28" DataField="MRP" HeaderText="MRP" runat="server" Width="60"
                                                    Align="right" Wrap="true">
                                                </cc1:Column>
                                                <cc1:Column DataField="ACTIVE" HeaderText="STATUS" runat="server" Width="75">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column31" DataField="CSDINDEXNO" HeaderText="CSD INDEX NO" runat="server"
                                                    Width="115" Wrap="true" Visible="false">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column AllowEdit="true" AllowDelete="true" HeaderText="EDIT" runat="server"
                                                    Width="60">
                                                    <TemplateSettings TemplateId="editBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="DELETE" AllowEdit="false" AllowDelete="true" Width="70" Visible="false">
                                                    <TemplateSettings TemplateId="deleteBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="Pack Size Mapping " AllowEdit="false" AllowDelete="true"
                                                    Align="center" Width="70" Wrap="true">
                                                    <TemplateSettings TemplateId="MappingTemplatePackSize" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="BS/ Factory/ TPU Mapping " AllowEdit="false" AllowDelete="true"
                                                    Align="center" Width="90" Wrap="true">
                                                    <TemplateSettings TemplateId="MappingTemplate" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="Depot Reorder Mapping " AllowEdit="false" AllowDelete="true"
                                                    Align="center" Width="90" Wrap="true">
                                                    <TemplateSettings TemplateId="MappingTemplateReorder" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="Group Mapping " AllowEdit="false" AllowDelete="true" Width="90"
                                                    Align="center" Wrap="true" Visible="false">
                                                    <TemplateSettings TemplateId="MappingTemplateGroup" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="State/ District Mapping " AllowEdit="false" AllowDelete="true"
                                                    Align="center" Width="90" Wrap="true">
                                                    <TemplateSettings TemplateId="MappingTemplateTerritory" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="Distributor Mapping" AllowEdit="false" AllowDelete="true"
                                                    Align="center" Width="80" Wrap="true">
                                                    <TemplateSettings TemplateId="mappingtepDistributor" />
                                                </cc1:Column>
                                            </Columns>
                                            <FilteringSettings MatchingType="AnyFilter" />
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="editBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-edit" id="btnGridEdit_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethod(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                                <cc1:GridTemplate runat="server" ID="slno">
                                                    <Template>
                                                        <nav style="position: relative;">
                                                            <span class="badge black">
                                                                <asp:Label ID="lblslno" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label></span>
                                                        </nav>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="deleteBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="gvProduct.delete_record(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="MappingTemplatePackSize">
                                                    <Template>
                                                        <a href="javascript: //" class="filter_btn airplane" title="Packing Size Mapping"
                                                            onclick="openBSMappingNew('<%# Container.DataItem["PRODUCTALIAS"] %>','<%# Container.DataItem["ID"] %>')" />
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="MappingTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="filter_btn electricity_input" title="Business Segment & TPU Mapping"
                                                            onclick="openBSMapping('<%# Container.DataItem["PRODUCTALIAS"] %>','<%# Container.DataItem["ID"] %>')" />
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>

                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="MappingTemplateReorder">
                                                    <Template>
                                                        <a href="javascript: //" class="filter_btn eyedropper" title="Depot Reorder Mapping"
                                                            onclick="openDepotReorderMapping('<%# Container.DataItem["PRODUCTALIAS"] %>','<%# Container.DataItem["ID"] %>')" />
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="MappingTemplateGroup">
                                                    <Template>
                                                        <a href="javascript: //" class="h_icon map" title="Group Mapping" onclick="openGroupMapping('<%# Container.DataItem["PRODUCTALIAS"] %>','<%# Container.DataItem["ID"] %>')" />
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="MappingTemplateTerritory">
                                                    <Template>
                                                        <a href="javascript: //" class="h_icon zoom_sl" title="Territory Mapping" onclick="openTerritoryMapping('<%# Container.DataItem["PRODUCTALIAS"] %>','<%# Container.DataItem["ID"] %>')" />
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="mappingtepDistributor">
                                                    <Template>
                                                        <a href="javascript: //" class="h_icon map" title="Distributor Mapping" onclick="openDistributorMapping('<%# Container.DataItem["PRODUCTALIAS"] %>','<%# Container.DataItem["ID"] %>')" />
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <ScrollingSettings ScrollWidth="100%" ScrollHeight="290" NumberOfFixedColumns="3" />
                                        </cc1:Grid>
                                        <asp:HiddenField runat="server" ID="hdnPid" />
                                        <asp:HiddenField runat="server" ID="hdnPid1" />
                                        <asp:Button ID="btngridsave" runat="server" Text="GridSave" Style="display: none"
                                            OnClick="btngridsave_Click" CausesValidation="false" />
                                        <asp:Button ID="btngrideditdistributor" runat="server" Text="Distributor Mapping "
                                            Style="display: none" OnClick="btngrideditdistributor_Click" CausesValidation="false" />
                                        <asp:HiddenField ID="hdnPName" runat="server" />
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <cc1:MessageBox ID="MessageBox1" runat="server" />
                    <span class="clear"></span>
                </div>
                <span class="clear"></span>
                <script type="text/javascript">
                    function OnBeforeDelete(record) {
                        record.Error = '';
                        document.getElementById("<%=Hdn_Fld.ClientID %>").value = record.ID;
                        if (confirm("Are you sure you want to delete? "))
                            return true;
                        else
                            return false;
                    }
                    function OnDelete(record) {
                        alert(record.Error);
                    }
                </script>
                <script type="text/javascript">

                    function CallGroupServerMethod(oLink) {
                        document.getElementById("<%=hdngroupDelete.ClientID %>").value = '';
                        var iRecordIndex = oLink.id.toString().replace("btnGridDelete_", "");
                        document.getElementById("<%=hdngroupDelete.ClientID %>").value = gvgroupadd.Rows[iRecordIndex].Cells[1].Value;
                        document.getElementById("<%=btngroupdelete.ClientID %>").click();
                    }
                </script>
                <script type="text/javascript">
                    function isNumber(evt) {
                        var iKeyCode = (evt.which) ? evt.which : evt.keyCode
                        if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                            return false;

                        return true;

                    }
                </script>
                <script type="text/javascript">
                    var searchTimeout = null;
                    function FilterTextBox_KeyUp() {
                        searchTimeout = window.setTimeout(performSearch, 500);
                    }

                    function performSearch() {
                        var searchValue = document.getElementById('FilterTextBox').value;
                        if (searchValue == FilterTextBox.WatermarkText) {
                            searchValue = '';
                        }
                        gvProduct.addFilterCriteria('CODE', OboutGridFilterCriteria.Contains, searchValue);
                        gvProduct.addFilterCriteria('PRODUCTALIAS', OboutGridFilterCriteria.Contains, searchValue);
                        gvProduct.addFilterCriteria('NATURENAME', OboutGridFilterCriteria.Contains, searchValue);
                        gvProduct.addFilterCriteria('FRGNAME', OboutGridFilterCriteria.Contains, searchValue);
                        gvProduct.executeFilter();
                        searchTimeout = null;
                        return false;
                    }
                </script>
                <script type="text/javascript">
                    function exportToExcel() {
                        gvProduct.exportToExcel();
                    }
                </script>
                <script type="text/javascript">
                    function CallServerMethod(oLink) {
                        document.getElementById("<%=hdnPid.ClientID %>").value = '';
                        var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                        document.getElementById("<%=Hdn_Fld.ClientID %>").value = gvProduct.Rows[iRecordIndex].Cells[0].Value;
                        document.getElementById("<%=btngridsave.ClientID %>").click();
                    }


                    function CallDeleteServerMethodForReorder(oLink) {
                        document.getElementById("<%=hdnReorder.ClientID %>").value = '';
                        var iRecordIndex = oLink.id.toString().replace("btnGridReorderDelete_", "");
                        document.getElementById("<%=hdnReorder.ClientID %>").value = gvReorder.Rows[iRecordIndex].Cells[0].Value;
                        document.getElementById("<%=btngridreorder.ClientID %>").click();
                    }

                    function openBSMapping(pname, pid) {
                        document.getElementById("<%=hdnPid.ClientID %>").value = pid;
                        document.getElementById("<%=hdnPid1.ClientID %>").value = "BS";
                        document.getElementById("<%=txtPname.ClientID %>").value = pname;
                        document.getElementById("<%=btngridsave.ClientID %>").click();
                    }

                    function openBSMappingNew(pname, pid) {
                        document.getElementById("<%=hdnPid.ClientID %>").value = pid;
                        document.getElementById("<%=hdnPid1.ClientID %>").value = "PS";
                        document.getElementById("<%=txtPname.ClientID %>").value = pname;
                        document.getElementById("<%=btngridsave.ClientID %>").click();
                    }

                    function openDepotReorderMapping(pname, pid) {
                        document.getElementById("<%=hdnPid.ClientID %>").value = pid;
                        document.getElementById("<%=hdnPid1.ClientID %>").value = "DR";
                        document.getElementById("<%=txtPname.ClientID %>").value = pname;
                        document.getElementById("<%=btngridsave.ClientID %>").click();
                    }

                    function openTerritoryMapping(pname, pid) {
                        document.getElementById("<%=hdnPid.ClientID %>").value = pid;
                        document.getElementById("<%=hdnPid1.ClientID %>").value = "TR";
                        document.getElementById("<%=hdnPName.ClientID %>").value = pname;
                        document.getElementById("<%=btngridsave.ClientID %>").click();
                    }

                    function openGroupMapping(pname, pid) {
                        document.getElementById("<%=hdnPid.ClientID %>").value = pid;
                        document.getElementById("<%=hdnPid1.ClientID %>").value = "GR";
                        document.getElementById("<%=txtPname.ClientID %>").value = pname;
                        document.getElementById("<%=btngridsave.ClientID %>").click();
                    }

                    function openDistributorMapping(pname, pid) {
                        document.getElementById("<%=Hdn_Fld.ClientID %>").value = pid;
                        document.getElementById("<%=txtdistproductname.ClientID %>").value = pname;
                        document.getElementById("<%=btngrideditdistributor.ClientID %>").click();
                    }
                </script>
                <script language="Javascript" type="text/javascript"> 

                    function onlyAlphabets(e, t) {
                        try {
                            if (window.event) {
                                var charCode = window.event.keyCode;
                            }
                            else if (e) {
                                var charCode = e.which;
                            }
                            else { return true; }
                            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || (charCode == 32))
                                return true;
                            else
                                return false;
                        }
                        catch (err) {
                            alert(err.Description);
                        }
                    }

                </script>
                <script type="text/javascript">
                    function validateFloatKeyPress(el, evt) {
                        var charCode = (evt.which) ? evt.which : event.keyCode;

                        if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                            return false;
                        }

                        if (charCode == 46 && el.value.indexOf(".") !== -1) {
                            return false;
                        }

                        if (el.value.indexOf(".") !== -1) {
                            var range = document.selection.createRange();

                            if (range.text != "") {
                            }
                            else {
                                var number = el.value.split('.');
                                if (number.length == 2 && number[1].length > 1)
                                    return false;
                            }
                        }

                        return true;
                    }
                </script>

                <script type="text/javascript">
                    oboutGrid.prototype._resizeColumn = oboutGrid.prototype.resizeColumn;
                    oboutGrid.prototype.resizeColumn = function (columnIndex, amountToResize, keepGridWidth) {
                        this._resizeColumn(columnIndex, amountToResize, false);

                        var width = this._getColumnWidth();

                        if (width > 0) {
                            if (this.ScrollWidth == '0px') {
                                this.GridMainContainer.style.width = width + 'px';
                            } else {
                                var scrollWidth = parseInt(this.ScrollWidth);
                                if (width < scrollWidth) {
                                    this.GridMainContainer.style.width = width + 'px';
                                    this.HorizontalScroller.style.display = 'none';
                                } else {
                                    this.HorizontalScroller.firstChild.firstChild.style.width = width + 'px';
                                    this.HorizontalScroller.style.display = '';
                                }
                            }
                        }
                    }

                    oboutGrid.prototype._getColumnWidth = function () {
                        var totalWidth = 0;
                        for (var i = 0; i < this.ColumnsCollection.length; i++) {
                            if (this.ColumnsCollection[i].Visible) {
                                totalWidth += this.ColumnsCollection[i].Width;
                            }
                        }

                        return totalWidth;
                    }
                </script>
                <script type="text/javascript" language="javascript">
                    function toggleTypeSelection(checkbox) {
                        for (var ii = 0; ii < grddistributor.getTotalNumberOfRecords(); ii++) {
                            var idc = "ContentPlaceHolder1_grddistributor_ob_grddistributorBodyContainer_ctl04_" + ii + "_ctl00_" + ii + "_ChkIDDistid_" + ii;
                            if (document.getElementById(checkbox.id).checked == true) {
                                document.getElementById(idc).checked = true;
                            }
                            else {
                                document.getElementById(idc).checked = false;
                            }
                        }
                    }
                </script>


                <script type="text/javascript" language="javascript">
                    function toggleTypeheaderSelectionchecked(checkbox) {
                        var ihc = "ContentPlaceHolder1_grddistributor_ob_grddistributorHeaderContainer_ctl06_0_ctl00_0_chkboxSelectAll_0";
                        var flag = 0;
                        var count = grddistributor.getTotalNumberOfRecords();
                        for (var ii = 0; ii < grddistributor.getTotalNumberOfRecords(); ii++) {
                            var idc = "ContentPlaceHolder1_grddistributor_ob_grddistributorBodyContainer_ctl06_" + ii + "_ctl00_" + ii + "_ChkIDDistid_" + ii;
                            if (document.getElementById(idc).checked == true) {
                                flag = flag + 1;
                            }
                            else {
                                document.getElementById(ihc).checked = false;
                            }
                        }
                        if (flag == count) {
                            document.getElementById(ihc).checked = true;
                        }
                    }
                </script>
                <script type="text/javascript">
                    window.onload = function () {
                        oboutGrid.prototype.selectRecordByClick = function () {
                            return;
                        }
                        oboutGrid.prototype.showSelectionArea = function () {
                            return;
                        }
                    }

                </script>
                <script type="text/javascript">
                    function isNumberKeyWithDot(evt) {
                        var charCode = (evt.which) ? evt.which : event.keyCode;
                        if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 46))
                            return false;

                        return true;
                    }
                </script>
                <script type="text/javascript">
                    function checkAll() {
                        var newState = document.getElementById("chk_all").checked;

                        var inputArr = document.getElementsByTagName("INPUT");
                        for (i = 0; i < inputArr.length; i++) {
                            var e = inputArr[i];
                            if (e && e.type == "checkbox" && e.id != null && e.id.indexOf("chk_grid_") == 0) {
                                e.checked = newState;

                                var oElement = e.parentNode.parentNode.parentNode;
                                var oContainer = oElement.parentNode;
                                if (oElement && oContainer) {
                                    var iRecordIndex = -1;
                                    for (var j = 0; j < oContainer.childNodes.length; j++) {
                                        if (oContainer.childNodes[j] == oElement) {
                                            iRecordIndex = j;
                                            break;
                                        }
                                    }

                                    if (iRecordIndex != -1) {
                                        if (newState == true) {
                                            // select the record
                                            grid1.selectRecord(iRecordIndex);
                                        } else {
                                            // deselect the record
                                            grid1.deselectRecord(iRecordIndex);
                                        }
                                    }
                                }
                            }
                        }
                    }
                </script>
                <script type="text/javascript">
                    function CallDeleteServerMethod(oLink) {
                        var iRecordIndexdelete = oLink.id.toString().replace("btnGridDelete_", "");
                        document.getElementById("<%=hdnMappingDelete.ClientID %>").value = gvPackingsizeMappingAdd.Rows[iRecordIndexdelete].Cells[0].Value;
                        document.getElementById("<%=btngrddelete.ClientID %>").click();
                    }
                </script>
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
                    function setrowcolor(chb) {
                        if (chb.checked) {
                        }
                        else {

                        }

                        var GridVwHeaderCheckbox = document.getElementById("<%=gvBrandTypeMap.ClientID %>");
                        var lbltotalcount = document.getElementById("<%=lbltotalcount.ClientID %>");
                        var totalcount = 0;

                        for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                            if (GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked == true) {
                                totalcount = totalcount + 1;
                            }
                        }
                        if (totalcount > 0) {
                            lbltotalcount.style.display = "";
                            lbltotalcount.innerHTML = 'You have selected : ' + totalcount + ' Product(s).';
                        }
                        else {
                            lbltotalcount.style.display = "none";
                        }
                    }
                </script>
                <script type="text/javascript" language="javascript">
                    function CheckAllheader(Checkbox) {
                        var GridVwHeaderCheckbox = document.getElementById("<%=gvBrandTypeMap.ClientID %>");
                        var totalcount = 0;
                        if (Checkbox.checked) {
                            for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                                GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
                                totalcount = totalcount + 1;
                            }
                        }
                        else {
                            for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                                GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = false;

                            }
                        }
                        if (totalcount > 0) {
                            lbltotalcount.style.display = "";
                            lbltotalcount.innerHTML = 'You have selected : ' + totalcount + ' Product(s).';
                        }
                        else {
                            lbltotalcount.style.display = "none";
                        }
                    }
                </script>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
