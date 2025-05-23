﻿namespace Application.EmailTemplate
{
    public class EmailTemplateDTO
    {
        public int EmailTemplateId { get; set; }
        public required string Subject { get; set; }
        public required string Body { get; set; }
        public Guid CreatedUser { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public Guid? UpdatedUser { get; set; }
        public DateTime? UpdatedTimestamp { get; set; }
    }
}
