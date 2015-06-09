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

namespace Cb.Model.Services
{
    public class Medical_ServicesDesc
    {
        #region fields
        private int id;
        private int mainId;
        private int langId;
        private string title;
        private string brief;
        private string detail;
        private string requiresys;

        #endregion

        #region properties
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        public int MainId
        {
            get { return this.mainId; }
            set { this.mainId = value; }
        }
        public int LangId
        {
            get { return this.langId; }
            set { this.langId = value; }
        }
        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }
        public string Brief
        {
            get { return this.brief; }
            set { this.brief = value; }
        }
        public string Detail
        {
            get { return this.detail; }
            set { this.detail = value; }
        }
        public string Requiresys
        {
            get { return this.requiresys; }
            set { this.requiresys = value; }
        }
        #endregion

        #region constructor
        public Medical_ServicesDesc()
        {
            this.id = int.MinValue;
            this.mainId = int.MinValue;
            this.langId = int.MinValue;
            this.title = string.Empty;
            this.brief = string.Empty;
            this.detail = string.Empty;
            this.requiresys = string.Empty;
        }
        public Medical_ServicesDesc(int id,
                    int mainId,
                    int langId,
                    string title,
                    string brief,
                    string detail,
                    string requiresys
                   )
        {
            this.id = id;
            this.mainId = mainId;
            this.langId = langId;
            this.title = title;
            this.brief = brief;
            this.detail = detail;
            this.requiresys = requiresys;
        }
        #endregion

    }
}