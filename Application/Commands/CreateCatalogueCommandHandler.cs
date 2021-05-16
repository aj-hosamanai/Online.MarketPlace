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
    public class CreateCatalogueCommand:IRequest<ApiResponse<bool>>
    {
       
          [Required]
        public string UserName { get; set; }
         [Required]
          public string CatalogueName { get; set; }
        public string CatalogueDescription { get; set; }
         [Required]
        public decimal CataloguePrice { get; set; }
         [Required]
        public int CatalogTypeId { get; set; }
    }
    public class CreateCatalogueCommandHandler : IRequestHandler<CreateCatalogueCommand, ApiResponse<bool>>

    {
        private readonly IPartnerDetailRepository _partnerDetailRepository;
        private readonly ICatalogueRepository  _catalogueRepository;
        
        public CreateCatalogueCommandHandler(IPartnerDetailRepository partnerDetailRepository, ICatalogueRepository catalogueRepository)
        {
            _partnerDetailRepository = partnerDetailRepository ?? throw new ArgumentNullException(nameof(partnerDetailRepository));
            _catalogueRepository = catalogueRepository ?? throw new ArgumentNullException(nameof(catalogueRepository));
            
        }
        public async Task<ApiResponse<bool>> Handle(CreateCatalogueCommand request, CancellationToken cancellationToken)
        {
            var users = await _partnerDetailRepository.GetAll();
          
            var createCatalogue = new Catalogue
            {
                Name=request.CatalogueName,
                Description=request.CatalogueDescription,
                CatalogTypeId=request.CatalogTypeId,
                Price=request.CataloguePrice,
                CreatedBy=request.UserName,
               

            };
            _catalogueRepository.Add(createCatalogue);
            _catalogueRepository.SaveChanges();
            return new ApiResponse<bool>
            {
                Data = true,
                StatusCode = (int)HttpStatusCode.Created,
                Success = true,
                Message = "Catalogue Added Successfully."

            };
        }
    }
}
