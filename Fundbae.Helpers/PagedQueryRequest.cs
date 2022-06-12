﻿using Fundbae.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundbae.Helpers
{
    public class PagedQueryRequest
    {

        /// <summary>
        /// The page number for the paginated results.  Default is 1.
        /// Setting a number beyond the last page will just return the last page of data.
        /// </summary>
        public int PageNumber { get; set; } = Utilities.DefaultPageNumber;

        private int _pageSize = Utilities.DefaultPageSize;
        /// <summary>
        /// The number of items returned for the page. A maximum size of 100 is set.  Default is 20.
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = Math.Min(value, Utilities.MaxPageSize); }
        }

    }
}