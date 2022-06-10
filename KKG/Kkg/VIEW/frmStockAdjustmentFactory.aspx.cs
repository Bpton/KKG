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

public class JournalBatchdetails_RMPM
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

public partial class VIEW_frmStockAdjustmentFactory : System.Web.UI.Page
{
    string MENUID = string.Empty;
    string Checker = string.Empty;

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.ClearData();
                this.pnlAdd.Style["display"] = "none";
                this.pnlDisplay.Style["display"] = "";
                this.LoadFactory();
                this.LoadBrand();
                this.LoadReason();
                this.LoadStoreloacation();
                MENUID = Request.QueryString["MENUID"].ToString().Trim();
                Checker = Request.QueryString["CHECKER"].ToString().Trim();
                if (Checker == "TRUE")
                {
                    this.btnaddhide.Visible = false;
                    this.divbtnSubmit.Visible = false;
                    this.divbtnapprove.Visible = true;
                    this.divbtnreject.Visible = true;
                }
                else
                {
                    this.btnaddhide.Visible = true;
                    this.divbtnSubmit.Visible = true;
                    this.divbtnapprove.Visible = false;
                    this.divbtnreject.Visible = false;
                }
                this.ddlCategory.Items.Clear();
                this.ddlCategory.Items.Insert(0, new ListItem("SELECT SUB ITEM NAME", "0"));
                this.ddlProduct.Items.Clear();
                this.ddlProduct.Items.Insert(0, new ListItem("SELECT PRODUCT NAME", "0"));
                this.ddlPackingSize.Items.Clear();
                this.ddlPackingSize.Items.Add(new ListItem("SELECT UOM", "0"));
                this.LoadAdjustmentDetails();

                #region Add FinYear Wise Date Lock
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
                CalendarExtender1.StartDate = oDate;

                if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
                {
                    this.txtfromdateins.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                    this.txttodateins.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                    this.txtopeningdate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                    Calendar1.EndDate = today1;
                    CalendarExtender4.EndDate = today1;
                    CalendarExtender1.EndDate = today1;
                }
                else
                {
                    this.txtfromdateins.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                    this.txttodateins.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                    this.txtopeningdate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                    Calendar1.EndDate = cDate;
                    CalendarExtender4.EndDate = cDate;
                    CalendarExtender1.EndDate = cDate;
                }
                #endregion
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

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

    public void BindGridOpenstock(string DepotID)
    {
        ClsStockJournal ClsStockJournal = new ClsStockJournal();
        Checker = Request.QueryString["CHECKER"].ToString().Trim();
        gvOpenStock.DataSource = ClsStockJournal.BindAdjustmentControl(Convert.ToString(this.txtfromdateins.Text.Trim()), Convert.ToString(this.txttodateins.Text.Trim()), Convert.ToString(Session["DEPOTID"]), HttpContext.Current.Session["FINYEAR"].ToString().Trim(), Checker);
        gvOpenStock.DataBind();
    }

    public void LoadAdjustmentDetails()
    {
        try
        {
            ClsStockJournal ClsStockJournal = new ClsStockJournal();
            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            gvOpenStock.DataSource = ClsStockJournal.BindAdjustmentControl(this.txtfromdateins.Text.Trim(), this.txttodateins.Text.Trim(), HttpContext.Current.Session["DEPOTID"].ToString().Trim(), HttpContext.Current.Session["FINYEAR"].ToString().Trim(), Checker);
            gvOpenStock.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            this.ClearData();
            this.CreateJournalTable();
            this.loadProduct();
            this.pnlAdd.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.btnaddhide.Style["display"] = "none";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #region LoadFactory
    public void LoadFactory()
    {
        try
        {
            ClsStockJournal ClsStockJournal = new ClsStockJournal();
            DataTable dt = new DataTable();
            this.ddlDeptName.Items.Clear();
            //if (Session["TPU"].ToString() == "F")
            //{
            dt = ClsStockJournal.BindFactory(Session["UserID"].ToString());
            this.ddlDeptName.DataSource = dt;
            this.ddlDeptName.DataValueField = "VENDORID";
            this.ddlDeptName.DataTextField = "VENDORNAME";
            this.ddlDeptName.DataBind();
            //}

            //if (dt.Rows.Count == 1)
            //{
            //    ddlDeptName.SelectedValue = Session["DEPOTID"].ToString();
            //}
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }

    }
    #endregion

    public void LoadBrand()
    {
        //ClsStockJournal ClsStockJournal = new ClsStockJournal();
        ClsItemType clsItemType = new ClsItemType();
        ddlBrand.Items.Clear();
        ddlBrand.Items.Insert(0, new ListItem("SELECT PRIMARY ITEM", "0"));
        ddlBrand.AppendDataBoundItems = true;
        ddlBrand.DataSource = clsItemType.LoadPrimaryItemType();//ClsStockJournal.BindBrand_SupliedItem();
        //ddlBrand.DataTextField = "DIVNAME";
        //ddlBrand.DataValueField = "DIVID";
        ddlBrand.DataValueField = "ID";
        ddlBrand.DataTextField = "ITEMDESC";
        ddlBrand.DataBind();
    }

    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBrand.SelectedValue != "")
            {
                ClsStockJournal ClsStockJournal = new ClsStockJournal();
                ddlCategory.Items.Clear();
                ddlCategory.Items.Insert(0, new ListItem("SELECT SUB ITEM", "0"));
                ddlCategory.AppendDataBoundItems = true;
                ddlCategory.DataSource = ClsStockJournal.BindCategory_SupliedItem(Convert.ToString(ddlBrand.SelectedValue));
                ddlCategory.DataTextField = "CATNAME";
                ddlCategory.DataValueField = "CATID";
                ddlCategory.DataBind();
            }
            else
            {
                ddlCategory.Items.Clear();
                ddlCategory.Items.Insert(0, new ListItem("SELECT CATAGORY NAME", "0"));
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCategory.SelectedValue != "")
            {
                ClsStockJournal ClsStockJournal = new ClsStockJournal();
                ddlProduct.Items.Clear();
                ddlProduct.Items.Insert(0, new ListItem("SELECT PRODUCT NAME", "0"));
                ddlProduct.AppendDataBoundItems = true;
                ddlProduct.DataSource = ClsStockJournal.BindProduct(Convert.ToString(ddlCategory.SelectedValue).Trim(), Convert.ToString(ddlBrand.SelectedValue).Trim());
                ddlProduct.DataTextField = "NAME";
                ddlProduct.DataValueField = "ID";
                ddlProduct.DataBind();
            }
            else
            {
                ddlProduct.Items.Clear();
                ddlProduct.Items.Insert(0, new ListItem("SELECT PRODUCT NAME", "0"));
            }

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    public void loadProduct()
    {
        try
        {
            ClsStockJournal ClsStockJournal = new ClsStockJournal();
            ddlProduct.Items.Clear();
            ddlProduct.Items.Insert(0, new ListItem("SELECT PRODUCT NAME", "0"));
            ddlProduct.AppendDataBoundItems = true;
            ddlProduct.DataSource = ClsStockJournal.BindProduct("-1", "-1");
            ddlProduct.DataTextField = "NAME";
            ddlProduct.DataValueField = "ID";
            ddlProduct.DataBind();
        }
        catch (Exception ex)
        {

        }
    }

    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProduct.SelectedValue != "")
            {
                ClsStockJournal ClsStockJournal = new ClsStockJournal();
                ClsMMPoOrder clsMMPo = new ClsMMPoOrder();
                ddlPackingSize.Items.Clear();

                ddlstorelocation.Items.Clear();
                LoadStoreloacation();


                DataTable dtPackSize = new DataTable();
                dtPackSize= ClsStockJournal.BindPackingSize(ddlProduct.SelectedValue.ToString()); ;
                ddlPackingSize.Items.Add(new ListItem("SELECT UOM", "0"));
                ddlPackingSize.AppendDataBoundItems = true;
                ddlPackingSize.DataSource = dtPackSize;
                ddlPackingSize.DataValueField = "PACKSIZEID_FROM";
                ddlPackingSize.DataTextField = "PACKSIZEName_FROM";
                ddlPackingSize.DataBind();

                if(dtPackSize.Rows.Count == 1)
                {
                    this.ddlPackingSize.SelectedValue = dtPackSize.Rows[0]["PACKSIZEID_FROM"].ToString();
                }

                DataTable dt = new DataTable();
                dt = ClsStockJournal.BindMRPperproduct(ddlProduct.SelectedValue.ToString());
                if (dt.Rows.Count > 0)
                {
                    this.txtmrp.Text = dt.Rows[0]["MRP"].ToString().Trim();
                    this.Hdnassementpercentage.Value = Convert.ToString(dt.Rows[0]["ASSESSABLEPERCENT"]);

                    //DataTable dtweight = new DataTable();
                    //dtweight = ClsStockJournal.BindMRPperproduct(ddlProduct.SelectedValue.ToString());
                }
                else
                {
                    this.txtmrp.Text = "0";
                    this.Hdnassementpercentage.Value = "0";
                }

                if (!string.IsNullOrEmpty(this.txtmfgdate.Text.Trim()))
                {
                    string expdate = expdate = ClsStockJournal.GetProductExpirydate(this.ddlProduct.SelectedValue.Trim(), txtmfgdate.Text.ToString());
                    this.txtexpdate.Text = expdate;
                }
            }
            else
            {
                this.ddlPackingSize.SelectedValue = "0";
                this.txtmrp.Text = "";
                this.Hdnassementpercentage.Value = "";
                this.ddlPackingSize.Items.Clear();
                this.ddlPackingSize.Items.Add(new ListItem("SELECT UOM", "0"));
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    public void LoadStoreloacation()
    {
        ClsStockJournal ClsStockJournal = new ClsStockJournal();
        DataTable Dt = new DataTable();
        Dt = ClsStockJournal.BindStorelocation();
        if (Dt.Rows.Count > 0)
        {
            ddlstorelocation.Items.Clear();
            ddlstorelocation.Items.Add(new ListItem("SELECT STORE LOCATION", "0"));
            ddlstorelocation.AppendDataBoundItems = true;
            ddlstorelocation.DataSource = Dt;

            ddlstorelocation.DataValueField = "ID";
            ddlstorelocation.DataTextField = "Name";
            ddlstorelocation.DataBind();

            if (Dt.Rows.Count == 5)
            {
                this.ddlstorelocation.SelectedValue = Convert.ToString(Dt.Rows[0]["ID"]);
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            this.ClearData();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

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

    protected void CreateJournalTable()
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
        dtadjustment.Columns.Add("STORELOCATIONID");
        dtadjustment.Columns.Add("STORELOCATIONNAME");
        dtadjustment.Columns.Add("MFDATE");
        dtadjustment.Columns.Add("EXPRDATE");
        dtadjustment.Columns.Add("ASSESMENTPERCENTAGE");
        dtadjustment.Columns.Add("MRP");
        dtadjustment.Columns.Add("WEIGHT");

        HttpContext.Current.Session["STOCKADJUSTMENTRECORDRMPM"] = dtadjustment;
    }

    public int DatatableCheck(string pid, string batchno, string ReasonId)
    {
        int flag = 0;
        DataTable dtadjustment = (DataTable)HttpContext.Current.Session["STOCKADJUSTMENTRECORDRMPM"];

        if (dtadjustment.Rows.Count > 0)
        {
            int NumberofRecord = dtadjustment.Select("PRODUCTID='" + pid + "' AND BATCHNO='" + batchno + "' AND REASONID='" + ReasonId + "'").Length;
            if (NumberofRecord > 0)
            {
                flag = 1;
            }
        }
        return flag;
    }

    #region LoadReason
    public void LoadReason()
    {
        ClsReceivedStock clsReceivedStock = new ClsReceivedStock();
        ddlreason.Items.Clear();
        ddlreason.Items.Add(new ListItem("-- SELECT REASON NAME --", "0"));
        ddlreason.AppendDataBoundItems = true;
        ddlreason.DataSource = clsReceivedStock.BindReason(Request.QueryString["MENUID"].ToString());
        ddlreason.DataValueField = "ID";
        ddlreason.DataTextField = "DESCRIPTION";
        ddlreason.DataBind();
    }
    #endregion


    protected void btnAddStock_Click(object sender, EventArgs e)
    {
        try
        {
            decimal adjustmentqty = Convert.ToDecimal(txtStockqty.Text.Trim());
            decimal stockqty = Convert.ToDecimal(txtstockinhand.Text.Trim());
            decimal remainingqty = 0;
            int flag = 0;
            if (Session["STOCKADJUSTMENTRECORDRMPM"] == null)
            {
                this.CreateJournalTable();
            }
            DataTable dtProductdetails = (DataTable)HttpContext.Current.Session["STOCKADJUSTMENTRECORDRMPM"];
            flag = DatatableCheck(this.ddlProduct.SelectedValue, Convert.ToString(txtBatchno.Text.Trim()), this.ddlreason.SelectedValue.Trim());
            if (ddlreason.SelectedValue != "0")
            {
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
                        MessageBox1.ShowInfo("<b><font color='red'>Stock Jornal Qty More Than Stock Qty.</font></b> !", 40, 450);
                        return;
                    }
                    else
                    {
                        DataRow dr = dtProductdetails.NewRow();
                        dr["GUID"] = Convert.ToString(Guid.NewGuid());
                        dr["PRODUCTID"] = Convert.ToString(this.ddlProduct.SelectedValue);
                        dr["PRODUCTNAME"] = Convert.ToString(this.ddlProduct.SelectedItem.Text);
                        dr["BATCHNO"] = "NA";
                        dr["PACKINGSIZEID"] = Convert.ToString(this.ddlPackingSize.SelectedValue);
                        dr["PACKINGSIZENAME"] = Convert.ToString(this.ddlPackingSize.SelectedItem.Text);
                        dr["ADJUSTMENTQTY"] = Convert.ToString(txtStockqty.Text.Trim());
                        dr["PRICE"] = Convert.ToString(0);
                        dr["AMOUNT"] = Convert.ToString(0);
                        dr["REASONID"] = Convert.ToString(this.ddlreason.SelectedValue);
                        dr["REASONNAME"] = Convert.ToString(this.ddlreason.SelectedItem.Text);
                        dr["STORELOCATIONID"] = Convert.ToString(this.ddlstorelocation.SelectedValue.ToString());
                        dr["STORELOCATIONNAME"] = Convert.ToString(this.ddlstorelocation.SelectedItem.Text);
                        dr["MFDATE"] = "01/01/1900";
                        dr["EXPRDATE"] = "01/01/1900";
                        dr["ASSESMENTPERCENTAGE"] = Convert.ToString(0);
                        dr["MRP"] = Convert.ToString(txtmrp.Text.Trim());
                        dr["WEIGHT"] = Convert.ToString(0);

                        dtProductdetails.Rows.Add(dr);
                        dtProductdetails.AcceptChanges();

                        if (dtProductdetails.Rows.Count > 0)
                        {
                            this.gvadjustment.DataSource = dtProductdetails;
                            this.gvadjustment.DataBind();

                            for (int i = 0; i < dtProductdetails.Rows.Count; i++)
                            {
                                decimal AdjustmentQty = 0;
                                AdjustmentQty = Convert.ToDecimal(dtProductdetails.Rows[i]["ADJUSTMENTQTY"].ToString());
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
                        else
                        {
                            this.gvadjustment.DataSource = null;
                            this.gvadjustment.DataBind();
                            this.txtPosPCS.Text = "0";
                            this.txtNegPCS.Text = "0";
                        }

                        this.ddlProduct.SelectedValue = "0";
                        this.ddlPackingSize.SelectedValue = "0";
                        this.ddlstorelocation.SelectedValue = "0";
                        this.txtStockqty.Text = "";
                        this.txtmfgdate.Text = "";
                        this.txtexpdate.Text = "";
                        this.txtmrp.Text = "";
                        this.txtBatchno.Text = "";
                    }
                }
                else
                {
                    MessageBox1.ShowInfo("<b>Product " + this.ddlProduct.SelectedItem.Text + " exist with same batchno with same reason!</b>", 40, 700);
                    return;
                }
            }

            else
            {
                MessageBox1.ShowInfo("<b> Select Reason!</b>", 40, 300);
                return;
            }
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + this.ddlstorelocation.ClientID + "').focus(); ", true);

            //else
            //{
            //    MessageBox1.ShowInfo("<b>Stock Jornal Qty More Than Stock Qty !</b>", 40, 400);
            //    return;
            //}
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btngriddelete_Click(object sender, EventArgs e)
    {
        try
        {
            string sGUID = Convert.ToString(hdndtAddStockDelete.Value);
            DataTable dtdeleteStock = new DataTable();
            dtdeleteStock = (DataTable)Session["STOCKADJUSTMENTRECORDRMPM"];

            DataRow[] drr = dtdeleteStock.Select("GUID='" + sGUID + "'");
            for (int i = 0; i < drr.Length; i++)
            {
                drr[i].Delete();
                dtdeleteStock.AcceptChanges();
            }
            Session["STOCKADJUSTMENTRECORDRMPM"] = dtdeleteStock;

            if (dtdeleteStock.Rows.Count > 0)
            {
                this.gvadjustment.DataSource = dtdeleteStock;
                this.gvadjustment.DataBind();

                for (int i = 0; i < dtdeleteStock.Rows.Count; i++)
                {
                    decimal AdjustmentQty = 0;
                    AdjustmentQty = Convert.ToDecimal(dtdeleteStock.Rows[i]["ADJUSTMENTQTY"].ToString());
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
            else
            {
                this.gvadjustment.DataSource = null;
                this.gvadjustment.DataBind();
                this.txtPosPCS.Text = "0";
                this.txtNegPCS.Text = "0";
            }
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string stockadjustmentno = string.Empty;
            string mode = string.Empty;
            DataTable dtadjustment = new DataTable();
            dtadjustment = (DataTable)HttpContext.Current.Session["STOCKADJUSTMENTRECORDRMPM"];
            if (dtadjustment.Rows.Count > 0)
            {
                ClsStockJournal ClsStockJournal = new ClsStockJournal();
                string xml = ConvertDatatableToXML(dtadjustment);
                stockadjustmentno = ClsStockJournal.InsertAdjustmentDetails(this.txtopeningdate.Text.Trim(), this.ddlDeptName.SelectedValue.ToString(), this.ddlDeptName.SelectedItem.Text, HttpContext.Current.Session["IUserID"].ToString(), HttpContext.Current.Session["FINYEAR"].ToString(), Convert.ToString(hdn_adjustmentid.Value), xml, this.txtRemarks.Text.Trim(), MENUID);

                if (stockadjustmentno != "")
                {
                    if (Convert.ToString(hdn_adjustmentid.Value) == "")
                    {
                        MessageBox1.ShowSuccess("Journal No :  <b><font color='green'>" + stockadjustmentno + "</font></b>  saved successfully", 60, 550);
                    }
                    else
                    {
                        MessageBox1.ShowSuccess("Journal No :  <b><font color='green'>" + stockadjustmentno + "</font></b> updated successfully", 60, 550);
                    }

                    this.LoadAdjustmentDetails();
                    this.ClearData();
                }
                else
                {
                    MessageBox1.ShowError("<b><font color='red'>Record saved unsuccessful..</font></b>");
                }
            }
            else
            {
                MessageBox1.ShowInfo("<b>Please entry atleast 1 product</b>");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btngridedit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsStockJournal ClsStockJournal = new ClsStockJournal();
            if (Session["STOCKADJUSTMENTRECORDRMPM"] == null)
            {
                this.CreateJournalTable();
            }
            DataTable dtadjustment = (DataTable)HttpContext.Current.Session["STOCKADJUSTMENTRECORDRMPM"];
            this.tradjunmentheader.Style["display"] = "";
            string adjustmentid = hdn_adjustmentid.Value.ToString();

            DataSet ds = new DataSet();
            ds = ClsStockJournal.EditJournalDetails(hdn_adjustmentid.Value);

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.txtadjustmentno.Text = ds.Tables[0].Rows[0]["ADJUSTMENTNO"].ToString();
                this.txtopeningdate.Text = ds.Tables[0].Rows[0]["ADJUSTMENTDATE"].ToString();
                this.ddlDeptName.SelectedValue = ds.Tables[0].Rows[0]["DEPOTID"].ToString();
                this.txtRemarks.Text = ds.Tables[0].Rows[0]["REMARKS"].ToString();

                if (ds.Tables[1].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        DataRow dr = dtadjustment.NewRow();
                        dr["GUID"] = Convert.ToString(ds.Tables[1].Rows[i]["GUID"]);
                        dr["PRODUCTID"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"]);
                        dr["PRODUCTNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTNAME"]);
                        dr["BATCHNO"] = Convert.ToString(ds.Tables[1].Rows[i]["BATCHNO"]);
                        dr["PACKINGSIZEID"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKINGSIZEID"]);
                        dr["PACKINGSIZENAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PACKINGSIZENAME"]);
                        dr["ADJUSTMENTQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["ADJUSTMENTQTY"]);
                        dr["PRICE"] = Convert.ToString(ds.Tables[1].Rows[i]["PRICE"]);
                        dr["AMOUNT"] = Convert.ToString(ds.Tables[1].Rows[i]["AMOUNT"]);
                        dr["REASONID"] = Convert.ToString(ds.Tables[1].Rows[i]["REASONID"]);
                        dr["REASONNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["REASONNAME"]);
                        dr["STORELOCATIONID"] = Convert.ToString(ds.Tables[1].Rows[i]["STORELOCATIONID"]);
                        dr["STORELOCATIONNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["STORELOCATIONNAME"]);
                        dr["MFDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["MFDATE"]);
                        dr["EXPRDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["EXPRDATE"]);
                        dr["ASSESMENTPERCENTAGE"] = Convert.ToString(ds.Tables[1].Rows[i]["ASSESMENTPERCENTAGE"]);
                        dr["MRP"] = Convert.ToString(ds.Tables[1].Rows[i]["MRP"]);
                        dr["WEIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["WEIGHT"]);

                        dtadjustment.Rows.Add(dr);
                        dtadjustment.AcceptChanges();
                    }

                    if (dtadjustment.Rows.Count > 0)
                    {
                        this.gvadjustment.DataSource = dtadjustment;
                        this.gvadjustment.DataBind();
                        HttpContext.Current.Session["STOCKADJUSTMENTRECORDRMPM"] = dtadjustment;

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
                    }
                    else
                    {
                        this.gvadjustment.DataSource = null;
                        this.gvadjustment.DataBind();
                        this.txtPosPCS.Text = "0";
                        this.txtNegPCS.Text = "0";
                    }
                }
            }
            this.ddlDeptName.Enabled = false;
            this.btnaddhide.Style["display"] = "none";
            this.pnlAdd.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";

            #region QueryString

            Checker = Request.QueryString["CHECKER"].ToString().Trim();
            if (Checker == "TRUE" && ClsStockJournal.GetApproveStatus(adjustmentid) == "1")
            {
                this.divbtnSubmit.Visible = false;
                this.divbtnapprove.Visible = false;
                this.divbtnreject.Visible = false;
                this.divadd.Visible = false;
            }
            else if (Checker == "TRUE" && ClsStockJournal.GetApproveStatus(adjustmentid) == "0")
            {
                this.divbtnSubmit.Visible = false;
                this.divbtnapprove.Visible = true;
                this.divbtnreject.Visible = true;
                this.divadd.Visible = false;
            }
            else
            {
                if (ClsStockJournal.GetApproveStatus(adjustmentid) == "1")
                {
                    this.divbtnSubmit.Visible = false;
                }
                else
                {
                    this.divbtnSubmit.Visible = true;
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

    public void ResetTable()
    {
        Session.Remove("STOCKADJUSTMENTRECORD");
    }

    public void ClearData()
    {
        this.hdnDepotID.Value = "";
        this.hdn_adjustmentid.Value = "";
        this.pnlAdd.Style["display"] = "none";
        this.pnlDisplay.Style["display"] = "";
        this.btnaddhide.Style["display"] = "";
        this.tradjunmentheader.Style["display"] = "none";
        //this.ddlDeptName.SelectedValue = "-1";
        this.ddlBrand.SelectedValue = "0";
        this.ddlCategory.SelectedValue = "0";
        this.ddlProduct.SelectedValue = "0";
        this.ddlPackingSize.SelectedValue = "0";
        this.txtBatchno.Text = "";
        this.txtStockqty.Text = "";
        this.txtmrp.Text = "";
        this.txtmfgdate.Text = "";
        this.txtexpdate.Text = "";
        this.txtRemarks.Text = "";
        this.txtPosPCS.Text = "0";
        this.txtNegPCS.Text = "0";
        this.Hdnassementpercentage.Value = "";
        this.gvadjustment.ClearPreviousDataSource();
        this.gvadjustment.DataSource = null;
        this.gvadjustment.DataBind();
        this.ddlstorelocation.SelectedValue = "0";
        this.ddlDeptName.Enabled = true;
        /*DateTime dtcurr = DateTime.Now;
        string date = "dd/MM/yyyy";
        this.txtopeningdate.Text = dtcurr.ToString(date).Replace('-', '/');*/
        this.ResetTable();
        Session["STOCKADJUSTMENTRECORDRMPM"] = null;
        txtstockinhand.Text = "0";
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

    protected void txtmfgdate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (this.ddlProduct.SelectedValue != "0")
            {
                ClsStockJournal ClsStockJournal = new ClsStockJournal();

                if (!string.IsNullOrEmpty(this.txtmfgdate.Text.Trim()))
                {
                    string PRODUCT = this.ddlProduct.SelectedValue;
                    string expdate = string.Empty;
                    expdate = ClsStockJournal.GetProductExpirydate(PRODUCT, txtmfgdate.Text.ToString());
                    txtexpdate.Text = expdate;
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

    protected void ddlstorelocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProduct.SelectedValue != "")
            {
                LoadBatchDetails();
                /*ClsStockJournal ClsStockJournal = new ClsStockJournal();
                DataTable dtStock = new DataTable();
                dtStock = ClsStockJournal.Bind_StockinHand(Convert.ToString(Session["DEPOTID"]), ddlProduct.SelectedValue.ToString(), ddlPackingSize.SelectedValue.ToString(), "0", 0, "0", ddlCategory.SelectedValue.ToString(), "", "", txtopeningdate.Text, "0", ddlstorelocation.SelectedValue.ToString());
                if (dtStock.Rows.Count > 0)
                {
                    this.txtstockinhand.Text = dtStock.Rows[0]["STOCK_QTY"].ToString().Trim();
                    this.txtBatchno.Text = dtStock.Rows[0]["BATCHNO"].ToString().Trim();
                    this.txtmfgdate.Text = dtStock.Rows[0]["MFGDATE"].ToString().Trim();
                    this.txtexpdate.Text = dtStock.Rows[0]["EXPIRDATE"].ToString().Trim();
                }
                else

                {
                    this.txtstockinhand.Text = "0";
                    this.txtBatchno.Text = "";
                    this.txtmfgdate.Text = "";
                    this.txtexpdate.Text = "";
                }*/
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #region rdbtype_SelectedIndexChanged
    protected void rdbtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.ddlBrand.SelectedValue = "0";
            this.ddlCategory.SelectedValue = "0";
            this.ddlProduct.SelectedValue = "0";
            this.ddlPackingSize.SelectedValue = "0";
            this.ddlstorelocation.SelectedValue = "0";
            this.ddlbatchno.SelectedValue = "0";
            this.txtBatchno.Text = "";
            this.txtstockinhand.Text = "0";
            this.txtmrp.Text = "";
            this.txtmfgdate.Text = "";
            this.txtexpdate.Text = "";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region LoadBatchDetails
    public void LoadBatchDetails(/*string ProductID, string PacksizeID, string DepotID, string BatchNo, string StoreLocation, string Mode*/)
    {
        try
        {
            ClsStockJournal clsAdj = new ClsStockJournal();
            DataTable dtBatch = new DataTable();
            DataTable dtBaseCost = new DataTable();
            if (rdbtype.SelectedValue.Trim() == "G")
            {
                txtBatchno.Enabled = false;
                txtmrp.Enabled = false;
                txtmfgdate.Enabled = false;
                txtexpdate.Enabled = false;
                ImageButtonMFDate.Enabled = false;
                ImageButtonExprDate.Enabled = false;

                //dtBatch = clsAdj.BindBatchDetails(DepotID, ProductID, PacksizeID, BatchNo, StoreLocation);
                dtBatch = clsAdj.Bind_StockinHand(Convert.ToString(Session["DEPOTID"]), ddlProduct.SelectedValue.ToString(), ddlPackingSize.SelectedValue.ToString(), "0", 0, "0", ddlCategory.SelectedValue.ToString(), "", "", txtopeningdate.Text, "0", ddlstorelocation.SelectedValue.ToString());

                List<JournalBatchdetails_RMPM> BatchDetails = new List<JournalBatchdetails_RMPM>();
                if (dtBatch.Rows.Count > 0)
                {
                    this.ddlbatchno.Items.Clear();
                    this.ddlbatchno.Items.Add(new ListItem("SELECT BATCHNO", "0"));

                    for (int i = 0; i < dtBatch.Rows.Count; i++)
                    {
                        JournalBatchdetails_RMPM po = new JournalBatchdetails_RMPM();
                        po.BATCHNO = dtBatch.Rows[i]["BATCHNO"].ToString();
                        po.STOCKQTY = dtBatch.Rows[i]["STOCK_QTY"].ToString();
                        po.ID = dtBatch.Rows[i]["BATCHNO"].ToString() + '|' + dtBatch.Rows[i]["MRP"].ToString() + dtBatch.Rows[i]["MFGDATE"].ToString() + dtBatch.Rows[i]["EXPIRDATE"].ToString();
                        po.MRP = dtBatch.Rows[i]["MRP"].ToString();
                        po.MFDATE = dtBatch.Rows[i]["MFGDATE"].ToString();
                        po.EXPRDATE = dtBatch.Rows[i]["EXPIRDATE"].ToString();
                        BatchDetails.Add(po);
                    }
                    //---------------------------------------------------------------------------------------------

                    string text1 = string.Format("{0}{1}{2}{3}{4}",
                    "BATCHNO".PadRight(18, '\u00A0'),
                    "STOCKQTY".PadRight(15, '\u00A0'),
                    "MRP".PadRight(11, '\u00A0'),
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

                    foreach (JournalBatchdetails_RMPM p in BatchDetails)
                    {
                        string text = string.Format("{0}{1}{2}{3}{4}",
                            p.BATCHNO.PadRight(18, '\u00A0'),
                            p.STOCKQTY.PadRight(15, '\u00A0'),
                            p.MRP.PadRight(11, '\u00A0'),
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
                decimal MRP = 0;
                MRP = clsAdj.GetMrpProductWise(this.ddlProduct.SelectedValue.ToString());
                this.ddlbatchno.Items.Clear();
                this.ddlbatchno.Items.Add(new ListItem("SELECT BATCHNO", "0"));
                txtBatchno.Enabled = true;
                txtmrp.Enabled = true;
                txtmfgdate.Enabled = true;
                txtexpdate.Enabled = true;
                ImageButtonMFDate.Enabled = true;
                ImageButtonExprDate.Enabled = true;

                txtBatchno.Text = "";
                txtmrp.Text = "";
                txtmfgdate.Text = "";
                txtexpdate.Text = "";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region BatchDetails
    protected void BatchDetails()
    {
        if (this.ddlProduct.SelectedValue != "0")
        {
            this.txtBatchno.Text = ddlbatchno.SelectedItem.Text.Substring(0, 18).Trim();
            this.txtstockinhand.Text = ddlbatchno.SelectedItem.Text.Substring(18, 15).Trim();
            this.txtmrp.Text = ddlbatchno.SelectedItem.Text.Substring(33, 11).Trim();
            this.hdn_mfgdate.Value = ddlbatchno.SelectedItem.Text.Substring(43, 14).Trim();
            this.hdn_exprdate.Value = ddlbatchno.SelectedItem.Text.Substring(54, 14).Trim();
            this.txtmfgdate.Text = hdn_mfgdate.Value;
            this.txtexpdate.Text = hdn_exprdate.Value;
        }
        else
        {
            this.txtmrp.Text = "";
            this.txtstockinhand.Text = "0";
            this.hdn_mfgdate.Value = "";
            this.hdn_exprdate.Value = "";
        }
    }
    #endregion

    protected void ddlbatchno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (this.ddlbatchno.SelectedValue != "0")
            {
                this.BatchDetails();
            }
            else
            {
                this.txtBatchno.Text = "";
                this.txtstockinhand.Text = "";
                this.txtmrp.Text = "";
                this.txtmfgdate.Text = "";
                this.txtexpdate.Text = "";
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            ClsStockJournal ClsStockJournal = new ClsStockJournal();
            string Adjustmentid = hdn_adjustmentid.Value.ToString();
            int flag = 0;
            flag = ClsStockJournal.ApproveStockJournalRMPM(Adjustmentid);
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

    #region gvOpenStock_RowDataBound
    protected void gvOpenStock_RowDataBound(object sender, GridRowEventArgs e)
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
}