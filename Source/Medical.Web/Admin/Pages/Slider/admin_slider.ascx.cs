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
using System.Configuration;
using System.IO;
using Cb.WebControls;
using Cb.Model;

namespace Cb.Web.Admin.Pages.Slider
{
    public partial class admin_slider : DGCUserControl
    {
        #region Parameter

        protected string template_path
        {
            get
            {
                if (ViewState["template_path"] != null)
                    return ViewState["template_path"].ToString();
                else
                    return null;
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
        private BannerBLL pcBll
        {
            get
            {
                if (ViewState["pcBll"] != null)
                    return (BannerBLL)ViewState["pcBll"];
                else return new BannerBLL();
            }
            set
            {
                ViewState["pcBll"] = value;
            }
        }
        private Generic<Medical_Banner> genericBLL
        {
            get
            {
                if (ViewState["genericBLLget"] != null)
                    return (Generic<Medical_Banner>)ViewState["genericBLLget"];
                else return new Generic<Medical_Banner>();
            }
            set
            {
                ViewState["genericBLLget"] = value;
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

        #region Common


        /// <summary>
        /// Init page
        /// </summary>
        private void InitPage()
        {
            genericBLL = new Generic<Medical_Banner>();
            this.template_path = WebUtils.GetWebPath();
            msg_confirm_delete_item = LocalizationUtility.GetText("mesConfirmDelete");
            msg_no_selected_item = LocalizationUtility.GetText("mesSelectItem");
            LocalizationUtility.SetValueControl(this);
            BindNewsCategory();
            getMessage();
        }

        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private int GetList(int position, string content, int begin, int end)
        {
            int total;
            pcBll = new BannerBLL();
            IList<Medical_Banner> lst = pcBll.GetList(position, content, string.Empty, begin, end, out total);
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
                    string url = LinkHelper.GetAdminLink("slider");
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
            string url = LinkHelper.GetAdminLink("edit_slider");
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
                link = LinkHelper.GetAdminLink("edit_slider", arrStr[0]);
            }
            else
                link = LinkHelper.GetAdminLink("edit_slider", cid);
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

                string link, url;

                if (genericBLL.Delete(cid))
                    link = LinkHelper.GetAdminMsgLink("slider", "delete");
                else
                    link = LinkHelper.GetAdminMsgLink("slider", "delfail");
                url = Utils.CombineUrl(template_path, link);
                Response.Redirect(url);

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
                    Medical_Banner productCat = new Medical_Banner();
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
                this.show_msg = string.Format("<div id=\"dgc-msg\"><div class=\"message\">{0}</div></div>", Constant.UI.admin_msg_save_success);
            }
            else if (msg == "delete")
            {
                this.show_msg = string.Format("<div id=\"dgc-msg\"><div class=\"message\">{0}</div></div>", Constant.UI.admin_msg_delete_success);
            }
        }

        private void Search()
        {
            string strSearch = Request.Form[search.ClientID.Replace('_', '$')];
            this.search.Value = strSearch;
            strSearch = strSearch == null ? string.Empty : SanitizeHtml.Sanitize(strSearch);
            GetList(DBConvert.ParseInt(drpPosition.SelectedValue), strSearch, this.currentPageIndex, 50);
        }

        private void BindNewsCategory()
        {
            XMLConfigBLL bll = new XMLConfigBLL();
            bll.getDataDropDownCategory(drpPosition, Constant.DSC.IdXmlBannerPostion);
        }

        private string GetPosition(int position)
        {
            ListItem lst = drpPosition.Items.FindByValue(position.ToString());
            if (lst != null)
                return lst.Text;
            else
                return string.Empty;
        }

        #endregion

        #region Event

        /// <summary>
        /// init component
        /// </summary>
        override protected void OnInit(EventArgs e)
        {
            //InitializeComponent();
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            GetAction();
            if (!IsPostBack)
            {
                InitializeComponent();
                Search();
            }
        }

        private void InitializeComponent()
        {
            InitPage();
            //this.rptResult.ItemDataBound += new RepeaterItemEventHandler(rptResult_ItemDataBound);
            //search.
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
                    Medical_Banner data = (Medical_Banner)e.Item.DataItem;

                    //Role
                    Literal ltr = null;
                    ltr = (Literal)e.Item.FindControl("ltrchk");
                    ltr.Text = string.Format(@"<INPUT class='txt' TYPE='checkbox' ID='cb{0}' NAME='cid[]' value='{1}' onclick='isChecked(this.checked);' >",
                                                e.Item.ItemIndex, data.Id);
                    //Name
                    ltr = (Literal)e.Item.FindControl("ltrName");
                    ltr.Text = data.Name; //Server.HtmlDecode(getScmplit(data.Lvl) + "&bull; | " + data.Lvl + " | " + data.ProductCategoryDesc.Name);
                    //Position
                    ltr = (Literal)e.Item.FindControl("ltrPosition");
                    ltr.Text = GetPosition(data.Position);
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

                    DGCBannerControl banner = e.Item.FindControl("ucBanner") as DGCBannerControl;
                    banner.FilePath = WebUtils.GetUrlImage("UploadSlider", data.Image, "SrcNoImage");
                    //Order
                    txt = (HtmlInputText)e.Item.FindControl("txtOrder");
                    txt.Value = DBConvert.ParseString(data.Ordering);
                    //Id
                    HtmlInputButton btId = (HtmlInputButton)e.Item.FindControl("btId");
                    btId.Value = DBConvert.ParseString(data.Id);
                    //set link
                    HyperLink hdflink = new HyperLink();
                    hdflink = (HyperLink)e.Item.FindControl("hdflink");
                    hdflink.NavigateUrl = template_path + LinkHelper.GetAdminLink("edit_slider", data.Id);
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
            Search();
            //this.GetList(1, string.Empty, this.currentPageIndex, Constant.DSC.PageSize);
        }

        protected void btn_Save_Click(object sender, ImageClickEventArgs e)
        {
            saveOrder();
            string url = Utils.CombineUrl(template_path, LinkHelper.GetAdminLink("slider"));
            Response.Redirect(url);
        }

        //protected void btnSearch_Click(object sender, EventArgs e)
        //{
        //    Search();
        //}

        #endregion
    }
}