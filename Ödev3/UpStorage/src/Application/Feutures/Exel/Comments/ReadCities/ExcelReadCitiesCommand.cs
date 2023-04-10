using Domain.Common;
using MediatR;


namespace Application.Feutures.Exel.Comments.ReadCities
{
    public class ExcelReadCitiesCommand: IRequest<Response<int>>
    {
        public string ExcelBase64File { get; set; }
    }
}
