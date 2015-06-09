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
    public class Medical_ProductCategory
    {
        #region fields
        private int id;
        private int parentId;
        private string published;
        private int ordering;
        private DateTime postDate;
        private DateTime updateDate;
        private string pathTree;
        private string baseImage;
        private string smallImage;
        private string thumbnailImage;
        private string page;
        #endregion

        #region properties
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        public int ParentId
        {
            get { return this.parentId; }
            set { this.parentId = value; }
        }
        public string Published
        {
            get { return this.published; }
            set { this.published = value; }
        }
        public int Ordering
        {
            get { return this.ordering; }
            set { this.ordering = value; }
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
        public string PathTree
        {
            get { return this.pathTree; }
            set { this.pathTree = value; }
        }
        public string BaseImage
        {
            get { return this.baseImage; }
            set { this.baseImage = value; }
        }
        public string SmallImage
        {
            get { return this.smallImage; }
            set { this.smallImage = value; }
        }
        public string ThumbnailImage
        {
            get { return this.thumbnailImage; }
            set { this.thumbnailImage = value; }
        }
        public string Page
        {
            get { return this.page; }
            set { this.page = value; }
        }
        #endregion

        #region constructor

        public Medical_ProductCategory(int id,
                    int parentId,
                    string published,
                    int ordering,
                    DateTime postDate,
                    DateTime updateDate,
                    string pathTree,
                    string baseImage,
                    string smallImage ,
                    string thumbnailImage,
                    string page)
        {
            this.id = id;
            this.parentId = parentId;
            this.published = published;
            this.ordering = ordering;
            this.postDate = postDate;
            this.updateDate = updateDate;
            this.pathTree = pathTree;
            this.baseImage = baseImage;
            this.smallImage = smallImage;
            this.thumbnailImage = thumbnailImage;
            this.page = page;
        }
        #endregion

        #region extend

        private Medical_ProductCategoryDesc newsCategoryDesc;
        public Medical_ProductCategoryDesc NewsCategoryDesc
        {
            get { return newsCategoryDesc; }
            set { newsCategoryDesc = value; }
        }

        public Medical_ProductCategory()
        {
            this.id = int.MinValue;
            this.parentId = int.MinValue;
            this.published = string.Empty;
            this.ordering = int.MinValue;
            this.postDate = DateTime.MinValue;
            this.updateDate = DateTime.MinValue;
            this.newsCategoryDesc = new Medical_ProductCategoryDesc();
            //this.pathTreeDesc = string.Empty;
        }

        #endregion
    }
}