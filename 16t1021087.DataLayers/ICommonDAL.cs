using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _16t1021087.DomainModels;

namespace _16t1021087.DataLayers
{/// <summary>
 /// Định nghĩa các phép dữ liệu chung cho các dữ liệu
 /// </summary>
    public interface ICommonDAL<T> where T : class
    {
        /// <summary>
        /// Tìm kiếm và lấy danh sách các nhà cung cấp dưới dạng phân trang
        /// </summary>
        /// <paramref name="page"/> Trang cần hiển thị</param>
        /// <paramref name="pageSize"/> Số dòng hiển thị trên mỗi trang(0 tức là ko yêu cầu phân trang)</param>
        /// <paramref name="searchValue"/>  Tên cần tìm kiếm(chuỗi rỗng nếu ko tìm kiếm theo tên)</param>
        /// <returns></returns>
        IList<T> List(int page = 1, int pageSize = 0, string searchValue = "");
        /// <summary>
        /// Đếm số nhà cung cấp tìm đc
        /// </summary>
        /// <param name="searchValue"> Tên cần tìm kiếm(chuỗi rỗng nếu ko tìm kiếm theo tên)</param>
        /// <returns></returns>
        int Count(string searchValue = "");
        /// <summary>
        /// Bổ sung thêm nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(T data);
        /// <summary>
        /// Cập nhật 
        /// </summary>
        /// <param name="data"></param>
        /// <returns> ID dữ liệu</returns>
        bool Update(T data);
        /// <summary>
        /// XÓA
        /// </summary>
        /// <param name="id"> mã của nhà cc cần xoa</param>
        /// <returns></returns>
        bool Delete(int id);
        /// <summary>
        /// Lấy thông tin 
        /// </summary>
        /// <param name="id">mã của nhà cung cấp</param>
        /// <returns></returns>
        T Get(int id);
        /// <summary>
        /// Kiểm tra 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool InUsed(int id);
    }
}
