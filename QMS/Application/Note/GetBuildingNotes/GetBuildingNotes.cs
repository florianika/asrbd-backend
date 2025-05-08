

using Application.Common.Translators;
using Application.Note.GetBuildingNotes.Request;
using Application.Note.GetBuildingNotes.Response;
using Application.Ports;
using Application.ProcessOutputLog.GetProcessOutputLogsByBuildingId.Response;

namespace Application.Note.GetBuildingNotes
{
    public class GetBuildingNotes : IGetBuildingNotes
    {
        private readonly INoteRepository _noteRepository;
        public GetBuildingNotes(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<GetBuildingNotesResponse> Execute(GetBuildingNotesRequest request)
        {
            var notes = await _noteRepository.GetBuildingNotes(request.BldId);
            return new GetBuildingNotesSuccessResponse
            {
                NotesDTO = Translator.ToDTOList(notes)
            };

        }
    }
}
