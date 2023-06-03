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
    /// Cài đặt xử lý dữ liệu liên quan đến quốc gia
    /// </summary>
    public class CountryDAL : _BaseDAL, ICountryDAL
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="connectionString"></param>
        public CountryDAL(string connectionString) : base(connectionString)
        {

        }
        /// <summary>
        /// lấy ra danh sách của quốc gia
        /// </summary>
        /// <returns></returns>
        public IList<Country> List()
        {
            List<Country> data = new List<Country>();
            using (var connection = OpenConnection())
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT CountryName FROM Countries";
                cmd.CommandType = CommandType.Text;
                SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dbReader.Read())
                {
                    data.Add(new Country()
                    {
                        CountryName = Convert.ToString(dbReader["CountryName"])
                    });
                }

                dbReader.Close();
                connection.Close();
            }

            return data;
        }
    }
}
