using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Text;
using Cb.Utility;
using Cb.BLL;
using Cb.Model;
using Cb.DBUtility;
using System.Configuration;

namespace Cb.Web.Admin.Controls
{
    public partial class block_baseimage : System.Web.UI.UserControl
    {
        #region Parameter

        protected string minSize, maxSize, setSelect;

        public string MinWidth
        {
            get
            {
                if (ViewState["MinWidth"] != null)
                    return ViewState["MinWidth"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                ViewState["MinWidth"] = value;
            }
        }
        public string MinHeigh
        {
            get
            {
                if (ViewState["MinHeigh"] != null)
                    return ViewState["MinHeigh"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                ViewState["MinHeigh"] = value;
            }
        }

        public string MaxWidth
        {
            get
            {
                if (ViewState["MaxWidth"] != null)
                    return ViewState["MaxWidth"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                ViewState["MaxWidth"] = value;
            }
        }
        public string MaxHeight
        {
            get
            {
                if (ViewState["MaxHeight"] != null)
                    return ViewState["MaxHeight"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                ViewState["MaxHeight"] = value;
            }
        }

        public string MaxWidthBox
        {
            get
            {
                if (ViewState["MaxWidthBox"] != null)
                    return ViewState["MaxWidthBox"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                ViewState["MaxWidthBox"] = value;
            }
        }
        public string MaxHeightBox
        {
            get
            {
                if (ViewState["MaxHeightBox"] != null)
                    return ViewState["MaxHeightBox"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                ViewState["MaxHeightBox"] = value;
            }
        }

        public string ImageName
        {
            get
            {
                if (ViewState["ImageName"] != null)
                    return ViewState["ImageName"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                ViewState["ImageName"] = value;
            }
        }
        public string ImagePath
        {
            get
            {
                if (ViewState["ImagePath"] != null)
                {
                    return ViewState["ImagePath"].ToString();
                }
                else
                    return string.Empty;
            }
            set
            {
                ViewState["ImagePath"] = value;
            }
        }

        #endregion

        #region Common

        private void InitPage()
        {
            string url = string.Format("{0}/{1}", ImagePath, ImageName);
            url = WebUtils.GetUrlImage(ImagePath, ImageName);
              //url = Utils.CombineUrl(WebUtils.GetBaseUrl(), url);
            if (!string.IsNullOrEmpty(url))
            {
                imgCropped.ImageUrl = url;
                btnReset.Visible = true;

            }

            SetScriptCrop();
        }

        private void SetScriptCrop()
        {
            minSize = string.Format("minSize: [ {0}, {1} ]", MinWidth, MinHeigh);
            maxSize = string.Format("maxSize: [ {0}, {1} ]", MaxWidth, MaxHeight);

            setSelect = string.Format("setSelect: [ {0}, {1}, {2}, {3} ]", 0, 0, MaxWidthBox, MaxHeightBox);
        }

        #endregion

        #region Event

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitPage();
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            SetScriptCrop();
            btnReset_Click(null, null);
            //string fileName = string.Empty;
            string filePath = string.Empty;
            string extension = string.Empty;
            try
            {
                //Check if Fileupload control has file in it
                if (fuImage.HasFile)
                {
                    // Get selected image extension
                    extension = Path.GetExtension(fuImage.FileName).ToLower();
                    //Check image is of valid type or not
                    if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif" || extension == ".bmp")
                    {
                        //Cretae unique name for the file
                        ImageName = string.Format("{0}{1}{2}", fuImage.PostedFile.FileName.Split('.')[0], DateTime.Now.ToString("ddMMyyyyhhmmss"), extension);
                        //Create path for the image to store
                        filePath = Path.Combine(Server.MapPath(ImagePath), ImageName);
                        //Save image in folder
                        fuImage.SaveAs(filePath);
                        //Show the panel and load the uploaded image in image control.
                        pnlCrop.Visible = true;

                        string url = string.Format("{0}/{1}", ImagePath, ImageName);
                        imgToCrop.ImageUrl = Utils.CombineUrl(WebUtils.GetBaseUrl(), url);
                        //imgToCrop.ImageUrl = "~/Images/" + fileName;
                    }
                    else
                    {
                        lblMsg.Text = "Please select jpg, jpeg, png, gif or bmp file only";
                    }
                }
                else
                {
                    lblMsg.Text = "Please select file to upload";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Oops!! error occured : " + ex.Message.ToString();
            }
            finally
            {
                extension = string.Empty;
            }
        }

        protected void btnCrop_Click(object sender, EventArgs e)
        {
            string croppedFileName = string.Empty;
            string croppedFilePath = string.Empty;
            //get uploaded image name
            string fileName = Path.GetFileName(imgToCrop.ImageUrl);
            string filePath = imgToCrop.ImageUrl.Replace(WebUtils.GetBaseUrl(), "");

            //Check if file exists on the path i.e. in the UploadedImages folder.
            if (File.Exists(Server.MapPath(filePath)))
            {

                //Get the image from UploadedImages folder.
                System.Drawing.Image orgImg = System.Drawing.Image.FromFile(Server.MapPath(filePath));
                //Get user selected cropped area
                //Convert.ToInt32(String.Format("{0:0.##}", YCoordinate.Value)),

                Rectangle areaToCrop = new Rectangle(
                    Convert.ToInt32(XCoordinate.Value),
                    Convert.ToInt32(YCoordinate.Value),
                    Convert.ToInt32(Width.Value),
                    Convert.ToInt32(Height.Value));
                try
                {

                    Bitmap bitMap = new Bitmap(areaToCrop.Width, areaToCrop.Height);
                    //Create graphics object for alteration
                    using (Graphics g = Graphics.FromImage(bitMap))
                    {
                        //Draw image to screen
                        g.DrawImage(orgImg, new Rectangle(0, 0, bitMap.Width, bitMap.Height), areaToCrop, GraphicsUnit.Pixel);
                    }

                    //name the cropped image
                    ImageName = croppedFileName = "crop_" + fileName;

                    //Create path to store the cropped image
                    string path = filePath.Replace(fileName, "");
                    croppedFilePath = Path.Combine(Server.MapPath(path), croppedFileName);
                    //croppedFilePath = Path.Combine(Server.MapPath("/Admin/Images"), croppedFileName);
                    //save cropped image in folder

                    bitMap.Save(croppedFilePath);
                    orgImg.Dispose();
                    bitMap = null;
                    //Now you can delete the original uploaded image from folder                
                    File.Delete(Server.MapPath(filePath));
                    //Hide the panel
                    pnlCrop.Visible = false;
                    //Show success message in label
                    lblMsg.ForeColor = Color.Green;
                    lblMsg.Text = "Image cropped and saved successfully";

                    //Show cropped image 
                    //string path = string.Format("{0}/{1}", Constant.DSC.ProductUploadFolder, croppedFileName);
                    imgCropped.ImageUrl = Path.Combine(path, croppedFileName);

                    //Show Reset button
                    btnReset.Visible = true;
                }
                catch (Exception ex)
                {
                    lblMsg.Text = "Oops!! error occured : " + ex.Message.ToString();
                }
                finally
                {

                    croppedFileName = string.Empty;
                    croppedFilePath = string.Empty;
                }
            }

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            imgCropped.ImageUrl = "";
            lblMsg.Text = string.Empty;
            btnReset.Visible = false;
        }

        #endregion
    }
}