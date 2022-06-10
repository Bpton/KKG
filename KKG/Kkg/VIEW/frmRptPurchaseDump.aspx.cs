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
public partial class VIEW_frmRptPurchaseDump : System.Web.UI.Page
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

            InisiliseDate();
        }
    }

    private void InisiliseDate()
    { 
        string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
        string startyear = finyear.Substring(0, 4);
        int startyear1 = Convert.ToInt32(startyear);
        string endyear = finyear.Substring(5);
        int endyear1 = Convert.ToInt32(endyear);
        DateTime oDate = new DateTime(startyear1, 04, 01);
        DateTime cDate = new DateTime(endyear1, 03, 31);
        CalendarExtender4.StartDate = oDate;
        CalendarExtender1.StartDate = oDate;      

        DateTime today1 = DateTime.Now;
        if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
        {
            this.txtInvoiceFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            this.txtInvoiceToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            CalendarExtender4.EndDate = today1;
            CalendarExtender1.EndDate = today1;

        }
        else
        {
            this.txtInvoiceFromDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
            this.txtInvoiceToDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
            CalendarExtender4.EndDate = cDate;
            CalendarExtender1.EndDate = cDate;
        }
        /* New code for date on 02/04/2019 End */
    }


   

    protected void grdInvoiceReport_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {


            if (e.Row.RowType == GridRowType.DataRow)
            {
         
                this.grdInvoiceReport.Columns[7].Align = "Right";
                this.grdInvoiceReport.Columns[8].Align = "Right";
                this.grdInvoiceReport.Columns[4].Align = "Right";
                this.grdInvoiceReport.Columns[5].Wrap = true;
                this.grdInvoiceReport.Columns[5].Width = "200px";
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

       
        DataTable dt = new DataTable();
        ClsStockReport clsInvc = new ClsStockReport();
        dt = clsInvc.BindPurchaseDump(this.txtInvoiceFromDate.Text.Trim(), this.txtInvoiceToDate.Text.Trim(), HttpContext.Current.Session["FINYEAR"].ToString().Trim());
         

            if (dt.Rows.Count > 0)
            {                
                this.grdInvoiceReport.DataSource = dt;
                this.grdInvoiceReport.DataBind();
            }
            else
            {
                this.grdInvoiceReport.DataSource = null;
                this.grdInvoiceReport.DataBind();
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
            Paragraph paragraph = new Paragraph("Purchase Dump Report As on " + DateTime.Now.ToString("dd/MM/yyyy"));
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
        Response.AddHeader("content-disposition", "attachment;filename=PurchaseDump.pdf");
        Response.ContentType = "application/pdf";
        Response.BinaryWrite(fileStream.ToArray());
        Response.End();
    }
    #endregion
}