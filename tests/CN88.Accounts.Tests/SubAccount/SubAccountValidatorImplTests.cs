namespace CN88.Accounts.Tests.SubAccount
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using CN88.Accounts.Shared.Resources;
    using CN88.Accounts.SubAccount;
    using CN88.Common.Models;
    using KellermanSoftware.CompareNetObjects;
    using NSubstitute;
    using Sunplus.Agent.Model.Entities;
    using Sunplus.Agent.Utilities;
    using Xunit;

    public class SubAccountValidatorImplTests
    {
        private readonly Encryptor encryptor;
        private readonly SubAccountValidatorImpl subAccountValidator;

        public SubAccountValidatorImplTests()
        {
            encryptor = Substitute.For<Encryptor>();
            subAccountValidator = new SubAccountValidatorImpl(encryptor);
        }

        [Fact]
        public void Encode_Always_CallsCfsEnCode()
        {
            //// Arrange
            var password = "pwd";

            //// Act
            subAccountValidator.Encode(password);

            //// Assert
            encryptor.Received().CfsEnCode(password);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void ValidateEmptyPassword_PasswordIsNullOrWhiteSpace_ReturnsErrorResult(string pwd)
        {
            //// Arrange
            var expectedResult = Result.CreateErrorResult(message: Resources.RequiredPasswordError);

            //// Act
            var actualResult = subAccountValidator.ValidateEmptyPassword(pwd);

            //// Assert
            Assert.True(new CompareLogic().Compare(expectedResult, actualResult).AreEqual);
        }

        [Fact]
        public void ValidateEmptyPassword_PasswordIsNOTNullOrWhiteSpace_ReturnsErrorResult()
        {
            //// Arrange
            var pwd = "pwd";
            var expectedResult = Result.CreateSuccessResult();

            //// Act
            var actualResult = subAccountValidator.ValidateEmptyPassword(pwd);

            //// Assert
            Assert.True(new CompareLogic().Compare(expectedResult, actualResult).AreEqual);
        }

        [Theory]
        [InlineData("BCD!$#H")]
        [InlineData("4593abcd")]
        public void IsMatchPasswordRules_PasswordIsEqualsPasswordHint_ReturnsErrorResult(string pwd)
        {
            //// Arrange
            var expectedResult = Result.CreateErrorResult(message: Resources.PasswordWrongRuleError);

            //// Act
            var actualResult = subAccountValidator.IsMatchPasswordRules(pwd, Enumerable.Empty<string>());

            //// Assert
            Assert.True(new CompareLogic().Compare(expectedResult, actualResult).AreEqual);
        }

        [Fact]
        public void IsMatchPasswordRules_PasswordIsContainsInvalidChars_ReturnsErrorResult()
        {
            //// Arrange
            var expectedResult = Result.CreateErrorResult(message: Resources.NewPwdContainsUserNameOrNickNameError);

            //// Act
            var actualResult = subAccountValidator.IsMatchPasswordRules("abc", new List<string> { "b" });

            //// Assert
            Assert.True(new CompareLogic().Compare(expectedResult, actualResult).AreEqual);
        }

        [Fact]
        public void IsMatchPasswordRules_PasswordIsContainsWhiteSpace_ReturnsErrorResult()
        {
            //// Arrange
            var expectedResult = Result.CreateErrorResult(message: Resources.PasswordWrongRuleError);

            //// Act
            var actualResult = subAccountValidator.IsMatchPasswordRules("1234 aa", Enumerable.Empty<string>());

            //// Assert
            Assert.True(new CompareLogic().Compare(expectedResult, actualResult).AreEqual);
        }

        [Fact]
        public void IsMatchPasswordRules_ValidateSuccess_ReturnsSuccessResult()
        {
            //// Arrange
            var expectedResult = Result.CreateSuccessResult();

            //// Act
            var actualResult = subAccountValidator.IsMatchPasswordRules("1234aa", Enumerable.Empty<string>());

            //// Assert
            Assert.True(new CompareLogic().Compare(expectedResult, actualResult).AreEqual);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void FilterWithValidPermissions_IsNullOrWhiteSpace_ReturnsCorrectResult(string pageAccess)
        {
            //// Arrange
            var expectedResult = Enumerable.Empty<string>();

            //// Act
            var actualResult = subAccountValidator.FilterWithValidPermissions(pageAccess);

            //// Assert
            Assert.True(new CompareLogic().Compare(expectedResult, actualResult).AreEqual);
        }

        [Fact]
        public void GetAccountManagementPermission_UpdateAccount_ReturnsCorrectResult()
        {
            //// Arrange
            var expectedResult = new List<string>
            {
                PageAccessType.ViewMemberInfo,
                PageAccessType.UpdateAccount
            };

            //// Act
            var actualResult = subAccountValidator.GetAccountManagementPermission("[B]").ToList();

            //// Assert
            Assert.True(new CompareLogic().Compare(expectedResult, actualResult).AreEqual);
        }

        [Fact]
        public void GetAccountManagementPermission_ViewMemberInfo_ReturnsCorrectResult()
        {
            //// Arrange
            var expectedResult = new List<string>
            {
                PageAccessType.ViewMemberInfo
            };

            //// Act
            var actualResult = subAccountValidator.GetAccountManagementPermission("[H]").ToList();

            //// Assert
            Assert.True(new CompareLogic().Compare(expectedResult, actualResult).AreEqual);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("abc")]
        public void ValidateSuffix_suffix1IsInvalid_ReturnsForbiddenResult(string suffix1)
        {
            //// Arrange
            var expectedResult = new Result((int)HttpStatusCode.Forbidden, Resources.NotAuthorized);

            //// Act
            var actualResult = subAccountValidator.ValidateSuffix(suffix1, "1");

            //// Assert
            Assert.True(new CompareLogic().Compare(expectedResult, actualResult).AreEqual);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("abc")]
        public void ValidateSuffix_suffix2IsInvalid_ReturnsForbiddenResult(string suffix2)
        {
            //// Arrange
            var expectedResult = new Result((int)HttpStatusCode.Forbidden, Resources.NotAuthorized);

            //// Act
            var actualResult = subAccountValidator.ValidateSuffix("1", suffix2);

            //// Assert
            Assert.True(new CompareLogic().Compare(expectedResult, actualResult).AreEqual);
        }

        [Fact]
        public void ValidateSuffix_ValidateSuccess_ReturnsForbiddenResult()
        {
            //// Arrange
            var expectedResult = Result.CreateSuccessResult();

            //// Act
            var actualResult = subAccountValidator.ValidateSuffix("1", "2");

            //// Assert
            Assert.True(new CompareLogic().Compare(expectedResult, actualResult).AreEqual);
        }
    }
}