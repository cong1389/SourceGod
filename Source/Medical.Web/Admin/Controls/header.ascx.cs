using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cb.Utility;

namespace Cb.Web.Admin.Controls
{
    public partial class header : System.Web.UI.UserControl
    {
        #region Fields
        protected string template_path;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            this.template_path = WebUtils.GetWebPath();
        }
    }
}