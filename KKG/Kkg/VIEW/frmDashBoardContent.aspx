<%--<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmDashBoardContent.aspx.cs" Inherits="VIEW_frmDashBoardContent" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-modal/0.9.1/jquery.modal.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-modal/0.9.1/jquery.modal.min.css" />
    <script type="text/javascript" src="../js/jsapi.js"></script>
    <script type="text/javascript" src="../js/jquery-ui.min.js"></script>
    <link href="../css/jquery-ui.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/vendors/mdi/css/materialdesignicons.min.css">
    <link rel="stylesheet" href="../../assets/vendors/css/vendor.bundle.base.css">
    <!-- Plugin css for this page -->
    <link rel="stylesheet" href="../../assets/vendors/font-awesome/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/vendors/bootstrap-datepicker/bootstrap-datepicker.min.css">
    <!-- End plugin css for this page -->
    <!-- Layout styles -->
    <link rel="stylesheet" href="../../assets/css/demo_3/style.css">
    <script src="../js/dashboard.js"></script>
    <style>
        .sidenav {
            height: 12%;
            width: 0;
            position: fixed;
            z-index: 10;
            top: 0;
            right: 0;
            background: linear-gradient(to bottom,#ffffff 0,#aaf0eb 60%);
            background-repeat: repeat;
            background-repeat: repeat-x;
            transition: 0.5s;
            margin-top: 7%;
            border-radius: 10px;
        }

            .sidenav a {
                text-decoration: none;
                font-size: 25px;
                color: #818181;
                display: block;
                transition: 0.5s;
            }

        .slideBtn {
            transition: .5s;
            position: fixed;
            right: 0;
            font-size: 30px;
            cursor: pointer;
            margin-right: 10px;
            font-size: large;
            background: linear-gradient(to bottom,#ffffff 0,#aaf0eb 60%);
            border-radius: 10px;
        }

        .closebtn {
            top: 0;
            right: 25px;
            font-size: 36px;
            margin-right: 50px;
            padding-top: 0px;
        }
        /*STIKEY NOTE .CSS*/
        .sticky {
            position: absolute;
            right: 0;
            z-index: 0;
            /*transform: rotate(5deg);*/
            width: 95%;
            min-height: 150px;
            margin: 5px 0px 4px;
            padding: 10px;
            font-family: "Comic Sans MS", "Comic Sans", "Chalkboard SE", "Comic Neue", cursive;
            font-size: 14px;
            color: black;
            background: rgba(255,255,255,0.4);
            box-shadow: 2px 2px 2px rgba(0,0,0,0.3);
        }

        .fa fa-filter {
            background-color: burlywood;
        }

        .widget_top {
            color: white;
        }
    </style>

    <style>
        .sidenav2 {
            height: 50%;
            width: 0;
            position: fixed;
            z-index: 0;
            top: 0;
            right: 0;
            background: linear-gradient(to bottom,#ffffff 0,#aaf0eb 60%);
            background-repeat: repeat;
            background-repeat: repeat-x;
            transition: 0.5s;
            margin-top: 5.1%;
            border-radius: 10px;
        }

            .sidenav2 a {
                text-decoration: none;
                font-size: 25px;
                color: #818181;
                display: block;
                transition: 0.5s;
            }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div></div>
      <div class="row">
            <div class="select-style">
                <asp:DropDownList ID="ddlBranch" runat="server" >
                </asp:DropDownList>
       
            </div>
        </div>
    
    <div class="row">
        <div class="col-md-6 stretch-card grid-margin">
            <div class="card bg-gradient-primary">
                <div class="card-body text-white">
                    <h4 class="mb-3">Pening Gate Entry</h4>
                    <div class="progress progress-sm">
                        <div class="progress-bar bg-success" role="progressbar" aria-valuenow="5" style="width: 50%" aria-valuemin="0" aria-valuemax="100"></div>
                    </div>
                    <div class="d-flex justify-content-between align-items-center">
                        <div class="mt-3">
                            <h3 class="mb-0"><label class="pendingGateEntryCount"></label></h3>
                            <span class="font-weight-medium">Cuurent</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-6 stretch-card grid-margin">
            <div class="card bg-gradient-danger">
                <div class="card-body text-white">
                    <h4 class="mb-3">Complete Gate Entry</h4>
                    <div class="progress progress-sm">
                        <div class="progress-bar bg-success" role="progressbar" aria-valuenow="5" style="width: 50%" aria-valuemin="0" aria-valuemax="100"></div>
                    </div>
                    <div class="d-flex justify-content-between align-items-center">
                        <div class="mt-3">
                            <h3 class="mb-0"><label class="GateEntryCount"></label></h3>
                            <span class="font-weight-medium">Current</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-6 stretch-card grid-margin">
            <div class="card bg-gradient-primary">
                <div class="card-body text-white">
                    <h4 class="mb-3">Total Stock Out</h4>
                    <div class="progress progress-sm">
                        <div class="progress-bar bg-success" role="progressbar" aria-valuenow="5" style="width: 50%" aria-valuemin="0" aria-valuemax="100"></div>
                    </div>
                    <div class="d-flex justify-content-between align-items-center">
                        <div class="mt-3">
                            <h3 class="mb-0">2345.00</h3>
                            <span class="font-weight-medium">Current</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-6 stretch-card grid-margin">
            <div class="card bg-gradient-danger">
                <div class="card-body text-white">
                    <h4 class="mb-3">Total Stock Out</h4>
                    <div class="progress progress-sm">
                        <div class="progress-bar bg-success" role="progressbar" aria-valuenow="5" style="width: 50%" aria-valuemin="0" aria-valuemax="100"></div>
                    </div>
                    <div class="d-flex justify-content-between align-items-center">
                        <div class="mt-3">
                            <h3 class="mb-0">2345.00</h3>
                            <span class="font-weight-medium">Current</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-6 stretch-card grid-margin">
            <div class="card bg-gradient-primary">
                <div class="card-body text-white">
                    <h4 class="mb-3">Total Stock Out</h4>
                    <div class="progress progress-sm">
                        <div class="progress-bar bg-success" role="progressbar" aria-valuenow="5" style="width: 50%" aria-valuemin="0" aria-valuemax="100"></div>
                    </div>
                    <div class="d-flex justify-content-between align-items-center">
                        <div class="mt-3">
                            <h3 class="mb-0">2345.00</h3>
                            <span class="font-weight-medium">Current</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>--%>

<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmDashBoardContent.aspx.cs" Inherits="VIEW_frmDashBoardContent" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
      <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-modal/0.9.1/jquery.modal.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-modal/0.9.1/jquery.modal.min.css" />
    <script type="text/javascript" src="../js/jsapi.js"></script>
    <script type="text/javascript" src="../js/jquery-ui.min.js"></script>
    <link href="../css/jquery-ui.min.css" rel="stylesheet" />
    <style>
        .sidenav {
            height: 12%;
            width: 0;
            position: fixed;
            z-index: 10;
            top: 0;
            right: 0;
            background: linear-gradient(to bottom,#ffffff 0,#aaf0eb 60%);
            background-repeat: repeat;
            background-repeat: repeat-x;
            transition: 0.5s;
            margin-top: 7%;
            border-radius: 10px;
        }

            .sidenav a {
                text-decoration: none;
                font-size: 25px;
                color: #818181;
                display: block;
                transition: 0.5s;
            }

        .slideBtn {
            transition: .5s;
            position: fixed;
            right: 0;
            font-size: 30px;
            cursor: pointer;
            margin-right: 10px;
            font-size: large;
            background: linear-gradient(to bottom,#ffffff 0,#aaf0eb 60%);
            border-radius: 10px;
        }

        .closebtn {
            top: 0;
            right: 25px;
            font-size: 36px;
            margin-right: 50px;
            padding-top: 0px;
        }
        /*STIKEY NOTE .CSS*/
        .sticky {
            position: absolute;
            right: 0;
            z-index: 0;
            /*transform: rotate(5deg);*/
            width: 95%;
            min-height: 150px;
            margin: 5px 0px 4px;
            padding: 10px;
            font-family: "Comic Sans MS", "Comic Sans", "Chalkboard SE", "Comic Neue", cursive;
            font-size: 14px;
            color: black;
            background: rgba(255,255,255,0.4);
            box-shadow: 2px 2px 2px rgba(0,0,0,0.3);
        }

        .fa fa-filter {
            background-color: burlywood;
        }

        .widget_top {
            color: white;
        }
    </style>

    <style>
        .sidenav2 {
            height: 50%;
            width: 0;
            position: fixed;
            z-index: 0;
            top: 0;
            right: 0;
            background: linear-gradient(to bottom,#ffffff 0,#aaf0eb 60%);
            background-repeat: repeat;
            background-repeat: repeat-x;
            transition: 0.5s;
            margin-top: 5.1%;
            border-radius: 10px;
        }

            .sidenav2 a {
                text-decoration: none;
                font-size: 25px;
                color: #818181;
                display: block;
                transition: 0.5s;
            }
    </style>

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


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


        .container {
  display: flex;
  justify-content: center;
}
.center {
  width: 800px;
}

    </style>

    <div class="page_title">
       
        <span class="title_icon"><span class="computer_imac"></span></span>
        <h3 style="margin-left:2px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Dashboard</h3>
        <div class="top_search">
            <div class="select-style">
                <asp:DropDownList ID="ddlBranch" runat="server" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                    AutoPostBack="true">
                </asp:DropDownList>
       
            </div>
 <p runat="server" id="invoice"><a href="#ex1" rel="modal:open" style="border:thick;background-color:aqua;">Click Here To Show Invoice Details</a></p>
        </div>
      <div id="sograph" runat="server">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btn" />
                                </Triggers>
                                <ContentTemplate>

                                    <div id="sidenav3" class="sidenav">
                                        <div id="slidebtn3" class="slideBtn fa fa-filter" onclick="openNav3()">Chart Filter</div>
                                        <a href="#" onclick="closeNav3()" class="closebtn">
                                            <img src="../images/Cancel.png" alt="" height="15px" /><span id="dvProgressBar3" align="center" style="visibility: hidden; font-size: small; color: black; padding-left: 40px;">Requesting under process....Please wait.....</span></a>
                                        <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                            <tr>
                                                <td class="field_title"><b>From Date</b></td>
                                                <td class="field_input">
                                                    <asp:TextBox ID="txtstart" runat="server" MaxLength="10" Width="70" EnableViewState="true"
                                                        placeholder="dd/MM/yyyy"
                                                        Font-Bold="true">
                                                    </asp:TextBox>
                                                    <asp:ImageButton ID="imgbtn" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle" runat="server" Height="24" />
                                                    <ajaxToolkit:CalendarExtender ID="Calendarstart" PopupButtonID="imgbtn" runat="server"
                                                        TargetControlID="txtstart" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td class="field_title"><b>To Date</b></td>
                                                <td class="field_input">
                                                    <asp:TextBox ID="txtend" runat="server" MaxLength="10" Width="70" EnableViewState="true"
                                                        placeholder="dd/MM/yyyy"
                                                        Font-Bold="true">
                                                    </asp:TextBox>
                                                    <asp:ImageButton ID="ImageButton4" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                        runat="server" Height="24" />
                                                    <ajaxToolkit:CalendarExtender ID="Calendarend" PopupButtonID="ImageButton4" runat="server"
                                                        TargetControlID="txtend" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td class="field_input">
                                                    <div class="btn_24_blue">
                                                        <span class="icon find_co"></span>
                                                        <asp:Button ID="btn" runat="server" Text="Search" CssClass="btn_link"
                                                            ValidationGroup="Search" OnClientClick="javascript:ShowProgressBar()" OnClick="btnSo_Click" />
                                                    </div>
                                                </td>

                                            </tr>

                                        </table>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>
          </div>
        <div id="asmgraph" runat="server">]
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnasm" />
                                </Triggers>
                                <ContentTemplate>

                                    <div id="sidenav4" class="sidenav">
                                        <div id="slidebtn4" class="slideBtn fa fa-filter" onclick="openNav4()">Chart Filter</div>
                                        <a href="#" onclick="closeNav4()" class="closebtn">
                                            <img src="../images/Cancel.png" alt="" height="15px" /><span id="dvProgressBar4" align="center" style="visibility: hidden; font-size: small; color: black; padding-left: 40px;">Requesting under process....Please wait.....</span></a>
                                        <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                            <tr>
                                                <td class="field_title"><b>From Date</b></td>
                                                <td class="field_input">
                                                    <asp:TextBox ID="start" runat="server" MaxLength="10" Width="70" EnableViewState="true"
                                                        placeholder="dd/MM/yyyy"
                                                        Font-Bold="true">
                                                    </asp:TextBox>
                                                    <asp:ImageButton ID="ImageButton3" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle" runat="server" Height="24" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarAsm" PopupButtonID="ImageButton3" runat="server"
                                                        TargetControlID="start" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td class="field_title"><b>To Date</b></td>
                                                <td class="field_input">
                                                    <asp:TextBox ID="end" runat="server" MaxLength="10" Width="70" EnableViewState="true"
                                                        placeholder="dd/MM/yyyy"
                                                        Font-Bold="true">
                                                    </asp:TextBox>
                                                    <asp:ImageButton ID="ImageButton5" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                        runat="server" Height="24" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarAsmend" PopupButtonID="ImageButton5" runat="server"
                                                        TargetControlID="end" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td class="field_input">
                                                    <div class="btn_24_blue">
                                                        <span class="icon find_co"></span>
                                                        <asp:Button ID="btnasm" runat="server" Text="Search" CssClass="btn_link"
                                                            ValidationGroup="Search" OnClientClick="javascript:ShowProgressBar()" OnClick="btnAsm_Click" />
                                                    </div>
                                                </td>

                                            </tr>

                                        </table>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>
          </div>
        <span class=""></span>
    </div>

    <div id="heightbaradjust" runat="server" style="height: 30px;"></div>
    <div>

        <div id="swichbarOther" runat="server" class="switch_bar">
            <ul>

                <li><a href="#" onclick="addTab('Invoice GT','frmOrderInvoice.aspx?MENUID=90&BSID=7F62F951-9D1F-4B8D-803B-74EEBA468CEE&CHECKER=FALSE&CHALLAN=FALSE')">
                    <span class="stats_icon invoice_sl"></span><span>Invoice GT</span></a></li>

                <li><a href="#" onclick="addTab('Invoice MT','frmSaleInvoice.aspx?MENUID=1123&BSID=0AA9353F-D350-4380-BC84-6ED5D0031E24&CHECKER=FALSE&CHALLAN=FALSE')">
                    <span class="stats_icon invoice_sl"></span><span>Invoice MT</span></a></li>

                <li><a href="#" onclick="addTab('Approval','frmcheckerApproval.aspx')">
                    <span class="stats_icon application_icons_co"></span><span>Approval</span></a></li>

                <li><a href="#" onclick="addTab('Advance Receipt','frmAccAdvanceVoucher.aspx?VOUCHERID=16&CHECKER=FALSE')">
                    <span class="stats_icon  application_put_co"></span><span>Bank Receipt</span></a></li>

                <li><a href="#" onclick="addTab('Journal','frmAccVoucher.aspx?VOUCHERID=2&CHECKER=FALSE&AUTOOPEN=FALSE&AUTOVOUCHERID=0&AUTOVOUCHERDATE=0')">
                    <span class="stats_icon product_design_sl"></span><span>Journal</span></a></li>

                <li><a href="#" onclick="addTab('Sale Tax Summary (GST)','SalesTaxSummary_Details_GST_Report.aspx')">
                    <span class="stats_icon sale_sl"></span><span>GST Report</span></a></li>

                <li><a href="#" onclick="addTab('StockInHand','frmRptStockInHand_Fac.aspx')">
                    <span class="stats_icon current_work_sl"></span><span>Stock In Hand</span></a></li>

                <li><a href="#" onclick="addTab('Item Ledger','frmRptItemLedger_Factory.aspx')">
                    <span class="stats_icon product_sl"></span><span>Item Ledger</span></a></li>

                <li><a href="#" onclick="addTab('Stock Journal','frmStockAdjustment.aspx?MENUID=47')">
                    <span class="stats_icon application_split_co"></span><span>Stock Jounal</span></a></li>

                <li><a href="#" onclick="addTab('Sync Status','frmrptDepotlastSyncStatus.aspx?MENUID=47')">
                    <span class="stats_icon application_boxes_co"></span><span>Sync Status</span></a></li>

                <li><a href="#">
                    <span class="stats_icon upcoming_work_sl"></span>
                    <asp:Button ID="btnWorkflow" runat="server" Text="Workflow" OnClick="btnWorkflow_Click" BackColor="Transparent" BorderColor="Transparent" /></a></li>
            </ul>
        </div>

        <div id="swichbarOther_fac" runat="server" class="switch_bar">
            <ul>
                <li><a href="#">
                    <span class="stats_icon product_sl"></span>
                    <asp:Button ID="btnWorkflowfac_qc" runat="server" Text="Stockin" OnClick="btnWorkflowfac_qc_Click" BackColor="Transparent" BorderColor="Transparent" /></a></li>

                <li><a href="#">
                    <span class="stats_icon upcoming_work_sl"></span>
                    <asp:Button ID="btnWorkflowfac" runat="server" Text="Production" OnClick="btnWorkflowfac_Click" BackColor="Transparent" BorderColor="Transparent" /></a></li>
            </ul>
        </div>

    </div>


    <div class="container">
       <div class="center">
    <div id="outPopUp" runat="server">
      <div class="widget_content" style="padding: 10px 0px; height: 115px;">
               <div style="overflow: scroll; height: 300px;" onscroll="OnScrollDiv(this)" id="DivMainContent10">
        <asp:GridView ID="grdDynamic" runat="server" CssClass="mydatagrid" AutoGenerateColumns="false" Style="height: 10px; overflow: auto"
            EmptyDataRowStyle-ForeColor="#cc0000" HeaderStyle-Width="100%" ItemStyle-Width="100%" ShowHeader="true">
            <Columns>
                <asp:BoundField  HeaderText="Slno" DataField="SLNO" HeaderStyle-Width="50px"/>
       <asp:TemplateField HeaderText="Page Name" >
            <ItemTemplate>
              <asp:HyperLink ID="hLnkProject" runat="server" Text='<%# Eval("PageName") %>'  Width="250px"
             NavigateUrl= '<%# "" + Eval("PageURL") %>' Font-Size="Medium" ForeColor="#999999">
              </asp:HyperLink>
            </ItemTemplate>
         </asp:TemplateField>
    </Columns>
        </asp:GridView>
                   </div>
          </div>
    </div>
       </div>
       </div>


    <div id="content" runat="server" style="display:none">


        <div class="grid_container">


            <div class="grid_6" style="margin-top: 5px;">
                <div class="widget_wrap" id="div_customer_details" runat="server" style="display: none;">
                    <div class="widget_top">
                        <span class="h_icon_he list_images"></span>
                        <h6>Customer Invoices Details</h6>
                    </div>
                    <div class="widget_content" style="padding: 10px 0px; height: 100px;">
                        <div class="gridcontent">
                            <div class="reportgrid">
                                <asp:GridView ID="gvCustomerInvoice1" runat="server" Width="100%" ShowFooter="true" RowStyle-Height="24px"
                                    AutoGenerateColumns="false" EmptyDataText="No Records Available"
                                    AllowPaging="true" PageSize="10">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <a href="#" onclick="addTab('<%# Eval("Invoices")%>','<%# Eval("PageURL") %>')" title="No Of Invoices" style="font-weight: bold; color: Green;"><%# Eval("Invoices")%></a>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No. of Invoices" HeaderStyle-Width="100">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInvouceDetails" runat="server" Text='<%# Eval("TOTAL_INVOICES") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>



            <div class="grid_6" style="margin-top: 5px;">
                <div class="widget_wrap" id="div_Workflow" runat="server" style="display: none;">
                    <div class="widget_top">
                        <span class="h_icon_he list_images"></span>
                    </div>
                    <div class="widget_content" style="padding: 10px 0px; height: 100px;">
                        <div class="gridcontent">
                            <div class="reportgrid">
                                <asp:GridView ID="grdPendingStockReceived" runat="server" Width="100%"
                                    AutoGenerateColumns="false" EmptyDataText="No Records Available"
                                    AllowPaging="true" PageSize="10">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <a href="#" onclick="addTab('Purchase Stock Receipt-Checker','frmStockReceived.aspx?MENUID=102&CHECKER=TRUE')" title="No Of Pending Purchase Stock Receipt" style="font-weight: bold; color: Green;">PENDING PURCHASE STOCK RECEIPT</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgrdNumber11" runat="server" Text='<%# Eval("PENDINGNUMBER") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NEXTLEVELID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgrdNumber1" runat="server" Text='<%# Eval("NEXTLEVELID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>



            <div class="grid_6" style="margin-top: 5px;">
                <div class="widget_wrap" id="divPurchaseStockReceipt" runat="server" style="display: none;">
                    <div class="widget_top">
                        <span class="h_icon_he list_images"></span>
                        <h6>Claim Details</h6>
                    </div>
                    <div class="widget_content" style="padding: 10px 0px; height: 100px;">
                        <div class="gridcontent" class="reportgrid">
                            <cc1:Grid ID="grdUserlist" runat="server" CallbackMode="false" Serialize="true" AllowSorting="false" AutoGenerateColumns="false" AllowPageSizeSelection="false"
                                AllowAddingRecords="false" AllowFiltering="true" AllowPaging="false" EnableRecordHover="true">
                                <Columns>
                                    <cc1:Column ID="Column1" DataField="CLAIM_ID" HeaderText="CLAIMTYPE ID" runat="server" Visible="false"></cc1:Column>
                                    <cc1:Column ID="Column2" DataField="CLAIM_TYPE" HeaderText="CLAIM TYPE" runat="server" Wrap="true" Width="300px">
                                        <TemplateSettings TemplateId="tpl_navigaretopage" />
                                    </cc1:Column>
                                    <cc1:Column ID="Column3" DataField="NO_PENDING_ITEMS" HeaderText="PENDING ITEMS" runat="server" Wrap="true" Width="100px"></cc1:Column>
                                </Columns>
                                <Templates>
                                    <cc1:GridTemplate runat="server" ID="tpl_navigaretopage">
                                        <Template>
                                            <a href="#" onclick="addTab('<%# Container.DataItem["CLAIM_TYPE"]%>','<%# Container.DataItem["PAGE_URL"] %>?CTYPE=<%# Container.DataItem["CLAIM_ID"] %>&MNID=<%# Container.DataItem["MNID"] %>')"><%# Container.DataItem["CLAIM_TYPE"]%></a>

                                        </Template>
                                    </cc1:GridTemplate>
                                </Templates>
                                <ScrollingSettings ScrollWidth="100%" ScrollHeight="350" />
                            </cc1:Grid>
                        </div>
                    </div>
                </div>
            </div>


            <table width="100%" border="50" cellspacing="50" cellpadding="50" class="form_container_td">
                <tr>
                    <td id="TD1" runat="server">
                        <fieldset style="width: 90%;">
                            <legend>Production Status</legend>


                            <asp:GridView ID="GridView1" runat="server" Width="100%" ShowFooter="true" AutoGenerateColumns="false"
                                CssClass="zebra">
                                <Columns>

                                    <asp:TemplateField HeaderText="Total Production">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblPRODUCTION" runat="server" Text='<%#Eval("PRODUCTION") %>' Width="170px"
                                                OnCommand="lblPRODUCTION_Click"> </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Requisition Done">
                                        <ItemTemplate>
                                            <asp:Label  ID="lblRequisition" runat="server" Text='<%# Eval("REQUISITIONDONE") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Requisition Pending">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblRequisitionPending" runat="server" Text='<%#Eval("REQUISITIONPENDING") %>' Width="170px"
                                                OnCommand="REQUISITIONPENDING_Click"> </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issue Done">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIssue" runat="server" Text='<%# Eval("ISSUEDONE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issue Pending">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblIssuePending" runat="server" Text='<%#Eval("ISSUEPENDING") %>' Width="170px"
                                                OnCommand="lblIssuePending_Click"> </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qa Done">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgrdNumber10" runat="server" Text='<%# Eval("QADONE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pending Qa">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblPENDINGQA" runat="server" Text='<%#Eval("PENDINGQA") %>' Width="170px"
                                                OnCommand="lblPENDINGQA_Click"> </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="NEXTLEVELID" Visible="false">
                            <ItemTemplate>
                                 <asp:Label ID="lblgrdNumber1" runat="server" Text='<%# Eval("NEXTLEVELID") %>'></asp:Label>
                            </ItemTemplate>
                         </asp:TemplateField>
                                    --%>
                                </Columns>
                            </asp:GridView>

                        </fieldset>
                    </td>

                    <td id="TD2" runat="server">
                        <fieldset style="width: 90%;">
                            <legend>Stock In Status</legend>


                            <asp:GridView ID="GridView2" runat="server" Width="100%" ShowFooter="true" AutoGenerateColumns="false"
                                CssClass="zebra">
                                <Columns>

                                    <asp:TemplateField HeaderText="Total GRN">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblTOTALGRNDONE" runat="server" Text='<%#Eval("TOTALGRNDONE") %>' Width="170px"
                                                OnCommand="lblTOTALGRNDONE_Click"> </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qc Done">
                                        <ItemTemplate>
                                            <asp:Label ID="lblqc" runat="server" Text='<%# Eval("TOTALQCDONE") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qc Pending">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblTOTALQCPENDING" runat="server" Text='<%#Eval("TOTALQCPENDING") %>' Width="170px"
                                                OnCommand="lblTOTALQCPENDING_Click"> </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stock In">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTOTALSTOCKIN" runat="server" Text='<%# Eval("TOTALSTOCKIN") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stock In Pending">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblTOTALSTOCKINPENDING" runat="server" Text='<%#Eval("TOTALSTOCKINPENDING") %>' Width="170px"
                                                OnCommand="lblTOTALSTOCKINPENDING_Click"> </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Checker1 Approved">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCHECKER1APPROVED" runat="server" Text='<%# Eval("CHECKER1APPROVED") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Checker1 Pending">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblCHECKER1APPROVEDPENDING" runat="server" Text='<%#Eval("CHECKER1APPROVEDPENDING") %>' Width="170px"
                                                OnCommand="lblCHECKER1APPROVEDPENDING_Click"> </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Checker2 Approved">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFINALAPPROVED" runat="server" Text='<%# Eval("FINALAPPROVED") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Checker2 Pending">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblFINALAPPROVEDPENDING" runat="server" Text='<%#Eval("FINALAPPROVEDPENDING") %>' Width="170px"
                                                OnCommand="lblFINALAPPROVEDPENDING_Click"> </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                </Columns>
                            </asp:GridView>

                        </fieldset>
                    </td>
                </tr>
            </table>


        </div>


        <div class="container-fluid">
            <div class="row">
                <div class="grid_container" id="dvGraph1" runat="server">
                    <div class="box box-default">
                        <div class="div3">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSearch" />
                                </Triggers>
                                <ContentTemplate>

                                    <div id="sidenav" class="sidenav">
                                        <div id="slidebtn" class="slideBtn fa fa-filter" onclick="openNav()">Chart Filter</div>
                                        <a href="#" onclick="closeNav()" class="closebtn">
                                            <img src="../images/Cancel.png" alt="" height="15px" /><span id="dvProgressBar" align="center" style="visibility: hidden; font-size: small; color: black; padding-left: 40px;">Requesting under process....Please wait.....</span></a>
                                        <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                            <tr>
                                                <td class="field_title"><b>From Date</b></td>
                                                <td class="field_input">
                                                    <asp:TextBox ID="txtFromDate" runat="server" MaxLength="10" Width="70" EnableViewState="true"
                                                        placeholder="dd/MM/yyyy"
                                                        Font-Bold="true">
                                                    </asp:TextBox>
                                                    <asp:ImageButton ID="imgPopup" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle" runat="server" Height="24" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarFromDate" PopupButtonID="imgPopup" runat="server"
                                                        TargetControlID="txtFromDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td class="field_title"><b>To Date</b></td>
                                                <td class="field_input">
                                                    <asp:TextBox ID="txtToDate" runat="server" MaxLength="10" Width="70" EnableViewState="true"
                                                        placeholder="dd/MM/yyyy"
                                                        Font-Bold="true">
                                                    </asp:TextBox>
                                                    <asp:ImageButton ID="ImageButton1" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle"
                                                        runat="server" Height="24" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtenderToDate" PopupButtonID="ImageButton1" runat="server"
                                                        TargetControlID="txtToDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td class="field_input">
                                                    <div class="btn_24_blue">
                                                        <span class="icon find_co"></span>
                                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn_link"
                                                            ValidationGroup="Search" OnClientClick="javascript:ShowProgressBar()" OnClick="btnSearch_Click" />
                                                    </div>
                                                </td>

                                            </tr>

                                        </table>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <div class="grid_4">
                                <!-- store graph -->
                                <div class="box box-default">
                                    <div class="div3">
                                        <div>
                                            <asp:Literal ID="ltCategorywisesale" runat="server"></asp:Literal>
                                            <div id="chart_divcategorywisesale" style="width: 100%; height: 190px; align-self: center; margin-top: 0px; box-shadow: 3px 3px 3px 3px #888888;">
                                                <div style="width: 100%; height: 150px; align-self: center; margin-top: 0px; float: right;">
                                                    <div class="sticky">
                                                        <div class='sticky'></div>
                                                    </div>
                                                    <div class="sticky">
                                                        <div class='sticky'>internet connection in not available,please connect internet to view category wise sale</div>
                                                        <img src="../images/net_unavailable.jpg" width="250" style="margin-left: 55px; margin-top: 15px;" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div>&nbsp;</div>
                                            <asp:Literal ID="ltTgtAch" runat="server"></asp:Literal>
                                            <div id="chart_divTgtAch" style="width: 100%; height: 190px; align-self: center; margin-top: 0px; float: right; box-shadow: 3px 3px 3px 3px #888888;">
                                                <div style="width: 100%; height: 150px; align-self: center; margin-top: 0px; float: right;">
                                                    <div class="sticky">
                                                        <div class='sticky'></div>
                                                    </div>
                                                    <div class="sticky">
                                                        <div class='sticky'>Coming soon...</div>
                                                        <img src="../images/net_unavailable.jpg" width="250" style="margin-left: 55px; margin-top: 15px;" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="grid_4">
                                <!-- store graph -->
                                <div class="box box-default">
                                    <div class="div3">
                                        <div>

                                            <asp:Literal ID="ltBrandwisesalegraph" runat="server"></asp:Literal>
                                            <div id="chart_divBrandsale" style="width: 100%; height: 190px; align-self: center; margin-top: 0px; float: right; box-shadow: 3px 3px 3px 3px #888888;">
                                                <div style="width: 100%; height: 150px; align-self: center; margin-top: 0px; float: right;">
                                                    <div class="sticky">
                                                        <div class='sticky'></div>
                                                    </div>
                                                    <div class="sticky">
                                                        <div class='sticky'>internet connection in not available,please connect internet to view brand wise sale</div>
                                                        <img src="../images/net_unavailable.jpg" width="250" style="margin-left: 55px; margin-top: 15px;" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div>&nbsp;</div>

                                            <asp:Literal ID="ltDistributor" runat="server"></asp:Literal>
                                            <div id="chart_divDistributor" style="width: 100%; height: 190px; align-self: center; margin-top: 0px; float: right; box-shadow: 3px 3px 3px 3px #888888;">
                                                <div style="width: 100%; height: 150px; align-self: center; margin-top: 0px; float: right;">
                                                    <div class="sticky">
                                                        <div class='sticky'></div>
                                                    </div>
                                                    <div class="sticky">
                                                        <%--<div class='sticky'>internet connection in not available,please connect internet to view top 10 distributor</div>--%>
                                                        <div class='sticky'>Coming soon...</div>
                                                        <img src="../images/net_unavailable.jpg" width="250" style="margin-left: 55px; margin-top: 15px;" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="grid_4">
                                <!-- store graph -->
                                <div class="box box-default">
                                    <div class="div3">
                                        <div>
                                            <asp:Literal ID="ltStatewisesalegraph" runat="server"></asp:Literal>
                                            <div id="chart_divStatesale" style="width: 100%; height: 190px; align-self: center; margin-top: 0px; float: right; box-shadow: 3px 3px 3px 3px #888888;">
                                                <div style="width: 100%; height: 150px; align-self: center; margin-top: 0px; float: right;">
                                                    <div class="sticky">
                                                        <div class='sticky'></div>
                                                    </div>
                                                    <div class="sticky">
                                                        <div class='sticky'>internet connection in not available,please connect internet to view top 5 state wise sale</div>
                                                        <img src="../images/net_unavailable.jpg" width="250" style="margin-left: 55px; margin-top: 15px;" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div>&nbsp;</div>
                                            <asp:Literal ID="ltproduct" runat="server"></asp:Literal>
                                            <div id="chart_divproduct" style="width: 100%; height: 190px; align-self: center; margin-top: 0px; float: right; box-shadow: 3px 3px 3px 3px #888888;">
                                                <div style="width: 100%; height: 150px; align-self: center; margin-top: 0px; float: right;">
                                                    <div class="sticky">
                                                        <div class='sticky'></div>
                                                    </div>
                                                    <div class="sticky">
                                                        <div class='sticky'>Coming soon...</div>
                                                        <img src="../images/net_unavailable.jpg" width="250" style="margin-left: 55px; margin-top: 15px;" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>

                <div>&nbsp;</div>

                <div class="grid_container" id="dvGraph2" runat="server">
                    <div class="box box-default">
                        <div class="div3">

                            <div class="grid_4">
                                <div class="box box-default">
                                    <div class="div3">
                                        <div>
                                            <asp:Literal ID="ltOrderInvoice" runat="server"></asp:Literal>
                                            <div id="chart_divOrderInvoice" style="width: 100%; height: 190px; align-self: center; margin-top: 0px; box-shadow: 3px 3px 3px 3px #888888;">
                                                <div style="width: 100%; height: 150px; align-self: center; margin-top: 0px; float: right;">
                                                    <div class="sticky">
                                                        <div class='sticky'></div>
                                                    </div>
                                                    <div class="sticky">
                                                        <div class='sticky'>Coming soon...</div>
                                                        <img src="../images/net_unavailable.jpg" width="250" style="margin-left: 55px; margin-top: 15px;" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div>&nbsp;</div>

                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="grid_4">
                                <div class="box box-default">
                                    <div class="div3">
                                        <div>
                                            <asp:Literal ID="ltMonthTrend" runat="server"></asp:Literal>
                                            <div id="chart_divMonthTrendsale" style="width: 100%; height: 190px; align-self: center; margin-top: 0px; box-shadow: 3px 3px 3px 3px #888888;">
                                                <div style="width: 100%; height: 150px; align-self: center; margin-top: 0px; float: right;">
                                                    <div class="sticky">
                                                        <div class='sticky'></div>
                                                    </div>
                                                    <div class="sticky">
                                                        <div class='sticky'>Coming soon...</div>
                                                        <img src="../images/net_unavailable.jpg" width="250" style="margin-left: 55px; margin-top: 15px;" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div>&nbsp;</div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="grid_4">
                                <div class="box box-default">
                                    <div class="div3">
                                        <div>
                                            <asp:Literal ID="ltWeeklyTrend" runat="server"></asp:Literal>
                                            <div id="chart_divWeeklyTrendsale" style="width: 100%; height: 190px; align-self: center; margin-top: 0px; float: right; box-shadow: 3px 3px 3px 3px #888888;">
                                                <div style="width: 100%; height: 150px; align-self: center; margin-top: 0px; float: right;">
                                                    <div class="sticky">
                                                        <div class='sticky'></div>
                                                    </div>
                                                    <div class="sticky">
                                                        <div class='sticky'>Coming soon...</div>
                                                        <img src="../images/net_unavailable.jpg" width="250" style="margin-left: 55px; margin-top: 15px;" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div>&nbsp;</div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                        </div>

                    </div>
                </div>

                <div>&nbsp;</div>

                <div class="grid_container" id="dvGraph3" runat="server">

                    <div class="box box-default">
                        <div class="div1">

                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnForecastSearch" />
                                </Triggers>
                                <ContentTemplate>

                                    <div id="sidenav2" class="sidenav2">
                                        <div id="slidebtn2" class="slideBtn fa fa-filter" onclick="openNav2()">Chart Filter</div>
                                        <a href="#" onclick="closeNav2()" class="closebtn">
                                            <img src="../images/Cancel.png" alt="" height="15px" /><span id="dvProgressBar2" align="center" style="visibility: hidden; font-size: small; color: black; padding-left: 40px;">Requesting under process....Please wait.....</span></a>
                                        <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td" height="100">

                                            <tr>
                                                <td ><b>Date</b></td>
                                                <td >
                                                    <asp:TextBox ID="txtAsOnDate" runat="server" MaxLength="10" Width="60" EnableViewState="true"
                                                        placeholder="dd/MM/yyyy"
                                                        Font-Bold="true">
                                                    </asp:TextBox>
                                                    <asp:ImageButton ID="ImageButton2" ImageUrl="../images/calendar.png" ImageAlign="AbsMiddle" runat="server" Height="24" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtenderAsOnDate" PopupButtonID="ImageButton2" runat="server"
                                                        TargetControlID="txtAsOnDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td ><b>Segment</b></td>
                                                <td >
                                                    <asp:DropDownList ID="ddlSegment" runat="server" Width="120">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td ><b>Brand</b></td>
                                                <td >
                                                    <asp:DropDownList ID="ddlBrand" runat="server" Width="120">
                                                    </asp:DropDownList>
                                                </td>
                                                <td ><b>Category</b></td>
                                                <td >
                                                    <asp:DropDownList ID="ddlCategory" runat="server" Width="120">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td ><b>Product</b></td>
                                                <td >
                                                    <asp:DropDownList ID="ddlProduct" runat="server" Width="150" >
                                                    </asp:DropDownList>
                                                </td>
                                                <td colspan="2">
                                                    <div class="btn_24_blue">
                                                        <span class="icon find_co"></span>
                                                        <asp:Button ID="btnForecastSearch" runat="server" Text="Search" CssClass="btn_link"
                                                            ValidationGroup="Search" OnClientClick="javascript:ShowProgressBar2()" OnClick="btnForecastSearch_Click" />
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <div>
                                <asp:Literal ID="ltForecast" runat="server"></asp:Literal>
                                <div id="chart_divForecast" style="width: 100%; height: 400px; align-self: center; margin-top: 0px; box-shadow: 3px 3px 3px 3px #888888;">
                                    <div style="width: 100%; height: 350px; align-self: center; margin-top: 0px; float: right;">
                                        <div class="sticky">
                                            <div class='sticky'></div>
                                        </div>
                                        <div class="sticky">
                                            <div class='sticky'>internet connection in not available,please connect internet to view forecast & stock</div>
                                             <img src="../images/nodata-found1.jpg" width="250" style="margin-left: 55px; margin-top: 15px" />
                                        </div>
                                    </div>
                                </div>
                                <div>&nbsp;</div>
                            </div>
                        </div>
                    </div>

                </div>

            </div>
        </div>

    </div>

    <div id="Div1" runat="server">
        <div class="container-fluid">

            <div class="row">

                <div class="grid_container" id="div2" runat="server">
                    <%--id div_customer_details add here--%>

                    <div class="grid_4">

                        <!-- store graph -->
                        <div class="box box-default">
                            <div class="div3">
                                <div>

                                    <div class="grid_container">

                                        <div id="ex1" class="modal" style="width:100%;">
                                              <div>
                                        <div class="auto-style2" style="height: 100%; width: 100%;">
                                            <div style="width: 100%; background-color: white; height: 100%">
                                                <%--id div_customer_details remove from here--%>
                                                <div>
                                                    <span></span>
                                                    <h6 style="padding-top: 2px;">Customer Invoices Details</h6>
                                                </div>
                                                <div class="widget_content" style="padding: 10px 0px; height: 115px;">

                                                    <div class="gridcontent">
                                                        <div class="reportgrid">
                                                            <asp:GridView ID="gvCustomerInvoice" runat="server" Width="90%" ShowFooter="true" RowStyle-Height="24px"
                                                                AutoGenerateColumns="false" EmptyDataText="No Records Available"
                                                                AllowPaging="true" PageSize="10">
                                                                <Columns>

                                                                    <asp:TemplateField HeaderText="Description">
                                                                        <ItemTemplate>
                                                                            <a href="#" onclick="addTab('<%# Eval("Invoices")%>','<%# Eval("PageURL") %>')" title="No Of Invoices" style="font-weight: bold; color: Green;"><%# Eval("Invoices")%></a>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="No. of Invoices" HeaderStyle-Width="100">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblInvouceDetails" runat="server" Text='<%# Eval("TOTAL_INVOICES") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Right" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                                  </div>
                                            </div>
                                        <div class="grid_6" style="margin-top: 5px;">
                                            <div class="widget_wrap" id="div3" runat="server" style="display: none;">
                                                <div class="widget_top">
                                                    <span class="h_icon_he list_images"></span>
                                                </div>
                                                <div class="widget_content" style="padding: 10px 0px; height: 100px;">
                                                    <div class="gridcontent">
                                                        <div class="reportgrid">
                                                            <asp:GridView ID="GridView4" runat="server" Width="100%"
                                                                AutoGenerateColumns="false" EmptyDataText="No Records Available"
                                                                AllowPaging="true" PageSize="10">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Description">
                                                                        <ItemTemplate>
                                                                            <a href="#" onclick="addTab('Purchase Stock Receipt-Checker','frmStockReceived.aspx?MENUID=102&CHECKER=TRUE')" title="No Of Pending Purchase Stock Receipt" style="font-weight: bold; color: Green;">PENDING PURCHASE STOCK RECEIPT</a>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Total">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgrdNumber11" runat="server" Text='<%# Eval("PENDINGNUMBER") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="NEXTLEVELID" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgrdNumber1" runat="server" Text='<%# Eval("NEXTLEVELID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="grid_6" style="margin-top: 5px;">
                                            <div class="widget_wrap" id="div4" runat="server" style="display: none;">
                                                <div class="widget_top">
                                                    <span class="h_icon_he list_images"></span>
                                                    <h6>Claim Details</h6>
                                                </div>
                                                <div class="widget_content" style="padding: 10px 0px; height: 100px;">
                                                    <div class="gridcontent" class="reportgrid">
                                                        <cc1:Grid ID="Grid1" runat="server" CallbackMode="false" Serialize="true" AllowSorting="false" AutoGenerateColumns="false" AllowPageSizeSelection="false"
                                                            AllowAddingRecords="false" AllowFiltering="true" AllowPaging="false" EnableRecordHover="true">
                                                            <Columns>
                                                                <cc1:Column ID="Column4" DataField="CLAIM_ID" HeaderText="CLAIMTYPE ID" runat="server" Visible="false"></cc1:Column>
                                                                <cc1:Column ID="Column5" DataField="CLAIM_TYPE" HeaderText="CLAIM TYPE" runat="server" Wrap="true" Width="300px">
                                                                    <TemplateSettings TemplateId="tpl_navigaretopage" />
                                                                </cc1:Column>
                                                                <cc1:Column ID="Column6" DataField="NO_PENDING_ITEMS" HeaderText="PENDING ITEMS" runat="server" Wrap="true" Width="100px"></cc1:Column>
                                                            </Columns>
                                                            <Templates>
                                                                <cc1:GridTemplate runat="server" ID="GridTemplate1">
                                                                    <Template>
                                                                        <a href="#" onclick="addTab('<%# Container.DataItem["CLAIM_TYPE"]%>','<%# Container.DataItem["PAGE_URL"] %>?CTYPE=<%# Container.DataItem["CLAIM_ID"] %>&MNID=<%# Container.DataItem["MNID"] %>')"><%# Container.DataItem["CLAIM_TYPE"]%></a>

                                                                    </Template>
                                                                </cc1:GridTemplate>
                                                            </Templates>
                                                            <ScrollingSettings ScrollWidth="100%" ScrollHeight="350" />
                                                        </cc1:Grid>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <table width="100%" border="50" cellspacing="20" cellpadding="30" class="form_container_td">
                                            <tr>
                                                <td id="TD3" runat="server" class="auto-style1">
                                                    <fieldset style="width: 90%;">
                                                        <legend>Production Status</legend>


                                                        <asp:GridView ID="GridView5" runat="server" Width="100%" ShowFooter="true" AutoGenerateColumns="false"
                                                            CssClass="zebra">
                                                            <Columns>

                                                                <asp:TemplateField HeaderText="Total Production">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lblPRODUCTION" runat="server" Text='<%#Eval("PRODUCTION") %>' Width="170px"
                                                                            OnCommand="lblPRODUCTION_Click"> </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Requisition Done">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRequisition" runat="server" Text='<%# Eval("REQUISITIONDONE") %>'></asp:Label>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Requisition Pending">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lblRequisitionPending" runat="server" Text='<%#Eval("REQUISITIONPENDING") %>' Width="170px"
                                                                            OnCommand="REQUISITIONPENDING_Click"> </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Issue Done">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblIssue" runat="server" Text='<%# Eval("ISSUEDONE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Issue Pending">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lblIssuePending" runat="server" Text='<%#Eval("ISSUEPENDING") %>' Width="170px"
                                                                            OnCommand="lblIssuePending_Click"> </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Qa Done">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgrdNumber10" runat="server" Text='<%# Eval("QADONE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Pending Qa">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lblPENDINGQA" runat="server" Text='<%#Eval("PENDINGQA") %>' Width="170px"
                                                                            OnCommand="lblPENDINGQA_Click"> </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="NEXTLEVELID" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgrdNumber1" runat="server" Text='<%# Eval("NEXTLEVELID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                            </Columns>
                                                        </asp:GridView>

                                                    </fieldset>
                                                </td>

                                                <td id="TD4" runat="server" class="auto-style1">
                                                    <fieldset style="width: 90%;">
                                                        <legend>Stock In Status</legend>


                                                        <asp:GridView ID="GridView6" runat="server" Width="100%" ShowFooter="true" AutoGenerateColumns="false"
                                                            CssClass="zebra">
                                                            <Columns>

                                                                <asp:TemplateField HeaderText="Total GRN">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lblTOTALGRNDONE" runat="server" Text='<%#Eval("TOTALGRNDONE") %>' Width="170px"
                                                                            OnCommand="lblTOTALGRNDONE_Click"> </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Qc Done">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblqc" runat="server" Text='<%# Eval("TOTALQCDONE") %>'></asp:Label>
                                                                    </ItemTemplate>

                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Qc Pending">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lblTOTALQCPENDING" runat="server" Text='<%#Eval("TOTALQCPENDING") %>' Width="170px"
                                                                            OnCommand="lblTOTALQCPENDING_Click"> </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Stock In">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTOTALSTOCKIN" runat="server" Text='<%# Eval("TOTALSTOCKIN") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Stock In Pending">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lblTOTALSTOCKINPENDING" runat="server" Text='<%#Eval("TOTALSTOCKINPENDING") %>' Width="170px"
                                                                            OnCommand="lblTOTALSTOCKINPENDING_Click"> </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Checker1 Approved">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCHECKER1APPROVED" runat="server" Text='<%# Eval("CHECKER1APPROVED") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Checker1 Pending">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lblCHECKER1APPROVEDPENDING" runat="server" Text='<%#Eval("CHECKER1APPROVEDPENDING") %>' Width="170px"
                                                                            OnCommand="lblCHECKER1APPROVEDPENDING_Click"> </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Checker2 Approved">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblFINALAPPROVED" runat="server" Text='<%# Eval("FINALAPPROVED") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Checker2 Pending">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lblFINALAPPROVEDPENDING" runat="server" Text='<%#Eval("FINALAPPROVEDPENDING") %>' Width="170px"
                                                                            OnCommand="lblFINALAPPROVEDPENDING_Click"> </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                        </table>

                                       <%-- <div>&nbsp;</div>
                                        <div>&nbsp;</div>
                                        <div>&nbsp;</div>
                                        <div>&nbsp;</div>--%>
                                         <asp:Literal ID="lttarget" runat="server"></asp:Literal>

                                  
                                          <div id="chart_divtargatechv" style="width: 100%; height: 190px; align-self: center; margin-top: 0px; float: right;">
                                        <div style="width: 100%;">
                                            <div class="sticky">
                                                <div class='sticky'></div>
                                            </div>
                                            <div class="sticky">
                                                <div class='sticky'>internet connection in not available,please connect internet to show TSI sale graph</div>
                                                 <img src="../images/nodata-found1.jpg" width="250" style="margin-left: 55px; margin-top: 15px" />
                                            </div>
                                        </div>
                                    </div>
                               
                                <div>&nbsp;</div>
                                <div>&nbsp;</div>



                                        <asp:Literal ID="lt12monthproduct" runat="server"></asp:Literal>

                                        <div id="chart_div12monthproduct" style="width: 100%; height: 190px; align-self: center; margin-top: 0px;">
                                            <div>
                                                <div class="sticky">
                                                    <div class='sticky'></div>
                                                </div>
                                                <div class="sticky">
                                                    <div class='sticky'>internet connection in not available,please connect internet to show last 6 month Primary and Secondary sale graph</div>
                                                     <img src="../images/nodata-found1.jpg" width="250" style="margin-left: 55px; margin-top: 15px" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div>&nbsp;</div>
                                <div>&nbsp;</div>
                                  
                                
                     

                                   
                               
                            </div>
                        </div>
                    </div>

                    <div class="grid_4">
                        <!-- store graph -->
                        <div class="box box-default">
                            <div class="div3">
                                <div>
                                    <asp:Literal ID="ltcategorysalegraph" runat="server"></asp:Literal>

                                    <div id="chart_divcategorysale" style="width: 100%; height: 190px; align-self: center; margin-top: 0px; float: right;">
                                        <div style="width: 100%;">
                                            <div class="sticky">
                                                <div class='sticky'></div>
                                            </div>
                                            <div class="sticky">
                                                <div class='sticky'>internet connection in not available,please connect internet to show category sale graph</div>
                                                 <img src="../images/nodata-found1.jpg" width="250" style="margin-left: 55px; margin-top: 15px" />
                                            </div>
                                        </div>
                                    </div>
                                    <div>&nbsp;</div>
                                    <div>&nbsp;</div>
                                    <asp:Literal ID="ltmonthsale" runat="server"></asp:Literal>

                                    <div id="chart_divmonthsale" style="width: 100%; height: 190px; align-self: center; margin-top: 0px; float: right;">
                                        <div style="width: 100%; height: 150px; align-self: center; margin-top: 0px; float: right;">
                                            <div class="sticky">
                                                <div class='sticky'></div>
                                            </div>
                                            <div class="sticky">
                                                <div class='sticky'>internet connection in not available,please connect internet to show last three month Distributor sale graph</div>
                                                <img src="../images/nodata-found1.jpg" width="250" style="margin-left: 55px; margin-top: 15px" />


                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>

                    </div>

                    <!-- for primary and secondary sale graph-->

                    <div class="grid_4">
                        <!-- store graph -->
                        <div class="box box-default">
                            <div class="div3">
                                <div>
                                    <asp:Literal ID="LiteralPrimarymonthly" runat="server"></asp:Literal>

                                    <div id="chart_divprimarysalemonth" style="width: 100%; height: 190px; align-self: center; margin-top: 0px; float: right;">
                                        <div style="width: 100%;">
                                            <div class="sticky">
                                                <div class='sticky'></div>
                                            </div>
                                            <div class="sticky">
                                                <div class='sticky'>internet connection is not available,please connect internet to show Monthly primary and secondary MTD graph</div>
                                                 <img src="../images/nodata-found1.jpg" width="250" style="margin-left: 55px; margin-top: 15px" />
                                            </div>
                                        </div>
                                    </div>
                                    <div>&nbsp;</div>
                                    <div>&nbsp;</div>
                                    <asp:Literal ID="LiteralPrimaryweekly" runat="server"></asp:Literal>

                                    <div id="chart_divprimarysaleweek" style="width: 100%; height: 190px; align-self: center; margin-top: 0px; float: right;">
                                        <div style="width: 100%; height: 150px; align-self: center; margin-top: 0px; float: right;">
                                            <div class="sticky">
                                                <div class='sticky'></div>
                                            </div>
                                            <div class="sticky">
                                                <div class='sticky'>internet connection is not available,please connect internet to show weekly primary and secondary MTD graph</div>
                                                <img src="../images/nodata-found1.jpg" width="250" style="margin-left: 55px; margin-top: 15px" />


                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>

                    </div>



               
                </div>

                 <div class="grid_container" id="div5" runat="server">

                      <!-------------------------------------------asm dashboard------------------------------------------------->

                      <div class="grid_4" id="categorysale" runat="server">
                        <!-- store graph -->
                        <div class="box box-default">
                            <div class="div3">
                                <div>
                                    <asp:Literal ID="Literalcategorysaleasm" runat="server"></asp:Literal>

                                    <div id="chart_divcategorysaleasm" style="width: 100%; height: 190px; align-self: center; margin-top: 0px; float: right;">
                                        <div style="width: 100%;">
                                            <div class="sticky">
                                                <div class='sticky'></div>
                                            </div>
                                            <div class="sticky">
                                                <div class='sticky'>internet connection in not available,please connect internet to show category sale graph</div>
                                                 <img src="../images/nodata-found1.jpg" width="250" style="margin-left: 55px; margin-top: 15px" />
                                            </div>
                                        </div>
                                    </div>
                                    <div>&nbsp;</div>
                                    <div>&nbsp;</div>
                                    <asp:Literal ID="Literalmonthsaleasm" runat="server"></asp:Literal>

                                    <div id="chart_divmonthsaleasm" style="width: 100%; height: 190px; align-self: center; margin-top: 0px; float: right;">
                                        <div style="width: 100%; height: 150px; align-self: center; margin-top: 0px; float: right;">
                                            <div class="sticky">
                                                <div class='sticky'></div>
                                            </div>
                                            <div class="sticky">
                                                <div class='sticky'>internet connection in not available,please connect internet to show last three month Distributor sale graph</div>
                                                <img src="../images/nodata-found1.jpg" width="250" style="margin-left: 55px; margin-top: 15px" />


                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>

                    </div>

                    <!-- for primary and secondary sale graph-->

                    <div class="grid_4" id="primarysale" runat="server">
                        <!-- store graph -->
                        <div class="box box-default">
                            <div class="div3">
                                <div>
                                    <asp:Literal ID="Literalprimarysalemonthasm" runat="server"></asp:Literal>

                                    <div id="chart_divprimarysalemonthasm" style="width: 100%; height: 190px; align-self: center; margin-top: 0px; float: right;">
                                        <div style="width: 100%;">
                                            <div class="sticky">
                                                <div class='sticky'></div>
                                            </div>
                                            <div class="sticky">
                                                <div class='sticky'>internet connection is not available,please connect internet to show Monthly primary and secondary MTD graph</div>
                                                 <img src="../images/nodata-found1.jpg" width="250" style="margin-left: 55px; margin-top: 15px" />
                                            </div>
                                        </div>
                                    </div>
                                    <div>&nbsp;</div>
                                    <div>&nbsp;</div>
                                    <asp:Literal ID="Literalprimarysaleweekasm" runat="server"></asp:Literal>

                                    <div id="chart_divprimarysaleweekasm" style="width: 100%; height: 190px; align-self: center; margin-top: 0px; float: right;">
                                        <div style="width: 100%; height: 150px; align-self: center; margin-top: 0px; float: right;">
                                            <div class="sticky">
                                                <div class='sticky'></div>
                                            </div>
                                            <div class="sticky">
                                                <div class='sticky'>Tsi Sale Contribution</div>
                                                <img src="../images/nodata-found1.jpg" width="250" style="margin-left: 55px; margin-top: 15px" />


                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>

                    </div>

                 <div class="grid_4" id="target" runat="server">
                        <!-- store graph -->
                        <div class="box box-default">
                            <div class="div3">
                                <div>
                                    <asp:Literal ID="Literaltargatechvasm" runat="server"></asp:Literal>

                                    <div id="chart_divtargatechvasm" style="width: 100%; height: 190px; align-self: center; margin-top: 0px; float: right;">
                                        <div style="width: 100%;">
                                            <div class="sticky">
                                                <div class='sticky'></div>
                                            </div>
                                            <div class="sticky">
                                                <div class='sticky'>internet connection is not available,please connect internet to show Monthly primary and secondary MTD graph</div>
                                                 <img src="../images/nodata-found1.jpg" width="250" style="margin-left: 55px; margin-top: 15px" />
                                            </div>
                                        </div>
                                    </div>
                                    <div>&nbsp;</div>
                                    <div>&nbsp;</div>
                                    <asp:Literal ID="Literalmonthproductasm" runat="server"></asp:Literal>

                                    <div id="chart_div12monthproductasm" style="width: 100%; height: 190px; align-self: center; margin-top: 0px; float: right;">
                                        <div style="width: 100%; height: 150px; align-self: center; margin-top: 0px; float: right;">
                                            <div class="sticky">
                                                <div class='sticky'></div>
                                            </div>
                                            <div class="sticky">
                                                <div class='sticky'>internet connection is not available,please connect internet to show product wise graph</div>
                                                <img src="../images/nodata-found1.jpg" width="250" style="margin-left: 55px; margin-top: 15px" />


                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>

                    </div>
 

                <!-------------------------------------------asm dashboard------------------------------------------------->
                     </div>
                <!-- /.box -->
            </div>
        </div>
    </div>

    <div id="divPoc" runat="server" style="display:none">
       <div class="widget_content" style="padding: 10px 0px; height: 115px;">

                                                    <div style="overflow: scroll; height: 300px;" onscroll="OnScrollDiv(this)" id="DivMainContent1">
                                                        
                                                            <asp:GridView ID="grdPurchaseOrder" runat="server" Width="90%" ShowFooter="true" RowStyle-Height="24px"
                                                                AutoGenerateColumns="false" EmptyDataText="No Records Available"
                                                                CssClass="zebra">
                                                                <Columns>
                                                                       <asp:TemplateField HeaderText="SLNO">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPOID" runat="server" Text='<%#Eval("POID") %>' Width="170px"
                                                                           > </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                 <asp:TemplateField HeaderText="" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPONO" runat="server" Text='<%#Eval("PONO") %>' Width="170px"
                                                                           > </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="PONO VIEW">
                                                                    <ItemTemplate>
                                                                        <asp:Button ID="lblPONOVIEW" runat="server" Text='<%#Eval("PONO") %>' Width="170px"
                                                                          OnClick="lblPONOVIEW_Click"  > </asp:Button>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="VENDORNAME">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVENDORNAME" runat="server" Text='<%#Eval("VENDORNAME") %>' Width="170px"
                                                                           > </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="ISSTATUS">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblISSTATUS" runat="server" Text='<%#Eval("ISSTATUS") %>' Width="170px"
                                                                            > </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="CREATEDDATE">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCREATEDDATE" runat="server" Text='<%#Eval("CREATEDDATE") %>' Width="170px"
                                                                            > </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="TOTALAMOUNT">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTOTALAMOUNT" runat="server" Text='<%#Eval("TOTALAMOUNT") %>' Width="170px"
                                                                            > </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                     <asp:TemplateField HeaderText="APPROVE">
                                                                    <ItemTemplate>
                                                                        <asp:Button ID="lblApproved" runat="server" Text="Approve" Width="50px"  ToolTip="Approve"
                                                                          OnClick="lblApproved_Click"  CssClass="btn-success"   > </asp:Button>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="REJECT">
                                                                    <ItemTemplate>
                                                                        <asp:Button ID="lblReject" runat="server" Text="Reject"  Width="50px"  ToolTip="Reject"
                                                                          OnClick="lblReject_Click" CssClass="btn-warning" > </asp:Button>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                    </Columns>
                                                            </asp:GridView>
                                                      
                                                    </div>
                                                </div>

    </div>


    <span class="clear"></span>

    <script>
        function openNav() {
            document.getElementById("sidenav").style.width = "34%";
        }
        function closeNav() {
            document.getElementById("sidenav").style.width = "0";
            document.getElementById("slidebtn").style.paddingRight = "0";
        }

        function openNav2() {
            document.getElementById("sidenav2").style.width = "34%";
        }
        function closeNav2() {
            document.getElementById("sidenav2").style.width = "0";
            document.getElementById("slidebtn2").style.paddingRight = "0";
        }

           function openNav3() {
            document.getElementById("sidenav3").style.width = "34%";
        }
        function closeNav3() {
            document.getElementById("sidenav3").style.width = "0";
            document.getElementById("slidebtn3").style.paddingRight = "0";
        }

         function openNav4() {
            document.getElementById("sidenav4").style.width = "34%";
        }
        function closeNav4() {
            document.getElementById("sidenav4").style.width = "0";
            document.getElementById("slidebtn4").style.paddingRight = "0";
        }
    </script>
    <script type="text/javascript">
        function ShowProgressBar() {
            document.getElementById('dvProgressBar').style.visibility = 'visible';
        }

        function HideProgressBar() {
            document.getElementById('dvProgressBar').style.visibility = "hidden";
        }

        function ShowProgressBar2() {
            document.getElementById('dvProgressBar2').style.visibility = 'visible';
        }

        function HideProgressBar2() {
            document.getElementById('dvProgressBar2').style.visibility = "hidden";
        }

       function ShowProgressBar3() {
            document.getElementById('dvProgressBar3').style.visibility = 'visible';
        }

        function HideProgressBar3() {
            document.getElementById('dvProgressBar3').style.visibility = "hidden";
        }
          function ShowProgressBar4() {
            document.getElementById('dvProgressBar4').style.visibility = 'visible';
        }

        function HideProgressBar4() {
            document.getElementById('dvProgressBar4').style.visibility = "hidden";
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            var idleinterval = setInterval("reloadpage()", 900000);  /*15 min*/
        })

        function reloadpage() {
            location.reload();
        }
    </script>
</asp:Content>
