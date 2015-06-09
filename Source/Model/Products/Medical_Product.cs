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
    public class Medical_Product
    {
        #region fields
        private int id;
        private int categoryId;
        private string image;
        private string latitude;
        private string published;
        private string hot;
        private string feature;
        private DateTime postDate;
        private DateTime updateDate;
        private int ordering;
        private string longitude;
        private string post;
        private Medical_ProductDesc productDesc;
        private Medical_ProductCategoryDesc productCategoryDesc;
        private string categoryDesc;
        private string rentDesc;
        private string nameUrlDesc;

        private string price;
        private string area;
        private string district;
        private int bedroom;
        private int bathroom;
        private string code;
        private string status;
        private string province;
        private string website;
        private int userId;
        private int cost;
        private string page;

        #endregion

        #region properties
        public string CategoryDesc
        {
            get { return categoryDesc; }
            set { categoryDesc = value; }
        }
        public string RentDesc
        {
            get { return rentDesc; }
            set { rentDesc = value; }
        }
        public string NameUrlDesc
        {
            get { return nameUrlDesc; }
            set { nameUrlDesc = value; }
        }
        public Medical_ProductCategoryDesc ProductCategoryDesc
        {
            get { return productCategoryDesc; }
            set { productCategoryDesc = value; }
        }
        public Medical_ProductDesc ProductDesc
        {
            get { return productDesc; }
            set { productDesc = value; }
        }
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        public int CategoryId
        {
            get { return this.categoryId; }
            set { this.categoryId = value; }
        }
        public string Image
        {
            get { return this.image; }
            set { this.image = value; }
        }
        public string Latitude
        {
            get { return this.latitude; }
            set { this.latitude = value; }
        }
        public string Published
        {
            get { return this.published; }
            set { this.published = value; }
        }
        public string Hot
        {
            get { return this.hot; }
            set { this.hot = value; }
        }
        public string Feature
        {
            get { return this.feature; }
            set { this.feature = value; }
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
        public int Ordering
        {
            get { return this.ordering; }
            set { this.ordering = value; }
        }
        public string Longitude
        {
            get { return this.longitude; }
            set { this.longitude = value; }
        }
        public string Post
        {
            get { return this.post; }
            set { this.post = value; }
        }

        public string Price
        {
            get { return this.price; }
            set { this.price = value; }
        }
        public string Area
        {
            get { return this.area; }
            set { this.area = value; }
        }
        public string District
        {
            get { return this.district; }
            set { this.district = value; }
        }
        public int Bedroom
        {
            get { return this.bedroom; }
            set { this.bedroom = value; }
        }
        public int Bathroom
        {
            get { return this.bathroom; }
            set { this.bathroom = value; }
        }
        public string Status
        {
            get { return this.status; }
            set { this.status = value; }
        }
        public string Code
        {
            get { return this.code; }
            set { this.code = value; }
        }
        public string Province
        {
            get { return this.province; }
            set { this.province = value; }
        }
        public string Website
        {
            get { return this.website; }
            set { this.website = value; }
        }

        public int UserId
        {
            get { return this.userId; }
            set { this.userId = value; }
        }
        public int Cost
        {
            get { return this.cost; }
            set { this.cost = value; }
        }
        public string Page
        {
            get { return this.page; }
            set { this.page = value; }
        }

        #endregion

        #region constructor
        public Medical_Product()
        {
            this.id = int.MinValue;
            this.categoryId = int.MinValue;
            this.image = string.Empty;
            this.latitude = string.Empty;
            this.published = string.Empty;
            this.hot = string.Empty;
            this.feature = string.Empty;
            this.postDate = DateTime.MinValue;
            this.updateDate = DateTime.MinValue;
            this.ordering = int.MinValue;
            this.longitude = string.Empty;
            productDesc = new Medical_ProductDesc();
            productCategoryDesc = new Medical_ProductCategoryDesc();

            this.price = string.Empty;
            this.area = string.Empty;
            this.district = string.Empty;
            this.bedroom = int.MinValue;
            this.bathroom = int.MinValue;
            this.status = string.Empty;
            this.code = string.Empty;
            this.province = string.Empty;
            this.website = string.Empty;
            this.userId = int.MinValue;
            this.cost = int.MinValue;
            this.page = string.Empty;
        }
        public Medical_Product(int id,
                    int categoryId,
                    string image,
                    string latitude,
                    string published,
                    string hot,
                    string feature,
                    DateTime postDate,
                    DateTime updateDate,
                    int ordering,
                    string longitude, string post,
                    string price,
                    string area,
                    string district,
                    int bedroom,
                    int bathroom,
                    string status,
                    string code,
                    string province,
                    string website,
                    int userId,
                     int cost,
                    string page)
        {
            this.id = id;
            this.categoryId = categoryId;
            this.image = image;
            this.latitude = latitude;
            this.published = published;
            this.hot = hot;
            this.feature = feature;
            this.postDate = postDate;
            this.updateDate = updateDate;
            this.ordering = ordering;
            this.longitude = longitude;
            this.post = post;
            this.price = price;
            this.area = area;
            this.district = district;
            this.bedroom = bedroom;
            this.bathroom = bathroom;
            this.status = status;
            this.code = code;
            this.province = province;
            this.website = website;
            this.userId = userId;
            this.cost = cost;
            this.page = page;
        }
        #endregion
    }
}