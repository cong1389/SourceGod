using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cb.DBUtility;
using System.Data;

using Cb.IDAL;
using Cb.SQLServerDAL;

namespace Cb.DAL.HomeRepair
{
    public class Menu : BaseHP
    {
        #region BIEN TOAN CUC
        protected static int ret = int.MinValue;

       
        #endregion

        #region SELECT METHOD

        /// <summary>
        /// GetHomeRepair_Menu:hien thi theo parentid su dung block_HomeRepair
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        public static IList<HomeRepair_Menu> GetListMenu(int ParentID)
        {
            int total;
            IList<HomeRepair_Menu> lst = new List<HomeRepair_Menu>();
            BaseHP a = new BaseHP();
            DGCParameter[] param = new DGCParameter[1];
            param[0] = new DGCParameter(string.Format("{0}ParentID", prefixParam), DbType.Int16, ParentID);
            lst = dal.GetList("GetMenu", param, out total);
            return lst;
        }

        /// <summary>
        /// HIEN THI TAT CA DANH SACH MENU SU DUNG CHO GRD
        /// GetMenu
        /// </summary>
        /// <returns></returns>
        public static DataTable GetMenu(int ParentID)
        {
            DGCParameter[] param = new DGCParameter[1];
            param[0] = new DGCParameter(string.Format("{0}ParentID", prefixParam), DbType.Int16, ParentID);
            DataTable dt = DBHelper.ExcuteFromStore("GetMenu", param);
            return dt;
        }
        #endregion        
        
    }
}
