<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmOpeningStockUpdate.aspx.cs" Inherits="VIEW_frmOpeningStockUpdate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .hiddencol {
            display: none;
        }

        input:disabled {
            color: #aaa;
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

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js" type="text/javascript"></script>
   
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
     <link href="https://unpkg.com/gijgo@1.9.13/css/gijgo.min.css" rel="stylesheet" type="text/css" />
    <script src="https://unpkg.com/gijgo@1.9.13/js/gijgo.min.js" type="text/javascript"></script>
      <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet" type="text/css" />
      <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <script src="https://kit.fontawesome.com/a076d05399.js" crossorigin="anonymous"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".Update").css('visibility', 'hidden');

            
        });
        $(function () {
            $("body").on("click", ".editRow", function (i) {
                debugger;
                let row = $(this).closest("tr");
                row.find('input:text[id="grdTxtQty"]').removeAttr("disabled");
                row.find('input:text[id="grdTxtRate"]').removeAttr("disabled");
                row.find($(".editRow")).hide();
                row.find($(".Update")).css('visibility', 'visible');
                return false;
            });
            $("body").on("click", ".Update", function (i) {
                let row = $(this).closest("tr");
                $("#hdnStoreId").val(row.find('td:eq(3)').html());
                let ProductId, StoreId, Qty, Rate;
                ProductId = row.find('td:eq(0)').html();
                StoreId = row.find('td:eq(3)').html();
                Qty = row.find('input:text[id="grdTxtQty"]').val();
                Rate = row.find('input:text[id="grdTxtRate"]').val();
                updateProduct(ProductId, StoreId, Qty, Rate);
                /*row.find($(".editRow")).show();*/
                //row.find($(".Update")).show();
                //
                row.find('input:text[id="grdTxtQty"]').attr('disabled', 'disabled');
                row.find('input:text[id="grdTxtRate"]').attr('disabled', 'disabled');
                row.find($(".Update")).css('visibility', 'hidden');
                row.find($(".editRow")).show();
                
                return false;
            });
        });

        function updateProduct(ProductId, StoreId, Qty, Rate) {
            try {
                $.ajax({
                    type: "POST",
                    url: "frmOpeningStockUpdate.aspx/Main",
                    data: '{ProductId: "' + ProductId + '",StoreId: "' + StoreId + '",Qty: "' + Qty + '",Rate: "' + Rate + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccess,
                    failure: function (response) {
                        alert(response.d);
                    }
                });
            }
            catch (ex) {
                alert(ex);
            }
        }

        function OnSuccess(response) {
            if (response.d === "1") {
                alert("Update Done");
                return false;
            }

            alert("Error Please contact to admin");
        }

       
    </script>
</head>
<body>
    <form id="form1" runat="server">
       <div class="container">
           <h2>Opening Stock Updtae</h2>
            <fieldset id="fld" runat="server">
                            <legend>Guidelines</legend>
                            <p>
                                <strong>Note :</strong>&nbsp;After update their must be neagative stock;
                            </p>
                        </fieldset>
               <div class="form-group">
               <label for="email">StoreLocation:</label>
                        <asp:DropDownList ID="ddlStoreLocation" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlStoreLocation_SelectedIndexChanged">
                        </asp:DropDownList>
                    
            <br />
                   <div style="height: 450px; overflow-x: scroll;">

        

            <asp:GridView ID="grdOpeningStock"  style="height:400px; overflow:auto;width:1200px" 
                HeaderStyle-BackColor="YellowGreen" AlternatingRowStyle-BackColor="WhiteSmoke"  runat="server" AutoGenerateColumns="false" >
                <Columns>
                    <asp:BoundField DataField="ProductId" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" HeaderText="ProductId" />
                    <asp:BoundField DataField="ProductName" ItemStyle-CssClass="Name" HeaderText="ProductName" HeaderStyle-Width="200px" ItemStyle-Width="200px" />
                    <asp:BoundField DataField="Code" ItemStyle-CssClass="Code" HeaderText="Code" HeaderStyle-Width="200px" ItemStyle-Width="200px" />
                    <asp:BoundField DataField="StoreId" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol" HeaderText="StoreId" />
                    <asp:BoundField DataField="StoreName" ItemStyle-CssClass="StoreName" HeaderText="StoreName" HeaderStyle-Width="200px" ItemStyle-Width="200px" />

                    <asp:TemplateField ItemStyle-CssClass="disabled" HeaderText="Opening Qty" HeaderStyle-Width="200px" ItemStyle-Width="200px">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="grdTxtQty" Enabled="false" ClientIDMode="Static" Text='<%#Eval("Qty") %>'>
                            </asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField ItemStyle-CssClass="Rate" HeaderText="Rate" HeaderStyle-Width="200px" ItemStyle-Width="200px">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="grdTxtRate" Enabled="false" ClientIDMode="Static" Text='<%#Eval("Rate") %>'>
                            </asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="200px" ItemStyle-Width="200px">
                        <ItemTemplate>
                            <a href="#" class="btn btn-default editRow"> <i class="fas fa-edit" title="edit"></i></a>
                            <a href="#" class="btn btn-default Update" ><i class="fas fa-save" title="Update"></i></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
                     </div>

            <asp:HiddenField ID="hdnProductId" runat="server" />
            <asp:HiddenField ID="hdnStoreId" runat="server" />
        </div>
    </form>
</body>
</html>
