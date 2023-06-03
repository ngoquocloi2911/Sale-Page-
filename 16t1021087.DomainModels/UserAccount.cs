using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16t1021087.DomainModels
{
    /// <summary>
    /// Tài khoản người dùng
    /// </summary>
    public class UserAccount
    {
        /// <summary>
        ///  nên dùng string hết cho tổng quát
        ///  Tên id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// tên đăng nhập
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Tên đầy đủ
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// pass
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Tên nhóm
        /// </summary>
        public string RoleName { get; set; }

        public string Photo { get; set; }

    }
}
