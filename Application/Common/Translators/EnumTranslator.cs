using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Translators
{
    public static class EnumTranslator
    {
        public static string TranslateAccountRole(AccountRole accountRole)
        {
            switch (accountRole)
            {
                case AccountRole.ADMIN:
                    return "Administrator";
                case AccountRole.SUPERVISOR:
                    return "Supervisor";
                case AccountRole.ENUMERATOR:
                    return "Enumerator";
                case AccountRole.CLIENT:
                    return "Client";
                case AccountRole.PUBLISHER:
                    return "Publisher";
                case AccountRole.USER:
                    return "User";
                default:
                    throw new ArgumentOutOfRangeException(nameof(accountRole), accountRole, "Unsupported AccountRole");
            }
        }
        public static string TranslateAccountStatus(AccountStatus accountStatus)
        {
            switch (accountStatus)
            {
                case AccountStatus.ACTIVE:
                    return "Active";
                case AccountStatus.LOCKED:
                    return "Locked";
                case AccountStatus.TERMINATED:
                    return "Terminated";
                case AccountStatus.UNCONFIRMED:
                    return "Unconfirmed";
                case AccountStatus.AUTOMATIC:
                    return "Automatic";
                default:
                    throw new ArgumentOutOfRangeException(nameof(accountStatus), accountStatus, "Unsupported AccountStatus");
            }
        }
    }
}
