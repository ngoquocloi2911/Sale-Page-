using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _16t1021087.DataLayers;
using _16t1021087.DomainModels;

namespace _16t1021087.BussinessLayers
{
    /// <summary>
    /// Các chức năng liên quan đến tài khoản
    /// </summary>
    public static class UserAccountService
    {
        private static IUserAccountDAL employeeAccountDB;
        private static IUserAccountDAL customerAccountDB;

        /// <summary>
        /// 
        /// </summary>
        static UserAccountService()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            employeeAccountDB = new DataLayers.SQLServer.EmployeeAccountDAL(connectionString);

            customerAccountDB = new DataLayers.SQLServer.CustomerAccountDAL(connectionString);
        }

        /// <summary>
        /// kiểm tra thông tin đăng nhập của nhân viên or khách hàng
        /// </summary>
        /// <param name="accountType"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static UserAccount Authorize(AccountTypes accountType, string userName, string password)
        {
            if (accountType == AccountTypes.Employee)
            {
                return employeeAccountDB.Authorize(userName, password);
            }
            else
            {
                return customerAccountDB.Authorize(userName, password);
            }    
        }

        /// <summary>
        /// Thay đổi mật khẩu của nhân viên hoặc khách hàng
        /// </summary>
        /// <param name="accountType"></param>
        /// <param name="userName"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public static bool ChangePassword (AccountTypes accountType, string userName, string oldPassword,string newPassword)
        {
            if (accountType == AccountTypes.Employee)
            {
                return employeeAccountDB.ChangePassword(userName, oldPassword, newPassword);
            }
            else
            {
                return customerAccountDB.ChangePassword(userName, oldPassword, newPassword);
            }
        }

    }
}
