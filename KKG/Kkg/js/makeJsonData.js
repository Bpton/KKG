$(function () {
    function groupChg(val) {
        var lower = Math.floor(val / 10) * 10,
            upper = Math.ceil((val + 1) / 10) * 10;
        return lower + " < " + upper;
    };

    var colM = [
        //extra column to show grouping (for both pivot and row grouping mode, used along with groupModel.titleIndx )            
        {
            title: 'Group', tpHide: true, menuInHide: true,
            dataIndx: 'grp',
            menuUI: {
                tabs: ['hideCols'] //display only hide columns tab in menu.
            }
        },
        { title: "Sport", dataIndx: "sport", width: 110 },
        { title: "Athlete", dataIndx: "athlete", width: 200 },
        {
            title: "Age", dataIndx: "age", width: 90, align: 'center', dataType: 'integer',
            groupChange: groupChg
        },
        {
            title: "Gold", dataIndx: "gold", width: 100, dataType: 'integer',
            tpCls: 'gold', denyGroup: true, denyPivot: true
        },
        { title: "Silver", dataIndx: "silver", width: 100, dataType: 'integer', denyGroup: true },
        { title: "Bronze", dataIndx: "bronze", width: 100, dataType: 'integer', denyGroup: true },
        { title: "Total", dataIndx: 'total', width: 100, dataType: 'integer', denyGroup: true },
        { title: "Country", dataIndx: "country", width: 120 },
        { title: "Year", dataIndx: "year", width: 90, dataType: 'integer' }
        //{title: "Date", dataIndx: "date", dataType:'date', width: 110},            
    ];

    colM.forEach(function (col) {
        col.menuIcon = true;
        col.filter = { condition: 'range' };
    })

    var groupModel = {
        on: true, //grouping mode.
        pivot: true, //pivotMode
        checkbox: true, checkboxHead: true, select: true,
        //titleInFirstCol: true,
        titleIndx: 'grp', //v7.0.0: new option instead of titleInFirstCol
        indent: 20, fixCols: false,
        groupCols: ['year'], //grouping along column axis.
        agg: { //aggregate fields.
            gold: 'sum',
            silver: 'sum',
            bronze: 'sum',
            total: 'sum'
        },
        header: false, //hide grouping toolbar.
        grandSummary: true, //show grand summary row.           
        dataIndx: ['country', 'sport'], //grouping along row axis.
        collapsed: [true, true],
        useLabel: true,
        summaryEdit: false
    };
    var dataModel = {
        location: "remote",
        cache: true,
        url: "/Content/olympicWinners.json",
        getData: function (data) {
            return { data: data };
        }
    };

    var obj = {
        height: 500,
        dataModel: dataModel,
        numberCell: { width: 50 },
        freezeCols: 1,
        flex: { one: true },
        rowBorders: false,
        colModel: colM,
        groupModel: groupModel,
        //sortModel: { sorter:[{dataIndx: 'country'}] },            
        summaryTitle: {
            avg: "",
            count: '',
            max: "",
            min: "",
            stdev: "",
            stdevp: "",
            sum: ""
        },
        formulas: [['total', function (rd) {
            var total = rd.gold + rd.silver + rd.bronze;
            return isNaN(total) ? "" : total;
        }]],
        showTitle: false,
        wrap: false,
        hwrap: false,
        editable: false,
        toolPanel: {
            show: true  //show toolPanel initially.
        },
        toolbar: {
            cls: 'pq-toolbar-export',
            items: [
                {
                    type: 'button',
                    label: "Export to Excel(xlsx)",
                    icon: 'ui-icon-document',
                    listener: function (evt) {
                        var str = this.exportExcel({ render: true });
                        saveAs(str, "pivot.xlsx");
                    }
                },
                {
                    type: 'button',
                    label: "Toolbar Panel",
                    icon: 'ui-icon-wrench',
                    listener: function (evt) {
                        this.ToolPanel().toggle();
                    }
                },
                {
                    type: 'textbox',
                    label: "Filter: ",
                    attr: 'placeholder="Enter text"',
                    listener: {
                        timeout: function (evt) {
                            var txt = $(evt.target).val();
                            var rules = this.getCMPrimary().map(function (col) {
                                return {
                                    dataIndx: col.dataIndx,
                                    condition: 'contain',
                                    value: txt
                                }
                            })
                            this.filter({
                                mode: 'OR',
                                rules: rules
                            })
                        }
                    }
                }
            ]
        },
        groupOption: function (evt, ui) {
            //hide Group column when no group rows are present.                
            this.Columns().find(function (col) {
                return col.title == "Group"
            }).hidden = !this.option('groupModel').dataIndx.length;
        },
        //use pivotCM event to make grouped columns collapsible.
        pivotCM: function (evt, ui) {
            //add collapsible to grouped parent columns.
            this.Columns().each(function (col) {
                var cm = col.colModel
                if (cm && cm.length > 1 && !col.collapsible)
                    col.collapsible = { on: true, last: true };
            }, ui.CM);
        }
    };
    var grid = pq.grid("#grid_pivot", obj);
});
