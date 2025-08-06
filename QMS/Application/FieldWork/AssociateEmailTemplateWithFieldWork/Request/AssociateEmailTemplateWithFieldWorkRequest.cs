namespace Application.FieldWork.AssociateEmailTemplateWithFieldWork.Request
{
    public class AssociateEmailTemplateWithFieldWorkRequest : FieldWork.Request
    {
        public int FieldWorkId {  get; set; }
        public int EmailTemplateId { get; set; }
        public Guid UpdatedUser { get; set; }
        public bool isOpen { get; set; }
    }
}
