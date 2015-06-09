﻿/**
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
    [Serializable]
    public class Medical_Banner
    {
        #region fields
        private int id;
        private int position;
        private int outPage;
        private string linkUrl;
        private string image;
        private string name;
        private string detail;
        private int height;
        private int width;
        private int clickCount;
        private int ordering;
        private string published;
        private DateTime postDate;
        private DateTime updateDate;
        private string arrPageName;
        #endregion

        #region properties
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        public int Position
        {
            get { return this.position; }
            set { this.position = value; }
        }
        public int OutPage
        {
            get { return this.outPage; }
            set { this.outPage = value; }
        }
        public string LinkUrl
        {
            get { return this.linkUrl; }
            set { this.linkUrl = value; }
        }
        public string Image
        {
            get { return this.image; }
            set { this.image = value; }
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
        public int Height
        {
            get { return this.height; }
            set { this.height = value; }
        }
        public int Width
        {
            get { return this.width; }
            set { this.width = value; }
        }
        public int ClickCount
        {
            get { return this.clickCount; }
            set { this.clickCount = value; }
        }
        public int Ordering
        {
            get { return this.ordering; }
            set { this.ordering = value; }
        }
        public string Published
        {
            get { return this.published; }
            set { this.published = value; }
        }
        public DateTime PostDate
        {
            get { return this.postDate; }
            set { this.postDate = value; }
        }
        public DateTime UpdateDate
        {
            get { return this.updateDate; }
            set { this.updateDate = value; }
        }
        public string ArrPageName
        {
            get { return this.arrPageName; }
            set { this.arrPageName = value; }
        }
        #endregion

        #region constructor
        public Medical_Banner()
        {
            this.id = int.MinValue;
            this.position = int.MinValue;
            this.outPage = int.MinValue;
            this.linkUrl = string.Empty;
            this.image = string.Empty;
            this.name = string.Empty;
            this.height = int.MinValue;
            this.width = int.MinValue;
            this.clickCount = int.MinValue;
            this.ordering = int.MinValue;
            this.published = string.Empty;
            this.postDate = DateTime.MinValue;
            this.updateDate = DateTime.MinValue;
            this.arrPageName = string.Empty;
        }
        public Medical_Banner(int id,
                    int position,
                    int outPage,
                    string linkUrl,
                    string image,
                    string name,
                    int height,
                    int width,
                    int clickCount,
                    int ordering,
                    string published,
                    DateTime postDate,
                    DateTime updateDate,
                    string arrPageName)
        {
            this.id = id;
            this.position = position;
            this.outPage = outPage;
            this.linkUrl = linkUrl;
            this.image = image;
            this.name = name;
            this.height = height;
            this.width = width;
            this.clickCount = clickCount;
            this.ordering = ordering;
            this.published = published;
            this.postDate = postDate;
            this.updateDate = updateDate;
            this.arrPageName = arrPageName;
        }
        #endregion

    }
}