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
using Cb.Model;

namespace Cb.BLL
{
    [Serializable]
    public class ManagementIDBLL
    {
        private static IGeneric2C<Medical_ManagementID, Medical_ManagementIDDesc> dal_2C;
        private string prefixParam;

        public ManagementIDBLL()
        {
            Type t = typeof(Cb.SQLServerDAL.Generic2C<Medical_ManagementID, Medical_ManagementIDDesc>);
            dal_2C = DataAccessGeneric2C<Medical_ManagementID, Medical_ManagementIDDesc>.CreateSession(t.FullName);

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

        public IList<Medical_ManagementID> GetList(string name, int pageIndex, int pageSize, out int total)
        {
            IList<Medical_ManagementID> lst = new List<Medical_ManagementID>();
            DGCParameter[] param = new DGCParameter[3];
            if (!string.IsNullOrEmpty(name))
                param[0] = new DGCParameter(string.Format("{0}name", prefixParam), DbType.String, name);
            else
                param[0] = new DGCParameter(string.Format("{0}name", prefixParam), DbType.String, DBNull.Value);

            if (pageIndex != int.MinValue)
                param[1] = new DGCParameter(string.Format("{0}pageIndex", prefixParam), DbType.Int16, pageIndex);
            else
                param[1] = new DGCParameter(string.Format("{0}pageIndex", prefixParam), DbType.Int16, DBNull.Value);

            if (pageSize != int.MinValue)
                param[2] = new DGCParameter(string.Format("{0}pageSize", prefixParam), DbType.Int16, pageSize);
            else
                param[2] = new DGCParameter(string.Format("{0}pageSize", prefixParam), DbType.Int16, DBNull.Value);

            lst = dal_2C.GetList("GetAllManagementID", param, out total);
            return lst;
        }

    }
}
