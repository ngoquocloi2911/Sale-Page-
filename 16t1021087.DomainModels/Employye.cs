using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _16t1021087.DomainModels
{
    /// <summary>
    /// Nhân viên
    /// </summary>
    public class Employye
    {
        /// <summary>
        /// ID
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        /// Họ
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Tên
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Ngày sinh 
        /// lấy tạm kiểu int, sửa sau 
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Ảnh , lấy tạm kiểu int
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// pass
        /// </summary>
        public string Password { get; set; }



    }
}