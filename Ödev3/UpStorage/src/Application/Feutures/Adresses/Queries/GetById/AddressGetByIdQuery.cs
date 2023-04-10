using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feutures.Adresses.Queries.GetById
{
    public class AddressGetByIdQuery : IRequest<List<AddressGetByIdDto>>
    {
        public int Id {get;set;}
        public bool? IsDeleted { get;set;}

        public AddressGetByIdQuery(int ıd, bool? isDeleted)
        {
            Id = ıd;
            IsDeleted = isDeleted;
        }
    }
}
