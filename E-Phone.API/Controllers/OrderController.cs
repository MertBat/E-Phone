using E_Phone.BLL.DTOs.Brand;
using E_Phone.BLL.DTOs.Order;
using E_Phone.BLL.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Phone.API.Controllers
{
    [Route("[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "User")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [ActionName("orders")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO createOrderDTO)
        {
            string tokenHeader = HttpContext.Request.Headers["Authorization"];
            string token = tokenHeader.Split(' ')[1];
            await _orderService.CreateOrderAsync(createOrderDTO, token);

            return Ok();
        }

        [HttpGet]
        [ActionName("orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            List<GetOrdersDTO> getOrdersDTOs = await _orderService.GetAllOrdersAsync();

            return Ok(getOrdersDTOs);
        }

        [HttpGet("orders/{customerId}")]
        [ActionName("customers")]
        public async Task<IActionResult> GetCustomerOrders(int customerId)
        {
            List<GetUserOrdersDTO> getUserOrdersDTOs = await _orderService.GetCustomerOrdesAsync(customerId);

            return Ok(getUserOrdersDTOs);
        }

        [HttpGet("{orderId}")]
        [ActionName("orders")]
        public async Task<IActionResult> GetOrder(int orderId)
        {
            GetSingleOrderDTO getSingleOrderDTO = await _orderService.GetOrderAsync(orderId);

            return Ok(getSingleOrderDTO);
        }

        [HttpPut("{orderId}")]
        [ActionName("orders")]
        public async Task<IActionResult> UpdateOrder(int orderId, [FromBody] UpdateOrderDTO updateOrderDTO)
        {
            await _orderService.UpdateOrderAsync(updateOrderDTO, orderId);

            return Ok();
        }

        [HttpDelete("{orderId}")]
        [ActionName("orders")]
        public IActionResult DeleteOrder(int orderId)
        {
            _orderService.DeleteOrder(orderId);

            return Ok();
        }
    }
}
