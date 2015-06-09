using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cb.Utility;
using Cb.DBUtility;

namespace Cb.Web.Pages.Search
{
    public partial class SearchResult : System.Web.UI.UserControl
    {
        #region Parameter

        protected string template_path, pageName, cid;
        int total;

        #endregion

        #region Common

        private void InitPage()
        {
            template_path = WebUtils.GetWebPath();
            pageName = Utils.GetParameter("page", "home");
            cid = Utils.GetParameter("cid", string.Empty);

            ltrTitle.Text = string.Format("Kết quả {0}", cid);
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