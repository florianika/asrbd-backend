
namespace Application.Rule
{
    public interface IRule<Req, Res>
       where Req : Request
       where Res : Response
    {
        public Task<Res> Execute(Req request);
    }
}
