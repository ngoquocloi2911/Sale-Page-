using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _16t1021087.DomainModels;
using _16t1021087.BussinessLayers;
using _16t1021087.wed.Models;

namespace _16t1021087.wed.Controllers
{
    [Authorize] // muốn vào phải login, đặt trước controller or login đều được
    public class SupplierController : Controller
    {
        // GET: Supplier
        /// <summary>
        /// GET: Supplier
        /// </summary>
        /// <returns></returns>
        // 
        private const int PAGE_SIZE = 5;

        private const string SUPPLIER_SEARCH = "SearchSupplierCondition";

        
       /// <summary>
       /// Dùng mục đích cho đầu vào tìm kiếm
       ///  hiển thị kết quả tìm kiếm
       /// </summary>
        public ActionResult Index()
        {
            /// Chuyển kiểu
           PaginationSearchInput condition = Session[SUPPLIER_SEARCH] as PaginationSearchInput;

            if(condition == null)
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

            int rowCount = 0;
            var data = CommonDataService.ListOfSuppliers(condition.Page,
                                                        condition.PageSize,
                                                        condition.SearchValue,
                                                        out rowCount);
            var result = new SupplierSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };
            Session[SUPPLIER_SEARCH] = condition;
            return View(result);
        }
        /// <summary>
        /// giao diện bổ sung nhà cung cấp mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            try 
            {
                var data = new Supplier()
                {
                    SupplierID = 0
                };
                ViewBag.Title = "Bổ sung nhà cung cấp";
                return View("Edit", data);
            } 
            catch (Exception ex) 
            {
                return Content("Có lỗi xảy ra vui lòng thử lại sau");
            }
            
        }
        /// <summary>
        /// Giao diện để chỉnh sửa nhà cung cấp
        /// Quy định tham số mặc định
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id = 0)
        {
            try 
            {
                if (id == 0)
                    return RedirectToAction("Index");


                var data = CommonDataService.GetSupplier(id);
                if (data == null)
                    return RedirectToAction("Index");


                ViewBag.Title = "Cập nhật nhà cung cấp";
                return View(data);
            } 
            catch (Exception ex)
            {
                // Ghi lại log lỗi (ex)
                return Content("Có lỗi xảy ra, vui lòng thử lại sau");
            }
           /* int supplierId = Convert.ToInt32(id);*/
           
        }
        /// <summary>
        /// Lưu lại danh sách dữ liệu ncc
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]        
        
        public ActionResult Save (Supplier data)
        {
            try 
            {
                // Kiểm soát đầu vào (hợp lệ hay không)
                if (string.IsNullOrWhiteSpace(data.SupplierName))
                    ModelState.AddModelError("SupplierName","Tên không được để trống");

                if (string.IsNullOrWhiteSpace(data.ContactName))
                    ModelState.AddModelError("ContactName", "Tên giao dịch không được để trống");

                if (string.IsNullOrWhiteSpace(data.Address))
                    ModelState.AddModelError("Address", "Địa chỉ không được để trống");

                if (string.IsNullOrWhiteSpace(data.Phone))
                    ModelState.AddModelError("Phone", "Số điện thoại không được để trống");

                if (string.IsNullOrWhiteSpace(data.City))
                    ModelState.AddModelError("City", "Thành phố không được để trống");

                if (string.IsNullOrWhiteSpace(data.PostalCode))
                    ModelState.AddModelError("PostalCode", "Mã bưu chính không được để trống");

                if (string.IsNullOrWhiteSpace(data.Country))
                    ModelState.AddModelError("Country", "Vui lòng chọn quốc gia");
               
                if(!ModelState.IsValid)
                {
                    ViewBag.Title = data.SupplierID == 0 ? "Bổ sung nhà cung cấp" : "Cập nhật nhà cung cấp";
                    return View("Edit", data);
                }    

                if (data.SupplierID == 0)
                {
                    CommonDataService.AddSupplier(data);
                }
                else
                {
                    CommonDataService.UpdateSupplier(data);
                }

                return RedirectToAction("Index");
            } 
            catch (Exception ex) 
            {
                // Ghi lại log lỗi (ex)
                return Content("Có lỗi xảy ra, vui lòng thử lại sau");
            }

            
        }
        /// <summary>
        /// Xóa nhà cung cấp 
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
                    var data = CommonDataService.GetSupplier(id);
                    if (data == null)
                    {
                        return RedirectToAction("index");
                    }
                    return View(data);
                }
                else
                {
                    CommonDataService.DeleteSupplier(id);
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