using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _16t1021087.wed.Models
{
    public class OrderSeachInput: PaginationSearchInput
    {
        public int Status { get; set; }
    }
}