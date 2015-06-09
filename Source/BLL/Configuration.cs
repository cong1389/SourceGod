using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cb.IDAL;
using Cb.Utility;
using Cb.DALFactory;
using Cb.DBUtility;
using System.Data;
using System.Configuration;

namespace Cb.BLL
{
    [Serializable]
    public class ConfigurationBLL
    {
        private static IGeneric<Medical_Configuration> dal_2C;
        private string prefixParam;
        public ConfigurationBLL()
        {
            Type t = typeof(Cb.SQLServerDAL.Generic<Medical_Configuration>);
            dal_2C = DataAccessGeneric<Medical_Configuration>.CreateSession(t.FullName);

            switch (ConfigurationManager.AppSettings["Database"])
            {
                case "SQLServer":
                    prefixParam = "@";
                    break;
                case "MySQL":
                    prefixParam = "v_";
                    break;
            }
        }

        public IList<Medical_Configuration> GetList()
        {
            IList<Medical_Configuration> lst = new List<Medical_Configuration>();

            lst = dal_2C.GetAllBy(new Medical_Configuration(), null, null);

            return lst;
        }

        /// <summary>
        /// SaveConfig
        /// </summary>
        public void SaveConfig(string email, string phone, string sitename, string fax, string skype, string yahoo, string companyName, string address, string address1, string logoHeader, string logoFooter, string location, string title, string metaDescription, string metaKeyword)
        {
            Generic<Medical_Configuration> cf = new Generic<Medical_Configuration>();
            Medical_Configuration obj = new Medical_Configuration();
            DGCParameter[] param = new DGCParameter[15];
            if (!string.IsNullOrEmpty(email))
                param[0] = new DGCParameter(string.Format("{0}config_email", prefixParam), DbType.String, email);
            else
                param[0] = new DGCParameter(string.Format("{0}config_email", prefixParam), DbType.String, DBNull.Value);

            if (!string.IsNullOrEmpty(phone))
                param[1] = new DGCParameter(string.Format("{0}config_phone", prefixParam), DbType.String, phone);
            else
                param[1] = new DGCParameter(string.Format("{0}config_phone", prefixParam), DbType.String, DBNull.Value);

            if (!string.IsNullOrEmpty(sitename))
                param[2] = new DGCParameter(string.Format("{0}config_sitename", prefixParam), DbType.String, sitename);
            else
                param[2] = new DGCParameter(string.Format("{0}config_sitename", prefixParam), DbType.String, DBNull.Value);

            if (!string.IsNullOrEmpty(fax))
                param[3] = new DGCParameter(string.Format("{0}config_fax", prefixParam), DbType.String, fax);
            else
                param[3] = new DGCParameter(string.Format("{0}config_fax", prefixParam), DbType.String, DBNull.Value);

            if (!string.IsNullOrEmpty(skype))
                param[4] = new DGCParameter(string.Format("{0}config_skypeid", prefixParam), DbType.String, skype);
            else
                param[4] = new DGCParameter(string.Format("{0}config_skypeid", prefixParam), DbType.String, DBNull.Value);

            if (!string.IsNullOrEmpty(yahoo))
                param[5] = new DGCParameter(string.Format("{0}config_yahooid", prefixParam), DbType.String, yahoo);
            else
                param[5] = new DGCParameter(string.Format("{0}config_yahooid", prefixParam), DbType.String, DBNull.Value);

            if (!string.IsNullOrEmpty(companyName))
                param[6] = new DGCParameter(string.Format("{0}config_company_name_vi", prefixParam), DbType.String, companyName);
            else
                param[6] = new DGCParameter(string.Format("{0}config_company_name_vi", prefixParam), DbType.String, DBNull.Value);

            if (!string.IsNullOrEmpty(address))
                param[7] = new DGCParameter(string.Format("{0}config_address_vi", prefixParam), DbType.String, address);
            else
                param[7] = new DGCParameter(string.Format("{0}config_address_vi", prefixParam), DbType.String, DBNull.Value);

            if (!string.IsNullOrEmpty(address1))
                param[8] = new DGCParameter(string.Format("{0}config_address1_vi", prefixParam), DbType.String, address1);
            else
                param[8] = new DGCParameter(string.Format("{0}config_address1_vi", prefixParam), DbType.String, DBNull.Value);

            if (!string.IsNullOrEmpty(logoFooter))
                param[9] = new DGCParameter(string.Format("{0}config_logoFooter", prefixParam), DbType.String, logoFooter);
            else
                param[9] = new DGCParameter(string.Format("{0}config_logoFooter", prefixParam), DbType.String, DBNull.Value);

            if (!string.IsNullOrEmpty(location))
                param[10] = new DGCParameter(string.Format("{0}config_location", prefixParam), DbType.String, location);
            else
                param[10] = new DGCParameter(string.Format("{0}config_location", prefixParam), DbType.String, DBNull.Value);

            if (!string.IsNullOrEmpty(logoHeader))
                param[11] = new DGCParameter(string.Format("{0}config_logoHeader", prefixParam), DbType.String, logoHeader);
            else
                param[11] = new DGCParameter(string.Format("{0}config_logoHeader", prefixParam), DbType.String, DBNull.Value);

            if (!string.IsNullOrEmpty(title))
                param[12] = new DGCParameter(string.Format("{0}title", prefixParam), DbType.String, title);
            else
                param[12] = new DGCParameter(string.Format("{0}title", prefixParam), DbType.String, DBNull.Value);

            if (!string.IsNullOrEmpty(metaDescription))
                param[13] = new DGCParameter(string.Format("{0}metaDescription", prefixParam), DbType.String, metaDescription);
            else
                param[13] = new DGCParameter(string.Format("{0}metaDescription", prefixParam), DbType.String, DBNull.Value);

            if (!string.IsNullOrEmpty(metaKeyword))
                param[14] = new DGCParameter(string.Format("{0}metaKeyword", prefixParam), DbType.String, metaKeyword);
            else
                param[14] = new DGCParameter(string.Format("{0}metaKeyword", prefixParam), DbType.String, DBNull.Value);

            cf.ExcuteNonQueryFromStore("Configuration_Update", param);

        }

    }
}
