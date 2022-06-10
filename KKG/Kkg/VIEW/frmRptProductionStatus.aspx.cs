using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PPBLL;

public partial class VIEW_frmRptProductionStatus : System.Web.UI.Page
{
    DateTime today1 = DateTime.Now;
    string date = "dd/MM/yyyy";
    DataTable objDt = new DataTable();
    ClsProductionOrder objClsProductionOrder = new ClsProductionOrder();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            this.DateLock();
            //date = Convert.ToString(today1);
            //this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            //this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');

        }
        catch (Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }

    }

    #region Add FinYear Wise Date Lock
    public void DateLock()
    {
        try
        {
            string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
            string startyear = finyear.Substring(0, 4);
            int startyear1 = Convert.ToInt32(startyear);
            string endyear = finyear.Substring(5);
            int endyear1 = Convert.ToInt32(endyear);
            DateTime oDate = new DateTime(startyear1, 04, 01);
            DateTime cDate = new DateTime(endyear1, 03, 31);
            DateTime today1 = DateTime.Now;
           
            CalendarExtender1.StartDate = oDate;
            CalendarExtenderToDate.StartDate = oDate;


            if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
            {
               
                this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');

              
                CalendarExtender1.EndDate = today1;
                CalendarExtenderToDate.EndDate = today1;

            }
            else
            {
               
                this.txtFromDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtToDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');

               
                CalendarExtenderToDate.EndDate = cDate;
                CalendarExtender1.EndDate = cDate;

            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            objDt = objClsProductionOrder.fettchProductionReport(this.txtFromDate.Text, this.txtToDate.Text, HttpContext.Current.Session["FINYEAR"].ToString());
            if(objDt.Rows.Count>0)
            {
                this.grdProduction.DataSource = objDt;
                this.grdProduction.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }
    }
}