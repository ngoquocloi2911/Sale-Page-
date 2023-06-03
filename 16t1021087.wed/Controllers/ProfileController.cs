using _16t1021087.BussinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _16t1021087.wed.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }



        /// <summary>
        /// THay đổi mật khẩu
        /// </summary>
        /// <param name="userName"> tên user</param>
        /// <param name="oldPassword"> mật khẩu cũ</param>
        /// <param name="newPassword"> mật khẩu mới</param>
        /// <param name="preNewPassword"> nhập lại mật khẩu mới</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string userName, string oldPassword, string newPassword, string preNewPassword)
        {
           
            //kiểm soát đầu vào có hợp lệ hay không
            if (string.IsNullOrWhiteSpace(oldPassword))
                ModelState.AddModelError("oldPassword", "Mật khẩu cũ không được để trống");
     
            if (string.IsNullOrWhiteSpace(newPassword))
                ModelState.AddModelError("newPassword", "Mật khẩu mới không được để trống");

            if (string.IsNullOrWhiteSpace(preNewPassword))
                ModelState.AddModelError("preNewPassword", "Vui lòng nhập lại mật khẩu");

            if (!newPassword.Equals(preNewPassword))
                ModelState.AddModelError("preNewPassword2", "Mật khẩu nhập lại không trùng");

            //Hợp lệ
              if (ModelState.IsValid)
            {
                UserAccountService.ChangePassword(AccountTypes.Employee, userName, oldPassword, newPassword);
                ViewBag.Mesage = "Đổi mật khẩu thành công!";
            }
            return View("Index");
        }
    }

}