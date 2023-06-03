using _16t1021087.BussinessLayers;
using _16t1021087.DomainModels;
using _16t1021087.wed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _16t1021087.wed.Controllers
{
    [Authorize]
    public class ShipperController : Controller
    {

        private const int PAGE_SIZE = 5;

        private const string SHIPPER_SEARCH = "SearchShipperCondition";
   

        /// <summary>
        /// Dùng mục đích kiểm tra đầu vào
        /// Hiển thị kết quả tìm kiếm
        /// </summary>
        public ActionResult Index()
        {
            /// Chuyển kiểu
            PaginationSearchInput condition = Session[SHIPPER_SEARCH] as PaginationSearchInput;

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
                var data = CommonDataService.ListOfShippers(condition.Page,
                                                            condition.PageSize,
                                                            condition.SearchValue,
                                                            out rowCount);
                var result = new ShipperSearchOutput()
                {
                    Page = condition.Page,
                    PageSize = condition.PageSize,
                    SearchValue = condition.SearchValue,
                    RowCount = rowCount,
                    Data = data
                };
                Session[SHIPPER_SEARCH] = condition;
                return View(result);
            }
            catch 
            {
                return Content("Có lỗi xảy ra, vui lòng thử lại");
            }


           
        }
        /// <summary>
        ///  Bổ sung một người giao hàng mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            try 
            {
                var data = new Shipper()
                {
                    ShipperID = 0
                };
                ViewBag.Title = "Bổ sung người giao hàng";
                return View("Edit", data);
            } catch
            {
                return Content("Có lỗi xảy ra, vui lòng thử lại");
            }
            
        }
        /// <summary>
        ///  Cập nhật người giao hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit( int id)
        {
            try 
            {
                int shipperID = Convert.ToInt32(id);

                var data = CommonDataService.GetShippers(shipperID);

                ViewBag.Title = "Cập nhật người giao hàng";
                return View(data);
            } catch
            {
                return Content("Có lỗi xảy ra, vui lòng thử lại");
            }
            
        }

        /// <summary>
        /// Lưu lại dữ liệu danh sách người giao hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Shipper data)
        {
            try 
            {
                // Kiểm soát đầu vào (hợp lệ hay không)
                if (string.IsNullOrWhiteSpace(data.ShipperName))
                    ModelState.AddModelError("ShipperName", "Tên không được để trống");

                if (string.IsNullOrWhiteSpace(data.Phone))
                    ModelState.AddModelError("Phone", "Tên giao dịch không được để trống");
          
                if (!ModelState.IsValid)
                {
                    ViewBag.Title = data.ShipperID == 0 ? "Bổ sung nhà cung cấp" : "Cập nhật nhà cung cấp";
                    return View("Edit", data);
                }


                if (data.ShipperID == 0)
                {
                    CommonDataService.AddShippers(data);
                }
                else
                {
                    CommonDataService.UpdateShippers(data);
                }
                return RedirectToAction("Index");
            } 
            catch 
            {
                return Content("Có lỗi xảy ra, vui lòng thử lại");
            }
            
        }

        /// <summary>
        /// Xóa người giao hàng không tham gia vào các nghiệp vụ khác
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            try
            {
                if (id <= 0)
                    return RedirectToAction("index");

                if (Request.HttpMethod == "GET")
                {
                    var data = CommonDataService.GetShippers(id);
                    if (data == null)
                    {
                        return RedirectToAction("index");
                    }
                    return View(data);
                }
                else
                {
                    CommonDataService.DeleteShippers(id);
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