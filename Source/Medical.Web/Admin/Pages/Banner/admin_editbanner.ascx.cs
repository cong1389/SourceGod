using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Cb.DBUtility;
using Cb.Utility;
using Cb.Utility;
using Cb.BLL;
using Cb.Localization;
using System.IO;
using System.Configuration;
using Cb.Model;


namespace Cb.Web.Admin.Pages.Banner
{
    public partial class admin_editbanner : System.Web.UI.UserControl
    {
        #region Fields
        protected int productcategoryId = int.MinValue;
        protected string template_path;
        private BannerBLL pcBll;
        private Generic<Medical_Banner> genericBLL;
        private XMLConfigBLL xmlBll;

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
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            btn_Delete.Attributes["onclick"] = string.Format("javascript:return confirm('{0}');", Constant.UI.admin_msg_confirm_delete_item);
            GetId();
            if (!IsPostBack)
            {
                InitPage();
                ShowNewscategory();
            }
        }

        #region Common

        private void ShowChkPage(string arrPage)
        {

            if (!string.IsNullOrEmpty(arrPage))
            {
                string[] arr = arrPage.Split(',');
                foreach (var item in arr)
                {
                    foreach (ListItem chk in chkPage.Items)
                    {
                        if (item == chk.Value)
                        {
                            chk.Selected = true;
                            break;
                        }
                    }
                }
            }
        }

        private void SaveChkPage(Medical_Banner obj)
        {
            string re = string.Empty;
            foreach (ListItem chk in chkPage.Items)
            {
                if (chk.Selected)
                {
                    re += string.Format("{0},", chk.Value);
                }
            }
            obj.ArrPageName = re.Remove(re.Length - 1);
        }

        private void BindPage()
        {
            xmlBll.getDataDropDownCategoryByAtt(chkPage, Constant.DSC.IdXmlPagePublish);
        }

        private void BindNewsCategory()
        {
            xmlBll.getDataDropDownCategory(drpCategory, Constant.DSC.IdXmlBannerPostion);
        }

        /// <summary>
        /// Init page
        /// </summary>
        private void InitPage()
        {

            LocalizationUtility.SetValueControl(this);
            //load category
            BindNewsCategory();
            BindPage();
        }

        private void GetId()
        {
            //get ID param 
            pcBll = new BannerBLL();
            genericBLL = new Generic<Medical_Banner>();
            xmlBll = new XMLConfigBLL();
            string strID = Utils.GetParameter("id", string.Empty);
            this.productcategoryId = strID == string.Empty ? int.MinValue : DBConvert.ParseByte(strID);
            this.template_path = WebUtils.GetWebPath();
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
                fuImage.Visible = true;
                btnUploadImage.Visible = true;
                lbnView.Visible = false;
                lbnDelete.Visible = false;
            }
            else
            {
                fuImage.Visible = false;
                btnUploadImage.Visible = false;
                lbnView.Attributes["href"] = filename;
                lbnView.Visible = true;
                lbnDelete.Visible = true;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Show location
        /// </summary>
        private void ShowNewscategory()
        {
            if (this.productcategoryId != int.MinValue)
            {
                Medical_Banner productcatObj = new Medical_Banner();
                string[] fields = { "Id" };
                productcatObj.Id = this.productcategoryId;
                productcatObj = genericBLL.Load(productcatObj, fields);
                this.chkPublished.Checked = productcatObj.Published == "1" ? true : false;
                chkOutPage.Checked = productcatObj.OutPage == 1 ? true : false;
                this.drpCategory.SelectedValue = productcatObj.Position.ToString();
                filenameUpload = productcatObj.Image;
                if (!string.IsNullOrEmpty(filenameUpload))
                    SetVisibleImg(false, WebUtils.GetUrlImage("UploadBanner", productcatObj.Image));
                else
                    SetVisibleImg(true, string.Empty);
                txtName.Value = productcatObj.Name;
                txtLink.Value = productcatObj.LinkUrl;
                ShowChkPage(productcatObj.ArrPageName);
            }
        }

        /// <summary>
        /// get data for insert update
        /// </summary>
        /// <param name="userObj"></param>
        /// <returns></returns>
        private Medical_Banner getDataObjectParent(Medical_Banner productcatObj)
        {
            productcatObj.Published = chkPublished.Checked ? "1" : "0";
            productcatObj.UpdateDate = DateTime.Now;
            productcatObj.Position = DBConvert.ParseByte(drpCategory.SelectedValue);
            productcatObj.Image = filenameUpload;
            productcatObj.Name = txtName.Value;
            productcatObj.OutPage = chkOutPage.Checked ? 1 : 0;
            productcatObj.LinkUrl = txtLink.Value;
            SaveChkPage(productcatObj);
            return productcatObj;
        }

        /// <summary>
        /// Save location
        /// </summary>
        private void SaveNewsCategory()
        {
            Medical_Banner productcatObj = new Medical_Banner();
            if (this.productcategoryId == int.MinValue)
            {
                //get data insert
                productcatObj = this.getDataObjectParent(productcatObj);
                productcatObj.PostDate = DateTime.Now;
                productcatObj.Ordering = genericBLL.getOrdering();

                
                //excute
                this.productcategoryId = genericBLL.Insert(productcatObj);
            }
            else
            {
                string[] fields = { "Id" };
                productcatObj.Id = this.productcategoryId;
                productcatObj = genericBLL.Load(productcatObj, fields);
                string publisheddOld = productcatObj.Published;
                //get data update
                productcatObj = this.getDataObjectParent(productcatObj);
                //excute
                genericBLL.Update(productcatObj, productcatObj, fields);

            }

        }

        /// <summary>
        /// delete location
        /// </summary>
        /// <param name="cid"></param>
        private void deleteNewsCategory(string cid)
        {
            if (cid != null)
            {

                string link, url;

                if (genericBLL.Delete(cid))
                    link = LinkHelper.GetAdminMsgLink("banner", "delete");
                else
                    link = LinkHelper.GetAdminMsgLink("banner", "delfail");
                url = Utils.CombineUrl(template_path, link);
                Response.Redirect(url);

            }
        }

        /// <summary>
        /// Cancel content
        /// </summary>
        private void CancelNewsCategory()
        {
            string url = LinkHelper.GetAdminLink("banner");
            Response.Redirect(url);
        }

        #endregion

        #region Event

        /// <summary>
        /// btn_Save_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Save_Click(object sender, ImageClickEventArgs e)
        {
            if (Page.IsValid)
            {
                SaveNewsCategory();
                string url = LinkHelper.GetAdminMsgLink("banner", "save");
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
                SaveNewsCategory();
                string url = LinkHelper.GetAdminLink("edit_banner", this.productcategoryId);
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
            deleteNewsCategory(DBConvert.ParseString(this.productcategoryId));
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

        protected void btnUploadImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (fuImage.HasFile)
                {
                    string extendFile = Path.GetExtension(fuImage.FileName);
                    string fileName = string.Format("{0}{1}{2}", fuImage.PostedFile.FileName.Split('.')[0], DateTime.Now.ToString("ddMMyyyyhhmmss"), extendFile);
                    filenameUpload = fileName;
                    //filenameUpload = string.Format("{0}{1}", GenerateString.Generate(10), Path.GetExtension(fuImage.FileName));
                    fuImage.SaveAs(Server.MapPath(Path.Combine(ConfigurationManager.AppSettings["UploadBanner"], filenameUpload)));
                    SetVisibleImg(false, WebUtils.GetUrlImage("UploadBanner", filenameUpload));
                }
            }
            catch (Exception ex)
            {
                Write2Log.WriteLogs("admin_editbanner", "btnUploadImage_Click", ex.ToString());
            }
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

        protected void lbnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(filenameUpload))
            {
                SetVisibleImg(true, string.Empty);
            }
        }

        #endregion
    }
}