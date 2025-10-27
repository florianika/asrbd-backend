
namespace Domain
{
    public class PasswordResetToken
    {
        public long Id { get; set; }
        public Guid UserId { get; set; }
        public string TokenHash { get; set; } = default!;
        public DateTime ExpiresAt { get; set; }
        public DateTime? ConsumedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
