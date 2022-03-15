using DataExtension;
using Validations;

namespace AuthApi.Model
{
    public class RefreshToken : BaseEntity
    {
        public RefreshToken()
        { }

        public RefreshToken(
            Guid userId,
            string jwtId
            )
        {
            SetUserId(userId);
            SetToken();
            SetJwtId(jwtId);
            SetAddedDate();
            SetExpDate();
        }

        public Guid UserId { get; set; }
        public string Token { get; set; }
        public string JwtId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime ExpDate { get; set; }

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

        private void SetToken()
        {
            Token = RandomString(35) + Guid.NewGuid();
        }

        private void SetUserId(Guid userId)
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
