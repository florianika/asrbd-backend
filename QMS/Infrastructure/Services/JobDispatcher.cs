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
                svc.SendOpenEmailsJob(fieldWorkId));
        }

        public void ScheduleClosureAndEmail(int fieldWorkId, Guid updatedUser, string Remarks)
        {
            var jobId = BackgroundJob.Enqueue<FieldWorkJobService>(svc =>
               svc.ConfirmFieldworkClosureJob(fieldWorkId, updatedUser, Remarks));
            BackgroundJob.ContinueWith<FieldWorkJobService>(jobId, svc =>
                svc.SendCloseEmailsJob(fieldWorkId));
        }
    }
}
