using MediatR;
using Personal.Common;
using Personal.Common.Domain;

namespace Porter.Application.Queries.Room
{
    public class GetAllRoomsQuery : IRequest<Result> { }

    public class GetRoomByNameQuery : IRequest<Result>
    { 
        public string Name { get; set; }
    }

}
