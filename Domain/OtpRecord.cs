namespace Domain
{
    public class OtpRecord
    {
        public long Id { get; set; }
        public Guid UserId { get; set; }
        public string CodeHash { get; set; } = default!;
        public DateTimeOffset ExpiresAt { get; set; }
        public DateTimeOffset? ConsumedAt { get; set; }
        public int Attempts { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
