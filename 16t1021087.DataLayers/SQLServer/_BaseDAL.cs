using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16t1021087.DataLayers.SQLServer
{
    /// <summary>
    /// Lớp cơ sở cho các lớp xử lý dữ liệu liên quan đến SQL sever
    /// </summary>
    public abstract class _BaseDAL
    {
        /// <summary>
        /// Chuỗi tham số kết nối CSDL
        /// </summary>
        protected string _connectionString; // Access Modifier
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="connecttionString"></param>
        public _BaseDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Tạo và mở kết nối csdl
        /// </summary>
        /// <returns></returns>
        protected SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _connectionString;
            connection.Open();
            return connection;
        }
        /// <summary>
        /// Đổi 1 giá trị sang giá trị để tương thích với dữ liệu được lưu cơ sở dữ liệu
        /// (Giải thích: vì giá trị null muốn lưu vào CSDL phải chuyển thành DBNull.Value)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected object ToDBValue(object value)
        {
            if (value != null)
                return value;
            return DBNull.Value;
        }
        /// <summary>
        /// Chuyển giá trị từ trong CSDL sang kiểu Nullable int
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected int? DBValueToNullableInt(object value)
        {
            try
            {
                if (value == DBNull.Value)
                    return null;
                return Convert.ToInt32(value);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Chuyển giá trị từ trong CSDL sang kiểu Nullable DateTime
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected DateTime? DBValueToNullableDateTime(object value)
        {
            try
            {
                if (value == DBNull.Value)
                    return null;
                return Convert.ToDateTime(value);
            }
            catch
            {
                return null;
            }
        }
    }
}
