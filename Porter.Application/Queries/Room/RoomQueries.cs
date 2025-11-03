using MediatR;
using Porter.Common;

namespace Porter.Application.Queries.Room
{
    public class GetAllRoomsQuery : IRequest<Result> { }

    public class GetRoomByNameQuery : IRequest<Result>
    { 
        public string Name { get; set; }
    }

}
