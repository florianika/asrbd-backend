using Application.Ports;
using Application.Queries.HasBldReviewExecuted;
using Application.Queries.HasBldReviewExecuted.Response;
using Infrastructure.Context;
using Infrastructure.Repositories;

namespace Infrastructure.Queries.HasBldReviewExecuted
{
    public class HasBldReviewExecutedQuery : IHasBldReviewExecutedQuery
    {

        private readonly DataContext _context;
        private readonly IFieldWorkRepository _fieldWorkRepository;
        public HasBldReviewExecutedQuery(DataContext context, IFieldWorkRepository fieldWorkRepository)
        {
            _context = context;
            _fieldWorkRepository = fieldWorkRepository;
        }
        public async Task<HasBldReviewExecutedResponse> Execute()
        {

            var result = await _fieldWorkRepository.HasBldReviewExecuted();
            return new HasBldReviewExecutedResponse
            {
                Result = result
            };
        }
    }
}
