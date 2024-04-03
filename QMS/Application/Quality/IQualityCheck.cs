using System;

namespace Application.Quality
{
    public interface IQualityCheck<Req, Res>
        where Req : IRequest
        where Res : IResponse
    {
        public Task<Res> Execute(Req request, string action);
    }
}
