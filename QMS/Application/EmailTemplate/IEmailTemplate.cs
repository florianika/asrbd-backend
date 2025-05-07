
namespace Application.EmailTemplate
{
    public interface IEmailTemplate<Req, Res>
      where Req : Request
      where Res : Response
    {
        public Task<Res> Execute(Req request);
    }
}
