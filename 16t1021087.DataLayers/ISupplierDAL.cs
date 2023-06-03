using _16t1021087.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16t1021087.DataLayers
{
    /// <summary>
    /// Định nghĩa các phép xử lý dữ liệu trên nhà cung cấp
    /// Sử dụng cách này dẫn đến viết lặp đi lặp lại các kiểu code giống nhau các đối tướng sd
    /// </summary>
    public interface ISupplierDAL
    {
        /// <summary>
        /// Tìm kiếm và lấy danh sách các nhà cung cấp dưới dạng phân trang
        /// </summary>
        /// <paramref name="page"/> Trang cần hiển thị</param>
        /// <paramref name="pageSize"/> Số dòng hiển thị trên mỗi trang(0 tức là ko yêu cầu phân trang)</param>
        /// <paramref name="searchValue"/>  Tên cần tìm kiếm(chuỗi rỗng nếu ko tìm kiếm theo tên)</param>
        /// <returns></returns>
        IList<Supplier> List(int page = 1, int pageSize = 0, string searchValue = "");
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
        int Add(Supplier data);
        /// <summary>
        /// Cập nhật thông tin của ncc
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Supplier data);
        /// <summary>
        /// Xóa 1 nhà cc dựa vào mã của nhà cc
        /// </summary>
        /// <param name="supplierID"> mã của nhà cc cần xoa</param>
        /// <returns></returns>
        bool Delete(int supplierID);
        /// <summary>
        /// Lấy thông tin của 1 nhà cc dựa vào mã của nhà cc 
        /// </summary>
        /// <param name="supplierID">mã của nhà cung cấp</param>
        /// <returns></returns>
        Supplier Get(int supplierID);
        /// <summary>
        /// Kiểm tra xem nhà cung cấp hienj có dữ liệu liên quan hay không?
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        bool InUsed(int supplierID);
    }
}
