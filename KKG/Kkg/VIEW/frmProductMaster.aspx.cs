using BAL;
using Obout.Grid;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmProductMaster : System.Web.UI.Page
{
    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["MENU"] == "Product Master")
                {
                    LoadProductMaster();
                    btnaddhide.Style["display"] = "";
                    pnlAdd.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "";
                    pnlBS.Style["display"] = "none";
                    excel.Style["display"] = "none";
                }
                else
                {
                    LoadProductMaster();
                    btnaddhide.Style["display"] = "none";
                    pnlAdd.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "";
                    pnlBS.Style["display"] = "none";
                    excel.Style["display"] = "";
                }

                this.pnlDistributor.Style["display"] = "none";
                this.LoadPackigsize();
                this.LoadSearchCategory();
                this.LoadPackigsize1();
                this.LoadDivision();
                this.LoadCategory();
                this.LoadNature();
                this.LoadUOM();
                this.LoadUOMNew();
                this.LoadFragrance();
                this.LoadBSGrid();
                this.LoadTPUMaster();
                this.LoadFactoryMaster();
                this.LoadDepoName();
                this.LoadBusinessSegment();
                HttpContext.Current.Session["PRODUCTGROUP"] = null;
                this.gvgroupadd.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
                this.gvgroupadd.ClearPreviousDataSource();
                this.gvgroupadd.DataSource = null;
                this.gvgroupadd.DataBind();
                CreateDataTable();
                this.LoadItemType();
                this.LoadDepotType();
                this.trDepotMap.Visible = false;
                this.trinvoice.Visible = false;
                this.LoadDepotGrid("NA", "NA", "A");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    public void LoadDepoName()
    {
        try
        {
            ClsProductMaster ClsProduct = new ClsProductMaster();
            ddlDeopt.Items.Clear();
            ddlDeopt.Items.Add(new ListItem("Select", "0"));
            ddlDeopt.AppendDataBoundItems = true;
            ddlDeopt.DataSource = ClsProduct.BindDepo();
            ddlDeopt.DataValueField = "BRID";
            ddlDeopt.DataTextField = "BRNAME";
            ddlDeopt.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    public void LoadStorelocation(string id)
    {
        try
        {
            String MODE = "ProductLocation";
           
            if (id == "")
            {
               
                ClsVendor_TPU ClsProduct = new ClsVendor_TPU();
                ddlstorelocation.Items.Clear();
                ddlstorelocation.Items.Add(new ListItem("Select", "0"));
                ddlstorelocation.AppendDataBoundItems = true;
                ddlstorelocation.DataSource = ClsProduct.BindGateEntry(MODE, "");
                ddlstorelocation.DataValueField = "ID";
                ddlstorelocation.DataTextField = "NAME";
                ddlstorelocation.DataBind();
            }
            else
            {
                DataTable DT = new DataTable();
                ClsVendor_TPU ClsProduct = new ClsVendor_TPU();
                DT = ClsProduct.BindGateEntry(MODE, id);
                ddlstorelocation.Items.Clear();
                ddlstorelocation.Items.Add(new ListItem("Select", "0"));
                ddlstorelocation.AppendDataBoundItems = true;
                ddlstorelocation.DataSource = DT;
                ddlstorelocation.DataValueField = "ID";
                ddlstorelocation.DataTextField = "NAME";
                ddlstorelocation.DataBind();
                if (DT.Rows.Count > 0)
                {
                    this.ddlstorelocation.SelectedValue = DT.Rows[0]["ID"].ToString();
                }
               
            }
           
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void LoadProductMaster()
    {
        try
        {
            if (Request.QueryString["MENU"] == "Product Master")
            {
                ClsProductMaster ClsProduct = new ClsProductMaster();
                gvProduct.ClearPreviousDataSource();
                gvProduct.DataSource = ClsProduct.BindProductMasterGrid();

                gvProduct.Columns[0].Visible = false;

                gvProduct.DataBind();
                //gvProduct.AllowGrouping = true;
                //gvProduct.GroupBy = "DIVNAME";

            }
            else
            {
                ClsProductMaster ClsProduct = new ClsProductMaster();
                gvProduct.ClearPreviousDataSource();
                gvProduct.DataSource = ClsProduct.BindProductMasterGrid();

                gvProduct.Columns[1].Visible = false;
                gvProduct.Columns[10].Visible = false;
                gvProduct.Columns[11].Visible = false;
                gvProduct.Columns[12].Visible = false;
                gvProduct.Columns[13].Visible = false;
                gvProduct.Columns[14].Visible = false;
                gvProduct.Columns[15].Visible = false;
                gvProduct.Columns[16].Visible = false;
                gvProduct.Columns[17].Visible = false;
                gvProduct.Columns[18].Visible = false;
                gvProduct.DataBind();
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void LoadItemType()
    {
        try
        {
            ClsProductMaster ClsProduct = new ClsProductMaster();
            DataTable dt = ClsProduct.Binditentype();
            if (dt.Rows.Count > 0)
            {
                ddlitemtype.Items.Clear();
                ddlitemtype.Items.Insert(0, new ListItem("-Select-", "0"));
                ddlitemtype.AppendDataBoundItems = true;
                ddlitemtype.DataSource = dt;
                ddlitemtype.DataTextField = "ITNAME";
                ddlitemtype.DataValueField = "ITID";
                ddlitemtype.DataBind();

            }
            else
            {
                ddlitemtype.Items.Clear();
                ddlitemtype.Items.Insert(0, new ListItem("-Select-", "0"));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void LoadTPUMaster()
    {
        try
        {
            ClsProductMaster ClsProduct = new ClsProductMaster();
            gvPTPUMap.DataSource = ClsProduct.BindTPUMasterGrid();
            gvPTPUMap.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void LoadFactoryMaster()
    {
        try
        {
            ClsProductMaster ClsProduct = new ClsProductMaster();
            gvFactory.DataSource = ClsProduct.BindFactoryMasterGrid();
            gvFactory.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void LoadDivision()
    {
        try
        {
            ClsProductMaster ClsProduct = new ClsProductMaster();
            ddlDivision.Items.Clear();
            ddlDivision.Items.Insert(0, new ListItem("-Select-", "0"));
            ddlDivision.DataSource = ClsProduct.BindDivision();
            ddlDivision.DataTextField = "DIVNAME";
            ddlDivision.DataValueField = "DIVID";
            ddlDivision.DataBind();


            ddlsearchbrand.Items.Clear();
            ddlsearchbrand.Items.Insert(0, new ListItem("ALL", "0"));

            ddlsearchbrand.DataSource = ClsProduct.BindDivision();
            ddlsearchbrand.DataTextField = "DIVNAME";
            ddlsearchbrand.DataValueField = "DIVID";
            ddlsearchbrand.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void BindGridByBrand()
    {
        try
        {
            ClsProductMaster ClsProduct = new ClsProductMaster();
            //DataTable dt = new DataTable();
            //dt = ClsProduct.bindprodctgrd(ddlsearchbrand.SelectedValue.Trim());
            if (this.ddlsearchbrand.SelectedValue.Trim() == "0" && this.ddlsearchCategory.SelectedValue.Trim() == "0")
            {

                this.gvProduct.ClearPreviousDataSource();
                this.gvProduct.DataSource = ClsProduct.BindProductMasterGrid();
                this.gvProduct.DataBind();

            }
            else if (this.ddlsearchbrand.SelectedValue.Trim() == "0" && this.ddlsearchCategory.SelectedValue.Trim() != "0")
            {
                this.gvProduct.ClearPreviousDataSource();
                this.gvProduct.DataSource = ClsProduct.bindprodctgrdcat(this.ddlsearchCategory.SelectedValue.Trim());
                this.gvProduct.DataBind();
            }
            else if (this.ddlsearchbrand.SelectedValue.Trim() != "0" && this.ddlsearchCategory.SelectedValue.Trim() == "0")
            {
                this.gvProduct.ClearPreviousDataSource();
                this.gvProduct.DataSource = ClsProduct.bindprodctgrd(this.ddlsearchbrand.SelectedValue.Trim()); ;
                this.gvProduct.DataBind();
            }
            else
            {
                this.gvProduct.ClearPreviousDataSource();
                this.gvProduct.DataSource = ClsProduct.BindProductMasterGridbycategory(this.ddlsearchbrand.SelectedValue.Trim(), this.ddlsearchCategory.SelectedValue.Trim());
                this.gvProduct.DataBind();
            }



        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void BindGridByCategory()
    {
        try
        {
            ClsProductMaster ClsProduct = new ClsProductMaster();

            if (this.ddlsearchbrand.SelectedValue.Trim() == "0" && this.ddlsearchCategory.SelectedValue.Trim() == "0")
            {
                this.gvProduct.ClearPreviousDataSource();
                this.gvProduct.DataSource = ClsProduct.BindProductMasterGrid();
                this.gvProduct.DataBind();
            }
            else if (this.ddlsearchbrand.SelectedValue != "0" && this.ddlsearchCategory.SelectedValue == "0")
            {
                this.gvProduct.ClearPreviousDataSource();
                this.gvProduct.DataSource = ClsProduct.bindprodctgrd(this.ddlsearchbrand.SelectedValue.Trim());
                this.gvProduct.DataBind();
            }
            else if (this.ddlsearchbrand.SelectedValue.Trim() == "0" && this.ddlsearchCategory.SelectedValue.Trim() != "0")
            {
                this.gvProduct.ClearPreviousDataSource();
                this.gvProduct.DataSource = ClsProduct.bindprodctgrdcat(this.ddlsearchCategory.SelectedValue.Trim());
                this.gvProduct.DataBind();
            }
            else
            {
                this.gvProduct.ClearPreviousDataSource();
                this.gvProduct.DataSource = ClsProduct.BindProductMasterGridbycategory(this.ddlsearchbrand.SelectedValue.Trim(), this.ddlsearchCategory.SelectedValue.Trim());
                this.gvProduct.DataBind();
            }




            //gvProduct.AllowGrouping = true;
            //gvProduct.GroupBy = "DIVNAME";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlsearchbrand_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridByBrand();
    }

    protected void ddlsearchCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridByCategory();
    }

    public void LoadSearchCategory()
    {
        try
        {
            ClsProductMaster ClsProduct = new ClsProductMaster();
            ddlsearchCategory.Items.Clear();
            ddlsearchCategory.Items.Insert(0, new ListItem("ALL", "0"));
            ddlsearchCategory.DataSource = ClsProduct.BindCategory();
            ddlsearchCategory.DataTextField = "CATNAME";
            ddlsearchCategory.DataValueField = "CATID";
            ddlsearchCategory.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void LoadCategory()
    {
        try
        {
            ClsProductMaster ClsProduct = new ClsProductMaster();
            ddlCategory.Items.Clear();
            ddlCategory.Items.Insert(0, new ListItem("-Select-", "0"));
            ddlCategory.DataSource = ClsProduct.BindCategory();
            ddlCategory.DataTextField = "CATNAME";
            ddlCategory.DataValueField = "CATID";
            ddlCategory.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void LoadNature()
    {
        try
        {
            DataTable dt = new DataTable();
            ClsProductMaster ClsProduct = new ClsProductMaster();
            dt= ClsProduct.BindNatureonlyfg();
            ddlNature.Items.Clear();
            ddlNature.Items.Insert(0, new ListItem("-Select-", "0"));
            ddlNature.DataSource = dt;
            ddlNature.DataTextField = "NOPNAME";
            ddlNature.DataValueField = "NOPID";
            ddlNature.DataBind();
            if(dt.Rows.Count==1)
            {
                this.ddlNature.SelectedValue = dt.Rows[0]["NOPID"].ToString();
            }
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void LoadUOM()
    {
        try
        {
            ClsProductMaster ClsProduct = new ClsProductMaster();
            ddlUOM.Items.Clear();
            ddlUOM.Items.Insert(0, new ListItem("-Select-", "0"));
            ddlUOM.DataSource = ClsProduct.BindUOM();
            ddlUOM.DataTextField = "UOMDESCRIPTION";
            ddlUOM.DataValueField = "UOMID";
            ddlUOM.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void LoadFragrance()
    {
        try
        {
            ClsProductMaster ClsProduct = new ClsProductMaster();
            ddlFragrance.Items.Clear();
            ddlFragrance.Items.Insert(0, new ListItem("-Select-", "0"));
            ddlFragrance.DataSource = ClsProduct.BindFragrance();
            ddlFragrance.DataTextField = "FRGNAME";
            ddlFragrance.DataValueField = "FRGID";
            ddlFragrance.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void GetProductName()
    {
        try
        {
            if (ddlDivision.SelectedValue.ToString() != "0" && ddlCategory.SelectedValue.ToString() == "0" && ddlFragrance.SelectedValue.ToString() == "0" && ddlUOM.SelectedValue.ToString() == "0" && txtUValue.Text.ToString() == "")
            {
                txtName.Text = "";
                txtCode.Text = "";
                this.lblPRODUCTOWNER.Text = "";
                string CATNAME = ddlCategory.SelectedItem.ToString();
                String[] CAT = CATNAME.Split('(');
                string NAME = CAT[0].Trim();

                txtName.Text = ddlDivision.SelectedItem.ToString();
                txtName.Text = txtName.Text.Replace("-Select-", " ");
                ClsProductMaster ClsProduct = new ClsProductMaster();
                string ProductCode = ClsProduct.ProductCode(this.ddlDivision.SelectedValue, NAME.Trim(), this.ddlFragrance.SelectedValue, this.ddlNature.SelectedItem.ToString(), this.ddlitemtype.SelectedValue.ToString(), this.txtUValue.Text);
                txtCode.Text = ProductCode;

                if (ddlDivision.SelectedValue.ToString() == "107CBAAA-10B4-406B-B5B9-4C48A77330D9" || ddlDivision.SelectedValue.ToString() == "27363B0F-3902-4775-93EF-222DF3E70B69")
                {
                    this.lblPRODUCTOWNER.Text = "R";
                }
                else if (ddlDivision.SelectedValue.ToString() == "E6B2F631-7237-4B97-BC33-B6FCF0B9075B")
                {
                    this.lblPRODUCTOWNER.Text = " ";
                }
                else
                {
                    this.lblPRODUCTOWNER.Text = "M";
                }
            }
            else if (ddlDivision.SelectedValue.ToString() != "0" && ddlCategory.SelectedValue.ToString() != "0" && ddlFragrance.SelectedValue.ToString() == "0" && ddlUOM.SelectedValue.ToString() == "0" && txtUValue.Text.ToString() == "")
            {
                txtName.Text = "";
                txtCode.Text = "";
                this.lblPRODUCTOWNER.Text = "";
                string CATNAME = ddlCategory.SelectedItem.ToString();
                String[] CAT = CATNAME.Split('(');
                string NAME = CAT[0].Trim();

                txtName.Text = (ddlDivision.SelectedItem.ToString() + " " + NAME);
                txtName.Text = txtName.Text.Replace("-Select-", " ");
                ClsProductMaster ClsProduct = new ClsProductMaster();
                string ProductCode = ClsProduct.ProductCode(this.ddlDivision.SelectedValue, this.ddlCategory.SelectedValue, this.ddlFragrance.SelectedValue, this.ddlNature.SelectedItem.ToString(), this.ddlitemtype.SelectedValue.ToString(), this.txtUValue.Text);
                txtCode.Text = ProductCode;
                if (ddlDivision.SelectedValue.ToString() == "107CBAAA-10B4-406B-B5B9-4C48A77330D9" || ddlDivision.SelectedValue.ToString() == "27363B0F-3902-4775-93EF-222DF3E70B69")
                {
                    this.lblPRODUCTOWNER.Text = "R";
                }
                else if (ddlDivision.SelectedValue.ToString() == "E6B2F631-7237-4B97-BC33-B6FCF0B9075B")
                {
                    this.lblPRODUCTOWNER.Text = " ";
                }
                else
                {
                    this.lblPRODUCTOWNER.Text = "M";
                }
            }
            else if (ddlDivision.SelectedValue.ToString() != "0" && ddlCategory.SelectedValue.ToString() != "0" && ddlFragrance.SelectedValue.ToString() != "0" && ddlUOM.SelectedValue.ToString() == "0" && txtUValue.Text.ToString() == "")
            {
                txtName.Text = "";
                txtCode.Text = "";
                this.lblPRODUCTOWNER.Text = "";
                string CATNAME = ddlCategory.SelectedItem.ToString();
                String[] CAT = CATNAME.Split('(');
                string NAME = CAT[0].Trim();


                txtName.Text = (ddlDivision.SelectedItem.ToString() + " " + NAME.Trim() + " " + ddlFragrance.SelectedItem.ToString());
                txtName.Text = txtName.Text.Replace("-Select-", " ");
                ClsProductMaster ClsProduct = new ClsProductMaster();
                string ProductCode = ClsProduct.ProductCode(this.ddlDivision.SelectedValue, this.ddlCategory.SelectedValue, this.ddlFragrance.SelectedValue, this.ddlNature.SelectedItem.ToString(), this.ddlitemtype.SelectedValue.ToString(), this.txtUValue.Text);
                txtCode.Text = ProductCode;
                if (ddlDivision.SelectedValue.ToString() == "107CBAAA-10B4-406B-B5B9-4C48A77330D9" || ddlDivision.SelectedValue.ToString() == "27363B0F-3902-4775-93EF-222DF3E70B69")
                {
                    this.lblPRODUCTOWNER.Text = "R";
                }
                else if (ddlDivision.SelectedValue.ToString() == "E6B2F631-7237-4B97-BC33-B6FCF0B9075B")
                {
                    this.lblPRODUCTOWNER.Text = " ";
                }
                else
                {
                    this.lblPRODUCTOWNER.Text = "M";
                }
            }
            else if (ddlDivision.SelectedValue.ToString() != "0" && ddlCategory.SelectedValue.ToString() != "0" && ddlFragrance.SelectedValue.ToString() != "0" && ddlUOM.SelectedValue.ToString() != "0" && txtUValue.Text.ToString() == "")
            {
                txtName.Text = "";
                txtCode.Text = "";
                this.lblPRODUCTOWNER.Text = "";
                string CATNAME = ddlCategory.SelectedItem.ToString();
                String[] CAT = CATNAME.Split('(');
                string NAME = CAT[0].Trim();

                txtName.Text = (ddlDivision.SelectedItem.ToString() + " " + NAME.Trim() + " " + ddlFragrance.SelectedItem.ToString() + " " + txtUValue.Text.ToString() + " " + ddlUOM.SelectedItem.ToString());
                txtName.Text = txtName.Text.Replace("-Select-", " ");
                ClsProductMaster ClsProduct = new ClsProductMaster();
                string ProductCode = ClsProduct.ProductCode(this.ddlDivision.SelectedValue, this.ddlCategory.SelectedValue, this.ddlFragrance.SelectedValue, this.ddlNature.SelectedItem.ToString(), this.ddlitemtype.SelectedValue.ToString(), this.txtUValue.Text);
                txtCode.Text = ProductCode;
                if (ddlDivision.SelectedValue.ToString() == "107CBAAA-10B4-406B-B5B9-4C48A77330D9" || ddlDivision.SelectedValue.ToString() == "27363B0F-3902-4775-93EF-222DF3E70B69")
                {
                    this.lblPRODUCTOWNER.Text = "R";
                }
                else if (ddlDivision.SelectedValue.ToString() == "E6B2F631-7237-4B97-BC33-B6FCF0B9075B")
                {
                    this.lblPRODUCTOWNER.Text = " ";
                }
                else
                {
                    this.lblPRODUCTOWNER.Text = "M";
                }
            }
            else
            {
                txtName.Text = "";
                txtCode.Text = "";
                this.lblPRODUCTOWNER.Text = "";
                string CATNAME = ddlCategory.SelectedItem.ToString();
                String[] CAT = CATNAME.Split('(');
                string NAME = CAT[0].Trim();

                txtName.Text = (ddlDivision.SelectedItem.ToString() + " " + NAME.Trim() + " " + ddlFragrance.SelectedItem.ToString() + " " + txtUValue.Text.ToString() + " " + ddlUOM.SelectedItem.ToString());
                txtName.Text = txtName.Text.Replace("-Select-", " ");
                ClsProductMaster ClsProduct = new ClsProductMaster();
                string ProductCode = ClsProduct.ProductCode(this.ddlDivision.SelectedValue, this.ddlCategory.SelectedValue, this.ddlFragrance.SelectedValue, this.ddlNature.SelectedItem.ToString(), this.ddlitemtype.SelectedValue.ToString(), this.txtUValue.Text);
                txtCode.Text = ProductCode;
                if (ddlDivision.SelectedValue.ToString() == "107CBAAA-10B4-406B-B5B9-4C48A77330D9" || ddlDivision.SelectedValue.ToString() == "27363B0F-3902-4775-93EF-222DF3E70B69")
                {
                    this.lblPRODUCTOWNER.Text = "R";
                }
                else if (ddlDivision.SelectedValue.ToString() == "E6B2F631-7237-4B97-BC33-B6FCF0B9075B")
                {
                    this.lblPRODUCTOWNER.Text = " ";
                }
                else
                {
                    this.lblPRODUCTOWNER.Text = "M";
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void LoadBSGrid()
    {
        try
        {
            ClsPBMap clsPBMaster = new ClsPBMap();
            gvPBMap.DataSource = clsPBMaster.BindBSNameGrid();
            gvPBMap.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void LoadPOProductItembyid(string id)
    {
        try
        {
            ClsPBMap clsPBMaster = new ClsPBMap();
            dt = clsPBMaster.BindBSNameGridbyid(id);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void LoadTPUbyid(string id)
    {
        try
        {
            ClsProductMaster ClsProduct = new ClsProductMaster();
            dt = ClsProduct.BindTPUMasterGridbyid(id);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void LoadPackigsize()
    {
        try
        {
            ClsProductPackingSizeMap clspacking = new ClsProductPackingSizeMap();
            ddlPackigsize.Items.Clear();
            ddlPackigsize.Items.Insert(0, new ListItem("-Select-", "0"));
            ddlPackigsize.DataSource = clspacking.BindPackigsize();
            ddlPackigsize.DataTextField = "PSNAME";
            ddlPackigsize.DataValueField = "PSID";
            ddlPackigsize.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void LoadUOMNew()
    {
        try
        {
            ClsProductPackingSizeMap clspacking = new ClsProductPackingSizeMap();
            this.ddlUOMPacksize.Items.Clear();
            this.ddlUOMPacksize.Items.Insert(0, new ListItem("-Select-", "0"));
            this.ddlUOMPacksize.DataSource = clspacking.BindUOM();
            this.ddlUOMPacksize.DataTextField = "UOMNAME";
            this.ddlUOMPacksize.DataValueField = "UOMID";
            this.ddlUOMPacksize.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void LoadPackigsize1()
    {
        try
        {
            ClsProductPackingSizeMap clspacking = new ClsProductPackingSizeMap();
            ddlPackigsize1.Items.Clear();
            ddlPackigsize1.Items.Insert(0, new ListItem("-Select-", "0"));
            ddlPackigsize1.DataSource = clspacking.BindPackigsizewithpcs();
            ddlPackigsize1.DataTextField = "PSNAME";
            ddlPackigsize1.DataValueField = "PSID";
            ddlPackigsize1.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void LoadFactoryid(string id)
    {
        try
        {
            ClsProductMaster ClsProduct = new ClsProductMaster();
            dt = ClsProduct.BindFactoryMasterGridbyid(id);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            this.txtName.Text = "";
            this.txtname2.Text = "";
            this.txtCode.Text = "";
            this.txtUValue.Text = "";
            this.txtMRP.Text = "";
            this.txtSingDollar.Text = "";
            this.txtNepRupee.Text = "";
            this.txtBangTaka.Text = "";
            this.txtShortName.Text = "";
            this.txtindexno.Text = "";            
            this.ddlDivision.SelectedValue = "0";
            this.ddlCategory.SelectedValue = "0";
            //this.ddlNature.SelectedValue = "0";
            this.ddlUOM.SelectedValue = "0";
            this.ddlFragrance.SelectedValue = "0";
            this.txtAssessablepercentage.Text = "";
            this.txtBestbefore.Text = "";
            this.txtMinstooklevel.Text = "";
            this.chkRefundable.Checked = false;
            this.ChkActive.Checked = true;
            this.chkservice.Checked = false;
            this.Hdn_Fld.Value = "";
            this.pnlAdd.Style["display"] = "";
            this.divproductinfo.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.btnaddhide.Style["display"] = "none";
            this.pnlBS.Style["display"] = "none";
            this.trProductsubmit.Style["display"] = "";
            this.txtName.Enabled = false;
            this.ddlDivision.Enabled = true;
            this.ddlCategory.Enabled = true;
            this.ddlNature.Enabled = true;
            this.ddlUOM.Enabled = true;
            this.ddlFragrance.Enabled = true;
            this.txtUValue.Enabled = true;
            this.btnRefresh.Visible = true;
            this.btnRefreshCategory.Visible = true;
            this.btnRefreshNature.Visible = true;
            this.btnRefreshUOM.Visible = true;
            this.btnRefreshFragrance.Visible = true;
            this.ddlitemtype.SelectedValue = "0";
            this.ddlsearchCategory.SelectedValue = "0";
            this.ddlsearchbrand.SelectedValue = "0";                      
            this.trDepotMap.Visible = false;
            this.trinvoice.Visible = false;
            this.ddlBillingDepotType.SelectedValue = "-1";

            txtShortName.Enabled = true;
            ddlDivision.Enabled = true;
            ddlCategory.Enabled = true;

            txtname2.Enabled = true;
            ddlitemtype.Enabled = true;
            txtBestbefore.Enabled = true;
            txtAssessablepercentage.Enabled = true;
            ChkActive.Enabled = true;

            LoadStorelocation("");

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }



    #region Export to Excel
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            ClsProductMaster clsproduct = new ClsProductMaster();
            DataTable dt = clsproduct.DownloadProductList();
            Response.ClearContent();
            Response.Buffer = true;
            string attachment = "attachment; filename=Product_List.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";

                }
                Response.Write("\n");
            }

            Response.End();

        }

        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int ID = 0;
            string Mode = "";
            string Active = string.Empty;
            string service = string.Empty;
            string PowerSKU = string.Empty;
            string guid = string.Empty;
            string ip_address = Request.UserHostAddress.ToString().Trim();
            string XML = string.Empty;

            if (this.ddlBillingDepotType.SelectedValue.Trim() == "2")
            {
                GridView gvunlock = gvBrandTypeMap;
                string gvchkbox = "";
                string gvbrandid = "";
                string gvbrandname = "";
                int count = 0;
                gvchkbox = "chkSelect";
                gvbrandid = "lblDEPOTID";
                gvbrandname = "lblDEPOTNAME";
                foreach (GridViewRow gvrow in gvunlock.Rows)
                {
                    CheckBox chkBx = (CheckBox)gvrow.FindControl("" + gvchkbox + "");
                    Label lblBRANDID = (Label)gvrow.FindControl("" + gvbrandid + "");
                    Label lblBRANDNAME = (Label)gvrow.FindControl("" + gvbrandname + "");
                    if (chkBx.Checked)
                    {
                        count = count + 1;

                    }
                }
                if (count == 0)
                {
                    MessageBox1.ShowWarning("Plese Select atleast 1 depot...! ");
                    return;
                }
            }

            if (Hdn_Fld.Value == "")
            {
                Mode = "A";
                guid = Convert.ToString(Guid.NewGuid()).ToUpper();
            }
            else
            {
                Mode = "U";
            }
            if (this.ChkActive.Checked == true)
            {
                Active = "T";
            }
            else
            {
                Active = "F";
            }

            if (this.chkpowersku.Checked == true)
            {
                PowerSKU = "Y";
            }
            else
            {
                PowerSKU = "N";
            }

            if (txtAssessablepercentage.Text == "")
            {
                txtAssessablepercentage.Text = "0";
            }
            string CATNAME = ddlCategory.SelectedItem.ToString();
            String[] CAT = CATNAME.Split('(');
            string NAME = CAT[0].Trim();
            ClsVendor_TPU ClsVendor_TPU = new ClsVendor_TPU();
            string id = string.Empty;
            if (Hdn_Fld.Value.Trim()=="")
            {
                id = guid.Trim();
            }
            else
            {
                id = Hdn_Fld.Value.Trim();
            }
            /*nw add for service*/
            if (this.chkservice.Checked == true)
            {
                service = "T";
            }
            else
            {
                service = "F";
            }

            ClsProductMaster ClsProduct = new ClsProductMaster();
            ID = ClsProduct.SaveProductMaster(guid.Trim(), Hdn_Fld.Value.Trim(), txtCode.Text.ToString(), txtName.Text.ToString().Trim() + " " + txtname2.Text.ToString().Trim(),
                                              txtName.Text.ToString().Trim(), txtname2.Text.ToString().Trim(), ddlDivision.SelectedValue.ToString(),
                                              ddlDivision.SelectedItem.ToString(), ddlCategory.SelectedValue.ToString(), NAME.Trim(),
                                              ddlNature.SelectedValue.ToString(), ddlNature.SelectedItem.ToString(), ddlUOM.SelectedValue.ToString(),
                                              ddlUOM.SelectedItem.ToString(), txtUValue.Text.ToString(), ddlFragrance.SelectedValue.ToString(),
                                              ddlFragrance.SelectedItem.ToString(), Convert.ToDecimal(txtAssessablepercentage.Text.ToString()),
                                              Convert.ToInt32(txtBestbefore.Text.ToString()), Convert.ToDecimal(txtMinstooklevel.Text == "" ? "0" : txtMinstooklevel.Text),
                                              chkRefundable.Checked.ToString(), Convert.ToDecimal(txtMRP.Text.ToString()), Mode,
                                              Active, txtShortName.Text.Trim(), Convert.ToString(ddlitemtype.SelectedValue).Trim(),
                                              Convert.ToString(ddlitemtype.SelectedItem).Trim(), txtindexno.Text.Trim(), ip_address,
                                              PowerSKU, /*this.ddlBillingDepotType.SelectedValue.Trim(),*/"1", this.txtbarcode.Text.Trim(),
                                              this.lblPRODUCTOWNER.Text.Trim(), this.txtbrandcategory.Text.Trim(), service);
            
            if (ID == 2 || ID == 1)
            {
                string UPDATE = ClsVendor_TPU.BillStatusCheck("UPDATE", id, this.ddlstorelocation.SelectedValue.ToString());
                #region Product Depot Mapping
                //if (this.ddlDivision.SelectedValue.Trim() == "1AC1960B-5C21-4C67-93D4-D38D1B3FAEB2" || this.ddlDivision.SelectedValue.Trim() == "12E3F880-C465-4C0E-80B8-42E8DD05E51D" || this.ddlDivision.SelectedValue.Trim() == "1865C411-3F93-4F03-9FE5-154C93A4CB7C")/*HEAVENS GARDEN,SECRET TEMPTATION,WILD STONE*/
                //{
                #region Selected Depot
                /*if (this.ddlBillingDepotType.SelectedValue.Trim() == "2")
                {
                    int DepotMap = 0;
                    string ProductID = string.Empty;
                    if (Hdn_Fld.Value.ToString().Trim() != "")
                    {
                        ProductID = Hdn_Fld.Value.ToString().Trim();
                    }
                    else
                    {
                        ProductID = guid.Trim();
                    }
                    if (HttpContext.Current.Session["DEPOTMAPPING"] == null)
                    {
                        CreateDepotMappingDataTable();
                    }
                    DataTable DT = (DataTable)HttpContext.Current.Session["DEPOTMAPPING"];
                    GridView gvunlock = gvBrandTypeMap;
                    string gvchkbox = "";
                    string gvdepotid = "";
                    string gvdepotname = "";
                    int count = 0;
                    gvchkbox = "chkSelect";
                    gvdepotid = "lblDEPOTID";
                    gvdepotname = "lblDEPOTNAME";
                    foreach (GridViewRow gvrow in gvunlock.Rows)
                    {
                        CheckBox chkBx = (CheckBox)gvrow.FindControl("" + gvchkbox + "");
                        Label lblDEPOTID = (Label)gvrow.FindControl("" + gvdepotid + "");
                        Label lblDEPOTNAME = (Label)gvrow.FindControl("" + gvdepotname + "");
                        if (chkBx.Checked)
                        {
                            DataRow dr = DT.NewRow();
                            dr["PRODUCTID"] = ProductID.Trim();
                            dr["PRODUCTNAME"] = txtShortName.Text.Trim();
                            dr["DEPOTID"] = lblDEPOTID.Text.Trim();
                            dr["DEPOTNAME"] = lblDEPOTNAME.Text.Trim();

                            DT.Rows.Add(dr);
                            DT.AcceptChanges();
                        }
                    }
                    HttpContext.Current.Session["DEPOTMAPPING"] = DT;
                    if (DT != null)
                    {
                        if (DT.Rows.Count > 0)
                        {
                            XML = ConvertDatatable(DT);
                        }
                    }
                    DepotMap = ClsProduct.SaveDepotMapping(ProductID, XML);
                    if (DepotMap > 0)
                    {
                        Session.Remove("DEPOTMAPPING");
                    }
                }*/
                #endregion

                #region All Depot
                /*if (this.ddlBillingDepotType.SelectedValue.Trim() == "1")
                {
                    int DepotMap = 0;
                    string ProductID = string.Empty;
                    if (Hdn_Fld.Value.ToString().Trim() != "")
                    {
                        ProductID = Hdn_Fld.Value.ToString().Trim();
                    }
                    else
                    {
                        ProductID = guid.Trim();
                    }
                    if (HttpContext.Current.Session["DEPOTMAPPING"] == null)
                    {
                        CreateDepotMappingDataTable();
                    }
                    DataTable DT = (DataTable)HttpContext.Current.Session["DEPOTMAPPING"];
                    GridView gvunlock = gvBrandTypeMap;
                    string gvdepotid = "";
                    string gvdepotname = "";
                    gvdepotid = "lblDEPOTID";
                    gvdepotname = "lblDEPOTNAME";
                    foreach (GridViewRow gvrow in gvunlock.Rows)
                    {
                        Label lblDEPOTID = (Label)gvrow.FindControl("" + gvdepotid + "");
                        Label lblDEPOTNAME = (Label)gvrow.FindControl("" + gvdepotname + "");
                        DataRow dr = DT.NewRow();
                        dr["PRODUCTID"] = ProductID.Trim();
                        dr["PRODUCTNAME"] = txtShortName.Text.Trim();
                        dr["DEPOTID"] = lblDEPOTID.Text.Trim();
                        dr["DEPOTNAME"] = lblDEPOTNAME.Text.Trim();
                        DT.Rows.Add(dr);
                        DT.AcceptChanges();

                    }
                    HttpContext.Current.Session["DEPOTMAPPING"] = DT;
                    if (DT != null)
                    {
                        if (DT.Rows.Count > 0)
                        {
                            XML = ConvertDatatable(DT);
                        }
                    }
                    DepotMap = ClsProduct.SaveDepotMapping(ProductID, XML);
                    if (DepotMap > 0)
                    {
                        Session.Remove("DEPOTMAPPING");
                    }
                }*/
                #endregion
                //}
                #endregion

                this.LoadProductMaster();
                this.pnlAdd.Style["display"] = "none";
                this.pnlDisplay.Style["display"] = "";
                this.btnaddhide.Style["display"] = "";
                this.ddlitemtype.SelectedValue = "0";
                this.chkpowersku.Checked = false;
                this.ddlBillingDepotType.SelectedValue = "1";

                if (guid.Trim() != "")
                {
                    this.hdnPid.Value = guid.Trim();
                    this.txtPname.Text = txtShortName.Text.ToString().Trim();
                    this.pnlBS.Style["display"] = "";
                    this.trBSTPU.Style["display"] = "";
                    this.trbtnBSTPU.Style["display"] = "";
                    this.pnlDisplay.Style["display"] = "none";
                    this.btnaddhide.Style["display"] = "none";
                    this.trPackSize.Style["display"] = "none";
                    this.trbtnPacksize.Style["display"] = "none";
                    this.pnlDistributor.Style["display"] = "none";
                    this.trDepot.Style["display"] = "none";
                    this.trbtnPackSizeDepot.Style["display"] = "none";
                    this.trGroup.Style["display"] = "none";
                    this.trGroupbtn.Style["display"] = "none";
                    Session["PRODUCTMAPPINGDETAILSRECORDS"] = null;
                    Session.Remove("DEPOTMAPPING");
                }
            }
            else if (ID == 4)
            {
                MessageBox1.ShowInfo("Code already exist..");
            }
            else if (ID == 3)
            {
                MessageBox1.ShowInfo("Name already exist..");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            txtname2.Enabled = true;
            ddlNature.Enabled = true;
            ddlUOM.Enabled = true;
            ddlFragrance.Enabled = true;
            ddlitemtype.Enabled = true;
            txtBestbefore.Enabled = true;
            txtAssessablepercentage.Enabled = true;
            ChkActive.Enabled = true;
            txtUValue.Enabled = true;
            txtShortName.Enabled = true;
            ddlDivision.Enabled = true;
            ddlCategory.Enabled = true;
            
            this.Hdn_Fld.Value = "";
            this.pnlAdd.Style["display"] = "none";
            this.divproductinfo.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "";
            this.btnaddhide.Style["display"] = "";
            this.pnlBS.Style["display"] = "none";
            this.trProductsubmit.Style["display"] = "none";
            this.ddlDivision.Enabled = true;
            this.ddlCategory.Enabled = true;
            this.ddlNature.Enabled = true;
            this.ddlUOM.Enabled = true;
            this.ddlFragrance.Enabled = true;
            this.txtUValue.Enabled = true;
            this.btnRefresh.Visible = true;
            this.btnRefreshCategory.Visible = true;
            this.btnRefreshNature.Visible = true;
            this.btnRefreshUOM.Visible = true;
            this.btnRefreshFragrance.Visible = true;
            this.txtname2.Text = "";
            this.txtShortName.Text = "";
            this.ddlitemtype.SelectedValue = "0";
            this.txtindexno.Text = "";
            this.ddlsearchCategory.SelectedValue = "0";
            this.ddlsearchbrand.SelectedValue = "0";
            this.chkpowersku.Checked = false;
            this.trDepotMap.Visible = false;
            this.trinvoice.Visible = false;
            this.chkservice.Checked = false;
            this.ddlBillingDepotType.SelectedValue = "-1";
            this.ddlstorelocation.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void DeleteRecord(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            if (e.Record["ID"].ToString().Trim() != "")
            {
                int ID = 0;
                ClsProductMaster ClsProduct = new ClsProductMaster();
                ID = ClsProduct.DeleteProductMaster(e.Record["ID"].ToString());
                if (ID > 0)
                {
                    e.Record["Error"] = "Record Deleted Successfully. ";
                    BindGridByCategory();
                }
                else
                {
                    e.Record["Error"] = "Error On Deleting. ";
                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btngridsave_Click(object sender, EventArgs e)
    {
        try
        {
            ClsProductMaster ClsProduct = new ClsProductMaster();
            if (hdnPid.Value == "")
            {
                string pflag = string.Empty;
                string userid = Hdn_Fld.Value.ToString();
                DataTable dt = new DataTable();

                dt = ClsProduct.BindProductMasterById(userid);
                divproductinfo.Style["display"] = "";
                pflag = dt.Rows[0]["SETFLAG"].ToString();

                txtCode.Text = dt.Rows[0]["CODE"].ToString();
                txtName.Text = dt.Rows[0]["NAME1"].ToString();
                txtname2.Text = dt.Rows[0]["NAME2"].ToString();
                ddlDivision.SelectedValue = dt.Rows[0]["DIVID"].ToString();
                ddlCategory.SelectedValue = dt.Rows[0]["CATID"].ToString();
                ddlNature.SelectedValue = dt.Rows[0]["NATUREID"].ToString();
                ddlUOM.SelectedValue = dt.Rows[0]["UOMID"].ToString();

                ddlFragrance.SelectedValue = dt.Rows[0]["FRGID"].ToString();
                txtUValue.Text = dt.Rows[0]["UNITVALUE"].ToString();

                txtAssessablepercentage.Text = dt.Rows[0]["ASSESSABLEPERCENT"].ToString();
                txtBestbefore.Text = dt.Rows[0]["BESTBEFORE"].ToString();
                txtMinstooklevel.Text = dt.Rows[0]["MINSTOCKLEVEL"].ToString();
                txtShortName.Text = Convert.ToString(dt.Rows[0]["PRODUCTALIAS"]).Trim();
                
                txtindexno.Text = Convert.ToString(dt.Rows[0]["CSDINDEXNO"]).Trim();
                this.LoadItemType();

                if (Convert.ToString(dt.Rows[0]["TYPEID"]).Trim() != "")
                {
                    this.ddlitemtype.SelectedValue = Convert.ToString(dt.Rows[0]["TYPEID"]).Trim();
                }
                else
                {
                    this.ddlitemtype.SelectedValue = "0";
                }
                LoadStorelocation(userid);
               
                this.ddlDivision.Enabled = false;
                this.ddlCategory.Enabled = true;
                this.ddlNature.Enabled = false;
                this.ddlUOM.Enabled = false;
                this.ddlFragrance.Enabled = false;
                this.txtUValue.Enabled = false;
                this.btnRefresh.Visible = false;
                this.btnRefreshCategory.Visible = false;
                this.btnRefreshNature.Visible = false;
                this.btnRefreshUOM.Visible = false;
                this.btnRefreshFragrance.Visible = false;

                if (dt.Rows[0]["RETURNABLE"].ToString() == "Y")
                {
                    chkRefundable.Checked = true;
                }
                else
                {
                    chkRefundable.Checked = false;
                }
                txtMRP.Text = dt.Rows[0]["MRP"].ToString();
                this.txtSingDollar.Text = dt.Rows[0]["SINGAPOREDOLLAR"].ToString();
                this.txtNepRupee.Text = dt.Rows[0]["NEPALESERUPEE"].ToString();
                this.txtBangTaka.Text = dt.Rows[0]["BANGLADESHITAKA"].ToString();
                if (dt.Rows[0]["ACTIVE"].ToString() == "Active")
                {
                    ChkActive.Checked = true;
                }
                else
                {
                    ChkActive.Checked = false;
                }

                if (dt.Rows[0]["FREIGHTSERVICE"].ToString() == "T")
                {
                    chkservice.Checked = true;
                }
                else
                {
                    chkservice.Checked = false;
                }

                if (dt.Rows[0]["ISPOWERSKU"].ToString() == "Y")
                {
                    chkpowersku.Checked = true;
                }
                else
                {
                    chkpowersku.Checked = false;
                }                              

                #region Product Depot Mapping
                this.LoadDepotType();
                this.ddlBillingDepotType.SelectedValue = Convert.ToString(dt.Rows[0]["DEPOT_MAPPING"]).Trim();
                if (this.ddlBillingDepotType.SelectedValue.Trim() == "-1")
                {
                    this.trDepotMap.Visible = false;
                    this.trinvoice.Visible = false;
                    this.gvBrandTypeMap.DataSource = null;
                    this.gvBrandTypeMap.DataBind();
                }
                else if (this.ddlBillingDepotType.SelectedValue.Trim() == "1")
                {
                    this.trDepotMap.Visible = false;
                    this.trinvoice.Visible = false;
                    if (Hdn_Fld.Value.Trim() == "")
                    {
                        this.LoadDepotGrid("NA", "NA", "A");
                    }
                    else
                    {
                        this.LoadDepotGrid(Hdn_Fld.Value.Trim(), this.ddlBillingDepotType.SelectedValue.Trim(), "U");
                    }
                }
                else
                {
                    if (Hdn_Fld.Value.Trim() == "")
                    {
                        this.LoadDepotGrid("NA", "NA", "A");
                    }
                    else
                    {
                        this.LoadDepotGrid(Hdn_Fld.Value.Trim(), this.ddlBillingDepotType.SelectedValue.Trim(), "U");
                    }
                    this.trDepotMap.Visible = true;
                    this.trinvoice.Visible = true;
                }
                #endregion

                this.pnlAdd.Style["display"] = "";
                this.pnlDisplay.Style["display"] = "none";
                this.btnaddhide.Style["display"] = "none";
                this.trProductsubmit.Style["display"] = "";
            }
            else
            {
                if (hdnPid1.Value == "BS")
                {

                    pnlAdd.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "none";
                    btnaddhide.Style["display"] = "none";
                    pnlBS.Style["display"] = "";
                    trProductsubmit.Style["display"] = "none";
                    trBSTPU.Style["display"] = "";
                    trbtnBSTPU.Style["display"] = "";

                    trDepot.Style["display"] = "none";
                    trPackSize.Style["display"] = "none";
                    trbtnPackSizeDepot.Style["display"] = "none";
                    trbtnPacksize.Style["display"] = "none";
                    trGroup.Style["display"] = "none";
                    trGroupbtn.Style["display"] = "none";
                    ddlPackigsize.SelectedValue = "0";
                    ddlPackigsize1.SelectedValue = "0";
                    txtUnitvalue.Text = "";
                    txtRorder.Text = "";


                }
                else if (hdnPid1.Value == "GR")
                {
                    ClsPBMap clsPBMaster = new ClsPBMap();
                    this.CreateDataTable();

                    pnlAdd.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "none";
                    btnaddhide.Style["display"] = "none";
                    pnlBS.Style["display"] = "";
                    trGroup.Style["display"] = "";
                    trGroupbtn.Style["display"] = "";
                    trBSTPU.Style["display"] = "none";
                    trbtnBSTPU.Style["display"] = "none";
                    trProductsubmit.Style["display"] = "none";
                    trDepot.Style["display"] = "none";
                    trPackSize.Style["display"] = "none";
                    trbtnPackSizeDepot.Style["display"] = "none";
                    trbtnPacksize.Style["display"] = "none";
                    ddlbusinesssegment.SelectedValue = "0";
                    ddlPackigsize.SelectedValue = "0";
                    ddlPackigsize1.SelectedValue = "0";
                    txtUnitvalue.Text = "";
                    txtRorder.Text = "";
                    DataTable dtgroup = new DataTable();
                    string ProductID = Convert.ToString(hdnPid.Value).Trim();
                    dtgroup = clsPBMaster.EditProductGroup(ProductID);


                    if (dtgroup.Rows.Count > 0)
                    {
                        DataTable dtGroupEdit = (DataTable)Session["PRODUCTGROUP"];

                        for (int i = 0; i < dtgroup.Rows.Count; i++)
                        {
                            DataRow dr = dtGroupEdit.NewRow();
                            dr["GUID"] = Guid.NewGuid();
                            dr["PRODUCTID"] = Convert.ToString(dtgroup.Rows[i]["PRODUCTID"]).Trim();
                            dr["PRODUCTNAME"] = Convert.ToString(dtgroup.Rows[i]["PRODUCTNAME"]).Trim();
                            dr["BSID"] = Convert.ToString(dtgroup.Rows[i]["BSID"]).Trim();
                            dr["BSNAME"] = Convert.ToString(dtgroup.Rows[i]["BSNAME"]).Trim();
                            dr["GROUPID"] = Convert.ToString(dtgroup.Rows[i]["GROUPID"]).Trim();
                            dr["GROUPNAME"] = Convert.ToString(dtgroup.Rows[i]["GROUPNAME"]).Trim();
                            dtGroupEdit.Rows.Add(dr);
                            dtGroupEdit.AcceptChanges();
                        }

                        HttpContext.Current.Session["PRODUCTGROUP"] = dtGroupEdit;
                        gvgroupadd.DataSource = dtgroup;
                        gvgroupadd.DataBind();
                    }
                    else
                    {
                        gvgroupadd.DataSource = null;
                        gvgroupadd.DataBind();
                    }
                }
                else if (hdnPid1.Value == "PS")
                {
                    pnlAdd.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "none";
                    btnaddhide.Style["display"] = "none";
                    pnlBS.Style["display"] = "";

                    pnlAdd.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "none";
                    btnaddhide.Style["display"] = "none";
                    pnlBS.Style["display"] = "";
                    trProductsubmit.Style["display"] = "none";
                    trBSTPU.Style["display"] = "none";
                    trbtnBSTPU.Style["display"] = "none";
                    trGroup.Style["display"] = "none";
                    trGroupbtn.Style["display"] = "none";
                    //this.trgroupsave.Visible = false;
                    trDepot.Style["display"] = "none";
                    trPackSize.Style["display"] = "";
                    trbtnPackSizeDepot.Style["display"] = "none";
                    trbtnPacksize.Style["display"] = "";

                    ddlPackigsize.SelectedValue = "0";
                    ddlPackigsize1.SelectedValue = "0";
                    txtUnitvalue.Text = "";
                    txtRorder.Text = "";

                    ClsProductPackingSizeMap clspacking = new ClsProductPackingSizeMap();
                    clspacking.ResetDataTables();
                    clspacking.BindMappingDataTable();
                    DataTable dt = clspacking.BindMappingBasedOnProduct(hdnPid.Value.Trim());
                    if (dt.Rows.Count > 0)
                    {
                        this.gvPackingsizeMappingAdd.DataSource = dt;
                        this.gvPackingsizeMappingAdd.DataBind();
                    }
                    else
                    {
                        this.gvPackingsizeMappingAdd.DataSource = null;
                        this.gvPackingsizeMappingAdd.DataBind();
                    }
                }
                else if (hdnPid1.Value == "TR")
                {
                    string strPopup = "<script language='javascript' ID='script1'>"

                // Passing intId to popup window.
                + "window.open('frmProductRegionMapping.aspx?productid=" + HttpUtility.UrlEncode(hdnPid.Value.ToString()) + "&productname=" + HttpUtility.UrlEncode(hdnPName.Value.ToString())

                + "','popUpWindow', 'top=200, left=550, width=500, height=500, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, titlebar=no, toolbar=no, status=no, center=yes')"

                + "</script>";

                    ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);

                }
                else
                {

                    pnlAdd.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "none";
                    btnaddhide.Style["display"] = "none";
                    pnlBS.Style["display"] = "";
                    trProductsubmit.Style["display"] = "none";
                    trBSTPU.Style["display"] = "none";
                    trbtnBSTPU.Style["display"] = "none";
                    trGroup.Style["display"] = "none";
                    trGroupbtn.Style["display"] = "none";
                    //this.trgroupsave.Visible = false;
                    trDepot.Style["display"] = "";
                    trPackSize.Style["display"] = "none";
                    trbtnPackSizeDepot.Style["display"] = "";
                    trbtnPacksize.Style["display"] = "none";

                    ddlPackigsize.SelectedValue = "0";
                    ddlPackigsize1.SelectedValue = "0";
                    txtUnitvalue.Text = "";
                    txtRorder.Text = "";
                    txtUnitvalue.Text = "";
                    txtRorder.Text = "";

                    //Reorder Grid Population
                    DataTable dtAddgrid = ClsProduct.BindReorderGrid(hdnPid.Value.Trim());
                    if (dtAddgrid.Rows.Count > 0)
                    {
                        this.gvReorder.DataSource = dtAddgrid;
                        this.gvReorder.DataBind();
                    }

                    else
                    {
                        this.gvReorder.DataSource = null;
                        this.gvReorder.DataBind();
                    }
                }
            }

            txtname2.Enabled = true;
            txtShortName.Enabled = true;
            ddlDivision.Enabled = false;
            ddlCategory.Enabled = true;
            ddlNature.Enabled = false;
            ddlUOM.Enabled = false;
            ddlFragrance.Enabled = false;
            ddlitemtype.Enabled = false;
            txtBestbefore.Enabled = false;
            txtAssessablepercentage.Enabled = false;
            txtUValue.Enabled = false;
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GetProductName();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GetProductName();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void ddlUOM_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GetProductName();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void txtUValue_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GetProductName();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void ddlFragrance_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GetProductName();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnBSTPUSave_Click(object sender, EventArgs e)
    {


        try
        {
            string Mode = "A";
            string Tag = "";
            int count = 0;
            int count1 = 0;
            int count2 = 0;
            int count3 = 0;
            int countbs = 0;
            int counttpu = 0;
            int countfactory = 0;
            ClsPBMap clsPBMaster = new ClsPBMap();
            clsPBMaster.DeletePBMapbyid(hdnPid.Value);

            if (gvPBMap.TotalRowCount > 0  && gvFactory.TotalRowCount > 0) /*&& gvPTPUMap.TotalRowCount > 0*/
            {
                for (int i = 0; i < gvPBMap.RowsInViewState.Count; i++)
                {
                    GridDataControlFieldCell cell = gvPBMap.RowsInViewState[i].Cells[1] as GridDataControlFieldCell;

                    CheckBox chk = cell.FindControl("ChkID") as CheckBox;
                    HiddenField hiddenField = cell.FindControl("BsName") as HiddenField;
                    if (chk.Checked == true)
                    {
                        int ID = 0;
                        countbs = countbs + 1;
                        count = count + 1;
                        if (count > 0)
                        {
                            ID = clsPBMaster.SavePBMap("", hdnPid.Value, txtPname.Text.ToString(), chk.ToolTip, hiddenField.Value, Mode);
                        }
                    }
                }


                if(gvPTPUMap.TotalRowCount > 0)
                {
                    for (int i = 0; i < gvPTPUMap.RowsInViewState.Count; i++)
                    {
                        GridDataControlFieldCell cell = gvPTPUMap.RowsInViewState[i].Cells[1] as GridDataControlFieldCell;

                        CheckBox chk = cell.FindControl("ChkIDTPU") as CheckBox;
                        HiddenField hiddenField = cell.FindControl("hdnTPUName") as HiddenField;
                        Tag = "T";
                        if (chk.Checked == true)
                        {
                            int ID = 0;
                            counttpu = counttpu + 1;
                            count = count + 1;
                            if (count > 0)
                            {
                                ID = clsPBMaster.SaveTPUMap("", hdnPid.Value, txtPname.Text.ToString(), chk.ToolTip, hiddenField.Value, Mode, Tag);
                            }
                        }
                    }
                    if (counttpu == 0 && countfactory == 0)
                    {
                        MessageBox1.ShowInfo("Please select atleast any 1 <b><font color='green'>TPU</font></b> or <b><font color='red'>Factory mapping</font></b>", 80, 450);
                        LoadBSGrid();
                        LoadTPUMaster();
                        LoadFactoryMaster();
                    }
                }
                
                for (int i = 0; i < gvFactory.RowsInViewState.Count; i++)
                {
                    GridDataControlFieldCell cell = gvFactory.RowsInViewState[i].Cells[1] as GridDataControlFieldCell;

                    CheckBox chk = cell.FindControl("ChkIDFactory") as CheckBox;
                    HiddenField hiddenField = cell.FindControl("hdnFactoryName") as HiddenField;
                    Tag = "F";
                    if (chk.Checked == true)
                    {
                        int ID = 0;
                        countfactory = countfactory + 1;
                        count = count + 1;
                        if (count > 0)
                        {
                            ID = clsPBMaster.SaveTPUMap("", hdnPid.Value, txtPname.Text.ToString(), chk.ToolTip, hiddenField.Value, Mode, Tag);
                        }
                    }
                }

                if (countbs == 0)
                {
                    MessageBox1.ShowInfo("Please select atleast Any 1 <b><font color='green'>Business Segment mapping</font></b>", 80, 470);
                    LoadBSGrid();
                    LoadTPUMaster();
                    LoadFactoryMaster();
                }
                //else if (countfactory == 0)
                //{
                //    MessageBox1.ShowInfo("Please select atleast Any 1 Factory mapping", 80, 370);
                //}
                else if (count == 0)
                {
                    //string message = "alert('Please select atleast Any 1 from those Business Segment Mapping or TPU Mapping or Factory Mapping')";
                    //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    MessageBox1.ShowInfo("Please select atleast Any 1 from those Business Segment or TPU or Factory Mapping", 80, 700);
                    pnlAdd.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "none";
                    btnaddhide.Style["display"] = "none";
                    pnlBS.Style["display"] = "";
                    LoadBSGrid();
                    LoadBSGrid();
                    LoadTPUMaster();
                    LoadFactoryMaster();

                }
                else
                {
                    //string message = "alert('Record Saved Successfully..')";
                    //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    ClsProductMaster CLSPRO = new ClsProductMaster();
                    int updateid = CLSPRO.UpdateLdomProductMaster(hdnPid.Value.Trim());



                    MessageBox1.ShowSuccess("Record Saved Successfully..");
                    pnlAdd.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "";
                    btnaddhide.Style["display"] = "";
                    pnlBS.Style["display"] = "none";
                }
            }
            else
            {
                if (gvPTPUMap.TotalRowCount > 0)
                {
                    for (int i = 0; i < gvPBMap.RowsInViewState.Count; i++)
                    {
                        GridDataControlFieldCell cell = gvPBMap.RowsInViewState[i].Cells[1] as GridDataControlFieldCell;

                        CheckBox chk = cell.FindControl("ChkID") as CheckBox;
                        HiddenField hiddenField = cell.FindControl("BsName") as HiddenField;
                        if (chk.Checked == true)
                        {
                            int ID = 0;
                            // int count1 = 0;
                            count1 = count1 + 1;
                            if (count1 > 0)
                            {
                                ID = clsPBMaster.SavePBMap("", hdnPid.Value, txtPname.Text.ToString(), chk.ToolTip, hiddenField.Value, Mode);


                            }

                            if (ID == 1)
                            {
                            }
                            else if (ID == 2)
                            {
                            }
                        }
                    }
                }

                if (gvPTPUMap.TotalRowCount > 0)
                {
                    for (int i = 0; i < gvPTPUMap.RowsInViewState.Count; i++)
                    {
                        GridDataControlFieldCell cell = gvPTPUMap.RowsInViewState[i].Cells[1] as GridDataControlFieldCell;

                        CheckBox chk = cell.FindControl("ChkIDTPU") as CheckBox;
                        HiddenField hiddenField = cell.FindControl("hdnTPUName") as HiddenField;
                        Tag = "T";
                        if (chk.Checked == true)
                        {
                            int ID = 0;
                            // int count2 = 0;
                            count2 = count2 + 1;
                            if (count2 > 0)
                            {
                                ID = clsPBMaster.SaveTPUMap("", hdnPid.Value, txtPname.Text.ToString(), chk.ToolTip, hiddenField.Value, Mode, Tag);
                            }
                            if (ID == 1)
                            {

                            }
                            else if (ID == 2)
                            {

                            }
                        }
                    }
                }
                if (gvFactory.TotalRowCount > 0)
                {
                    for (int i = 0; i < gvFactory.RowsInViewState.Count; i++)
                    {
                        GridDataControlFieldCell cell = gvFactory.RowsInViewState[i].Cells[1] as GridDataControlFieldCell;

                        CheckBox chk = cell.FindControl("ChkIDFactory") as CheckBox;
                        HiddenField hiddenField = cell.FindControl("hdnFactoryName") as HiddenField;
                        Tag = "F";
                        if (chk.Checked == true)
                        {
                            int ID = 0;
                            // int count3 = 0;
                            count3 = count3 + 1;
                            if (count3 > 0)
                            {
                                ID = clsPBMaster.SaveTPUMap("", hdnPid.Value, txtPname.Text.ToString(), chk.ToolTip, hiddenField.Value, Mode, Tag);
                            }
                            if (ID == 1)
                            {


                            }

                            else if (ID == 2)
                            {


                            }

                        }

                    }
                }

                if (count1 > 0 || count2 > 0 || count3 > 0)
                {
                    //string message = "alert('Record Saved Successfully..')";
                    //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    MessageBox1.ShowSuccess("Record Saved Successfully..");
                    pnlAdd.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "";
                    btnaddhide.Style["display"] = "";
                    pnlBS.Style["display"] = "none";
                }
                else
                {
                    //string message = "alert('Please select atleast Any 1 from those Business Segment Mapping or TPU Mapping or Factory Mapping')";
                    //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    MessageBox1.ShowInfo("Please select atleast Any 1 from those Business Segment Mapping or TPU Mapping or Factory Mapping", 80, 800);

                    pnlAdd.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "none";
                    btnaddhide.Style["display"] = "none";
                    pnlBS.Style["display"] = "";
                    LoadBSGrid();
                    LoadBSGrid();
                    LoadTPUMaster();
                    LoadFactoryMaster();
                }



            }




        }

        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnPackSizeSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string Mode = "A";
            ClsProductPackingSizeMap clspacking = new ClsProductPackingSizeMap();
            DataTable dtRecordsCheck = (DataTable)HttpContext.Current.Session["PRODUCTMAPPINGDETAILSRECORDS"];
            if (dtRecordsCheck.Rows.Count > 0)
            {
                clspacking.SaveMaster(hdnPid.Value, txtPname.Text.ToString(), Mode);
                ClsProductMaster CLSPRO = new ClsProductMaster();
                int updateid = CLSPRO.UpdateLdomProductMaster(hdnPid.Value.Trim());
            }
            else
            {
                MessageBox1.ShowInfo("Please Add atleast 1 record for mapping");
            }
            clspacking.ResetDataTables();
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            pnlBS.Style["display"] = "none";

            Session["PRODUCTMAPPINGDETAILSRECORDS"] = null;
            gvPackingsizeMappingAdd.ClearPreviousDataSource();
            this.gvPackingsizeMappingAdd.DataSource = null;
            this.gvPackingsizeMappingAdd.DataBind();

            //string message = "alert('Record Saved Successfully..')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            if (hdnPid1.Value == "PS")
            {
                MessageBox1.ShowSuccess("PackSize maped successfully");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

        if (hdnPid1.Value != "PS")
        {

            pnlBS.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";
            trPackSize.Style["display"] = "none";
            trbtnPacksize.Style["display"] = "none";
            pnlDistributor.Style["display"] = "none";
            trDepot.Style["display"] = "none";
            trbtnPackSizeDepot.Style["display"] = "none";
            trGroup.Style["display"] = "none";
            trGroupbtn.Style["display"] = "none";
            trbtnBSTPU.Style["display"] = "";
            trBSTPU.Style["display"] = "";
        }
    }

    protected void btnPBMapSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsProductPackingSizeMap clspacking = new ClsProductPackingSizeMap();
            string Mode = "A";
            DataTable dtReorderRecordsCheck = (DataTable)HttpContext.Current.Session["PRODUCTREORDERMAPPING"];
            if (dtReorderRecordsCheck.Rows.Count > 0)
            {
                ClsProductMaster ClsProduct = new ClsProductMaster();
                ClsProduct.SaveReorderMaster(hdnPid.Value, txtPname.Text.ToString(), Mode);
                ClsProductMaster CLSPRO = new ClsProductMaster();
                int updateid = CLSPRO.UpdateLdomProductMaster(hdnPid.Value.Trim());
            }

            else
            {
                //string message1 = "alert('Please Add atleast 1 record for mapping')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
                MessageBox1.ShowInfo("Please Add atleast 1 record for mapping");
            }
            clspacking.ResetDataTables();

            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            pnlBS.Style["display"] = "none";

            //string message = "alert('Record Saved Successfully..')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            MessageBox1.ShowSuccess("Record Saved Successfully..");

            Session["PRODUCTREORDERMAPPING"] = null;
            gvReorder.ClearPreviousDataSource();
            this.gvReorder.DataSource = null;
            this.gvReorder.DataBind();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }


    }

    protected void ddlproductname_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (hdnPid.Value != "")
            {
                LoadPOProductItembyid(hdnPid.Value);
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void gvPBMap_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            //if(Session["Hdn_Fld.Value"]!=null)
            //{
            //    hdnPid.Value = Convert.ToString(Session ["Hdn_Fld.Value"]);
            //}
            if (hdnPid.Value != "")
            {
                LoadPOProductItembyid(hdnPid.Value);
            }
            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[1] as GridDataControlFieldCell;
                CheckBox chk = cell.FindControl("ChkID") as CheckBox;
                HiddenField hiddenField = cell.FindControl("BsName") as HiddenField;


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString() == chk.ToolTip)
                    {
                        chk.Checked = true;

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

    protected void gvPTPUMap_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (hdnPid.Value != "")
            {
                LoadTPUbyid(hdnPid.Value);
            }

            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[1] as GridDataControlFieldCell;
                CheckBox chk = cell.FindControl("ChkIDTPU") as CheckBox;
                HiddenField hiddenField = cell.FindControl("hdnTPUName") as HiddenField;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString() == chk.ToolTip)
                    {
                        chk.Checked = true;
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

    protected void btnPackSizeCancel_Click(object sender, EventArgs e)
    {
        try
        {
            hdnPid.Value = "";
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "none";
            pnlBS.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            Session["PRODUCTMAPPINGDETAILSRECORDS"] = null;
            gvPackingsizeMappingAdd.ClearPreviousDataSource();
            gvPackingsizeMappingAdd.DataSource = null;
            gvPackingsizeMappingAdd.DataBind();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnPBMapCancel_Click(object sender, EventArgs e)
    {
        try
        {
            pnlAdd.Style["display"] = "none";
            pnlBS.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            hdnPid.Value = "";
            Hdn_Fld.Value = "";
            Session["PRODUCTREORDERMAPPING"] = null;
            gvReorder.ClearPreviousDataSource();
            gvReorder.DataSource = null;
            gvReorder.DataBind();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnBSTPUCancel_Click(object sender, EventArgs e)
    {
        try
        {
            hdnPid.Value = "";
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "none";
            pnlBS.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            Session["PRODUCTMAPPINGDETAILSRECORDS"] = null;
            Session["PRODUCTREORDERMAPPING"] = null;

            gvPackingsizeMappingAdd.ClearPreviousDataSource();
            gvPackingsizeMappingAdd.DataSource = null;
            gvPackingsizeMappingAdd.DataBind();

            gvReorder.ClearPreviousDataSource();
            gvReorder.DataSource = null;
            gvReorder.DataBind();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnADDGrid_Click(object sender, EventArgs e)
    {
        try

        {
            ClsProductPackingSizeMap clspacking = new ClsProductPackingSizeMap();
            if (Session["PRODUCTMAPPINGDETAILSRECORDS"] == null)
            {
                clspacking.BindMappingDataTable();
            }

            if (Convert.ToString(ddlPackigsize1.SelectedValue) != Convert.ToString(ddlPackigsize.SelectedValue))
            {
                if (clspacking.MappingRecordsCheck(Convert.ToString(ddlPackigsize1.SelectedValue), Convert.ToString(ddlPackigsize.SelectedValue), Convert.ToString(Hdn_Fld.Value)) == 0)
                {
                    if (Hdn_Fld.Value.ToString() == "")
                    {
                        DataTable dtForecastClone = new DataTable();
                        dtForecastClone = (DataTable)HttpContext.Current.Session["PRODUCTMAPPINGDETAILSRECORDS"];
                        string Mode = "A";
                        gvPackingsizeMappingAdd.DataSource = clspacking.BindMappingGridRecords(Convert.ToString(ddlPackigsize.SelectedValue), Convert.ToString(ddlPackigsize.SelectedItem),
                                                                                           Convert.ToString(ddlPackigsize1.SelectedValue),
                                                                                          Convert.ToString(ddlPackigsize1.SelectedItem),
                                                                                          Convert.ToString(txtUnitvalue.Text.Trim()),
                                                                                          this.txtGrossWeight.Text.Trim(),
                                                                                          Convert.ToString(this.ddlUOMPacksize.SelectedValue).Trim(),
                                                                                          Convert.ToString(this.ddlUOMPacksize.SelectedItem).Trim(),
                                                                                          Mode);


                        gvPackingsizeMappingAdd.DataBind();
                    }
                    else
                    {
                        DataTable dtForecastClone = new DataTable();
                        dtForecastClone = (DataTable)HttpContext.Current.Session["PRODUCTMAPPINGDETAILSRECORDS"];
                        string Mode = "U";
                        gvPackingsizeMappingAdd.DataSource = clspacking.BindMappingGridRecords(Convert.ToString(ddlPackigsize.SelectedValue), Convert.ToString(ddlPackigsize.SelectedItem),
                                                                                           Convert.ToString(ddlPackigsize1.SelectedValue),
                                                                                          Convert.ToString(ddlPackigsize1.SelectedItem),
                                                                                          Convert.ToString(txtUnitvalue.Text.Trim()),
                                                                                          this.txtGrossWeight.Text.Trim(),
                                                                                          Convert.ToString(this.ddlUOMPacksize.SelectedValue).Trim(),
                                                                                          Convert.ToString(this.ddlUOMPacksize.SelectedItem).Trim(),
                                                                                          Mode);


                        gvPackingsizeMappingAdd.DataBind();
                    }

                    this.ddlPackigsize.SelectedValue = "0";
                    this.ddlPackigsize1.SelectedValue = "0";
                    this.txtUnitvalue.Text = "";
                    this.txtGrossWeight.Text = "0";
                    this.ddlUOMPacksize.SelectedValue = "0";
                }
                else
                {
                    MessageBox1.ShowInfo("Record already exists!");
                }
            }
            else
            {
                MessageBox1.ShowInfo("<b>Packsize can not same!</b>");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btngrddelete_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtValidate = new DataTable();
            ClsProductMaster obj = new ClsProductMaster();
            string ProductID = this.hdnPid.Value.Trim();
            dtValidate = obj.PacksizeValidation(ProductID);
            if (Convert.ToString(dtValidate.Rows[0]["FLAG"]).Trim() != "1")
            {

                string forecastSlNo = Convert.ToString(hdnMappingDelete.Value);
                DataTable dtdeleteforecastrecord = new DataTable();
                dtdeleteforecastrecord = (DataTable)Session["PRODUCTMAPPINGDETAILSRECORDS"];

                DataRow[] drr = dtdeleteforecastrecord.Select("SLNO='" + forecastSlNo + "'");
                for (int i = 0; i < drr.Length; i++)
                {
                    drr[i].Delete();
                    dtdeleteforecastrecord.AcceptChanges();
                }
                this.gvPackingsizeMappingAdd.DataSource = dtdeleteforecastrecord;
                this.gvPackingsizeMappingAdd.DataBind();
            }
            else
            {
                MessageBox1.ShowWarning("Not allowed to change the packsize <br/>Transaction already done in  <br/>" + Convert.ToString(dtValidate.Rows[0]["MODULE"]).Trim() + "", 60, 550);
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btngridreorder_Click(object sender, EventArgs e)
    {
        try
        {
            string slno = Convert.ToString(hdnReorder.Value);
            DataTable dtdeletereorderecord = new DataTable();
            dtdeletereorderecord = (DataTable)Session["PRODUCTREORDERMAPPING"];

            DataRow[] drr = dtdeletereorderecord.Select("SLNO='" + slno + "'");
            for (int i = 0; i < drr.Length; i++)
            {
                drr[i].Delete();
                dtdeletereorderecord.AcceptChanges();
            }
            this.gvReorder.DataSource = dtdeletereorderecord;
            this.gvReorder.DataBind();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnADDgvReorder_Click(object sender, EventArgs e)
    {
        try
        {
            ClsProductMaster ClsProduct = new ClsProductMaster();
            if (ClsProduct.MappinReorderRecordsCheck(Convert.ToString(ddlDeopt.SelectedValue), Convert.ToString(hdnReorder.Value)) == 0)
            {
                if (Hdn_Fld.Value.ToString() == "")
                {
                    DataTable dtReorder = new DataTable();
                    dtReorder = (DataTable)HttpContext.Current.Session["PRODUCTREORDERMAPPING"];
                    string Mode = "A";

                    gvReorder.DataSource = ClsProduct.BindMappingGridRecords(Convert.ToString(ddlDeopt.SelectedValue), Convert.ToString(ddlDeopt.SelectedItem), Convert.ToString(txtRorder.Text.Trim()), Mode);
                    gvReorder.DataBind();
                }
                else
                {
                    //DataTable dtReorder = new DataTable();
                    //dtReorder = (DataTable)HttpContext.Current.Session["PRODUCTREORDERMAPPING"];
                    //string Mode = "U";

                    //gvReorder.DataSource = ClsProduct.BindMappingGridRecords(Convert.ToString(ddlDeopt.SelectedValue), Convert.ToString(ddlDeopt.SelectedItem), Convert.ToString(txtRorder.Text.Trim()), Mode);
                    //gvReorder.DataBind();
                }

                this.ddlDeopt.SelectedValue = "0";
                this.txtRorder.Text = "";


                //string message = "alert('Record added successfully.')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
            else
            {
                //string message = "alert('Record already exists!')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowInfo("Record already exists!");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void gvFactory_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (hdnPid.Value != "")
            {
                LoadFactoryid(hdnPid.Value);

            }
            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[1] as GridDataControlFieldCell;
                CheckBox chk = cell.FindControl("ChkIDFactory") as CheckBox;
                HiddenField hiddenField = cell.FindControl("hdnFactoryName") as HiddenField;


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString() == chk.ToolTip)
                    {
                        chk.Checked = true;

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

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            this.LoadDivision();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnRefreshCategory_Click(object sender, EventArgs e)
    {
        try
        {
            this.LoadCategory();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnRefreshNature_Click(object sender, EventArgs e)
    {
        try
        {
            this.LoadNature();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnRefreshUOM_Click(object sender, EventArgs e)
    {
        try
        {
            this.LoadUOM();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnRefreshFragrance_Click(object sender, EventArgs e)
    {
        try
        {
            this.LoadFragrance();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void gvProduct_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[9] as GridDataControlFieldCell;
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

    //========================Added By Sourav Mukherjee on 31/03/2016================//

    #region ddlbusinesssegment_SelectedIndexChanged
    protected void ddlbusinesssegment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClsProductMaster ClsProduct = new ClsProductMaster();
            gvgroup.DataSource = ClsProduct.BindGroup(ddlbusinesssegment.SelectedValue);
            gvgroup.DataBind();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
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

    #region Create DataTable Structure
    public DataTable CreateDataTable()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add(new DataColumn("GUID", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("BSID", typeof(string)));
        dt.Columns.Add(new DataColumn("BSNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("GROUPID", typeof(string)));
        dt.Columns.Add(new DataColumn("GROUPNAME", typeof(string)));
        HttpContext.Current.Session["PRODUCTGROUP"] = dt;
        return dt;
    }
    #endregion

    #region GroupMappingSave
    protected void btngroupsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string XML = string.Empty;
            DataTable dt = new DataTable();
            dt = (DataTable)HttpContext.Current.Session["PRODUCTGROUP"];
            XML = ConvertDatatableToXML(dt);
            int ID = 0;
            ClsPBMap clsPBMaster = new ClsPBMap();
            ID = clsPBMaster.SaveGroupMap(hdnPid.Value, XML);
            if (dt.Rows.Count > 0)
            {
                if (ID > 0)
                {
                    ClsProductMaster CLSPRO = new ClsProductMaster();
                    int updateid = CLSPRO.UpdateLdomProductMaster(hdnPid.Value.Trim());
                    MessageBox1.ShowSuccess("Record Saved Successfully..");
                    pnlAdd.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "";
                    btnaddhide.Style["display"] = "";
                    pnlBS.Style["display"] = "none";
                    gvgroup.DataSource = null;
                    gvgroup.DataBind();
                }
                else
                {
                    MessageBox1.ShowError("Error on saving");
                }
            }
            else
            {
                MessageBox1.ShowInfo("Please select atleast 1 record");
            }

        }

        catch (Exception ex)
        {

            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #endregion

    #region btngroupadd_Click
    protected void btngroupadd_Click(object sender, EventArgs e)
    {
        try
        {
            ClsPBMap clsPBMaster = new ClsPBMap();
            DataTable dtgroup = new DataTable();
            int COUNT = 0;
            if (Session["PRODUCTGROUP"] != null)
            {
                dtgroup = (DataTable)HttpContext.Current.Session["PRODUCTGROUP"];
            }
            if (ddlbusinesssegment.SelectedValue != "0")
            {

                if (gvgroup.TotalRowCount > 0)
                {
                    for (int i = 0; i < gvgroup.RowsInViewState.Count; i++)
                    {
                        GridDataControlFieldCell cell = gvgroup.RowsInViewState[i].Cells[1] as GridDataControlFieldCell;

                        CheckBox chk = cell.FindControl("Chkgroup") as CheckBox;
                        HiddenField hiddenField = cell.FindControl("GROUPNAME") as HiddenField;
                        if (chk.Checked == true)
                        {
                            int numberOfRecords = dtgroup.Select("PRODUCTID = '" + this.hdnPid.Value + "' AND GROUPID = '" + chk.ToolTip + "'").Length;
                            if (numberOfRecords > 0)
                            {
                                MessageBox1.ShowError("<b><font color='red'> Records already exists..!</font></b>");
                            }
                            else
                            {
                                COUNT = COUNT + 1;
                                DataRow dr = dtgroup.NewRow();
                                dr["GUID"] = Guid.NewGuid();
                                dr["PRODUCTID"] = Convert.ToString(this.hdnPid.Value).Trim();
                                dr["PRODUCTNAME"] = Convert.ToString(this.txtPname.Text).Trim();
                                dr["BSID"] = Convert.ToString(this.ddlbusinesssegment.SelectedValue).Trim();
                                dr["BSNAME"] = Convert.ToString(this.ddlbusinesssegment.SelectedItem.Text).Trim();
                                dr["GROUPID"] = chk.ToolTip;
                                dr["GROUPNAME"] = hiddenField.Value;
                                dtgroup.Rows.Add(dr);
                                dtgroup.AcceptChanges();
                            }
                        }

                    }
                    Session["PRODUCTGROUP"] = dtgroup;
                    if (dtgroup.Rows.Count > 0)
                    {
                        this.gvgroupadd.DataSource = dtgroup;
                        this.gvgroupadd.DataBind();
                        this.gvgroup.DataSource = null;
                        this.gvgroup.DataBind();
                    }
                    else
                    {
                        this.gvgroupadd.ClearPreviousDataSource();
                        this.gvgroupadd.DataSource = null;
                        this.gvgroupadd.DataBind();
                    }
                }
                else
                {
                    MessageBox1.ShowInfo("Please select group");
                }
            }
            else
            {
                MessageBox1.ShowInfo("Please select group");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region InnerGrid Delete
    protected void btngroupdelete_Click(object sender, EventArgs e)
    {
        try
        {
            string GUID = Convert.ToString(hdngroupDelete.Value);
            DataTable dtdeleterecord = new DataTable();
            dtdeleterecord = (DataTable)Session["PRODUCTGROUP"];
            if (HttpContext.Current.Session["PRODUCTGROUP"] != null)
            {
                DataRow[] drr = dtdeleterecord.Select("GUID='" + GUID + "' ");
                for (int i = 0; i < drr.Length; i++)
                {
                    drr[i].Delete();
                    dtdeleterecord.AcceptChanges();
                }
                this.gvgroupadd.DataSource = dtdeleterecord;
                this.gvgroupadd.DataBind();
            }
            else
            {
                this.gvgroupadd.DataSource = dtdeleterecord;
                this.gvgroupadd.DataBind();
            }


        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region GroupMappingCancel
    protected void btngroupcancel_Click(object sender, EventArgs e)
    {
        try
        {
            hdnPid.Value = "";
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "none";
            pnlBS.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            Session["PRODUCTMAPPINGDETAILSRECORDS"] = null;
            Session["PRODUCTREORDERMAPPING"] = null;
            trGroupbtn.Style["display"] = "none";

            gvgroup.ClearPreviousDataSource();
            gvgroup.DataSource = null;
            gvgroup.DataBind();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region BindBusinessSegment
    public void LoadBusinessSegment()
    {
        try
        {
            ClsProductMaster ClsProduct = new ClsProductMaster();
            ddlbusinesssegment.Items.Clear();
            ddlbusinesssegment.Items.Insert(0, new ListItem("-Select-", "0"));
            ddlbusinesssegment.DataSource = ClsProduct.BindBusinessSegment();
            ddlbusinesssegment.DataTextField = "BSNAME";
            ddlbusinesssegment.DataValueField = "BSID";
            ddlbusinesssegment.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region DistributorMapping
    //==============================ADD SUBHODIP DE ON 24.05.2016==================//

    #region btngrideditdistributor_Click
    protected void btngrideditdistributor_Click(object sender, EventArgs e)
    {
        try
        {
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "none";
            pnlBS.Style["display"] = "none";
            pnlDistributor.Style["display"] = "";
            btnaddhide.Style["display"] = "none";
            this.CreatedDistributorDataTable();
            this.LoadDistributor();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    #endregion

    #region LoadDistributor
    protected void LoadDistributor()
    {
        try
        {
            ClsProductMaster ClsProduct = new ClsProductMaster();
            DataTable dt = ClsProduct.BindDistributor();
            if (dt.Rows.Count > 0)
            {
                grddistributor.DataSource = dt;
                grddistributor.DataBind();
            }
            else
            {
                grddistributor.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
                grddistributor.ClearPreviousDataSource();
                grddistributor.DataSource = null;
                grddistributor.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region CreatedDistributorDataTable
    public DataTable CreatedDistributorDataTable()
    {
        try
        {
            DataTable dtdistributor = new DataTable();
            dtdistributor.Columns.Add("ID", typeof(string));
            dtdistributor.Columns.Add("NAME", typeof(string));
            dtdistributor.Columns.Add("DISTRIBUTORID", typeof(string));
            dtdistributor.Columns.Add("DISTRIBUTORNAME", typeof(string));

            HttpContext.Current.Session["DISTRIBUTORMAPPING"] = dtdistributor;

            return dtdistributor;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Convert DataTable To XML
    public string ConvertDistributorDatatableToXML(DataTable dt)
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

    #region Btn Distributor submit
    protected void btnDistributorSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsProductMaster ClsProduct = new ClsProductMaster();
            string xml = string.Empty;
            int Distributorcount = 0;
            int ID = 0;
            string tag = "P";
            DataTable dtdistributorchk = (DataTable)HttpContext.Current.Session["DISTRIBUTORMAPPING"];
            if (grddistributor.TotalRowCount > 0)
            {
                for (int i = 0; i < grddistributor.RowsInViewState.Count; i++)
                {
                    GridDataControlFieldCell cell = grddistributor.RowsInViewState[i].Cells[1] as GridDataControlFieldCell;
                    CheckBox chk = cell.FindControl("ChkIDDistid") as CheckBox;
                    HiddenField hiddenField = cell.FindControl("hdnDistributorname") as HiddenField;
                    if (chk.Checked == true)
                    {
                        Distributorcount = Distributorcount + 1;
                        DataRow dr = dtdistributorchk.NewRow();
                        dr["ID"] = Convert.ToString(Hdn_Fld.Value).Trim();
                        dr["NAME"] = Convert.ToString(txtdistproductname.Text);
                        dr["DISTRIBUTORID"] = chk.ToolTip;
                        dr["DISTRIBUTORNAME"] = Convert.ToString(hiddenField.Value).Trim();

                        dtdistributorchk.Rows.Add(dr);
                        dtdistributorchk.AcceptChanges();
                    }
                }

                Session["DISTRIBUTORMAPPING"] = dtdistributorchk;

            }

            if (Distributorcount == 0)
            {
                MessageBox1.ShowInfo("<b>Please select atleast 1 Distributor</b>", 60, 400);
            }
            else
            {
                if (dtdistributorchk.Rows.Count > 0)
                {
                    xml = ConvertDistributorDatatableToXML(dtdistributorchk);
                    ID = ClsProduct.SavedDistributorMapping(Hdn_Fld.Value, xml, tag.Trim());
                    if (ID > 0)
                    {
                        ClsProductMaster CLSPRO = new ClsProductMaster();
                        int updateid = CLSPRO.UpdateLdomProductMaster(Hdn_Fld.Value.Trim());
                        MessageBox1.ShowSuccess("Record saved successfully..");

                        Hdn_Fld.Value = "";
                        txtdistproductname.Text = "";
                        pnlAdd.Style["display"] = "none";
                        pnlDisplay.Style["display"] = "";
                        pnlBS.Style["display"] = "none";
                        pnlDistributor.Style["display"] = "none";
                        btnaddhide.Style["display"] = "";


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
    #endregion

    #region   btn DistributorCancel_Click
    protected void btnDistributorCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Hdn_Fld.Value = "";
            txtdistproductname.Text = "";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            pnlBS.Style["display"] = "none";
            pnlDistributor.Style["display"] = "none";
            btnaddhide.Style["display"] = "";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    #endregion

    #region   grddistributor_RowDataBound
    protected void grddistributor_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            ClsProductMaster ClsProduct = new ClsProductMaster();
            if (e.Row.RowType == GridRowType.DataRow && grddistributor.Rows.Count > 0)
            {
                DataTable dt = ClsProduct.EditdISTRIBUTOR(Hdn_Fld.Value);
                GridDataControlFieldCell cell = e.Row.Cells[1] as GridDataControlFieldCell;
                CheckBox chk = cell.FindControl("ChkIDDistid") as CheckBox;
                HiddenField hiddenField = cell.FindControl("hdnDistributorname") as HiddenField;


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][2].ToString() == chk.ToolTip)
                    {
                        chk.Checked = true;

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
    #endregion

    //============================================//
    #endregion


    #region gvProduct_Exporting
    protected void gvProduct_Exporting(object sender, Obout.Grid.GridExportEventArgs e)
    {
        TableRow row = new TableRow();
        TableCell cell = new TableCell();

        cell.ColumnSpan = 8;
        cell.BorderStyle = BorderStyle.None;

        cell.Text = "Product Details";
        //TableHeaderRow row1 = new TableHeaderRow();
        //row1.Cells[1].Visible = false;
        cell.HorizontalAlign = HorizontalAlign.Center;
        cell.Font.Bold = true;
        cell.BackColor = Color.YellowGreen;
        e.Table.GridLines = GridLines.None;
        row.Cells.Add(cell);
        e.Table.Rows.Add(row);
    }
    #endregion

    #region gvProduct_Exported
    protected void gvProduct_Exported(object sender, Obout.Grid.GridExportEventArgs e)
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

    #region Generate Templete

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    #region btngeneratetemp_Click
    protected void btngeneratetemp_Click(object sender, EventArgs e)
    {
        try
        {
            ClsProductMaster clsproduct = new ClsProductMaster();
            DataTable dt = clsproduct.BindGenerateTemp();
            Response.ClearContent();
            Response.Buffer = true;
            string attachment = "attachment; filename=ProductDetails.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }

        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #endregion

    #region LoadDepotType
    public void LoadDepotType()
    {
        try
        {
            ClsProductMaster ClsCustomer = new ClsProductMaster();
            this.ddlBillingDepotType.Items.Clear();
            this.ddlBillingDepotType.Items.Insert(0, new ListItem("Select Billing Depot", "-1"));
            this.ddlBillingDepotType.DataSource = ClsCustomer.BindDepotType();
            this.ddlBillingDepotType.DataTextField = "BILLINGDEPOTTYPE";
            this.ddlBillingDepotType.DataValueField = "BILLINGDEPOTVALUE";
            this.ddlBillingDepotType.DataBind();
            this.ddlBillingDepotType.SelectedValue = "1";
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion

    #region ddlBillingDepotType_SelectedIndexChanged
    protected void ddlBillingDepotType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (this.ddlBillingDepotType.SelectedValue.Trim() == "-1")
            {
                this.trDepotMap.Visible = false;
                this.trinvoice.Visible = false;
                this.gvBrandTypeMap.DataSource = null;
                this.gvBrandTypeMap.DataBind();
            }
            else if (this.ddlBillingDepotType.SelectedValue.Trim() == "1")
            {
                this.trDepotMap.Visible = false;
                this.trinvoice.Visible = false;
                if (Hdn_Fld.Value.Trim() == "")
                {
                    this.LoadDepotGrid("NA", "NA", "A");
                }
                else
                {
                    this.LoadDepotGrid(Hdn_Fld.Value.Trim(), this.ddlBillingDepotType.SelectedValue.Trim(), "U");
                }
            }
            else
            {
                if (Hdn_Fld.Value.Trim() == "")
                {
                    this.LoadDepotGrid("NA", "NA", "A");
                }
                else
                {
                    this.LoadDepotGrid(Hdn_Fld.Value.Trim(), this.ddlBillingDepotType.SelectedValue.Trim(), "U");
                }
                this.trDepotMap.Visible = true;
                this.trinvoice.Visible = true;
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region LoadDepot
    public void LoadDepotGrid(string ProductID, string DepotID, string Mode)
    {
        try
        {
            ClsProductMaster ClsCustomer = new ClsProductMaster();
            DataTable dtDepot = new DataTable();
            dtDepot = ClsCustomer.BindDepot(ProductID, DepotID, Mode);
            if (dtDepot.Rows.Count > 0)
            {
                this.gvBrandTypeMap.DataSource = dtDepot;
                this.gvBrandTypeMap.DataBind();
            }
            else
            {
                this.gvBrandTypeMap.DataSource = null;
                this.gvBrandTypeMap.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion

    #region RowDataBound
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblTAG = (e.Row.FindControl("lblTAG") as Label);
            CheckBox chkSelect = (e.Row.FindControl("chkSelect") as CheckBox);

            if (lblTAG.Text.Trim() == "0")
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    chkSelect.Checked = false;
                }
            }
            else
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    chkSelect.Checked = true;
                }
            }

        }
    }
    #endregion

    #region Create DataTable Structure
    public void CreateDepotMappingDataTable()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("DEPOTID", typeof(string)));
        dt.Columns.Add(new DataColumn("DEPOTNAME", typeof(string)));
        HttpContext.Current.Session["DEPOTMAPPING"] = dt;
    }
    #endregion

    #region Convert DataTable To XML
    public string ConvertDatatable(DataTable dt)
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

  
}