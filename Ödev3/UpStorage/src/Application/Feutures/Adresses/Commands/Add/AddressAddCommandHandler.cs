using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Domain.Extensions;
using MediatR;


namespace Application.Feutures.Adresses.Commands.Add
{
    public class AddressAddCommandHandler : IRequestHandler<AddressAddCommand, Response<int>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public AddressAddCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Response<int>> Handle(AddressAddCommand request, CancellationToken cancellationToken)
        {
            var address = new Address()
            {
                Name = request.Name,
                UserId= request.UserId,
                CountryId = request.CountryId,
                CityId= request.CityId,
                District= request.District,
                PostCode= request.PostCode,
                AddressLine1= request.AddressLine1,
                AddressLine2= request.AddressLine2,
                CreatedOn = DateTimeOffset.Now,
                CreatedByUserId = null,
                IsDeleted = false,
            };
            await _applicationDbContext.Address.AddAsync(address, cancellationToken);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new Response<int>($"The new address named \"{address.Name}\" was successfully added.", address.Id);
        }
    }
}
