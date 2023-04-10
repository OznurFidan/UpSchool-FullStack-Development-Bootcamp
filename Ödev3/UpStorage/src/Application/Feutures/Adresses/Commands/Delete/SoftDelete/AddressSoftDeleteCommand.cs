using Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feutures.Adresses.Commands.Delete.SoftDelete
{
    public class AddressSoftDeleteCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
