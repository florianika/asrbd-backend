
namespace Application.Enums
{
    enum ErrorCodes
    {
        AnUnexpectedErrorOcurred = 100,
        CredentialsAreNotValid = 101,
        AccessTokenIsNotValid = 102,
        RefreshTokenIsNotActive = 103,
        RefreshTokenHasExpired = 104,
        RefreshTokenIsNotCorrect = 105,
        UserDoesNotExist = 106,
        AccountStatusNotActive = 107,
        AccountRoleIsNotCorrect = 108,
        EntityTypeIsNotCorrect = 109,
        PermissionIsNotCorrect = 110,
        PermissionRoleNotExist = 111,
        BadRequest = 112
    }
}
