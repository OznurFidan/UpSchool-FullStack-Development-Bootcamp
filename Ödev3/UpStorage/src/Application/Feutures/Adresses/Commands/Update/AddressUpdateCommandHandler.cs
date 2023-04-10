using Application.Common.Interfaces;
using Domain.Common;
using MediatR;

namespace Application.Feutures.Adresses.Commands.Update
{
    public class AddressUpdateCommandHandler : IRequestHandler<AddressUpdateCommand, Response<int>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        public AddressUpdateCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Response<int>> Handle(AddressUpdateCommand request, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext.Address.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new Exception();
            }
            entity.Title = request.Title;

            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return new Response<int>();
        }
    }
}