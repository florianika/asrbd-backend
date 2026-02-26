namespace Application.Ports
{
    public interface IJobDispatcher
    {
        void ScheduleBldReviewAndEmail(int fieldWorkId, Guid updatedUser);
        void ScheduleClosureAndEmail(int fieldWorkId, Guid updatedUser, string? Remarks);        
    }
}
