using AutoMapper;
using E_Phone.BLL.DTOs.Order;
using E_Phone.BLL.Handlers.Abstract;
using E_Phone.BLL.Services.Abstract;
using E_Phone.Core.Entities;
using E_Phone.Core.Enums;
using E_Phone.Core.IRepositories.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Phone.BLL.Services.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IBaseRepository<Order> _orderRepository;
        private readonly IBaseRepository<Core.Entities.Version> _versionRepository;
        private readonly ITokenHandler _tokenHandler;
        private readonly IMapper _mapper;

        public OrderService(IBaseRepository<Order> OrderRepository, IBaseRepository<Core.Entities.Version> VersionRepository, ITokenHandler tokenHandler, IMapper mapper)
        {
            _orderRepository = OrderRepository;
            _versionRepository = VersionRepository;
            _tokenHandler = tokenHandler;
            _mapper = mapper;
        }

        public async Task CreateOrderAsync(CreateOrderDTO orderDTO, string token)
        {
            int userId = _tokenHandler.GetIdFromToken(token);

            Core.Entities.Version version = await _versionRepository.GetAsync(v => v.Id == orderDTO.VersionId);
            if (version == null)
                throw new ArgumentException("Version Bulunamadı.");

            Order newOrder = new()
            {
                VersionId = version.Id,
                OrderCondition = OrderCondition.Waiting,
                OrderCount = orderDTO.OrderCount,
                OrderDate = DateTime.Now,
                TotalPrice = orderDTO.OrderCount * version.price,
                UserId = userId,
            };

            await _orderRepository.CreateAsync(newOrder);
        }

        public async Task DeleteOrder(int id)
        {
            Order order = await _orderRepository.GetAsync(o => o.Id == id);
            if (order == null)
                throw new ArgumentException("Sipariş bulunamadı.");

            _orderRepository.Delete(order);
        }

        public async Task<List<GetOrdersDTO>> GetAllOrdersAsync()
        {
            List<Order> orders = await _orderRepository.GetAllAsync();

            return _mapper.Map<List<GetOrdersDTO>>(orders);
        }

        public async Task<List<GetUserOrdersDTO>> GetCustomerOrdesAsync(int userId)
        {
            List<Order> orders = await _orderRepository.GetAllAsync(o => o.UserId == userId);

            return _mapper.Map<List<GetUserOrdersDTO>>(orders);
        }

        public async Task<GetSingleOrderDTO> GetOrderAsync(int id)
        {
            Order order = await _orderRepository.GetAsync(o => o.Id == id);
            if (order == null)
                throw new ArgumentException("Sipariş bulunamadı");

            return _mapper.Map<GetSingleOrderDTO>(order);
        }

        public async Task UpdateOrderAsync(UpdateOrderDTO updateOrderDTO, int id)
        {
            Order SelectedOrder = await _orderRepository.GetAsync(o => o.Id == id);
            if (SelectedOrder == null)
                throw new ArgumentException("Sipariş bulunamadı");

            Core.Entities.Version version = await _versionRepository.GetAsync(v => v.Id == updateOrderDTO.VersionId);
            if (version == null)
                throw new ArgumentException("Version Bulunamadı.");

            SelectedOrder.TotalPrice = updateOrderDTO.OrderCount * version.price;
            SelectedOrder.OrderCondition = updateOrderDTO.OrderCondition;
            SelectedOrder.VersionId = updateOrderDTO.VersionId;
            _orderRepository.Update(SelectedOrder);
        }
    }
}
