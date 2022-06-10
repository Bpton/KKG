using PPBLL;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

public partial class VIEW_frmQCCOAFileDownload_FAC : System.Web.UI.Page
{
    string Mode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            DocumentUpload.Attributes.Add("onchange", "SetFileName('" + DocumentUpload.ClientID + "','" + Insuranceimage.ClientID + "')");

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["QCID"].ToString().Trim()))
                {
                    Session["QCID"] = Request.QueryString["QCID"];
                    Mode = Request.QueryString["MODE"];
                    if (Mode == "QC")
                    {
                        //this.Tdlblupload.Visible = false;
                        //this.TdDocumentUpload.Visible = false;
                        //this.TdButtonEvent.Visible = false;
                        //this.lbluploadbtn.Visible = false;
                        this.LoadUploadGrid();
                    }
                    else
                    {
                        Tdlblupload.Visible = false;
                        TdDocumentUpload.Visible = false;
                        TdButtonEvent.Visible = false;
                        LoadUploadGrid();
                    }
                }
                else
                {
                    if (Session["UPLOADFILENAMECOA"] == null)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("filename", typeof(string));
                        HttpContext.Current.Session["UPLOADFILENAMECOA"] = dt;
                    }
                    gvFiles.DataSource = Session["UPLOADFILENAMECOA"];
                    gvFiles.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    public void imageload()
    {
        try
        {
            string currentpath = Server.MapPath("~/IMG_INSURANCE/") + Path.GetFileName(DocumentUpload.FileName);
            Session["imagename"] = DocumentUpload.FileName;
            Session["imageurl"] = currentpath;
            MessageBox.Show("File uploaded successfully!");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void LoadUploadGrid()
    {
        try
        {
            ClsMMPPQualityControl clsqc = new ClsMMPPQualityControl();
            DataTable fileList = clsqc.GetFile_COA(Session["QCID"].ToString());
            Session["UPLOADFILENAMECOA"] = fileList;
            gvFiles.DataSource = fileList;
            gvFiles.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private string Random()
    {
        try
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            return finalString;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnInsuranceDocument_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();

            HttpFileCollection files = Request.Files;
            dt.Columns.Add("filename");
            //dt.Columns.Add("path");
            DataRow _uploadfile = dt.NewRow();

            foreach (string fileTagName in files)
            {
                HttpPostedFile file = Request.Files[fileTagName];
                if (file.ContentLength > 0)
                {
                    if (DocumentUpload.HasFile)
                    {

                        string filename = Random() + Path.GetFileName(DocumentUpload.FileName);

                        DocumentUpload.SaveAs(Server.MapPath("~/FileUpload/") + filename);

                        if (Session["UPLOADFILENAMECOA"] != null)
                        {
                            dt1 = (DataTable)Session["UPLOADFILENAMECOA"];
                        }

                        _uploadfile["filename"] = filename;


                        dt.Rows.Add(_uploadfile);
                        dt1.Merge(dt);

                        gvFiles.DataSource = dt1;
                        gvFiles.DataBind();
                        Session["UPLOADFILENAMECOA"] = dt1;
                        // MessageBox.Show("File uploaded successfully");
                        MessageBox1.ShowSuccess("File uploaded successfully");
                    }
                    else
                    {
                        MessageBox1.ShowInfo("Please Browse 1 file!");
                    }
                }
                else
                {
                    MessageBox1.ShowInfo("No file selected!");
                }
            }

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void DownloadFile(object sender, EventArgs e)
    {
        try
        {
            string filename = (sender as LinkButton).CommandArgument;
            /*Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
            Response.WriteFile(Server.MapPath("~/FileUpload/") + filename);
            Response.End();*/

            string path = (Server.MapPath("~/FileUpload/") + filename);
            System.IO.FileInfo file = new System.IO.FileInfo(path);
            if (file.Exists)
            {
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.WriteFile(file.FullName);
                Response.End();

            }
            else
            {
                MessageBox.Show("This file does not exist.");
            }

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void DeleteFile(object sender, EventArgs e)
    {
        try
        {
            DataTable dt1 = new DataTable();
            string filename = (sender as LinkButton).CommandArgument;


            if (Session["UPLOADFILENAMECOA"] != null)
            {
                dt1 = (DataTable)Session["UPLOADFILENAMECOA"];
            }

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                if (dt1.Rows[i]["filename"].ToString().Trim() == filename.ToString().Trim())
                {
                    dt1.Rows[i].Delete();
                    dt1.AcceptChanges();
                    File.Delete(Server.MapPath("~/FileUpload/") + filename);
                    //MessageBox.Show("File deleted successfully");
                    MessageBox1.ShowSuccess("File deleted successfully");
                }
            }
            gvFiles.DataSource = dt1;
            gvFiles.DataBind();
            Session["UPLOADFILENAMECOA"] = dt1;
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            //ClsQualityControl clsqc = new ClsQualityControl();
            DataTable dt = new DataTable();
            string Mode = string.Empty;

            if (Session["UPLOADFILENAMECOA"] != null)
            {
                dt = (DataTable)Session["UPLOADFILENAMECOA"];
            }
            if (dt.Rows.Count > 0)
            {
                string close = @"<script type='text/javascript'>
                                window.returnValue = true;
                                window.close();
                                </script>";
                base.Response.Write(close);
            }
            else
            {
                MessageBox1.ShowInfo("<b>Please upload atleast 1 file!</b>");
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnuploadcancel_Click(object sender, EventArgs e)
    {
        try
        {
            string close = @"<script type='text/javascript'>
                                window.returnValue = true;
                                window.close();
                                </script>";
            base.Response.Write(close);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
}