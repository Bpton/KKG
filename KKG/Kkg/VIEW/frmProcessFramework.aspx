<%@ Page Title="" Language="C#" MasterPageFile="~/VIEW/frmChildMaster.master" AutoEventWireup="true" CodeFile="frmProcessFramework.aspx.cs" Inherits="VIEW_frmProcessFramework" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="ProudMonkey.Common.Controls" Namespace="ProudMonkey.Common.Controls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />
    <script type="text/javascript" src="../js/bootstrap.js"></script>
    <script type="text/javascript" src="../js/bootstrap-multiselect.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../js/jquery-ui.js" type="text/javascript"></script>
    <link href="../css/jquery-ui.css" type="text/css" rel="Stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {
            BindTabFunctions();
            BindQCGridForChkboxChckd();

        });
    </script>
    <script type="text/javascript">
        $(function () {
            $('#ContentPlaceHolder1_ddlLRNo').multiselect({
                includeSelectAllOption: true
            });
        });
   
    </script>
    <style type="text/css">
        .hidden
        {
            display: none;
        }
    </style>
    <script type='text/javascript'>
        $(function () {
            $('#ContentPlaceHolder1_ddlMaterialType').multiselect({ includeSelectAllOption: true });

        });
    </script>
    <script type="text/javascript">
        function ValidateListBox(sender, args) {
            var options = document.getElementById("<%=ddlMaterialType.ClientID%>").options;
            for (var i = 0; i < options.length; i++) {
                if (options[i].selected == true) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $('#ContentPlaceHolder1_ddlMaterialType').multiselect({
                includeSelectAllOption: true
            });
            $("#ContentPlaceHolder1_ddlMaterialType").multiselect('selectAll', false);
            $("#ContentPlaceHolder1_ddlMaterialType").multiselect('updateButtonText');
        });         
    </script>
    <style type="text/css">
        .header
        {
            font-weight: bold;
            position: absolute;
            background-color: White;
        }
    </style>
    <script type="text/javascript">
        function BindTabFunctions() {

            var tabValue = $("#" + '<%= hdfTabID.ClientID %>').val();
            var subTabValue = $("#" + '<%= hdfSubTabID.ClientID %>').val();
            if (tabValue == "") {
                $(".tabBox .content").hide();
                $(".tabBox .tabs li:first").addClass("selected");
                $("#tab1").show();

                $(".tabBoxinner .content").hide();
                $(".tabBoxinner .tabs li:first").addClass("selected");
                $("#tab5").show();
            }

            else {

                if (tabValue == 1) {
                    $(".tabBox .content").hide();
                    $(".tabBox .tabs li:first").addClass("selected");
                    $("#tab1").show();

                    if (subTabValue == 1) {

                        $(".tabBoxinner .content").hide();
                        $(".tabBoxinner .tabs li:first").addClass("selected");
                        $("#tab5").show();
                        $("#" + '<%= hdfSubTabID.ClientID %>').val("");
                    }
                    $("#" + '<%= hdfTabID.ClientID %>').val("");
                }

                else if (tabValue == 2) {
                    $(".tabBox .content").hide();
                    $("ul.tabs li:nth-child(2)").addClass("selected");
                    $("#tab2").show();
                    $("#" + '<%= hdfTabID.ClientID %>').val("");
                }

                else if (tabValue == 3) {
                    $(".tabBox .content").hide();
                    $("ul.tabs li:nth-child(3)").addClass("selected");
                    $("#tab3").show();
                    $("#" + '<%= hdfTabID.ClientID %>').val("");
                }

                else if (tabValue == 4) {
                    $(".tabBox .content").hide();
                    $("ul.tabs li:nth-child(4)").addClass("selected");
                    $("#tab4").show();
                    $("#" + '<%= hdfTabID.ClientID %>').val("");
                }

                else {
                    $(".tabBox .content").hide();
                    $("ul.tabs li:nth-child(1)").addClass("selected");
                    $("#tab1").show();
                    $("#" + '<%= hdfTabID.ClientID %>').val("");
                }
            }

            $(".tabBox .tabs li").click(function () {
                $(".tabBox .tabs li").removeClass("selected");
                $(this).addClass("selected");
                $(".tabBox .content").hide();

                var activeTab = $(this).find("a").attr("href");
                $(activeTab).show();
                if ($(activeTab).selector == "#tab6") {
                    $(".tabBoxinner .content").hide();
                    $("ul.tabs li:nth-child(6)").addClass("selected");
                    $("#tab6").show();
                }

                else {
                    $(".tabBoxinner .content").hide();
                    $(".tabBoxinner .tabs li:first").addClass("selected");
                    $("#tab5").show();
                }
                return false;
            });

            $(".tabBoxinner .tabs li").click(function () {
                var activeTab = $(this).find("a").attr("href");
                if ($(activeTab).selector == "#tab6") {
                    $(".tabBox .content").hide();
                    $(".tabBox .tabs li:first").addClass("selected");
                    $("#tab1").show();
                    $("ul.tabs li:nth-child(5)").removeClass("selected");
                    $(".tabBoxinner .content").hide();
                    $("ul.tabs li:nth-child(6)").addClass("selected");
                    $("#tab6").show();
                }

                else {
                    $(".tabBox .content").hide();
                    $(".tabBox .tabs li:first").addClass("selected");
                    $("#tab1").show();
                    $("ul.tabs li:nth-child(6)").removeClass("selected");
                    $(".tabBoxinner .content").hide();
                    $("ul.tabs li:nth-child(5)").addClass("selected");
                    $("#tab5").show();
                }
                return false;
            });
        };

        function BindResourceGridForChkboxChckd() {
            $("#<%=gvResourceSource.ClientID%> input[id*='chkRow']:checkbox").click(function () {
                //Get number of checkboxes in list either checked or not checked
                var totalCheckboxes = $("#<%=gvResourceSource.ClientID%> input[id*='chkRow']:checkbox").size();
                //Get number of checked checkboxes in list
                var checkedCheckboxes = $("#<%=gvResourceSource.ClientID%> input[id*='chkRow']:checkbox:checked").size();
                //Check / Uncheck top checkbox if all the checked boxes in list are checked
                $("#<%=gvResourceSource.ClientID%> input[id*='chkHeader']:checkbox").attr('checked', totalCheckboxes == checkedCheckboxes);
            });

            $("#<%=gvResourceSource.ClientID%> input[id*='chkHeader']:checkbox").click(function () {
                //Check/uncheck all checkboxes in list according to main checkbox 
                $("#<%=gvResourceSource.ClientID%> input[id*='chkRow']:checkbox").attr('checked', $(this).is(':checked'));
            });
        }

        function BindQCGridForChkboxChckd() {
            $("#<%=gvQCSource.ClientID%> input[id*='chkRow']:checkbox").click(function () {
                //Get number of checkboxes in list either checked or not checked
                var totalCheckboxes = $("#<%=gvQCSource.ClientID%> input[id*='chkRow']:checkbox").size();
                //Get number of checked checkboxes in list
                var checkedCheckboxes = $("#<%=gvQCSource.ClientID%> input[id*='chkRow']:checkbox:checked").size();
                //Check / Uncheck top checkbox if all the checked boxes in list are checked
                $("#<%=gvQCSource.ClientID%> input[id*='chkHeader']:checkbox").attr('checked', totalCheckboxes == checkedCheckboxes);
            });

            $("#<%=gvQCSource.ClientID%> input[id*='chkHeader']:checkbox").click(function () {
                //Check/uncheck all checkboxes in list according to main checkbox 
                $("#<%=gvQCSource.ClientID%> input[id*='chkRow']:checkbox").attr('checked', $(this).is(':checked'));
            });
        }
		
    </script>
    <!--End for tab-->
    <!--Start for dragdrop grid to grid and store process data to datatable-->
    <!--<script type="text/javascript">
	    $(function () {
	        $(".drag_drop_grid").sortable({
	            items: 'tr:not(tr:first-child)',
	            cursor: 'crosshair',
	            connectWith: '.drag_drop_grid',
	            axis: 'y,x',
	            dropOnEmpty: true,
	            receive: function (e, ui) {
	                $(this).find("tbody").append(ui.item);
	                var product = {};
	                product.ProcessID = $("[id*=gvDest] tr:last").find("td:nth-child(1)").html();
	                product.ProcessCode = $("[id*=gvDest] tr:last").find("td:nth-child(2)").html();
	                product.ProcessName = $("[id*=gvDest] tr:last").find("td:nth-child(3)").html();
	                product.ProcessTotalDuration = $("[id*=gvDest] tr:last").find("td:nth-child(4)").html();

	                $.ajax({
	                    type: "POST",
	                    url: "frmProcessFramework.aspx/SaveProduct",
	                    data: '{product: ' + JSON.stringify(product) + '}',
	                    contentType: "application/json; charset=utf-8",
	                    dataType: "json",
	                    success: function (response) {
	                    }
	                });	                
	                return false;
	            }
	        });
	    });

	    function BindDragDropGrid() {
	        $(".drag_drop_grid").sortable({

	            items: 'tr:not(tr:first-child)',
	            cursor: 'crosshair',
	            connectWith: '.drag_drop_grid',
	            axis: 'y,x',
	            dropOnEmpty: true,
	            receive: function (e, ui) {
	                $(this).find("tbody").append(ui.item);
	                var product = {};
	                product.ProcessID = $("[id*=gvDest] tr:last").find("td:nth-child(1)").html();
	                product.ProcessCode = $("[id*=gvDest] tr:last").find("td:nth-child(2)").html();
	                product.ProcessName = $("[id*=gvDest] tr:last").find("td:nth-child(3)").html();
	                product.ProcessTotalDuration = $("[id*=gvDest] tr:last").find("td:nth-child(4)").html();

	                $.ajax({
	                    type: "POST",
	                    url: "frmProcessFramework.aspx/SaveProduct",
	                    data: '{product: ' + JSON.stringify(product) + '}',
	                    contentType: "application/json; charset=utf-8",
	                    dataType: "json",
	                    success: function (response) {
	                        $("#GridView1").append("<tr><td>" + product.ProcessID +
									"</td><td>" + product.ProcessCode +
									"</td><td>" + product.ProcessName + "</td></tr>");
	                    }

	                });
	                return false;
	            }
	        });
	    };
	</script>-->
    <!--End for dragdrop grid to grid and store process data to datatable-->
    <style type="text/css">
        /*Start css for both grid*/
        .GridSrc td
        {
            background-color: #A1DCF2;
            color: black;
            font-size: 10pt;
            font-family: Arial;
            line-height: 200%;
            cursor: pointer;
            width: 100px;
        }
        .GridSrc th
        {
            background-color: #3AC0F2;
            color: White;
            font-family: Arial;
            font-size: 10pt;
            line-height: 200%;
            width: 200px;
        }
        .GridDest td
        {
            background-color: #eee !important;
            color: black;
            font-family: Arial;
            font-size: 10pt;
            line-height: 200%;
            cursor: pointer;
            width: 100px;
        }
        .GridDest th
        {
            background-color: #6C6C6C !important;
            color: White;
            font-family: Arial;
            font-size: 10pt;
            line-height: 200%;
            width: 200px;
        }
        /*End css for both grid*/
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=40);
            opacity: 0.4;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            width: 1000px;
            height: 510px;
            border: 3px solid #0DA9D0;
        }
        .modalPopup .header
        {
            background-color: #2FBDF1;
            height: 30px;
            color: White;
            line-height: 30px;
            text-align: center;
            font-weight: bold;
        }
        .modalPopup .body
        {
            min-height: 50px;
            line-height: 30px;
            text-align: center;
            padding: 5px;
        }
        .modalPopup .footer
        {
            padding: 3px;
            padding-left: 50px;
        }
        .modalPopup .button
        {
            height: 23px;
            color: White;
            line-height: 23px;
            text-align: center;
            font-weight: bold;
            cursor: pointer;
            background-color: #9F9F9F;
            border: 1px solid #5C5C5C;
        }
        
        .close-btn
        {
            border: none;
            position: static;
            padding: 1px 5px;
            top: -20px;
            left: 198px;
        }
        
        .close-btn a
        {
            font-size: 15px;
            font-weight: bold;
            color: white;
            text-decoration: none;
        }
        .modalPopup td
        {
            text-align: left;
        }        
        
        .ajax__tab_xp .ajax__tab_header .ajax__tab_tab
        {
            width: 50px !important;
            padding: 4px;
            margin: 0px;
            overflow: hidden;
            margin-bottom: -1px;
            height: 2.50em !important;
        }
        .tabBox .tabs
        {
            margin: 0;
            padding: 0;
            overflow: hidden;
            margin-bottom: -1px;
            height: 2.50em;
        }
        
        .tabBox .tabs li
        {
            float: left;
            list-style: none;
            margin: 0;
            padding: .25em .25em 0;
            overflow: hidden;
            position: relative;
            z-index: 1;
            border-bottom: 1px solid #FFF;
        }
        
        .tabBox .tabs li.selected
        {
            z-index: 3;
        }
        
        .tabBox .tabs a
        {
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
        
        .tabBox .tabs .selected a
        {
            background: #FFF;
        }
        
        .tabBox .tabs a:hover
        {
            background: #FFF;
        }
        
        .tabBox .tabs a:active
        {
            background: #FFF;
        }
        
        .tabBox .content
        {
            display: none;
            clear: left;
            position: relative;
            height: 350px;
            z-index: 2;
            padding: 2em 1em;
            border: 1px solid #BDBDBD;
            background: #FFF; /*overflow-y:scroll;*/
        }
        
        .tabBoxinner .tabs
        {
            margin: 0;
            padding: 0;
            overflow: hidden;
            margin-bottom: -1px;
            height: 2.50em;
        }
        
        .tabBoxinner .tabs li
        {
            float: left;
            list-style: none;
            margin: 0;
            padding: .25em .25em 0;
            overflow: hidden;
            position: relative;
            z-index: 2;
            border-bottom: 1px solid #FFF;
        }
        
        .tabBoxinner .tabs li.selected
        {
            z-index: 7;
        }
        
        .tabBoxinner .tabs a
        {
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
        
        .tabBoxinner .tabs .selected a
        {
            background: #FFF;
        }
        
        .tabBoxinner .tabs a:hover
        {
            background: #FFF;
        }
        
        .tabBoxinner .tabs a:active
        {
            background: #FFF;
        }
        
        .tabBoxinner .content
        {
            display: none;
            clear: left;
            position: relative;
            height: 300px;
            z-index: 2;
            padding: 2em 1em;
            border: 1px solid #BDBDBD;
            background: #FFF; /*overflow-y: scroll;*/
        }
        .selected
        {
            background-color: #A1DCF2;
        }
    </style>
    <script type="text/javascript">
        var close = $('#close');
        close.click(function () {
            $("#pnlPopup").hide();
        });
    </script>
    <script language="javascript" type="text/javascript">
        function MakeStaticHeaderWorkStationSource(gridId, height, width, headerHeight, isFooter) {
            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRowWorkStationSource');
                var DivMC = document.getElementById('DivMainContentWorkStationSource');
                var DivFR = document.getElementById('DivFooterRowWorkStationSource');

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

        function OnScrollDivWorkstationSource(Scrollablediv) {
            document.getElementById('DivHeaderRowWorkStationSource').scrollLeft = Scrollablediv.scrollLeft;
            document.getElementById('DivFooterRowWorkStationSource').scrollLeft = Scrollablediv.scrollLeft;
        }

        function MakeStaticHeaderWorkStationDestination(gridId, height, width, headerHeight, isFooter) {
            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRowWorkStationDestination');
                var DivMC = document.getElementById('DivMainContentWorkStationDestination');
                var DivFR = document.getElementById('DivFooterRowWorkStationDestination');

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

        function OnScrollDivWorkstationDestination(Scrollablediv) {
            document.getElementById('DivHeaderRowWorkStationDestination').scrollLeft = Scrollablediv.scrollLeft;
            document.getElementById('DivFooterRowWorkStationDestination').scrollLeft = Scrollablediv.scrollLeft;
        }

        function MakeStaticHeaderResourceSource(gridId, height, width, headerHeight, isFooter) {
            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRowResourceSource');
                var DivMC = document.getElementById('DivMainContentResourceSource');
                var DivFR = document.getElementById('DivFooterRowResourceSource');

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

        function OnScrollDivResourceSource(Scrollablediv) {
            document.getElementById('DivHeaderRowResourceSource').scrollLeft = Scrollablediv.scrollLeft;
            document.getElementById('DivFooterRowResourceSource').scrollLeft = Scrollablediv.scrollLeft;
        }

        function MakeStaticHeaderResourceDestination(gridId, height, width, headerHeight, isFooter) {
            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRowResourceDestination');
                var DivMC = document.getElementById('DivMainContentResourceDestination');
                var DivFR = document.getElementById('DivFooterRowResourceDestination');

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

        function OnScrollDivResourceDestination(Scrollablediv) {
            document.getElementById('DivHeaderRowResourceDestination').scrollLeft = Scrollablediv.scrollLeft;
            document.getElementById('DivFooterRowResourceDestination').scrollLeft = Scrollablediv.scrollLeft;
        }

        function MakeStaticHeaderQCSource(gridId, height, width, headerHeight, isFooter) {
            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRowQCSource');
                var DivMC = document.getElementById('DivMainContentQCSource');
                var DivFR = document.getElementById('DivFooterRowQCSource');

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

        function OnScrollDivQCSource(Scrollablediv) {
            document.getElementById('DivHeaderRowQCSource').scrollLeft = Scrollablediv.scrollLeft;
            document.getElementById('DivFooterRowQCSource').scrollLeft = Scrollablediv.scrollLeft;
        }

        function MakeStaticHeaderQCDestination(gridId, height, width, headerHeight, isFooter) {
            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRowQCDestination');
                var DivMC = document.getElementById('DivMainContentQCDestination');
                var DivFR = document.getElementById('DivFooterRowQCDestination');

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

        function OnScrollDivQCDestination(Scrollablediv) {
            document.getElementById('DivHeaderRowQCDestination').scrollLeft = Scrollablediv.scrollLeft;
            document.getElementById('DivFooterRowQCDestination').scrollLeft = Scrollablediv.scrollLeft;
        }

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

        function MakeStaticHeaderBOMDestination(gridId, height, width, headerHeight, isFooter) {
            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRowBOMDestination');
                var DivMC = document.getElementById('DivMainContentBOMDestination');
                var DivFR = document.getElementById('DivFooterRowBOMDestination');

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

        function OnScrollDivBOMDestination(Scrollablediv) {
            document.getElementById('DivHeaderRowBOMDestination').scrollLeft = Scrollablediv.scrollLeft;
            document.getElementById('DivFooterRowBOMDestination').scrollLeft = Scrollablediv.scrollLeft;
        }

        function MakeStaticHeaderOutputDestination(gridId, height, width, headerHeight, isFooter) {
            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRowOutputDestination');
                var DivMC = document.getElementById('DivMainContentOutputDestination');
                var DivFR = document.getElementById('DivFooterRowOutputDestination');

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

        function OnScrollDivOutputDestination(Scrollablediv) {
            document.getElementById('DivHeaderRowOutputDestination').scrollLeft = Scrollablediv.scrollLeft;
            document.getElementById('DivFooterRowOutputDestination').scrollLeft = Scrollablediv.scrollLeft;
        }

        function MakeStaticHeaderOutputSource(gridId, height, width, headerHeight, isFooter) {
            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRowOutputSource');
                var DivMC = document.getElementById('DivMainContentOutputSource');
                var DivFR = document.getElementById('DivFooterRowOutputSource');

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

        function OnScrollDivOutputSource(Scrollablediv) {
            document.getElementById('DivHeaderRowOutputSource').scrollLeft = Scrollablediv.scrollLeft;
            document.getElementById('DivFooterRowOutputSource').scrollLeft = Scrollablediv.scrollLeft;
        }

    </script>
    <%--========================New==================================--%>
    <script language="javascript" type="text/javascript">
        function CreateGridHeader(DataDiv, GridView1, HeaderDiv) {
            var DataDivObj = document.getElementById(DataDiv);
            var DataGridObj = document.getElementById(GridView1);
            var HeaderDivObj = document.getElementById(HeaderDiv);

            //********* Creating new table which contains the header row ***********
            var HeadertableObj = HeaderDivObj.appendChild(document.createElement('table'));

            DataDivObj.style.paddingTop = '0px';
            var DataDivWidth = DataDivObj.clientWidth;
            DataDivObj.style.width = '5000px';

            //********** Setting the style of Header Div as per the Data Div ************
            HeaderDivObj.className = DataDivObj.className;
            HeaderDivObj.style.cssText = DataDivObj.style.cssText;
            //**** Making the Header Div scrollable. *****
            HeaderDivObj.style.overflow = 'auto';
            //*** Hiding the horizontal scroll bar of Header Div ****
            HeaderDivObj.style.overflowX = 'hidden';
            //**** Hiding the vertical scroll bar of Header Div **** 
            HeaderDivObj.style.overflowY = 'hidden';
            HeaderDivObj.style.height = DataGridObj.rows[0].clientHeight + 'px';
            //**** Removing any border between Header Div and Data Div ****
            HeaderDivObj.style.borderBottomWidth = '0px';

            //********** Setting the style of Header Table as per the GridView ************
            HeadertableObj.className = DataGridObj.className;
            //**** Setting the Headertable css text as per the GridView css text 
            HeadertableObj.style.cssText = DataGridObj.style.cssText;
            HeadertableObj.border = '1px';
            HeadertableObj.rules = 'all';
            HeadertableObj.cellPadding = DataGridObj.cellPadding;
            HeadertableObj.cellSpacing = DataGridObj.cellSpacing;

            //********** Creating the new header row **********
            var Row = HeadertableObj.insertRow(0);
            Row.className = DataGridObj.rows[0].className;
            Row.style.cssText = DataGridObj.rows[0].style.cssText;
            Row.style.fontWeight = 'bold';

            //******** This loop will create each header cell *********
            for (var iCntr = 0; iCntr < DataGridObj.rows[0].cells.length; iCntr++) {
                var spanTag = Row.appendChild(document.createElement('td'));
                spanTag.innerHTML = DataGridObj.rows[0].cells[iCntr].innerHTML;
                var width = 0;
                //****** Setting the width of Header Cell **********
                if (spanTag.clientWidth > DataGridObj.rows[1].cells[iCntr].clientWidth) {
                    width = spanTag.clientWidth;
                }
                else {
                    width = DataGridObj.rows[1].cells[iCntr].clientWidth;
                }
                if (iCntr <= DataGridObj.rows[0].cells.length - 2) {
                    spanTag.style.width = width + 'px';
                }
                else {
                    spanTag.style.width = width + 20 + 'px';
                }
                DataGridObj.rows[1].cells[iCntr].style.width = width + 'px';
            }
            var tableWidth = DataGridObj.clientWidth;
            //********* Hidding the original header of GridView *******
            DataGridObj.rows[0].style.display = 'none';
            //********* Setting the same width of all the componets **********
            HeaderDivObj.style.width = DataDivWidth + 'px';
            DataDivObj.style.width = DataDivWidth + 'px';
            DataGridObj.style.width = tableWidth + 'px';
            HeadertableObj.style.width = tableWidth + 20 + 'px';
            return false;
        }

        function Onscrollfnction() {
            var div = document.getElementById('DataDiv');
            var div2 = document.getElementById('HeaderDiv');
            //****** Scrolling HeaderDiv along with DataDiv ******
            div2.scrollLeft = div.scrollLeft;
            return false;
        }
        function isNumberKeyWithDot(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((charCode < 48 || charCode > 57) && (charCode != 8) && (charCode != 46))
                return false;

            return true;
        } /*new add by p.basu on 03032020*/
	
    </script>
    <%--========================New==================================--%>
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
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:Image ID="Image1" ImageUrl="~/images/loading123.gif" AlternateText="Processing"
                runat="server" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <!--modalPopup--->
    <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="contentarea">
                <div class="grid_container">
                    <div class="grid_12">
                        <div class="widget_wrap">
                            <div class="widget_top active" id="trBtn" runat="server">
                                <span class="h_icon_he list_images"></span>
                                <h6>
                                    Process Framework</h6>
                                <div class="btn_30_light" style="float: right;" id="btnaddhide" runat="server">
                                    <span class="icon add_co"></span>
                                    <asp:Button ID="btnAddProcessFramework" runat="server" Text="Add New Record" CssClass="btn_link"
                                        CausesValidation="false" OnClick="btnAddProcessFramework_Click" /></div>
                            </div>
                            <div class="widget_content">
                                <asp:Panel ID="pnlAdd" runat="server">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" class="form_container_td">
                                        <tr>
                                            <td class="field_input" style="padding-left: 10px;">
                                                <fieldset>
                                                    <legend>Process Framework Details</legend>
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td width="120" class="field_title">
                                                                <asp:Label ID="lblProcessFrameworkCode" Text="Code" runat="server"></asp:Label><span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtProcessFrameworkCode" runat="server" CssClass="mid" MaxLength="20"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="PVProcessCode" runat="server" Display="None" ErrorMessage="Process Framework Code is required!"
                                                                    ControlToValidate="txtProcessFrameworkCode" ValidateEmptyText="false" OnServerValidate="PVProcessCode_ServerValidate"
                                                                    SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                                                    TargetControlID="PVProcessCode" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="~/images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">
                                                                <asp:Label ID="lblProcessFrameworkName" Text="Name" runat="server"></asp:Label>
                                                                <span class="req">*</span>
                                                                <asp:HiddenField ID="hdfProcessID" runat="server" />
                                                                <asp:HiddenField ID="hdfTabID" runat="server" />
                                                                <asp:HiddenField ID="hdfSubTabID" runat="server" />
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtProcessFrameworkName" runat="server" CssClass="mid" MaxLength="50"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="PVProcessName" runat="server" Display="None" ErrorMessage="Process Name is required!"
                                                                    ControlToValidate="txtProcessFrameworkName" ValidateEmptyText="false" OnServerValidate="PVProcessName_ServerValidate"
                                                                    SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                                    TargetControlID="PVProcessName" PopupPosition="Right" HighlightCssClass="errormassage"
                                                                    WarningIconImageUrl="~/images/050.png">
                                                                </ajaxToolkit:ValidatorCalloutExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">
                                                                <asp:Label ID="lblProcessFrameworkDesc" Text="Description" runat="server"></asp:Label><span
                                                                    class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:TextBox ID="txtFrameworkDescription" runat="server" CssClass="mid" onfocus="if(this.value==''){this.value=''}"
                                                                    MaxLength="50"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label5" Text="Product" runat="server"></asp:Label><span class="req">*</span>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:DropDownList ID="ddlProduct" runat="server" CssClass="chosen-select" Width="330px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="field_title">
                                                                <asp:Label ID="Label1" Text="Active" runat="server"></asp:Label>
                                                            </td>
                                                            <td class="field_input">
                                                                <asp:CheckBox ID="chkActive" runat="server" Text=" " />
                                                            </td>
                                                        </tr>
                                                        <tr align="center">
                                                            <td colspan="2">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:GridView ID="gvSource" runat="server" CssClass="drag_drop_grid zebra" AutoGenerateColumns="false"
                                                                                Width="400px" OnSelectedIndexChanged="OnSelectedIndexChanged">
                                                                                <Columns>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkRow" runat="server" Text=" " />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblProcessID" runat="server" Text='<%# Eval("ProcessID") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Process Code" HeaderStyle-Width="199px">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblProcessCode" runat="server" Text='<%# Eval("ProcessCode") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Process Name" HeaderStyle-Width="200px">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblProcessName" runat="server" Text='<%# Eval("ProcessName") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                            <asp:GridView ID="GridView1" ShowHeaderWhenEmpty="true" runat="server">
                                                                            </asp:GridView>
                                                                        </td>
                                                                        <td>
                                                                            <asp:ImageButton ID="imgAddProcess" runat="server" ImageUrl="~/images/arrowhr.png"
                                                                                OnClick="imgAddProcess_Click" CausesValidation="false" />
                                                                        </td>
                                                                        <td>
                                                                            &nbsp;
                                                                        </td>
                                                                        <td>
                                                                            <div class="btn_30_light" style="display: none">
                                                                                <span class="icon arrow_refresh_co">
                                                                                    <asp:Button ID="btnRefresh" runat="server" CssClass="submit" OnClick="btnRefresh_Click"
                                                                                        CausesValidation="false" Enabled="true" />
                                                                                </span>
                                                                            </div>
                                                                            <asp:HiddenField ID="hdnProcessID" runat="server" />
                                                                            <asp:GridView ID="gvDest" runat="server" CssClass="drag_drop_grid zebra" AutoGenerateColumns="false"
                                                                                Width="500px" ShowHeaderWhenEmpty="true" OnRowCommand="gvDest_RowCommand">
                                                                                <Columns>
                                                                                    <%-- <asp:TemplateField Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblProcessID" runat="server" Text='<%# Eval("ProcessID") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>--%>
                                                                                    <asp:BoundField DataField="ProcessID" HeaderText="Process ID">
                                                                                        <ItemStyle CssClass="hidden" />
                                                                                        <HeaderStyle CssClass="hidden" />
                                                                                    </asp:BoundField>
                                                                                    <%-- <asp:TemplateField HeaderText="Process Code" HeaderStyle-Width="200px" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblProcessCode" runat="server" Text='<%# Eval("ProcessCode") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Process Name" HeaderStyle-Width="200px" HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblProcessName" runat="server" Text='<%# Eval("ProcessName") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>--%>
                                                                                    <asp:BoundField DataField="ProcessCode" HeaderText="Process Code" />
                                                                                    <asp:BoundField DataField="ProcessName" HeaderText="Process Name" />
                                                                                    <asp:TemplateField HeaderText="Total Duration In Hour" HeaderStyle-Width="199px"
                                                                                        HeaderStyle-HorizontalAlign="Left">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtTotalDuration" runat="server" onkeypress="return isNumberKeyWithDot(event);" MaxLength="6" Width="75%" Text='<%#Eval("ProcessTotalDuration")%>'></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:Button runat="server" ID="imgbPopupOpen" CommandName="ProcessWiseWindowOpen"
                                                                                                CausesValidation="true" ToolTip="PopupOpen" CommandArgument='<%#Eval("ProcessID")+","+Eval("ProcessName") %>'
                                                                                                CssClass="h_icon page_paste_co" />
                                                                                            <%--<asp:ImageButton ID="imgbPopupOpen" runat="server" CssClass="h_icon page_paste_co"
																				CommandName="ProcessWiseWindowOpen" CausesValidation="true"
																				CommandArgument='<%#Eval("ProcessID")+","+Eval("ProcessName") %>' />--%>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="imgProcessSourceDelete" runat="server" ImageUrl="~/images/delete.jpg"
                                                                                                ToolTip="Delete" Height="20px" Width="20px" CausesValidation="false" CommandName="ProcessSourceDelete"
                                                                                                CommandArgument='<%# Eval("ProcessID")+","+Eval("ProcessName") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                            <p style="display: none">
                                                                                <font size="8" color="red"><b>*</b></font>Drag and Drop the Process</p>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td style="padding: 8px 0px;">
                                                                <div class="btn_24_blue">
                                                                    <span class="icon disk_co"></span>
                                                                    <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn_link" OnClick="btnSubmit_Click" />
                                                                </div>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                                <div class="btn_24_blue">
                                                                    <span class="icon cross_octagon_co"></span>
                                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn_link" CausesValidation="false"
                                                                        OnClick="btnCancel_Click" />
                                                                </div>
                                                                <asp:HiddenField ID="hdnfProcessFrameworkID" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlDisplay" runat="server">
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
                                        <cc1:Grid ID="gvProcessFramework" runat="server" CallbackMode="true" Serialize="true"
                                            FolderStyle="../GridStyles/premiere_blue" AutoGenerateColumns="false" AllowPageSizeSelection="false"
                                            OnDeleteCommand="DeleteRecord" AllowAddingRecords="false" AllowFiltering="true">
                                            <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" OnClientDelete="OnDelete" />
                                            <FilteringSettings InitialState="Visible" />
                                            <Columns>
                                                <cc1:Column ID="Column1" DataField="ProcessFrameworkID" ReadOnly="true" HeaderText="Process Framework ID"
                                                    runat="server" Visible="false" />
                                                <cc1:Column ID="Column2" DataField="ProcessCode" HeaderText="Process Framework Code"
                                                    runat="server" Width="240">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column3" DataField="ProcessName" HeaderText="Process Framework Name"
                                                    runat="server" Width="240">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column4" DataField="ProcessFrameworkDescription" HeaderText="Description"
                                                    runat="server" Width="280">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="EqualTo" />
                                                        <cc1:FilterOption Type="Contains" />
                                                        <cc1:FilterOption Type="DoesNotContain" />
                                                        <cc1:FilterOption Type="StartsWith" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column5" DataField="Active" HeaderText="Active" SortOrder="Asc" runat="server"
                                                    Width="120">
                                                    <FilterOptions>
                                                        <cc1:FilterOption Type="NoFilter" />
                                                        <cc1:FilterOption Type="Contains" />
                                                    </FilterOptions>
                                                </cc1:Column>
                                                <cc1:Column ID="Column8" AllowEdit="true" AllowDelete="true" HeaderText="Edit" runat="server"
                                                    Width="80">
                                                    <TemplateSettings TemplateId="editBtnTemplate" />
                                                </cc1:Column>
                                                <cc1:Column HeaderText="Delete" AllowEdit="false" AllowDelete="true" Width="80">
                                                    <TemplateSettings TemplateId="deleteBtnTemplate" />
                                                </cc1:Column>
                                            </Columns>
                                            <FilteringSettings MatchingType="AnyFilter" />
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="editBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-edit" title="Edit" id="btnGridEdit_<%# Container.PageRecordIndex %>"
                                                            onclick="CallServerMethod(this)"></a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <Templates>
                                                <cc1:GridTemplate runat="server" ID="deleteBtnTemplate">
                                                    <Template>
                                                        <a href="javascript: //" class="action-icons c-delete" title="Delete" onclick="gvProcessFramework.delete_record(this)">
                                                        </a>
                                                    </Template>
                                                </cc1:GridTemplate>
                                            </Templates>
                                            <ScrollingSettings ScrollWidth="100%" ScrollHeight="290" />
                                        </cc1:Grid>
                                        <asp:Button ID="btngridedit" runat="server" Text="GridSave" Style="display: none"
                                            OnClick="btngridedit_Click" CausesValidation="false" />
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <%--<div class="header">
						Process Sequence Diagram <a href="#" id="close" class="close-btn">X</a>
					</div>--%>
                    <div id="light" class="white_content" runat="server">
                        <div id="Div1" class="widget_top" runat="server">
                            <span class="h_icon_he list_images"></span>
                            <h6>
                                Process Sequence Diagram</h6>
                        </div>
                        <div class="tabBox">
                            <ul class="tabs">
                                <li><a href="#tab1">Material</a></li>
                                <li><a href="#tab2">Workstation</a></li>
                                <li><a href="#tab3">Resource</a></li>
                                <li><a href="#tab4">QC</a></li>
                            </ul>
                            <div id="tab1" class="content">
                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <%-- <td width="25%">
																						<asp:Label ID="Label2" runat="server" Text="Material Type" Font-Bold="true" Font-Size="Large"></asp:Label>
																					</td>--%>
                                        <%--<td width="25%" style="padding-bottom: 10px;">
																						<asp:ListBox ID="ddlMaterialType" runat="server" SelectionMode="Multiple" data-placeholder="Choose Material Type"
																							CssClass="chosen-select" AutoPostBack="true" OnSelectedIndexChanged="ddlMaterialType_SelectedIndexChanged"></asp:ListBox>
																					</td>--%>
                                        <%--  ===================================--%>
                                        <td id="Td1" width="25%" runat="server" visible="false">
                                            <asp:Label ID="Label3" runat="server" Text="BOM Sequence" Font-Bold="true" Font-Size="Large"></asp:Label>
                                        </td>
                                        <td id="Td2" width="25%" runat="server" visible="false">
                                            <asp:TextBox ID="txtBOMSequence" runat="server" Width="75px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <div class="tabBoxinner">
                                    <ul class="tabs">
                                        <li><a href="#tab5">BOM</a></li>
                                        <li><a href="#tab6">FG & SFG</a></li>
                                    </ul>
                                    <div id="tab5" class="content">
                                        <%--===================--%>
                                        <table>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="lblProcessMaterialBOM" runat="server" Font-Bold="true" Font-Size="Small"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <asp:Label ID="Label2" runat="server" Text="Input Material List" Font-Bold="true"
                                                        Font-Size="Large"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr id="Tr1" runat="server" visible="false">
                                                <td class="field_title">
                                                    <asp:Label ID="Label10" runat="server" Text="Material Type"></asp:Label>
                                                </td>
                                                <td class="field_input">
                                                    <asp:ListBox ID="ddlMaterialType" runat="server" SelectionMode="Multiple" ValidationGroup="ADD"
                                                        AppendDataBoundItems="True" name="options[]" multiple="multiple"></asp:ListBox>
                                                    <asp:CustomValidator ID="CustomValidator2" runat="server" ValidateEmptyText="true"
                                                        ControlToValidate="ddlMaterialType" ValidationGroup="ADD" ErrorMessage="Required!"
                                                        Display="Dynamic" ClientValidationFunction="ValidateListBox" ForeColor="Red"></asp:CustomValidator>
                                                </td>
                                                <td width="75" style="display: none">
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div style="overflow: hidden; width: 100%;" id="DivHeaderRowBOMSource">
                                                    </div>
                                                    <div style="overflow: scroll; border: 1px solid #c3c9ce; margin-bottom: 8px; height: 260px;"
                                                        onscroll="OnScrollDivBOMSource(this)" id="DivMainContentBOMSource">
                                                        <asp:GridView ID="gvSourceMaterialInput" runat="server" AutoGenerateColumns="False"
                                                            CssClass="zebra" Width="400px" EmptyDataText="No Records Available" EmptyDataRowStyle-Font-Bold="true"
                                                            EmptyDataRowStyle-VerticalAlign="Middle" OnRowDataBound="gvSourceMaterialInput_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMaterialInputID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Code" HeaderStyle-Width="100px" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMaterialInputCode" runat="server" Text='<%# Eval("CODE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Name" HeaderStyle-Width="100px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbllMaterialInputName" runat="server" Text='<%# Eval("NAME") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Type" HeaderStyle-Width="100px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbllMaterialInputType" runat="server" Text='<%# Eval("TYPE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Unit" HeaderStyle-Width="100px">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="ddlUnit" runat="server">
                                                                        </asp:DropDownList>
                                                                        <%--      <asp:Label ID="lbllMaterialInputUnit" runat="server" Text='<%# Eval("UNITVALUE") %>'></asp:Label>--%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtQty" onkeypress="return isNumberKeyWithDot(event);" MaxLength="6" runat="server" Width="50px" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Ref Qty" HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtRefQty" onkeypress="return isNumberKeyWithDot(event);" MaxLength="6" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                    <div id="DivFooterRowBOMSource" style="overflow: hidden">
                                                    </div>
                                                </td>
                                                <td>
                                                    <td>
                                                        <asp:ImageButton ID="imgbAddMaterialInput" runat="server" ImageUrl="~/images/arrowhr.png"
                                                            OnClick="imgbAddMaterialInput_Click" CausesValidation="false" />
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td valign="top">
                                                        <div style="overflow: hidden; width: 100%;" id="DivHeaderRowBOMDestination">
                                                        </div>
                                                        <div style="overflow: scroll; border: 1px solid #c3c9ce; margin-bottom: 8px; height: 260px;"
                                                            onscroll="OnScrollDivBOMDestination(this)" id="DivMainContentBOMDestination">
                                                            <asp:GridView ID="gvDestMaterialInput" runat="server" AutoGenerateColumns="False"
                                                                EmptyDataText="Please select material from material list for process" EmptyDataRowStyle-Font-Bold="true"
                                                                EmptyDataRowStyle-VerticalAlign="Middle" CssClass="zebra" Width="400px" ShowFooter="true"
                                                                EmptyDataRowStyle-Font-Size="Large" OnRowCommand="gvDestMaterialInput_RowCommand">
                                                                <Columns>
                                                                    <asp:TemplateField Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMaterialInputID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Code" HeaderStyle-Width="100px" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMaterialInputCode" runat="server" Text='<%# Eval("CODE") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Name" HeaderStyle-Width="100px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbllMaterialInputName" runat="server" Text='<%# Eval("NAME") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Type" HeaderStyle-Width="100px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbllMaterialInputType" runat="server" Text='<%# Eval("TYPE") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Unit" HeaderStyle-Width="100px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbllMaterialInputUnit" runat="server" Text='<%# Eval("UNITNAME") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%--Add By Rajeev--%>
                                                                    <asp:TemplateField HeaderText="Unit" HeaderStyle-Width="100px" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbllMaterialInputUnitValue" runat="server" Text='<%# Eval("UNITVALUE") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="120px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblQty" runat="server" Text='<%#Eval("Qty")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Ref Qty" HeaderStyle-Width="120px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRefQty" runat="server" Text='<%#Eval("Refqty")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="imginputMatDelete" runat="server" ImageUrl="~/images/delete.jpg"
                                                                                Height="20px" Width="20px" CausesValidation="false" CommandName="imginputMatDelete"
                                                                                CommandArgument='<%# Eval("ID")+","+Eval("NAME") %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                        <div id="DivFooterRowBOMDestination" style="overflow: hidden">
                                                        </div>
                                                    </td>
                                            </tr>
                                        </table>
                                        <%--=============== --%>
                                    </div>
                                    <div id="tab6" class="content">
                                        <table>
                                            <tr>
                                                <td colspan="4">
                                                    <asp:Label ID="Label4" runat="server" Text="Output Material List" Font-Bold="true"
                                                        Font-Size="Large"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div style="overflow: hidden; width: 100%;" id="DivHeaderRowOutputSource">
                                                    </div>
                                                    <div style="overflow: scroll; border: 1px solid #c3c9ce; margin-bottom: 8px; height: 260px;"
                                                        onscroll="OnScrollDivOutputSource(this)" id="DivMainContentOutputSource">
                                                        <asp:GridView ID="gvSourceMaterialOutput" runat="server" AutoGenerateColumns="False"
                                                            CssClass="zebra" Width="350px" EmptyDataText="No Records Available" EmptyDataRowStyle-Font-Bold="true"
                                                            EmptyDataRowStyle-VerticalAlign="Middle" OnRowDataBound="gvSourceMaterialOutput_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMaterialOutputID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Code" HeaderStyle-Width="100px" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMaterialOutputCode" runat="server" Text='<%# Eval("CODE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Name" HeaderStyle-Width="100px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbllMaterialOutputName" runat="server" Text='<%# Eval("NAME") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Type" HeaderStyle-Width="100px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbllMaterialOutputType" runat="server" Text='<%# Eval("TYPE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Unit" HeaderStyle-Width="100px">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="ddlUnit" runat="server">
                                                                        </asp:DropDownList>
                                                                        <%--      <asp:Label ID="lbllMaterialInputUnit" runat="server" Text='<%# Eval("UNITVALUE") %>'></asp:Label>--%>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="50px" ItemStyle-Width="50px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtQty" runat="server" Width="50px" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Ref Qty" HeaderStyle-Width="100px" ItemStyle-Width="50px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtRefQtyOutput" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                    <div id="DivFooterRowOutputSource" style="overflow: hidden">
                                                    </div>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="imgbAddMaterialOutput" runat="server" ImageUrl="~/images/arrowhr.png"
                                                        OnClick="imgbAddMaterialOutput_Click" CausesValidation="false" />
                                                </td>
                                                <td valign="top">
                                                    <div style="overflow: hidden; width: 100%;" id="DivHeaderRowOutputDestination">
                                                    </div>
                                                    <div style="overflow: scroll; border: 1px solid #c3c9ce; margin-bottom: 8px; height: 260px;"
                                                        onscroll="OnScrollDivOutputDestination(this)" id="DivMainContentOutputDestination">
                                                        <asp:GridView ID="gvDestMaterialOutput" runat="server" AutoGenerateColumns="False"
                                                            ShowFooter="true" EmptyDataText="Please select material from material list for process"
                                                            EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-VerticalAlign="Middle" CssClass="zebra"
                                                            Width="400px" EmptyDataRowStyle-Font-Size="Large" OnRowCommand="gvDestMaterialOutput_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMaterialOutputID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Code" HeaderStyle-Width="100px" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMaterialOutputCode" runat="server" Text='<%# Eval("CODE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Name" HeaderStyle-Width="100px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbllMaterialOutputName" runat="server" Text='<%# Eval("NAME") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Type" HeaderStyle-Width="100px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbllMaterialOutputType" runat="server" Text='<%# Eval("TYPE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Unit" HeaderStyle-Width="100px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbllMaterialInputUnit" runat="server" Text='<%# Eval("UNITNAME") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Unit" HeaderStyle-Width="100px" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbllMaterialInputUnitValue" runat="server" Text='<%# Eval("UNITVALUE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="120px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblQty" runat="server" Text='<%#Eval("Qty")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Ref Qty" HeaderStyle-Width="120px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRefQtyOutput" runat="server" Text='<%#Eval("Refqty")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgMaterialOutputDelete" runat="server" ImageUrl="~/images/delete.jpg"
                                                                            Height="20px" Width="20px" CausesValidation="false" CommandName="MaterialOutputDelete"
                                                                            CommandArgument='<%# Eval("ID")+","+Eval("NAME") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                    <div id="DivFooterRowOutputDestination" style="overflow: hidden">
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <!--Start Workstation Section!-->
                            <div id="tab2" class="content">
                                <p>
                                    <table border="1">
                                        <tr>
                                            <td colspan="4">
                                                <asp:Label ID="lblProcessWorkstation" runat="server" Font-Bold="true" Font-Size="Small"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <asp:Label ID="lblWorkstationList" runat="server" Text="Workstation List" Font-Bold="true"
                                                    Font-Size="Large"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div style="overflow: hidden; width: 100%;" id="DivHeaderRowWorkStationSource">
                                                </div>
                                                <div style="overflow: scroll; border: 1px solid #c3c9ce; margin-bottom: 8px;" onscroll="OnScrollDivWorkstationSource(this)"
                                                    id="DivMainContentWorkStationSource">
                                                    <asp:GridView ID="gvWorkstaionSource" runat="server" AutoGenerateColumns="False"
                                                        CssClass="zebra" EmptyDataText="No Records Available" Style="height: 100px; overflow: auto"
                                                        HeaderStyle-CssClass="FixedHeader">
                                                        <Columns>
                                                            <asp:TemplateField Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblWorkStationID" runat="server" Text='<%# Eval("WorkStationID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Code" HeaderStyle-Width="100px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblWorkStationCode" runat="server" Text='<%# Eval("WorkStationCode") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name" HeaderStyle-Width="100px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblWorkStationName" runat="server" Text='<%# Eval("WorkStationName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Duration In hour" HeaderStyle-Width="120px">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtDuration" runat="server" Width="100px" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <%--  </div>
													  <div id="DivFooterRowWorkStationSource" style="overflow: hidden"></div>--%>
                                            </td>
                                            <td>
                                                <td>
                                                    <asp:ImageButton ID="imgbAddWorkstation" runat="server" ImageUrl="~/images/arrowhr.png"
                                                        OnClick="imgbAddWorkstation_Click" CausesValidation="false" />
                                                </td>
                                                <td>
                                                </td>
                                                <td valign="top">
                                                    <asp:GridView ID="gvWorkstaionDestination" runat="server" AutoGenerateColumns="False"
                                                        EmptyDataText="Please select workstation from workstation list for process" EmptyDataRowStyle-Font-Bold="true"
                                                        EmptyDataRowStyle-VerticalAlign="Middle" CssClass="zebra" HeaderStyle-CssClass="FixedHeader"
                                                        EmptyDataRowStyle-Font-Size="Large" OnRowCommand="gvWorkstaionDestination_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblWorkStationID" runat="server" Text='<%# Eval("WorkStationID") %>'
                                                                        Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Code" HeaderStyle-Width="100px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblWorkStationCode" runat="server" Text='<%# Eval("WorkStationCode") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name" HeaderStyle-Width="100px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblWorkStationName" runat="server" Text='<%# Eval("WorkStationName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Duration In hour" HeaderStyle-Width="120px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDuration" runat="server" Text='<%# Eval("DurationInHour") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgbWorkstationDelete" runat="server" ImageUrl="~/images/delete.jpg"
                                                                        Height="20px" Width="20px" CommandName="WorkstationDelete" CommandArgument='<%# Eval("WorkStationID")+","+ Eval("WorkStationName")%>'
                                                                        CausesValidation="false" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <%--  </div>
														  <div id="DivFooterRowWorkStationDestination" style="overflow: hidden"></div>--%>
                                                </td>
                                        </tr>
                                    </table>
                                </p>
                            </div>
                            <!--End Workstation Section!-->
                            <!--Start Resource Section!-->
                            <div id="tab3" class="content">
                                <p>
                                    <table border="1">
                                        <tr>
                                            <td colspan="4">
                                                <asp:Label ID="lblProcessResource" runat="server" Font-Bold="true" Font-Size="Small"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <asp:Label ID="lblResourceList" runat="server" Text="Resource List" Font-Bold="true"
                                                    Font-Size="Large"></asp:Label>
                                            </td>
                                        </tr>
                                        <%--   START SOURCE RESOURCE--%>
                                        <tr>
                                            <td colspan="2">
                                                <div class="Grid" id="divgvgroup" runat="server" style="width: 400px; height: 280px">
                                                    <div class="head">
                                                        <table width="100%" cellspacing="0" cellpadding="0" border="0" id="tblHeader">
                                                            <tr>
                                                                <td width="49%">
                                                                    User Type
                                                                </td>
                                                                <td width="30%">
                                                                    No. Of Resource
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div class="zebra" style="width: 400px; height: 250px">
                                                        <asp:GridView ID="gvResourceSource" runat="server" AutoGenerateColumns="False" CssClass="zebra"
                                                            EmptyDataText="No Records Available" OnRowDataBound="gvResourceSource_RowDataBound"
                                                            ShowHeader="false" GridLines="Horizontal" Width="100%">
                                                            <Columns>
                                                                <asp:TemplateField Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUTID" runat="server" Text='<%# Eval("UTID") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="User Type" HeaderStyle-Width="100px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUTNAME" runat="server" Text='<%# Eval("UTNAME") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="No. Of Resource" HeaderStyle-Width="300px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtNoOfResource" runat="server" Width="100px"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle CssClass="header" Height="20px" />
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="ImgbAddResoure" runat="server" ImageUrl="~/images/arrowhr.png"
                                                    OnClick="ImgbAddResoure_Click" CausesValidation="false" />
                                            </td>
                                            <td>
                                            </td>
                                            <td valign="top">
                                                <div style="overflow: hidden; width: 100%;" id="DivHeaderRowResourceDestination">
                                                </div>
                                                <div style="overflow: scroll; border: 1px solid #c3c9ce; margin-bottom: 8px; height: 280px;"
                                                    onscroll="OnScrollDivResourceDestination(this)" id="DivMainContentResourceDestination">
                                                    <asp:GridView ID="gvResourceDestination" runat="server" AutoGenerateColumns="False"
                                                        EmptyDataText="Please select resource from resource list for process" EmptyDataRowStyle-Font-Bold="true"
                                                        EmptyDataRowStyle-VerticalAlign="Middle" CssClass="zebra" Width="400px" EmptyDataRowStyle-Font-Size="Large"
                                                        OnRowCommand="gvResourceDestination_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUTID" runat="server" Text='<%# Eval("ResourceID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="User Type" HeaderStyle-Width="100px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUTNAME" runat="server" Text='<%# Eval("ResourceName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="No. Of Resource" HeaderStyle-Width="100px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNoOfResource" runat="server" Text='<%# Eval("NoOfResource") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-Width="10px">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgbResourceDelete" runat="server" ImageUrl="~/images/delete.jpg"
                                                                        Height="20px" Width="20px" CommandName="ResourceDelete" CommandArgument='<%# Eval("ResourceID")+","+ Eval("ResourceName")%>'
                                                                        CausesValidation="false" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                        <%--   END SOURCE RESOURCE--%>
                                    </table>
                                </p>
                            </div>
                            <!--End Resource Section!-->
                            <!--Start QC Section!-->
                            <div id="tab4" class="content">
                                <p>
                                    <table>
                                        <tr>
                                            <td colspan="4">
                                                <asp:Label ID="lblProcessQC" runat="server" Font-Bold="true" Font-Size="Small"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <asp:Label ID="lblQCList" runat="server" Text="QC List" Font-Bold="true" Font-Size="Large"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div style="overflow: hidden; width: 100%;" id="DivHeaderRowQCSource">
                                                </div>
                                                <div style="overflow: scroll; border: 1px solid #c3c9ce; margin-bottom: 8px;" onscroll="OnScrollDivQCSource(this)"
                                                    id="DivMainContentQCSource">
                                                    <asp:GridView ID="gvQCSource" runat="server" AutoGenerateColumns="False" CssClass="zebra">
                                                        <Columns>
                                                            <asp:TemplateField Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQCID" runat="server" Text='<%# Eval("QCID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="QC Name" HeaderStyle-Width="300px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQCName" runat="server" Text='<%# Eval("QCName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkHeader" runat="server" Text=" " />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkRow" runat="server" Text=" " />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                                <div id="DivFooterRowQCSource" style="overflow: hidden">
                                                </div>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgbAddQC" runat="server" ImageUrl="~/images/arrowhr.png" OnClick="imgbAddQC_Click"
                                                    CausesValidation="false" />
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td valign="top">
                                                <div style="overflow: hidden; width: 100%;" id="DivHeaderRowQCDestination">
                                                </div>
                                                <div style="overflow: scroll; border: 1px solid #c3c9ce; margin-bottom: 8px;" onscroll="OnScrollDivQCDestination(this)"
                                                    id="DivMainContentQCDestination">
                                                    <asp:GridView ID="gvQCDestination" runat="server" AutoGenerateColumns="False" EmptyDataText="Please select QC from QC list for process"
                                                        EmptyDataRowStyle-Font-Bold="true" EmptyDataRowStyle-VerticalAlign="Middle" CssClass="zebra"
                                                        Width="400px" EmptyDataRowStyle-Font-Size="Large" OnRowCommand="gvQCDestination_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQCID" runat="server" Text='<%# Eval("QCID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="QC Name" HeaderStyle-Width="300px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQCName" runat="server" Text='<%# Eval("QCName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgbQCDelete" runat="server" ImageUrl="~/images/delete.jpg"
                                                                        Height="20px" Width="20px" CausesValidation="false" CommandName="QCDelete" CommandArgument='<%# Eval("QCID")+","+Eval("QCName") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                                <div id="DivFooterRowQCDestination" style="overflow: hidden">
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </p>
                            </div>
                            <!--End QC Section!-->
                        </div>
                        <div class="footer"  style="width:45px">
                            <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn-danger" CausesValidation="false"
                                OnClick="btnClose_Click" />
                        </div>
                    </div>
                    <div id="fade" class="black_overlay" runat="server">
                    </div>
                    <cc1:MessageBox ID="MessageBox1" runat="server" />
                    <span class="clear"></span>
                </div>
                <span class="clear"></span>
                <script type="text/javascript">
                    function OnBeforeDelete(record) {
                        record.Error = '';
                        document.getElementById("<%=hdnfProcessFrameworkID.ClientID %>").value = record.ProcessFrameworkID;
                        if (confirm("Are you sure you want to delete? "))
                            return true;
                        else
                            return false;
                    }
                    function OnDelete(record) {
                        //alert(record.Error);
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

                        gvProcessFramework.addFilterCriteria('ProcessFrameworkCode', OboutGridFilterCriteria.Contains, searchValue);
                        gvProcessFramework.addFilterCriteria('ProcessFrameworkName', OboutGridFilterCriteria.Contains, searchValue);
                        gvProcessFramework.addFilterCriteria('ProcessFrameworkDescription', OboutGridFilterCriteria.Contains, searchValue);
                        gvProcessFramework.executeFilter();
                        searchTimeout = null;
                        return false;
                    }
                </script>
                <script type="text/javascript">
                    function CallServerMethod(oLink) {
                        var iRecordIndex = oLink.id.toString().replace("btnGridEdit_", "");
                        document.getElementById("<%=hdnfProcessFrameworkID.ClientID %>").value = gvProcessFramework.Rows[iRecordIndex].Cells[0].Value;
                        document.getElementById("<%=btngridedit.ClientID %>").click();
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>