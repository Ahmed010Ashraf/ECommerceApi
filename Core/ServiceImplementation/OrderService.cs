using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Domain.Models.Order;
using ServiceImplementation.specifications;
using ServicesAbstraction;
using Shared.DTOs.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation
{
    public class OrderService(IUniteOfWork _uniteofwork , IMapper _mapper , IBasketRepository _basketRepo) : IOrderService
    {
        public async Task<OrderResponse> Create(string userEmail, OrderRequest orderRequest)
        {
            //check basket exist
            var basket = await _basketRepo.GetBasketAsync(orderRequest.BasketId)
                           ?? throw new BasketNotFoundException(orderRequest.BasketId);


            //check and get items for order
            List<OrderItems> Items = [];

            foreach (var item in basket.Items) {
                var OriginalProduct = await _uniteofwork.GetRepository<Product, int>().GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);


                var myItem = new OrderItems()
                {
                    PictureUrl = OriginalProduct.PictureUrl,
                    Price = OriginalProduct.Price,
                    ProductId = OriginalProduct.Id,
                    ProductName = OriginalProduct.Name,
                    Quantity = item.Quantity,
                };

                Items.Add(myItem);
            }


            //chek and get delivary method

            var method = await _uniteofwork.GetRepository<DelivaryMethod, Guid>().GetByIdAsync(orderRequest.DelivaryMethodId)
                ?? throw new DelivaryMethodNotFoundException(orderRequest.DelivaryMethodId);

            //get order address
            var address = _mapper.Map<OrderAddress>(orderRequest.Address);


            //get subtotal
            var subtotal = Items.Sum(i => (i.Price * i.Quantity));


            //create the order 
            var order = new Order
            {
                Address = address,
                Subtotal = subtotal,
                Items = Items,
                UserEmail = userEmail,
                DelivaryMethod = method,

            };

            var repo = _uniteofwork.GetRepository<Order, Guid>();
            repo.Add(order);
            await _uniteofwork.SavechangesAsync();

            return _mapper.Map<OrderResponse>(order);
        }

        public async Task<IEnumerable<OrderResponse>> GetAllOrdersAsync(string userEmail)
        {
            var orders = await _uniteofwork.GetRepository<Order, Guid>().GetAll(new OrderSpecification(userEmail));

            return _mapper.Map<IEnumerable<OrderResponse>>(orders);
                
        }

        public async Task<IEnumerable<DeliveryMethodResponse>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods = await _uniteofwork.GetRepository<DelivaryMethod, Guid>().GetAll();
            return _mapper.Map<IEnumerable<DeliveryMethodResponse>>(deliveryMethods);
        }

        public async Task<OrderResponse> GetOrderByIdAsync(Guid Id)
        {
            var order = await  _uniteofwork.GetRepository<Order, Guid>().GetByIdAsync(new OrderSpecification(Id));

            return _mapper.Map<OrderResponse>(order);

        }
    }
}
