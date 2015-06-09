using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cb.DBUtility;
using Cb.Utility;
using Cb.Model;
using Cb.BLL;
using Cb.Localization;
using System.IO;

namespace Cb.Web.Admin.Pages.ManagementID
{
    public partial class admin_editmanagementid : System.Web.UI.UserControl
    {
        #region Parameter

        protected int productcategoryId = int.MinValue;
        protected string template_path;
        private ManagementIDBLL pcBll;
        private Generic<Medical_ManagementID> genericBLL;
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

        #region Common

        /// <summary>
        /// Show location
        /// </summary>
        private void ShowNewscategory()
        {
            if (this.productcategoryId != int.MinValue)
            {
                Medical_ManagementID productcatObj = new Medical_ManagementID();
                string[] fields = { "Id" };
                productcatObj.Id = this.productcategoryId;
                productcatObj = genericBLL.Load(productcatObj, fields);
                this.chkPublished.Checked = productcatObj.Published == "1" ? true : false;
                txtName.Value = productcatObj.Name;
                txtValue.Value = productcatObj.Value;
            }
        }

        /// <summary>
        /// get data for insert update
        /// </summary>
        /// <param name="userObj"></param>
        /// <returns></returns>
        private Medical_ManagementID getDataObjectParent(Medical_ManagementID productcatObj)
        {
            productcatObj.Published = chkPublished.Checked ? "1" : "0";
            productcatObj.UpdateDate = DateTime.Now;
            productcatObj.Name = txtName.Value;
            productcatObj.Value = txtValue.Value;
            return productcatObj;
        }

        /// <summary>
        /// Save location
        /// </summary>
        private void SaveNewsCategory()
        {
            Medical_ManagementID productcatObj = new Medical_ManagementID();
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
                    link = LinkHelper.GetAdminMsgLink("managementid", "delete");
                else
                    link = LinkHelper.GetAdminMsgLink("managementid", "delfail");
                url = Utils.CombineUrl(template_path, link);
                Response.Redirect(url);

            }
        }

        /// <summary>
        /// Cancel content
        /// </summary>
        private void CancelNewsCategory()
        {
            string url = LinkHelper.GetAdminLink("managementid");
            Response.Redirect(url);
        }

        /// <summary>
        /// Init page
        /// </summary>
        private void InitPage()
        {

            LocalizationUtility.SetValueControl(this);

        }

        private void GetId()
        {
            //get ID param 
            pcBll = new ManagementIDBLL();
            genericBLL = new Generic<Medical_ManagementID>();
            xmlBll = new XMLConfigBLL();
            string strID = Utils.GetParameter("id", string.Empty);
            this.productcategoryId = strID == string.Empty ? int.MinValue : DBConvert.ParseByte(strID);
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
                SaveNewsCategory();
                string url = LinkHelper.GetAdminMsgLink("managementid", "save");
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
                string url = LinkHelper.GetAdminLink("edit_managementid", this.productcategoryId);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alert"></param>
        private void Alert(string alert)
        {
            string script = string.Format("alert('{0}')", alert);
            ScriptManager.RegisterStartupScript(this, GetType(), "alertproductcategory", script, true);
        }

        #endregion
    }
}