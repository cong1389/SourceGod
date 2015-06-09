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
    public class Medical_Services
    {
        #region fields
        private int id;
        private int categoryId;
        private string image;
        private string resource;
        private string published;
        private DateTime postDate;
        private DateTime updateDate;
        private int ordering;
        private string isHot;
        private string phone;
        private Medical_ServicesDesc serviesDesc;
        private Medical_ServicesCategoryDesc servicesCategoryDesc;
        private string categoryDesc;

        #endregion

        #region properties
        public string CategoryDesc
        {
            get { return categoryDesc; }
            set { categoryDesc = value; }
        }
        public Medical_ServicesCategoryDesc ServicesCategoryDesc
        {
            get { return servicesCategoryDesc; }
            set { servicesCategoryDesc = value; }
        }
        public Medical_ServicesDesc ServicesDesc
        {
            get { return serviesDesc; }
            set { serviesDesc = value; }
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
        public string Resource
        {
            get { return this.resource; }
            set { this.resource = value; }
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
        public int Ordering
        {
            get { return this.ordering; }
            set { this.ordering = value; }
        }
        public string IsHot
        {
            get { return this.isHot; }
            set { this.isHot = value; }
        }
        public string Phone
        {
            get { return this.phone; }
            set { this.phone = value; }
        }
        #endregion

        #region constructor
        public Medical_Services()
        {
            this.id = int.MinValue;
            this.categoryId = int.MinValue;
            this.image = string.Empty;
            this.resource = string.Empty;
            this.published = string.Empty;
            this.postDate = DateTime.MinValue;
            this.updateDate = DateTime.MinValue;
            this.ordering = int.MinValue;
            this.isHot = string.Empty;
            this.phone = string.Empty;
            serviesDesc = new Medical_ServicesDesc();
            servicesCategoryDesc = new Medical_ServicesCategoryDesc();
        }
        public Medical_Services(int id,
                    int categoryId,
                    string image,
                    string resource,
                    string published,
                    DateTime postDate,
                    DateTime updateDate,
                    int ordering,
                    string isHot, string phone)
        {
            this.id = id;
            this.categoryId = categoryId;
            this.image = image;
            this.resource = resource;
            this.published = published;
            this.postDate = postDate;
            this.updateDate = updateDate;
            this.ordering = ordering;
            this.isHot = isHot;
            this.phone = phone;
        }
        #endregion

    }
}