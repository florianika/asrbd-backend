
namespace Application.FieldWorkRule
{
    public interface IFieldWorkRule<Req, Res>
    where Req : Request
    where Res : Response
    {
        public Task<Res> Execute(Req request);
    }
    
}
