using _16t1021087.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _16t1021087.wed.Models;

namespace _16t1021087.wed.Models
{
    /// <summary>
    /// Kết quả tìm kiếm khách hàng phân trang
    /// </summary>
    public class CustomerSearchOutput : PaginationSearchOutput
    {
        /// <summary>
        /// Danh sách khách hàng
        /// </summary>
        public List<Customer> Data { get; set; }
    }
}