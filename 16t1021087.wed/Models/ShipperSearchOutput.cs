using _16t1021087.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _16t1021087.wed.Models
{
    /// <summary>
    /// kết quả tìm kiếm dưới dạng phân trang
    /// </summary>
    public class ShipperSearchOutput : PaginationSearchOutput
    {
        public List<Shipper> Data { get; set; }
    }
}