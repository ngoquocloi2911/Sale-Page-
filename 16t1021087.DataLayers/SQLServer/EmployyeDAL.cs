using _16t1021087.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16t1021087.DataLayers.SQLServer
{
    /// <summary>
    /// Cài đặt các phép xử lý dữ liệu cho nhà cc
    /// </summary>
    public class EmployyeDAL : _BaseDAL, ICommonDAL<Employye>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public EmployyeDAL(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// Thêm một nhân viên vào cơ sở dữ liệu
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Employye data)
        {
            int result = 0;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Employees(LastName, FirstName, BirthDate, Photo, Notes, Email,Password)
                                    VALUES(@LastName, @FirstName, @BirthDate, @Photo, @Notes, @Email, @Password);
                                    SELECT SCOPE_IDENTITY()";
                //VALUES(@LastName, @FirstName, @BirthDate, @Photo, @Notes, @Country, @Email, @Password);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@LastName", data.LastName);
                cmd.Parameters.AddWithValue("@FirstName", data.FirstName);
                cmd.Parameters.AddWithValue("@BirthDate", data.BirthDate);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Notes", data.Notes);
                cmd.Parameters.AddWithValue("@Email", data.Email);
                cmd.Parameters.AddWithValue("@Password", "");


                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// tìm kiếm nhân viên theo họ, tên, ngày sinh, email
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
                                    FROM	Employees 
                                    WHERE	(@SearchValue = N'')
	                                    OR	(
			                                    (LastName LIKE @SearchValue)
			                                    OR (FirstName LIKE @SearchValue)
			                                    OR (BirthDate LIKE @SearchValue)
                                                OR (Notes LIKE @SearchValue)
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
        /// Xóa một nhân viên 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM Employees 
                                    WHERE EmployeeID = @EmployeeID AND NOT EXISTS(SELECT * FROM Orders WHERE EmployeeID = @EmployeeID)";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@EmployeeID", id);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// Lấy dữ liệu nhân viên từ mã nhân viên
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Employye Get(int id)
        {

            Employye data = null;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM Employees WHERE EmployeeID = @EmployeeID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@EmployeeID", id);
                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    data = new Employye()
                    {
                        EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                        LastName = Convert.ToString(dbReader["LastName"]),
                        FirstName = Convert.ToString(dbReader["FirstName"]),
                        BirthDate = Convert.ToDateTime(dbReader["BirthDate"]),
                        Photo = Convert.ToString(dbReader["Photo"]),
                        Notes = Convert.ToString(dbReader["Notes"]),
                        Email = Convert.ToString(dbReader["Email"]),
                        Password = Convert.ToString(dbReader["Password"])
                    };
                }
                cn.Close();
            }
            return data;
        }
        /// <summary>
        /// kiểm tra xem một nhân viên có đang được sử dụng trong bảng khác hay không
        /// </summary>
        /// <param name="id"> Mã nhân viên</param>
        /// <returns></returns>
        public bool InUsed(int id)
        {

            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT CASE 
                                                WHEN EXISTS(SELECT * FROM Orders WHERE EmployeeID = @EmployeeID) THEN 1 
                                                ELSE 0 
                                            END";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@EmployeeID", id);

                result = Convert.ToBoolean(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// lấy ra danh sách nhân viên theo từ khóa tìm kiếm, có phân trang( pageSize = 0 thì không phân trang)
        /// </summary>
        /// <param name="page"> số trang</param>
        /// <param name="pageSize"> số dòng trên một trang</param>
        /// <param name="searchValue"> giá trị tìm kiếm</param>
        /// <returns></returns>
        public IList<Employye> List(int page = 1, int pageSize = 0, string searchValue = "")
        {
            List<Employye> data = new List<Employye>();

            if (searchValue != "")
                searchValue = "%" + searchValue + "%";

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT *
                                    FROM 
                                    (
	                                    SELECT	*, ROW_NUMBER() OVER (ORDER BY LastName) AS RowNumber
	                                    FROM	Employees 
	                                    WHERE	(@SearchValue = N'')
		                                    OR	(
				                                    (LastName LIKE @SearchValue)
			                                     OR (FirstName LIKE @SearchValue)
			                                     OR (BirthDate LIKE @SearchValue)
                                                 OR (Notes LIKE @SearchValue)
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
                    data.Add(new Employye()
                    {
                        EmployeeID = Convert.ToInt32(dbReader["EmployeeID"]),
                        LastName = Convert.ToString(dbReader["LastName"]),
                        FirstName = Convert.ToString(dbReader["FirstName"]),
                        BirthDate = Convert.ToDateTime(dbReader["BirthDate"]),
                        Photo = Convert.ToString(dbReader["Photo"]),
                        Notes = Convert.ToString(dbReader["Notes"]),
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
        /// Cập nhật dữ liệu của một nhân viên
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(Employye data)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                if (data.Photo != null)
                {
                    cmd.CommandText = @"UPDATE Employees
                                    SET LastName = @LastName, FirstName = @FirstName, BirthDate = @BirthDate, Photo = @Photo, Notes = @Notes, Email = @Email
                                    WHERE EmployeeID = @EmployeeID";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;
                    cmd.Parameters.AddWithValue("@EmployeeID", data.EmployeeID);
                    cmd.Parameters.AddWithValue("@LastName", data.LastName);
                    cmd.Parameters.AddWithValue("@FirstName", data.FirstName);
                    cmd.Parameters.AddWithValue("@BirthDate", data.BirthDate);
                    cmd.Parameters.AddWithValue("@Photo", data.Photo);
                    cmd.Parameters.AddWithValue("@Notes", data.Notes);
                    cmd.Parameters.AddWithValue("@Email", data.Email);

                }
                else
                {
                    cmd.CommandText = @"UPDATE Employees
                                    SET LastName = @LastName, FirstName = @FirstName, BirthDate = @BirthDate, Notes = @Notes, Email = @Email
                                    WHERE EmployeeID = @EmployeeID";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;
                    cmd.Parameters.AddWithValue("@EmployeeID", data.EmployeeID);
                    cmd.Parameters.AddWithValue("@LastName", data.LastName);
                    cmd.Parameters.AddWithValue("@FirstName", data.FirstName);
                    cmd.Parameters.AddWithValue("@BirthDate", data.BirthDate);
                    cmd.Parameters.AddWithValue("@Notes", data.Notes);
                    cmd.Parameters.AddWithValue("@Email", data.Email);
                }



                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }


    }
}
