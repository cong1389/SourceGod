using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cb.Utility;
using Cb.Localization;

namespace Cb.Web.Admin.Controls
{
    public partial class menu : System.Web.UI.UserControl
    {
        #region Fields

        protected string web_path;

        protected string linkMenuHomePage;

        //protected string link_menu_staff_manager;
        protected string link_menu_user_manager;

        protected string link_menu_logout;


        //logo

        protected string link_menu_tool_logo;

        //banner
        protected string link_menu_tool_slider = LinkHelper.GetAdminLink("slider");

        ////services
        //protected string l_menu_tool_services;
        //protected string link_menu_tool_services = LocalizationUtility.GetText("strHeaderServices");

        //Product Category
        protected string link_menu_tool_productcategory = LinkHelper.GetAdminLink("productcategory");
        protected string link_menu_tool_product = LinkHelper.GetAdminLink("product");

        //config      
        protected string link_menu_config;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            this.web_path = WebUtils.GetHostPath();
            linkMenuHomePage = WebUtils.RedirectHomePage();// LinkHelper.GetLink("home", "vn");            
            link_menu_logout = Utils.CombineUrl(WebUtils.GetBaseUrl(), "logout.aspx");//logout
            link_menu_user_manager = LinkHelper.GetAdminLink("user");//user

            //config
            link_menu_config = LinkHelper.GetAdminLink("config");

            //Set Log out
            hypLogOut.HRef = link_menu_logout;

            //Set UserName
            Medical_User lst_user = (Medical_User)Session[Global.SESS_USER];
            ltrUserName.Text = lst_user.Username;


        }
    }
}