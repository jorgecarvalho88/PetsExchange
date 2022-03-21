using PetsExchangeApi.DTO;
using PetsExchangeApi.Extensions;
using UserApiClient;
using UserApiContract;

namespace PetsExchangeApi.Service.User
{
    public class UserService : IUserService
    {
        private readonly IUserApiClient _userApiClient;

        public UserService(IUserApiClient userApiClient)
        {
            _userApiClient = userApiClient;
        }
        public async Task<UserDto?> Get(Guid uniqueId)
        {
            var user = (await _userApiClient.GetUser(uniqueId)).Body;

            if(user is not null)
            {
                return user.ToDto();
            }

            return null;
        }

        public async Task<UserDto> Add(UserDto user)
        {
            if(user.IsValid)
            {
                var newUser = (await _userApiClient.CreateUser(
                    new UserContract(user.UniqueId,
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
                user.Errors))).Body;

                user.Errors.AddRange(newUser.Errors);
                user.UniqueId = newUser.UniqueId;
            }

            return user;
        }

        public async Task<UserDto> Update(UserDto user)
        {
            var updatedUser = (await _userApiClient.UpdateUser(
                new UserContract(user.UniqueId,
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
                user.Errors))).Body;

            user.Errors.AddRange(updatedUser.Errors);
            user.UniqueId = updatedUser.UniqueId;

            return user;
        }

        public async Task<UserDto> Delete(Guid uniqueId)
        {
            var deletedUser = (await _userApiClient.DeleteUser(uniqueId)).Body;

            return new UserDto(
                deletedUser.UniqueId,
                deletedUser.FirstName,
                deletedUser.LastName,
                deletedUser.Email,
                deletedUser.MobileNumber,
                deletedUser.Address,
                deletedUser.PostCode,
                deletedUser.City,
                deletedUser.DateOfBirth,
                deletedUser.SitterProfileId,
                deletedUser.ProfilePhotoUrl,
                deletedUser.Errors);
        }
    }
}
