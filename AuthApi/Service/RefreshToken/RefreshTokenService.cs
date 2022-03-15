using AuthApi.Infrastructure.RefreshToken;
using AuthApiContract;

namespace AuthApi.Service.RefreshToken
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private IRefreshTokenRepository _refreshTokenRepository;
        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public RefreshTokenApiContract Get(string refreshToken)
        {
            return ConvertToApiContractCart(_refreshTokenRepository.GetByRefreshToken(refreshToken));
        }

        public RefreshTokenApiContract Add(RefreshTokenApiContract refreshToken)
        {
            _refreshTokenRepository.BeginTransaction();

            var newRefreshToken = new Model.RefreshToken(refreshToken.UserUniqueId, refreshToken.JwtId);

            //validate data is valid
            if (newRefreshToken.IsValid)
            {
                _refreshTokenRepository.Create(newRefreshToken);
                _refreshTokenRepository.Commit();
                _refreshTokenRepository.CommitTransaction();

                refreshToken.RefreshToken = newRefreshToken.Token;
            }
            else
            {
                _refreshTokenRepository.RollBackTransaction();
                refreshToken.Errors.AddRange(newRefreshToken.Errors);
            }
            return refreshToken;
        }

        #region Methods
        private RefreshTokenApiContract ConvertToApiContractCart(Model.RefreshToken refreshToken)
        {
            if (refreshToken == null)
            {
                return new RefreshTokenApiContract() { Errors = new List<string>() { "Token does not exist" } };
            }
            return new RefreshTokenApiContract(refreshToken.JwtId, refreshToken.Token, refreshToken.UserId, refreshToken.Errors);
        }
        #endregion
    }
}
