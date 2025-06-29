using Application.Ports;
using Hangfire;

namespace Infrastructure.Services
{
    public class JobDispatcher : IJobDispatcher
    {
        public void ScheduleBldReviewAndEmail(int fieldWorkId, Guid updatedUser)
        {
            var jobId = BackgroundJob.Enqueue<FieldWorkJobService>(svc =>
                svc.UpdateBldReviewStatusJob(fieldWorkId, updatedUser));
            BackgroundJob.ContinueWith<FieldWorkJobService>(jobId, svc =>
                svc.SendEmailsJob(fieldWorkId));
        }
    }
}
