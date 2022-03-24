using DataExtension;
using Validations;

namespace AuthApi.Model
{
    public class RefreshToken : BaseEntity
    {
        public RefreshToken()
        { }

        public RefreshToken(
            string userId,
            string jwtId
            )
        {
            SetUserId(userId);
            SetJwtId(jwtId);
            SetAddedDate();
            SetExpDate();
            SetToken();
        }

        public string UserId { get; protected set; }
        public string Token { get; protected set; }
        public string JwtId { get; protected set; }
        public bool IsUsed { get; protected set; }
        public bool IsRevoked { get; protected set; }
        public DateTime AddedDate { get; protected set; }
        public DateTime ExpDate { get; protected set; }

        private void SetExpDate()
        {
            ExpDate = DateTime.UtcNow.AddMonths(6);
        }

        private void SetAddedDate()
        {
            AddedDate = DateTime.Now;
        }

        private void SetJwtId(string jwtId)
        {
            JwtId = jwtId;
        }

        public void SetToken()
        {
            Token = RandomString(35) + Guid.NewGuid();
        }

        public void SetIsUsed()
        {
            IsUsed = true;
        }

        private void SetUserId(string userId)
        {
            UserId = userId;
        }

        #region Métodos
        private string RandomString(int length)
        {
            var random = new Random();
            var chars = "abcdefghijklmnopqrstuvwxyz0123456789";

            return new string(Enumerable.Repeat(chars, length).Select(x => x[random.Next(x.Length)]).ToArray());
        }
        #endregion
    }
}
