using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Cb.DBUtility;
using Cb.Utility;
using Cb.Utility;
using Cb.BLL;
using Cb.Localization;
using Cb.Web;

namespace Web.Admin.Pages.User
{
    public partial class admin_edituser : System.Web.UI.UserControl
    {
        #region Fields
        protected string template_path;
        protected string l_btn_save;
        protected string l_btn_apply;
        protected string l_btn_delete;
        protected string l_btn_cancel;
        protected string show_msg;
        protected string header_name;
        protected string l_publish;
        protected string l_username;
        protected string l_password;
        protected string l_confirmpassword;
        protected string l_name;
        protected string l_email;
        protected string l_phone;
        protected string l_address;
        protected string l_permission;
        protected string l_dept;
        //alert
        protected string msg_confirm_delete_item;

        protected int id = int.MinValue;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            btn_Delete.Attributes["onclick"] = string.Format("javascript:return confirm('{0}');", Constant.UI.admin_msg_confirm_delete_item);

            //check role
            Medical_User user = (Medical_User)Session[Global.SESS_USER];
            if (user != null && user.Role != DBConvert.ParseByte(Constant.Security.AdminRoleValue))
            {
                Response.Redirect(LinkHelper.GetAdminLink("home"));
            }
            //end
            GetId();
            InitPage();
            if (!IsPostBack)
            {
                LocalizationUtility.SetValueControl(this);
                ShowUser();
            }
        }

        #region Common
        private void LoadDataDropdownlist(DropDownList _drp)
        {
            int total;
            LocationBLL pcBll = new LocationBLL();
            string strTemp;
            _drp.Items.Clear();
            _drp.Items.Add(new ListItem(LocalizationUtility.GetText("enuChat_All_none"), int.MinValue.ToString()));
            IList<Medical_Location> lst = pcBll.GetList(Constant.DB.LangId, string.Empty, 1, 300, out total);
            if (lst != null && lst.Count > 0)
            {
                foreach (Medical_Location item in lst)
                {
                    strTemp = item.ObjLocDesc.Name;
                    _drp.Items.Add(new ListItem(strTemp, DBConvert.ParseString(item.Id)));
                }
            }
        }
        /// <summary>
        /// InitPage
        /// </summary>
        private void InitPage()
        {
            this.template_path = WebUtils.GetWebPath();
            this.header_name = Constant.UI.admin_users_header_title_edit;
            this.l_btn_apply = Constant.UI.admin_apply;
            this.l_btn_save = Constant.UI.admin_save;
            this.l_btn_delete = Constant.UI.admin_delete;
            this.l_btn_cancel = Constant.UI.admin_cancel;
            this.l_publish = Constant.UI.admin_publish;
            this.l_username = Constant.UI.admin_user_username;
            this.l_password = Constant.UI.admin_user_password;
            this.l_confirmpassword = Constant.UI.admin_user_confirmpassword;
            this.l_name = Constant.UI.admin_name;
            this.l_email = Constant.UI.admin_user_email;
            this.l_phone = Constant.UI.admin_user_phone;
            //this.ltr_Mobile.Text = Constant.UI.admin_user_mobile;
            this.l_address = Constant.UI.admin_user_address;
            //this.ltr_city.Text = Constant.UI.admin_user_city;
            this.l_permission = Constant.UI.admin_user_permission_label;
            //this.l_dept = Constant.UI.admin_user_dept;

            //this.ltrNoteUsername.Text = Constant.UI.admin_user_note_username;
            //this.ltrNotePassword.Text = Constant.UI.admin_user_note_psaaword;
            //alert
            this.msg_confirm_delete_item = Constant.UI.admin_msg_confirm_delete_item;
            this.reqv_txtUsername.ErrorMessage = Constant.UI.alert_empty_username;
            this.reqv_txtPassword.ErrorMessage = Constant.UI.alert_empty_password;
            this.reqvc_txtConfirmpassword.ErrorMessage = Constant.UI.alert_empty_password2;
            this.comv_Password.ErrorMessage = Constant.UI.alert_invalid_password2;
            this.cusv_txtUsername.ErrorMessage = Constant.UI.alert_empty_username;
            this.reqvc_txtEmail.ErrorMessage = Constant.UI.alert_empty_email;
            this.regv_Email.ErrorMessage = Constant.UI.alert_invalid_email;
            this.btn_Delete.OnClientClick = "javascript:return confirmDelete('" + msg_confirm_delete_item + "');";
            this.reqvc_txtFullName.ErrorMessage = Constant.UI.alert_empty_name_outsite;
            regv_txtPhone.ErrorMessage = Constant.UI.alert_invalid_phone;
            regv_txtMobile.ErrorMessage = Constant.UI.alert_invalid_mobile;
            //load data drop down list
            LoadDataDropdownlist(drpCity);
            UserBLL.BindRoleName(drpPermission);

            //Init Validate string
            regv_txtPhone.ValidationExpression = Constant.RegularExpressionString.validatePhone;
            regv_txtMobile.ValidationExpression = Constant.RegularExpressionString.validatePhone;
            regv_Email.ValidationExpression = Constant.RegularExpressionString.validateEmail;
            //Event server validate
            cusv_txtUsername.ServerValidate += new ServerValidateEventHandler(cusv_txtUsername_ServerValidate);
            this.cus_checkPassWord.ErrorMessage = Constant.UI.msg_account_password_short;


            //Image
            btn_Save.ImageUrl = string.Format("{0}/{1}",template_path, "images/save_f2.png");
            btn_Apply.ImageUrl = string.Format("{0}/{1}", template_path, "images/apply_f2.png");
            btn_Delete.ImageUrl = string.Format("{0}/{1}", template_path, "images/delete_f2.png");
            btn_Cancel.ImageUrl = string.Format("{0}/{1}", template_path, "images/cancel_f2.png");
        }

        private void GetId()
        {
            //get ID param 
            string strID = Utils.GetParameter("id", string.Empty);
            this.id = strID == string.Empty ? int.MinValue : DBConvert.ParseInt(strID);
        }
        #endregion

        #region Methods
        /// <summary>
        /// get data for insert update
        /// </summary>
        /// <param name="userObj"></param>
        /// <returns></returns>
        private Medical_User getDataObject(Medical_User userObj)
        {
            userObj.Published = chkPublished.Checked ? "1" : "0";
            userObj.Username = txtUsername.Value;
            if (txtPassword.Value.Length > 0)
            {
                userObj.Password = Global.ToEncoding(txtPassword.Value);
            }
            userObj.FullName = Server.HtmlEncode(txtFullName.Value);
            userObj.Address = Server.HtmlEncode(txtAddress.Value);
            userObj.Email = txtEmail.Value;
            userObj.Phone = txtPhone.Value;
            userObj.Mobile = txtMobile.Value;
            if (drpCity.SelectedValue != string.Empty)
            {
                userObj.LocationId = DBConvert.ParseInt(drpCity.SelectedValue);
            }
            //Set Role = 3 when no select
            if (drpPermission.SelectedValue != "0")
                userObj.Role = byte.Parse(drpPermission.SelectedValue);
            else
                userObj.Role = 3;
            //if (!string.IsNullOrEmpty(drpDept.SelectedValue))
            //    userObj.DeptId = DBConvert.ParseInt(drpDept.SelectedValue);
            userObj.IsNewsletter = cbxNewsPromo.Checked ? "1" : "0";
            return userObj;
        }
        /// <summary>
        /// Save user
        /// </summary>
        private void SaveUser(int userID)
        {

            Medical_User userObj = new Medical_User();
            Generic<Medical_User> sdUser = new Generic<Medical_User>();
            //truong hop insert
            if (this.id == int.MinValue)
            {
                getDataObject(userObj);
                userObj.PostDate = DateTime.Now;
                userObj.UpdateDate = DateTime.Now;
                userObj.IsNewsletter = "0";
                //execute
                this.id = sdUser.Insert(userObj);
                //this.id = Medical_User.Insert(userObj);
            }
            else
            {
                string[] fields = { "Id" };
                userObj.Id = this.id;
                userObj = sdUser.Load(userObj, fields);
                getDataObject(userObj);
                userObj.UpdateDate = DateTime.Now;
                sdUser.Update(userObj, userObj, fields);
            }
        }
        /// <summary>
        /// Show user
        /// </summary>
        private void ShowUser()
        {
            if (this.id != int.MinValue)
            {
                Medical_User userObj = new Medical_User();
                Generic<Medical_User> sdUser = new Generic<Medical_User>();
                string[] fields = { "Id" };
                userObj.Id = this.id;
                userObj = sdUser.Load(userObj, fields);
                this.chkPublished.Checked = userObj.Published == "1" ? true : false;
                this.txtFullName.Value = Server.HtmlDecode(userObj.FullName);
                this.txtUsername.Value = Server.HtmlDecode(userObj.Username);
                this.txtPassword.Value = Global.ToDecoding(Server.HtmlEncode(userObj.Password));
                this.txtConfirmpassword.Value = Global.ToDecoding(Server.HtmlEncode(userObj.Password));
                this.txtEmail.Value = userObj.Email;
                this.txtPhone.Value = userObj.Phone;
                this.txtMobile.Value = userObj.Mobile;
                this.txtAddress.Value = Server.HtmlDecode(userObj.Address);
                this.drpPermission.SelectedValue = userObj.Role.ToString();
                this.drpCity.SelectedValue = userObj.LocationId.ToString();
                //this.drpDept.SelectedValue = userObj.DeptId.ToString();
                this.reqv_txtPassword.Visible = false;
                this.reqvc_txtConfirmpassword.Visible = false;
                this.cusv_txtUsername.Visible = false;
                txtUsername.Disabled = true;
                cbxNewsPromo.Checked = userObj.IsNewsletter == "1" ? true : false;
            }
            else
            {
                this.reqv_txtPassword.Visible = true;
                this.reqvc_txtConfirmpassword.Visible = true;
                this.cusv_txtUsername.Visible = true;
            }
        }
        /// <summary>
        /// Apply user
        /// </summary>
        private void ApplyUser()
        {
            SaveUser(this.id);
        }
        /// <summary>
        /// delete user
        /// </summary>
        /// <param name="cid"></param>
        private void deleteUsers(string cid)
        {
            if (cid != null)
            {
                string link, url;
                Generic<Medical_User> sdUser = new Generic<Medical_User>();
                if (sdUser.Delete(cid))
                    link = LinkHelper.GetAdminMsgLink("user", "delete");
                else
                    link = LinkHelper.GetAdminMsgLink("user", "delfail");
                url = Utils.CombineUrl(template_path, link);
                Response.Redirect(url);
            }
        }
        /// <summary>
        /// Cancel user
        /// </summary>
        private void CancelUser()
        {
            Response.Redirect(LinkHelper.GetAdminLink("user"));

        }


        #region Event
        protected void btn_Save_Click(object sender, ImageClickEventArgs e)
        {
            if (Page.IsValid)
            {
                ApplyUser();
                Response.Redirect(LinkHelper.GetAdminMsgLink("user", "save"));
            }
        }
        protected void btn_Apply_Click(object sender, ImageClickEventArgs e)
        {
            if (Page.IsValid)
            {
                ApplyUser();
                Response.Redirect(LinkHelper.GetAdminLink("edit_user", this.id));
            }
        }
        protected void btn_Delete_Click(object sender, ImageClickEventArgs e)
        {
            deleteUsers(DBConvert.ParseString(this.id));
        }
        protected void btn_Cancel_Click(object sender, ImageClickEventArgs e)
        {
            CancelUser();
        }

        void cusv_txtUsername_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //if (!UserBLL.CheckValidUsername(txtUsername.Value))
            //{
            //    args.IsValid = false;
            //    ((CustomValidator)source).Text = Constant.UI.alert_invalid_username;
            //}
        }
        #endregion

        #endregion
    }
}