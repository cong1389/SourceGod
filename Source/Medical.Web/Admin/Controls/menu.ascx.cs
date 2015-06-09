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
        protected string l_menu_outside;
        protected string link_menu_outside;
        protected string l_menu_staff_manager;
        protected string link_menu_staff_manager;        
        protected string link_menu_user_manager;
        protected string l_menu_logout;
        protected string link_menu_logout;
        protected string l_menu_manage;
        protected string l_menu_tool;

        //logo
        protected string l_menu_tool_logo;
        protected string link_menu_tool_logo;

        //banner
        protected string link_menu_tool_slider = LinkHelper.GetAdminLink("slider");

        ////services
        //protected string l_menu_tool_services;
        protected string link_menu_tool_services=LocalizationUtility.GetText("strHeaderServices");

        //Product Category
        protected string link_menu_tool_productcategory = LinkHelper.GetAdminLink("productcategory");
        protected string link_menu_tool_product=LinkHelper.GetAdminLink("product");

        //config      
        protected string link_menu_config;
        

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            this.web_path = WebUtils.GetHostPath();            
            this.l_menu_outside = Constant.UI.menu_home;           
            this.l_menu_staff_manager = Constant.UI.menu_home_staff_manager;            
            this.l_menu_logout = Constant.UI.menu_home_logout;
            this.l_menu_tool = Constant.UI.menu_tool;
            this.link_menu_outside = LinkHelper.GetLink("home", "vn");
            link_menu_logout = Utils.CombineUrl(WebUtils.GetBaseUrl(), "logout.aspx");//logout
            link_menu_user_manager = LinkHelper.GetAdminLink("user");//user

            ////services
            //this.l_menu_tool_services = LocalizationUtility.GetText("strHeaderServices");
            //this.link_menu_tool_services = LinkHelper.GetAdminLink("services");

            ////Product Category
            //this.link_menu_tool_productcategory = LinkHelper.GetAdminLink("productcategory");
            //this.link_menu_tool_product = LinkHelper.GetAdminLink("product");

            ////adv
            //this.link_menu_tool_logo = LinkHelper.GetAdminLink("adv");
            //this.l_menu_tool_logo = Constant.UI.menu_tool_logo;

            //config
            link_menu_config = LinkHelper.GetAdminLink("config");
            
            
        }
    }
}