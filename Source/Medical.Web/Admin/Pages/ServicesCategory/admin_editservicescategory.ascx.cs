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
using Cb.Utility;
using Cb.BLL;
using Cb.Localization;
using Cb.Model.Services;
using Cb.BLL.Services;

namespace Cb.Web.Admin.Pages.ServicesCategory
{
    public partial class admin_editservicescategory : System.Web.UI.UserControl
    {
        #region Fields
        protected int productcategoryId = int.MinValue;
        protected string template_path;
        private ServicesCategoryBLL pcBll;
        private Generic<Medical_ServicesCategory> genericBLL;
        private Generic<Medical_ServicesCategoryDesc> genericDescBLL;
        private Generic2C<Medical_ServicesCategory, Medical_ServicesCategoryDesc> generic2CBLL;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            btn_Delete.Attributes["onclick"] = string.Format("javascript:return confirm('{0}');", Constant.UI.admin_msg_confirm_delete_item);
            GetId();
            if (!IsPostBack)
            {
                InitPage();
                ShowServicescategory();
            }
        }

        #region Common
        
        /// <summary>
        /// getDataDropDownCategory
        /// </summary>
        /// <param name="_drp"></param>
        private void getDataDropDownCategory(DropDownList _drp)
        {
            int  total;
            string strTemp;
            _drp.Items.Clear();
            _drp.Items.Add(new ListItem(Constant.UI.admin_Category, "1"));
            IList<Medical_ServicesCategory> lst = pcBll.GetList(Constant.DB.LangId, string.Empty, 1, 300, out total);
            if (lst != null && lst.Count > 0)
            {
                foreach (Medical_ServicesCategory item in lst)
                {
                    strTemp = Utils.getScmplit(item.NewsCategoryDesc.Name, item.PathTreeDesc);
                    _drp.Items.Add(new ListItem(strTemp, DBConvert.ParseString(item.Id)));
                }
            }
        }
        
        /// <summary>
        /// Init page
        /// </summary>
        private void InitPage()
        {
            this.ltrAminHeaderName.Text = LocalizationUtility.GetText("strEditServicesCategory");
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
            this.getDataDropDownCategory(this.drpCategory);
        }

        private void GetId()
        {
            //get ID param 
            pcBll = new ServicesCategoryBLL();
            genericBLL = new Generic<Medical_ServicesCategory>();
            generic2CBLL = new Generic2C<Medical_ServicesCategory, Medical_ServicesCategoryDesc>();
            genericDescBLL = new Generic<Medical_ServicesCategoryDesc>();
            string strID = Utils.GetParameter("id", string.Empty);
            this.productcategoryId = strID == string.Empty ? int.MinValue : DBConvert.ParseByte(strID);
            this.template_path = WebUtils.GetWebPath();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Show newscategory
        /// </summary>
        private void ShowServicescategory()
        {
            if (this.productcategoryId != int.MinValue)
            {
                Medical_ServicesCategory productcatObj = new Medical_ServicesCategory();
                string[] fields = { "Id" };
                productcatObj.Id = this.productcategoryId;
                productcatObj = generic2CBLL.Load(productcatObj, fields, Constant.DB.LangId);
                this.chkPublished.Checked = productcatObj.Published == "1" ? true : false;
                this.drpCategory.SelectedValue = productcatObj.ParentId.ToString();
                //this.chkShowProduct.Checked = productcatObj.Published == "1" ? true : false;
                IList<Medical_ServicesCategoryDesc> lst = genericDescBLL.GetAllBy(new Medical_ServicesCategoryDesc(), string.Format(" where mainid = {0}", this.productcategoryId), null);
                foreach (Medical_ServicesCategoryDesc item in lst)
                {
                    switch (item.LangId)
                    {
                        case 1:
                            this.txtName.Value = item.Name;
                            //editDetailVi.Text = item.Name;
                            break;
                        case 2:
                            this.txtName_En.Value = item.Name;
                            //editDetailEn.Text = item.Intro;
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// get data for insert update
        /// </summary>
        /// <param name="userObj"></param>
        /// <returns></returns>
        private Medical_ServicesCategory getDataObjectParent(Medical_ServicesCategory productcatObj)
        {
            productcatObj.Published = chkPublished.Checked ? "1" : "0";
            productcatObj.UpdateDate = DateTime.Now;
            productcatObj.ParentId = DBConvert.ParseByte(drpCategory.SelectedValue);

            //productcatObj.Published = chkShowProduct.Checked ? "1" : "0";
            return productcatObj;
        }
        /// <summary>
        /// get data child for insert update
        /// </summary>
        /// <param name="contdescObj"></param>
        /// <returns></returns>
        private Medical_ServicesCategoryDesc getDataObjectChild(Medical_ServicesCategoryDesc productcatdescObj, int lang)
        {
            switch (lang)
            {
                case 1:
                    productcatdescObj.MainId = this.productcategoryId;
                    productcatdescObj.LangId = Constant.DB.LangId;
                    productcatdescObj.Name = SanitizeHtml.Sanitize(txtName.Value);
                    //productcatdescObj.Intro = editDetailVi.Text;
                    break;
                case 2:
                    productcatdescObj.MainId = this.productcategoryId;
                    productcatdescObj.LangId = Constant.DB.LangId_En;
                    productcatdescObj.Name = !string.IsNullOrEmpty(txtName_En.Value) ? SanitizeHtml.Sanitize(txtName_En.Value) : SanitizeHtml.Sanitize(txtName.Value);
                    //productcatdescObj.Intro = editDetailEn.Text;
                    break;
            }
            return productcatdescObj;
        }
        /// <summary>
        /// Save newscategory
        /// </summary>
        private void SaveNewsCategory()
        {
            Medical_ServicesCategory productcatObj = new Medical_ServicesCategory();
            Medical_ServicesCategoryDesc productcatObjVn = new Medical_ServicesCategoryDesc();
            Medical_ServicesCategoryDesc productcatObjEn = new Medical_ServicesCategoryDesc();
            if (this.productcategoryId == int.MinValue)
            {
                //get data insert
                productcatObj = this.getDataObjectParent(productcatObj);
                productcatObj.PostDate = DateTime.Now;
                productcatObj.Ordering = genericBLL.getOrdering();
                productcatObj.PathTreeDesc = "1";
                productcatObjVn = this.getDataObjectChild(productcatObjVn, Constant.DB.LangId);
                productcatObjEn = this.getDataObjectChild(productcatObjEn, Constant.DB.LangId_En);

                List<Medical_ServicesCategoryDesc> lst = new List<Medical_ServicesCategoryDesc>();
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
                productcatObj = this.getDataObjectParent(productcatObj);
                productcatObjVn = this.getDataObjectChild(productcatObjVn, Constant.DB.LangId);
                productcatObjEn = this.getDataObjectChild(productcatObjEn, Constant.DB.LangId_En);
                List<Medical_ServicesCategoryDesc> lst = new List<Medical_ServicesCategoryDesc>();
                lst.Add(productcatObjVn);
                lst.Add(productcatObjEn);
                //excute
                generic2CBLL.Update(productcatObj, lst, fields);
                //neu ve Published oo thay doi thi chay ham ChangeWithTransaction de doi Published cac con va cac product
                //if (publisheddOld != productcatObj.Published)
                //    Medical_ServicesCategory.ChangeWithTransaction(DBConvert.ParseString(this.productcategoryId), productcatObj.Published);
            }

        }
        /// <summary>
        /// delete newscategory
        /// </summary>
        /// <param name="cid"></param>
        private void deleteServicesCategory(string cid)
        {
            if (cid != null)
            {
                IList<Medical_ServicesCategory> lst = pcBll.GetAllChild(DBConvert.ParseInt(cid), false);

                if (lst != null && lst.Count > 0)
                {
                    string script = string.Format("alert('{0}')", Constant.UI.alert_invalid_delete_productcategory_exist_child);
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), script, true);
                }
                else
                {
                    string link, url;

                    if (generic2CBLL.Delete(cid))
                        link = LinkHelper.GetAdminLink("servicescategory", "delete");//string.Format(SiteNavigation.link_adminPage_productcategory_msg, "delete");
                    else
                        link = LinkHelper.GetAdminLink("servicescategory", "delfail");
                    url = Utils.CombineUrl(template_path, link);
                    Response.Redirect(url);
                }
            }
        }
        /// <summary>
        /// Cancel content
        /// </summary>
        private void CancelNewsCategory()
        {
            string url = LinkHelper.GetAdminLink("servicescategory");
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
                string url = LinkHelper.GetAdminLink("servicescategory");
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
                string url = LinkHelper.GetAdminLink("edit_servicescategory", this.productcategoryId);
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
            deleteServicesCategory(DBConvert.ParseString(this.productcategoryId));
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
            IList<Medical_ServicesCategory> lst = pcBll.GetAllChild(productcategoryId, true);
            if (lst != null && lst.Count > 0)
                foreach (Medical_ServicesCategory item in lst)
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

        //private bool CheckExistName(string value, int langId)
        //{
        //    bool re = false;
        //    int totalrow, total;
        //    Medical_ServicesCategoryDesc pcDesc = null;
        //    searchProductCategory searchProductCat = new searchProductCategory() { Id = DBConvert.ParseInt(drpCategory.SelectedValue) };
        //    List<searchProductCategory> lst = searchProductCategory.GetTree(DBConvert.ParseInt(drpCategory.SelectedValue), Constant.DSC.LangId, txtName.Value, int.MinValue, string.Empty, 1, 3000, out totalrow, out total);
        //    lst = lst ?? new List<searchProductCategory>();
        //    lst.Add(searchProductCat);
        //    //them
        //    if (productcategoryId == int.MinValue)
        //    {
        //        for (int i = 0; i < lst.Count; i++)
        //        {
        //            pcDesc = Medical_ServicesCategoryDesc.LoadByProductCategoryDescAndLangue(lst[i].Id, langId);
        //            if (pcDesc != null && pcDesc.Name == value)
        //            {
        //                re = true;
        //                break;
        //            }
        //        }
        //    }
        //    //sua
        //    else
        //        for (int i = 0; i < lst.Count; i++)
        //        {
        //            pcDesc = Medical_ServicesCategoryDesc.LoadByProductCategoryDescAndLangue(lst[i].Id, langId);
        //            if (pcDesc != null && lst[i].Id != productcategoryId && pcDesc.Name == value)
        //            {
        //                re = true;
        //                break;
        //            }
        //        }
        //    return re;
        //}

        //protected void csv_txtName_ServerValidate(object source, ServerValidateEventArgs args)
        //{
        //    args.IsValid = !CheckExistName(txtName.Value.Trim(), Constant.DSC.LangId);
        //    if (!args.IsValid)
        //        Alert(Constant.UI.alert_invalid_exist_name_category_vn);
        //}

        //protected void cus_txtName_En_ServerValidate(object source, ServerValidateEventArgs args)
        //{
        //    args.IsValid = !CheckExistName(txtName_En.Value.Trim(), Constant.DSC.LangIdEn);
        //    if (!args.IsValid)
        //        Alert(Constant.UI.alert_invalid_exist_name_category_en);
        //}

        #endregion
    }
}