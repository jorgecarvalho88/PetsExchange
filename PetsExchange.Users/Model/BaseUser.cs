using Microsoft.AspNetCore.Identity;

namespace UserApi.Model
{
    public class BaseUser : IdentityUser
    {
        public int Id { get; protected set; }
        public Guid UniqueId { get; protected set; }
        public List<string> Errors { get; set; }
        public bool IsValid => !Errors.Any();

        public BaseUser()
        {
            Errors = new List<string>();
            UniqueId = Guid.NewGuid();
        }
    }
}
