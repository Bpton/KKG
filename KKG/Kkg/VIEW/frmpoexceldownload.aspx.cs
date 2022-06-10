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

public partial class VIEW_frmpoexceldownload : System.Web.UI.Page
{

    Hashtable ht = new Hashtable();
    Hashtable RptHt = new Hashtable();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["POID"] = Request.QueryString["POID"];
        string POID = Request.QueryString["POID"];
        

    }

    protected void btnpdf_ServerClick(object sender, EventArgs e)
    {
        string POID = Request.QueryString["POID"];

        //this.ShowReport_PO("rptPurchaseOrderexcel1.rdlc", "", ref ht, ref RptHt, POID);
        //this.ShowReport_PO("rptPurchaseOrder.rdlc", "", ref ht, ref RptHt, POID);
        this.ShowReport_PO("PrchaseOrderReport.rdlc", "", ref ht, ref RptHt, POID);
    }

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

    }
}