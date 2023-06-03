using _16t1021087.DomainModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace _16t1021087.wed
{
    /// <summary>
    /// hàm chuyển đổi ngày tháng sang đúng định dạng
    /// </summary>
    public class Converter
    {
        public static DateTime? DMYStringToDateTime(string s, string format = "d/M/yyyy")
        {
            try
            {
                return DateTime.ParseExact(s, format, CultureInfo.InvariantCulture);
            }
            catch 
            {
                return null;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public static UserAccount CookieToUserAccount (string cookie)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<UserAccount>(cookie);
        }
          
    }
}