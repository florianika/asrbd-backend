
using Application.Exceptions;
using Application.Ports;
using Domain;
using Infrastructure.Context;
using Infrastructure.Migrations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly DataContext _context;
        public NoteRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public async Task<long> CreateNote(Note note)
        {
            await _context.Notes.AddAsync(note);
            await _context.SaveChangesAsync();
            return note.NoteId;
        }

        public async Task DeleteNote(Note note)
        {
             _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Note>> GetBuildingNotes(Guid buildingId)
        {
            return await _context.Notes.Where(n => n.BldId == buildingId).OrderByDescending(n=>n.CreatedTimestamp).ToListAsync();
        }

        public async Task<Note> GetNote(long id)
        {
            return await _context.Notes.FirstOrDefaultAsync(x => x.NoteId.Equals(id))
               ?? throw new NotFoundException("Note not found");
        }
        public async Task UpdateNote(Domain.Note note)
        {
            _context.Notes.Update(note);
            await _context.SaveChangesAsync();
        }
    }
}
