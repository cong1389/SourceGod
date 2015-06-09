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
    public partial class block_silder : DGCUserControl
    {
        #region Parameter

        protected string pageName, template_path = string.Empty;

        private int id, total;

        #endregion

        #region Common

        private void InitPage()
        {
            GetBanner();
        }

        private void GetBanner()
        {
            BannerBLL bannerBLL = new BannerBLL();
            IList<Medical_Banner> lst = bannerBLL.GetList(DBConvert.ParseInt(ConfigurationManager.AppSettings["idBanner"]), string.Empty, "1", 1, 100, out total);
            if (total > 0)
            {
                this.rptResult.DataSource = lst;
                this.rptResult.DataBind();
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

        protected void rptResult_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Medical_Banner data = e.Item.DataItem as Medical_Banner;

                HtmlImage img = e.Item.FindControl("img") as HtmlImage;
                img.Src = WebUtils.GetUrlImage(ConfigurationManager.AppSettings["UploadSlider"], data.Image);

                Literal ltrName = e.Item.FindControl("ltrName") as Literal;
                ltrName.Text = img.Alt = data.Name;

                Literal ltrDetail = e.Item.FindControl("ltrDetail") as Literal;
                ltrDetail.Text = data.Detail;
            }
        }

        #endregion
    }
}