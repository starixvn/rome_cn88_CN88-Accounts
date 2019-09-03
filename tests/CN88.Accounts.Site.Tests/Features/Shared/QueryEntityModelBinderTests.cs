using System;
using System.Web.Mvc;
using CN88.Accounts.Site.Features.ProblemAccount.Requests;
using CN88.Accounts.Site.Features.Shared;
using Xunit;

namespace CN88.Accounts.Site.Tests.Features.Shared
{
    public class QueryEntityModelBinderTests : QueryEntityModelBinder
    {
        protected override object CreateInstanceOfAssignableBaseQuery(Type modelType)
        {
            return "CreateInstanceOfAssignableBaseQuery";
        }

        protected override object CallBaseCreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            return "CallBaseCreateModel";
        }

        [Fact]
        public void CreateModel_ConstructorParamIsAssignableFromBaseQuery_CallCreateInstanceOfAssignableBaseQuery()
        {
            // Arrange
            var problemAccountQuery = typeof(ProblemAccountQuery);

            // Act
            var createdInstance = CreateModel(null, null, problemAccountQuery);

            // Assert
            Assert.Equal("CreateInstanceOfAssignableBaseQuery", createdInstance);
        }

        [Fact]
        public void CreateModel_ConstructorParamIsNotAssignableFromReportBaseQuery_CallBaseCreateModel()
        {
            // Arrange
            var userType = typeof(User);

            // Act
            var createdInstance = CreateModel(null, null, userType);

            // Assert
            Assert.Equal("CallBaseCreateModel", createdInstance);
        }
    }
}
