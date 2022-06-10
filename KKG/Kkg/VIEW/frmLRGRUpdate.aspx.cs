 using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Obout.Grid;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;



public partial class VIEW_frmLRGRUpdate : System.Web.UI.Page
{
   

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                pnlADD.Style["display"] = "";
                this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.Loaddepot();
                this.LoadBusinessSegment();
                gvUnlockInvoice.HeaderRow.Cells[9].Text = "TOTAL CASE";
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
            clsSaleInvoice clsInvc = new clsSaleInvoice();
            DataTable dt = clsInvc.BindBranch();
            if (dt.Rows.Count > 0)
            {
                this.ddlDepot.Items.Clear();
                this.ddlDepot.Items.Add(new ListItem("Select Depot", "0"));
                this.ddlDepot.AppendDataBoundItems = true;
                this.ddlDepot.DataSource = dt; 
                this.ddlDepot.DataValueField = "BRID";
                this.ddlDepot.DataTextField = "BRPREFIX";
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

    private void LoadBusinessSegment()
    {
        try
        {
            clsSaleInvoice clsInvc = new clsSaleInvoice();
            DataTable dt = clsInvc.BindBusinessSegment();
            if (dt.Rows.Count > 0)
            {
                this.ddlBusinessSegment.Items.Clear();
                this.ddlBusinessSegment.Items.Add(new ListItem("Select Group", "0"));
                this.ddlBusinessSegment.AppendDataBoundItems = true;
                this.ddlBusinessSegment.DataSource = dt;
                this.ddlBusinessSegment.DataValueField = "BSID";
                this.ddlBusinessSegment.DataTextField = "BSNAME";
                this.ddlBusinessSegment.DataBind();
            }
            else
            {
                this.ddlBusinessSegment.Items.Clear();
                this.ddlBusinessSegment.Items.Add(new ListItem("Select Depot", "0"));
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }

    protected void ddlModule_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlModule.SelectedValue == "1" || ddlModule.SelectedValue == "5")/*invoice Or Sale Return*/
            {
                tdBusinessSegment.Visible = true;
               
                tdGroup.Visible = true;
            }
            else
            {
                tdBusinessSegment.Visible = false;
                tdGroup.Visible = false;
              
            }
            this.gvUnlockInvoice.DataSource = null;
            this.gvUnlockInvoice.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }

    #region LoadInvoice
    public void LoadInvoice()
    {
        try
        {
            if (ddlModule.SelectedValue == "2")/*else than invoie*/
            {
                this.ddlBusinessSegment.SelectedValue = "0";
            }
           

            clsSaleInvoice clsInvc = new clsSaleInvoice();
            DataTable dt = new DataTable();
            dt = clsInvc.BindInvoiceBlankLrGrNo(txtFromDate.Text.Trim(), txtToDate.Text.Trim(), HttpContext.Current.Session["FINYEAR"].ToString(), 
                                                    this.ddlBusinessSegment.SelectedValue.Trim(), this.ddlDepot.SelectedValue.Trim(),this.ddlModule.SelectedValue.Trim());
            if (dt.Rows.Count > 0)
            {
                this.gvUnlockInvoice.DataSource = dt;
                this.gvUnlockInvoice.DataBind();
            }
            else
            {

                this.gvUnlockInvoice.DataSource = null;
                this.gvUnlockInvoice.DataBind();
            }

            if (ddlModule.SelectedValue == "5")
            {
                gvUnlockInvoice.HeaderRow.Cells[9].Text = "TOTAL PCS";
            }
            else
            {
                gvUnlockInvoice.HeaderRow.Cells[9].Text = "TOTAL CASE";
            }

        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

  

    #region Search
    protected void btnSearchInvoice_Click(object sender, EventArgs e)
    {
        try
        {
            BindTransporter();
            this.LoadInvoice();
            CreateDataTable();
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
        dt.Columns.Add(new DataColumn("SALEINVOICEID", typeof(string)));
        dt.Columns.Add(new DataColumn("LRGRNO", typeof(string)));
        dt.Columns.Add(new DataColumn("LRGRDATE", typeof(string)));
        dt.Columns.Add(new DataColumn("TRANSPORTERID", typeof(string)));
        dt.Columns.Add(new DataColumn("TRANSPORTERNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("TOTALCASE", typeof(string)));
        dt.Columns.Add(new DataColumn("VEHICHLENO", typeof(string)));

        HttpContext.Current.Session["SALEINVOICE"] = dt;


    }
    #endregion

    protected void btnsave_Click(object sender, EventArgs e)
    {
        clsSaleInvoice clsInvc = new clsSaleInvoice();
        int id = 0;
        if (HttpContext.Current.Session["SALEINVOICE"] == null)
        {
            CreateDataTable();
        }
        DataTable DT = (DataTable)HttpContext.Current.Session["SALEINVOICE"];
        int count = 0;
        string XML = string.Empty;

        string SALEINVOICEID = string.Empty;
        foreach (GridViewRow gvrow in gvUnlockInvoice.Rows)
        {
            TextBox txtlrgrno = (TextBox)gvrow.FindControl("txtlrgrno");
            TextBox txtvehichleno = (TextBox)gvrow.FindControl("txtvehichleno");
            TextBox txttotalcasepack = (TextBox)gvrow.FindControl("txttotalcasepack");

            Label lblSALEINVOICEID = (Label)gvrow.FindControl("lblSALEINVOICEID");
            TextBox txtlrgrdate = (TextBox)gvrow.FindControl("grdtxtlrgrdate");
            CheckBox chkBx = (CheckBox)gvrow.FindControl("chkSelect");

            DropDownList ddlTransporter = (DropDownList)gvrow.FindControl("ddlTransporter");
            string Transporterselectvalue = ddlTransporter.SelectedValue.Trim();
            string Transporterselectitem = ddlTransporter.SelectedItem.ToString().Trim();
            if (chkBx.Checked == true)
            {
                //if (txtlrgrno.Text != "" )
                //{
                    count = count + 1;
                    DataRow dr = DT.NewRow();
                    dr["SALEINVOICEID"] = lblSALEINVOICEID.Text.Trim();
                    dr["LRGRNO"] = txtlrgrno.Text.Trim();
                    dr["LRGRDATE"] = txtlrgrdate.Text.Trim();
                    dr["TRANSPORTERID"] = Transporterselectvalue.Trim();
                    dr["TRANSPORTERNAME"] = Transporterselectitem.Trim();

                    dr["TOTALCASE"] = txttotalcasepack.Text.Trim();
                    dr["VEHICHLENO"] = txtvehichleno.Text.Trim();



                    DT.Rows.Add(dr);
                    DT.AcceptChanges();
                //}
            }
        }
        HttpContext.Current.Session["SALEINVOICE"] = DT;

        if (count > 0)
        {
            XML = ConvertDatatable(DT);
            id = clsInvc.UpdateLrGrNo(this.ddlDepot.SelectedValue.Trim(), XML,this.ddlModule.SelectedValue.Trim());
            this.gvUnlockInvoice.DataSource = null;
            this.gvUnlockInvoice.DataBind();
        }
        if (count == 0)
        {
            MessageBox1.ShowInfo("Please Select at least 1 Record");
            
            return;
        }
        else
        {
            if (id > 0)
            {
                MessageBox1.ShowSuccess("Record saved successfully..");
                this.gvUnlockInvoice.DataSource = null;
                this.gvUnlockInvoice.DataBind();
                HttpContext.Current.Session["SALEINVOICE"] = null;
            }
            else
            {
                MessageBox1.ShowInfo("Error Saving Record ");
                HttpContext.Current.Session["SALEINVOICE"] = null;
                return;
            }
        }
    }


    public void BindTransporter()
    {
        clsSaleInvoice clsInvc = new clsSaleInvoice();
        DataTable dtTransporter = clsInvc.BindDepot_Transporter(this.ddlDepot.SelectedValue.Trim(),"");
        Session["Transporter"] = dtTransporter;
    }

    protected void gvUnlockInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           
            DropDownList DropDownList1 = (e.Row.FindControl("ddlTransporter") as DropDownList);
           
            clsSaleInvoice clsInvc = new clsSaleInvoice();
            //DataTable dtTransporter = clsInvc.BindDepot_Transporter(this.ddlDepot.SelectedValue.Trim());
            DataTable dt = (DataTable)Session["Transporter"];
            //DropDownList1.Items.Clear();
            //DropDownList1.Items.Add(new ListItem("Select Transporter", "0"));
            DropDownList1.AppendDataBoundItems = true;
            DropDownList1.DataSource = dt;
            DropDownList1.DataValueField = "ID";
            DropDownList1.DataTextField = "NAME";
            DropDownList1.DataBind();
            


            string lblSALEINVOICEID = (e.Row.FindControl("lblSALEINVOICEID") as Label).Text;
            string transpoerterid = clsInvc.BindTransporter(lblSALEINVOICEID, ddlModule.SelectedValue.Trim());
            DropDownList1.SelectedValue = Convert.ToString(transpoerterid.Trim());

            //DropDownList1.Items.FindByValue(country).Selected = true;

            //DropDownList1.DataTextField = "qualification";
            //DropDownList1.DataValueField = "qualification";
            //DropDownList1.DataBind();
            //DropDownList1.Items.Insert(0, new ListItem("--Select Qualification--", "0"))


        }

    }

    #region btnPrint_Click
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;

            Label grdlblid = gvr.Cells[1].Controls[0].FindControl("lblSALEINVOICEID") as Label;

            string upath = "";


            if (this.ddlModule.SelectedValue == "1") /* Sale invoice */
            {
                upath = "frmPrintPopUp.aspx?pid=" + grdlblid.Text + "&&BSID=" + this.ddlBusinessSegment.SelectedValue + "";
            }
            else if (this.ddlModule.SelectedValue == "3")  /* Purchase Recieved */
            {
                upath = "frmRptPurchasePrint.aspx?pid=" + grdlblid.Text + ""; /* Purchase Recieved */
            }
            else if (this.ddlModule.SelectedValue == "2")   /*  Stock Transfer */
            {
                upath = "frmPrintPopUp.aspx?Stnid=" + grdlblid.Text + "&&BSID=" + "1" + "&&pid=" + "1" + "&&MenuId=" + "46" + " ";
            }

            //else if (this.ddlModule.SelectedValue == "4")  /*  factory Despatch */
            //{
            //    upath = "frmRptPurchasePrint.aspx?pid=" + grdlblid.Text + "";

            //}
            //else if (this.ddlModule.SelectedValue == "5")  /*  sale return  */
            //{
            //    upath = "frmRptInvoicePrint.aspx?pid=" + grdlblid.Text +"";

            //}



            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=1000,height=600,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=100,top=100' );", true);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion
  
}