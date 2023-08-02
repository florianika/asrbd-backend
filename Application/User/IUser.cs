using System.Threading.Tasks;

namespace Application.User
{
    public interface IUser<Req, Res> 
        where Req: Request
        where Res: Response
    {
        public Task<Res> Execute(Req request);
    }
}
