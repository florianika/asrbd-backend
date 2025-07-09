using Application.FieldWork.AssociateEmailTemplateWithFieldWork.Request;
using Application.FieldWork.AssociateEmailTemplateWithFieldWork.Response;
using Domain;

namespace Application.FieldWork.AssociateEmailTemplateWithFieldWork
{
    public interface IAssociateEmailTemplateWithFieldWork : IFieldWork<AssociateEmailTemplateWithFieldWorkRequest, AssociateEmailTemplateWithFieldWorkResponse>
    {
    }
}
