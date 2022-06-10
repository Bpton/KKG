using BAL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmMenuRights : System.Web.UI.Page
{
    ClsMenuActivity ClsMenuList = new ClsMenuActivity();
    DataTable dtMenu = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UTNAME"].ToString() != null)
            {
                if (Session["UTNAME"].ToString().ToLower().Trim() == "admin")
                {
                    string script = "$(document).ready(function () { $('[id*=pageloaddiv]').click(); });";
                    ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);

                    this.loadvouchertupe();
                    PopulateParentTreeView();
                    trmenulist.Visible = true;
                    this.divcredential.Style["display"] = "none";
                }
                else
                {
                    this.pnlterritory.Style["display"] = "none";
                    this.divcredential.Style["display"] = "";
                    string message = "alert('You have no permission to access this page, please check your credential!')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                }

                //Response.Redirect("frmAdminLogin.aspx");
            }
        }
    }

    public void loadvouchertupe()
    {
        try
        {
            ddlusertype.Items.Clear();
            ddlusertype.Items.Add(new ListItem("--Select User Type--", "0"));
            DataTable dt = ClsMenuList.BindUsertype();
            ddlusertype.AppendDataBoundItems = true;
            ddlusertype.DataSource = dt;
            ddlusertype.DataTextField = "UTNAME";
            ddlusertype.DataValueField = "UTID";
            ddlusertype.DataBind();
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }

    private void AddNodes(TreeNode node)
    {
        DataTable dtlevel = ClsMenuList.BindChildIdTree(node.Value);
        foreach (DataRow r in dtlevel.Rows)
        {
            TreeNode nNode = new TreeNode();
            nNode.Value = r["ChildID"].ToString();
            nNode.Text = r["DESCRIPTION"].ToString();
            AddNodes(nNode);
            node.ChildNodes.Add(nNode);
        }
    }

    private void AddChildNodes(TreeNode node)
    {
        DataTable dtlevel = ClsMenuList.BindChildIdTree(node.Value);
        foreach (DataRow r in dtlevel.Rows)
        {
            TreeNode nNode = new TreeNode();
            nNode.Value = r["ChildID"].ToString();
            nNode.Text = r["DESCRIPTION"].ToString();
            AddNodes(nNode);
            node.ChildNodes.Add(nNode);
        }
    }

    private void PopulateParentTreeView()
    {
        DataTable dtParent = ClsMenuList.BindParent();

        foreach (DataRow dr in dtParent.Rows)
        {
            TreeNode node = new TreeNode();
            node.Text = dr["DESCRIPTION"].ToString();
            node.Value = dr["ChildID"].ToString();
            PopulateTreeView(node);
            trmenulist.Nodes.Add(node);
        }

    }

    private void PopulateTreeView(TreeNode treeNode)
    {
        DataTable dtParent = ClsMenuList.BindParentTree(treeNode.Value);

        foreach (DataRow dr in dtParent.Rows)
        {
            TreeNode node = new TreeNode();
            node.Text = dr["DESCRIPTION"].ToString();
            node.Value = dr["ChildID"].ToString();
            AddNodes(node);
            treeNode.ChildNodes.Add(node);
        }
    }


    private void PopulateSelectUserMenu(string UserTypeID)
    {

        try
        {
            DataTable dtmenucredit = ClsMenuList.Populate(UserTypeID);

            foreach (DataRow row in dtmenucredit.Rows)
            {

                foreach (TreeNode node in trmenulist.Nodes)
                {
                    CheckUserMenu(row["MenuID"].ToString(), node);
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

    protected void ddlusertype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlusertype.SelectedValue != "0")
        {
            CheckUncheckTreeNode(trmenulist.Nodes, false);
            PopulateSelectUserMenu(ddlusertype.SelectedValue.ToString());
        }
        else
        {
            trmenulist.Nodes.Clear();
            PopulateParentTreeView();
            trmenulist.ExpandAll();
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
            if (this.ddlusertype.SelectedValue != "0")
            {
                InsertMenu(trmenulist);
                trmenulist.Nodes.Clear();
                PopulateParentTreeView();
                trmenulist.ExpandAll();
                string message = "alert('Menu rights saved successfully!')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                ddlusertype.SelectedValue = "0";
            }
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }

    protected void InsertMenu(TreeView tree)
    {
        try
        {
            ClsMenuList.DeleteUserType(ddlusertype.SelectedValue);

            foreach (TreeNode node in tree.Nodes)
            {
                InsertMenuSection(node);
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }
    private void InsertMenuSection(TreeNode node)
    {
        try
        {
            if (node.Checked)
            {
                int result = ClsMenuList.InsertMapping(node.Value, ddlusertype.SelectedValue.ToString().Trim());

            }
            foreach (TreeNode node2 in node.ChildNodes)
            {
                InsertMenuSection(node2);
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
            ddlusertype.SelectedValue = "0";
            trmenulist.Nodes.Clear();
            PopulateParentTreeView();
            trmenulist.ExpandAll();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void lnklogin_Click(object sender, EventArgs e)
    {
        try
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("frmAdminLogin.aspx");
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void lnkmenu_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("frmmenucreation.aspx");
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
}