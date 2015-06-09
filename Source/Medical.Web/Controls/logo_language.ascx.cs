using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using Cb.Utility;
using Cb.Localization;
using Cb.BLL;

namespace Cb.Web.Controls
{
    public partial class logo_language : DGCUserControl
    {
        #region Fields
        private string langId;
        CultureInfo ci = null;
        private int langInt;
        protected string template_path;
        private string pageName;
     

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            GetLang();
            ltrLangue.Text = LocalizationUtility.GetText(ltrLangue.ID, Ci);
            if (!this.IsPostBack)
            {
                InitPage();
              
            }
        }

        #region Common
        private void InitPage()
        {
            this.template_path = WebUtils.GetWebPath();
            string link, url, name, rawUrl, cid = "cid", id = "content", tagA = "<a href='{0}'><img border='0' id='{1}' title='{2}' src='{3}' /></a>";
            if (pageName == "home")
            {
                //ltrLangVn
                link = string.Format(SiteNavigation.link_home_rewrite, Constant.DB.langVn, cid, id);
                url = string.Format("{0}{1}", template_path, link);
                name = LocalizationUtility.GetText("lang_VietNamese", ci);
                ltrLangVn.Text = string.Format(tagA, url, Constant.DB.langVn, name, template_path + "/images/flagvn.png");
                //ltrLangEn
                link = string.Format(SiteNavigation.link_home_rewrite, Constant.DB.langEng, cid, id);
                url = string.Format("{0}{1}", template_path, link);
                name = LocalizationUtility.GetText("lang_English", ci);
                ltrLangEn.Text = string.Format(tagA, url, Constant.DB.langEng, name, template_path + "/images/flagen.png");
            }
            else
            {
                rawUrl = Request.RawUrl;
                //ltrLangVn
                url = rawUrl.Replace(Constant.DB.langEng, Constant.DB.langVn);
                name = LocalizationUtility.GetText("lang_VietNamese", ci);
                ltrLangVn.Text = string.Format(tagA, url, Constant.DB.langVn, name, template_path + "/images/flagvn.png");
                //ltrLangEn
                url = rawUrl.Replace(Constant.DB.langVn, Constant.DB.langEng);
                name = LocalizationUtility.GetText("lang_English", ci);
                ltrLangEn.Text = string.Format(tagA, url, Constant.DB.langEng, name, template_path + "/images/flagen.png");
            }
        }

        private void GetLang()
        {
            langId = Utils.GetParameter("langid", Constant.DB.langVn);
            this.ci = WebUtils.getResource(langId);
            langInt = langId == Constant.DB.langVn ? 1 : 2;
            template_path = WebUtils.GetWebPath();
            pageName = Utils.GetParameter("page", "home");
        }
        #endregion

       
    }
}