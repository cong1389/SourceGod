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
using Cb.Model.Services;

namespace Cb.BLL.Services
{
    [Serializable]
    public class ServicesBLL
    {
        private static IGeneric2C<Medical_Services, Medical_ServicesDesc> dal_2C;    
        private string prefixParam;

        public ServicesBLL()
        {         
            Type t = typeof(Cb.SQLServerDAL.Generic2C<Medical_Services, Medical_ServicesDesc>);
            dal_2C = DataAccessGeneric2C<Medical_Services, Medical_ServicesDesc>.CreateSession(t.FullName);


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

        public IList<Medical_Services> GetList(int langId, string name, string newsCateId, int pageIndex, int pageSize, out int total)
        {
            IList<Medical_Services> lst = new List<Medical_Services>();
            DGCParameter[] param = new DGCParameter[5];
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
                param[4] = new DGCParameter(string.Format("{0}cateId", prefixParam), DbType.AnsiString, newsCateId);
            else
                param[4] = new DGCParameter(string.Format("{0}cateId", prefixParam), DbType.AnsiString, DBNull.Value);

            lst = dal_2C.GetList("sp_GetAllServices", param, out total);
            return lst;
        }

        public IList<Medical_Services> GetListNewsBest(int langId, string name, string newsCateId, out int total)
        {
            IList<Medical_Services> lst = new List<Medical_Services>();
            DGCParameter[] param = new DGCParameter[3];
            if (langId != byte.MinValue)
                param[0] = new DGCParameter(string.Format("{0}langId", prefixParam), DbType.Int16, langId);
            else
                param[0] = new DGCParameter(string.Format("{0}langId", prefixParam), DbType.Int16, DBNull.Value);

            if (!string.IsNullOrEmpty(name))
                param[1] = new DGCParameter(string.Format("{0}name", prefixParam), DbType.String, name);
            else
                param[1] = new DGCParameter(string.Format("{0}name", prefixParam), DbType.String, DBNull.Value);
            if (!string.IsNullOrEmpty(newsCateId))
                param[2] = new DGCParameter(string.Format("{0}cateId", prefixParam), DbType.AnsiString, newsCateId);
            else
                param[2] = new DGCParameter(string.Format("{0}cateId", prefixParam), DbType.AnsiString, DBNull.Value);
            lst = dal_2C.GetList("sp_GetServicesBest", param, out total);
            return lst;
        }

        public IList<Medical_Services> GetListT(int id, int langId)
        {
            IList<Medical_Services> lst = new List<Medical_Services>();
            DGCParameter[] param = new DGCParameter[2];

            param[0] = new DGCParameter(string.Format("{0}id", prefixParam), DbType.Int16, id);
            param[1] = new DGCParameter(string.Format("{0}langId", prefixParam), DbType.Int16, langId);

            lst = dal_2C.GetListT("sp_GetAllServices1", param);
            return lst;
        } 
    }
}
