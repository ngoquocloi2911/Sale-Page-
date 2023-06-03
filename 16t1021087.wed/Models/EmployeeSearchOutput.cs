using _16t1021087.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _16t1021087.wed.Models
{
    /// <summary>
    /// Kết quả tìm kiếm nhân viên dưới dạng phân trang
    /// </summary>
    public class EmployeeSearchOutput : PaginationSearchOutput
    {

        /// <summary>
        /// Danh sách nhân viên
        /// </summary>
        public List<Employye> Data { get; set; }
    }
}