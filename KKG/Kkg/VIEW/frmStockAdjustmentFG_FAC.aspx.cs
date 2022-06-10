using BAL;
using Obout.Grid;
using PPBLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public class JournalBatchdetails
{
    public string BATCHNO { get; set; }
    public string STOCKQTY { get; set; }
    public string ID { get; set; }
    public string MRP { get; set; }
    public string ASSESMENTPERCENTAGE { get; set; }
    public string MFDATE { get; set; }
    public string EXPRDATE { get; set; }
    public string RATE { get; set; }
}

public partial class FACTORY_frmStockAdjustmentFG_FAC : System.Web.UI.Page
{
    string MENUID = string.Empty;
    string Checker = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.DateLock();
                this.BackDtSetting();
                this.pnlAdd.Style["display"] = "none";
                this.pnlDisplay.Style["display"] = "";
                this.LoadStoreloacation();
                this.LoadAdjustmentDetails();
                this.LoadBusinessSegment();
                this.pnlreserve.Style["display"] = "none";
                MENUID = Request.QueryString["MENUID"].ToString().Trim();
                Checker = Request.QueryString["CHECKER"].ToString().Trim();
                if (Checker == "TRUE")
                {
                    this.btnaddhide.Visible = false;
                    this.divbtnsave.Visible = false;
                    this.divbtnapprove.Visible = true;
                    this.divbtnreject.Visible = true;
                }
                else
                {
                    this.btnaddhide.Visible = true;
                    this.divbtnsave.Visible = true;
                    this.divbtnapprove.Visible = false;
                    this.divbtnreject.Visible = false;
                }
                if (HttpContext.Current.Session["DEPOTID"].ToString() == "0EEDDA49-C3AB-416A-8A44-0B9DFECD6670")
                {
                    this.divstockreserve.Style["display"] = "none";
                }
                else
                {
                    this.divstockreserve.Style["display"] = "none";
                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    public void LoadDepo()
    {
        ClsStockAdjustment_FAC clsstockadjustment = new ClsStockAdjustment_FAC();
        ddlfromdepot.Items.Clear();
        //ddlfromdepot.Items.Add(new ListItem("-- SELECT DEPOT NAME --", "0"));
        ddlfromdepot.AppendDataBoundItems = true;
        ddlfromdepot.DataSource = clsstockadjustment.BindDepo(HttpContext.Current.Session["DEPOTID"].ToString());
        ddlfromdepot.DataValueField = "BRID";
        ddlfromdepot.DataTextField = "BRNAME";
        ddlfromdepot.DataBind();
    }

    public void LoadStoreloacation()
    {
        ClsStockAdjustment_FAC clsstockadjustment = new ClsStockAdjustment_FAC();
        ddlstorelocation.Items.Clear();
        ddlstorelocation.Items.Add(new ListItem("SELECT STORE LOCATION", "0"));
        ddlstorelocation.AppendDataBoundItems = true;
        ddlstorelocation.DataSource = clsstockadjustment.BindStorelocation();
        ddlstorelocation.DataValueField = "ID";
        ddlstorelocation.DataTextField = "Name";
        ddlstorelocation.DataBind();
    }

    #region LoadCategory
    public void LoadCategory()
    {
        try
        {
            ClsProductMaster clsProduct = new ClsProductMaster();
            ddlCategory.Items.Clear();
            ddlCategory.Items.Add(new ListItem("--- ALL ---", "0"));
            ddlCategory.AppendDataBoundItems = true;
            ddlCategory.DataSource = clsProduct.BindCategory();
            ddlCategory.DataValueField = "CATID";
            ddlCategory.DataTextField = "CATNAME";
            ddlCategory.DataBind();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";

        }

    }
    #endregion

    #region ddlCategory_SelectedIndexChanged
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.LoadProduct();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlproductname.ClientID + "').focus(); ", true);

    }
    #endregion

    #region LoadBusinessSegment
    public void LoadBusinessSegment()
    {
        try
        {
            ClsStockAdjustment_FAC clsstockadjustment = new ClsStockAdjustment_FAC();
            DataTable dt = clsstockadjustment.BindBusinessegment();
            if (dt.Rows.Count > 0)
            {
                ddlbsegment.Items.Clear();
                ddlbsegment.Items.Add(new ListItem("ALL", "0"));
                ddlbsegment.AppendDataBoundItems = true;
                ddlbsegment.DataSource = dt;
                ddlbsegment.DataValueField = "ID";
                ddlbsegment.DataTextField = "NAME";
                ddlbsegment.DataBind();
            }
            else
            {
                ddlbsegment.Items.Clear();
                ddlbsegment.Items.Add(new ListItem("Select", "0"));
                ddlbsegment.AppendDataBoundItems = true;
            }
        }

        catch (Exception ex)
        {
            string message = ex.Message;
        }
    }
    #endregion

    public void LoadReason()
    {
        ClsStockAdjustment_FAC clsstockadjustment = new ClsStockAdjustment_FAC();
        ddlreason.Items.Clear();
        ddlreason.Items.Add(new ListItem("SELECT REASON NAME", "0"));
        ddlreason.AppendDataBoundItems = true;
        ddlreason.DataSource = clsstockadjustment.BindReason(Request.QueryString["MENUID"].ToString());
        ddlreason.DataValueField = "ID";
        ddlreason.DataTextField = "DESCRIPTION";
        ddlreason.DataBind();
    }
    protected void ddlfromdepot_SelectedChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlfromdepot.SelectedValue == "0")
            {
                ddlproductname.Items.Clear();
                ddlproductname.Items.Add(new ListItem("SELECT PRODUCT NAME", "0"));
                ddlproductname.AppendDataBoundItems = true;

                ddlbatchno.Items.Clear();
                ddlbatchno.Items.Add(new ListItem("SELECT BATCH NO", "0"));
                ddlbatchno.AppendDataBoundItems = true;

                ddlpackingsize.Items.Clear();
                ddlpackingsize.Items.Add(new ListItem("SELECT PACK SIZE", "0"));
                ddlpackingsize.AppendDataBoundItems = true;

                ddlreason.Items.Clear();
                ddlreason.Items.Add(new ListItem("SELECT REASON NAME", "0"));
                ddlreason.AppendDataBoundItems = true;

                txtprice.Text = "";
                txtstockqty.Text = "";
                txtadjustmentqty.Text = "";
            }
            else
            {
                LoadProduct();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    public void LoadProduct()
    {
        ClsStockAdjustment_FAC clsstockadjustment = new ClsStockAdjustment_FAC();
        DataTable dtproduct = clsstockadjustment.BindProductCategoryWise(ddlfromdepot.SelectedValue.ToString(), this.ddlCategory.SelectedValue.Trim(), this.rdbtype.SelectedValue.Trim());
        if (dtproduct.Rows.Count > 0)
        {
            ddlproductname.Items.Clear();
            ddlproductname.Items.Add(new ListItem("SELECT PRODUCT NAME", "0"));
            ddlproductname.AppendDataBoundItems = true;
            //ddlproductname.DataSource = clsstocktransfer.BindProduct(ddlfromdepot.SelectedValue.ToString());
            DataTable uniqueCols = dtproduct.DefaultView.ToTable(true, "PRODUCTID", "PRODUCTNAME");
            ddlproductname.DataSource = uniqueCols;
            ddlproductname.DataValueField = "PRODUCTID";
            ddlproductname.DataTextField = "PRODUCTNAME";
            ddlproductname.DataBind();
        }
        else
        {
            ddlproductname.Items.Clear();
            ddlproductname.Items.Add(new ListItem("SELECT PRODUCT NAME", "0"));
        }
    }
    protected void ddlproductname_SelectedChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlproductname.SelectedValue == "0")
            {
                ddlbatchno.Items.Clear();
                ddlbatchno.Items.Add(new ListItem("SELECT BATCH NO", "0"));
                ddlbatchno.AppendDataBoundItems = true;

                ddlpackingsize.Items.Clear();
                ddlpackingsize.Items.Add(new ListItem("SELECT PACK SIZE", "0"));
                ddlpackingsize.AppendDataBoundItems = true;

                ddlreason.Items.Clear();
                ddlreason.Items.Add(new ListItem("SELECT REASON NAME", "0"));
                ddlreason.AppendDataBoundItems = true;

                txtprice.Text = "";
                txtstockqty.Text = "";
                txtadjustmentqty.Text = "";
            }
            else
            {
                ClsStockAdjustment_FAC clsstockadjustment = new ClsStockAdjustment_FAC();
                ClsStockJournal ClsStock = new ClsStockJournal();
                ddlpackingsize.Items.Clear();
                ddlpackingsize.Items.Add(new ListItem("SELECT PACK SIZE", "0"));
                ddlpackingsize.AppendDataBoundItems = true;
                DataTable dt = new DataTable();
                dt = ClsStock.BindPackingSize(ddlproductname.SelectedValue.ToString());
                if (dt.Rows.Count > 0)
                {
                    ddlpackingsize.DataSource = dt;
                    ddlpackingsize.DataValueField = "PACKSIZEID_FROM";
                    ddlpackingsize.DataTextField = "PACKSIZEName_FROM";
                    ddlpackingsize.DataBind();
                    ddlpackingsize.SelectedValue = Convert.ToString(dt.Rows[0]["PACKSIZEID_FROM"]).Trim();
                    if (this.ddlstorelocation.SelectedValue != "0")
                    {
                        if (ddlpackingsize.SelectedValue != "0" && this.ddlproductname.SelectedValue != "0")
                        {
                            this.LoadBatchDetails(this.ddlproductname.SelectedValue.Trim(), this.ddlpackingsize.SelectedValue.Trim(), this.ddlfromdepot.SelectedValue.Trim(), "0", this.ddlstorelocation.SelectedValue.Trim(), this.rdbtype.SelectedValue.Trim());
                            hdn_editedstockqty.Value = "";
                            hdn_lockadjustmentqty.Value = "";
                            txtstockqty.Text = "0";
                            txtadjustmentqty.Text = "";
                            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlpackingsize.ClientID + "').focus(); ", true);
                        }
                    }
                    else
                    {
                        MessageBox1.ShowInfo("Please Select Store Loaction...");
                        this.ddlproductname.SelectedValue = "0";
                        return;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #region BatchDetails
    protected void BatchDetails()
    {
        if (this.ddlproductname.SelectedValue != "0" && this.ddlpackingsize.SelectedValue != "0")
        {
            this.txtbatchno.Text = ddlbatchno.SelectedItem.Text.Substring(0, 15).Trim();
            this.txtmrp.Text = ddlbatchno.SelectedItem.Text.Substring(33, 11).Trim();
            this.txtstockqty.Text = ddlbatchno.SelectedItem.Text.Substring(18, 15).Trim();
            this.hdn_ASSESMENTPERCENTAGE.Value = ddlbatchno.SelectedItem.Text.Substring(56, 14).Trim();
            this.txtassesment.Text = hdn_ASSESMENTPERCENTAGE.Value;
            this.txtprice.Text = ddlbatchno.SelectedItem.Text.Substring(44, 12).Trim();
            this.hdn_mfgdate.Value = ddlbatchno.SelectedItem.Text.Substring(70, 14).Trim();
            this.hdn_exprdate.Value = ddlbatchno.SelectedItem.Text.Substring(84, 14).Trim();
            this.txtmfgdate.Text = hdn_mfgdate.Value;
            this.txtexpdate.Text = hdn_exprdate.Value;

            string lockadjustmentqty = "0";
            string editedsqty = "0";
            hdn_stockqtyinpcs.Value = this.txtstockqty.Text.Trim();
            hdn_editedstockqty.Value = editedsqty;
            hdn_lockadjustmentqty.Value = lockadjustmentqty;
        }
        else
        {
            this.txtmrp.Text = "";
            this.txtmrp.Text = "";
            this.txtstockqty.Text = "0";
            this.hdn_ASSESMENTPERCENTAGE.Value = "";
            this.txtprice.Text = "";
            this.hdn_mfgdate.Value = "";
            this.hdn_exprdate.Value = "";
        }
    }
    #endregion

    protected void ddlbatchno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClsStockTransfer clsstocktransfer = new ClsStockTransfer();
            if (ddlbatchno.SelectedValue == "0" && this.ddlpackingsize.SelectedValue == "0")
            {
                txtprice.Text = "";
                txtstockqty.Text = "";

                ddlreason.Items.Clear();
                ddlreason.Items.Add(new ListItem("SELECT REASON NAME", "0"));
                ddlreason.AppendDataBoundItems = true;

                ddlpackingsize.Items.Clear();
                ddlpackingsize.Items.Add(new ListItem("SELECT PACK SIZE", "0"));
                ddlpackingsize.AppendDataBoundItems = true;
            }
            else
            {
                this.BatchDetails();
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + this.txtadjustmentqty.ClientID + "').focus(); ", true);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + this.ddlstorelocation.ClientID + "').focus(); ", true);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + this.btnadd.ClientID + "').focus(); ", true);
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #region LoadBatchDetails
    public void LoadBatchDetails(string ProductID, string PacksizeID, string DepotID, string BatchNo, string StoreLocation, string Mode)
    {
        try
        {
            ClsStockAdjustment_FAC clsAdj = new ClsStockAdjustment_FAC();
            DataTable dtBatch = new DataTable();
            DataTable dtBaseCost = new DataTable();
            //dtBatch = clsAdj.BindBatchDetails(DepotID, ProductID, PacksizeID, BatchNo, StoreLocation);
            if (Mode == "G")
            {
                txtbatchno.Enabled = false;
                txtprice.Enabled = false;
                txtmrp.Enabled = false;
                txtmfgdate.Enabled = false;
                txtexpdate.Enabled = false;
                ImageButtonMFDate.Enabled = false;
                ImageButtonExprDate.Enabled = false;
                txtassesment.Enabled = false;

                dtBatch = clsAdj.BindBatchDetails(DepotID, ProductID, PacksizeID, BatchNo, StoreLocation);

                List<JournalBatchdetails> BatchDetails = new List<JournalBatchdetails>();
                if (dtBatch.Rows.Count > 0)
                {
                    this.ddlbatchno.Items.Clear();
                    this.ddlbatchno.Items.Add(new ListItem("SELECT BATCHNO", "0"));

                    for (int i = 0; i < dtBatch.Rows.Count; i++)
                    {
                        JournalBatchdetails po = new JournalBatchdetails();
                        po.BATCHNO = dtBatch.Rows[i]["BATCHNO"].ToString();
                        po.STOCKQTY = dtBatch.Rows[i]["INVOICESTOCKQTY"].ToString();
                        po.ID = dtBatch.Rows[i]["BATCHNO"].ToString() + '|' + dtBatch.Rows[i]["MRP"].ToString() + dtBatch.Rows[i]["MFGDATE"].ToString() + dtBatch.Rows[i]["EXPIRDATE"].ToString();
                        po.MRP = dtBatch.Rows[i]["MRP"].ToString();
                        decimal rate = clsAdj.BindDepotRate(this.ddlproductname.SelectedValue.Trim(), Convert.ToDecimal(dtBatch.Rows[i]["MRP"].ToString().Trim()));

                        po.RATE = rate.ToString().Trim();

                        po.ASSESMENTPERCENTAGE = dtBatch.Rows[i]["ASSESMENTPERCENTAGE"].ToString();
                        po.MFDATE = dtBatch.Rows[i]["MFGDATE"].ToString();
                        po.EXPRDATE = dtBatch.Rows[i]["EXPIRDATE"].ToString();

                        BatchDetails.Add(po);
                    }

                    //---------------------------------------------------------------------------------------------

                    string text1 = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                    "BATCHNO".PadRight(18, '\u00A0'),
                    "STOCKQTY".PadRight(15, '\u00A0'),
                    "MRP".PadRight(11, '\u00A0'),
                    "RATE".PadRight(12, '\u00A0'),
                    "ASSESMENT(%)".PadRight(14, '\u00A0'),
                    "MFG DATE".PadRight(14, '\u00A0'),
                    "EXP DATE".PadRight(14, '\u00A0')
                    );

                    ddlbatchno.Items.Add(new ListItem(text1, "1"));

                    foreach (ListItem item in ddlbatchno.Items)
                    {
                        if (item.Value == "1")
                        {
                            item.Attributes.Add("disabled", "disabled");
                            item.Attributes.CssStyle.Add("color", "blue");  // rgb(200, 35, 218)
                                                                            //item.Attributes.CssStyle.Add("background-color", "rgb(133, 217, 231)");
                        }
                    }

                    foreach (JournalBatchdetails p in BatchDetails)
                    {
                        string text = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                            p.BATCHNO.PadRight(18, '\u00A0'),
                            p.STOCKQTY.PadRight(15, '\u00A0'),
                            p.MRP.PadRight(11, '\u00A0'),
                            p.RATE.PadRight(12, '\u00A0'),
                            p.ASSESMENTPERCENTAGE.PadRight(14, '\u00A0'),
                            p.MFDATE.PadRight(14, '\u00A0'),
                            p.EXPRDATE.PadRight(14, '\u00A0')
                            );

                        ddlbatchno.Items.Add(new ListItem(text, "" + p.ID + ""));
                    }
                }
                else
                {
                    this.ddlbatchno.Items.Clear();
                    this.ddlbatchno.Items.Add(new ListItem("SELECT BATCHNO", "0"));
                    MessageBox1.ShowInfo("Stock not available");
                }
            }
            else
            {
                this.ddlbatchno.Items.Clear();
                this.ddlbatchno.Items.Add(new ListItem("SELECT BATCHNO", "0"));
                txtbatchno.Enabled = true;
                txtprice.Enabled = true;
                txtmrp.Enabled = true;
                txtmfgdate.Enabled = true;
                txtexpdate.Enabled = true;
                ImageButtonMFDate.Enabled = true;
                ImageButtonExprDate.Enabled = true;
                txtassesment.Enabled = true;

                txtbatchno.Text = "";
                txtprice.Text = "";
                txtmrp.Text = "";
                txtmfgdate.Text = "";
                txtexpdate.Text = "";
                txtassesment.Text = "";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    protected void ddlpackingsize_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlpackingsize.SelectedValue == "0" && this.ddlproductname.SelectedValue == "0")
            {
                txtstockqty.Text = "";
                hdn_stockqtyinpcs.Value = "";
                hdn_editedstockqty.Value = "";
                hdn_lockadjustmentqty.Value = "";
            }
            else if (ddlpackingsize.SelectedValue != "0" && this.ddlproductname.SelectedValue != "0")
            {
                this.LoadBatchDetails(this.ddlproductname.SelectedValue.Trim(), this.ddlpackingsize.SelectedValue.Trim(), this.ddlfromdepot.SelectedValue.Trim(), "0", this.ddlstorelocation.SelectedValue.Trim(), this.rdbtype.SelectedValue.Trim());
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    public DataTable BindAdjustmentGrid()
    {
        DataTable dtadjustment = new DataTable();
        dtadjustment.Columns.Add("GUID");
        dtadjustment.Columns.Add("PRODUCTID");
        dtadjustment.Columns.Add("PRODUCTNAME");
        dtadjustment.Columns.Add("BATCHNO");
        dtadjustment.Columns.Add("PACKINGSIZEID");
        dtadjustment.Columns.Add("PACKINGSIZENAME");
        dtadjustment.Columns.Add("ADJUSTMENTQTY");
        dtadjustment.Columns.Add("PRICE");
        dtadjustment.Columns.Add("AMOUNT");
        dtadjustment.Columns.Add("REASONID");
        dtadjustment.Columns.Add("REASONNAME");
        dtadjustment.Columns.Add("TAG");
        dtadjustment.Columns.Add("BEFOREEDITEDQTY");
        dtadjustment.Columns.Add("STORELOCATIONID");
        dtadjustment.Columns.Add("STORELOCATIONNAME");
        dtadjustment.Columns.Add("MFDATE");
        dtadjustment.Columns.Add("EXPRDATE");
        dtadjustment.Columns.Add("ASSESMENTPERCENTAGE");
        dtadjustment.Columns.Add("MRP");
        dtadjustment.Columns.Add("WEIGHT");

        HttpContext.Current.Session["STOCKADJUSTMENTRECORD"] = dtadjustment;
        return dtadjustment;
    }

    public int DatatableCheck(string pid, string pname, string batchno, decimal qty, string ReasonId)
    {
        int flag = 0;
        DataTable dtadjustment = (DataTable)HttpContext.Current.Session["STOCKADJUSTMENTRECORD"];

        if (dtadjustment.Rows.Count > 0)
        {
            int NumberofRecord = dtadjustment.Select("PRODUCTID='" + pid + "' AND PRODUCTNAME='" + pname + "' AND BATCHNO='" + batchno + "' AND REASONID='" + ReasonId + "'").Length;
            if (NumberofRecord > 0)
            {
                flag = 1;
            }
        }
        return flag;
    }

    public void BindAdjustmentDetails(string pid, string pname, string batchno, string packsizeid, string packsizename, decimal tqty, decimal price, string reasonid, string reasonname, string hdn_adjustmentid, decimal lockadjustmentqty, string storelocationid, string storelocation, string storeTolocationid, string storeTolocationname, string MFDATE, string EXPRDATE, string Assesment, string MRP, string Weight)
    {
        ClsStockAdjustment_FAC clsstockadjustment = new ClsStockAdjustment_FAC();
        DataTable dtadjustment = (DataTable)HttpContext.Current.Session["STOCKADJUSTMENTRECORD"];
        string GUID = Guid.NewGuid().ToString();
        decimal adjqtypcs = clsstockadjustment.CalculateQtyPcs(this.ddlproductname.SelectedValue, this.ddlpackingsize.SelectedValue, Convert.ToDecimal(this.txtadjustmentqty.Text.Trim()));
        decimal totalprice = price * Math.Abs(adjqtypcs);
        batchno = "NA";
        MFDATE= "01/01/1900";
        EXPRDATE = "01/01/1900";
        Assesment = "0.00";
        DataRow dr = dtadjustment.NewRow();
        dr["GUID"] = GUID;
        dr["PRODUCTID"] = pid;
        dr["PRODUCTNAME"] = pname;
        dr["BATCHNO"] = batchno;
        dr["PACKINGSIZEID"] = packsizeid;
        dr["PACKINGSIZENAME"] = packsizename;
        if (storeTolocationid.Trim() != "")
        {
            dr["ADJUSTMENTQTY"] = -1 * tqty;
        }
        else
        {
            dr["ADJUSTMENTQTY"] = tqty;
        }
        dr["PRICE"] = price.ToString("F");
        dr["AMOUNT"] = totalprice.ToString("F");
        dr["REASONID"] = reasonid;
        dr["REASONNAME"] = reasonname;
        dr["STORELOCATIONID"] = storelocationid;
        dr["STORELOCATIONNAME"] = storelocation;
        dr["MFDATE"] = MFDATE;
        dr["EXPRDATE"] = EXPRDATE;
        dr["ASSESMENTPERCENTAGE"] = Assesment == "" ? "0.00" : Assesment;
        dr["MRP"] = MRP;
        dr["WEIGHT"] = Weight;

        if (hdn_adjustmentid == "")
        {
            dr["TAG"] = "A";
        }
        else
        {
            dr["TAG"] = "U";
        }
        dr["BEFOREEDITEDQTY"] = lockadjustmentqty;

        dtadjustment.Rows.Add(dr);

        if (storeTolocationid.Trim() != "")
        {
            DataRow dr1 = dtadjustment.NewRow();

            dr1["GUID"] = GUID;
            dr1["PRODUCTID"] = pid;
            dr1["PRODUCTNAME"] = pname;
            dr1["BATCHNO"] = batchno;
            dr1["PACKINGSIZEID"] = packsizeid;
            dr1["PACKINGSIZENAME"] = packsizename;
            dr1["ADJUSTMENTQTY"] = tqty;
            dr1["PRICE"] = price.ToString("F");
            dr1["AMOUNT"] = totalprice.ToString("F");
            dr1["REASONID"] = reasonid;
            dr1["REASONNAME"] = reasonname;
            dr1["STORELOCATIONID"] = storeTolocationid;
            dr1["STORELOCATIONNAME"] = storeTolocationname;

            dr1["MFDATE"] = MFDATE;
            dr1["EXPRDATE"] = EXPRDATE;
            dr1["ASSESMENTPERCENTAGE"] = Assesment == "" ? "0.00" : Assesment;
            dr1["MRP"] = MRP;
            dr1["WEIGHT"] = Weight;

            if (hdn_adjustmentid == "")
            {
                dr1["TAG"] = "A";
            }
            else
            {
                dr1["TAG"] = "U";
            }
            dr1["BEFOREEDITEDQTY"] = lockadjustmentqty;

            dtadjustment.Rows.Add(dr1);
        }
        HttpContext.Current.Session["STOCKADJUSTMENTRECORD"] = dtadjustment;

        if (dtadjustment.Rows.Count > 0)
        {
            this.txtPosPCS.Text = "0";
            this.txtNegPCS.Text = "0";

            for (int i = 0; i < dtadjustment.Rows.Count; i++)
            {
                decimal AdjustmentQty = 0;
                AdjustmentQty = Convert.ToDecimal(dtadjustment.Rows[i]["ADJUSTMENTQTY"].ToString());
                if (AdjustmentQty > 0)
                {
                    this.txtPosPCS.Text = Convert.ToString(Convert.ToDecimal(this.txtPosPCS.Text.Trim()) + AdjustmentQty);
                }
                else
                {
                    this.txtNegPCS.Text = Convert.ToString(Convert.ToDecimal(this.txtNegPCS.Text.Trim()) + AdjustmentQty);
                }
            }
            this.gvadjustment.DataSource = dtadjustment;
            this.gvadjustment.DataBind();
        }
        else
        {
            this.gvadjustment.DataSource = null;
            this.gvadjustment.DataBind();
        }
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            ClsStockAdjustment_FAC clsstockadjustment = new ClsStockAdjustment_FAC();
            DataTable dtProductdetails = new DataTable();
            if (Session["STOCKADJUSTMENTRECORD"] == null)
            {
                this.BindAdjustmentGrid();
            }
            int flag = 0;
            decimal remainingqty = 0;
            string weight = "0";

            string pid = ddlproductname.SelectedValue.ToString();
            string pname = ddlproductname.SelectedItem.Text;
            string batchno = ddlbatchno.SelectedValue.ToString() == "0" ? txtbatchno.Text : ddlbatchno.SelectedValue.ToString();
            String[] tbatch = batchno.Split('|');
            batchno = tbatch[0].Trim();
            string packsizeid = ddlpackingsize.SelectedValue.ToString();
            string packname = ddlpackingsize.SelectedItem.Text;
            decimal adjustmentqty = Convert.ToDecimal(txtadjustmentqty.Text.Trim());
            decimal stockqty = Convert.ToDecimal(txtstockqty.Text.Trim());
            if (hdn_lockadjustmentqty.Value == "")
            {
                hdn_lockadjustmentqty.Value = "0";
                hdn_editedstockqty.Value = "0";
            }
            if (hdn_editedstockqty.Value == "")
            {
                hdn_editedstockqty.Value = "0";
              
            }
            decimal beforeeditedstockqty = Convert.ToDecimal(hdn_lockadjustmentqty.Value.ToString());
            decimal editedstockqty = Convert.ToDecimal(hdn_editedstockqty.Value.ToString());

            flag = DatatableCheck(pid, pname, batchno, adjustmentqty, this.ddlreason.SelectedValue.Trim());

            if (flag == 0)
            {
                remainingqty = (stockqty - Math.Abs(adjustmentqty));
                if (adjustmentqty == 0)
                {
                    MessageBox1.ShowInfo("<b>Adjustment Qty should be greater than<font color='red'> 0</font></b> !", 40, 500);
                    return;
                }
                else if (remainingqty < 0 && adjustmentqty < 0)
                {
                    MessageBox1.ShowInfo("Stock <b><font color='red'>Jornal qty more than stock qty</font></b> !", 40, 450);
                    return;
                }
                else
                {
                    DataSet dtweight = clsstockadjustment.GetWeight(this.ddlproductname.SelectedValue, this.ddlpackingsize.SelectedValue, Convert.ToDecimal(this.txtstockqty.Text.Trim()));
                    if (dtweight.Tables[0].Rows.Count > 0)
                    {
                        weight = dtweight.Tables[0].Rows[0]["NETWEIGHT"].ToString();
                    }
                    else
                    {
                        weight = "0";
                    }
                    this.BindAdjustmentDetails(pid, pname, batchno, packsizeid, packname, adjustmentqty, Convert.ToDecimal(txtprice.Text.Trim()), ddlreason.SelectedValue.ToString(), ddlreason.SelectedItem.Text, hdn_adjustmentid.Value.ToString(), beforeeditedstockqty, ddlstorelocation.SelectedValue.ToString(), ddlstorelocation.SelectedItem.Text, "", "", hdn_mfgdate.Value.Trim(), txtexpdate.Text.Trim()/*hdn_exprdate.Value.Trim()*/, hdn_ASSESMENTPERCENTAGE.Value.Trim(), txtmrp.Text.Trim(), weight);

                    ddlproductname.SelectedValue = "0";
                    ddlbatchno.Items.Clear();
                    ddlpackingsize.SelectedValue = "0";
                    txtadjustmentqty.Text = "";
                    txtstockqty.Text = "";
                    txtprice.Text = "";
                    txtmrp.Text = "";
                    hdn_editedstockqty.Value = "";
                    hdn_stockqtyinpcs.Value = "";
                    hdn_mfgdate.Value = "";
                    hdn_exprdate.Value = "";
                    hdn_ASSESMENTPERCENTAGE.Value = "";
                    txtbatchno.Text = "";
                }
            }
            else
            {
                MessageBox1.ShowInfo("<b>Product " + pname + " exist with same batchno with same reason!</b>", 40, 700);
                return;
            }
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + ddlproductname.ClientID + "').focus(); ", true);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void gvadjustment_RowDataBound(object sender, GridRowEventArgs e)
    {
        //try
        //{
        //    if (e.Row.RowType == GridRowType.DataRow)
        //    {
        //        GridDataControlFieldCell cell = e.Row.Cells[6] as GridDataControlFieldCell;

        //        decimal journalqty = 0;
        //        //totalqty += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "ADJUSTMENTQTY"));

        //        journalqty = Convert.ToDecimal(cell.Text);
        //        if (this.txtPosPCS.Text == "")
        //        {
        //            this.txtPosPCS.Text = "0";
        //        }
        //        if (this.txtNegPCS.Text == "")
        //        {
        //            this.txtNegPCS.Text = "0";
        //        }
        //        if (journalqty > 0)
        //        {

        //            this.txtPosPCS.Text =Convert.ToString((Convert.ToDecimal(this.txtPosPCS.Text) +journalqty));
        //        }
        //        else
        //        {
        //            this.txtNegPCS.Text = Convert.ToString((Convert.ToDecimal(this.txtNegPCS.Text) + journalqty));
        //        }

        //        //string qty = Convert.ToString(journalqty);
        //        //qty = "";

        //    }
        //}
        //catch (Exception ex)
        //{
        //    string message = "alert('" + ex.Message.Replace("'", "") + "')";
        //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        //}
    }

    #region Convert DataTable To XML
    public string ConvertDatatableToXML(DataTable dt)
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

    public int AdjustmentRecordsDelete(string GUID)
    {
        int delflag = 0;
        string pid = string.Empty;
        string batchno = string.Empty;
        decimal tqty = 0;
        DataTable dtadjustment = (DataTable)HttpContext.Current.Session["STOCKADJUSTMENTRECORD"];

        int i = dtadjustment.Rows.Count - 1;
        while (i >= 0)
        {
            if (Convert.ToString(dtadjustment.Rows[i]["GUID"]) == GUID)
            {
                pid = dtadjustment.Rows[i]["PRODUCTID"].ToString();
                batchno = dtadjustment.Rows[i]["BATCHNO"].ToString();
                tqty = Convert.ToDecimal(dtadjustment.Rows[i]["ADJUSTMENTQTY"].ToString());
                dtadjustment.Rows[i].Delete();
                dtadjustment.AcceptChanges();
                delflag = 1;
                //break;
            }
            i--;
        }

        HttpContext.Current.Session["STOCKADJUSTMENTRECORD"] = dtadjustment;
        return delflag;
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            string stockadjustmentno = string.Empty;
            string mode = string.Empty;
            DataTable dtadjustment = (DataTable)HttpContext.Current.Session["STOCKADJUSTMENTRECORD"];
            if (dtadjustment.Rows.Count > 0)
            {
                ClsStockAdjustment_FAC clsstockadjustment = new ClsStockAdjustment_FAC();
                string xml = ConvertDatatableToXML(dtadjustment);
                MENUID = Request.QueryString["MENUID"].ToString().Trim();
                stockadjustmentno = clsstockadjustment.InsertAdjustmentDetails(txtadjustmentdate.Text.Trim(), ddlfromdepot.SelectedValue.ToString(), ddlfromdepot.SelectedItem.Text, HttpContext.Current.Session["UserID"].ToString(), HttpContext.Current.Session["FINYEAR"].ToString(), Convert.ToString(hdn_adjustmentid.Value), xml, this.txtRemarks.Text.Trim(), MENUID);

                if (stockadjustmentno != "")
                {
                    string finyear = Session["FINYEAR"].ToString();

                    if (Convert.ToString(hdn_adjustmentid.Value) == "")
                    {
                        MessageBox1.ShowSuccess("Journal No :  <b><font color='green'>" + stockadjustmentno + "</font></b>  saved successfully", 60, 550);
                    }
                    else
                    {
                        MessageBox1.ShowSuccess("Journal No :  <b><font color='green'>" + stockadjustmentno + "</font></b> updated successfully", 60, 550);
                    }
                    this.txtadjustmentno.Text = stockadjustmentno;
                    this.btnaddhide.Style["display"] = "";
                    this.pnlAdd.Style["display"] = "none";
                    this.pnlDisplay.Style["display"] = "";
                    this.LoadAdjustmentDetails();
                    this.hdn_adjustmentid.Value = "";
                    this.btnadd.Visible = true;
                    this.divbtnsave.Visible = false;
                    this.btncancel.Visible = false;
                    this.btnnewentry.Visible = true;
                    this.pnlAdd.Enabled = false;
                    this.hdn_lockadjustmentqty.Value = "";
                    this.hdn_editedstockqty.Value = "";
                    this.hdn_stockqtyinpcs.Value = "";
                    this.hdn_guid.Value = "";
                    this.gvadjustment.ClearPreviousDataSource();
                    this.gvadjustment.DataSource = null;
                    this.gvadjustment.DataBind();
                    this.clear();
                    this.ResetTable();
                    this.trAdd.Style["display"] = "";
                    this.divbtnsave.Visible = true;
                    this.ddlCategory.SelectedValue = "0";
                    this.Session["STOCKADJUSTMENTRECORD"] = null;
                }
                else
                {
                    MessageBox1.ShowError("Error on Saving record..");
                }
            }
            else
            {
                MessageBox1.ShowInfo("Please add atleast 1 record");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    public void clear()
    {
        this.txtadjustmentno.Text = "";
        this.ddlproductname.SelectedValue = "0";
        this.ddlbatchno.SelectedValue = "0";
        this.ddlpackingsize.SelectedValue = "0";
        this.ddlreason.SelectedValue = "0";
        this.txtadjustmentqty.Text = "";
        this.txtstockqty.Text = "";
        this.txtprice.Text = "";
        this.txtmrp.Text = "";
        this.txtRemarks.Text = "";
        this.ddlstorelocation.SelectedValue = "0";
        this.Session["STOCKADJUSTMENTRECORD"] = null;
    }
    public void ResetTable()
    {
        Session.Remove("STOCKADJUSTMENTRECORD");
    }
    protected void btnSTfind_Click(object sender, EventArgs e)
    {
        try
        {
            LoadAdjustmentDetails();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    public void LoadAdjustmentDetails()
    {
        try
        {
            ClsStockAdjustment_FAC clsstockadjustment = new ClsStockAdjustment_FAC();
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            this.gvStockAdjustmentDetails.DataSource = clsstockadjustment.BindAdjustmentControl(txtfromdateins.Text, txttodateins.Text, HttpContext.Current.Session["DEPOTID"].ToString(), HttpContext.Current.Session["FINYEAR"].ToString().Trim(), Checker);
            this.gvStockAdjustmentDetails.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        try
        {
            this.hdn_adjustmentid.Value = "";
            this.btnnewentry.Visible = true;
            this.gvadjustment.DataSource = null;
            this.gvadjustment.DataBind();
            this.LoadAdjustmentDetails();
            this.ResetTable();
            this.clear();
            this.pnlAdd.Style["display"] = "none";
            this.btnaddhide.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "";
            this.ddlfromdepot.SelectedValue = "0";
            this.gvadjustment.ClearPreviousDataSource();
            this.gvadjustment.DataSource = null;
            this.gvadjustment.DataBind();
            //this.ddlCategory.SelectedValue = "0";
            this.LoadCategory();

            this.ddlstorelocation.SelectedValue = "0";

            this.trAdd.Style["display"] = "";
            this.divbtnsave.Visible = true;
            this.txtPosPCS.Text = "";
            this.txtNegPCS.Text = "";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnnewentry_Click(object sender, EventArgs e)
    {
        try
        {
            this.hdn_adjustmentid.Value = "";
            this.LoadCategory();
            this.BindAdjustmentGrid();
            this.gvadjustment.ClearPreviousDataSource();
            this.gvadjustment.DataSource = null;
            this.gvadjustment.DataBind();
            this.pnlAdd.Enabled = true;
            this.imgPopuppodate.Visible = true;
            this.btnaddhide.Style["display"] = "none";
            this.tradjunmentno.Style["display"] = "none";
            this.tradjunmentheader.Style["display"] = "none";
            this.pnlAdd.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.btnadd.Visible = true;
            this.divbtnsave.Visible = true;
            this.btncancel.Visible = true;
            this.ResetTable();
            this.clear();
            this.LoadDepo();
            this.LoadProduct();
            this.LoadAdjustmentDetails();
            this.LoadReason();
            this.ddlfromdepot.Enabled = true;
            this.ddlstorelocation.SelectedValue = "0";
            this.trAdd.Style["display"] = "";
            this.divbtnsave.Visible = true;
            this.txtPosPCS.Text = "";
            this.txtNegPCS.Text = "";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btngrddelete_Click(object sender, EventArgs e)
    {
        try
        {
            ClsStockAdjustment_FAC clsstockadjustment = new ClsStockAdjustment_FAC();
            int flag = 0;
            string guid = hdn_guid.Value.ToString().Trim();

            if (clsstockadjustment.Getstatus(guid) == "1")
            {
                MessageBox1.ShowInfo("Day End Operation already done,not allow to edit.", 50, 450);
                return;
            }

            DataTable dtadjustment = (DataTable)HttpContext.Current.Session["STOCKADJUSTMENTRECORD"];
            flag = AdjustmentRecordsDelete(guid);

            if (flag == 1)
            {
                this.gvadjustment.DataSource = dtadjustment;
                this.gvadjustment.DataBind();

                this.ddlproductname.SelectedValue = "0";
                this.ddlbatchno.SelectedValue = "0";
                this.ddlpackingsize.SelectedValue = "0";
                this.txtadjustmentqty.Text = "";
                this.hdn_guid.Value = "";
                this.txtPosPCS.Text = "0";
                this.txtNegPCS.Text = "0";
                if (dtadjustment.Rows.Count > 0)
                {
                    for (int i = 0; i < dtadjustment.Rows.Count; i++)
                    {
                        decimal AdjustmentQty = 0;
                        AdjustmentQty = Convert.ToDecimal(dtadjustment.Rows[i]["ADJUSTMENTQTY"].ToString());
                        if (AdjustmentQty > 0)
                        {
                            this.txtPosPCS.Text = Convert.ToString(Convert.ToDecimal(this.txtPosPCS.Text.Trim()) + AdjustmentQty);
                        }
                        else
                        {
                            this.txtNegPCS.Text = Convert.ToString(Convert.ToDecimal(this.txtNegPCS.Text.Trim()) + AdjustmentQty);
                        }
                    }
                }

                MessageBox1.ShowSuccess("Record deleted successfully!");
            }
            else
            {
                MessageBox1.ShowError("Record deleted unsuccessful!");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btngrdedit_Click(object sender, EventArgs e)
    {
        try
        {
            this.txtPosPCS.Text = "";
            this.txtNegPCS.Text = "";
            ClsStockAdjustment_FAC clsstockadjustment = new ClsStockAdjustment_FAC();
            if (clsstockadjustment.Getstatus(hdn_adjustmentid.Value.Trim()) == "1")
            {
                MessageBox1.ShowInfo("Day End Operation already done,not allow to edit.", 40, 450);
                return;
            }
            this.BindAdjustmentGrid();
            this.LoadReason();
            this.LoadDepo();
            DataTable dtadjustment = (DataTable)HttpContext.Current.Session["STOCKADJUSTMENTRECORD"];

            //this.imgPopuppodate.Visible = false;
            this.tradjunmentno.Style["display"] = "";
            this.tradjunmentheader.Style["display"] = "";
            string adjustmentid = hdn_adjustmentid.Value.ToString();
            DataTable dteditedadjustmentrecord = new DataTable();
            dteditedadjustmentrecord = clsstockadjustment.BindAdjustmentHeader(hdn_adjustmentid.Value.Trim());
            if (dteditedadjustmentrecord.Rows.Count > 0)
            {
                this.txtadjustmentno.Text = dteditedadjustmentrecord.Rows[0]["ADJUSTMENTNO"].ToString();
                this.txtadjustmentdate.Text = dteditedadjustmentrecord.Rows[0]["ADJUSTMENTDATE"].ToString();
                this.ddlfromdepot.SelectedValue = dteditedadjustmentrecord.Rows[0]["DEPOTID"].ToString();
                this.txtRemarks.Text = dteditedadjustmentrecord.Rows[0]["REMARKS"].ToString();
            }
            this.ddlCategory.SelectedValue = "0";
            this.LoadCategory();
            this.LoadProduct();

            dtadjustment = clsstockadjustment.BindAdjustmentDetails(adjustmentid);
            if (dtadjustment.Rows.Count > 0)
            {
                this.gvadjustment.DataSource = dtadjustment;
                this.gvadjustment.DataBind();
                HttpContext.Current.Session["STOCKADJUSTMENTRECORD"] = dtadjustment;
            }
            else
            {
                this.gvadjustment.DataSource = null;
                this.gvadjustment.DataBind();
            }
            this.txtPosPCS.Text = "0";
            this.txtNegPCS.Text = "0";
            if (dtadjustment.Rows.Count > 0)
            {
                for (int i = 0; i < dtadjustment.Rows.Count; i++)
                {
                    decimal AdjustmentQty = 0;
                    AdjustmentQty = Convert.ToDecimal(dtadjustment.Rows[i]["ADJUSTMENTQTY"].ToString());
                    if (AdjustmentQty > 0)
                    {
                        this.txtPosPCS.Text = Convert.ToString(Convert.ToDecimal(this.txtPosPCS.Text.Trim()) + AdjustmentQty);
                    }
                    else
                    {
                        this.txtNegPCS.Text = Convert.ToString(Convert.ToDecimal(this.txtNegPCS.Text.Trim()) + AdjustmentQty);
                    }
                }
            }
            //if (Convert.ToString(dteditedadjustmentrecord.Rows[0]["ISVERIFIED"]).Trim() == "Y")
            //{
            //    this.trAdd.Style["display"] = "none";
            //    this.divbtnsave.Visible = false;
            //}
            //else
            //{
            //    this.trAdd.Style["display"] = "";
            //    this.divbtnsave.Visible = true;
            //}
            this.ddlfromdepot.Enabled = false;
            this.btnaddhide.Style["display"] = "none";
            this.pnlAdd.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.btnadd.Visible = true;
            this.btncancel.Visible = true;
            this.btnnewentry.Visible = false;

            #region QueryString
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            if (Checker == "TRUE" && clsstockadjustment.GetApproveStatus(adjustmentid) == "1")
            {
                this.divbtnsave.Visible = false;
                this.divbtnapprove.Visible = false;
                this.divbtnreject.Visible = false;
                this.divadd.Visible = false;
            }
            else if (Checker == "TRUE" && clsstockadjustment.GetApproveStatus(adjustmentid) == "0")
            {
                this.divbtnsave.Visible = false;
                this.divbtnapprove.Visible = true;
                this.divbtnreject.Visible = true;
                this.divadd.Visible = false;
            }
            else
            {
                if (clsstockadjustment.GetApproveStatus(adjustmentid) == "1")
                {
                    this.divbtnsave.Visible = false;
                }
                else
                {
                    this.divbtnsave.Visible = false;
                }
            }
            #endregion
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnaddreserve_Click(object sender, EventArgs e)
    {
        try
        {
            string status = string.Empty;
            ClsStockAdjustment_FAC clsstockadjustment = new ClsStockAdjustment_FAC();
            status = clsstockadjustment.TReserve(this.txtreservedate.Text.Trim());

            if (status == "1")
            {
                MessageBox1.ShowSuccess("<b><font color='green'>Stock reserve successfully.</font></b>");
            }
            else if (status == "0")
            {
                MessageBox1.ShowInfo("<b>Stock reserve already done with selected date.</b>", 40, 400);
            }
            else
            {
                MessageBox1.ShowError("Error on reserve record..");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btncancelreserver_Click(object sender, EventArgs e)
    {
        try
        {
            this.pnlreserve.Style["display"] = "none";
            this.pnlAdd.Style["display"] = "none";
            this.pnlDisplay.Style["display"] = "";
            this.btnaddhide.Style["display"] = "";
            //this.divstockreserve.Style["display"] = "";
            this.divstockreserve.Style["display"] = "none";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnstockreserve_Click(object sender, EventArgs e)
    {
        try
        {
            this.pnlreserve.Style["display"] = "";
            this.pnlAdd.Style["display"] = "none";
            this.pnlDisplay.Style["display"] = "none";
            this.btnaddhide.Style["display"] = "none";
            this.divstockreserve.Style["display"] = "none";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        try
        {
            ClsStockAdjustment_FAC clsstockadjustment = new ClsStockAdjustment_FAC();
            DataTable dtbatch = new DataTable();
            dtbatch = clsstockadjustment.BatchDetails(this.txtreservedate.Text.Trim(), this.ddlbsegment.SelectedValue.Trim());

            if (dtbatch.Rows.Count > 0)
            {
                this.grdstockdetails.DataSource = dtbatch;
                this.grdstockdetails.DataBind();
            }
            else
            {
                this.grdstockdetails.DataSource = null;
                this.grdstockdetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #region btnPrint_Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string upath = string.Empty;
            upath = "frmRptInvoicePrint_FAC.aspx?StkJrnl_ID=" + hdn_adjustmentid.Value.Trim() + "";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
            hdn_adjustmentid.Value = "";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region rdbtype_SelectedIndexChanged
    protected void rdbtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.LoadProduct();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region MFG Calculation
    protected void txtmfgdate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (this.ddlproductname.SelectedValue != "0")
            {
                ClsOpenStock clsopenstock = new ClsOpenStock();
                if (!string.IsNullOrEmpty(this.txtmfgdate.Text.Trim()))
                {
                    string PRODUCT = this.ddlproductname.SelectedValue;
                    string expdate = string.Empty;
                    expdate = clsopenstock.GetProductExpirydate(PRODUCT, txtmfgdate.Text.ToString());
                    txtexpdate.Text = expdate;

                    string lockadjustmentqty = "0";
                    string editedsqty = "0";
                    hdn_stockqtyinpcs.Value = this.txtstockqty.Text.Trim();
                    hdn_editedstockqty.Value = editedsqty;
                    hdn_lockadjustmentqty.Value = lockadjustmentqty;

                    this.hdn_mfgdate.Value = txtmfgdate.Text;
                    this.hdn_exprdate.Value = txtexpdate.Text;
                    this.hdn_ASSESMENTPERCENTAGE.Value = txtassesment.Text;
                }
            }
            else
            {
                this.txtmfgdate.Text = "";
                //MessageBox1.ShowInfo("<b>Please select product name!</b>");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

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
        Calendar1.StartDate = oDate;
        CalendarExtender4.StartDate = oDate;
        CalendarAdjustmentdate.StartDate = oDate;

        if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
        {
            this.txtfromdateins.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.txttodateins.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.txtadjustmentdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            Calendar1.EndDate = today1;
            CalendarExtender4.EndDate = today1;
            CalendarAdjustmentdate.EndDate = today1;
        }
        else
        {
            this.txtfromdateins.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
            this.txttodateins.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
            this.txtadjustmentdate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
            Calendar1.EndDate = cDate;
            CalendarExtender4.EndDate = cDate;
            CalendarAdjustmentdate.EndDate = cDate;
        }
    }
    #endregion

    #region Back Date Entry Validation
    public void BackDtSetting()
    {
        BAL.ClsCommonFunction ClsCommon = new BAL.ClsCommonFunction();
        int Flag = ClsCommon.CheckDate(Request.QueryString["MENUID"].ToString().Trim());
        if (Flag > 0)
        {
            this.imgPopuppodate.Enabled = true;
            this.CalendarAdjustmentdate.Enabled = true;
        }
        else
        {
            this.imgPopuppodate.Enabled = true;
            this.CalendarAdjustmentdate.Enabled = true;
        }
    }
    #endregion

    #region gvStockAdjustmentDetails_RowDataBound
    protected void gvStockAdjustmentDetails_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[7] as GridDataControlFieldCell;
                string status = cell.Text;

                if (status == "PENDING")
                {
                    cell.ForeColor = Color.Blue;
                }
                else if (status == "REJECTED")
                {
                    cell.ForeColor = Color.Red;
                }
                else if (status == "HOLD")
                {
                    cell.ForeColor = Color.Black;
                }
                else
                {
                    cell.ForeColor = Color.Green;
                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btnApprove_Click
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            ClsStockAdjustment_FAC ClsStockJournal = new ClsStockAdjustment_FAC();
            string Adjustmentid = hdn_adjustmentid.Value.ToString();
            int flag = 0;
            flag = ClsStockJournal.ApproveStockJournalFG(Adjustmentid);
            if (flag == 1)
            {
                this.pnlDisplay.Style["display"] = "";
                this.pnlAdd.Style["display"] = "none";
                this.LoadAdjustmentDetails();
                this.hdn_adjustmentid.Value = "";
                MessageBox1.ShowSuccess("Stock Journal RMPM: <b><font color='green'>" + this.txtadjustmentno.Text + "</font></b> Approved Successfully.", 60, 550);
            }
            else
            {
                MessageBox1.ShowError("Error on Approve Record..");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btnReject_Click
    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            this.pnlAdd.Style["display"] = "none";
            this.btnaddhide.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    protected void ddlstorelocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ddlproductname.SelectedValue = "0";
    }
}