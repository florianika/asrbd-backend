using Application.FieldWork.ExecuteJob.Request;
using Application.FieldWork.ExecuteJob.Response;

namespace Application.FieldWork.ExecuteJob
{
   
    public interface IExecuteJob : IFieldWork<ExecuteJobRequest, ExecuteJobResponse>
    {
    }
}
