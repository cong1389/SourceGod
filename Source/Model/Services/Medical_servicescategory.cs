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
    public class Medical_ServicesCategory
    {
        #region fields
        private int id;
        private int parentId;
        private string published;
        private int ordering;
        private DateTime postDate;
        private DateTime updateDate;
        private string pathTreeDesc;
        private Medical_ServicesCategoryDesc newsCategoryDesc;


        #endregion

        #region properties
        public string PathTreeDesc
        {
            get { return pathTreeDesc; }
            set { pathTreeDesc = value; }
        }
        public Medical_ServicesCategoryDesc NewsCategoryDesc
        {
            get { return newsCategoryDesc; }
            set { newsCategoryDesc = value; }
        }
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
        #endregion

        #region constructor
        public Medical_ServicesCategory()
        {
            this.id = int.MinValue;
            this.parentId = int.MinValue;
            this.published = string.Empty;
            this.ordering = int.MinValue;
            this.postDate = DateTime.MinValue;
            this.updateDate = DateTime.MinValue;
            this.newsCategoryDesc = new Medical_ServicesCategoryDesc();
            //this.pathTreeDesc = string.Empty;
        }
        public Medical_ServicesCategory(int id,
                    int parentId,
                    string published,
                    int ordering,
                    DateTime postDate,
                    DateTime updateDate
                    )
        {
            this.id = id;
            this.parentId = parentId;
            this.published = published;
            this.ordering = ordering;
            this.postDate = postDate;
            this.updateDate = updateDate;
            //this.pathTreeDesc = pt;
        }
        #endregion

    }
}