using _16t1021087.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _16t1021087.DataLayers.SQLServer;

namespace _16t1021087.DataLayers.SQLServer
{
    /// <summary>
    /// Cài đặt các phép xử lý dữ liệu cho khách hàng
    /// </summary>
    public class CustomerDAL : _BaseDAL, ICommonDAL<Customer>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public CustomerDAL(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// Thêm một khách hàng vào cơ sở dữ liệu
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Customer data)
        {
            int result = 0;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Customers(CustomerName, ContactName, Address, City, PostalCode, Country, Email,Password)
                                    VALUES(@CustomerName, @ContactName, @Address, @City, @PostalCode, @Country, @Email, @Password);
                                    SELECT SCOPE_IDENTITY()";
                //VALUES(@CustomerName, @ContactName, @Address, @City, @PostalCode, @Country, @Email, @Password);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@CustomerName", data.CustomerName);
                cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@City", data.City);
                cmd.Parameters.AddWithValue("@PostalCode", data.PostalCode);
                cmd.Parameters.AddWithValue("@Country", data.Country);
                cmd.Parameters.AddWithValue("@Email", data.Email);
                cmd.Parameters.AddWithValue("@Password", "");


                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// tìm kiếm khách hàng theo họ tên, tên liên lạc, địa chỉ, email
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public int Count(string searchValue = "")
        {
            int count = 0;

            if (searchValue != "")
                searchValue = "%" + searchValue + "%";

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT	COUNT(*)
                                    FROM	Customers 
                                    WHERE	(@SearchValue = N'')
	                                    OR	(
			                                    (CustomerName LIKE @SearchValue)
			                                    OR (ContactName LIKE @SearchValue)
			                                    OR (Address LIKE @SearchValue)
                                                OR (Email LIKE @SearchValue)
		                                    )";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@SearchValue", searchValue);

                count = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }
            return count;
        }
        /// <summary>
        /// Xóa một khách hàng nếu khách hàng đó không có trong bảng dữ liệu order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM Customers 
                                    WHERE CustomerID = @CustomerID AND NOT EXISTS(SELECT * FROM Orders WHERE CustomerID = @CustomerID)";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@CustomerID", id);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// Lấy dữ liệu của một khách hàng từ mã khách hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer Get(int id)
        {
            Customer data = null;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM Customers WHERE CustomerID = @CustomerID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@CustomerID", id);
                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    data = new Customer()
                    {
                        CustomerID = Convert.ToInt32(dbReader["CustomerID"]),
                        CustomerName = Convert.ToString(dbReader["CustomerName"]),
                        ContactName = Convert.ToString(dbReader["ContactName"]),
                        Address = Convert.ToString(dbReader["Address"]),
                        City = Convert.ToString(dbReader["City"]),
                        PostalCode = Convert.ToString(dbReader["PostalCode"]),
                        Country = Convert.ToString(dbReader["Country"]),
                        Email = Convert.ToString(dbReader["Email"])
                    };
                }
                cn.Close();
            }
            return data;
        }
        /// <summary>
        /// kiểm tra xem một khách hàng có đang được sử dụng trong bảng khác hay không
        /// </summary>
        /// <param name="id"> mã khách hàng</param>
        /// <returns></returns>
        public bool InUsed(int id)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT CASE 
                                                WHEN EXISTS(SELECT * FROM Orders WHERE CustomerID = @CustomerID) THEN 1 
                                                ELSE 0 
                                            END";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@CustomerID", id);

                result = Convert.ToBoolean(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// lấy ra danh sách khách hàng theo từ khóa tìm kiếm, có phân trang( pageSize = 0 thì không phân trang)
        /// </summary>
        /// <param name="page">số trang</param>
        /// <param name="pageSize">số dòng trên một trang</param>
        /// <param name="searchValue"> từ khóa tìm kiếm</param>
        /// <returns></returns>
        public IList<Customer> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            List<Customer> data = new List<Customer>();

            if (searchValue != "")
                searchValue = "%" + searchValue + "%";

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT *
                                    FROM 
                                    (
	                                    SELECT	*, ROW_NUMBER() OVER (ORDER BY CustomerName) AS RowNumber
	                                    FROM	Customers 
	                                    WHERE	(@SearchValue = N'')
		                                    OR	(
				                                    (CustomerName LIKE @SearchValue)
			                                     OR (ContactName LIKE @SearchValue)
			                                     OR (Address LIKE @SearchValue)
                                                 OR (Email LIKE @SearchValue)
			                                    )
                                    ) AS t
                                    WHERE (@PageSize = 0) OR (t.RowNumber BETWEEN (@Page - 1) * @PageSize + 1 AND @Page * @PageSize);";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@Page", page);
                cmd.Parameters.AddWithValue("@PageSize", pageSize);
                cmd.Parameters.AddWithValue("@SearchValue", searchValue);

                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dbReader.Read())
                {
                    data.Add(new Customer()
                    {
                        CustomerID = Convert.ToInt32(dbReader["CustomerID"]),
                        CustomerName = Convert.ToString(dbReader["CustomerName"]),
                        ContactName = Convert.ToString(dbReader["ContactName"]),
                        Address = Convert.ToString(dbReader["Address"]),
                        City = Convert.ToString(dbReader["City"]),
                        PostalCode = Convert.ToString(dbReader["PostalCode"]),
                        Country = Convert.ToString(dbReader["Country"]),
                        Email = Convert.ToString(dbReader["Email"]),
                        Password = Convert.ToString(dbReader["Password"]),

                    });
                }
                dbReader.Close();
                cn.Close();
            }

            return data;
        }
        /// <summary>
        /// Cập nhật dữ liệu của một khách hàng
        /// </summary>
        /// <param name="data"> mã khách hàng</param>
        /// <returns></returns>
        public bool Update(Customer data)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE Customers
                                    SET CustomerName =@CustomerName, ContactName = @ContactName, Address = @Address, City = @City, PostalCode = @PostalCode, Country = @Country, Email = @Email
                                    WHERE CustomerID = @CustomerID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@CustomerID", data.CustomerID);
                cmd.Parameters.AddWithValue("@CustomerName", data.CustomerName);
                cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@City", data.City);
                cmd.Parameters.AddWithValue("@PostalCode", data.PostalCode);
                cmd.Parameters.AddWithValue("@Country", data.Country);
                SqlParameter emailPara = cmd.Parameters.AddWithValue("@Email", data.Email);
                if(data.Email == null)
                {
                    emailPara.Value = DBNull.Value;
                }    

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }

        
    }
}
