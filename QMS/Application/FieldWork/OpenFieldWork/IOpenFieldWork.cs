using Application.FieldWork.OpenFieldWork.Request;
using Application.FieldWork.OpenFieldWork.Response;

namespace Application.FieldWork.OpenFieldWork
{
    public interface IOpenFieldWork : IFieldWork<OpenFieldWorkRequest, OpenFieldWorkResponse>
    {
    }
}
