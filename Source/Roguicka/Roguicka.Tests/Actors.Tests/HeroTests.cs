using Roguicka.Actors;
using Xunit;

namespace Roguicka.Tests.Actors.Tests
{
    public class HeroTests
    {
        [Fact]
        public void ShouldNotBeDead()
        {
            var sut = new Hero(1,1,10,10,'@');

            Assert.False(sut.IsDead);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void ShouldBeDead(int hp)
        {
            var sut = new Hero(1, 1, hp, 10, '@');

            Assert.True(sut.IsDead);
        }

        [Theory]
        [InlineData(2, 5, 7)]
        [InlineData(10, 10, 20)]
        public void ShouldHeal(int start, int heal, int result)
        {
            var sut = new Hero(1, 1, start, start + heal, '@');
            sut.Heal(heal);
            Assert.Equal(result,sut.CurrentHP);
        }

        [Theory]
        [InlineData(2, 10, 10)]
        [InlineData(10, 100000, 10)]
        public void ShouldNotHealAboveMaxHp(int start, int heal, int max)
        {
            var sut = new Hero(1, 1, start, max, '@');
            sut.Heal(heal);
            Assert.Equal(max,sut.CurrentHP);
        }
    }
}
