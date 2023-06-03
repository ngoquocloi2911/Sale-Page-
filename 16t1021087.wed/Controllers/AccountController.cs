 using _16t1021087.BussinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _16t1021087.DomainModels;
using System.Web.Security;

namespace _16t1021087.wed.Controllers
{
    /// <summary>
    /// Hạn chế quyền truy cập của người dùng bằng thuộc tính authorize
    // dùng thuộc tính authorize buộc người dùng phải đăng nhập để xác minh danh tính của họ
    /// </summary>
    [Authorize]
    public class AccountController : Controller
    {
        /// <summary>
        /// Trang login
        /// </summary>
        /// <returns></returns>
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Thuộc tính AllowAnonumouns cho phép người dùng có thể vào trang mà không cần đăng nhập
        /// Vì đây là trang đăng nhập mà :))
        /// Get sử dụng để lấy dữ liệu, dữ liệu được mã hóa vào url
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// validateantiforgerytoken ngăn chặn các cuộc tấn công giả mạo được yêu cầu trên nhiều trang wed
        /// Thêm mã @Html.AntiForgeryToken() để xác thực biểu mẫu khi gửi
        /// post được sử dụng để lưu trữ và cập nhập dữ liệu, dữ liệu được nối vào phần body
        /// Hàm truyền giá trị, kiểm tra đầu vào của trang login
        /// </summary>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]  
        
        public ActionResult Login( string userName = "", string password = "")
        {
            
            // Kiểm tra giá trị đầu vào
            ViewBag.UserName = userName;
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password) ) 
            { 
                
                ModelState.AddModelError("", "Vui lòng nhập đủ thông tin");
                return View();
            }

            var userAccount = UserAccountService.Authorize(AccountTypes.Employee, userName, password);
            if (userAccount == null)
            {
                ModelState.AddModelError("", "Đăng nhập thất bại");
                return View();
            }

            string cookieValue = Newtonsoft.Json.JsonConvert.SerializeObject(userAccount);
            FormsAuthentication.SetAuthCookie(cookieValue, false);
            return RedirectToAction("Index", "Home");
            
        }

        /// <summary>
        /// Đăng xuất
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }

       
      
        
            
        }

        
   
}