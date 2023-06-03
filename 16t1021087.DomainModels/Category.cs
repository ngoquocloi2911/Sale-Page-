using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _16t1021087.DomainModels
{
    /// <summary>
    ///  Thông tin loại hàng
    /// </summary>
    public class Category
    {
        /// <summary>
        /// ID
        /// </summary>
        public int CategoryID { get; set; }

        /// <summary>
        /// Tên loại hàng
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Thư mục mẹ
        /// </summary>
        public int ParentCategoryId { get; set; }
    }
}