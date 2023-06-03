using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _16t1021087.DomainModels;

namespace _16t1021087.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép xử lý dữ liệu liên quan đến các tk người dùng
    /// khi cài đặt các tài khoản khác sẽ kế thừa cái này
    /// lớp interface phải là public
    /// </summary>
    public interface IUserAccountDAL
    {
        /// <summary>
        /// kiểm tra tên đăng nhập và mật khẩu của người dùng có hợp lệ hay không
        /// Nếu hợp lệ thì trả về thông tin của tài khoản, ngược lại(không hợp lệ) trả về null
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        UserAccount Authorize (string userName, string password);

        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        bool ChangePassword(string userName, string oldPassword, string newPassword);
        
    }
}
