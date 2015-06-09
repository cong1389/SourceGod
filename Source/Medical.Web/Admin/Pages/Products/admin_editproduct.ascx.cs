// =============================================
// Author:		Congtt
// Create date: 22/09/2014
// Description:	Edit danh sach sản phẩm
//    Cột Area lưu file name PDF
//    Cột Hot lưu sach hay nhat
//    Cột Feature lưu sach nổi bật
//    Cột Design lưu tagName
//    Cột Utility lưu tagUrl
// =============================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Cb.DBUtility;
using Cb.Utility;
using Cb.BLL;
using Cb.Localization;
using System.IO;
using Microsoft.Security.Application;
using Cb.Model.Products;
using Cb.BLL.Products;
using System.Diagnostics;
using System.Text;
using System.Net;
using Cb.Model;
using System.Drawing;

namespace Cb.Web.Admin.Pages.Products
{
    public partial class admin_editproduct : DGCUserControl
    {
        #region Parameter

        private Generic<Medical_Product> genericBLL;
        private Generic<Medical_ProductDesc> genericDescBLL;
        private Generic2C<Medical_Product, Medical_ProductDesc> generic2CBLL;

        protected int productcategoryId = int.MinValue;
        string productCategoryName = string.Empty;
        protected string template_path;

        private string filenameUpload
        {
            get
            {
                if (ViewState["filenameUpload"] != null)
                    return ViewState["filenameUpload"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                ViewState["filenameUpload"] = value;
            }
        }

        private string categoryId
        {
            get
            {
                if (ViewState["categoryId"] != null)
                    return ViewState["categoryId"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                ViewState["categoryId"] = value;
            }
        }

        private string id
        {
            get
            {
                if (ViewState["id"] != null)
                    return ViewState["id"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                ViewState["id"] = value;
            }
        }

        #endregion

        #region Common

        /// <summary>
        /// Init page
        /// </summary>
        private void InitPage()
        {
            reqv_txtNameVi.ErrorMessage = LocalizationUtility.GetText("msg_EnterTitle", Ci);

            //Validator control
            //reqE_Area.ValidationExpression = reqE_Price.ValidationExpression = req_BedRoom.ValidationExpression = reqE_BathRoom.ValidationExpression
            //    = req_Latitude.ValidationExpression = req_Longitude.ValidationExpression = Constant.RegularExpressionString.validateNumber;
            LocalizationUtility.SetValueControl(this);

            //load category
            GetDataDropDownCategory(this.drpCategory);

            #region Set thuoc tinh cho block_baseimage

            block_baseimage.ImagePath = block_uploadimage.ImagePath = ConfigurationManager.AppSettings["ProductUpload"];
            block_baseimage.MinWidth = ConfigurationManager.AppSettings["minwidth"];
            block_baseimage.MinHeigh = ConfigurationManager.AppSettings["minheight"];
            block_baseimage.MaxWidth = ConfigurationManager.AppSettings["maxwidth"];
            block_baseimage.MaxHeight = ConfigurationManager.AppSettings["maxheight"];
            block_baseimage.MaxWidthBox = ConfigurationManager.AppSettings["minwidthbox"];
            block_baseimage.MaxHeightBox = ConfigurationManager.AppSettings["maxheightbox"];

            block_uploadimage.CategoryId = categoryId.ToString();
            block_uploadimage.Id = id;
            #endregion

            BindCost();
        }

        /// <summary>
        /// GetId
        /// </summary>
        private void GetId()
        {
            genericBLL = new Generic<Medical_Product>();
            generic2CBLL = new Generic2C<Medical_Product, Medical_ProductDesc>();
            genericDescBLL = new Generic<Medical_ProductDesc>();
            categoryId = Utils.GetParameter("cid", string.Empty);
            id = Utils.GetParameter("id", string.Empty);
            this.productcategoryId = id == string.Empty ? int.MinValue : DBConvert.ParseInt(id);
            this.template_path = WebUtils.GetWebPath();
        }

        /// <summary>
        /// Xem PDF file
        /// </summary>
        /// <param name="fileName"></param>
        private void ViewPdf(string fileName)
        {
            //string path = Request.PhysicalApplicationPath;            
            //string url = Path.Combine(ConfigurationManager.AppSettings["ProductUpload"], fileName).Replace("/","\\");
            //url = Utils.CombineUrl(path, url);

            string url = string.Format("{0}/{1}", ConfigurationManager.AppSettings["ProductUpload"], fileName);
            url = Utils.CombineUrl(WebUtils.GetBaseUrl(), url);

            WebClient wc = new WebClient();
            Byte[] buffer = wc.DownloadData(url);
            if (buffer != null)
            {
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(buffer);
                Response.Flush();
                Response.End();
            }
        }

        /// <summary>
        /// UploadMp3
        /// </summary>
        /// <param name="fu"></param>
        /// <returns></returns>
        private string UploadMp3(FileUpload fu)
        {
            //File giữ nguyen định dạng
            string fileNameMp3 = string.Format("{0}{1}.{2}", WebUtils.GetFileName(fu.PostedFile.FileName.Split('.')[0]), DateTime.Now.ToString("ddMMyyyyhhmmss"), WebUtils.GetFileExtension(fu.FileName));
            string pathNameMp3 = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["ProductUpload"]), fileNameMp3);
            string fileNameOgg = pathNameMp3.Replace("mp3", "ogg").Replace("MP3", "ogg");
            //if (File.Exists(fileNameMp3))
            //{
            fu.SaveAs(pathNameMp3);
            fu.SaveAs(fileNameOgg);
            //}
            return fileNameMp3;
        }

        /// <summary>
        /// ViewMusic
        /// </summary>
        /// <param name="fu"></param>
        /// <returns></returns>
        private void ViewMusic(string fileName)
        {
            string path = Request.PhysicalApplicationPath;
            string url = string.Format("{0}/{1}", ConfigurationManager.AppSettings["ProductUpload"], fileName);
            url = Utils.CombineUrl(path, url);
            WebClient wc = new WebClient();
            Byte[] buffer = wc.DownloadData(url);
            if (buffer != null)
            {
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(buffer);
                Response.Flush();
                Response.End();
            }
        }

        /// <summary>
        /// Phân quyền tài khoản Congtt full quyền, những tk còn lại k có quyền xóa và edit
        /// </summary>
        private void SetRoleMenu()
        {
            Medical_User lst_user = (Medical_User)Session[Global.SESS_USER];
            if (lst_user.Username != "congtt")
                divPage.Visible = false;
        }

        /// <summary>
        /// Show location
        /// </summary>
        private void ShowNewscategory()
        {
            if (this.productcategoryId != int.MinValue)
            {
                Medical_Product productcatObj = new Medical_Product();
                string[] fields = { "Id" };
                productcatObj.Id = this.productcategoryId;
                productcatObj = generic2CBLL.Load(productcatObj, fields, Constant.DB.LangId);
                this.chkPublished.Checked = productcatObj.Published == "1" ? true : false;
                this.chkPublishedHot.Checked = productcatObj.Hot == "1" ? true : false;
                this.chkPublishedFeature.Checked = productcatObj.Feature == "1" ? true : false;

                txtBedRoom.Value = DBConvert.ParseString(productcatObj.Bedroom);
                txtBathRoom.Value = DBConvert.ParseString(productcatObj.Bathroom);
                txtCode.Value = DBConvert.ParseString(productcatObj.Code);
                txtStatus.Value = DBConvert.ParseString(productcatObj.Status);
                this.drpProvince.SelectedValue = productcatObj.Province == string.Empty ? "" : drpProvince.Items.FindByText(productcatObj.Province).Value;
                this.drpDistrict.SelectedValue = productcatObj.District == string.Empty ? "" : drpDistrict.Items.FindByText(productcatObj.District).Value;
                this.drpCategory.SelectedValue = productcatObj.CategoryId.ToString();
                txtPrice.Value = productcatObj.Price.ToString();
                drpCost.SelectedValue = DBConvert.ParseString(productcatObj.Cost);
                //txtArea.Value = productcatObj.Area.ToString();
                txtWebsite.Value = DBConvert.ParseString(productcatObj.Website);
                txtLatitude.Value = productcatObj.Latitude;
                txtLongitude.Value = productcatObj.Longitude;
                txtPage.Text = productcatObj.Page == "" ? ConfigurationManager.AppSettings["pagePathProductDetail"] : productcatObj.Page;
                block_baseimage.ImageName = productcatObj.Image;
                filenameUpload = productcatObj.Area;
                if (!string.IsNullOrEmpty(filenameUpload))
                {
                    string path = string.Format("{0}/{1}", ConfigurationManager.AppSettings["ProductUpload"], filenameUpload);
                    SetVisibleImg(false, Utils.CombineUrl(WebUtils.GetBaseUrl(), path));
                }
                else
                {
                    SetVisibleImg(true, string.Empty);
                }

                //this.chkShowProduct.Checked = productcatObj.Published == "1" ? true : false;
                IList<Medical_ProductDesc> lst = genericDescBLL.GetAllBy(new Medical_ProductDesc(), string.Format(" where mainid = {0}", this.productcategoryId), null);
                foreach (Medical_ProductDesc item in lst)
                {
                    switch (item.LangId)
                    {
                        case 1:
                            this.txtName.Value = item.Title;
                            this.txtIntro.Text = item.Brief;
                            this.editBriefVi.Text = item.Detail;
                            this.editPositionVi.Text = item.Position;
                            this.editUtilityVi.Text = item.Utility;
                            this.editPicturesVi.Text = item.Pictures;
                            this.editDesignVi.Text = item.Design;
                            this.editPaymentVi.Text = item.Payment;
                            this.editContactVi.Text = item.Contact;
                            this.txtMetaTitle.Text = item.MetaTitle;
                            this.txtMetaDescription.Text = item.Metadescription;
                            this.txtMetaKeyword.Text = item.MetaKeyword;
                            break;
                        case 2:
                            this.txtName_En.Value = item.Title;
                            this.txtIntroEn.Text = item.Brief;
                            this.editBriefEn.Text = item.Detail;
                            this.editUtilityEn.Text = item.Utility;
                            this.editPicturesEn.Text = item.Pictures;
                            this.editDesignEn.Text = item.Design;
                            this.editPaymentEn.Text = item.Payment;
                            this.editContactEn.Text = item.Contact;
                            this.txtMetaTitleEng.Text = item.MetaTitle;
                            this.txtMetaTitleEng.Text = item.Metadescription;
                            this.txtMetaTitleEng.Text = item.MetaKeyword;
                            break;
                    }
                }
            }
            else
                txtPage.Text = ConfigurationManager.AppSettings["PageCategoryDetail"];
        }

        /// <summary>
        /// BindCost
        /// </summary>
        private void BindCost()
        {
            drpCost.Items.Clear();
            drpCost.Items.Add(new ListItem(LocalizationUtility.GetText("strSelAItem"), string.Empty));
            string full;
            Type t = typeof(enuCostId);
            Array arr = Enum.GetValues(t);
            foreach (enuCostId enu in arr)
            {
                if (enu.ToString("d") != int.MinValue.ToString())
                {
                    full = string.Format("{0}_{1}", t.Name, enu.ToString());
                    drpCost.Items.Add(new ListItem(LocalizationUtility.GetText(full), enu.ToString("d")));
                }
            }
            drpCost.SelectedIndex = 1;
        }

        /// <summary>
        /// get data for insert update
        /// </summary>
        /// <param name="userObj"></param>
        /// <returns></returns>
        private Medical_Product GetDataObjectParent(Medical_Product productcatObj)
        {
            productcatObj.Published = chkPublished.Checked ? "1" : "0";
            productcatObj.Hot = chkPublishedHot.Checked ? "1" : "0";
            productcatObj.Feature = chkPublishedFeature.Checked ? "1" : "0";
            productcatObj.Price = txtPrice.Value;
            productcatObj.Cost = DBConvert.ParseInt(drpCost.SelectedValue);
            //productcatObj.Area = txtArea.Value;
            productcatObj.District = drpDistrict.SelectedItem == null ? string.Empty : drpDistrict.SelectedItem.Text;
            productcatObj.Bedroom = DBConvert.ParseInt(txtBedRoom.Value);
            productcatObj.Bathroom = DBConvert.ParseInt(txtBathRoom.Value);
            productcatObj.Code = txtCode.Value;
            productcatObj.Status = txtStatus.Value;
            productcatObj.Province = drpProvince.SelectedItem == null ? string.Empty : drpProvince.SelectedItem.Text;
            productcatObj.Website = txtWebsite.Value;
            productcatObj.UpdateDate = DateTime.Now;
            productcatObj.CategoryId = DBConvert.ParseInt(drpCategory.SelectedValue);
            productcatObj.Image = block_baseimage.ImageName;
            productcatObj.Area = filenameUpload.Trim();//PDF file
            productcatObj.Post = "1";//Đăng tin
            productcatObj.Latitude = txtLatitude.Value;//Vi do
            productcatObj.Longitude = txtLongitude.Value;//Kinh do   
            productcatObj.Page = txtPage.Text.Trim();
            return productcatObj;
        }

        /// <summary>
        /// get data child for insert update
        /// </summary>
        /// <param name="contdescObj"></param>
        /// <returns></returns>
        private Medical_ProductDesc GetDataObjectChild(Medical_ProductDesc productcatdescObj, int lang)
        {
            switch (lang)
            {
                case 1:
                    productcatdescObj.MainId = this.productcategoryId;
                    productcatdescObj.LangId = Constant.DB.LangId;
                    productcatdescObj.Title = SanitizeHtml.Sanitize(txtName.Value);
                    productcatdescObj.TitleUrl = Utils.RemoveUnicode(SanitizeHtml.Sanitize(txtName.Value));
                    productcatdescObj.Brief = txtIntro.Text;
                    productcatdescObj.Detail = editBriefVi.Text;
                    productcatdescObj.Position = editPositionVi.Text;
                    productcatdescObj.Utility = Utils.RemoveUnicode(editDesignVi.Text);
                    //productcatdescObj.Utility = editUtilityVi.Text;
                    productcatdescObj.Pictures = editPicturesVi.Text;
                    productcatdescObj.Design = editDesignVi.Text;
                    productcatdescObj.Payment = editPaymentVi.Text;
                    productcatdescObj.Contact = editContactVi.Text;
                    productcatdescObj.MetaTitle = txtMetaTitle.Text;
                    productcatdescObj.Metadescription = txtMetaDescription.Text;
                    productcatdescObj.MetaKeyword = txtMetaKeyword.Text;
                    break;
                case 2:
                    productcatdescObj.MainId = this.productcategoryId;
                    productcatdescObj.LangId = Constant.DB.LangId_En;
                    string title = string.IsNullOrEmpty(txtName_En.Value) ? SanitizeHtml.Sanitize(txtName.Value) : SanitizeHtml.Sanitize(txtName_En.Value);
                    productcatdescObj.Title = title;
                    productcatdescObj.TitleUrl = Utils.RemoveUnicode(title);
                    productcatdescObj.Brief = string.IsNullOrEmpty(txtIntroEn.Text) ? txtIntro.Text : txtIntroEn.Text;
                    productcatdescObj.Detail = string.IsNullOrEmpty(editUtilityEn.Text) ? editBriefVi.Text : editBriefEn.Text;

                    productcatdescObj.Position = string.IsNullOrEmpty(editPositionEn.Text) ? editPositionVi.Text : editPositionEn.Text;

                    productcatdescObj.Design = string.IsNullOrEmpty(editDesignEn.Text) ? editDesignVi.Text : editDesignEn.Text;
                    productcatdescObj.Pictures = string.IsNullOrEmpty(editPicturesEn.Text) ? editPicturesVi.Text : editPicturesEn.Text;
                    productcatdescObj.Payment = string.IsNullOrEmpty(editPaymentEn.Text) ? editPaymentVi.Text : editPaymentEn.Text;
                    productcatdescObj.Contact = string.IsNullOrEmpty(editContactEn.Text) ? editContactVi.Text : editContactEn.Text;

                    productcatdescObj.MetaTitle = string.IsNullOrEmpty(txtMetaTitleEng.Text) ? txtMetaTitle.Text : txtMetaTitleEng.Text;
                    productcatdescObj.Metadescription = string.IsNullOrEmpty(txtMetaDescriptionEng.Text) ? txtMetaDescription.Text : txtMetaDescriptionEng.Text;
                    productcatdescObj.MetaKeyword = string.IsNullOrEmpty(txtMetaKeywordEng.Text) ? txtMetaKeyword.Text : txtMetaKeywordEng.Text;

                    break;
            }
            return productcatdescObj;
        }

        /// <summary>
        /// Save location
        /// </summary>
        private void SaveProductCategory()
        {
            Medical_Product productcatObj = new Medical_Product();
            Medical_ProductDesc productcatObjVn = new Medical_ProductDesc();
            Medical_ProductDesc productcatObjEn = new Medical_ProductDesc();
            if (this.productcategoryId == int.MinValue)
            {
                //get data insert
                productcatObj = this.GetDataObjectParent(productcatObj);
                productcatObj.PostDate = DateTime.Now;
                productcatObj.Ordering = genericBLL.getOrdering();
                productcatObjVn = this.GetDataObjectChild(productcatObjVn, Constant.DB.LangId);
                productcatObjEn = this.GetDataObjectChild(productcatObjEn, Constant.DB.LangId_En);

                List<Medical_ProductDesc> lst = new List<Medical_ProductDesc>();
                lst.Add(productcatObjVn);
                lst.Add(productcatObjEn);
                //excute
                this.productcategoryId = generic2CBLL.Insert(productcatObj, lst);
            }
            else
            {
                string[] fields = { "Id" };
                productcatObj.Id = this.productcategoryId;
                productcatObj = genericBLL.Load(productcatObj, fields);
                string publisheddOld = productcatObj.Published;
                //get data update
                productcatObj = this.GetDataObjectParent(productcatObj);
                productcatObjVn = this.GetDataObjectChild(productcatObjVn, Constant.DB.LangId);
                productcatObjEn = this.GetDataObjectChild(productcatObjEn, Constant.DB.LangId_En);
                List<Medical_ProductDesc> lst = new List<Medical_ProductDesc>();
                lst.Add(productcatObjVn);
                lst.Add(productcatObjEn);
                //excute
                generic2CBLL.Update(productcatObj, lst, fields);
                //neu ve Published oo thay doi thi chay ham ChangeWithTransaction de doi Published cac con va cac product
                //if (publisheddOld != productcatObj.Published)
                //    Medical_Product.ChangeWithTransaction(DBConvert.ParseString(this.productcategoryId), productcatObj.Published);
            }

        }

        /// <summary>
        /// Delete Image In Folder
        /// </summary>
        private bool DeleteImage()
        {
            bool result = false;
            if (!string.IsNullOrEmpty(filenameUpload))
            {
                string f = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["ProductUpload"]), filenameUpload);
                if (File.Exists(f))
                {
                    try
                    {
                        File.Delete(f);
                        filenameUpload = null;
                        SetVisibleImg(true, string.Empty);
                        result = true;
                    }
                    catch
                    {
                        result = false;
                    }
                }
                else
                {
                    filenameUpload = null;
                    SetVisibleImg(true, string.Empty);
                }
            }
            return result;
        }

        /// <summary>
        /// Delete image in folder and database
        /// </summary>
        /// <param name="cid"></param>
        private void DeleteProductCategory(string cid)
        {
            string link, url;

            if (generic2CBLL.Delete(cid) && DeleteImage())
                link = LinkHelper.GetAdminLink("product", categoryId, "delete");
            else
                link = LinkHelper.GetAdminLink("product", categoryId, "delfail");
            url = Utils.CombineUrl(template_path, link);
            Response.Redirect(url);
        }

        /// <summary>
        /// Cancel content
        /// </summary>
        private void CancelNewsCategory()
        {
            string url = LinkHelper.GetAdminLink("product");
            Response.Redirect(url);
        }

        /// <summary>
        /// getDataDropDownCategory
        /// </summary>
        /// <param name="_drp"></param>
        public static void GetDataDropDownCategory(DropDownList _drp)
        {
            int totalrow;
            string strTemp;
            _drp.Items.Clear();
            _drp.Items.Add(new ListItem(Constant.UI.admin_Category, Constant.DSC.IdRootProductCategory.ToString()));
            ProductCategoryBLL ncBll = new ProductCategoryBLL();
            IList<Medical_ProductCategory> lst = ncBll.GetList(Constant.DB.LangId, string.Empty, 1, 300, out totalrow);
            if (lst != null && lst.Count > 0)
            {
                foreach (Medical_ProductCategory item in lst)
                {
                    strTemp = Utils.getScmplit(item.NewsCategoryDesc.Name, item.PathTree);
                    _drp.Items.Add(new ListItem(strTemp, DBConvert.ParseString(item.Id)));
                }
            }
            //_drp.SelectedIndex = _drp.Items.IndexOf(_drp.Items.FindByValue(ConfigurationManager.AppSettings["parentIdLeture"]));
        }

        /// <summary>
        ///Hien thi o upload hinh anh( true: chua upload hinh) 
        /// </summary>
        /// <param name="isShowUplImg"></param>
        /// <param name="filename"></param>
        private void SetVisibleImg(bool isShowUplImg, string filename)
        {
            if (isShowUplImg)
            {
                fuImage.Visible = btnUploadImage.Visible = true;
                lbnView.Visible = lbnDelete.Visible = false;
            }
            else
            {
                fuImage.Visible = btnUploadImage.Visible = false;
                //lbnView.Attributes["href"] = filename;
                lbnView.Visible = lbnDelete.Visible = true;
            }
        }

        #endregion

        #region Event

        protected void Page_Load(object sender, EventArgs e)
        {
            //AjaxPro.Utility.RegisterTypeForAjax(typeof(admin_editnews), this.Page);
            btn_Delete.Attributes["onclick"] = string.Format("javascript:return confirm('{0}');", Constant.UI.admin_msg_confirm_delete_item);
            GetId();
            if (!IsPostBack)
            {
                InitPage();
                ShowNewscategory();
            }
        }

        /// <summary>
        /// btn_Save_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Save_Click(object sender, ImageClickEventArgs e)
        {
            if (Page.IsValid)
            {
                SaveProductCategory();
                string url = LinkHelper.GetAdminMsgLink("product", categoryId, "save");
                Response.Redirect(url);
            }
        }

        /// <summary>
        /// btn_Apply_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Apply_Click(object sender, ImageClickEventArgs e)
        {
            if (Page.IsValid)
            {
                SaveProductCategory();
                string url = LinkHelper.GetAdminLink("edit_product", categoryId, productcategoryId.ToString());
                Response.Redirect(url);
            }
        }

        /// <summary>
        /// btn_Delete_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Delete_Click(object sender, ImageClickEventArgs e)
        {
            DeleteProductCategory(DBConvert.ParseString(this.productcategoryId));
        }

        /// <summary>
        /// btn_Cancel_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Cancel_Click(object sender, ImageClickEventArgs e)
        {
            CancelNewsCategory();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alert"></param>
        private void Alert(string alert)
        {
            string script = string.Format("alert('{0}')", alert);
            ScriptManager.RegisterStartupScript(this, GetType(), "alertproductcategory", script, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        protected void csv_drpCategory_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //args.IsValid = !CheckParentIsThisOrChild();
            //if (!args.IsValid)
            //    Alert(Constant.UI.alert_invalid_parent_productcategory);
        }

        /// <summary>
        /// UpLoad image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUploadImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (fuImage.HasFile)
                {
                    string extendFile = Path.GetExtension(fuImage.FileName).ToLower();

                    if (extendFile == ".pdf")
                    {
                        string fileName = string.Format("{0}{1}{2}", fuImage.PostedFile.FileName.Split('.')[0], DateTime.Now.ToString("ddMMyyyyhhmmss"), extendFile);
                        filenameUpload = fileName;//string.Format("{0}.{1}", GenerateString.Generate(10), fuImage.FileName.Split('.')[1]);
                        string path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["ProductUpload"]), filenameUpload);

                        fuImage.SaveAs(path);

                        //fuImage.SaveAs(path);
                        SetVisibleImg(false, string.Format("{0}/{1}", ConfigurationManager.AppSettings["ProductUpload"], filenameUpload));
                    }
                    else if (extendFile == ".mp3")
                    {
                        filenameUpload = UploadMp3(fuImage);
                        SetVisibleImg(false, filenameUpload);
                    }
                }
            }
            catch (Exception ex)
            {
                Write2Log.WriteLogs("admin_editproduct", "btnUploadImage_Click", ex.ToString());
            }
        }

        /// <summary>
        /// Event Delete image in folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbnDeleteImage_Click(object sender, EventArgs e)
        {
            DeleteImage();
        }

        protected void btnViewPdf_Click(object sender, EventArgs e)
        {
            if (filenameUpload.Contains(".pdf"))
            {
                ViewPdf(filenameUpload);
            }
            //if (filenameUpload.Contains(".mp3"))
            //{
            //    ViewMusic(filenameUpload);
            //}

        }

        #endregion
    }
}