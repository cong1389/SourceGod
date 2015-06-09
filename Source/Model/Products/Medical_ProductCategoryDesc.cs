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

namespace Cb.Model.Products
{
    public class Medical_ProductCategoryDesc
    {
        #region fields
        private int id;
        private int mainId;
        private int langId;
        private string name;
        private string nameUrl;
        private string brief;
        private string detail;
       
        private string metaTitle;
        private string metaKeyword;
        private string metaDecription;
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
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public string NameUrl
        {
            get { return this.nameUrl; }
            set { this.nameUrl = value; }
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
        
        public string MetaTitle
        {
            get { return this.metaTitle; }
            set { this.metaTitle = value; }
        }
        public string MetaKeyword
        {
            get { return this.metaKeyword; }
            set { this.metaKeyword = value; }
        }
        public string MetaDecription
        {
            get { return this.metaDecription; }
            set { this.metaDecription = value; }
        }
        #endregion

        #region constructor
        public Medical_ProductCategoryDesc()
        {
            this.id = int.MinValue;
            this.mainId = int.MinValue;
            this.langId = int.MinValue;
            this.name = string.Empty;
            this.nameUrl = string.Empty;
            this.brief = string.Empty;
            this.detail = string.Empty;
            this.metaTitle = string.Empty;
            this.metaKeyword = string.Empty;
            this.metaDecription = string.Empty;
        }
        public Medical_ProductCategoryDesc(int id,
                    int mainId,
                    int langId,
                    string name,
                    string nameUrl,
                    string brief,
                    string detail,
                    string metaTitle,
                    string metaKeyword,
                    string metaDecription)
        {
            this.id = id;
            this.mainId = mainId;
            this.langId = langId;
            this.name = name;
            this.nameUrl = nameUrl;
            this.brief = brief;
            this.detail = detail;           
            this.metaTitle = metaTitle;
            this.metaKeyword = metaKeyword;
            this.metaDecription = metaDecription;
        }
        #endregion

    }
}