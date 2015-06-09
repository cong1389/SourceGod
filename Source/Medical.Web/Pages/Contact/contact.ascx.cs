using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using Cb.Utility;
using Cb.BLL;
using Cb.Localization;
using System.IO;
using Cb.DBUtility;


namespace Cb.Web.Pages.Contact
{
    public partial class contact : DGCUserControl
    {
        #region KHAI BAO BIEN TOAN CUC
        protected string show_msg;
        protected string securityCode;
        private string langId;
        CultureInfo ci = null;
        private int langInt;
        private string template_path;
        private Generic<Medical_Configuration> genericBLL = new Generic<Medical_Configuration>();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            GetLang();

            if (!Page.IsPostBack)
            {
                InitPage();
                ShowConfig();
                GenSecurityCode();
            }
        }

        #region GetLang
        private void GetLang()
        {
            ltrContact.Text = LocalizationUtility.GetText(ltrContact.ID, Ci).ToUpper();
            ltrSupport.Text = LocalizationUtility.GetText("strSupport", Ci);
            ltrCode.Text = LocalizationUtility.GetText(ltrCode.ID, Ci);
            btn_Send.Text = LocalizationUtility.GetText(btn_Send.ID, Ci);


            langId = Utils.GetParameter("langid", Constant.DB.langVn);
            this.ci = WebUtils.getResource(langId);
            langInt = langId == Constant.DB.langVn ? 1 : 2;
            template_path = WebUtils.GetWebPath();
        }
        #endregion

        #region InitPage

        protected void InitPage()
        {
            RequiredFieldValidator1.ToolTip = Constant.UI.alert_empty_email;
            RegularExpressionValidator1.ToolTip = Constant.UI.alert_invalid_email;
            ltrAddress.Text = LocalizationUtility.GetText(ltrAddress.ID, Ci);
            ltrPhone.Text = LocalizationUtility.GetText(ltrPhone.ID, Ci);
            LocalizationUtility.SetValueControl(this);
        }

        #endregion

        #region ShowConfig

        private void ShowConfig()
        {
            ConfigurationBLL pcBll = new ConfigurationBLL();
            IList<Medical_Configuration> lst = pcBll.GetList();
            if (lst != null && lst.Count > 0)
            {
                foreach (Medical_Configuration item in lst)
                {
                    if (langInt == 1)
                    {
                        if (item.Key_name == Constant.Configuration.config_company_name_vi)
                        {
                            ltrCompanyName.Text = item.Value_name;
                        }
                        else if (item.Key_name == Constant.Configuration.config_address_vi)
                        {
                            ltrAdderss1.Text = item.Value_name;
                        }
                    }
                    else if (langInt == 2)
                    {
                        if (item.Key_name == Constant.Configuration.config_company_name_en)
                        {
                            ltrCompanyName.Text = item.Value_name;
                        }
                        else if (item.Key_name == Constant.Configuration.config_address_en)
                        {
                            ltrAdderss1.Text = item.Value_name;
                        }

                    }
                    if (item.Key_name == Constant.Configuration.phone)
                    {
                        ltrPhone1.Text = item.Value_name;
                    }

                    else if (item.Key_name == Constant.Configuration.fax)
                    {
                        ltrFax1.Text = item.Value_name;
                    }
                    else if (item.Key_name == Constant.Configuration.email)
                    {
                        ltrEmail1.Text = item.Value_name;

                    }
                    else if (item.Key_name == Constant.Configuration.sitename)
                    {
                        ltrWebsite1.Text = item.Value_name;
                    }

                }

            }
        }
        #endregion

        #region Event

        protected void ValidateCheckSameSecurityCodeServer(object source, ServerValidateEventArgs args)
        {
            bool result = true;
            string pass = this.txt_Security_Code.Value.Trim().ToLower();
            string verifyCode = (string)Session["SECURITYCODE_CANHAN"];
            if (!pass.Equals(verifyCode, StringComparison.OrdinalIgnoreCase))
            {
                ((CustomValidator)source).Text = "Mã xác nhận không đúng.";
                result = false;
            }
            args.IsValid = result;
        }

        protected void Submit_ServerClick(object sender, EventArgs e)
        {
            try
            {
                GenSecurityCode();
                if (Page.IsValid)
                {
                    bool result = false;
                    string path = Request.PhysicalApplicationPath;
                    string strHtml = WebUtils.GetMailTemplate(Path.Combine(path, "TemplateMail/Contact.txt"));
                    string body = string.Format(strHtml, "admin", txtName.Value, txtEmail.Value, txtMessage.Value);
                    result =true;
                    if (result == true)
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), string.Format("jAlert('Gửi Liên hệ thành công','Message',function(r) {{window.location='{0}'}});", Request.RawUrl), true);
                    else
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), string.Format("jAlert('Gửi Liên hệ thất baị','Message',function(r) {{window.location='{0}'}});", Request.RawUrl), true);
                    txtEmail.Value = txtMessage.Value = txtName.Value = txt_Security_Code.Value = "";

                }
            }
            catch (Exception) { }
        }

        private void GenSecurityCode()
        {
            //gen ma an toan
            try
            {
                this.securityCode = GenerateString.Generate(5);
                Session.Add("SECURITYCODE_CANHAN", this.securityCode);
                img_Security_Code.Src = string.Format("{0}{1}", template_path, string.Format("/gen/{0}", Global.ToEncoding(securityCode)));
            }
            catch (Exception ex)
            {

                Write2Log.WriteLogs("GenSecurityCode", "GenSecurityCode()", ex.Message);
            }
        }

        #endregion
    }
}