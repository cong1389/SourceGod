using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cb.IDAL;
using Cb.DALFactory;
using Cb.Utility;
using Cb.DBUtility;
using System.Data;
using System.Configuration;
using Cb.BLL;
using Cb.Model.Products;

namespace Cb.BLL.Products
{
    [Serializable]
    public class ProductBLL
    {
        private static IGeneric2C<Medical_Product, Medical_ProductDesc> dal_2C;

        private string prefixParam;

        public ProductBLL()
        {
            Type t = typeof(Cb.SQLServerDAL.Generic2C<Medical_Product, Medical_ProductDesc>);
            dal_2C = DataAccessGeneric2C<Medical_Product, Medical_ProductDesc>.CreateSession(t.FullName);

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

        public IList<Medical_Product> GetList(int langId, string name, string publish, string newsCateId, string id, int pageIndex, int pageSize, out int total)
        {
            return GetList(langId, name, publish, newsCateId, id, null, null, pageIndex, pageSize, out  total);
        }

        public IList<Medical_Product> GetList(int langId, string name, string publish, string newsCateId, string id, string hot, string feature, int pageIndex, int pageSize, out int total)
        {
            return GetList(langId, name, publish, newsCateId, id, hot, feature, null, pageIndex, pageSize, out  total);
        }

        public IList<Medical_Product> GetList(int langId, string name, string publish, string newsCateId, string id, string hot, string feature, string tag, int pageIndex, int pageSize, out int total)
        {
            IList<Medical_Product> lst = new List<Medical_Product>();
            DGCParameter[] param = new DGCParameter[10];
            if (langId != int.MinValue)
                param[0] = new DGCParameter(string.Format("{0}langId", prefixParam), DbType.Int16, langId);
            else
                param[0] = new DGCParameter(string.Format("{0}langId", prefixParam), DbType.Int16, DBNull.Value);

            if (!string.IsNullOrEmpty(name))
                param[1] = new DGCParameter(string.Format("{0}name", prefixParam), DbType.String, name);
            else
                param[1] = new DGCParameter(string.Format("{0}name", prefixParam), DbType.String, DBNull.Value);

            if (pageIndex != int.MinValue)
                param[2] = new DGCParameter(string.Format("{0}pageIndex", prefixParam), DbType.Int16, pageIndex);
            else
                param[2] = new DGCParameter(string.Format("{0}pageIndex", prefixParam), DbType.Int16, DBNull.Value);

            if (pageSize != int.MinValue)
                param[3] = new DGCParameter(string.Format("{0}pageSize", prefixParam), DbType.Int16, pageSize);
            else
                param[3] = new DGCParameter(string.Format("{0}pageSize", prefixParam), DbType.Int16, DBNull.Value);

            if (!string.IsNullOrEmpty(newsCateId))
                param[4] = new DGCParameter(string.Format("{0}cateId", prefixParam), DbType.String, newsCateId);
            else
                param[4] = new DGCParameter(string.Format("{0}cateId", prefixParam), DbType.String, DBNull.Value);

            if (!string.IsNullOrEmpty(id))
                param[5] = new DGCParameter(string.Format("{0}id", prefixParam), DbType.String, id);
            else
                param[5] = new DGCParameter(string.Format("{0}id", prefixParam), DbType.String, DBNull.Value);

            if (!string.IsNullOrEmpty(publish))
                param[6] = new DGCParameter(string.Format("{0}published", prefixParam), DbType.AnsiString, publish);
            else
                param[6] = new DGCParameter(string.Format("{0}published", prefixParam), DbType.AnsiString, DBNull.Value);

            if (!string.IsNullOrEmpty(hot))
                param[7] = new DGCParameter(string.Format("{0}hot", prefixParam), DbType.AnsiString, hot);
            else
                param[7] = new DGCParameter(string.Format("{0}hot", prefixParam), DbType.AnsiString, DBNull.Value);

            if (!string.IsNullOrEmpty(feature))
                param[8] = new DGCParameter(string.Format("{0}feature", prefixParam), DbType.AnsiString, feature);
            else
                param[8] = new DGCParameter(string.Format("{0}feature", prefixParam), DbType.AnsiString, DBNull.Value);

            if (!string.IsNullOrEmpty(tag))
                param[9] = new DGCParameter(string.Format("{0}tag", prefixParam), DbType.String, tag);
            else
                param[9] = new DGCParameter(string.Format("{0}tag", prefixParam), DbType.String, DBNull.Value);

            lst = dal_2C.GetList("sp_GetAllProduct", param, out total);
            return lst;
        }

        public IList<Medical_Product> GetListSearch(int langId, string name, string publish, string newsCateId, string id, int pageIndex, int pageSize, out int total)
        {
            IList<Medical_Product> lst = new List<Medical_Product>();
            DGCParameter[] param = new DGCParameter[7];
            if (langId != int.MinValue)
                param[0] = new DGCParameter(string.Format("{0}langId", prefixParam), DbType.Int16, langId);
            else
                param[0] = new DGCParameter(string.Format("{0}langId", prefixParam), DbType.Int16, DBNull.Value);

            if (!string.IsNullOrEmpty(name))
                param[1] = new DGCParameter(string.Format("{0}name", prefixParam), DbType.String, name);
            else
                param[1] = new DGCParameter(string.Format("{0}name", prefixParam), DbType.String, DBNull.Value);

            if (pageIndex != int.MinValue)
                param[2] = new DGCParameter(string.Format("{0}pageIndex", prefixParam), DbType.Int32, pageIndex);
            else
                param[2] = new DGCParameter(string.Format("{0}pageIndex", prefixParam), DbType.Int32, DBNull.Value);

            if (pageSize != int.MinValue)
                param[3] = new DGCParameter(string.Format("{0}pageSize", prefixParam), DbType.Int32, pageSize);
            else
                param[3] = new DGCParameter(string.Format("{0}pageSize", prefixParam), DbType.Int32, DBNull.Value);

            if (!string.IsNullOrEmpty(newsCateId))
                param[4] = new DGCParameter(string.Format("{0}cateId", prefixParam), DbType.String, newsCateId);
            else
                param[4] = new DGCParameter(string.Format("{0}cateId", prefixParam), DbType.String, DBNull.Value);

            if (!string.IsNullOrEmpty(id))
                param[5] = new DGCParameter(string.Format("{0}id", prefixParam), DbType.String, id);
            else
                param[5] = new DGCParameter(string.Format("{0}id", prefixParam), DbType.String, DBNull.Value);

            if (!string.IsNullOrEmpty(publish))
                param[6] = new DGCParameter(string.Format("{0}published", prefixParam), DbType.AnsiString, publish);
            else
                param[6] = new DGCParameter(string.Format("{0}published", prefixParam), DbType.AnsiString, DBNull.Value);

            lst = dal_2C.GetList("sp_GetAllProductSearch", param, out total);
            return lst;
        }

        public IList<Medical_Product> GetListRelate(int langId, string name, string newsCateId, string Id, int pageIndex, int pageSize, out int total)
        {
            IList<Medical_Product> lst = new List<Medical_Product>();
            DGCParameter[] param = new DGCParameter[6];
            if (langId != int.MinValue)
                param[0] = new DGCParameter(string.Format("{0}langId", prefixParam), DbType.Int16, langId);
            else
                param[0] = new DGCParameter(string.Format("{0}langId", prefixParam), DbType.Int16, DBNull.Value);

            if (!string.IsNullOrEmpty(name))
                param[1] = new DGCParameter(string.Format("{0}name", prefixParam), DbType.String, name);
            else
                param[1] = new DGCParameter(string.Format("{0}name", prefixParam), DbType.String, DBNull.Value);

            if (pageIndex != int.MinValue)
                param[2] = new DGCParameter(string.Format("{0}pageIndex", prefixParam), DbType.Int16, pageIndex);
            else
                param[2] = new DGCParameter(string.Format("{0}pageIndex", prefixParam), DbType.Int16, DBNull.Value);

            if (pageSize != int.MinValue)
                param[3] = new DGCParameter(string.Format("{0}pageSize", prefixParam), DbType.Int16, pageSize);
            else
                param[3] = new DGCParameter(string.Format("{0}pageSize", prefixParam), DbType.Int16, DBNull.Value);

            if (!string.IsNullOrEmpty(newsCateId))
                param[4] = new DGCParameter(string.Format("{0}cateId", prefixParam), DbType.String, newsCateId);
            else
                param[4] = new DGCParameter(string.Format("{0}cateId", prefixParam), DbType.String, DBNull.Value);

            if (!string.IsNullOrEmpty(Id))
                param[5] = new DGCParameter(string.Format("{0}Id", prefixParam), DbType.String, Id);
            else
                param[5] = new DGCParameter(string.Format("{0}Id", prefixParam), DbType.String, DBNull.Value);

            lst = dal_2C.GetList("sp_GetAllProductRelate", param, out total);
            return lst;
        }
    }
}
