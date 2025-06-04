using Application.FieldWork.SendFieldWorkEmail.Request;
using Application.FieldWork.SendFieldWorkEmail.Response;

namespace Application.FieldWork.SendFieldWorkEmail
{
    public interface ISendFieldWorkEmail
    {
        Task<SendFieldWorkEmailResponse> Execute(SendFieldWorkEmailRequest request);
    }
}
