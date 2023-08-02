namespace Domain.RefreshToken
{
    public class RefreshToken
    {
        public int RefreshTokenId { get; set; }
        public string Value { get; set; }
        public bool Active { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Guid UserId { get; set; }//foreign key to user
        public Domain.User.User User { get; set; } //navigation property to User
    }
}
