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
using Cb.Model.Services;

namespace Cb.BLL.Services
{
  
    public class ServicesCategoryBLL
    {
        private static IGeneric2C<Medical_ServicesCategory, Medical_ServicesCategoryDesc> dal_2C;
        private string prefixParam;

        public ServicesCategoryBLL()
        {
            Type t = typeof(Cb.SQLServerDAL.Generic2C<Medical_ServicesCategory, Medical_ServicesCategoryDesc>);
            dal_2C = DataAccessGeneric2C<Medical_ServicesCategory, Medical_ServicesCategoryDesc>.CreateSession(t.FullName);

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

        public IList<Medical_ServicesCategory> GetList(int langId, string name, int pageIndex, int pageSize, out int total)
        {
            return GetList(langId, name, int.MinValue, false, pageIndex, pageSize, out  total);
        }

        public IList<Medical_ServicesCategory> GetList(int langId, string name, int parentId, bool isTree, int pageIndex, int pageSize, out int total)
        {
            IList<Medical_ServicesCategory> lst = new List<Medical_ServicesCategory>();
            DGCParameter[] param = new DGCParameter[6];
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

            lst = dal_2C.GetList("GetAllServicesCategory", param, out total);
            return lst;
        }

        public IList<Medical_ServicesCategory> GetAllChild(int categoryId, bool includeMe)
        {
            IList<Medical_ServicesCategory> lst = new List<Medical_ServicesCategory>();
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
            lst = dal_2C.GetList("fc_GetAllChildServicesCategory", param, out total);
            return lst;
        }
    }
}
