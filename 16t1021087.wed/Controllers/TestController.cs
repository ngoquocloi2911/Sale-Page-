using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _16t1021087.wed.Controllers
{
    [Authorize]
    /*[RoutePrefix("Thu-nghiem")]*/
    public class TestController : Controller
    {
        // Định nghĩa theo kiểu mớihttp://localhost:50003/Thu-nghiem/Xin-chao/Phong/47
        /*  [Route("Xin-chao/{name}/{age?}")]*/
        // GET: Test
        /// <summary>
        /// WEd API
        /// </summary>
        /// <returns></returns>
        /* public string SayHello(string name, int age = 18)
         {
             /// trước chuỗi sử  dấu đôla $  trong chuỗi có thể sử dụng chèn giá trị vào đây
             /// Trước chuỗi sử dụng dấu @
             return $" Hello {name},{age} year old ";
         }*/
        [HttpGet]
       public ActionResult Input()
        {
            Person p = new Person();
            {

            };
            return View(p);
        }

        [HttpPost]
        public ActionResult Input(Person p) //(string Name, DateTime BirthDate, float Salary)
        {
            var data = new
            {
                Name = p.Name,
                BirthDate = string.Format("{0:dd/mm/yyyy}", p.BirthDate),
                Salary = p.Salary
            };

            return Json(p, JsonRequestBehavior.AllowGet);
        }

        public string TestDate( DateTime value)
        {
            DateTime d = value;
            return string.Format("{0:dd/MM/yyyy}", d);
        }
    }
    public class Person
    {
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }  

        public float Salary { get; set; }

    }
}