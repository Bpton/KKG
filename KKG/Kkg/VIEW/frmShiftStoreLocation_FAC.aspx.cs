using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

public class JournalBatchdetails1
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

public partial class FACTORY_frmShiftStoreLocation_FAC : System.Web.UI.Page
{
    string Checker = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.pnlAdd.Style["display"] = "none";
            this.pnlDisplay.Style["display"] = "";            
            this.DateLock();
            /*Calender Control Date Range*/
            CalendarExtender3.EndDate = DateTime.Now;
            /****************************/
            this.LoadStoreloacation();
            this.LoadAdjustmentDetails();
            this.LoadToStoreloacation();
        }
    }

    public void LoadDepo()
    {
        ClsStockAdjustment_FAC clsstockadjustment = new ClsStockAdjustment_FAC();
        this.ddlfromdepot.Items.Clear();
        //ddlfromdepot.Items.Add(new ListItem("-- SELECT DEPOT NAME --", "0"));
        this.ddlfromdepot.AppendDataBoundItems = true;
        this.ddlfromdepot.DataSource = clsstockadjustment.BindDepo(HttpContext.Current.Session["DEPOTID"].ToString());
        this.ddlfromdepot.DataValueField = "BRID";
        this.ddlfromdepot.DataTextField = "BRNAME";
        this.ddlfromdepot.DataBind();
    }
    public void LoadStoreloacation()
    {
        ClsStockAdjustment_FAC clsstockadjustment = new ClsStockAdjustment_FAC();
        this.ddlstorelocation.Items.Clear();
        this.ddlstorelocation.Items.Add(new ListItem("-- SELECT FROM STORE LOCATION  --", "0"));
        this.ddlstorelocation.AppendDataBoundItems = true;
        this.ddlstorelocation.DataSource = clsstockadjustment.BindStorelocation();
        this.ddlstorelocation.DataValueField = "ID";
        this.ddlstorelocation.DataTextField = "Name";
        this.ddlstorelocation.DataBind();
    }
    public void LoadToStoreloacation()
    {
        ClsStockAdjustment_FAC clsstockadjustment = new ClsStockAdjustment_FAC();
        ddltostorelocation.Items.Clear();
        ddltostorelocation.Items.Add(new ListItem("-- SELECT TO STORE LOCATION  --", "0"));
        ddltostorelocation.AppendDataBoundItems = true;
        ddltostorelocation.DataSource = clsstockadjustment.BindStorelocation();
        ddltostorelocation.DataValueField = "ID";
        ddltostorelocation.DataTextField = "Name";
        ddltostorelocation.DataBind();
    }

    public void LoadReason()
    {
        ClsStockAdjustment_FAC clsstockadjustment = new ClsStockAdjustment_FAC();
        ddlreason.Items.Clear();
        ddlreason.Items.Add(new ListItem("-- SELECT REASON NAME --", "0"));
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
                ddlproductname.Items.Add(new ListItem("-- SELECT PRODUCT NAME --", "0"));
                ddlproductname.AppendDataBoundItems = true;

                ddlbatchno.Items.Clear();
                ddlbatchno.Items.Add(new ListItem("-- SELECT BATCH NO --", "0"));
                ddlbatchno.AppendDataBoundItems = true;

                ddlpackingsize.Items.Clear();
                ddlpackingsize.Items.Add(new ListItem("-- SELECT PACK SIZE --", "0"));
                ddlpackingsize.AppendDataBoundItems = true;

                ddlreason.Items.Clear();
                ddlreason.Items.Add(new ListItem("-- SELECT REASON NAME --", "0"));
                ddlreason.AppendDataBoundItems = true;

                txtprice.Text = "";
                txtstockqty.Text = "";
                txtadjustmentqty.Text = "";
            }
            else
            {
                MotherDepoChange();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    public void MotherDepoChange()
    {
        ClsStockAdjustment_FAC clsstockadjustment = new ClsStockAdjustment_FAC();
        DataTable dtproduct = clsstockadjustment.BindProduct(ddlfromdepot.SelectedValue.ToString());
        ddlproductname.Items.Clear();
        ddlproductname.Items.Add(new ListItem("-- SELECT PRODUCT NAME --", "0"));
        ddlproductname.AppendDataBoundItems = true;
        //ddlproductname.DataSource = clsstocktransfer.BindProduct(ddlfromdepot.SelectedValue.ToString());
        DataTable uniqueCols = dtproduct.DefaultView.ToTable(true, "PRODUCTID", "PRODUCTNAME");
        ddlproductname.DataSource = uniqueCols;
        ddlproductname.DataValueField = "PRODUCTID";
        ddlproductname.DataTextField = "PRODUCTNAME";
        ddlproductname.DataBind();
    }
    protected void ddlproductname_SelectedChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlproductname.SelectedValue == "0")
            {
                ddlbatchno.Items.Clear();
                ddlbatchno.Items.Add(new ListItem("-- SELECT BATCH NO --", "0"));
                ddlbatchno.AppendDataBoundItems = true;

                ddlpackingsize.Items.Clear();
                ddlpackingsize.Items.Add(new ListItem("-- SELECT PACK SIZE --", "0"));
                ddlpackingsize.AppendDataBoundItems = true;

                ddlreason.Items.Clear();
                ddlreason.Items.Add(new ListItem("-- SELECT REASON NAME --", "0"));
                ddlreason.AppendDataBoundItems = true;

                txtprice.Text = "";
                txtstockqty.Text = "";
                txtadjustmentqty.Text = "";
            }
            else
            {
                ClsStockAdjustment_FAC clsstockadjustment = new ClsStockAdjustment_FAC();
                ClsMMPoOrder clsMMPo = new ClsMMPoOrder();
                ddlpackingsize.Items.Clear();
                ddlpackingsize.Items.Add(new ListItem("-- SELECT PACK SIZE --", "0"));
                ddlpackingsize.AppendDataBoundItems = true;
                ddlpackingsize.DataSource = clsstockadjustment.BindPackingSize(ddlproductname.SelectedValue.ToString());
                ddlpackingsize.DataValueField = "PACKSIZEID_FROM";
                ddlpackingsize.DataTextField = "PACKSIZEName_FROM";
                ddlpackingsize.DataBind();

                hdn_editedstockqty.Value = "";
                hdn_lockadjustmentqty.Value = "";
                txtstockqty.Text = "";
                txtadjustmentqty.Text = "";
            }
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + this.ddlpackingsize.ClientID + "').focus(); ", true);
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
            this.txtmrp.Text = ddlbatchno.SelectedItem.Text.Substring(40, 11).Trim();
            this.txtstockqty.Text = ddlbatchno.SelectedItem.Text.Substring(25, 15).Trim();
            this.txtprice.Text = ddlbatchno.SelectedItem.Text.Substring(51, 12).Trim();

            string lockadjustmentqty = "0";
            string editedsqty = "0";
            hdn_stockqtyinpcs.Value = this.txtstockqty.Text.Trim();
            hdn_editedstockqty.Value = editedsqty;
            hdn_lockadjustmentqty.Value = lockadjustmentqty;
        }
        else
        {
            this.txtmrp.Text = "";
            this.txtstockqty.Text = "0";
            this.txtprice.Text = "";
        }
    }
    #endregion

    protected void ddlbatchno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlbatchno.SelectedValue == "0" && this.ddlpackingsize.SelectedValue == "0")
            {
                txtprice.Text = "";
                txtstockqty.Text = "";

                ddlreason.Items.Clear();
                ddlreason.Items.Add(new ListItem("-- SELECT REASON NAME --", "0"));
                ddlreason.AppendDataBoundItems = true;

                ddlpackingsize.Items.Clear();
                ddlpackingsize.Items.Add(new ListItem("-- SELECT PACK SIZE --", "0"));
                ddlpackingsize.AppendDataBoundItems = true;
            }
            else if (ddlbatchno.SelectedValue != "0" && this.ddlpackingsize.SelectedValue != "0")
            {
                this.BatchDetails();
            }
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + this.txtadjustmentqty.ClientID + "').focus(); ", true);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #region LoadBatchDetails
    public void LoadBatchDetails(string ProductID, string PacksizeID, string DepotID, string BatchNo, string StoreLocation)
    {
        try
        {
            ClsStockAdjustment_FAC clsAdj = new ClsStockAdjustment_FAC();
            DataTable dtBatch = new DataTable();
            DataTable dtBaseCost = new DataTable();
            dtBatch = clsAdj.BindBatchDetails(DepotID, ProductID, PacksizeID, BatchNo, StoreLocation);

            List<JournalBatchdetails1> BatchDetails = new List<JournalBatchdetails1>();
            if (dtBatch.Rows.Count > 0)
            {
                this.ddlbatchno.Items.Clear();
                this.ddlbatchno.Items.Add(new ListItem("SELECT BATCHNO", "0"));

                for (int i = 0; i < dtBatch.Rows.Count; i++)
                {
                    JournalBatchdetails1 po = new JournalBatchdetails1();
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
                "BATCHNO".PadRight(25, '\u00A0'),
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
                        item.Attributes.CssStyle.Add("color", "blue");
                    }
                }

                foreach (JournalBatchdetails1 p in BatchDetails)
                {
                    string text = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                        p.BATCHNO.PadRight(25, '\u00A0'),
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
                this.LoadBatchDetails(this.ddlproductname.SelectedValue.Trim(), this.ddlpackingsize.SelectedValue.Trim(), this.ddlfromdepot.SelectedValue.Trim(), "0", this.ddlstorelocation.SelectedValue.Trim());
            }
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + this.ddlbatchno.ClientID + "').focus(); ", true);
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

        HttpContext.Current.Session["STOCKSTORELOCATION"] = dtadjustment;
        return dtadjustment;
    }

    public int DatatableCheck(string pid, string pname, string batchno, decimal qty, string ReasonId)
    {
        int flag = 0;
        DataTable dtadjustment = (DataTable)HttpContext.Current.Session["STOCKSTORELOCATION"];

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
        DataTable dtadjustment = (DataTable)HttpContext.Current.Session["STOCKSTORELOCATION"];
        string GUID = Guid.NewGuid().ToString();
        decimal adjqtypcs = clsstockadjustment.CalculateQtyPcs(this.ddlproductname.SelectedValue, this.ddlpackingsize.SelectedValue, Convert.ToDecimal(this.txtadjustmentqty.Text.Trim()));
        decimal totalprice = price * Math.Abs(adjqtypcs);

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
        dr["ASSESMENTPERCENTAGE"] = Assesment;
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
            dr1["ASSESMENTPERCENTAGE"] = Assesment;
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

        HttpContext.Current.Session["STOCKSTORELOCATION"] = dtadjustment;

        if (dtadjustment.Rows.Count > 0)
        {
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
            ClsInterBatchTransfer_FAC clsinterbatch = new ClsInterBatchTransfer_FAC();
            DataTable dtProductdetails = new DataTable();
            int flag = 0;
            decimal remainingqty = 0;
            string mfDate = string.Empty;
            string exprDate = string.Empty;
            string assesment = string.Empty;
            //string mrp = string.Empty;
            string weight = string.Empty;
            string pid = ddlproductname.SelectedValue.ToString();
            string pname = ddlproductname.SelectedItem.Text;
            string batchno = ddlbatchno.SelectedValue.ToString();
            String[] tbatch = batchno.Split('|');
            batchno = tbatch[0].Trim();
            string packsizeid = ddlpackingsize.SelectedValue.ToString();
            string packname = ddlpackingsize.SelectedItem.Text;
            decimal adjustmentqty = Convert.ToDecimal(txtadjustmentqty.Text.Trim());
            decimal stockqty = Convert.ToDecimal(txtstockqty.Text.Trim());
            decimal beforeeditedstockqty = Convert.ToDecimal(hdn_lockadjustmentqty.Value.ToString());
            decimal editedstockqty = Convert.ToDecimal(hdn_editedstockqty.Value.ToString());

            flag = DatatableCheck(pid, pname, batchno, adjustmentqty, this.ddlreason.SelectedValue.Trim());

            if (flag == 0)
            {
                remainingqty = (stockqty - adjustmentqty);
                if (adjustmentqty == 0)
                {
                    MessageBox1.ShowInfo("<b>Adjustment Qty should be greater than<font color='red'> 0</font></b> !", 40, 500);
                }
                else if (remainingqty < 0)
                {
                    MessageBox1.ShowInfo("<b><font color='red'>Jornal qty more than stock qty</font></b> !", 40, 450);
                }
                else
                {
                    //dtProductdetails = clsinterbatch.BindPRoductDetails(pid, batchno);
                    dtProductdetails = clsinterbatch.BindPRoductDetails(this.ddlfromdepot.SelectedValue.Trim(), this.ddlproductname.SelectedValue.Trim(), this.ddlpackingsize.SelectedValue.Trim(), batchno, this.ddlstorelocation.SelectedValue.Trim());
                    if (dtProductdetails.Rows.Count > 0)
                    {
                        mfDate = Convert.ToString(ddlbatchno.SelectedItem.Text.Substring(77, 14).Trim()).Trim(); ;
                        exprDate = Convert.ToString(ddlbatchno.SelectedItem.Text.Substring(91, 14).Trim()).Trim();
                        assesment = Convert.ToString(dtProductdetails.Rows[0]["ASSESMENTPERCENTAGE"]).Trim();
                        //mrp = Convert.ToString(dtProductdetails.Rows[0]["MRP"]).Trim();
                        weight = "0";//Convert.ToString(dtProductdetails.Rows[0]["WEIGHT"]).Trim();
                    }
                    else
                    {
                        mfDate = "";
                        exprDate = "";
                        assesment = "0";
                        //mrp = "0";
                        weight = "";
                    }
                    this.BindAdjustmentDetails(pid, pname, batchno, packsizeid, packname, adjustmentqty, Convert.ToDecimal(txtprice.Text.Trim()), ddlreason.SelectedValue.ToString(), ddlreason.SelectedItem.Text, hdn_adjustmentid.Value.ToString(), beforeeditedstockqty, ddlstorelocation.SelectedValue.ToString(), ddlstorelocation.SelectedItem.Text, ddltostorelocation.SelectedValue.Trim(), ddltostorelocation.SelectedItem.Text.Trim(), mfDate, exprDate, assesment, txtmrp.Text.Trim(), weight);

                    ddlproductname.SelectedValue = "0";
                    ddlbatchno.Items.Clear();
                    ddlpackingsize.SelectedValue = "0";
                    txtadjustmentqty.Text = "";
                    txtstockqty.Text = "";
                    txtprice.Text = "";
                    txtmrp.Text = "";
                    hdn_editedstockqty.Value = "";
                    hdn_stockqtyinpcs.Value = "";
                    //ddlstorelocation.SelectedValue = "0";
                    //ddltostorelocation.SelectedValue = "0";
                }
            }
            else
            {
                MessageBox1.ShowInfo("<b>Product " + pname + " exist with same batchno with same reason!</b>", 40, 700);
                return;
            }
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + this.ddlstorelocation.ClientID + "').focus(); ", true);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
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
        DataTable dtadjustment = (DataTable)HttpContext.Current.Session["STOCKSTORELOCATION"];

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
        HttpContext.Current.Session["STOCKSTORELOCATION"] = dtadjustment;
        return delflag;
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            ClsEntryLock objLock = new ClsEntryLock();
            bool ObjDate = objLock.EntryLock(this.txtadjustmentdate.Text.Trim(), Convert.ToString(HttpContext.Current.Session["FINYEAR"]).Trim());
            if (ObjDate == true)
            {
                string stockadjustmentno = string.Empty;
                string mode = string.Empty;

                DataTable dtadjustment = (DataTable)HttpContext.Current.Session["STOCKSTORELOCATION"];
                if (dtadjustment.Rows.Count > 0)
                {
                    ClsStockAdjustment_FAC clsstockadjustment = new ClsStockAdjustment_FAC();
                    string xml = ConvertDatatableToXML(dtadjustment);
                    stockadjustmentno = clsstockadjustment.InsertShiftStoreLocationDetails(txtadjustmentdate.Text.Trim(), ddlfromdepot.SelectedValue.ToString(), ddlfromdepot.SelectedItem.Text, HttpContext.Current.Session["UserID"].ToString(), HttpContext.Current.Session["FINYEAR"].ToString(), Convert.ToString(hdn_adjustmentid.Value), xml, this.txtRemarks.Text.Trim());

                    if (stockadjustmentno != "")
                    {
                        string finyear = Session["FINYEAR"].ToString();

                        if (Convert.ToString(hdn_adjustmentid.Value) == "")
                        {
                            MessageBox1.ShowSuccess("Journal No :  <b><font color='green'>" + stockadjustmentno + "</font></b>  saved successfully", 40, 550);
                        }
                        else
                        {
                            MessageBox1.ShowSuccess("Journal No :  <b><font color='green'>" + stockadjustmentno + "</font></b> updated successfully", 40, 550);
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
                        this.hdn_adjustmentid.Value = "";
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
            else
            {
                MessageBox1.ShowInfo("Entry Date is Locked, Please Contact to Admin", 60, 500);
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
    }

    public void ResetTable()
    {
        Session.Remove("STOCKADJUSTMENTRECORD");
    }
    protected void btnSTfind_Click(object sender, EventArgs e)
    {
        try
        {
            this.LoadAdjustmentDetails();
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
            //Checker = Request.QueryString["CHECKER"].ToString().Trim();
            gvStockAdjustmentDetails.DataSource = clsstockadjustment.BindAdjustmentControl(txtfromdateins.Text, txttodateins.Text, HttpContext.Current.Session["DEPOTID"].ToString(), HttpContext.Current.Session["FINYEAR"].ToString().Trim(), Checker);
            gvStockAdjustmentDetails.DataBind();
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
            this.ddlstorelocation.SelectedValue = "0";
            this.trAdd.Style["display"] = "";
            this.divbtnsave.Visible = true;
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
            this.MotherDepoChange();
            this.LoadAdjustmentDetails();
            this.LoadReason();
            this.ddlfromdepot.Enabled = true;
            this.BindAdjustmentGrid();
            this.ddlstorelocation.SelectedValue = "0";
            this.trAdd.Style["display"] = "";
            this.divbtnsave.Visible = true;
            this.hdn_adjustmentid.Value = "";
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

            DataTable dtadjustment = (DataTable)HttpContext.Current.Session["STOCKSTORELOCATION"];
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
            ClsStockAdjustment_FAC clsstockadjustment = new ClsStockAdjustment_FAC();
            if (clsstockadjustment.Getstatus(hdn_adjustmentid.Value.Trim()) == "1")
            {
                MessageBox1.ShowInfo("Day End Operation already done,not allow to edit.", 50, 450);
                return;
            }
            if (Session["STOCKSTORELOCATION"] == null)
            {
                this.BindAdjustmentGrid();
            }
            this.LoadReason();
            this.LoadDepo();
            DataTable dtadjustment = (DataTable)HttpContext.Current.Session["STOCKSTORELOCATION"];

            this.imgPopuppodate.Visible = true;
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
            this.MotherDepoChange();

            dtadjustment = clsstockadjustment.BindAdjustmentDetails(adjustmentid);
            if (dtadjustment.Rows.Count > 0)
            {
                this.gvadjustment.DataSource = dtadjustment;
                this.gvadjustment.DataBind();
                HttpContext.Current.Session["STOCKSTORELOCATION"] = dtadjustment;
            }
            else
            {
                this.gvadjustment.DataSource = null;
                this.gvadjustment.DataBind();
            }

            if (Convert.ToString(dteditedadjustmentrecord.Rows[0]["ISVERIFIED"]).Trim() == "Y")
            {
                this.trAdd.Style["display"] = "none";
                this.divbtnsave.Visible = false;
            }
            else
            {
                this.trAdd.Style["display"] = "";
                this.divbtnsave.Visible = true;
            }
            this.ddlfromdepot.Enabled = false;
            this.btnaddhide.Style["display"] = "none";
            this.pnlAdd.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.btnadd.Visible = true;
            this.btncancel.Visible = true;
            this.btnnewentry.Visible = false;
            this.pnlAdd.Enabled = true;
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
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
            Calendar1.StartDate = oDate;
            CalendarExtender4.StartDate = oDate;
            CalendarExtender3.StartDate = oDate;

            if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
            {
                this.txtfromdateins.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.txttodateins.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.txtadjustmentdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                Calendar1.EndDate = today1;
                CalendarExtender4.EndDate = today1;
                CalendarExtender3.EndDate = today1;
            }
            else
            {
                this.txtfromdateins.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
                this.txttodateins.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
                this.txtadjustmentdate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
                Calendar1.EndDate = cDate;
                CalendarExtender4.EndDate = cDate;
                CalendarExtender3.EndDate = cDate;
            }
        }
        catch (Exception ex)
        {
                        
        }
    }
    #endregion
}