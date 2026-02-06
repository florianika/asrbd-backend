

using Application.User.GetAllUsers.Response;
using Domain.Enum;

namespace Application.User.GetAllUsers
{
    public interface IGetAllUsers
    {
        Task<GetAllUsersResponse> Execute(Guid userId, string role);
    }
}
