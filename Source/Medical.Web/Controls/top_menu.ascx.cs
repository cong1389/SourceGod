using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Cb.Utility;
using Cb.Localization;
using Cb.BLL;
using System.IO;
using Cb.Model.Products;
using Cb.BLL.Products;
using Cb.DBUtility;
using System.Configuration;
using System.Web.UI.HtmlControls;

namespace Cb.Web.Controls
{
    public partial class top_menu : DGCUserControl
    {
        #region Parameter

        protected string pageName, template_path = string.Empty;

        private int id, total;

        private ProductCategoryBLL pcBll
        {
            get
            {
                if (ViewState["pcBll"] != null)
                    return (ProductCategoryBLL)ViewState["pcBll"];
                else return new ProductCategoryBLL();
            }
            set
            {
                ViewState["pcBll"] = value;
            }
        }

        #endregion

        #region Common

        private void InitPage()
        {
            GetMenu();
            GetLogo();
        }

        /// <summary>
        /// Get menu voi ParentID=0
        /// </summary>
        private void GetMenu()
        {
            IList<Medical_ProductCategory> lst = pcBll.GetList(LangInt, string.Empty, "1", DBConvert.ParseInt(ConfigurationManager.AppSettings["parentId"]), false, "p.ordering", 1, 10, out  total);
            if (total > 0)
            {
                rptResult.DataSource = lst;
                rptResult.DataBind();
            }
        }

        private void GetLogo()
        {
            ConfigurationBLL pcBll = new ConfigurationBLL();
            IList<Medical_Configuration> lst = pcBll.GetList();
            if (lst != null && lst.Count > 0)
            {
                foreach (Medical_Configuration item in lst)
                {
                    if (item.Key_name == Constant.Configuration.config_logoHeader)
                    {
                        imgLogo.Src = WebUtils.GetUrlImage(Constant.DSC.AdvUploadFolder, item.Value_name);
                        hypImgHomePage.HRef = WebUtils.RedirectHomePage();
                    }
                }
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
                Medical_ProductCategory data = e.Item.DataItem as Medical_ProductCategory;

                HtmlAnchor hypName = e.Item.FindControl("hypName") as HtmlAnchor;
                hddParentNameUrl.Value = hypName.HRef = LinkHelper.GetLink(data.NewsCategoryDesc.NameUrl, LangId);

                Literal ltrName = e.Item.FindControl("ltrName") as Literal;
                hypName.Title = ltrName.Text = data.NewsCategoryDesc.Name;

                //Set menu Icon Home 
                if (data.NewsCategoryDesc.Id == DBConvert.ParseInt(ConfigurationManager.AppSettings["parentIdHome"]))
                {
                    Literal ltrIconHome = e.Item.FindControl("ltrIconHome") as Literal;
                    ltrIconHome.Text = "<span class=\"icon-homeChurch\"></span>";
                }

                //Sub menu level 2
                if (data.NewsCategoryDesc.Id.ToString() == ConfigurationManager.AppSettings["parentIdThanhCa"])
                {
                    IList<Medical_ProductCategory> lst = pcBll.GetList(LangInt, string.Empty, "1", DBConvert.ParseInt(ConfigurationManager.AppSettings["parentIdThanhCa"]), false, "p.ordering", 1, 10, out  total);
                    if (total > 0)
                    {
                        hddParentNameUrl.Value = data.NewsCategoryDesc.NameUrl;
                        hypName.Attributes.Add("data-toggle", "dropdown");

                        //Set Icon menu tho phuong
                        Literal ltrIconSub = e.Item.FindControl("ltrIconSub") as Literal;
                        ltrIconSub.Text = "<span class=\"glyphicon glyphicon-menu-down\"></span>";

                        Repeater rptResultSub2 = e.Item.FindControl("rptResultSub2") as Repeater;
                        rptResultSub2.DataSource = lst;
                        rptResultSub2.DataBind();
                    }
                }
            }
        }

        protected void rptResultSub2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater dataa = (Repeater)e.Item.Parent;

                Medical_ProductCategory data = e.Item.DataItem as Medical_ProductCategory;
                HtmlAnchor hypNameSub2 = e.Item.FindControl("hypNameSub2") as HtmlAnchor;
                hypNameSub2.HRef = LinkHelper.GetLink(data.NewsCategoryDesc.NameUrl, LangId);
                //hypNameSub2.HRef = LinkHelper.GetLink(hddParentNameUrl.Value, LangId, data.NewsCategoryDesc.NameUrl);
                Literal ltrNameSub2 = e.Item.FindControl("ltrNameSub2") as Literal;
                hypNameSub2.Title = ltrNameSub2.Text = data.NewsCategoryDesc.Name;

            }
        }

        #endregion
    }
}