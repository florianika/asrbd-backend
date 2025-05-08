
using Application.Ports;
using Domain;
using Infrastructure.Context;
using Infrastructure.Migrations;

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
    }
}
