namespace CN88.Accounts.Site.Tests.Features.Shared.Helpers
{
    using System;
    using System.Collections.Generic;
    using CN88.Accounts.Site.Features.Shared.Helpers;
    using NSubstitute;

    public static class UrlHelperTestSetUp
    {
        public static void SetUp()
        {
            UrlHelper.MapFeaturesPath = Substitute.For<Func<string, string>>();
            UrlHelper.MapFeaturesPath(Arg.Any<string>()).Returns("FeaturesPath");

            UrlHelper.AddQueryStringParamsFunc = Substitute.For<Func<string, IDictionary<string, object>, string>>();
            UrlHelper.ReplaceOrAddQueryStringParamsFunc = Substitute.For<Func<string, IDictionary<string, object>, string>>();
        }
    }
}
