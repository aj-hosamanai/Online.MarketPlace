using Application.Online.MarketPlace.ViewModels;
using Infrastruture.Online.MarketPlace.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Online.MarketPlace.Commands
{

    public class DeleteCatalogueCommand:IRequest<ApiResponse<bool>>
    {
        public DeleteCatalogueCommand(int id )
        {
            Id = id;
        }
        public int Id { get; set; }
    }

    public class DeleteCatalogueCommandHandler : IRequestHandler<DeleteCatalogueCommand, ApiResponse<bool>>
    {
        private readonly IPartnerDetailRepository _partnerDetailRepository;
        private readonly ICatalogueRepository _catalogueRepository;

        public DeleteCatalogueCommandHandler(IPartnerDetailRepository partnerDetailRepository, ICatalogueRepository catalogueRepository)
        {
            _partnerDetailRepository = partnerDetailRepository ?? throw new ArgumentNullException(nameof(partnerDetailRepository));
            _catalogueRepository = catalogueRepository ?? throw new ArgumentNullException(nameof(catalogueRepository));

        }
        public async Task<ApiResponse<bool>> Handle(DeleteCatalogueCommand request, CancellationToken cancellationToken)
        {
            var removeCatalogue = await _catalogueRepository.GetById(request.Id);
            _catalogueRepository.Delete(removeCatalogue);
            _catalogueRepository.SaveChanges();
            return new ApiResponse<bool>
            {
                Data = true,
                Message = "Deleted Successfully",
                StatusCode = (int)HttpStatusCode.OK,
                Success = true
            };
        }
    }
}
