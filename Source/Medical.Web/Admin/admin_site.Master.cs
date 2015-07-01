using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cb.Utility;

namespace Cb.Web.Admin
{
    public partial class admin_site : System.Web.UI.MasterPage
    {
        #region Fields
        protected string template_path;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPage();
        }

        #region Common
        private void InitPage()
        {
            this.template_path = WebUtils.GetWebPath();

            WebUtils.IncludeCSS(this.Page, template_path + "/Style/jquery-ui.css");
            WebUtils.IncludeCSS(this.Page, template_path + "/Style/theme.css");
            WebUtils.IncludeJS(this.Page, template_path + "/Style/user_files/meerkat.js");
            WebUtils.IncludeJS(this.Page, template_path + "/Style/user_files/JSCookMenu.js");
            WebUtils.IncludeJS(this.Page, template_path + "/Style/user_files/theme.js");
            WebUtils.IncludeCSS(this.Page, template_path + "/Style/global.css");
            WebUtils.IncludeCSS(this.Page, template_path + "/Style/jquery.fancybox-1.3.4.css");
            WebUtils.IncludeCSS(this.Page, template_path + "/Style/style.css");
            WebUtils.IncludeCSS(this.Page, template_path + "/Style/jquery.Jcrop.css");//upload multifile
            WebUtils.IncludeCSS(this.Page, template_path + "/Style/jquery.alerts.css");

            //BootStrap
            WebUtils.IncludeCSS(this.Page, "/Styles/bootstrap.min.css");
            WebUtils.IncludeCSS(this.Page, "/Styles/font-awesome.min.css");
            WebUtils.IncludeJS(this.Page, "/Scripts/jquery.js");
            WebUtils.IncludeJS(this.Page, "/Scripts/bootstrap.min.js");

            WebUtils.IncludeJS(this.Page, template_path + "/javascript/jquery.alerts.js");
            WebUtils.IncludeJS(this.Page, template_path + "/javascript/jquery/jquery.min.js");
            WebUtils.IncludeJS(this.Page, template_path + "/javascript/jquery/jquery-ui.js");
            WebUtils.IncludeJS(this.Page, template_path + "/javascript/jquery/jquery.fancybox-1.3.4.pack.js");
            WebUtils.IncludeJS(this.Page, template_path + "/javascript/jquery.Jcrop.js");
            WebUtils.IncludeJS(this.Page, template_path + "/Style/user_files/meerkat.js");
            WebUtils.IncludeJS(this.Page, template_path + "/Style/user_files/JSCookMenu.js");
            WebUtils.IncludeJS(this.Page, template_path + "/Style/user_files/theme.js");
            WebUtils.IncludeJS(this.Page, template_path + "/javascript/functions.js");

        }
        #endregion

    }
}
