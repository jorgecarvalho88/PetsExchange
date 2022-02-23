using UserApi.Infrastructure.User;
using UserApi.Model;
using UserApiContract;

namespace UserApi.Service
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserContract Get(Guid uniqueId)
        {
            return ConvertToApiContractCart(_userRepository.Get(uniqueId));
        }

        public UserContract Add(UserContract user)
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

        public UserContract Update(UserContract user)
        {
            _userRepository.BeginTransaction();

            var existingUser = _userRepository.Get(user.UniqueId);
            if(existingUser is null)
            {
                _userRepository.RollBackTransaction();
                user.Errors.Add("Invalid UniqueId");
                return user;
            }

            existingUser.SetName(user.Name);
            existingUser.SetEmail(user.Email);

            if(!existingUser.IsValid)
            {
                _userRepository.RollBackTransaction();
                user.Errors.AddRange(existingUser.Errors);
                return user;
            }

            _userRepository.Update(existingUser);
            _userRepository.Commit();
            _userRepository.CommitTransaction();
            return user;
        }

        public UserContract Delete(Guid uniqueId)
        {
            _userRepository.BeginTransaction();

            var existingUser = _userRepository.Get(uniqueId);
            if( existingUser is null)
            {
                _userRepository.RollBackTransaction();
                return new UserContract()
                {
                    Errors = new List<string>() { "Invalid UniqueId" }
                };
            }

            var user = ConvertToApiContractCart(existingUser);

            _userRepository.Delete(existingUser);
            _userRepository.Commit();
            _userRepository.CommitTransaction();
            return user;
        }


        #region Methods
        private UserContract ConvertToApiContractCart(User user)
        {
            if (user == null)
            {
                return new UserContract() { Errors = new List<string>() { "User not found" } };
            }
            return new UserContract(user.UniqueId, user.Name, user.Email, user.Errors);
        }
        #endregion
    }
}
