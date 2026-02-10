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
            BackgroundJob.ContinueJobWith<FieldWorkJobService>(jobId, svc =>
                svc.SendOpenEmailsJob(fieldWorkId));
        }

        public void ScheduleClosureAndEmail(int fieldWorkId, Guid updatedUser, string remarks)
        {
            var jobId = BackgroundJob.Enqueue<FieldWorkJobService>(svc =>
               svc.ConfirmFieldworkClosureJob(fieldWorkId, updatedUser, remarks));
            BackgroundJob.ContinueJobWith<FieldWorkJobService>(jobId, svc =>
                svc.SendCloseEmailsJob(fieldWorkId));
        }
    }
}
