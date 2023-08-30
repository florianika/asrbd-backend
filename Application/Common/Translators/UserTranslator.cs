
using Application.User;

namespace Application.Common.Translators
{
    public static class UserTranslator
    {
        public static List<UserDTO> TranslateToDTOList(List<Domain.User.User> users)
        {
            var usersDTO = new List<UserDTO>();
            foreach (var user in users)
            {
                var userDTO = TranslateToDTO(user);
                usersDTO.Add(userDTO);
            }
            return usersDTO;
        }
        public static UserDTO TranslateToDTO(Domain.User.User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                AccountRole = user.AccountRole,
                AccountStatus = user.AccountStatus,
                Email = user.Email,
                Name = user.Name,
                LastName = user.LastName
            };
        }
    }
}
