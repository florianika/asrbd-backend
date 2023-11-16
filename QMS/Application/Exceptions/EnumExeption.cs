namespace Application.Exceptions
{
#nullable disable
    public class EnumExeption : Exception
    {
        public List<string> Errors { get; }
        public EnumExeption() : base() { }
        public EnumExeption(string message) : base(message) { }
        public EnumExeption(string message, Exception innerException) : base(message, innerException) { }
        public EnumExeption(List<string> errors) : base("One or more validation errors occurred.")
        {
            Errors = errors;
        }
    }
}
