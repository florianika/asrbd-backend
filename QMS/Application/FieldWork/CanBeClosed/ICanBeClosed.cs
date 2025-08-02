using Application.FieldWork.CanBeClosed.Request;
using Application.FieldWork.CanBeClosed.Response;
using Domain;

namespace Application.FieldWork.CanBeClosed
{
    public interface ICanBeClosed : IFieldWork<CanBeClosedRequest, CanBeClosedResponse>
    {

    }
}
