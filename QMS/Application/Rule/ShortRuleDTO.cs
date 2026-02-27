using Domain.Enum;

namespace Application.Rule
{
    public class ShortRuleDTO
    {
        public long Id { get; set; }
        public string? LocalId { get; set; }
        public EntityType EntityType { get; set; }
        public string? NameAl { get; set; }
        public string? NameEn { get; set; }
    }
}
