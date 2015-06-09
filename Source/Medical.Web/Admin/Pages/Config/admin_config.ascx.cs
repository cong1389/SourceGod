using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cb.Utility;
using Cb.DBUtility;
using Cb.BLL;
using Cb.Localization;
using System.Data;
using System.IO;
using System.Configuration;
using System.Xml;
using System.Xml.Linq;

namespace Cb.Web.Admin.Pages.Config
{
    public partial class admin_config : System.Web.UI.UserControl
    {
        #region Fields
        protected string template_path;

        private string filenameUploadHeader
        {
            get
            {
                if (ViewState["filenameUploadHeader"] != null)
                    return ViewState["filenameUploadHeader"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                ViewState["filenameUploadHeader"] = value;
            }
        }

        private string filenameUploadFooter
        {
            get
            {
                if (ViewState["filenameUploadFooter"] != null)
                    return ViewState["filenameUploadFooter"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                ViewState["filenameUploadFooter"] = value;
            }
        }

        private string filenameUploadLocation
        {
            get
            {
                if (ViewState["filenameUploadLocation"] != null)
                    return ViewState["filenameUploadLocation"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                ViewState["filenameUploadLocation"] = value;
            }
        }
        #endregion

        #region Common

        private void InitPage()
        {

            this.template_path = WebUtils.GetWebPath();
            this.regv_Email.ErrorMessage = Constant.UI.alert_invalid_email;

            //Init Validate string
            regv_Email.ValidationExpression = Constant.RegularExpressionString.validateEmail;

            ShowConfig();
            SetRoleMenu();
            GetWebconfig();
        }

        /// <summary>
        /// Phân quyền tài khoản Congtt full quyền, những tk còn lại k có quyền xóa và Edit
        /// </summary>
        private void SetRoleMenu()
        {
            Medical_User lst_user = (Medical_User)Session[Global.SESS_USER];
            if (lst_user.Username != "congtt")
            {
                tabWebconfig.Style.Add("display", "none");
            }
        }

        /// <summary>
        ///GetWebconfig
        /// </summary>
        private void GetWebconfig()
        {
            var myXml = WebUtils.LoadWebConfig();
            txtWebConfig.Value = myXml.ToXml();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isShowUplImg"></param>
        /// <param name="filename"></param>
        private void SetVisibleImg(bool isShowUplImg, string filename, FileUpload fuImage, Button btnUploadImage, LinkButton lbnView, LinkButton lbnDelete)
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

        /// <summary>
        /// ShowConfig
        /// </summary>
        /// 
        private void ShowConfig()
        {
            //List<sd_Configuration> lst = sd_Configuration.GetAll(new sd_Configuration(), string.Empty, null);
            ConfigurationBLL pcBll = new ConfigurationBLL();
            IList<Medical_Configuration> lst = pcBll.GetList();
            foreach (Medical_Configuration item in lst)
            {
                if (item.Key_name == Constant.Configuration.sitename)
                {
                    this.txt_config_sitename.Value = item.Value_name;
                }
                else if (item.Key_name == Constant.Configuration.email)
                {
                    this.txt_config_email.Value = item.Value_name;
                }
                else if (item.Key_name == Constant.Configuration.phone)
                {
                    this.txt_config_phone.Value = item.Value_name;
                }
                else if (item.Key_name == Constant.Configuration.fax)
                {
                    this.txtFax.Value = item.Value_name;
                }
                else if (item.Key_name == Constant.Configuration.skypeid)
                {
                    this.txtSkype.Value = item.Value_name;
                }
                else if (item.Key_name == Constant.Configuration.yahooid)
                {
                    this.txtYahoo.Value = item.Value_name;
                }
                else if (item.Key_name == Constant.Configuration.config_address_vi)
                {
                    this.txtAddress.Value = item.Value_name;
                }
                else if (item.Key_name == Constant.Configuration.config_address1_vi)
                {
                    this.txtAddress1.Value = item.Value_name;
                }
                else if (item.Key_name == Constant.Configuration.config_company_name_vi)
                {
                    this.txtCompanyName.Value = item.Value_name;
                }

                else if (item.Key_name == Constant.Configuration.config_title)
                {
                    this.txtTitle.Value = item.Value_name;
                }

                else if (item.Key_name == Constant.Configuration.config_metaDescription)
                {
                    this.txtMetaDescription.Value = item.Value_name;
                }

                else if (item.Key_name == Constant.Configuration.config_metaKeyword)
                {
                    this.txtMetaKeyword.Value = item.Value_name;
                }

                else if (item.Key_name == Constant.Configuration.config_logoHeader)
                {
                    filenameUploadHeader = item.Value_name;
                    if (!string.IsNullOrEmpty(filenameUploadHeader))
                        SetVisibleImg(false, string.Format("{0}/{1}", ConfigurationManager.AppSettings["AdvImageFolder"], filenameUploadHeader), fuImageHeader, btnUploadImageHeader, lbnViewHeader, lbnDeleteHeader);
                    else
                        SetVisibleImg(true, string.Empty, fuImageHeader, btnUploadImageHeader, lbnViewHeader, lbnDeleteHeader);
                }

                else if (item.Key_name == Constant.Configuration.config_logoFooter)
                {
                    filenameUploadFooter = item.Value_name;
                    if (!string.IsNullOrEmpty(filenameUploadFooter))
                        SetVisibleImg(false, string.Format("{0}/{1}", ConfigurationManager.AppSettings["AdvImageFolder"], filenameUploadFooter), fuImageFooter, btnUploadImageFooter, lbnViewFooter, lbnDeleteFooter);
                    else
                        SetVisibleImg(true, string.Empty, fuImageFooter, btnUploadImageFooter, lbnViewFooter, lbnDeleteFooter);
                }

                else if (item.Key_name == "config_location")
                {
                    filenameUploadLocation = item.Value_name;
                    if (!string.IsNullOrEmpty(filenameUploadLocation))
                        SetVisibleImg(false, string.Format("{0}/{1}", ConfigurationManager.AppSettings["AdvImageFolder"], filenameUploadLocation), fuLocation, btnUploadLocation, lbnViewLocation, lbnDeleteLocation);
                    else
                        SetVisibleImg(true, string.Empty, fuLocation, btnUploadLocation, lbnViewLocation, lbnDeleteLocation);
                }
            }
        }

        /// <summary>
        /// SaveConfig
        /// </summary>
        private void SaveConfig()
        {
            ConfigurationBLL cgBLL = new ConfigurationBLL();
            cgBLL.SaveConfig(this.txt_config_email.Value.Trim(), this.txt_config_phone.Value.Trim(), txt_config_sitename.Value.Trim(), txtFax.Value.Trim(), txtSkype.Value.Trim(), txtYahoo.Value.Trim(), txtCompanyName.Value.Trim(), txtAddress.Value.Trim(), txtAddress1.Value.Trim(), filenameUploadHeader, filenameUploadFooter, filenameUploadLocation
                , txtTitle.Value.Trim(), txtMetaDescription.Value.Trim(), txtMetaKeyword.Value.Trim());

        }

        /// <summary>
        /// CancelConfig
        /// </summary>
        private void CancelConfig()
        {
            ShowConfig();
        }

        #endregion

        #region Event

        protected void Page_Load(object sender, EventArgs e)
        {
            //check role
            Medical_User user = (Medical_User)Session[Global.SESS_USER];
            if (user != null && user.Role != DBConvert.ParseByte(Constant.Security.AdminRoleValue))
            {
                Response.Redirect(LinkHelper.GetAdminLink("home"));
            }
            //end

            if (!this.IsPostBack)
            {
                InitPage();
                LocalizationUtility.SetValueControl(this);
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
                SaveConfig();
            }
        }

        /// <summary>
        /// btn_Cancel_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Cancel_Click(object sender, ImageClickEventArgs e)
        {
            CancelConfig();
        }

        protected void btnUploadImageHeader_Click(object sender, EventArgs e)
        {
            try
            {
                if (fuImageHeader.HasFile)
                {
                    filenameUploadHeader = string.Format("{0}.{1}", DateTime.Now.Ticks + GenerateString.Generate(5), fuImageHeader.FileName.Split('.')[1]);
                    fuImageHeader.SaveAs(Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["AdvImageFolder"]), filenameUploadHeader));
                    SetVisibleImg(false, string.Format("{0}/{1}", ConfigurationManager.AppSettings["AdvImageFolder"], filenameUploadHeader), fuImageHeader, btnUploadImageHeader, lbnViewHeader, lbnDeleteHeader);
                }
            }
            catch (Exception ex)
            {
                Write2Log.WriteLogs("admin_editnews", "btnUploadImageHeader_Click", ex.ToString());
            }
        }

        protected void btnUploadImageFooter_Click(object sender, EventArgs e)
        {
            try
            {
                if (fuImageFooter.HasFile)
                {
                    filenameUploadFooter = string.Format("{0}.{1}", DateTime.Now.Ticks + GenerateString.Generate(5), fuImageFooter.FileName.Split('.')[1]);
                    fuImageFooter.SaveAs(Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["AdvImageFolder"]), filenameUploadFooter));
                    SetVisibleImg(false, string.Format("{0}/{1}", ConfigurationManager.AppSettings["AdvImageFolder"], filenameUploadFooter), fuImageFooter, btnUploadImageFooter, lbnViewFooter, lbnDeleteFooter);
                }
            }
            catch (Exception ex)
            {
                Write2Log.WriteLogs("admin_editnews", "btnUploadImageFooter_Click", ex.ToString());
            }
        }

        protected void btnUploadLocation_Click(object sender, EventArgs e)
        {
            try
            {
                if (fuLocation.HasFile)
                {
                    filenameUploadLocation = string.Format("{0}.{1}", DateTime.Now.Ticks + GenerateString.Generate(5), fuLocation.FileName.Split('.')[1]);
                    fuLocation.SaveAs(Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["AdvImageFolder"]), filenameUploadLocation));
                    SetVisibleImg(false, string.Format("{0}/{1}", ConfigurationManager.AppSettings["AdvImageFolder"], filenameUploadLocation), fuLocation, btnUploadLocation, lbnViewLocation, lbnDeleteLocation);
                }
            }
            catch (Exception ex)
            {
                Write2Log.WriteLogs("admin_editnews", "btnUploadLocation_Click", ex.ToString());
            }
        }

        protected void lbnDeleteImageHeader_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(filenameUploadHeader))
            {
                string path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["AdvImageFolder"]), filenameUploadHeader);
                if (File.Exists(path))
                    File.Delete(path);
                SetVisibleImg(true, string.Empty, fuImageHeader, btnUploadImageHeader, lbnViewHeader, lbnDeleteHeader);
            }
        }

        protected void lbnDeleteImageFooter_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(filenameUploadFooter))
            {
                string path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["AdvImageFolder"]), filenameUploadFooter);
                if (File.Exists(path))
                    File.Delete(path);
                SetVisibleImg(true, string.Empty, fuImageFooter, btnUploadImageFooter, lbnViewFooter, lbnDeleteFooter);
            }
        }

        protected void lbnDeleteLocation_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(filenameUploadLocation))
            {
                string path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings["AdvImageFolder"]), filenameUploadLocation);
                if (File.Exists(path))
                    File.Delete(path);
                SetVisibleImg(true, string.Empty, fuLocation, btnUploadLocation, lbnViewLocation, lbnDeleteLocation);
            }
        }

        /// <summary>
        /// Read Value by Key from Web.config
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGetValue_Click(object sender, EventArgs e)
        {
            txtWebConfigValue.Value = WebUtils.GetWebConfigKey(txtWebConfigKey.Value.Trim());
        }

        /// <summary>
        /// Write Value by Key from Web.config
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSetValue_Click(object sender, EventArgs e)
        {
            WebUtils.SetWebConfigKey(txtWebConfigKey.Value.Trim(), txtWebConfigValue.Value.Trim());
            GetWebconfig();
        }

        #endregion
    }
}