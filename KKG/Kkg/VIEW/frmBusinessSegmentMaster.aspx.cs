using BAL;
using Obout.Grid;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmBusinessSegmentMaster : System.Web.UI.Page
{
    ClsBusinessSegmentMaster ClsBSMaster = new ClsBusinessSegmentMaster();
    ClsBusniessProductMap clsPBMaster = new ClsBusniessProductMap();
    ClsProductMaster ClsProduct = new ClsProductMaster();
    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ////Session["UserID"] = "1";
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                LoadBusinessSegmentMaster();
                LoadDivision();
                LoadCategory();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

    }

    public void LoadBusinessSegmentMaster()
    {
        try
        {
            gvBusinessSegment.DataSource = ClsBSMaster.BindBusnessSegmentMasterGrid();
            gvBusinessSegment.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    protected void btnAddBusinessSegment_Click(object sender, EventArgs e)
    {
        try
        {
            txtBSCode.Text = "";
            txtBusinessSegmentName.Text = "";
            txtBSDescription.Text = "";
            chkActive.Checked = true;
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";

        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    protected void btnBSSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            if (Hdn_Fld.Value == "")
            {
                ViewState["mode"] = "A";
            }
            else
            {
                ViewState["mode"] = "U";
            }

            string Mode = ViewState["mode"].ToString();

            int ID = 0;

            ID = ClsBSMaster.SaveBusnessSegmentMaster(Hdn_Fld.Value, txtBSCode.Text.ToString(), txtBusinessSegmentName.Text.ToString(), txtBSDescription.Text.ToString(), Mode, chkActive.Checked.ToString());

            if (ID == 2)
            {
                //string message = "alert('Record Saved Successfully..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowSuccess("Record Saved Successfully..");
                LoadBusinessSegmentMaster();
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                btnaddhide.Style["display"] = "";
                Hdn_Fld.Value = "";
            }

            else if (ID == 3)
            {
                //string message = "alert('Code already exist..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowError("Code already exist..");

            }
            else if (ID == 4)
            {
                //string message = "alert('Name already exist..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowError("Name already exist..", 80, 400);
            }
        }


        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void btnBSCancel_Click(object sender, EventArgs e)
    {
        try
        {
            txtBSCode.Text = "";
            txtBusinessSegmentName.Text = "";
            txtBSDescription.Text = "";
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            LoadBusinessSegmentMaster();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

    }


    protected void DeleteRecord(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            if (this.HdnPredined.Value.Trim() == "Y")
            {
                //MessageBox1.ShowInfo("Predefined Country can't be deleted");
                e.Record["Error"] = "Predefined Business Segment can't be deleted. ";
            }
            else
            {
                if (e.Record["BSID"] != "")
                {
                    int ID = 0;
                    ID = ClsBSMaster.DeleteBusnessSegmentMaster(e.Record["BSID"].ToString());
                    if (ID > 0)
                    {
                        e.Record["Error"] = "Record Deleted Successfully. ";
                        LoadBusinessSegmentMaster();
                    }
                    else
                    {
                        e.Record["Error"] = "Error On Deleting. ";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void CV_BSCode_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            String str = args.Value.ToString();
            if (str.Length < 2)
                args.IsValid = false;
            else
                args.IsValid = true;
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    protected void btngridsave_Click(object sender, EventArgs e)
    {

        try
        {


            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";
            pnlBS.Style["display"] = "";
            Bindgrid();
            //LoadPOProductItembyid(hdnPid.Value);

        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    public void Bindgrid()
    {
        String[] PrimaryID = this.ddlDivision.SelectedValue.Trim().Split('~');
        string ID = PrimaryID[1].Trim();
        gvPBMap.DataSource = clsPBMaster.BindPBGrid(ID, ddlCategory.SelectedValue);
        gvPBMap.DataBind();

    }
    public void LoadDivision()
    {
        try
        {
            ddlDivision.Items.Clear();
            ddlDivision.Items.Insert(0, new ListItem("-Select-", "0"));
            ddlDivision.DataSource = ClsProduct.BindPrimaryitemtype();
            ddlDivision.DataTextField = "ITEMDESC";
            ddlDivision.DataValueField = "ID";
            ddlDivision.DataBind();
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    public void LoadCategory()
    {
        try
        {
            String[] PrimaryID = this.ddlDivision.SelectedValue.Trim().Split('~');
            string ID = PrimaryID[1].Trim();
            ddlCategory.Items.Clear();
            ddlCategory.Items.Insert(0, new ListItem("-Select-", "0"));
            ddlCategory.DataSource = ClsProduct.BindSubitemtype(ID);
            ddlCategory.DataTextField = "SUBITEMDESC";
            ddlCategory.DataValueField = "SUBTYPEID";
            ddlCategory.DataBind();
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    //==============Added bym Sourav Mukherjee on 19/04/2016=================//

    # region OpenProductMapping
    protected void btnProductMapping_Click(object sender, EventArgs e)
    {
        ClsBusniessProductMap clsPBMaster = new ClsBusniessProductMap();
        this.CreateProductDataTable();
        //this.pnlDepotMapping.Style["display"] = "none";
        //this.pnlProductMapping.Style["display"] = "";
        this.pnlBS.Style["display"] = "";
        this.pnlAdd.Style["display"] = "none";
        this.pnlDisplay.Style["display"] = "none";
        this.btnaddhide.Style["display"] = "none";
        this.gvProductadd.ClearPreviousDataSource();
        this.gvProductadd.DataSource = null;
        this.gvProductadd.DataBind();
        this.gvPBMap.ClearPreviousDataSource();
        this.gvPBMap.DataSource = null;
        this.gvPBMap.DataBind();
        this.ddlDivision.SelectedValue = "0";
        this.ddlCategory.SelectedValue = "0";
        DataTable dtProductEdit = (DataTable)HttpContext.Current.Session["PRODUCTSEGMENTMAPPING"];
        DataTable dtproduct = new DataTable();
        //string CustomerID = Convert.ToString(Hdn_Fld.Value).Trim();
        dtproduct = clsPBMaster.EditProductByID(Hdn_Fld.Value);
        if (dtproduct.Rows.Count > 0)
        {

            for (int i = 0; i < dtproduct.Rows.Count; i++)
            {
                DataRow dr = dtProductEdit.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["PRODUCTID"] = Convert.ToString(dtproduct.Rows[i]["PRODUCTID"]).Trim();
                dr["PRODUCTNAME"] = Convert.ToString(dtproduct.Rows[i]["PRODUCTNAME"]).Trim();
                dr["BUSNESSSEGMENTID"] = Convert.ToString(dtproduct.Rows[i]["BUSNESSSEGMENTID"]).Trim();
                dr["BUSINESSSEGMENTNAME"] = Convert.ToString(dtproduct.Rows[i]["BUSINESSSEGMENTNAME"]).Trim();
                dtProductEdit.Rows.Add(dr);
                dtProductEdit.AcceptChanges();
            }

            HttpContext.Current.Session["PRODUCTGROUP"] = dtProductEdit;
            gvProductadd.DataSource = dtProductEdit;
            gvProductadd.DataBind();
        }
    }
    #endregion

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

    #region CreateProductDataTable
    public DataTable CreateProductDataTable()
    {
        DataTable dtProduct = new DataTable();

        dtProduct.Columns.Add("GUID", typeof(string));
        dtProduct.Columns.Add("BUSNESSSEGMENTID", typeof(string));
        dtProduct.Columns.Add("BUSINESSSEGMENTNAME", typeof(string));
        dtProduct.Columns.Add("PRODUCTID", typeof(string));
        dtProduct.Columns.Add("PRODUCTNAME", typeof(string));
        HttpContext.Current.Session["PRODUCTSEGMENTMAPPING"] = dtProduct;

        return dtProduct;
    }
    #endregion

    #region Add
    protected void btnproductadd_Click(object sender, EventArgs e)
    {
        try
        {
            int Count = 0;
            bool FLAG = false;
            string ProductID = string.Empty;
            int state = 0;
            string ProductName = string.Empty;
            DataTable dtProduct = new DataTable();

            if (HttpContext.Current.Session["PRODUCTSEGMENTMAPPING"] == null)
            {

                dtProduct.Columns.Add("GUID", typeof(string));
                dtProduct.Columns.Add("BUSNESSSEGMENTID", typeof(string));
                dtProduct.Columns.Add("BUSINESSSEGMENTNAME", typeof(string));
                dtProduct.Columns.Add("PRODUCTID", typeof(string));
                dtProduct.Columns.Add("PRODUCTNAME", typeof(string));
                HttpContext.Current.Session["PRODUCTSEGMENTMAPPING"] = dtProduct;
            }
            else
            {

                dtProduct = (DataTable)HttpContext.Current.Session["PRODUCTSEGMENTMAPPING"];

            }
            if (dtProduct.Rows.Count == 0)
            {
                if (gvPBMap.SelectedRecords != null)
                {
                    foreach (Hashtable row in gvPBMap.SelectedRecords)
                    {
                        Count = Count + 1;
                        DataRow dr = dtProduct.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["BUSNESSSEGMENTID"] = Convert.ToString(this.Hdn_Fld.Value).Trim();
                        dr["BUSINESSSEGMENTNAME"] = Convert.ToString(this.txtBS.Text);
                        dr["PRODUCTID"] = Convert.ToString(row["ID"]);
                        dr["PRODUCTNAME"] = Convert.ToString(row["NAME"]);
                        dtProduct.Rows.Add(dr);
                        dtProduct.AcceptChanges();
                    }
                }


                if (Count == 0)
                {

                    MessageBox1.ShowInfo("Please select atleast 1 Product");
                }
                else
                {
                    this.ddlDivision.SelectedValue = "0";
                    this.ddlCategory.SelectedValue = "0";
                    this.gvPBMap.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
                    this.gvPBMap.ClearPreviousDataSource();
                    this.gvPBMap.DataSource = null;
                    this.gvPBMap.DataBind();
                }
            }

            else if (dtProduct.Rows.Count > 0)
            {
                FLAG = false;
                ProductName = "";
                if (gvPBMap.SelectedRecords != null)
                {
                    foreach (Hashtable row in gvPBMap.SelectedRecords)
                    {
                        for (int k = 0; k < gvProductadd.Rows.Count; k++)
                        {
                            ProductID = gvProductadd.Rows[k].Cells[4].Text.ToString();
                            Count = Count + 1;
                            if (ProductID == Convert.ToString(row["ID"]))
                            {
                                FLAG = true;
                                state = 2;
                                ProductName = ProductName + "," + Convert.ToString(row["NAME"]);
                                break;
                            }
                        }
                        if (FLAG == false)
                        {
                            DataRow dr = dtProduct.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["BUSNESSSEGMENTID"] = Convert.ToString(this.Hdn_Fld.Value).Trim();
                            dr["BUSINESSSEGMENTNAME"] = Convert.ToString(this.txtBS.Text);
                            dr["PRODUCTID"] = Convert.ToString(row["ID"]);
                            dr["PRODUCTNAME"] = Convert.ToString(row["NAME"]);
                            dtProduct.Rows.Add(dr);
                            dtProduct.AcceptChanges();
                        }
                    }
                }

                gvPBMap.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
                gvPBMap.ClearPreviousDataSource();
                gvPBMap.DataSource = null;
                gvPBMap.DataBind();
                this.ddlDivision.SelectedValue = "0";
                this.ddlCategory.SelectedValue = "0";
                if (Count == 0)
                {
                    //MessageBox1.ShowInfo("Please select atleast 1 Product");
                }
                else
                {
                    if (FLAG == true)
                    {
                        if (state == 2)
                        {
                            ProductName = ProductName.Substring(0, ProductName.Length - 1);
                            MessageBox1.ShowSuccess("" + ProductName + " already exists", 60, 700);
                        }

                    }
                    else
                    {
                        this.ddlDivision.SelectedValue = "0";
                        this.ddlCategory.SelectedValue = "0";
                        this.gvPBMap.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
                        this.gvPBMap.ClearPreviousDataSource();
                        this.gvPBMap.DataSource = null;
                        this.gvPBMap.DataBind();
                    }
                }

            }

            if (dtProduct.Rows.Count > 0)
            {
                gvProductadd.DataSource = dtProduct;

                gvProductadd.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

    }
    #endregion

    #region Submit Mapping
    protected void btnPBMapSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsBusniessProductMap clsPBMaster = new ClsBusniessProductMap();
            string XML = string.Empty;
            DataTable dt = new DataTable();
            dt = (DataTable)HttpContext.Current.Session["PRODUCTSEGMENTMAPPING"];
            XML = ConvertDatatableToXML(dt);
            int ID = 0;

            ID = clsPBMaster.SavePBMap(Hdn_Fld.Value, XML);
            if (dt.Rows.Count > 0)
            {
                if (ID > 0)
                {

                    MessageBox1.ShowSuccess("Record Saved Successfully..");
                    pnlAdd.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "";
                    btnaddhide.Style["display"] = "";
                    pnlBS.Style["display"] = "none";

                    gvProductadd.DataSource = null;
                    gvProductadd.DataBind();

                    this.ddlDivision.SelectedValue = "0";
                    this.ddlCategory.SelectedValue = "0";
                    this.gvPBMap.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
                    this.gvPBMap.ClearPreviousDataSource();
                    this.gvPBMap.DataSource = null;
                    this.gvPBMap.DataBind();

                }

                else
                {
                    MessageBox1.ShowError("Error on saving");
                }
            }


            MessageBox1.ShowSuccess("Record Saved Successfully..");
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            pnlBS.Style["display"] = "none";
            //pnlDepotMapping.Style["display"] = "none";

            gvProductadd.DataSource = null;
            gvProductadd.DataBind();

            this.ddlDivision.SelectedValue = "0";
            this.ddlCategory.SelectedValue = "0";
            this.gvPBMap.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
            this.gvPBMap.ClearPreviousDataSource();
            this.gvPBMap.DataSource = null;
            this.gvPBMap.DataBind();


        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }

    }
    #endregion

    #region btnproductdelete_Click
    protected void btnproductdelete_Click(object sender, EventArgs e)
    {
        try
        {
            string GUID = Convert.ToString(hdnproductdelete.Value);
            DataTable dt = new DataTable();
            dt = (DataTable)Session["PRODUCTSEGMENTMAPPING"];

            DataRow[] drr = dt.Select("GUID='" + GUID + "'");
            for (int i = 0; i < drr.Length; i++)
            {
                drr[i].Delete();
                dt.AcceptChanges();
                //MessageBox1.ShowSuccess("Deleted Successfully");
            }
            this.gvProductadd.DataSource = dt;
            this.gvProductadd.DataBind();

            //Session["dtCity"] = null;
            //this.gvproductselect.ClearPreviousDataSource();
            //this.gvproductselect.DataSource = null;
            //this.gvproductselect.DataBind();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    //=======================================================================//

    protected void btnPBMapCancel_Click(object sender, EventArgs e)
    {
        pnlAdd.Style["display"] = "none";
        pnlBS.Style["display"] = "none";
        pnlDisplay.Style["display"] = "";
        btnaddhide.Style["display"] = "";
        ddlDivision.SelectedValue = "0";
        ddlCategory.SelectedValue = "0";
        dt.Clear();
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        //LoadPOProductItembyid(hdnPid.Value);//now
        Bindgrid();

    }

    public void LoadPOProductItembyid(string id)
    {
        try
        {
            dt = clsPBMaster.BindPSNameGridbyid(id);

        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void gvBusinessSegment_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[4] as GridDataControlFieldCell;
                string status = cell.Text;

                if (status == "Active")
                {
                    cell.ForeColor = Color.Green;
                }
                else
                {
                    cell.ForeColor = Color.Red;
                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCategory();
    }
}