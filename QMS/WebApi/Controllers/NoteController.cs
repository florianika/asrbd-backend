using Application.Note.CreateNote;
using Application.Note.CreateNote.Request;
using Application.Note.CreateNote.Response;
using Application.Note.GetBuildingNotes;
using Application.Note.GetBuildingNotes.Request;
using Application.Note.GetBuildingNotes.Response;
using Application.Ports;
using Application.ProcessOutputLog.GetProcessOutputLogsByBuildingId.Request;
using Application.ProcessOutputLog.GetProcessOutputLogsByBuildingId.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
        [ApiController]
        [Route("api/qms/note")]
    public class NoteController : ControllerBase
    {
        private readonly ICreateNote _createNoteService;
        private readonly IGetBuildingNotes _getBuildingNotesService;
        private readonly IAuthTokenService _authTokenService;
        public NoteController(ICreateNote createNoteService, IGetBuildingNotes getBuildingNotesService, IAuthTokenService authTokenService)
        {
            _getBuildingNotesService = getBuildingNotesService;
            _createNoteService = createNoteService;
            _authTokenService = authTokenService;
        }
        [HttpPost]
        [Route("")]
        public async Task<CreateNoteResponse> CreateNote(CreateNoteRequest request)
        {
            var token = Request.Headers["Authorization"].ToString();
            token = token.Replace("Bearer ", "");
            request.CreatedUser = await _authTokenService.GetUserIdFromToken(token);
            return await _createNoteService.Execute(request);
        }
        [HttpGet]
        [Route("buildings/{id:guid}")]
        public async Task<GetBuildingNotesResponse> GetBuildingNotes(Guid id)
        {
            return await _getBuildingNotesService.Execute(new GetBuildingNotesRequest() { BldId = id });
        }
    }
}
