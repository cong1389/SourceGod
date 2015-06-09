using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cb.IDAL;
using Cb.Utility;
using Cb.DALFactory;
using Cb.DBUtility;
using System.Data;
using System.Data.Common;
using System.Web.UI.WebControls;
using System.Configuration;
using Cb.Model;

namespace Cb.BLL
{
    [Serializable]
    public class UploadImageBLL
    {
        private static IGeneric<Medical_UploadImage> dal;
        private string prefixParam;
        public UploadImageBLL()
        {
            Type t = typeof(Cb.SQLServerDAL.Generic<Medical_UploadImage>);
            dal = DataAccessGeneric<Medical_UploadImage>.CreateSession(t.FullName);

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

        public IList<Medical_UploadImage> GetList(string id, int productid, string publish, int pageIndex, int pageSize, out int total)
        {
            IList<Medical_UploadImage> lst = new List<Medical_UploadImage>();
            DGCParameter[] param = new DGCParameter[5];
            total = 0;
            if (!string.IsNullOrEmpty(id))
                param[0] = new DGCParameter(string.Format("{0}id", prefixParam), DbType.String, id);
            else
                param[0] = new DGCParameter(string.Format("{0}id", prefixParam), DbType.String, DBNull.Value);

            if (pageIndex != int.MinValue)
                param[1] = new DGCParameter(string.Format("{0}pageIndex", prefixParam), DbType.Int16, pageIndex);
            else
                param[1] = new DGCParameter(string.Format("{0}pageIndex", prefixParam), DbType.Int16, DBNull.Value);

            if (pageSize != int.MinValue)
                param[2] = new DGCParameter(string.Format("{0}pageSize", prefixParam), DbType.Int16, pageSize);
            else
                param[3] = new DGCParameter(string.Format("{0}pageSize", prefixParam), DbType.Int16, DBNull.Value);

            if (!string.IsNullOrEmpty(publish))
                param[3] = new DGCParameter(string.Format("{0}published", prefixParam), DbType.AnsiString, publish);
            else
                param[3] = new DGCParameter(string.Format("{0}published", prefixParam), DbType.AnsiString, DBNull.Value);

            if (productid != int.MinValue)
                param[4] = new DGCParameter(string.Format("{0}productid", prefixParam), DbType.Int32, productid);
            else
                param[4] = new DGCParameter(string.Format("{0}productid", prefixParam), DbType.Int32, DBNull.Value);

            lst = dal.GetList("sp_GetImageUpload", param, out total);
            return lst;
        }

        public int Insert(string action, string imageName, string createdBy, out int total)
        {
            DGCParameter[] param = new DGCParameter[3];
            total = 0;
            if (!string.IsNullOrEmpty(action))
                param[0] = new DGCParameter(string.Format("{0}action", prefixParam), DbType.String, action);
            else
                param[0] = new DGCParameter(string.Format("{0}action", prefixParam), DbType.String, DBNull.Value);

            if (!string.IsNullOrEmpty(imageName))
                param[1] = new DGCParameter(string.Format("{0}imageName", prefixParam), DbType.String, imageName);
            else
                param[1] = new DGCParameter(string.Format("{0}imageName", prefixParam), DbType.String, DBNull.Value);

            //param[2] = new DGCParameter(string.Format("{0}image", prefixParam), DbType.Byte, image);

            if (!string.IsNullOrEmpty(createdBy))
                param[2] = new DGCParameter(string.Format("{0}createdBy", prefixParam), DbType.String, createdBy);
            else
                param[2] = new DGCParameter(string.Format("{0}createdBy", prefixParam), DbType.String, DBNull.Value);

            dal.GetList("sp_ImageUpload", param, out total);
            return total;
        }
    }
}
