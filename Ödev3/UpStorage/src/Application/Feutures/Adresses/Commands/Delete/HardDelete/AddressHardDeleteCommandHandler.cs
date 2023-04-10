using Application.Common.Interfaces;
using Application.Feutures.Adresses.Commands.Delete.SoftDelete;
using Domain.Common;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feutures.Adresses.Commands.Delete.HardDelete
{
    public class AddressHardDeleteCommandHandler : IRequestHandler<AddressSoftDeleteCommand, Response<int>>
    {
        public readonly IApplicationDbContext _applicationDbContext;
        public AddressHardDeleteCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Response<int>> Handle(AddressSoftDeleteCommand request, CancellationToken cancellationToken)
        {
          
            var address = new Address()
            {
                Id = request.Id,
                DeletedOn = DateTimeOffset.Now,
                DeletedByUserId = request.UserId,
                IsDeleted = false,
               
            };
            if (!request.IsDeleted)
            {
                address.IsDeleted = true;
                address.DeletedOn = DateTimeOffset.Now;
                address.DeletedByUserId = request.UserId;
                _applicationDbContext.Address.Remove(address);
                _applicationDbContext.SaveChanges();
            }

            else
            {
                address.DeletedOn = DateTimeOffset.Now;
                address.DeletedByUserId = request.UserId;
                _applicationDbContext.Address.Remove(address); 
                _applicationDbContext.SaveChanges();

            }
            return new Response<int>($"The address  \"{address.Name}\" was successfully deleted.", address.Id);
        }
    }
}
