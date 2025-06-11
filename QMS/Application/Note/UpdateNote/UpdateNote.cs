using Application.Exceptions;
using Application.Note.UpdateNote.Request;
using Application.Note.UpdateNote.Response;
using Application.Ports;
using Application.Rule.UpdateRule.Response;
using Microsoft.Extensions.Logging;

namespace Application.Note.UpdateNote
{
    public class UpdateNote : IUpdateNote
    {
        private readonly INoteRepository _noteRepository;
        public UpdateNote(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<UpdateNoteResponse> Execute(UpdateNoteRequest request)
        {
            var note = await _noteRepository.GetNote(request.NoteId);
            if (note.UserId != request.UserId)
            {
                throw new ForbidenException("User can not update comment");
            }
            note.NoteText = request.NoteText;
            note.UpdatedUser = request.UpdatedUser;
            note.UpdatedTimestamp = DateTime.Now;

            await _noteRepository.UpdateNote(note);

            return new UpdateNoteSuccessResponse
            {
                Message = "Note updated"
            };
        }
    }
}
