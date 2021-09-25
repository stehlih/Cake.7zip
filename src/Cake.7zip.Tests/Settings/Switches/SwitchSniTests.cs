using Cake.SevenZip.Switches;
using Cake.SevenZip.Tests.Fixtures;

using Shouldly;

using Xunit;

namespace Cake.SevenZip.Tests.Settings.Switches
{
    public class SwitchSniTests
    {
        [Fact]
        public void Sni_set_outputs_switch()
        {
            var fixture = new SevenZipSettingsFixture();
            var sut = new SwitchNtSecurityInformation(true);
            const string expected = "-sni";

            var actual = fixture.Parse(b => sut.BuildArguments(ref b));

            actual.ShouldBe(expected);
        }

        [Fact]
        public void Sni_not_set_outputs_nothing()
        {
            var fixture = new SevenZipSettingsFixture();
            var sut = new SwitchNtSecurityInformation(false);
            const string expected = "";

            var actual = fixture.Parse(b => sut.BuildArguments(ref b));

            actual.ShouldBe(expected);
        }
    }
}
