using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cb.BLL.Products;
using Cb.Model.Products;
using Cb.Utility;

namespace Cb.Web.Controls
{
    public partial class block_breakumb : DGCUserControl
    {
        #region Parameter

        protected string cid, pageName, id, template_path = string.Empty;
        private int total;

        #endregion

        #region Common

        private void InitPage()
        {
            GetPageName();

            hypHome.HRef = WebUtils.RedirectHomePage();
        }

        private void GetPageName()
        {
            pageName = Utils.GetParameter("page", string.Empty);
            ProductCategoryBLL pcBll = new ProductCategoryBLL();
            IList<Medical_ProductCategory> lst = pcBll.GetList(LangInt, pageName, string.Empty, int.MinValue, false, "p.ordering", 1, 9999, out  total);
            if (total > 0)
            {
                lst = lst.Where(p => p.NewsCategoryDesc.NameUrl == pageName).ToList();
                pageName = lst[0].NewsCategoryDesc.Name;
                ltrPageHeader.Text = ltrPageSub.Text = pageName;
                hypPageSub.HRef = LinkHelper.GetLink(lst[0].NewsCategoryDesc.NameUrl, LangId);
            }
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