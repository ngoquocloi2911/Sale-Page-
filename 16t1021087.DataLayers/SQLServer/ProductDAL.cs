using _16t1021087.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace _16t1021087.DataLayers.SQLServer
{
    public class ProductDAL : _BaseDAL, IProductDAL
    {/// <summary>
    /// Cài đặt các phép xử lý cho mặt hàng
    /// </summary>
    /// <param name="connectionString"></param>
        public ProductDAL(string connectionString) : base(connectionString)
        {
        }
        /// <summary>
        /// Thêm một mặt hàng vào csdl
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Product data)
        {
            int result = 0;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Products(ProductName, Unit, Price,Photo,SupplierID,CategoryID)
                                    VALUES(@ProductName, @Unit,@Price,@Photo,@SupplierID,@CategoryID);
                                    SELECT SCOPE_IDENTITY()";
                //VALUES(@ShipperName, @Phone);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@ProductName", data.ProductName);
                cmd.Parameters.AddWithValue("@Unit", data.Unit);
                cmd.Parameters.AddWithValue("@Price", data.Price);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);
                cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);

                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }

        /// <summary>
        /// Thêm thuộc tính vào csdl
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public long AddAttribute(ProductAttribute data)
        {
            int result = 0;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO ProductAttributes(AttributeName, AttributeValue, DisplayOrder,ProductID)
                                    VALUES(@AttributeName, @AttributeValue,@DisplayOrder,@ProductID);
                                    SELECT SCOPE_IDENTITY()";
                //VALUES(@ShipperName, @Phone);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@AttributeName", data.AttributeName);
                cmd.Parameters.AddWithValue("@AttributeValue", data.AttributeValue);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);


                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }

        /// <summary>
        /// Thêm một ảnh vào csdl
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public long AddPhoto(ProductPhoto data)
        {
            int result = 0;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO ProductPhotos(ProductID,Photo, Description, DisplayOrder,IsHidden)
                                    VALUES(@ProductID,@Photo, @Description,@DisplayOrder,@IsHidden);
                                    SELECT SCOPE_IDENTITY()";
                //VALUES(@ShipperName, @Phone);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                cmd.Parameters.AddWithValue("@IsHidden", data.IsHidden);


                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }

        /// <summary>
        /// đếm số lượng mặt hàng
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="categoryID"></param>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public int Count(string searchValue = "", int categoryID = 0, int supplierID = 0)
        {
            int count = 0;

            if (searchValue != "")
                searchValue = "%" + searchValue + "%";

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT  COUNT(*)
                                    FROM    Products as p
                                    WHERE   ((p.ProductName like @searchValue) or (@searchValue = N''))
					                                    and 
					                                    ((p.CategoryID = @categoryID) or (@categoryID = 0))
					                                    and
					                                    ((p.SupplierID = @supplierID) or (@supplierID = 0))";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@SearchValue", searchValue);
                cmd.Parameters.AddWithValue("@categoryID", categoryID);
                cmd.Parameters.AddWithValue("@supplierID", supplierID);
                count = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }
            return count;
        }

        /// <summary>
        /// Xóa một mặt hàng cụ thể
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public bool Delete(int productID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM Products 
                                    WHERE ProductID = @ProductID" ;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@ProductID", productID);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }

        /// <summary>
        /// Xóa thuộc tính mặt hàng đó
        /// </summary>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        public bool DeleteAttribute(long attributeID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM ProductAttributes 
                                    WHERE AttributeID = @AttributeID ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@AttributeID", attributeID);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }

        /// <summary>
        /// Xóa ảnh mặt hàng
        /// </summary>
        /// <param name="photoID"></param>
        /// <returns></returns>
        public bool DeletePhoto(long productID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM ProductPhotos 
                                    WHERE PhotoID = @PhotoID ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@PhotoID", productID);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }

        /// <summary>
        /// Lấy dữ liệu loại hàng từ mã loại hàng
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public Product GetProduct(long productID)
        {

            Product data = null;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM Products WHERE ProductID = @ProductID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@ProductID", productID);
                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    data = new Product()
                    {
                        ProductID = Convert.ToInt32(dbReader["ProductID"]),
                        ProductName = Convert.ToString(dbReader["ProductName"]),
                        SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                        CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                        Unit = Convert.ToString(dbReader["Unit"]),
                        Price = Convert.ToDecimal(dbReader["Price"]),
                        Photo = Convert.ToString(dbReader["Photo"])
                        
                    };
                }
                cn.Close();
            }
            return data;
        }

        /// <summary>
        /// Lấy dữ liệu thuộc tính từ mã thuộc tính
        /// </summary>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        public ProductAttribute GetAttribute(long attributeID)
        {
            ProductAttribute data = null;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM ProductAttributes WHERE AttributeID = @AttributeID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@AttributeID", attributeID);
                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    data = new ProductAttribute()
                    {
                        AttributeID = Convert.ToInt32(dbReader["AttributeID"]),
                        ProductID = Convert.ToInt32(dbReader["ProductID"]),
                        AttributeName = Convert.ToString(dbReader["AttributeName"]),
                        DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"]),
                        AttributeValue = Convert.ToString(dbReader["AttributeValue"])

                    };
                }
                cn.Close();
            }
            return data;
        }

        /// <summary>
        /// Lấy dữ liệu ảnh từ mã ảnh
        /// </summary>
        /// <param name="photoID"></param>
        /// <returns></returns>
        public ProductPhoto GetPhoto(long photoID)
        {
            ProductPhoto data = null;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM ProductPhotos WHERE PhotoID = @PhotoID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@PhotoID", photoID);
                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    data = new ProductPhoto()
                    {
                        PhotoID = Convert.ToInt32(dbReader["PhotoID"]),
                        ProductID = Convert.ToInt32(dbReader["ProductID"]),
                        Photo = Convert.ToString(dbReader["Photo"]),
                        DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"]),
                        Description = Convert.ToString(dbReader["Description"]),
                        IsHidden = Convert.ToBoolean(dbReader["IsHidden"])

                    };
                }
                cn.Close();
            }
            return data;
        }



        /// <summary>
        /// Kiểm tra xem sản phẩm đó có tồn tại không
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public bool InUsed(int productID)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT CASE WHEN (EXISTS (SELECT * FROM OrderDetails
                                    WHERE ProductID = @productID)or EXISTS(SELECT * FROM ProductAttributes
                                    WHERE ProductID = @productID)or EXISTS(SELECT * FROM ProductPhotos
                                    WHERE ProductID = @productID)) THEN 1 
                                    ELSE 0 END";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@ProductID", productID);

                result = Convert.ToBoolean(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }

        /// <summary>
        /// Lấy danh sách mặt hàng theo từ khóa tìm kiếm , có phân trang ( nếu pageSize = 0 thì không phân trang)
        /// </summary>
        /// <param name="page"> số trang</param>
        /// <param name="pageSize"> số dòng trên một trang</param>
        /// <param name="searchValue"> giá trị tìm kiếm</param>
        /// <param name="categoryID"> mã loại hàng</param>
        /// <param name="supplierID"> mã nhà cung cấp</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IList<Product> List(int page = 1, int pageSize = 0, string searchValue = "", int categoryID = 0, int supplierID = 0)
        {
            List<Product> data = new List<Product>();

            if (searchValue != "")
                searchValue = "%" + searchValue + "%";

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select *
                                    from
                                        (
                                            select *,
                                                    row_number() over (order by ProductName) as RowNumber
                                            from Products as p
		                                    where 
                                                        ((p.ProductName like @searchValue) or (@searchValue = N''))
					                                    and 
					                                    ((p.CategoryID = @categoryID) or (@categoryID = 0))
					                                    and
					                                    ((p.SupplierID = @supplierID) or (@supplierID = 0))
                
        
                                        ) as t join Categories as c on t.CategoryID = c.CategoryID
			                                    join Suppliers as s on t.SupplierID = s.SupplierID
                                    where (@pageSize = 0) or (t.RowNumber between (@page -1) *@pageSize + 1 and @page *@pageSize)
                                    order by t.RowNumber";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@Page", page);
                cmd.Parameters.AddWithValue("@PageSize", pageSize);
                cmd.Parameters.AddWithValue("@SearchValue", searchValue);
                cmd.Parameters.AddWithValue("@categoryID", categoryID);
                cmd.Parameters.AddWithValue("@supplierID", supplierID);

                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dbReader.Read())
                {
                    data.Add(new Product()
                    {
                        ProductID = Convert.ToInt32(dbReader["ProductID"]),
                        ProductName = Convert.ToString(dbReader["ProductName"]),
                        SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                        CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                        Unit = Convert.ToString(dbReader["Unit"]),
                        Price = Convert.ToDecimal(dbReader["Price"]),
                        Photo = Convert.ToString(dbReader["Photo"]),

                    });
                }
                dbReader.Close();
                cn.Close();
            }

            return data;
        }

        /// <summary>
        /// lây dang sách thuộc tính theo từ khóa tìm kiếm 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IList<ProductAttribute> ListAttributes(int productID)
        {
            List<ProductAttribute> data = new List<ProductAttribute>();

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT    *, ROW_NUMBER() OVER (ORDER BY DisplayOrder) AS RowNumber
                                        FROM    ProductAttributes
                                        WHERE   ProductID = @productID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@productID", productID);

                var result = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (result.Read())
                {
                    data.Add(new ProductAttribute()
                    {
                        AttributeID = Convert.ToInt32(result["AttributeID"]),
                        ProductID = Convert.ToInt32(result["ProductID"]),
                        AttributeName = Convert.ToString(result["AttributeName"]),
                        AttributeValue = Convert.ToString(result["AttributeValue"]),
                        DisplayOrder = Convert.ToInt32(result["DisplayOrder"])
                    });
                }
                result.Close();
                cn.Close();
            }
            return data;
        }

        /// <summary>
        /// Lấy danh sách ảnh theo từ khóa tìm kiếm
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IList<ProductPhoto> ListPhotos(int productID)
        {
            List<ProductPhoto> data = new List<ProductPhoto>();

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT    *, ROW_NUMBER() OVER (ORDER BY DisplayOrder) AS RowNumber
                                        FROM    ProductPhotos
                                        WHERE   ProductID = @productID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@productID", productID);

                var result = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (result.Read())
                {
                    data.Add(new ProductPhoto()
                    {
                        PhotoID = Convert.ToInt32(result["PhotoID"]),
                        ProductID = Convert.ToInt32(result["ProductID"]),
                        Photo = Convert.ToString(result["Photo"]),
                        Description = Convert.ToString(result["Description"]),
                        DisplayOrder = Convert.ToInt32(result["DisplayOrder"]),
                        IsHidden = Convert.ToBoolean(result["IsHidden"])
                    });
                }
                result.Close();
                cn.Close();
            }
            return data;
        }

        /// <summary>
        /// Cập nhật một mặt hàng mới
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(Product data)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                
                    cmd.CommandText = @"UPDATE Products
                                    SET ProductName = @ProductName, Unit = @Unit, Price = @Price, Photo = @Photo, SupplierID = @SupplierID,
                                    CategoryID = @CategoryID
                                    WHERE ProductID = @ProductID";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;
                    cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                    cmd.Parameters.AddWithValue("@ProductName", data.ProductName);
                    cmd.Parameters.AddWithValue("@Unit", data.Unit);
                    cmd.Parameters.AddWithValue("@Price", data.Price);
                    cmd.Parameters.AddWithValue("@Photo", data.Photo);
                    cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                    cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);


                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }

        /// <summary>
        /// Cập nhật một thuộc tính mới
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool UpdateAttribute(ProductAttribute data)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = @"UPDATE ProductAttributes
                                    SET AttributeName = @AttributeName, AttributeValue = @AttributeValue, DisplayOrder = @DisplayOrder
                                    WHERE AttributeID = @AttributeID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@AttributeID", data.AttributeID);
                cmd.Parameters.AddWithValue("@AttributeName", data.AttributeName);
                cmd.Parameters.AddWithValue("@AttributeValue", data.AttributeValue);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);




                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;


        }
        

        /// <summary>
        /// cập nhật ảnh mới
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
            public bool UpdatePhoto(ProductPhoto data)
        {

            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
               
                    cmd.CommandText = @"UPDATE ProductPhotos
                                    SET Photo = @Photo, Description = @Description, DisplayOrder = @DisplayOrder, IsHidden = @IsHidden
                                    WHERE PhotoID = @PhotoID";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = cn;
                    cmd.Parameters.AddWithValue("@PhotoID", data.PhotoID);
                    cmd.Parameters.AddWithValue("@Description", data.Description);
                    cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                    cmd.Parameters.AddWithValue("@IsHidden", data.IsHidden);
                    cmd.Parameters.AddWithValue("@Photo", data.Photo);



                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }

    }
}
