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
using System.ComponentModel.DataAnnotations;

namespace Application.Online.MarketPlace.Commands
{
    public class UpdateCatalogueCommand:IRequest<ApiResponse<bool>>
        {
            [Required]
        public int Id { get; set; }
          [Required]
                  public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int CatalogTypeId { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
   public  class UpdateCatalogueCommandHandler:IRequestHandler<UpdateCatalogueCommand ,ApiResponse<bool>>

    {
        private readonly IPartnerDetailRepository _partnerDetailRepository;
        private readonly ICatalogueRepository _catalogueRepository;

        public UpdateCatalogueCommandHandler(IPartnerDetailRepository partnerDetailRepository, ICatalogueRepository catalogueRepository)
        {
            _partnerDetailRepository = partnerDetailRepository ?? throw new ArgumentNullException(nameof(partnerDetailRepository));
            _catalogueRepository = catalogueRepository ?? throw new ArgumentNullException(nameof(catalogueRepository));

        }

        public async Task<ApiResponse<bool>> Handle(UpdateCatalogueCommand request, CancellationToken cancellationToken)
        {
            var existingCatalogue = await _catalogueRepository.GetById(request.Id);
            var updateCatalogue = new Catalogue
            {
                Id = existingCatalogue.Id,
                CatalogTypeId = existingCatalogue.CatalogTypeId,
                Description = request.Description,
                Name = request.Name,
                UpdatedBy = request.UpdatedBy
            };
            _catalogueRepository.Update(updateCatalogue);
            _catalogueRepository.SaveChanges();
            return new ApiResponse<bool>
            {
                Data=true,
                Message="Updated Successfully",
                StatusCode=(int)HttpStatusCode.OK,
                Success=true
            };
        }
    }
}
