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
using Cb.Model.Services;
using Cb.BLL.Services;

namespace Cb.Web.Admin.Pages.Services
{
    public partial class admin_editservices : DGCUserControl
    {
        #region Fields
        protected int productcategoryId = int.MinValue;
        protected string template_path;
       
        private Generic<Medical_Services> genericBLL;
        private Generic<Medical_ServicesDesc> genericDescBLL;
        private Generic2C<Medical_Services, Medical_ServicesDesc> generic2CBLL;
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
            //AjaxPro.Utility.RegisterTypeForAjax(typeof(admin_editnews), this.Page);
            btn_Delete.Attributes["onclick"] = string.Format("javascript:return confirm('{0}');", Constant.UI.admin_msg_confirm_delete_item);
            GetId();
            if (!IsPostBack)
            {
                InitPage();
                ShowNewscategory();
            }
        }

        #region Common
        /// <summary>
        /// getScmplit
        /// </summary>
        /// <param name="lvl"></param>
        /// <returns></returns>
        //private static string getScmplit(string name, string pathTree)
        //{
        //    string re = string.Empty;
        //    int count = pathTree.Count(i => i.Equals('.'));
        //    for (int i = 0; i < count; i++)
        //    {
        //        re += " &nbsp;&nbsp;&nbsp;";
        //    }
        //    re += " |" + count + "|&nbsp;" + name;
        //    return re;
        //}
        /// <summary>
        /// getDataDropDownCategory
        /// </summary>
        /// <param name="_drp"></param>
        public static void getDataDropDownCategory(DropDownList _drp)
        {
            int totalrow;
            string strTemp;
            _drp.Items.Clear();
            _drp.Items.Add(new ListItem(Constant.UI.admin_Category, "1"));
            ServicesCategoryBLL ncBll = new ServicesCategoryBLL();
            IList<Medical_ServicesCategory> lst = ncBll.GetList(Constant.DB.LangId, string.Empty, 1, 300, out totalrow);
            if (lst != null && lst.Count > 0)
            {
                foreach (Medical_ServicesCategory item in lst)
                {
                    strTemp = Utils.getScmplit(item.NewsCategoryDesc.Name, item.PathTreeDesc);
                    _drp.Items.Add(new ListItem(strTemp, DBConvert.ParseString(item.Id)));
                }
            }
            _drp.SelectedIndex = 1;

        }

        /// <summary>
        /// Init page
        /// </summary>
        private void InitPage()
        {
            //ckfinder.FileBrowser _FileBrowser = new CKFinder.FileBrowser();
            //_FileBrowser.BasePath = "/ckfinder/";
            //_FileBrowser.SetupCKEditor(CKEditorControl1);
            reqv_txtNameVi.ErrorMessage = LocalizationUtility.GetText("msg_EnterTitle", Ci);
            LocalizationUtility.SetValueControl(this);
            //load category
            getDataDropDownCategory(this.drpCategory);
        }

        private void GetId()
        {
            //get ID param          
            genericBLL = new Generic<Medical_Services>();
            generic2CBLL = new Generic2C<Medical_Services, Medical_ServicesDesc>();
            genericDescBLL = new Generic<Medical_ServicesDesc>();
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
                Medical_Services productcatObj = new Medical_Services();
                string[] fields = { "Id" };
                productcatObj.Id = this.productcategoryId;
                productcatObj = generic2CBLL.Load(productcatObj, fields, Constant.DB.LangId);
                this.chkPublished.Checked = productcatObj.Published == "1" ? true : false;
                txtPhone.Value = productcatObj.Phone;
                this.drpCategory.SelectedValue = productcatObj.CategoryId.ToString();
                filenameUpload = productcatObj.Image;
                if (!string.IsNullOrEmpty(filenameUpload))
                    SetVisibleImg(false, WebUtils.GetBaseUrl() + string.Format("{0}/{1}", Constant.DSC.ServicesUploadFolder, filenameUpload));
                else
                    SetVisibleImg(true, string.Empty);
                //this.chkShowProduct.Checked = productcatObj.Published == "1" ? true : false;
                IList<Medical_ServicesDesc> lst = genericDescBLL.GetAllBy(new Medical_ServicesDesc(), string.Format(" where mainid = {0}", this.productcategoryId), null);
                foreach (Medical_ServicesDesc item in lst)
                {
                    switch (item.LangId)
                    {
                        case 1:
                            this.txtName.Value = item.Title;
                            this.txtIntro.Text = item.Brief;
                            this.editBriefVi.Text = item.Detail;
                            //this.editDetailVi.Text = item.Detail;
                            break;
                        case 2:
                            this.txtName_En.Value = item.Title;
                            this.txtIntroEn.Text = item.Brief;
                            this.editBriefEn.Text = item.Detail;
                            //this.editDetailEn.Text = item.Detail;
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
        private Medical_Services getDataObjectParent(Medical_Services productcatObj)
        {
            productcatObj.Phone = this.txtPhone.Value;
            productcatObj.Published = chkPublished.Checked ? "1" : "0";
            productcatObj.UpdateDate = DateTime.Now;
            productcatObj.CategoryId = DBConvert.ParseByte(drpCategory.SelectedValue);
            productcatObj.Image = filenameUpload;
            return productcatObj;
        }
        /// <summary>
        /// get data child for insert update
        /// </summary>
        /// <param name="contdescObj"></param>
        /// <returns></returns>
        private Medical_ServicesDesc getDataObjectChild(Medical_ServicesDesc productcatdescObj, int lang)
        {
            switch (lang)
            {
                case 1:
                    productcatdescObj.MainId = this.productcategoryId;
                    productcatdescObj.LangId = Constant.DB.LangId;
                    //string str = SanitizeHtml.Sanitize(txtName.Value);

                    productcatdescObj.Title = SanitizeHtml.Sanitize(txtName.Value);
                    productcatdescObj.Brief = SanitizeHtml.Sanitize(txtIntro.Text);
                    productcatdescObj.Detail = Sanitizer.GetSafeHtml(editBriefVi.Text);

                    break;
                case 2:
                    productcatdescObj.MainId = this.productcategoryId;
                    productcatdescObj.LangId = Constant.DB.LangId_En;
                    productcatdescObj.Title = string.IsNullOrEmpty(txtName_En.Value) ? SanitizeHtml.Sanitize(txtName.Value) : SanitizeHtml.Sanitize(txtName_En.Value);
                    productcatdescObj.Brief = string.IsNullOrEmpty(txtIntroEn.Text) ? SanitizeHtml.Sanitize(txtIntro.Text) : SanitizeHtml.Sanitize(txtIntroEn.Text);
                    productcatdescObj.Detail = string.IsNullOrEmpty(editBriefEn.Text) ? SanitizeHtml.Sanitize(editBriefVi.Text) : Sanitizer.GetSafeHtml(editBriefEn.Text);

                    break;
            }
            return productcatdescObj;
        }
        /// <summary>
        /// Save location
        /// </summary>
        private void SaveServicesCategory()
        {
            Medical_Services productcatObj = new Medical_Services();
            Medical_ServicesDesc productcatObjVn = new Medical_ServicesDesc();
            Medical_ServicesDesc productcatObjEn = new Medical_ServicesDesc();
            if (this.productcategoryId == int.MinValue)
            {
                //get data insert
                productcatObj = this.getDataObjectParent(productcatObj);
                productcatObj.PostDate = DateTime.Now;
                productcatObj.Ordering = genericBLL.getOrdering();
                productcatObjVn = this.getDataObjectChild(productcatObjVn, Constant.DB.LangId);
                productcatObjEn = this.getDataObjectChild(productcatObjEn, Constant.DB.LangId_En);

                List<Medical_ServicesDesc> lst = new List<Medical_ServicesDesc>();
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
                List<Medical_ServicesDesc> lst = new List<Medical_ServicesDesc>();
                lst.Add(productcatObjVn);
                lst.Add(productcatObjEn);
                //excute
                generic2CBLL.Update(productcatObj, lst, fields);
                //neu ve Published oo thay doi thi chay ham ChangeWithTransaction de doi Published cac con va cac product
                //if (publisheddOld != productcatObj.Published)
                //    Medical_Services.ChangeWithTransaction(DBConvert.ParseString(this.productcategoryId), productcatObj.Published);
            }

        }
        /// <summary>
        /// delete location
        /// </summary>
        /// <param name="cid"></param>
        private void deleteServicesCategory(string cid)
        {
            if (cid != null)
            {

                string link, url;

                if (generic2CBLL.Delete(cid))
                    link = LinkHelper.GetAdminLink("services", "delete");
                else
                    link = LinkHelper.GetAdminLink("services", "delfail");
                url = Utils.CombineUrl(template_path, link);
                Response.Redirect(url);

            }
        }
        /// <summary>
        /// Cancel content
        /// </summary>
        private void CancelNewsCategory()
        {
            string url = LinkHelper.GetAdminLink("services");
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
                SaveServicesCategory();
                string url = LinkHelper.GetAdminMsgLink("services", "save");
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
                SaveServicesCategory();
                string url = LinkHelper.GetAdminLink("edit_services", this.productcategoryId);
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


        protected void btnUploadImage_Click(object sender, EventArgs e)
        {
           
            try
            {
                if (fuImage.HasFile)
                {
                  
                    filenameUpload = string.Format("{0}.{1}", GenerateString.Generate(10), fuImage.FileName.Split('.')[1]);
                    //string str = Path.Combine(Request.PhysicalApplicationPath, Constant.DSC.NewsUploadFolder.Replace("/", "\\") + filenameUpload);
                    fuImage.SaveAs(Path.Combine(Server.MapPath(Constant.DSC.ServicesUploadFolder), filenameUpload));
                   
                   
                    //string strTemp = string.Format("<a class='zoom-image' href='{0}''>&nbsp;{1}</a>", Utils.CombineUrl(template_path, string.Format("{0}/{1}", Constant.DSC.NewsUploadFolder.Replace("\\", "/"), filename)), LocalizationUtility.GetText("strView"));
                    //strTemp += string.Format("<a href='{0}' >{1}</a>",LocalizationUtility.GetText("strDelete"));
                    //ltrImage.Text = strTemp;
                    SetVisibleImg(false, string.Format("{0}/{1}", Constant.DSC.ServicesUploadFolder, filenameUpload));
                }
            }
            catch (Exception ex)
            {
                Write2Log.WriteLogs("admin_editnews", "btnUploadImage_Click", ex.ToString());
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


        protected void lbnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(filenameUpload))
            {
                SetVisibleImg(true, string.Empty);

                filenameUpload = string.Empty; string f = Path.Combine(Server.MapPath(Constant.DSC.ServicesUploadFolder), filenameUpload);
                if (File.Exists(f))
                {
                    try
                    {
                        File.Delete(f);
                    }
                    catch { }
                }
            }
        }

        #endregion
    }
}