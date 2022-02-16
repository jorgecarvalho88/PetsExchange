using UserApi.Infrastructure.User;
using UserApi.Model;
using UserApiDto;

namespace UserApi.Service
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserDto Get(Guid uniqueId)
        {
            return ConvertToApiContractCart(_userRepository.Get(uniqueId));
        }

        public UserDto Add(UserDto user)
        {
            _userRepository.BeginTransaction();

            //validate user exists
            var existingsUser = _userRepository.Get(user.UniqueId);
            if(existingsUser is not null)
            {
                _userRepository.RollBackTransaction();
                user.Errors.Add("User already exists");
                return user;
            }

            //create new user
            var newUser = new User(user.Name, user.Email);

            //validate data is valid
            if (newUser.IsValid)
            {
                _userRepository.Create(newUser);
                _userRepository.Commit();
                _userRepository.CommitTransaction();

                user.UniqueId = newUser.UniqueId;
            }
            else
            {
                _userRepository.RollBackTransaction();
                user.Errors.AddRange(newUser.Errors);
            }
            return user;
        }

        private UserDto ConvertToApiContractCart(User user)
        {
            if (user == null)
            {
                return new UserDto() { Errors = new List<string>() { "User not found" } };
            }
            return new UserDto(user.UniqueId, user.Name, user.Email, user.Errors);
        }
    }
}
