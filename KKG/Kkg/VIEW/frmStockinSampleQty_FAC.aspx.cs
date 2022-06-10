using BAL;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class FACTORY_frmStockinSampleQty_FAC : System.Web.UI.Page
{
    string Mode = string.Empty;
    ClsGRNMM clsgrnmm = new ClsGRNMM();
    DataTable dtSampleQty = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["RECEIVEDID"].ToString().Trim()))
                {
                    string ReceivedID = Request.QueryString["RECEIVEDID"];
                    Mode = Request.QueryString["MODE"];
                    if (Mode == "StockIn")
                    {
                        LoadReceiveQty(ReceivedID);
                        divSaveButton.Visible = true;
                    }
                    else
                    {
                        LoadSampleGrid(ReceivedID);
                        divSaveButton.Visible = false;
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

    #region CreateDataTable_SampleQty
    private void CreateDataTable_SampleQty()
    {
        Session["STOCKRECEIVED_SAMPLEQTY"] = null;
        DataTable dtSample = new DataTable();
        dtSample.Columns.Add(new DataColumn("STOCKRECEIVEDID", typeof(string)));
        dtSample.Columns.Add(new DataColumn("POID", typeof(string)));
        dtSample.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
        dtSample.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));
        dtSample.Columns.Add(new DataColumn("RECEIVEDQTY", typeof(string)));
        dtSample.Columns.Add(new DataColumn("SAMPLEQTY", typeof(string)));
        dtSample.Columns.Add(new DataColumn("OBSERVATIONQTY", typeof(string)));
        HttpContext.Current.Session["STOCKRECEIVED_SAMPLEQTY"] = dtSample;
    }
    #endregion

    public void LoadReceiveQty(string ReceivedID)
    {
        try
        {
            dtSampleQty = clsgrnmm.BindReceiveQty(ReceivedID);
            GvSample.DataSource = dtSampleQty;
            GvSample.DataBind();
            //HttpContext.Current.Session["STOCKRECEIVED_SAMPLEQTY"] = dtSampleQty;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void LoadSampleGrid(string ReceivedID)
    {
        try
        {
            dtSampleQty = clsgrnmm.BindSampleQty(ReceivedID);
            GvSample.DataSource = dtSampleQty;
            GvSample.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        DataTable DtSample = new DataTable();
        CreateDataTable_SampleQty();
        DtSample = (DataTable)HttpContext.Current.Session["STOCKRECEIVED_SAMPLEQTY"];
        foreach (GridViewRow gvRow in GvSample.Rows)
        {
            decimal lblReceivedQty = Convert.ToDecimal(((Label)gvRow.FindControl("lblReceivedQty")).Text);
            decimal txtsampleQty = Convert.ToDecimal(((TextBox)gvRow.FindControl("txtsampleQty")).Text);
            decimal txtObservationQty = Convert.ToDecimal(((TextBox)gvRow.FindControl("txtObservationQty")).Text);

            if (txtsampleQty != 0 || txtObservationQty != 0)
            {
                if (lblReceivedQty >= txtsampleQty)
                {
                    DataRow dr = DtSample.NewRow();
                    dr["STOCKRECEIVEDID"] = ((Label)gvRow.FindControl("lblStockReceivedID")).Text;
                    dr["POID"] = ((Label)gvRow.FindControl("lblPOID")).Text;
                    dr["PRODUCTID"] = ((Label)gvRow.FindControl("lblProductid")).Text;
                    dr["PRODUCTNAME"] = ((Label)gvRow.FindControl("lblProductName")).Text;
                    dr["RECEIVEDQTY"] = ((Label)gvRow.FindControl("lblReceivedQty")).Text;
                    dr["SAMPLEQTY"] = ((TextBox)gvRow.FindControl("txtsampleQty")).Text;
                    dr["OBSERVATIONQTY"] = ((TextBox)gvRow.FindControl("txtObservationQty")).Text;
                    DtSample.Rows.Add(dr);
                    DtSample.AcceptChanges();
                }
                else
                {
                    MessageBox1.ShowInfo("Receive Qty Can not be greater than Balance Qty.", 60, 450);
                    return;
                }
            }
            else
            {
                MessageBox1.ShowInfo("Sample Qty OR Observation Qty Can not be 0.", 60, 450);
                return;
            }
        }
        if (DtSample.Rows.Count > 0)
        {
            HttpContext.Current.Session["STOCKRECEIVED_SAMPLEQTY"] = DtSample;
            MessageBox1.ShowSuccess("Sample Qty Save : <b><font color='green'></font></b>Successfully", 60, 350);
        }
        else
        {
            MessageBox1.ShowError("<b><font color='red'>Error on Add record..</font></b>");
        }
    }

    protected void btnuploadcancel_Click(object sender, EventArgs e)
    {
        try
        {
            string close = @"<script type='text/javascript'>
                                window.returnValue = true;
                                window.close();
                                </script>";
            base.Response.Write(close);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
}