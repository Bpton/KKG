<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmProductionOrder_WithoutMRP.aspx.cs" Inherits="VIEW_frmProductionOrder_WithoutMRP" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
    <style type="text/css">
        .white_content {
            top: 40%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" type="text/css" href="../css/jquery.datetimepicker.css" />
    <script type="text/javascript" src="../js/jquery.datetimepicker.js"></script>

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

    <script type="text/javascript">
        $(document).ready(function () {
            debugger;
            //BindTabFunctions();
            $('#txtStartTime').datetimepicker({

                format: 'd/m/Y H:i'
                //format : 'D/M/Y',                  -- Day format
                //format : 'D/M/Y',
                //hours12:true,
                // timepicker:false,
                // datepicker: true,               
                //yearStart:2000,
                //yearEnd: 2050,

            });

        });
    </script>
    <script type="text/javascript">
        function BindTabFunctions() {

            debugger;

            var tabValue = $("#" + '<%= hdfTabID.ClientID %>').val();
            if (tabValue == "") {
                $(".tabBox .content").hide();
                $(".tabBox .tabs li:first").addClass("selected");
                $("#tab1").show();

                //                $(".tabBoxinner .content").hide();
                //                $(".tabBoxinner .tabs li:first").addClass("selected");
                //                $("#tab5").show();
            }
            else {
                if (tabValue == 2) {
                    $(".tabBox .content").hide();
                    $("ul.tabs li:nth-child(2)").addClass("selected");
                    $("#tab2").show();
                    $("#" + '<%= hdfTabID.ClientID %>').val("");
                }
            }

            $(".tabBox .tabs li").click(function () {
                debugger;
                $(".tabBox .tabs li").removeClass("selected");
                $(this).addClass("selected");
                $(".tabBox .content").hide();
                var activeTab = $(this).find("a").attr("href");
                $(activeTab).show();
                return false;
            });

        };

    </script>
    <style type="text/css">
        .GridHeader {
            text-align: center !important;
        }
    </style>
    <style type="text/css">
        .ajax__tab_xp .ajax__tab_header .ajax__tab_tab {
            width: 50px !important;
            padding: 4px;
            margin: 0px;
            overflow: hidden;
            margin-bottom: -1px;
            height: 2.50em !important;
        }

        .tabBox .tabs {
            margin: 0;
            padding: 0;
            overflow: hidden;
            margin-bottom: -1px;
            height: 2.50em;
        }

            .tabBox .tabs li {
                float: left;
                list-style: none;
                margin: 0;
                padding: .25em .25em 0;
                overflow: hidden;
                position: relative;
                z-index: 1;
                border-bottom: 1px solid #FFF;
            }

                .tabBox .tabs li.selected {
                    z-index: 3;
                }

            .tabBox .tabs a {
                float: left;
                height: 3em;
                line-height: 2em;
                -webkit-border-radius: 4px 4px 0 0;
                -moz-border-radius: 4px 4px 0 0;
                border-radius: 4px 4px 0 0;
                background: #EEE;
                border: 1px solid #BDBDBD;
                border-bottom: 0;
                padding: 0px 15px;
                color: #000;
                padding-bottom: 20px;
                font-family: Lucida Grande,Lucida Sans,Arial,sans-serif;
                font-size: 1.1em;
                text-decoration: none;
                behavior: url(/pie/PIE.htc);
            }

            .tabBox .tabs .selected a {
                background: #FFF;
            }

            .tabBox .tabs a:hover {
                background: #FFF;
            }

            .tabBox .tabs a:active {
                background: #FFF;
            }


        .tabBox .content {
            display: none;
            clear: left;
            position: relative;
            height: 350px;
            z-index: 2;
            padding: 2em 1em;
            border: 1px solid #BDBDBD;
            background: #FFF; /*overflow-y:scroll;*/
        }

        .tabBoxinner .tabs {
            margin: 0;
            padding: 0;
            overflow: hidden;
            margin-bottom: -1px;
            height: 2.50em;
        }

            .tabBoxinner .tabs li {
                float: left;
                list-style: none;
                margin: 0;
                padding: .25em .25em 0;
                overflow: hidden;
                position: relative;
                z-index: 2;
                border-bottom: 1px solid #FFF;
            }

                .tabBoxinner .tabs li.selected {
                    z-index: 7;
                }

            .tabBoxinner .tabs a {
                float: left;
                height: 3em;
                line-height: 2em;
                -webkit-border-radius: 4px 4px 0 0;
                -moz-border-radius: 4px 4px 0 0;
                border-radius: 4px 4px 0 0;
                background: #EEE;
                border: 1px solid #BDBDBD;
                border-bottom: 0;
                padding: 0px 15px;
                color: #000;
                padding-bottom: 20px;
                font-family: Lucida Grande,Lucida Sans,Arial,sans-serif;
                font-size: 1.1em;
                text-decoration: none;
                behavior: url(/pie/PIE.htc);
            }

            .tabBoxinner .tabs .selected a {
                background: #FFF;
            }

            .tabBoxinner .tabs a:hover {
                background: #FFF;
            }

            .tabBoxinner .tabs a:active {
                background: #FFF;
            }


        .tabBoxinner .content {
            display: none;
            clear: left;
            position: relative;
            height: 300px;
            z-index: 2;
            padding: 2em 1em;
            border: 1px solid #BDBDBD;
            background: #FFF;
            overflow-y: scroll;
        }

        .selected {
            background-color: #A1DCF2;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function MakeStaticHeaderBOMSource(gridId, height, width, headerHeight, isFooter) {
            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRowBOMSource');
                var DivMC = document.getElementById('DivMainContentBOMSource');
                var DivFR = document.getElementById('DivFooterRowBOMSource');

                //*** Set divheaderRow Properties ****
                DivHR.style.height = headerHeight + 'px';
                //DivHR.style.width = (parseInt(width) - 16) + 'px';
                DivHR.style.width = '98%';
                DivHR.style.position = 'relative';
                DivHR.style.top = '0px';
                DivHR.style.zIndex = '10';
                DivHR.style.verticalAlign = 'top';

                //*** Set divMainContent Properties ****
                DivMC.style.width = width + 'px';
                DivMC.style.height = height + 'px';
                DivMC.style.position = 'relative';
                DivMC.style.top = -headerHeight + 'px';
                DivMC.style.zIndex = '1';

                //*** Set divFooterRow Properties ****
                DivFR.style.width = (parseInt(width) - 16) + 'px';
                DivFR.style.position = 'relative';
                DivFR.style.top = -headerHeight + 'px';
                DivFR.style.verticalAlign = 'top';
                DivFR.style.paddingtop = '2px';

                if (isFooter) {
                    var tblfr = tbl.cloneNode(true);
                    tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                    var tblBody = document.createElement('tbody');
                    tblfr.style.width = '100%';
                    tblfr.cellSpacing = "0";
                    tblfr.border = "0px";
                    tblfr.rules = "none";
                    //*****In the case of Footer Row *******
                    tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
                    tblfr.appendChild(tblBody);
                    DivFR.appendChild(tblfr);
                }
                //****Copy Header in divHeaderRow****
                DivHR.appendChild(tbl.cloneNode(true));
            }
        }
        function OnScrollDivBOMSource(Scrollablediv) {
            document.getElementById('DivHeaderRowBOMSource').scrollLeft = Scrollablediv.scrollLeft;
            document.getElementById('DivFooterRowBOMSource').scrollLeft = Scrollablediv.scrollLeft;
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
    <%--<script type="text/javascript">
        function ValidateListBox(sender, args) {
            var options = document.getElementById("<%=ddlFG.ClientID%>").options;
            for (var i = 0; i < options.length; i++) {
                if (options[i].selected == true) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }
    </script>--%>
    <script type="text/javascript">        
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((charCode < 48 || charCode > 57) && (charCode != 8))
                return false;

            return true;
        }
    </script>
    <script type="text/javascript">
        function disableautocompletion(id) {
            var TextBoxControl = document.getElementById(id);
            TextBoxControl.setAttribute("autocomplete", "off");
        }
    </script>
    <script type="text/javascript">
        function isNumberKeyWithoutDot(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((charCode < 48 || charCode > 57) && (charCode != 8))
                return false;

            return true;
        }
    </script>

    <script type="text/javascript" language="javascript">
        function calculate(a) {
            var Qtyid = 0;
            var Qty = 0;
            var BufferQtyID = 0;
            var BufferQty = 0;
            var NetQtyID = 0;
            var NetQty = 0;

            var rowData = a.parentNode.parentNode;
            var rowIndex = rowData.rowIndex - 1;
            var grd = document.getElementById('<%= gvBOM.ClientID %>');
            var grid = document.getElementById('ContentPlaceHolder1_gvBOM');

            Qtyid = "ContentPlaceHolder1_gvBOM_lblQty_" + rowIndex;
            Qty = parseFloat(document.getElementById(Qtyid).value);

            BufferQtyID = "ContentPlaceHolder1_gvBOM_txtBufferQty_" + rowIndex;
            BufferQty = parseFloat(document.getElementById(BufferQtyID).value);

            NetQtyID = "ContentPlaceHolder1_gvBOM_txtNetQty_" + rowIndex;
            requisitionQty = "ContentPlaceHolder1_gvBOM_lblRequisitionQty_" + rowIndex;

            if (Qty <= BufferQty) {
                alert("Buffer Qty Can Not be greater than Production Qty.");
                document.getElementById(BufferQtyID).value = 0;
            }
            else {
                NetQty = (BufferQty + Qty);
                document.getElementById(NetQtyID).value = NetQty.toFixed(3);
                document.getElementById(requisitionQty).value = NetQty.toFixed(3);
            }
        }
    </script>

    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="../images/103.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <script type="text/javascript">
                Sys.Application.add_load(BindTabFunctions);
            </script>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>Production Order without Forecasting
                                </h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAddnewRecord" runat="server" Text="Add New Record" CssClass="btn_link"
                                        OnClick="btnAddnewRecord_Click" CausesValidation="false" />
                                </div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                                <blockquote class="quote_orange" id="divcancelorder" runat="server" style="display:none">
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr style="display:none">
                                                            <td class="innerfield_title">
                                                                <asp:CheckBox ID="chkCancelled" runat="server" Text=" " /><asp:Label ID="Label213"
                                                                    Text="&nbsp; ORDER CANCEL" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </blockquote>
                                                <fieldset>
                                                    <legend>Production Order Detail</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                                        <tr runat="server" id="trAutoPoNo">
                                                            <td class="field_title">
                                                                <asp:Label ID="lblPoNo" Text="Production Order" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input" colspan="5" >
                                                                <asp:TextBox ID="txtPoNo" runat="server" Width="120" placeholder="Auto Generate No."
                                                                    Enabled="false" Font-Bold="true"> </asp:TextBox>
                                                                <asp:Label ID="lblshowbatchno" Text="Batch No" runat="server" style="display:none" CssClass="field_title"></asp:Label>
                                                                <asp:TextBox ID="txtshowbatchno" runat="server" Enabled="false" style="display:none" CssClass="field_input"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td id="td5" runat="server" class="field_titleTr" width="100">
                                                                <asp:Label ID="lblEntryDt" runat="server" Text="Entry Date"></asp:Label>
                                                            </td>
                                                            <td width="100" class="field_input">
                                                                <asp:TextBox ID="txtentrydt" runat="server" Width="100" placeholder="dd/mm/yyyy"
                                                                    MaxLength="10" Enabled="false" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                <asp:ImageButton ID="ImgEntrydt" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                    runat="server" Height="24" />
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImgEntrydt" runat="server"
                                                                    TargetControlID="txtentrydt" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </td>

                                                             <td id="td6" runat="server" class="field_titleTr" width="70">
                                                                <asp:Label ID="Label9" runat="server" Text="Production Type"></asp:Label>
                                                            </td>
                                                            <td width="160" id="Td7" runat="server">
                                                                <asp:DropDownList ID="ddlProductionType" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                                                    class="chosen-select" data-placeholder="Choose a Type" 
                                                                    Style="width: 180px;" OnSelectedIndexChanged="ddlProductionType_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                                                                    <asp:ListItem Value="WP">WITH PLANNING</asp:ListItem>
                                                                    <asp:ListItem Value="WOP">WITH OUT PLANNING</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>


                                                            <td id="td1" runat="server" class="field_titleTr" width="70">
                                                                <asp:Label ID="Label6" runat="server" Text="Type"></asp:Label>
                                                            </td>
                                                            <td width="160" id="Td2" runat="server">
                                                                <asp:DropDownList ID="ddlType" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                                                    class="chosen-select" data-placeholder="Choose a Product Type" OnSelectedIndexChanged="ddlMRP_SelectedIndexChanged"
                                                                    Style="width: 180px;" OnClientClick="getvalue(a)">
                                                                    <asp:ListItem Value="0" Selected="True">Please Select</asp:ListItem>
                                                                    <asp:ListItem Value="FG">FINISHED GOODS</asp:ListItem>
                                                                    <asp:ListItem Value="SFG">SEMI FINISHED GOODS</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>

                                                         <tr>

                                                            <div id="divPlanningNummer" runat="server" style="display:none">
                                                                <td class="innerfield_title" width="150">&nbsp;&nbsp;
                                                                <asp:Label ID="Label10" runat="server" Text="Planning Number"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                               
                                                                
                                                            <td class="field_titleTr">
                                                                <asp:DropDownList ID="ddlPlanningNumber" runat="server" AppendDataBoundItems="true" class="chosen-select" AutoPostBack="true"
                                                                    Style="width: 250px;" data-placeholder="Choose a Number" OnSelectedIndexChanged="ddlPlanningNumber_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                                </div>
                                                               </tr>




                                                        <tr>
                                                            <td class="field_titleTr">
                                                                <asp:Label ID="Label2" runat="server" Text="Finished Goods"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="150" style="padding-right: 20px;">

                                                                <asp:DropDownList ID="ddlFG" runat="server" AppendDataBoundItems="true" class="chosen-select"
                                                                    TabIndex="4" AutoPostBack="true" OnSelectedIndexChanged="ddlFG_SelectedIndexChanged1"
                                                                    Style="width: 250px;" data-placeholder="Select Product" DataTextField="NAME" DataValueField="PRODUCTID">
                                                                </asp:DropDownList>
                                                                <asp:CustomValidator ID="CustomValidator10" runat="server" ValidateEmptyText="true"
                                                                    ControlToValidate="ddlFG" ValidationGroup="ADD" ErrorMessage="Required!" Display="Dynamic"
                                                                     ForeColor="Red"></asp:CustomValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlFG" runat="server" ControlToValidate="ddlFG"
                                                                    ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ValidationGroup="Save"
                                                                    ForeColor="Red" InitialValue="0">
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                            <td  width="50" style="padding-right: 10px;">
                                                                 <asp:ImageButton ID="refer" runat="server" Text="Refersh" ImageUrl="~/images/icons/reload.png" AlternateText="Click for reload material" CssClass="refresh" OnClick="refer_Click" />
                                                            </td>
                                                           
                                                             <div id="divProductionQty" runat="server" style="display:none">
                                                             <td class="innerfield_title" width="150">&nbsp;&nbsp;
                                                                <asp:Label ID="Label12" runat="server" Text="Remainning Planning Qty"></asp:Label>
                                                                <span class="req">*</span>
                                                                <td class="field_titleTr">
                                                                    <asp:TextBox ID="txtProductionQty" Width="100" runat="server" AutoPostBack="true" Enabled="false">
                                                                </asp:TextBox>
                                                                </td>
                                                            </div>
                                                           


                                                            <td class="innerfield_title" width="150">&nbsp;&nbsp;
                                                                <asp:Label ID="lblframework" runat="server" Text="Choose Framework"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlframework" runat="server" AppendDataBoundItems="true" class="chosen-select"
                                                                    Style="width: 250px;" data-placeholder="Choose a Plant">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="Reqrdddlframework" runat="server" ControlToValidate="ddlframework"
                                                                    ValidationGroup="Save" SetFocusOnError="true" ErrorMessage="Required!" ForeColor="Red">
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                            <td class="innerfield_title" width="90" style="display:none" >
                                                                <asp:Label ID="Label4" runat="server" Text="Work Station"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td style="display:none">
                                                                <asp:DropDownList ID="ddlPlant" runat="server" AppendDataBoundItems="true" class="chosen-select"
                                                                    Style="width: 200px;" data-placeholder="Choose a Work Station">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="ReqrdddlPlant" runat="server" ControlToValidate="ddlPlant"
                                                                    ValidationGroup="Save" SetFocusOnError="true" ErrorMessage="Required!" ForeColor="Red">
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                                &nbsp;
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_titleTr">
                                                                <asp:Label ID="Label5" runat="server" Text="Factory"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td width="150" style="padding-right: 10px;">
                                                                <asp:DropDownList ID="ddlfactory" runat="server" AppendDataBoundItems="true" class="chosen-select"
                                                                    Style="width: 250px;" data-placeholder="Choose a Plant">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="innerfield_title" width="90">&nbsp;&nbsp;
                                                                <asp:Label ID="Label8" runat="server" Text="Process"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlprocess" runat="server" AppendDataBoundItems="true" class="chosen-select"
                                                                    Style="width: 250px;" data-placeholder="Choose a Process" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="innerfield_title" width="90" runat="server" id="tdbatch">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:Label ID="lblbatch" runat="server" Text="Batch No"></asp:Label>
                                                                <span class="req">*</span>
                                                            </td>
                                                            <td runat="server" id="tdddlbatch">
                                                                <asp:DropDownList ID="ddlbatch" runat="server" AppendDataBoundItems="true" class="chosen-select"
                                                                    Visible="false" Style="width: 210px;" data-placeholder="Choose a batch" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlbatch_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <tr>
                                                                <td class="field_titleTr">
                                                                    <asp:Label ID="Label11" runat="server" Text="Production Start Date"></asp:Label>
                                                                </td>
                                                                <td width="165" class="field_input">
                                                                    <asp:TextBox ID="txtFromDate" runat="server" Width="100" placeholder="dd/mm/yyyy"
                                                                        MaxLength="10" ValidationGroup="datecheckpodetail" Enabled="false" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                                    <asp:ImageButton ID="imgPopupFromDate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                                        runat="server" Height="24" />
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="imgPopupFromDate"
                                                                        runat="server" TargetControlID="txtFromDate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                                    </ajaxToolkit:CalendarExtender>
                                                                </td>
                                                                <td width="140" class="field_title">
                                                                    <asp:Label ID="Label3" runat="server" Text="Production Start Time"></asp:Label>
                                                                </td>
                                                                <td width="100" class="field_input">
                                                                    <asp:TextBox ID="txtStartTime" runat="server" onkeypress="return isNumberKey(event);" Width="100"
                                                                        ValidationGroup="A" AutoCompleteType="Disabled" onfocus="disableautocompletion(this.id);"></asp:TextBox>
                                                                </td>
                                                            </tr>

                                                            <tr id="trUseQtyDtls" runat="server" visible="false">
                                                                <td class="field_titleTr">
                                                                    <asp:Label ID="lblbulkqty" runat="server" Text="Bulk Qty"></asp:Label>
                                                                </td>
                                                                <td width="150" style="padding-right: 10px;">
                                                                    <asp:TextBox ID="txtbulkqty" runat="server" Width="100" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td class="innerfield_title" width="90">&nbsp;&nbsp;
                                                                    <asp:Label ID="lbluseqty" runat="server" Text="Use Qty"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtuseqty" runat="server" Width="100" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td class="innerfield_title" width="90">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    <asp:Label ID="lblbalanceqty" runat="server" Text="Balance Qty"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtbalanceqty" runat="server" Width="100" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>

                                                            <tr id="trBatchDtls" runat="server">
                                                                <td class="field_titleTr">
                                                                    <asp:Label ID="lblpreviousBatch" runat="server" Text="Previous Batch"></asp:Label>
                                                                </td>
                                                                <td width="150" style="padding-right: 10px;">
                                                                    <asp:TextBox ID="txtpreviousBatch" runat="server" Width="100" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td class="innerfield_title" width="90">&nbsp;&nbsp;
                                                                    <asp:Label ID="lblBatchQty" runat="server" Text="PREVIOUS Batch Qty/Kg"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtBatchQty" runat="server" Width="100" Enabled="false"></asp:TextBox>
                                                                </td>
                                                                <td class="innerfield_title" width="90">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    <asp:Label ID="lblNewBatch" runat="server" Text="New Batch"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtbatch" runat="server" Width="100" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr id="trReject" runat="server">
                                                                <td class="field_titleTr">
                                                                    <asp:Label ID="lblRejectRemarks" runat="server" Text="Reject Remarks"></asp:Label>
                                                                </td>
                                                                <td width="150">
                                                                    <asp:TextBox ID="txtRejectRemarks" runat="server" Width="250" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>


                                         
                                        
                                        <tr>
                                            <td class="field_inputTr">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <td class="field_inputTr">
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td>
                                                        <div class="btn_24_blue" style="display: none">
                                                            <span class="icon find_co"></span>
                                                            <asp:Button ID="btnShow" runat="server" Text="Search" CssClass="btn_link" OnClick="btnShow_Click" />
                                                        </div>
                                                    </td>
                                                    <td width="136" class="field_title" runat="server" visible="false">
                                                        <asp:Label ID="Label1" runat="server" Text="Production End Date"></asp:Label>
                                                        <span class="req">*</span>
                                                    </td>
                                                    <td class="field_input" runat="server" visible="false">
                                                        <asp:TextBox ID="txtToDate" runat="server" Width="120" placeholder="dd/mm/yyyy" MaxLength="10"
                                                            ValidationGroup="datecheckpodetail" Enabled="false" onkeypress="return isNumberKeyWithslash(event);"></asp:TextBox>
                                                        <asp:ImageButton ID="imgPopupToDate" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                            runat="server" Height="24" />
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopupToDate"
                                                            runat="server" TargetControlID="txtToDate" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                        </ajaxToolkit:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="ReqToDate" runat="server" ControlToValidate="txtToDate"
                                                            ValidationGroup="datecheckpodetail" SetFocusOnError="true" ErrorMessage="Order Date is required!"
                                                            Display="None"></asp:RequiredFieldValidator>
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                            TargetControlID="ReqToDate" PopupPosition="Right" HighlightCssClass="errormassage"
                                                            WarningIconImageUrl="../images/050.png">
                                                        </ajaxToolkit:ValidatorCalloutExtender>
                                                    </td>
                                                    <td class="field_title" width="135" runat="server" visible="false">
                                                        <asp:Label ID="Label7" runat="server" Text="Production End Time(HH:MM)"></asp:Label>
                                                        <span class="req">*</span>
                                                    </td>
                                                    <td class="field_input" width="40" runat="server" visible="false">
                                                        <asp:TextBox ID="txtEndTime" runat="server" MaxLength="5" ToolTip="HH:MM" placeholder="HH:MM">
                                                        </asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="rev" runat="server" ErrorMessage="InvalidTime"
                                                            ControlToValidate="txtEndTime" ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$"
                                                            Display="Dynamic">
                                                        </asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        </tr>
                                        <tr>
                                        </tr>
                                    </table>
                                    </td> </tr> </table> </fieldset> </td> </tr>
                                    <tr>
                                        <td class="field_input" style="padding-left: 10px;">
                                            <div class="gridcontent">
                                                <table border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:GridView ID="gvPlannedOrder" runat="server" CssClass="zebra" HeaderStyle-CssClass="header"
                                                                AutoGenerateColumns="false" Width="500">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Planned Order No." HeaderStyle-CssClass="GridHeader"
                                                                        ItemStyle-CssClass="GridHeader">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPlannedNo" runat="server" Text='<%#Eval("PLANNED_NO") %>'></asp:Label>
                                                                            <asp:Label ID="lblPlannedID" runat="server" Text='<%#Eval("PLANNEDID")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lblQTY" runat="server" Text='<%#Eval("QTY")%>' Visible="false"></asp:Label>
                                                                            <asp:Label ID="lblQtyInPcs" runat="server" Text='<%#Eval("QtyInPcs")%>' Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Creation Date" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCreateDate" runat="server" Text='<%#Eval("CREATEDDATE") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader">
                                                                        <HeaderTemplate>
                                                                            <asp:CheckBox ID="chkHeader" runat="server" onclick="checkAll(this);" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkRow" runat="server" onclick="Check_Click(this);" Text=" " />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding: 8px 0px;">
                                                            <div class="btn_24_blue" id="Generate" runat="server">
                                                                <span class="icon page_white_gear_co"></span>
                                                                <asp:Button ID="btnGenerate" runat="server" Text="Generate" CssClass="btn_link" OnClick="btnGenerate_Click" />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="field_input" style="padding-left: 10px;">
                                            <div class="gridcontent" style="display: none" id="td_gridview" runat="server">
                                                <table border="0" cellspacing="0" cellpadding="0" class="form_container_td">
                                                    <tr>
                                                        <td valign="top">
                                                            <asp:GridView ID="gvItem" runat="server" CssClass="zebra" HeaderStyle-CssClass="header"
                                                                AutoGenerateColumns="false" EmptyDataText=" There are no records available."
                                                                Width="720px">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader" HeaderStyle-Width="150px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblItem" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="NameID" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader"
                                                                        Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblItemID" runat="server" Text='<%#Eval("ID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="SKU Size" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader" HeaderStyle-Width="40px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblUnitValue" runat="server" Text='<%#Eval("UNITVALUE") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Specific Gravity" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSpecificGravity" runat="server" Text='<%#Eval("SPECIFICGRAVITY") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Unit" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblUnit" runat="server" Text='<%#Eval("UOMNAME") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="UnitID" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader"
                                                                        Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblUnitID" runat="server" Text='<%#Eval("UOMID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Mrp Qty" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMRPQTY" runat="server" Text='<%#Eval("QTY") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Pack Size" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader" HeaderStyle-Width="40px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblConversionQty" runat="server" Text='<%#Eval("CONVERSIONQTY") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="QTY (IN KG/BOXES)" HeaderStyle-CssClass="GridHeader" HeaderStyle-Width="40px"
                                                                        ItemStyle-CssClass="GridHeader">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtQtyinKg" runat="server" Text='<%#Eval("QTYINKG") %>' OnTextChanged="txtQtyinKg_OnTextChanged"
                                                                                AutoPostBack="true" Width="50px" Enabled="false" ></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="QTY (IN PCS)" HeaderStyle-CssClass="GridHeader"
                                                                        ItemStyle-CssClass="GridHeader">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TXTQTY" runat="server" Text='<%#Eval("Consumption") %>' OnTextChanged="TXTQTY_OnTextChanged"
                                                                                AutoPostBack="true" Width="50px" onkeypress="return isNumberKey()" ></asp:TextBox>
                                                                            <asp:CompareValidator ID="valQtyNumeric1" runat="server" ControlToValidate="TXTQTY"  Display="Dynamic" SetFocusOnError="true"
                                                                                     Text="" ErrorMessage="Error:Enter Only Number!"  ForeColor="Red" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="WorkStation" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblworkstation" runat="server" Text='<%#Eval("WorkStationName") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                  
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>       
                                                    <tr>
                                                        <td style="padding-top: 10px;">
                                                            <div style="overflow: hidden; width: 100%;" id="DivHeaderRowBOMSource">
                                                            </div>
                                                            <div style="overflow: scroll; border: 1px solid #c3c9ce; margin-bottom: 8px;" onscroll="OnScrollDivBOMSource(this)"
                                                                id="DivMainContentBOMSource">
                                                                <asp:GridView ID="gvBOM" runat="server" AutoGenerateColumns="False" EmptyDataRowStyle-Font-Bold="true"
                                                                    EmptyDataRowStyle-VerticalAlign="Middle" CssClass="zebra" Width="720px" Height="75px"
                                                                    EmptyDataRowStyle-Font-Size="Large" OnRowDataBound="gvBOM_RowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Product Name" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader" HeaderStyle-Width="380px">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("NAME") %>' Width="180px"></asp:Label>
                                                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ITEMID") %>' Visible="false"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Unit" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblUOMNAME" runat="server" Text='<%# Eval("UOMNAME") %>' Height="30px"
                                                                                    Width="30px"></asp:Label>
                                                                                <asp:Label ID="lblUOMID" runat="server" Text='<%# Eval("UNIT") %>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lblCATID" runat="server" Text='<%# Eval("CATID") %>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lblCATNAME" runat="server" Text='<%# Eval("CATNAME") %>' Visible="false"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Qty" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader">
                                                                            <ItemTemplate>
                                                                                <%-- <asp:Label ID="lblQty" runat="server" Text='<%# Eval("Qty") %>' Height="30px" Width="100px"></asp:Label>--%>
                                                                                <asp:TextBox ID="lblQty" runat="server" Text='<%#Eval("Qty") %>' Width="50px" Enabled="false"></asp:TextBox>

                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Buffer Qty" HeaderStyle-CssClass="GridHeader" HeaderStyle-Width="40px" ItemStyle-CssClass="GridHeader">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtBufferQty" runat="server" Text='<%#Eval("BUFFERQTY") %>'
                                                                                    Width="50px" onkeyup="calculate(this);" onkeypress="return isNumberKeyWithoutDot()"></asp:TextBox>
                                                                                <asp:CompareValidator ID="valQtyNumeric2" runat="server" ControlToValidate="txtBufferQty"  Display="Dynamic" SetFocusOnError="true"
                                                                                     Text="" ErrorMessage="Error:Enter Only Number!"  ForeColor="Red" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Net Qty" HeaderStyle-CssClass="GridHeader" HeaderStyle-Width="40px" ItemStyle-CssClass="GridHeader">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="txtNetQty" runat="server" Text='<%#Eval("NETQTY") %>' Width="50px" Enabled="false"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>


                                                                        <asp:TemplateField HeaderText="Source Location" Visible="false" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSourceLocationId" runat="server" Text='<%# Eval("OWNLOCATIONID") %>' Height="30px"
                                                                                    Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                         <asp:TemplateField HeaderText="Source Location" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSourceLocation" runat="server" Text='<%# Eval("OWNLOCATION") %>' Height="30px"
                                                                                    Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Source Stock Qty" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblstockQty" runat="server" Text='<%# Eval("StockQty") %>' Height="30px"
                                                                                    Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Mapped Location" Visible="false" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblMappedLocationId" runat="server" Text='<%# Eval("TOLOCATIONID") %>' Height="30px"
                                                                                    Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Mapped Location" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblMappedLocation" runat="server" Text='<%# Eval("TOLOCATION") %>' Height="30px"
                                                                                    Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>


                                                                         <asp:TemplateField HeaderText="Process Running Qty" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader">
                                                                            <ItemTemplate>
                                                                                <asp:label runat="server"  ID="lblAlreadyRequQty"  Text='<%# Eval("ALREADYEQUQTY") %>'
                                                                                    Width="50px"></asp:label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>


                                                                        <asp:TemplateField HeaderText="Requisition Qty" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox runat="server"  ID="lblRequisitionQty"  Text='<%# Eval("REQUISITIONQTY") %>'
                                                                                    Width="50px" onkeypress="return isNumberKeyWithoutDot()"></asp:TextBox>
                                                                          
                                                                             <asp:CompareValidator ID="valQtyNumeric3" runat="server" ControlToValidate="lblRequisitionQty"  Display="Dynamic" SetFocusOnError="true"
                                                                                     Text="" ErrorMessage="Error:Enter Only Number!"  ForeColor="Red" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                                                                  </ItemTemplate>
                                                                        </asp:TemplateField>


                                                                         <asp:TemplateField HeaderText="Destination Department" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader" Visible="false">
                                                                             <ItemTemplate>
                                                                                <asp:label runat="server"  ID="lblSendDepartMent"  Text='<%# Eval("DEPTID") %>'
                                                                                    Width="50px"></asp:label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        
                                                                        <asp:TemplateField HeaderText="Destination Department" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader">
                                                                             <ItemTemplate>
                                                                                <asp:label runat="server"  ID="lblSendDepartMentName"  Text='<%# Eval("DEPTNAME") %>'
                                                                                    Width="50px"></asp:label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Applicable for Requisition" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblApplicable" runat="server" Height="30px"  Text='<%# Eval("SENDREQUEST") %>'
                                                                                    Width="100px"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="STORELOCATION WISE STOCK" HeaderStyle-CssClass="GridHeader" ItemStyle-CssClass="GridHeader">
                                                                            <ItemTemplate>
                                                                                <asp:Button ID="btnViewStock" runat="server" OnClick="btnViewStock_Click" class="action-icons c-edit "  />
                                                                                  
                                                                            </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                            <div id="DivFooterRowBOMSource" style="overflow: hidden">
                                                            </div>
                                                        </td>
                                                        <tr>
                                                        <div id="divStoreLocationWiseStock" runat="server" style="display:none">

                                                          <td style="padding-top: 10px;">
                                                                <asp:GridView ID="grdStoreWiseStock" runat="server" AutoGenerateColumns="true" EmptyDataRowStyle-Font-Bold="true"
                                                                    EmptyDataRowStyle-VerticalAlign="Middle" CssClass="zebra" Width="450px" Height="75px"
                                                                    EmptyDataRowStyle-Font-Size="Large">
                                                        </asp:GridView>
                                                           </td>
                                                         </div>
                                                            </tr>

                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="field_input" style="padding-left: 10px;">
                                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td style="padding: 0px 0px;">
                                                        <div class="btn_24_blue" id="Div_Submit" runat="server">
                                                            <span class="icon disk_co"></span>
                                                            <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn_link" OnClick="btnSubmit_Click"
                                                                ValidationGroup="Save" />
                                                        </div>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                        <div class="btn_24_blue" id="Div_Cancel" runat="server">
                                                            <span class="icon cross_octagon_co"></span>
                                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn_link" OnClick="btnCancel_Click"
                                                                CausesValidation="false" />
                                                        </div>
                                                        <asp:HiddenField ID="Hdn_Fld" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlDisplay" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_title">
                                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                    <tr>
                                                        <td width="90">
                                                            <asp:Label ID="lblfrmdt" runat="server" Text="From Date"></asp:Label>
                                                        </td>
                                                        <td width="120">
                                                            <asp:TextBox ID="txtFromDateSearch" runat="server" MaxLength="10" Width="80" Enabled="false"
                                                                placeholder="dd/MM/yyyy" onkeypress="return isNumberKeyWithslash(event);"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="ImgFromDateSearch" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="ClendFromDateSearch" PopupButtonID="ImgFromDateSearch" runat="server"
                                                                TargetControlID="txtFromDateSearch" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>

                                                        </td>
                                                        <td width="70">
                                                            <asp:Label ID="lbltodt" runat="server" Text="To Date"></asp:Label>
                                                        </td>

                                                        <td width="120">
                                                            <asp:TextBox ID="txtToDateSearch" runat="server" MaxLength="10" Width="80" Enabled="false"
                                                                placeholder="dd/MM/yyyy" onkeypress="return isNumberKeyWithslash(event);"
                                                                Font-Bold="true"></asp:TextBox>
                                                            <asp:ImageButton ID="ImgToDateSearch" ImageUrl="~/images/calendar.png" ImageAlign="AbsMiddle"
                                                                runat="server" Height="24" />
                                                            <ajaxToolkit:CalendarExtender ID="CalenToDateSearch" PopupButtonID="ImgToDateSearch" runat="server"
                                                                TargetControlID="txtToDateSearch" CssClass="cal_Theme1" Format="dd/MM/yyyy">
                                                            </ajaxToolkit:CalendarExtender>

                                                        </td>

                                                        <td width="90" class="field_title">
                                                            <asp:Label ID="lblproduct" Text="Product" runat="server"></asp:Label>
                                                            <span class="req">*</span>
                                                        </td>
                                                        <td width="180" class="field_input">
                                                            <asp:DropDownList ID="ddlproduct" Width="250" runat="server" class="chosen-select"
                                                                data-placeholder="Select Product" AppendDataBoundItems="True" ValidationGroup="Show">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rvproduct" runat="server" ControlToValidate="ddlproduct"
                                                                ValidateEmptyText="false" SetFocusOnError="true" ErrorMessage="Required!" ForeColor="Red"
                                                                InitialValue="0" ValidationGroup="Show"></asp:RequiredFieldValidator>
                                                        </td>

                                                        <td width="110">
                                                            <div class="btn_24_blue">
                                                                <span class="icon find_co"></span>
                                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn_link"
                                                                    OnClick="btnSearch_Click" />
                                                            </div>
                                                        </td>
                                                        <td width="60">
                                                            <asp:Label ID="Label120" runat="server" Text="Filter" Visible="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlWaybillFilter" Width="150" runat="server" class="chosen-select"
                                                                Visible="false" data-placeholder="Choose Waybill Filter" AutoPostBack="true">
                                                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Without Waybill" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="With Waybill" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="With C-Form" Value="3"></asp:ListItem>
                                                                <asp:ListItem Text="Without C-Form" Value="4"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
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
                                        <cc1:Grid ID="gvCompany" runat="server" CallbackMode="true" Serialize="true" FolderStyle="../GridStyles/premiere_blue"
                                            AutoGenerateColumns="false" AllowPageSizeSelection="true"
                                            PageSize="200" AllowPaging="true" AllowAddingRecords="false" AllowFiltering="true"
                                            EnableRecordHover="true" OnRowDataBound="gvCompany_RowDataBound" >
                                            <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnClientDelete="OnDelete" />
                                            <FilteringSettings InitialState="Visible" />
                                            <Columns>
                                                <cc1:Column ID="Column1" DataField="ORDERID" ReadOnly="true" HeaderText="COMPANYID"
                                                    runat="server" Visible="false" />
                                                <cc1:Column ID="Column12" DataField="Sl" HeaderText="SL" runat="server" Width="3%">
                                                    <TemplateSettings TemplateId="slnoTemplateIN" />
                                                </cc1:Column>
                                                <cc1:Column ID="Column2" DataField="PRODUCTION_NO" HeaderText="ORDER NO" runat="server"
                                                    Width="100" Wrap="true">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column3" DataField="CREATION_DATE" HeaderText="DATE" runat="server"
                                                    Wrap="true" Width="70">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column5" DataField="BatchNO" HeaderText="BATCH NO" runat="server"
                                                    Wrap="true" Width="100">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column6" DataField="PRODUCTNAME" HeaderText="PRODUCT" runat="server"
                                                    Wrap="true" Width="170">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                <cc1:Column ID="Column7" DataField="FRAMEWORKNAME" HeaderText="FRAMEWORK NAME" runat="server"
                                                    Wrap="true" Width="200">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                <cc1:Column ID="Column9" DataField="QTYINKG" HeaderText="QTY(KG/BOXES)" runat="server"
                                                    Wrap="true" Width="80">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                <cc1:Column ID="Column8" DataField="BATCHSIZE" HeaderText="QTY(KG/PCS)" runat="server"
                                                    Wrap="true" Width="80">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                <cc1:Column ID="Column11" DataField="PRODUCEQTYINKG" HeaderText="PRODUCEQTY(KG/BOXES)" runat="server"
                                                    Wrap="true" Width="80">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                <cc1:Column ID="Column13" DataField="PRODUCEQTY" HeaderText="QTY(KG/PCS)" runat="server"
                                                    Wrap="true" Width="80">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                <cc1:Column ID="Column10" DataField="PROCESSSTATUS" HeaderText="PROCESSSTATUS" runat="server"
                                                    Wrap="true" Width="80" ItemStyle-ForeColor="Red">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>

                                                <cc1:Column ID="Column4" AllowEdit="true" AllowDelete="true" HeaderText="EDIT" runat="server"
                                                    Width="70">
                                                    <TemplateSettings TemplateId="editBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="DELETE" AllowEdit="false" AllowDelete="true" Width="70">
                                                    <TemplateSettings TemplateId="deleteBtnTemplate" />
                                                </cc1:Column>
                                            </Columns>
                                            <FilteringSettings MatchingType="AnyFilter" />
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="editBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-edit" id="btnGridEdit_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethod(this);"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                                <cc1:GridTemplate runat="server" ID="slnoTemplateIN">
                                                    <Template>
                                                        <asp:Label ID="lblslnoin" runat="server" Text='<%#Container.PageRecordIndex+1 %>'></asp:Label>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="deleteBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-delete" title="Delete" 
                                                            id="btnGriddelete_<%# Container.PageRecordIndex %>"
                                                             onclick="DeleteRecord(this);">
                                                            ></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <ScrollingSettings ScrollWidth="100%" ScrollHeight="290" />
                                        </cc1:Grid>
                                        <asp:Button ID="btngridedit" runat="server" Text="Gridedit" CausesValidation="false"
                                            Style="display: none" OnClick="btngridedit_Click" /> 
                                        <asp:Button ID="btnGriddelete" runat="server" Text="Griddelete" CausesValidation="false"
                                            Style="display: none" OnClick="btnGriddelete_Click" />
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <div class="footer" align="right">
                        <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="button" CausesValidation="false"
                            Visible="false" OnClick="btnClose_Click" />
                    </div>
                    <div style="display:none">
                         <td id="tdlblOdNo" runat="server" class="field_titleTr" style="display: none">
                                                                <asp:HiddenField ID="hdfTabID" runat="server" Visible="false" />
                                                                <asp:Label ID="lblMRP" runat="server" Text="MRP" Visible="false"></asp:Label>
                                                            </td>
                                                            <td id="Td3" width="230" runat="server" style="display: none">
                                                                <asp:DropDownList ID="ddlMRP" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                                                    Visible="false" OnSelectedIndexChanged="ddlMRP_SelectedIndexChanged" class="chosen-select"
                                                                    data-placeholder="Choose a MRP" Style="width: 220px;">
                                                                    <asp:ListItem Value="0">Select MRP</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td id="Td4" runat="server" style="display:none">
                                                                <asp:RadioButtonList ID="rdbatch" AutoPostBack="true" RepeatDirection="Horizontal"
                                                                    runat="server" OnSelectedIndexChanged="rdbatch_SelectedIndexChanged">
                                                                    <asp:ListItem Value="1">Existing Batch</asp:ListItem>
                                                                    <asp:ListItem Value="0">New Batch</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                    </div>
                    <cc1:MessageBox ID="MessageBox1" runat="server" />
                    <span class="clear"></span>
                </div>
                <span class="clear"></span>
            </div>
            <script type="text/javascript">
                function OnBeforeDelete(record) {
                    record.Error = '';
                    document.getElementById("<%=Hdn_Fld.ClientID %>").value = record.COMPANYID;
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
                function isNumberKey(evt) {
                    var charCode = (evt.which) ? evt.which : event.keyCode;
                    if (charCode < 48 || charCode > 57)
                        return false;
                    return true;
                }
            </script>
            <script type="text/javascript">
                function CallServerMethod(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                    document.getElementById("<%=Hdn_Fld.ClientID %>").value = gvCompany.Rows[iRecordIndex].Cells[0].Value;
                    document.getElementById("<%=btngridedit.ClientID %>").click();
                }

            </script>
            <script type="text/javascript">
                function DeleteRecord(oLink) {
                    var iRecordIndex = oLink.id.toString().replace("btnGriddelete_", "");
                    document.getElementById("<%=Hdn_Fld.ClientID %>").value = gvCompany.Rows[iRecordIndex].Cells[0].Value;
                    document.getElementById("<%=btnGriddelete.ClientID %>").click();
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

                    gvCompany.addFilterCriteria('COMPANYCODE', OboutGridFilterCriteria.Contains, searchValue);
                    gvCompany.addFilterCriteria('COMPANYNAME', OboutGridFilterCriteria.Contains, searchValue);

                    gvCompany.executeFilter();
                    searchTimeout = null;
                    return false;
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>