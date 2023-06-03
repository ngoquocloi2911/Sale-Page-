using _16t1021087.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _16t1021087.wed.Models
{
    /// <summary>
    /// Thuộc tính mặt hàng
    /// </summary>
    public class OderDetailModel
    {
        public Order Data { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }
        /// <summary>
        /// Hàm tính tổng giá
        /// </summary>
        public decimal TotalPriceOrder
        {
            get
            {
                decimal SUM = 0;
                foreach (var item in OrderDetail)
                {
                    SUM += item.TotalPrice;
                }
                return SUM;
            }
        }
    }
}