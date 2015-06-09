using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cb.Utility;
using System.Data;
using Cb.DBUtility;
using Cb.Localization;
using System.Globalization;

namespace Cb.Web
{
    public partial class Template : System.Web.UI.MasterPage
    {
        #region Parameter

        protected string pageName, template_path = string.Empty;

        private int id;

        #endregion

        #region Common

        private void InitPage()
        {
            try
            {
                this.template_path = WebUtils.GetWebPath();
                pageName = Utils.GetParameter("page", "home");

                string pathUsc = pageName;
                switch (pageName)
                {
                    case "home":
                    case "trang-chu":
                        pathUsc = "Pages/home.ascx";
                        break;
                    default:
                        pathUsc = "Controls/block_breakumb.ascx";
                        break;

                }
                UserControl contentView = (UserControl)Page.LoadControl(pathUsc);
                childContent.Controls.Add(contentView);
            }
            catch (Exception ex)
            {


            }
            //if (pageName == "home")
            //    block_silder.Visible = true;

        }

        #endregion

        #region Event

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPage();
        }

        #endregion
    }
}