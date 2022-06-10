#region Namespace
using BAL;
using Obout.Grid;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Collections;
#endregion

public partial class VIEW_frmDespatchMM : System.Web.UI.Page
{
    string menuID = string.Empty;
    string Checker = string.Empty;
    ArrayList Arry = new ArrayList();

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>$(function () {$('#ContentPlaceHolder1_ddlIssue').multiselect({includeSelectAllOption: true});});</script>", false);
        try
        {
            if (!IsPostBack)
            {
                Checker = Request.QueryString["CHECKER"].ToString().Trim();
                menuID = Request.QueryString["MENUID"].ToString().Trim();
                this.pnlDisplay.Style["display"] = "";
                this.pnlAdd.Style["display"] = "none";
                this.LoadTransporter();
                this.LoadVendor();
                this.LoadVendor1();
                this.DateLock();
                //this.LoadIssueNo("0");
                if (Checker == "TRUE")
                {
                    //grdDespatchHeader.Columns[11].Visible = false;
                    this.LoadDespatch();
                    btnaddhide.Style["display"] = "none";
                    this.btnsubmitdiv.Visible = false;
                    this.divbtnapprove.Visible = true;
                    this.divbtnrejection.Visible = true;
                }
                else
                {
                    this.LoadDespatch();
                    btnaddhide.Style["display"] = "";
                    this.btnsubmitdiv.Visible = true;
                    this.divbtnapprove.Visible = false;
                    this.divbtnrejection.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + grdAddDespatch.ClientID + "', 200, '100%' , 30 ,false); </script>", false);
    }
    #endregion

    #region LoadTransporter
    public void LoadTransporter()
    {
        try
        {
            ClsDespatchMM clsDespatchStck = new ClsDespatchMM();
            this.ddlTransporter.Items.Clear();
            this.ddlTransporter.Items.Add(new ListItem("Select Transporter", "0"));
            this.ddlTransporter.AppendDataBoundItems = true;
            this.ddlTransporter.DataSource = clsDespatchStck.BindTPU_Transporter(Convert.ToString(this.Session["DEPOTID"]));
            this.ddlTransporter.DataValueField = "ID";
            this.ddlTransporter.DataTextField = "NAME";
            this.ddlTransporter.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadVendor
    public void LoadVendor()
    {
        try
        {
            ClsFactoryReport clspurchaseorder = new ClsFactoryReport();
            this.ddlvendor.Items.Clear();
            this.ddlvendor.Items.Add(new ListItem("SELECT VENDOR NAME", "0"));
            this.ddlvendor.AppendDataBoundItems = true;
            this.ddlvendor.DataSource = clspurchaseorder.BindPoWiseTpu(HttpContext.Current.Session["DEPOTID"].ToString().Trim());
            this.ddlvendor.DataValueField = "VENDORID";
            this.ddlvendor.DataTextField = "VENDORNAME";
            this.ddlvendor.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region LoadVendor1
    public void LoadVendor1()
    {
        try
        {
            ClsDespatchMM clspurchaseorder = new ClsDespatchMM();
            this.ddlpartyname.Items.Clear();
            this.ddlpartyname.Items.Add(new ListItem("SELECT PARTY NAME", "0"));
            this.ddlpartyname.AppendDataBoundItems = true;
            this.ddlpartyname.DataSource = clspurchaseorder.BindVendor();
            this.ddlpartyname.DataValueField = "VENDORID";
            this.ddlpartyname.DataTextField = "VENDORNAME";
            this.ddlpartyname.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region LoadDespatch
    public void LoadDespatch()
    {
        try
        {
            ClsDespatchMM clsDespatchStck = new ClsDespatchMM();
            this.grdDespatchHeader.DataSource = clsDespatchStck.BindDespatch(this.txtFromDate.Text.Trim(), this.txtToDate.Text.Trim(), HttpContext.Current.Session["FINYEAR"].ToString().Trim(),
                                                                              HttpContext.Current.Session["DEPOTID"].ToString().Trim(), Checker);
            this.grdDespatchHeader.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadWorkOrder
    public void LoadWorkOrder(string VendorID, string DespatchID)
    {
        try
        {
            ClsDespatchMM clsDespatchStck = new ClsDespatchMM();
            this.ddlPo.Items.Clear();
            this.ddlPo.Items.Add(new ListItem("SELECT ORDER NO", "0"));
            this.ddlPo.AppendDataBoundItems = true;
            this.ddlPo.DataSource = clsDespatchStck.BindWorkOrder(VendorID, DespatchID);
            this.ddlPo.DataValueField = "POID";
            this.ddlPo.DataTextField = "PONO";
            this.ddlPo.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region LoadIssueNo
    public void LoadIssueNo(string Poid)
    {
        try
        {
            ClsDespatchMM clsDespatchStck = new ClsDespatchMM();
            this.ddlIssue.Items.Clear();
            //this.ddlIssue.Items.Add(new ListItem("SELECT ORDER NO", "0"));
            this.ddlIssue.AppendDataBoundItems = true;
            this.ddlIssue.DataSource = clsDespatchStck.BindIssueNo(Poid);
            this.ddlIssue.DataValueField = "ISSUEID";
            this.ddlIssue.DataTextField = "ISSUENO";
            this.ddlIssue.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region ddlPo_SelectedIndexChanged
    protected void ddlPo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LoadIssueNo(ddlPo.SelectedValue.ToString());
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region ddlIssue_SelectedIndexChanged
    protected void ddlIssue_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.LoadOrderDetails();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region ddlvendor_SelectedIndexChanged
    protected void ddlvendor_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.LoadWorkOrder(this.ddlvendor.SelectedValue, this.hdnDespatchID.Value);
            //if (this.Session["DESPATCH_TAX_DETAILS"] == null)
            //{
            //    this.CreateTaxTable();
            //}
            
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btnorderadd_Click
    protected void btnorderadd_Click(object sender, EventArgs e)
    {
        try
        {
            string TAXID = string.Empty;
            ClsGRNMM clsgrnmm = new ClsGRNMM();
            if (Session["DESPATCHDETAILS_MM"] == null)
            {
                this.CreateDataTable();
            }

            DataTable dtDespatch = (DataTable)HttpContext.Current.Session["DESPATCHDETAILS_MM"];

            foreach (GridViewRow gvr in grdAddDespatch.Rows)
            {
                TextBox txtqty = (TextBox)gvr.FindControl("grdtxtpoqty");
                TextBox txtamount = (TextBox)gvr.FindControl("grdtxtamount");
                Label lblproductid = (Label)gvr.FindControl("grdlblproductid");
                Label lblproductname = (Label)gvr.FindControl("grdlblproductname");
                Label lbluomid = (Label)gvr.FindControl("grdlbluomid");
                Label lbluomname = (Label)gvr.FindControl("grdlbluomname");
                Label lblmrp = (Label)gvr.FindControl("grdlblmrp");
                Label lblrate = (Label)gvr.FindControl("grdlblrate");
                Label lblbatchno = (Label)gvr.FindControl("grdlblbatchno");
                Label lbltotalmrp = (Label)gvr.FindControl("grdlbltotalmrp");
                Label lblallocatedqty = (Label)gvr.FindControl("grdlblallocatedqty");

                if (!string.IsNullOrEmpty(txtqty.Text.Trim()))
                {
                    DataRow dr = dtDespatch.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["POID"] = Convert.ToString(this.ddlPo.SelectedValue);
                    dr["PRODUCTID"] = Convert.ToString(lblproductid.Text).Trim();
                    dr["PRODUCTNAME"] = Convert.ToString(lblproductname.Text).Trim();
                    dr["UOMID"] = Convert.ToString(lbluomid.Text);
                    dr["UOMNAME"] = Convert.ToString(lbluomname.Text).Trim();
                    dr["MRP"] = Convert.ToString(lblmrp.Text.Trim());
                    dr["QTY"] = Convert.ToString(txtqty.Text.Trim());
                    dr["RATE"] = Convert.ToString(lblrate.Text.Trim());
                    dr["AMOUNT"] = Convert.ToString(String.Format("{0:0.00}", txtamount.Text.Trim()));
                    dr["TOTMRP"] = Convert.ToString(lbltotalmrp.Text);
                    dr["BATCHNO"] = Convert.ToString(lblbatchno.Text);
                    dr["ASSESMENTPERCENTAGE"] = Convert.ToString(0);
                    dr["TOTALASSESMENTVALUE"] = Convert.ToString(String.Format("{0:0.00}", 0));
                    dr["NETWEIGHT"] = "0";
                    dr["ALLOCATEDQTY"] = Convert.ToString(lblallocatedqty.Text.Trim());
                    dr["MFDATE"] = Convert.ToString(null);
                    dr["EXPRDATE"] = Convert.ToString(null);
                    dr["GROSSWEIGHT"] = "0";

                    //#region Loop For Adding Itemwise Tax Component
                    //DataTable dtTaxCountDataAddition = (DataTable)Session["dtTaxCount"];
                    //ViewState["Invoice_Type"] = dtTaxCountDataAddition.Rows.Count;
                    //for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                    //{
                    //    switch (Convert.ToString(dtTaxCountDataAddition.Rows[k]["RELATEDTO"]))
                    //    {
                    //        case "1":
                    //            TAXID = clsgrnmm.TaxID(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                    //            ProductWiseTax = clsgrnmm.GetHSNTax(TAXID, Convert.ToString(lblproductid.Text.ToString().Substring(0, lblproductid.Text.ToString().IndexOf("~"))).Trim(), ddlvendor.SelectedValue.ToString().Trim(), "");
                    //            dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                    //            dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", 10 * ProductWiseTax / 100));
                    //            break;
                    //    }
                    //    Arry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());

                    //    CreateTaxDatatable(Convert.ToString(this.ddlPo.SelectedValue.Trim()),
                    //                       Convert.ToString(lblproductid.Text.Trim()),
                    //                       "",
                    //                       dtTaxCountDataAddition.Rows[k]["NAME"].ToString().Trim(),
                    //                       Convert.ToString(ProductWiseTax),
                    //                       dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""].ToString().Trim(),
                    //                       Convert.ToString(lblproductname.Text).Substring(0, 35).Trim(),
                    //                       TAXID);
                    //}
                    //#endregion

                    dtDespatch.Rows.Add(dr);
                    dtDespatch.AcceptChanges();
                }
            }

            

            if (dtDespatch.Rows.Count > 0)
            {
                this.grdAddDespatch.DataSource = null;
                this.grdAddDespatch.DataBind();
                this.grdproductdetails.DataSource = null;
                this.grdproductdetails.DataBind();
                this.grddespatchrecord.DataSource = dtDespatch;
                this.grddespatchrecord.DataBind();
                HttpContext.Current.Session["DESPATCHDETAILS_MM"] = dtDespatch;

                decimal Roundoff = 0;
                decimal GrossTotal = 0;

                #region Item-Wise-Amount Calculation
                GrossTotal = this.CalculateGrossTotal(dtDespatch);
                Roundoff = Math.Round(GrossTotal);
                this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal));
                this.txtRoundoff.Text = Convert.ToString(String.Format("{0:0.00}", 0));
                this.txtFinalAmt.Text = Convert.ToString(String.Format("{0:0.00}", Math.Round(GrossTotal)));
                #endregion
            }
            else
            {
                this.grddespatchrecord.DataSource = null;
                this.grddespatchrecord.DataBind();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region CreateTaxDatatable
    void CreateTaxDatatable(string POID, string PRODUCTID, string BATCH, string NAME, string TAXPERCENTAGE, string VALUES, string PRODUCTNAME, string Taxid)
    {
        //string TaxID = string.Empty;
        DataTable dt = (DataTable)Session["TAXCOMPONENTDETAILS"];
        DataRow dr = dt.NewRow();
        dr["POID"] = POID;
        dr["PRODUCTID"] = PRODUCTID;
        dr["BATCHNO"] = BATCH;
        //TaxID = clsgrnmm.TaxID(NAME);
        dr["TAXID"] = Taxid;
        dr["PERCENTAGE"] = TAXPERCENTAGE;
        dr["TAXVALUE"] = VALUES;
        dr["PRODUCTNAME"] = PRODUCTNAME;
        dr["TAXNAME"] = NAME;
        dt.Rows.Add(dr);
        dt.AcceptChanges();
    }
    #endregion


    #region CreateDataTableTaxComponent Structure
    public DataTable CreateDataTableTaxComponent()
    {
        DataTable dt = new DataTable();
        if (hdnDespatchID.Value == "")
        {
            dt.Clear();
            dt.Columns.Add(new DataColumn("POID", typeof(string)));
            dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
            dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXID", typeof(string)));
            dt.Columns.Add(new DataColumn("PERCENTAGE", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXVALUE", typeof(string)));
            dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXNAME", typeof(string)));
            HttpContext.Current.Session["TAXCOMPONENTDETAILS"] = dt;
        }
        else
        {
            dt.Clear();
            dt.Columns.Add(new DataColumn("POID", typeof(string)));
            dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
            dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXID", typeof(string)));
            dt.Columns.Add(new DataColumn("PERCENTAGE", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXVALUE", typeof(string)));
            dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));
            dt.Columns.Add(new DataColumn("TAXNAME", typeof(string)));
            HttpContext.Current.Session["TAXCOMPONENTDETAILS"] = dt;
        }
        return dt;
    }
    #endregion

    #region Create DataTable Structure
    public void CreateDataTable()
    {
        DataTable dt = new DataTable();
        DataTable dtTaxCount = new DataTable();// for Tax Count
        try
        {
            ClsGRNMM clsgrnmm = new ClsGRNMM();
            dt.Clear();
            dt.Columns.Add(new DataColumn("GUID", typeof(string)));                //1
            dt.Columns.Add(new DataColumn("POID", typeof(string)));                //2
            dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));           //3
            dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));         //4
            dt.Columns.Add(new DataColumn("UOMID", typeof(string)));               //5
            dt.Columns.Add(new DataColumn("UOMNAME", typeof(string)));             //6
            dt.Columns.Add(new DataColumn("MRP", typeof(string)));                 //7
            dt.Columns.Add(new DataColumn("QTY", typeof(string)));                 //8
            dt.Columns.Add(new DataColumn("RATE", typeof(string)));                //9
            dt.Columns.Add(new DataColumn("AMOUNT", typeof(string)));              //10
            dt.Columns.Add(new DataColumn("TOTMRP", typeof(string)));              //11
            dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));             //12
            dt.Columns.Add(new DataColumn("ASSESMENTPERCENTAGE", typeof(string))); //13
            dt.Columns.Add(new DataColumn("TOTALASSESMENTVALUE", typeof(string))); //14
            dt.Columns.Add(new DataColumn("NETWEIGHT", typeof(string)));           //15
            dt.Columns.Add(new DataColumn("ALLOCATEDQTY", typeof(string)));        //16
            dt.Columns.Add(new DataColumn("MFDATE", typeof(string)));              //17
            dt.Columns.Add(new DataColumn("EXPRDATE", typeof(string)));            //18
            dt.Columns.Add(new DataColumn("GROSSWEIGHT", typeof(string)));         //19
            dt.Columns.Add(new DataColumn("PRODUCTRATE", typeof(string)));         //20
            dt.Columns.Add(new DataColumn("PURCHASEAMT", typeof(string)));         //21
            dt.Columns.Add(new DataColumn("WORKORDERNO", typeof(string)));         //22
            dt.Columns.Add(new DataColumn("STOCKQTY", typeof(string)));         //23


            #region Loop For Adding Itemwise Tax Component
            if (hdnDespatchID.Value == "")
            {
                string flag = clsgrnmm.BindRegion(this.ddlvendor.SelectedValue.Trim(), HttpContext.Current.Session["DEPOTID"].ToString());

                if (string.IsNullOrEmpty(flag))
                {
                    dtTaxCount = clsgrnmm.ItemWiseTaxCount(Request.QueryString["MENUID"].ToString(), "1", this.ddlvendor.SelectedValue.Trim(),"", HttpContext.Current.Session["DEPOTID"].ToString(), "");
                }
                else
                {
                    dtTaxCount = clsgrnmm.ItemWiseTaxCount(Request.QueryString["MENUID"].ToString(), "0", this.ddlvendor.SelectedValue.Trim(), "", HttpContext.Current.Session["DEPOTID"].ToString(), "");
                }
                Session["dtTaxCount"] = dtTaxCount;
                for (int k = 0; k < dtTaxCount.Rows.Count; k++)
                {
                    dt.Columns.Add(new DataColumn("" + Convert.ToString(dtTaxCount.Rows[k]["NAME"]) + "" + "(%)", typeof(string)));
                    dt.Columns.Add(new DataColumn("" + Convert.ToString(dtTaxCount.Rows[k]["NAME"]) + "", typeof(string)));
                }
            }
            else
            {
                DataSet ds = new DataSet();
                ClsDespatchMM clsDespatchStck = new ClsDespatchMM();
                string despatchID = Convert.ToString(hdnDespatchID.Value).Trim();
                ds = clsDespatchStck.EditDespatchDetails(despatchID);
                Session["dtTaxCount"] = ds.Tables[6];
                for (int k = 0; k < ds.Tables[6].Rows.Count; k++)
                {
                    dt.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables[6].Rows[k]["NAME"]) + "" + "(%)", typeof(string)));
                    dt.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables[6].Rows[k]["NAME"]) + "", typeof(string)));
                }
            }
            #endregion


            dt.Columns.Add(new DataColumn("CGSTID", typeof(string)));
            dt.Columns.Add(new DataColumn("CGSTPERCENT", typeof(string)));
            dt.Columns.Add(new DataColumn("CGSTAMOUNT", typeof(string)));
            dt.Columns.Add(new DataColumn("SGSTID", typeof(string)));
            dt.Columns.Add(new DataColumn("SGSTPERCENT", typeof(string)));
            dt.Columns.Add(new DataColumn("SGSTAMOUNT", typeof(string)));
            dt.Columns.Add(new DataColumn("IGSTID", typeof(string)));
            dt.Columns.Add(new DataColumn("IGSTPERCENT", typeof(string)));
            dt.Columns.Add(new DataColumn("IGSTAMOUNT", typeof(string)));

            HttpContext.Current.Session["DESPATCHDETAILS_MM"] = dt;
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region Create IssueDataTable Structure
    public void CreateIssueDataTable()
    {
        DataTable dtIssue = new DataTable();
        try
        {
            dtIssue.Clear();
            dtIssue.Columns.Add(new DataColumn("GUID", typeof(string)));                //1
            dtIssue.Columns.Add(new DataColumn("POID", typeof(string)));                //2
            dtIssue.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));           //3
            dtIssue.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));         //4
            dtIssue.Columns.Add(new DataColumn("ISSUEID", typeof(string)));             //5
            dtIssue.Columns.Add(new DataColumn("QTY", typeof(string)));                 //6
            dtIssue.Columns.Add(new DataColumn("BULKPRODUCTID", typeof(string)));       //7
            dtIssue.Columns.Add(new DataColumn("BULKPRODUCTNAME", typeof(string)));     //8
            dtIssue.Columns.Add(new DataColumn("STORELOCATIONID", typeof(string)));     //9
            dtIssue.Columns.Add(new DataColumn("STORELOCATIONNAME", typeof(string)));   //10

            HttpContext.Current.Session["ISSUEDETAILS_MM"] = dtIssue;
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region Create DataTable BOM Structure
    public void CreateDataTableBOM()
    {
        DataTable dt = new DataTable();
        DataTable dtTaxCount = new DataTable();// for Tax Count
        ClsGRNMM clsgrnmm = new ClsGRNMM();
        try
        {
            dt.Clear();
            dt.Columns.Add(new DataColumn("GUID", typeof(string)));                 //1
            dt.Columns.Add(new DataColumn("POID", typeof(string)));                 //2
            dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));            //3
            dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));          //4
            dt.Columns.Add(new DataColumn("UOMID", typeof(string)));                //5
            dt.Columns.Add(new DataColumn("UOMNAME", typeof(string)));              //6
            dt.Columns.Add(new DataColumn("MRP", typeof(string)));                  //7
            dt.Columns.Add(new DataColumn("QTY", typeof(string)));                  //8
            dt.Columns.Add(new DataColumn("RATE", typeof(string)));                 //9
            dt.Columns.Add(new DataColumn("AMOUNT", typeof(string)));               //10
            dt.Columns.Add(new DataColumn("TOTMRP", typeof(string)));               //11
            dt.Columns.Add(new DataColumn("BATCHNO", typeof(string)));              //12
            dt.Columns.Add(new DataColumn("ASSESMENTPERCENTAGE", typeof(string)));  //13
            dt.Columns.Add(new DataColumn("TOTALASSESMENTVALUE", typeof(string)));  //14
            dt.Columns.Add(new DataColumn("NETWEIGHT", typeof(string)));            //15
            dt.Columns.Add(new DataColumn("ALLOCATEDQTY", typeof(string)));         //16
            dt.Columns.Add(new DataColumn("MFDATE", typeof(string)));               //17
            dt.Columns.Add(new DataColumn("EXPRDATE", typeof(string)));             //18
            dt.Columns.Add(new DataColumn("GROSSWEIGHT", typeof(string)));          //19
            dt.Columns.Add(new DataColumn("ISSUEID", typeof(string)));              //20
            dt.Columns.Add(new DataColumn("BULKPRODUCTID", typeof(string)));        //21
            dt.Columns.Add(new DataColumn("BULKPRODUCTNAME", typeof(string)));      //22
            dt.Columns.Add(new DataColumn("STORELOCATIONID", typeof(string)));      //23            
            dt.Columns.Add(new DataColumn("STORELOCATIONNAME", typeof(string)));    //24
            dt.Columns.Add(new DataColumn("REQUISITIONQTY", typeof(string)));       //25
            dt.Columns.Add(new DataColumn("STOCKQTY", typeof(string)));       //26

            #region Loop For Adding Itemwise Tax Component
            if (hdnDespatchID.Value == "")
            {
                string flag = clsgrnmm.BindRegion(this.ddlvendor.SelectedValue.Trim(), HttpContext.Current.Session["DEPOTID"].ToString());

                if (string.IsNullOrEmpty(flag))
                {
                    dtTaxCount = clsgrnmm.ItemWiseTaxCount(Request.QueryString["MENUID"].ToString(), "1", this.ddlvendor.SelectedValue.Trim(), "", HttpContext.Current.Session["DEPOTID"].ToString(), "");
                }
                else
                {
                    dtTaxCount = clsgrnmm.ItemWiseTaxCount(Request.QueryString["MENUID"].ToString(), "0", this.ddlvendor.SelectedValue.Trim(), "", HttpContext.Current.Session["DEPOTID"].ToString(), "");
                }
                Session["dtTaxCount"] = dtTaxCount;
                for (int k = 0; k < dtTaxCount.Rows.Count; k++)
                {
                    dt.Columns.Add(new DataColumn("" + Convert.ToString(dtTaxCount.Rows[k]["NAME"]) + "" + "(%)", typeof(string)));
                    dt.Columns.Add(new DataColumn("" + Convert.ToString(dtTaxCount.Rows[k]["NAME"]) + "", typeof(string)));
                }
            }
            else
            {
                DataSet ds = new DataSet();
                ClsDespatchMM clsDespatchStck = new ClsDespatchMM();
                string despatchID = Convert.ToString(hdnDespatchID.Value).Trim();
                ds = clsDespatchStck.EditDespatchDetails(despatchID);
                Session["dtTaxCount"] = ds.Tables[6];
                for (int k = 0; k < ds.Tables[6].Rows.Count; k++)
                {
                    dt.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables[6].Rows[k]["NAME"]) + "" + "(%)", typeof(string)));
                    dt.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables[6].Rows[k]["NAME"]) + "", typeof(string)));
                }
            }
            #endregion


            dt.Columns.Add(new DataColumn("CGSTID", typeof(string)));
            dt.Columns.Add(new DataColumn("CGSTPERCENT", typeof(string)));
            dt.Columns.Add(new DataColumn("CGSTAMOUNT", typeof(string)));
            dt.Columns.Add(new DataColumn("SGSTID", typeof(string)));
            dt.Columns.Add(new DataColumn("SGSTPERCENT", typeof(string)));
            dt.Columns.Add(new DataColumn("SGSTAMOUNT", typeof(string)));
            dt.Columns.Add(new DataColumn("IGSTID", typeof(string)));
            dt.Columns.Add(new DataColumn("IGSTPERCENT", typeof(string)));
            dt.Columns.Add(new DataColumn("IGSTAMOUNT", typeof(string)));
            dt.Columns.Add(new DataColumn("AFTERTAXAMNT", typeof(string)));

            HttpContext.Current.Session["DESPATCHDETAILS_BOM_MM"] = dt;
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion


    #region BindViewstateTaxid
    public void BindViewstateTaxid()
    {
       
        try
        {
            DataTable objDt = new DataTable();
            ClsDespatchMM objCls = new ClsDespatchMM();
            string MODE = "JOBTAX";
            string _menuID = Request.QueryString["MENUID"].ToString().Trim();
            objDt= objCls.bindTaxMenuWise(MODE, _menuID,"");

            if (objDt.Rows.Count > 0)
            {
                for (int i=0 ; i< objDt.Rows.Count; i++)
                {
                    if(Convert.ToString(objDt.Rows[i]["NAME"])== "INPUT CGST")
                    {
                        ViewState["CGSTID"] = Convert.ToString(objDt.Rows[i]["TAXID"]);
                    }
                    if (Convert.ToString(objDt.Rows[i]["NAME"]) == "INPUT SGST")
                    {
                        ViewState["SGSTID"] = Convert.ToString(objDt.Rows[i]["TAXID"]);
                    }
                    if (Convert.ToString(objDt.Rows[i]["NAME"]) == "INPUT IGST")
                    {
                        ViewState["IGSTID"] = Convert.ToString(objDt.Rows[i]["TAXID"]);
                    }
                }
               
            }
            else
            {
                MessageBox1.ShowWarning("Tax Menu Mapping Not Done Please Check");
                return;
            }
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion


    public void LoadOrderDetails()
    {
        BindViewstateTaxid();
        this.ResetSession();
        this.CreateDataTable();
        this.CreateDataTableTaxComponent();
        this.CreateDataTableBOM();
        this.CreateIssueDataTable();
        string TAXID = string.Empty;
        //decimal ProductWiseTax = 0;
        ClsGRNMM clsgrnmm = new ClsGRNMM();

        string CGSTID = Convert.ToString(ViewState["CGSTID"]);
        string SGSTID = Convert.ToString(ViewState["SGSTID"]);
        string IGSTID = Convert.ToString(ViewState["IGSTID"]);

        string IssueId = "";
        var query = from ListItem item in ddlIssue.Items where item.Selected select item;
        foreach (ListItem item in query)
        {
            // item ...
            IssueId += item.Value + ',';
        }
        IssueId = IssueId.Substring(0, IssueId.Length - 1);

        ClsDespatchMM clsDespatchStck = new ClsDespatchMM();
        DataSet ds = clsDespatchStck.BindIssueDetails(this.ddlPo.SelectedValue, IssueId);
        if (ds.Tables[1].Rows.Count > 0)
        {
            #region Product Details
            this.grdproductdetails.DataSource = ds.Tables[1];
            this.grdproductdetails.DataBind();
            #endregion

            #region Bom Details
            DataTable dtDespatch = (DataTable)HttpContext.Current.Session["DESPATCHDETAILS_MM"];
            foreach (GridViewRow gvr in grdproductdetails.Rows)
            {
                Label lblamount = (Label)gvr.FindControl("grdlblamount1");
                Label lblproductid = (Label)gvr.FindControl("grdlblproductid1");
                Label lblproductname = (Label)gvr.FindControl("grdlblproductname1");
                Label lbluomid = (Label)gvr.FindControl("grdlbluomid1");
                Label lbluomname = (Label)gvr.FindControl("grdlbluomname1");
                Label lblmrp = (Label)gvr.FindControl("grdlblmrp1");
                Label lblrate = (Label)gvr.FindControl("grdlblrate1");
                Label lblbatchno = (Label)gvr.FindControl("grdlblbatchno1");
                Label lbltotalmrp = (Label)gvr.FindControl("grdlbltotalmrp1");
                Label lblallocatedqty = (Label)gvr.FindControl("grdlblallocatedqty1");

                if (!string.IsNullOrEmpty(lblallocatedqty.Text.Trim()))
                {
                    DataRow dr = dtDespatch.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["POID"] = Convert.ToString(this.ddlPo.SelectedValue);
                    dr["PRODUCTID"] = Convert.ToString(lblproductid.Text).Trim();
                    dr["PRODUCTNAME"] = Convert.ToString(lblproductname.Text).Trim();
                    dr["UOMID"] = Convert.ToString(lbluomid.Text);
                    dr["UOMNAME"] = Convert.ToString(lbluomname.Text).Trim();
                    dr["MRP"] = Convert.ToString(lblmrp.Text.Trim());
                    dr["QTY"] = Convert.ToString(lblallocatedqty.Text.Trim());
                    dr["RATE"] = Convert.ToString(lblrate.Text.Trim());
                    dr["AMOUNT"] = Convert.ToString(lblamount.Text.Trim());
                    dr["TOTMRP"] = Convert.ToString(lbltotalmrp.Text);
                    dr["BATCHNO"] = Convert.ToString(lblbatchno.Text);
                    dr["ASSESMENTPERCENTAGE"] = Convert.ToString(0);
                    dr["TOTALASSESMENTVALUE"] = Convert.ToString(String.Format("{0:0.00}", 0));
                    dr["NETWEIGHT"] = "0";
                    dr["ALLOCATEDQTY"] = Convert.ToString(lblallocatedqty.Text.Trim());
                    dr["MFDATE"] = Convert.ToString(null);
                    dr["EXPRDATE"] = Convert.ToString(null);
                    dr["GROSSWEIGHT"] = "0";


                    //#region Loop For Adding Itemwise Tax Component
                    //DataTable dtTaxCountDataAddition = (DataTable)Session["dtTaxCount"];
                    //ViewState["Invoice_Type"] = dtTaxCountDataAddition.Rows.Count;
                    //for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                    //{
                    //    switch (Convert.ToString(dtTaxCountDataAddition.Rows[k]["RELATEDTO"]))
                    //    {
                    //        case "1":
                    //            TAXID = clsgrnmm.TaxID(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                    //            ProductWiseTax = clsgrnmm.GetHSNTax(TAXID, Convert.ToString(lblproductid.Text), ddlvendor.SelectedValue.ToString().Trim(), "");
                    //            dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + "" + "(%)"] = Convert.ToString(String.Format("{0:0.00}", ProductWiseTax));
                    //            dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""] = Convert.ToString(String.Format("{0:0.00}", 10 * ProductWiseTax / 100));
                    //            break;
                    //    }
                    //    Arry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());

                    //    CreateTaxDatatable(Convert.ToString(this.ddlPo.SelectedValue.Trim()),
                    //                       Convert.ToString(lblproductid.Text.Trim()),
                    //                       "",
                    //                       dtTaxCountDataAddition.Rows[k]["NAME"].ToString().Trim(),
                    //                       Convert.ToString(ProductWiseTax),
                    //                       dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""].ToString().Trim(),
                    //                       Convert.ToString(lblproductname.Text).Substring(0, 35).Trim(),
                    //                       TAXID);
                    //}
                    //#endregion

                    dtDespatch.Rows.Add(dr);
                    dtDespatch.AcceptChanges();
                }
            }
            #endregion            

            #region Issue Details
            DataTable dtIssue = (DataTable)HttpContext.Current.Session["ISSUEDETAILS_MM"];
            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
            {
                DataRow drIssue = dtIssue.NewRow();
                drIssue["GUID"] = Guid.NewGuid();
                drIssue["POID"] = Convert.ToString(ds.Tables[2].Rows[i]["POID"]).Trim();
                drIssue["PRODUCTID"] = Convert.ToString(ds.Tables[2].Rows[i]["PRODUCTID"]).Trim();
                drIssue["PRODUCTNAME"] = Convert.ToString(ds.Tables[2].Rows[i]["PRODUCTNAME"]).Trim();
                drIssue["ISSUEID"] = Convert.ToString(ds.Tables[2].Rows[i]["ISSUEID"]).Trim();
                drIssue["QTY"] = Convert.ToString(ds.Tables[2].Rows[i]["QTY"]).Trim();
                drIssue["BULKPRODUCTID"] = Convert.ToString(ds.Tables[2].Rows[i]["BULKPRODUCTID"]).Trim();
                drIssue["BULKPRODUCTNAME"] = Convert.ToString(ds.Tables[2].Rows[i]["BULKPRODUCTNAME"]).Trim();
                drIssue["STORELOCATIONID"] = Convert.ToString(ds.Tables[2].Rows[i]["STORELOCATIONID"]).Trim();
                drIssue["STORELOCATIONNAME"] = Convert.ToString(ds.Tables[2].Rows[i]["STORELOCATIONNAME"]).Trim();

                dtIssue.Rows.Add(drIssue);
                dtIssue.AcceptChanges();
            }
            #endregion

            #region Item-Wise-Amount Calculation
            decimal GrossTotal = 0;
            GrossTotal = this.CalculateGrossTotal(dtDespatch);
            //this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", GrossTotal));
            //this.txtFinalAmt.Text = Convert.ToString(String.Format("{0:0.00}", Math.Round(GrossTotal)));
            //this.txtRoundoff.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(this.txtFinalAmt.Text.Trim()) - Convert.ToDecimal(this.txtTotalGross.Text.Trim())));

            #endregion
        }
        else
        {
            this.grdproductdetails.DataSource = null;
            this.grdproductdetails.DataBind();
        }

        if (ds.Tables[0].Rows.Count > 0)
        {
            #region Product Details
            this.grdAddDespatch.DataSource = ds.Tables[0];
            this.grdAddDespatch.DataBind();
            #endregion

            #region Bom Details
            DataTable dtDespatchBOM = (DataTable)HttpContext.Current.Session["DESPATCHDETAILS_BOM_MM"];

            foreach (GridViewRow gvr in grdAddDespatch.Rows)
            {
                TextBox txtqty = (TextBox)gvr.FindControl("grdtxtpoqty");
                TextBox txtamount = (TextBox)gvr.FindControl("grdtxtamount");
                Label lblproductid = (Label)gvr.FindControl("grdlblproductid");
                Label lblproductname = (Label)gvr.FindControl("grdlblproductname");
                Label lbluomid = (Label)gvr.FindControl("grdlbluomid");
                Label lbluomname = (Label)gvr.FindControl("grdlbluomname");
                Label lblmrp = (Label)gvr.FindControl("grdlblmrp");
                Label lblrate = (Label)gvr.FindControl("grdlblrate");
                Label lblbatchno = (Label)gvr.FindControl("grdlblbatchno");
                Label lbltotalmrp = (Label)gvr.FindControl("grdlbltotalmrp");
                Label lblallocatedqty = (Label)gvr.FindControl("grdlblallocatedqty");
                Label lblissueid = (Label)gvr.FindControl("grdlblissueid");
                Label lblWorkOrderproductid = (Label)gvr.FindControl("grdlblWorkOrderproductid");
                Label lblBulkproductname = (Label)gvr.FindControl("grdlblBulkproductname");
                Label lblfromstorelocationid = (Label)gvr.FindControl("grdlblfromstorelocationid");
                Label lblfromstorelocation = (Label)gvr.FindControl("grdlblfromstorelocation");
                Label grdlblrequisitionqty = (Label)gvr.FindControl("grdlblrequisitionqty");
                Label grdlblstockqty = (Label)gvr.FindControl("grdlblstockqty");
                TextBox grdTOTALAMOUNT = (TextBox)gvr.FindControl("grdTOTALAMOUNT");

                if (!string.IsNullOrEmpty(txtqty.Text.Trim()))
                {
                    DataRow dr = dtDespatchBOM.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["POID"] = Convert.ToString(this.ddlPo.SelectedValue);
                    dr["PRODUCTID"] = Convert.ToString(lblproductid.Text).Trim();
                    dr["PRODUCTNAME"] = Convert.ToString(lblproductname.Text).Trim();
                    dr["UOMID"] = Convert.ToString(lbluomid.Text);
                    dr["UOMNAME"] = Convert.ToString(lbluomname.Text).Trim();
                    dr["MRP"] = Convert.ToString(lblmrp.Text.Trim());
                    dr["QTY"] = Convert.ToString(txtqty.Text.Trim());
                    dr["RATE"] = 0;
                    dr["AMOUNT"] = 0;
                    dr["TOTMRP"] = Convert.ToString(lbltotalmrp.Text);
                    dr["BATCHNO"] = Convert.ToString(lblbatchno.Text);
                    dr["ASSESMENTPERCENTAGE"] = Convert.ToString(0);
                    dr["TOTALASSESMENTVALUE"] = Convert.ToString(String.Format("{0:0.00}", 0));
                    dr["NETWEIGHT"] = "0";
                    dr["ALLOCATEDQTY"] = Convert.ToString(lblallocatedqty.Text.Trim());
                    dr["MFDATE"] = Convert.ToString(null);
                    dr["EXPRDATE"] = Convert.ToString(null);
                    dr["GROSSWEIGHT"] = "0";
                    dr["ISSUEID"] = Convert.ToString(lblissueid.Text.Trim());
                    dr["BULKPRODUCTID"] = Convert.ToString(lblWorkOrderproductid.Text.Trim());
                    dr["BULKPRODUCTNAME"] = Convert.ToString(lblBulkproductname.Text.Trim());
                    dr["STORELOCATIONID"] = Convert.ToString(lblfromstorelocationid.Text.Trim());                    
                    dr["STORELOCATIONNAME"] = Convert.ToString(lblfromstorelocation.Text).Trim();
                    dr["REQUISITIONQTY"] = Convert.ToString(grdlblrequisitionqty.Text).Trim();
                    dr["STOCKQTY"] = Convert.ToString(grdlblstockqty.Text).Trim();

                    #region Loop For Adding Itemwise Tax Component
                    DataTable dtTaxCountDataAddition = (DataTable)Session["dtTaxCount"];
                    ViewState["Invoice_Type"] = dtTaxCountDataAddition.Rows.Count;
                    decimal productWiseTax = 0;
                    string _rate = Convert.ToString(lblmrp.Text);
                    decimal rate = Convert.ToDecimal(_rate);
                    for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                    {
                        DataTable objDt = new DataTable();
                        ClsDespatchMM objCls=new ClsDespatchMM();
                        string MODE = "PIDTAX";
                        string productId = lblproductid.Text;
                        objDt = objCls.bindTaxMenuWise(MODE, productId, this.txtDespatchDate.Text);
                        string _TAXID = (Convert.ToString(objDt.Rows[k]["TAXID"]));
                        if(_TAXID!= "")
                        {
                            TAXID = _TAXID;
                             productWiseTax = clsgrnmm.GetHSNTax(TAXID, Convert.ToString(lblproductid.Text), ddlvendor.SelectedValue.ToString().Trim(), this.txtDespatchDate.Text);
                        }

                        if (productWiseTax == 0)
                        {
                            productWiseTax = Convert.ToDecimal(dtTaxCountDataAddition.Rows[k]["PERCENTAGE"]);
                        }


                        if (Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]).Trim() == "INPUT CGST")
                        {
                            dr["CGSTID"] = Convert.ToString(CGSTID);
                            dr["CGSTPERCENT"] = Convert.ToString(String.Format("{0:0.00}", productWiseTax));
                            dr["CGSTAMOUNT"] = Convert.ToString(String.Format("{0:0.00}", rate * productWiseTax / 100));
                            dr["IGSTID"] = "";
                            dr["IGSTPERCENT"] = "0";
                            dr["IGSTAMOUNT"] = "0";
                        }
                        else if (Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]).Trim() == "INPUT SGST")
                        {
                            dr["SGSTID"] = Convert.ToString(SGSTID);
                            dr["SGSTPERCENT"] = Convert.ToString(String.Format("{0:0.00}", productWiseTax));
                            dr["SGSTAMOUNT"] = Convert.ToString(String.Format("{0:0.00}", rate * productWiseTax / 100));
                            dr["IGSTID"] = "";
                            dr["IGSTPERCENT"] = "0";
                            dr["IGSTAMOUNT"] = "0";
                        }
                        else if (Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]).Trim() == "INPUT IGST")
                        {
                            dr["IGSTID"] = Convert.ToString(IGSTID);
                            dr["IGSTPERCENT"] = Convert.ToString(String.Format("{0:0.00}", productWiseTax));
                            dr["IGSTAMOUNT"] = Convert.ToString(String.Format("{0:0.00}", rate * productWiseTax / 100));
                            dr["CGSTID"] = "";
                            dr["CGSTPERCENT"] = "0";
                            dr["CGSTAMOUNT"] = "0";
                            dr["SGSTID"] = "";
                            dr["SGSTPERCENT"] = "0";
                            dr["SGSTAMOUNT"] = "0";
                        }
                        dr["AFTERTAXAMNT"] = "0";
                        string amnt = "0";
                        CreateTaxDatatable(Convert.ToString(this.ddlPo.SelectedValue.Trim()),
                                           Convert.ToString(lblproductid.Text.Trim()),
                                           "",
                                           dtTaxCountDataAddition.Rows[k]["NAME"].ToString().Trim(),
                                           Convert.ToString(productWiseTax),
                                           amnt,
                                          //dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""].ToString().Trim(),
                                           Convert.ToString(lblproductname.Text),
                                           TAXID);
                    }
                    #endregion

                    dtDespatchBOM.Rows.Add(dr);
                    dtDespatchBOM.AcceptChanges();
                }
                ViewState["IssueID"] = lblissueid.Text.Trim();
            }
            this.grdAddDespatch.DataSource = dtDespatchBOM;
            this.grdAddDespatch.DataBind();
            #endregion
        }
        else
        {
            this.grdAddDespatch.DataSource = null;
            this.grdAddDespatch.DataBind();
        }
    }

    #region CalculateGrossTotal
    decimal CalculateGrossTotal(DataTable dt)
    {
        decimal GrossTotal = 0;
        try
        {
            for (int Counter = 0; Counter < dt.Rows.Count; Counter++)
            {
                GrossTotal += Convert.ToDecimal(dt.Rows[Counter]["AMOUNT"]);
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
        return GrossTotal;
    }
    #endregion

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

    #region Save Despatch
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
           

            foreach (GridViewRow gvr in grdAddDespatch.Rows)
            {
                Label lblproductname = (Label)gvr.FindControl("grdlblproductname");
                TextBox grdRate = (TextBox)gvr.FindControl("grdRate");
                TextBox grdQty = (TextBox)gvr.FindControl("grdtxtpoqty");

                if(Convert.ToDecimal(grdQty.Text) != 0)/*if qty greater than zero than rate cannot be zero*/
                {
                    if (Convert.ToDecimal(grdRate.Text) == 0)
                    {
                        MessageBox1.ShowWarning(lblproductname.Text + " " + "rate cannot be zero please check");
                        grdRate.BackColor = Color.OrangeRed;
                        return;
                    }
                }
               

            }
            string DespatchtNo = string.Empty;
            string xml = string.Empty;
            string taxDetails = string.Empty;
            string IssueXml = string.Empty;
            ClsDespatchMM clsDespatchStck = new ClsDespatchMM();
            updateDataTable();
            DataTable dtRecordsCheck = (DataTable)HttpContext.Current.Session["DESPATCHDETAILS_BOM_MM"];
            DataTable dtIssueDetails = (DataTable)HttpContext.Current.Session["ISSUEDETAILS_MM"];
            DataTable dtTax = (DataTable)HttpContext.Current.Session["TAXCOMPONENTDETAILS"];

            if (dtRecordsCheck.Rows.Count > 0)
            {
                xml = ConvertDatatableToXML(dtRecordsCheck);
                taxDetails = ConvertDatatableToXML(dtTax);
                IssueXml = ConvertDatatableToXML(dtIssueDetails);

                
                DespatchtNo = clsDespatchStck.InsertDespatchDetails(this.txtDespatchDate.Text.Trim(), this.ddlvendor.SelectedValue.Trim(),
                              Convert.ToString(this.ddlvendor.SelectedItem).Trim(),
                              this.ddlWaybill.SelectedValue.Trim(), this.ddlTransporter.SelectedValue.Trim(), this.txtVehicle.Text.Trim(),
                              HttpContext.Current.Session["DEPOTID"].ToString().Trim(),
                              HttpContext.Current.Session["DEPOTNAME"].ToString().Trim(),
                              this.txtLRGRNo.Text.Trim(), this.txtLRGRDate.Text.Trim(),
                              this.ddlTransportMode.SelectedValue.Trim(), Convert.ToString(HttpContext.Current.Session["UserID"]), HttpContext.Current.Session["FINYEAR"].ToString().Trim(),
                              this.txtRemarks.Text.Trim(), this.txtgatepassno.Text.Trim(), this.txtgatepassdate.Text.Trim(), xml, Convert.ToString(hdnDespatchID.Value).Trim(),
                              Convert.ToDecimal(txtTotalGross.Text), Convert.ToDecimal(txtRoundoff.Text), Convert.ToString(ViewState["IssueID"]), IssueXml, this.txtinvoiceno.Text.Trim(),
                              this.txtinvoicedate.Text.Trim(), this.ddlpartyname.SelectedValue.Trim(), taxDetails, Convert.ToDecimal(txtTaxAmnt.Text), Convert.ToDecimal(txtFinalAmt.Text));

                if (DespatchtNo != "")
                {
                    if (Convert.ToString(hdnDespatchID.Value) == "")
                    {
                        MessageBox1.ShowSuccess("Despatch No :  <b><font color='green'>" + DespatchtNo + "</font></b>  Saved Successfully", 60, 550);
                        this.grdAddDespatch.DataSource = null;
                        this.grdAddDespatch.DataBind();
                        this.grdproductdetails.DataSource = null;
                        this.grdproductdetails.DataBind();
                        this.LoadDespatch();
                        pnlAdd.Style["display"] = "none";
                        pnlDisplay.Style["display"] = "";
                        btnaddhide.Style["display"] = "";
                    }
                    else
                    {
                        MessageBox1.ShowSuccess("Despatch No :  <b><font color='green'>" + DespatchtNo + "</font></b> Updated Successfully", 60, 550);
                        this.LoadDespatch();
                        pnlAdd.Style["display"] = "none";
                        pnlDisplay.Style["display"] = "";
                        btnaddhide.Style["display"] = "";
                    }
                    this.hdnDespatchID.Value = "";
                    this.ResetSession();
                }
                else
                {
                    MessageBox1.ShowError("<b><font color='red'>Error on Saving record..</font></b>");
                    return;
                }
            }
            else
            {
                MessageBox1.ShowInfo("<b>Please add atleast 1 record</b>");
                return;
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region ResetSession
    public void ResetSession()
    {
        try
        {
            Session.Remove("DESPATCHDETAILS_MM");
            Session.Remove("DESPATCHDETAILS_BOM_MM");
            Session.Remove("ISSUEDETAILS_MM");
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region Delete Despatch
    protected void DeleteRecordDespatch(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            int flag = 0;
            ClsDespatchMM clsDespatchStck = new ClsDespatchMM();
            string despatchID = Convert.ToString(e.Record["STOCKDESPATCHID"]).Trim();

            if (clsDespatchStck.GetPOStatus(despatchID) == "1")
            {
                e.Record["Error"] = "GRN Already done,not allow to delete record!";
                return;
            }
            flag = clsDespatchStck.DeleteDespatch(e.Record["STOCKDESPATCHID"].ToString());
            this.hdnDespatchID.Value = "";

            if (flag == 1)
            {
                this.LoadDespatch();
                e.Record["Error"] = "Record Deleted Successfully. ";
            }
            else if (flag == -1)
            {
                this.LoadDespatch();
                e.Record["Error"] = "Issue done for this work order you cannot delete this work order";
            }
            else
            {
                e.Record["Error"] = "Error On Deleting. ";
            }


        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Edit Despatch
    protected void btngrdedit_Click(object sender, EventArgs e)
    {
        try
        {
            BindViewstateTaxid();
            this.CreateDataTableTaxComponent();
            string CGSTID = Convert.ToString(ViewState["CGSTID"]);
            string SGSTID = Convert.ToString(ViewState["SGSTID"]);
            string IGSTID = Convert.ToString(ViewState["IGSTID"]);
            ClsDespatchMM clsDespatchStck = new ClsDespatchMM();
            DataSet ds = new DataSet();
            string despatchID = Convert.ToString(hdnDespatchID.Value).Trim();
            string Status = Convert.ToString(hdnStatus.Value).Trim();
            this.trAutoDespatchNo.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.pnlAdd.Style["display"] = "";
            this.btnaddhide.Style["display"] = "none";
            ddlvendor.Enabled = false;
            ddlPo.Enabled = false;
            ddlIssue.Attributes.Add("disabled", "false");
            this.CreateDataTable();
            this.CreateDataTableBOM();
            this.CreateIssueDataTable();
            ds = clsDespatchStck.EditDespatchDetails(despatchID);

            #region Header Information
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.txtDespatchNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["STOCKDESPATCHNO"]);
                this.txtDespatchDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["DESPATCHDATE"]);
                this.ddlTransportMode.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["MODEOFTRANSPORT"]);
                this.txtVehicle.Text = Convert.ToString(ds.Tables[0].Rows[0]["VEHICHLENO"]);
                this.txtLRGRNo.Text = Convert.ToString(ds.Tables[0].Rows[0]["LRGRNO"]);
                if (Convert.ToString(ds.Tables[0].Rows[0]["LRGRDATE"]) == "01/01/1900")
                {
                    this.txtLRGRDate.Text = "";
                }
                else
                {
                    this.txtLRGRDate.Text = Convert.ToString(ds.Tables[0].Rows[0]["LRGRDATE"]);
                }
                this.txtRemarks.Text = Convert.ToString(ds.Tables[0].Rows[0]["REMARKS"]);
                this.ddlvendor.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["VENDORID"]);
                this.ddlTransporter.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["TRANSPORTERID"]);
                this.txtgatepassno.Text = Convert.ToString(ds.Tables[0].Rows[0]["GATEPASSNO"]);
                this.txtinvoiceno.Text = Convert.ToString(ds.Tables[0].Rows[0]["INVOICENO"]);
                if (Convert.ToString(ds.Tables[0].Rows[0]["INVOICEDATE"]) == "01/01/1900")
                {
                    this.txtinvoicedate.Text = "";
                }
                else
                {
                    this.txtinvoicedate.Text = Convert.ToString(ds.Tables[0].Rows[0]["INVOICEDATE"]);
                }
                this.ddlpartyname.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["PARTYID"]);

                this.LoadIssueNo("0");
                var myList = new List<string>(ds.Tables[5].Rows[0]["ISSUEID"].ToString().Split(','));

                if (ds.Tables[5].Rows.Count > 0)
                {
                    for (int counter = 0; counter < myList.Count; counter++)
                    {
                        for (int innercounter = 0; innercounter < ddlIssue.Items.Count; innercounter++)
                        {
                            if (myList[counter] == ddlIssue.Items[innercounter].Value)
                            {
                                ddlIssue.Items[innercounter].Selected = true;
                            }
                        }
                    }
                }
                //this.ddlIssue.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["ISSUEID"]);

                if (Convert.ToString(ds.Tables[0].Rows[0]["GATEPASSDATE"]) == "01/01/1900")
                {
                    this.txtgatepassdate.Text = "";
                }
                else
                {
                    this.txtgatepassdate.Text = Convert.ToString(ds.Tables[0].Rows[0]["GATEPASSDATE"]);
                }
            }
            #endregion

            #region BOM Details Information
            if (ds.Tables[1].Rows.Count > 0)
            {
                this.LoadWorkOrder(this.ddlvendor.SelectedValue, this.hdnDespatchID.Value);
                this.ddlPo.SelectedValue = Convert.ToString(ds.Tables[1].Rows[0]["POID"]).Trim();

                DataTable dtDespatchEditBOM = (DataTable)Session["DESPATCHDETAILS_BOM_MM"];
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    DataRow dr = dtDespatchEditBOM.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["POID"] = Convert.ToString(ds.Tables[1].Rows[i]["POID"]).Trim();
                    dr["PRODUCTID"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTID"]).Trim();
                    dr["PRODUCTNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["PRODUCTNAME"]).Trim();
                    dr["UOMID"] = Convert.ToString(ds.Tables[1].Rows[i]["UOMID"]).Trim();
                    dr["UOMNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["UOMNAME"]).Trim();
                    dr["MRP"] = Convert.ToString(ds.Tables[1].Rows[i]["MRP"]).Trim();
                    dr["QTY"] = Convert.ToString(ds.Tables[1].Rows[i]["QTY"]).Trim();
                    dr["RATE"] = Convert.ToString(ds.Tables[1].Rows[i]["RATE"]).Trim();
                    dr["AMOUNT"] = Convert.ToString(ds.Tables[1].Rows[i]["AMOUNT"]).Trim();
                    dr["TOTMRP"] = Convert.ToString(ds.Tables[1].Rows[i]["TOTMRP"]).Trim();
                    dr["BATCHNO"] = Convert.ToString(ds.Tables[1].Rows[i]["BATCHNO"]).Trim();
                    dr["ASSESMENTPERCENTAGE"] = Convert.ToString(ds.Tables[1].Rows[i]["ASSESMENTPERCENTAGE"]).Trim();
                    dr["TOTALASSESMENTVALUE"] = Convert.ToInt32(ds.Tables[1].Rows[i]["TOTALASSESMENTVALUE"]);
                    dr["NETWEIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["WEIGHT"]).Trim();
                    dr["ALLOCATEDQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["ALLOCATEDQTY"]).Trim();
                    dr["MFDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["MFDATE"]).Trim();
                    dr["EXPRDATE"] = Convert.ToString(ds.Tables[1].Rows[i]["EXPRDATE"]).Trim();
                    dr["GROSSWEIGHT"] = Convert.ToString(ds.Tables[1].Rows[i]["GROSSWEIGHT"]).Trim();
                    dr["ISSUEID"] = Convert.ToString(ds.Tables[1].Rows[i]["ISSUEID"]).Trim();
                    dr["BULKPRODUCTID"] = Convert.ToString(ds.Tables[1].Rows[i]["BULKPRODUCTID"]).Trim();
                    dr["BULKPRODUCTNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["BULKPRODUCTNAME"]).Trim();
                    dr["STORELOCATIONID"] = Convert.ToString(ds.Tables[1].Rows[i]["STORELOCATIONID"]).Trim();
                    dr["STORELOCATIONNAME"] = Convert.ToString(ds.Tables[1].Rows[i]["STORELOCATIONNAME"]).Trim();
                    dr["REQUISITIONQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["REQUISITIONQTY"]).Trim();
                    dr["STOCKQTY"] = Convert.ToString(ds.Tables[1].Rows[i]["STOCKQTY"]).Trim();
                    dr["CGSTPERCENT"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["CGSTPERCENT"]);
                    dr["CGSTAMOUNT"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["CGSTAMOUNT"]);
                    dr["SGSTPERCENT"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["SGSTPERCENT"]);
                    dr["SGSTAMOUNT"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["SGSTAMOUNT"]);
                    dr["IGSTPERCENT"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["IGSTPERCENT"]);
                    dr["IGSTAMOUNT"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["IGSTAMOUNT"]);
                    dr["AFTERTAXAMNT"] = Convert.ToDecimal(ds.Tables[1].Rows[i]["AFTERTAXAMNT"]);

                    dtDespatchEditBOM.Rows.Add(dr);
                    dtDespatchEditBOM.AcceptChanges();
                }

                #region grdAddDespatch DataBind
                HttpContext.Current.Session["DESPATCHDETAILS_BOM_MM"] = dtDespatchEditBOM;

                this.grdAddDespatch.DataSource = dtDespatchEditBOM;
                this.grdAddDespatch.DataBind();

                #endregion

            }
            else
            {
                #region grdAddDespatch DataBind

                this.grdAddDespatch.DataSource = null;
                this.grdAddDespatch.DataBind();
                #endregion
            }
            #endregion

            #region Product Details Information
            if (ds.Tables[3].Rows.Count > 0)
            {
                DataTable dtDespatchEdit = (DataTable)Session["DESPATCHDETAILS_MM"];
                for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                {
                    DataRow dr = dtDespatchEdit.NewRow();
                    dr["GUID"] = Guid.NewGuid();
                    dr["POID"] = Convert.ToString(ds.Tables[3].Rows[i]["POID"]).Trim();
                    dr["PRODUCTID"] = Convert.ToString(ds.Tables[3].Rows[i]["PRODUCTID"]).Trim();
                    dr["PRODUCTNAME"] = Convert.ToString(ds.Tables[3].Rows[i]["PRODUCTNAME"]).Trim();
                    dr["UOMID"] = Convert.ToString(ds.Tables[3].Rows[i]["UOMID"]).Trim();
                    dr["UOMNAME"] = Convert.ToString(ds.Tables[3].Rows[i]["UOMNAME"]).Trim();
                    dr["MRP"] = Convert.ToString(ds.Tables[3].Rows[i]["MRP"]).Trim();
                    dr["QTY"] = Convert.ToString(ds.Tables[3].Rows[i]["QTY"]).Trim();
                    dr["RATE"] = Convert.ToString(ds.Tables[3].Rows[i]["PRODUCTRATE"]).Trim();//Convert.ToString(ds.Tables[3].Rows[i]["RATE"]).Trim();
                    dr["AMOUNT"] = Convert.ToString(ds.Tables[3].Rows[i]["PURCHASEAMT"]).Trim();//Convert.ToString(ds.Tables[3].Rows[i]["AMOUNT"]).Trim();
                    dr["TOTMRP"] = Convert.ToString(ds.Tables[3].Rows[i]["TOTMRP"]).Trim();
                    dr["BATCHNO"] = Convert.ToString(ds.Tables[3].Rows[i]["BATCHNO"]).Trim();
                    dr["ASSESMENTPERCENTAGE"] = Convert.ToString(ds.Tables[3].Rows[i]["ASSESMENTPERCENTAGE"]).Trim();
                    dr["TOTALASSESMENTVALUE"] = Convert.ToInt32(ds.Tables[3].Rows[i]["TOTALASSESMENTVALUE"]);
                    dr["NETWEIGHT"] = Convert.ToString(ds.Tables[3].Rows[i]["WEIGHT"]).Trim();
                    dr["ALLOCATEDQTY"] = Convert.ToString(ds.Tables[3].Rows[i]["ALLOCATEDQTY"]).Trim();
                    dr["MFDATE"] = Convert.ToString(ds.Tables[3].Rows[i]["MFDATE"]).Trim();
                    dr["EXPRDATE"] = Convert.ToString(ds.Tables[3].Rows[i]["EXPRDATE"]).Trim();
                    dr["GROSSWEIGHT"] = Convert.ToString(ds.Tables[3].Rows[i]["GROSSWEIGHT"]).Trim();
                    dr["PRODUCTRATE"] = Convert.ToString(ds.Tables[3].Rows[i]["PRODUCTRATE"]).Trim();
                    dr["PURCHASEAMT"] = Convert.ToString(ds.Tables[3].Rows[i]["PURCHASEAMT"]).Trim();
                    dr["WORKORDERNO"] = Convert.ToString(ds.Tables[3].Rows[i]["WORKORDERNO"]).Trim();

                    dtDespatchEdit.Rows.Add(dr);
                    dtDespatchEdit.AcceptChanges();
                }

                #region grdAddDespatch DataBind
                HttpContext.Current.Session["DESPATCHDETAILS_MM"] = dtDespatchEdit;

                this.grdproductdetails.DataSource = dtDespatchEdit;
                this.grdproductdetails.DataBind();

                #endregion

            }
            else
            {
                #region grdproductdetails DataBind

                this.grdproductdetails.DataSource = null;
                this.grdproductdetails.DataBind();
                #endregion
            }
            #endregion

            #region Issue Details
            DataTable dtEditIssue = (DataTable)HttpContext.Current.Session["ISSUEDETAILS_MM"];
            for (int i = 0; i < ds.Tables[4].Rows.Count; i++)
            {
                DataRow drEditIssue = dtEditIssue.NewRow();
                drEditIssue["GUID"] = Guid.NewGuid();
                drEditIssue["POID"] = Convert.ToString(ds.Tables[4].Rows[i]["POID"]).Trim();
                drEditIssue["PRODUCTID"] = Convert.ToString(ds.Tables[4].Rows[i]["PRODUCTID"]).Trim();
                drEditIssue["PRODUCTNAME"] = Convert.ToString(ds.Tables[4].Rows[i]["PRODUCTNAME"]).Trim();
                drEditIssue["ISSUEID"] = Convert.ToString(ds.Tables[4].Rows[i]["ISSUEID"]).Trim();
                drEditIssue["QTY"] = Convert.ToString(ds.Tables[4].Rows[i]["QTY"]).Trim();
                drEditIssue["BULKPRODUCTID"] = Convert.ToString(ds.Tables[4].Rows[i]["BULKPRODUCTID"]).Trim();
                drEditIssue["BULKPRODUCTNAME"] = Convert.ToString(ds.Tables[4].Rows[i]["BULKPRODUCTNAME"]).Trim();
                dtEditIssue.Rows.Add(drEditIssue);
                dtEditIssue.AcceptChanges();
            }
            HttpContext.Current.Session["ISSUEDETAILS_MM"] = dtEditIssue;
            #endregion

            #region Amount-Calculation
            if (ds.Tables[2].Rows.Count > 0)
            {
                this.txtAdj.Text = String.Format("{0:0.00}", ds.Tables[2].Rows[0]["ADJUSTMENTVALUE"].ToString());
                this.txtTaxAmnt.Text = String.Format("{0:0.00}", ds.Tables[2].Rows[0]["TAXAMNT"].ToString());
                this.txtFinalAmt.Text = String.Format("{0:0.00}", ds.Tables[2].Rows[0]["NETAMNT"].ToString());
                this.txtRoundoff.Text = String.Format("{0:0.00}", ds.Tables[2].Rows[0]["ROUNDOFFVALUE"].ToString());
                this.txtOtherCharge.Text = String.Format("{0:0.00}", ds.Tables[2].Rows[0]["OTHERCHARGESVALUE"].ToString());
                this.txtTotalGross.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[2].Rows[0]["TOTALDESPATCHVALUE"].ToString()) - Convert.ToDecimal(ds.Tables[2].Rows[0]["ROUNDOFFVALUE"].ToString())));
            }
            #endregion

            this.ddlTransportMode.Enabled = true;
            this.ddlTransporter.Enabled = true;
            this.txtVehicle.Enabled = true;
            this.txtLRGRNo.Enabled = true;
            this.imgbtnLRGRCalendar.Enabled = true;
            this.ddlWaybill.Enabled = true;
            this.Imgbtngatepass.Enabled = true;


            if(Status== "APPROVED")
            {
                this.btnsubmitdiv.Style["display"] = "none";
            }
            else 
            {
                this.btnsubmitdiv.Style["display"] = "";
            }

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region New Entry
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            this.btnsubmitdiv.Style["display"] = "";
            this.ddlTransportMode.Enabled = true;
            this.ddlTransporter.Enabled = true;
            this.txtVehicle.Enabled = true;
            this.txtLRGRNo.Enabled = true;
            this.imgbtnLRGRCalendar.Enabled = true;
            this.ddlWaybill.Enabled = true;
            this.Imgbtngatepass.Enabled = true;
            this.hdnDespatchID.Value = "";
            this.trAutoDespatchNo.Style["display"] = "none";
            this.pnlDisplay.Style["display"] = "none";
            this.pnlAdd.Style["display"] = "";
            this.btnaddhide.Style["display"] = "none";
            this.ClearControls();
            this.ResetSession();
            this.grdAddDespatch.DataSource = null;
            this.grdAddDespatch.DataBind();
            this.grddespatchrecord.DataSource = null;
            this.grddespatchrecord.DataBind();
            this.ddlvendor.SelectedValue = "0";
            this.ddlPo.Items.Clear();
            this.ddlPo.Items.Add(new ListItem("SELECT ORDER NO", "0"));
            this.ddlPo.AppendDataBoundItems = true;
            this.ddlPo.SelectedValue = "0";
            this.ddlvendor.Enabled = true;
            this.ddlPo.Enabled = true;
            this.ddlIssue.Items.Clear();
            this.ddlIssue.SelectedValue = "0";
            DateTime dtcurr = DateTime.Now;
            string date = "dd/MM/yyyy";
            this.txtDespatchDate.Text = dtcurr.ToString(date).Replace('-', '/');
            //ddlIssue.Attributes.Add("Enabled", "true");
            ddlIssue.Attributes.Remove("disabled");
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    #endregion

    #region ClearControls
    protected void ClearControls()
    {
        try
        {
            this.ddlTransportMode.Enabled = true;
            this.ddlTransporter.Enabled = true;
            this.txtVehicle.Enabled = true;
            this.txtLRGRNo.Enabled = true;
            this.imgbtnLRGRCalendar.Enabled = true;
            this.ddlWaybill.Enabled = true;
            this.Imgbtngatepass.Enabled = true;
            this.hdnDespatchID.Value = "";
            this.txtDespatchNo.Text = "";
            this.ddlTransporter.SelectedValue = "0";
            this.ddlTransportMode.SelectedValue = "0";
            this.txtLRGRDate.Text = "";
            this.txtLRGRNo.Text = "";
            this.txtVehicle.Text = "";
            this.txtRemarks.Text = "";
            this.ddlWaybill.SelectedValue = "0";
            this.ddlPo.Items.Clear();
            this.ddlPo.Items.Add(new ListItem("SELECT ORDER NO", "0"));
            this.ddlPo.AppendDataBoundItems = true;
            this.ddlPo.SelectedValue = "0";
            this.txtgatepassno.Text = "";
            this.txtgatepassdate.Text = "";
            this.txtAdj.Text = "0";
            this.txtOtherCharge.Text = "0";
            this.txtTotalGross.Text = "";
            this.txtRoundoff.Text = "";
            this.grdAddDespatch.DataSource = null;
            this.grdAddDespatch.DataBind();
            this.grdproductdetails.DataSource = null;
            this.grdproductdetails.DataBind();
            this.ddlPo.SelectedValue = "0";
            //this.ddlIssue.SelectedValue = "0";
            this.txtFinalAmt.Text = "";
            this.txtTotalGross.Text = "";
            this.txtTaxAmnt.Text = "0";
            this.txtinvoiceno.Text = "";
            this.txtinvoicedate.Text = "";
            this.ddlpartyname.SelectedValue = "0";
            this.btnsubmitdiv.Style["display"] = "";
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    #region Cancel
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            this.trAutoDespatchNo.Style["display"] = "none";
            this.pnlDisplay.Style["display"] = "";
            this.pnlAdd.Style["display"] = "none";
            this.btnaddhide.Style["display"] = "";
            this.ClearControls();
            this.ResetSession();
            this.grdAddDespatch.DataSource = null;
            this.grdAddDespatch.DataBind();
            this.grdproductdetails.DataSource = null;
            this.grdproductdetails.DataBind();
            this.grddespatchrecord.DataSource = null;
            this.grddespatchrecord.DataBind();
            this.hdnDespatchID.Value = "";
            this.LoadDespatch();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Search Despatch
    protected void btnSearchDespatch_Click(object sender, EventArgs e)
    {
        try
        {
            this.LoadDespatch();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Approval 
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        int flag = 0;
        ClsDespatchMM clsDespatchStck = new ClsDespatchMM();
        flag = clsDespatchStck.JobWorkApprove(hdnDespatchID.Value.Trim());
        if (flag == 1)
        {
            this.LoadDespatch();
            btnaddhide.Style["display"] = "none";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            MessageBox1.ShowSuccess("Despatch No : <b><font color='green'>" + txtDespatchNo.Text + "</font></b>  Approved Successfully", 60, 600);
        }
        else if (flag == 0)
        {
            btnaddhide.Style["display"] = "none";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            MessageBox1.ShowError("<b><font color='red'>Error saving record..!</font></b>");
        }
    }
    #endregion

    #region Reject
    protected void btnreject_Click(object sender, EventArgs e)
    {
        try
        {
            btnaddhide.Style["display"] = "none";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
        }
        catch
        {

        }
    }
    #endregion

    #region grdDespatchHeader_RowDataBound
    protected void grdDespatchHeader_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[10] as GridDataControlFieldCell;
                string status = cell.Text.Trim().ToUpper();

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

    #region btnPrint_Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            string receivedID = Convert.ToString(hdnDespatchID.Value);

            string path = "frmRptInvoicePrint_FAC.aspx?Stnid=" + receivedID + "&&MenuId=" + Request.QueryString["MENUID"].ToString().Trim() + "";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + path + "', 'Archive', 'channelmode,width=1000,height=600,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=100,top=100' );", true);

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
        CalendarFromDate.StartDate = oDate;
        CalendarExtenderToDate.StartDate = oDate;
        CalendarExtenderDespatchDate.StartDate = oDate;

        if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
        {
            this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtDespatchDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            CalendarFromDate.EndDate = today1;
            CalendarExtenderToDate.EndDate = today1;
            CalendarExtenderDespatchDate.EndDate = today1;
        }
        else
        {
            this.txtFromDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtToDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            this.txtDespatchDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
            CalendarFromDate.EndDate = cDate;
            CalendarExtenderToDate.EndDate = cDate;
            CalendarExtenderDespatchDate.EndDate = cDate;
        }
    }
    #endregion

    protected void grdRate_TextChanged(object sender, EventArgs e)
    {



    }

    public void updateDataTable()
    {
         this.Session["DESPATCHDETAILS_BOM_MM"] = null;
         this.Session["TAXCOMPONENTDETAILS"] = null;
        string TAXID = string.Empty;
        if (this.Session["DESPATCHDETAILS_BOM_MM"] == null)
        {
            this.CreateDataTableBOM();
        }
        if (this.Session["TAXCOMPONENTDETAILS"] == null)
        {
            this.CreateDataTableTaxComponent();
        }
        DataTable dtDespatchBOM = (DataTable)HttpContext.Current.Session["DESPATCHDETAILS_BOM_MM"];

        foreach (GridViewRow gvr in grdAddDespatch.Rows)
        {
            TextBox txtqty = (TextBox)gvr.FindControl("grdtxtpoqty");
            TextBox txtamount = (TextBox)gvr.FindControl("grdtxtamount");
            Label lblproductid = (Label)gvr.FindControl("grdlblproductid");
            Label lblproductname = (Label)gvr.FindControl("grdlblproductname");
            Label lbluomid = (Label)gvr.FindControl("grdlbluomid");
            Label lbluomname = (Label)gvr.FindControl("grdlbluomname");
            Label lblmrp = (Label)gvr.FindControl("grdlblmrp");
            Label lblrate = (Label)gvr.FindControl("grdlblrate");
            Label lblbatchno = (Label)gvr.FindControl("grdlblbatchno");
            Label lbltotalmrp = (Label)gvr.FindControl("grdlbltotalmrp");
            Label lblallocatedqty = (Label)gvr.FindControl("grdlblallocatedqty");
            Label lblissueid = (Label)gvr.FindControl("grdlblissueid");
            Label lblWorkOrderproductid = (Label)gvr.FindControl("grdlblWorkOrderproductid");
            Label lblBulkproductname = (Label)gvr.FindControl("grdlblBulkproductname");
            Label lblfromstorelocationid = (Label)gvr.FindControl("grdlblfromstorelocationid");
            Label lblfromstorelocation = (Label)gvr.FindControl("grdlblfromstorelocation");
            Label grdlblrequisitionqty = (Label)gvr.FindControl("grdlblrequisitionqty");
            TextBox grdCGST_PERCENTAGE = (TextBox)gvr.FindControl("grdCGST_PERCENTAGE");
            TextBox grdCGSTAMOUNT = (TextBox)gvr.FindControl("grdCGSTAMOUNT");
            TextBox grdSGST_PERCENTAGE = (TextBox)gvr.FindControl("grdSGST_PERCENTAGE");
            TextBox grdSGSTAMOUNT = (TextBox)gvr.FindControl("grdSGSTAMOUNT");
            TextBox grdIGST_PERCENTAGE = (TextBox)gvr.FindControl("grdIGST_PERCENTAGE");
            TextBox grdIGSTAMOUNT = (TextBox)gvr.FindControl("grdIGSTAMOUNT");
            TextBox grdTAXABLEAMNT = (TextBox)gvr.FindControl("grdTAXABLEAMNT");
            TextBox grdRate = (TextBox)gvr.FindControl("grdRate");

           


            if (!string.IsNullOrEmpty(txtqty.Text.Trim()))
            {
                DataRow dr = dtDespatchBOM.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["POID"] = Convert.ToString(this.ddlPo.SelectedValue);
                dr["PRODUCTID"] = Convert.ToString(lblproductid.Text).Trim();
                dr["PRODUCTNAME"] = Convert.ToString(lblproductname.Text).Trim();
                dr["UOMID"] = Convert.ToString(lbluomid.Text);
                dr["UOMNAME"] = Convert.ToString(lbluomname.Text).Trim();
                dr["MRP"] = Convert.ToString(lblmrp.Text.Trim());
                dr["QTY"] = Convert.ToString(txtqty.Text.Trim());
                dr["RATE"] = Convert.ToDecimal(grdRate.Text);
                dr["AMOUNT"] = Convert.ToString(String.Format("{0:0.00}", grdTAXABLEAMNT.Text.Trim()));
                dr["TOTMRP"] = Convert.ToString(lbltotalmrp.Text);
                dr["BATCHNO"] = Convert.ToString(lblbatchno.Text);
                dr["ASSESMENTPERCENTAGE"] = Convert.ToString(0);
                dr["TOTALASSESMENTVALUE"] = Convert.ToString(String.Format("{0:0.00}", 0));
                dr["NETWEIGHT"] = "0";
                dr["ALLOCATEDQTY"] = Convert.ToString(lblallocatedqty.Text.Trim());
                dr["MFDATE"] = Convert.ToString(null);
                dr["EXPRDATE"] = Convert.ToString(null);
                dr["GROSSWEIGHT"] = "0";
                dr["ISSUEID"] = Convert.ToString(lblissueid.Text.Trim());
                dr["BULKPRODUCTID"] = Convert.ToString(lblWorkOrderproductid.Text.Trim());
                dr["BULKPRODUCTNAME"] = Convert.ToString(lblBulkproductname.Text.Trim());
                dr["STORELOCATIONID"] = Convert.ToString(lblfromstorelocationid.Text.Trim());
                dr["STORELOCATIONNAME"] = Convert.ToString(lblfromstorelocation.Text).Trim();
                dr["REQUISITIONQTY"] = Convert.ToString(grdlblrequisitionqty.Text).Trim();
                dr["CGSTID"]= ViewState["CGSTID"];
                dr["CGSTPERCENT"] = Convert.ToDecimal(grdCGST_PERCENTAGE.Text);
                dr["CGSTAMOUNT"]= Convert.ToDecimal(grdCGSTAMOUNT.Text);
                dr["SGSTID"]= ViewState["SGSTID"];
                dr["SGSTPERCENT"]= Convert.ToDecimal(grdSGST_PERCENTAGE.Text);
                dr["SGSTAMOUNT"] = Convert.ToDecimal(grdSGSTAMOUNT.Text);
                dr["IGSTID"]= ViewState["IGSTID"];
                dr["IGSTPERCENT"]= Convert.ToDecimal(grdIGST_PERCENTAGE.Text);
                dr["IGSTAMOUNT"]= Convert.ToDecimal(grdIGSTAMOUNT.Text);
               
                dtDespatchBOM.Rows.Add(dr);
                dtDespatchBOM.AcceptChanges();

                ClsGRNMM clsgrnmm = new ClsGRNMM();
                DataTable dtTaxCountDataAddition = (DataTable)Session["dtTaxCount"];
                decimal productWiseTax = 0;
                ViewState["Invoice_Type"] = dtTaxCountDataAddition.Rows.Count;
                for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
                {
                    TAXID = clsgrnmm.TaxID(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
                    DataTable objDt = new DataTable();
                    ClsDespatchMM objCls = new ClsDespatchMM();
                    string MODE = "PIDTAX";
                    string productId = lblproductid.Text;
                    objDt = objCls.bindTaxMenuWise(MODE, productId, this.txtDespatchDate.Text);
                    string _TAXID = (Convert.ToString(objDt.Rows[k]["TAXID"]));
                    if (_TAXID != "")
                    {
                        TAXID = _TAXID;
                        productWiseTax = clsgrnmm.GetHSNTax(TAXID, Convert.ToString(lblproductid.Text), ddlvendor.SelectedValue.ToString().Trim(), this.txtDespatchDate.Text);
                    }

                    if (productWiseTax == 0)
                    {
                        productWiseTax = Convert.ToDecimal(dtTaxCountDataAddition.Rows[k]["PERCENTAGE"]);
                    }

                    string amnt = "0";

                    if (Convert.ToString(grdCGSTAMOUNT.Text) == "0")
                    {
                        amnt = Convert.ToString(grdIGSTAMOUNT.Text);
                    }

                    amnt = Convert.ToString(grdCGSTAMOUNT.Text);
                    

                    CreateTaxDatatable(Convert.ToString(this.ddlPo.SelectedValue.Trim()),
                                       Convert.ToString(lblproductid.Text.Trim()),
                                       "",
                                       dtTaxCountDataAddition.Rows[k]["NAME"].ToString().Trim(),
                                       Convert.ToString(productWiseTax),
                                       amnt,
                                       //dr["" + Convert.ToString(dtTaxCountDataAddition.Rows[k]["NAME"]) + ""].ToString().Trim(),
                                       Convert.ToString(lblproductname.Text),
                                       TAXID);
                }

            }
           
        }
          
        
    }

    protected void grdAddDespatch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            decimal qty = 0;
            decimal TAXABLEAMNT = 0;
            decimal CGSTAMOUNT = 0;
            decimal SGSTAMOUNT = 0;
            decimal IGSTAMOUNT = 0;
            decimal TOTALAMOUNT = 0;
            DataTable dt = new DataTable();
            dt = (DataTable)HttpContext.Current.Session["DESPATCHDETAILS_BOM_MM"];
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                /*only data row exists or not check*/
            }

            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                foreach (GridViewRow gvr in grdAddDespatch.Rows)
                {
                    TextBox txtqty = (TextBox)gvr.FindControl("grdtxtpoqty");//7
                    //TextBox grdTAXABLEAMNT = (TextBox)gvr.FindControl("grdTAXABLEAMNT");//9
                    //TextBox grdCGSTAMOUNT = (TextBox)gvr.FindControl("grdCGSTAMOUNT");//11
                    //TextBox grdSGSTAMOUNT = (TextBox)gvr.FindControl("grdSGSTAMOUNT");//13
                    //TextBox grdIGSTAMOUNT = (TextBox)gvr.FindControl("grdIGSTAMOUNT");//15
                    //TextBox grdTOTALAMOUNT = (TextBox)gvr.FindControl("grdTOTALAMOUNT");//16

                   // if(grdTAXABLEAMNT.Text=="")
                   // {
                   //     grdTAXABLEAMNT.Text = "0";
                   // }
                   //else if (grdCGSTAMOUNT.Text=="")
                   // {
                   //     grdCGSTAMOUNT.Text = "0";
                   // }
                   // else if (grdSGSTAMOUNT.Text=="")
                   // {
                   //     grdSGSTAMOUNT.Text = "0";
                   // }
                   // else if(grdIGSTAMOUNT.Text=="")
                   // {
                   //     grdIGSTAMOUNT.Text = "0";
                   // }
                   // else if(grdTOTALAMOUNT.Text=="")
                   // {
                   //     grdTOTALAMOUNT.Text = "0";
                   // }


                    qty += Convert.ToDecimal(txtqty.Text.Trim());
                    //TAXABLEAMNT += Convert.ToDecimal(grdTAXABLEAMNT.Text.Trim());
                    //CGSTAMOUNT += Convert.ToDecimal(grdCGSTAMOUNT.Text.Trim());
                    //SGSTAMOUNT += Convert.ToDecimal(grdSGSTAMOUNT.Text.Trim());
                    //IGSTAMOUNT += Convert.ToDecimal(grdIGSTAMOUNT.Text.Trim());
                    //TOTALAMOUNT += Convert.ToDecimal(grdTOTALAMOUNT.Text.Trim());

                }

                // Display the summary data in the appropriate cells
                e.Row.Cells[7].Text =  "Total: " + qty.ToString();
                //e.Row.Cells[9].Text =  "Total: " + TAXABLEAMNT.ToString();
                //e.Row.Cells[11].Text = "Total: " + CGSTAMOUNT.ToString();
                //e.Row.Cells[13].Text = "Total: " + SGSTAMOUNT.ToString();
                //e.Row.Cells[15].Text = "Total: " + IGSTAMOUNT.ToString();
                //e.Row.Cells[16].Text = "Total: " + TOTALAMOUNT.ToString();
            }
        }
        catch(Exception ex)
        {
            MessageBox1.ShowError("ex"+ex);
        }
    }
}