using System;

namespace Domain.Enum
{
    public enum ToiletType
    {
        FLUSH_TOILET_WITHIN_DWELLING = 10,
        FLUSH_TOILET_WITHIN_BUILDING = 20,
        OTHER = 40,
        NO_TOILET_AVAILABLE = 50,
        NOT_APPLICABLE = 90,
        UNKNOWN = 99
    }
}
