using NUnit.Framework;
using System;

namespace Pawotter.API.Tests
{
    [TestFixture]
    public class Test
    {
        [TestCase("pawotter://home", null)]
        [TestCase("pawotter://home?key=value", null)]
        [TestCase("pawotter://authorize?code=abcd1234", "abcd1234")]
        public void GetOAuthAuthorizeUriTest(string uri, string code)
        {
            Assert.AreEqual(code, MastodonOAuthClient.GetRefreshTokenFromRedirectUri(new Uri(uri)));
        }
    }
}
