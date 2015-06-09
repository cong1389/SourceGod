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
using System.Text;
using Cb.BLL;
using Cb.Model;

namespace Cb.Web.Controls
{
    public partial class block_music : DGCUserControl
    {
        #region Parameter

        protected string template_path, pageName, cid, id, url, script;
        int total, productId;

        ProductBLL pcBll = new ProductBLL();
        IList<Medical_Product> lstProduct;

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
            id = Utils.GetParameter("id", string.Empty);

            GetListProduct();
        }

        /// <summary>
        /// Lấy danh sách Audio: mp3
        /// </summary>
        /// <returns></returns>
        private void GetListProduct()
        {
            lstProduct = pcBll.GetList(LangInt, pageName, "1", string.Empty, cid, string.Empty, string.Empty, string.Empty, currentPageIndex, DBConvert.ParseInt(ConfigurationManager.AppSettings["pageSizeBlogLeture"]), out  total);

            if (total > 0)
            {
                productId = lstProduct[0].Id;
                UploadImageBLL bll = new UploadImageBLL();
                IList<Medical_UploadImage> lstUploadImage = bll.GetList(string.Empty, productId, "1", 1, 100, out  total);
                if (total > 0)
                {
                    #region Play audio

                    if (lstUploadImage[0].Name.Contains("jpg") || lstUploadImage[0].Name.Contains("jpeg") || lstUploadImage[0].Name.Contains("png")
                      || lstUploadImage[0].Name.Contains("gif") || lstUploadImage[0].Name.Contains("bmp") || lstUploadImage[0].Name.Contains("mp3"))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(" new jPlayerPlaylist({ ");
                        sb.AppendLine(" jPlayer: \"#jquery_jplayer_N\",");
                        sb.AppendLine(" cssSelectorAncestor: \"#jp_container_N\"");
                        sb.AppendLine("  },[ ");

                        for (int i = 0; i < total; i++)
                        {
                            string dirUpload = Utils.CombineUrl(template_path, ConfigurationManager.AppSettings["ProductUpload"]);
                            string pathMp3 = string.Format("{0}/{1}", dirUpload, lstUploadImage[i].Name);
                            string pathOgg = pathMp3.Replace("mp3", "ogg");
                            url = WebUtils.GetUrlImage(ConfigurationManager.AppSettings["ProductUpload"], lstProduct[0].Image);
                            sb.AppendLine("  {");
                            sb.AppendLine(" title: \"" + lstUploadImage[i].Name + " \",");
                            sb.AppendLine(" artist: \" " + lstUploadImage[i].Name + "   \",");
                            sb.AppendLine(" mp3: \" " + pathMp3 + "  \",");
                            sb.AppendLine(" oga: \" " + pathOgg + "  \",");
                            sb.AppendLine(" poster: \" " + url + "  \"");
                            sb.AppendLine(" },");
                        }

                        sb.AppendLine(" ],");
                        sb.AppendLine(" {");
                        sb.AppendLine(" playlistOptions: {");
                        sb.AppendLine(" enableRemoveControls: true");
                        sb.AppendLine(" },");
                        sb.AppendLine(" swfPath: \"js\",");
                        sb.AppendLine(" supplied: \"webmv, ogv, m4v, oga, mp3\",");
                        sb.AppendLine(" smoothPlayBar: true,");
                        sb.AppendLine(" keyEnabled: true,");
                        sb.AppendLine(" audioFullScreen: true ,");
                        sb.AppendLine("size: { height: \"0%\" }");
                        sb.AppendLine(" });");
                        script = sb.ToString();
                        divAudio.Visible = true;
                        divVideo.Visible = false;
                    }

                    #endregion

                    #region Play Video

                    else
                    {
                        fVideo.Attributes.Add("src", "//www.youtube.com/embed/" + lstUploadImage[0].Name + "?rel=0&amp;autoplay=1");

                        divAudio.Visible = false;
                        divVideo.Visible = true;
                    }

                    #endregion

                    ltrTitle.Text = lstProduct[0].ProductDesc.Title;
                }
            }
        }

        //private void SetValue()
        //{
        //    lstProduct = pcBll.GetList(LangInt, pageName, "1", string.Empty, cid, string.Empty, string.Empty, string.Empty, currentPageIndex, DBConvert.ParseInt(ConfigurationManager.AppSettings["pageSizeBlogLeture"]), out  total);

        //    string src, url = string.Empty;
        //    if (total > 0)
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        sb.Append(" new jPlayerPlaylist({ ");
        //        sb.AppendLine(" jPlayer: \"#jquery_jplayer_N\",");
        //        sb.AppendLine(" cssSelectorAncestor: \"#jp_container_N\"");
        //        sb.AppendLine("  },[ ");

        //        //for (int i = 0; i < total; i++)
        //        //{
        //        string dirUpload = Utils.CombineUrl(template_path, ConfigurationManager.AppSettings["ProductUpload"]);
        //        string pathMp3 = string.Format("{0}/{1}", dirUpload, lstProduct[0].Area);
        //        string pathOgg = pathMp3.Replace("mp3", "ogg");
        //        url = WebUtils.GetUrlImage(ConfigurationManager.AppSettings["ProductUpload"], lstProduct[0].Image);
        //        sb.AppendLine("  {");
        //        sb.AppendLine(" title: \"" + lstProduct[0].ProductDesc.Title + " \",");
        //        sb.AppendLine(" artist: \" " + lstProduct[0].ProductDesc.Brief + "   \",");
        //        sb.AppendLine(" mp3: \" " + pathMp3 + "  \",");
        //        sb.AppendLine(" oga: \" " + pathOgg + "  \",");
        //        sb.AppendLine(" poster: \" " + url + "  \"");
        //        sb.AppendLine(" },");
        //        //}

        //        sb.AppendLine(" ],");
        //        sb.AppendLine(" {");
        //        sb.AppendLine(" playlistOptions: {");
        //        sb.AppendLine(" enableRemoveControls: true");
        //        sb.AppendLine(" },");
        //        sb.AppendLine(" swfPath: \"js\",");
        //        sb.AppendLine(" supplied: \"webmv, ogv, m4v, oga, mp3\",");
        //        sb.AppendLine(" smoothPlayBar: true,");
        //        sb.AppendLine(" keyEnabled: true,");
        //        sb.AppendLine(" audioFullScreen: true ,");
        //        sb.AppendLine("size: { height: \"0%\" }");
        //        sb.AppendLine(" });");

        //        script = sb.ToString();

        //        ltrTitle.Text = lstProduct[0].ProductDesc.Title;
        //    }
        //}

        #endregion

        #region Event

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                InitPage();
            }
        }

        #endregion
    }
}