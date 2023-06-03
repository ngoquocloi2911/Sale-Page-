using _16t1021087.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _16t1021087.wed.Models
{
    /// <summary>
    /// Kết quả tìm kiếm và lấy dữ liệu dưới dạng phân trang
    /// </summary>
    public class ProductSearchOutput : PaginationSearchOutput
    {
        /// <summary>
        /// danh sách mặt hàng
        /// </summary>
       public List<Product> Data { get; set; }

        public int CategoryID { get; set; }
        public int SupplierID { get; set; }
    }
}