using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkFlow;

public partial class VIEW_frmJobOrderApproval : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ClsUnlockInvoice clsInvc = new ClsUnlockInvoice();
                this.pnlADD.Style["display"] = "";
                this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.Loaddepot();
                this.LoadJobOrderVendor();
                this.trinvoice.Style["display"] = "";
                this.gvUnlockInvoice.DataSource = null;
                this.gvUnlockInvoice.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    private void Loaddepot()
    {
        try
        {
            ClsUnlockInvoice clsInvc = new ClsUnlockInvoice();
            DataTable dt = clsInvc.BindBranch();
             if (dt.Rows.Count > 0)
             {
                 this.ddlDepot.Items.Clear();
                 this.ddlDepot.Items.Add(new ListItem("Select Depot", "0"));
                 this.ddlDepot.AppendDataBoundItems = true;
                 this.ddlDepot.DataSource = dt;
                 this.ddlDepot.DataValueField = "BRID";
                 this.ddlDepot.DataTextField = "BRNAME";
                 this.ddlDepot.DataBind();

             }
             else
             {
                 this.ddlDepot.Items.Clear();
                 this.ddlDepot.Items.Add(new ListItem("Select Depot", "0"));
             }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }

    public void LoadJobOrderVendor()
    {
        try
        {
            string mode = "JobOrderVendor";
            ClsVendor_TPU clsInvc = new ClsVendor_TPU();
            DataTable dt = clsInvc.BindPoFromTpu(mode,"");
            if (dt.Rows.Count > 0)
            {
                this.ddlVendor.Items.Clear();
                this.ddlVendor.Items.Add(new ListItem("Select Vendor", "0"));
                this.ddlVendor.AppendDataBoundItems = true;
                this.ddlVendor.DataSource = dt;
                this.ddlVendor.DataValueField = "VENDORID";
                this.ddlVendor.DataTextField = "VENDORNAME";
                this.ddlVendor.DataBind();

            }
            else
            {
                this.ddlVendor.Items.Clear();
                this.ddlVendor.Items.Add(new ListItem("Select Vendor", "0"));
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }


    protected void btnSearchInvoice_Click(object sender, EventArgs e)
    {

        try
        {
            string mode = "SearchJobOrder";
            DataTable objDt = new DataTable();
            ClsVendor_TPU objCls = new ClsVendor_TPU();
            objDt = objCls.SearchGateEntry(mode, this.txtFromDate.Text, this.txtToDate.Text,this.ddlVendor.SelectedValue);
            if(objDt.Rows.Count> 0)
            {
                this.gvUnlockInvoice.DataSource = objDt;
                this.gvUnlockInvoice.DataBind();
            }
            else
            {
                MessageBox1.ShowInfo("No data found");
                return;
            }
        }
        catch(Exception EX)
        {
            string msg = EX.Message.Replace("'", "");
        }

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        ClsPurchaseStockReceipt_FAC clsPurchaseStockReceipt = new ClsPurchaseStockReceipt_FAC();
        string xml = string.Empty;
        if (HttpContext.Current.Session["JOB_WORK"] == null)
        {
            CreateDataTable();
        }
        DataTable DT = (DataTable)HttpContext.Current.Session["SALEINVOICE"];
        foreach (GridViewRow gvrow in gvUnlockInvoice.Rows)
        {
            CheckBox chkBx = (CheckBox)gvrow.FindControl("chkSelect");
            Label lblSALEINVOICEID = (Label)gvrow.FindControl("lblSALEINVOICEID");
            Label lblSALEINVOICENO = (Label)gvrow.FindControl("lblSALEINVOICENO");
            Label lblINVOICENO = (Label)gvrow.FindControl("lblINVOICENO");
            Label lblINVOICEDATE = (Label)gvrow.FindControl("lblINVOICEDATE");
            Label lblSALEINVOICEDATE = (Label)gvrow.FindControl("lblSALEINVOICEDATE");
            Label lblDISTRIBUTORNAME = (Label)gvrow.FindControl("lblDISTRIBUTORNAME");
            Label lblBASICVALUE = (Label)gvrow.FindControl("lblBASICVALUE");
            Label lblTOTALTAXAMNT = (Label)gvrow.FindControl("lblTOTALTAXAMNT");
            Label lblNETAMOUNT = (Label)gvrow.FindControl("lblNETAMOUNT");

            if (chkBx.Checked)
            {
                
                DataRow dr = DT.NewRow();
                dr["SALEINVOICEID"] = lblSALEINVOICEID.Text.Trim();
                dr["SALEINVOICENO"] = lblSALEINVOICENO.Text.Trim();
                dr["INVOICENO"] = lblINVOICENO.Text.Trim();
                dr["INVOICEDATE"] = lblINVOICEDATE.Text.Trim();
                dr["SALEINVOICEDATE"] = lblSALEINVOICEDATE.Text.Trim();
                dr["DISTRIBUTORNAME"] = lblDISTRIBUTORNAME.Text.Trim();
                dr["BASICVALUE"] = lblBASICVALUE.Text.Trim();
                dr["TOTALTAXAMNT"] = lblTOTALTAXAMNT.Text.Trim();
                dr["NETAMOUNT"] = lblNETAMOUNT.Text.Trim();
                DT.Rows.Add(dr);
                DT.AcceptChanges();
            }
        }
        int flag = 0;
        xml = ConvertDatatable(DT);
        //flag = clsPurchaseStockReceipt.ApprovedJobOrder(xml, Session["FINYEAR"].ToString(), Session["USERID"].ToString());


    }


    #region Create DataTable Structure
    public void CreateDataTable()
    {
        DataTable dt = new DataTable();

        dt.Clear();

        dt.Columns.Add(new DataColumn("SALEINVOICEID", typeof(string)));
        dt.Columns.Add(new DataColumn("SALEINVOICENO", typeof(string)));
        dt.Columns.Add(new DataColumn("INVOICENO", typeof(string)));
        dt.Columns.Add(new DataColumn("INVOICEDATE", typeof(string)));
        dt.Columns.Add(new DataColumn("SALEINVOICEDATE", typeof(string)));
        dt.Columns.Add(new DataColumn("DISTRIBUTORNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("BASICVALUE", typeof(string)));
        dt.Columns.Add(new DataColumn("TOTALTAXAMNT", typeof(string)));
        dt.Columns.Add(new DataColumn("NETAMOUNT", typeof(string)));


        HttpContext.Current.Session["JOB_WORK"] = dt;


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
}