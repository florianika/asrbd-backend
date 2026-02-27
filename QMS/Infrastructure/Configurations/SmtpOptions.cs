namespace Infrastructure.Configurations
{
    public sealed class SmtpOptions
    {
        public const string SectionName = "Smtp";

        public string Host { get; init; } = "";
        public string Port { get; init; } = "587"; 
        public string Username { get; init; } = "";
        public string EncryptedPassword { get; init; } = "";
        public string EncryptionKey { get; init; } = "";
        public string EncryptionIV { get; init; } = "";
    }
}
