
namespace Application.User.UpdateUserRole.Response
{
    public class UpdateUserRoleErrorResponse : UpdateUserRoleResponse
    {
        public string Message { get; internal set; }
        public string Code { get; internal set; }
    }
}
