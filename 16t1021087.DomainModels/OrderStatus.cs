using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16t1021087.DomainModels
{
    /// <summary>
    /// Định nghĩa các hằng trạng thái đơn hàng
    /// </summary>
    public class OrderStatus
    {
        /// <summary>
        /// Đơn hàng vừa được khởi tạo (đang chờ duyệt)
        /// </summary>
        public const int INIT = 1;
        /// <summary>
        /// Đơn hàng đã được duyệt (đang chờ chuyển hàng)
        /// </summary>
        public const int ACCEPTED = 2;
        /// <summary>
        /// Đơn hàng đang được chuyển hàng
        /// </summary>
        public const int SHIPPING = 3;
        /// <summary>
        /// Đơn hàng đã hoàn tất
        /// </summary>
        public const int FINISHED = 4;
        /// <summary>
        /// Đơn hàng bị hủy
        /// </summary>
        public const int CANCEL = -1;
        /// <summary>
        /// Đơn hàng bị từ chối
        /// </summary>
        public const int REJECTED = -2;

        public int Status { get; set; }

        public string Description
        {
            get
            {
                switch (Status)
                {
                    case OrderStatus.INIT:
                        return "Đơn hàng mới. Đang chờ duyệt";
                    case OrderStatus.ACCEPTED:
                        return "Đơn đã chấp nhận. Đang chờ chuyển hàng";
                    case OrderStatus.SHIPPING:
                        return "Đơn hàng đang được giao";
                    case OrderStatus.FINISHED:
                        return "Đơn hàng đã hoàn tất";
                    case OrderStatus.CANCEL:
                        return "Đơn hàng đã bị hủy";
                    case OrderStatus.REJECTED:
                        return "Đơn hàng bị từ chối";
                    default:
                        return "";
                }
            }
        }
    }
}
