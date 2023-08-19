
namespace Application.RolePermission
{
    public interface IRolePermission<Req, Res> 
        where Req: RequestRolePermission
        where Res: ResponseRolePermission
    {
        public Task<Res> Execute(Req request);
    }
}
