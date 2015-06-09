// =============================================
// Author:		Congtt
// Create date: 22/09/2014
// Description:	danh sach sản phẩm
// =============================================

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
using Cb.Model.Products;
using Cb.BLL.Products;
using System.IO;
using System.Configuration;

namespace Cb.Web.Admin.Pages.Products
{
    public partial class admin_product : System.Web.UI.UserControl
    {
        #region Parameter

        private ProductBLL pcBll
        {
            get
            {
                if (ViewState["pcBll"] != null)
                    return (ProductBLL)ViewState["pcBll"];
                else return new ProductBLL();
            }
            set
            {
                ViewState["pcBll"] = value;
            }
        }
        private Generic<Medical_Product> genericBLL
        {
            get
            {
                if (ViewState["genericBLLget"] != null)
                    return (Generic<Medical_Product>)ViewState["genericBLLget"];
                else return new Generic<Medical_Product>();
            }
            set
            {
                ViewState["genericBLLget"] = value;
            }
        }
        private Generic2C<Medical_Product, Medical_ProductDesc> generic2CBLL
        {
            get
            {
                if (ViewState["generic2CBLL"] != null)
                    return (Generic2C<Medical_Product, Medical_ProductDesc>)ViewState["generic2CBLL"];
                else return new Generic2C<Medical_Product, Medical_ProductDesc>();
            }
            set
            {
                ViewState["generic2CBLL"] = value;
            }
        }

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
        protected string show_msg, action, l_search, records, msg_no_selected_item, msg_confirm_delete_item;

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

        private string categoryId
        {
            get
            {
                if (ViewState["CategoryID"] != null)
                    return ViewState["CategoryID"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                ViewState["CategoryID"] = value;
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
            categoryId = Utils.GetParameter("cid", string.Empty);

            pcBll = new ProductBLL();
            genericBLL = new Generic<Medical_Product>();
            generic2CBLL = new Generic2C<Medical_Product, Medical_ProductDesc>();
            this.template_path = WebUtils.GetWebPath();
            msg_confirm_delete_item = LocalizationUtility.GetText("mesConfirmDelete");
            msg_no_selected_item = LocalizationUtility.GetText("mesSelectItem");
            LocalizationUtility.SetValueControl(this);
            BindNewsCategory();
            GetMessage();
        }

        /// <summary>
        /// GetList
        /// </summary>
        /// <param name="cateId">Mảng cái ID con</param>
        /// <param name="content">Đây là ID trong GetList Bill, Where theo TitlUrl bảng Product</param>
        /// <returns></returns>
        private int GetList(byte langid, string content, string cateId, int begin, int end)
        {
            int total;
            IList<Medical_Product> lst = pcBll.GetList(1, string.Empty, string.Empty, cateId, content, null, string.Empty, begin, end, out total);

            this.records = DBConvert.ParseString(lst.Count);
            this.pager.PageSize = DBConvert.ParseInt(ConfigurationManager.AppSettings["PageSizeAdmin"]);
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
                    Add();
                    break;
                case "edit":
                    Edit(cid);
                    break;
                case "publish":
                    Change(cid, "1");
                    break;
                case "unpublish":
                    Change(cid, "0");
                    break;
                case "delete":
                    Delete(cid);
                    break;
                case "save":
                    SaveOrder();
                    string url = LinkHelper.GetAdminLink("news");
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

        /// <summary>
        ///Thêm Item
        /// </summary>
        private void Add()
        {
            string url = LinkHelper.GetAdminLink("edit_product");
            Response.Redirect(url);
        }

        /// <summary>
        /// Edit Item
        /// </summary>
        /// <param name="cid"></param>
        private void Edit(string cid)
        {
            if (cid == null) return;
            string link, url;
            string[] arrStr;
            if (cid.IndexOf(',') >= 0)
            {
                arrStr = cid.Split(',');
                link = LinkHelper.GetAdminLink("edit_product", arrStr[0]);
                //link = string.Format(SiteNavigation.link_adminPage_editproductcategory, arrStr[0]);
            }
            else
                link = LinkHelper.GetAdminLink("edit_product", cid);
            Response.Redirect(link);
        }

        /// <summary>
        /// change
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="state"></param>
        private void Change(string cid, string state)
        {
            if (cid != null)
            {
                genericBLL.ChangeWithTransaction(cid, state);
                Search();
            }
        }

        /// <summary>
        /// Delete image in folder and database
        /// </summary>
        private void Delete(string cid)
        {
            if (cid != null)
            {
                Medical_Product productCatObj = new Medical_Product();
                string[] fields = { "Id" };
                productCatObj.Id = DBConvert.ParseInt(cid);
                productCatObj = genericBLL.Load(productCatObj, new string[] { "Id" });
                //string f = Path.Combine(Server.MapPath(Constant.DSC.ProductUploadFolder), strHeaderProduct.Text.Trim().Remove(0, 5), productCatObj.Image);
                //if (File.Exists(f))
                //{
                //    try
                //    {
                //        File.Delete(f);
                //    }
                //    catch { }
                //}

                string link, url;
                if (generic2CBLL.Delete(cid))
                {
                    link = LinkHelper.GetAdminMsgLink("product", categoryId, "delete");
                }
                else
                    link = LinkHelper.GetAdminMsgLink("product", categoryId, "delfail");
                url = Utils.CombineUrl(template_path, link);
                Response.Redirect(url);
            }
        }

        /// <summary>
        /// saveOrder
        /// </summary>
        private void SaveOrder()
        {
            foreach (RepeaterItem item in rptResult.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    HtmlInputButton btId = (HtmlInputButton)item.FindControl("btId");
                    Medical_Product productCat = new Medical_Product();
                    productCat.Id = DBConvert.ParseInt(btId.Value);
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
        private void GetMessage()
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

        //private string getScmplit(int lvl)
        //{
        //    string result = string.Empty;
        //    for (int i = 0; i < lvl; i++)
        //    {
        //        result += "&nbsp;&nbsp;&nbsp;";
        //    }
        //    result += result.Length > 0 ? "&nbsp;&nbsp;" : string.Empty;
        //    return result;
        //}

        /// <summary>
        /// Tìm kiếm
        /// </summary>
        private void Search()
        {
            string strSearch = Request.Form[search.ClientID.Replace('_', '$')];
            this.search.Value = strSearch;
            strSearch = strSearch == null ? string.Empty : Utils.RemoveUnicode(SanitizeHtml.Sanitize(strSearch));

            GetList(1, strSearch, GetAllChildCategory(), this.currentPageIndex, DBConvert.ParseInt(ConfigurationManager.AppSettings["PageSizeAdmin"]));
        }

        /// <summary>
        /// Gán dữ liệu cho drpNewsCategory
        /// Vi Get du lieu theo Medical_ProductCategory, cid lay tu url xuong nên SelectedIndex theo cid        
        /// </summary>
        private void BindNewsCategory()
        {
            admin_editproduct.GetDataDropDownCategory(drpNewsCategory);
            //drpNewsCategory.SelectedIndex = 0;
            drpNewsCategory.SelectedIndex = drpNewsCategory.Items.IndexOf(drpNewsCategory.Items.FindByValue(ConfigurationManager.AppSettings["parentIdLeture"]));
            //drpNewsCategory.Attributes.Add("disabled", "disabled");
            //drpNewsCategory.Style.Add("background-color", "#dddddd");
        }

        private string GetAllChildCategory()
        {
            string arrId = "'''";
            ProductCategoryBLL newsCateBll = new ProductCategoryBLL();
            IList<Medical_ProductCategory> lst = newsCateBll.GetAllChild(DBConvert.ParseInt(drpNewsCategory.SelectedValue), true);
            arrId = arrId + Utils.ArrayToString<Medical_ProductCategory>((List<Medical_ProductCategory>)lst, "Id", "'',''") + "'''";
            return arrId;// !string.IsNullOrEmpty(arrId) ? arrId : "-1011";
        }

        #endregion

        #region Event

        protected void Page_Load(object sender, EventArgs e)
        {
            GetAction();
            BindNewsCategory();
            if (!IsPostBack)
            {
                InitializeComponent();
                Search();
            }
        }

        /// <summary>
        /// init component
        /// </summary>
        override protected void OnInit(EventArgs e)
        {
            //InitializeComponent();
            //  base.OnInit(e);
        }

        /// <summary>
        /// Nhiệm vụ: 1.Khởi tạo đối tượng; 2. Set text control.
        /// </summary>
        private void InitializeComponent()
        {
            InitPage();

            strHeaderProduct.Text = drpNewsCategory.SelectedItem.Text;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveOrder();
            string url = Utils.CombineUrl(template_path, LinkHelper.GetAdminLink("product"));
            Response.Redirect(url);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
            //rptResult.DataBind();
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
                    Medical_Product data = (Medical_Product)e.Item.DataItem;

                    //Role
                    Literal ltr = null;
                    ltr = (Literal)e.Item.FindControl("ltrchk");
                    ltr.Text = string.Format(@"<INPUT class='txt' TYPE='checkbox' ID='cb{0}' NAME='cid[]' value='{1}' onclick='isChecked(this.checked);' >",
                                                e.Item.ItemIndex, data.Id);

                    //ltrNewsCategory
                    ltr = (Literal)e.Item.FindControl("ltrNewsCategory");
                    ltr.Text = data.CategoryDesc;
                    //Name
                    ltr = (Literal)e.Item.FindControl("ltrName");
                    ltr.Text = data.ProductDesc.Title; //Server.HtmlDecode(getScmplit(data.Lvl) + "&bull; | " + data.Lvl + " | " + data.ProductCategoryDesc.Name);
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
                    hdflink.NavigateUrl = template_path + LinkHelper.GetAdminLink("edit_product", data.CategoryId.ToString(), data.Id.ToString());
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

        #endregion
    }
}