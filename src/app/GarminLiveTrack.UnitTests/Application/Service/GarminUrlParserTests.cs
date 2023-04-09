using GarminLiveTrack.Web.Application.Service;
using Xunit;

namespace GarminLiveTrack.UnitTests.Application.Service
{
    public class GarminUrlParserTests
    {
        private readonly GarminUrlParser _sut;

        public GarminUrlParserTests()
        {
            _sut = new GarminUrlParser();
        }

        [Fact]
        public void Should_FindUrl_When_MessageIsValid()
        {
            const string expectedUrl = @"https://livetrack.garmin.com/session/6fd044a1-3f42-44e2-bf4c-bd2eeb97e9f3/token/ECA9E5ACEEC12E97D19343310831E62";
            var email = GarminLiveTrackEmail.Get();
            var parsedUrl = _sut.GetLiveTrackUrl(email);

            Assert.Equal(expectedUrl, parsedUrl);
        }

        [Fact]
        public void Should_NotFindUrl_When_MessageIsNotValid()
        {
            const string email = "bla bla bla invalid email";
            var parsedUrl = _sut.GetLiveTrackUrl(email);

            Assert.Equal(string.Empty, parsedUrl);
        }
    }
}
