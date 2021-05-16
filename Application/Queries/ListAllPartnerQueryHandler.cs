using Application.Online.MarketPlace.ViewModels;
using Domain.Online.MarketPlace.DTO;
using Domain.Online.MarketPlace.Model;
using Infrastruture.Online.MarketPlace.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Online.MarketPlace.Queries
{
    public class ListAllPartnerQuery:IRequest<ApiResponse<PartnerDetails>>
    {

    }
    public class ListAllPartnerQueryHandler : IRequestHandler<ListAllPartnerQuery, ApiResponse<PartnerDetails>>

    {
        private readonly IPartnerDetailRepository _partnerDetailRepository;
        public ListAllPartnerQueryHandler(IPartnerDetailRepository partnerDetailRepository)
        {
            _partnerDetailRepository = partnerDetailRepository ?? throw new ArgumentNullException(nameof(partnerDetailRepository));
        }
        public async  Task<ApiResponse<PartnerDetails>> Handle(ListAllPartnerQuery request, CancellationToken cancellationToken)
        {
            var result =await  _partnerDetailRepository.GetAll();
         
            return new ApiResponse<PartnerDetails>
            {
                Data = result,
                StatusCode = (int)HttpStatusCode.OK,
                Message = null,
                Success = true

            };

        }
    }
}

