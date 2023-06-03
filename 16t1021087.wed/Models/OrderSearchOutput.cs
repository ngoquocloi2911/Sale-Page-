using _16t1021087.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _16t1021087.wed.Models
{
    public class OrderSearchOutput:PaginationSearchOutput
    {
        public List<Order> Data { get; set; }
    }
}