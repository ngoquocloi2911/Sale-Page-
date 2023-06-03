using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _16t1021087.DomainModels
{
    /// <summary>
    /// Thông tin khách hàng
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Tên ID
        /// </summary>
        public int CustomerID { get; set; }

        /// <summary>
        /// Tên khách hàng
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// Tên liên hệ
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// Địa chỉ khách hàng
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        /// 

        public string Email { get; set; }

        /// <summary>
        /// Thành phố
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Quốc gia
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Mã bưu điện 
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Mật khẩu
        /// </summary>
        public string Password { get; set; }
    }
}