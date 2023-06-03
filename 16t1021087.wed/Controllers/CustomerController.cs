using _16t1021087.BussinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _16t1021087.DomainModels;
using _16t1021087.wed.Models;
using System.Drawing.Printing;

namespace _16t1021087.wed.Controllers
{
    /// <summary>
    /// thuộc tính authorize buộc người dùng phải đăng nhập để xác minh danh tính của họ
    /// - biến const là 1 hằng số, có thể khởi tạo bới một biểu thức nhưng phải đảm bảo các toán hạng trong biểu thức cũng phải là constant
    ///-- Sử dụng biến const khi chắc chắn giá trị đó sẽ không thay đổi
    ///Private chỉ truy cập trong nộp bộ của lớp này thôi.
    ///public thành phần công khai được truy cập tự do bên ngoài
    /// </summary>
    [Authorize]
    public class CustomerController : Controller
    {
        private const int PAGE_SIZE = 5;

        private const string CUSTOMER_SEARCH = "SearchCustomerCondition";
       /// <summary>
       /// dùng mục đích cho đầu vào tìm kiếm, hiển thị kết quả tìm kiếm
       /// Trang khách hàng
       /// </summary>
       /// <returns></returns>
        public ActionResult Index()
        {
            /// Chuyển kiểu
            PaginationSearchInput condition = Session[CUSTOMER_SEARCH] as PaginationSearchInput;

            if (condition == null)
            {
                condition = new PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = ""
                };
            }

            return View(condition);
        }

        /// <summary>
        /// Thực hiện chức năng tìm kiếm
        /// sử dụng biến session để khi thao tác trên 1 trang, 
        /// đi đâu đó quay lại thì nó vẫn ở trang vừa nãy mà mình tìm kiếm
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult Search(PaginationSearchInput condition)
        {
            try
            {
                int rowCount = 0;
                var data = CommonDataService.ListOfCustomers(condition.Page,
                                                            condition.PageSize,
                                                            condition.SearchValue,
                                                            out rowCount);
                var result = new CustomerSearchOutput()
                {
                    Page = condition.Page,
                    PageSize = condition.PageSize,
                    SearchValue = condition.SearchValue,
                    RowCount = rowCount,
                    Data = data
                };
                Session[CUSTOMER_SEARCH] = condition;
                return View(result);
            }
            catch
            {
                return Content("Có lỗi xảy ra, vui lòng thử lại");
            }
           
        }

            /// <summary>
            ///  giao diện bổ sung một khách hàng mới
            /// </summary>
            /// <returns></returns>
            public ActionResult Create()
        {
            var data = new Customer()
            {
                CustomerID = 0
            };
            ViewBag.Title = "Bổ sung khách hàng";
            return View("Edit", data);
        }

        /// <summary>
        /// Giao diện chỉnh sửa khách hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id=0)
        {
           
                if (id <= 0)
                    return RedirectToAction("Index");
                

                var data = CommonDataService.GetCustomers(id);
                if(data == null)
                {
                    return RedirectToAction("Index");
                }    
                ViewBag.Title = "Cập nhật khách hàng";
                return View(data);



        }

        /// <summary>
        /// lưu lại dữ liệu danh sách khách hàng
        /// validateantiforgerytoken ngăn chặn các cuộc tấn công giả mạo được yêu cầu trên nhiều trang wed
        /// - Thêm mã @Html.AntiForgeryToken() để xác thực biểu mẫu khi gửi
        /// post được dùng để lưu trữ, lấy dữ liệu, dữ liệu nối vào phần body
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer data)
        {
            try
            {
                // Kiểm soát đầu vào có hợp lệ hay không
                if (string.IsNullOrWhiteSpace(data.CustomerName))
                    ModelState.AddModelError("CustomerName", " Tên không được để trống");
                if (string.IsNullOrWhiteSpace(data.ContactName))
                    ModelState.AddModelError("ContactName", "Tên liên hệ không được để trống");
                if (string.IsNullOrWhiteSpace(data.Address))
                    ModelState.AddModelError("Address", "Địa chỉ không được để trống");
                if (string.IsNullOrWhiteSpace(data.Email))
                    ModelState.AddModelError("Email", "Email không được để trống");
                if (string.IsNullOrWhiteSpace(data.Country))
                    ModelState.AddModelError("Country", "Quốc gia không được để trống");
                if (string.IsNullOrWhiteSpace(data.City))
                    ModelState.AddModelError("City", "Thành phố không được để trống");
                if (string.IsNullOrWhiteSpace(data.PostalCode))
                    ModelState.AddModelError("PostalCode", "Mã bưu chính không được để trống");

                //hợp lệ
                if(!ModelState.IsValid) 
                {
                    ViewBag.Title = data.CustomerID == 0 ? "Bổ sung khách hàng" : "Cập nhật khách hàng";
                    return View("Edit", data);
                }

                if (data.CustomerID == 0)
                {
                    CommonDataService.AddCustomers(data);
                }
                else
                {
                    CommonDataService.UpdateCustomers(data);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content("Có lỗi xảy ra, vui lòng thử lại sau");
            }
            
        }

        /// <summary>
        /// xóa khách hàng không có trong mặt hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            try
            {
                if (id <= 0)
                    return RedirectToAction("index");

                if (Request.HttpMethod == "GET")
                {
                    var data = CommonDataService.GetCustomers(id);
                    if (data == null)
                    {
                        return RedirectToAction("index");
                    }
                    return View(data);
                }
                else
                {
                    CommonDataService.DeleteCustomers(id);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                return Content("Không thể thực hiện tác vụ này, vui lòng thử lại sau!");
            }

        }

    }
}