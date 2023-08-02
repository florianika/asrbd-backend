namespace Domain.Claim
{
    public class Claim
    {
        public int ClaimId { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }

        public Guid UserId { get; set; }//foreign key to user
        public Domain.User.User User { get; set; } //navigation property to User
    }
}
