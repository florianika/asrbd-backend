﻿using Application.Exceptions;
using Application.Note.CreateNote;
using Application.Note.CreateNote.Request;
using Application.Note.CreateNote.Response;
using Application.Note.DeleteNote;
using Application.Note.DeleteNote.Request;
using Application.Note.DeleteNote.Response;
using Application.Note.GetBuildingNotes;
using Application.Note.GetBuildingNotes.Request;
using Application.Note.GetBuildingNotes.Response;
using Application.Note.GetNote;
using Application.Note.GetNote.Request;
using Application.Note.GetNote.Response;
using Application.Note.UpdateNote;
using Application.Note.UpdateNote.Request;
using Application.Note.UpdateNote.Response;
using Application.Ports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
        [ApiController]
        [Route("api/qms/notes")]
    public class NoteController : QmsControllerBase
    {
        private readonly IDeleteNote _deleteNoteService;
        private readonly IUpdateNote _updateNoteService;
        private readonly ICreateNote _createNoteService;
        private readonly IGetBuildingNotes _getBuildingNotesService;
        private readonly IGetNote _getNoteService;
        private readonly IAuthTokenService _authTokenService;
        public NoteController(ICreateNote createNoteService, IGetBuildingNotes getBuildingNotesService, IGetNote getNoteService,
            IUpdateNote updateNoteService, IDeleteNote deleteNoteService ,IAuthTokenService authTokenService)
        {
            _deleteNoteService = deleteNoteService;
            _updateNoteService = updateNoteService;
            _getNoteService = getNoteService;
            _getBuildingNotesService = getBuildingNotesService;
            _createNoteService = createNoteService;
            _authTokenService = authTokenService;
        }
        
        [HttpPost]
        [Route("")]
        public async Task<CreateNoteResponse> CreateNote(CreateNoteRequest request)
        {
            var token = ExtractBearerToken();
            var userId = await _authTokenService.GetUserIdFromToken(token);
            request.UserId = userId;
            return await _createNoteService.Execute(request);
        }

        [HttpGet]
        [Route("buildings/{id:guid}")]
        public async Task<GetBuildingNotesResponse> GetBuildingNotes(Guid id)
        {
            return await _getBuildingNotesService.Execute(new GetBuildingNotesRequest() { BldId = id });
        }

        [HttpGet]
        [Route("{id:long}")]
        public async Task<GetNoteResponse> GetNote(long id)
        {
            return await _getNoteService.Execute(new GetNoteRequest() { Id = id });
        }

        [HttpPut("{id:long}")]
        //TODO change this method to patch 
        public async Task<UpdateNoteResponse> UpdateNote(long id, UpdateNoteRequest request)
        {
           
            var token = ExtractBearerToken();
            var userId = await _authTokenService.GetUserIdFromToken(token);
            request.NoteId = id;
            request.UserId = userId;
            return await _updateNoteService.Execute(request);
        }

        [HttpDelete("{id:long}")]
        public async Task<DeleteNoteResponse> DeleteNote(long id)
        {
            var token = ExtractBearerToken(); 
            var userId = await _authTokenService.GetUserIdFromToken(token);
            var role = await _authTokenService.GetUserRoleFromToken(token);
            return await _deleteNoteService.Execute(new DeleteNoteRequest() { Id=id, UserId=userId, Role=role });
        }        
    }
}