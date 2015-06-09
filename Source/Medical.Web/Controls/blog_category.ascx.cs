using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cb.Utility;
using Cb.BLL.Products;
using System.Configuration;
using Cb.Model.Products;
using System.IO;
using System.Net;
using System.Web.UI.HtmlControls;
using Cb.DBUtility;

namespace Cb.Web.Controls.CategoryManagement
{
    public partial class blog_category : DGCUserControl
    {
        #region Parameter

        protected string template_path, pageName, cid, url, records;
        int total;

        ProductBLL pcBll = new ProductBLL();
        IList<Medical_Product> lst;

        protected int currentPageIndex
        {
            get
            {
                if (ViewState["CurrentPageIndex"] != null)
                    return int.Parse(ViewState["CurrentPageIndex"].ToString());
                else
                    return 1;
            }
            set
            {
                ViewState["CurrentPageIndex"] = value;
            }
        }

        #endregion

        #region Common

        private void InitPage()
        {
            template_path = WebUtils.GetWebPath();
            pageName = Utils.GetParameter("page", "home");
            cid = Utils.GetParameter("cid", string.Empty);

            GetList();
        }

        private void GetList()
        {
            if (cid == "")
                lst = pcBll.GetList(LangInt, pageName, "1", string.Empty, string.Empty, currentPageIndex, DBConvert.ParseInt(ConfigurationManager.AppSettings["pageSizeBlogLeture"]), out  total);

            //TH dòng thời gian
            else if (cid.Contains("@"))
            {
                int month = DBConvert.ParseInt(cid.Split('@')[0]);
                int year = DBConvert.ParseInt(cid.Split('@')[1]);
                lst = pcBll.GetList(LangInt, pageName, "1", string.Empty, string.Empty, currentPageIndex, DBConvert.ParseInt(ConfigurationManager.AppSettings["pageSizeBlogLeture"]), out  total);
                lst = lst.Where(m => m.PostDate.Year == year).ToList();
                lst = lst.Where(m => m.PostDate.Month == month).ToList();
            }

            //Trang tìm kiếm tìm tất cả TitleUrl không phân biệt PageName
            else if (pageName == "tim-kiem")
            {
                lst = pcBll.GetList(LangInt, string.Empty, "1", string.Empty, cid, string.Empty, string.Empty, string.Empty, currentPageIndex, DBConvert.ParseInt(ConfigurationManager.AppSettings["pageSizeBlogLeture"]), out  total);
            }

            //else//Filter theo tagUrl
            //    lst = pcBll.GetList(LangInt, pageName, "1", string.Empty, string.Empty, string.Empty, string.Empty, cid, currentPageIndex, DBConvert.ParseInt(ConfigurationManager.AppSettings["pageSizeBlogLeture"]), out  total);

            if (total > 0)
            {
                this.records = DBConvert.ParseString(lst.Count);
                this.pager.PageSize = DBConvert.ParseInt(ConfigurationManager.AppSettings["pageSizeBlogLeture"]);
                this.pager.ItemCount = total;

                this.rptResult.DataSource = lst;
                this.rptResult.DataBind();

                //WebUtils.SeoPage(string.Format("{0} | {1}", lst[0].CategoryDesc.ToString().ToUpper(), Template_path), , "", this.Page);
            }
        }

        private void ViewPdf(string fileName)
        {
            string path = Request.PhysicalApplicationPath;
            string url = Path.Combine("resource", "upload", "Products", fileName);
            url = Utils.CombineUrl(path, url);
            WebClient wc = new WebClient();
            Byte[] buffer = wc.DownloadData(url);
            if (buffer != null)
            {
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(buffer);
                Response.Flush();
                Response.End();
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

        protected void lbnHeader1_Command(object sender, CommandEventArgs e)
        {
            ViewPdf(e.CommandArgument.ToString().Trim());
        }

        protected void rptResult_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Medical_Product data = e.Item.DataItem as Medical_Product;
                HtmlAnchor hypImg = e.Item.FindControl("hypImg") as HtmlAnchor;
                //hypImg.HRef = LinkHelper.GetLink(data.NameUrlDesc, LangId, data.ProductDesc.TitleUrl);
                Literal ltrBrief = e.Item.FindControl("ltrBrief") as Literal;
                hypImg.Title = ltrBrief.Text = data.ProductDesc.Title;

                HtmlImage img = e.Item.FindControl("img") as HtmlImage;
                img.Src = WebUtils.GetUrlImage(ConfigurationManager.AppSettings["ProductUpload"], data.Image);

                LinkButton lbnHeader1 = e.Item.FindControl("lbnHeader1") as LinkButton;
                lbnHeader1.CommandArgument = data.Area;
                if (pageName == "sach-dich" || pageName == "bai-suy-gam")
                {
                    string clientID = string.Format("{0}${1}", lbnHeader1.NamingContainer.UniqueID, lbnHeader1.ID);
                    hypImg.HRef = "javascript:__doPostBack('" + clientID + "','');";
                }
                else
                {
                    hypImg.HRef = LinkHelper.GetLink(data.NameUrlDesc, LangId, data.ProductDesc.TitleUrl);
                }
            }
        }

        public void pager_Command(object sender, CommandEventArgs e)
        {
            this.currentPageIndex = Convert.ToInt32(e.CommandArgument);
            pager.CurrentIndex = this.currentPageIndex;
            InitPage();
        }

        #endregion
    }
}