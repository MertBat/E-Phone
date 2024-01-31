using E_Phone.BLL.DTOs.Brand;
using E_Phone.BLL.DTOs.Order;
using E_Phone.BLL.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Phone.API.Controllers
{
    /// <summary>
    /// Sipariş işlemelri
    /// </summary>
    [ApiController]
    [Authorize(AuthenticationSchemes = "User")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Sipariş oluşturma
        /// </summary>
        /// <param name="createOrderDTO">
        /// <strong>orderCount (sipariş miktarı):</strong> 0 dan büyük bir değer almalıdır.<br/>
        /// <strong>versionId (versiyon id):</strong> Aktif bir versiyon olmalıdır.
        /// </param>
        [HttpPost("orders")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO createOrderDTO)
        {
            string tokenHeader = HttpContext.Request.Headers["Authorization"];
            string token = tokenHeader.Split(' ')[1];
            await _orderService.CreateOrderAsync(createOrderDTO, token);

            return Ok();
        }

        /// <summary>
        /// Bütün siparişleri listeleme
        /// </summary>
        [HttpGet("orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            List<GetOrdersDTO> getOrdersDTOs = await _orderService.GetAllOrdersAsync();

            return Ok(getOrdersDTOs);
        }

        /// <summary>
        /// Müşterinin siparişlerini listeleme
        /// </summary>
        /// <param name="customerId">Müşteri id ile müşterinin siparişlerini listeleme.</param>
        [HttpGet("customers/{customerId}/orders", Name = "GetOrders")]
        public async Task<IActionResult> GetCustomerOrders(int customerId)
        {
            List<GetUserOrdersDTO> getUserOrdersDTOs = await _orderService.GetCustomerOrdesAsync(customerId);

            return Ok(getUserOrdersDTOs);
        }

        /// <summary>
        /// Belirli bir siparişin görüntülenmesi
        /// </summary>
        /// <param name="orderId">Id ye göre sipariş detaylarının görüntüleme.</param>
        [HttpGet("orders/{orderId}")]
        public async Task<IActionResult> GetOrder(int orderId)
        {
            GetSingleOrderDTO getSingleOrderDTO = await _orderService.GetOrderAsync(orderId);

            return Ok(getSingleOrderDTO);
        }

        /// <summary>
        /// Sipariş değiştirme
        /// </summary>
        /// <param name="orderId">Id ye göre siparişin güncellenmesi</param>
        /// <param name="updateOrderDTO">
        /// <strong>versionId (versiyon id): </strong> Aktif bir versiyon olmalıdır.<br/>
        /// <strong>orderCondition (sipariş durumu):</strong> <i> 0:</i> beklemede,<i> 1:</i> tamamlandı,<i> 2:</i> iptal edildi. <br/> 
        /// <strong>orderCount (sipariş miktarı):</strong> 0 dan büyük bir değer almalıdır. 
        /// </param>
        [HttpPut("orders/{orderId}")]
        public async Task<IActionResult> UpdateOrder(int orderId, [FromBody] UpdateOrderDTO updateOrderDTO)
        {
            await _orderService.UpdateOrderAsync(updateOrderDTO, orderId);

            return Ok();
        }

        /// <summary>
        /// Belirli bir markanın silinmesi
        /// </summary>
        /// <param name="orderId">Id ye göre siparişin silinmesi</param>
        [HttpDelete("orders/{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
           await _orderService.DeleteOrder(orderId);

            return Ok();
        }
    }
}
