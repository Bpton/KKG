using System;
using System.Data;
using System.Web.UI;
using Microsoft.Reporting.WebForms;
using PPBLL;
//using BAL;

public partial class VIEW_frmRptPurchaseBillMM : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string MenuId = string.Empty;
                string invoiceid = "";
                DataTable dtheader = new DataTable();
                ClsStockReport clsrpt = new ClsStockReport();
                if (Request.QueryString["id"] != null)
                {
                    invoiceid = Request.QueryString["id"];
                    MenuId = Request.QueryString["MenuId"];
                }
                if (MenuId == "1636")/*Stock Receive From TPU Print in Invoice Format.*/
                {
                    dtheader = clsrpt.BindPurchaseReceived_Header(invoiceid.Trim());
                    if (dtheader.Rows[0]["INVOICE_TYPE"].ToString() == "1")
                    {
                        ShowReport_PurchaseReceived("PurchaseBill_IGST.rdlc", "", "", invoiceid);
                    }
                    if (dtheader.Rows[0]["INVOICE_TYPE"].ToString() == "2")
                    {
                        ShowReport_PurchaseReceived("PurchaseBill_GST.rdlc", "", "", invoiceid);
                    }
                }
                else if (MenuId == "2927")/*Stock Receive From Factory.*/
                {
                    dtheader = clsrpt.BindPurchaseReceived_Header(invoiceid.Trim());
                    if (dtheader.Rows[0]["INVOICE_TYPE"].ToString() == "0")
                    {
                        ShowReport_StockReceive_FromFactory("StockReceipt_FromFactory.rdlc", "", "", invoiceid);
                    }
                    if (dtheader.Rows[0]["INVOICE_TYPE"].ToString() == "1")
                    {
                        ShowReport_PurchaseReceived("PurchaseBill_IGST.rdlc", "", "", invoiceid);
                    }
                    if (dtheader.Rows[0]["INVOICE_TYPE"].ToString() == "2")
                    {
                        ShowReport_PurchaseReceived("PurchaseBill_GST.rdlc", "", "", invoiceid);
                    }
                }
                else if (MenuId == "2737" || MenuId == "2738")/*Import Purchase Print*/
                {
                    ShowReport_ImportPurchase("ImportPrint.rdlc", "", "", invoiceid);
                }
                else/*Stock Receive From TPU Print in Grn Format.*/
                {
                    ShowReport_PurchaseBillReceipt("PurchaseBIl_RecepitMM.rdlc", "", "", invoiceid);
                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #region ShowReport_StockReceipt
    private void ShowReport_PurchaseBillReceipt(string ReportFile, string spName, string rptType, string Voucherid)
    {
        ClsStockReport clsrpt = new ClsStockReport();
        DataTable dtheader = new DataTable();
        DataTable dtdetails = new DataTable();
        DataTable dt_Comp_info = new DataTable();

        dtheader = clsrpt.BindPurchaseHeader(Voucherid.Trim());
        dtdetails = clsrpt.BindPurchaseDetails(Voucherid.Trim());
        dt_Comp_info = clsrpt.Bind_CompanyInfo();

        this.ReportViewer1.LocalReport.EnableExternalImages = true;
        if (rptType == "") //Normal View
        {
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("PB_HEADER", dtheader));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("PB_DETAILS", dtdetails));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("PB_COMPANYINFO", dt_Comp_info));

            this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports_FAC/" + ReportFile);

            //ReportParameter rp1 = new ReportParameter("p_STNO", Voucherid.Trim());
            //ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1 });

            string format = "PDF",
                   devInfo = @"<DeviceInfo><Toolbar>True</Toolbar></DeviceInfo>";

            //out parameters

            string mimeType = "",
                encoding = "",
                fileNameExtn = "";
            string[] stearms = null;
            Microsoft.Reporting.WebForms.Warning[] warnings = null;

            byte[] result = null;
            //byte[] result_sub = null;

            try
            {
                //render report, it will returns bite array

                result = ReportViewer1.LocalReport.Render(format, devInfo, out mimeType, out encoding, out fileNameExtn, out stearms, out warnings);
                // result = ConcatBytes(result, result_sub);  //ConcatBytes concat two Byte[]

                // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.    
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "inline; filename= GRN_RECEIVED.pdf");
                Response.BinaryWrite(result); // create the file    
                Response.Flush();
            }
            catch (Exception ex)
            { }
        }
        else if (rptType == "PDF")  //PDF View
        {
            //this.ReportViewer1.LocalReport.Refresh();
        }
    }
    #endregion

    #region ShowReport_GT
    private void ShowReport_PurchaseReceived(string ReportFile, string spName, string rptType, string invoiceid)
    {
        DataTable dtdetails = new DataTable();
        DataSet dtheader = new DataSet();

        ClsStockReport clsrpt = new ClsStockReport();
        dtdetails = clsrpt.BindPurchaseReceived_Details(invoiceid.Trim(), "1");
        dtheader = clsrpt.Bind_PurchaseReceived_Print(invoiceid.Trim());

        this.ReportViewer1.LocalReport.EnableExternalImages = true;

        if (rptType == "") //Normal View
        {
            this.ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource();
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DETAILS", dtdetails));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HEADER", dtheader.Tables[0]));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("FOOTER", dtheader.Tables[1]));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DISTRIBUTOR_INFO", dtheader.Tables[3]));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("COMPANY_INFO", dtheader.Tables[2]));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("SALEINVOICE_FROMDEPOT", dtheader.Tables[4]));

            this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports_FAC/" + ReportFile);

            ReportParameter rp1 = new ReportParameter("p_INVOICEID", invoiceid.Trim());
            ReportParameter rp2 = new ReportParameter("p_InvoiceType_original", "1");
            ReportParameter rp3 = new ReportParameter("p_InvoiceType_duplicate", "2");
            ReportParameter rp4 = new ReportParameter("p_InvoiceType_triplicate", "3");
            ReportParameter rp5 = new ReportParameter("p_InvoiceType_backup", "4");
            ReportParameter rp6 = new ReportParameter("pcount", "1");
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1 });
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp2 });
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp3 });
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp4 });
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp5 });
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp6 });

            string format = "PDF",
                   devInfo = @"<DeviceInfo><Toolbar>True</Toolbar></DeviceInfo>";

            //out parameters

            string mimeType = "",
                encoding = "",
                fileNameExtn = "";
            string[] stearms = null;
            Microsoft.Reporting.WebForms.Warning[] warnings = null;

            byte[] result = null;
            //byte[] result_sub = null;

            try
            {
                //render report, it will returns bite array

                result = ReportViewer1.LocalReport.Render(format, devInfo, out mimeType, out encoding, out fileNameExtn, out stearms, out warnings);
                // result = ConcatBytes(result, result_sub);  //ConcatBytes concat two Byte[]
                // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
                string ReportName = string.Empty;
                ReportName = ReportFile.Replace(".rdlc", "").ToString() + "_" + dtheader.Tables[0].Rows[0]["SALEINVOICENO"].ToString();
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "inline; filename= " + ReportName + ".pdf");
                Response.BinaryWrite(result); // create the file    
                Response.Flush();
            }
            catch (Exception ex)
            {
            }
            ReportViewer1.ServerReport.Refresh();
        }
        else if (rptType == "PDF")  //PDF View
        {
            //this.ReportViewer1.LocalReport.Refresh();
        }
    }
    #endregion

    #region ShowReport_StockReceive_FromFactory
    private void ShowReport_StockReceive_FromFactory(string ReportFile, string spName, string rptType, string invoiceid)
    {
        DataTable dtdetails = new DataTable();
        DataTable dtheader = new DataTable();
        DataTable dttax = new DataTable();
        DataTable dtfooter = new DataTable();
        DataTable dt_qtyscheme = new DataTable();
        DataTable dt_pricescheme = new DataTable();
        DataTable dt_ItemWiseTax = new DataTable();
        DataTable dt_DistributorDetails = new DataTable();
        DataTable dt_Comp_info = new DataTable();
        DataTable dt_Terms = new DataTable();
        DataTable dt_TaxComponent = new DataTable();
        DataTable dt_Accounts = new DataTable();
        DataTable dt_InvoiceTax = new DataTable();

        ClsStockReport clsrpt = new ClsStockReport();
        dtdetails = clsrpt.BindPurchaseDetails(invoiceid.Trim());//BindPurchaseDetails_GST(invoiceid.Trim());
        dtheader = clsrpt.BindPurchaseHeader(invoiceid.Trim());
        dttax = clsrpt.BindPurchaseTax(invoiceid.Trim());
        dtfooter = clsrpt.BindPurchaseFooter(invoiceid.Trim());
        dt_Comp_info = clsrpt.Bind_CompanyInfo();

        this.ReportViewer1.LocalReport.EnableExternalImages = true;

        if (rptType == "") //Normal View
        {
            this.ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource();
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DETAILS", dtdetails));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HEADER", dtheader));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("FOOTER", dtfooter));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("ITEMWISE_TAX", dttax));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("COMP_INFO", dt_Comp_info));

            this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports_FAC/" + ReportFile);
            ReportParameter rp1 = new ReportParameter("p_STCKRID", invoiceid.Trim());
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1 });


            string format = "PDF",
                   devInfo = @"<DeviceInfo><Toolbar>True</Toolbar></DeviceInfo>";

            //out parameters

            string mimeType = "",
                encoding = "",
                fileNameExtn = "";
            string[] stearms = null;
            Microsoft.Reporting.WebForms.Warning[] warnings = null;

            byte[] result = null;
            //byte[] result_sub = null;

            try
            {
                //render report, it will returns bite array

                result = ReportViewer1.LocalReport.Render(format,
                    devInfo, out mimeType, out encoding,
                    out fileNameExtn, out stearms, out warnings);
                // result = ConcatBytes(result, result_sub);  //ConcatBytes concat two Byte[]
                // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.    
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "inline; filename= PurchaseBill.pdf");
                Response.BinaryWrite(result); // create the file    
                Response.Flush();
            }
            catch (Exception ex)
            {

            }
            ReportViewer1.ServerReport.Refresh();
        }
        else if (rptType == "PDF")  //PDF View
        {
            //this.ReportViewer1.LocalReport.Refresh();
        }
    }
    #endregion

    #region Import Purchse Print
    private void ShowReport_ImportPurchase(string ReportFile, string spName, string rptType, string Voucherid)
    {
        ClsInvoicePrint_FAC clsrpt = new ClsInvoicePrint_FAC();
        DataTable dtheader = new DataTable();
        DataTable dtdetails = new DataTable();
        DataTable dt_Transporter = new DataTable();

        dtheader = clsrpt.Bind_ImportPurchaseHeader(Voucherid.Trim());
        dtdetails = clsrpt.Bind_ImportPurchaseDetails(Voucherid.Trim());
        dt_Transporter = clsrpt.Bind_ImportPurchaseTransporter(Voucherid.Trim());

        this.ReportViewer1.LocalReport.EnableExternalImages = true;
        if (rptType == "") //Normal View
        {
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Import_Header", dtheader));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Import_Details", dtdetails));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Import_transporter", dt_Transporter));

            this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports_FAC/" + ReportFile);

            ReportParameter rp1 = new ReportParameter("p_INVOICEID", Voucherid.Trim());
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1 });

            string format = "PDF",
                   devInfo = @"<DeviceInfo><Toolbar>True</Toolbar></DeviceInfo>";

            //out parameters

            string mimeType = "",
                encoding = "",
                fileNameExtn = "";
            string[] stearms = null;
            Microsoft.Reporting.WebForms.Warning[] warnings = null;

            byte[] result = null;
            //byte[] result_sub = null;

            try
            {
                //render report, it will returns bite array

                result = ReportViewer1.LocalReport.Render(format, devInfo, out mimeType, out encoding, out fileNameExtn, out stearms, out warnings);
                // result = ConcatBytes(result, result_sub);  //ConcatBytes concat two Byte[]

                // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.    
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "inline; filename= ImportPurchase.pdf");
                Response.BinaryWrite(result); // create the file    
                Response.Flush();
            }
            catch (Exception ex)
            { }
        }
        else if (rptType == "PDF")  //PDF View
        {
            //this.ReportViewer1.LocalReport.Refresh();
        }
    }
    #endregion
}