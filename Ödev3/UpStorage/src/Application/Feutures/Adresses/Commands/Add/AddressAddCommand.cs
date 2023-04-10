using Domain.Common;
using Domain.Entities;
using Domain.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feutures.Adresses.Commands.Add
{
    public class AddressAddCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }

        public string UserId { get; set; }

        public int CountryId { get; set; }
        public int CityId { get; set; }
        public string District { get; set; }
        public string PostCode { get; set; }

        public string AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }


    }
}
