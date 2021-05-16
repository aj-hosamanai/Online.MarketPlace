using Application.Online.MarketPlace.ViewModels;
using Domain.Online.MarketPlace.Model;
using Infrastruture.Online.MarketPlace.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Online.MarketPlace.Commands
{
    public class CreateOrderCommand : IRequest<ApiResponse<bool>>
    {
        public string Orderstatus { get; set; }
        [Required]
        public int CatalogueId { get; set; }
        [Required]
        public string CatalogueName { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public int OrderQuantity { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ApiResponse<bool>>
    {
        private readonly IOrderRepository _orderRepository;
        public CreateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }
        public async Task<ApiResponse<bool>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var createOrder = new Order
            {
                CatalogueName = request.CatalogueName,
                CatalogueId = request.CatalogueId,
                Orderstatus = request.Orderstatus,
                OrderDate = request.OrderDate,
                OrderQuantity = request.OrderQuantity,
                UnitPrice = request.UnitPrice
            };

            _orderRepository.Add(createOrder);
            _orderRepository.SaveChanges();
            return new ApiResponse<bool>
            {
                Data = true,
                Message = "Order Placed Successfully",
                StatusCode = (int)HttpStatusCode.Created,
                Success = true
            };
        }
    }
}
