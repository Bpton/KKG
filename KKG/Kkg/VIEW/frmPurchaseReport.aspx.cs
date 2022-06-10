using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Obout.Grid;
using System.Collections.Generic;
using System.Collections;
using System.Threading;
using iTextSharp.text;
using iTextSharp.text.pdf;

public partial class VIEW_frmPurchaseReport : System.Web.UI.Page
{
    ArrayList Arry = new ArrayList();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtInvoiceFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtInvoiceToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            ViewState["NetAmt"] = "0.00";
            ViewState["Excise"] = "0.00";            
        }
    }


    #region Create DataTable Structure
    public DataTable CreateDataTable()
    {
        DataTable dt = new DataTable();
        DataTable dtInner = new DataTable();
        dt.Clear();
        dt.Columns.Add(new DataColumn("STOCKRECEIVEDID", typeof(string)));
        dt.Columns.Add(new DataColumn("INVOICENO", typeof(string)));
        dt.Columns.Add(new DataColumn("INVOICEDATE", typeof(string)));
        dt.Columns.Add(new DataColumn("RECEIVEDSALENO", typeof(string)));
        dt.Columns.Add(new DataColumn("STOCKRECEIVEDDATE", typeof(string)));
        dt.Columns.Add(new DataColumn("STOCKDESPATCHID", typeof(string)));
        dt.Columns.Add(new DataColumn("STOCKDESPATCHDATE", typeof(string)));
        dt.Columns.Add(new DataColumn("VENDORNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("CODE", typeof(string)));
        dt.Columns.Add(new DataColumn("NETAMT", typeof(string)));
        #region Loop For Adding Itemwise Tax Component
        DataSet ds = new DataSet();

        ClsStockReport clsInvc = new ClsStockReport();
        ds = clsInvc.PurchaseReportERP(this.txtInvoiceFromDate.Text.Trim(), this.txtInvoiceToDate.Text.Trim(), HttpContext.Current.Session["DEPOTID"].ToString().Trim());
        Session["dtInvoiceTaxCount1"] = ds.Tables[1];

        for (int k = 0; k < ds.Tables[1].Rows.Count; k++)
        {
            
            dt.Columns.Add(new DataColumn("" + Convert.ToString(ds.Tables[1].Rows[k]["NAME"]) + "", typeof(string)));
        }
        #endregion
        
        HttpContext.Current.Session["SALEINVOICEDETAILS"] = dt;
        return dt;
    }
    #endregion

    #region CreateDataTableTaxComponent Structure
    public DataTable CreateDataTableTaxComponent()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add(new DataColumn("STOCKRECEIVEDID", typeof(string)));
        dt.Columns.Add(new DataColumn("TAXID", typeof(string)));
        dt.Columns.Add(new DataColumn("PERCENTAGE", typeof(string)));
        dt.Columns.Add(new DataColumn("TAXVALUE", typeof(string)));
        dt.Columns.Add(new DataColumn("NAME", typeof(string)));
        HttpContext.Current.Session["INVOICETAXCOMPONENTDETAILS"] = dt;

        return dt;
    }
    #endregion


    string parseValueIntoCurrency(double number)
    {
        // set currency format
        string curCulture = Thread.CurrentThread.CurrentCulture.ToString();
        System.Globalization.NumberFormatInfo currencyFormat = new
            System.Globalization.CultureInfo(curCulture).NumberFormat;

        currencyFormat.CurrencyNegativePattern = 1;

        return number.ToString("c", currencyFormat).Remove(0, 3).Trim();
    }

    protected void grdInvoiceReport_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {

           
            if (e.Row.RowType == GridRowType.DataRow)
            {
                if ((e.Row.Cells[8].Text) != "")
                {
                    ViewState["Excise"] = Convert.ToString(parseValueIntoCurrency(double.Parse(ViewState["Excise"].ToString()) + double.Parse(e.Row.Cells[8].Text)));
                }
                else
                {
                    e.Row.Cells[8].Text = "0";
                }
                ViewState["NetAmt"] = Convert.ToString(parseValueIntoCurrency(double.Parse(ViewState["NetAmt"].ToString()) + double.Parse(e.Row.Cells[7].Text)));
                               
                this.grdInvoiceReport.Columns[7].Align = "Right";
                this.grdInvoiceReport.Columns[8].Align = "Right";
                this.grdInvoiceReport.Columns[4].Align = "Right"; 
                this.grdInvoiceReport.Columns[5].Wrap = true;
                this.grdInvoiceReport.Columns[5].Width = "200px";
            }
            if (e.Row.RowType == GridRowType.ColumnFooter)
            {
                e.Row.Cells[6].Text = "Total (Rs.):";
                e.Row.Cells[6].ForeColor = System.Drawing.Color.Blue;
                e.Row.Cells[7].Text = ViewState["NetAmt"].ToString();
                e.Row.Cells[7].ForeColor = System.Drawing.Color.Blue;
                e.Row.Cells[8].Text = ViewState["Excise"].ToString();
                e.Row.Cells[8].ForeColor = System.Drawing.Color.Blue;
                ViewState["NetAmt"] = 0;
                ViewState["Excise"] = 0;
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

   
    #region btnShow_Click
    protected void btnShow_Click(object sender, EventArgs e)
    {
        ViewState["NetAmt"] = 0;
        ViewState["Excise"] = 0;

        this.CreateDataTable();
        this.CreateDataTableTaxComponent();
        DataSet ds = new DataSet();
        ClsStockReport clsInvc = new ClsStockReport();
        ds = clsInvc.PurchaseReportERP(this.txtInvoiceFromDate.Text.Trim(), this.txtInvoiceToDate.Text.Trim(), HttpContext.Current.Session["DEPOTID"].ToString().Trim());
        DataTable dtTaxComponentEdit = (DataTable)Session["INVOICETAXCOMPONENTDETAILS"];


        #region Item-wise Tax Component
        if (ds.Tables[2].Rows.Count > 0)
        {

            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
            {
                DataRow dr = dtTaxComponentEdit.NewRow();
                dr["TAXID"] = Convert.ToString(ds.Tables[2].Rows[i]["TAXID"]);
                dr["PERCENTAGE"] = Convert.ToString(ds.Tables[2].Rows[i]["PERCENTAGE"]);
                dr["TAXVALUE"] = Convert.ToString(ds.Tables[2].Rows[i]["TAXVALUE"]);
                dr["NAME"] = Convert.ToString(ds.Tables[2].Rows[i]["NAME"]);
                dtTaxComponentEdit.Rows.Add(dr);
                dtTaxComponentEdit.AcceptChanges();
            }

            HttpContext.Current.Session["INVOICETAXCOMPONENTDETAILS"] = dtTaxComponentEdit;
        }
        #endregion

        if (ds.Tables[0].Rows.Count > 0)
        {
            #region Loop For Adding Itemwise Tax Component
            DataTable dtTaxCountDataAddition = (DataTable)Session["dtInvoiceTaxCount1"];
            for (int k = 0; k < dtTaxCountDataAddition.Rows.Count; k++)
            {
                Arry.Add(dtTaxCountDataAddition.Rows[k]["NAME"].ToString());
            }
            #endregion

            DataTable dtInvoiceEdit = (DataTable)Session["SALEINVOICEDETAILS"];
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
               
                DataRow drEditInvoice = dtInvoiceEdit.NewRow();
                drEditInvoice["STOCKRECEIVEDID"] = Convert.ToString(ds.Tables[0].Rows[i]["STOCKRECEIVEDID"]);
                drEditInvoice["INVOICENO"] = Convert.ToString(ds.Tables[0].Rows[i]["INVOICENO"]);
                drEditInvoice["INVOICEDATE"] = Convert.ToString(ds.Tables[0].Rows[i]["INVOICEDATE"]);
                drEditInvoice["RECEIVEDSALENO"] = Convert.ToString(ds.Tables[0].Rows[i]["RECEIVEDSALENO"]);
                drEditInvoice["STOCKRECEIVEDDATE"] = Convert.ToString(ds.Tables[0].Rows[i]["STOCKRECEIVEDDATE"]);
                drEditInvoice["STOCKDESPATCHID"] = Convert.ToString(ds.Tables[0].Rows[i]["STOCKDESPATCHID"]);
                drEditInvoice["STOCKDESPATCHDATE"] = Convert.ToString(ds.Tables[0].Rows[i]["STOCKDESPATCHDATE"]);
                drEditInvoice["VENDORNAME"] = Convert.ToString(ds.Tables[0].Rows[i]["VENDORNAME"]);
                drEditInvoice["CODE"] = Convert.ToString(ds.Tables[0].Rows[i]["CODE"]);
                drEditInvoice["NETAMT"] = Convert.ToString(ds.Tables[0].Rows[i]["NETAMT"]);

                for (int j = 0; j < dtTaxCountDataAddition.Rows.Count; j++)
                {
                    for (int k = 0; k < ds.Tables[2].Rows.Count; k++)
                    {
                        if ((ds.Tables[0].Rows[i]["STOCKRECEIVEDID"].ToString() == ds.Tables[2].Rows[k]["STOCKRECEIVEDID"].ToString()) && Convert.ToString(dtTaxCountDataAddition.Rows[j]["TAXID"]).Trim() == Convert.ToString(ds.Tables[2].Rows[k]["TAXID"]).Trim())
                        {

                            drEditInvoice["" + Convert.ToString(dtTaxCountDataAddition.Rows[j]["NAME"]) + ""] = Convert.ToString(ds.Tables[2].Rows[k]["TAXVALUE"]);
                        }
                    }
                }

                dtInvoiceEdit.Rows.Add(drEditInvoice);
                dtInvoiceEdit.AcceptChanges();
            }

            HttpContext.Current.Session["SALEINVOICEDETAILS"] = dtInvoiceEdit;

            if (dtInvoiceEdit.Rows.Count > 0)
            {
                dtInvoiceEdit.Columns.Remove("STOCKRECEIVEDID");
                dtInvoiceEdit.Columns.Remove("STOCKDESPATCHID");
                this.grdInvoiceReport.DataSource = dtInvoiceEdit;
                this.grdInvoiceReport.DataBind();          
            }
            else
            {
                this.grdInvoiceReport.DataSource = null;
                this.grdInvoiceReport.DataBind();
            }
        }

    }
    #endregion

    #region gvStockreport_Exported
    protected void gvStockreport_Exported(object sender, Obout.Grid.GridExportEventArgs e)
    {
        foreach (TableRow item in e.Table.Rows)
        {
            int cellcount = item.Cells.Count;
            for (int i = 0; i < cellcount; i++)
            {
                item.Cells[i].Style.Add("border", "thin solid black");

            }

            //not using css.
            e.Table.GridLines = GridLines.None;
        }
    }
    #endregion

    #region btnPDFExport_Click
    protected void btnPDFExport_Click(object sender, EventArgs e)
    {
        // Export all pages
        grdInvoiceReport.PageSize = -1;
        grdInvoiceReport.DataBind();
        ExportGridToPDF();
    }
    #endregion

    #region ExportGridToPDF
    private void ExportGridToPDF()
    {
        // Stream which will be used to render the data
        MemoryStream fileStream = new MemoryStream();

        Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
        try
        {
            //Create Document class object and set its size to letter and give space left, right, Top, Bottom Margin
            PdfWriter wri = PdfWriter.GetInstance(doc, fileStream);

            doc.Open();//Open Document to write
            iTextSharp.text.
            Font font8 = iTextSharp.text.FontFactory.GetFont("ARIAL", 7);

            //Write some content
            Paragraph paragraph = new Paragraph("Expiry Stock Report As on " + DateTime.Now.ToString("dd/MM/yyyy"));
            paragraph.Alignment = Element.ALIGN_CENTER;

            //Craete instance of the pdf table and set the number of column in that table

            int visible_col = 0;
            foreach (Column col in grdInvoiceReport.Columns)
            {
                if (col.Visible)
                {
                    visible_col = visible_col + 1;

                }
            }

            PdfPTable PdfTable = new PdfPTable(visible_col);
            PdfPCell PdfPCell = null;

            //Add headers of the pdf table


            foreach (Column col in grdInvoiceReport.Columns)
            {
                if (col.Visible)
                {
                    PdfPCell = new PdfPCell(new Phrase(new Chunk(col.HeaderText, font8)));
                    PdfTable.AddCell(PdfPCell);

                }
            }


            //How add the data from the Grid to pdf table
            for (int i = 0; i < grdInvoiceReport.Rows.Count; i++)
            {
                Hashtable dataItem = grdInvoiceReport.Rows[i].ToHashtable();

                foreach (Column col in grdInvoiceReport.Columns)
                {
                    if (col.Visible)
                    {
                        if (dataItem[col.DataField] != null)
                        {
                            PdfPCell = new PdfPCell(new Phrase(new Chunk(dataItem[col.DataField].ToString(), font8)));
                            PdfTable.AddCell(PdfPCell);
                        }
                    }
                }
            }

            PdfTable.SpacingBefore = 15f;

            doc.Add(paragraph);
            doc.Add(PdfTable);
        }
        catch (DocumentException docEx)
        {
            //handle pdf document exception if any
        }
        catch (IOException ioEx)
        {
            // handle IO exception
        }
        catch (Exception ex)
        {
            // ahndle other exception if occurs
        }
        finally
        {
            //Close document and writer
            doc.Close();
        }

        // Send the data and the appropriate headers to the browser
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=StockReport.pdf");
        Response.ContentType = "application/pdf";
        Response.BinaryWrite(fileStream.ToArray());
        Response.End();
    }
    #endregion
    
}