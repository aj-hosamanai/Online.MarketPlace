using Application.Online.MarketPlace.ViewModels;
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
    public class ListAllCatalogueQuery:IRequest<ApiResponse<Catalogue>>
    {

    }
    public class ListAllCatalogueQueryHandler : IRequestHandler<ListAllCatalogueQuery, ApiResponse<Catalogue>>
    {
        private readonly ICatalogueRepository _catalogueRepository;
        public ListAllCatalogueQueryHandler( ICatalogueRepository catalogueRepository)
        {
            
            _catalogueRepository = catalogueRepository ?? throw new ArgumentNullException(nameof(catalogueRepository));

        }
        public async Task<ApiResponse<Catalogue>> Handle(ListAllCatalogueQuery request, CancellationToken cancellationToken)
        {
            var result = await _catalogueRepository.GetAll();
            return new ApiResponse<Catalogue>
            {
                Data = result,
                StatusCode = (int)HttpStatusCode.OK,
                Message = null,
                Success = true
            };
        }
    }
}
