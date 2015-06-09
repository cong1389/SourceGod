using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.IO;
using Cb.BLL;
using Cb.Utility;

using Cb.DBUtility;
using Cb.Localization;
using System.Net;
using Cb.Model.Services;
using Cb.BLL.Services;

namespace Cb.Web.Pages.Service
{
    public partial class service : DGCUserControl
    {
        #region Fields
        private int pageSize;
        private ServicesBLL bll;
        private int countItem;
        public static string LogFolder = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "log");
        public static string LogFileName = "error.log";
        #region Viewstate
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

        #endregion

        #region Common

        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (!this.IsPostBack)
            {
                InitPage();
               
            }
        }

        private void InitPage()
        {
            ltrService.Text = LocalizationUtility.GetText(ltrService.ID, Ci);
            try
            {
                bll = new ServicesBLL();
                IList<Medical_Services> lst = null;
                int total;
                pageSize = pager.PageSize;
                int cid = DBConvert.ParseInt(Utils.GetParameter("cid", string.Empty));
                lst = bll.GetList(LangInt, string.Empty, DBConvert.ParseString(cid), currentPageIndex, pageSize, out total);
                countItem = lst.Count;                
                this.pager.ItemCount = total;
                if (lst != null && lst.Count > 0)
                {
                    dtlstService.DataSource = lst;
                    dtlstService.DataBind();
                }
            }
            catch (Exception ex)
            {
                
                 Write2Log.WriteLogs("Service", "InitPage", ex.ToString());
            }
        }
        #endregion

        #region Event





        protected void dtlstService_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Medical_Services data = e.Item.DataItem as Medical_Services;


                    string src, url, link = LinkHelper.GetLink("servicedetails", LangId, data.CategoryId, data.Id);
                    if (!string.IsNullOrEmpty(data.Image))
                    {
                        src = Path.Combine(Constant.DSC.ServicesUploadFolder, data.Image);
                        url = Server.MapPath(src);
                        if (File.Exists(url))
                            url = Utils.CombineUrl(Template_path, src);
                        else
                            url = Utils.CombineUrl(Template_path, Constant.DSC.NoImage);
                    }
                    else
                        url = Utils.CombineUrl(Template_path, Constant.DSC.NoImage);

                    HtmlImage Img = e.Item.FindControl("Img") as HtmlImage;
                    Img.Src = url;
                    Img.Width = 200;
                    HtmlAnchor hypImg = e.Item.FindControl("hypImg") as HtmlAnchor;
                    HtmlAnchor hypTitle = e.Item.FindControl("hypTitle") as HtmlAnchor;

                    hypImg.HRef = hypTitle.HRef = link;

                    Literal ltrTitle = e.Item.FindControl("ltrTitle") as Literal;
                    ltrTitle.Text = data.ServicesDesc.Title;

                    Literal ltrIntro = e.Item.FindControl("ltrIntro") as Literal;
                    ltrIntro.Text = data.ServicesDesc.Brief;
                }
            }
            catch (Exception ex)
            {
                
                 Write2Log.WriteLogs("Service", "dtlstService_ItemDataBound", ex.ToString());
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