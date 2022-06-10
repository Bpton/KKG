using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Account;

public partial class VIEW_frmChequeBooklet : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                HttpContext.Current.Session["mode"] = "a";
                this.LoadBranch();
                this.LoadBank();
                this.LoadBankVouter();
                this.pnlAdd.Style["display"] = "none";
                Session["BOOKLETDETAILS"] = null;
                //Session["vouterdetails"] = null;

            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    public void LoadBranch()
    {
        try
        {
            ClsVoucherEntry clsrpt = new ClsVoucherEntry();
            DataTable dtbank = new DataTable();
            dtbank = clsrpt.Bind_Bank_Booklet('4', Session["UserID"].ToString(), "", "", "");
            if (dtbank.Rows.Count > 0)
            {
                ddldept.Items.Clear();
                ddldept.Items.Insert(0, new ListItem("Select", "0"));
                ddldept.AppendDataBoundItems = true;
                ddldept.DataSource = dtbank;
                ddldept.DataTextField = "BRNAME";
                ddldept.DataValueField = "BRID";
                ddldept.DataBind();
            }
            else
            {
                ddldept.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    public void LoadBank()
    {
        try
        {
            ClsVoucherEntry clsrpt = new ClsVoucherEntry();
            DataTable dtbank = new DataTable();
            dtbank = clsrpt.Bind_Bank_Booklet('0', "", "", "", "");
            if (dtbank.Rows.Count > 0)
            {
                ddlbank.Items.Clear();
                ddlbank.Items.Insert(0, new ListItem("Select", "0"));
                ddlbank.AppendDataBoundItems = true;
                ddlbank.DataSource = dtbank;
                ddlbank.DataTextField = "BANKNAME";
                ddlbank.DataValueField = "BANKID";
                ddlbank.DataBind();


                ddlbank1.Items.Clear();
                ddlbank1.Items.Insert(0, new ListItem("Select", "0"));
                ddlbank1.AppendDataBoundItems = true;
                ddlbank1.DataSource = dtbank;
                ddlbank1.DataTextField = "BANKNAME";
                ddlbank1.DataValueField = "BANKID";
                ddlbank1.DataBind();
            }
            else
            {
                ddlbank.Items.Clear();
                ddlbank1.Items.Clear();

            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public DataTable CreateTable()
    {
        DataTable dtbank = new DataTable();
        dtbank.Clear();
        dtbank.Columns.Add("BANKID");
        dtbank.Columns.Add("BANKNAME");
        dtbank.Columns.Add("DEPARTMENTID");
        dtbank.Columns.Add("DEPARTMENT");
        dtbank.Columns.Add("BOOKLETNO");
        dtbank.Columns.Add("BOOKLETNO_AUTO");
        dtbank.Columns.Add("CHEQUEFROMNO");
        dtbank.Columns.Add("CHEQUETONO");
        dtbank.Columns.Add("ISACTIVE");
        dtbank.Columns.Add("NEWENTRY");
        HttpContext.Current.Session["BOOKLETDETAILS"] = dtbank;
        return dtbank;
    }



    protected void btnADDGrid_Click(object sender, EventArgs e)
    {
        try
        {

            ClsVoucherEntry clsrpt = new ClsVoucherEntry();
            int str = clsrpt.SAVEandValidation(this.ddlbank.SelectedValue.ToString(), Convert.ToString(this.txtBookletNo.Text.Trim()), Convert.ToString(this.txtfromcheqno.Text.Trim()), Convert.ToString(this.txttocheqno.Text.Trim()),
                Convert.ToString(this.ddldept.SelectedValue), Convert.ToString(this.ddldept.SelectedItem.Text), Session["UserID"].ToString().Trim(),
                this.txtstart.Text, "A");
            if (str > 0)
            {
                MessageBox1.ShowInfo("<b>Save successfully !</b>");

            }
            else
            {
                MessageBox1.ShowInfo("<b>Cheque number is not valid !</b>");
                return;
            }


            DataTable dtbank = new DataTable();
            dtbank = clsrpt.Bind_Bank_Booklet('1', "", Convert.ToString(this.ddlbank.SelectedValue), "", Convert.ToString(this.ddldept.SelectedValue));
            if (dtbank.Rows.Count > 0)
            {
                Session["BOOKLETDETAILS"] = dtbank;
                this.grdDSR.DataSource = dtbank;
                this.grdDSR.DataBind();
            }
            else
            {
                this.grdDSR.DataSource = null;
                this.grdDSR.DataBind();
            }


            this.txtBookletNo.Text = "";
            this.txtfromcheqno.Text = "";
            this.txttocheqno.Text = "";


        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    protected void ddlbank_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            ClsVoucherEntry clsrpt = new ClsVoucherEntry();
            DataTable dtbank = new DataTable();
            dtbank = clsrpt.Bind_Bank_Booklet('1', "", Convert.ToString(this.ddlbank.SelectedValue), "", Convert.ToString(this.ddldept.SelectedValue));
            if (dtbank.Rows.Count > 0)
            {
                Session["BOOKLETDETAILS"] = dtbank;
                this.grdDSR.DataSource = dtbank;
                this.grdDSR.DataBind();
            }
            else
            {
                this.grdDSR.DataSource = null;
                this.grdDSR.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Session["BOOKLETDETAILS"] == null)
            {
                CreateTable();
            }

            DataTable dtBANK = (DataTable)HttpContext.Current.Session["BOOKLETDETAILS"];

            ClsVoucherEntry clsrpt = new ClsVoucherEntry();
            DataTable dtbank = new DataTable();
            dtbank = clsrpt.Bind_Bank_Booklet('1', "", "", "", Convert.ToString(this.ddldept.SelectedValue));
            if (dtbank.Rows.Count > 0)
            {
                Session["BOOKLETDETAILS"] = dtbank;
                this.grdDSR.DataSource = dtbank;
                this.grdDSR.DataBind();
            }
            else
            {
                this.grdDSR.DataSource = null;
                this.grdDSR.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
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

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try

        {

            if (Session["BOOKLETDETAILS"] == null)
            {
                CreateTable();
            }

            DataTable dtBANK = (DataTable)HttpContext.Current.Session["BOOKLETDETAILS"];

            string xml = ConvertDatatableToXML(dtBANK);

            ClsVoucherEntry clsrpt = new ClsVoucherEntry();
            string Status = clsrpt.Acc_Booklet_Insert(this.txtstart.Text, this.ddlbank.SelectedValue, Session["UserID"].ToString().Trim(), xml, Convert.ToString(HttpContext.Current.Session["mode"]));

            if (Status == "1")
            {

                MessageBox1.ShowSuccess("Booklet details save successfully.");
                this.grdDSR.DataSource = null;
                this.grdDSR.DataBind();
                this.ddlbank.SelectedValue = "0";
                this.txtBookletNo.Text = "";
                this.txtfromcheqno.Text = "";
                this.txttocheqno.Text = "";
                Session["BOOKLETDETAILS"] = null;
                HttpContext.Current.Session["mode"] = "a";
            }
            else
            {
                MessageBox1.ShowError("Booklet details save unsuccessful.");
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void btngrddelete_Click(object sender, EventArgs e)
    {
        try
        {

            HttpContext.Current.Session["mode"] = "D";
            DataTable dtdelete = (DataTable)Session["BOOKLETDETAILS"];
            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;
            Label lblbookletID = (Label)gvr.FindControl("lbbookletno");

            ClsVoucherEntry clsrpt = new ClsVoucherEntry();
            int str = clsrpt.SAVEandValidation(this.ddlbank.SelectedValue.ToString(), lblbookletID.Text.ToString(), "0", "0",
                Convert.ToString(this.ddldept.SelectedValue), Convert.ToString(this.ddldept.SelectedItem.Text), Session["UserID"].ToString().Trim(),
                this.txtstart.Text, "D");
            if (str > 0)
            {
                MessageBox1.ShowInfo("<b>Delete successfully !</b>");

            }
            else
            {
                MessageBox1.ShowInfo("<b>Cheque number is already used !</b>");
                return;
            }


            DataTable dtbank = new DataTable();
            dtbank = clsrpt.Bind_Bank_Booklet('1', "", Convert.ToString(this.ddlbank.SelectedValue), "", Convert.ToString(this.ddldept.SelectedValue));
            if (dtbank.Rows.Count > 0)
            {
                Session["BOOKLETDETAILS"] = dtbank;
                this.grdDSR.DataSource = dtbank;
                this.grdDSR.DataBind();
            }
            else
            {
                this.grdDSR.DataSource = null;
                this.grdDSR.DataBind();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.pnlAdd.Style["display"] = "";
        this.pnlDisplay.Style["display"] = "none";
    }
    public void LoadBankVouter()
    {
        try
        {
            ClsVoucherEntry clsrpt = new ClsVoucherEntry();
            DataTable dtbank = new DataTable();
            dtbank = clsrpt.Bind_Bank_Booklet('4', Session["UserID"].ToString(), "", "", "");
            if (dtbank.Rows.Count > 0)
            {
                ddlbranch.Items.Clear();
                ddlbranch.Items.Insert(0, new ListItem("Select", "0"));
                ddlbranch.AppendDataBoundItems = true;
                ddlbranch.DataSource = dtbank;
                ddlbranch.DataTextField = "BRNAME";
                ddlbranch.DataValueField = "BRID";
                ddlbranch.DataBind();
            }
            else
            {
                ddlbranch.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    protected void ddlbranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try

        {
            ClsVoucherEntry clsrpt = new ClsVoucherEntry();
            DataTable dtbank = new DataTable();
            dtbank = clsrpt.Bind_Bank_Booklet('2', "", "", "", Convert.ToString(this.ddlbranch.SelectedValue));
            if (dtbank.Rows.Count > 0)
            {
                this.ddlbooklet1.Items.Clear();
                this.ddlbooklet1.Items.Insert(0, new ListItem("SELECT", " "));
                this.ddlbooklet1.Items.Insert(1, new ListItem("ALL", "0"));
                this.ddlbooklet1.AppendDataBoundItems = true;
                this.ddlbooklet1.DataSource = dtbank;
                this.ddlbooklet1.DataTextField = "BOOKLETNO";
                this.ddlbooklet1.DataValueField = "DEPARTMENTID";
                this.ddlbooklet1.DataBind();
            }
            else
            {
                this.ddlbooklet1.Items.Clear();
                this.ddlbooklet1.Items.Insert(0, new ListItem("SELECT", " "));
                this.ddlbooklet1.Items.Insert(1, new ListItem("ALL", "0"));
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void ddlchqcancel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try

        {
            foreach (GridViewRow row in VouterGrdview.Rows)
            {
                DropDownList ddlchqcancel1 = row.FindControl("ddlchqcancel") as DropDownList;
                TextBox txtremarks = row.FindControl("txtremarks") as TextBox;

                if (ddlchqcancel1.SelectedValue == "Y" || ddlchqcancel1.SelectedValue == "N")
                {
                    txtremarks.Enabled = true;
                }
                else
                {
                    txtremarks.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void ddlbooklet1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try

        {
            ClsVoucherEntry clsrpt = new ClsVoucherEntry();
            DataTable dtbankvaucher = new DataTable();
            dtbankvaucher = clsrpt.Bind_Bank_Booklet('3', "", Convert.ToString(this.ddlbank1.SelectedValue), Convert.ToString(this.ddlbooklet1.SelectedValue), Convert.ToString(this.ddlbranch.SelectedValue));
            if (dtbankvaucher.Rows.Count > 0)
            {
                this.VouterGrdview.DataSource = dtbankvaucher;
                this.VouterGrdview.DataBind();
                             
            }

            else
            {
                this.VouterGrdview.DataSource = null;
                this.VouterGrdview.DataBind();
            }



        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    public DataTable CreateTablevoutarsave()
    {
        DataTable dtvouter = new DataTable();
        dtvouter.Clear();
        dtvouter.Columns.Add("BANKID");
        dtvouter.Columns.Add("BANKNAME");
        dtvouter.Columns.Add("DEPARTMENTID");
        dtvouter.Columns.Add("DEPARTMENT");
        dtvouter.Columns.Add("BOOKLETNO");
        dtvouter.Columns.Add("CHEQUENO");
        dtvouter.Columns.Add("CHEQUEDATE");
        dtvouter.Columns.Add("ISCANCEL");
        dtvouter.Columns.Add("CANCELREASON");
        dtvouter.Columns.Add("CBU");
        dtvouter.Columns.Add("DTOC");
        dtvouter.Columns.Add("MBU");
        dtvouter.Columns.Add("LDTOM");
        dtvouter.Columns.Add("ISACTIVE");
        HttpContext.Current.Session["vouterdetails"] = dtvouter;
        return dtvouter;
    }

  
    protected void btnSearchSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["vouterdetails"] == null)
            {
                CreateTablevoutarsave();
            }

            DataTable dtVOUCHER = (DataTable)HttpContext.Current.Session["vouterdetails"];


            foreach (GridViewRow row in VouterGrdview.Rows)
            {
                Label lblbank = row.FindControl("lblbank") as Label;
                Label lblbooletno = row.FindControl("lblbookletno1") as Label;
                Label lblbranchid = row.FindControl("lblbranchid") as Label;
                Label lblbranch = row.FindControl("lblbranch") as Label;
                DropDownList ddlchqcancel = row.FindControl("ddlchqcancel") as DropDownList;
                TextBox txtremarks = row.FindControl("txtremarks") as TextBox;
                if ((Convert.ToString(ddlchqcancel.SelectedValue) == "Y" || Convert.ToString(ddlchqcancel.SelectedValue) == "N"))
                {
                    if (txtremarks.Text == "")
                    {
                        MessageBox1.ShowInfo("<b>Remarks mandetory</b>");
                        return;
                    }
                    else
                    {
                        DataRow dr = dtVOUCHER.NewRow();
                        dr["BANKID"] = Convert.ToString(this.ddlbank1.SelectedValue);
                        dr["ISCANCEL"] = Convert.ToString(ddlchqcancel.SelectedItem);
                        dr["CANCELREASON"] = Convert.ToString(txtremarks.Text);
                        dr["CHEQUENO"] = Convert.ToString(lblbank.Text);
                        dr["BOOKLETNO"] = Convert.ToString(lblbooletno.Text);
                        dr["DEPARTMENTID"] = Convert.ToString(lblbranchid.Text);
                        dr["DEPARTMENT"] = Convert.ToString(lblbranch.Text);
                        dtVOUCHER.Rows.Add(dr);
                        dtVOUCHER.AcceptChanges();

                    }
                }




            }


            string xml = ConvertDatatableToXML(dtVOUCHER);

            ClsVoucherEntry clsrpt = new ClsVoucherEntry();
            string Status = clsrpt.Acc_Booklet_Insert_vouter(this.ddlbank1.SelectedValue, Session["UserID"].ToString().Trim(), xml);

            if (Status == "1")
            {
                MessageBox1.ShowSuccess("details save successfully.");

                Session["vouterdetails"] = null;
                this.VouterGrdview.DataSource = null;
                this.VouterGrdview.DataBind();

            }
            else
            {
                MessageBox1.ShowError("Booklet details save unsuccessful.");
                Session["vouterdetails"] = null;
            }

        }
        catch (Exception ex)
        {
            Session["vouterdetails"] = null;
            string msg = ex.Message;
        }

    }

    protected void btncancelitem_Click(object sender, EventArgs e)
    {
        this.pnlAdd.Style["display"] = "none";
        this.pnlDisplay.Style["display"] = "";
        Session["vouterdetails"] = null;

    }
}