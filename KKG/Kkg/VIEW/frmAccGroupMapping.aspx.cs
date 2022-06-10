using Account;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class VIEW_frmAccGroupMapping : System.Web.UI.Page
{
    ClsAccGrp ClsGRp = new ClsAccGrp();
    DataTable dtMenu = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string script = "$(document).ready(function () { $('[id*=pageloaddiv]').click(); });";
            ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);

            this.loadvouchertupe();
            PopulateParentTreeViewDebit();
            PopulateParentTreeViewCredit();

            trvwcredit.Visible = true;
            trvwdebit.Visible = true;
        }
    }

    public void loadvouchertupe()
    {
        try
        {
            ddlvouchertype.Items.Clear();
            ddlvouchertype.Items.Add(new ListItem("--Select Voucher Type--", "0"));
            DataTable dt = ClsGRp.Bindvouchertype();
            ddlvouchertype.AppendDataBoundItems = true;
            ddlvouchertype.DataSource = dt;
            ddlvouchertype.DataTextField = "VOUCHERNAME";
            ddlvouchertype.DataValueField = "ID";
            ddlvouchertype.DataBind();
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
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
            AddNodes(nNode);
            node.ChildNodes.Add(nNode);
        }
    }
    private void PopulateParentTreeViewDebit()
    {
        DataTable dtParent = ClsGRp.BindLedgerPry();


        foreach (DataRow dr in dtParent.Rows)
        {
            TreeNode node = new TreeNode();
            node.Text = dr["name"].ToString();
            node.Value = dr["id"].ToString();
            PopulateTreeView(node);
            trvwdebit.Nodes.Add(node);
        }

    }
    private void PopulateParentTreeViewCredit()
    {
        DataTable dtParent = ClsGRp.BindLedgerPry();


        foreach (DataRow dr in dtParent.Rows)
        {
            TreeNode node = new TreeNode();
            node.Text = dr["name"].ToString();
            node.Value = dr["id"].ToString();
            PopulateTreeView(node);
            trvwcredit.Nodes.Add(node);
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
            AddNodes(node);
            treeNode.ChildNodes.Add(node);
        }
    }


    private void PopulateSelectUserMenudebit(string VoucherTypeID)
    {

        try
        {
            DataTable dtmenudebit = ClsGRp.Populateaccgroupmappingdeit(VoucherTypeID);

            foreach (DataRow row in dtmenudebit.Rows)
            {

                foreach (TreeNode node in trvwdebit.Nodes)
                {
                    CheckUserMenu(row["AccGrpId"].ToString(), node);
                }
            }
            DataTable dtmenucredit = ClsGRp.Populateaccgroupmappingcredit(VoucherTypeID);

            foreach (DataRow row in dtmenucredit.Rows)
            {

                foreach (TreeNode node in trvwcredit.Nodes)
                {
                    CheckUserMenu(row["AccGrpId"].ToString(), node);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private void CheckUserMenu(string menuid, TreeNode node)
    {
        try
        {
            if (menuid == node.Value)
            {
                node.Checked = true;
            }
            else
            {
                foreach (TreeNode node2 in node.ChildNodes)
                {
                    CheckUserMenu(menuid, node2);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void ddlvouchertype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlvouchertype.SelectedValue != "0")
        {
            CheckUncheckTreeNode(trvwdebit.Nodes, false);
            CheckUncheckTreeNode(trvwcredit.Nodes, false);
            PopulateSelectUserMenudebit(ddlvouchertype.SelectedValue.ToString());
        }
        else
        {
            trvwcredit.Nodes.Clear();
            PopulateParentTreeViewCredit();
            trvwcredit.ExpandAll();
            trvwdebit.Nodes.Clear();
            PopulateParentTreeViewDebit();
            trvwdebit.ExpandAll();
        }
    }

    #region UncheckAllTree Npdes
    private void CheckUncheckTreeNode(TreeNodeCollection trNodeCollection, bool isCheck)
    {
        foreach (TreeNode trNode in trNodeCollection)
        {
            trNode.Checked = isCheck;
            if (trNode.ChildNodes.Count > 0)
                CheckUncheckTreeNode(trNode.ChildNodes, isCheck);
        }
    }
    #endregion


    protected void Add_Click(object sender, EventArgs e)
    {
        try
        {
            InsertMenu(trvwcredit, "C");
            InsertMenu(trvwdebit, "D");
            string message = "alert('Group mapping saved successfully!')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            ddlvouchertype.SelectedValue = "0";
            trvwcredit.Nodes.Clear();
            PopulateParentTreeViewCredit();
            trvwcredit.ExpandAll();
            trvwdebit.Nodes.Clear();
            PopulateParentTreeViewDebit();
            trvwdebit.ExpandAll();
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }

    protected void InsertMenu(TreeView tree, string tag)
    {
        try
        {
            ClsGRp.Deleteaccgroup(ddlvouchertype.SelectedValue, tag);

            foreach (TreeNode node in tree.Nodes)
            {
                InsertMenuSection(node, tag);
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }
    private void InsertMenuSection(TreeNode node, string tag)
    {
        try
        {
            if (node.Checked)
            {
                int result = ClsGRp.InsertAccGroupMapping(node.Value, ddlvouchertype.SelectedValue.ToString().Trim(), tag, hdnPid.Value);

            }
            foreach (TreeNode node2 in node.ChildNodes)
            {
                InsertMenuSection(node2, tag);
            }

        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void Cancel_close(object sender, EventArgs e)
    {
        try
        {
            ddlvouchertype.SelectedValue = "0";
            trvwcredit.Nodes.Clear();
            PopulateParentTreeViewCredit();
            trvwcredit.ExpandAll();
            trvwdebit.Nodes.Clear();
            PopulateParentTreeViewDebit();
            trvwdebit.ExpandAll();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
}