
using Application.Quality.SetBldToUntested.Request;
using Application.Quality.SetBldToUntested.Response;

namespace Application.Quality.SetBldToUntested
{
    public interface ISetBldToUntested : IQualityCheck<SetBldToUntestedRequest, SetBldToUntestedResponse>
    {
    }
}
