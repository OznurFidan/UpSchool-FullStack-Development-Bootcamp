using Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feutures.Adresses.Commands.Update
{
    public class AddressUpdateCommand:IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
