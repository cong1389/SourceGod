using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cb.BLL;
using Cb.Utility;
using Cb.DBUtility;
using Cb.Model;
using Cb.Model.Products;
using Cb.BLL.Products;
using Cb.Localization;
using System.Configuration;

namespace Cb.Web
{
    public partial class _default : DGCPage
    {
        #region Parameter

        protected string cid, pageName, id, template_path = string.Empty;
        private int total;

        #endregion

        #region Common

        private void getPageName(string pageName)
        {
            try
            {
                cid = Utils.GetParameter("cid", string.Empty);
                id = Utils.GetParameter("id", string.Empty);

                //block timeline, nếu cid có chứa '@' thì filter theo pageName
                //pageName=tim-kiem chuyển sang ProductCategoryBLL
                if (cid.Contains("@") || pageName == "tim-kiem") cid = string.Empty;
                if (cid != string.Empty)
                {
                    ProductBLL pcBll = new ProductBLL();
                    IList<Medical_Product> lst = pcBll.GetList(1, pageName, string.Empty, string.Empty, cid, null, string.Empty, 1, 9999, out total);
                    if (total > 0)
                    {
                        lst = lst.Where(p => p.ProductDesc.TitleUrl == cid).ToList();
                        pageName = lst[0].Page;
                    }
                }
                else
                {
                    ProductCategoryBLL pcBll = new ProductCategoryBLL();
                    IList<Medical_ProductCategory> lst = pcBll.GetList(LangInt, string.Empty, string.Empty, int.MinValue, false, "p.ordering", 1, 9999, out  total);
                    if (total > 0)
                    {
                        lst = lst.Where(p => p.NewsCategoryDesc.NameUrl == pageName).ToList();
                        pageName = lst[0].Page;
                    }
                }
                UserControl contentView = (UserControl)Page.LoadControl(pageName);
                phdContent.Controls.Add(contentView);
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        #region Event

        protected void Page_Load(object sender, EventArgs e)
        {
            pageName = Utils.GetParameter("page", "home");
            //if (!string.IsNullOrEmpty(pageName) && pageName != "home")
            getPageName(pageName);
        }

        #endregion
    }
}