namespace Application.Ports
{
    public interface IJobDispatcher
    {
        void ScheduleBldReviewAndEmail(int fieldWorkId, Guid updatedUser);
    }
}
