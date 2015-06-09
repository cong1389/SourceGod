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
    public class Medical_ProductDesc
    {
        #region fields
        private int id;
        private int mainId;
        private int langId;
        private string title;
        private string brief;
        private string detail;
        private string require;
        private string titleurl;
        private string position;
        private string utility;
        private string design;
        private string pictures;
        private string payment;
        private string contact;
        private string metadescription;
        private string metaKeyword;
        private string metaTitle;
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
        public string Require
        {
            get { return this.require; }
            set { this.require = value; }
        }
        public string TitleUrl
        {
            get { return this.titleurl; }
            set { this.titleurl = value; }
        }
        public string Position
        {
            get { return this.position; }
            set { this.position = value; }
        }
        public string Utility
        {
            get { return this.utility; }
            set { this.utility = value; }
        }
        public string Design
        {
            get { return this.design; }
            set { this.design = value; }
        }
        public string Pictures
        {
            get { return this.pictures; }
            set { this.pictures = value; }
        }
        public string Payment
        {
            get { return this.payment; }
            set { this.payment = value; }
        }
        public string Contact
        {
            get { return this.contact; }
            set { this.contact = value; }
        }
        public string Metadescription
        {
            get { return this.metadescription; }
            set { this.metadescription = value; }
        }
        public string MetaKeyword
        {
            get { return this.metaKeyword; }
            set { this.metaKeyword = value; }
        }
        public string MetaTitle
        {
            get { return this.metaTitle; }
            set { this.metaTitle = value; }
        }
        #endregion

        #region constructor
        public Medical_ProductDesc()
        {
            this.id = int.MinValue;
            this.mainId = int.MinValue;
            this.langId = int.MinValue;
            this.title = string.Empty;
            this.brief = string.Empty;
            this.detail = string.Empty;
            this.require = string.Empty;
            this.titleurl = string.Empty;
            this.position = string.Empty;
            this.utility = string.Empty;
            this.design = string.Empty;
            this.pictures = string.Empty;
            this.payment = string.Empty;
            this.contact = string.Empty;
            this.metadescription = string.Empty;
            this.metaKeyword = string.Empty;
            this.metaTitle = string.Empty;
        }
        public Medical_ProductDesc(int id,
                    int mainId,
                    int langId,
                    string title,
                    string brief,
                    string detail,
                    string require,
                    string titleurl,
                    string position,
                    string utility,
                    string design,
                    string pictures,
                    string payment,
                    string contact,
                    string metadescription,
                    string metaKeyword,
                    string metaTitle)
        {
            this.id = id;
            this.mainId = mainId;
            this.langId = langId;
            this.title = title;
            this.brief = brief;
            this.detail = detail;
            this.require = require;
            this.titleurl = titleurl;
            this.position = position;
            this.utility = utility;
            this.design = design;
            this.pictures = pictures;
            this.payment = payment;
            this.contact = contact;
            this.metadescription = metadescription;
            this.metaKeyword = metaKeyword;
            this.metaTitle = metaTitle;
        }
        #endregion
    }
}