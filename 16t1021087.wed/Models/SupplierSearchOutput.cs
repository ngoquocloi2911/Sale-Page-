using _16t1021087.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _16t1021087.wed.Models;

namespace _16t1021087.wed.Models
{
    /// <summary>
    /// Kết quả tìm kiếm nhà cung cấp dưới dạng phân trang
    /// </summary>
    public class SupplierSearchOutput : PaginationSearchOutput
    {
        /// <summary>
        /// Danh sách nhà cung cấp
        /// </summary>
        public List<Supplier> Data { get; set; }
    }
}