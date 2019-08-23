using System.Collections.Specialized;
using System.Configuration;
using CN88.Accounts.Shared;
using Xunit;

namespace CN88.Accounts.Tests.Shared
{
    public class AppSettingsTests
    {
        [Fact]
        public void Verify_MissingStoredProcedurePath_ThrowsConfigurationErrorsException()
        {
            // Arrange
            AppSettings.SettingValues = new NameValueCollection();

            // Act & Assert
            Assert.Throws<ConfigurationErrorsException>(() => AppSettings.Verify());
        }

        [Fact]
        public void Verify_HasEnoughSettings_PassVerify()
        {
            // Arrange
            AppSettings.SettingValues = new NameValueCollection
            {
                { "StoredProcedurePath", "Local" }
            };
            var passVerify = false;

            // Act
            AppSettings.Verify();
            passVerify = true;

            // Assert
            Assert.True(passVerify);
        }
    }
}
