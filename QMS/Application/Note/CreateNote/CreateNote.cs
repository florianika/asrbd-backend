using Application.Note.CreateNote.Request;
using Application.Note.CreateNote.Response;
using Application.Ports;
using Microsoft.Extensions.Logging;

namespace Application.Note.CreateNote
{
    public class CreateNote : ICreateNote
    {
        private readonly ILogger _logger;
        private readonly INoteRepository _noteRepository;
        public CreateNote(ILogger<CreateNote> logger, INoteRepository noteRepository)
        {
            _logger = logger;
            _noteRepository = noteRepository;
        }
        public async Task<CreateNoteResponse> Execute(CreateNoteRequest request)
        {
            try
            {
                var note = new Domain.Note
                {
                    BldId = request.BldId,
                    NoteText = request.NoteText,
                    CreatedUser = request.CreatedUser,
                    CreatedTimestamp = DateTime.Now,
                    UserId = request.UserId,
                    UpdatedUser = null,
                    UpdatedTimestamp = null
                };
                var result = await _noteRepository.CreateNote(note);
                return new CreateNoteSuccessResponse
                {
                    NoteId = result
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
