
using Application.FieldWork;

public interface IFieldWork<Req, Res>
    where Req : Request
    where Res : Response
{
    public Task<Res> Execute(Req request);
}
