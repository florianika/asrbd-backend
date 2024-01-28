using System;

namespace Domain.Enum
{
    public enum Ownership
    {
        STATE_HOUSING_OFFICE = 10,
        PRIVATE_PERSON_OR_HOUSEHOLD = 20,
        PRIVATE_COMPANY = 30,
        EX_OWNER = 40,
        LOCAL_GOVERNMENT = 50,
        OTHER = 60,
        NOT_APPLICABLE = 90,
        UNKNOWN = 99
    }
}
