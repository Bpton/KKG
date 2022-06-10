using System;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

public partial class UserControl_ucMenu : System.Web.UI.UserControl
{
    //TreeNode tr = new TreeNode();
    Utility.ClsMenu objmenu = new Utility.ClsMenu();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["UTypeId"] == null || Session["UTypeId"] == "")
                {
                    Session["UTypeId"] = "1";
                }
                PopulateTree("0");// root node 

                //Tree();
                //TreeView1.ExpandDepth = 0;               
                //  BindData(trP.Value);
            }
        }
        catch (Exception exp)
        {
            HttpContext.Current.Trace.Warn("Error in GetCustomerByID()", exp.Message, exp);
        }
    }
    protected void PopulateTree(string val)
    {
        try
        {
            Utility.ClsMenu objmenu = new Utility.ClsMenu();
            Utility.ClsMenu objSubmenu = new Utility.ClsMenu();

            StringBuilder sb = new StringBuilder();
            string strtemp = "";
            Utility.ClsMenu mn = new Utility.ClsMenu();
            Utility.ClsMenu mn1 = new Utility.ClsMenu();
            Utility.ClsMenu mn2 = new Utility.ClsMenu();
            ArrayList MenuList = objmenu.GetMenu(val, Session["UTypeId"].ToString().Trim());
            ArrayList SUBMenuList;
            ArrayList SUBMenuList1;

            if (MenuList != null)
            {
                strtemp = @"<div id=""leftMenu""><nav><h2 class=""icon-heading""><i class=""fa fa-reorder""></i>Menu list</h2><ul class=""first"">";
                sb.Append(strtemp);
                for (int i = 0; i < MenuList.Count; i++)
                {
                    mn = (Utility.ClsMenu)MenuList[i];

                    string menuhead = mn.PageName;
                    strtemp = @"<li><a href=""#""><i class=""fa fa-sort""></i>" + mn.PageName + @"</a>
                                <h2><i class=""fa fa-reorder""></i>" + mn.PageName + "</h2>";
                    sb.Append(strtemp);

                    SUBMenuList = GetPersonNames(mn.ChildID);
                    if (SUBMenuList.Count > 0)
                    {
                        strtemp = @"<ul>";
                        sb.Append(strtemp);

                        int k = 1;
                        for (int ii = 0; ii < SUBMenuList.Count; ii++)
                        {
                            mn1 = (Utility.ClsMenu)SUBMenuList[ii];
                            if (mn.ParentID != "-1")
                            {
                                SUBMenuList1 = GetPersonNames(mn1.ChildID);

                                if (mn2.ParentID != "-1")
                                {
                                    if (mn.FolderName.Trim().ToUpper() == "FACTORY")
                                    {
                                        strtemp = @"<li><a href=""../" + mn.FolderName + "/" + mn.PageUrl + @"""><i class=""fa fa-sort""></i>" + mn1.PageName + @"</a>
                                                <h2><i class=""fa fa-reorder""></i>" + mn1.PageName + "</h2>";
                                    }
                                    else if (mn.FolderName.Trim().ToUpper() == "VIEW")
                                    {
                                        strtemp = @"<li><a href=""" + mn.PageUrl + @"""><i class=""fa fa-sort""></i>" + mn1.PageName + @"</a>
                                                <h2><i class=""fa fa-reorder""></i>" + mn1.PageName + "</h2>";
                                    }
                                    else if (mn.FolderName.Trim().ToUpper() == "FAC")
                                    {
                                        strtemp = @"<li><a href=""" + mn.PageUrl + @"""><i class=""fa fa-sort""></i>" + mn1.PageName + @"</a>
                                                <h2><i class=""fa fa-reorder""></i>" + mn1.PageName + "</h2>";
                                    }
                                    //strtemp = @"<li><a href=""" + mn.PageUrl  +@"""><i class=""fa fa-sort""></i>" + mn1.PageName + @"</a><h2><i class=""fa fa-tags""></i>" + mn1.PageName + "</h2>";
                                    sb.Append(strtemp);
                                }
                                else
                                {
                                    if (mn.FolderName.Trim().ToUpper() == "FACTORY")
                                    {
                                        strtemp = @"<li ><a href=""#"" onclick=""addTab('" + mn1.PageName + @"','../" + mn.FolderName + "/" + mn.PageUrl + @"')"">" + mn1.PageName + @"</a>";
                                        sb.Append(strtemp);
                                    }
                                    else if (mn.FolderName.Trim().ToUpper() == "VIEW")
                                    {
                                        strtemp = @"<li ><a href=""#"" onclick=""addTab('" + mn1.PageName + @"','" + mn.PageUrl + @"')"">" + mn1.PageName + @"</a>";
                                        sb.Append(strtemp);
                                    }
                                    else
                                    {

                                    }
                                }
                                if (SUBMenuList1.Count > 0)
                                {
                                    mn = (Utility.ClsMenu)MenuList[i];

                                    if (mn.ParentID != "-1")
                                    {
                                        strtemp = @"<ul>";
                                        sb.Append(strtemp);

                                        int k1 = 1;
                                        for (int ii1 = 0; ii1 < SUBMenuList1.Count; ii1++)
                                        {

                                            mn1 = (Utility.ClsMenu)SUBMenuList1[ii1];
                                            if (mn1.ParentID != "-1")
                                            {
                                                if (mn1.FolderName.Trim().ToUpper() == "FACTORY")
                                                {
                                                    strtemp = @"<li ><a href=""#"" onclick=""addTab('" + mn1.PageName + @"','../" + mn1.FolderName + "/" + mn1.PageUrl + @"')"">" + mn1.PageName + @"</a></li>";
                                                    sb.Append(strtemp);
                                                }
                                                else
                                                if (mn1.FolderName.Trim().ToUpper() == "VIEW")
                                                {
                                                    strtemp = @"<li ><a href=""#"" onclick=""addTab('" + mn1.PageName + @"','" + mn1.PageUrl + @"')"">" + mn1.PageName + @"</a></li>";
                                                    sb.Append(strtemp);
                                                }
                                                else
                                                    if (mn1.FolderName.Trim().ToUpper() == "FAC")
                                                {
                                                    strtemp = @"<li ><a href=""#"" onclick=""addTab('" + mn1.PageName + @"','" + mn1.PageUrl + @"')"">" + mn1.PageName + @"</a></li>";
                                                   // strtemp = @"<li ><a href='" + mn1.PageUrl + @"' target=""_blank"" onclick=""addTab(##)"">" + mn1.PageName + @"</a></li>";

                                                   // strtemp = @"<li ><a href=""http://www.google.com"" @</a></li>";
                                                    sb.Append(strtemp);
                                                }
                                                k1++;
                                            }
                                        }

                                        strtemp = " </ul>";
                                        sb.Append(strtemp);
                                    }
                                }
                                strtemp = "</li>";
                                sb.Append(strtemp);
                            }
                        }
                        k++;
                        strtemp = "</ul>";
                        sb.Append(strtemp);
                    }
                    strtemp = "</li>";
                    sb.Append(strtemp);
                }

                strtemp = @"</ul></nav><p class=""vert"">Menu</p></div>";
                sb.Append(strtemp);
                menubar.InnerHtml = sb.ToString();
            }
        }
        catch (Exception exp)
        {
            HttpContext.Current.Trace.Warn("Error in GetCustomerByID()", exp.Message, exp);
        }
    }

    protected void BindData(string topicid)
    {
        try
        {
            ArrayList MenuList = objmenu.GetMenu(topicid, HttpContext.Current.Session["UTypeId"].ToString());
            if (MenuList.Count > 0)
            {
                //DataList1.DataSource = MenuList;// ((BusinessLayer.HRMAN)MenuList[0]).TopicDetails.ToString();
                //DataList1.DataBind();
            }
        }
        catch (Exception ex)
        {
            string strErr = ex.Message;
        }
    }

    protected void PopulateChildNode(TreeNode trP)
    {
        ArrayList MenuList = GetPersonNames(trP.Value);

        if (MenuList != null)
        {
            for (int i = 0; i < MenuList.Count; i++)
            {
                //TreeNode trC = new TreeNode();
                if (((Utility.ClsMenu)MenuList[i]).ParentID.ToString() != "-1")
                {
                    TreeNode trC = new TreeNode();
                    trC.Value = ((Utility.ClsMenu)MenuList[i]).ChildID.ToString();
                    trC.Text = ((Utility.ClsMenu)MenuList[i]).PageName.ToString();
                    trC.NavigateUrl = ((Utility.ClsMenu)MenuList[i]).PageUrl.ToString();
                    trC.SelectAction = TreeNodeSelectAction.SelectExpand;
                    //trP.ChildNodes.Add(trC);
                    trP.ChildNodes.Add(trC);
                    PopulateChildNode(trC);
                    trC = null;
                }
            }
        }
    }

    public ArrayList GetPersonNames(string val)
    {
        ArrayList arrmenu = new ArrayList();
        Utility.ClsMenu objmenu = new Utility.ClsMenu();
        arrmenu = objmenu.GetMenu(val, Session["UTypeId"].ToString().Trim());
        return arrmenu;
    }

    private void ExpandToRoot(TreeNode node)
    {
        node.Expand();
        if (node.Parent != null)
        {
            ExpandToRoot(node.Parent);
        }
    }
}