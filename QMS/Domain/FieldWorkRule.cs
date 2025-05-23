﻿namespace Domain
{
    #nullable disable
    public class FieldWorkRule
    {
        public long Id { get; set; }
        // Foreign Key
        public int FieldWorkId { get; set; }
        public FieldWork FieldWork { get; set; }
        public long RuleId { get; set; }
        public Rule Rule { get; set; }
        public Guid CreatedUser { get; set; }
        public DateTime CreatedTimestamp { get; set; }
    }
}
