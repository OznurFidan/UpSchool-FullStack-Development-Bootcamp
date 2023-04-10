using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feutures.Adresses.Commands.Delete.SoftDelete
{
    public class AddressSoftDeleteCommandHandler : IRequestHandler<AddressSoftDeleteCommand, Response<int>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public AddressSoftDeleteCommandHandler(IApplicationDbContext applicationDbContext)
        {
            applicationDbContext = _applicationDbContext;
        }

        public async Task<Response<int>> Handle(AddressSoftDeleteCommand request, CancellationToken cancellationToken)
        {
            var address = new Address()
            {
                Id= request.Id,
                DeletedOn = DateTimeOffset.Now,
                DeletedByUserId = request.UserId,
                IsDeleted = false,
            };
            if(!request.IsDeleted)
            {
                address.IsDeleted = true;
                address.DeletedOn = DateTimeOffset.Now;
                address.DeletedByUserId =request.UserId ;
                _applicationDbContext.SaveChanges();
            }
            return new Response<int>($"The address  \"{address.Name}\" was successfully deleted.", address.Id);
        }
    }
}
