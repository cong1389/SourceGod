using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cb.Utility;
using Cb.BLL.Products;
using Cb.Model.Products;

namespace Cb.Web.Pages.CategoryManagement
{
    public partial class Category : DGCUserControl
    {
        #region Parameter

        protected string template_path, pageName;
        int total;

        #endregion

        #region Common

        private void InitPage()
        {
            template_path = WebUtils.GetWebPath();
            pageName = Utils.GetParameter("page", "home");

            GetSEO();
        }

        private void GetSEO()
        {
            ProductCategoryBLL pcBll = new ProductCategoryBLL();
            IList<Medical_ProductCategory> lst = pcBll.GetList(LangInt, string.Empty, string.Empty, int.MinValue, false, "p.ordering", 1, 9999, out  total);
            if (total > 0)
            {
                lst = lst.Where(p => p.NewsCategoryDesc.NameUrl == pageName).ToList();               
                WebUtils.SeoPage(lst[0].NewsCategoryDesc.MetaTitle, lst[0].NewsCategoryDesc.MetaDecription, lst[0].NewsCategoryDesc.MetaKeyword, this.Page);
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