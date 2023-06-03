using _16t1021087.DataLayers;
using _16t1021087.DomainModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16t1021087.BussinessLayers
{
    /// <summary>
    /// 
    /// </summary>
    public class CommonDataService
    {
        private static  ICountryDAL countryBD;
        private static ICommonDAL<Supplier> supplierDB;
        private static ICommonDAL<Shipper> shipperDB;
        private static ICommonDAL<Customer> customerDB;
        private static ICommonDAL<Employye> employyeDB;
        private static ICommonDAL<Category> categoryDB;
        static CommonDataService()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            countryBD = new DataLayers.SQLServer.CountryDAL(connectionString);
            supplierDB = new DataLayers.SQLServer.SupplierDAL(connectionString);
            shipperDB = new DataLayers.SQLServer.ShipperDAL(connectionString);
            customerDB = new DataLayers.SQLServer.CustomerDAL(connectionString);
            employyeDB = new DataLayers.SQLServer.EmployyeDAL(connectionString);
            categoryDB = new DataLayers.SQLServer.CategoryDAL(connectionString);
        }
        #region các nghiệp vụ lq đến qốc gia
        /// <summary>
        /// Lấy danh sách cacs quốc gia
        /// </summary>
        /// <returns></returns>
        public static List<Country> ListOfCountries()
        {
            return countryBD.List().ToList();
        }
        #endregion

        #region các nghiệp vụ lq đến nhà cung cấp
        /// <summary>
        /// Tìm kiếm, lấy danh sách các nhà cung cấp dưới dạng phân trangg
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pagesize">Số dòng trên mỗi trang</param>
        /// <param name="searchValue">Gía trị tìm kiếm</param>
        /// <param name="rowCount"> Output: Tổng số dòng tìm được</param>
        /// <returns></returns>
        public static List<Supplier> ListOfSuppliers(int page, int pagesize, string searchValue, out int rowCount)
        {
            rowCount = supplierDB.Count(searchValue);
            return supplierDB.List(page, pagesize, searchValue).ToList();
        }

        /// <summary>
        /// Tìm kiếm, lấy danh sách các nhà cung cấp dưới dạng không phân trangg
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Supplier> ListOfSuppliers(string searchValue)
        {
            return supplierDB.List(1, 0, searchValue).ToList();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Supplier> ListOfSuppliers()
        {
            return supplierDB.List(1, 0, "").ToList();
        }

        /// <summary>
        /// Bổ sung nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns>mã của nhà cung cấp</returns>
        public static int AddSupplier(Supplier data)
        {
            return supplierDB.Add(data);

        }
        /// <summary>
        /// Cập nhật nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateSupplier(Supplier data)
        {
            return supplierDB.Update(data);

        }
        /// <summary>
        /// Xóa nhà cung cấp
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool DeleteSupplier(int supplierID)
        {
            return supplierDB.Delete(supplierID);

        }
        /// <summary>
        /// Lấy thông tin của 1 nhà cc
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static Supplier GetSupplier(int supplierID)
        {
            return supplierDB.Get(supplierID);

        }
        /// <summary>
        /// Kiểm tra xem 1 ncc hiện có dữ liệu lq không?
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public static bool InUsedSupplier(int supplierID)
        {
            return supplierDB.InUsed(supplierID);

        }
        #endregion

        #region các nghiệp vụ liên quan đến khách hàng
        /// <summary>
        /// Tìm kiếm, lấy danh sách các khách hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">Số dòng trên một trang (bằng 0 thì không phân trang)</param>
        /// <param name="searchValue">Giá trị tìm kiếm (chuỗi rỗng thì ko tìm kiếm)</param>
        /// <param name="rowCount">Out: tổng số dòng tìm được</param>
        /// <returns></returns>
        public static List<Customer> ListOfCustomers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = customerDB.Count(searchValue);
            return customerDB.List(page, pageSize, searchValue).ToList();
        }

       
        /// <summary>
        /// Tìm kiếm, lấy danh sách không phân trang
        /// </summary>
        /// <param name="searchValue">Giá trị tìm kiếm (rỗng nếu không tìm)</param>
        /// <returns></returns>
        public static List<Customer> ListOfCustomers(String searchValue = "")
        {
            return customerDB.List(1, 0, searchValue).ToList();
        }
        /// <summary>
        /// Thêm một khách hàng vào CSDL
        /// </summary>
        /// <param name="data">Mã khách hàng</param>
        /// <returns></returns>
        public static int AddCustomers(Customer data)
        {
            return customerDB.Add(data);
        }
        /// <summary>
        /// Cập nhật dữ liệu của một khách hàng
        /// </summary>
        /// <param name="data">Mã khách hàng</param>
        /// <returns></returns>
        public static bool UpdateCustomers(Customer data)
        {
            return customerDB.Update(data);
        }
        /// <summary>
        /// Xóa một khách hàng không tham gia ở bảng khác ra khỏi CSDL
        /// </summary>
        /// <param name="employeeID">Mã khách hàng</param>
        /// <returns></returns>
        public static bool DeleteCustomers(int employeeID)
        {
            return customerDB.Delete(employeeID);
        }
        /// <summary>
        /// Lấy dữ liệu của một khách hàng
        /// </summary>
        /// <param name="employeeID">Mã khách hàng</param>
        /// <returns></returns>
        public static Customer GetCustomers(int employeeID)
        {
            return customerDB.Get(employeeID);
        }
        /// <summary>
        /// Kiểm tra xem 1 khách hàng hiện có dữ liệu liên quan hay không 
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public static bool InUsedCustomer(int employeeID)
        {
            return customerDB.InUsed(employeeID);
        }

        #endregion

        #region Các nghiệp vụ liên quan đến người giao hàng

        /// <summary>
        /// Tìm kiếm, lấy danh sách người giao hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page"> Trang cần xem</param>
        /// <param name="pageSize"> Số dòng trên một trang</param>
        /// <param name="searchValue"> Giá trị tìm kiếm</param>
        /// <param name="rowCount"> Tổng số dòng tìm được</param>
        /// <returns></returns>
        public static List<Shipper> ListOfShippers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = shipperDB.Count(searchValue);
            return shipperDB.List(page, pageSize, searchValue).ToList();
        }

        /// <summary>
        /// Tìm kiếm, lấy danh sách người giao hàng dưới dạng không phân trang
        /// </summary>
        /// <param name="searchValue">Giá trị tìm kiếm ( chuỗi rỗng nếu không tìm)</param>
        /// <returns></returns>
        public static List<Shipper> ListOfShippers(String searchValue="")
        {
            return shipperDB.List(1, 0, searchValue).ToList();
        }

        /// <summary>
        /// Thêm một người giao hàng vào csdl
        /// </summary>
        /// <param name="data"> Mã người giao hàng</param>
        /// <returns></returns>
        public static int AddShippers(Shipper data)
        {
            return shipperDB.Add(data);
        }

        /// <summary>
        /// Cập nhật dữ liệu của một người giao hàng
        /// </summary>
        /// <param name="data"> Mã người giao hàng</param>
        /// <returns></returns>
        public static bool UpdateShippers(Shipper data)
        {
            return shipperDB.Update(data);
        }

        /// <summary>
        /// Xóa một người giao hàng không tham gia ở bảng khác ra khỏi csdl
        /// </summary>
        /// <param name="shipperID"> Mã người giao hàng</param>
        /// <returns></returns>
        public static bool DeleteShippers(int shipperID)
        {
            return shipperDB.Delete(shipperID);
        }

        /// <summary>
        /// Lấy dữ liệu của một khách hàng
        /// </summary>
        /// <param name="shipperID"> Mã khách hàng</param>
        /// <returns></returns>
        public static Shipper GetShippers(int shipperID)
        {
            return shipperDB.Get(shipperID);
        }

        /// <summary>
        /// Kiểm tra xem người giao hàng có dữ liệu liên quan hay không
        /// </summary>
        /// <param name="shipperID"> Mã người giao hàng</param>
        /// <returns></returns>
        public static bool InUsedShipper(int shipperID)
        {
            return shipperDB.InUsed(shipperID);
        }


        #endregion

        #region Các nghiệp vụ liên quan đến nhân viên
        /// <summary>
        /// Tìm kiếm, lấy danh sách các nhân viên dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">Số dòng trên một trang (bằng 0 thì không phân trang)</param>
        /// <param name="searchValue">Giá trị tìm kiếm (chuỗi rỗng thì ko tìm kiếm)</param>
        /// <param name="rowCount">Out: tổng số dòng tìm được</param>
        /// <returns></returns>
        public static List<Employye> ListOfEmployees(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = employyeDB.Count(searchValue);
            return employyeDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Tìm kiếm, lấy danh sách nhân viên không phân trang
        /// </summary>
        /// <param name="searchValue">Giá trị tìm kiếm (rỗng nếu không tìm)</param>
        /// <returns></returns>
        public static List<Employye> ListOfEmployees(String searchValue="")
        {
            return employyeDB.List(1, 0, searchValue).ToList();
        }
        /// <summary>
        /// Thêm một nhân viên vào CSDL
        /// </summary>
        /// <param name="data">Mã khách hàng</param>
        /// <returns></returns>
        public static int AddEmployees(Employye data)
        {
            return employyeDB.Add(data);
        }
        /// <summary>
        /// Cập nhật dữ liệu của một nhân viên
        /// </summary>
        /// <param name="data">Mã khách hàng</param>
        /// <returns></returns>
        public static bool UpdateEmployees(Employye data)
        {
            return employyeDB.Update(data);
        }
        /// <summary>
        /// Xóa một nhân viên không tham gia ở bảng khác ra khỏi CSDL
        /// </summary>
        /// <param name="employeeID">Mã khách hàng</param>
        /// <returns></returns>
        public static bool DeleteEmployees(int employeeID)
        {
            return employyeDB.Delete(employeeID);
        }
        /// <summary>
        /// Lấy dữ liệu của một nhân viên
        /// </summary>
        /// <param name="employeeID">Mã khách hàng</param>
        /// <returns></returns>
        public static Employye GetEmployees(int employeeID)
        {
            return employyeDB.Get(employeeID);
        }
        /// <summary>
        /// Kiểm tra xem 1 khách hàng hiện có dữ liệu liên quan hay không 
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public static bool InUsedEmployees(int employeeID)
        {
            return employyeDB.InUsed(employeeID);
        }


        #endregion

        #region Các nghiệp vụ liên qua đến loại hàng

        /// <summary>
        /// Tìm kiếm, lấy danh sách loại hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page"> Trang cần xem</param>
        /// <param name="pageSize"> Số dòng trên một trang</param>
        /// <param name="searchValue"> Giá trị tìm kiếm</param>
        /// <param name="rowCount"> Tổng số dòng tìm được</param>
        /// <returns></returns>
        public static List<Category> ListOfCategories(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = categoryDB.Count(searchValue);
            return categoryDB.List(page, pageSize, searchValue).ToList();
        }

        /// <summary>
        /// Tìm kiếm, lấy danh sách loại hàng dưới dạng không phân trang
        /// </summary>
        /// <param name="searchValue">Giá trị tìm kiếm ( chuỗi rỗng nếu không tìm)</param>
        /// <returns></returns>
        public static List<Category> ListOfCategories(String searchValue)
        {
            return categoryDB.List(1, 0, searchValue).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Category> ListOfCategories()
        {
            return categoryDB.List(1, 0, "").ToList();
        }


        /// <summary>
        /// Thêm một loại hàng vào csdl
        /// </summary>
        /// <param name="data"> Mã loại hàng</param>
        /// <returns></returns>
        public static int AddCategories(Category data)
        {
            return categoryDB.Add(data);
        }

        /// <summary>
        /// Cập nhật dữ liệu của một loại hàng
        /// </summary>
        /// <param name="data"> Mã loại hàng</param>
        /// <returns></returns>
        public static bool UpdateCategories(Category data)
        {
            return categoryDB.Update(data);
        }


        /// <summary>
        /// Xóa một loại hàng không tham gia ở bảng khác ra khỏi csdl
        /// </summary>
        /// <param name="categoryID"> Mã loại hàng</param>
        /// <returns></returns>
        public static bool DeleteCategories(int categoryID)
        {
            return categoryDB.Delete(categoryID);
        }

        /// <summary>
        /// Lấy dữ liệu của một loại hàng
        /// </summary>
        /// <param name="categoryID"> Mã loại hàng</param>
        /// <returns></returns>
        public static Category GetCategories(int categoryID)
        {
            return categoryDB.Get(categoryID);
        }

        /// <summary>
        /// Kiểm tra xem loại hàng có dữ liệu liên quan hay không
        /// </summary>
        /// <param name="categoryID"> Mã loại hàng</param>
        /// <returns></returns>
        public static bool InUsedCategories(int categoryID)
        {
            return categoryDB.InUsed(categoryID);
        }


        #endregion
    }
}
