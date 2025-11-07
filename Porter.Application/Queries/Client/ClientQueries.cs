using MediatR;
using Personal.Common;
using Personal.Common.Domain;

namespace Porter.Application.Queries.Client
{
    public class GetAllClientsQuery : IRequest<Result> { }

    public class GetClientByDoctoQuery : IRequest<Result>
    { 
        public string Docto { get; set; }
    }
}
