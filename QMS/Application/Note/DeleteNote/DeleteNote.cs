
using Application.Exceptions;
using Application.Note.DeleteNote.Request;
using Application.Note.DeleteNote.Response;
using Application.Ports;

namespace Application.Note.DeleteNote
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
            var isAdmin = string.Equals(request.Role, "ADMIN", StringComparison.OrdinalIgnoreCase);
            var isSupervisor = string.Equals(request.Role, "SUPERVISOR", StringComparison.OrdinalIgnoreCase);
            var isOwner = note.UserId == request.UserId;

            if (!isOwner && !isAdmin && !isSupervisor)
                throw new ForbidenException("User is not authorized to delete this note");

            await _noteRepository.DeleteNote(note);

            return new DeleteNoteSuccessResponse
            {
                Message = "Note successfully deleted."
            };
        }
    }
}
