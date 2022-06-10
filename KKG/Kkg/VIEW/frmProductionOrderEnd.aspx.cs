#region developer Info
/*Developer Name:Pritam Basu
Page Purpose:Production Order End
Start Date:09-03-2021
End Date:20-03-2021
Project:KKG */
#endregion

#region nameSpace
using PPBLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion

public partial class VIEW_frmProductionOrderEnd : System.Web.UI.Page
{
    /*PPBLL*/
    ClsProductionOrder objClsProductionOrder = new ClsProductionOrder();
    DataTable objDt = new DataTable();
    decimal glProddutionStartQty = 0;
    decimal glProddutionRunningQty = 0;
    decimal glProddutionEndQty = 0;


    #region Pageload
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = new DataTable(); ;
        try
        {
            if (!IsPostBack)
            {

                LoadProductionOrderWithReturnGrid();
                this.pnlDisplay.Style["display"] = "";
                this.pnlAdd.Style["display"] = "none";
                DateLock();
            }
        }
        catch (ArgumentNullException ex)
        {
            //code specifically for a ArgumentNullException
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        catch (WebException ex)
        {
            //code specifically for a WebException
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        catch (Exception ex)
        {
            //code for any other type of exception
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        finally
        {
            //call this if exception occurs or not
            //in this example, dispose the WebClient
            //dt?.Dispose();
        }

    }

    #endregion

    #region methods

    public void LoadMainProduct()
    {
        try
        {
            string mode = string.Empty;
            mode = "MainItem";
            string userId = HttpContext.Current.Session["USERID"].ToString();
            objDt = objClsProductionOrder.BindDataModeWise(mode, userId, "");
            if (objDt.Rows.Count > 0)
            {
                this.ddlProduct.Items.Clear();
                this.ddlProduct.Items.Insert(0, new ListItem("Select Product", "0"));
                this.ddlProduct.DataSource = objDt;
                this.ddlProduct.DataTextField = "PRODUCTNAME";
                this.ddlProduct.DataValueField = "PRODUCTID";
                this.ddlProduct.DataBind();
            }
            else
            {
                MessageBox1.ShowInfo("No Product Found");
                return;
            }
        }
        catch (Exception ex)
        {
            //code for any other type of exception
            string message = "alert('" + ex.Message.Replace("'", "") + "')";

        }
        finally
        {
            //call this if exception occurs or not
            //in this example, dispose the WebClient
            //objDt?.Dispose();
        }
    }

    public void LoadProductionOrderWithReturnGrid()
    {
       
        objDt = objClsProductionOrder.FetchProductionEnd(txtfromdateins.Text, txttodateins.Text, Session["UserID"].ToString(), Session["FINYEAR"].ToString(),this.txtProductSearch.Text);
        if (objDt.Rows.Count > 0)
        {
            this.grdReturn.DataSource = objDt;
            this.grdReturn.DataBind();
        }
        else
        {
            this.grdReturn.DataSource = null;
            this.grdReturn.DataBind();
        }
    }

    /*Only Those Production Orders are come here after production order Process are start*/
    public void LoadProductionOrder()
    {
        try
        {
            string mode = string.Empty;
            string productId = this.ddlProduct.SelectedValue.Trim();
            mode = "StartProcess";
            string userId = HttpContext.Current.Session["USERID"].ToString();
            objDt = objClsProductionOrder.BindDataModeWise(mode, userId, productId);
            if (objDt.Rows.Count > 0)
            {
                this.ddlProductionNumber.Items.Clear();
                this.ddlProductionNumber.Items.Insert(0, new ListItem("Select Production", "0"));
                this.ddlProductionNumber.DataSource = objDt;
                this.ddlProductionNumber.DataTextField = "PRODUCTIONNO";
                this.ddlProductionNumber.DataValueField = "PRODUCTION_ORDERID";
                this.ddlProductionNumber.DataBind();
                /*for grid clear*/
                this.gvItem.DataSource = null;
                this.gvItem.DataBind();
                this.gvBOM.DataSource = null;
                this.gvBOM.DataBind();
                /*for grid clear*/
            }
            else
            {
                MessageBox1.ShowInfo("No Production Order Start Yet For Your Department");
                this.ddlProductionNumber.Items.Clear();
                /*for grid clear*/
                this.gvItem.DataSource = null;
                this.gvItem.DataBind();
                this.gvBOM.DataSource = null;
                this.gvBOM.DataBind();
                /*for grid clear*/
                return;
            }
        }
        catch (Exception ex)
        {
            //code for any other type of exception
            string message = "alert('" + ex.Message.Replace("'", "") + "')";

        }
        finally
        {
            //call this if exception occurs or not
            //in this example, dispose the WebClient
          //  objDt?.Dispose();
        }
    }

    /*Table For Return Details*/
    public DataTable ReturnDetailsDataTable()
    {
        DataTable dtReturnDetails = new DataTable();
        dtReturnDetails.Clear();
        dtReturnDetails.Columns.Add(new DataColumn("RETURNID", typeof(String)));//0
        dtReturnDetails.Columns.Add(new DataColumn("CATEGORYID", typeof(String)));//1
        dtReturnDetails.Columns.Add(new DataColumn("CATEGORYNAME", typeof(String)));//2
        dtReturnDetails.Columns.Add(new DataColumn("MATERIALID", typeof(String)));//3
        dtReturnDetails.Columns.Add(new DataColumn("MATERIALNAME", typeof(String)));//4
        dtReturnDetails.Columns.Add(new DataColumn("UOMID", typeof(String)));//5
        dtReturnDetails.Columns.Add(new DataColumn("UOMNAME", typeof(String)));//6
        dtReturnDetails.Columns.Add(new DataColumn("Qty", typeof(decimal)));//7
        dtReturnDetails.Columns.Add(new DataColumn("CONSUMABLESQTY", typeof(decimal)));//8
        dtReturnDetails.Columns.Add(new DataColumn("WASTAGEQTY", typeof(decimal)));//9
        dtReturnDetails.Columns.Add(new DataColumn("STORELOCATIONID", typeof(String)));//10
        dtReturnDetails.Columns.Add(new DataColumn("WIPQTY", typeof(decimal)));//11
        dtReturnDetails.Columns.Add(new DataColumn("ALREADYCONSUMPTIONQTY", typeof(decimal)));//12
        dtReturnDetails.Columns.Add(new DataColumn("NETQTY", typeof(decimal)));//13
        return dtReturnDetails;
    }

    public DataTable CreateDataTableFormNotification()
    {
        DataTable dtNotification = new DataTable();
        dtNotification.Clear();
        dtNotification.Columns.Add(new DataColumn("CATEGORYID", typeof(String)));
        dtNotification.Columns.Add(new DataColumn("CATEGORYNAME", typeof(String)));
        dtNotification.Columns.Add(new DataColumn("MATERIALID", typeof(String)));
        dtNotification.Columns.Add(new DataColumn("MATERIALNAME", typeof(String)));
        dtNotification.Columns.Add(new DataColumn("UOMID", typeof(String)));
        dtNotification.Columns.Add(new DataColumn("UOMNAME", typeof(String)));
        dtNotification.Columns.Add(new DataColumn("PRODUCEQTY", typeof(String)));
        dtNotification.Columns.Add(new DataColumn("REJECTEDQTY", typeof(String)));
        dtNotification.Columns.Add(new DataColumn("RETURNQTY", typeof(String)));
        dtNotification.Columns.Add(new DataColumn("YEILD", typeof(String)));
        return dtNotification;
    }

    #region Convert Datatable To XML
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

    public void clearControl()
    {
        this.gvItem.DataSource = null;
        this.gvItem.DataBind();
        this.gvBOM.DataSource = null;
        this.gvBOM.DataBind();
        this.ddlProduct.SelectedValue = "0";
        this.ddlProductionNumber.SelectedValue = "0";
        this.modlaPopUp.Style["display"] = "none";
        chkChoice.Checked = false;
        glProddutionStartQty = 0;
        glProddutionRunningQty = 0;
        glProddutionEndQty = 0;
        this.txtCloseQty.Text = "0";
        this.txtProductioCloseReason.Text = "";
        this.hdnProddutionEndQty.Value = "";
        this.hdnProddutionStartQty.Value = "";
        this.hdnProddutionStartQty.Value = "";
        this.Hdn_Fld.Value = "";
    }

    public void close()
    {

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
        Calendar1.StartDate = oDate;
        CalendarExtender1.StartDate = oDate;
        CalendarExtender2.StartDate = oDate;

        if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
        {
            this.txtfromdateins.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            this.txttodateins.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtentrydt.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');

            Calendar1.EndDate = today1;
            CalendarExtender1.EndDate = today1;
            CalendarExtender2.EndDate = today1;

        }
        else
        {
            this.txtfromdateins.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            this.txttodateins.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtentrydt.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');

            Calendar1.EndDate = cDate;
            CalendarExtender1.EndDate = cDate;
            CalendarExtender2.EndDate = cDate;
        }
    }
    #endregion

    #endregion

    #region contorls
    protected void ddlProductionNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet objDataset = new DataSet();
        try
        {
            string UserId = HttpContext.Current.Session["UserID"].ToString().Trim();
            objDataset = objClsProductionOrder.BindBomFromProduction(this.ddlProductionNumber.SelectedValue, UserId);
            if (objDataset.Tables[3].Rows.Count > 0)
            {
                this.gvItem.DataSource = objDataset.Tables[3];
                this.gvItem.DataBind();

                this.hdnProddutionStartQty.Value = Convert.ToString(objDataset.Tables[3].Rows[0]["Consumption"]);
                this.hdnProddutionRunningQty.Value = Convert.ToString(objDataset.Tables[3].Rows[0]["AlreadyProductionQty"]);

            }
            else
            {
                this.gvItem.DataSource = null;
                this.gvItem.DataBind();
            }
            if (objDataset.Tables[4].Rows.Count > 0)
            {
                this.gvBOM.DataSource = objDataset.Tables[4];
                this.gvBOM.DataBind();
            }
            else
            {
                this.gvBOM.DataSource = null;
                this.gvBOM.DataBind();
            }
        }
        catch (ArgumentNullException ex)
        {
            //code specifically for a ArgumentNullException
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        catch (WebException ex)
        {
            //code specifically for a WebException
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        catch (Exception ex)
        {
            //code for any other type of exception
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        finally
        {
            //call this if exception occurs or not
            //in this example, dispose the WebClient
         //   objDataset?.Dispose();
        }
    }

    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProduct.SelectedValue == "0")
        {
            btnSubmit.BackColor = Color.Green;
            btnSubmit.Text = "SAVE";
        }
        else
        {
            LoadProductionOrder();
            btnSubmit.BackColor = Color.OrangeRed;
            btnSubmit.Text = "PROCESS END";
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        try
        {
            if (this.ddlProduct.SelectedValue == "0")
            {
                MessageBox1.ShowWarning("Please Select Product");
                return;
            }
            if (this.ddlProductionNumber.SelectedValue == "0")
            {
                MessageBox1.ShowWarning("Please Select Order Number");
                return;
            }

            if (chkChoice.Checked == false)
            {
                string xml = string.Empty;
                string MRPNo = "";


                decimal endQty = 0;
                foreach (GridViewRow gvRow in gvItem.Rows)
                {

                    TextBox txtEndQty = (TextBox)gvRow.FindControl("txtEndQty");
                    endQty = Convert.ToDecimal(txtEndQty.Text);
                }

                if (endQty == 0)
                {
                    MessageBox1.ShowWarning("Please Enter Production End Qty");
                    return;
                }

                dt = ReturnDetailsDataTable();
                DataRow dr;
                foreach (GridViewRow row in gvBOM.Rows)
                {
                    Label lblCATID = (Label)row.FindControl("lblCATID");
                    Label lblCATNAME = (Label)row.FindControl("lblCATNAME");
                    Label lblID = (Label)row.FindControl("lblID");
                    Label lblName = (Label)row.FindControl("lblName");
                    Label lblUOMID = (Label)row.FindControl("lblUOMID");
                    Label lblUOMNAME = (Label)row.FindControl("lblUOMNAME");
                    TextBox txtReturnQty = (TextBox)row.FindControl("txtReturnQty");
                    TextBox txtConsumptionQty = (TextBox)row.FindControl("txtConsumptionQty");
                    TextBox txtWastageQty = (TextBox)row.FindControl("txtWastageQty");
                    Label lblSourceLocationId = (Label)row.FindControl("lblSourceLocationId");
                    Label lblstockQty = (Label)row.FindControl("lblstockQty");

                    TextBox txtAlreadyConsumptionQty = (TextBox)row.FindControl("txtAlreadyConsumptionQty");
                    TextBox txtAlreadyWastageQty = (TextBox)row.FindControl("txtAlreadyWastageQty");
                    TextBox txtBufferQty = (TextBox)row.FindControl("txtBufferQty");
                    TextBox txtNetQty = (TextBox)row.FindControl("txtNetQty");
                    TextBox lblQty = (TextBox)row.FindControl("lblQty");


                    decimal stockQty = Convert.ToDecimal(lblstockQty.Text);
                    decimal AlreadyConsumptionQty = Convert.ToDecimal(txtAlreadyConsumptionQty.Text);
                    decimal ConsumptionQty = Convert.ToDecimal(txtConsumptionQty.Text);
                    decimal AlreadyWastageQty = Convert.ToDecimal(txtAlreadyWastageQty.Text);
                    decimal WastageQty = Convert.ToDecimal(txtWastageQty.Text);
                    decimal x = 0;
                    x = (ConsumptionQty + WastageQty);
                    if (x > stockQty)
                    {
                        MessageBox1.ShowWarning("<font color='red'> StockQty Not Avilable For : </font>" + lblName.Text);
                        return;
                    }
                    if (stockQty <= 0)
                    {
                        MessageBox1.ShowWarning("<font color='red'> StockQty Not Avilable For : </font>" + lblName.Text);
                        return;
                    }

                    decimal consumableQty = Convert.ToDecimal(txtConsumptionQty.Text);

                    if (consumableQty > 0)
                    {
                        dr = dt.NewRow();
                        dr[1] = lblCATID.Text;
                        dr[2] = lblCATNAME.Text;
                        dr[3] = lblID.Text;
                        dr[4] = lblName.Text;
                        dr[5] = lblUOMID.Text;
                        dr[6] = lblUOMNAME.Text;
                        dr[7] = lblQty.Text;
                        dr[8] = txtConsumptionQty.Text;
                        dr[9] = txtWastageQty.Text;
                        dr[10] = lblSourceLocationId.Text;
                        dr[11] = txtBufferQty.Text;
                        dr[13] = txtNetQty.Text;

                        dt.Rows.Add(dr);
                    }
                }


                xml = ConvertDatatableToXML(dt);

                MRPNo = objClsProductionOrder.InsertUpdateReturnDetails(xml, this.ddlProductionNumber.SelectedValue, HttpContext.Current.Session["FINYEAR"].ToString(), HttpContext.Current.Session["USERID"].ToString(), endQty,0, txtentrydt.Text);
                if (MRPNo != "")
                {
                    MessageBox1.ShowSuccess("Production Update/Requisition No: <b><font color='green'>" + MRPNo + "</font></b> saved successfully", 80, 550);
                    this.gvItem.DataSource = null;
                    this.gvItem.DataBind();
                    this.gvBOM.DataSource = null;
                    this.gvBOM.DataBind();
                    this.pnlDisplay.Style["display"] = "";
                    this.pnlAdd.Style["display"] = "none";
                    this.divAddNew.Visible = true;
                    this.divEditTime.Style["display"] = "none";
                    this.addDiv.Style["display"] = "";
                    clearControl();
                    LoadMainProduct();
                    LoadProductionOrderWithReturnGrid();
                }
                else
                {
                    MessageBox1.ShowError("Error On Saving Record");
                    return;
                }
            }

            else
            {
                decimal closeQty = 0;
                string productionOrderId = this.ddlProductionNumber.SelectedValue.ToString();
                string userId = HttpContext.Current.Session["USERID"].ToString();
                string reason = this.txtProductioCloseReason.Text.ToString();
                if (this.ddlProductionNumber.SelectedValue == "0")
                {
                    MessageBox1.ShowWarning("Please Select Order Number");
                    return;
                }
                if (reason == "")
                {
                    this.txtProductioCloseReason.BackColor = Color.WhiteSmoke;
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "FocusOnState", "document.getElementById('" + txtProductioCloseReason.ClientID + "').focus(); ", true);
                    return;
                }
                else
                {

                    string xml = string.Empty;
                    string MRPNo = "";


                    decimal endQty = 0;
                    foreach (GridViewRow gvRow in gvItem.Rows)
                    {

                        TextBox txtEndQty = (TextBox)gvRow.FindControl("txtEndQty");
                        endQty = Convert.ToDecimal(txtEndQty.Text);
                    }

                    if (endQty == 0)
                    {
                        endQty = 0;
                    }

                    dt = ReturnDetailsDataTable();
                    DataRow dr;
                    foreach (GridViewRow row in gvBOM.Rows)
                    {
                        Label lblCATID = (Label)row.FindControl("lblCATID");
                        Label lblCATNAME = (Label)row.FindControl("lblCATNAME");
                        Label lblID = (Label)row.FindControl("lblID");
                        Label lblName = (Label)row.FindControl("lblName");
                        Label lblUOMID = (Label)row.FindControl("lblUOMID");
                        Label lblUOMNAME = (Label)row.FindControl("lblUOMNAME");
                        TextBox txtReturnQty = (TextBox)row.FindControl("txtReturnQty");
                        TextBox txtConsumptionQty = (TextBox)row.FindControl("txtConsumptionQty");
                        TextBox txtWastageQty = (TextBox)row.FindControl("txtWastageQty");
                        Label lblSourceLocationId = (Label)row.FindControl("lblSourceLocationId");
                        Label lblstockQty = (Label)row.FindControl("lblstockQty");

                        TextBox txtAlreadyConsumptionQty = (TextBox)row.FindControl("txtAlreadyConsumptionQty");
                        TextBox txtAlreadyWastageQty = (TextBox)row.FindControl("txtAlreadyWastageQty");
                        TextBox txtBufferQty = (TextBox)row.FindControl("txtBufferQty");
                        TextBox txtNetQty = (TextBox)row.FindControl("txtNetQty");
                        TextBox lblQty = (TextBox)row.FindControl("lblQty");


                        decimal stockQty = Convert.ToDecimal(lblstockQty.Text);
                        decimal AlreadyConsumptionQty = Convert.ToDecimal(txtAlreadyConsumptionQty.Text);
                        decimal ConsumptionQty = Convert.ToDecimal(txtConsumptionQty.Text);
                        decimal AlreadyWastageQty = Convert.ToDecimal(txtAlreadyWastageQty.Text);
                        decimal WastageQty = Convert.ToDecimal(txtWastageQty.Text);
                        decimal x = 0;
                        x = (ConsumptionQty + WastageQty);
                        if (x > stockQty)
                        {
                            MessageBox1.ShowWarning("<font color='red'> StockQty Not Avilable For : </font>" + lblName.Text);
                            return;
                        }
                        if (stockQty <= 0)
                        {
                            MessageBox1.ShowWarning("<font color='red'> StockQty Not Avilable For : </font>" + lblName.Text);
                            return;
                        }

                        decimal consumableQty = Convert.ToDecimal(txtConsumptionQty.Text);

                        if (consumableQty > 0)
                        {
                            dr = dt.NewRow();
                            dr[1] = lblCATID.Text;
                            dr[2] = lblCATNAME.Text;
                            dr[3] = lblID.Text;
                            dr[4] = lblName.Text;
                            dr[5] = lblUOMID.Text;
                            dr[6] = lblUOMNAME.Text;
                            dr[7] = lblQty.Text;
                            dr[8] = txtConsumptionQty.Text;
                            dr[9] = txtWastageQty.Text;
                            dr[10] = lblSourceLocationId.Text;
                            dr[11] = txtBufferQty.Text;
                            dr[13] = txtNetQty.Text;

                            dt.Rows.Add(dr);
                        }
                    }

                    decimal _closeQty = Convert.ToDecimal(this.txtCloseQty.Text);
                    xml = ConvertDatatableToXML(dt);

                    MRPNo = objClsProductionOrder.InsertUpdateReturnDetails(xml, this.ddlProductionNumber.SelectedValue, HttpContext.Current.Session["FINYEAR"].ToString(), HttpContext.Current.Session["USERID"].ToString(), endQty, _closeQty,txtentrydt.Text);

                    if (MRPNo != "")
                    {
                        MessageBox1.ShowSuccess("Production Update/Requisition No: <b><font color='green'>" + MRPNo + "</font></b> saved successfully", 80, 550);
                        this.gvItem.DataSource = null;
                        this.gvItem.DataBind();
                        this.gvBOM.DataSource = null;
                        this.gvBOM.DataBind();
                        this.pnlDisplay.Style["display"] = "";
                        this.pnlAdd.Style["display"] = "none";
                        this.divAddNew.Visible = true;
                        this.divEditTime.Style["display"] = "none";
                        this.addDiv.Style["display"] = "";
                        clearControl();
                        LoadMainProduct();
                        LoadProductionOrderWithReturnGrid();
                    }
                    else
                    {
                        MessageBox1.ShowError("Error On Saving Record");
                        return;
                    }











                    //this.lblErrorMEssage.Text = "";
                    //objDt = objClsProductionOrder.CloseProductionOrder(productionOrderId, userId, reason);
                    //if (objDt.Rows[0]["MESSAGE"].ToString() == "Done")
                    //{
                    //    MessageBox1.ShowSuccess("Production Order Close Sucessfully");
                    //    this.gvItem.DataSource = null;
                    //    this.gvItem.DataBind();
                    //    this.gvBOM.DataSource = null;
                    //    this.gvBOM.DataBind();
                    //    this.pnlDisplay.Style["display"] = "";
                    //    this.pnlAdd.Style["display"] = "none";
                    //    this.divAddNew.Visible = true;
                    //    this.divEditTime.Style["display"] = "none";
                    //    this.addDiv.Style["display"] = "";
                    //    clearControl();
                    //    LoadMainProduct();
                    //    LoadProductionOrderWithReturnGrid();
                    //    return;
                    //}
                    //else if (objDt.Rows[0]["MESSAGE"].ToString() == "REQUISITION")
                    //{
                    //    MessageBox1.ShowError("Requisition Done,Not Allow To close");
                    //    return;
                    //}
                    //else
                    //{
                    //    MessageBox1.ShowError("Error,Please Cotact To support Team");
                    //    return;
                    //}
                }

            }


        }
        catch (ArgumentNullException ex)
        {
            //code specifically for a ArgumentNullException
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        catch (WebException ex)
        {
            //code specifically for a WebException
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        catch (Exception ex)
        {
            //code for any other type of exception
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        finally
        {
            //call this if exception occurs or not
            //in this example, dispose the WebClient
            //dt?.Dispose();
        }

    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        this.pnlDisplay.Style["display"] = "";
        this.pnlAdd.Style["display"] = "none";
        this.divAddNew.Visible = true;
        this.divEditTime.Style["display"] = "none";
        this.addDiv.Style["display"] = "";
        clearControl();
        LoadProductionOrderWithReturnGrid();
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        this.pnlDisplay.Style["display"] = "none";
        this.pnlAdd.Style["display"] = "";
        this.divAddNew.Visible = false;
        this.Div_Submit.Visible = true;
        this.trChkChoice.Visible = true;
        //this.Div_Close.Visible = true;
        this.divEditTime.Style["display"] = "none";
        this.addDiv.Style["display"] = "";
        btnSubmit.BackColor = Color.Green;
        btnSubmit.Text = "SAVE";
        gvItem.Columns[11].Visible = true;
        LoadMainProduct();
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        this.pnlDisplay.Style["display"] = "none";
        this.pnlAdd.Style["display"] = "";
        this.Div_Submit.Visible = false;
        this.divAddNew.Visible = false;
        this.trChkChoice.Visible = false;
        // this.Div_Close.Visible = false;
        gvItem.Columns[11].Visible = false;
        Button btn_views = (Button)sender;
        GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
        Label lblPRODUCTION_ORDERID = (Label)gvr.FindControl("lblPRODUCTION_ORDERID");
        this.Hdn_Fld.Value = lblPRODUCTION_ORDERID.Text.Trim();
        DataSet dataSet = new DataSet();
        string UserId = HttpContext.Current.Session["UserID"].ToString().Trim();
        dataSet = objClsProductionOrder.ViewDataFromProduction(this.Hdn_Fld.Value, UserId);

        /*for main product and production number*/
        this.divEditTime.Style["display"] = "";
        this.addDiv.Style["display"] = "none";
        this.txteditProductionNumber.Text = dataSet.Tables[0].Rows[0]["PRODUCTIONORDER"].ToString();
        this.txtMainProductName.Text = dataSet.Tables[2].Rows[0]["NAME"].ToString();

        /*for main product and production number*/

        /*for grids*/
        if (dataSet.Tables[3].Rows.Count > 0)
        {
            this.gvItem.DataSource = dataSet.Tables[3];
            this.gvItem.DataBind();

        }
        else
        {
            this.gvItem.DataSource = null;
            this.gvItem.DataBind();
        }
        if (dataSet.Tables[4].Rows.Count > 0)
        {
            this.gvBOM.DataSource = dataSet.Tables[4];
            this.gvBOM.DataBind();
        }
        else
        {
            this.gvBOM.DataSource = null;
            this.gvBOM.DataBind();
        }
        /*for grids*/

    }

    protected void btnProductSearch_Click(object sender, EventArgs e)
    {
        LoadProductionOrderWithReturnGrid();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        this.txtProductSearch.Text = "";
        LoadProductionOrderWithReturnGrid();
    }

    protected void txtEndQty_TextChanged(object sender, EventArgs e)
    {
        chkChoice.Checked = false;
        this.txtCloseQty.Text = "0";
        this.modlaPopUp.Style["display"] = "none";
        string UserId = HttpContext.Current.Session["UserID"].ToString().Trim();
        decimal endQty = 0;
        decimal StartQty = 0;
        decimal alreadyProductionQty = 0;
        foreach (GridViewRow gvRow in gvItem.Rows)
        {

            TextBox TXTQTY = (TextBox)gvRow.FindControl("TXTQTY");
            StartQty = Convert.ToDecimal(TXTQTY.Text);

            TextBox txtEndQty = (TextBox)gvRow.FindControl("txtEndQty");
            endQty = Convert.ToDecimal(txtEndQty.Text);

            TextBox txtAlreadyProductionQty = (TextBox)gvRow.FindControl("txtAlreadyProductionQty");
            alreadyProductionQty = Convert.ToDecimal(txtAlreadyProductionQty.Text);
        }

        if (alreadyProductionQty > 0)
        {
            if (endQty > (StartQty - alreadyProductionQty))
            {
                MessageBox1.ShowWarning("End Qty Cannont Be Greater Than Production Start Qty");
                foreach (GridViewRow gvRow in gvItem.Rows)
                {
                    TextBox txtEndQty = (TextBox)gvRow.FindControl("txtEndQty");
                    txtEndQty.Text = "0.0000";
                }
                return;
            }
        }
        else if (alreadyProductionQty == 0)
        {
            if (endQty > (StartQty))
            {
                MessageBox1.ShowWarning("End Qty Cannont Be Greater Than Production Start Qty");
                foreach (GridViewRow gvRow in gvItem.Rows)
                {
                    TextBox txtEndQty = (TextBox)gvRow.FindControl("txtEndQty");
                    txtEndQty.Text = "0.0000";
                }
                return;
            }
        }


        DataTable objDt = new DataTable();

        objDt = objClsProductionOrder.fetchQty(this.ddlProductionNumber.SelectedValue, endQty, "");
        if (objDt.Rows.Count > 0)
        {
            this.gvBOM.DataSource = objDt;
            this.gvBOM.DataBind();
            foreach (GridViewRow gvRow in gvItem.Rows)
            {

                TextBox TXTQTY = (TextBox)gvRow.FindControl("TXTQTY");
                StartQty = Convert.ToDecimal(TXTQTY.Text);

                TextBox txtEndQty = (TextBox)gvRow.FindControl("txtEndQty");
                endQty = Convert.ToDecimal(txtEndQty.Text);
            }

        }
        this.hdnProddutionEndQty.Value = Convert.ToString(endQty);

        
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadProductionOrderWithReturnGrid();
    }


    protected void chkChoice_CheckedChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        try
        {

            if (chkChoice.Checked == true)
            {
                MessageBox1.ShowInfo("You want to close this Oreder,");
                this.modlaPopUp.Style["display"] = "";

                if (this.hdnProddutionEndQty.Value == "")
                {
                    this.glProddutionEndQty = 0;
                }
                else
                {
                    this.glProddutionEndQty = Convert.ToDecimal(this.hdnProddutionEndQty.Value);
                }
                this.glProddutionRunningQty = Convert.ToDecimal(this.hdnProddutionRunningQty.Value);
                this.glProddutionStartQty = Convert.ToDecimal(this.hdnProddutionStartQty.Value);

                decimal closQty = (glProddutionStartQty) - (glProddutionRunningQty + glProddutionEndQty);
                this.txtCloseQty.Text = Convert.ToString(closQty);




            }
            else
            {
                this.modlaPopUp.Style["display"] = "none";
                this.txtCloseQty.Text = "0";
            }

        }
        catch (ArgumentNullException ex)
        {
            //code specifically for a ArgumentNullException
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        catch (WebException ex)
        {
            //code specifically for a WebException
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        catch (Exception ex)
        {
            //code for any other type of exception
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        finally
        {
            //call this if exception occurs or not
            //in this example, dispose the WebClient
            //dt?.Dispose();
        }
    }
    #endregion
}
