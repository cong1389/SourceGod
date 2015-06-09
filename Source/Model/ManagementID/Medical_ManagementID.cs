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
    public class Medical_ManagementID
    {
        #region fields
        private int id;
        private int ordering;
        private DateTime postdate;
        private string published;
        private DateTime updateDate;
        private string name;
        private string value;
        #endregion

        #region properties
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        public int Ordering
        {
            get { return this.ordering; }
            set { this.ordering = value; }
        }
        public DateTime PostDate
        {
            get { return this.postdate; }
            set { this.postdate = value; }
        }
        public string Published
        {
            get { return this.published; }
            set { this.published = value; }
        }
        public DateTime UpdateDate
        {
            get { return this.updateDate; }
            set { this.updateDate = value; }
        }
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        #endregion

        #region constructor
        public Medical_ManagementID()
        {
            this.id = int.MinValue;
            this.ordering = int.MinValue;
            this.postdate = DateTime.MinValue;
            this.published = string.Empty;
            this.updateDate = DateTime.MinValue;
            this.name = string.Empty;
            this.value = string.Empty;
        }
        public Medical_ManagementID(int id,
                    int ordering,
                    DateTime postdate,
                    string published,
                    DateTime updateDate,
                    string name,
                    string value)
        {
            this.id = id;
            this.ordering = ordering;
            this.postdate = postdate;
            this.published = published;
            this.updateDate = updateDate;
            this.name = name;
            this.value = value;
        }
        #endregion

    }
}