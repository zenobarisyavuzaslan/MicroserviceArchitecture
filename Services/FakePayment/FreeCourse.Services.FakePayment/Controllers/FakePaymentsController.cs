using FreeCourse.Services.FakePayment.Models;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Dtos;
using FreeCourse.Shared.Messages;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentsController : CustomBaseController
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        public FakePaymentsController(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }
        [HttpPost]
        public async Task<IActionResult> ReceivePayment(PaymentDto paymentDto)
        {
            //Ödeme İşlemi gerçekleşir
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-order-service"));

            var createOrderMessageCommand = new CreateOrderMessageCommand();
            createOrderMessageCommand.BuyerId = paymentDto.Order.BuyerId;
            createOrderMessageCommand.Province = paymentDto.Order.Address.Province;
            createOrderMessageCommand.Street = paymentDto.Order.Address.Street;
            createOrderMessageCommand.ZipCode = paymentDto.Order.Address.ZipCode;
            createOrderMessageCommand.District = paymentDto.Order.Address.District;
            createOrderMessageCommand.Line = paymentDto.Order.Address.Line;
            var orderItems = new List<OrderItem>();
            paymentDto.Order.OrderItems.ForEach(x =>
            {
                var newOrderItem = new OrderItem();
                newOrderItem.PictureUrl = x.PictureUrl;
                newOrderItem.Price = x.Price;
                newOrderItem.ProductId = x.ProductId;
                newOrderItem.ProductName = x.ProductName;
                orderItems.Add(newOrderItem);

            });
            createOrderMessageCommand.OrderItems = orderItems;

            await sendEndpoint.Send<CreateOrderMessageCommand>(createOrderMessageCommand);

            return CreateActionResultInstance<NoContent>(Shared.Dtos.Response<NoContent>.Success(200));
        }
    }
}
