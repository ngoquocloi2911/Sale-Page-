using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _16t1021087.DomainModels
{
    /// <summary>
    /// Thông tin nhà cung cấp
    /// </summary>
    public class Supplier
    {
        /// <summary>
        /// Tên ID
        /// </summary>
        public int SupplierID { get; set; }
        /// <summary>
        /// Tên Nhà cung cấp
        /// </summary>
        public string SupplierName { get; set; }
        /// <summary>
        /// Tên liên hệ
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Quốc gia
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Thành phố
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Mã Bưu chính
        /// </summary>
        public string PostalCode { get; set; }
    }
}