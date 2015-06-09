using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cb.Utility;
using Cb.BLL;
using Cb.Localization;
using Cb.DBUtility;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Configuration;
using System.IO;
using Cb.BLL.Services;
using Cb.Model.Services;

namespace Cb.Web.Pages.Service
{
    public partial class servicedetails : DGCUserControl
    {
        #region field
        private ServicesBLL _ServiceBLL;
        protected string template_path;
        string pageName;


        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            template_path = WebUtils.GetWebPath();
            pageName = Utils.GetParameter("page", "home");
            hdfLinkFace.Value = Utils.CombineUrl(template_path, Request.RawUrl);
            if (!this.IsPostBack)
            {
                InitPage();
            }
        }


        private void InitPage()
        {
            try
            {
                string id = Utils.GetParameter("id", string.Empty);
                ServicesBLL bll = new ServicesBLL();
                IList<Medical_Services> lst = null;
                lst = bll.GetListT(int.Parse(id), LangInt);
                if (lst != null && lst.Count > 0)
                {
                    ltrService.Text = lst[0].ServicesDesc.Title;
                    ltrDetail.Text = lst[0].ServicesDesc.Detail;
                }
                else
                    ltrService.Text = LocalizationUtility.GetText(ltrService.ID, Ci);

            }
            catch (Exception ex)
            {
                Write2Log.WriteLogs("gallerydetails", "InitPage", ex.InnerException.ToString());
            }
        }

    }
}