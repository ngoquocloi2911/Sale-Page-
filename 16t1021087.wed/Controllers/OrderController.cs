using _16t1021087.BussinessLayers;
using _16t1021087.DomainModels;
using _16t1021087.wed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace _16t1021087.wed.Controllers
{
    /// <summary>
    ///Routing Table (RT) ( Mô tả cấu trúc của URL) 1 bảng lưu trữ danh sách sử dụng 
    /// cho URL(Có thể đảo Edit/Supplier/18, RT phải định nghĩa)
    /// </summary>
    [Authorize]
    [RoutePrefix("Order")]
    public class OrderController : Controller
    {
        private const string SHOPPING_CART = "ShoppingCart";
        private const string ERROR_MESSAGE = "ErrorMessage";
        private const int PAGE_SIZE = 10;

        /// <summary>
        /// Tìm kiếm, phân trang cho đơn hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //TODO: Code chức năng tìm kiếm, phân trang cho đơn hàng
              OrderSeachInput condition = Session[SHOPPING_CART] as OrderSeachInput;

            if (condition == null)
            {
                condition = new OrderSeachInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = "",
                    Status = -99
                };
            }

            return View(condition);

            
        }

        /// <summary>
        /// Thực hiện chức năng tìm kiếm
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ActionResult Search(OrderSeachInput condition)
        {
           
                int rowCount = 0;
                var data = OrderDataService.ListOrders(condition.Page,
                                                            condition.PageSize,
                                                            condition.Status,
                                                            condition.SearchValue,
                                                            out rowCount);
                var result = new OrderSearchOutput()
                {
                    Page = condition.Page,
                    PageSize = condition.PageSize,
                    SearchValue = condition.SearchValue,
                    RowCount = rowCount,
                    Data = data
                };
                Session[SHOPPING_CART] = condition;
                return View(result);
            

        }
        /// <summary>
        /// Xem thông tin và chi tiết của đơn hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id = 0)
        {
            //TODO: Code chức năng lấy và hiển thị thông tin của đơn hàng và chi tiết của đơn hàng
            if (id <= 0) 
                return View("index");

            var data = OrderDataService.GetOrder(id);
            if (data == null) 
                return View("index");

            var orderDetail = new OderDetailModel()
            {
                Data = data,
                OrderDetail = OrderDataService.ListOrderDetails(id)
             };

            var employeeID = Converter.CookieToUserAccount(User.Identity.Name).UserId;

            if (Convert.ToInt32(employeeID) == data.EmployeeID)
            {
                ViewBag.isAuthor = true;
            }
            else ViewBag.isAuthor = false;


            return View(orderDetail);
        }
        /// <summary>
        /// Giao diện Thay đổi thông tin chi tiết đơn hàng
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        [Route("EditDetail/{orderID}/{productID}")]
        public ActionResult EditDetail(int orderID = 0, int productID = 0)
        {
            //TODO: Code chức năng để lấy chi tiết đơn hàng cần edit
            var data = OrderDataService.GetOrderDetail(orderID, productID);
            if (data == null)
            {
                return RedirectToAction($"Details/{orderID}");
            }
            return View(data);
        }
        /// <summary>
        /// Thay đổi thông tin chi tiết đơn hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpdateDetail(OrderDetail data)
        {
            //TODO: Code chức năng để cập nhật chi tiết đơn hàng

            //Kiểm tra dữ liệu đầu vào
            if (data.SalePrice <= 0)
                return "Giá bán không được để trống";
            if (data.Quantity <= 0)
                return "Số lượng không được để trống";

            // hàm SaveOrderDetail xài chung giữa update và add
            OrderDataService.SaveOrderDetail(data.OrderID, data.ProductID, data.Quantity, data.SalePrice);

            return "";
        }
        /// <summary>
        /// Xóa 1 chi tiết trong đơn hàng
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        [Route("DeleteDetail/{orderID}/{productID}")]
        public ActionResult DeleteDetail(int orderID = 0, int productID = 0)
        {
            //TODO: Code chức năng xóa 1 chi tiết trong đơn hàng

            OrderDataService.DeleteOrderDetail(orderID, productID);

            return RedirectToAction($"Details/{orderID}");
        }
        /// <summary>
        /// Xóa đơn hàng nếu được phép 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id = 0)
        {
            //TODO: Code chức năng để xóa đơn hàng (nếu được phép xóa)

            OrderDataService.DeleteOrder(id);

            return RedirectToAction("Index");
        }
        /// <summary>
        /// Chấp nhận đơn hàng nếu được phép
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Accept(int id = 0)
        {
            //TODO: Code chức năng chấp nhận đơn hàng (nếu được phép)

            var order = OrderDataService.GetOrder(id);
            var employeeID = Converter.CookieToUserAccount(User.Identity.Name).UserId;

            if (Convert.ToInt32(employeeID) == order.EmployeeID && order.Status == 1)
            {
                OrderDataService.AcceptOrder(id);
            }
            return RedirectToAction($"Details/{id}");
        }
        /// <summary>
        /// Xác nhận chuyển đơn hàng cho người giao hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Shipping(int id = 0, int shipperID = 0)
        {
            //TODO: Code chức năng chuyển đơn hàng sang trạng thái đang giao hàng (nếu được phép)

            var order = OrderDataService.GetOrder(id);
            return View(order);
           

        }
        /// <summary>
        /// cập nhật thông tin giao hàng
        /// chuyển trạng thái từ đơn hàng sang giao hàng nếu là nhân viên phụ trách
        /// sử dụng kiểu khai báo string có thể return về 1 chuỗi 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public string UpdateShipping(int id = 0, int shipperID = 0)
        {
            if (id <= 0) return "Nhân viên giao hàng không được để trống";
            if (shipperID <= 0) return "Nhân viên phụ trách không được để trống";

            var order = OrderDataService.GetOrder(id);
            var employeeID = Converter.CookieToUserAccount(User.Identity.Name).UserId;

            if (Convert.ToInt32(employeeID) == order.EmployeeID && order.Status == 2)
            {
                OrderDataService.ShipOrder(id, shipperID);
            }
            return "";
        }
        /// <summary>
        /// Ghi nhận hoàn tất thành công đơn hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Finish(int id = 0)
        {
            //TODO: Code chức năng ghi nhận hoàn tất đơn hàng (nếu được phép)
            var order = OrderDataService.GetOrder(id);
            var employeeID = Converter.CookieToUserAccount(User.Identity.Name).UserId;

            if(Convert.ToInt32(employeeID) == order.EmployeeID && order.Status == 3)
            {
                OrderDataService.FinishOrder(id);
            }    

            return RedirectToAction($"Details/{id}");
        }
        /// <summary>
        /// Hủy bỏ đơn hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Cancel(int id = 0)
        {
            //TODO: Code chức năng hủy đơn hàng (nếu được phép)
            var order = OrderDataService.GetOrder(id);
            var employeeID = Converter.CookieToUserAccount(User.Identity.Name).UserId;

            if(Convert.ToInt32(employeeID) == order.EmployeeID && order.Status != 4)
            {
                OrderDataService.CancelOrder(id);
            }    

            return RedirectToAction($"Details/{id}");
        }
        /// <summary>
        /// Từ chối đơn hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Reject(int id = 0)
        {
            //TODO: Code chức năng từ chối đơn hàng (nếu được phép)
            var order = OrderDataService.GetOrder(id);
            var employeeID = Converter.CookieToUserAccount(User.Identity.Name).UserId;

            if(Convert.ToInt32(employeeID) == order.EmployeeID && order.Status == 1)
            {
                OrderDataService.RejectOrder(id);
            }    

            return RedirectToAction($"Details/{id}");
        }

        /// <summary>
        /// Sử dụng 1 biến session để lưu tạm giỏ hàng (danh sách các chi tiết của đơn hàng) trong quá trình xử lý.
        /// Hàm này lấy giỏ hàng hiện đang có trong session (nếu chưa có thì tạo mới giỏ hàng rỗng)
        /// </summary>
        /// <returns></returns>
        private List<OrderDetail> GetShoppingCart()
        {
            List<OrderDetail> shoppingCart = Session[SHOPPING_CART] as List<OrderDetail>;

            if (shoppingCart == null)
            {
                shoppingCart = new List<OrderDetail>();
                Session[SHOPPING_CART] = shoppingCart;
            }
            return shoppingCart;
        }
        /// <summary>
        /// Giao diện lập đơn hàng mới
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.ErrorMessage = TempData[ERROR_MESSAGE] ?? "";
            return View(GetShoppingCart());
        }
        /// <summary>
        /// Tìm kiếm mặt hàng để bổ sung vào giỏ hàng
        /// </summary>
        /// <param name="page"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public ActionResult SearchProducts(int page = 1, string searchValue = "")
        {
            OrderSeachInput condition = Session[SHOPPING_CART] as OrderSeachInput;

            int rowCount = 0;
            var data = ProductDataService.ListProducts(page, PAGE_SIZE, searchValue, 0, 0, out rowCount);
            ViewBag.Page = page;

            return View(data);
        }
        /// <summary>
        /// Bổ sung thêm hàng vào giỏ hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddToCart(OrderDetail data)
        {
            if (data == null)
            {
                TempData[ERROR_MESSAGE] = "Dữ liệu không hợp lệ";
                return RedirectToAction("Create");
            }
            if (data.SalePrice <= 0 || data.Quantity <= 0)
            {
                TempData[ERROR_MESSAGE] = "Giá bán và số lượng không hợp lệ";
                return RedirectToAction("Create");
            }

            List<OrderDetail> shoppingCart = GetShoppingCart();
            var existsProduct = shoppingCart.FirstOrDefault(m => m.ProductID == data.ProductID);

            if (existsProduct == null) //Nếu mặt hàng cần được bổ sung chưa có trong giỏ hàng thì bổ sung vào giỏ
            {

                shoppingCart.Add(data);
            }
            else //Trường hợp mặt hàng cần bổ sung đã có thì tăng số lượng và thay đổi đơn giá
            {
                existsProduct.Quantity += data.Quantity;
                existsProduct.SalePrice = data.SalePrice;
            }
            Session[SHOPPING_CART] = shoppingCart;
            return RedirectToAction("Create");
        }
        /// <summary>
        /// Xóa 1 mặt hàng khỏi giỏ hàng
        /// </summary>
        /// <param name="id">Mã mặt hàng</param>
        /// <returns></returns>
        public ActionResult RemoveFromCart(int id = 0)
        {
            List<OrderDetail> shoppingCart = GetShoppingCart();
            int index = shoppingCart.FindIndex(m => m.ProductID == id);

            if (index >= 0)

                shoppingCart.RemoveAt(index);
            Session[SHOPPING_CART] = shoppingCart;

            return RedirectToAction("Create");
        }
        /// <summary>
        /// Xóa toàn bộ giỏ hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult ClearCart()
        {
            List<OrderDetail> shoppingCart = GetShoppingCart();
            shoppingCart.Clear();
            Session[SHOPPING_CART] = shoppingCart;
            return RedirectToAction("Create");
        }
        /// <summary>
        /// Khởi tạo đơn hàng (với phần thông tin chi tiết của đơn hàng là giỏ hàng đang có trong session)
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Init(int customerID = 0, int employeeID = 0)
        {
            List<OrderDetail> shoppingCart = GetShoppingCart();
            if (shoppingCart == null || shoppingCart.Count == 0)
            {
                TempData[ERROR_MESSAGE] = "Không thể tạo đơn hàng với giỏ hàng trống";
                return RedirectToAction("Create");
            }

            if (customerID == 0 || employeeID == 0)
            {
                TempData[ERROR_MESSAGE] = "Vui lòng chọn khách hàng và nhân viên phụ trách";
                return RedirectToAction("Create");
            }

            int orderID = OrderDataService.InitOrder(customerID, employeeID, DateTime.Now, shoppingCart);

            Session.Remove(SHOPPING_CART); //Xóa giỏ hàng 

            return RedirectToAction($"Details/{orderID}");
        }
    }
}