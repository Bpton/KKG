using Account;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class VIEW_frmAccGroup : System.Web.UI.Page
{
    ClsAccGrp ClsGRp = new ClsAccGrp();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            txtName.Style.Add("color", "black !important");
            ddlgroup.Style.Add("color", "black !important");
            txtName.BackColor = SystemColors.Window;
            //TreeView1.Attributes.Add("onclick", "OnTreeClick(event)");
            divView.Style["display"] = "";
            diventry.Style["display"] = "none";
            //TreeView1.Visible = true;
            ViewState["mode"] = null;
            ViewState["code"] = null;
            PopulateParentTreeView();
            BindGrid("");
            TrvGroup.Visible = true;
            gvGroup.Visible = false;
            tblSearch.Visible = false;
            divgvgroup.Style["display"] = "none";
        }
    }

    private void AddNodes(TreeNode node)
    {
        DataTable dtlevel = ClsGRp.BindChildIdTree(node.Value);
        foreach (DataRow r in dtlevel.Rows)
        {
            TreeNode nNode = new TreeNode();
            nNode.Value = r["Code"].ToString();
            nNode.Text = r["grpName"].ToString();
            nNode.SelectAction = TreeNodeSelectAction.None;
            AddNodes(nNode);
            node.ChildNodes.Add(nNode);
        }
    }

    private void AddChildNodes(TreeNode node)
    {
        DataTable dtlevel = ClsGRp.BindChildIdTree(node.Value);
        foreach (DataRow r in dtlevel.Rows)
        {
            TreeNode nNode = new TreeNode();
            nNode.Value = r["Code"].ToString();
            nNode.Text = r["grpName"].ToString();
            nNode.SelectAction = TreeNodeSelectAction.None;
            AddNodes(nNode);
            node.ChildNodes.Add(nNode);
        }
    }

    private void PopulateParentTreeView()
    {
        DataTable dtParent = ClsGRp.BindLedgerPry();

        foreach (DataRow dr in dtParent.Rows)
        {
            TreeNode node = new TreeNode();
            node.Text = dr["name"].ToString();
            node.Value = dr["id"].ToString();
            node.SelectAction = TreeNodeSelectAction.None;
            PopulateTreeView(node);
            TrvGroup.Nodes.Add(node);
            TrvGroup.ExpandDepth = 10;
        }

    }

    private void BindGrid(string strSearch)
    {
        DataTable dt = new DataTable();

        dt = ClsGRp.BindGrid(strSearch);
        if (dt.Rows.Count > 0)
        {
            gvGroup.DataSource = dt;
            gvGroup.DataBind();
        }
    }

    private void PopulateTreeView(TreeNode treeNode)
    {
        DataTable dtParent = ClsGRp.BindParentTree(treeNode.Value);

        foreach (DataRow dr in dtParent.Rows)
        {
            TreeNode node = new TreeNode();
            node.Text = dr["grpname"].ToString();
            node.Value = dr["code"].ToString();
            node.SelectAction = TreeNodeSelectAction.None;
            AddNodes(node);

            treeNode.ChildNodes.Add(node);
        }
    }

    protected void BindLedgerDDL()
    {

        ddlgroup.Items.Clear();
        ddlgroup.Items.Add(new ListItem("Select Group", "0"));
        ddlgroup.AppendDataBoundItems = true;
        ddlgroup.DataSource = ClsGRp.BindLedger();
        ddlgroup.DataValueField = "code";
        ddlgroup.DataTextField = "grpName";
        ddlgroup.DataBind();

    }

    protected void BindPrymLedgerDDL()
    {

        this.ddlgroup.Items.Clear();
        this.ddlgroup.Items.Add(new ListItem("Select", "0"));
        this.ddlgroup.AppendDataBoundItems = true;
        this.ddlgroup.DataSource = ClsGRp.BindLedgerPry();
        this.ddlgroup.DataTextField = "Name";
        this.ddlgroup.DataValueField = "Id";
        this.ddlgroup.DataBind();
    }

    protected void chkpry_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkpry.Checked == true)
            {
                BindPrymLedgerDDL();
            }
            else
            {
                BindLedgerDDL();
            }
        }
        catch (Exception ex)
        {
            MessageBox1.ShowInfo("Error: " + ex);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string IDDuid = string.Empty;
            if (Convert.ToString(ViewState["mode"]) == "A")
            {
                IDDuid = Guid.NewGuid().ToString();
            }
            else
            {
                IDDuid = Convert.ToString(ViewState["code"]);
            }
            int Id = 0;
            string parentid = string.Empty;
            string TypID = "";
            string IsPrm = "";
            string Prdfn = "";
            int Level = 0;
            string dtLevel = "";


            if (chkpry.Checked == true)
            {
                parentid = null;
                TypID = ddlgroup.SelectedValue;
                IsPrm = "1";
            }
            else
            {
                parentid = ddlgroup.SelectedValue;
                TypID = ClsGRp.BindTypId(ddlgroup.SelectedValue);
                IsPrm = "0";
            }
            dtLevel = ClsGRp.BindLevel(ddlgroup.SelectedValue);
            Level = Convert.ToInt32(dtLevel);
            Level = Level + 1;

            Id = ClsGRp.SaveGrp(Convert.ToString(ViewState["mode"]), IDDuid, TypID, txtName.Text.Trim(), parentid, IsPrm, Prdfn, Level, Convert.ToInt32(this.txtsequence.Text.Trim()));

            if (Id > 0)
            {
                //string message = "alert('Inserted successfully!!')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                if (Convert.ToString(ViewState["mode"]) == "D")
                {
                    MessageBox1.ShowSuccess("<b>Record <font color='red'>deleted</font> successfully</b>");
                }
                else
                {
                    MessageBox1.ShowSuccess("<b><font color='green'>Record save successfully!</font></b>");
                }
                txtName.Text = "";
                BindGrid("");
                BindLedgerDDL();
                this.divView.Style["display"] = "";
                this.diventry.Style["display"] = "none";
                TrvGroup.Nodes.Clear();
                PopulateParentTreeView();
                TrvGroup.ExpandAll();
                btnSave.Text = "Save";
                this.txtsequence.Text = "";
            }
            else if (Id == -2)
            {
                MessageBox1.ShowInfo("<b>Record <font color='red'>" + txtName.Text + "</font> alredy exist!</b>", 60, 450);
            }
            else
            {
                //string message = "alert('Alredy Exist!!')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowError("<b><font color='red'>Record save unsuccessfull!</font></b>");
                BindLedgerDDL();
                txtName.Text = "";
                BindGrid("");
                TrvGroup.Nodes.Clear();
                PopulateParentTreeView();
                TrvGroup.ExpandAll();
                this.txtsequence.Text = "";

            }
            btnaddhide.Style["display"] = "";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        btnSave.Text = "Save";
        Response.Redirect("frmAccGroup.aspx");
    }

    protected void Btnadd_Click(object sender, EventArgs e)
    {
        ViewState["mode"] = "A";
        chkpry.Checked = false;
        BindLedgerDDL();
        divView.Style["display"] = "none";
        diventry.Style["display"] = "";
        btnaddhide.Style["display"] = "none";
        btnSave.Text = "Save";
    }

    protected void rdGridView_CheckedChanged(object sender, EventArgs e)
    {
        tblSearch.Visible = true;
        gvGroup.Visible = true;
        TrvGroup.Visible = false;
        divgvgroup.Style["display"] = "";
    }
    protected void rdTreeView_CheckedChanged(object sender, EventArgs e)
    {
        tblSearch.Visible = false;
        gvGroup.Visible = false;
        TrvGroup.Visible = true;
        divgvgroup.Style["display"] = "none";
        TrvGroup.Nodes.Clear();
        PopulateParentTreeView();
        TrvGroup.ExpandAll();
    }
    protected void gvGroup_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ViewState["mode"] = "E";
        Label parentId = gvGroup.Rows[e.NewEditIndex].FindControl("lblParentId") as Label;
        Label Id = gvGroup.Rows[e.NewEditIndex].FindControl("lblId") as Label;
        Label code = gvGroup.Rows[e.NewEditIndex].FindControl("lblCode") as Label;
        Label parentGrp = gvGroup.Rows[e.NewEditIndex].FindControl("lblParentGrp") as Label;
        Label childGrp = gvGroup.Rows[e.NewEditIndex].FindControl("lblChildGrp") as Label;
        Label primaryGrp = gvGroup.Rows[e.NewEditIndex].FindControl("lblPrimaryGrp") as Label;
        CheckBox isPrimary = gvGroup.Rows[e.NewEditIndex].FindControl("chkisPrimary") as CheckBox;
        Label lblsequence = gvGroup.Rows[e.NewEditIndex].FindControl("lblsequence") as Label;
        divView.Style["display"] = "none";
        diventry.Style["display"] = "";
        txtName.Text = childGrp.Text;

        if (isPrimary.Checked == true)
        {
            chkpry.Checked = true;
            BindPrymLedgerDDL();
            ddlgroup.SelectedValue = Id.Text;
            this.txtsequence.Text = lblsequence.Text;
        }
        else
        {
            chkpry.Checked = false;
            BindLedgerDDL();
            ddlgroup.SelectedValue = parentId.Text;
            this.txtsequence.Text = lblsequence.Text;
        }
        ViewState["code"] = code.Text;
        btnaddhide.Style["display"] = "none";
    }

    protected void gvGroup_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ViewState["mode"] = "D";
        Label parentId = gvGroup.Rows[e.RowIndex].FindControl("lblParentId") as Label;
        Label Id = gvGroup.Rows[e.RowIndex].FindControl("lblId") as Label;
        Label code = gvGroup.Rows[e.RowIndex].FindControl("lblCode") as Label;
        Label parentGrp = gvGroup.Rows[e.RowIndex].FindControl("lblParentGrp") as Label;
        Label childGrp = gvGroup.Rows[e.RowIndex].FindControl("lblChildGrp") as Label;
        Label primaryGrp = gvGroup.Rows[e.RowIndex].FindControl("lblPrimaryGrp") as Label;
        CheckBox isPrimary = gvGroup.Rows[e.RowIndex].FindControl("chkisPrimary") as CheckBox;
        Label lblsequence = gvGroup.Rows[e.RowIndex].FindControl("lblsequence") as Label;
        divView.Style["display"] = "none";
        diventry.Style["display"] = "";
        txtName.Text = childGrp.Text;

        if (isPrimary.Checked == true)
        {
            chkpry.Checked = true;
            BindPrymLedgerDDL();
            ddlgroup.SelectedValue = Id.Text;
            this.txtsequence.Text = lblsequence.Text;
        }
        else
        {
            chkpry.Checked = false;
            BindLedgerDDL();
            ddlgroup.SelectedValue = parentId.Text;
            this.txtsequence.Text = lblsequence.Text;
        }
        ViewState["code"] = code.Text;
        btnSave.Text = "Delete";
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid(txtSearch.Text);
    }
}