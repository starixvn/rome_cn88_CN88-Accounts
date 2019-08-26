namespace CN88.Accounts.Tests.Shared.Extensions
{
    using System;
    using CN88.Accounts.Shared.Extensions;
    using Xunit;

    public class DateTimeExtensionsTests
    {
        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 0, 0)]
        [InlineData(12, 0, 0)]
        [InlineData(13, 0, 0)]
        [InlineData(23, 59, 59)]
        public void ToShortDateWithHyphen_Always_ReturnsShortDayWithSlash(int hour, int minute, int second)
        {
            // Arrange
            var dateTime = new DateTime(2016, 11, 24, hour, minute, second);
            var expectedDay = "2016-11-24";

            // Act
            var actualDay = dateTime.ToShortDateWithHyphen();

            // Assert
            Assert.Equal(expectedDay, actualDay);
        }

        [Theory]
        [InlineData(0, 0, 0, "2016-11-24 00:00:00")]
        [InlineData(1, 0, 0, "2016-11-24 01:00:00")]
        [InlineData(11, 59, 59, "2016-11-24 11:59:59")]
        [InlineData(12, 0, 0, "2016-11-24 12:00:00")]
        [InlineData(13, 0, 0, "2016-11-24 13:00:00")]
        [InlineData(23, 59, 59, "2016-11-24 23:59:59")]
        public void ToLongDateWithHyphen_Always_ReturnsShortDayWithSlash(int hour, int minute, int second, string expectedDay)
        {
            // Arrange
            var dateTime = new DateTime(2016, 11, 24, hour, minute, second);

            // Act
            var actualDay = dateTime.ToLongDateWithHyphen();

            // Assert
            Assert.Equal(expectedDay, actualDay);
        }
    }
}
