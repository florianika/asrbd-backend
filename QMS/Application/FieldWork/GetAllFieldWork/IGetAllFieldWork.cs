using Application.FieldWork.GetAllFieldWork.Response;

namespace Application.FieldWork.GetAllFieldWork
{
    public interface IGetAllFieldWork
    {
        Task<GetAllFieldWorkResponse> Execute();
    }
}
