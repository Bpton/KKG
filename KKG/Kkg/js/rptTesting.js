//const columnDefs = [
//    {
//        headerName: 'PRODUCTNAME', field: 'PRODUCTNAME', width: 140
       
//    },
//    {
//        headerName: 'Athlete Details',
//        children: [
//            {
//                field: 'athlete',
//                width: 180,
//                filter: 'agTextColumnFilter',
//            },
//            {
//                field: 'age',
//                width: 90,
//                filter: 'agNumberColumnFilter',
//            },
//            { headerName: 'Country', field: 'country', width: 140 },
//        ],
//    },
//    {
//        headerName: 'Sports Results',
//        children: [
//            { field: 'sport', width: 140 },
//            {
//                columnGroupShow: 'closed',
//                field: 'total',
//                width: 100,
//                filter: 'agNumberColumnFilter',
//            },
//            {
//                columnGroupShow: 'open',
//                field: 'gold',
//                width: 100,
//                filter: 'agNumberColumnFilter',
//            },
//            {
//                columnGroupShow: 'open',
//                field: 'silver',
//                width: 100,
//                filter: 'agNumberColumnFilter',
//            },
//            {
//                columnGroupShow: 'open',
//                field: 'bronze',
//                width: 100,
//                filter: 'agNumberColumnFilter',
//            },
//        ],
//    },
//];

const columnDefs = [
    {
        headerName: 'PRODUCTNAME', field: 'PRODUCTNAME', width: 140

    },
    {
        headerName: 'CONSUMBLE STORE',
        children: [
            {
                field: 'QTY',
                width: 180,
                filter: 'agTextColumnFilter',
            },
            {
                field: 'VALUE',
                width: 90,
                filter: 'agNumberColumnFilter',
            },
        ],
    },
    {
        headerName: 'FAN ASSEMBLY',
        children: [
            {
                field: 'QTY',
                width: 180,
                filter: 'agTextColumnFilter',
            },
            {
                field: 'VALUE',
                width: 90,
                filter: 'agNumberColumnFilter',
            },
        ],
    },
    {
        headerName: 'FINISH GOODS ',
        children: [
            {
                field: 'QTY',
                width: 180,
                filter: 'agTextColumnFilter',
            },
            {
                field: 'VALUE',
                width: 90,
                filter: 'agNumberColumnFilter',
            },
        ],
    },
    {
        headerName: 'GAS WATER HEATER ASSEMBLY',
        children: [
            {
                field: 'QTY',
                width: 180,
                filter: 'agTextColumnFilter',
            },
            {
                field: 'VALUE',
                width: 90,
                filter: 'agNumberColumnFilter',
            },
        ],
    },
    {
        headerName: 'KKG 55',
        children: [
            {
                field: 'QTY',
                width: 180,
                filter: 'agTextColumnFilter',
            },
            {
                field: 'VALUE',
                width: 90,
                filter: 'agNumberColumnFilter',
            },
        ],
    },
    {
        headerName: 'MACHINE SHOP',
        children: [
            {
                field: 'QTY',
                width: 180,
                filter: 'agTextColumnFilter',
            },
            {
                field: 'VALUE',
                width: 90,
                filter: 'agNumberColumnFilter',
            },
        ],
    },
    {
        headerName: 'MAIN STORE',
        children: [
            {
                field: 'QTY',
                width: 180,
                filter: 'agTextColumnFilter',
            },
            {
                field: 'VALUE',
                width: 90,
                filter: 'agNumberColumnFilter',
            },
        ],
    },
    {
        headerName: 'MOULDING ',
        children: [
            {
                field: 'QTY',
                width: 180,
                filter: 'agTextColumnFilter',
            },
            {
                field: 'VALUE',
                width: 90,
                filter: 'agNumberColumnFilter',
            },
        ],
    },
    {
        headerName: 'POWDER COATING SHOP ',
        children: [
            {
                field: 'QTY',
                width: 180,
                filter: 'agTextColumnFilter',
            },
            {
                field: 'VALUE',
                width: 90,
                filter: 'agNumberColumnFilter',
            },
        ],
    },
    {
        headerName: 'PRESS SHOP',
        children: [
            {
                field: 'QTY',
                width: 180,
                filter: 'agTextColumnFilter',
            },
            {
                field: 'VALUE',
                width: 90,
                filter: 'agNumberColumnFilter',
            },
        ],
    },
    {
        headerName: 'Saleable',
        children: [
            {
                field: 'QTY',
                width: 180,
                filter: 'agTextColumnFilter',
            },
            {
                field: 'VALUE',
                width: 90,
                filter: 'agNumberColumnFilter',
            },
        ],
    },
    {
        headerName: 'TPW SHOP',
        children: [
            {
                field: 'QTY',
                width: 180,
                filter: 'agTextColumnFilter',
            },
            {
                field: 'VALUE',
                width: 90,
                filter: 'agNumberColumnFilter',
            },
        ],
    },
    {
        headerName: 'WINDING',
        children: [
            {
                field: 'QTY',
                width: 180,
                filter: 'agTextColumnFilter',
            },
            {
                field: 'VALUE',
                width: 90,
                filter: 'agNumberColumnFilter',
            },
        ],
    },
   
];

const gridOptions = {
    defaultColDef: {
        sortable: true,
        resizable: true,
        filter: true,
    },
    // debug: true,
    columnDefs: columnDefs,
    rowData: null,
};


document.addEventListener('DOMContentLoaded', function () {
    const gridDiv = document.querySelector('#myGrid');
    new agGrid.Grid(gridDiv, gridOptions);

    /*fetch('../Api/jsonclass.cs/main')*/
    fetch('../data.json')


        /*fetch('https://www.ag-grid.com/example-assets/olympic-winners.json')*/
        .then((response) => response.json())
        .then((data) => gridOptions.api.setRowData(data));

});


   

