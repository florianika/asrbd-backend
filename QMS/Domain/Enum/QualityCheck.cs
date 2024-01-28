using System;

namespace Domain.Enum
{
    public enum QualityCheck
    {
        DELETED_DATA = 0,
        ERROR_FREE_DATA = 1,
        DATA_GAPS = 2,
        INCOSISTENT_DATA = 3,
        UNTESTED_DATA = 9
    }
}
