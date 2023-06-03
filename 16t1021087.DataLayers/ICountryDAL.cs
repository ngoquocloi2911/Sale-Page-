using _16t1021087.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16t1021087.DataLayers
{/// <summary>
 /// Các chức năng xử lý dữ liệu cho quốc gia
 /// </summary>
    public interface ICountryDAL
    {
        /// <summary>
        /// Lấy danh sách các quốc gia
        /// </summary>
        IList<Country> List();
    }
}

