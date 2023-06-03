using _16t1021087.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _16t1021087.wed.Models
{
    public class ProductEditModel : Product
    {
        public Product Product { get; set; }
        public List<ProductAttribute> Attributes { get; set;}

        public List<ProductPhoto> photos { get; set; }
    }
}