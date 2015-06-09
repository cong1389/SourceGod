using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Cb.Utility;
using Cb.DBUtility;
using Cb.BLL;
using Cb.Localization;
using Cb.Model.Services;
using Cb.BLL.Services;

namespace Cb.Web.Admin.Pages.ServicesCategory
{
    public partial class admin_servicescategory : System.Web.UI.UserControl
    {
        #region Fields
        protected string template_path
        {
            get
            {
                if (ViewState["template_path"] != null)
                    return ViewState["template_path"].ToString();
                return string.Empty;
            }
            set
            {
                ViewState["template_path"] = value;
            }
        }
        protected string show_msg;
        protected string l_search;
        protected string records;
        protected string msg_no_selected_item;
        protected string msg_confirm_delete_item;
        private string action;
        private ServicesCategoryBLL pcBll
        {
            get
            {
                if (ViewState["pcBll"] != null)
                    return (ServicesCategoryBLL)ViewState["pcBll"];
                else return new ServicesCategoryBLL();
            }
            set
            {
                ViewState["pcBll"] = value;
            }
        }
        private Generic<Medical_ServicesCategory> genericBLL
        {
            get
            {
                if (ViewState["genericBLL"] != null)
                    return (Generic<Medical_ServicesCategory>)ViewState["genericBLL"];
                else return new Generic<Medical_ServicesCategory>();
            }
            set
            {
                ViewState["genericBLL"] = value;
            }
        }
        private Generic2C<Medical_ServicesCategory, Medical_ServicesCategoryDesc> generic2CBLL
        {
            get
            {
                if (ViewState["generic2CBLL"] != null)
                    return (Generic2C<Medical_ServicesCategory, Medical_ServicesCategoryDesc>)ViewState["generic2CBLL"];
                else return new Generic2C<Medical_ServicesCategory, Medical_ServicesCategoryDesc>();
            }
            set
            {
                ViewState["generic2CBLL"] = value;
            }
        }
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

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            GetAction();
            if (!IsPostBack)
            {
                InitializeComponent();
                Search();
            }
        }
        #endregion

        #region Common
        /// <summary>
        /// Init page
        /// </summary>
        private void InitPage()
        {
            pcBll = new ServicesCategoryBLL();
            genericBLL = new Generic<Medical_ServicesCategory>();
            generic2CBLL = new Generic2C<Medical_ServicesCategory, Medical_ServicesCategoryDesc>();
            this.template_path = WebUtils.GetWebPath();
            LocalizationUtility.SetValueControl(this);
            msg_confirm_delete_item = LocalizationUtility.GetText("mesConfirmDelete");
            msg_no_selected_item = LocalizationUtility.GetText("mesSelectItem");
            getMessage();
        }
        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private int GetList(byte langid, string content, int begin, int end)
        {
            int total;
            IList<Medical_ServicesCategory> lst = pcBll.GetList(langid, content, begin, end, out total);
            this.records = DBConvert.ParseString(lst.Count);
            this.pager.PageSize = 50;
            this.pager.ItemCount = total;
            this.rptResult.DataSource = lst;
            this.rptResult.DataBind();
            return total;
        }
        /// <summary>
        /// action
        /// </summary>
        private void GetAction()
        {
            this.action = Request.Form["task"];
            string cid = Request.Form["cid[]"];
            switch (action)
            {
                case "new":
                    add();
                    break;
                case "edit":
                    edit(cid);
                    break;
                case "publish":
                    change(cid, "1");
                    break;
                case "unpublish":
                    change(cid, "0");
                    break;
                case "delete":
                    delete(cid);
                    break;
                case "save":
                    saveOrder();
                    string url = LinkHelper.GetAdminLink("servicescategory");
                    Response.Redirect(url);
                    break;
                case "search":
                    pager.CurrentIndex = 1;
                    this.currentPageIndex = 1;
                    Search();
                    break;
                //default:
                //    show();
                //    break;
            }
        }
        private void add()
        {
            string url = LinkHelper.GetAdminLink("edit_servicescategory");
            Response.Redirect(url);
        }
        private void edit(string cid)
        {
            if (cid == null) return;
            string link, url;
            string[] arrStr;
            if (cid.IndexOf(',') >= 0)
            {
                arrStr = cid.Split(',');
                link = LinkHelper.GetAdminLink("edit_servicescategory", arrStr[0]);
                //link = string.Format(SiteNavigation.link_adminPage_editproductcategory, arrStr[0]);
            }
            else
                link = LinkHelper.GetAdminLink("edit_servicescategory", cid);
            Response.Redirect(link);
        }
        /// <summary>
        /// change
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="state"></param>
        private void change(string cid, string state)
        {
            if (cid != null)
            {
                genericBLL.ChangeWithTransaction(cid, state);
                Search();
            }
        }
        /// <summary>
        /// delete
        /// </summary>
        private void delete(string cid)
        {
            if (cid != null)
            {
                //các category con của nó
                IList<Medical_ServicesCategory> lst = pcBll.GetAllChild(DBConvert.ParseInt(cid), true);

                if (lst != null && lst.Count > 0)
                {
                    string script = string.Format("alert('{0}')", Constant.UI.alert_invalid_delete_productcategory_exist_child);
                    ScriptManager.RegisterStartupScript(this, GetType(), Guid.NewGuid().ToString(), script, true);
                }
                else
                {
                    string link, url;

                    if (generic2CBLL.Delete(cid))
                        link = LinkHelper.GetAdminMsgLink("servicescategory", "delete");
                    else
                        link = LinkHelper.GetAdminMsgLink("servicescategory", "delfail");
                    url = Utils.CombineUrl(template_path, link);
                    Response.Redirect(url);
                }
            }
        }

        /// <summary>
        /// saveOrder
        /// </summary>
        private void saveOrder()
        {
            foreach (RepeaterItem item in rptResult.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    HtmlInputButton btId = (HtmlInputButton)item.FindControl("btId");
                    Medical_ServicesCategory productCat = new Medical_ServicesCategory();
                    productCat.Id = DBConvert.ParseByte(btId.Value);
                    productCat = genericBLL.Load(productCat, new string[] { "Id" });
                    HtmlInputText txtOrder = (HtmlInputText)item.FindControl("txtOrder");
                    if (txtOrder != null)
                    {
                        try
                        {
                            productCat.Ordering = DBConvert.ParseInt(txtOrder.Value);
                            if (productCat.Ordering > 0)
                            {
                                genericBLL.Update(productCat, productCat, new string[] { "Id" });
                            }
                        }
                        catch { }
                    }
                }
            }
        }
        /// <summary>
        /// get msg
        /// </summary>
        private void getMessage()
        {
            string msg = Utils.GetParameter("msg", string.Empty);
            if (msg == string.Empty) return;
            if (msg == "save")
            {
                this.show_msg = string.Format("<div id=\"Cb-msg\"><div class=\"message\">{0}</div></div>", Constant.UI.admin_msg_save_success);
            }
            else if (msg == "delete")
            {
                this.show_msg = string.Format("<div id=\"Cb-msg\"><div class=\"message\">{0}</div></div>", Constant.UI.admin_msg_delete_success);
            }
        }
        private string getScmplit(int lvl)
        {
            string result = string.Empty;
            for (int i = 0; i < lvl; i++)
            {
                result += "&nbsp;&nbsp;&nbsp;";
            }
            result += result.Length > 0 ? "&nbsp;&nbsp;" : string.Empty;
            return result;
        }

        private void Search()
        {
            string strSearch = Request.Form[search.ClientID.Replace('_', '$')];
            this.search.Value = strSearch;
            strSearch = strSearch == null ? string.Empty : SanitizeHtml.Sanitize(strSearch);
            GetList(1, strSearch, this.currentPageIndex, 50);
        }

        protected void btn_Save_Click(object sender, ImageClickEventArgs e)
        {
            saveOrder();
            string url = Utils.CombineUrl(template_path, LinkHelper.GetAdminLink("servicescategory"));
            Response.Redirect(url);
        }


        #endregion

        #region Web Form Designer generated code

        /// <summary>
        /// init component
        /// </summary>
        override protected void OnInit(EventArgs e)
        {
            //InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            InitPage();
            //this.rptResult.ItemDataBound += new RepeaterItemEventHandler(rptResult_ItemDataBound);
        }
        /// <summary>
        /// ItemDataBound
        /// </summary>
        protected void rptResult_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string img, alt, publishedTask;
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("trList");
                HtmlInputText txt = null;
                if (e.Item.ItemIndex % 2 == 0)
                {

                    tr.Attributes.Add("class", "even");
                }
                else
                {
                    tr.Attributes.Add("class", "old");
                }

                try
                {
                    Medical_ServicesCategory data = (Medical_ServicesCategory)e.Item.DataItem;

                    //Role
                    Literal ltr = null;
                    ltr = (Literal)e.Item.FindControl("ltrchk");
                    ltr.Text = string.Format(@"<INPUT class='txt' TYPE='checkbox' ID='cb{0}' NAME='cid[]' value='{1}' onclick='isChecked(this.checked);' >",
                                                e.Item.ItemIndex, data.Id);
                    //Name
                    ltr = (Literal)e.Item.FindControl("ltrName");
                    ltr.Text = Utils.getScmplit(data.NewsCategoryDesc.Name, data.PathTreeDesc); //Server.HtmlDecode(getScmplit(data.Lvl) + "&bull; | " + data.Lvl + " | " + data.ProductCategoryDesc.Name);
                    //ltr.Text=
                    //image
                    if (data.Published == "1")
                    {
                        img = "tick.png";
                        alt = Constant.UI.admin_publish;
                        publishedTask = "unpublish";
                    }
                    else
                    {
                        img = "publish_x.png";
                        alt = Constant.UI.admin_unpublish;
                        publishedTask = "publish";
                    }
                    //Order
                    txt = (HtmlInputText)e.Item.FindControl("txtOrder");
                    txt.Value = DBConvert.ParseString(data.Ordering);
                    //Id
                    HtmlInputButton btId = (HtmlInputButton)e.Item.FindControl("btId");
                    btId.Value = DBConvert.ParseString(data.Id);
                    //set link
                    HyperLink hdflink = new HyperLink();
                    hdflink = (HyperLink)e.Item.FindControl("hdflink");
                    hdflink.NavigateUrl = template_path + LinkHelper.GetAdminLink("edit_servicescategory", data.Id);
                    //HtmlTableCell td = (HtmlTableCell)e.Item.FindControl("tdName");
                    //td.Attributes.Add("onclick", string.Format("listItemTask('cb{0}', 'edit')", e.Item.ItemIndex));
                    //td = (HtmlTableCell)e.Item.FindControl("trUpdateDate");
                    //td.Attributes.Add("onclick", string.Format("listItemTask('cb{0}', 'edit')", e.Item.ItemIndex));
                    ImageButton imgctr = (ImageButton)e.Item.FindControl("btnPublish");
                    imgctr.ImageUrl = string.Format("/Admin/images/{0}", img);
                    imgctr.Attributes.Add("alt", alt);
                    HtmlTableCell btn = (HtmlTableCell)e.Item.FindControl("tdbtn");
                    btn.Attributes.Add("onclick", string.Format(" return listItemTask('cb{0}', '{1}')", e.Item.ItemIndex, publishedTask));
                }
                catch { }

            }
        }

        /// <summary>
        /// Pager
        /// <summary>
        public void pager_Command(object sender, CommandEventArgs e)
        {
            this.currentPageIndex = Convert.ToInt32(e.CommandArgument);
            pager.CurrentIndex = this.currentPageIndex;
            this.GetList(1, string.Empty, this.currentPageIndex, 50);
        }

        //private string GetNameShow(string name, string pathtree)
        //{
        //    string re = string.Empty;
        //    int count = pathtree.Count(i => i.Equals('.')) - 1;
        //    for (int i = 0; i < count; i++)
        //    {
        //        re += " &nbsp;&nbsp;&nbsp;";
        //    }
        //    re += "&bull; |" + count + "|&nbsp;" + name;
        //    return re;
        //}

        //protected void btn_Save_Click(object sender, ImageClickEventArgs e)
        //{
        //    saveOrder();
        //    Response.Redirect(string.Format("{0}/admin/newscategory", WebUtils.GetHostPath()));
        //}
        #endregion
    }
}