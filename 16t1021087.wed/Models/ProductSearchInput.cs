using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _16t1021087.wed.Models
{
    public class ProductSearchInput : PaginationSearchInput
    {
        public int CategoryID { get; set; } 

        public int SupplierID { get; set; }
    }
}