/**
                             * @version $Id:
                             * @package Digicom.NET
                             * @author Digicom Dev <dev@dgc.vn>
                             * @copyright Copyright (C) 2011 by Digicom. All rights reserved.
                             * @link http://www.dgc.vn
                            */

using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;

namespace Cb.Model
{
    public class Medical_ManagementIDDesc
    {
        #region fields
        private int id;
        private int mainid;
        private int langid;
        private string name;
        private string detail;
        #endregion

        #region properties
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        public int Mainid
        {
            get { return this.mainid; }
            set { this.mainid = value; }
        }
        public int Langid
        {
            get { return this.langid; }
            set { this.langid = value; }
        }
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public string Detail
        {
            get { return this.detail; }
            set { this.detail = value; }
        }
        #endregion

        #region constructor
        public Medical_ManagementIDDesc()
        {
            this.id = int.MinValue;
            this.mainid = int.MinValue;
            this.langid = int.MinValue;
            this.name = string.Empty;
            this.detail = string.Empty;
        }
        public Medical_ManagementIDDesc(int id,
                    int mainid,
                    int langid,
                    string name,
                    string detail)
        {
            this.id = id;
            this.mainid = mainid;
            this.langid = langid;
            this.name = name;
            this.detail = detail;
        }
        #endregion

    }
}