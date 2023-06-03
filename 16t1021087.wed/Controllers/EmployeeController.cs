using _16t1021087.BussinessLayers;
using _16t1021087.DomainModels;
using _16t1021087.wed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Xml.Schema;

namespace _16t1021087.wed.Controllers
    
{
    /// <summary>
    /// dùng thuộc tính authorize buộc người dùng phải đăng nhập  để xác minh danh tính của họ
    ///  - biến const là 1 hằng số, có thể khởi tạo bới một biểu thức nhưng phải đảm bảo các toán hạng trong biểu thức cũng phải là constant
    ///-- Sử dụng biến const khi chắc chắn giá trị đó sẽ không thay đổi
    /// Private chỉ truy cập trong nộp bộ của lớp này thôi.
    /// public thành phần công khai được truy cập tự do bên ngoài
    /// </summary>
    [Authorize]
    public class EmployeeController : Controller
    {
        private const int PAGE_SIZE = 5;

        private const string EMPLOYEE_SEARCH = "SearchEmployeeCondition";   

        /// <summary>
        /// Dùng mục đích cho đầu vào tìm kiếm
        ///  hiển thị kết quả tìm kiếm
        /// </summary>
        public ActionResult Index()
        {
            try
            {
                PaginationSearchInput condition = Session[EMPLOYEE_SEARCH] as PaginationSearchInput;

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
            catch 
            {
                return Content("Có lỗi xảy ra, vui lòng thử lại");
            }
           
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
                var data = CommonDataService.ListOfEmployees(condition.Page,
                                                            condition.PageSize,
                                                            condition.SearchValue,
                                                            out rowCount);
                var result = new EmployeeSearchOutput()
                {
                    Page = condition.Page,
                    PageSize = condition.PageSize,
                    SearchValue = condition.SearchValue,
                    RowCount = rowCount,
                    Data = data
                };
                Session[EMPLOYEE_SEARCH] = condition;
                return View(result);
            } 
            catch
            {
                return Content("Có lỗi xảy ra, vui lòng thử lại");
            }
            
        }
        /// <summary>
        /// giao diện bổ sung một nhân viên mới
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult Create()
        {
            try 
            {
                var data = new Employye()
                {
                    EmployeeID = 0
                };
                ViewBag.Title = "Bổ sung nhân viên";
                return View("Edit", data);
            } 
            catch
            {
                return Content("Có lỗi xảy ra, vui lòng thử lại");
            }
           
        }
        /// <summary>
        /// Giao diện chỉnh sửa nhân viên
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit( string id)
        {
            try
            {
                int employeeID = Convert.ToInt32(id);

                var data = CommonDataService.GetEmployees(employeeID);

                ViewBag.Title = "Cập nhật nhân viên";
                return View(data);
            } 
            catch
            {
                return Content("Có lỗi xảy ra, vui lòng thử lại");
            }
            
        }


        /// <summary>
        /// lưu lại dữ liệu danh sách nhân viên
        /// post được sử dụng để lưu trữ và cập nhập dữ liệu, dữ liệu được nối vào phần body
        /// validateantiforgerytoken ngăn chặn các cuộc tấn công giả mạo được yêu cầu trên nhiều trang wed
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Employye data, string birthday, HttpPostedFileBase uploadPhoto)
        {
            
                
                if ( string.IsNullOrWhiteSpace(birthday) )
                {
                    ModelState.AddModelError("BirthDate", "Ngày  sinh không được để trống");
                }
                else
                {
                // chuyển kiểu 
                    DateTime? d = Converter.DMYStringToDateTime(birthday);
                    if (d == null)
                        ModelState.AddModelError("BirthDate", $"ngày {birthday} sinh không hợp lệ");
                    else
                        data.BirthDate = d.Value;
                }
                
                
                // Kiểm soát đầu vào có hợp lệ hay không
                if (string.IsNullOrWhiteSpace(data.LastName))
                    ModelState.AddModelError("LastName", " Họ không được để trống");

                if (string.IsNullOrWhiteSpace(data.FirstName))
                    ModelState.AddModelError("FirstName", "Tên không được để trống");
   
                if (string.IsNullOrWhiteSpace(data.Email))
                    ModelState.AddModelError("Email", "Email không được để trống");


                if (string.IsNullOrWhiteSpace(data.Notes))
                    ModelState.AddModelError("Notes", "ghi chú không được để trống");
             
               

                if(uploadPhoto!= null)
                {
                // lưu file vào đường dẫn vật lý, đường dẫn ảo
                    string path = Server.MapPath("~/Photo");
                    string fileName= $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                    string filePath = System.IO.Path.Combine(path, fileName);// phép cộng chuỗi
                    uploadPhoto.SaveAs(filePath);
                    data.Photo = fileName;
                }
                else if(data.Photo == null)
                {
                    ModelState.AddModelError("Photo", "Ảnh không được để trống");

                }

                   // hợp lệ
                 if (!ModelState.IsValid)
                 {
                     ViewBag.Title = data.EmployeeID == 0 ? "Bổ sung nhân viên" : "Cập nhật nhân viên";
                 return View("Edit", data);
                 }



            if (data.EmployeeID == 0)
                {
                    CommonDataService.AddEmployees(data);
                }
                else
                {
                    CommonDataService.UpdateEmployees(data);
                }
                return RedirectToAction("Index");
           
            
        }

        /// <summary>
        /// Xóa nhân viên không có trong mặt hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete( int id)
        {
            try
            {
                if (id <= 0)
                    return RedirectToAction("index");

                if (Request.HttpMethod == "GET")
                {
                    var data = CommonDataService.GetEmployees(id);
                    if (data == null)
                    {
                        return RedirectToAction("index");
                    }
                    return View(data);
                }
                else
                {
                    CommonDataService.DeleteEmployees(id);
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