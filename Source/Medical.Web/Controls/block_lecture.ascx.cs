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
using Cb.BLL.Products;
using Cb.Model.Products;
using System.Configuration;
using System.Net;
using Cb.DBUtility;

namespace Cb.Web.Controls
{
    public partial class block_lecture : DGCUserControl
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
            lst = pcBll.GetList(LangInt, string.Empty, "1", ConfigurationManager.AppSettings["parentIdLeture"], string.Empty, 1, DBConvert.ParseInt(ConfigurationManager.AppSettings["pageSizeBlockLeture"]), out  total);

            if (total > 0)
            {
                this.rptResult.DataSource = lst;
                this.rptResult.DataBind();

                //WebUtils.SeoPage(string.Format("{0} | {1}", lst[0].CategoryDesc.ToString().ToUpper(), Template_path), "", "", this.Page);
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
                hypImg.Title = ltrBrief.Text = data.ProductDesc.Brief;

                HtmlImage img = e.Item.FindControl("img") as HtmlImage;
                img.Src = WebUtils.GetUrlImage(ConfigurationManager.AppSettings["ProductUpload"], data.Image);

                LinkButton lbnHeader1 = e.Item.FindControl("lbnHeader1") as LinkButton;
                lbnHeader1.CommandArgument = data.Area;

                string clientID = string.Format("{0}${1}", lbnHeader1.NamingContainer.UniqueID, lbnHeader1.ID);                
                hypImg.HRef = "javascript:__doPostBack('" + clientID + "','');";
            }
        }

        #endregion
    }
}