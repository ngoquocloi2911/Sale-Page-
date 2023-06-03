using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _16t1021087.BussinessLayers;
using _16t1021087.DomainModels;

namespace _16t1021087.wed
{
    /// <summary>
    /// Một lớp là static, yêu cầu các biến hàm trong đó phải là static
    /// Cung cấp một số hàm tiện ích liên quan đến SelectList
    /// hàm gọi về một option là dữ liệu các quốc gia
    /// </summary>
    public static class SelectListHelper
    {
        public static List<SelectListItem> Countries()
        {
            List<SelectListItem> list= new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "-- Chọn quốc gia --"
            }); 
            foreach( var item in CommonDataService.ListOfCountries())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.CountryName,
                    Text = item.CountryName
                });
            }    

            return list;
        }

        /// <summary>
        /// hàm gọi về là một option loại hàng
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> Categories()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Value = "", Text = "-- Chọn loại hàng --" });
            foreach (var item in CommonDataService.ListOfCategories())
            {
                list.Add(new SelectListItem()
                {
                    Value = Convert.ToString(item.CategoryID),
                    Text = item.CategoryName
                });
            }
            return list;
        }

        /// <summary>
        /// hàm gọi về là một option các nhà cung cấp
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> Suppliers()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Value = "", Text = "-- Chọn nhà cung cấp --" });
            foreach (var item in CommonDataService.ListOfSuppliers())
            {
                list.Add(new SelectListItem()
                {
                    Value = Convert.ToString(item.SupplierID),
                    Text = item.SupplierName
                });
            }
            return list;
        }

        /// <summary>
        /// Hàm gọi về là các trạng thái của mặt hàng
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> Status()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "-99",
                Text = "--Tất cả trạng thái--"
            });
            foreach (var item in OrderDataService.getListOrderStatus())
            {
                list.Add(new SelectListItem()
                {
                    Value = item.Status.ToString(),
                    Text = item.Description
                });
            }
            return list;
        }
    }
}