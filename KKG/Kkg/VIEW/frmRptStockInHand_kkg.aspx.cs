using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_Default : System.Web.UI.Page
{

    ClsStockReport objStockReport = new ClsStockReport();
    DateTime today1 = DateTime.Now;
    DataTable objDt = new DataTable();
    string date = "dd/MM/yyyy";
    public enum MessageType { Success, Error, Info, Warning };
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> $(function () { $('#ContentPlaceHolder1_ddlDiv').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlCat').multiselect({ includeSelectAllOption: true  });});</script>", false);
            if (!IsPostBack)
            {
                loadFactory();
                loadDivison();
                loadCategory();
                loadProduct();
                loadStoreLocation();
                this.divExport.Style["display"] = "none";
                this.txtReqFromDate.Text = today1.ToString(date).Replace('-', '/');
                this.CalendarExtender6.EndDate = DateTime.Now;
                DateLock();


            }
        }
        catch (Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }
    }

    #region methods

    #region Add FinYear Wise Date Lock
    public void DateLock()
    {
        string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
        string startyear = finyear.Substring(0, 4);
        int startyear1 = Convert.ToInt32(startyear);
        string endyear = finyear.Substring(5);
        int endyear1 = Convert.ToInt32(endyear);
        DateTime oDate = new DateTime(startyear1, 04, 01);
        DateTime cDate = new DateTime(endyear1, 03, 31);
        DateTime today1 = DateTime.Now;
        CalendarExtender6.StartDate = oDate;
       
        if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
        {
            this.txtReqFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            CalendarExtender6.EndDate = today1;
        }
        else
        {
            this.txtReqFromDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            CalendarExtender6.EndDate = cDate;
        }
    }
    #endregion

    public void loadFactory()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = objStockReport.BindDepotIncludeExport();
            if (dt.Rows.Count > 0)
            {
                this.ddlFactory.Items.Clear();
                this.ddlFactory.Items.Add(new ListItem("Select Factory", "0"));
                this.ddlFactory.AppendDataBoundItems = true;
                this.ddlFactory.DataSource = dt;
                this.ddlFactory.DataTextField = "BRNAME";
                this.ddlFactory.DataValueField = "BRID";
                this.ddlFactory.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }
    }

    public void loadDivison()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = objStockReport.BindBrand();
            if (dt.Rows.Count > 0)
            {
                this.ddlDiv.Items.Clear();
                this.ddlDiv.Items.Add(new ListItem("Select Divison", "0"));
                this.ddlDiv.AppendDataBoundItems = true;
                this.ddlDiv.DataSource = dt;
                this.ddlDiv.DataTextField = "DIVNAME";
                this.ddlDiv.DataValueField = "DIVID";
                this.ddlDiv.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }
    }

    public void loadCategory()
    {
        try
        {
            //string DivId = string.Empty;
            //var query = from ListItem item in ddlDiv.Items where item.Selected select item;
            //foreach (ListItem item in query)
            //{
            //    DivId += item.Value + ',';
            //}
            //DivId = DivId.Substring(0, DivId.Length - 1);

            DataTable dt = new DataTable();
            PPBLL.ClsStockReport clsreport = new PPBLL.ClsStockReport();
            dt = clsreport.BindCategory();
            if (dt.Rows.Count > 0)
            {
                this.ddlCat.Items.Clear();
                this.ddlCat.Items.Add(new ListItem("Select Category", "0"));
                this.ddlCat.AppendDataBoundItems = true;
                this.ddlCat.DataSource = dt;
                this.ddlCat.DataTextField = "CATNAME";
                this.ddlCat.DataValueField = "CATID";
                this.ddlCat.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }
    }

    public void loadProduct()
    {
        try 
        {
            //string DivId = string.Empty;
            //string CatId = string.Empty;
            //var query = from ListItem item in ddlDiv.Items where item.Selected select item;
            //foreach (ListItem item in query)
            //{
            //    DivId += item.Value + ',';
            //}
            //DivId = DivId.Substring(0, DivId.Length - 3);

            //var query1 = from ListItem item in ddlCat.Items where item.Selected select item;
            //foreach (ListItem item in query1)
            //{
            //    CatId += item.Value + ',';
            //}
            //CatId = CatId.Substring(0, CatId.Length - 3);

            PPBLL.ClsStockReport clsreport = new PPBLL.ClsStockReport();
            DataTable dt = new DataTable();
            dt = clsreport.BindProduct(HttpContext.Current.Session["DEPOTID"].ToString());
            if (dt.Rows.Count > 0)
            {
                this.ddlProduct.Items.Clear();
                this.ddlProduct.Items.Add(new ListItem("Select All Product", "-1"));
                this.ddlProduct.AppendDataBoundItems = true;
                this.ddlProduct.DataSource = dt;
                this.ddlProduct.DataTextField = "NAME";
                this.ddlProduct.DataValueField = "ID";
                this.ddlProduct.DataBind();
            }

        }
        catch (Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }
    }

    public void loadStoreLocation()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = objStockReport.BindStoreLocation();
            if (dt.Rows.Count > 0)
            {
                this.ddlStore.Items.Clear();
                this.ddlStore.Items.Add(new ListItem("Select All Store", "-1"));
                this.ddlStore.AppendDataBoundItems = true;
                this.ddlStore.DataSource = dt;
                this.ddlStore.DataTextField = "NAME";
                this.ddlStore.DataValueField = "ID";
                this.ddlStore.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }
    }

    #endregion
    protected void ShowMessage(string Message, MessageType type)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
    }

    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        try
        {
            if(this.ddlFactory.SelectedValue=="0")
            {
                ShowMessage("Please Select Factory Name", MessageType.Error);
                return;
            }
            if(ddlDiv.SelectedValue=="")
            {
                ShowMessage("Please Select Divison", MessageType.Warning);
                return;
            }
            if (ddlCat.SelectedValue=="")
            {
                ShowMessage("Please Select Category", MessageType.Warning);
                return;
            }
            string DivId = string.Empty;
            string CatId = string.Empty;
            var query = from ListItem item in ddlDiv.Items where item.Selected select item;
            foreach (ListItem item in query)
            {
                DivId += item.Value + ',';
            }
            DivId = DivId.Substring(0, DivId.Length - 1);

            if (DivId=="" || DivId=="0")
            {
                ShowMessage("Please Select Divison", MessageType.Warning);
                return;
            }

            var query1 = from ListItem item in ddlCat.Items where item.Selected select item;
            foreach (ListItem item in query1)
            {
                CatId += item.Value + ',';
            }
            CatId = CatId.Substring(0, CatId.Length - 1);

            if (CatId == "" || CatId == "0")
            {
                ShowMessage("Please Select Category", MessageType.Warning);
                return;
            }

            objDt = objStockReport.FetchStockInHand(this.ddlFactory.SelectedValue,this.ddlProduct.SelectedValue,this.ddlStore.SelectedValue,
                DivId, CatId,this.txtReqFromDate.Text.Trim());



            

            if (objDt.Rows.Count > 0)
            {
                this.divExport.Style["display"] = "";
                ShowMessage("Please Wait......", MessageType.Success);
                this.grdStockInHand.DataSource = objDt;
                this.grdStockInHand.DataBind();
            }
            else
            {
                this.divExport.Style["display"] = "none";
                ShowMessage("No data found", MessageType.Info);
                this.grdStockInHand.DataSource = null;
                this.grdStockInHand.DataBind();
                return;
            }



        }
        catch (Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }
    }

    protected void btnGetProduct_Click(object sender, EventArgs e)
    {
        loadProduct();
    }
}