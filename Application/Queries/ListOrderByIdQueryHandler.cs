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

namespace Application.Online.MarketPlace.Queries
{
    public class ListOrderByIdQuery:IRequest<ApiResponse<IEnumerable<Order>>>

    {
        [Required]
        public int Id { get; set; }
        public ListOrderByIdQuery(int id)
        {
            Id = id;
        }
    }

    public class ListOrderByIdQueryHandler : IRequestHandler<ListOrderByIdQuery, ApiResponse<IEnumerable<Order>>>
    {
        private readonly IOrderRepository _orderRepository;
        public ListOrderByIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }
        public async  Task<ApiResponse<IEnumerable<Order>>> Handle(ListOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _orderRepository.GetById(request.Id);
            if(result!=null)
            {
                return new ApiResponse<IEnumerable<Order>>
                {
                    Data = result,
                    StatusCode = (int)HttpStatusCode.OK,
                    Message = null,
                    Success = true
                };
            }
            else
            {
                return new ApiResponse<IEnumerable<Order>>
                {
                    Data = null,
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = "No records found",
                    Success = false
                };
            }

        }
    }
}
