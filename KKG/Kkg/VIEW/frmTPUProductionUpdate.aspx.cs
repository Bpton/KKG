using BAL;
using Obout.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

public class Productiondetails
{
    public string POID { get; set; }
    public string PONO { get; set; }
    public string POQTY { get; set; }
    public string PRODUCTIONQTY { get; set; }
    public string AVAILABLEQTY { get; set; }
    public string ORDERTYPENAME { get; set; }
    public string PODATE { get; set; }
}

public partial class VIEW_frmTPUProductionUpdate : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    DataTable dt1 = new DataTable();
    DataTable dt2 = new DataTable();
    DateTime dtcurr = DateTime.Now;
    string date = "dd/MM/yyyy";
   

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("frmAdminLogin.aspx");
                }
                else
                {
                    pnlAdd.Style["display"] = "none";
                    pnlAdd1.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "none";
                    pnlDisplay2.Style["display"] = "none";
                    pnlDisplay3.Style["display"] = "";
                    Session["gvpu"] = null;
                    Session["gvpu1"] = null;
                    txtsearchfromdate.Style.Add("color", "black !important");
                    txtsearchtodate.Style.Add("color", "black !important");
                    DateLock();
                    LoadPO();
                    LoadPUSheet();
                    LoadUpdateGrid(this.txtsearchfromdate.Text.Trim(), this.txtsearchtodate.Text.Trim());
                    dinadd.Visible = true;
                    if (Session["TPU"].ToString() == "T")
                    {
                        LgndTPU.InnerText = "PURCHASE ORDER DATE";
                        lngdPUdtae.InnerText = "PRODUCTION UPDATE INFO";
                        lblPdate.Text = "Entry Date";
                    }
                    else
                    {
                        LgndTPU.InnerText = "INDENT DETAILS";
                        lngdPUdtae.InnerText = "PRODUCTION UPDATE INFO";
                        lblPdate.Text = "Entry Date";
                    }
                }
                
            }
            if (Session["TPU"].ToString() == "T")
            {
                gvPOProduct.Columns[0].HeaderText = @"PO NO";
                gvPU.Columns[0].HeaderText = @"PO NO";
            }
            else
            {
                gvPOProduct.Columns[0].HeaderText = @"INDENT NO";
                 gvPU.Columns[0].HeaderText = @"INDENT NO";
            }

            foreach (ListItem item in ddlpono.Items)
            {
                if (item.Value == "1")
                {
                    item.Attributes.Add("disabled", "disabled");
                    item.Attributes.CssStyle.Add("color", "blue");
                }
                if (item.Value != "1" && item.Value != "0")
                {
                    if (item.Text.Substring(72, 10).Trim() == "EXPORT")
                    {
                        item.Attributes.CssStyle.Add("color", "green");
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

    /// <summary>
    ///  MAIN UPDATE GRID ******************************************************************

    public void LoadUpdateGrid(string FromDate, string ToDate)
    {
        try
        {
            ClsTPUProductionUpdate clspruction = new ClsTPUProductionUpdate();
            gvPUSheet.DataSource = clspruction.BindPUPDATENO(Session["DEPOTID"].ToString(), Session["FINYEAR"].ToString(), FromDate, ToDate);
            gvPUSheet.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public void LoadProduct(string tpuid)
    {
        try
        {
            ClsTPUProductionUpdate TPUProductionUpdate = new ClsTPUProductionUpdate();
            DataTable dtProduct = new DataTable();
            dtProduct = TPUProductionUpdate.LoadProduct(tpuid);
            if (dtProduct.Rows.Count > 0)
            {
                this.ddlProductName.Items.Clear();
                this.ddlProductName.Items.Add(new ListItem("SELECT PRODUCT NAME", "0"));
                this.ddlProductName.DataSource = dtProduct;
                this.ddlProductName.DataValueField = "ID";
                this.ddlProductName.DataTextField = "NAME";
                this.ddlProductName.DataBind();
            }
            else
            {
                this.ddlProductName.Items.Clear();
                this.ddlProductName.Items.Add(new ListItem("SELECT PRODUCT NAME", "0"));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    protected void ddlProductName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProductName.SelectedValue == "0")
            {
                this.ddlpono.Items.Clear();
                this.ddlpono.Items.Add(new ListItem("SELECT PO NO", "0"));
                this.ddlpono.AppendDataBoundItems = true;
            }
            else
            {
                ClsTPUProductionUpdate clsProductionUpdate = new ClsTPUProductionUpdate();

                DataTable dt = new DataTable();
                dt = clsProductionUpdate.GetProduction_QtyCombo(this.ddlProductName.SelectedValue, Session["DEPOTID"].ToString().Trim(), Session["FINYEAR"].ToString().Trim(), Session["TPU"].ToString().Trim());
                List<Productiondetails> PUDetails = new List<Productiondetails>();
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Productiondetails pu = new Productiondetails();
                        pu.POID = dt.Rows[i]["POID"].ToString();
                        pu.PONO = dt.Rows[i]["PONO"].ToString();
                        pu.POQTY = dt.Rows[i]["POQTY"].ToString();
                        pu.PRODUCTIONQTY = dt.Rows[i]["PRODUCTIONQTY"].ToString();
                        pu.AVAILABLEQTY = dt.Rows[i]["REMAININGQTY"].ToString();
                      //  pu.ORDERTYPENAME = dt.Rows[i]["ORDERTYPENAME"].ToString();
                        pu.PODATE = dt.Rows[i]["PODATE"].ToString();
                        PUDetails.Add(pu);
                    }

                    //---------------------------------------------------------------------------------------------
                    ddlpono.Items.Clear();
                    ddlpono.Items.Add(new ListItem("SELECT PO NO", "0"));
                    string text1 = string.Format("{0}{1}{2}{3}{4}",
                    "ORDERNO".PadRight(28, '\u00A0'),
                    "ORDERQTY".PadRight(12, '\u00A0'),
                    "PRODUCTIONQTY".PadRight(16, '\u00A0'),
                    "REMAININGQTY".PadRight(16, '\u00A0'),
                    // "ORDER FOR".PadRight(12, '\u00A0'),
                    "ORDER DATE".PadRight(1, '\u00A0'));

                    ddlpono.Items.Add(new ListItem(text1, "1"));

                    foreach (ListItem item in ddlpono.Items)
                    {
                        if (item.Value == "1")
                        {
                            item.Attributes.Add("disabled", "disabled");
                            item.Attributes.CssStyle.Add("color", "blue");
                        }
                    }

                    foreach (Productiondetails p in PUDetails)
                    {
                        string text = string.Format("{0}{1}{2}{3}{4}",
                        p.PONO.PadRight(28, '\u00A0'),
                        p.POQTY.PadRight(12, '\u00A0'),
                        p.PRODUCTIONQTY.PadRight(16, '\u00A0'),
                        p.AVAILABLEQTY.PadRight(16, '\u00A0'),
                        //p.ORDERTYPENAME.PadRight(12, '\u00A0'),
                        p.PODATE.PadRight(1, '\u00A0'));

                        ddlpono.Items.Add(new ListItem(text, "" + p.POID + ""));
                    }

                    foreach (ListItem item in ddlpono.Items)
                    {
                        if (item.Value != "1" && item.Value != "0")
                        {
                            if (item.Text.Substring(72, 10).Trim() == "EXPORT")
                            {
                                item.Attributes.CssStyle.Add("color", "green");
                            }
                        }
                    }
                }
                else
                {

                    ddlpono.Items.Clear();
                    ddlpono.Items.Add(new ListItem("SELECT PO NO", "0"));
                    string text1 = string.Format("{0}{1}{2}{3}{4}",
                    "ORDERNO".PadRight(28, '\u00A0'),
                    "ORDERQTY".PadRight(12, '\u00A0'),
                    "PRODUCTIONQTY".PadRight(16, '\u00A0'),
                    "REMAININGQTY".PadRight(16, '\u00A0'),
                    //"ORDER FOR".PadRight(12, '\u00A0'),
                    "ORDER DATE".PadRight(1, '\u00A0'));

                    ddlpono.Items.Add(new ListItem(text1, "1"));

                    foreach (ListItem item in ddlpono.Items)
                    {
                        if (item.Value == "1")
                        {
                            item.Attributes.Add("disabled", "disabled");
                            item.Attributes.CssStyle.Add("color", "blue");
                        }
                    }

                    foreach (Productiondetails p in PUDetails)
                    {
                        string text = string.Format("{0}{1}{2}{3}{4}",
                        p.PONO.PadRight(28, '\u00A0'),
                        p.POQTY.PadRight(12, '\u00A0'),
                        p.PRODUCTIONQTY.PadRight(16, '\u00A0'),
                        p.AVAILABLEQTY.PadRight(16, '\u00A0'),
                       // p.ORDERTYPENAME.PadRight(12, '\u00A0'),
                        p.PODATE.PadRight(1, '\u00A0'));

                        ddlpono.Items.Add(new ListItem(text, "" + p.POID + ""));
                    }

                    foreach (ListItem item in ddlpono.Items)
                    {
                        if (item.Value != "1" && item.Value != "0")
                        {
                            if (item.Text.Substring(72, 10).Trim() == "EXPORT")
                            {
                                item.Attributes.CssStyle.Add("color", "green");
                            }
                        }
                    }

                    MessageBox1.ShowInfo("<b>No Production Order available</b>");
                }

            }           
        }
        
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void ddlpono_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlpono.SelectedValue != "0")
            {
                string manufacturedate = dtcurr.ToString(date).Replace('-', '/');
                pnlDisplay.Style["display"] = "";
                LoadPOProductItem(this.ddlpono.SelectedValue.ToString().Trim(), this.ddlpono.SelectedValue.ToString().Trim(), manufacturedate, this.ddlProductName.SelectedValue.Trim());
                if (Session["TPU"].ToString() == "T")
                {
                    gvPOProduct.Columns[0].HeaderText = @"PO NO";
                }
                else
                {
                    gvPOProduct.Columns[0].HeaderText = @"INDENT NO";
                }
            }
            else
            {
                gvPOProduct.DataSource = null;
                gvPOProduct.DataBind();
            }
        }

        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btngridsave_Click(object sender, EventArgs e)
    {

        try
        {
            ClsEntryLock objLock = new ClsEntryLock();
            bool ObjDate = objLock.EntryLock(this.txtfromdate.Text.Trim(), Convert.ToString(HttpContext.Current.Session["FINYEAR"]).Trim());
            if (ObjDate == true)
            {
                ClsTPUProductionUpdate clspruction = new ClsTPUProductionUpdate();

                string puid = Hdnn_Fld.Value.ToString();

                if (clspruction.GetQCstatus(puid) == "1")
                {
                    MessageBox1.ShowInfo("<b><font color='green'>QA already done,not allow to edit</font></b>.", 60, 400);
                    return;
                }

                this.LoadProduct(Session["DEPOTID"].ToString().Trim());
                this.ddlpono.Items.Clear();
                this.ddlpono.Items.Add(new ListItem("SELECT PO NO", "0"));
                this.ddlpono.AppendDataBoundItems = true;
                this.ddlProductName.SelectedValue = "0";

                DataTable dt = new DataTable();
                dt = clspruction.BindPUHeader(puid);
                if (dt.Rows.Count >= 0)
                {
                    txtPUdate.Text = dt.Rows[0]["PUDATE"].ToString();
                    imgPopup.Visible = false;
                    lblPUID.Text = dt.Rows[0]["PUNO"].ToString();
                }



                gvPOProduct.DataSource = null;
                gvPOProduct.DataBind();

                pnlAdd.Style["display"] = "";
                pnlAdd1.Style["display"] = "";

                pnlDisplay.Style["display"] = "none";

                DataTable Newddt = new DataTable();

                //Newddt = (DataTable)Session["gvpu"];
                if (Session["TPU"].ToString() == "T")
                {
                    Newddt = clspruction.BindPU(Hdnn_Fld.Value.ToString());
                }
                else
                {
                    Newddt = clspruction.BindFACTORY(Hdnn_Fld.Value.ToString());
                }
                gvPU.DataSource = Newddt;
                gvPU.DataBind();
                Session["gvpu1"] = Newddt;
                this.txtcasepack.Text = Convert.ToString(TotalCasePack());
                pnlDisplay3.Style["display"] = "none";
                dinadd.Style["display"] = "none";

                pnlDisplay2.Style["display"] = "";
                divautogenerate.Style["display"] = "";
                divautogenerateheader.Style["display"] = "";
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

    public void LoadPUSheet()
    {
        try
        {
            ClsTPUProductionUpdate clspruction = new ClsTPUProductionUpdate();
            dt = clspruction.BindPU(Hdnn_Fld.Value.ToString());
            Session["gvpu"] = dt;
            gvPOProduct.DataSource = dt;
            gvPOProduct.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// </summary>***************************************************************************



    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            this.LoadProduct(Session["DEPOTID"].ToString().Trim());
            this.ddlpono.Items.Clear();
            this.ddlpono.Items.Add(new ListItem("SELECT PO NO", "0"));
            this.ddlpono.AppendDataBoundItems = true;
            this.ddlProductName.SelectedValue = "0";
            txtfromdate.Text = dtcurr.ToString(date).Replace('-', '/');
            txttodate.Text = dtcurr.ToString(date).Replace('-', '/');
            txtPUdate.Text = dtcurr.ToString(date).Replace('-', '/');
            ddlPO.SelectedValue = "0";
            Hdnn_Fld.Value = "";
            pnlAdd.Style["display"] = "";
            pnlAdd1.Style["display"] = "";
            
            pnlDisplay3.Style["display"] = "none";
            Session["gvpu"] = null;
            Session["gvpu1"] = null;
            lblPUID.Text = "";
            dinadd.Style["display"] = "none";
            divautogenerate.Style["display"] = "none";
            divautogenerateheader.Style["display"] = "none";
            gvPU.ClearPreviousDataSource();
            gvPU.DataBind();
            if (Session["TPU"].ToString() == "T")
            {
                LgndTPU.InnerText="PURCHASE ORDER DATE";
                gvPOProduct.Columns[0].HeaderText = @"PO NO";
            }
            else
            {
                LgndTPU.InnerText = "INDENT DETAILS";
                gvPOProduct.Columns[0].HeaderText = @"INDENT NO";
            }
            gvPOProduct.ClearPreviousDataSource();
            gvPOProduct.DataSource = null;
            gvPOProduct.DataBind();
            pnlDisplay.Style["display"] = "";
            imgPopup.Visible = true;
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btngvfill_Click(object sender, EventArgs e)
    {
        try
        {          
             LoadPO();         
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }


    public void LoadPOProductItem(string pono, string poid,string mfgdate,string ProductID)
    {
        try
        {
            ClsTPUProductionUpdate clspruction = new ClsTPUProductionUpdate();
            if (Session["TPU"].ToString() == "T")
            {
                dt = clspruction.BindPOProctuct(pono, poid, mfgdate, ProductID);
            }
            else
            {
                dt = clspruction.BindFactoryProctuct(pono, poid, mfgdate, ProductID);
            }
            Session["gvpu"] = dt;
            gvPOProduct.DataSource = dt;
            gvPOProduct.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void LoadPO()
    {
        try
        {
            ClsTPUProductionUpdate clspruction = new ClsTPUProductionUpdate();
            ddlPO.Items.Clear();
            if (Session["TPU"].ToString() == "T")
            {
                ddlPO.Items.Insert(0, new ListItem("SELECT PO NO", "0"));
                ddlPO.DataSource = clspruction.BindPO(txtfromdate.Text.Trim(), txttodate.Text.Trim(),/*Session["TPUID"].ToString()*/Session["DEPOTID"].ToString());
            }
            else
            {
                ddlPO.Items.Insert(0, new ListItem("SELECT INDENT NO", "0"));
                ddlPO.DataSource = clspruction.BindFactory(txtfromdate.Text.Trim(), txttodate.Text.Trim(),/*Session["TPUID"].ToString()*/ Session["DEPOTID"].ToString());
            }
            ddlPO.DataTextField = "PONO";
            ddlPO.DataValueField = "POID";
            ddlPO.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlPO_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            pnlAdd.Style["display"] = "none";
            pnlAdd1.Style["display"] = "none";
            pnlDisplay.Style["display"] = "none";
            pnlDisplay2.Style["display"] = "none";
            pnlDisplay3.Style["display"] = "";
            Session["gvpu"] = null;
            Session["gvpu1"] = null;
            ddlPO.SelectedValue = "0";
            dinadd.Style["display"] = "";
            this.ddlpono.Items.Clear();
            this.ddlpono.Items.Add(new ListItem("SELECT PO NO", "0"));
            this.ddlpono.AppendDataBoundItems = true;
            this.ddlProductName.SelectedValue = "0";
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

    protected void  btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsTPUProductionUpdate clspruction = new ClsTPUProductionUpdate();
            bool FLAG = false;
            int Counter = 0;
            int state = 0;
            string expdate = string.Empty;
            string mastermfgdate = string.Empty;
            string masterexpdate = string.Empty;

            dt = (DataTable)Session["gvpu"];
            gvPOProduct.DataSource = dt;
            gvPOProduct.DataBind();
            pnlDisplay2.Style["display"] = "";

            if (Session["TPU"].ToString() == "T")
            {
                gvPU.Columns[0].HeaderText = @"PO NO";
            }
            else
            {
                gvPU.Columns[0].HeaderText = @"INDENT NO";
            }
            if (Session["gvpu1"] != null)
            {
                dt1 = (DataTable)Session["gvpu1"];
            }
            else
            {
                dt1 = dt.Clone();
            }

           
            //Validation on PO grid for checking the QTY OF PO & PRODUCTIN QTY

            for (int i = 0; i < gvPOProduct.RowsInViewState.Count; i++)
            {
                GridDataControlFieldCell actulaqtycell = gvPOProduct.RowsInViewState[i].Cells[4] as GridDataControlFieldCell;
                GridDataControlFieldCell uomcell = gvPOProduct.RowsInViewState[i].Cells[5] as GridDataControlFieldCell;
                GridDataControlFieldCell manfdatecell = gvPOProduct.RowsInViewState[i].Cells[8] as GridDataControlFieldCell;
                GridDataControlFieldCell batchnocell = gvPOProduct.RowsInViewState[i].Cells[9] as GridDataControlFieldCell;
                GridDataControlFieldCell expnocell = gvPOProduct.RowsInViewState[i].Cells[10] as GridDataControlFieldCell;

                HiddenField hiddenField = actulaqtycell.FindControl("ProductName") as HiddenField;
                TextBox txtQTY = actulaqtycell.FindControl("Quantity") as TextBox;
                DropDownList ddlpackingsize = uomcell.FindControl("ddlUOM") as DropDownList;

                TextBox txtmanf = manfdatecell.FindControl("txtmanfsate") as TextBox;
                TextBox txtbatch = batchnocell.FindControl("txtbatchno") as TextBox;
                TextBox txtexp = expnocell.FindControl("txtexpsate") as TextBox;

                string PRODUCT = gvPOProduct.Rows[i].Cells[7].Text;
                string POID = gvPOProduct.Rows[i].Cells[6].Text;
                string balanceQty = gvPOProduct.Rows[i].Cells[3].Text;

                string str1 = null;
                string[] strArr1 = null;
                str1 = dt.Rows[i][2].ToString();
                char[] splitchar1 = { ' ' };
                strArr1 = str1.Split(splitchar1);

                string str = null;
                string[] strArr = null;
                str = dt.Rows[i][3].ToString();
                char[] splitchar = { ' ' };
                strArr = str.Split(splitchar);

                int numberOfRecords = dt1.Select("PRODUCTID = '" + PRODUCT + "' AND POID = '" + POID + "'").Length;

                if (numberOfRecords > 0)
                {
                    MessageBox1.ShowInfo("<b>Record already Exists..</b>");
                    return;
                }

                if (Convert.ToDecimal(txtQTY.Text.ToString()) > 0)
                {
                    
                    DataTable dtbatchcheck = new DataTable();
                    dtbatchcheck = clspruction.MasterBatchDetailsCheck(PRODUCT, txtbatch.Text.Trim(), 0, txtmanf.Text.Trim(), txtexp.Text.Trim(), this.Session["FinYear"].ToString().Trim());

                   

                    string existsbatchno=string.Empty;
                    existsbatchno = clspruction.ExistsBatchno(PRODUCT, txtbatch.Text.Trim());
                    DataTable podate = new DataTable();
                    if (Session["TPU"].ToString() == "T")
                    {
                        podate = clspruction.FetchPODate(ddlpono.SelectedValue);
                    }
                    else
                    {
                        podate = clspruction.FetchFACTORYDate(ddlpono.SelectedValue);
                    }
                    dt.Rows[i][4] = txtQTY.Text.ToString();
                    dt.Rows[i][5] = ddlpackingsize.SelectedItem.ToString();
                    dt.Rows[i][6] = ddlpackingsize.SelectedValue.ToString();

                    decimal PRODUCTIONQTY = 0;
                    decimal ORDERQTY = 0;
                    
                    PRODUCTIONQTY =Convert.ToDecimal(dt.Rows[i][4]);
                    ORDERQTY = Convert.ToDecimal(dt.Rows[i][3]);

                    if(PRODUCTIONQTY> ORDERQTY)
                    {
                        MessageBox1.ShowWarning("Production Qty Cannont Be Greater Than OrderQty");
                        return;
                    }

                    if (FLAG == true)
                    {
                        break;
                    }

                    if (FLAG == false)
                    {
                        dt1.ImportRow(dt.Rows[i]);
                    }
                }
                else
                {
                    FLAG = true;
                    state = 5;
                    break;
                }
            }

            if (FLAG == true)
            {
               
            }

            if (FLAG == false)
            {
                Session["gvpu1"] = dt1;
                gvPU.DataSource = dt1;
                gvPU.DataBind();
                if (Session["TPU"].ToString() == "T")
                {
                    gvPOProduct.Columns[0].HeaderText = @"PO NO";
                }
                else
                {
                    gvPOProduct.Columns[0].HeaderText = @"INDENT NO";
                }

                this.txtcasepack.Text = Convert.ToString(TotalCasePack());

                this.gvPOProduct.DataSource = null;
                this.gvPOProduct.DataBind();
                this.ddlpono.Items.Clear();
                this.ddlpono.Items.Add(new ListItem("SELECT PO NO", "0"));
                this.ddlpono.AppendDataBoundItems = true;
                this.ddlProductName.SelectedValue = "0";
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    public decimal TotalCasePack()
    {
        decimal casepack = 0;
        decimal convertioncasepack = 0;
        ClsTPUProductionUpdate clspruction = new ClsTPUProductionUpdate();

        DataTable dtproduction = (DataTable)HttpContext.Current.Session["gvpu1"];
        foreach (DataRow dr in dtproduction.Rows)
        {
            convertioncasepack = clspruction.GetPackingSize_OnCall(dr["PRODUCTID"].ToString().Trim(), dr["UOMID"].ToString().Trim(), Convert.ToDecimal(dr["ACTLQTY"].ToString().Trim()));
            casepack = casepack + convertioncasepack;
        }

        return casepack;
    }


    protected void btnSubmitFinal_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtnewid = new DataTable();
            ClsTPUProductionUpdate clspruction = new ClsTPUProductionUpdate();

            if (Session["gvpu1"] != null)
            {
                dt1 = (DataTable)Session["gvpu1"];
            }
            //string puid, string poid, string pono, string prodid, string prodname, decimal orderqty, decimal rmianingqty, decimal actualqty, string tpuid, string tpuname, string  remarks,DateTime pudate

            if (dt1.Rows.Count > 0)
            {
                string poid = string.Empty;
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    poid = poid + "'" + dt1.Rows[i]["POID"].ToString() + "'" + ",";
                }
                poid = poid.Substring(0, poid.Length - 1);

                DataTable podate = new DataTable();
                string Tag = "";
                //if (Session["TPU"].ToString() == "T")
                //{
                //    podate = clspruction.BindMaxPODate(poid);
                //    Tag = "T";
                //}
                //else
                //{
                    podate = clspruction.BindMaxFactoryDatekkg(poid);/*add by p.basu 301120202 for max productiondate*/
                    Tag = "F";
                //}

                int dtpodate = Convert.ToInt32(podate.Rows[0]["PODATE"].ToString());
                int dtpudate = Convert.ToInt32(Conver_To_ISO(txtPUdate.Text.Trim()));

                if (dtpodate > dtpudate)
                {
                    //string message = "alert('Production Update date can not less than PO date: " + podate.Rows[0]["SHOWPODATE"].ToString() + "!')";
                    //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    if (Session["TPU"].ToString() == "T")
                    {
                        MessageBox1.ShowInfo("Production date can not less than <b><font color='red'>PO date</font></b>!", 60, 500);
                    }
                    else
                    {
                        MessageBox1.ShowInfo("Production date can not less than <b><font color='red'>Indent date</font></b>!", 60, 500);
                    }


                }


                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    string str = null;
                    string[] strArr = null;
                    str = dt1.Rows[i][2].ToString();
                    char[] splitchar = { ' ' };
                    strArr = str.Split(splitchar);

                    string str1 = null;
                    string[] strArr1 = null;
                    str1 = dt1.Rows[i][3].ToString();
                    char[] splitchar1 = { ' ' };
                    strArr1 = str1.Split(splitchar1);

                    if (i == 0)
                    {
                        dtnewid = clspruction.SavePUSheetHeader(Hdnn_Fld.Value, dt1.Rows[i][7].ToString(), dt1.Rows[i][0].ToString(), dt1.Rows[i]["PRODUCTID"].ToString(), dt1.Rows[i][1].ToString(), Convert.ToDecimal(strArr[0]), Convert.ToDecimal(strArr1[0]), Convert.ToDecimal(dt1.Rows[i][4].ToString()), Session["DEPOTID"].ToString(), Session["DEPOTNAME"].ToString(), Convert.ToString(""), txtPUdate.Text.Trim(), dt1.Rows[i][5].ToString().Trim(), dt1.Rows[i][6].ToString().Trim(), dt1.Rows[i]["BATCHNO"].ToString().Trim(), dt1.Rows[i][10].ToString().Trim(), dt1.Rows[i][11].ToString().Trim(), Tag, Convert.ToDecimal(this.txtcasepack.Text.Trim()));
                    }
                    else
                    {
                        if (dtnewid.Rows[0][0].ToString() == "1")
                        {
                            dtnewid.Rows[0][0] = Hdnn_Fld.Value;
                        }

                        dtnewid = clspruction.SavePUSheetDetails(Hdnn_Fld.Value, dtnewid.Rows[0][0].ToString(), dt1.Rows[i][7].ToString(), dt1.Rows[i][0].ToString(), dt1.Rows[i]["PRODUCTID"].ToString(), dt1.Rows[i][1].ToString(), Convert.ToDecimal(strArr[0]), Convert.ToDecimal(strArr1[0]), Convert.ToDecimal(dt1.Rows[i][4].ToString()), HttpContext.Current.Session["DEPOTID"].ToString(), Convert.ToString(HttpContext.Current.Session["DEPOTNAME"].ToString()), Convert.ToString(""), txtPUdate.Text.Trim(), dt1.Rows[i][5].ToString(), dt1.Rows[i][6].ToString(), dt1.Rows[i]["BATCHNO"].ToString(), dt1.Rows[i][10].ToString(), dt1.Rows[i][11].ToString());
                    }
                }
                if (dtnewid.Rows.Count > 0)
                {
                    if (dtnewid.Rows[0][0].ToString() != "1")
                    {
                        lblPUID.Text = dtnewid.Rows[0][0].ToString();
                        //txtBatchNo.Text = "";
                        //string message = "alert('Production Update No " + lblPUID.Text + " Saved Successfully!')";
                        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        MessageBox1.ShowSuccess("Production Update No <b><font color='green'>" + lblPUID.Text + "</font></b> saved successfully!", 60, 600);
                    }
                    else
                    {
                        lblPUID.Text = "";
                        //txtBatchNo.Text = "";
                        //string message = "alert('Record Updated Successfully!')";
                        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        MessageBox1.ShowSuccess("<b><font color='green'>Production updated successfully!</font></b>");
                    }
                    pnlAdd.Style["display"] = "none";
                    pnlAdd1.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "none";
                    pnlDisplay2.Style["display"] = "none";
                    pnlDisplay3.Style["display"] = "";
                    Session["gvpu"] = null;
                    Session["gvpu1"] = null;
                    LoadUpdateGrid(this.txtsearchfromdate.Text.Trim(), this.txtsearchtodate.Text.Trim());
                    dinadd.Style["display"] = "";
                    //txtBatchNo.Text = "";
                    txtPUdate.Text = "";
                    lblPUID.Text = "";
                    dinadd.Visible = true;
                    gvPU.ClearPreviousDataSource();
                    imgPopup.Visible = true;
                    ddlPO.SelectedValue = "0";
                }
                else
                {
                    lblPUID.Text = "";
                    //string message = "alert('Record not Saved Successfully!')";
                    MessageBox1.ShowError("Error saving records..!");
                    //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                }
            }
            else
            {
                MessageBox1.ShowInfo("Please entry atleast 1 production update");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }

    protected void gvPOProduct_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == GridRowType.DataRow && gvPOProduct.RowsInViewState.Count > 0)
            {
                ClsTPUProductionUpdate clspruction = new ClsTPUProductionUpdate();
                GridDataControlFieldCell cell = e.Row.Cells[5] as GridDataControlFieldCell;
                GridDataControlFieldCell cell1 = e.Row.Cells[4] as GridDataControlFieldCell;
                DropDownList ddlpackingsize = cell.FindControl("ddlUOM") as DropDownList;
                HiddenField hiddenField = cell1.FindControl("ProductName") as HiddenField;
                HiddenField hiddenFieldpid = cell1.FindControl("ProductID") as HiddenField;

                ddlpackingsize.Items.Clear();
                //ddlpackingsize.Items.Add(new ListItem("-- SELECT--", "0"));
                ddlpackingsize.AppendDataBoundItems = true;
                ddlpackingsize.DataSource = clspruction.BindPackingSize(hiddenFieldpid.Value.ToString());
                ddlpackingsize.DataValueField = "PACKSIZEID_FROM";
                ddlpackingsize.DataTextField = "PACKSIZEName_FROM";
                ddlpackingsize.DataBind();

                if (gvPOProduct.RowsInViewState.Count > e.Row.RowIndex)
                {
                    GridDataControlFieldCell cellqty = e.Row.Cells[4] as GridDataControlFieldCell;
                    TextBox textBoxqty = cellqty.FindControl("Quantity") as TextBox;

                    GridDataControlFieldCell cellmfg = e.Row.Cells[8] as GridDataControlFieldCell;
                    TextBox textBoxmfg = cellmfg.FindControl("txtmanfsate") as TextBox;

                    GridDataControlFieldCell cellbatch = e.Row.Cells[9] as GridDataControlFieldCell;
                    TextBox textBoxbatchno = cellbatch.FindControl("txtbatchno") as TextBox;

                    GridDataControlFieldCell cellexp = e.Row.Cells[10] as GridDataControlFieldCell;
                    TextBox textBoxexp = cellexp.FindControl("txtexpsate") as TextBox;

                    GridDataControlFieldCell cellpacksize = e.Row.Cells[5] as GridDataControlFieldCell;
                    DropDownList ddlpacksize = cellpacksize.FindControl("ddlUOM") as DropDownList;

                    if (gvPOProduct.RowsInViewState != null)
                    {

                        if (gvPOProduct.RowsInViewState[e.Row.RowIndex].Cells.Count > 3)
                        {

                            GridDataControlFieldCell cellInViewState = gvPOProduct.RowsInViewState[e.Row.RowIndex].Cells[4] as GridDataControlFieldCell;
                            TextBox textBoxInViewState = cellInViewState.FindControl("Quantity") as TextBox;
                            textBoxqty.Text = textBoxInViewState.Text;
                        }

                        if (gvPOProduct.RowsInViewState[e.Row.RowIndex].Cells.Count > 7)
                        {

                            GridDataControlFieldCell cellInViewState = gvPOProduct.RowsInViewState[e.Row.RowIndex].Cells[8] as GridDataControlFieldCell;
                            TextBox textBoxInViewState = cellInViewState.FindControl("txtmanfsate") as TextBox;
                            textBoxmfg.Text = textBoxInViewState.Text;
                        }

                        if (gvPOProduct.RowsInViewState[e.Row.RowIndex].Cells.Count > 8)
                        {

                            GridDataControlFieldCell cellInViewState = gvPOProduct.RowsInViewState[e.Row.RowIndex].Cells[9] as GridDataControlFieldCell;
                            TextBox textBoxInViewState = cellInViewState.FindControl("txtbatchno") as TextBox;
                            textBoxbatchno.Text = textBoxInViewState.Text;
                        }

                        if (gvPOProduct.RowsInViewState[e.Row.RowIndex].Cells.Count > 9)
                        {

                            GridDataControlFieldCell cellInViewState = gvPOProduct.RowsInViewState[e.Row.RowIndex].Cells[10] as GridDataControlFieldCell;
                            TextBox textBoxInViewStateexp = cellInViewState.FindControl("txtexpsate") as TextBox;
                            textBoxexp.Text = textBoxInViewStateexp.Text;
                        }

                        if (gvPOProduct.RowsInViewState[e.Row.RowIndex].Cells.Count > 4)
                        {

                            GridDataControlFieldCell cellInViewState = gvPOProduct.RowsInViewState[e.Row.RowIndex].Cells[5] as GridDataControlFieldCell;
                            DropDownList packsizeInViewState = cellInViewState.FindControl("ddlUOM") as DropDownList;
                            ddlpacksize.SelectedValue = packsizeInViewState.SelectedValue.ToString();
                        }
                    }
                }
            }
            //if (e.Row.RowType == GridRowType.DataRow)
            //{
            //    GridDataControlFieldCell Remainingqty = e.Row.Cells[3] as GridDataControlFieldCell;
            //    string status = Remainingqty.Text.ToString();

            //    if (status == "0")
            //    {
            //        Remainingqty.ForeColor = Color.Red;
            //    }
            //    else
            //    {
            //        Remainingqty.ForeColor = Color.Green ;
            //    }
            //}
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void DeleteRecord(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            if (e.Record["PRODUCTID"] != "")
            {
                int ID = 0;

                if (Session["gvpu1"] != null)
                {

                    dt2 = (DataTable)Session["gvpu1"];
                }


                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    if (dt2.Rows[i]["POID"].ToString().Trim() == e.Record["POID"].ToString().Trim() && dt2.Rows[i]["PRODUCTID"].ToString().Trim() == e.Record["PRODUCTID"].ToString().Trim() && dt2.Rows[i]["BATCHNO"].ToString().Trim() == e.Record["BATCHNO"].ToString().Trim())
                    {
                        dt2.Rows[i].Delete();
                        dt2.AcceptChanges();
                        ID = 1;
                    }

                }

                if (ID > 0)
                {
                    Session["gvpu1"] = dt2;
                    gvPU.DataSource = dt2;
                    gvPU.DataBind();
                    e.Record["Error"] = "Record Deleted Successfully. ";

                    this.txtcasepack.Text = Convert.ToString(TotalCasePack());
                }
                else
                {
                    e.Record["Error"] = "Error On Deleting. ";
                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }


    protected void btnCancelFinal_Click(object sender, EventArgs e)
    {
        try
        {
            pnlAdd.Style["display"] = "none";
            pnlAdd1.Style["display"] = "none";
            pnlDisplay.Style["display"] = "none";
            pnlDisplay2.Style["display"] = "none";
            pnlDisplay3.Style["display"] = "";
            Session["gvpu"] = null;
            Session["gvpu1"] = null;
            //txtBatchNo.Text = "";
            txtPUdate.Text = "";
            lblPUID.Text = "";
            ddlPO.SelectedValue = "0";
            dinadd.Style["display"] = "";
            imgPopup.Visible = true;
            this.ddlpono.Items.Clear();
            this.ddlpono.Items.Add(new ListItem("SELECT PO NO", "0"));
            this.ddlpono.AppendDataBoundItems = true;
            this.ddlProductName.SelectedValue = "0";

            //pnlAdd1.Style["display"] = "";
            divbtnsave.Style["display"] = "";
            gvPU.Columns[11].Visible = true;
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btnbBack_Click(object sender, EventArgs e)
    {
        try
        {
            pnlAdd.Style["display"] = "none";
            pnlAdd1.Style["display"] = "none";
            pnlDisplay.Style["display"] = "none";
            pnlDisplay2.Style["display"] = "none";
            pnlDisplay3.Style["display"] = "";
            dinadd.Style["display"] = "";
            Session["gvpu"] = null;
            Session["gvpu1"] = null;
            //txtBatchNo.Text = "";
            txtPUdate.Text = "";
            lblPUID.Text = "";
            ddlPO.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void txtmanfsate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            dt = (DataTable)Session["gvpu"];
            gvPOProduct.DataSource = dt;
            gvPOProduct.DataBind();

            for (int i = 0; i < gvPOProduct.RowsInViewState.Count; i++)
            {
                ClsTPUProductionUpdate clspruction = new ClsTPUProductionUpdate();
                GridDataControlFieldCell manfdatecell = gvPOProduct.RowsInViewState[i].Cells[8] as GridDataControlFieldCell;
                GridDataControlFieldCell expnocell = gvPOProduct.RowsInViewState[i].Cells[10] as GridDataControlFieldCell;

                TextBox txtmanf = manfdatecell.FindControl("txtmanfsate") as TextBox;
                TextBox txtexp = expnocell.FindControl("txtexpsate") as TextBox;

                string PRODUCT = gvPOProduct.Rows[i].Cells[7].Text;
                string expdate = string.Empty;
                expdate = clspruction.GetProductExpirydate(PRODUCT, txtmanf.Text.ToString());
                txtexp.Text = expdate;
                dt.Rows[i]["EXPRDATE"] = expdate;
            }

            gvPOProduct.DataSource = dt;
            gvPOProduct.DataBind();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnpusearch_Click(object sender, EventArgs e)
    {
        try
        {
            LoadUpdateGrid(this.txtsearchfromdate.Text.Trim(), this.txtsearchtodate.Text.Trim());
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btngridView_Click(object sender, EventArgs e)
    {
        try
        {
            ClsTPUProductionUpdate clspruction = new ClsTPUProductionUpdate();
            DataTable dt = new DataTable();
            dt = clspruction.BindPUHeader(Hdnn_Fld.Value.ToString().Trim());
            if (dt.Rows.Count >= 0)
            {
                txtPUdate.Text = dt.Rows[0]["PUDATE"].ToString();
                imgPopup.Visible = false;
                lblPUID.Text = dt.Rows[0]["PUNO"].ToString();
            }

            pnlAdd.Style["display"] = "";
            pnlAdd1.Style["display"] = "";

            pnlDisplay.Style["display"] = "none";

            pnlAdd1.Style["display"] = "none";
            divbtnsave.Style["display"] = "none";
            gvPU.Columns[11].Visible = false;

            DataTable Newddt = new DataTable();
            if (Session["TPU"].ToString() == "T")
            {
                Newddt = clspruction.BindPU(Hdnn_Fld.Value.ToString());
            }
            else
            {
                Newddt = clspruction.BindFACTORY(Hdnn_Fld.Value.ToString());
            }
            gvPU.DataSource = Newddt;
            gvPU.DataBind();
            Session["gvpu1"] = Newddt;
            this.txtcasepack.Text = Convert.ToString(TotalCasePack());
            pnlDisplay3.Style["display"] = "none";
            dinadd.Style["display"] = "none";
            
            pnlDisplay2.Style["display"] = "";
            divautogenerate.Style["display"] = "";
            divautogenerateheader.Style["display"] = "";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnGridDelete_Click(object sender, EventArgs e)
    {
        try
        {
            ClsTPUProductionUpdate clspruction = new ClsTPUProductionUpdate();
            if (clspruction.GetQCstatus(Hdnn_Fld.Value.ToString().Trim()) == "1")
            {
                MessageBox1.ShowInfo("<b><font color='green'>QA already done,not allow to delete!!!</font></b>.", 60, 400);
                return;
            }
            else
            {
                int flag = clspruction.DeleteProduction(Hdnn_Fld.Value.ToString().Trim());
                if (flag == 1)
                {
                    MessageBox1.ShowSuccess("Record Deleted Successfully!");
                    this.LoadUpdateGrid(this.txtsearchfromdate.Text.Trim(), this.txtsearchtodate.Text.Trim());
                }
                else
                {
                    MessageBox1.ShowSuccess("Record Deleted UnSuccessful!");
                }
            }
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
        DateTime datevalidation = DateTime.Today.Date;
        DateTime beforedays = datevalidation.AddDays(-7);
        /*CalendarExtender3.EndDate = datevalidation;
        CalendarExtender3.StartDate = beforedays;*/

        string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
        string startyear = finyear.Substring(0, 4);
        int startyear1 = Convert.ToInt32(startyear);
        string endyear = finyear.Substring(5);
        int endyear1 = Convert.ToInt32(endyear);
        DateTime oDate = new DateTime(startyear1, 04, 01);
        DateTime cDate = new DateTime(endyear1, 03, 31);
        DateTime today1 = DateTime.Now;
        CalendarExtender3.StartDate = oDate;
        CalendarExtender5.StartDate = oDate;
        CalendarExtender4.StartDate = beforedays;
        CalendarExtender1.StartDate = oDate;
        CalendarExtender2.StartDate = oDate;
        //CalendarExtenderDespatchDate.StartDate = beforedays;
        if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
        {
            this.txtsearchfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtsearchtodate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtPUdate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            this.txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            CalendarExtender3.EndDate = today1;
            CalendarExtender5.EndDate = today1;
            CalendarExtender4.EndDate = today1;
            CalendarExtender1.EndDate = today1;
            CalendarExtender2.EndDate = today1;            
        }
        else
        {
            this.txtsearchfromdate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtsearchtodate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtPUdate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtfromdate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            this.txttodate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            CalendarExtender3.EndDate = cDate;
            CalendarExtender5.EndDate = cDate;
            CalendarExtender4.EndDate = cDate;
            CalendarExtender1.EndDate = cDate;
            CalendarExtender2.EndDate = cDate;
        }
    }
    #endregion
}