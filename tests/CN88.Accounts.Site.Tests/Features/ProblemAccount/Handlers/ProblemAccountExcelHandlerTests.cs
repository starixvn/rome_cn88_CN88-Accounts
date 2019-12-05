namespace CN88.Accounts.Site.Tests.Features.ProblemAccount.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using AutoFixture;
    using CN88.Accounts.ProblemAccount;
    using CN88.Accounts.ProblemAccount.Params;
    using CN88.Accounts.Shared;
    using CN88.Accounts.Shared.Extensions;
    using CN88.Accounts.Shared.Models;
    using CN88.Accounts.Shared.Resources;
    using CN88.Accounts.Site.Features.ProblemAccount.Handler;
    using CN88.Accounts.Site.Features.ProblemAccount.Requests;
    using CN88.Accounts.Site.Features.Shared.ViewModels;
    using CN88.Accounts.Site.Tests.Fixtures;
    using KellermanSoftware.CompareNetObjects;
    using NSubstitute;
    using ONEbook.SpreadSheets;
    using Xunit;

    [Collection("UserCollection")]
    public class ProblemAccountExcelHandlerTests
    {
        private readonly IFixture fixture;
        private readonly UserFixture userFixture;
        private readonly ProblemAccountService problemAccountService;
        private readonly Workbook<ProblemAccount> workbook;
        private readonly ProblemAccountExcelQuery message;
        private readonly ProblemAccountExcelHandler problemAccountExcelHandler;

        public ProblemAccountExcelHandlerTests(UserFixture userFixture)
        {
            this.userFixture = userFixture;
            fixture = new Fixture();
            problemAccountService = Substitute.For<ProblemAccountService>();
            var problemAccounts = new List<ProblemAccount>
            {
                new ProblemAccount(1,"UplineUsername","Username","Name","Nickname",5,1,DateTime.Now,false)
            };
            problemAccountService.GetProblemAccounts(Arg.Any<GetProblemAccountsParam>())
                .Returns(new PagedData<ProblemAccount>(problemAccounts, 1, 10, 0));

            workbook = Substitute.For<Workbook<ProblemAccount>>();
            InitWorkbook();

            message = new ProblemAccountExcelQuery(userFixture.User)
            {
                CustLevelId = 3
            };

            problemAccountExcelHandler = new ProblemAccountExcelHandler(problemAccountService, workbook)
            {
                MapPath = (path) => path,
                IsFileExist = (path) => true
            };
        }

        private void InitWorkbook()
        {
            workbook.UseTemplate(Arg.Any<TemplateInfo>()).Returns(workbook);
            workbook.Title(Arg.Any<string>()).Returns(workbook);
            workbook.Header(Arg.Any<Func<string, string>>()).Returns(workbook);
            workbook.Body(Arg.Any<IEnumerable<ProblemAccount>>(), Arg.Any<Mapper<ProblemAccount>>()).Returns(workbook);
            workbook.Footer(Arg.Any<ProblemAccount>(), Arg.Any<Mapper<ProblemAccount>>()).Returns(workbook);
        }

        [Fact]
        public void Handle_Always_CallsGetProblemAccounts()
        {
            // Act
            problemAccountExcelHandler.Handle(message).ConfigureAwait(false);

            // Assert
            problemAccountService.Received().GetProblemAccounts(
                Arg.Is<GetProblemAccountsParam>(
                    p => p.CustId == message.ViewerId
                        && p.SubAccId == message.SubAccountId
                        && p.IsAllDownline == message.IsAllDownline
                        && p.UplineUsername == message.UplineUsername
                        && p.CustLevelId == message.CustLevelId
                        && p.SearchUsername == message.CustName
                        && p.ReviewStatus == message.SelectedReviewStatus
                        && p.FromDate == message.FromDate
                        && p.ToDate == message.ToDate
                        && p.PageIndex == message.PageIndex
                        && p.PageSize == message.PageSize));
        }

        [Fact]
        public void Handle_Workbook_UseTemplate()
        {
            // Act
            problemAccountExcelHandler.Handle(message).ConfigureAwait(false);

            // Assert
            workbook.Received().UseTemplate(Arg.Is<TemplateInfo>(
                t => t.TemplatePath == $"~/Features/ProblemAccount/ExcelTemplates/{message.CustLevel.DisplayName}.xls"
                && t.NoInfomationText == Resources.NoInformationIsAvailable
                && t.TitleStartIndex == -1 - 1
                && t.HeaderStartIndex == 1 - 1
                && t.BodyStartIndex == 2 - 1
                && t.BodyEndIndex == 3 - 1));
        }

        [Fact]
        public void Handle_Workbook_Header()
        {
            // Act
            problemAccountExcelHandler.Handle(message).ConfigureAwait(false);

            // Assert
            workbook.Received().Header(Arg.Is<Func<string, string>>(f => f == Resources.ResourceManager.GetString));
        }

        [Fact]
        public void Handle_Workbook_Body()
        {
            // Act
            problemAccountExcelHandler.Handle(message).ConfigureAwait(false);

            // Assert
            workbook.Received().Body(Arg.Any<IEnumerable<ProblemAccount>>(), Arg.Any<Mapper<ProblemAccount>>());
        }

        [Fact]
        public void Handle_Workbook_CallExportToBytes()
        {
            // Act
            problemAccountExcelHandler.Handle(message).ConfigureAwait(false);

            // Assert
            workbook.Received().ExportToBytes();
        }

        [Fact]
        public void Handle_Always_ReturnExcelViewModel()
        {
            // Arrange
            message.FromDate = DateTime.Now;
            message.ToDate = DateTime.Now;
            var title = $"{Resources.ProblemAccount} {message.FromDate.ToShortDateWithHyphen()} --> {message.ToDate.ToShortDateWithHyphen()} - {Resources.ResourceManager.GetString(message.CustLevel.DisplayName)}";
            var excelFileContent = fixture.CreateMany<byte>().ToArray();
            workbook.ExportToBytes().Returns(excelFileContent);
            var expectedViewModel = new ExcelFileViewModel(title, excelFileContent);

            // Act
            var actualViewModel = problemAccountExcelHandler.Handle(message).GetAwaiter().GetResult();

            // Assert
            Assert.True(new CompareLogic().Compare(actualViewModel, expectedViewModel).AreEqual);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void GetExcelTemplatePath_TemplateNameIsNullOrWhiteSpace_ThrowsArgumentNullException(string templateName)
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => problemAccountExcelHandler.GetExcelTemplatePath(templateName));
        }

        [Fact]
        public void GetExcelTemplatePath_FileIsNotExist_ThrowsArgumentNullException()
        {
            // Arrange
            problemAccountExcelHandler.IsFileExist = (path) => false;

            // Act & Assert
            Assert.Throws<FileNotFoundException>(() => problemAccountExcelHandler.GetExcelTemplatePath("Agent"));
        }

        [Fact]
        public void GetBodyRowMapper_MasterLevel_ReturnsCorrectMapper()
        {
            // Arrange
            SystemTime.Today = new DateTime(2019, 8, 29);
            message.CustLevelId = 3;
            var data = new ProblemAccount(1, "UplineUsername", "Username", "Name", "Nickname", 5, 1, SystemTime.Today, false);

            // Act
            var actualMapper = problemAccountExcelHandler.GetBodyRowMapper(message);

            // Assert
            Assert.True((int)actualMapper.Mappings[Column.A](data) == 1
                && (string)actualMapper.Mappings[Column.B](data) == "Username"
                && (string)actualMapper.Mappings[Column.C](data) == "Name"
                && (string)actualMapper.Mappings[Column.D](data) == "Nickname"
                && (int)actualMapper.Mappings[Column.E](data) == 5
                && (string)actualMapper.Mappings[Column.F](data) == "Inactive"
                && (DateTime)actualMapper.Mappings[Column.G](data) == SystemTime.Today
                && (string)actualMapper.Mappings[Column.H](data) == "Not Reviewed");
        }

        [Fact]
        public void GetBodyRowMapper_AgentLevel_ReturnsCorrectMapper()
        {
            // Arrange
            SystemTime.Today = new DateTime(2019, 8, 29);
            message.CustLevelId = 2;
            var data = new ProblemAccount(1, "UplineUsername", "Username", "Name", "Nickname", 5, 1, SystemTime.Today, false);

            // Act
            var actualMapper = problemAccountExcelHandler.GetBodyRowMapper(message);

            // Assert
            Assert.True((int)actualMapper.Mappings[Column.A](data) == 1
                 && (string)actualMapper.Mappings[Column.B](data) == "UplineUsername"
                && (string)actualMapper.Mappings[Column.C](data) == "Username"
                && (string)actualMapper.Mappings[Column.D](data) == "Name"
                && (string)actualMapper.Mappings[Column.E](data) == "Nickname"
                && (int)actualMapper.Mappings[Column.F](data) == 5
                && (string)actualMapper.Mappings[Column.G](data) == "Inactive"
                && (DateTime)actualMapper.Mappings[Column.H](data) == SystemTime.Today
                && (string)actualMapper.Mappings[Column.I](data) == "Not Reviewed");
        }

        [Fact]
        public void GetBodyRowMapper_MemberLevel_ReturnsCorrectMapper()
        {
            // Arrange
            SystemTime.Today = new DateTime(2019, 8, 29);
            message.CustLevelId = 1;
            var data = new ProblemAccount(1, "UplineUsername", "Username", "Name", "Nickname", 5, 1, SystemTime.Today, false);

            // Act
            var actualMapper = problemAccountExcelHandler.GetBodyRowMapper(message);

            // Assert
            Assert.True((int)actualMapper.Mappings[Column.A](data) == 1
                && (string)actualMapper.Mappings[Column.B](data) == "UplineUsername"
                && (string)actualMapper.Mappings[Column.C](data) == "Username"
                && (string)actualMapper.Mappings[Column.D](data) == "Name"
                && (string)actualMapper.Mappings[Column.E](data) == "Nickname"
                && (string)actualMapper.Mappings[Column.F](data) == "Inactive"
                && (DateTime)actualMapper.Mappings[Column.G](data) == SystemTime.Today
                && (string)actualMapper.Mappings[Column.H](data) == "Not Reviewed");
        }
    }
}