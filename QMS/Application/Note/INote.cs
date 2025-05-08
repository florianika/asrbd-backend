
namespace Application.Note
{
    public interface INote<Req, Res>
     where Req : Request
     where Res : Response
    {
        public Task<Res> Execute(Req request);
    }
}
