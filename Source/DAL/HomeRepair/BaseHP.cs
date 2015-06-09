using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Cb.DBUtility;
using Cb.SQLServerDAL;
using Cb.DALFactory;
using Cb.IDAL;
using Cb.Model.HomeRepair;

namespace Cb.DAL.HomeRepair
{

    public class BaseHP
    {
        public static IGeneric<HomeRepair_Menu> dal;
        static protected string prefixParam;
        public BaseHP()
        {
            Type t = typeof(Generic<HomeRepair_Menu>);
            dal = DataAccessGeneric<HomeRepair_Menu>.CreateSession(t.FullName);


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


    }
}
