using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPBLL;
using System.Data;
using System.Web.Services;

public partial class VIEW_frmOpeningStockUpdate : System.Web.UI.Page
{
    ClsStockJournal objclsStockJournal = new ClsStockJournal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadStorelocation();
            
        }
    }
    public void loadStorelocation()
    {
        string mode = "ProductLocation";
        DataTable objDt = new DataTable();
        objDt = objclsStockJournal.BindProductDetailsFromProduct(mode,"");
        
        this.ddlStoreLocation.DataSource = objDt;
        this.ddlStoreLocation.DataValueField = "ID";
        this.ddlStoreLocation.DataTextField = "NAME";
        this.ddlStoreLocation.DataBind();

    }
    public void loadGrid()
    {
        string mode = "OpeningPrt";
        DataTable objDt = new DataTable();
        objDt = objclsStockJournal.BindProductDetailsFromProduct(mode,this.ddlStoreLocation.SelectedValue.ToString());
        grdOpeningStock.DataSource = objDt;
        grdOpeningStock.DataBind();

    }

    protected void ddlStoreLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadGrid();
    }
    [System.Web.Services.WebMethod]
    public static string Main(string ProductId,string StoreId, decimal Qty, decimal Rate)
    {
        string ret = "";
        ClsStockJournal objClass = new ClsStockJournal();
        DataTable objDt = new DataTable();
        objDt = objClass.UpdateOpeningStock(ProductId, StoreId, Qty, Rate);
        if(objDt.Rows.Count>0)
        {
            ret = Convert.ToString(objDt.Rows[0]["id"].ToString());

        }
        return ret;
    }

   
}