using System;

namespace Domain.Enum
{
    public enum BuildingStatus
    {
        PERMITTED = 1,
        UNDER_CONSTRUCTION = 2,
        COMPLETED = 3,
        EXISTING = 4,
        ABANDONED = 5,
        DEMOLISHED = 6,
        UNKNOWN = 9
    }
}
