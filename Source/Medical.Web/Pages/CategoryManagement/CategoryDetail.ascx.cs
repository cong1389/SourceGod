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
    public partial class CategoryDetail : DGCUserControl
    {
        #region Parameter

        protected string template_path, pageName, cid,id;
        int total;

        #endregion

        #region Common

        private void InitPage()
        {
            template_path = WebUtils.GetWebPath();
            pageName = Utils.GetParameter("page", "home");
            cid = Utils.GetParameter("cid", string.Empty);
            id = Utils.GetParameter("id", string.Empty);

            GetSEO();
        }

        private void GetSEO()
        {
            ProductBLL pcBll = new ProductBLL();
            IList<Medical_Product> lst = pcBll.GetList(1, pageName, string.Empty, string.Empty, cid, null, string.Empty, 1, 9999, out total);
            if (total > 0)
            {
                lst = lst.Where(p => p.ProductDesc.TitleUrl == cid).ToList();
                WebUtils.SeoPage(lst[0].ProductDesc.MetaTitle, lst[0].ProductDesc.Metadescription, lst[0].ProductDesc.MetaKeyword, this.Page);
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