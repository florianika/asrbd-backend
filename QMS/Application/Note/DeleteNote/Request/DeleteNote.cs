
using Application.Note.DeleteNote.Response;
using Application.Note.UpdateNote.Response;
using Application.Ports;

namespace Application.Note.DeleteNote.Request
{
    public class DeleteNote : IDeleteNote
    {
        private readonly INoteRepository _noteRepository;

        public DeleteNote(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<DeleteNoteResponse> Execute(DeleteNoteRequest request)
        {
            var note = await _noteRepository.GetNote(request.Id);
            if (note == null)
            {
                return new DeleteNoteErrorResponse { Code = "404", Message = "Note not found" };
            }
            await _noteRepository.DeleteNote(note);
            return new DeleteNoteSuccessResponse
            {
                Message = "Note deleted"
            };
        }
    }
}
