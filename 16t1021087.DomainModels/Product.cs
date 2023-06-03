using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16t1021087.DomainModels
{
    /// <summary>
    ///  Thông tin của mặt hàng
    /// </summary>
    public class Product
    {
        /// <summary>
        /// ID mặt hàng
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// Tên mặt hàng
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// ID nhà cung cấp
        /// </summary>
        public int SupplierID { get; set; }

        /// <summary>
        /// ID Loại hàng
        /// </summary>
        public int CategoryID { get; set; }

        /// <summary>
        /// Thư mục
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Giá tiền
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Ảnh
        /// </summary>
        public string Photo { get; set; }

    }
}
