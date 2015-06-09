// =============================================
// Author:		Congtt
// Create date: 22/09/2014
// Description:Edit	danh sach danh mục sản phẩm
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
using Cb.Model;
using Cb.Model.Products;
using Cb.BLL.Products;
using System.IO;

namespace Cb.Web.Admin.Pages.ProductsCategory
{
    public partial class admin_editproductcategory : System.Web.UI.UserControl
    {
        #region Parameter

        private ProductCategoryBLL pcBll;
        private Generic<Medical_ProductCategory> genericBLL;
        private Generic<Medical_ProductCategoryDesc> genericDescBLL;
        private Generic2C<Medical_ProductCategory, Medical_ProductCategoryDesc> generic2CBLL;

        protected int productcategoryId = int.MinValue;
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

        #endregion

        #region Common

        /// <summary>
        /// Init page
        /// </summary>
        private void InitPage()
        {
            LocalizationUtility.SetValueControl(this);
            this.ltrAminPublish.Text = Constant.UI.admin_publish;
            this.ltrAdminApply.Text = Constant.UI.admin_apply;
            this.ltrAdminCancel.Text = Constant.UI.admin_cancel;
            this.ltrAdminDelete.Text = Constant.UI.admin_delete;
            this.ltrAdminSave.Text = Constant.UI.admin_save;
            this.ltrAminName.Text = Constant.UI.admin_name;
            this.ltrAminLangVi.Text = Constant.UI.admin_lang_Vi;
            this.ltrAminLangEn.Text = Constant.UI.admin_lang_En;
            this.ltrAminName_En.Text = Constant.UI.admin_name_en;
            this.ltrAminCategory.Text = Constant.UI.admin_Category;
            //this.ltrShowProduct.Text = Constant.UI.categoryProduct_ShowProduct;
            //this.ltrAdminIntro.Text = Constant.UI.categoryProduct_Intro;
            this.reqv_txtNameVi.ErrorMessage = Constant.UI.alert_empty_name;
            reqv_txtNameEn.ErrorMessage = Constant.UI.alert_empty_name_en;
            //load category
            this.GetDataDropDownCategory(this.drpCategory);

            SetRoleMenu();

        }

        /// <summary>
        /// Phân quyền tài khoản Congtt full quyền, những tk còn lại k có quyền xóa và edit
        /// </summary>
        private void SetRoleMenu()
        {
            Medical_User lst_user = (Medical_User)Session[Global.SESS_USER];
            if (lst_user.Username != "congtt")
                tdDelete.Visible = trPage.Visible = false;
        }

        /// <summary>
        /// Show newscategory
        /// </summary>
        private void ShowProductcategory()
        {
            if (this.productcategoryId != int.MinValue)
            {
                Medical_ProductCategory productcatObj = new Medical_ProductCategory();
                string[] fields = { "Id" };
                productcatObj.Id = this.productcategoryId;
                productcatObj = generic2CBLL.Load(productcatObj, fields, Constant.DB.LangId);
                this.chkPublished.Checked = productcatObj.Published == "1" ? true : false;
                this.drpCategory.SelectedValue = productcatObj.ParentId.ToString();
                block_baseimage.ImageName = productcatObj.BaseImage;
                txtPage.Text = productcatObj.Page;

                IList<Medical_ProductCategoryDesc> lst = genericDescBLL.GetAllBy(new Medical_ProductCategoryDesc(), string.Format(" where mainid = {0}", this.productcategoryId), null);
                foreach (Medical_ProductCategoryDesc item in lst)
                {
                    switch (item.LangId)
                    {
                        case 1:
                            txtName.Value = item.Name;
                            txtIntro.Text = item.Brief;
                            txtMetaTitle.Text = item.MetaTitle;
                            txtMetaKeyword.Text = item.MetaKeyword;
                            txtMetaDescription.Text = item.MetaDecription;
                            txtDetail.Text = item.Detail;
                            break;
                        case 2:
                            txtName_En.Value = item.Name;
                            txtIntro_Eng.Text = item.Brief;
                            txtMetaTitle_Eng.Text = item.MetaTitle;
                            txtMetaKeyword_Eng.Text = item.MetaKeyword;
                            txtMetaDescription_Eng.Text = item.MetaDecription;
                            txtDetail_Eng.Text = item.Detail;
                            break;
                    }
                }
            }
            else
                txtPage.Text = ConfigurationManager.AppSettings["PageCategory"];
        }

        /// <summary>
        /// get data for insert update
        /// </summary>
        /// <param name="userObj"></param>
        /// <returns></returns>
        private Medical_ProductCategory getDataObjectParent(Medical_ProductCategory productcatObj)
        {
            productcatObj.Published = chkPublished.Checked ? "1" : "0";
            productcatObj.UpdateDate = DateTime.Now;
            productcatObj.ParentId = DBConvert.ParseInt(drpCategory.SelectedValue);
            productcatObj.BaseImage = block_baseimage.ImageName;
            productcatObj.SmallImage = string.Empty;
            productcatObj.ThumbnailImage = string.Empty;
            productcatObj.Page = txtPage.Text.Trim();
            return productcatObj;
        }

        /// <summary>
        /// get data child for insert update
        /// </summary>
        /// <param name="contdescObj"></param>
        /// <returns></returns>
        private Medical_ProductCategoryDesc getDataObjectChild(Medical_ProductCategoryDesc productcatdescObj, int lang)
        {
            switch (lang)
            {
                case 1:
                    productcatdescObj.MainId = this.productcategoryId;
                    productcatdescObj.LangId = Constant.DB.LangId;
                    productcatdescObj.Name = SanitizeHtml.Sanitize(txtName.Value);
                    productcatdescObj.NameUrl = Utils.RemoveUnicode(SanitizeHtml.Sanitize(txtName.Value));
                    productcatdescObj.Brief = SanitizeHtml.Sanitize(txtIntro.Text);
                    productcatdescObj.MetaTitle = txtMetaTitle.Text.Trim();
                    productcatdescObj.MetaKeyword = txtMetaKeyword.Text.Trim();
                    productcatdescObj.MetaDecription = txtMetaDescription.Text.Trim();
                    productcatdescObj.Detail = SanitizeHtml.Sanitize(txtDetail.Text);

                    break;
                case 2:
                    productcatdescObj.MainId = this.productcategoryId;
                    productcatdescObj.LangId = Constant.DB.LangId_En;
                    string name = !string.IsNullOrEmpty(txtName_En.Value) ? SanitizeHtml.Sanitize(txtName_En.Value) : SanitizeHtml.Sanitize(txtName.Value);
                    productcatdescObj.Name = name;
                    productcatdescObj.NameUrl = Utils.RemoveUnicode(SanitizeHtml.Sanitize(name));
                    productcatdescObj.Brief = SanitizeHtml.Sanitize(txtIntro_Eng.Text);
                    productcatdescObj.MetaTitle = txtMetaTitle_Eng.Text.Trim();
                    productcatdescObj.MetaKeyword = txtMetaKeyword_Eng.Text.Trim();
                    productcatdescObj.MetaDecription = txtMetaDescription_Eng.Text.Trim();
                    productcatdescObj.Detail = SanitizeHtml.Sanitize(txtDetail_Eng.Text);

                    break;
            }
            return productcatdescObj;
        }

        /// <summary>
        /// Save newscategory
        /// </summary>
        private void SaveNewsCategory()
        {
            Medical_ProductCategory productcatObj = new Medical_ProductCategory();
            Medical_ProductCategoryDesc productcatObjVn = new Medical_ProductCategoryDesc();
            Medical_ProductCategoryDesc productcatObjEn = new Medical_ProductCategoryDesc();

            if (this.productcategoryId == int.MinValue)
            {
                //get data insert
                productcatObj = this.getDataObjectParent(productcatObj);
                productcatObj.PostDate = DateTime.Now;
                productcatObj.Ordering = genericBLL.getOrdering();
                productcatObj.PathTree = "1";
                productcatObjVn = this.getDataObjectChild(productcatObjVn, Constant.DB.LangId);
                productcatObjEn = this.getDataObjectChild(productcatObjEn, Constant.DB.LangId_En);

                List<Medical_ProductCategoryDesc> lst = new List<Medical_ProductCategoryDesc>();
                lst.Add(productcatObjVn);
                lst.Add(productcatObjEn);
                //excute
                this.productcategoryId = generic2CBLL.Insert(productcatObj, lst);

                //update pathtree sau khi insert
                DGCParameter[] param = new DGCParameter[2];
                param[0] = new DGCParameter("@parentId", DbType.Int32, productcatObj.ParentId);
                param[1] = new DGCParameter("@productCategoryId", DbType.Int32, productcategoryId);
                DBHelper.ExcuteFromStoreNonQuery("spUpdatePathTree", param);
            }
            else
            {
                string[] fields = { "Id" };
                productcatObj.Id = this.productcategoryId;
                productcatObj = genericBLL.Load(productcatObj, fields);
                string publisheddOld = productcatObj.Published;
                //get data update
                productcatObj = this.getDataObjectParent(productcatObj);
                productcatObjVn = this.getDataObjectChild(productcatObjVn, Constant.DB.LangId);
                productcatObjEn = this.getDataObjectChild(productcatObjEn, Constant.DB.LangId_En);
                List<Medical_ProductCategoryDesc> lst = new List<Medical_ProductCategoryDesc>();
                lst.Add(productcatObjVn);
                lst.Add(productcatObjEn);
                //excute
                generic2CBLL.Update(productcatObj, lst, fields);

                //update pathtree sau khi edit
                DGCParameter[] param = new DGCParameter[2];
                param[0] = new DGCParameter("@parentId", DbType.Int32, productcatObj.ParentId);
                param[1] = new DGCParameter("@productCategoryId", DbType.Int32, productcategoryId);
                DBHelper.ExcuteFromStoreNonQuery("spUpdatePathTree", param);
            }

        }

        /// <summary>
        /// delete newscategory
        /// </summary>
        /// <param name="cid"></param>
        private void DeleteProductCategory(string cid)
        {
            if (cid != null)
            {
                //IList<Medical_ProductCategory> lst = pcBll.GetAllChild(DBConvert.ParseInt(cid), false);

                //if (lst != null && lst.Count > 0)
                //{
                //    string script = string.Format("alert('{0}')", Constant.UI.alert_invalid_delete_productcategory_exist_child);
                //    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), script, true);
                //}
                //else
                //{
                string link, url;

                if (generic2CBLL.Delete(cid))
                    link = LinkHelper.GetAdminLink("productcategory", "delete");//string.Format(SiteNavigation.link_adminPage_productcategory_msg, "delete");
                else
                    link = LinkHelper.GetAdminLink("productcategory", "delfail");
                url = Utils.CombineUrl(template_path, link);
                Response.Redirect(url);
                //}
            }
        }

        /// <summary>
        /// Cancel content
        /// </summary>
        private void CancelNewsCategory()
        {
            string url = LinkHelper.GetAdminLink("productcategory");
            Response.Redirect(url);
        }

        /// <summary>
        /// getDataDropDownCategory
        /// </summary>
        /// <param name="_drp"></param>
        private void GetDataDropDownCategory(DropDownList _drp)
        {
            int total;
            string strTemp;
            _drp.Items.Clear();
            _drp.Items.Add(new ListItem(Constant.UI.admin_Category, Constant.DSC.IdRootProductCategory.ToString()));
            IList<Medical_ProductCategory> lst = pcBll.GetList(Constant.DB.LangId, string.Empty, 1, 300, out total);
            if (lst != null && lst.Count > 0)
            {
                foreach (Medical_ProductCategory item in lst)
                {
                    strTemp = Utils.getScmplit(item.NewsCategoryDesc.Name, item.PathTree);
                    _drp.Items.Add(new ListItem(strTemp, DBConvert.ParseString(item.Id)));
                }
            }
        }

        private void GetId()
        {
            #region Set thuoc tinh cho block_baseimage

            block_baseimage.ImagePath = ConfigurationManager.AppSettings["ProductsCategoryUpload"];
            block_baseimage.MinWidth = ConfigurationManager.AppSettings["minwidthProductCategory"];
            block_baseimage.MinHeigh = ConfigurationManager.AppSettings["minheightProductCategory"];
            block_baseimage.MaxWidth = ConfigurationManager.AppSettings["maxwidthProductCategory"];
            block_baseimage.MaxHeight = ConfigurationManager.AppSettings["maxheightProductCategory"];
            block_baseimage.MaxWidthBox = ConfigurationManager.AppSettings["minwidthboxProductCategory"];
            block_baseimage.MaxHeightBox = ConfigurationManager.AppSettings["maxheightboxProductCategory"];

            #endregion

            //get ID param 
            pcBll = new ProductCategoryBLL();
            genericBLL = new Generic<Medical_ProductCategory>();
            generic2CBLL = new Generic2C<Medical_ProductCategory, Medical_ProductCategoryDesc>();
            genericDescBLL = new Generic<Medical_ProductCategoryDesc>();
            string strID = Utils.GetParameter("cid", string.Empty);
            this.productcategoryId = strID == string.Empty ? int.MinValue : DBConvert.ParseInt(strID);
            this.template_path = WebUtils.GetWebPath();
        }

        #endregion

        #region Event

        protected void Page_Load(object sender, EventArgs e)
        {
            btn_Delete.Attributes["onclick"] = string.Format("javascript:return confirm('{0}');", Constant.UI.admin_msg_confirm_delete_item);
            GetId();
            if (!IsPostBack)
            {
                InitPage();
                ShowProductcategory();
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
                SaveNewsCategory();
                string url = LinkHelper.GetAdminLink("productcategory");
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
                string url = LinkHelper.GetAdminLink("edit_productcategory", this.productcategoryId);
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

        private bool CheckParentIsThisOrChild()
        {
            IList<Medical_ProductCategory> lst = pcBll.GetAllChild(productcategoryId, true);
            if (lst != null && lst.Count > 0)
                foreach (Medical_ProductCategory item in lst)
                {
                    if (item.Id == DBConvert.ParseInt(drpCategory.SelectedValue))
                        return true;
                }
            return false;
        }

        private void Alert(string alert)
        {
            string script = string.Format("alert('{0}')", alert);
            ScriptManager.RegisterStartupScript(this, GetType(), "alertproductcategory", script, true);
        }

        protected void csv_drpCategory_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !CheckParentIsThisOrChild();
            if (!args.IsValid)
                Alert(Constant.UI.alert_invalid_parent_productcategory);
        }

        #endregion
    }
}