using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using BAL;
using Microsoft.Reporting.WebForms;
public partial class VIEW_frmRptInvoicePrint : System.Web.UI.Page
{
    Hashtable ht = new Hashtable();
    Hashtable RptHt = new Hashtable();
    private int m_currentPageIndex;
    private IList<Stream> m_streams;

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
                string Isautovoucher = string.Empty;
                string AUTOACCENTRYID = string.Empty;
                DataTable dtheader = new DataTable();
                DataTable dtheader_SR = new DataTable();
                ClsStockReport clsrpt = new ClsStockReport();
                Utility.ClsParam objParam = new Utility.ClsParam();
                ViewState["_ParamBSegment"] = objParam.Paramilitary;

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
                            //ShowReport_PO("PurchaseOrder.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                            ShowReport_PO("rptPurchaseOrder.rdlc", "", ref ht, ref RptHt, invoiceid);
                        }
                    }
                }
                #endregion

                #region Stock Print Section
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
                        else
                        {
                            dtheader = clsrpt.BindPurchaseHeader(invoiceid.Trim());
                            int InvoiceDate = Convert.ToInt32(Conver_To_ISO(dtheader.Rows[0]["STOCKRECEIVEDDATE"].ToString()));
                            int checkdate = Convert.ToInt32(Conver_To_ISO("30/06/2017"));

                            if (InvoiceDate <= checkdate)
                            {
                                ShowReport_PurchaseBill_VAT("PurchaseBill.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                            }
                            else
                            {
                                if (dtheader.Rows[0]["INVOICETYPE"].ToString() == "0")
                                {
                                    ShowReport_PurchaseBill("PurchaseBil_.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                                }
                                if (dtheader.Rows[0]["INVOICETYPE"].ToString() == "1")
                                {
                                    ShowReport_PurchaseBill("PurchaseBil_IGST.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                                }
                                if (dtheader.Rows[0]["INVOICETYPE"].ToString() == "2")
                                {
                                    ShowReport_PurchaseBill("PurchaseBil_GST.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                                }
                            }
                        }
                    }
                    if (MenuId == "46")
                    {
                        dtheader = clsrpt.BindST_Header(invoiceid.Trim());
                        if (dtheader.Rows[0]["INVOICE_TYPE"].ToString() == "0")
                        {
                            if (Request.QueryString["ewaybill"].ToString() == "5")
                            {
                                ShowReport_EWayBillTransfer("Ewaybill_StockTransfer.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For GT AND MT

                            }
                            else
                            {
                                ShowReport_StockTransfer("DeliveryChallan_Transfer.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                            }
                        }
                        if (dtheader.Rows[0]["INVOICE_TYPE"].ToString() == "9")
                        {
                            if (Request.QueryString["ewaybill"].ToString() == "5")
                            {
                                ShowReport_EWayBillTransfer("Ewaybill_StockTransfer.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For GT AND MT

                            }
                            else
                            {
                                ShowReport_StockTransfer("BillofSupply_Transfer.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                            }
                        }
                        if (dtheader.Rows[0]["INVOICE_TYPE"].ToString() == "1")
                        {
                            if (Request.QueryString["ewaybill"].ToString() == "5")
                            {
                                ShowReport_EWayBillTransfer("Ewaybill_StockTransfer.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For GT AND MT
                            }
                            else
                            {
                                ShowReport_StockTransfer("StockDespatch_GST.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                            }
                        }
                        if (dtheader.Rows[0]["INVOICE_TYPE"].ToString() == "1")
                        {
                            if (Request.QueryString["ewaybill"].ToString() == "5")
                            {
                                ShowReport_EWayBillTransfer("Ewaybill_StockTransfer.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For GT AND MT
                            }
                            else
                            {
                                ShowReport_StockTransfer("StockDespatch_GST.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                            }
                        }
                        if (dtheader.Rows[0]["INVOICE_TYPE"].ToString() == "2")
                        {
                            if (Request.QueryString["ewaybill"].ToString() == "5")
                            {
                                ShowReport_EWayBillTransfer("Ewaybill_StockTransfer.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For GT AND MT

                            }
                            else
                            {
                                ShowReport_StockTransfer("StockTransfer_GST.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                            }
                        }
                    }
                    else
                    {
                        dtheader_SR = clsrpt.BindSR_Transfer(invoiceid.Trim());
                        if (dtheader_SR.Rows[0]["INVOICE_TYPE"].ToString() == "0" || dtheader_SR.Rows[0]["INVOICE_TYPE"].ToString() == "9")
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

                /*Added By Sayan Dey On 04.06.2018*/
                if (Request.QueryString["AdvRecpt_ID"] != null && Request.QueryString["Voucherid"] != null)
                {
                    string Depotid = Session["DEPOTID"].ToString();
                    AdvRecpt_ID = Request.QueryString["AdvRecpt_ID"];
                    Voucherid = Request.QueryString["Voucherid"];
                    if (Voucherid == "13" || Voucherid == "14")
                    {
                        ShowReport_DebitNote("DebitNote.rdlc", "", ref ht, ref RptHt, "", AdvRecpt_ID, Voucherid);
                    }
                    else if (Voucherid == "15" || Voucherid == "16")
                    {
                        if (Depotid == "14857CFC-2450-4D52-B93A-486D9507A1BE" || Depotid == "FFC65354-AB46-4983-A67F-111486EC3D39")
                        {
                            ShowReport_AdvanceReceipt("Voucher_FAC.rdlc", "", ref ht, ref RptHt, "", AdvRecpt_ID, Voucherid);
                        }
                        else
                        {
                            ShowReport_AdvanceReceipt("Voucher.rdlc", "", ref ht, ref RptHt, "", AdvRecpt_ID, Voucherid);
                        }
                    }
                    else if (Voucherid == "11" || Voucherid == "12" || Voucherid == "2" || Voucherid == "8")
                    {
                        ShowReport_Journal("AccountsJournal.rdlc", "", ref ht, ref RptHt, "", AdvRecpt_ID, Voucherid);
                    }
                    else if (Voucherid == "5")
                    {
                        ShowReport_Journal("TransporterVoucher.rdlc", "", ref ht, ref RptHt, "", AdvRecpt_ID, Voucherid);
                    }
                    else if (Voucherid == "1")
                    {
                        ShowReport_Journal("Contra.rdlc", "", ref ht, ref RptHt, "", AdvRecpt_ID, Voucherid);
                    }
                }
                /*=================================================*/

                #endregion

                if (Request.QueryString["SLRID"] != null && Request.QueryString["BSID"] != null)
                {
                    invoiceid = Request.QueryString["SLRID"];
                    ShowReport_SaleReturn_GT("SaleReturn_IGST.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                }

                #region Advance And Debit Print Section
                if (Request.QueryString["AdvRecpt_ID"] != null && Request.QueryString["Voucherid"] != null)
                {
                    AdvRecpt_ID = Request.QueryString["AdvRecpt_ID"];
                    Voucherid = Request.QueryString["Voucherid"];
                    Isautovoucher = Request.QueryString["Isautovoucher"];
                    AUTOACCENTRYID = Request.QueryString["AUTOACCENTRYID"];

                    if (Voucherid == "13" || Voucherid == "14")
                    {
                        ShowReport_DebitNote("DebitNote.rdlc", "", ref ht, ref RptHt, "", AdvRecpt_ID, Voucherid);
                    }
                    else if (Voucherid == "15" || Voucherid == "16")
                    {
                        if (Isautovoucher == "N")
                        {
                            ShowReport_AdvanceReceipt("Voucher.rdlc", "", ref ht, ref RptHt, "", AdvRecpt_ID, Voucherid);
                        }
                        /*Added By Sayan Dey On 04.06.2018*/
                        else if (Isautovoucher == "Y")
                        {
                            ShowReport_AdvanceReceipt("AutoVoucher.rdlc", "", ref ht, ref RptHt, "", AUTOACCENTRYID, Voucherid);
                        }
                        /*================================*/
                    }
                    else if (Voucherid == "11" || Voucherid == "12" || Voucherid == "8")
                    {
                        ShowReport_Journal("AccountsJournal.rdlc", "", ref ht, ref RptHt, "", AdvRecpt_ID, Voucherid);
                    }
                    /*Added By Sayan Dey On 04.06.2018*/
                    else if (Voucherid == "2" && Isautovoucher == "Y")
                    {
                        ShowReport_Journal("AutoAccountsJournal.rdlc", "", ref ht, ref RptHt, "", AUTOACCENTRYID, Voucherid);
                    }
                    else if (Voucherid == "2" && Isautovoucher == "N")
                    {
                        ShowReport_Journal("AccountsJournal.rdlc", "", ref ht, ref RptHt, "", AdvRecpt_ID, Voucherid);
                    }
                    /*===============================*/
                    else if (Voucherid == "5")
                    {
                        ShowReport_Journal("TransporterVoucher.rdlc", "", ref ht, ref RptHt, "", AdvRecpt_ID, Voucherid);
                    }
                    else if (Voucherid == "1")
                    {
                        ShowReport_Journal("Contra.rdlc", "", ref ht, ref RptHt, "", AdvRecpt_ID, Voucherid);
                    }
                }
                #endregion
                #region Stock Journal Print Section
                if (Request.QueryString["StkJrnl_ID"] != null && Request.QueryString["ISTPU"] != "True")
                {
                    invoiceid = Request.QueryString["StkJrnl_ID"];
                    ShowReport_StockJournal("Stock_Journal.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                }
                else if (Request.QueryString["StkJrnl_ID"] != null && Request.QueryString["ISTPU"] == "True")
                {
                    invoiceid = Request.QueryString["StkJrnl_ID"];
                    ShowReport_TPUStockJournal("Stock_Journal.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                }
                #endregion
                if (Request.QueryString["pid"] != null)
                {
                    invoiceid = Request.QueryString["pid"];
                    dtheader = clsrpt.Bind_InvoiceType(invoiceid.Trim());
                    DataTable dtreturn = new DataTable();
                    dtreturn = clsrpt.Bind_Returntype(invoiceid.Trim());
                    DataTable dtpurchasereturn = new DataTable();
                    dtpurchasereturn = clsrpt.Bind_PurchaseReturnType(invoiceid.Trim());

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
                    if (dtreturn.Rows.Count > 0)
                    {
                        if (dtreturn.Rows[0]["GROSSRETURN"].ToString() == "Y")
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
                        else if (dtreturn.Rows[0]["GROSSRETURN"].ToString() == "N")
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
                    }
                    if (dtpurchasereturn.Rows.Count > 0)
                    {
                        if (dtpurchasereturn.Rows[0]["MODULE"].ToString() == "12")
                        {
                            if (dtpurchasereturn.Rows[0]["INVOICETYPEID"].ToString() == "2")
                            {
                                Purchase_return_gst("Purchasereturn_GST.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                            }
                            else
                            {
                                Purchase_return_gst("Purchasereturn_IGST.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                            }
                        }
                    }
                    if (dtheader.Rows[0]["INVOICETYPEID"].ToString() == "0")
                    {
                        if (Request.QueryString["BSID"].ToString().Trim() == "7F62F951-9D1F-4B8D-803B-74EEBA468CEE" || Request.QueryString["BSID"].ToString().Trim() == "0AA9353F-D350-4380-BC84-6ED5D0031E24" || Request.QueryString["BSID"].ToString().Trim() == "A18FE04D-057C-4A88-9204-5FE1613626C9" || Request.QueryString["BSID"].ToString().Trim() == "33F6AC5E-1F37-4B0F-B959-D1C900BB43A5" || Request.QueryString["BSID"].ToString().Trim() == "91B9CC35-AF4F-4732-92B1-433C68104DE6" || Request.QueryString["BSID"].ToString().Trim() == "97547CB6-F40B-4B43-923D-B63F61A910C2")
                        {
                            //For GT AND MT AND E-COMMERCE AND INSTITUTIONAL

                            if (Request.QueryString["ewaybill"].ToString() == "5")
                            {
                                ShowReport_EWayBill("Ewaybill.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For GT AND MT
                            }
                            else
                            {
                                if (Request.QueryString["BSID"].ToString().Trim() == "33F6AC5E-1F37-4B0F-B959-D1C900BB43A5")
                                {
                                    ShowReport_GT("DeliveryChallan_Trading.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                                }
                                else
                                {
                                    ShowReport_GT("DeliveryChallan.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For GT AND MT
                                }
                            }
                        }
                        else if (Request.QueryString["BSID"].ToString().Trim() == "GTMT" && PSID == "SALERETURN_GT_MT")
                        {
                            ShowReport_SaleReturn_GT("SaleReturn_Challan.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                        }
                        else if (Request.QueryString["BSID"].ToString().Trim() == "C5038911-9331-40CF-B7F9-583D50583592")
                        {
                            if (Request.QueryString["ewaybill"].ToString() == "5")
                            {
                                ShowReport_EWayBill("Ewaybill.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For GT AND MT
                            }
                            else
                            {
                                if (Request.QueryString["backpsheet"].ToString() == "0")
                                {
                                    ShowReport_CSD("DeliveryChallan_CSD.rdlc", "", ref ht, ref RptHt, "", invoiceid);    //For CSD
                                }
                                else
                                {
                                    ShowReport_CSD("CSD_BackupSheet.rdlc", "", ref ht, ref RptHt, "", invoiceid);    //For CSD_BackupSheet
                                }
                            }
                        }
                        else if (Request.QueryString["BSID"].ToString().Trim() == "A0A1E83E-1993-4FB9-AF53-9DD595D09596")
                        {
                            if (Request.QueryString["ewaybill"].ToString() == "5")
                            {
                                ShowReport_EWayBill("Ewaybill.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For GT AND MT
                            }
                            else
                            {
                                ShowReport_CPC("DeliveryChallan_CPC.rdlc", "", ref ht, ref RptHt, "", invoiceid);   //For CPC
                            }
                        }
                        else if (Request.QueryString["BSID"].ToString().Trim() == "8AE0E8C9-F4F7-4382-B8AB-870A64ABF996")
                        {
                            if (Request.QueryString["ewaybill"].ToString() == "5")
                            {
                                ShowReport_EWayBill("Ewaybill.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For GT AND MT
                            }
                            else
                            {
                                ShowReport_CPC("DeliveryChallan_INCS.rdlc", "", ref ht, ref RptHt, "", invoiceid);   //For INCS
                            }
                        }
                        else if (Request.QueryString["BSID"].ToString().Trim() == "2E96A0A4-6256-472C-BE4F-C59599C948B0")
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
                            if (PSID == "WEIGHT")
                            {
                                ShowReport_PackingList("WeightList.rdlc", "", ref ht, ref RptHt, "", invoiceid);  //For Weight LIST
                            }
                        }
                    }

                    if (dtheader.Rows[0]["INVOICETYPEID"].ToString() == "9")
                    {
                        if (Request.QueryString["BSID"].ToString().Trim() == "7F62F951-9D1F-4B8D-803B-74EEBA468CEE" || Request.QueryString["BSID"].ToString().Trim() == "0AA9353F-D350-4380-BC84-6ED5D0031E24" || Request.QueryString["BSID"].ToString().Trim() == "A18FE04D-057C-4A88-9204-5FE1613626C9" || Request.QueryString["BSID"].ToString().Trim() == "33F6AC5E-1F37-4B0F-B959-D1C900BB43A5")
                        {
                            if (Request.QueryString["ewaybill"].ToString() == "5")
                            {
                                ShowReport_EWayBill("Ewaybill.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For GT AND MT
                            }
                            else
                            {
                                ShowReport_GT("BillOfSupply_IGST.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For GT AND MT
                            }
                        }
                        else if (Request.QueryString["BSID"].ToString().Trim() == "GTMT" && PSID == "SALERETURN_GT_MT")
                        {
                            ShowReport_SaleReturn_GT("SaleReturn_Challan.rdlc", "", ref ht, ref RptHt, "", invoiceid);

                        }
                        else if (Request.QueryString["BSID"].ToString().Trim() == "C5038911-9331-40CF-B7F9-583D50583592")
                        {
                            if (Request.QueryString["ewaybill"].ToString() == "5")
                            {
                                ShowReport_EWayBill("Ewaybill.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For CSD

                            }
                            else
                            {
                                if (Request.QueryString["backpsheet"].ToString() == "0")
                                {
                                    ShowReport_CSD("DeliveryChallan_CSD.rdlc", "", ref ht, ref RptHt, "", invoiceid);    //For CSD

                                }
                                else
                                {
                                    ShowReport_CSD("CSD_BackupSheet.rdlc", "", ref ht, ref RptHt, "", invoiceid);    //For CSD_BackupSheet
                                }
                            }

                        }
                        else if (Request.QueryString["BSID"].ToString().Trim() == "A0A1E83E-1993-4FB9-AF53-9DD595D09596")
                        {
                            if (Request.QueryString["ewaybill"].ToString() == "5")
                            {
                                ShowReport_EWayBill("Ewaybill.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For CPC

                            }
                            else
                            {
                                ShowReport_CPC("DeliveryChallan_CPC.rdlc", "", ref ht, ref RptHt, "", invoiceid);   //For CPC
                            }

                        }
                        else if (Request.QueryString["BSID"].ToString().Trim() == "8AE0E8C9-F4F7-4382-B8AB-870A64ABF996")
                        {
                            if (Request.QueryString["ewaybill"].ToString() == "5")
                            {
                                ShowReport_EWayBill("Ewaybill.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For INCS

                            }
                            else
                            {
                                ShowReport_CPC("DeliveryChallan_INCS.rdlc", "", ref ht, ref RptHt, "", invoiceid);   //For INCS
                            }

                        }
                        else if (Request.QueryString["BSID"].ToString().Trim() == "2E96A0A4-6256-472C-BE4F-C59599C948B0")
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
                        }
                    }
                    if (dtheader.Rows[0]["INVOICETYPEID"].ToString() == "1")
                    {
                        if (Request.QueryString["BSID"].ToString().Trim() == "7F62F951-9D1F-4B8D-803B-74EEBA468CEE" || Request.QueryString["BSID"].ToString().Trim() == "0AA9353F-D350-4380-BC84-6ED5D0031E24" || Request.QueryString["BSID"].ToString().Trim() == "A18FE04D-057C-4A88-9204-5FE1613626C9" || Request.QueryString["BSID"].ToString().Trim() == "33F6AC5E-1F37-4B0F-B959-D1C900BB43A5" || Request.QueryString["BSID"].ToString().Trim() == "91B9CC35-AF4F-4732-92B1-433C68104DE6" || Request.QueryString["BSID"].ToString().Trim() == "97547CB6-F40B-4B43-923D-B63F61A910C2")
                        {
                            if (Request.QueryString["ewaybill"].ToString() == "5")
                            {
                                ShowReport_EWayBill("Ewaybill.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For GT AND MT

                            }
                            else
                            {
                                ShowReport_GT("SaleInvoice_IGST.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For GT AND MT
                            }

                        }
                        else if (Request.QueryString["BSID"].ToString().Trim() == "GTMT" && PSID == "SALERETURN_GT_MT")
                        {
                            //ShowReport_SaleReturn_GT("SaleReturn.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For GT AND MT
                            ShowReport_GT("SaleInvoice_IGST.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                        }
                        else if (Request.QueryString["BSID"].ToString().Trim() == "C5038911-9331-40CF-B7F9-583D50583592")
                        {
                            if (Request.QueryString["ewaybill"].ToString() == "5")
                            {
                                ShowReport_EWayBill("Ewaybill.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For CSD

                            }
                            else
                            {
                                if (Request.QueryString["backpsheet"].ToString() == "0")
                                {
                                    ShowReport_CSD("SaleInvoice_CSD_IGST.rdlc", "", ref ht, ref RptHt, "", invoiceid);    //For CSD
                                }
                                else
                                {
                                    ShowReport_CSD("CSD_BackupSheet.rdlc", "", ref ht, ref RptHt, "", invoiceid);    //For CSD_BackupSheet
                                }
                            }
                        }
                        /*Added By Avishek On 10-06-2018*/
                        else if (Request.QueryString["BSID"].ToString().Trim() == "62240D9B-F7FC-4259-AECE-B15D1018A972")
                        {
                            if (Request.QueryString["ewaybill"].ToString() == "5")
                            {
                                ShowReport_EWayBill("Ewaybill.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For CORPORATE

                            }
                            else
                            {
                                if (Request.QueryString["backpsheet"].ToString() == "0")
                                {
                                    ShowReport_Corporate("SaleInvoice_Corporate_IGST.rdlc", "", ref ht, ref RptHt, "", invoiceid);    //For CORPORATE
                                }
                                else
                                {
                                    ShowReport_Corporate("CSD_BackupSheet.rdlc", "", ref ht, ref RptHt, "", invoiceid);    //For CSD_BackupSheet
                                }
                            }
                        }
                        else if (Request.QueryString["BSID"].ToString().Trim() == "A0A1E83E-1993-4FB9-AF53-9DD595D09596")
                        {
                            if (Request.QueryString["ewaybill"].ToString() == "5")
                            {
                                ShowReport_EWayBill("Ewaybill.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For CPC

                            }
                            else
                            {
                                //ShowReport_CPC("SaleInvoice_CPC.rdlc", "", ref ht, ref RptHt, "", invoiceid);   //For CPC
                                ShowReport_CPC("SaleInvoice_CPC_IGST.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                            }
                        }
                        else if (Request.QueryString["BSID"].ToString().Trim() == Convert.ToString(ViewState["_ParamBSegment"]).Trim())
                        {
                            if (Request.QueryString["ewaybill"].ToString() == "5")
                            {
                                ShowReport_EWayBill("Ewaybill.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For Ex-para-military

                            }
                            else
                            {
                                //For Ex-para-military
                                ShowReport_CPC("SaleInvoice_EXP_IGST.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                            }
                        }

                        else if (Request.QueryString["BSID"].ToString().Trim() == "8AE0E8C9-F4F7-4382-B8AB-870A64ABF996") /*INCS*/
                        {
                            if (Request.QueryString["ewaybill"].ToString() == "5")
                            {
                                ShowReport_EWayBill("Ewaybill.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For INCS

                            }
                            else
                            {
                                ShowReport_CPC("SaleInvoice_INCS_IGST.rdlc", "", ref ht, ref RptHt, "", invoiceid);   //For INCS
                            }
                        }

                        else if (Request.QueryString["BSID"].ToString().Trim() == "2E96A0A4-6256-472C-BE4F-C59599C948B0")
                        {
                            if (PSID == "EXPORT")
                            {
                                ShowReport_EXPORT("ExportInvoice.rdlc", "", ref ht, ref RptHt, "", invoiceid);    //For EXPORT
                                //ShowReport_GT("SaleInvoice_IGST.rdlc", "", ref ht, ref RptHt, "", invoiceid);
                            }
                            if (PSID == "PROFORMA")
                            {
                                ShowReport_Proforma("ProformaInvoice.rdlc", "", ref ht, ref RptHt, "", invoiceid);  //For PROFORMA
                            }
                            if (PSID == "PACKING")
                            {
                                ShowReport_PackingList("PackingList.rdlc", "", ref ht, ref RptHt, "", invoiceid);  //For PACKING LIST
                            }
                        }

                    }
                    if (dtheader.Rows[0]["INVOICETYPEID"].ToString() == "2")
                    {
                        if (Request.QueryString["BSID"].ToString().Trim() == "7F62F951-9D1F-4B8D-803B-74EEBA468CEE" || Request.QueryString["BSID"].ToString().Trim() == "0AA9353F-D350-4380-BC84-6ED5D0031E24" || Request.QueryString["BSID"].ToString().Trim() == "A18FE04D-057C-4A88-9204-5FE1613626C9" || Request.QueryString["BSID"].ToString().Trim() == "33F6AC5E-1F37-4B0F-B959-D1C900BB43A5" || Request.QueryString["BSID"].ToString().Trim() == "91B9CC35-AF4F-4732-92B1-433C68104DE6" || Request.QueryString["BSID"].ToString().Trim() == "97547CB6-F40B-4B43-923D-B63F61A910C2")
                        {


                            if (Request.QueryString["ewaybill"].ToString() == "5")
                            {
                                ShowReport_EWayBill("Ewaybill.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For GT AND MT

                            }
                            else
                            {
                                string scheme = string.Empty;
                                if (Convert.ToString(Request.QueryString["BSID"]).Trim() != "33F6AC5E-1F37-4B0F-B959-D1C900BB43A5")/*Trading*/
                                {
                                    scheme = clsrpt.Bind_Scheme_Coin(invoiceid);
                                }
                                if (scheme == "COIN_SCHEME")
                                {
                                    ShowReport_GT("SaleInvoice_GST_SCHEME.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For GT AND MT
                                }
                                else
                                {
                                    ShowReport_GT("SaleInvoice_GST.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For GT AND MT
                                }
                            }
                        }
                        else if (Request.QueryString["BSID"].ToString().Trim() == "GTMT" && PSID == "SALERETURN_GT_MT")
                        {
                            ShowReport_SaleReturn_GT("SaleReturn_GST.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For GT AND MT

                        }
                        else if (Request.QueryString["BSID"].ToString().Trim() == "C5038911-9331-40CF-B7F9-583D50583592")
                        {
                            if (Request.QueryString["ewaybill"].ToString() == "5")
                            {
                                ShowReport_EWayBill("Ewaybill.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For GT AND MT
                            }
                            else
                            {
                                if (Request.QueryString["backpsheet"].ToString() == "0")
                                {
                                    ShowReport_CSD("SaleInvoice_CSD_GST.rdlc", "", ref ht, ref RptHt, "", invoiceid);    //For CSD
                                }
                                else
                                {
                                    ShowReport_CSD("CSD_BackupSheet.rdlc", "", ref ht, ref RptHt, "", invoiceid);    //For CSD_BackupSheet
                                }
                            }

                        }
                        /*Added By Avishek On 10-06-2018*/
                        else if (Request.QueryString["BSID"].ToString().Trim() == "62240D9B-F7FC-4259-AECE-B15D1018A972")
                        {
                            if (Request.QueryString["ewaybill"].ToString() == "5")
                            {
                                ShowReport_EWayBill("Ewaybill.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //Corporate
                            }
                            else
                            {
                                if (Request.QueryString["backpsheet"].ToString() == "0")
                                {
                                    ShowReport_Corporate("SaleInvoice_Corporate_GST.rdlc", "", ref ht, ref RptHt, "", invoiceid);    //For CORPORATE
                                }
                                else
                                {
                                    ShowReport_Corporate("CSD_BackupSheet.rdlc", "", ref ht, ref RptHt, "", invoiceid);    //For CSD_BackupSheet
                                }
                            }

                        }
                        else if (Request.QueryString["BSID"].ToString().Trim() == "A0A1E83E-1993-4FB9-AF53-9DD595D09596")
                        {
                            if (Request.QueryString["ewaybill"].ToString() == "5")
                            {
                                ShowReport_EWayBill("Ewaybill.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For GT AND MT

                            }
                            else
                            {

                                ShowReport_CPC("SaleInvoice_CPC_GST.rdlc", "", ref ht, ref RptHt, "", invoiceid);   //For CPC
                            }

                        }

                        else if (Request.QueryString["BSID"].ToString().Trim() == Convert.ToString(ViewState["_ParamBSegment"]).Trim())
                        {
                            if (Request.QueryString["ewaybill"].ToString() == "5")
                            {
                                ShowReport_EWayBill("Ewaybill.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For Ex-para-military
                            }
                            else
                            {

                                ShowReport_CPC("SaleInvoice_EXP_GST.rdlc", "", ref ht, ref RptHt, "", invoiceid);   //For Ex-para-military
                            }

                        }
                        else if (Request.QueryString["BSID"].ToString().Trim() == "8AE0E8C9-F4F7-4382-B8AB-870A64ABF996") /*INCS*/
                        {
                            if (Request.QueryString["ewaybill"].ToString() == "5")
                            {
                                ShowReport_EWayBill("Ewaybill.rdlc", "", ref ht, ref RptHt, "", invoiceid);        //For INCS

                            }
                            else
                            {
                                ShowReport_CPC("SaleInvoice_INCS_GST.rdlc", "", ref ht, ref RptHt, "", invoiceid);   //For INCS
                            }
                        }
                        else if (Request.QueryString["BSID"].ToString().Trim() == "2E96A0A4-6256-472C-BE4F-C59599C948B0")
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

        #region ShowReport_Proforma
        private void ShowReport_Proforma(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string invoiceid)
        {
            DataTable dt_InvoiceDetails = new DataTable();
            DataTable dt_Comp_info = new DataTable();
            DataTable dt_terms = new DataTable();


            ClsStockReport clsrpt = new ClsStockReport();

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

        #region ShowReport_PackingList
        private void ShowReport_PackingList(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string invoiceid)
        {
            DataTable dt_InvoiceDetails = new DataTable();
            DataTable dt_Comp_info = new DataTable();
            DataTable dt_userinfo = new DataTable();
            DataTable dt_terms = new DataTable();


            ClsStockReport clsrpt = new ClsStockReport();

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

        #region ShowReport_EXPORT
        private void ShowReport_EXPORT(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string invoiceid)
        {
            DataTable dt_InvoiceDetails = new DataTable();
            DataTable dt_Comp_info = new DataTable();


            ClsStockReport clsrpt = new ClsStockReport();

            dt_InvoiceDetails = clsrpt.Bind_ExportInvoice(invoiceid.Trim());
            dt_Comp_info = clsrpt.Bind_CompanyInfo();


            this.ReportViewer1.LocalReport.EnableExternalImages = true;
            if (rptType == "") //Normal View
            {
                this.ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource();

                this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("COMP_INFO", dt_Comp_info));
                this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DST_EXPORTINVOICE", dt_InvoiceDetails));

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

        #region ShowReport_GT
        private void ShowReport_GT(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string invoiceid)
        {

            DataTable dtdetails = new DataTable();
            DataSet dtheader = new DataSet();


            ClsStockReport clsrpt = new ClsStockReport();
            dtdetails = clsrpt.Bind_InvoiceDetails_GST_Print(invoiceid.Trim(), Request.QueryString["original"].ToString(), Request.QueryString["duplicate"].ToString(), Request.QueryString["triplicate"].ToString(), Request.QueryString["backpsheet"].ToString());
            dtheader = clsrpt.Bind_InvoiceHeader_Print(invoiceid.Trim());


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

        #region ShowReport_EWayBill
        private void ShowReport_EWayBill(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string invoiceid)
        {

            DataTable dtdetails = new DataTable();
            DataSet dtheader = new DataSet();


            ClsStockReport clsrpt = new ClsStockReport();
            dtdetails = clsrpt.BindHSNInvoice(invoiceid.Trim());
            dtheader = clsrpt.Bind_InvoiceHeader_Print(invoiceid.Trim());


            this.ReportViewer1.LocalReport.EnableExternalImages = true;

            if (rptType == "") //Normal View
            {
                this.ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource();
                this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("EWAYBILL", dtdetails));
                this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HEADER", dtheader.Tables[0]));
                this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("FOOTER", dtheader.Tables[1]));
                this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DISTRIBUTOR_INFO", dtheader.Tables[3]));
                this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("COMPANY_INFO", dtheader.Tables[2]));
                this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("SALEINVOICE_FROMDEPOT", dtheader.Tables[4]));

                this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports/" + ReportFile);


                ReportParameter rp1 = new ReportParameter("p_INVOICEID", invoiceid.Trim());
                ReportParameter rp2 = new ReportParameter("p_InvoiceType_original", Request.QueryString["original"].ToString());
                ReportParameter rp3 = new ReportParameter("p_InvoiceType_duplicate", Request.QueryString["duplicate"].ToString());
                ReportParameter rp4 = new ReportParameter("p_InvoiceType_triplicate", Request.QueryString["triplicate"].ToString());
                ReportParameter rp5 = new ReportParameter("p_InvoiceType_backup", Request.QueryString["backpsheet"].ToString());
                ReportParameter rp6 = new ReportParameter("p_ewaybill", Request.QueryString["backpsheet"].ToString());
                ReportParameter rp7 = new ReportParameter("pcount", Request.QueryString["pcount"].ToString());
                ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1 });
                ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp2 });
                ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp3 });
                ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp4 });
                ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp5 });
                ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp6 });
                ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp7 });


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

        #region ShowReport_CPC
        private void ShowReport_CPC(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string invoiceid)
        {

            DataTable dtdetails = new DataTable();
            DataSet dtheader = new DataSet();
            DataTable dt_COMBO = new DataTable();



            ClsStockReport clsrpt = new ClsStockReport();
            dtdetails = clsrpt.Bind_InvoiceDetails_CPC_GST_Print(invoiceid.Trim(), Request.QueryString["original"].ToString(), Request.QueryString["duplicate"].ToString(), Request.QueryString["triplicate"].ToString(), Request.QueryString["backpsheet"].ToString());
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

        #region ShowReport_CSD
        private void ShowReport_CSD(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string invoiceid)
        {

            DataTable dtdetails = new DataTable();
            DataSet dtheader = new DataSet();
            DataTable dt_COMBO = new DataTable();



            ClsStockReport clsrpt = new ClsStockReport();
            if (Request.QueryString["backpsheet"].ToString() == "4")
            {
                dtdetails = clsrpt.Bind_InvoiceDetails_CPC_GST_Print(invoiceid.Trim(), "1", Request.QueryString["duplicate"].ToString(), Request.QueryString["triplicate"].ToString(), Request.QueryString["backpsheet"].ToString());
            }
            else
            {
                dtdetails = clsrpt.Bind_InvoiceDetails_CPC_GST_Print(invoiceid.Trim(), Request.QueryString["original"].ToString(), Request.QueryString["duplicate"].ToString(), Request.QueryString["triplicate"].ToString(), Request.QueryString["backpsheet"].ToString());

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

        #region ShowReport_Corporate
        /*Added By Avishek On 10-06-2018*/
        private void ShowReport_Corporate(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string invoiceid)
        {

            DataTable dtdetails = new DataTable();
            DataSet dtheader = new DataSet();
            DataTable dt_COMBO = new DataTable();



            ClsStockReport clsrpt = new ClsStockReport();
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

        #region ShowReport_StockTransfer
        private void ShowReport_StockTransfer(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string Invoiceid)
        {
            DataTable dtdetails = new DataTable();
            DataTable dt_Comp_info = new DataTable();


            ClsStockReport clsrpt = new ClsStockReport();
            dtdetails = clsrpt.BindST_Details_GST(Invoiceid.Trim(), Request.QueryString["original"].ToString(), Request.QueryString["duplicate"].ToString(), Request.QueryString["triplicate"].ToString(), Request.QueryString["backpsheet"].ToString());
            dt_Comp_info = clsrpt.Bind_CompanyInfo();

            /********************New Method******************/
            DataSet dsheaderprint = new DataSet();
            dsheaderprint = clsrpt.Bind_StockTransferHeader_Print(Invoiceid.Trim());

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

                this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports/" + ReportFile);

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
            ClsStockReport clsrpt = new ClsStockReport();
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

        #region ShowReport_Advance_Receipt(Voucher)
        private void ShowReport_AdvanceReceipt(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string AdvRecpt_ID, string Voucherid)
        {
            DataTable dt_accountsdebitnote = new DataTable();
            DataTable dt_Comp_info = new DataTable();

            ClsStockReport clsrpt = new ClsStockReport();

            dt_accountsdebitnote = clsrpt.BindDebitNote(Voucherid, AdvRecpt_ID.Trim());
            dt_Comp_info = clsrpt.Bind_CompanyInfo();

            this.ReportViewer1.LocalReport.EnableExternalImages = true;
            if (rptType == "") //Normal View
            {
                this.ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource();

                this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("COMP_INFO", dt_Comp_info));
                this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("VOUCHER_DETAILS", dt_accountsdebitnote));

                this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports/" + ReportFile);

                ReportParameter rp1 = new ReportParameter("p_VOUCHERTYPE", Voucherid);
                ReportParameter rp2 = new ReportParameter("p_VOUCHERNO", AdvRecpt_ID.Trim());
                ReportParameter rp3 = new ReportParameter("p_UserType", Session["TPU"].ToString());
                ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });

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

        #region ShowReport_DebitNote
        private void ShowReport_DebitNote(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string DebtNote_ID, string Voucherid)
        {
            DataTable dt_accountsdebitnote = new DataTable();
            DataTable dt_Comp_info = new DataTable();
            ClsStockReport clsrpt = new ClsStockReport();

            dt_accountsdebitnote = clsrpt.BindDebitNote(Voucherid.Trim(), DebtNote_ID.Trim());
            dt_Comp_info = clsrpt.Bind_CompanyInfo();

            this.ReportViewer1.LocalReport.EnableExternalImages = true;
            if (rptType == "") //Normal View
            {
                this.ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource();

                this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Comp_Info", dt_Comp_info));
                this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Ds_Accounts", dt_accountsdebitnote));

                this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports/" + ReportFile);

                ReportParameter rp1 = new ReportParameter("p_VOUCHERTYPE", Voucherid.Trim());
                ReportParameter rp2 = new ReportParameter("p_VOUCHERNO", DebtNote_ID.Trim());
                ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2 });

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
                    Response.AddHeader("content-disposition", "inline; filename= Debit.pdf");
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

        #region ShowReport_Journal
        private void ShowReport_Journal(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string Journal_ID, string Voucherid)
        {
            DataTable dt_accountsdebitnote = new DataTable();
            DataTable dt_Comp_info = new DataTable();
            ClsStockReport clsrpt = new ClsStockReport();

            dt_accountsdebitnote = clsrpt.BindJournal(Voucherid.Trim(), Journal_ID.Trim());
            dt_Comp_info = clsrpt.Bind_CompanyInfo();

            this.ReportViewer1.LocalReport.EnableExternalImages = true;
            if (rptType == "") //Normal View
            {
                this.ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource();

                this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("COMP_INFO", dt_Comp_info));
                this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("JOURNAL_DETAILS", dt_accountsdebitnote));

                this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports/" + ReportFile);

                ReportParameter rp1 = new ReportParameter("p_VOUCHERTYPE", Voucherid.Trim());
                ReportParameter rp2 = new ReportParameter("p_VOUCHERNO", Journal_ID.Trim());
                ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2 });

                //Response.AddHeader("content-disposition", "inline; filename=PackingList.pdf");

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
                    Response.AddHeader("content-disposition", "inline; filename= Journal.pdf");
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

            DataTable dtdetails = new DataTable();
            DataTable dtheader = new DataTable();
            DataTable dtfooter = new DataTable();
            DataTable dt_pricescheme = new DataTable();
            DataTable dttax = new DataTable();
            DataTable dt_DistributorDetails = new DataTable();
            DataTable dt_Comp_info = new DataTable();



            ClsStockReport clsrpt = new ClsStockReport();

            dtdetails = clsrpt.Bind_InvoiceDetails_SaleReturn(invoiceid.Trim());
            dtheader = clsrpt.Bind_InvoiceHeader_SaleReturn(invoiceid.Trim());
            dttax = clsrpt.Bind_InvoiceTax_SaleReturn(invoiceid.Trim());
            dtfooter = clsrpt.Bind_InvoiceFooter_SaleReturn(invoiceid.Trim());
            dt_pricescheme = clsrpt.Bind_InvoicePriceScheme_SaleReturn(invoiceid.Trim());
            dt_DistributorDetails = clsrpt.Bind_InvoiceDistributor_SaleReturn(invoiceid.Trim());
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
                this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("PRICE_SCHEME", dt_pricescheme));
                this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DISTRIBUTOR_INFO", dt_DistributorDetails));
                this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("COMPANY_INFO", dt_Comp_info));


                this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports/" + ReportFile);


                ReportParameter rp1 = new ReportParameter("P_SALERETURNID", invoiceid.Trim());
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

        #region ShowReport_PurchaseBill
        private void ShowReport_PurchaseBill(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string invoiceid)
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
            dtdetails = clsrpt.BindPurchaseDetails_GST(invoiceid.Trim());
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

                this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports/" + ReportFile);


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

        #region ShowReport_PurchaseBill
        private void ShowReport_PurchaseBill_VAT(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string invoiceid)
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
            dtdetails = clsrpt.BindPurchaseDetails(invoiceid.Trim());
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

                this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports/" + ReportFile);


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

        #region BillEntry_GST
        private void BillEntry_GST(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string invoiceid)
        {

            DataTable dtdetails = new DataTable();
            DataTable dtheader = new DataTable();
            DataTable dtfooter = new DataTable();
            DataTable dt_Comp_info = new DataTable();

            ClsStockReport clsrpt = new ClsStockReport();
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

        #region ShowReport_StockJournal
        private void ShowReport_StockJournal(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string invoiceid)
        {

            DataTable dtdetails = new DataTable();
            DataTable dtheader = new DataTable();
            DataTable dt_Comp_info = new DataTable();
            DataTable dt_COMBO = new DataTable();



            ClsStockReport clsrpt = new ClsStockReport();
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

                this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports/" + ReportFile);




                string format = "PDF",
                      devInfo =
    @"<DeviceInfo><Toolbar>True</Toolbar></DeviceInfo>";

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

        #region ShowReport_EWayBillTransfer
        private void ShowReport_EWayBillTransfer(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string Invoiceid)
        {
            DataTable dtdetails = new DataTable();
            DataTable dt_Comp_info = new DataTable();


            ClsStockReport clsrpt = new ClsStockReport();
            dtdetails = clsrpt.BindHSNStkTransfer(Invoiceid.Trim());
            dt_Comp_info = clsrpt.Bind_CompanyInfo();

            /********************New Method******************/
            DataSet dsheaderprint = new DataSet();
            dsheaderprint = clsrpt.Bind_StockTransferHeader_Print(Invoiceid.Trim());

            /************************************************/

            this.ReportViewer1.LocalReport.EnableExternalImages = true;
            if (rptType == "") //Normal View
            {
                this.ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource();
                this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("EWayBill", dtdetails));//
                this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Header", dsheaderprint.Tables[0]));
                this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Footer", dsheaderprint.Tables[1]));
                this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("FROMDEPOT", dsheaderprint.Tables[2]));
                this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("TODEPOT", dsheaderprint.Tables[3]));
                this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("COMPANY_INFO", dt_Comp_info));

                this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports/" + ReportFile);

                ReportParameter rp1 = new ReportParameter("p_STNO", Invoiceid.Trim());
                ReportParameter rp2 = new ReportParameter("p_InvoiceType_original", Request.QueryString["original"].ToString());
                ReportParameter rp3 = new ReportParameter("p_InvoiceType_duplicate", Request.QueryString["duplicate"].ToString());
                ReportParameter rp4 = new ReportParameter("p_InvoiceType_triplicate", Request.QueryString["triplicate"].ToString());
                ReportParameter rp5 = new ReportParameter("p_InvoiceType_backup", Request.QueryString["backpsheet"].ToString());
                ReportParameter rp6 = new ReportParameter("p_ewaybill", Request.QueryString["ewaybill"].ToString());
                ReportParameter rp7 = new ReportParameter("pcount", Request.QueryString["pcount"].ToString());
                ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1 });
                ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp2 });
                ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp3 });
                ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp4 });
                ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp5 });
                ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp6 });
                ReportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp7 });

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

        #region Gross_return_gst
        private void Gross_return_gst(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string invoiceid)
        {

            DataTable dtdetails = new DataTable();
            DataTable dtheader = new DataTable();
            DataTable dtfooter = new DataTable();
            DataTable dt_Comp_info = new DataTable();
            DataTable dt_distributor_info = new DataTable();
            DataTable dt_saleinvoice_fromdepot = new DataTable();

            ClsStockReport clsrpt = new ClsStockReport();
            dtdetails = clsrpt.BindSalereturnDetails(invoiceid.Trim(), Request.QueryString["original"].ToString(), Request.QueryString["duplicate"].ToString(), Request.QueryString["triplicate"].ToString(), Request.QueryString["backpsheet"].ToString());
            dtheader = clsrpt.BindSalereturnHeader(invoiceid.Trim());
            dtfooter = clsrpt.BindSalereturnFooter(invoiceid.Trim());
            dt_Comp_info = clsrpt.Bind_CompanyInfo();
            dt_distributor_info = clsrpt.Bind_InvoiceDistributorReturn(invoiceid.Trim());
            dt_saleinvoice_fromdepot = clsrpt.Bind_ReturnFromDepot(invoiceid.Trim());

            this.ReportViewer1.LocalReport.EnableExternalImages = true;

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

        #region ShowReport_PO
        private void ShowReport_PO(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string POID)
        {

            DataTable dtdetails = new DataTable();
            DataTable dtheader = new DataTable();
            DataTable dt_Comp_info = new DataTable();


            ClsStockReport clsrpt = new ClsStockReport();
            dtdetails = clsrpt.BindPODetails(POID);
            dtheader = clsrpt.BindPOHeader(POID);
            dt_Comp_info = clsrpt.Bind_CompanyInfo();


            this.ReportViewer1.LocalReport.EnableExternalImages = true;

            this.ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource();
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DETAILS", dtdetails));//
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("HEADER", dtheader));
            this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("COMP_INFO", dt_Comp_info));

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

        #region Purchase_return_gst
        private void Purchase_return_gst(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string invoiceid)
        {

            DataTable dtdetails = new DataTable();
            DataTable dtheader = new DataTable();
            DataTable dtfooter = new DataTable();
            DataTable dt_Comp_info = new DataTable();
            DataTable dt_distributor_info = new DataTable();
            DataTable dt_saleinvoice_fromdepot = new DataTable();

            ClsStockReport clsrpt = new ClsStockReport();
            dtdetails = clsrpt.BindSalereturnDetailsPurchase(invoiceid.Trim(), Request.QueryString["original"].ToString(), Request.QueryString["duplicate"].ToString(), Request.QueryString["triplicate"].ToString(), Request.QueryString["backpsheet"].ToString());
            dtheader = clsrpt.BindSalereturnHeader(invoiceid.Trim());
            dtfooter = clsrpt.BindSalereturnFooter(invoiceid.Trim());
            dt_Comp_info = clsrpt.Bind_CompanyInfo();
            dt_distributor_info = clsrpt.Bind_InvoiceDistributorReturn(invoiceid.Trim());
            dt_saleinvoice_fromdepot = clsrpt.Bind_ReturnFromDepot(invoiceid.Trim());

            this.ReportViewer1.LocalReport.EnableExternalImages = true;

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
                    Response.Buffer = true;
                    Response.Clear();
                    Response.ContentType = mimeType;
                    Response.AddHeader("content-disposition", "inline; filename= Purchase_Return.pdf");
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
        private void ShowReport_TPUStockJournal(string ReportFile, string spName, ref Hashtable htParam, ref Hashtable RptHtParam, string rptType, string invoiceid)
        {
            DataTable dtdetails = new DataTable();
            DataTable dtheader = new DataTable();
            DataTable dt_Comp_info = new DataTable();
            DataTable dt_COMBO = new DataTable();

            ClsStockReport clsrpt = new ClsStockReport();
            dtdetails = clsrpt.BindStockjournaldetails(invoiceid.Trim());
            dtheader = clsrpt.BindStockjournalheader(invoiceid.Trim());
            dt_Comp_info = clsrpt.Bind_TPUInfo(Session["TPUID"].ToString());

            this.ReportViewer1.LocalReport.EnableExternalImages = true;
            if (rptType == "") //Normal View
            {
                this.ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource();
                this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("stockjournaldetails", dtdetails));
                this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("stockjournalheader", dtheader));
                this.ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Comp_Info", dt_Comp_info));

                this.ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/DMSReports/" + ReportFile);

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
                    result = ReportViewer1.LocalReport.Render(format, devInfo, out mimeType, out encoding, out fileNameExtn, out stearms, out warnings);
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
    }