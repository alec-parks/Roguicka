using Roguicka.Actors;
using Xunit;

namespace Roguicka.Tests.Actors.Test
{
    public class HeroTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void ShouldBeDead(int hp)
        {
            var sut = new Hero(1,1,hp,10,'@');

            Assert.True(sut.IsDead());
        }
    }
}
