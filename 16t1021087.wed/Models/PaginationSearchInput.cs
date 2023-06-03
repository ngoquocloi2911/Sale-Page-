using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _16t1021087.wed.Models
{
    /// <summary>
    /// Biểu diễn dữ liệu đầu vào để tìm kiếm phân trang
    /// mô tả thêm category, supplier là gì
    /// </summary>
    public class PaginationSearchInput
    {

        /// <summary>
        /// Trang cần hiển thị
        /// </summary>
        public int Page { get; set; }
        
        /// <summary>
        /// Số dòng cần hiển thị trên một trang
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Giá trị cần tìm
        /// </summary>
        public string SearchValue { get; set; }
    }
}