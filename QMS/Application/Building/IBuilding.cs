namespace Application.Building
{
    public interface IBuilding<Req, Res> 
        where Req : Request
        where Res : Response
    {
        public Task<Res> Execute(Req request);
    }
}
