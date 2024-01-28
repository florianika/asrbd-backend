using System;

namespace Application.Quality
{
    public abstract class BaseDto : IDto
    {
        public string? ObjectId { get; set; }
        public Guid GlobalId { get; set; }
        public string? Created_user { get; set; }
        public DateTime Created_date { get; set; }
        public string? Last_edited_user { get; set; }
        public DateTime Last_edited_date { get; set; }
    }
}
