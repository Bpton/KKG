using Account;
using Obout.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmAccLedgerInfo : System.Web.UI.Page
{
    ClsAccLedgerInfo clsLdgInfo = new ClsAccLedgerInfo();
    protected void Page_Load(object sender, EventArgs e)
    {

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> $(function () { $('#ContentPlaceHolder1_ddltax').multiselect({ includeSelectAllOption: true  });});</script>", false);
        if (!IsPostBack)
        {
            ViewState["LedgerEdit"] = "I"; /* Edit time appear all group but insert time not all by D.Mondal on 31/10/2018 */

            this.txtName.Style.Add("color", "black !important");
            this.txtName.BackColor = SystemColors.Window;
            this.pnlDisplay.Style["display"] = "";
            this.pnlAdd.Style["display"] = "none";
            this.pnlledgerDepotMapping.Style["display"] = "none";
            this.trTree.Visible = false;
            this.trGrid.Visible = true;
            this.GrdBindView();
            this.BindVoucherTypes();
            this.BindDepot();
            this.txtdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeaderOutStanding('" + gvadd.ClientID + "', 200, '100%' , 30 ,false); </script>", false);
    }

    protected void Btnadd_Click(object sender, EventArgs e)
    {
        this.pnlDisplay.Style["display"] = "none";
        this.pnlAdd.Style["display"] = "";
        this.GrdBind();
        this.BindGroup();
        this.BindTax_FromDutiesTaxes();
        this.btnaddhide.Style["display"] = "none";
        this.txtamount.Text = "";
        this.txtinvoice.Text = "";
        this.gvadd.DataSource = null;
        this.gvadd.DataBind();
        HttpContext.Current.Session["ACC_INVOICEENTRY"] = null;
    }

    protected DataTable CreateLedgerTable()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add(new DataColumn("ID", typeof(string)));
        dt.Columns.Add(new DataColumn("DEBITAMOUNT", typeof(string)));
        dt.Columns.Add(new DataColumn("CREDITAMOUNT", typeof(string)));
        dt.Columns.Add(new DataColumn("OPENBALANCE", typeof(string)));
        dt.Columns.Add(new DataColumn("BALANCETYPE", typeof(string)));
        dt.Columns.Add(new DataColumn("REGIONID", typeof(string)));
        dt.Columns.Add(new DataColumn("REGIONNAME", typeof(string)));

        HttpContext.Current.Session["LEDGERDETAILS"] = dt;

        return dt;
    }

    protected void BindGroup()
    {
        try
        {
            this.ddlgroup.Items.Clear();
            this.ddlgroup.Items.Add(new ListItem("Select Group", "0"));
            this.ddlgroup.AppendDataBoundItems = true;
            this.ddlgroup.DataSource = clsLdgInfo.BindGroup(ViewState["LedgerEdit"].ToString());
            this.ddlgroup.DataTextField = "grpName";
            this.ddlgroup.DataValueField = "Code";
            this.ddlgroup.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void BindTax_FromDutiesTaxes()
    {
        try
        {
            string TaxId = string.Empty;
            var query = from ListItem item in ddltax.Items where item.Selected select item;
            foreach (ListItem item in query)
            {
                TaxId += item.Value + ",";
            }

            if (TaxId.Trim() == "")
            {
                this.ddltax.Items.Clear();
                //this.ddltax.Items.Add(new ListItem("Select Tax", "0"));
                //this.ddltax.AppendDataBoundItems = true;
                this.ddltax.DataSource = clsLdgInfo.BindTax();
                this.ddltax.DataTextField = "Name";
                this.ddltax.DataValueField = "ID";
                this.ddltax.DataBind();
            }


        }
        catch (Exception ex)
        {
            throw ex;
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            int ID = 0;
            string Tag = "D";
            string PERIODID = string.Empty;
            string xmlinvoice = string.Empty;
            DataTable dt = new DataTable();
            if (Session["ACC_INVOICEENTRY"] != null)
            {
                dt = (DataTable)Session["ACC_INVOICEENTRY"];
            }
            xmlinvoice = ConvertDatatableToXML(dt);

            if (Session["LEDGERDETAILS"] == null)
            {
                this.CreateLedgerTable();
            }
            DataTable dtledger = (DataTable)Session["LEDGERDETAILS"];

            if (Grdboard.TotalRowCount > 0)
            {
                PERIODID = clsLdgInfo.PeriodID(Session["FINYEAR"].ToString());

                for (int i = 0; i < Grdboard.Rows.Count; i++)
                {
                    decimal Amount = 0;

                    string BrCode = Grdboard.Rows[i].Cells[0].Text;
                    string BrName = Grdboard.Rows[i].Cells[2].Text;

                    GridDataControlFieldCell dramount = Grdboard.RowsInViewState[i].Cells[3] as GridDataControlFieldCell;
                    TextBox txtdebitamount = dramount.FindControl("txtDrAmount") as TextBox;

                    GridDataControlFieldCell cramount = Grdboard.RowsInViewState[i].Cells[4] as GridDataControlFieldCell;
                    TextBox txtcreditamount = cramount.FindControl("txtCrAmount") as TextBox;



                    if (!string.IsNullOrEmpty(txtdebitamount.Text.Trim()))
                    {
                        if (Convert.ToDecimal(txtdebitamount.Text.Trim()) != 0)
                        {
                            Amount = Convert.ToDecimal(txtdebitamount.Text.Trim());
                            Tag = "D";
                        }
                    }
                    if (!string.IsNullOrEmpty(txtcreditamount.Text.Trim()))
                    {
                        if (Convert.ToDecimal(txtcreditamount.Text.Trim()) != 0)
                        {
                            Amount = Convert.ToDecimal(txtcreditamount.Text.Trim());
                            Tag = "C";
                        }
                    }

                    //if (Amount != 0)
                    //{
                    int NumberofRecord = dtledger.Select("REGIONID='" + BrCode + "'").Length;
                    if (NumberofRecord > 0)
                    {
                        if (Tag == "D")
                        {
                            dtledger.Rows[i]["DEBITAMOUNT"] = Amount;
                            dtledger.Rows[i]["CREDITAMOUNT"] = 0;
                        }
                        else
                        {
                            dtledger.Rows[i]["CREDITAMOUNT"] = Amount;
                            dtledger.Rows[i]["DEBITAMOUNT"] = 0;
                        }
                        dtledger.Rows[i]["BALANCETYPE"] = Tag;
                        dtledger.Rows[i]["OPENBALANCE"] = Amount;
                        dtledger.AcceptChanges();
                    }
                    else
                    {
                        DataRow dr = dtledger.NewRow();
                        dr["ID"] = Guid.NewGuid();
                        dr["OPENBALANCE"] = Amount;
                        dr["BALANCETYPE"] = Tag;
                        if (Tag == "D")
                        {
                            dr["DEBITAMOUNT"] = Amount;
                            dr["CREDITAMOUNT"] = 0;
                        }
                        else
                        {
                            dr["DEBITAMOUNT"] = 0;
                            dr["CREDITAMOUNT"] = Amount;
                        }
                        dr["REGIONID"] = BrCode;
                        dr["REGIONNAME"] = BrName;

                        dtledger.Rows.Add(dr);
                        dtledger.AcceptChanges();
                    }
                    //}
                }

                HttpContext.Current.Session["LEDGERDETAILS"] = dtledger;

                if (dtledger.Rows.Count > 0)
                {
                    string xmlledger = string.Empty;
                    xmlledger = ConvertDatatableToXML(dtledger);

                    string iscostcenter = "N";
                    string isallownegativebal = "N";
                    string TaxId = "";
                    if (chkcostcenter.Checked == true)
                    {
                        iscostcenter = "Y";
                    }
                    if (chknegativebal.Checked == true)
                    {
                        isallownegativebal = "Y";
                    }

                    var query = from ListItem item in ddltax.Items where item.Selected select item;
                    foreach (ListItem item in query)
                    {
                        TaxId += item.Value + ",";
                    }

                    if (TaxId.Length > 0)
                    {
                        TaxId = TaxId.Substring(0, TaxId.Length - 1);
                    }

                    ID = clsLdgInfo.SaveAccInfo(txtName.Text.Trim(), this.ddlgroup.SelectedValue, Session["FINYEAR"].ToString(), hdn_accid.Value.ToString(), xmlledger, iscostcenter, TaxId, xmlinvoice, isallownegativebal);

                    if (ID == 1)
                    {
                        MessageBox1.ShowSuccess("Record save successfully!");

                        this.pnlDisplay.Style["display"] = "";
                        this.pnlAdd.Style["display"] = "none";
                        this.btnaddhide.Style["display"] = "";
                        this.hdn_accid.Value = "";
                        this.txtName.Text = "";
                        this.txtName.Enabled = true;
                        this.chkcostcenter.Checked = false;
                        this.chknegativebal.Checked = false;
                        this.ddltax.SelectedIndex = -1;
                        this.Session["LEDGERDETAILS"] = null;
                        this.Grdboard.ClearPreviousDataSource();
                        this.Grdboard.DataSource = null;
                        this.Grdboard.DataBind();
                        this.trTree.Visible = false;
                        this.trGrid.Visible = true;
                        this.txtName.Enabled = true;
                        this.ddlgroup.Enabled = true;
                        this.ddltax.Enabled = true;
                        this.chkcostcenter.Enabled = true;
                        this.chknegativebal.Enabled = true;
                        this.GrdBindView();
                        HttpContext.Current.Session["ACC_INVOICEENTRY"] = null;
                    }
                    else if (ID == 2)
                    {
                        MessageBox1.ShowInfo("Name already exist!....");
                    }
                    else
                    {
                        MessageBox1.ShowError("Record save unsuccessfully!");
                    }
                }
                else
                {
                    MessageBox1.ShowInfo("<b>Please Insert <font color='red'>Debit or Credit</font> Amount!</b>", 60, 450);
                }
            }
            else
            {
                MessageBox1.ShowInfo("<b><font color='red'>No region name found!</font></b>");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void rdGridView_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            this.trTree.Visible = false;
            this.trGrid.Visible = true;
            this.GrdBindView();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void rdTreeView_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            this.trTree.Visible = true;
            this.trGrid.Visible = false;
            this.TrvGroup.Nodes.Clear();
            this.PopulateParentTreeView();
            this.TrvGroup.ExpandAll();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    private void PopulateParentTreeView()
    {
        ClsAccGrp ClsGRp = new ClsAccGrp();
        DataTable dtParent = ClsGRp.BindLedgerPry();

        foreach (DataRow dr in dtParent.Rows)
        {
            TreeNode node = new TreeNode();
            node.SelectAction = TreeNodeSelectAction.None;
            node.Text = dr["name"].ToString();
            node.Value = dr["id"].ToString();
            this.PopulateTreeView(node);
            this.TrvGroup.Nodes.Add(node);
        }
    }

    private void PopulateTreeView(TreeNode treeNode)
    {
        ClsAccGrp ClsGRp = new ClsAccGrp();
        DataTable dtParent = ClsGRp.BindParentTree(treeNode.Value);

        foreach (DataRow dr in dtParent.Rows)
        {
            TreeNode node = new TreeNode();
            node.SelectAction = TreeNodeSelectAction.None;
            node.Text = dr["grpname"].ToString();
            node.Value = dr["code"].ToString();
            AddNodes(node);

            treeNode.ChildNodes.Add(node);
            this.AddSubNodes(node);
        }
    }

    private void AddNodes(TreeNode node)
    {
        ClsAccGrp ClsGRp = new ClsAccGrp();
        DataTable dtlevel = ClsGRp.BindChildIdTree(node.Value);
        foreach (DataRow r in dtlevel.Rows)
        {
            TreeNode nNode = new TreeNode();
            nNode.SelectAction = TreeNodeSelectAction.None;
            nNode.Value = r["Code"].ToString();
            nNode.Text = r["grpName"].ToString();
            AddNodes(nNode);
            this.AddSubNodes(nNode);
            node.ChildNodes.Add(nNode);
        }
    }

    private void AddChildNodes(TreeNode node)
    {
        ClsAccGrp ClsGRp = new ClsAccGrp();
        DataTable dtlevel = ClsGRp.BindChildIdTree(node.Value);
        foreach (DataRow r in dtlevel.Rows)
        {
            TreeNode nNode = new TreeNode();
            nNode.SelectAction = TreeNodeSelectAction.None;
            nNode.Value = r["Code"].ToString();
            nNode.Text = r["grpName"].ToString();
            this.AddNodes(nNode);
            node.ChildNodes.Add(nNode);
        }
    }

    private void AddSubNodes(TreeNode node)
    {
        ClsAccGrp ClsGRp = new ClsAccGrp();
        DataTable dtlevel = ClsGRp.BindLastChildIdTree(node.Value);
        foreach (DataRow r in dtlevel.Rows)
        {
            TreeNode nNode = new TreeNode();
            nNode.SelectAction = TreeNodeSelectAction.None;
            nNode.Value = r["Code"].ToString();
            nNode.Text = r["grpName"].ToString();
            this.AddNodes(nNode);
            node.ChildNodes.Add(nNode);
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            this.pnlDisplay.Style["display"] = "";
            this.pnlAdd.Style["display"] = "none";
            this.btnaddhide.Style["display"] = "";
            this.hdn_accid.Value = "";
            this.txtName.Text = "";
            this.txtName.Enabled = true;
            this.chkcostcenter.Checked = false;
            this.chknegativebal.Checked = false;
            this.ddltax.SelectedIndex = -1;
            this.Grdboard.ClearPreviousDataSource();
            this.Grdboard.DataSource = null;
            this.Session["LEDGERDETAILS"] = null;
            this.Grdboard.DataBind();
            this.trTree.Visible = false;
            this.trGrid.Visible = true;
            this.txtName.Enabled = true;
            this.ddlgroup.Enabled = true;
            this.ddltax.Enabled = true;
            this.chkcostcenter.Enabled = true;
            this.chknegativebal.Enabled = true;
            this.GrdBindView();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void GrdBind()
    {
        try
        {
            this.Grdboard.DataSource = clsLdgInfo.BindGridBranch();
            this.Grdboard.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void GrdBindView()
    {
        try
        {
            this.grdView.DataSource = clsLdgInfo.BindGridView();
            this.grdView.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void Grdboard_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == GridRowType.DataRow || Grdboard.RowsInViewState.Count > 0)
            {
                if (Grdboard.RowsInViewState.Count > e.Row.RowIndex)
                {
                    GridDataControlFieldCell cell = e.Row.Cells[3] as GridDataControlFieldCell;
                    TextBox DrAmountBox = cell.FindControl("txtDrAmount") as TextBox;

                    GridDataControlFieldCell cell3 = e.Row.Cells[4] as GridDataControlFieldCell;
                    TextBox CrAmountBox = cell3.FindControl("txtCrAmount") as TextBox;

                    if (Grdboard.RowsInViewState != null)
                    {
                        if (Grdboard.RowsInViewState[e.Row.RowIndex].Cells.Count > 2)
                        {
                            GridDataControlFieldCell cellInViewState = Grdboard.RowsInViewState[e.Row.RowIndex].Cells[3] as GridDataControlFieldCell;
                            TextBox textBoxInViewState = cellInViewState.FindControl("txtDrAmount") as TextBox;
                            DrAmountBox.Text = textBoxInViewState.Text;
                        }

                        if (Grdboard.RowsInViewState[e.Row.RowIndex].Cells.Count > 3)
                        {
                            GridDataControlFieldCell cellInViewState = Grdboard.RowsInViewState[e.Row.RowIndex].Cells[4] as GridDataControlFieldCell;
                            TextBox textBoxInViewState1 = cellInViewState.FindControl("txtCrAmount") as TextBox;
                            CrAmountBox.Text = textBoxInViewState1.Text;
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

    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        try
        {
            string status = clsLdgInfo.LedgerDelete(hdn_accid.Value.ToString());

            if (status == "6")
            {
                MessageBox1.ShowSuccess("<b>Ledger deleted successfully.</b>");
                this.GrdBindView();
            }
            else
            {
                MessageBox1.ShowInfo("<b>" + status + "</b>", 50, 500);
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btn_Edit_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["LedgerEdit"] = "U"; /* Edit time appear all group but insert time not all by D.Mondal on 31/10/2018 */

            int ispredefine = clsLdgInfo.IsPredefine(hdn_accid.Value.ToString().Trim());

            if (ispredefine == 1)
            {
                this.txtName.Enabled = false;
                this.ddlgroup.Enabled = false;
                //this.ddltax.Enabled = false;
                //this.chkcostcenter.Enabled = false;
            }

            CreateDataTable();
            DataTable dtEdit = new DataTable();
            if (Session["ACC_INVOICEENTRY"] != null)
            {
                dtEdit = (DataTable)HttpContext.Current.Session["ACC_INVOICEENTRY"];
            }
            if (Session["LEDGERDETAILS"] == null)
            {
                this.CreateLedgerTable();
            }
            DataTable dtledger = (DataTable)Session["LEDGERDETAILS"];
            string accid = hdn_accid.Value.ToString();
            BindGroup();
            DataSet ds = new DataSet();
            ds = clsLdgInfo.FetchLedgerInfo(accid, Session["FINYEAR"].ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    DataRow dr = dtEdit.NewRow();
                    dr["AccEntryID"] = Convert.ToString(ds.Tables[2].Rows[i]["AccEntryID"]).Trim();
                    dr["InvoiceID"] = Convert.ToString(ds.Tables[2].Rows[i]["InvoiceID"]).Trim();
                    dr["InvoiceNo"] = Convert.ToString(ds.Tables[2].Rows[i]["InvoiceNo"]).Trim();
                    dr["InvoiceDate"] = Convert.ToString(ds.Tables[2].Rows[i]["InvoiceDate"]).Trim();
                    dr["InvoiceAmt"] = Convert.ToString(ds.Tables[2].Rows[i]["InvoiceAmt"]).Trim();
                    dr["AmtPaid"] = Convert.ToString(ds.Tables[2].Rows[i]["AmtPaid"]).Trim();
                    dr["VoucherTypeID"] = Convert.ToString(ds.Tables[2].Rows[i]["VoucherTypeID"]).Trim();
                    dr["VoucherTypeName"] = Convert.ToString(ds.Tables[2].Rows[i]["VoucherTypeName"]).Trim();
                    dr["BranchID"] = Convert.ToString(ds.Tables[2].Rows[i]["BranchID"]).Trim();
                    dr["BranchName"] = Convert.ToString(ds.Tables[2].Rows[i]["BranchName"]).Trim();
                    dr["InvoiceBranchID"] = Convert.ToString(ds.Tables[2].Rows[i]["InvoiceBranchID"]).Trim();
                    dr["InvoiceBranchName"] = Convert.ToString(ds.Tables[2].Rows[i]["InvoiceBranchName"]).Trim();
                    dr["InvoiceOthers"] = Convert.ToString(ds.Tables[2].Rows[i]["InvoiceOthers"]).Trim();
                    dr["Type"] = Convert.ToString(ds.Tables[2].Rows[i]["Type"]).Trim();
                    dtEdit.Rows.Add(dr);
                    dtEdit.AcceptChanges();
                }

                HttpContext.Current.Session["ACC_INVOICEENTRY"] = dtEdit;

                this.gvadd.DataSource = dtEdit;
                this.gvadd.DataBind();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = dtledger.NewRow();
                    dr["ID"] = Guid.NewGuid();
                    dr["BALANCETYPE"] = ds.Tables[0].Rows[i]["balanceType"].ToString();

                    if (ds.Tables[0].Rows[i]["balanceType"].ToString() == "D")
                    {
                        dr["DEBITAMOUNT"] = ds.Tables[0].Rows[i]["DEBITAMOUNT"].ToString();
                        dr["CREDITAMOUNT"] = ds.Tables[0].Rows[i]["CREDITAMOUNT"].ToString();
                        dr["OPENBALANCE"] = ds.Tables[0].Rows[i]["DEBITAMOUNT"].ToString();
                    }
                    else
                    {
                        dr["DEBITAMOUNT"] = ds.Tables[0].Rows[i]["DEBITAMOUNT"].ToString();
                        dr["CREDITAMOUNT"] = ds.Tables[0].Rows[i]["CREDITAMOUNT"].ToString();
                        dr["OPENBALANCE"] = ds.Tables[0].Rows[i]["CREDITAMOUNT"].ToString();
                    }

                    dr["REGIONID"] = ds.Tables[0].Rows[i]["REGIONID"].ToString();
                    dr["REGIONNAME"] = ds.Tables[0].Rows[i]["REGIONNAME"].ToString();

                    dtledger.Rows.Add(dr);
                    dtledger.AcceptChanges();
                }

                HttpContext.Current.Session["LEDGERDETAILS"] = dtledger;
                this.Grdboard.DataSource = dtledger;
                this.Grdboard.DataBind();
                this.ddlgroup.SelectedValue = ds.Tables[1].Rows[0]["Code"].ToString();
                this.txtName.Text = ds.Tables[1].Rows[0]["name"].ToString();
                //this.txtName.Enabled = false;

                if (!string.IsNullOrEmpty(ds.Tables[1].Rows[0]["taxid"].ToString()))
                {
                    var myList = new List<string>(ds.Tables[1].Rows[0]["taxid"].ToString().Split(','));
                    ddltax.Items.Clear();
                    this.BindTax_FromDutiesTaxes();
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        for (int counter = 0; counter < myList.Count; counter++)
                        {
                            for (int innercounter = 0; innercounter < ddltax.Items.Count; innercounter++)
                            {
                                if (myList[counter] == ddltax.Items[innercounter].Value)
                                {
                                    ddltax.Items[innercounter].Selected = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    this.BindTax_FromDutiesTaxes();
                }

                if (ds.Tables[1].Rows[0]["costcenter"].ToString().Trim() == "Y")
                {
                    chkcostcenter.Checked = true;
                }
                else
                {
                    chkcostcenter.Checked = false;
                }
                if (ds.Tables[1].Rows[0]["isallownegativebal"].ToString().Trim() == "Y")
                {
                    chknegativebal.Checked = true;
                }
                else
                {
                    chknegativebal.Checked = false;
                }

                pnlDisplay.Style["display"] = "none";
                pnlAdd.Style["display"] = "";
                btnaddhide.Style["display"] = "none";
            }
            else
            {
                this.Grdboard.DataSource = null;
                this.Grdboard.DataBind();
            }
            //}
            //else
            //{
            //    MessageBox1.ShowInfo("<b>You can not edit this leadger because it's auto generated!</b>",60,520);
            //}
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }


    #region Create DataTable Structure
    public DataTable CreateDataTableDepotMapping()
    {
        DataTable dt = new DataTable();

        dt.Clear();

        dt.Columns.Add(new DataColumn("LEDGERID", typeof(string)));
        dt.Columns.Add(new DataColumn("LEDGERNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("BRANCHID", typeof(string)));
        dt.Columns.Add(new DataColumn("BRANCHNAME", typeof(string)));

        HttpContext.Current.Session["DEPOTMAPPING"] = dt;

        return dt;
    }
    #endregion

    private void LoadDepot()
    {
        try
        {
            ClsAccLedgerInfo clsLdgInfo = new ClsAccLedgerInfo();
            DataTable dt = clsLdgInfo.BindDepot();
            if (dt.Rows.Count > 0)
            {
                this.gvdepot.DataSource = dt;
                this.gvdepot.DataBind();
            }
            else
            {
                this.gvdepot.DataSource = null;
                this.gvdepot.DataBind();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
        }
    }

    public void DepotMapping(string LedgerID)
    {
        try
        {
            ClsAccLedgerInfo clsLdgInfo = new ClsAccLedgerInfo();
            this.CreateDataTableDepotMapping();
            this.LoadDepot();

            DataTable dt = clsLdgInfo.BindEditDepot(LedgerID);
            if (dt.Rows.Count > 0)
            {
                foreach (GridViewRow gvrow in gvdepot.Rows)
                {
                    CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
                    Label depotid = (Label)gvrow.FindControl("lblBRID");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][2].ToString() == depotid.Text)
                        {
                            chk.Checked = true;
                        }
                    }
                }
            }

            this.pnlDisplay.Style["display"] = "none";
            this.pnlAdd.Style["display"] = "none";
            this.pnlledgerDepotMapping.Style["display"] = "";
            this.btnaddhide.Style["display"] = "none";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnDepotMapping_Click(object sender, EventArgs e)
    {
        try
        {
            this.DepotMapping(this.hdn_accid.Value);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btnDepotCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ClsAccLedgerInfo clsLdgInfo = new ClsAccLedgerInfo();
            this.hdn_accid.Value = "";
            this.txtledgernameMapping.Text = "";
            this.pnlDisplay.Style["display"] = "";
            this.pnlAdd.Style["display"] = "none";
            this.pnlledgerDepotMapping.Style["display"] = "none";
            this.btnaddhide.Style["display"] = "";
            this.gvdepot.DataSource = null;
            this.gvdepot.DataBind();
            HttpContext.Current.Session["DEPOTMAPPING"] = null;

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btnDepotSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsAccLedgerInfo clsLdgInfo = new ClsAccLedgerInfo();
            //CreateDataTableDepotMapping();
            int id = 0;
            DataTable dt = (DataTable)HttpContext.Current.Session["DEPOTMAPPING"];

            int count = 0;
            foreach (GridViewRow gvrow in gvdepot.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
                Label lblBRID = (Label)gvrow.FindControl("lblBRID");


                if (chk != null & chk.Checked)
                {
                    count = count + 1;
                    DataRow dr = dt.NewRow();

                    dr["LEDGERID"] = hdn_accid.Value.Trim();
                    dr["LEDGERNAME"] = txtledgernameMapping.Text.Trim();
                    dr["BRANCHID"] = lblBRID.Text;
                    dr["BRANCHNAME"] = gvrow.Cells[1].Text;
                    dt.Rows.Add(dr);
                    dt.AcceptChanges();
                }
            }
            if (count == 0)
            {
                MessageBox1.ShowInfo("Please select atleast 1 Depot", 60, 400);
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    string xml = ConvertDatatableToXML(dt);

                    id = clsLdgInfo.SaveDepotMapping(hdn_accid.Value.Trim(), xml);

                    if (id > 0)
                    {
                        MessageBox1.ShowSuccess("Record saved successfully..");

                        this.hdn_accid.Value = "";
                        this.txtledgernameMapping.Text = "";
                        this.pnlDisplay.Style["display"] = "";
                        this.pnlAdd.Style["display"] = "none";
                        this.pnlledgerDepotMapping.Style["display"] = "none";
                        this.btnaddhide.Style["display"] = "";
                        HttpContext.Current.Session["DEPOTMAPPING"] = null;
                        this.gvdepot.DataSource = null;
                        this.gvdepot.DataBind();
                    }
                    else
                    {
                        MessageBox1.ShowError("<b><font color='red'>Error saving records..!</font></b>");
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

    private void CreateDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("AccEntryID", typeof(string)));
        dt.Columns.Add(new DataColumn("InvoiceID", typeof(string)));
        dt.Columns.Add(new DataColumn("InvoiceNo", typeof(string)));
        dt.Columns.Add(new DataColumn("InvoiceDate", typeof(string)));
        dt.Columns.Add(new DataColumn("InvoiceAmt", typeof(string)));
        dt.Columns.Add(new DataColumn("AmtPaid", typeof(string)));
        dt.Columns.Add(new DataColumn("VoucherTypeID", typeof(string)));
        dt.Columns.Add(new DataColumn("VoucherTypeName", typeof(string)));
        dt.Columns.Add(new DataColumn("BranchID", typeof(string)));
        dt.Columns.Add(new DataColumn("BranchName", typeof(string)));
        dt.Columns.Add(new DataColumn("InvoiceBranchID", typeof(string)));
        dt.Columns.Add(new DataColumn("InvoiceBranchName", typeof(string)));
        dt.Columns.Add(new DataColumn("InvoiceOthers", typeof(string)));
        dt.Columns.Add(new DataColumn("Type", typeof(string)));
        HttpContext.Current.Session["ACC_INVOICEENTRY"] = dt;
    }

    protected void btnADDGrid_Click(object sender, EventArgs e)
    {
        try
        {
            int chkinvoiceno = 0;
            DataTable dtadd = new DataTable();

            if (HttpContext.Current.Session["ACC_INVOICEENTRY"] == null)
            {
                CreateDataTable(); ;
            }
            if (HttpContext.Current.Session["ACC_INVOICEENTRY"] != null)
            {
                dtadd = (DataTable)HttpContext.Current.Session["ACC_INVOICEENTRY"];
            }

            chkinvoiceno = dtadd.Select("InvoiceNo='" + Convert.ToString(this.txtinvoice.Text).Trim() + "'").Length;

            if (chkinvoiceno > 0)
            {
                MessageBox1.ShowInfo("Invoice No. already exist...");
                return;
            }
            else
            {
                DataRow dr = dtadd.NewRow();
                dr["AccEntryID"] = Convert.ToString("-1");
                dr["InvoiceID"] = Convert.ToString(Guid.NewGuid()).ToUpper();
                dr["InvoiceNo"] = Convert.ToString(this.txtinvoice.Text).Trim();
                dr["InvoiceDate"] = Convert.ToString(this.txtdate.Text).Trim();
                dr["InvoiceAmt"] = Convert.ToString(this.txtamount.Text).Trim();
                dr["AmtPaid"] = Convert.ToString(0);
                dr["VoucherTypeID"] = Convert.ToString(this.ddlvouchertype.SelectedValue).Trim();
                dr["VoucherTypeName"] = Convert.ToString(this.ddlvouchertype.SelectedItem.Text).Trim();
                dr["BranchID"] = Convert.ToString(this.ddlbranch.SelectedValue.Trim());
                dr["BranchName"] = Convert.ToString(this.ddlbranch.SelectedItem.Text.Trim());
                dr["InvoiceBranchID"] = Convert.ToString(this.ddlbranch.SelectedValue.Trim());
                dr["InvoiceBranchName"] = Convert.ToString(this.ddlbranch.SelectedItem.Text.Trim());
                dr["InvoiceOthers"] = Convert.ToString("");

                if (this.ddlvouchertype.SelectedValue == "9")   /* 9=Payment */
                {
                    dr["Type"] = Convert.ToString("Cr");
                }
                else
                {
                    dr["Type"] = Convert.ToString("Dr");
                }

                dtadd.Rows.Add(dr);
                dtadd.AcceptChanges();
            }

            HttpContext.Current.Session["ACC_INVOICEENTRY"] = dtadd;
            if (dtadd.Rows.Count > 0)
            {
                this.gvadd.DataSource = dtadd;
                this.gvadd.DataBind();
            }
            else
            {
                this.gvadd.DataSource = null;
                this.gvadd.DataBind();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void BindVoucherTypes()
    {
        try
        {
            this.ddlvouchertype.Items.Clear();
            this.ddlvouchertype.Items.Add(new ListItem("Select", "0"));
            this.ddlvouchertype.AppendDataBoundItems = true;
            this.ddlvouchertype.DataSource = clsLdgInfo.BindVoucherTypes();
            this.ddlvouchertype.DataTextField = "VoucherName";
            this.ddlvouchertype.DataValueField = "Id";
            this.ddlvouchertype.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void BindDepot()
    {
        try
        {
            this.ddlbranch.Items.Clear();
            this.ddlbranch.Items.Add(new ListItem("Select", "0"));
            this.ddlbranch.AppendDataBoundItems = true;
            this.ddlbranch.DataSource = clsLdgInfo.BindDepot();
            this.ddlbranch.DataTextField = "BRNAME";
            this.ddlbranch.DataValueField = "BRID";
            this.ddlbranch.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btn_TempDelete_Click(object sender, EventArgs e)
    {
        try
        {

            Button btn_views = (Button)sender;
            GridViewRow gvr = (GridViewRow)btn_views.NamingContainer;

            Label lblguid = (Label)gvr.FindControl("lbl1");

            //string value = gvr.Cells[2].Text.Trim();

            //string GUID = Convert.ToString(lblguid.Value);
            DataTable dtdeleterecord = new DataTable();
            dtdeleterecord = (DataTable)Session["ACC_INVOICEENTRY"];

            DataRow[] drr = dtdeleterecord.Select("AccEntryID='" + lblguid.Text + "'");
            for (int i = 0; i < drr.Length; i++)
            {
                drr[i].Delete();
                dtdeleterecord.AcceptChanges();
            }


            this.gvadd.DataSource = dtdeleterecord;
            this.gvadd.DataBind();



        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #region grdView_Exporting
    protected void grdView_Exporting(object sender, Obout.Grid.GridExportEventArgs e)
    {
        TableRow row = new TableRow();
        TableCell cell = new TableCell();

        cell.ColumnSpan = 5;
        cell.BorderStyle = BorderStyle.None;

        cell.Text = "Ledger Details";
        //TableHeaderRow row1 = new TableHeaderRow();
        //row1.Cells[1].Visible = false;
        cell.HorizontalAlign = HorizontalAlign.Center;
        cell.Font.Bold = true;
        cell.BackColor = Color.Gray;
        e.Table.GridLines = GridLines.None;
        row.Cells.Add(cell);
        e.Table.Rows.Add(row);
    }
    #endregion

    #region grdView_Exported
    protected void grdView_Exported(object sender, Obout.Grid.GridExportEventArgs e)
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
}