using PetsExchangeApi.DTO;
using UserApiContract;

namespace PetsExchangeApi.Extensions
{
    public static class UserExtension
    {
        public static UserDto? ToDto(this UserContract user)
        {
            if (user is null)
            {
                return null;
            }

            return new UserDto(
                user.UniqueId, 
                user.FirstName, 
                user.LastName, 
                user.Email, 
                user.MobileNumber, 
                user.Address, 
                user.PostCode, 
                user.City, 
                user.DateOfBirth,
                user.SitterProfileId, 
                user.ProfilePhotoUrl, 
                user.Errors);
        }
    }
}
