

namespace Application.ProcessOutputLog
{
    public interface IProcessOutputLog<Req, Res>
      where Req : Request
      where Res : Response
    {
        public Task<Res> Execute(Req request);
    }
}
