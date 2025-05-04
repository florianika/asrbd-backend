using Application.FieldWork.CreateFieldWork.Request;
using Application.FieldWork.CreateFieldWork.Response;

namespace Application.FieldWork.CreateFieldWork
{
    public interface ICreateFieldWork : IFieldWork<CreateFieldWorkRequest, CreateFieldWorkResponse>
    {
    }
}
