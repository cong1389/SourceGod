﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cb.IDAL;
using Cb.DALFactory;
using Cb.Utility;
using Cb.DBUtility;
using System.Data;
using System.Configuration;
namespace Cb.BLL
{
    [Serializable]
    public class LocationBLL
    {
        private static IGeneric2C<Medical_Location, Medical_LocationDesc> dal_2C;
        private string prefixParam;

        public LocationBLL()
        {
            Type t = typeof(Cb.SQLServerDAL.Generic2C<Medical_Location, Medical_LocationDesc>);
            dal_2C = DataAccessGeneric2C<Medical_Location, Medical_LocationDesc>.CreateSession(t.FullName);

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

        public IList<Medical_Location> GetList(int langId, string name, int pageIndex, int pageSize, out int total)
        {
            IList<Medical_Location> lst = new List<Medical_Location>();
            DGCParameter[] param = new DGCParameter[4];
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

            lst = dal_2C.GetList("sp_GetAllLocation", param, out total);
            return lst;
        }

    }
}
