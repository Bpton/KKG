using BAL;
using PPBLL;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

public partial class VIEW_frmProcessFramework : System.Web.UI.Page
{
    Process oProcess = new Process();
    BAL.ClsCommonFunction ClsCommon = new BAL.ClsCommonFunction();
    ProcessFramework oProcessFramework = new ProcessFramework();
    ClsWorkstationMaster oClsWorkstationMaster = new ClsWorkstationMaster();
    ClsQCMaster oClsQCMaster = new ClsQCMaster();
    ClsProductMaster oClsProductMaster = new ClsProductMaster();
    ClsItemType oClsItemType = new ClsItemType();
    DataTable dt = new DataTable();
    string str;

    public class Product
    {
        public string ProcessID { get; set; }
        public string ProcessCode { get; set; }
        public string ProcessName { get; set; }
        public string ProcessTotalDuration { get; set; }
    }

    #region Page Event
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "CreateGridHeader", "<script>CreateGridHeader('DataDiv', 'gvResourceSource', 'HeaderDiv');</script>");
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", " <script type='text/javascript'>$(function () {$('#ContentPlaceHolder1_ddlMaterialType').multiselect({ includeSelectAllOption: true });});</script>", false);

            if (!IsPostBack)
            {
                ResetSession();
                pnlAdd.Style["display"] = "none";
                LoadProcessFramework();
                LoadProcessMaster();
                LoadProduct();
                gvResourceSource.DataSource = CreateDataTableForResourceSource();
                gvResourceSource.DataBind();
                gvWorkstaionSource.DataSource = CreateDataTableForWorkStationSource();
                gvWorkstaionSource.DataBind();
                gvSourceMaterialInput.DataSource = CreateDataTableForMaterialInputSource();
                gvSourceMaterialInput.DataBind();
                gvSourceMaterialOutput.DataSource = CreateDataTableForMaterialOutputSource();
                gvSourceMaterialOutput.DataBind();
                dt.Columns.AddRange(new DataColumn[4]
                {
                new DataColumn("ProcessID"),
                new DataColumn("ProcessCode"),
                new DataColumn("ProcessName"),
                new DataColumn("ProcessTotalDuration")

                });

                dt.Rows.Clear();
                dt.Rows.Add();
                gvDest.DataSource = dt;
                gvDest.DataBind();
            }

            /*Add By Rajeev*/
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "script", "BindTabFunctions();", true);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "script1", "BindDragDropGrid();", true);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "script2", "BindQCGridForChkboxChckd();", true);
            /*End*/

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeaderWorkStationSource('" + gvWorkstaionSource.ClientID + "', 200, '100%' , 30 ,false); </script>", false);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key1", "<script>MakeStaticHeaderWorkStationDestination('" + gvWorkstaionDestination.ClientID + "', 200, '100%' , 30 ,false); </script>", false);
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key2", "<script>MakeStaticHeaderResourceSource('" + gvResourceSource.ClientID + "', 200, '100%' , 30 ,false); </script>", false);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key3", "<script>MakeStaticHeaderResourceDestination('" + gvResourceDestination.ClientID + "', 200, '100%' , 30 ,false); </script>", false);

            //ClientScript.RegisterStartupScript(Page, this.GetType(), "Key4", "<script>MakeStaticHeaderBOMSource('" + gvSourceMaterialInput.ClientID + "', 200, '100%', 30,false); </script>", false);
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Javascript", "javascript:MakeStaticHeaderBOMSource('" + gvSourceMaterialInput.ClientID + "', 200, '100%', 30,false); ", true);

            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key4", "<script>MakeStaticHeaderBOMSource('" + gvSourceMaterialInput.ClientID + "', 200, '100%', 30,false); </script>", false);
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key5", "<script>MakeStaticHeaderBOMDestination('" + gvDestMaterialInput.ClientID + "', 200, '100%', 30,false); </script>", false);
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key6", "<script>MakeStaticHeaderOutputSource('" + gvSourceMaterialOutput.ClientID + "', 200, '100%' , 30 ,false); </script>", false);

            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key7", "<script>MakeStaticHeaderOutputDestination('" + gvDestMaterialOutput.ClientID + "', 200, '100%' , 30 ,false); </script>", false);
        }
        catch (Exception ex)
        {
            string msg = ex.ToString();
        }
    }

    #endregion

    #region Events
    /// <summary>
    /// Add process framework
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddProcessFramework_Click(object sender, EventArgs e)
    {
        try
        {
            ResetAll();
            pnlDisplay.Style["display"] = "none";
            pnlAdd.Style["display"] = "";

            dt.Rows.Clear();
            dt.Rows.Add();
            gvDest.DataSource = dt;
            gvDest.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    /// <summary>
    /// Cancel Record
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            txtProcessFrameworkCode.Text = "";
            txtProcessFrameworkName.Text = "";
            txtFrameworkDescription.Text = "";
            hdnfProcessFrameworkID.Value = "";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            LoadProcessFramework();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    /// <summary>
    /// Cancel Record
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            this.light.Style["display"] = "none";
            this.fade.Style["display"] = "none";
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    public static DataTable GetFilteredData1(DataTable table, string filterValues, string Condition)
    {
        var dv = new DataView(table);
        var filter = string.Join("','", filterValues);
        dv.RowFilter = Condition + " NOT IN ('" + filter + "')";
        return dv.ToTable();
    }

    public DataTable GetFilteredData(DataTable table, string filterValues, string Condition)
    {
        var dv = new DataView(table);
        var filter = string.Join("','", filterValues);
        dv.RowFilter = Condition + " NOT IN ('" + filter + "')";
        return dv.ToTable();
    }

    public DataTable GetDataExist(DataTable table, string filterValues, string Condition)
    {
        var dv = new DataView(table);
        var filter = string.Join("','", filterValues);
        dv.RowFilter = Condition + " IN ('" + filter + "')";
        return dv.ToTable();
    }

    public DataTable GetDataExist(DataTable table, string[] filterValues, string Condition)
    {
        var dv = new DataView(table);
        var filter = string.Join("','", filterValues);
        dv.RowFilter = Condition + " IN ('" + filter + "')";
        return dv.ToTable();
    }

    public DataTable GetDataExistInDrop(DataTable table, string filterValues, string Condition)
    {
        var dv = new DataView(table);
        //var filter = string.Join("','", filterValues);
        dv.RowFilter = Condition + " NOT IN ('FG')";
        return dv.ToTable();
    }

    public DataTable GetDataExistInFGSFG(DataTable table, string filterValues, string Condition)
    {
        var dv = new DataView(table);
        //var filter = string.Join("','", filterValues);
        dv.RowFilter = Condition + " IN ('FG','SFG','PM','RM','TRADING','PF')";
        return dv.ToTable();
    }

    public DataTable GetSourceDataExist(DataTable table, string filterFirstValues, string filterSecondValues, string FirstCondition, string SecondCondition)
    {
        var dv = new DataView(table);
        if (dv.Count > 0)
        {
            var filter = string.Join("','", filterFirstValues);
            dv.RowFilter = FirstCondition + " IN ('" + filter + "') AND " + SecondCondition + " IN ('" + filterSecondValues + "')";
        }
        return dv.ToTable();
    }

    private void getMaterialInputSelectedGridInfo()
    {
        try
        {

            //HttpContext.Current.Session["MaterialInputDestination"] = ds.Tables[3];
            string strMaterialQTY = string.Empty;
            DataTable dtMaterialInputSource = new DataTable();
            DataTable dtMaterialInputDestination = new DataTable();
            DataRow dr;
            if (HttpContext.Current.Session["MaterialInputSource"] == null)
            {
                dtMaterialInputSource = CreateDataTableForMaterialInputSource();
            }
            else
            {
                dtMaterialInputSource = (DataTable)HttpContext.Current.Session["MaterialInputSource"];
            }
            if (HttpContext.Current.Session["MaterialInputDestination"] == null)
            {
                dtMaterialInputDestination = CreateDataTableForMaterialInputDestination();
            }
            else
            {
                dtMaterialInputDestination = (DataTable)HttpContext.Current.Session["MaterialInputDestination"];
            }
            dt = GetDataExist(dtMaterialInputSource, hdfProcessID.Value, "ProcessID");

            if (dt.Rows.Count > 0)
            {
                for (int i = dtMaterialInputSource.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow drMaterialInput = dtMaterialInputSource.Rows[i];
                    if (Convert.ToString(drMaterialInput[0]) == hdfProcessID.Value)
                    {
                        dtMaterialInputSource.Rows.Remove(drMaterialInput);
                    }
                }
            }
            foreach (GridViewRow row in gvSourceMaterialInput.Rows)
            {
                Label lblMaterialInputID = (Label)row.FindControl("lblMaterialInputID");
                Label lblMaterialInputCode = (Label)row.FindControl("lblMaterialInputCode");
                Label lbllMaterialInputName = (Label)row.FindControl("lbllMaterialInputName");
                Label lbllMaterialInputType = (Label)row.FindControl("lbllMaterialInputType");
                DropDownList ddlUnit = (DropDownList)row.FindControl("ddlUnit");
                TextBox txtQty = (TextBox)row.FindControl("txtQty");
                TextBox txtRefQty = (TextBox)row.FindControl("txtRefQty");

                if (txtQty.Text.Trim() != null && txtQty.Text.Trim() != string.Empty)
                {
                    strMaterialQTY = txtQty.Text.Replace(",", "").Trim();
                    if (strMaterialQTY != null && strMaterialQTY != string.Empty)
                    {
                        dr = dtMaterialInputDestination.NewRow();
                        dr[2] = hdfProcessID.Value;
                        dr[3] = lblMaterialInputID.Text;
                        dr[4] = lblMaterialInputCode.Text;
                        dr[5] = lbllMaterialInputName.Text;
                        dr[6] = lbllMaterialInputType.Text;
                        dr[7] = ddlUnit.SelectedValue;
                        dr[8] = ddlUnit.SelectedItem.Text;
                        dr[9] = txtQty.Text;
                        dr[10] = "1";
                        dr[11] = txtRefQty.Text;
                        dtMaterialInputDestination.Rows.Add(dr);
                    }
                    else
                    {
                        dr = dtMaterialInputSource.NewRow();
                        dr[0] = hdfProcessID.Value;
                        dr[1] = lblMaterialInputID.Text;
                        dr[2] = lblMaterialInputCode.Text;
                        dr[3] = lbllMaterialInputName.Text;
                        dr[4] = lbllMaterialInputType.Text;
                        dr[5] = ddlUnit.SelectedValue;
                        dr[6] = txtQty.Text;
                        dr[7] = "1";
                        dr[8] = txtRefQty.Text;
                        dtMaterialInputSource.Rows.Add(dr);
                    }
                }
            }
            HttpContext.Current.Session["MaterialInputSource"] = dtMaterialInputSource;
            HttpContext.Current.Session["MaterialInputDestination"] = dtMaterialInputDestination;
            LoadSourceMaterialInput(hdfProcessID.Value);
            LoadDestinationMaterialInput(hdfProcessID.Value);
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }
    private void getMaterialOutputSelectedGridInfo()
    {
        try
        {
            string strMaterialQTY = string.Empty;
            DataTable dtMaterialOutputSource = new DataTable();
            DataTable dtMaterialOutputDestination = new DataTable();
            DataRow dr;
            if (HttpContext.Current.Session["MaterialOutputSource"] == null)
            {
                dtMaterialOutputSource = CreateDataTableForMaterialOutputSource();
            }
            else
            {
                dtMaterialOutputSource = (DataTable)HttpContext.Current.Session["MaterialOutputSource"];
            }
            if (HttpContext.Current.Session["MaterialOutputDestination"] == null)
            {
                dtMaterialOutputDestination = CreateDataTableForMaterialOutputDestination();
            }
            else
            {
                dtMaterialOutputDestination = (DataTable)HttpContext.Current.Session["MaterialOutputDestination"];
            }

            dt = GetDataExist(dtMaterialOutputSource, hdfProcessID.Value, "ProcessID");
            if (dt.Rows.Count > 0)
            {
                for (int i = dtMaterialOutputSource.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow drMaterialInput = dtMaterialOutputSource.Rows[i];
                    if (Convert.ToString(drMaterialInput[0]) == hdfProcessID.Value)
                    {
                        dtMaterialOutputSource.Rows.Remove(drMaterialInput);
                    }
                }
            }
            foreach (GridViewRow row in gvSourceMaterialOutput.Rows)
            {
                Label lblMaterialOutputID = (Label)row.FindControl("lblMaterialOutputID");
                Label lblMaterialOutputCode = (Label)row.FindControl("lblMaterialOutputCode");
                Label lbllMaterialOutputName = (Label)row.FindControl("lbllMaterialOutputName");
                Label lbllMaterialOutputType = (Label)row.FindControl("lbllMaterialOutputType");
                DropDownList ddlUnit = (DropDownList)row.FindControl("ddlUnit");
                TextBox txtQty = (TextBox)row.FindControl("txtQty");
                TextBox txtRefQtyOutput = (TextBox)row.FindControl("txtRefQtyOutput");
                if (txtQty.Text.Trim() != null && txtQty.Text.Trim() != string.Empty)
                {
                    strMaterialQTY = txtQty.Text.Replace(",", "").Trim();
                    if (strMaterialQTY != null && strMaterialQTY != string.Empty)
                    {
                        dr = dtMaterialOutputDestination.NewRow();
                        dr[2] = hdfProcessID.Value;
                        dr[3] = lblMaterialOutputID.Text;
                        dr[4] = lblMaterialOutputCode.Text;
                        dr[5] = lbllMaterialOutputName.Text;
                        dr[6] = lbllMaterialOutputType.Text;
                        dr[7] = ddlUnit.SelectedValue;
                        dr[8] = ddlUnit.SelectedItem.Text;
                        dr[9] = txtQty.Text;
                        dr[10] = "1";
                        dr[11] = txtRefQtyOutput.Text;
                        dtMaterialOutputDestination.Rows.Add(dr);
                    }
                    else
                    {
                        dr = dtMaterialOutputSource.NewRow();
                        dr[0] = hdfProcessID.Value;
                        dr[1] = lblMaterialOutputID.Text;
                        dr[2] = lblMaterialOutputCode.Text;
                        dr[3] = lbllMaterialOutputName.Text;
                        dr[4] = lbllMaterialOutputType.Text;
                        dr[5] = ddlUnit.SelectedValue;
                        dr[6] = txtQty.Text;
                        dr[7] = "1";
                        dr[8] = txtRefQtyOutput.Text;
                        dtMaterialOutputSource.Rows.Add(dr);
                    }
                }
            }
            HttpContext.Current.Session["MaterialOutputSource"] = dtMaterialOutputSource;
            HttpContext.Current.Session["MaterialOutputDestination"] = dtMaterialOutputDestination;
            LoadSourceMaterialOutput(hdfProcessID.Value);
            LoadDestinationMaterialOutput(hdfProcessID.Value);
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }

    public DataTable FGProduct()
    {
        dt = CreateDataTableForMaterialOutputDestination();
        DataRow dr;
        dr = dt.NewRow();
        /*dr[2] = "C92CD125-6EB2-42A5-8DF1-9EEC82214F20";*/
        dr[2] = "1";
        dr[3] = ddlProduct.SelectedValue.ToString();
        //dr[4] = lblMaterialOutputCode.Text;
        //dr[5] = lbllMaterialOutputName.Text;
        dr[6] = "FG";
        dr[7] = "B9F29D12-DE94-40F1-A668-C79BF1BF4425";
        dr[9] = "1";
        dr[10] = "1";
        dr[11] = "1";
        dt.Rows.Add(dr);
        return dt;
    }

    protected void imgbTempSave_Click(object sender, EventArgs e)
    {
        try
        {
            getWorkstationSelectedGridInfo();
            gvWorkstaionDestination.DataSource = oClsWorkstationMaster.FetchWorkstationMaster();
            gvWorkstaionDestination.DataBind();
            Session["TabNo"] = 2;
            this.light.Style["display"] = "block";
            this.fade.Style["display"] = "block";
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    protected void btngridedit_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds;
            if (hdnfProcessFrameworkID.Value != "")
            {
                string id = hdnfProcessFrameworkID.Value.ToString();
                ds = oProcessFramework.FetchProcessFrameworkByID(id);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtProcessFrameworkCode.Text = ds.Tables[0].Rows[0]["ProcessFrameworkCode"].ToString().Trim();
                    txtProcessFrameworkName.Text = ds.Tables[0].Rows[0]["ProcessFrameworkName"].ToString().Trim();
                    txtFrameworkDescription.Text = ds.Tables[0].Rows[0]["ProcessFrameworkDescription"].ToString().Trim();
                    ddlProduct.SelectedValue = ds.Tables[0].Rows[0]["ItemID"].ToString().Trim();
                    if (ds.Tables[0].Rows[0]["ACTIVE"].ToString().Trim() == "Active")
                    {
                        chkActive.Checked = true;
                    }
                    else
                    {
                        chkActive.Checked = false;
                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        gvDest.DataSource = ds.Tables[1];
                        gvDest.DataBind();
                        HttpContext.Current.Session["ProcessSequence"] = ds.Tables[1];//Store ProcessSequence Value after click Edit.
                        ViewState["ProcessID"] = ds.Tables[1].Rows[0]["ProcessID"].ToString().Trim();
                    }
                    pnlAdd.Style["display"] = "";
                    pnlDisplay.Style["display"] = "none";
                    btnaddhide.Style["display"] = "none";
                    LoadProcessFramework();
                }
                else
                {
                    pnlAdd.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "";
                    btnaddhide.Style["display"] = "";
                    LoadProcessFramework();
                }
            }
            else
            {
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                btnaddhide.Style["display"] = "";
                LoadProcessFramework();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #endregion

    #region Public Method

    public void LoadProduct()
    {
        try
        {
            dt = oProcess.BindActiveMaterial(HttpContext.Current.Session["DEPOTID"].ToString());
            dt = GetDataExistInFGSFG(dt, str, "TYPE");
            if (dt.Rows.Count > 0)
            {
                ddlProduct.Items.Clear();
                ddlProduct.DataSource = dt;
                ddlProduct.DataTextField = "PRODUCTALIAS";
                ddlProduct.DataValueField = "ID";
                ddlProduct.DataBind();
                ddlProduct.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    public void LoadProcessMaster()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = oProcess.FetchProcessMaster();
            gvSource.UseAccessibleHeader = true;
            if (dt.Rows.Count > 0)
            {
                gvSource.DataSource = dt;
                gvSource.DataBind();
            }
            HttpContext.Current.Session["ProcessSource"] = oProcess.FetchProcessMaster();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    private void LoadWorkstationMaster()
    {
        if (HttpContext.Current.Session["WorkStationSource"] == null)
        {
            gvWorkstaionSource.DataSource = oClsWorkstationMaster.FetchWorkstationMaster();
            gvWorkstaionSource.DataBind();
        }
    }

    public void LoadDestinationMaster()
    {
        try
        {
            GridView gvDest = new GridView();
            DataTable dt1 = new DataTable();
            //if ((Convert.ToString(Session["ProcessSequence"])) != null)
            {
                dt1 = (DataTable)HttpContext.Current.Session["ProcessSequence"];
                gvDest.DataSource = dt1;
                gvDest.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    public void LoadProcessFramework()
    {
        try
        {
            gvProcessFramework.DataSource = oProcessFramework.FetchProcessFramework();
            gvProcessFramework.DataBind();
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    public void ResetAll()
    {
        txtProcessFrameworkCode.Text = "";
        txtProcessFrameworkName.Text = "";
        txtFrameworkDescription.Text = "";
        chkActive.Checked = true;
        hdnfProcessFrameworkID.Value = "";
        pnlAdd.Style["display"] = "none";
        btnaddhide.Style["display"] = "none";
    }
    #endregion

    #region Protected Method

    protected void DeleteRecord(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            if (e.Record["ProcessFrameworkID"].ToString() != "")
            {
                int ID = 0;
                ID = oProcessFramework.DeleteProcessFrameworkRecord(e.Record["ProcessFrameworkID"].ToString());
                if (ID > 0)
                {
                    MessageBox1.ShowSuccess("Record Deleted Successfully!");
                    LoadProcessFramework();
                }
                else if (ID == -1)
                {
                    e.Record["Error"] = "You Cannot delete because this ProcessFrameworkID is allready Exists in Production_Order.";
                }
                else
                {
                    e.Record["Error"] = "Error On Deleting. ";
                }
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    #endregion

    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["ProcessSequence"] != null)
        {
            dt = (DataTable)HttpContext.Current.Session["ProcessSequence"];
            gvDest.DataSource = dt;
            gvDest.DataBind();
            this.light.Style["display"] = "block";
            this.fade.Style["display"] = "block";
            //mpe.Show();
        }
    }

    public void OnSelectedIndexChanged123()
    {
        //string str = gvDest.SelectedRow.Cells[0].Text;
        //lblProcessID.Text = gvDest.SelectedRow.Cells[0].Text;
        if (HttpContext.Current.Session["ProcessSequence"] != null)
        {
            dt = (DataTable)HttpContext.Current.Session["ProcessSequence"];
            gvDest.DataSource = dt;
            gvDest.DataBind();
            this.light.Style["display"] = "block";
            this.fade.Style["display"] = "block";
            //mpe.Show();
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public static void SaveProduct(Product product)
    {
        //Button ContentPlaceHolder1_btnRefresh = new Button();
        //ContentPlaceHolder1_btnRefresh.Enabled = true;
        //VIEW_frmProcessFramework ostatic = new VIEW_frmProcessFramework();
        //Button btnRefresh = new Button();
        //ostatic.btnRefresh.Click += new EventHandler(ostatic.btnRefresh_Click);

        //Page page = (Page)HttpContext.Current.Handler;
        //Button btnRefresh = (Button)page.FindControl("btnRefresh");
        //btnRefresh.Enabled = true;

        DataTable dt = new DataTable("MyTable");
        DataTable dtProcessSource = new DataTable();
        dtProcessSource = (DataTable)HttpContext.Current.Session["ProcessSource"];
        GridView gvDest = new GridView();
        GridView gvSource = new GridView();
        if (HttpContext.Current.Session["ProcessSequence"] == null)
        {
            dt.Columns.Add("ProcessID", typeof(string));
            dt.Columns.Add("ProcessCode", typeof(string));
            dt.Columns.Add("ProcessName", typeof(string));
            dt.Columns.Add("ProcessTotalDuration", typeof(string));
        }
        else
        {
            dt = (DataTable)HttpContext.Current.Session["ProcessSequence"];
        }

        DataRow dt_row;
        dt_row = dt.NewRow();
        dt_row["ProcessID"] = product.ProcessID;
        dt_row["ProcessCode"] = product.ProcessCode;
        dt_row["ProcessName"] = product.ProcessName;
        dt_row["ProcessTotalDuration"] = product.ProcessTotalDuration;
        dt.Rows.Add(dt_row);

        HttpContext.Current.Session["ProcessSequence"] = dt;
        dt = (DataTable)HttpContext.Current.Session["ProcessSequence"];
        foreach (GridViewRow row in gvDest.Rows)
        {
            DataRow dr;
            //ImageButton imgbPopupOpen = (ImageButton)row.FindControl("imgbPopupOpen");
            TextBox txtTotalDuration = (TextBox)row.FindControl("txtTotalDuration");
            //HiddenField hdfProcessID = (HiddenField)row.FindControl("hdfProcessID");
            string cell_1_Value = gvDest.Rows[row.RowIndex].Cells[0].Text;
            string cell_2_Value = gvDest.Rows[row.RowIndex].Cells[1].Text;
            string cell_3_Value = gvDest.Rows[row.RowIndex].Cells[2].Text;
            string cell_4_Value = txtTotalDuration.Text;
            if (cell_4_Value != null && cell_4_Value != string.Empty)
            {
                dr = dt.NewRow();
                dr[0] = cell_1_Value;
                dr[1] = cell_2_Value;
                dr[2] = cell_3_Value;
                dr[3] = cell_4_Value;
                dt.Rows.Add(dr);
            }
        }
        gvDest.DataSource = dt;
        gvDest.DataBind();
        dtProcessSource = GetFilteredData1(dtProcessSource, product.ProcessID, "ProcessID");
        HttpContext.Current.Session["ProcessSource"] = dtProcessSource;
    }

    [WebMethod]
    [ScriptMethod]
    public static string SaveProcess(Product product)
    {
        VIEW_frmProcessFramework ostatic = new VIEW_frmProcessFramework();
        DataTable dt = new DataTable("MyTable");
        DataSet ds = new DataSet();
        GridView gvDest = new GridView();
        if (HttpContext.Current.Session["ProcessSequence"] == null)
        {
            dt.Columns.Add("ProcessID", typeof(string));
            dt.Columns.Add("ProcessCode", typeof(string));
            dt.Columns.Add("ProcessName", typeof(string));
        }
        else
        {
            dt = (DataTable)HttpContext.Current.Session["ProcessSequence"];
        }

        DataRow dt_row;
        dt_row = dt.NewRow();
        dt_row["ProcessID"] = product.ProcessID;
        dt_row["ProcessCode"] = product.ProcessCode;
        dt_row["ProcessName"] = product.ProcessName;
        dt.Rows.Add(dt_row);
        HttpContext.Current.Session["ProcessSequence"] = dt;
        ds.Tables.Add(dt);
        return ds.GetXml();
    }

    protected void gvSourceMaterialInput_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dt;
            DropDownList ddlUnit = (DropDownList)e.Row.FindControl("ddlUnit");
            Label lbllMaterialInputType = (Label)e.Row.FindControl("lbllMaterialInputType");
            if (lbllMaterialInputType.Text == "PM")
            {
                dt = oClsProductMaster.BindUOMFORPM();
            }
            else if (lbllMaterialInputType.Text == "RM")
            {
                dt = oClsProductMaster.BindUOMFORRM();
            }
            else
            {
                dt = oClsProductMaster.BindUOMFORSFG();
            }
            //dt = oClsProductMaster.BindUOM();

            if (dt.Rows.Count > 0)
            {
                ddlUnit.Items.Clear();
                ddlUnit.DataSource = dt;
                ddlUnit.DataTextField = "UOMDESCRIPTION";
                ddlUnit.DataValueField = "UOMID";
                ddlUnit.DataBind();
                ddlUnit.Enabled = true;
                if (lbllMaterialInputType.Text == "PM")
                {
                    ddlUnit.SelectedItem.Text = "gm";
                }
                else if (lbllMaterialInputType.Text == "RM")
                {
                    ddlUnit.SelectedItem.Text = "KILOGRAM";
                }
                else
                {
                    ddlUnit.SelectedItem.Text = "KILOGRAM";
                }
            }
            else
            {
                ddlUnit.Items.Clear();
                ddlUnit.Items.Insert(0, new ListItem("-Select Unit-", "0"));
                ddlUnit.AppendDataBoundItems = true;
            }
        }
    }

    protected void gvSourceMaterialOutput_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlUnit = (DropDownList)e.Row.FindControl("ddlUnit");
            // ddlUnit.DataSource = oClsProductMaster.BindUOM();
            DataTable dt = oClsProductMaster.BindUOM();

            if (dt.Rows.Count > 0)
            {
                ddlUnit.Items.Clear();
                //ddlUnit.Items.Insert(0, new ListItem("-Select Unit-", "0"));
                ddlUnit.DataSource = dt;
                ddlUnit.DataTextField = "UOMDESCRIPTION";

                ddlUnit.DataValueField = "UOMID";
                ddlUnit.DataBind();
                ddlUnit.Enabled = true;
            }
            else
            {
                ddlUnit.Items.Clear();
                ddlUnit.Items.Insert(0, new ListItem("-Select Unit-", "0"));
                ddlUnit.AppendDataBoundItems = true;
            }
        }
    }

    #region Resource Section

    #region Events
    protected void ImgbAddResoure_Click(object sender, EventArgs e)
    {
        try
        {
            getResourceSelectedGridInfo();
            hdfTabID.Value = "3";
            this.light.Style["display"] = "block";
            this.fade.Style["display"] = "block";
            //mpe.Show();
        }

        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void imgbAddMaterialInput_Click(object sender, EventArgs e)
    {
        try
        {
            getMaterialInputSelectedGridInfo();
            hdfTabID.Value = "1";
            hdfSubTabID.Value = "1";
            // getMaterialBOMSelectedGridInfo();
            this.light.Style["display"] = "block";
            this.fade.Style["display"] = "block";
            //mpe.Show();
        }

        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void imgbAddMaterialOutput_Click(object sender, EventArgs e)
    {
        try
        {
            getMaterialOutputSelectedGridInfo();
            //hdfTabID.Value = "1";
            hdfSubTabID.Value = "2";
            //getMaterialBOMSelectedGridInfo();
            //mpe.Show();
            this.light.Style["display"] = "block";
            this.fade.Style["display"] = "block";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Private Method

    private void getResourceSelectedGridInfo()
    {
        try
        {
            //HttpContext.Current.Session["ResourceDestination"] = null;(Rajeev)
            DataTable dtResourceSource = new DataTable();
            DataTable dtResourceDestination = new DataTable();
            DataRow dr;
            string strNoOfResource = string.Empty;
            if (HttpContext.Current.Session["ResourceSource"] == null)
            {
                dtResourceSource = CreateDataTableForResourceSource();
            }
            else
            {
                dtResourceSource = (DataTable)HttpContext.Current.Session["ResourceSource"];
            }
            if (HttpContext.Current.Session["ResourceDestination"] == null)
            {
                dtResourceDestination = CreateDataTableForResourceDestination();
            }
            else
            {
                dtResourceDestination = (DataTable)HttpContext.Current.Session["ResourceDestination"];
            }
            dt = GetDataExist(dtResourceSource, hdfProcessID.Value, "ProcessID");
            if (dt.Rows.Count > 0)
            {
                for (int i = dtResourceSource.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow drResource = dtResourceSource.Rows[i];
                    if (Convert.ToString(drResource[0]) == hdfProcessID.Value)
                    {
                        dtResourceSource.Rows.Remove(drResource);
                    }
                }
            }
            foreach (GridViewRow row in gvResourceSource.Rows)
            {
                Label lblUTID = (Label)row.FindControl("lblUTID");
                Label lblUTNAME = (Label)row.FindControl("lblUTNAME");
                TextBox txtNoOfResource = (TextBox)row.FindControl("txtNoOfResource");
                if (txtNoOfResource.Text.Trim() != null && txtNoOfResource.Text.Trim() != string.Empty)
                {
                    strNoOfResource = txtNoOfResource.Text.Replace(",", "").Trim();
                    if (strNoOfResource != null && strNoOfResource != string.Empty)
                    {
                        dr = dtResourceDestination.NewRow();
                        dr[2] = hdfProcessID.Value;
                        dr[3] = lblUTID.Text;
                        dr[4] = lblUTNAME.Text;
                        dr[5] = strNoOfResource;
                        dtResourceDestination.Rows.Add(dr);
                    }
                    else
                    {
                        dr = dtResourceSource.NewRow();
                        dr[0] = hdfProcessID.Value;
                        dr[1] = lblUTID.Text;
                        dr[2] = lblUTNAME.Text;
                        dr[3] = strNoOfResource;
                        dtResourceSource.Rows.Add(dr);
                    }
                }
                HttpContext.Current.Session["ResourceSource"] = dtResourceSource;
                HttpContext.Current.Session["ResourceDestination"] = dtResourceDestination;
                LoadDestinationResource(hdfProcessID.Value);
                LoadSourceResource(hdfProcessID.Value);
            }
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }
    #endregion

    #region Gridview Events

    #endregion

    #endregion

    #region Gridview Event
    protected void gvDest_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dtProcessSeq = new DataTable();
        DataRow dr;
        if (HttpContext.Current.Session["ProcessSequence"] != null)
        {
            // dt = CreateDataTableForProcessDestination();
            dt = (DataTable)HttpContext.Current.Session["ProcessSequence"];
            gvDest.DataSource = dt;
            gvDest.DataBind();
        }
        else
        {
            dt = (DataTable)HttpContext.Current.Session["ProcessSequence"];
            gvDest.DataSource = dt;
            gvDest.DataBind();
        }

        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        string ProcessID = commandArgs[0];
        string ProcessName = commandArgs[1];
        if (e.CommandName == "ProcessWiseWindowOpen")
        {
            lblProcessWorkstation.Text = "Process Name: " + ProcessName;
            lblProcessResource.Text = "Process Name: " + ProcessName;
            lblProcessQC.Text = "Process Name: " + ProcessName;
            lblProcessMaterialBOM.Text = "Process Name: " + ProcessName;
            hdfProcessID.Value = ProcessID;
            dtProcessSeq = CreateDataTableForProcessDestination();
            if (hdnfProcessFrameworkID.Value == "")
            {
                foreach (GridViewRow row in gvDest.Rows)
                {
                    TextBox txtTotalDuration = (TextBox)gvDest.Rows[row.RowIndex].FindControl("txtTotalDuration");
                    string cell_1_Value = gvDest.Rows[row.RowIndex].Cells[0].Text;
                    string cell_2_Value = gvDest.Rows[row.RowIndex].Cells[1].Text;
                    string cell_3_Value = gvDest.Rows[row.RowIndex].Cells[2].Text;
                    string cell_4_Value = txtTotalDuration.Text;
                    //if (cell_4_Value != null && cell_4_Value != string.Empty)

                    dr = dtProcessSeq.NewRow();
                    dr[0] = "";
                    dr[1] = "";
                    dr[2] = cell_1_Value;
                    dr[3] = cell_2_Value;
                    dr[4] = cell_3_Value;
                    dr[5] = cell_4_Value;
                    dtProcessSeq.Rows.Add(dr);
                    HttpContext.Current.Session["ProcessSequence"] = dtProcessSeq;
                    LoadWorkstation(ProcessID);
                    LoadResource(ProcessID);
                    LoadQC(ProcessID);
                    LoadSourceMaterialInput(ProcessID);
                    LoadDestinationMaterialInput(ProcessID);
                    LoadSourceMaterialOutput(ProcessID);
                    LoadDestinationMaterialOutput(ProcessID);
                    this.light.Style["display"] = "block";
                    this.fade.Style["display"] = "block";
                    //mpe.Show();
                }
            }
            else
            {
                DataSet ds1;
                ds1 = oProcessFramework.FetchProcessFrameworkByID(hdnfProcessFrameworkID.Value);
                if (ds1.Tables[1].Rows.Count > 0)
                {
                    gvDest.DataSource = ds1.Tables[1];
                    gvDest.DataBind();
                    Session["ProcessSequence"] = ds1.Tables[1];//Add By Rajeev
                }
                LoadWorkstation(ProcessID);
                LoadResource(ProcessID);
                LoadQC(ProcessID);
                LoadSourceMaterialInput(ProcessID);
                LoadDestinationMaterialInput_New(ProcessID);

                //LoadDestinationMaterialInput(ProcessID);
                LoadSourceMaterialOutput(ProcessID);
                //LoadDestinationMaterialOutput(ProcessID);
                LoadDestinationMaterialOutput_New(ProcessID);
                this.light.Style["display"] = "block";
                this.fade.Style["display"] = "block";
            }
        }
        DataTable dtProcessSourceDestination = new DataTable();
        if (e.CommandName == "ProcessSourceDelete")
        {
            string[] CommandSourceDeleteArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string ProcessSourceID = CommandSourceDeleteArgs[0];
            string ProcessSourceName = CommandSourceDeleteArgs[1];
            dtProcessSourceDestination = GetFilteredData(((DataTable)(Session["ProcessSequence"])), ProcessSourceID, "ProcessID");
            Session["ProcessSequence"] = dtProcessSourceDestination;
            gvDest.DataSource = dtProcessSourceDestination;
            gvDest.DataBind();
        }
    }

    private void LoadDestinationMaterialInput_New(string ProcessID)
    {
        try
        {
            DataTable dt = new DataTable();
            DataSet ds;
            double InputQty = 0;
            if (hdnfProcessFrameworkID.Value != "")
            {
                ds = oProcessFramework.FetchProcessFrameworkEdit(hdnfProcessFrameworkID.Value, ProcessID);
                if (ds.Tables[3].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[3].Rows)
                    {
                        InputQty += Convert.ToDouble(dr["Qty"]);
                    }
                    gvDestMaterialInput.Columns[4].FooterText = "Total";
                    gvDestMaterialInput.Columns[6].FooterText = InputQty.ToString("N2");
                    gvDestMaterialInput.DataSource = ds.Tables[3];
                    gvDestMaterialInput.DataBind();
                    HttpContext.Current.Session["MaterialInputDestination"] = ds.Tables[3];//Comments OPen By Rajeev_11072017
                }
            }
            else
            {
                //LoadDestinationInputMaterial(ProcessID);
                LoadDestinationMaterialInput(ProcessID);
            }
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }

    private void LoadDestinationMaterialOutput_New(string ProcessID)
    {
        try
        {
            DataSet ds;
            DataTable dt = new DataTable();
            double OutPutQty = 0;
            if (hdnfProcessFrameworkID.Value != "")
            {
                ds = oProcessFramework.FetchProcessFrameworkEdit(hdnfProcessFrameworkID.Value, ProcessID);
                if (ds.Tables[4].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[4].Rows)
                    {
                        OutPutQty += Convert.ToDouble(dr["Qty"]);
                    }
                    gvDestMaterialOutput.Columns[4].FooterText = "Total";
                    gvDestMaterialOutput.Columns[6].FooterText = OutPutQty.ToString("N2");
                    gvDestMaterialOutput.DataSource = ds.Tables[4];
                    gvDestMaterialOutput.DataBind();
                    HttpContext.Current.Session["MaterialOutputDestination"] = ds.Tables[4];//(Comments Open By Rajeev)
                }
            }
            else
            {
                //LoadDestinationOutputMaterial(dt);
                LoadDestinationMaterialOutput(ProcessID);
            }
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }

    public void LoaMaterialType()
    {
        DataTable dt = oClsItemType.BindPrimaryItemType();
        string strMaterialType = "'PM','RM','SFG','TRADING','PF'";
        dt = GetDataExistInDrop(dt, strMaterialType, "ITEMCODE");
        if (dt.Rows.Count > 0)
        {
            ddlMaterialType.Items.Clear();
            ddlMaterialType.DataSource = dt;
            ddlMaterialType.DataTextField = "ITEMDESC";

            ddlMaterialType.DataValueField = "ITEMCODE";
            ddlMaterialType.DataBind();
            ddlMaterialType.Enabled = true;
            ddlMaterialType.Enabled = true;
        }
        else
        {
            ddlMaterialType.Items.Clear();
            ddlMaterialType.AppendDataBoundItems = true;
        }
    }
    private void LoadMaterialBOM(string ProcessID)
    {
        LoadSourceMaterialBOM(ProcessID);
        LoadDestinationMaterialBOM(ProcessID);
    }

    private void LoadQC(string ProcessID)
    {
        DataSet ds;
        LoadSourceQC(ProcessID);
        if (hdnfProcessFrameworkID.Value != "")
        {
            ds = oProcessFramework.FetchProcessFrameworkEdit(hdnfProcessFrameworkID.Value, ProcessID);
            if (ds.Tables[2].Rows.Count > 0)
            {
                gvQCDestination.DataSource = ds.Tables[2];
                gvQCDestination.DataBind();
                Session["QCDestination"] = ds.Tables[2];//Use For Delete Purpose
            }
        }
        else
        {
            LoadDestinationQC(ProcessID);
        }
    }

    private void LoadResource(string ProcessID)
    {
        DataSet ds;
        LoadSourceResource(ProcessID);
        if (hdnfProcessFrameworkID.Value != "")
        {
            ds = oProcessFramework.FetchProcessFrameworkEdit(hdnfProcessFrameworkID.Value, ProcessID);
            if (ds.Tables[1].Rows.Count > 0)
            {
                gvResourceDestination.DataSource = ds.Tables[1];
                gvResourceDestination.DataBind();
                HttpContext.Current.Session["ResourceDestination"] = ds.Tables[1];//For ResourceDestination Delete Perpous
            }
        }
        else
        {
            LoadDestinationResource(ProcessID);
        }
    }
    #endregion

    #region QC Section

    #region Private
    /// <summary>
    /// Bind QC data with source grid
    /// </summary>
    private void LoadDestinationQC(string ProcessID)
    {
        try
        {
            DataTable dt = new DataTable();
            if (HttpContext.Current.Session["QCDestination"] == null)
            {
                //LoadQC();
                //gvQCDestination.DataSource = dt;
                //gvQCDestination.DataBind();
                LoadDestinationQC(dt);
            }
            else
            {
                //DataTable dt = new DataTable();
                dt = GetDataExist(((DataTable)(Session["QCDestination"])), ProcessID, "ProcessID");
                if (dt.Rows.Count == 0)
                {
                    LoadDestinationQC(dt);
                    //LoadQC();
                }
                else
                {
                    //dt = GetDataExist(((DataTable)(Session["QCSource"])), ProcessID, "ProcessID");
                    //LoadQC(dt);
                    LoadDestinationQC(dt);
                }
            }
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }

    /// <summary>
    /// Bind QC data with source grid
    /// </summary>
    private void LoadSourceQC(string ProcessID)
    {
        try
        {
            if (HttpContext.Current.Session["QCSource"] == null)
            {
                DataTable dt = new DataTable();
                LoadQC();
                LoadDestinationQC(dt);
            }
            else
            {
                DataTable dt = new DataTable();
                dt = GetDataExist(((DataTable)(Session["QCSource"])), ProcessID, "ProcessID");
                if (dt.Rows.Count == 0)
                {
                    LoadQC();
                }
                else
                {
                    //dt = GetDataExist(((DataTable)(Session["QCSource"])), ProcessID, "ProcessID");
                    LoadQC(dt);
                }
            }
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }
    /// <summary>
    /// Bind Resource data with source grid
    /// </summary>
    private void LoadSourceResource(string ProcessID)
    {
        try
        {
            if (HttpContext.Current.Session["ResourceSource"] == null)
            {
                DataTable dt = new DataTable();
                LoadResource();
                LoadDestinationResource(dt);
            }
            else
            {
                DataTable dt = new DataTable();
                dt = GetDataExist(((DataTable)(Session["ResourceSource"])), ProcessID, "ProcessID");
                if (dt.Rows.Count == 0)
                {
                    LoadResource();
                }
                else
                {
                    LoadResource(dt);
                }
            }
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }
    /// <summary>
    /// Bind material BOM data with source grid
    /// </summary>
    private void LoadSourceMaterialBOM(string ProcessID)
    {
        try
        {
            if (HttpContext.Current.Session["MaterialBOMSource"] == null)
            {
                DataTable dt = new DataTable();
                LoadMaterialBOM();
                //LoadDestinationMaterialBOM(dt);
            }
            else
            {
                DataTable dt = new DataTable();
                dt = GetDataExist(((DataTable)(Session["MaterialBOMSource"])), ProcessID, "ProcessID");
                if (dt.Rows.Count == 0)
                {
                    LoadMaterialBOM();
                }
                else
                {
                    LoadMaterialBOM(dt);
                }
            }
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }
    /// <summary>
    /// Bind Resource data with destination grid
    /// </summary>
    private void LoadDestinationResource(string ProcessID)
    {
        try
        {
            DataTable dt = new DataTable();
            if (HttpContext.Current.Session["ResourceDestination"] == null)
            {
                //LoadQC();
                //gvQCDestination.DataSource = dt;
                //gvQCDestination.DataBind();
                LoadDestinationResource(dt);
            }
            else
            {
                //DataTable dt = new DataTable();
                dt = GetDataExist(((DataTable)(Session["ResourceDestination"])), ProcessID, "ProcessID");
                if (dt.Rows.Count == 0)
                {
                    LoadDestinationResource(dt);
                    //LoadQC();
                }
                else
                {
                    //dt = GetDataExist(((DataTable)(Session["QCSource"])), ProcessID, "ProcessID");
                    //LoadQC(dt);
                    LoadDestinationResource(dt);
                }
            }
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }

    /// <summary>
    /// Bind workstation data with destination grid
    /// </summary>
    private void LoadDestinationWorkstation(string ProcessID)
    {
        try
        {
            DataTable dt = new DataTable();
            if (HttpContext.Current.Session["WorkstationDestination"] == null)
            {
                LoadDestinationWorkstation(dt);
            }
            else
            {
                dt = GetDataExist(((DataTable)(Session["WorkstationDestination"])), ProcessID, "ProcessID");
                if (dt.Rows.Count == 0)
                {
                    LoadDestinationWorkstation(dt);
                }
                else
                {
                    LoadDestinationWorkstation(dt);
                }
            }
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }

    /// <summary>
    /// Bind material BOM data with destination grid
    /// </summary>
    private void LoadDestinationMaterialBOM(string ProcessID)
    {
        try
        {
            DataTable dt = new DataTable();
            if (HttpContext.Current.Session["MaterialBOMDestination"] == null)
            {
                // LoadDestinationMaterialBOM(dt);
            }
            else
            {
                dt = GetDataExist(((DataTable)(Session["MaterialBOMDestination"])), ProcessID, "ProcessID");
                if (dt.Rows.Count == 0)
                {
                    //   LoadDestinationMaterialBOM(dt);
                }
                else
                {
                    // LoadDestinationMaterialBOM(dt);
                }
            }
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }

    private void LoadDestinationQC(DataTable dt)
    {
        gvQCDestination.DataSource = dt;
        //gvQCDestination.DataSource = HttpContext.Current.Session["QCDestination"];
        gvQCDestination.DataBind();
    }

    private void LoadDestinationResource(DataTable dt)
    {
        gvResourceDestination.DataSource = dt;
        gvResourceDestination.DataBind();
    }

    private void LoadQC(DataTable dt)
    {
        gvQCSource.DataSource = dt;
        gvQCSource.DataBind();
    }

    private void LoadDestinationMaterialInput(DataTable dt)
    {
        gvDestMaterialInput.DataSource = dt;
        gvDestMaterialInput.DataBind();
    }

    private void LoadMaterialBOM(DataTable dt)
    {
        //gvMaterialBOMSource.DataSource = dt;
        //gvMaterialBOMSource.DataBind();
    }

    private void LoadResource(DataTable dt)
    {
        gvResourceSource.DataSource = dt;
        gvResourceSource.DataBind();
    }
    private void LoadQC()
    {
        DataTable dtQCSource = new DataTable();
        dtQCSource = oClsQCMaster.FetchQCMaster();
        gvQCSource.DataSource = dtQCSource = oClsQCMaster.FetchQCMaster();
        gvQCSource.DataBind();
        //Session["QCSource"] = dtQCSource;//Rajeev
    }

    #region Workstation

    #region Private Method

    private void LoadWorkstation()
    {
        try
        {
            gvWorkstaionSource.DataSource = oClsWorkstationMaster.FetchWorkstationMaster();
            gvWorkstaionSource.DataBind();
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }

    private void LoadDestinationWorkstation(DataTable dt)
    {
        gvWorkstaionDestination.DataSource = dt;
        gvWorkstaionDestination.DataBind();
    }

    private void LoadDestinationInputMaterial(DataTable dt)
    {
        gvDestMaterialInput.DataSource = dt;
        /*decimal QTY = dt.AsEnumerable().Sum(row => row.Field<decimal>("Qty"));
        gvDestMaterialInput.Columns[4].FooterText = "Total:-";
        gvDestMaterialInput.Columns[6].FooterText = QTY.ToString("N2");*/
        gvDestMaterialInput.DataBind();
        //gvDestMaterialInput.FooterRow.Cells[4].ForeColor = Color.Blue;
        //gvDestMaterialInput.FooterRow.Cells[6].ForeColor = Color.Blue;
    }

    private void LoadDestinationOutputMaterial(DataTable dt)
    {
        gvDestMaterialOutput.DataSource = dt;
        gvDestMaterialOutput.DataBind();
        /*decimal QTY = dt.AsEnumerable().Sum(row => row.Field<decimal>("Qty"));
        gvDestMaterialOutput.Columns[4].FooterText = "Total:-";
        gvDestMaterialOutput.Columns[6].FooterText = QTY.ToString("N2");
        gvDestMaterialOutput.FooterRow.Cells[4].ForeColor = Color.Blue;
        gvDestMaterialOutput.FooterRow.Cells[6].ForeColor = Color.Blue;*/
    }

    private void LoadWorkstation(DataTable dt)
    {
        gvWorkstaionSource.DataSource = dt;
        gvWorkstaionSource.DataBind();
    }

    private void LoadSourceInputMaterial(DataTable dt)
    {
        gvSourceMaterialInput.DataSource = dt;
        gvSourceMaterialInput.DataBind();
    }

    private void LoadSourceOutputMaterial(DataTable dt)
    {
        gvSourceMaterialOutput.DataSource = dt;
        gvSourceMaterialOutput.DataBind();
    }

    private void LoadWorkstation(string ProcessID)
    {
        DataSet ds;
        LoadSourceWorkstation(ProcessID);
        if (hdnfProcessFrameworkID.Value != "")
        {
            ds = oProcessFramework.FetchProcessFrameworkEdit(hdnfProcessFrameworkID.Value, ProcessID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvWorkstaionDestination.DataSource = ds.Tables[0];
                gvWorkstaionDestination.DataBind();
                HttpContext.Current.Session["WorkstationDestination"] = ds.Tables[0];//(Comments Open By Rajeev)
            }
        }
        else
        {
            LoadDestinationWorkstation(ProcessID);
        }
    }

    /// <summary>
    /// Bind workstation data with source grid
    /// </summary>
    private void LoadSourceWorkstation(string ProcessID)
    {
        try
        {
            if (HttpContext.Current.Session["WorkstationSource"] == null)
            {
                DataTable dt = new DataTable();
                LoadWorkstation();
                LoadDestinationWorkstation(dt);
            }
            else
            {
                DataTable dt = new DataTable();
                dt = GetDataExist(((DataTable)(Session["WorkstationSource"])), ProcessID, "ProcessID");
                if (dt.Rows.Count == 0)
                {
                    LoadWorkstation();
                }
                else
                {
                    LoadWorkstation(dt);
                }
            }
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }

    private void getWorkstationSelectedGridInfo()
    {
        try
        {
            //HttpContext.Current.Session["WorkstationDestination"] = null;(Rajeev)
            DataTable dtWorkstationSource = new DataTable();
            DataTable dtWorkstationDestination = new DataTable();
            DataRow dr;
            string strDuration = string.Empty;
            if (HttpContext.Current.Session["WorkstationSource"] == null)
            {
                dtWorkstationSource = CreateDataTableForWorkStationSource();
            }
            else
            {
                dtWorkstationSource = (DataTable)HttpContext.Current.Session["WorkstationSource"];
            }
            if (HttpContext.Current.Session["WorkstationDestination"] == null)
            {
                dtWorkstationDestination = CreateDataTableForWorkStationDestination();
            }
            else
            {
                dtWorkstationDestination = (DataTable)HttpContext.Current.Session["WorkstationDestination"];
            }
            dt = GetDataExist(dtWorkstationSource, hdfProcessID.Value, "ProcessID");
            if (dt.Rows.Count > 0)
            {
                for (int i = dtWorkstationSource.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow drResource = dtWorkstationSource.Rows[i];
                    if (Convert.ToString(drResource[0]) == hdfProcessID.Value)
                    {
                        dtWorkstationSource.Rows.Remove(drResource);
                    }
                }
            }
            foreach (GridViewRow row in gvWorkstaionSource.Rows)
            {
                Label lblWorkStationID = (Label)row.FindControl("lblWorkStationID");
                Label lblWorkStationCode = (Label)row.FindControl("lblWorkStationCode");
                Label lblWorkStationName = (Label)row.FindControl("lblWorkStationName");
                TextBox txtDuration = (TextBox)row.FindControl("txtDuration");
                if (txtDuration.Text.Trim() != null && txtDuration.Text.Trim() != string.Empty)
                {
                    strDuration = txtDuration.Text.Replace(",", "").Trim();
                    if (strDuration != null && strDuration != string.Empty)
                    {
                        dr = dtWorkstationDestination.NewRow();
                        dr[2] = hdfProcessID.Value;
                        dr[3] = lblWorkStationID.Text;
                        dr[4] = lblWorkStationCode.Text;
                        dr[5] = lblWorkStationName.Text;
                        dr[6] = strDuration;
                        dtWorkstationDestination.Rows.Add(dr);
                    }
                    else
                    {
                        dr = dtWorkstationSource.NewRow();
                        dr[0] = hdfProcessID.Value;
                        dr[1] = lblWorkStationID.Text;
                        dr[2] = lblWorkStationCode.Text;
                        dr[3] = lblWorkStationName.Text;
                        dr[4] = strDuration;
                        dtWorkstationSource.Rows.Add(dr);
                    }
                }
                else
                {
                    dr = dtWorkstationSource.NewRow();
                    dr[0] = hdfProcessID.Value;
                    dr[1] = lblWorkStationID.Text;
                    dr[2] = lblWorkStationCode.Text;
                    dr[3] = lblWorkStationName.Text;
                    dr[4] = txtDuration.Text;
                    dtWorkstationSource.Rows.Add(dr);
                }
            }
            HttpContext.Current.Session["WorkstationSource"] = dtWorkstationSource;
            //HttpContext.Current.Session["WorkstationDestination"] = dtWorkstationDestination;
            LoadDestinationWorkstation(hdfProcessID.Value);
            LoadSourceWorkstation(hdfProcessID.Value);
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }

    #endregion

    #region Datatable For Workstation
    public DataTable CreateDataTableForWorkStationSource()
    {
        DataTable dtWorkstationSource = new DataTable();
        dtWorkstationSource.Clear();
        dtWorkstationSource.Columns.Add(new DataColumn("ProcessID", typeof(String)));
        dtWorkstationSource.Columns.Add(new DataColumn("WorkStationID", typeof(String)));
        dtWorkstationSource.Columns.Add(new DataColumn("WorkStationCode", typeof(String)));
        dtWorkstationSource.Columns.Add(new DataColumn("WorkStationName", typeof(String)));
        dtWorkstationSource.Columns.Add(new DataColumn("Duration", typeof(string)));
        HttpContext.Current.Session["WorkStationSource"] = dtWorkstationSource;
        return dtWorkstationSource;
    }

    public DataTable CreateDataTableForWorkStationDestination()
    {
        DataTable dtWorkstationDestination = new DataTable();
        dtWorkstationDestination.Clear();
        dtWorkstationDestination.Columns.Add(new DataColumn("ProcessWorkstationID", typeof(String)));
        dtWorkstationDestination.Columns.Add(new DataColumn("ProcessFrameworkID", typeof(String)));
        dtWorkstationDestination.Columns.Add(new DataColumn("ProcessID", typeof(String)));
        dtWorkstationDestination.Columns.Add(new DataColumn("WorkstationID", typeof(String)));
        dtWorkstationDestination.Columns.Add(new DataColumn("WorkStationCode", typeof(String)));
        dtWorkstationDestination.Columns.Add(new DataColumn("WorkStationName", typeof(String)));
        dtWorkstationDestination.Columns.Add(new DataColumn("DurationInHour", typeof(String)));
        HttpContext.Current.Session["WorkstationDestination"] = dtWorkstationDestination;
        return dtWorkstationDestination;
    }

    #endregion

    #region Event
    protected void imgbAddWorkstation_Click(object sender, EventArgs e)
    {
        try
        {
            getWorkstationSelectedGridInfo();
            hdfTabID.Value = "2";
            //mpe.Show();
            this.light.Style["display"] = "block";
            this.fade.Style["display"] = "block";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #endregion

    private void LoadMaterialBOM()
    {
        try
        {
            //DataTable dt = new DataTable();
            //string str = "";
            //dt = GetDataExist(oClsProductMaster.BindActiveMaterial(), hdfProcessID.Value, "ProcessID");       
            //gvMaterialBOMSource.DataSource = oClsProductMaster.BindActiveMaterial();
            //gvMaterialBOMSource.DataBind();
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }

    #region Resource
    private void LoadResource()
    {
        try
        {
            gvResourceSource.DataSource = ClsCommon.BindUsertype();
            gvResourceSource.DataBind();
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }
    #endregion

    /// <summary>
    /// Basis on chkbox selection rebind source & destination grid for QCubmit
    /// 
    /// </summary>
    private void getQCSelectedGridInfo()
    {
        try
        {
            /*DataTable dtcopydest = new DataTable();
           dtcopydest = (DataTable)HttpContext.Current.Session["QCDestination"];*/
            //HttpContext.Current.Session["QCDestination"] = null;
            DataTable dtQCSource = new DataTable();
            DataTable dtQCDestination = new DataTable();
            DataRow dr;
            if (HttpContext.Current.Session["QCSource"] == null)
            {
                dtQCSource = CreateDataTableForQCSource();
                //dtQCSource = oClsQCMaster.FetchQCMaster();
            }
            else
            {
                dtQCSource = (DataTable)HttpContext.Current.Session["QCSource"];
            }
            if (HttpContext.Current.Session["QCDestination"] == null)
            {
                dtQCDestination = CreateDataTableForQCDestination();
            }
            else
            {
                dtQCDestination = (DataTable)HttpContext.Current.Session["QCDestination"];
            }
            dt = GetDataExist(dtQCSource, hdfProcessID.Value, "ProcessID");
            if (dt.Rows.Count > 0)
            {
                for (int i = dtQCSource.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow drQC = dtQCSource.Rows[i];
                    if (Convert.ToString(drQC[0]) == hdfProcessID.Value)
                    {
                        dtQCSource.Rows.Remove(drQC);
                    }
                }
            }
            foreach (GridViewRow row in gvQCSource.Rows)
            {
                Label lblQCID = (Label)row.FindControl("lblQCID");
                Label lblQCName = (Label)row.FindControl("lblQCName");
                CheckBox chkRow = (CheckBox)row.FindControl("chkRow");
                if (chkRow != null & chkRow.Checked)
                {
                    dr = dtQCDestination.NewRow();
                    /* Comments By Rajeev */
                    dr[2] = hdfProcessID.Value;
                    dr[3] = lblQCID.Text;
                    dr[4] = lblQCName.Text;

                    dtQCDestination.Rows.Add(dr);
                    //dtQCSource.Rows.RemoveAt(row.RowIndex);
                }
                else
                {
                    //dt = GetSourceDataExist(dtQCSource, hdfProcessID.Value, lblQCName.Text, "ProcessID", "QCName");
                    //dt = GetDataExist(dtQCSource, hdfProcessID.Value,"ProcessID");

                    //if (dt.Rows.Count > 0)
                    //{

                    //    for (int i = dtQCSource.Rows.Count - 1; i >= 0; i--)
                    //    {
                    //        DataRow dr1 = dtQCSource.Rows[i];
                    //        if (dr1[0] == hdfProcessID.Value)
                    //        {
                    //            dtQCSource.Rows.Remove(dr1);
                    //            ViewState["RecordExist"] = 1;
                    //        }
                    //    }
                    //}
                    //if (dt.Rows.Count == 0)
                    //{
                    //else
                    //{

                    dr = dtQCSource.NewRow();
                    dr[0] = hdfProcessID.Value;
                    dr[1] = lblQCID.Text;
                    dr[2] = lblQCName.Text;
                    dtQCSource.Rows.Add(dr);

                    //}
                    //}
                }
            }
            //dtQCDestination = GetDataExist(dtQCDestination, hdfProcessID.Value, "ProcessID");
            //LoadDestinationQC(hdfProcessID.Value);
            HttpContext.Current.Session["QCSource"] = dtQCSource;
            HttpContext.Current.Session["QCDestination"] = dtQCDestination;
            LoadDestinationQC(hdfProcessID.Value);
            LoadSourceQC(hdfProcessID.Value);
            //Session["QCDestinationData"]
            //gvQCSource.DataSource = dtQCSource;
            //gvQCSource.DataBind();
            //gvQCDestination.DataSource = dtQCDestination;
            //gvQCDestination.DataBind();

            //DataRow dr;
            //DataTable dtQCSource = new DataTable();
            //DataTable dtQCTempSource = new DataTable();
            //DataTable dtTempDestinationResource = new DataTable();

            //dtQCSource.Columns.Add(new System.Data.DataColumn("QCID", typeof(String)));
            //dtQCSource.Columns.Add(new System.Data.DataColumn("QCName", typeof(String)));         

            //foreach (GridViewRow row in gvQCSource.Rows)
            //{
            //    Label lblQCID = (Label)row.FindControl("lblQCID");
            //    Label lblQCName = (Label)row.FindControl("lblQCName");
            //    CheckBox chkRow = (CheckBox)row.FindControl("chkRow");
            //    if (chkRow != null & chkRow.Checked)
            //    {
            //        dr = dtQCSource.NewRow();
            //        dr[0] = lblQCID.Text;
            //        dr[1] = lblQCName.Text;
            //        dtQCSource.Rows.Add(dr);
            //        if ((DataTable)Session["QCDestinationData"] != null)
            //        {
            //            dtTempDestinationResource = (DataTable)Session["QCDestinationData"];
            //            dr = dtTempDestinationResource.NewRow();
            //            dr[0] = lblQCID.Text;
            //            dr[1] = lblQCName.Text;
            //            dtTempDestinationResource.Rows.Add(dr);
            //            Session["QCDestinationData"] = dtTempDestinationResource;
            //        }
            //        dtQCTempSource = GetFilteredData(((DataTable)(Session["QCSourceData"])), lblQCID.Text, "QCID");
            //        Session["QCSourceData"] = dtQCTempSource;
            //    }
            //}

            //if (dtTempDestinationResource.Rows.Count == 0)
            //{
            //    Session["QCDestinationData"] = dtQCSource;

            //}

            //Rebind QC Source Grid
            //gvQCSource.DataSource = (DataTable)Session["QCSourceData"];
            //gvQCSource.DataBind();

            //Rebind QC Destination Grid
            //gvQCDestination.DataSource = (DataTable)Session["QCDestinationData"];
            //gvQCDestination.DataBind();
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }

    #endregion

    #region Events
    protected void imgbAddQC_Click(object sender, EventArgs e)
    {
        try
        {
            getQCSelectedGridInfo();
            hdfTabID.Value = "4";
            this.light.Style["display"] = "block";
            this.fade.Style["display"] = "block";
            //mpe.Show();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #endregion

    #region Gridview Event

    protected void gvDestMaterialInput_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dtSource = new DataTable();
        DataTable dtinputMatDest = new DataTable();
        DataRow dr;

        if (e.CommandName == "imginputMatDelete")
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string INPUTMATID = commandArgs[0];
            string INPUTMATNAME = commandArgs[1];
            dtinputMatDest = GetFilteredData(((DataTable)(Session["MaterialInputDestination"])), INPUTMATID, "ID");
            Session["MaterialInputDestination"] = dtinputMatDest;
            gvDestMaterialInput.DataSource = dtinputMatDest;
            gvDestMaterialInput.DataBind();

            /*decimal QTY = dtinputMatDest.AsEnumerable().Sum(row => row.Field<decimal>("Qty"));
            gvDestMaterialInput.Columns[4].FooterText = "Total:-";
            gvDestMaterialInput.Columns[6].FooterText = QTY.ToString("N2");
            gvDestMaterialInput.FooterRow.Cells[4].ForeColor = Color.Blue;
            gvDestMaterialInput.FooterRow.Cells[6].ForeColor = Color.Blue;*/



            /*dtSource = (DataTable)Session["QCSourceData"];
            dr = dtSource.NewRow();
            dr[0] = INPUTMATID;
            dr[2] = INPUTMATNAME;
            dtSource.Rows.Add(dr);
            Session["QCSourceData"] = dtSource;
            gvResourceSource.DataSource = dtSource;
            gvResourceSource.DataBind();
            this.light.Style["display"] = "block";
            this.fade.Style["display"] = "block";*/
            //mpe.Show();
        }
    }
    protected void gvDestMaterialOutput_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dtSource = new DataTable();
        DataTable dtOutPututMatDest = new DataTable();
        DataRow dr;

        if (e.CommandName == "MaterialOutputDelete")
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string OUTPUTMATID = commandArgs[0];
            string OUTPUTMATNAME = commandArgs[1];
            dtOutPututMatDest = GetFilteredData(((DataTable)(Session["MaterialOutputDestination"])), OUTPUTMATID, "ID");
            Session["MaterialOutputDestination"] = dtOutPututMatDest;
            gvDestMaterialOutput.DataSource = dtOutPututMatDest;
            gvDestMaterialOutput.DataBind();

            this.light.Style["display"] = "block";
            this.fade.Style["display"] = "block";

            /*dtSource = (DataTable)Session["QCSourceData"];
            dr = dtSource.NewRow();
            dr[0] = INPUTMATID;
            dr[2] = INPUTMATNAME;
            dtSource.Rows.Add(dr);
            Session["QCSourceData"] = dtSource;
            gvResourceSource.DataSource = dtSource;
            gvResourceSource.DataBind();
            this.light.Style["display"] = "block";
            this.fade.Style["display"] = "block";*/
            //mpe.Show();
        }
    }

    protected void gvWorkstaionDestination_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dtSource = new DataTable();
        DataTable dtWorkstationDest = new DataTable();
        DataRow dr;
        if (e.CommandName == "WorkstationDelete")
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string WorkstID = commandArgs[0];
            string WorkStationName = commandArgs[1];
            dtWorkstationDest = GetFilteredData(((DataTable)(Session["WorkstationDestination"])), WorkstID, "WorkStationID");
            Session["WorkstationDestination"] = dtWorkstationDest;
            gvWorkstaionDestination.DataSource = dtWorkstationDest;
            gvWorkstaionDestination.DataBind();
            hdfTabID.Value = "2";

            /*dtSource = (DataTable)Session["QCSourceData"];
            dr = dtSource.NewRow();
            dr[0] = QCID;
            //dr[2] = QCName;
            dtSource.Rows.Add(dr);
            Session["QCSourceData"] = dtSource;
            gvQCSource.DataSource = dtSource;
            gvQCSource.DataBind();
            this.light.Style["display"] = "block";
            this.fade.Style["display"] = "block";*/
            //mpe.Show();
        }
    }


    protected void gvQCDestination_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dtSource = new DataTable();
        DataTable dtDestination = new DataTable();
        DataRow dr;
        if (e.CommandName == "QCDelete")
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string QCID = commandArgs[0];
            string QCName = commandArgs[1];
            dtDestination = GetFilteredData(((DataTable)(Session["QCDestination"])), QCID, "QCID");
            Session["QCDestination"] = dtDestination;
            gvQCDestination.DataSource = dtDestination;
            gvQCDestination.DataBind();
            hdfTabID.Value = "4";

            /*dtSource = (DataTable)Session["QCSource"];//(DataTable)Session["QCSourceData"];
            dr = dtSource.NewRow();
            dr[0] = QCID;
            dr[2] = QCName;
            dtSource.Rows.Add(dr);
            //Session["QCSourceData"] = dtSource;
            //Session["QCSource"] = dtSource;
            gvQCSource.DataSource = dtSource;
            gvQCSource.DataBind();
            this.light.Style["display"] = "block";
            this.fade.Style["display"] = "block";*/
            //mpe.Show();           
        }
    }

    protected void gvResourceDestination_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dtSource = new DataTable();
        DataTable dtResourceDest = new DataTable();
        DataRow dr;

        if (e.CommandName == "ResourceDelete")
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string RESOURCEID = commandArgs[0];
            string RESOURCENAME = commandArgs[1];
            dtResourceDest = GetFilteredData(((DataTable)(Session["ResourceDestination"])), RESOURCEID, "ResourceID");
            Session["ResourceDestination"] = dtResourceDest;
            gvResourceDestination.DataSource = dtResourceDest;
            gvResourceDestination.DataBind();
            hdfTabID.Value = "3";

            /*dtSource = (DataTable)Session["QCSourceData"];
            dr = dtSource.NewRow();
            dr[0] = RESOURCEID;
            dr[2] = RESOURCENAME;
            dtSource.Rows.Add(dr);
            Session["QCSourceData"] = dtSource;
            gvResourceSource.DataSource = dtSource;
            gvResourceSource.DataBind();
            this.light.Style["display"] = "block";
            this.fade.Style["display"] = "block";*/
            //mpe.Show();
        }
    }

    protected void gvMaterialBOMDestination_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dtSource = new DataTable();
        DataTable dtDestination = new DataTable();
        DataRow dr;
        if (e.CommandName == "QCDelete")
        {
            string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
            string QCID = commandArgs[0];
            string QCName = commandArgs[1];
            dtDestination = GetFilteredData(((DataTable)(Session["QCDestination"])), QCID, "QCID");
            Session["QCDestination"] = dtDestination;
            gvQCDestination.DataSource = dtDestination;
            gvQCDestination.DataBind();

            dtSource = (DataTable)Session["QCSourceData"];
            dr = dtSource.NewRow();
            dr[0] = QCID;
            dr[2] = QCName;
            dtSource.Rows.Add(dr);
            Session["QCSourceData"] = dtSource;
            gvQCSource.DataSource = dtSource;
            gvQCSource.DataBind();
            this.light.Style["display"] = "block";
            this.fade.Style["display"] = "block";
            //mpe.Show();
        }
    }
    #endregion

    #endregion

    #region Datatable For QC

    public DataTable CreateDataTableForQCSource()
    {
        DataTable dtQCSource = new DataTable();
        dtQCSource.Clear();
        dtQCSource.Columns.Add(new DataColumn("ProcessID", typeof(String)));
        dtQCSource.Columns.Add(new DataColumn("QCID", typeof(String)));
        dtQCSource.Columns.Add(new DataColumn("QCName", typeof(String)));
        dtQCSource.Columns.Add(new DataColumn("QCType", typeof(String)));
        HttpContext.Current.Session["QCSource"] = dtQCSource;
        return dtQCSource;
    }

    public DataTable CreateDataTableForQCDestination()
    {
        DataTable dtQCDestination = new DataTable();
        dtQCDestination.Clear();
        dtQCDestination.Columns.Add(new DataColumn("ProcesQClID", typeof(String)));
        dtQCDestination.Columns.Add(new DataColumn("ProcessFrameworkID", typeof(String)));
        dtQCDestination.Columns.Add(new DataColumn("ProcessID", typeof(String)));
        dtQCDestination.Columns.Add(new DataColumn("QCID", typeof(String)));
        dtQCDestination.Columns.Add(new DataColumn("QCName", typeof(String)));
        HttpContext.Current.Session["QCDestination"] = dtQCDestination;
        return dtQCDestination;
    }

    #endregion

    #region Datatable For Resource

    public DataTable CreateDataTableForResourceSource()
    {
        DataTable dtResourceSource = new DataTable();
        dtResourceSource.Clear();
        dtResourceSource.Columns.Add(new DataColumn("ProcessID", typeof(String)));
        dtResourceSource.Columns.Add(new DataColumn("UTID", typeof(String)));
        dtResourceSource.Columns.Add(new DataColumn("UTNAME", typeof(String)));
        dtResourceSource.Columns.Add(new DataColumn("NoOfResource", typeof(String)));
        HttpContext.Current.Session["ResourceSource"] = dtResourceSource;
        return dtResourceSource;
    }

    public DataTable CreateDataTableForMaterialBOMSource()
    {
        DataTable dtMaterialBOMSource = new DataTable();
        dtMaterialBOMSource.Clear();
        dtMaterialBOMSource.Columns.Add(new DataColumn("ProcessID", typeof(String)));
        dtMaterialBOMSource.Columns.Add(new DataColumn("ID", typeof(String)));
        dtMaterialBOMSource.Columns.Add(new DataColumn("NAME", typeof(String)));
        dtMaterialBOMSource.Columns.Add(new DataColumn("TYPE", typeof(String)));
        dtMaterialBOMSource.Columns.Add(new DataColumn("Qty", typeof(String)));
        dtMaterialBOMSource.Columns.Add(new DataColumn("UNITVALUE", typeof(String)));
        HttpContext.Current.Session["ResourceSource"] = dtMaterialBOMSource;
        return dtMaterialBOMSource;
    }

    public DataTable CreateDataTableForResourceDestination()
    {
        DataTable dtResourceDestination = new DataTable();
        dtResourceDestination.Clear();
        dtResourceDestination.Columns.Add(new DataColumn("ProcessResourceID", typeof(String)));
        dtResourceDestination.Columns.Add(new DataColumn("ProcessFrameworkID", typeof(String)));
        dtResourceDestination.Columns.Add(new DataColumn("ProcessID", typeof(String)));
        dtResourceDestination.Columns.Add(new DataColumn("ResourceID", typeof(String)));
        dtResourceDestination.Columns.Add(new DataColumn("ResourceName", typeof(String)));
        dtResourceDestination.Columns.Add(new DataColumn("NoOfResource", typeof(String)));
        HttpContext.Current.Session["ResourceDestination"] = dtResourceDestination;
        return dtResourceDestination;
    }

    public DataTable CreateDataTableForProcessSource()
    {
        DataTable dtProcessSource = new DataTable();
        dtProcessSource.Clear();
        dtProcessSource.Columns.Add(new DataColumn("ProcessID", typeof(String)));
        dtProcessSource.Columns.Add(new DataColumn("ProcessCode", typeof(String)));
        dtProcessSource.Columns.Add(new DataColumn("ProcessName", typeof(String)));
        HttpContext.Current.Session["ProcessSource"] = dtProcessSource;
        return dtProcessSource;
    }

    public DataTable CreateDataTableForProcessDestination()
    {
        DataTable dtProcessDestination = new DataTable();
        dtProcessDestination.Clear();
        dtProcessDestination.Columns.Add(new DataColumn("ProcessSequenceID", typeof(String)));
        dtProcessDestination.Columns.Add(new DataColumn("ProcessFrameworkID", typeof(String)));
        dtProcessDestination.Columns.Add(new DataColumn("ProcessID", typeof(String)));
        dtProcessDestination.Columns.Add(new DataColumn("ProcessCode", typeof(String)));
        dtProcessDestination.Columns.Add(new DataColumn("ProcessName", typeof(String)));
        dtProcessDestination.Columns.Add(new DataColumn("ProcessTotalDuration", typeof(String)));
        HttpContext.Current.Session["ProcessSequence"] = dtProcessDestination;
        return dtProcessDestination;
    }

    public DataTable CreateDataTableForMaterialBOMDestination()
    {
        DataTable dtMaterialBOMDestination = new DataTable();
        dtMaterialBOMDestination.Clear();
        dtMaterialBOMDestination.Columns.Add(new DataColumn("ProcessMaterialID", typeof(String)));
        dtMaterialBOMDestination.Columns.Add(new DataColumn("ProcessFrameworkID", typeof(String)));
        dtMaterialBOMDestination.Columns.Add(new DataColumn("ProcessID", typeof(String)));
        dtMaterialBOMDestination.Columns.Add(new DataColumn("ID", typeof(String)));
        dtMaterialBOMDestination.Columns.Add(new DataColumn("NAME", typeof(String)));
        dtMaterialBOMDestination.Columns.Add(new DataColumn("TYPE", typeof(String)));
        dtMaterialBOMDestination.Columns.Add(new DataColumn("Qty", typeof(String)));
        dtMaterialBOMDestination.Columns.Add(new DataColumn("UNITVALUE", typeof(String)));

        HttpContext.Current.Session["MaterialBOMDestination"] = dtMaterialBOMDestination;
        return dtMaterialBOMDestination;
    }
    #endregion

    #region Material
    #region Input Material

    #region Datatable

    public DataTable CreateDataTableForMaterialInputSource()
    {
        DataTable dtMaterialInputSource = new DataTable();
        dtMaterialInputSource.Clear();
        dtMaterialInputSource.Columns.Add(new DataColumn("ProcessID", typeof(String)));
        dtMaterialInputSource.Columns.Add(new DataColumn("ID", typeof(String)));
        dtMaterialInputSource.Columns.Add(new DataColumn("CODE", typeof(String)));
        dtMaterialInputSource.Columns.Add(new DataColumn("NAME", typeof(String)));
        dtMaterialInputSource.Columns.Add(new DataColumn("TYPE", typeof(String)));
        dtMaterialInputSource.Columns.Add(new DataColumn("UNITVALUE", typeof(String)));
        dtMaterialInputSource.Columns.Add(new DataColumn("Qty", typeof(String)));
        dtMaterialInputSource.Columns.Add(new DataColumn("BOMSeq", typeof(String)));
        dtMaterialInputSource.Columns.Add(new DataColumn("Refqty", typeof(String)));
        HttpContext.Current.Session["MaterialInputSource"] = dtMaterialInputSource;
        return dtMaterialInputSource;
    }

    public DataTable CreateDataTableForMaterialOutputSource()
    {
        DataTable dtMaterialOutputSource = new DataTable();
        dtMaterialOutputSource.Clear();
        dtMaterialOutputSource.Columns.Add(new DataColumn("ProcessID", typeof(String)));
        dtMaterialOutputSource.Columns.Add(new DataColumn("ID", typeof(String)));
        dtMaterialOutputSource.Columns.Add(new DataColumn("CODE", typeof(String)));
        dtMaterialOutputSource.Columns.Add(new DataColumn("NAME", typeof(String)));
        dtMaterialOutputSource.Columns.Add(new DataColumn("TYPE", typeof(String)));
        dtMaterialOutputSource.Columns.Add(new DataColumn("UNITVALUE", typeof(String)));
        dtMaterialOutputSource.Columns.Add(new DataColumn("Qty", typeof(String)));
        dtMaterialOutputSource.Columns.Add(new DataColumn("BOMSeq", typeof(String)));
        dtMaterialOutputSource.Columns.Add(new DataColumn("Refqty", typeof(String)));
        HttpContext.Current.Session["MaterialOutputSource"] = dtMaterialOutputSource;
        return dtMaterialOutputSource;
    }

    public DataTable CreateDataTableForMaterialInputDestination()
    {
        DataTable dtMaterialInputDestination = new DataTable();
        dtMaterialInputDestination.Clear();
        dtMaterialInputDestination.Columns.Add(new DataColumn("ProcessMaterialID", typeof(String)));
        dtMaterialInputDestination.Columns.Add(new DataColumn("ProcessFrameworkID", typeof(String)));
        dtMaterialInputDestination.Columns.Add(new DataColumn("ProcessID", typeof(String)));
        dtMaterialInputDestination.Columns.Add(new DataColumn("ID", typeof(String)));
        dtMaterialInputDestination.Columns.Add(new DataColumn("CODE", typeof(String)));
        dtMaterialInputDestination.Columns.Add(new DataColumn("NAME", typeof(String)));
        dtMaterialInputDestination.Columns.Add(new DataColumn("TYPE", typeof(String)));
        dtMaterialInputDestination.Columns.Add(new DataColumn("UNITVALUE", typeof(String)));
        dtMaterialInputDestination.Columns.Add(new DataColumn("UNITName", typeof(String)));
        dtMaterialInputDestination.Columns.Add(new DataColumn("Qty", typeof(String)));
        dtMaterialInputDestination.Columns.Add(new DataColumn("BOMSeq", typeof(String)));
        dtMaterialInputDestination.Columns.Add(new DataColumn("Refqty", typeof(String)));
        HttpContext.Current.Session["MaterialInputDestination"] = dtMaterialInputDestination;
        return dtMaterialInputDestination;
    }

    public DataTable CreateDataTableForMaterialOutputDestination()
    {
        DataTable dtMaterialOutputDestination = new DataTable();
        dtMaterialOutputDestination.Clear();
        dtMaterialOutputDestination.Columns.Add(new DataColumn("ProcessMaterialID", typeof(String)));
        dtMaterialOutputDestination.Columns.Add(new DataColumn("ProcessFrameworkID", typeof(String)));
        dtMaterialOutputDestination.Columns.Add(new DataColumn("ProcessID", typeof(String)));
        dtMaterialOutputDestination.Columns.Add(new DataColumn("ID", typeof(String)));
        dtMaterialOutputDestination.Columns.Add(new DataColumn("CODE", typeof(String)));
        dtMaterialOutputDestination.Columns.Add(new DataColumn("NAME", typeof(String)));
        dtMaterialOutputDestination.Columns.Add(new DataColumn("TYPE", typeof(String)));
        dtMaterialOutputDestination.Columns.Add(new DataColumn("UNITVALUE", typeof(String)));
        dtMaterialOutputDestination.Columns.Add(new DataColumn("UNITName", typeof(String)));
        dtMaterialOutputDestination.Columns.Add(new DataColumn("Qty", typeof(String)));
        dtMaterialOutputDestination.Columns.Add(new DataColumn("BOMSeq", typeof(String)));
        dtMaterialOutputDestination.Columns.Add(new DataColumn("Refqty", typeof(String)));
        HttpContext.Current.Session["MaterialOutputDestination"] = dtMaterialOutputDestination;
        return dtMaterialOutputDestination;
    }
    #endregion

    #region Private Method
    /// <summary>
    /// Bind material input data with source grid
    /// </summary>
    private void LoadSourceMaterialInput(string ProcessID)
    {
        try
        {
            if (HttpContext.Current.Session["MaterialInputSource"] == null)
            {
                DataTable dt = new DataTable();
                //LoadWorkstation();
                LoadMaterialInput();
                LoadDestinationInputMaterial(dt);
                //LoadDestinationWorkstation(dt);
            }
            else
            {
                DataTable dt = new DataTable();
                dt = GetDataExist(((DataTable)(Session["MaterialInputSource"])), ProcessID, "ProcessID");
                if (dt.Rows.Count == 0)
                {
                    LoadMaterialInput();
                    // LoadWorkstation();
                }
                else
                {
                    LoadSourceInputMaterial(dt);
                    //LoadWorkstation(dt);
                }
            }
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }

    private void LoadSourceMaterialOutput(string ProcessID)
    {
        try
        {
            if (HttpContext.Current.Session["MaterialOutputSource"] == null)
            {
                DataTable dt = new DataTable();
                //LoadWorkstation();
                LoadMaterialOutput();
                LoadDestinationOutputMaterial(dt);
                //LoadDestinationWorkstation(dt);
            }
            else
            {
                DataTable dt = new DataTable();
                dt = GetDataExist(((DataTable)(Session["MaterialOutputSource"])), ProcessID, "ProcessID");
                if (dt.Rows.Count == 0)
                {
                    LoadMaterialOutput();
                    // LoadWorkstation();
                }
                else
                {
                    LoadSourceOutputMaterial(dt);
                    //LoadWorkstation(dt);
                }
            }
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }

    private void LoadDestinationMaterialInput(string ProcessID)
    {
        try
        {
            DataTable dt = new DataTable();
            DataSet ds;
            if (HttpContext.Current.Session["MaterialInputDestination"] == null)//Comments By rajeev 25072017
            {
                //if (hdnfProcessFrameworkID.Value != "")
                //{
                //    ds = oProcessFramework.FetchProcessFrameworkEdit(hdnfProcessFrameworkID.Value, ProcessID);
                //    if (ds.Tables[3].Rows.Count > 0)
                //    {
                //        gvDestMaterialInput.DataSource = ds.Tables[3];
                //        gvDestMaterialInput.DataBind();
                //        HttpContext.Current.Session["MaterialInputDestination"] = ds.Tables[3];//Comments OPen By Rajeev_11072017
                //    }
                //}
                //else
                //{
                LoadDestinationInputMaterial(dt);
                //}
            }//Comments By rajeev 25072017
            else
            {
                //DataTable dt = new DataTable();
                dt = GetDataExist(((DataTable)(Session["MaterialInputDestination"])), ProcessID, "ProcessID");
                if (dt.Rows.Count == 0)
                {
                    LoadDestinationInputMaterial(dt);
                    //LoadDestinationQC(dt);                    
                }
                else
                {
                    LoadDestinationInputMaterial(dt);
                    //dt = GetDataExist(((DataTable)(Session["QCSource"])), ProcessID, "ProcessID");                        
                    //LoadQC(dt);
                    //LoadDestinationQC(dt);
                }
            }
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }
    private void LoadDestinationMaterialOutput(string ProcessID)
    {
        try
        {
            DataSet ds;
            DataTable dt = new DataTable();
            if (HttpContext.Current.Session["MaterialOutputDestination"] == null)
            {
                /*if (hdnfProcessFrameworkID.Value != "")
                {
                    ds = oProcessFramework.FetchProcessFrameworkEdit(hdnfProcessFrameworkID.Value, ProcessID);
                    if (ds.Tables[4].Rows.Count > 0)
                    {
                        gvDestMaterialOutput.DataSource = ds.Tables[4];
                        gvDestMaterialOutput.DataBind();
                        HttpContext.Current.Session["MaterialOutputDestination"] = ds.Tables[4];//(Comments Open By Rajeev)
                    }
                }
                else
                {*/
                LoadDestinationOutputMaterial(dt);
                //}
            }
            else
            {
                //DataTable dt = new DataTable();
                dt = GetDataExist(((DataTable)(Session["MaterialOutputDestination"])), ProcessID, "ProcessID");
                if (dt.Rows.Count == 0)
                {
                    LoadDestinationOutputMaterial(dt);
                    //LoadDestinationQC(dt);
                }
                else
                {
                    LoadDestinationOutputMaterial(dt);
                    //dt = GetDataExist(((DataTable)(Session["QCSource"])), ProcessID, "ProcessID");                        
                    //LoadQC(dt);
                    //LoadDestinationQC(dt);
                }
            }
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }
    private void LoadMaterialInput(String[] MaterialType)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = oProcess.BindActive_ProcessMaterial(HttpContext.Current.Session["DEPOTID"].ToString());
            dt = GetDataExist(dt, MaterialType, "TYPE");
            if (dt.Rows.Count > 0)
            {
                gvSourceMaterialInput.DataSource = dt;
                gvSourceMaterialInput.DataBind();
            }
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }
    private void LoadMaterialInput()
    {
        try
        {
            string str = "test";
            DataTable dt = new DataTable();
            dt = oProcess.BindActive_ProcessMaterial(HttpContext.Current.Session["DEPOTID"].ToString());
            dt = GetDataExistInDrop(dt, str, "TYPE");
            if (dt.Rows.Count > 0)
            {
                gvSourceMaterialInput.DataSource = dt;
                gvSourceMaterialInput.DataBind();
            }
            else
            {
                gvSourceMaterialInput.DataSource = null;
                gvSourceMaterialInput.DataBind();
            }
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }
    private void LoadMaterialOutput()
    {
        try
        {
            string str = "test";
            DataTable dt = new DataTable();
            dt = oProcess.BindActive_ProcessOutputMaterial(HttpContext.Current.Session["DEPOTID"].ToString());
            dt = GetDataExistInFGSFG(dt, str, "TYPE");
            if (dt.Rows.Count > 0)
            {
                gvSourceMaterialOutput.DataSource = dt;
                gvSourceMaterialOutput.DataBind();
            }
            else
            {
                gvSourceMaterialOutput.DataSource = null;
                gvSourceMaterialOutput.DataBind();
            }
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }
    protected void ddlMaterialType_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        string Product = "";
        var query = from ListItem item in ddlMaterialType.Items where item.Selected select item;
        foreach (ListItem item in query)
        {
            // item ...
            Product += item.Value + ',';
        }
        Product = Product.Substring(0, Product.Length - 1);
    }
    #endregion
    #endregion
    #endregion

    #region ConvertDatatableToXMLProcessSequence
    public string ConvertDatatableToXMLProcessSequence(DataTable dt)
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

    #region ConvertDatatableToXMLWorkstation
    public string ConvertDatatableToXMLWorkstation(DataTable dt)
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

    #region ConvertDatatableToXMLResource
    public string ConvertDatatableToXMLResource(DataTable dt)
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

    #region ConvertDatatableToXMLQC
    public string ConvertDatatableToXMLQC(DataTable dt)
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

    #region Save Process Framework

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            //string Product = "";
            //var query = from ListItem item in ddlMaterialType.Items where item.Selected select item;
            //foreach (ListItem item in query)
            //{
            //    // item ...
            //    Product += item.Value + ',';

            //}
            //Product = Product.Substring(0, Product.Length - 1);
            ////LoadMaterialInput(String[] Product);
            //LoadMaterialInput();
            //mpe.Show();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string InvoiceNo = string.Empty;
            string xmlProcessSequence = string.Empty;
            string xmlProdessMaterialInput = string.Empty;
            string xmlProdessMaterialOutput = string.Empty;
            string xmlProcessFrameworkWorkstation = string.Empty;
            string xmlProcessFrameworkResource = string.Empty;
            string xmlProcessFrameworkQC = string.Empty;
            string xmlFGProduct = string.Empty;
            string ProcessFrameworkID = hdnfProcessFrameworkID.Value == "" ? "" : hdnfProcessFrameworkID.Value;
            DataTable dtProcessSequence = new DataTable();
            DataTable dtremove = new DataTable();

            getWorkstationDestinationGridInfo();//["WorkstationDestination"]
            getMaterialInputDestinationGridInfo();//["MaterialInputDestination"]
            getMaterialOutputDestinationGridInfo();//Session["MaterialOutputDestination"];
            getResourceDestinationGridInfo();//Session["ResourceDestination"];
            getQCDestinationGridInfo();//Session["QCDestination"];

            if (ProcessFrameworkID == "")
            {
                dtremove = (DataTable)HttpContext.Current.Session["ProcessSequence"];
            }
            else
            {
                dtremove = (DataTable)HttpContext.Current.Session["ProcessSequence"];
                if (dtremove.Columns["ProcessSequenceID"].ColumnName == "ProcessSequenceID" || dtremove.Columns["ProcessFrameworkID"].ColumnName == "ProcessFrameworkID")
                {
                    dtremove.Columns.Remove("ProcessSequenceID");
                    dtremove.Columns.Remove("ProcessFrameworkID");
                }
                else
                {
                    dtProcessSequence = dtremove;
                }
            }
            dtProcessSequence = dtremove;
            DataTable dtProcessWorkstation = (DataTable)HttpContext.Current.Session["WorkstationDestination"];
            DataTable dtProcessMaterialInput = (DataTable)HttpContext.Current.Session["MaterialInputDestination"];
            DataTable dtProcessMaterialOutput = (DataTable)HttpContext.Current.Session["MaterialOutputDestination"];
            DataTable dtProcessResource = (DataTable)HttpContext.Current.Session["ResourceDestination"];
            DataTable dtProcessQC = (DataTable)HttpContext.Current.Session["QCDestination"];
            DataTable dtFGProduct = FGProduct();
            xmlProcessSequence = ConvertDatatableToXMLProcessSequence(dtProcessSequence);
            xmlProcessFrameworkWorkstation = ConvertDatatableToXMLWorkstation(dtProcessWorkstation);
            xmlProcessFrameworkResource = ConvertDatatableToXMLResource(dtProcessResource);
            xmlProcessFrameworkQC = ConvertDatatableToXMLQC(dtProcessQC);
            xmlProdessMaterialInput = ConvertDatatableToXMLQC(dtProcessMaterialInput);
            xmlProdessMaterialOutput = ConvertDatatableToXMLQC(dtProcessMaterialOutput);
            xmlFGProduct = ConvertDatatableToXMLQC(dtFGProduct);
            if (dtProcessSequence.Rows.Count > 0)
            {
                if (ProcessFrameworkID == "")
                {
                    InvoiceNo = oProcessFramework.InsertUpdateProcessFramework(this.txtProcessFrameworkName.Text.Trim(), this.txtProcessFrameworkCode.Text.Trim(),
                                                                       this.txtFrameworkDescription.Text.Trim(), ddlProduct.SelectedValue.ToString().Trim(), HttpContext.Current.Session["UserID"].ToString(),
                                                                       HttpContext.Current.Session["UserID"].ToString(), xmlProcessSequence, xmlProcessFrameworkWorkstation,
                                                                       xmlProcessFrameworkResource, xmlProdessMaterialInput, xmlProdessMaterialOutput, xmlProcessFrameworkQC, xmlFGProduct, "I", ProcessFrameworkID, hdfProcessID.Value);
                }
                else
                {
                    InvoiceNo = oProcessFramework.InsertUpdateProcessFramework(this.txtProcessFrameworkName.Text.Trim(), this.txtProcessFrameworkCode.Text.Trim(),
                                                                           this.txtFrameworkDescription.Text.Trim(), ddlProduct.SelectedValue.ToString().Trim(), HttpContext.Current.Session["UserID"].ToString(),
                                                                           HttpContext.Current.Session["UserID"].ToString(), xmlProcessSequence, xmlProcessFrameworkWorkstation,
                                                                           xmlProcessFrameworkResource, xmlProdessMaterialInput, xmlProdessMaterialOutput, xmlProcessFrameworkQC, xmlFGProduct, "U", ProcessFrameworkID, hdfProcessID.Value);
                }
                if (InvoiceNo == "1")
                {
                    ResetSession();
                    MessageBox1.ShowSuccess("Record Saved Successfully!");
                    LoadProcessFramework();
                    pnlAdd.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "";
                    btnaddhide.Style["display"] = "";
                    hdnfProcessFrameworkID.Value = "";
                    hdfProcessID.Value = string.Empty;
                }
                else
                {
                    MessageBox1.ShowError("Error saving records..!");
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

    #region Reset Session

    public void ResetSession()
    {
        HttpContext.Current.Session["ProcessSource"] = null;
        HttpContext.Current.Session["ProcessSequence"] = null;
        HttpContext.Current.Session["MaterialInputSource"] = null;
        HttpContext.Current.Session["MaterialInputDestination"] = null;
        HttpContext.Current.Session["MaterialOutputSource"] = null;
        HttpContext.Current.Session["MaterialOutputDestination"] = null;
        HttpContext.Current.Session["WorkstationSource"] = null;
        HttpContext.Current.Session["WorkstationDestination"] = null;
        HttpContext.Current.Session["ResourceSource"] = null;
        HttpContext.Current.Session["ResourceDestination"] = null;
        HttpContext.Current.Session["QCSource"] = null;
        HttpContext.Current.Session["QCDestination"] = null;
    }
    #endregion

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            DataRow dr;
            dt = (DataTable)HttpContext.Current.Session["ProcessSequence"];
            if (dt.Rows.Count > 0)
            {

            }
            foreach (GridViewRow row in gvDest.Rows)
            {
                //ImageButton imgbPopupOpen = (ImageButton)row.FindControl("imgbPopupOpen");
                TextBox txtTotalDuration = (TextBox)row.FindControl("txtTotalDuration");
                // HiddenField hdfProcessID = (HiddenField)row.FindControl("hdfProcessID");
                string cell_1_Value = gvDest.Rows[row.RowIndex].Cells[0].Text;
                if (cell_1_Value != "&nbsp;")
                {
                    hdnProcessID.Value = cell_1_Value;
                }
                //
                string cell_2_Value = gvDest.Rows[row.RowIndex].Cells[1].Text;
                string cell_3_Value = gvDest.Rows[row.RowIndex].Cells[2].Text;
                string cell_4_Value = txtTotalDuration.Text;
                if (cell_1_Value != "&nbsp;" && cell_1_Value != "")
                {
                    dr = dt.NewRow();
                    //if (row.RowIndex ==dt.Rows.Count-1)
                    //{
                    //    hdnProcessID.Value = gvDest.Rows[row.RowIndex].Cells[0].Text;
                    //}
                    dr[0] = cell_1_Value;
                    dr[1] = cell_2_Value;
                    dr[2] = cell_3_Value;
                    dr[3] = cell_4_Value;
                    dt.Rows.Add(dr);
                }
                gvSource.DataSource = (DataTable)HttpContext.Current.Session["ProcessSource"];
                gvSource.DataBind();

                //else
                //{
                //    dr = dtWorkstationSource.NewRow();
                //    dr[0] = hdfProcessID.Value;
                //    dr[1] = lblWorkStationID.Text;
                //    dr[2] = lblWorkStationCode.Text;
                //    dr[3] = lblWorkStationName.Text;
                //    dr[4] = strDuration;
                //    dtWorkstationSource.Rows.Add(dr);
                //}
                // }

                // str = str.Replace(",", "");
                //str = str.Trim();
                //if (strDuration != null && strDuration != string.Empty)
                //{
                //    dr = dtWorkstationDestination.NewRow();
                //    dr[2] = hdfProcessID.Value;
                //    dr[3] = lblWorkStationID.Text;
                //    dr[4] = lblWorkStationCode.Text;
                //    dr[5] = lblWorkStationName.Text;
                //    dr[6] = txtDuration.Text;
                //    dtWorkstationDestination.Rows.Add(dr);
                //}

                //else
                //{
                //    //dr = dtWorkstationSource.NewRow();
                //    //dr[0] = hdfProcessID.Value;
                //    //dr[1] = lblWorkStationID.Text;
                //    //dr[2] = lblWorkStationCode.Text;
                //    //dr[3] = lblWorkStationName.Text;
                //    //dr[4] = txtDuration.Text;
                //    //dtWorkstationSource.Rows.Add(dr);
                //}
            }
            HttpContext.Current.Session["ProcessSequence"] = dt;
            gvDest.DataSource = dt;
            gvDest.DataBind();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void gvResourceSource_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowIndex == 0)
                e.Row.Style.Add("height", "40px");
        }
    }

    /*Add By Rajeev*/

    private void getWorkstationDestinationGridInfo()
    {
        try
        {
            DataTable dtWorkstationSource = new DataTable();
            DataTable dtWorkstationDestination = new DataTable();
            DataRow dr;
            string strDuration = string.Empty;
            if (HttpContext.Current.Session["WorkstationSource"] == null)
            {
                dtWorkstationSource = CreateDataTableForWorkStationSource();
            }
            else
            {
                dtWorkstationSource = (DataTable)HttpContext.Current.Session["WorkstationSource"];
            }

            if (HttpContext.Current.Session["WorkstationDestination"] == null || HttpContext.Current.Session["WorkstationDestination"] != null)
            {
                dtWorkstationDestination = CreateDataTableForWorkStationDestination();
            }
            else
            {
                dtWorkstationDestination = (DataTable)HttpContext.Current.Session["WorkstationDestination"];
            }
            dt = GetDataExist(dtWorkstationSource, hdfProcessID.Value, "ProcessID");
            if (dt.Rows.Count > 0)
            {
                for (int i = dtWorkstationSource.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow drResource = dtWorkstationSource.Rows[i];
                    if (Convert.ToString(drResource[0]) == hdfProcessID.Value)
                    {
                        dtWorkstationSource.Rows.Remove(drResource);
                    }
                }
            }
            foreach (GridViewRow row in gvWorkstaionDestination.Rows)
            {
                Label lblWorkStationID = (Label)row.FindControl("lblWorkStationID");
                Label lblWorkStationCode = (Label)row.FindControl("lblWorkStationCode");
                Label lblWorkStationName = (Label)row.FindControl("lblWorkStationName");
                Label lblDuration = (Label)row.FindControl("lblDuration");
                if (lblDuration.Text.Trim() != null && lblDuration.Text.Trim() != string.Empty)
                {
                    strDuration = lblDuration.Text.Replace(",", "").Trim();
                    if (strDuration != null && strDuration != string.Empty)
                    {
                        dr = dtWorkstationDestination.NewRow();
                        dr[2] = hdfProcessID.Value == "" ? Convert.ToString(ViewState["ProcessID"]) : hdfProcessID.Value;
                        dr[3] = lblWorkStationID.Text;
                        dr[4] = lblWorkStationCode.Text;
                        dr[5] = lblWorkStationName.Text;
                        dr[6] = strDuration;
                        dtWorkstationDestination.Rows.Add(dr);
                    }
                    else
                    {
                        dr = dtWorkstationSource.NewRow();
                        dr[0] = hdfProcessID.Value;
                        dr[1] = lblWorkStationID.Text;
                        dr[2] = lblWorkStationCode.Text;
                        dr[3] = lblWorkStationName.Text;
                        dr[4] = strDuration;
                        dtWorkstationSource.Rows.Add(dr);
                    }
                }
                else
                {
                    dr = dtWorkstationSource.NewRow();
                    dr[0] = hdfProcessID.Value;
                    dr[1] = lblWorkStationID.Text;
                    dr[2] = lblWorkStationCode.Text;
                    dr[3] = lblWorkStationName.Text;
                    dr[4] = lblDuration.Text;
                    dtWorkstationSource.Rows.Add(dr);
                }
            }
            //HttpContext.Current.Session["WorkstationSource"] = dtWorkstationSource;
            HttpContext.Current.Session["WorkstationDestination"] = dtWorkstationDestination;
            //LoadDestinationWorkstation(hdfProcessID.Value);
            //LoadSourceWorkstation(hdfProcessID.Value);
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }

    private void getMaterialInputDestinationGridInfo()
    {
        try
        {
            string strMaterialQTY = string.Empty;
            DataTable dtMaterialInputSource = new DataTable();
            DataTable dtMaterialInputDestination = new DataTable();
            DataRow dr;
            if (HttpContext.Current.Session["MaterialInputSource"] == null)
            {
                dtMaterialInputSource = CreateDataTableForMaterialInputSource();
            }
            else
            {
                dtMaterialInputSource = (DataTable)HttpContext.Current.Session["MaterialInputSource"];
            }
            if (HttpContext.Current.Session["MaterialInputDestination"] == null || HttpContext.Current.Session["MaterialInputDestination"] != null)
            {
                dtMaterialInputDestination = CreateDataTableForMaterialInputDestination();
            }
            else
            {
                dtMaterialInputDestination = (DataTable)HttpContext.Current.Session["MaterialInputDestination"];
            }
            dt = GetDataExist(dtMaterialInputSource, hdfProcessID.Value, "ProcessID");

            if (dt.Rows.Count > 0)
            {
                for (int i = dtMaterialInputSource.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow drMaterialInput = dtMaterialInputSource.Rows[i];
                    if (Convert.ToString(drMaterialInput[0]) == hdfProcessID.Value)
                    {
                        dtMaterialInputSource.Rows.Remove(drMaterialInput);
                    }
                }
            }
            foreach (GridViewRow row in gvDestMaterialInput.Rows)
            {
                Label lblMaterialInputID = (Label)row.FindControl("lblMaterialInputID");
                Label lblMaterialInputCode = (Label)row.FindControl("lblMaterialInputCode");
                Label lbllMaterialInputName = (Label)row.FindControl("lbllMaterialInputName");
                Label lbllMaterialInputType = (Label)row.FindControl("lbllMaterialInputType");
                Label lblMaterialInputUnit = (Label)row.FindControl("lbllMaterialInputUnit");
                Label lbllMaterialInputUnitValue = (Label)row.FindControl("lbllMaterialInputUnitValue");

                Label txtQty = (Label)row.FindControl("lblQty");
                Label txtRefQty = (Label)row.FindControl("lblRefQty");

                if (txtQty.Text.Trim() != null && txtQty.Text.Trim() != string.Empty)
                {
                    strMaterialQTY = txtQty.Text.Replace(",", "").Trim();
                    if (strMaterialQTY != null && strMaterialQTY != string.Empty)
                    {
                        dr = dtMaterialInputDestination.NewRow();
                        //dr[2] = hdfProcessID.Value;
                        dr[2] = hdfProcessID.Value == "" ? Convert.ToString(ViewState["ProcessID"]) : hdfProcessID.Value;
                        dr[3] = lblMaterialInputID.Text;
                        dr[4] = lblMaterialInputCode.Text;
                        dr[5] = lbllMaterialInputName.Text;
                        dr[6] = lbllMaterialInputType.Text;
                        dr[7] = lbllMaterialInputUnitValue.Text;
                        dr[8] = lblMaterialInputUnit.Text;
                        dr[9] = txtQty.Text;
                        dr[10] = "1";
                        dr[11] = txtRefQty.Text;
                        dtMaterialInputDestination.Rows.Add(dr);
                    }
                    else
                    {
                        dr = dtMaterialInputSource.NewRow();
                        dr[0] = hdfProcessID.Value;
                        dr[1] = lblMaterialInputID.Text;
                        dr[2] = lblMaterialInputCode.Text;
                        dr[3] = lbllMaterialInputName.Text;
                        dr[4] = lbllMaterialInputType.Text;
                        dr[5] = lblMaterialInputUnit.Text;
                        dr[6] = txtQty.Text;
                        dr[7] = "1";
                        dr[8] = txtRefQty.Text;
                        dtMaterialInputSource.Rows.Add(dr);
                    }
                }
            }
            //HttpContext.Current.Session["MaterialInputSource"] = dtMaterialInputSource;
            HttpContext.Current.Session["MaterialInputDestination"] = dtMaterialInputDestination;
            //LoadSourceMaterialInput(hdfProcessID.Value);
            //LoadDestinationMaterialInput(hdfProcessID.Value);
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }

    private void getMaterialOutputDestinationGridInfo()
    {
        try
        {
            string strMaterialQTY = string.Empty;
            DataTable dtMaterialOutputSource = new DataTable();
            DataTable dtMaterialOutputDestination = new DataTable();
            DataRow dr;
            if (HttpContext.Current.Session["MaterialOutputSource"] == null)
            {
                dtMaterialOutputSource = CreateDataTableForMaterialOutputSource();
            }
            else
            {
                dtMaterialOutputSource = (DataTable)HttpContext.Current.Session["MaterialOutputSource"];
            }
            if (HttpContext.Current.Session["MaterialOutputDestination"] == null || HttpContext.Current.Session["MaterialOutputDestination"] != null)
            {
                dtMaterialOutputDestination = CreateDataTableForMaterialOutputDestination();
            }
            else
            {
                dtMaterialOutputDestination = (DataTable)HttpContext.Current.Session["MaterialOutputDestination"];
            }
            dt = GetDataExist(dtMaterialOutputSource, hdfProcessID.Value, "ProcessID");
            if (dt.Rows.Count > 0)
            {
                for (int i = dtMaterialOutputSource.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow drMaterialInput = dtMaterialOutputSource.Rows[i];
                    if (Convert.ToString(drMaterialInput[0]) == hdfProcessID.Value)
                    {
                        dtMaterialOutputSource.Rows.Remove(drMaterialInput);
                    }
                }
            }
            foreach (GridViewRow row in gvDestMaterialOutput.Rows)
            {
                Label lblMaterialOutputID = (Label)row.FindControl("lblMaterialOutputID");
                Label lblMaterialOutputCode = (Label)row.FindControl("lblMaterialOutputCode");
                Label lbllMaterialOutputName = (Label)row.FindControl("lbllMaterialOutputName");
                Label lbllMaterialOutputType = (Label)row.FindControl("lbllMaterialOutputType");
                Label ddlUnit = (Label)row.FindControl("lbllMaterialInputUnit");
                Label lbllMaterialInputUnitValue = (Label)row.FindControl("lbllMaterialInputUnitValue");
                Label txtQty = (Label)row.FindControl("lblQty");
                Label txtRefQtyOutput = (Label)row.FindControl("lblRefQtyOutput");
                if (txtQty.Text.Trim() != null && txtQty.Text.Trim() != string.Empty)
                {
                    strMaterialQTY = txtQty.Text.Replace(",", "").Trim();
                    if (strMaterialQTY != null && strMaterialQTY != string.Empty)
                    {
                        dr = dtMaterialOutputDestination.NewRow();
                        dr[2] = hdfProcessID.Value == "" ? Convert.ToString(ViewState["ProcessID"]) : hdfProcessID.Value;
                        dr[3] = lblMaterialOutputID.Text;
                        dr[4] = lblMaterialOutputCode.Text;
                        dr[5] = lbllMaterialOutputName.Text;
                        dr[6] = lbllMaterialOutputType.Text;
                        dr[7] = lbllMaterialInputUnitValue.Text;
                        dr[8] = ddlUnit.Text;
                        dr[9] = txtQty.Text;
                        dr[10] = "1";
                        dr[11] = txtRefQtyOutput.Text;
                        dtMaterialOutputDestination.Rows.Add(dr);
                    }
                    else
                    {
                        dr = dtMaterialOutputSource.NewRow();
                        dr[0] = hdfProcessID.Value;
                        dr[1] = lblMaterialOutputID.Text;
                        dr[2] = lblMaterialOutputCode.Text;
                        dr[3] = lbllMaterialOutputName.Text;
                        dr[4] = lbllMaterialOutputType.Text;
                        dr[5] = ddlUnit.Text;
                        dr[6] = txtQty.Text;
                        dr[7] = "1";
                        dr[8] = txtRefQtyOutput.Text;
                        dtMaterialOutputSource.Rows.Add(dr);
                    }
                }
            }
            //HttpContext.Current.Session["MaterialOutputSource"] = dtMaterialOutputSource;
            HttpContext.Current.Session["MaterialOutputDestination"] = dtMaterialOutputDestination;
            //LoadSourceMaterialOutput(hdfProcessID.Value);
            //LoadDestinationMaterialOutput(hdfProcessID.Value);
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }

    private void getResourceDestinationGridInfo()
    {
        try
        {
            DataTable dtResourceSource = new DataTable();
            DataTable dtResourceDestination = new DataTable();
            DataRow dr;
            string strNoOfResource = string.Empty;
            if (HttpContext.Current.Session["ResourceSource"] == null)
            {
                dtResourceSource = CreateDataTableForResourceSource();
            }
            else
            {
                dtResourceSource = (DataTable)HttpContext.Current.Session["ResourceSource"];
            }
            if (HttpContext.Current.Session["ResourceDestination"] == null || HttpContext.Current.Session["ResourceDestination"] != null)
            {
                dtResourceDestination = CreateDataTableForResourceDestination();
            }
            else
            {
                dtResourceDestination = (DataTable)HttpContext.Current.Session["ResourceDestination"];
            }
            dt = GetDataExist(dtResourceSource, hdfProcessID.Value, "ProcessID");
            if (dt.Rows.Count > 0)
            {
                for (int i = dtResourceSource.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow drResource = dtResourceSource.Rows[i];
                    if (Convert.ToString(drResource[0]) == hdfProcessID.Value)
                    {
                        dtResourceSource.Rows.Remove(drResource);
                    }
                }
            }
            foreach (GridViewRow row in gvResourceDestination.Rows)
            {
                Label lblUTID = (Label)row.FindControl("lblUTID");
                Label lblUTNAME = (Label)row.FindControl("lblUTNAME");
                Label lblNoOfResource = (Label)row.FindControl("lblNoOfResource");
                if (lblNoOfResource.Text.Trim() != null && lblNoOfResource.Text.Trim() != string.Empty)
                {
                    strNoOfResource = lblNoOfResource.Text.Replace(",", "").Trim();
                    if (strNoOfResource != null && strNoOfResource != string.Empty)
                    {
                        dr = dtResourceDestination.NewRow();
                        dr[2] = hdfProcessID.Value == "" ? Convert.ToString(ViewState["ProcessID"]) : hdfProcessID.Value;
                        dr[3] = lblUTID.Text;
                        dr[4] = lblUTNAME.Text;
                        dr[5] = strNoOfResource;
                        dtResourceDestination.Rows.Add(dr);
                    }
                    else
                    {
                        dr = dtResourceSource.NewRow();
                        dr[0] = hdfProcessID.Value;
                        dr[1] = lblUTID.Text;
                        dr[2] = lblUTNAME.Text;
                        dr[3] = strNoOfResource;
                        dtResourceSource.Rows.Add(dr);
                    }
                }
                //HttpContext.Current.Session["ResourceSource"] = dtResourceSource;
                HttpContext.Current.Session["ResourceDestination"] = dtResourceDestination;
                //LoadDestinationResource(hdfProcessID.Value);
                //LoadSourceResource(hdfProcessID.Value);
            }
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }

    private void getQCDestinationGridInfo()
    {
        try
        {
            DataTable dtQCSource = new DataTable();
            DataTable dtQCDestination = new DataTable();
            DataRow dr;
            if (HttpContext.Current.Session["QCSource"] == null)
            {
                dtQCSource = CreateDataTableForQCSource();
            }
            else
            {
                dtQCSource = (DataTable)HttpContext.Current.Session["QCSource"];
            }
            if (HttpContext.Current.Session["QCDestination"] == null || HttpContext.Current.Session["QCDestination"] != null)
            {
                dtQCDestination = CreateDataTableForQCDestination();
            }
            else
            {
                dtQCDestination = (DataTable)HttpContext.Current.Session["QCDestination"];
            }
            dt = GetDataExist(dtQCSource, hdfProcessID.Value, "ProcessID");
            if (dt.Rows.Count > 0)
            {
                for (int i = dtQCSource.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow drQC = dtQCSource.Rows[i];
                    if (Convert.ToString(drQC[0]) == hdfProcessID.Value)
                    {
                        dtQCSource.Rows.Remove(drQC);
                    }
                }
            }
            foreach (GridViewRow row in gvQCDestination.Rows)
            {
                Label lblQCID = (Label)row.FindControl("lblQCID");
                Label lblQCName = (Label)row.FindControl("lblQCName");

                if (lblQCID.Text != null & lblQCID.Text != string.Empty)
                {
                    dr = dtQCDestination.NewRow();
                    dr[2] = hdfProcessID.Value == "" ? Convert.ToString(ViewState["ProcessID"]) : hdfProcessID.Value;
                    dr[3] = lblQCID.Text;
                    dr[4] = lblQCName.Text;
                    dtQCDestination.Rows.Add(dr);
                }
                else
                {
                    dr = dtQCSource.NewRow();
                    dr[0] = hdfProcessID.Value;
                    dr[1] = lblQCID.Text;
                    dr[2] = lblQCName.Text;
                    dtQCSource.Rows.Add(dr);
                }
            }
            //HttpContext.Current.Session["QCSource"] = dtQCSource;
            HttpContext.Current.Session["QCDestination"] = dtQCDestination;
            //LoadDestinationQC(hdfProcessID.Value);
            //LoadSourceQC(hdfProcessID.Value);
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }

    #region Add Process
    protected void imgAddProcess_Click(object sender, EventArgs e)
    {
        try
        {
            GetProcessGridInfo();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    private void GetProcessGridInfo()
    {
        try
        {
            DataTable dtProcessSource = new DataTable();
            DataTable dtProcessDestination = new DataTable();
            DataRow dr;
            if (HttpContext.Current.Session["ProcessSource"] == null)
            {
                dtProcessSource = CreateDataTableForProcessSource();
            }
            else
            {
                dtProcessSource = (DataTable)HttpContext.Current.Session["ProcessSource"];
            }
            if (HttpContext.Current.Session["ProcessSequence"] == null)
            {
                dtProcessDestination = CreateDataTableForProcessDestination();
            }
            else
            {
                dtProcessDestination = (DataTable)HttpContext.Current.Session["ProcessSequence"];
            }
            dt = GetDataExist(dtProcessSource, hdfProcessID.Value, "ProcessID");
            if (dt.Rows.Count > 0)
            {
                for (int i = dtProcessSource.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow drProcess = dtProcessSource.Rows[i];
                    if (Convert.ToString(drProcess[0]) == hdfProcessID.Value)
                    {
                        dtProcessSource.Rows.Remove(drProcess);
                    }
                }
            }
            foreach (GridViewRow row in gvSource.Rows)
            {
                Label lblProcessID = (Label)row.FindControl("lblProcessID");
                Label lblProcessCode = (Label)row.FindControl("lblProcessCode");
                Label lblProcessName = (Label)row.FindControl("lblProcessName");
                TextBox txtTotalDuration = (TextBox)row.FindControl("txtTotalDuration");
                CheckBox chkRow = (CheckBox)row.FindControl("chkRow");
                if (chkRow != null & chkRow.Checked)
                {
                    dr = dtProcessDestination.NewRow();
                    dr[2] = lblProcessID.Text;//hdfProcessID.Value;
                    dr[3] = lblProcessCode.Text;
                    dr[4] = lblProcessName.Text;
                    dtProcessDestination.Rows.Add(dr);
                }
                else
                {
                    dr = dtProcessSource.NewRow();
                    dr[0] = lblProcessID.Text;//hdfProcessID.Value;
                    dr[1] = lblProcessCode.Text;
                    dr[2] = lblProcessName.Text;
                    dtProcessSource.Rows.Add(dr);
                }
            }
            HttpContext.Current.Session["ProcessSource"] = dtProcessSource;
            HttpContext.Current.Session["ProcessSequence"] = dtProcessDestination;
            //LoadDestinationProcess(hdfProcessID.Value);
            gvDest.DataSource = dtProcessDestination;
            gvDest.DataBind();
            LoadSourceProcess(hdfProcessID.Value);
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }

    private void LoadDestinationProcess(string ProcessID)
    {
        try
        {
            DataTable dt = new DataTable();
            if (HttpContext.Current.Session["ProcessSequence"] == null)
            {
                LoadDestinationProcess(dt);
            }
            else
            {
                dt = GetDataExist(((DataTable)(Session["ProcessSequence"])), ProcessID, "ProcessID");
                if (dt.Rows.Count == 0)
                {
                    LoadDestinationProcess(dt);
                }
                else
                {
                    LoadDestinationProcess(dt);
                }
            }
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }

    private void LoadSourceProcess(string ProcessID)
    {
        try
        {
            if (HttpContext.Current.Session["ProcessSource"] == null)
            {
                DataTable dt = new DataTable();
                LoadProcessMaster();
                LoadDestinationProcess(dt);
            }
            else
            {
                DataTable dt = new DataTable();
                dt = GetDataExist(((DataTable)(Session["ProcessSource"])), ProcessID, "ProcessID");
                if (dt.Rows.Count == 0)
                {
                    LoadProcessMaster();
                }
                else
                {
                    LoadProcess(dt);
                }
            }
        }
        catch (Exception ex)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script>alert('" + ex.Message.Replace("'", "") + "')</script>");
        }
    }

    private void LoadDestinationProcess(DataTable dt)
    {
        gvDest.DataSource = dt;
        gvDest.DataBind();
    }

    private void LoadProcess(DataTable dt)
    {
        //gvSource.DataSource = dt;
        //gvSource.DataBind();
    }

    #endregion

    /*End*/
}