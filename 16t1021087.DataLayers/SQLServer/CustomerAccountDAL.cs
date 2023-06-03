using _16t1021087.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16t1021087.DataLayers.SQLServer
{
    /// <summary>
    /// xử lý dữ liệu tài khoản liên quan đến khách hàng
    /// </summary>
    public class CustomerAccountDAL : _BaseDAL, IUserAccountDAL
    {
        public CustomerAccountDAL(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// kiểm tra user và pass có đúng hay không
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserAccount Authorize(string userName, string password)
        {
            UserAccount data = null;
            using (SqlConnection connection = OpenConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"SELECT EmployeeID, FirstName, LastName, Email
                                    FROM Employees
                                    Where Email = @Email And Password = @PassWord";
            }    

            return data;
        }

        /// <summary>
        /// Đổi mật khẩu của khách hàng -- chưa làm :))
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
