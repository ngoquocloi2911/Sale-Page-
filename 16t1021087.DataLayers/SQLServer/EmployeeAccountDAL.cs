using _16t1021087.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _16t1021087.DataLayers;

namespace _16t1021087.DataLayers.SQLServer
{
    /// <summary>
    /// Xử lý dữ liệu liên quan đến tài khoản nhân viên
    /// </summary>
    public class EmployeeAccountDAL : _BaseDAL, IUserAccountDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public EmployeeAccountDAL(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// kiểm tra tk , mk của nhân viên có đúng k
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public UserAccount Authorize(string userName, string password)
        {
            UserAccount data = null;
            using (SqlConnection connection = OpenConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"SELECT EmployeeID, FirstName, LastName, Email, Photo
                                    FROM Employees
                                    Where Email = @Email And Password = @PassWord";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Email", userName);
                cmd.Parameters.AddWithValue("Password", password);
                


                using (var dbReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if(dbReader.Read())
                    {
                        data = new UserAccount()
                        {
                            UserId = Convert.ToString(dbReader["EmployeeID"]),
                            UserName = Convert.ToString(dbReader["Email"]),
                            FullName = $"{dbReader["FirstName"]} {dbReader["LastName"]}",
                            Email = Convert.ToString(dbReader["Email"]),
                            Password = "",
                            Photo = Convert.ToString(dbReader["Photo"]),
                            RoleName = ""
                        };
                    }    
                    dbReader.Close();
                }
                connection.Close();
            }

            return data;
        }



        /// <summary>
        /// Đổi mật khẩu nhân viên
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            if (string.IsNullOrEmpty(userName))
            {
                // Xử lý trường hợp tên người dùng là null hoặc trống
                throw new ArgumentException("tên người dùng không thể null hoặc rỗng.", nameof(userName));
            }

            bool result = false;
            using (var connection = OpenConnection())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"UPDATE Employees
                            SET Password = @newPassword
                            WHERE Email = @Email AND Password = @oldPassword";

                cmd.Parameters.AddWithValue("@Email", userName);
                cmd.Parameters.AddWithValue("@oldPassword", oldPassword);
                cmd.Parameters.AddWithValue("@newPassword", newPassword);

                result = cmd.ExecuteNonQuery() > 0;
            }
            return result;
        }
    }
}
