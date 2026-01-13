using Application.Queries.HasBldReviewExecuted.Response;

namespace Application.Queries.HasBldReviewExecuted
{
    public interface IHasBldReviewExecutedQuery
    {
        Task<HasBldReviewExecutedResponse> Execute();
    }
}
