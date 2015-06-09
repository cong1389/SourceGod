using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cb.Utility;
using Cb.DBUtility;
using Cb.BLL;
using Cb.Model;

namespace Cb.Web.Admin
{
    public partial class _default : DGCPage
    {
        #region Fields
        private string pageName;
        private int id;
        private XMLConfigBLL configXML;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //check login
            if (Session[Global.SESS_USER] == null)
            {
                string link = string.Empty;
                string url = string.Empty;
                link = string.Format(SiteNavigation.link_login, Constant.DB.langVn);              
                url = Utils.CombineUrl(WebUtils.GetWebPath(), link);
                Response.Redirect(url);
            }
            pageName = Utils.GetParameter("page", "home");
            id = DBConvert.ParseInt(Utils.GetParameter("id", string.Empty));
            configXML = new XMLConfigBLL();
            getPageName(pageName);
        }

        #region Common

        private void getPageName(string pageName)
        {
            try
            {
                pageName = configXML.LoadPage(pageName, Constant.DSC.IdXmlPageAdmin, id);
                UserControl contentView = (UserControl)Page.LoadControl(pageName);
                phdContent.Controls.Add(contentView);
            }
            catch (Exception ex)
            {
                Write2Log.WriteLogs("admin_editservice", "default", ex.Message);
            }
        }

        #endregion
    }
}
