using E_Phone.BLL.DTOs.Model;
using E_Phone.BLL.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Phone.BLL.Services.Abstract
{
    public interface IOrderService
    {
        Task CreateOrderAsync(CreateOrderDTO order, string token);
        Task<List<GetOrdersDTO>> GetAllOrdersAsync();
        Task<List<GetUserOrdersDTO>> GetCustomerOrdesAsync(int userId);
        Task<GetSingleOrderDTO> GetOrderAsync(int id);
        Task UpdateOrderAsync(UpdateOrderDTO order, int id);
        Task DeleteOrder(int id);
    }
}
