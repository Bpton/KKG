using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Account;

public partial class VIEW_frmFinalChequeBookletCancel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.LoadBankName();
                Session["BOOKLETDETAILS"] = null;
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #region LoadBankName
    public void LoadBankName()
    {
        try
        {
            ClsVoucherEntry clsreport = new ClsVoucherEntry();
            DataTable dt = clsreport.LoadBankName("Bank", "0");
            ddlBankName.Items.Clear();
            ddlBankName.Items.Add(new ListItem("Select All", "0"));
            ddlBankName.AppendDataBoundItems = true;
            ddlBankName.DataSource = dt;
            ddlBankName.DataValueField = "BANKID";
            ddlBankName.DataTextField = "BANKNAME";
            ddlBankName.DataBind();
            ddlBookletNo.Items.Add(new ListItem("Select All", "0"));

        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
    #region LoadBookletNo
    protected void ddlBankName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LoadBookletNo();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    public void LoadBookletNo()
    {
        try
        {
            ClsVoucherEntry clsreport = new ClsVoucherEntry();
            DataTable dt = clsreport.LoadBankName("Booklet", ddlBankName.SelectedValue.ToString());
            ddlBookletNo.Items.Clear();
            ddlBookletNo.Items.Add(new ListItem("Select All", "0"));
            ddlBookletNo.AppendDataBoundItems = true;
            ddlBookletNo.DataSource = dt;
            ddlBookletNo.DataValueField = "ChequeBookletID";
            ddlBookletNo.DataTextField = "BookletNo";
            ddlBookletNo.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion  

    #region LoadVoucher
    public void BindGrid()
    {
        try
        {
            ClsVoucherEntry clsreport = new ClsVoucherEntry();
            DataTable dt = new DataTable();
            string str = "FCancel";
            dt = clsreport.BindChequeBooklet(ddlBankName.SelectedValue.ToString(), str, ddlBookletNo.SelectedValue.ToString());
            if (dt.Rows.Count > 0)
            {
                this.gvUnlockVoucher.DataSource = dt;
                this.gvUnlockVoucher.DataBind();
            }
            else
            {
                this.gvUnlockVoucher.DataSource = null;
                this.gvUnlockVoucher.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }
    #endregion




    #region Search
    public void btnSearchInvoice_Click(object sender, EventArgs e)
    {
        try
        {
            BindGrid();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {

            this.ResetGrid();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    public void ResetGrid()
    {
        try
        {
            this.gvUnlockVoucher.DataSource = null;
            this.gvUnlockVoucher.DataBind();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable CreateTable()
    {
        DataTable dtbank = new DataTable();
        dtbank.Clear();
        dtbank.Columns.Add("BOOKLETNO");
        dtbank.Columns.Add("AUTOBOOKLETNO");
        dtbank.Columns.Add("CHEQUENO");
        dtbank.Columns.Add("REMARKS");
        HttpContext.Current.Session["BOOKLETDETAILS"] = dtbank;
        return dtbank;
    }
    #region Convert DataTable To XML
    public string ConvertDatatableToXML(DataTable dt)
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
    public void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            ClsVoucherEntry clsVoucher1 = new ClsVoucherEntry();
            int totalcount = 0;
            GridView gvunlock = gvUnlockVoucher;
            string gvchkbox = "";
            string gvAutoBookLetNo = "";
            string gvRemarks = "";
            string BOOKLETNO = "";
            string ChequeNo = "";
            string Remarks = "";

            gvunlock = gvUnlockVoucher;
            gvchkbox = "chkSelectvou";

            gvAutoBookLetNo = "LBLAutoBookletNo";
            gvRemarks = "txtremarks";
            BOOKLETNO = "lblBOOKLETNO";
            ChequeNo = "lblCheqNumber";
            Remarks = "txtremarks";

            
            if (totalcount <= 500)
            {
                
                ClsVoucherEntry clsVoucher = new ClsVoucherEntry();
                int flag = 0;
                int totalapproval = 0;
                int successcount = 0;
                int failcount = 0;
                string SALEINVOICEID = string.Empty;

                this.CreateTable();
                DataTable dtBooklet = (DataTable)HttpContext.Current.Session["BOOKLETDETAILS"];
                string IPAddress = Request.UserHostAddress.ToString().Trim();

                foreach (GridViewRow gvrow in gvunlock.Rows)
                {
                    CheckBox chkBx = (CheckBox)gvrow.FindControl("" + gvchkbox + "");
                    Label lblAutoBookLetNo = (Label)gvrow.FindControl("" + gvAutoBookLetNo + "");
                    TextBox txtRemarks = (TextBox)gvrow.FindControl("" + gvRemarks + "");
                    Label lblBOOKLETNO = (Label)gvrow.FindControl("" + BOOKLETNO + "");
                    Label lblChequeNo = (Label)gvrow.FindControl("" + ChequeNo + "");
                    TextBox txRemarks = (TextBox)gvrow.FindControl("" + Remarks + "");

                    if (chkBx.Checked)
                    {
                        totalcount += 1;
                        totalapproval += 1;

                        DataRow dr = dtBooklet.NewRow();

                        dr["BOOKLETNO"] = lblBOOKLETNO.Text.ToString();
                        dr["AUTOBOOKLETNO"] = lblAutoBookLetNo.Text.ToString();
                        dr["CHEQUENO"] = lblChequeNo.Text.ToString();
                        dr["REMARKS"] = txRemarks.Text.ToString();

                        dtBooklet.Rows.Add(dr);
                        dtBooklet.AcceptChanges();

                    }

                }
                Session["USERID"].ToString();
                string xml = ConvertDatatableToXML(dtBooklet);

                ClsVoucherEntry clsreport = new ClsVoucherEntry();
                string Status = clsreport.InsertChequeCancel(Session["UserID"].ToString().Trim(), xml, "FC");

                if (Status == "1")
                {
                    MessageBox1.ShowSuccess("Cheque APPROVED  successfully.");

                    Session["BOOKLETDETAILS"] = null;
                    this.gvUnlockVoucher.DataSource = null;
                    this.gvUnlockVoucher.DataBind();


                }
                else
                {
                    MessageBox1.ShowError("Cheque APPROVED unsuccessful.");
                    Session["BOOKLETDETAILS"] = null;
                }

            }
            else
            {
                MessageBox1.ShowInfo("<b><font color='maroon'>Note :</font> <font color='gray'>You can select maximum 500 cheque at a time.</font></b>", 40, 510);
                return;
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

  
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


    #region gvUnlockVoucher_RowDataBound
    protected void gvUnlockVoucher_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }

        }
        catch (Exception ex)
        {

            MessageBox1.ShowInfo("<b>'" + ex.Message + "'</br></br><font color='gray'>Please logout,then try again. Otherwise inform to administrator</font></b>", 60, 500);
        }
    }
    #endregion

}