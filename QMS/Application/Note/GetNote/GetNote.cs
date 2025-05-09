
using Application.Common.Translators;
using Application.Note.GetNote.Request;
using Application.Note.GetNote.Response;
using Application.Ports;

namespace Application.Note.GetNote
{
    public class GetNote : IGetNote
    {
        private readonly INoteRepository _noteRepository;
        public GetNote(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }
        public async Task<GetNoteResponse> Execute(GetNoteRequest request)
        {
            var note = await _noteRepository.GetNote(request.Id);
            var noteDto = Translator.ToDTO(note);
            return new GetNoteSuccessResponse
            {
                NotesDTO = noteDto
            };
        }
    }
}
