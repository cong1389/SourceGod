using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cb.BLL.Products;
using Cb.Model.Products;
using Cb.Utility;
using Cb.DBUtility;
using System.Configuration;
using System.Web.UI.HtmlControls;
using Cb.BLL;

namespace Cb.Web.Controls
{
    public partial class block_archive : DGCUserControl
    {
        #region Parameter

        protected string template_path, pageName, cid, url, records;
        int total;

        ProductBLL pcBll = new ProductBLL();
        IList<Archive> lst;

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
            Generic<Archive> gen = new Generic<Archive>();
            lst = gen.GetAllBy(new Archive(), "SELECT Month(PostDate) AS Month ,YEAR(PostDate) AS Year FROM Medical_Product GROUP BY Month(PostDate) ,YEAR(PostDate)", null, null);

            if (lst.Count() > 0)
            {
                this.rptResult.DataSource = lst;
                this.rptResult.DataBind();
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
                Archive data = e.Item.DataItem as Archive;

                string date = Utils.RemoveUnicode(string.Format("{0} {1}", data.Month, data.Year));
                date = date.Replace("-", "@");
                HtmlAnchor hypItem = e.Item.FindControl("hypItem") as HtmlAnchor;
                hypItem.HRef = LinkHelper.GetLink(pageName, LangId, date);

                Literal ltrItem = e.Item.FindControl("ltrItem") as Literal;
                ltrItem.Text = string.Format("Tháng {0} {1}", data.Month, data.Year);
                //Literal ltrItem = e.Item.FindControl("ltrItem") as Literal;
                //ltrItem.Text = data.ProductDesc.Design;              
            }
        }

        #endregion
    }

    public class Archive
    {
        #region fields

        private int month;
        private int year;

        #endregion

        #region properties

        public int Month
        {
            get { return month; }
            set { month = value; }
        }

        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        #endregion

        #region constructor
        public Archive()
        {
            this.month = int.MinValue;
            this.year = int.MinValue;
        }
        #endregion
    }
}