using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace _16t1021087.wed
{
    public class RouteConfig
    {
        /// <summary>
        /// Định nghĩa các cách để ánh xạ url (routing URL)
        /// </summary>
        /// <param name="routes"></param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //// định nghĩa routes thông qua chính Attribute của controller, action
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }// controller mặc định không có cũng được
            );
        }
    }
}
