using Application.Note.CreateNote;
using Application.Note.CreateNote.Request;
using Application.Note.CreateNote.Response;
using Application.Ports;
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
        private readonly IAuthTokenService _authTokenService;
        public NoteController(ICreateNote createNoteService,IAuthTokenService authTokenService)
        {
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
    }
}
