using BAL;
using Microsoft.Reporting.WebForms;
//using PPBLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web;
using PPBLL;
using ZXing;
using System.Drawing;
using System.Drawing.Imaging;

public partial class VIEW_frmRptInvoicePrint_FAC : System.Web.UI.Page
{
    Hashtable ht = new Hashtable();
    Hashtable RptHt = new Hashtable();
    private int m_currentPageIndex;
    private IList<Stream> m_streams;
    BAL.ClsInvoicePrint_FAC objInvPrint = new BAL.ClsInvoicePrint_FAC();
    string ackno = string.Empty;
    string ackdt = string.Empty;
    string irnno = string.Empty;
    string qrcode = string.Empty;
    string imagePath = string.Empty;
    string _qrfile = string.Empty;

    #region Page_Init
    protected void Page_Init(object sender, EventArgs e)
    {
        ScriptManager.GetCurrent(Page).ScriptMode = ScriptMode.Release;
    }
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string invoiceid = "";
                string StnId = string.Empty;
                string AdvRecpt_ID = string.Empty;
                string Voucherid = string.Empty;
                string MenuId = string.Empty;
                DataTable dtheader = new DataTable();
                DataTable dtheader_SR = new DataTable();

                #region Despatch Print Section
                if (Request.QueryString["Stnid"] != null && Request.QueryString["MenuId"] != null)
                {
                    invoiceid = Request.QueryString["Stnid"];
                    string tag = Request.QueryString["TAG"];
                    MenuId = Request.QueryString["MenuId"];

                    if (MenuId == "45")
                    {
                        if (tag == "TB")
                        {
                            BillEntry_GST("TransporterBill_GST.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                        }
                    }
                    if (MenuId == "1600" || MenuId == "1650")
                    {
                        Job_order_fac("joborder.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                    }

                    if (MenuId == "46")
                    {
                        //dtheader = clsrpt.BindST_Header(invoiceid.Trim());
                        dtheader = objInvPrint.BindFactoryDespatch_Header(invoiceid.Trim());
                        if (dtheader.Rows[0]["INVOICE_TYPE"].ToString() == "0")
                        {
                            ShowReport_StockTransfer("BillofSupply.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                        }
                        if (dtheader.Rows[0]["INVOICE_TYPE"].ToString() == "1")
                        {
                            ShowReport_StockTransfer("StockDespatch_GST.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                        }
                        if (dtheader.Rows[0]["INVOICE_TYPE"].ToString() == "2")
                        {
                            ShowReport_StockTransfer("StockTransfer_GST.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                        }
                    }
                    else
                    {
                        dtheader_SR = objInvPrint.BindSR_Transfer(invoiceid.Trim());
                        if (dtheader_SR.Rows[0]["INVOICE_TYPE"].ToString() == "0")
                        {
                            ShowReport_StockReceipt("StockReceipt_.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                        }
                        if (dtheader_SR.Rows[0]["INVOICE_TYPE"].ToString() == "1")
                        {
                            ShowReport_StockReceipt("StockReceipt_IGST.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                        }
                        if (dtheader_SR.Rows[0]["INVOICE_TYPE"].ToString() == "2")
                        {
                            ShowReport_StockReceipt("StockReceipt_GST.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                        }
                    }
                }
                #endregion

                #region SaleReturn Print Section
                if (Request.QueryString["SLRID"] != null && Request.QueryString["BSID"] != null)
                {
                    invoiceid = Request.QueryString["SLRID"];
                    ShowReport_SaleReturn_GT("SaleReturn_IGST.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                }
                #endregion                

                #region Stock Journal Print Section
                if (Request.QueryString["StkJrnl_ID"] != null)
                {
                    invoiceid = Request.QueryString["StkJrnl_ID"];
                    ShowReport_StockJournal("Stock_Journal.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                }
                #endregion

                #region Purchase Order Print Section
                if (Request.QueryString["PurchaseOrderId"] != null && Request.QueryString["MenuId"] != null)
                {
                    invoiceid = Request.QueryString["PurchaseOrderId"];
                    string tag = Request.QueryString["TAG"];
                    MenuId = Request.QueryString["MenuId"];
                    if (MenuId == "29")
                    {
                        if (tag == "PO")
                        {
                            ShowReport_PO("rptPurchaseOrder.rdlc", "", ref ht, ref RptHt, invoiceid);
                        }
                    }
                }
                #endregion

                #region NRGP Print Section
                if (Request.QueryString["NRGPIvoicveid"] != null && Request.QueryString["MenuId"] != null)
                {
                    invoiceid = Request.QueryString["NRGPIvoicveid"];
                    string tag = Request.QueryString["TAG"];
                    MenuId = Request.QueryString["MenuId"];
                    if (MenuId == "144")
                    {
                        if (tag == "NRGP")
                        {
                            ShowReport_NRGP("RptNRGPInvoice.rdlc", "", ref ht, ref RptHt, invoiceid);
                        }
                        else if (tag == "RGP")
                        {
                            ShowReport_NRGP("RptRGPInvoice.rdlc", "", ref ht, ref RptHt, invoiceid);
                        }
                    }
                }
                #endregion

                #region Requisition Print Section
                if (Request.QueryString["REQSIvoicveid"] != null && Request.QueryString["MenuId"] != null)
                {
                    invoiceid = Request.QueryString["REQSIvoicveid"];
                    string tag = Request.QueryString["TAG"];
                    MenuId = Request.QueryString["MenuId"];
                    if (MenuId == "518")
                    {
                        if (tag == "REQS")
                        {
                            ShowReport_Requisition("RptREQSInvoice.rdlc", "", ref ht, ref RptHt, invoiceid);
                        }
                    }
                }
                #endregion

                #region Grn Rejection Print Section
                if (Request.QueryString["Ivoicveid"] != null)
                {
                    invoiceid = Request.QueryString["Ivoicveid"];
                    ShowReport_GRNREJECTION("RptGrnRejection.rdlc", "", ref ht, ref RptHt, invoiceid);
                }
                #endregion

                #region Issue Print Section
                if (Request.QueryString["ISSUEIvoicveid"] != null && Request.QueryString["MenuId"] != null)
                {
                    invoiceid = Request.QueryString["ISSUEIvoicveid"];
                    string tag = Request.QueryString["TAG"];
                    MenuId = Request.QueryString["MenuId"];
                    if (MenuId == "519")
                    {
                        if (tag == "ISS")
                        {
                            ShowReport_Issue("RptIssueInvoice.rdlc", "", ref ht, ref RptHt, invoiceid);
                        }
                    }
                }
                #endregion

                if (Request.QueryString["pid"] != null)
                {
                    invoiceid = Request.QueryString["pid"];
                    dtheader = objInvPrint.Bind_InvoiceType(invoiceid.Trim());
                    DataTable dtreturn = new DataTable();
                    dtreturn = objInvPrint.Bind_Returntype(invoiceid.Trim());

                    if (dtreturn.Rows.Count > 0)
                    {
                        if (dtreturn.Rows[0]["INVOICETYPE"].ToString() == "2")
                        {
                            Gross_return_gst("Groosreturn_GST.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                        }
                        else
                        {
                            Gross_return_gst("Groosreturn_IGST.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                        }
                    }
                    string BSID = "";
                    if (Request.QueryString["BSID"] != null)
                    {
                        BSID = Request.QueryString["BSID"];
                    }
                    string PSID = "";
                    if (Request.QueryString["PSID"] != null)
                    {
                        PSID = Request.QueryString["PSID"];
                    }

                    else if (Request.QueryString["PSID"] == null)
                    {
                        PSID = "PURCHASERETURN";
                    }
                    #region Export Print Section
                    if (Request.QueryString["BSID"].ToString().Trim() == "2E96A0A4-6256-472C-BE4F-C59599C948B0")
                    {
                        if (PSID == "EXPORT")
                        {
                            ShowReport_EXPORT("ExportInvoice.rdlc", "", ref ht, ref RptHt, "", invoiceid);    //For EXPORT
                        }
                        if (PSID == "PROFORMA")
                        {
                            ShowReport_Proforma("ProformaInvoice.rdlc", "", ref ht, ref RptHt, "", invoiceid);  //For PROFORMA
                        }
                        if (PSID == "PACKING")
                        {
                            ShowReport_PackingList("PackingList.rdlc", "", ref ht, ref RptHt, "", invoiceid);  //For PACKING LIST
                        }
                        if (PSID == "EXPORTGST")
                        {
                            ShowReport_EXPORT_GST("ExportInvoice_GST.rdlc", "", ref ht, ref RptHt, "", invoiceid);    //For EXPORTGST
                        }
                    }
                    #endregion

                    if (dtheader.Rows[0]["INVOICETYPEID"].ToString() == "0")
                    {
                        if (Request.QueryString["BSID"].ToString().Trim() == "7F62F951-9D1F-4B8D-803B-74EEBA468CEE" || Request.QueryString["BSID"].ToString().Trim() == "0AA9353F-D350-4380-BC84-6ED5D0031E24" || Request.QueryString["BSID"].ToString().Trim() == "A18FE04D-057C-4A88-9204-5FE1613626C9")
                        {
                            ShowReport_GT("DeliveryChallan.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For GT AND MT
                        }
                        else if (Request.QueryString["BSID"].ToString().Trim() == "GTMT" && PSID == "SALERETURN_GT_MT")
                        {
                            ShowReport_SaleReturn_GT("SaleReturn_Challan.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                        }
                    }

                    if (dtheader.Rows[0]["INVOICETYPEID"].ToString() == "9")
                    {
                        if (Request.QueryString["BSID"].ToString().Trim() == "7F62F951-9D1F-4B8D-803B-74EEBA468CEE" || Request.QueryString["BSID"].ToString().Trim() == "0AA9353F-D350-4380-BC84-6ED5D0031E24" || Request.QueryString["BSID"].ToString().Trim() == "A18FE04D-057C-4A88-9204-5FE1613626C9")
                        {
                            ShowReport_GT("BillOfSupply_IGST.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For GT AND MT
                        }
                        else if (Request.QueryString["BSID"].ToString().Trim() == "GTMT" && PSID == "SALERETURN_GT_MT")
                        {
                            ShowReport_SaleReturn_GT("SaleReturn_Challan.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                        }
                    }
                    if (dtheader.Rows[0]["INVOICETYPEID"].ToString() == "1")
                    {
                        if (Request.QueryString["BSID"].ToString().Trim() == "7F62F951-9D1F-4B8D-803B-74EEBA468CEE" || Request.QueryString["BSID"].ToString().Trim() == "0AA9353F-D350-4380-BC84-6ED5D0031E24" || Request.QueryString["BSID"].ToString().Trim() == "A18FE04D-057C-4A88-9204-5FE1613626C9" || Request.QueryString["BSID"].ToString().Trim() == "33F6AC5E-1F37-4B0F-B959-D1C900BB43A5"|| Request.QueryString["BSID"].ToString().Trim() == "0A300260-A96F-4A92-BBFE-4E48AA296BFB")
                        {
                            ShowReport_GT1("SaleInvoice_IGST_QR.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For GT AND MT
                        }
                        else if (Request.QueryString["BSID"].ToString().Trim() == "GTMT" && PSID == "SALERETURN_GT_MT")
                        {
                            //ShowReport_SaleReturn_GT("SaleReturn.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For GT AND MT
                            ShowReport_GT("SaleInvoice_IGST.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                        }
                        else if (Request.QueryString["BSID"].ToString().Trim() == "GTMT" && PSID == "PURCHASERETURN")
                        {
                            //ShowReport_SaleReturn_GT("SaleReturn.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For GT AND MT
                            ShowReport_PurchaseReturn("PurchaseReturn_IGST_V1.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                        }
                        else if (Request.QueryString["BSID"].ToString().Trim() == "62240D9B-F7FC-4259-AECE-B15D1018A972")
                        {
                            ShowReport_Corporate("SaleInvoice_Corporate_IGST.rdlc", "", ref ht, ref RptHt, "", invoiceid);    //For CORPORATE
                        }
                    }
                    if (dtheader.Rows[0]["INVOICETYPEID"].ToString() == "2")
                    {
                        if (Request.QueryString["BSID"].ToString().Trim() == "7F62F951-9D1F-4B8D-803B-74EEBA468CEE" || Request.QueryString["BSID"].ToString().Trim() == "0AA9353F-D350-4380-BC84-6ED5D0031E24" || Request.QueryString["BSID"].ToString().Trim() == "A18FE04D-057C-4A88-9204-5FE1613626C9" || Request.QueryString["BSID"].ToString().Trim() == "33F6AC5E-1F37-4B0F-B959-D1C900BB43A5"|| Request.QueryString["BSID"].ToString().Trim() == "0A300260-A96F-4A92-BBFE-4E48AA296BFB")
                        {
                            ShowReport_GT("SaleInvoice_GST.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For GT AND MT
                        }
                        else if (Request.QueryString["BSID"].ToString().Trim() == "GTMT" && PSID == "PURCHASERETURN")
                        {
                            //ShowReport_SaleReturn_GT("SaleReturn_GST.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For GT AND MT
                            ShowReport_PurchaseReturn("PurchaseReturn_GST_V1.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For GT AND MT
                        }
                    }

                    if (dtheader.Rows[0]["INVOICETYPEID"].ToString() == "3")
                    {
                        if (Request.QueryString["BSID"].ToString().Trim() == "7F62F951-9D1F-4B8D-803B-74EEBA468CEE" || Request.QueryString["BSID"].ToString().Trim() == "0AA9353F-D350-4380-BC84-6ED5D0031E24" || Request.QueryString["BSID"].ToString().Trim() == "A18FE04D-057C-4A88-9204-5FE1613626C9" || Request.QueryString["BSID"].ToString().Trim() == "33F6AC5E-1F37-4B0F-B959-D1C900BB43A5")
                        {
                            ShowReport_GT("SaleInvoice_GST.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For GT AND MT
                        }
                        else if (Request.QueryString["BSID"].ToString().Trim() == "GTMT" && PSID == "SALERETURN_GT_MT")
                        {
                            ShowReport_SaleReturn_GT("SaleReturn_GST.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For GT AND MT
                        }
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
    #endregion    

    #region ShowReport_GT
    private void ShowReport_GT(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string invoiceid)
    {
        DataTable dtdetails = new DataTable();
        DataSet dtheader = new DataSet();
        BAL.ClsInvoicePrint_FAC objInvPrint = new BAL.ClsInvoicePrint_FAC();
        dtdetails = objInvPrint.Bind_InvoiceDetails_GST_Print(invoiceid.Trim(), Request.QueryString["original"].ToString(), Request.QueryString["duplicate"].ToString(), Request.QueryString["triplicate"].ToString(), Request.QueryString["backpsheet"].ToString());
        dtheader = objInvPrint.Bind_InvoiceHeader_Print(invoiceid.Trim());
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
            ReportParameter rp2 = new ReportParameter("p_InvoiceType_original", Request.QueryString["original"].ToString());
            ReportParameter rp3 = new ReportParameter("p_InvoiceType_duplicate", Request.QueryString["duplicate"].ToString());
            ReportParameter rp4 = new ReportParameter("p_InvoiceType_triplicate", Request.QueryString["triplicate"].ToString());
            ReportParameter rp5 = new ReportParameter("p_InvoiceType_backup", Request.QueryString["backpsheet"].ToString());
            ReportParameter rp6 = new ReportParameter("pcount", Request.QueryString["pcount"].ToString());
          
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

                result = ReportViewer1.LocalReport.Render(format,
                    devInfo, out mimeType, out encoding,
                    out fileNameExtn, out stearms, out warnings);
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
    #region ShowReport_GT
    private void ShowReport_GT1(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string invoiceid)
    {
        DataTable dtdetails = new DataTable();
        DataSet dtheader = new DataSet();
        BAL.ClsInvoicePrint_FAC objInvPrint = new BAL.ClsInvoicePrint_FAC();
        dtdetails = objInvPrint.Bind_InvoiceDetails_GST_Print(invoiceid.Trim(), Request.QueryString["original"].ToString(), Request.QueryString["duplicate"].ToString(), Request.QueryString["triplicate"].ToString(), Request.QueryString["backpsheet"].ToString());
        dtheader = objInvPrint.Bind_InvoiceHeader_Print(invoiceid.Trim());
        this.ReportViewer1.LocalReport.EnableExternalImages = true;

        ackno = Convert.ToString(dtheader.Tables[0].Rows[0]["AckNo"]);
        if (ackno == "")
        {
            ackno = "0";

        }
        irnno = Convert.ToString(dtheader.Tables[0].Rows[0]["Irn"]);
        if (ackdt == "")
        {
            ackdt = "01/01/1900";
        }
        ackdt = Convert.ToString(dtheader.Tables[0].Rows[0]["AckDt"]);
        if (ackdt == "")
        {
            ackdt = "01/01/1900";
        }
        string qrcode = Convert.ToString(dtheader.Tables[0].Rows[0]["Qrcode"]);
        if (ackno != "0")
        {
            _qrfile = GenerateCode(qrcode, ackno);
            imagePath = new Uri(Server.MapPath("~/FileUpload/" + _qrfile)).AbsoluteUri;
        }
        else
        {
            imagePath = new Uri(Server.MapPath("~/FileUpload/QRImage.jpg")).AbsoluteUri;
        }

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
            ReportParameter rp2 = new ReportParameter("p_InvoiceType_original", Request.QueryString["original"].ToString());
            ReportParameter rp3 = new ReportParameter("p_InvoiceType_duplicate", Request.QueryString["duplicate"].ToString());
            ReportParameter rp4 = new ReportParameter("p_InvoiceType_triplicate", Request.QueryString["triplicate"].ToString());
            ReportParameter rp5 = new ReportParameter("p_InvoiceType_backup", Request.QueryString["backpsheet"].ToString());
            ReportParameter rp6 = new ReportParameter("pcount", Request.QueryString["pcount"].ToString());
            ReportParameter parameter = new ReportParameter("ImagePath", imagePath);
            ReportParameter rp7 = new ReportParameter("Ackno", ackno);
            ReportParameter rp8 = new ReportParameter("Ackdt", ackdt);

            ReportViewer1.LocalReport.SetParameters(parameter);
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1 });
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp2 });
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp3 });
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp4 });
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp5 });
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp6 });
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp7 });
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp8 });

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

    #region ShowReport_StockTransfer
    private void ShowReport_StockTransfer(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string Invoiceid)
    {
        DataTable dtdetails = new DataTable();
        DataTable dt_Comp_info = new DataTable();
        BAL.ClsInvoicePrint_FAC objInvPrint = new BAL.ClsInvoicePrint_FAC();
        dtdetails = objInvPrint.BindST_Details_GST(Invoiceid.Trim(), Request.QueryString["original"].ToString(), Request.QueryString["duplicate"].ToString(), Request.QueryString["triplicate"].ToString(), Request.QueryString["backpsheet"].ToString());
        dt_Comp_info = objInvPrint.Bind_CompanyInfo();

        /********************New Method******************/
        DataSet dsheaderprint = new DataSet();
        dsheaderprint = objInvPrint.Bind_StockTransferHeader_Print(Invoiceid.Trim());

        /************************************************/

        this.ReportViewer1.LocalReport.EnableExternalImages = true;
        if (rptType == "") //Normal View
        {
            this.ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource();
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("ST_DETAILS", dtdetails));//
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("ST_HEADER", dsheaderprint.Tables[0]));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("ST_FOOTER", dsheaderprint.Tables[1]));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("ST_FROM_DEPOT", dsheaderprint.Tables[2]));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("ST_TO_DEPOT", dsheaderprint.Tables[3]));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("COMP_INFO", dt_Comp_info));

            this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports_FAC/" + ReportFile);

            ReportParameter rp1 = new ReportParameter("p_STNO", Invoiceid.Trim());
            ReportParameter rp2 = new ReportParameter("p_orginal", Request.QueryString["original"].ToString());
            ReportParameter rp3 = new ReportParameter("p_duplicate", Request.QueryString["duplicate"].ToString());
            ReportParameter rp4 = new ReportParameter("p_triplicate", Request.QueryString["triplicate"].ToString());
            ReportParameter rp5 = new ReportParameter("p_extra", Request.QueryString["backpsheet"].ToString());
            ReportParameter rp6 = new ReportParameter("pcount", Request.QueryString["pcount"].ToString());
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1 });
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp2 });
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp3 });
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp4 });
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp5 });
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp6 });

            string format = "PDF",
                   devInfo = @"<DeviceInfo><Toolbar>True</Toolbar></DeviceInfo>";

            //out parameters

            string mimeType = "", encoding = "", fileNameExtn = "";
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
                string ReportName = string.Empty;
                ReportName = ReportFile.Replace(".rdlc", "").ToString() + "_" + dsheaderprint.Tables[0].Rows[0]["STOCKTRANSFERNO"].ToString();
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "inline; filename= " + ReportName + ".pdf");
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

    #region ShowReport_StockReceipt
    private void ShowReport_StockReceipt(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string Voucherid)
    {
        //DataSet dsdetails = new DataSet();
        BAL.ClsInvoicePrint_FAC clsrpt = new BAL.ClsInvoicePrint_FAC();
        DataTable dtTransfer = new DataTable();
        DataTable dtTransfer_Footer = new DataTable();
        DataTable dtItems = new DataTable();
        DataTable dtRecvdDepo = new DataTable();
        DataTable dtSendDepo = new DataTable();
        DataTable dt_Comp_info = new DataTable();

        dtTransfer = clsrpt.BindSR_Transfer(Voucherid.Trim());
        dtTransfer_Footer = clsrpt.BindSR_FOOTER_GST(Voucherid.Trim());
        dtItems = clsrpt.BindSR_Items_GST(Voucherid.Trim());
        dtRecvdDepo = clsrpt.BindSR_RecvdDepo(Voucherid.Trim());
        dtSendDepo = clsrpt.BindSR_SendDepo(Voucherid.Trim());
        dt_Comp_info = clsrpt.Bind_CompanyInfo();

        //dsdetails = clsrpt.BindSR_DetailsDS(ddlStockTransferNo.SelectedValue.ToString().Trim());

        this.ReportViewer1.LocalReport.EnableExternalImages = true;
        if (rptType == "") //Normal View
        {
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("SR_TRANSFER", dtTransfer));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("SR_FOOTER", dtTransfer_Footer));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("SR_ITEMS", dtItems));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("SR_RECEVIEDDEPO", dtRecvdDepo));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("SR_SENDDEPO", dtSendDepo));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("SR_COMPANYINFO", dt_Comp_info));

            this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports/" + ReportFile);

            ReportParameter rp1 = new ReportParameter("p_STNO", Voucherid.Trim());
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
                Response.AddHeader("content-disposition", "inline; filename= Voucher.pdf");
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

    #region ShowReport_SaleReturn_GT
    private void ShowReport_SaleReturn_GT(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string invoiceid)
    {
        //DataTable dtdetails = new DataTable();
        //DataTable dtheader = new DataTable();
        //DataTable dtfooter = new DataTable();
        //DataTable dt_pricescheme = new DataTable();
        //DataTable dttax = new DataTable();
        //DataTable dt_DistributorDetails = new DataTable();
        //DataTable dt_Comp_info = new DataTable();

        //ClsStockReport clsrpt = new ClsStockReport();

        //dtdetails = clsrpt.Bind_InvoiceDetails_SaleReturn(invoiceid.Trim());
        //dtheader = clsrpt.Bind_InvoiceHeader_SaleReturn(invoiceid.Trim());
        //dttax = clsrpt.Bind_InvoiceTax_SaleReturn(invoiceid.Trim());
        //dtfooter = clsrpt.Bind_InvoiceFooter_SaleReturn(invoiceid.Trim());
        //dt_pricescheme = clsrpt.Bind_InvoicePriceScheme_SaleReturn(invoiceid.Trim());
        //dt_DistributorDetails = clsrpt.Bind_InvoiceDistributor_SaleReturn(invoiceid.Trim());
        //dt_Comp_info = clsrpt.Bind_CompanyInfo();


        //this.ReportViewer1.LocalReport.EnableExternalImages = true;

        //if (rptType == "") //Normal View
        //{
        //    this.ReportViewer1.LocalReport.DataSources.Clear();
        //    ReportDataSource rds = new ReportDataSource();
        //    this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DETAILS", dtdetails));
        //    this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HEADER", dtheader));
        //    this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("FOOTER", dtfooter));
        //    this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("ITEMWISE_TAX", dttax));
        //    this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("PRICE_SCHEME", dt_pricescheme));
        //    this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DISTRIBUTOR_INFO", dt_DistributorDetails));
        //    this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("COMPANY_INFO", dt_Comp_info));


        //    this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports/" + ReportFile);


        //    ReportParameter rp1 = new ReportParameter("P_SALERETURNID", invoiceid.Trim());
        //    ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1 });


        //    string format = "PDF",
        //           devInfo = @"<DeviceInfo><Toolbar>True</Toolbar></DeviceInfo>";

        //    //out parameters

        //    string mimeType = "",
        //        encoding = "",
        //        fileNameExtn = "";
        //    string[] stearms = null;
        //    Microsoft.Reporting.WebForms.Warning[] warnings = null;

        //    byte[] result = null;
        //    //byte[] result_sub = null;

        //    try
        //    {
        //        //render report, it will returns bite array

        //        result = ReportViewer1.LocalReport.Render(format,
        //            devInfo, out mimeType, out encoding,
        //            out fileNameExtn, out stearms, out warnings);
        //        // result = ConcatBytes(result, result_sub);  //ConcatBytes concat two Byte[]


        //        // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.    
        //        Response.Buffer = true;
        //        Response.Clear();
        //        Response.ContentType = mimeType;
        //        Response.AddHeader("content-disposition", "inline; filename= SaleReturn.pdf");
        //        Response.BinaryWrite(result); // create the file    
        //        Response.Flush();
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    ReportViewer1.ServerReport.Refresh();
        //}

        //else if (rptType == "PDF")  //PDF View
        //{

        //    //this.ReportViewer1.LocalReport.Refresh();
        //}
    }
    #endregion

    #region ShowReport_PurchaseBill
    private void ShowReport_PurchaseBill(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string invoiceid)
    {

        //DataTable dtdetails = new DataTable();
        //DataTable dtheader = new DataTable();
        //DataTable dttax = new DataTable();
        //DataTable dtfooter = new DataTable();
        //DataTable dt_qtyscheme = new DataTable();
        //DataTable dt_pricescheme = new DataTable();
        //DataTable dt_ItemWiseTax = new DataTable();
        //DataTable dt_DistributorDetails = new DataTable();
        //DataTable dt_Comp_info = new DataTable();
        //DataTable dt_Terms = new DataTable();
        //DataTable dt_TaxComponent = new DataTable();
        //DataTable dt_Accounts = new DataTable();
        //DataTable dt_InvoiceTax = new DataTable();

        //ClsStockReport clsrpt = new ClsStockReport();
        //dtdetails = clsrpt.BindPurchaseDetails_GST(invoiceid.Trim());
        //dtheader = clsrpt.BindPurchaseHeader(invoiceid.Trim());
        //dttax = clsrpt.BindPurchaseTax(invoiceid.Trim());
        //dtfooter = clsrpt.BindPurchaseFooter(invoiceid.Trim());
        //dt_Comp_info = clsrpt.Bind_CompanyInfo();

        //this.ReportViewer1.LocalReport.EnableExternalImages = true;

        //if (rptType == "") //Normal View
        //{
        //    this.ReportViewer1.LocalReport.DataSources.Clear();
        //    ReportDataSource rds = new ReportDataSource();
        //    this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DETAILS", dtdetails));
        //    this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HEADER", dtheader));
        //    this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("FOOTER", dtfooter));
        //    this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("ITEMWISE_TAX", dttax));
        //    this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("COMP_INFO", dt_Comp_info));

        //    this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports/" + ReportFile);


        //    ReportParameter rp1 = new ReportParameter("p_STCKRID", invoiceid.Trim());
        //    ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1 });


        //    string format = "PDF",
        //           devInfo = @"<DeviceInfo><Toolbar>True</Toolbar></DeviceInfo>";

        //    //out parameters

        //    string mimeType = "",
        //        encoding = "",
        //        fileNameExtn = "";
        //    string[] stearms = null;
        //    Microsoft.Reporting.WebForms.Warning[] warnings = null;

        //    byte[] result = null;
        //    //byte[] result_sub = null;

        //    try
        //    {
        //        //render report, it will returns bite array

        //        result = ReportViewer1.LocalReport.Render(format,
        //            devInfo, out mimeType, out encoding,
        //            out fileNameExtn, out stearms, out warnings);
        //        // result = ConcatBytes(result, result_sub);  //ConcatBytes concat two Byte[]


        //        // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.    
        //        Response.Buffer = true;
        //        Response.Clear();
        //        Response.ContentType = mimeType;
        //        Response.AddHeader("content-disposition", "inline; filename= PurchaseBill.pdf");
        //        Response.BinaryWrite(result); // create the file    
        //        Response.Flush();
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    ReportViewer1.ServerReport.Refresh();
        //}

        //else if (rptType == "PDF")  //PDF View
        //{

        //    //this.ReportViewer1.LocalReport.Refresh();
        //}
    }
    #endregion

    #region ShowReport_PurchaseBill
    private void ShowReport_PurchaseBill_VAT(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string invoiceid)
    {

        //DataTable dtdetails = new DataTable();
        //DataTable dtheader = new DataTable();
        //DataTable dttax = new DataTable();
        //DataTable dtfooter = new DataTable();
        //DataTable dt_qtyscheme = new DataTable();
        //DataTable dt_pricescheme = new DataTable();
        //DataTable dt_ItemWiseTax = new DataTable();
        //DataTable dt_DistributorDetails = new DataTable();
        //DataTable dt_Comp_info = new DataTable();
        //DataTable dt_Terms = new DataTable();
        //DataTable dt_TaxComponent = new DataTable();
        //DataTable dt_Accounts = new DataTable();
        //DataTable dt_InvoiceTax = new DataTable();

        //ClsStockReport clsrpt = new ClsStockReport();
        //dtdetails = clsrpt.BindPurchaseDetails(invoiceid.Trim());
        //dtheader = clsrpt.BindPurchaseHeader(invoiceid.Trim());
        //dttax = clsrpt.BindPurchaseTax(invoiceid.Trim());
        //dtfooter = clsrpt.BindPurchaseFooter(invoiceid.Trim());
        //dt_Comp_info = clsrpt.Bind_CompanyInfo();

        //this.ReportViewer1.LocalReport.EnableExternalImages = true;

        //if (rptType == "") //Normal View
        //{
        //    this.ReportViewer1.LocalReport.DataSources.Clear();
        //    ReportDataSource rds = new ReportDataSource();
        //    this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DETAILS", dtdetails));
        //    this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HEADER", dtheader));
        //    this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("FOOTER", dtfooter));
        //    this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("ITEMWISE_TAX", dttax));
        //    this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("COMP_INFO", dt_Comp_info));

        //    this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports/" + ReportFile);


        //    ReportParameter rp1 = new ReportParameter("p_STCKRID", invoiceid.Trim());
        //    ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1 });


        //    string format = "PDF",
        //           devInfo = @"<DeviceInfo><Toolbar>True</Toolbar></DeviceInfo>";

        //    //out parameters

        //    string mimeType = "",
        //        encoding = "",
        //        fileNameExtn = "";
        //    string[] stearms = null;
        //    Microsoft.Reporting.WebForms.Warning[] warnings = null;

        //    byte[] result = null;
        //    //byte[] result_sub = null;

        //    try
        //    {
        //        //render report, it will returns bite array

        //        result = ReportViewer1.LocalReport.Render(format,
        //            devInfo, out mimeType, out encoding,
        //            out fileNameExtn, out stearms, out warnings);
        //        // result = ConcatBytes(result, result_sub);  //ConcatBytes concat two Byte[]


        //        // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.    
        //        Response.Buffer = true;
        //        Response.Clear();
        //        Response.ContentType = mimeType;
        //        Response.AddHeader("content-disposition", "inline; filename= PurchaseBill.pdf");
        //        Response.BinaryWrite(result); // create the file    
        //        Response.Flush();
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    ReportViewer1.ServerReport.Refresh();
        //}

        //else if (rptType == "PDF")  //PDF View
        //{

        //    //this.ReportViewer1.LocalReport.Refresh();
        //}
    }
    #endregion

    #region ShowReport_Purchase Return
    private void ShowReport_PurchaseReturn(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string invoiceid)
    {
        DataTable dtdetails = new DataTable();
        DataTable dtheader = new DataTable();
        DataTable dtfooter = new DataTable();
        DataTable dt_pricescheme = new DataTable();
        DataTable dttax = new DataTable();
        DataTable dt_DistributorDetails = new DataTable();
        DataTable dt_Comp_info = new DataTable();
        BAL.ClsInvoicePrint_FAC clsrpt = new BAL.ClsInvoicePrint_FAC();
        dtdetails = clsrpt.Bind_InvoiceDetails_PurchaseReturn(invoiceid.Trim(), Request.QueryString["original"].ToString(), Request.QueryString["duplicate"].ToString(), Request.QueryString["triplicate"].ToString(), Request.QueryString["backpsheet"].ToString());
        dtheader = clsrpt.Bind_InvoiceHeader_PurchaseReturn(invoiceid.Trim());
        dttax = clsrpt.Bind_InvoiceTax_PurchaseReturn(invoiceid.Trim());
        dtfooter = clsrpt.Bind_InvoiceFooter_PurchaseReturn(invoiceid.Trim());
        //dt_pricescheme = clsrpt.Bind_InvoicePriceScheme_SaleReturn(invoiceid.Trim());
        dt_DistributorDetails = clsrpt.Bind_InvoiceVendor_PurchaseReturn(invoiceid.Trim());
        dt_Comp_info = clsrpt.Bind_CompanyInfo();

        this.ReportViewer1.LocalReport.EnableExternalImages = true;
        this.ReportViewer1.LocalReport.DataSources.Clear();
        if (rptType == "") //Normal View
        {
            this.ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource();
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DETAILS", dtdetails));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HEADER", dtheader));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("FOOTER", dtfooter));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("ITEMWISE_TAX", dttax));
            //this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("PRICE_SCHEME", dt_pricescheme));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DISTRIBUTOR_INFO", dt_DistributorDetails));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("COMPANY_INFO", dt_Comp_info));
            this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports_FAC/" + ReportFile);

            ReportParameter rp1 = new ReportParameter("P_SALERETURNID", invoiceid.Trim());
            ReportParameter rp2 = new ReportParameter("p_InvoiceType_original", Request.QueryString["original"].ToString());
            ReportParameter rp3 = new ReportParameter("p_InvoiceType_duplicate", Request.QueryString["duplicate"].ToString());
            ReportParameter rp4 = new ReportParameter("p_InvoiceType_triplicate", Request.QueryString["triplicate"].ToString());
            ReportParameter rp5 = new ReportParameter("p_InvoiceType_extra", Request.QueryString["backpsheet"].ToString());
            ReportParameter rp6 = new ReportParameter("pcount", Request.QueryString["pcount"].ToString());
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

                result = ReportViewer1.LocalReport.Render(format,
                    devInfo, out mimeType, out encoding,
                    out fileNameExtn, out stearms, out warnings);
                // result = ConcatBytes(result, result_sub);  //ConcatBytes concat two Byte[]


                // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.    
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "inline; filename= SaleReturn.pdf");
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

    #region ShowReport_StockJournal
    private void ShowReport_StockJournal(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string invoiceid)
    {
        DataTable dtdetails = new DataTable();
        DataTable dtheader = new DataTable();
        DataTable dt_Comp_info = new DataTable();
        DataTable dt_COMBO = new DataTable();

        BAL.ClsInvoicePrint_FAC clsrpt = new BAL.ClsInvoicePrint_FAC();
        dtdetails = clsrpt.BindStockjournaldetails(invoiceid.Trim());
        dtheader = clsrpt.BindStockjournalheader(invoiceid.Trim());
        dt_Comp_info = clsrpt.Bind_CompanyInfo();

        this.ReportViewer1.LocalReport.EnableExternalImages = true;
        if (rptType == "") //Normal View
        {
            this.ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource();
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("stockjournaldetails", dtdetails));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("stockjournalheader", dtheader));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Comp_Info", dt_Comp_info));

            this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports_FAC/" + ReportFile);

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
                // result = ConcatBytes(result, result_sub); 
                //ConcatBytes concat two Byte[]

                // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
                string ReportName = string.Empty;
                ReportName = ReportFile.Replace(".rdlc", "").ToString() + "_" + dtheader.Rows[0]["ADJUSTMENTNUM"].ToString();
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = mimeType;
                Response.AddHeader("content-disposition", "inline;filename= " + ReportName + ".pdf");
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

    #region BillEntry_GST
    private void BillEntry_GST(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string invoiceid)
    {

        DataTable dtdetails = new DataTable();
        DataTable dtheader = new DataTable();
        DataTable dtfooter = new DataTable();
        DataTable dt_Comp_info = new DataTable();

        BAL.ClsInvoicePrint_FAC clsrpt = new BAL.ClsInvoicePrint_FAC();
        dtdetails = clsrpt.BindTransporterBillDetails(invoiceid.Trim());
        dtheader = clsrpt.BindTransporterBillHeader(invoiceid.Trim());
        dtfooter = clsrpt.BindTransporterBillFooter(invoiceid.Trim());
        dt_Comp_info = clsrpt.Bind_CompanyInfo();

        this.ReportViewer1.LocalReport.EnableExternalImages = true;

        if (rptType == "") //Normal View
        {
            //this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource();
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DS_TransporterBill_Details", dtdetails));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Ds_TransporterBill_Header", dtheader));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Ds_TransporterBill_Footer", dtfooter));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Ds_Comp_Info", dt_Comp_info));

            this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports/" + ReportFile);


            ReportParameter rp1 = new ReportParameter("p_TRANSPORTERBILLID", invoiceid.Trim());
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
                Response.AddHeader("content-disposition", "inline; filename= TransporterBill.pdf");
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

    #region Gross_return_gst
    private void Gross_return_gst(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string invoiceid)
    {

        DataTable dtdetails = new DataTable();
        DataTable dtheader = new DataTable();
        DataTable dtfooter = new DataTable();
        DataTable dt_Comp_info = new DataTable();
        DataTable dt_distributor_info = new DataTable();
        DataTable dt_saleinvoice_fromdepot = new DataTable();

        BAL.ClsInvoicePrint_FAC clsrpt = new BAL.ClsInvoicePrint_FAC();
        dtdetails = clsrpt.BindSalereturnDetails(invoiceid.Trim(), Request.QueryString["original"].ToString(), Request.QueryString["duplicate"].ToString(), Request.QueryString["triplicate"].ToString(), Request.QueryString["backpsheet"].ToString());
        dtheader = clsrpt.BindSalereturnHeader(invoiceid.Trim());
        dtfooter = clsrpt.BindSalereturnFooter(invoiceid.Trim());
        dt_Comp_info = clsrpt.Bind_CompanyInfo();
        dt_distributor_info = clsrpt.Bind_InvoiceDistributorReturn(invoiceid.Trim());
        dt_saleinvoice_fromdepot = clsrpt.Bind_ReturnFromDepot(invoiceid.Trim());

        this.ReportViewer1.LocalReport.EnableExternalImages = true;

        ackno = Convert.ToString(dtheader.Rows[0]["AckNo"]);
        if (ackno == "")
        {
            ackno = "0";

        }
        irnno = Convert.ToString(dtheader.Rows[0]["Irn"]);
        if (ackdt == "")
        {
            ackdt = "";
        }
        ackdt = Convert.ToString(dtheader.Rows[0]["AckDt"]);
        if (ackdt == "")
        {
            ackdt = "";
        }
        string qrcode = Convert.ToString(dtheader.Rows[0]["Qrcode"]);
        if (ackno != "0")
        {
            _qrfile = GenerateCode(qrcode, ackno);
            imagePath = new Uri(Server.MapPath("~/FileUpload/" + _qrfile)).AbsoluteUri;
        }
        else
        {
            imagePath = new Uri(Server.MapPath("~/FileUpload/QRImage.jpg")).AbsoluteUri;
        }

        if (rptType == "") //Normal View
        {
            //this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource();
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DETAILS", dtdetails));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HEADER", dtheader));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("FOOTER", dtfooter));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("COMPANY_INFO", dt_Comp_info));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DISTRIBUTOR_INFO", dt_distributor_info));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("SALEINVOICE_FROMDEPOT", dt_saleinvoice_fromdepot));

            this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports_FAC/" + ReportFile);



            ReportParameter rp1 = new ReportParameter("p_INVOICEID", invoiceid.Trim());
            ReportParameter rp2 = new ReportParameter("p_InvoiceType_original", Request.QueryString["original"].ToString());
            ReportParameter rp3 = new ReportParameter("p_InvoiceType_duplicate", Request.QueryString["duplicate"].ToString());
            ReportParameter rp4 = new ReportParameter("p_InvoiceType_triplicate", Request.QueryString["triplicate"].ToString());
            ReportParameter rp5 = new ReportParameter("p_InvoiceType_backup", Request.QueryString["backpsheet"].ToString());
            ReportParameter rp6 = new ReportParameter("pcount", Request.QueryString["pcount"].ToString());
            ReportParameter parameter = new ReportParameter("ImagePath", imagePath);
            ReportParameter rp7 = new ReportParameter("Ackno", ackno);
            ReportParameter rp8 = new ReportParameter("Ackdt", ackdt);

            ReportViewer1.LocalReport.SetParameters(parameter);
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1 });
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp2 });
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp3 });
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp4 });
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp5 });
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp6 });
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp7 });
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp8 });

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
                Response.AddHeader("content-disposition", "inline; filename= Salereturn.pdf");
                Response.BinaryWrite(result); // create the file    
                Response.Flush();
            }
            catch (Exception ex)
            {
                throw ex;

            }

            ReportViewer1.ServerReport.Refresh();
        }

        else if (rptType == "PDF")  //PDF View
        {

            //this.ReportViewer1.LocalReport.Refresh();
        }
    }
    #endregion

    #region Job_order_fac
    private void Job_order_fac(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string invoiceid)
    {
        DataTable dtdetails = new DataTable();
        DataTable dtheader = new DataTable();
        BAL.ClsInvoicePrint_FAC clsrpt = new BAL.ClsInvoicePrint_FAC();
        dtdetails = clsrpt.Joborderdetails_print(invoiceid.Trim());
        dtheader = clsrpt.Joborderheader_print(invoiceid.Trim());

        this.ReportViewer1.LocalReport.EnableExternalImages = true;

        if (rptType == "") //Normal View
        {
            //this.ReportViewer1.LocalReport.Refresh();
            this.ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource();
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("details", dtdetails));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("header", dtheader));


            this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports_FAC/" + ReportFile);


            ReportParameter rp1 = new ReportParameter("P_STOCKDESPATCHID", invoiceid.Trim());
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
                Response.AddHeader("content-disposition", "inline; filename= Joborder.pdf");
                Response.BinaryWrite(result); // create the file    
                Response.Flush();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            ReportViewer1.ServerReport.Refresh();
        }

        else if (rptType == "PDF")  //PDF View
        {

            //this.ReportViewer1.LocalReport.Refresh();
        }
    }
    #endregion

    #region ShowReport_Corporate    
    private void ShowReport_Corporate(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string invoiceid)
    {
        DataTable dtdetails = new DataTable();
        DataSet dtheader = new DataSet();
        DataTable dt_COMBO = new DataTable();
        BAL.ClsInvoicePrint_FAC clsrpt = new BAL.ClsInvoicePrint_FAC();
        if (Request.QueryString["backpsheet"].ToString() == "4")
        {
            dtdetails = clsrpt.Bind_InvoiceDetails_Corporate_GST_Print(invoiceid.Trim(), "1", Request.QueryString["duplicate"].ToString(), Request.QueryString["triplicate"].ToString(), Request.QueryString["backpsheet"].ToString());
        }
        else
        {
            dtdetails = clsrpt.Bind_InvoiceDetails_Corporate_GST_Print(invoiceid.Trim(), Request.QueryString["original"].ToString(), Request.QueryString["duplicate"].ToString(), Request.QueryString["triplicate"].ToString(), Request.QueryString["backpsheet"].ToString());
        }
        dtheader = clsrpt.Bind_InvoiceHeader_Print(invoiceid.Trim());
        dt_COMBO = clsrpt.Bind_Combo(invoiceid.Trim());
        this.ReportViewer1.LocalReport.EnableExternalImages = true;
        if (rptType == "") //Normal View
        {
            this.ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource();
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DETAILS", dtdetails));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HEADER", dtheader.Tables[0]));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("FOOTER", dtheader.Tables[1]));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DISTRIBUTORINFO", dtheader.Tables[3]));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("COMPANY_INFO", dtheader.Tables[2]));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("SALEINVOICE_FROMDEPOT", dtheader.Tables[4]));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("COMBOPRODUCT", dt_COMBO));
            this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports/" + ReportFile);
            ReportParameter rp1 = new ReportParameter("p_INVOICEID", invoiceid.Trim());
            ReportParameter rp2 = new ReportParameter("p_InvoiceType_original", Request.QueryString["original"].ToString());
            ReportParameter rp3 = new ReportParameter("p_InvoiceType_duplicate", Request.QueryString["duplicate"].ToString());
            ReportParameter rp4 = new ReportParameter("p_InvoiceType_triplicate", Request.QueryString["triplicate"].ToString());
            ReportParameter rp5 = new ReportParameter("p_InvoiceType_backup", Request.QueryString["backpsheet"].ToString());
            ReportParameter rp6 = new ReportParameter("pcount", Request.QueryString["pcount"].ToString());
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

                result = ReportViewer1.LocalReport.Render(format,
                    devInfo, out mimeType, out encoding,
                    out fileNameExtn, out stearms, out warnings);
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

    #region ShowReport_Proforma
    private void ShowReport_Proforma(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string invoiceid)
    {
        DataTable dt_InvoiceDetails = new DataTable();
        DataTable dt_Comp_info = new DataTable();
        DataTable dt_terms = new DataTable();
        BAL.ClsInvoicePrint_FAC clsrpt = new BAL.ClsInvoicePrint_FAC();

        dt_InvoiceDetails = clsrpt.BindProforma(invoiceid.Trim());
        dt_Comp_info = clsrpt.Bind_CompanyInfo();
        dt_terms = clsrpt.BindTerms_Proforma(invoiceid.Trim());
        this.ReportViewer1.LocalReport.EnableExternalImages = true;
        if (rptType == "") //Normal View
        {
            this.ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource();
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dst_compinfo", dt_Comp_info));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dst_creditnote", dt_InvoiceDetails));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("TERMS", dt_terms));
            this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports/" + ReportFile);
            ReportParameter rp1 = new ReportParameter("p_INVOICEID", invoiceid.Trim());
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
                Response.AddHeader("content-disposition", "inline; filename= ProformaInvoice.pdf");
                Response.BinaryWrite(result); // create the file    
                Response.Flush();
            }
            catch (Exception ex)
            {

            }
            ReportViewer1.ServerReport.Refresh();
        }
    }
    #endregion

    #region ShowReport_EXPORT
    private void ShowReport_EXPORT(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string invoiceid)
    {
        DataTable dt_InvoiceDetails = new DataTable();
        DataTable dt_Comp_info = new DataTable();
        DataTable header = new DataTable();
        BAL.ClsInvoicePrint_FAC clsrpt = new BAL.ClsInvoicePrint_FAC();

        dt_InvoiceDetails = clsrpt.Bind_ExportInvoice(invoiceid.Trim());
        dt_Comp_info = clsrpt.Bind_CompanyInfo();
        header = clsrpt.Bind_Headerexport(invoiceid.Trim());
        this.ReportViewer1.LocalReport.EnableExternalImages = true;
        if (rptType == "") //Normal View
        {
            this.ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource();

            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("COMP_INFO", dt_Comp_info));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DST_EXPORTINVOICE", dt_InvoiceDetails));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("export_header", header));

            this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports/" + ReportFile);

            ReportParameter rp1 = new ReportParameter("p_INVOICEID", invoiceid.Trim());
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
                Response.AddHeader("content-disposition", "inline; filename= ExportInvoice.pdf");
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

    #region SHOWREPORT_EXPORT_GST
    private void ShowReport_EXPORT_GST(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string invoiceid)
    {
        DataTable dt_InvoiceDetails = new DataTable();
        DataTable dt_Comp_info = new DataTable();
        DataTable header = new DataTable();

        BAL.ClsInvoicePrint_FAC clsrpt = new BAL.ClsInvoicePrint_FAC();
        dt_InvoiceDetails = clsrpt.Bind_ExportInvoice_GST(invoiceid.Trim(), Request.QueryString["original"].ToString(), Request.QueryString["duplicate"].ToString(), Request.QueryString["triplicate"].ToString());
        //dt_InvoiceDetails = clsrpt.Bind_ExportInvoice(invoiceid.Trim());
        dt_Comp_info = clsrpt.Bind_CompanyInfo();
        header = clsrpt.Bind_Headerexport(invoiceid.Trim());

        this.ReportViewer1.LocalReport.EnableExternalImages = true;
        if (rptType == "") //Normal View
        {
            this.ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource();

            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("COMP_INFO", dt_Comp_info));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DST_EXPORTINVOICE", dt_InvoiceDetails));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("export_header", header));

            this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports_FAC/" + ReportFile);


            ReportParameter rp1 = new ReportParameter("p_INVOICEID", invoiceid.Trim());
            ReportParameter rp2 = new ReportParameter("p_InvoiceType_original", Request.QueryString["original"].ToString());
            ReportParameter rp3 = new ReportParameter("p_InvoiceType_duplicate", Request.QueryString["duplicate"].ToString());
            ReportParameter rp4 = new ReportParameter("p_InvoiceType_triplicate", Request.QueryString["triplicate"].ToString());
            ReportParameter rp5 = new ReportParameter("pcount", Request.QueryString["pcount"].ToString());
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1 });
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp2 });
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp3 });
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp4 });
            ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp5 });

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
                Response.AddHeader("content-disposition", "inline; filename= ExportInvoice.pdf");
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

    #region ShowReport_PackingList
    private void ShowReport_PackingList(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string invoiceid)
    {
        DataTable dt_InvoiceDetails = new DataTable();
        DataTable dt_Comp_info = new DataTable();
        DataTable dt_userinfo = new DataTable();
        DataTable dt_terms = new DataTable();
        BAL.ClsInvoicePrint_FAC clsrpt = new BAL.ClsInvoicePrint_FAC();
        dt_InvoiceDetails = clsrpt.BindTerms_PaclingList(invoiceid.Trim());
        dt_Comp_info = clsrpt.Bind_CompanyInfo();
        dt_userinfo = clsrpt.Bind_Headerexport(invoiceid.Trim());

        this.ReportViewer1.LocalReport.EnableExternalImages = true;
        if (rptType == "") //Normal View
        {
            this.ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource();

            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DS_CompInfo", dt_Comp_info));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DS_PackList", dt_InvoiceDetails));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("export_header", dt_userinfo));

            this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports/" + ReportFile);


            ReportParameter rp1 = new ReportParameter("p_INVOICEID", invoiceid.Trim());
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
                Response.AddHeader("content-disposition", "inline; filename=PackingList.pdf");
                Response.BinaryWrite(result); // create the file    
                Response.Flush();
            }
            catch (Exception ex)
            {

            }
            ReportViewer1.ServerReport.Refresh();
        }
    }
    #endregion

    #region ShowReport_PO
    private void ShowReport_PO(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string POID)
    {
        DataTable dtdetails = new DataTable();
        DataTable dtheader = new DataTable();
        DataTable dt_Comp_info = new DataTable();
        DataTable dt_Comp_infofactoryid = new DataTable();

        BAL.ClsStockReport clsrpt = new BAL.ClsStockReport();
        dtdetails = clsrpt.BindPODetails(POID);
        dtheader = clsrpt.BindPOHeader(POID);
        dt_Comp_info = clsrpt.Bind_CompanyInfo();
        dt_Comp_infofactoryid = clsrpt.Bind_CompanyInfofactoryid(POID); /*new add for factory wise address*/

        this.ReportViewer1.LocalReport.EnableExternalImages = true;

        this.ReportViewer1.LocalReport.DataSources.Clear();
        ReportDataSource rds = new ReportDataSource();
        this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DETAILS", dtdetails));//
        this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HEADER", dtheader));
        this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("COMP_INFO", dt_Comp_info));
        this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("COMP_INFOFACTORYWISE", dt_Comp_infofactoryid));/*unit one and unit 2*/

        this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports/" + ReportFile);
        ReportParameter rp1 = new ReportParameter("p_POID", POID);
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
            Response.AddHeader("content-disposition", "inline; filename= PurchaseOrder.pdf");
            Response.BinaryWrite(result); // create the file    
            Response.Flush();
        }
        catch (Exception ex)
        {

        }
        ReportViewer1.ServerReport.Refresh();
    }
    #endregion

    #region ShowReport_NRGP
    private void ShowReport_NRGP(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string NRGPID)
    {
        DataTable dtdetails = new DataTable();
        DataTable dtheader = new DataTable();
        DataTable dt_Comp_info = new DataTable();
        //DataTable dt_Comp_infofactoryid = new DataTable();

        ClsSaleorderFG clsrpt = new ClsSaleorderFG();
        dtdetails = clsrpt.BindNRGPDetails(NRGPID);
        dtheader = clsrpt.BindNRGPHeader(NRGPID);
        dt_Comp_info = clsrpt.Bind_CompanyInfo();
        //dt_Comp_infofactoryid = clsrpt.Bind_CompanyInfofactoryid(NRGPID); /*new add for factory wise address*/

        this.ReportViewer1.LocalReport.EnableExternalImages = true;

        this.ReportViewer1.LocalReport.DataSources.Clear();
        ReportDataSource rds = new ReportDataSource();
        this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("NRGP_DETAILS", dtdetails));
        this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("NRGP_HEADER", dtheader));
        this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("COMP_INFO", dt_Comp_info));
        //this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("COMP_INFOFACTORYWISE", dt_Comp_infofactoryid));/*unit one and unit 2*/

        this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports/" + ReportFile);
        ReportParameter rp1 = new ReportParameter("p_POID", NRGPID);
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
            Response.AddHeader("content-disposition", "inline; filename= RptNRGPInvoice.pdf");
            Response.BinaryWrite(result); // create the file    
            Response.Flush();
        }
        catch (Exception ex)
        {

        }
        ReportViewer1.ServerReport.Refresh();
    }
    #endregion

    #region ShowReport_GRNREJECTION
    private void ShowReport_GRNREJECTION(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string RejectionID)
    {
        DataTable dtdetails = new DataTable();
        DataTable dtheader = new DataTable();
        DataTable dt_Comp_info = new DataTable();

        dtdetails = objInvPrint.BindRejectionDetails(RejectionID);
        dtheader = objInvPrint.BindRejectionHeader(RejectionID);
        dt_Comp_info = objInvPrint.Bind_CompanyInfo();

        this.ReportViewer1.LocalReport.EnableExternalImages = true;
        this.ReportViewer1.LocalReport.DataSources.Clear();
        ReportDataSource rds = new ReportDataSource();
        this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("NRGP_DETAILS", dtdetails));
        this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("NRGP_HEADER", dtheader));
        this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("COMP_INFO", dt_Comp_info));

        this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports_FAC/" + ReportFile);
        ReportParameter rp1 = new ReportParameter("p_POID", RejectionID);
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
            Response.AddHeader("content-disposition", "inline;filename= " + ReportFile + ".pdf");
            Response.BinaryWrite(result); // create the file    
            Response.Flush();
        }
        catch (Exception ex)
        {

        }
        ReportViewer1.ServerReport.Refresh();
    }
    #endregion

    #region ShowReport_Requisition
    private void ShowReport_Requisition(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string REQSID)
    {
        DataTable dtdetails = new DataTable();
        DataTable dtheader = new DataTable();
        DataTable dt_Comp_info = new DataTable();
        //DataTable dt_Comp_infofactoryid = new DataTable();

        ClsSaleorderFG clsrpt = new ClsSaleorderFG();
        dtdetails = clsrpt.BindREQSDetails(REQSID);
        dtheader = clsrpt.BindREQSHeader(REQSID);
        this.ReportViewer1.LocalReport.EnableExternalImages = true;

        this.ReportViewer1.LocalReport.DataSources.Clear();
        ReportDataSource rds = new ReportDataSource();
        this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("REQUISITION_DETAILS", dtdetails));
        this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("REQUISITION_HEADER", dtheader));

        this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports_FAC/" + ReportFile);
        ReportParameter rp1 = new ReportParameter("p_POID", REQSID);
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
            Response.AddHeader("content-disposition", "inline; filename= RptRequisitionSlip.pdf");
            Response.BinaryWrite(result); // create the file    
            Response.Flush();
        }
        catch (Exception ex)
        {

        }
        ReportViewer1.ServerReport.Refresh();
    }
    #endregion

    #region ShowReport_Issue
    private void ShowReport_Issue(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string IssueID)
    {
        DataTable dtdetails = new DataTable();
        DataTable dtheader = new DataTable();
        DataTable dt_Comp_info = new DataTable();

        ClsIssue clsrpt = new ClsIssue();
        dtdetails = clsrpt.BindIssueDetails(IssueID);
        dtheader = clsrpt.BindIssueHeader(IssueID);

        this.ReportViewer1.LocalReport.EnableExternalImages = true;

        this.ReportViewer1.LocalReport.DataSources.Clear();
        ReportDataSource rds = new ReportDataSource();
        this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("ISSUE_DETAILS", dtdetails));
        this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("ISSUE_HEADER", dtheader));


        this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports_FAC/" + ReportFile);
        ReportParameter rp1 = new ReportParameter("p_POID", IssueID);
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
            Response.AddHeader("content-disposition", "inline; filename= RptIssueSlip.pdf");
            Response.BinaryWrite(result); // create the file    
            Response.Flush();
        }
        catch (Exception ex)
        {

        }
        ReportViewer1.ServerReport.Refresh();
    }
    #endregion

    #region GenarateCode
    private string GenerateCode(string name, string ackno)
    {
        var writer = new BarcodeWriter();
        writer.Format = BarcodeFormat.QR_CODE;
        writer.Options = new ZXing.Common.EncodingOptions
        {
            Width = 930,
            Height = 930
        };
        var result = writer.Write(name);
        string Qrfile = "QRImage_" + ackno + ".jpg";
        string path = Server.MapPath("~/FileUpload/" + Qrfile);
        var barcodeBitmap = new Bitmap(result);
        using (MemoryStream memory = new MemoryStream())
        {
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                barcodeBitmap.Save(memory, ImageFormat.Jpeg);
                byte[] bytes = memory.ToArray();
                fs.Write(bytes, 0, bytes.Length);
            }

        }
        return Qrfile;
    }
    #endregion
}