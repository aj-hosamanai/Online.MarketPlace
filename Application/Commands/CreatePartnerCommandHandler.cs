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
using System.ComponentModel.DataAnnotations;


namespace Application.Online.MarketPlace.Commands
{
    public class CreatePartnerCommand:IRequest<ApiResponse<bool>>
    {
        [Required(ErrorMessage="FirstName Required")]
        public string FirstName { get; set; }
       [Required(ErrorMessage="LastName Required")]
        public string LastName { get; set; }
         [Required(ErrorMessage="Email Required")]
         [DataType(DataType.EmailAddress)]
         
        public string Email { get; set; }
       [Required(ErrorMessage="Password Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
    public class CreatePartnerCommandHandler : IRequestHandler<CreatePartnerCommand, ApiResponse<bool>>
    {
       private readonly IPartnerDetailRepository _partnerDetailRepository;
        public CreatePartnerCommandHandler(IPartnerDetailRepository partnerDetailRepository)
        {
            _partnerDetailRepository = partnerDetailRepository ?? throw new ArgumentNullException(nameof(partnerDetailRepository));
        }

        public async Task<ApiResponse<bool>> Handle(CreatePartnerCommand request, CancellationToken cancellationToken)
        {
            var createPartner = new PartnerDetails
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password
            };
              _partnerDetailRepository.Add(createPartner);
               _partnerDetailRepository.SaveChanges();
            return new ApiResponse<bool>
            {
                Data = true,
                StatusCode = (int)HttpStatusCode.Created,
                Success = true,
                Message = "Partner Details Added Successfully."

            };
        }
    }
}

