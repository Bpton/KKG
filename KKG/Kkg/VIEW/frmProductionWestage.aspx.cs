using PPBLL;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmProductionWestage : System.Web.UI.Page
{
    ClsProductionOrderClosingMM clsPOC = new ClsProductionOrderClosingMM();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                pnlADD.Style["display"] = "";
                LoadBulkProduct();
                DateLock();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    #region LoadProductioOrderClosing
    public void LoadProductioOrderClosing()
    {
        try
        {
            DataTable dt = new DataTable();
            string DepotId = HttpContext.Current.Session["DEPOTID"].ToString().Trim();
            dt = clsPOC.BindWastageDtls(txtFromDate.Text, txtToDate.Text, HttpContext.Current.Session["FINYEAR"].ToString(), DepotId, ddlBulkProduct.SelectedValue.ToString(), ddlproductionno.SelectedValue.ToString());
            if (dt.Rows.Count > 0)
            {
                this.gvProductionWastage.DataSource = dt;
                this.gvProductionWastage.DataBind();
            }
            else
            {
                this.gvProductionWastage.DataSource = null;
                this.gvProductionWastage.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadBulkProduct
    public void LoadBulkProduct()
    {
        try
        {
            DataTable dtBP = new DataTable();
            string DepotId = HttpContext.Current.Session["DEPOTID"].ToString().Trim();
            dtBP = clsPOC.BindBulkProduct_WithOutBatchno(DepotId, HttpContext.Current.Session["FINYEAR"].ToString());
            if (dtBP.Rows.Count > 0)
            {
                if (dtBP.Rows.Count == 1)
                {
                    this.ddlBulkProduct.Items.Clear();
                    this.ddlBulkProduct.AppendDataBoundItems = true;
                    this.ddlBulkProduct.Items.Add(new ListItem("Select Bulk Product", "0"));
                    this.ddlBulkProduct.DataSource = dtBP;
                    this.ddlBulkProduct.DataValueField = "ID";
                    this.ddlBulkProduct.DataTextField = "PRODUCTALIAS";
                    this.ddlBulkProduct.DataBind();
                    this.ddlBulkProduct.SelectedValue = Convert.ToString(dtBP.Rows[0]["ID"]);
                }
                if (dtBP.Rows.Count > 1)
                {
                    this.ddlBulkProduct.Items.Clear();
                    this.ddlBulkProduct.AppendDataBoundItems = true;
                    this.ddlBulkProduct.Items.Add(new ListItem("Select Bulk Product", "0"));
                    this.ddlBulkProduct.DataSource = dtBP;
                    this.ddlBulkProduct.DataValueField = "ID";
                    this.ddlBulkProduct.DataTextField = "PRODUCTALIAS";
                    this.ddlBulkProduct.DataBind();
                }
            }
            else
            {
                this.ddlBulkProduct.Items.Clear();
                this.ddlBulkProduct.AppendDataBoundItems = true;
                this.ddlBulkProduct.Items.Add(new ListItem("Select Bulk Product", "0"));
                this.ddlBulkProduct.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }

    #endregion

    #region Search
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            CreateDataTable();
            LoadProductioOrderClosing();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Convert DataTable To XML
    public string ConvertDatatable(DataTable dt)
    {
        MemoryStream str = new MemoryStream();
        dt.TableName = "XMLData";
        dt.WriteXml(str, true);
        str.Seek(0, SeekOrigin.Begin);
        StreamReader sr = new StreamReader(str);
        string xmlstr;
        xmlstr = sr.ReadToEnd();
        return (xmlstr);
    }
    #endregion

    #region Create DataTable Structure
    public void CreateDataTable()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add(new DataColumn("CATEGORYID", typeof(string)));
        dt.Columns.Add(new DataColumn("CATEGORYNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("MATERIALID", typeof(string)));
        dt.Columns.Add(new DataColumn("MATERIALNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("UOMID", typeof(string)));
        dt.Columns.Add(new DataColumn("UOMNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("QTY", typeof(string)));
        dt.Columns.Add(new DataColumn("REQUIREDFROMDATE", typeof(string)));
        dt.Columns.Add(new DataColumn("REQUIREDTODATE", typeof(string)));
        dt.Columns.Add(new DataColumn("CONSUMABLESQTY", typeof(string)));
        dt.Columns.Add(new DataColumn("WASTAGEQTY", typeof(string)));
        HttpContext.Current.Session["RMPMWASTAGE"] = dt;
    }
    #endregion

    #region Conver_To_ISO
    public string Conver_To_ISO(string dt)
    {
        string strOpenDate = dt;
        string day = strOpenDate.Substring(0, strOpenDate.IndexOf("/"));
        string month = strOpenDate.Substring(strOpenDate.IndexOf("/"));
        month = month.Substring(1, month.Length - 1);
        string year = month.Substring(month.IndexOf("/"));
        month = month.Substring(0, month.IndexOf("/"));
        year = year.Substring(1, year.Length - 1);
        dt = year + month + day;
        return dt;
    }
    #endregion

    protected void btnsave_Click(object sender, EventArgs e)
    {
        int id = 0;
        if (HttpContext.Current.Session["RMPMWASTAGE"] == null)
        {
            CreateDataTable();
        }
        DataTable dt = (DataTable)HttpContext.Current.Session["RMPMWASTAGE"];
        int count = 0;
        string XML = string.Empty;
        int result = 0;
        decimal ConsumbleQty = 0;


        foreach (GridViewRow gvrow in gvProductionWastage.Rows)
        {
            /*CheckBox chkBx = (CheckBox)gvrow.FindControl("chkSelect");
            if (chkBx.Checked)
            {*/
            Label lblCATEGORYID = (Label)gvrow.FindControl("lblCATEGORYID");
            Label lblCATEGORYNAME = (Label)gvrow.FindControl("lblCATEGORYNAME");
            Label lblMATERIALID = (Label)gvrow.FindControl("lblMATERIALID");
            Label lblMATERIALNAME = (Label)gvrow.FindControl("lblMATERIALNAME");
            Label lblUOMID = (Label)gvrow.FindControl("lblUOMID");
            Label lblUOMNAME = (Label)gvrow.FindControl("lblUOMNAME");
            TextBox lblISSUEQTY = (TextBox)gvrow.FindControl("lblISSUEQTY");
            TextBox txtCONSUMABLESQTY = (TextBox)gvrow.FindControl("txtCONSUMABLESQTY");
            TextBox txtWastage = (TextBox)gvrow.FindControl("txtWastage");
            TextBox lblReturnQty = (TextBox)gvrow.FindControl("lblReturnQty");
            //ConsumbleQty = (Convert.ToDecimal(lblISSUEQTY.Text.Trim()) - Convert.ToDecimal(lblReturnQty.Text.Trim()));

            if (Convert.ToDecimal(lblISSUEQTY.Text.Trim()) >= Convert.ToDecimal(txtWastage.Text.Trim()))
            {
                count = count + 1;
                DataRow drQty = dt.NewRow();
                drQty["CATEGORYID"] = Convert.ToString(lblCATEGORYID.Text).Trim();
                drQty["CATEGORYNAME"] = Convert.ToString(lblCATEGORYNAME.Text).Trim();
                drQty["MATERIALID"] = Convert.ToString(lblMATERIALID.Text).Trim();
                drQty["MATERIALNAME"] = Convert.ToString(lblMATERIALNAME.Text).Trim();
                drQty["QTY"] = Convert.ToString(lblISSUEQTY.Text).Trim();//Convert.ToString(lblReturnQty.Text).Trim();//String.Format("{0:#0.000}", FinalQty);
                drQty["UOMID"] = Convert.ToString(lblUOMID.Text).Trim();
                drQty["UOMNAME"] = Convert.ToString(lblUOMNAME.Text).Trim();
                drQty["REQUIREDFROMDATE"] = "";
                drQty["REQUIREDTODATE"] = "";
                //drQty["CONSUMABLESQTY"] = Convert.ToDecimal(txtCONSUMABLESQTY.Text);
                drQty["CONSUMABLESQTY"] = Convert.ToString(ConsumbleQty).Trim();
                drQty["WASTAGEQTY"] = Convert.ToDecimal(txtWastage.Text);
                dt.Rows.Add(drQty);
                dt.AcceptChanges();
            }
            else
            {
                Session["MATERIALNAME"] = lblMATERIALNAME.Text.Trim();
                result = 1;
                break;
            }
            //}
        }
        if (result == 1)
        {
            MessageBox1.ShowInfo("Issue Qty must be less than or equal to Wastage Qty <b><font color='red'> " + Session["MATERIALNAME"] + " </font></b>... ", 60, 500);
        }
        else
        {
            if (count > 0)
            {
                HttpContext.Current.Session["RMPMWASTAGE"] = dt;
                XML = ConvertDatatable(dt);
                id = clsPOC.SaveReworkingWastage(ddlproductionno.SelectedValue.ToString(), ddlbatchno.SelectedItem.Value.ToString(), ddlBulkProduct.SelectedValue.ToString(), ddlBulkProduct.SelectedItem.ToString(), HttpContext.Current.Session["FINYEAR"].ToString().Trim(), XML, HttpContext.Current.Session["DEPOTID"].ToString().Trim());
                this.gvProductionWastage.DataSource = null;
                this.gvProductionWastage.DataBind();
            }
            if (count == 0)
            {
                MessageBox1.ShowInfo("Plese Select atleast 1 record... ");
            }
            else
            {
                if (id > 0)
                {
                    MessageBox1.ShowSuccess("Record saved successfully..");
                    this.gvProductionWastage.DataSource = null;
                    this.gvProductionWastage.DataBind();
                    HttpContext.Current.Session["RMPMWASTAGE"] = null;
                    Session["MATERIALNAME"] = null;
                    result = 0;
                    ddlBulkProduct.SelectedValue = "0";
                    ddlproductionno.SelectedValue = "0";
                }
                else
                {
                    MessageBox1.ShowInfo("Error Saving Record ");
                    HttpContext.Current.Session["RMPMWASTAGE"] = null;
                    result = 0;
                }
            }
        }
    }

    #region Add FinYear Wise Date Lock
    public void DateLock()
    {
        try
        {
            string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
            string startyear = finyear.Substring(0, 4);
            int startyear1 = Convert.ToInt32(startyear);
            string endyear = finyear.Substring(5);
            int endyear1 = Convert.ToInt32(endyear);
            DateTime oDate = new DateTime(startyear1, 04, 01);
            DateTime cDate = new DateTime(endyear1, 03, 31);
            DateTime today1 = DateTime.Now;
            CalendarFromDate.StartDate = oDate;
            CalendarExtenderToDate.StartDate = oDate;

            if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
            {
                this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                CalendarFromDate.EndDate = today1;
                CalendarExtenderToDate.EndDate = today1;
            }
            else
            {
                this.txtFromDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtToDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                CalendarFromDate.EndDate = cDate;
                CalendarExtenderToDate.EndDate = cDate;
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    protected void ddlBulkProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dtLoadPO = new DataTable();
            string DepotId = HttpContext.Current.Session["DEPOTID"].ToString().Trim();
            dtLoadPO = clsPOC.LoadWastegePono(txtFromDate.Text, txtToDate.Text, HttpContext.Current.Session["FINYEAR"].ToString(), DepotId, ddlBulkProduct.SelectedValue.ToString());
            if (dtLoadPO.Rows.Count > 0)
            {
                this.ddlproductionno.Items.Clear();
                this.ddlproductionno.AppendDataBoundItems = true;
                this.ddlproductionno.Items.Add(new ListItem("Select Po No", "0"));
                this.ddlproductionno.DataSource = dtLoadPO;
                this.ddlproductionno.DataValueField = "POID";
                this.ddlproductionno.DataTextField = "PONO";
                this.ddlproductionno.DataBind();
            }
            else
            {
                this.ddlproductionno.Items.Clear();
                this.ddlproductionno.AppendDataBoundItems = true;
                this.ddlproductionno.Items.Add(new ListItem("Select Po No", "0"));
                this.ddlproductionno.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }

    protected void ddlproductionno_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtBatch = new DataTable();
        this.ddlbatchno.Items.Clear();
        this.ddlbatchno.AppendDataBoundItems = true;
        //this.ddlbatchno.Items.Add(new ListItem("Select Batch No", "0"));
        dtBatch = clsPOC.LoadWastegeBatchNO(ddlproductionno.SelectedValue.ToString());
        this.ddlbatchno.DataSource = dtBatch;
        this.ddlbatchno.DataValueField = "BATCHNO";
        this.ddlbatchno.DataTextField = "BATCHNO";
        this.ddlbatchno.DataBind();
    }
}