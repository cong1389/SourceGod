using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cb.Utility;
using Cb.BLL.Products;
using System.Configuration;
using Cb.DBUtility;
using Cb.Model.Products;
using System.IO;
using System.Net;
using System.Web.UI.HtmlControls;

namespace Cb.Web.Controls.CategoryManagement
{
    public partial class block_tagcloud : DGCUserControl
    {
        #region Parameter

        protected string template_path, pageName, nameurl, url, records;
        int total;

        #endregion

        #region Common

        private void InitPage()
        {
            template_path = WebUtils.GetWebPath();
            pageName = Utils.GetParameter("page", "home");
            nameurl = Utils.GetParameter("cid", string.Empty);

            GetDetail();
        }

        private void GetDetail()
        {
            ProductBLL pcBll = new ProductBLL();
            IList<Medical_Product> lst;
            lst = pcBll.GetList(LangInt, pageName, "1", string.Empty, string.Empty, string.Empty, "1", 1, DBConvert.ParseInt(ConfigurationManager.AppSettings["pageSizeBlogHot"]), out  total);
            //lst = lst.Where(m => m.ProductDesc.Utility != "").ToList();

            if (total > 0)
            {
                this.rptResult.DataSource = lst;
                this.rptResult.DataBind();

                ltrTilte.Text = string.Format("{0} nổi bật", lst[0].CategoryDesc);
                //WebUtils.SeoPage(string.Format("{0} | {1}", lst[0].CategoryDesc.ToString().ToUpper(), Template_path), "", "", this.Page);
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

        protected void rptResult_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Medical_Product data = e.Item.DataItem as Medical_Product;
                HtmlAnchor hypItem = e.Item.FindControl("hypItem") as HtmlAnchor;
                hypItem.HRef = LinkHelper.GetLink(data.NameUrlDesc, LangId, data.ProductDesc.TitleUrl);

                Literal ltrItem = e.Item.FindControl("ltrItem") as Literal;
                ltrItem.Text = data.ProductDesc.Design;
            }
        }

        #endregion
    }
}