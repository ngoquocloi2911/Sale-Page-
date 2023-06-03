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
    /// <summary>
    /// dùng thuộc tính authorize buộc người dùng phải đăng nhập  để xác minh danh tính của họ
    /// [RoutePrefix("product")] tiền tố chung cho tất cả các tuyến đường được xác định trong bộ điều khiển đều phải đi qua
    /// 
    /// </summary>
    [Authorize]
    [RoutePrefix("product")]
    public class ProductController : Controller
    {

        /// <summary>
        /// -- biến const là 1 hằng số, có thể khởi tạo bới một biểu thức nhưng phải đảm bảo các toán hạng trong biểu thức cũng phải là constant
        ///-- Sử dụng biến const khi chắc chắn giá trị đó sẽ không thay đổi
        /// </summary>
        private const string PRODUCT_SEARCH = "SearchProductCondition";
        /// <summary>
        /// Tìm kiếm, hiển thị mặt hàng dưới dạng phân trang
        /// trang mặt hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Index( int CategoryID =0, int SupplierID =0)
        {
            ProductSearchInput model = Session[PRODUCT_SEARCH] as ProductSearchInput;

            if (model == null)
            {
                model = new ProductSearchInput()
                {
                    Page = 1,
                    PageSize = 10,
                    SearchValue = ""
                };
            }

            if (CategoryID > 0 || SupplierID > 0)
            {
                model.CategoryID = CategoryID;
                model.SupplierID = SupplierID;
                model.SearchValue = "";
            }

            return View(model);
        }
        /// <summary>
        /// Tạo mặt hàng mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {

            var data = new Product()
            {
                ProductID = 0
            };
            ViewBag.Title = "Bổ sung mặt hàng";
            return View( data);
        }


        /// <summary>
        /// Thực hiện chức nằn tìm kiếm
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult Search(ProductSearchInput condition)
        {
            int rowCount = 0;
            var data = ProductDataService.ListProducts(condition.Page,
                                                         condition.PageSize,
                                                         condition.SearchValue,
                                                         condition.CategoryID,
                                                         condition.SupplierID,
                                                         out rowCount

                                                          );
            var result = new ProductSearchOutput()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue,
                RowCount = rowCount,
                Data = data
            };

            Session[PRODUCT_SEARCH] = condition;
            return View(result);
        }
        /// <summary>
        /// Cập nhật thông tin mặt hàng, 
        /// Hiển thị danh sách ảnh và thuộc tính của mặt hàng, điều hướng đến các chức năng
        /// quản lý ảnh và thuộc tính của mặt hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        [Route("edit/{productID}")]
        public ActionResult Edit(int productID)
        {
            int id = 0;
            try
            {
                id = Convert.ToInt32(productID);
            }
            catch
            {
                return RedirectToAction("Index");
            }
            ViewBag.Title = "Cập nhật thông tin mặt hàng";

            Product model = ProductDataService.GetProduct(id);

            if (model == null)
            {
                RedirectToAction("Index");
            }

            return View(model);
        }


        /// <summary>
        /// Xóa mặt hàng không có đơn hàng nào liên quan
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        [Route("delete/{productID}")]
        public ActionResult Delete(int productID)
        {
            int id = 0;

            try
            {
                id = Convert.ToInt32(productID);
            }
            catch
            {
                return RedirectToAction("Index");
            }

            if (Request.HttpMethod == "POST")
            {
                ProductDataService.DeleteProduct(id);
                return RedirectToAction("Index");
            }

            var data = ProductDataService.GetProduct(id);

            if (data == null)
                return RedirectToAction("Index");

            return View(data);
        }


        /// <summary>
        /// Lưu lại dữ liệu danh sách mặt hàng
        /// public ActionResult trả về một đối tượng thuộc loại actionresult có thể truy cấp bất kì đâu trong chương trình
        /// validateantiforgerytoken ngăn chặn các cuộc tấn công giả mạo được yêu cầu trên nhiều trang wed
        /// - Thêm mã @Html.AntiForgeryToken() để xác thực biểu mẫu khi gửi
        /// post được dùng để lưu trữ, lấy dữ liệu,
        /// </summary>
        /// <param name="data"></param>
        /// <param name="uploadPhoto"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Product data, HttpPostedFileBase uploadPhoto)
        {

           
            // kiểm soát đầu vào hợp lệ hay không
            if (string.IsNullOrWhiteSpace(data.ProductName))
                ModelState.AddModelError("ProductName", "Tên không được để trống");

            if (string.IsNullOrEmpty(data.Unit))
                ModelState.AddModelError("Unit", "đơn vị không được để trống");

            if (string.IsNullOrEmpty(data.SupplierID.ToString()))
                ModelState.AddModelError("SupplierID", "Nhà cung cấp không được để trống");

            if (string.IsNullOrEmpty(data.CategoryID.ToString()))
                ModelState.AddModelError("CategoryID", "Loại sản phẩm không được để trống");

            if (data.Price <= 0)
            {
                ModelState.AddModelError("Price", "giá bán không được để trống");
            }

          

            if (uploadPhoto != null)
            {
                // lưu file vào đường dẫn vật lý, đường dẫn ảo
                string path = Server.MapPath("~/Photo");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(path, fileName);// phép cộng chuỗi
                uploadPhoto.SaveAs(filePath);
                data.Photo = fileName;
                    
            }
            else if(data.Photo == null)
            {
                ModelState.AddModelError("Photo", "Ảnh không được để trống");
            }

            if (!ModelState.IsValid)
            {
                if (data.ProductID > 0)
                {
                    ViewBag.Title = "Cập nhật mặt hàng";
                    return View("edit", data);
                }
                else
                {
                    ViewBag.Title = "Bổ sung";
                    return View("create", data);
                }
            }

            if (data.ProductID > 0)
                ProductDataService.UpdateProduct(data);
            else
                ProductDataService.AddProduct(data);

           

            return RedirectToAction("Index");

         
        }


        /// <summary>
        /// Các chức năng quản lý ảnh của mặt hàng
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="photoID"></param>
        /// <returns></returns>
        [Route("photo/{method?}/{productID?}/{photoID?}")]
        public ActionResult Photo(string method, int productID, long? photoID)
        {
            ProductPhoto data = new ProductPhoto();
            int id = 0;
            switch (method)
            {
                case "add":
                    ViewBag.Title = "Bổ sung ảnh";

                    data.PhotoID = 0;
                    try
                    {
                        id = Convert.ToInt32(productID);
                    }
                    catch
                    {
                        return RedirectToAction("Index");
                    }
                    data.ProductID = id;
                    break;
                case "edit":

                    try
                    {
                        id = Convert.ToInt32(photoID);
                    }
                    catch
                    {
                        return RedirectToAction("Index");
                    }

                    data = ProductDataService.GetPhoto(id);
                    ViewBag.Title = "Thay đổi ảnh";
                    break;
                case "delete":
                    try
                    {
                        id = Convert.ToInt32(photoID);
                    }
                    catch
                    {
                        return RedirectToAction("Index");
                    }

                    ProductDataService.DeletePhoto(id);
                    return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }
            return View(data);
        }

        /// <summary>
        /// thực hiện chức năng lưu lại thông tin ảnh nhập vào
        /// </summary>
        /// <param name="data"></param>
        /// <param name="uploadPhoto"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SavePhoto(ProductPhoto data, HttpPostedFileBase uploadPhoto)
        {

            /// kiểm soát dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(data.Description))
                ModelState.AddModelError("Description", "nội dung không được để trống");


            if (uploadPhoto != null)
            {
                // lưu file vào đường dẫn vật lý, đường dẫn ảo
                string path = Server.MapPath("~/Photo");
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(path, fileName);// phép cộng chuỗi
                uploadPhoto.SaveAs(filePath);
                data.Photo = fileName;
                
            }
            else if (data.Photo == null)
            {
                ModelState.AddModelError("Photo", "Ảnh không được để trống");
            }

            if (!ModelState.IsValid)
            {
                if (data.PhotoID > 0)
                    ViewBag.Title = "Cập nhật ảnh";

                else
                    ViewBag.Title = "Bổ sung ảnh";

                return View("Photo", data);

            }

            if (data.PhotoID > 0)

                ProductDataService.UpdatePhoto(data);
            else

                ProductDataService.AddPhoto(data);

            return RedirectToAction("edit/" + data.ProductID);

          
        }

        /// <summary>
        /// Các chức năng quản lý thuộc tính của mặt hàng
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        [Route("attribute/{method?}/{productID}/{attributeID?}")]
        public ActionResult Attribute(string method, int productID, int? attributeID)
        {
            ProductAttribute model = new ProductAttribute();
            int id = 0;

            switch (method)
            {
                case "add":
                    ViewBag.Title = "Bổ sung thuộc tính";

                    model.AttributeID = 0;

                    try
                    {
                        id = Convert.ToInt32(productID);
                    }
                    catch
                    {
                        return RedirectToAction("Index");
                    }

                    model.ProductID = id;

                    break;
                case "edit":

                    try
                    {
                        id = Convert.ToInt32(attributeID);
                    }
                    catch
                    {
                        return RedirectToAction("Index");
                    }

                    model = ProductDataService.GetAttribute(id);

                    ViewBag.Title = "Thay đổi thuộc tính";

                    break;

                case "delete":

                    try
                    {
                        id = Convert.ToInt32(attributeID);
                    }
                    catch
                    {
                        return RedirectToAction("Index");
                    }

                    ProductDataService.DeleteAttribute(id);

                    return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }

            return View(model);
        }

        /// <summary>
        /// thực hiện chức năng lưu lại thuộc tính mặt hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveAttribute(ProductAttribute data)
        {
            // kiểm soát dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(data.AttributeName))
                ModelState.AddModelError("ProductName", "Tên không được để trống");

            if (string.IsNullOrWhiteSpace(data.AttributeValue))
                ModelState.AddModelError("Unit", "đơn vị không được để trống");

            if (!ModelState.IsValid)
            {
                if (data.AttributeID > 0)
                    ViewBag.Title = "Cập nhật thông tin";
                else
                    ViewBag.Title = "Bổ sung thông tin";

                return View("Attribute", data);

            }

            if (data.AttributeID > 0)
                ProductDataService.UpdateAttribute(data);
            else
                ProductDataService.AddAttribute(data);

            return RedirectToAction("edit/" + data.ProductID);

          
           
        }
    }

}