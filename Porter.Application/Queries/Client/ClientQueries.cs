using MediatR;
using Personal.Common;

namespace Porter.Application.Queries.Client
{
    public class GetAllClientsQuery : IRequest<Result> { }

    public class GetClientByDoctoQuery : IRequest<Result>
    { 
        public string Docto { get; set; }
    }
}
