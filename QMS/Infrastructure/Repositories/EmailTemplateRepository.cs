
using Application.Exceptions;
using Application.Ports;
using Domain;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infrastructure.Repositories
{
    public class EmailTemplateRepository : IEmailTemplateRepository
    {
        private readonly DataContext _context;
        public EmailTemplateRepository(DataContext dataContext)
        {
            _context = dataContext;
        }
        public async Task<int> CreateEmailTemplate(EmailTemplate emailTemplate)
        {
            await _context.EmailTemplates.AddAsync(emailTemplate);
            await _context.SaveChangesAsync();
            return emailTemplate.EmailTemplateId;
        }

        public async Task<List<EmailTemplate>>GetAllEmailTemplates()
        {
            return await _context.EmailTemplates.ToListAsync();
        }

        public async Task<EmailTemplate> GetEmailTemplate(int id)
        {
            return await _context.EmailTemplates.FirstOrDefaultAsync(x => x.EmailTemplateId.Equals(id))
               ?? throw new NotFoundException("Email Template not found");
        }

        public async Task UpdateEmailTemplate(EmailTemplate emailTemplate)
        {
            _context.EmailTemplates.Update(emailTemplate);
            await _context.SaveChangesAsync();
        }
    }
}
