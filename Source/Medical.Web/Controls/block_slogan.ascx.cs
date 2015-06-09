using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cb.Model;
using Cb.BLL;
using Cb.Utility;
using System.IO;
using System.Web.UI.HtmlControls;
using Cb.Localization;
using System.Configuration;
using Cb.DBUtility;

namespace Cb.Web.Controls
{
    public partial class block_slogan : DGCUserControl
    {
        #region Parameter

        protected string pageName, template_path = string.Empty;

        private int total;

        #endregion

        #region Common

        private void InitPage()
        {
            GetBanner();
        }

        private void GetBanner()
        {
            BannerBLL bannerBLL = new BannerBLL();
            IList<Medical_Banner> lst = bannerBLL.GetList(DBConvert.ParseInt(ConfigurationManager.AppSettings["idSlogan"]), string.Empty, "1", 1, 100, out total);
            if (total > 0)
            {
                ltrDetail.Text = lst[0].Detail;
            }
        }

        #endregion

        #region Event

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }

        #endregion
    }
}