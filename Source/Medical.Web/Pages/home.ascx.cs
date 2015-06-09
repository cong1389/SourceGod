using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cb.BLL;
using Cb.Utility;

namespace Cb.Web.Pages
{
    public partial class home : System.Web.UI.UserControl
    {

        #region Parameter

        protected string title, metaDescription, metaKeyword;

        #endregion

        #region Common

        private void InitPage()
        {
            GetSEO();
        }

        private void GetSEO()
        {
            ConfigurationBLL pcBll = new ConfigurationBLL();
            IList<Medical_Configuration> lst = pcBll.GetList();
            if (lst != null && lst.Count > 0)
            {
                foreach (Medical_Configuration item in lst)
                {
                    if (item.Key_name == Constant.Configuration.config_title)
                    {
                        title = item.Value_name;
                    }
                    else if (item.Key_name == Constant.Configuration.config_metaDescription)
                    {
                        metaDescription = item.Value_name;
                    }
                    else if (item.Key_name == Constant.Configuration.config_metaKeyword)
                    {
                        metaKeyword = item.Value_name;
                    }
                }
                WebUtils.SeoPage(title, metaDescription, metaKeyword, this.Page);
            }
        }

        #endregion

        #region Event

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitPage();
            }
        }

        #endregion
    }
}