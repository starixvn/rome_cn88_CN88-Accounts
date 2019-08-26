namespace CN88.Accounts.Tests.Shared
{
    using System;
    using CN88.Accounts.Shared;
    using Xunit;

    public class SystemTimeTests
    {
        [Fact]
        public void Now_Always_ReturnsValueSetToNowProperty()
        {
            // Arrange
            var expectedDate = new DateTime(2019, 8, 26);
            SystemTime.Today = expectedDate;

            // Act
            var actualDate = SystemTime.Today;

            // Assert
            Assert.Equal(expectedDate, actualDate);
        }
    }
}
