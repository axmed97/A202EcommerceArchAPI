using Core.Utilities.Security.Hashing;

namespace NTesting
{
    [TestFixture]
    public class PasswordTest
    {
        [Test]
        public void CheckUserSuccessPassword()
        {
            HashingHelper.HashPassword("axmed123", out byte[] passwordHash, out byte[] passwordSalt);
            var data = HashingHelper.VerifyPassword("axmed123", passwordHash, passwordSalt);
            Assert.True(data);
        }
        [Test]
        public void CheckUserWrongPassword()
        {
            HashingHelper.HashPassword("axmed1234", out byte[] passwordHash, out byte[] passwordSalt);
            var data = HashingHelper.VerifyPassword("axmed123", passwordHash, passwordSalt);
            Assert.False(data);
        }
    }
}
