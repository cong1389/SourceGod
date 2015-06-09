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
    public class Medical_UploadImage
    {
        #region fields
        private int id;
        private string name;
        private string published;
        private string imagePath;
        private int ordering;
        private DateTime postDate;
        private DateTime updatedate;
        private int productId;
        #endregion

        #region properties
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public string Published
        {
            get { return this.published; }
            set { this.published = value; }
        }
        public string ImagePath
        {
            get { return this.imagePath; }
            set { this.imagePath = value; }
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
        public DateTime Updatedate
        {
            get { return this.updatedate; }
            set { this.updatedate = value; }
        }
        public int ProductId
        {
            get { return this.productId; }
            set { this.productId = value; }
        }
        #endregion

        #region constructor
        public Medical_UploadImage()
        {
            this.id = int.MinValue;
            this.name = string.Empty;
            this.published = string.Empty;
            this.imagePath = string.Empty;
            this.ordering = int.MinValue;
            this.postDate = DateTime.MinValue;
            this.updatedate = DateTime.MinValue;
            this.productId = int.MinValue;
        }
        public Medical_UploadImage(int id,
                    string name,
                    string published,
                    string imagePath,
                    int ordering,
                    DateTime postDate,
                    DateTime updatedate,
                    int productId)
        {
            this.id = id;
            this.name = name;
            this.published = published;
            this.imagePath = imagePath;
            this.ordering = ordering;
            this.postDate = postDate;
            this.updatedate = updatedate;
            this.productId = productId;
        }
        #endregion

    }
}