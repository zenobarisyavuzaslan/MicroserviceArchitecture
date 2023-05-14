using FreeCourse.Web.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IOrderService
    {
        /// <summary>
        /// Senkron İletişim-direk olarak order microservisine istek yapılacak
        /// </summary>
        /// <param name="checkoutInfoInput"></param>
        /// <returns></returns>
        Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput);
        /// <summary>
        /// Askenron iletişim-sipariş bilgileri rabbitmq ya gönderilecek
        /// </summary>
        /// <param name="checkoutInfoInput"></param>
        /// <returns></returns>
        Task<OrderSuspendViewModel> SuspendOrder(CheckoutInfoInput checkoutInfoInput);
        Task<List<OrderViewModel>> GetOrder();
    }
}
