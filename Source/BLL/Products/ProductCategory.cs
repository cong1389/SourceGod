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
using Cb.Model.Products;

namespace Cb.BLL.Products
{
    [Serializable]
    public class ProductCategoryBLL
    {
        private static IGeneric2C<Medical_ProductCategory, Medical_ProductCategoryDesc> dal_2C;

        private string prefixParam;

        public ProductCategoryBLL()
        {
            Type t = typeof(Cb.SQLServerDAL.Generic2C<Medical_ProductCategory, Medical_ProductCategoryDesc>);
            dal_2C = DataAccessGeneric2C<Medical_ProductCategory, Medical_ProductCategoryDesc>.CreateSession(t.FullName);

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

        public IList<Medical_ProductCategory> GetList(int langId, string name, int pageIndex, int pageSize, out int total)
        {
            return GetList(langId, name, "1", int.MinValue, false, string.Empty, pageIndex, pageSize, out  total);
        }

        public IList<Medical_ProductCategory> GetList(int langId, string name, string publish, int parentId, bool isTree, int pageIndex, int pageSize, out int total)
        {
            return GetList(langId, name, publish, int.MinValue, false, string.Empty, pageIndex, pageSize, out  total);
        }

        public IList<Medical_ProductCategory> GetList(int langId, string name, string publish, int parentId, bool isTree, string field, int pageIndex, int pageSize, out int total)
        {
            IList<Medical_ProductCategory> lst = new List<Medical_ProductCategory>();
            DGCParameter[] param = new DGCParameter[8];
            if (langId != int.MinValue)
                param[0] = new DGCParameter(string.Format("{0}langId", prefixParam), DbType.Int16, langId);
            else
                param[0] = new DGCParameter(string.Format("{0}langId", prefixParam), DbType.Int16, DBNull.Value);

            if (!string.IsNullOrEmpty(name))
                param[1] = new DGCParameter(string.Format("{0}name", prefixParam), DbType.String, name);
            else
                param[1] = new DGCParameter(string.Format("{0}name", prefixParam), DbType.String, DBNull.Value);

            if (parentId != int.MinValue)
                param[2] = new DGCParameter(string.Format("{0}parentid", prefixParam), DbType.Int32, parentId);
            else
                param[2] = new DGCParameter(string.Format("{0}parentid", prefixParam), DbType.Int32, DBNull.Value);

            param[3] = new DGCParameter(string.Format("{0}tree", prefixParam), DbType.Boolean, isTree);

            if (pageIndex != int.MinValue)
                param[4] = new DGCParameter(string.Format("{0}pageIndex", prefixParam), DbType.Int16, pageIndex);
            else
                param[4] = new DGCParameter(string.Format("{0}pageIndex", prefixParam), DbType.Int16, DBNull.Value);

            if (pageSize != int.MinValue)
                param[5] = new DGCParameter(string.Format("{0}pageSize", prefixParam), DbType.Int16, pageSize);
            else
                param[5] = new DGCParameter(string.Format("{0}pageSize", prefixParam), DbType.Int16, DBNull.Value);

            if (!string.IsNullOrEmpty(publish))
                param[6] = new DGCParameter(string.Format("{0}published", prefixParam), DbType.AnsiString, publish);
            else
                param[6] = new DGCParameter(string.Format("{0}published", prefixParam), DbType.AnsiString, DBNull.Value);

            if (!string.IsNullOrEmpty(field))
                param[7] = new DGCParameter(string.Format("{0}field", prefixParam), DbType.String, field);
            else
                param[7] = new DGCParameter(string.Format("{0}field", prefixParam), DbType.String, DBNull.Value);

            lst = dal_2C.GetList("GetAllProductCategory", param, out total);
            return lst;
        }

        public IList<Medical_ProductCategory> GetAllChild(int categoryId, bool includeMe)
        {
            IList<Medical_ProductCategory> lst = new List<Medical_ProductCategory>();
            DGCParameter[] param = new DGCParameter[2];

            if (categoryId != int.MinValue)
                param[0] = new DGCParameter(string.Format("{0}IDCategory", prefixParam), DbType.Int32, categoryId);
            else
                param[0] = new DGCParameter(string.Format("{0}IDCategory", prefixParam), DbType.Int32, DBNull.Value);
            if (includeMe)
                param[1] = new DGCParameter(string.Format("{0}unclude_me", prefixParam), DbType.Int16, 1);
            else
                param[1] = new DGCParameter(string.Format("{0}unclude_me", prefixParam), DbType.Int16, 0);

            int total;
            lst = dal_2C.GetList("GetAllChildProductCategory", param, out total);
            return lst;
        }
    }
}
