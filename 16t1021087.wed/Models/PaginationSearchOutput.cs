using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _16t1021087.wed.Models
{
    /// <summary>
    /// Lớp cơ sở cho các lớp dùng để lưu trữ kết quả tìm kiếm dưới dạng phân trang
    /// lớp abstract dùng để kế thừa có những tính năng được cài đặt sẵn chứ không được new các lớp trên nó
    ///  ở đây PaginationSearchOutput chỉ mô tả cái chung chung, chứ chưa đủ, không thể dùng trực tiếp, mô tả thêm dữ liệu đầu ra
    /// </summary>
    public abstract class PaginationSearchOutput
    {
        /// <summary>
        /// TRang cần hiển thị
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Số dòng cần hiển thị trên trang
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Số dòng tìm được 
        /// </summary>
        public string SearchValue { get; set; }

        /// <summary>
        /// Tổng số dòng tìm được
        /// </summary>
        public int RowCount { get; set; }

        public int PageCount
        {
            get
            {
                if(PageSize ==0)
                    return 1;
                int p = RowCount / PageSize;
                    if (RowCount % PageSize > 0)
                        p += 1;
                    return p;
            }    
        }

    }
}