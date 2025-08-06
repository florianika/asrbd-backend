
using Application.Exceptions;
using Application.FieldWork.CreateFieldWork.Request;
using Application.FieldWork.CreateFieldWork.Response;
using Application.Ports;
using Application.Rule.CreateRule.Response;
using Microsoft.Extensions.Logging;

namespace Application.FieldWork.CreateFieldWork
{
    public class CreateFieldWork : ICreateFieldWork
    {
        private readonly ILogger _logger;
        private readonly IFieldWorkRepository _fieldWorkRepository;
        public CreateFieldWork(ILogger<CreateFieldWork> logger, IFieldWorkRepository fieldWorkRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _fieldWorkRepository = fieldWorkRepository ?? throw new ArgumentNullException(nameof(fieldWorkRepository));

        }

        public async Task<CreateFieldWorkResponse> Execute(CreateFieldWorkRequest request)
        {
            try 
            {
                if (!await _fieldWorkRepository.HasActiveFieldWork())
                {
                    throw new ForbidenException("There are active fieldwork and cannot create e new one");
                }
                var fieldWork = new Domain.FieldWork
                {
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    FieldWorkName = request.FieldWorkName,
                    Description = request.Description,
                    OpenEmailTemplateId = 0,
                    CloseEmailTemplateId = 0,
                    CreatedUser = request.CreatedUser,
                    CreatedTimestamp = DateTime.Now,
                    FieldWorkStatus = Domain.Enum.FieldWorkStatus.NEW,
                    UpdatedUser = null,
                    UpdatedTimestamp = null
                };
                var result = await _fieldWorkRepository.CreateFieldWork(fieldWork);
                return new CreateFieldWorkSuccessResponse
                {
                    FieldWorkId = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
