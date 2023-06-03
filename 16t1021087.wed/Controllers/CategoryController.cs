using _16t1021087.BussinessLayers;
using _16t1021087.DomainModels;
using _16t1021087.wed.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _16t1021087.wed.Controllers
    
    
{
    /// <summary>
    /// dùng authorize người dùng buộc phải đăng nhập để xác minh danh tính của họ
    /// một cái gì mà được định nghĩa 2 lần trở lên thì nên dùng thành hàm!!
    /// 
    /// </summary>
    [Authorize]
    public class CategoryController : Controller
    {

        private const int PAGE_SIZE = 5;

        private const string CATEGORY_SEARCH = "SearchCategoryCondition";
   
        /// <summary>
        /// Dùng mục đích cho đầu vào tìm kiếm
        ///  hiển thị kết quả tìm kiếm
        /// </summary>
        public ActionResult Index()
        {
            /// Trang loại hàng
            PaginationSearchInput condition = Session[CATEGORY_SEARCH] as PaginationSearchInput;

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
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult Search(PaginationSearchInput condition)
        {
            try 
            {
                int rowCount = 0;
                var data = CommonDataService.ListOfCategories(condition.Page,
                                                            condition.PageSize,
                                                            condition.SearchValue,
                                                            out rowCount);
                var result = new CategorySearchOutput()
                {
                    Page = condition.Page,
                    PageSize = condition.PageSize,
                    SearchValue = condition.SearchValue,
                    RowCount = rowCount,
                    Data = data
                };
                Session[CATEGORY_SEARCH] = condition;
                return View(result);
            }
            
            catch 
            {
                return Content("Có lỗi xảy ra, vui lòng thử lại sau");
            }
            
        }

        /// <summary>
        /// Bổ sung một loại hàng mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()

        {
            try 
            {
                var data = new Category()
                {
                    CategoryID = 0
                };
                ViewBag.Title = "Bổ sung loại hàng";
                return View("Edit", data);
            }
            catch
            {
                return Content("Có lỗi xảy ra, vui lòng thử lại sau");
            }
           
        }
        /// <summary>
        /// Cập nhật một loại hàng mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit( int id)
        {

            try
            {
                int categoryID = Convert.ToInt32(id);

                var data = CommonDataService.GetCategories(categoryID);

                ViewBag.Title = "Cập nhật loại hàng";
                return View(data);
            }
            catch
            {
                return Content("Có lỗi xảy ra, vui lòng thử lại sau");
            }
            
        }

        /// <summary>
        /// Lưu lại dữ liệu danh sách loại hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Category data)
        {

            try 
            {
                // Kiểm soát đầu vào (hợp lệ hay không)
                if (string.IsNullOrWhiteSpace(data.CategoryName))
                    ModelState.AddModelError("CategoryName", "Tên không được để trống");

                if (string.IsNullOrWhiteSpace(data.Description))
                    ModelState.AddModelError("Description", "Tên giao dịch không được để trống");
                
                //hợp lệ
                if (!ModelState.IsValid)
                {
                    ViewBag.Title = data.CategoryID == 0 ? "Bổ sung loại hàng" : "Cập nhật loại hàng";
                    return View("Edit", data);
                }

                if (data.CategoryID == 0)
                {
                    CommonDataService.AddCategories(data);
                }
                else
                {
                    CommonDataService.UpdateCategories(data);
                }
                return RedirectToAction("Index");
            }
            catch 
            {
                return Content("Có lỗi xảy ra, vui lòng thử lại sau");
            }

        }

        /// <summary>
        /// xóa môt loại hàng không có trong mặt hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id = 0)
        {
            try
            {
                if (id <= 0)
                    return RedirectToAction("index");

                if (Request.HttpMethod == "GET")
                {
                    var data = CommonDataService.GetCategories(id);
                    if (data == null)
                    {
                        return RedirectToAction("index");
                    }
                    return View(data);
                }
                else
                {
                   
                    CommonDataService.DeleteCategories(id);
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