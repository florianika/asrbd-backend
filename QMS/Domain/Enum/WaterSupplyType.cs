using System;

namespace Domain.Enum
{
    public enum WaterSupplyType
    {
        PIPED_WATER_WITHIN_DWELLING = 10,
        PIPED_WATER_WITHIN_BUILDING = 20,
        OTHER = 40,
        NO_WATER_SUPLY = 50,
        NOT_APPLICABLE = 90,
        UNKNOWN = 99
    }
}
