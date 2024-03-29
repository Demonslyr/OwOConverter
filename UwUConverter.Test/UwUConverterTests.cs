using Microsoft.AspNetCore.Mvc.Testing;
using UwUConverter.StartupHelpers;
using UwUConverter.StringExtensions;

namespace UwUConverter.Tests
{
    public class UwUConverterTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public UwUConverterTests(WebApplicationFactory<Program> factory)
        {
            // Arrange
            _client = factory.CreateClient();
        }

        [Fact]
        public void TestUwUConversionWithNoFace()
        {
            const string input = "Hello, world";
            const string expected = "Hewwo, wowwd";

            var actual = input.ConvertToUwU();

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void TestUwUFace()
        {
            const string input = "!";

            var actual = input.ConvertToUwU();

            Assert.Contains(actual.Trim(), StringExtensions.UwUConverter.Faces);
        }
        [Fact]
        public async Task TestRootEndpoint()
        {
            // Act
            var response = await _client.GetAsync("/");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            const string expectedSubstringStart = @"Send a stwing in the uww ";
            const string expectedSubstringEnd = @" Wike ""/hewwo""";
            Assert.StartsWith(expectedSubstringStart, responseString);
            Assert.EndsWith(expectedSubstringEnd, responseString);
            Assert.Contains(StringExtensions.UwUConverter.Faces, f => responseString.Contains(f));
        }

        [Fact]
        public async Task TestVariableEndpoint()
        {
            // Act
            var response = await _client.GetAsync("/hello");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            const string expected = "hello";
            Assert.Equal(expected.ConvertToUwU(), responseString);
        }
    }
}