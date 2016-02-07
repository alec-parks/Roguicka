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
            Assert.Equal(result,sut.CurrentHp);
        }

        [Theory]
        [InlineData(2, 10, 10)]
        [InlineData(10, 100000, 10)]
        public void ShouldNotHealAboveMaxHp(int start, int heal, int max)
        {
            var sut = new Hero(1, 1, start, max, '@');
            sut.Heal(heal);
            Assert.Equal(max,sut.CurrentHp);
        }

        [Theory]
        [InlineData(1, -1, 1)]
        public void ShouldNotHealNegativeValues(int start, int heal, int result)
        {
            var sut = new Hero(1, 1, start, result, '@');
            sut.Heal(heal);
            Assert.Equal(result,sut.CurrentHp);
        }

        [Fact]
        public void ShouldBeHero()
        {
            var sut = new Hero();
            Assert.Equal(ActorType.Player,sut.Type);
        }

        [Fact]
        public void ShouldBlock()
        {
            var sut = new Hero();
            Assert.True(sut.Blocks);
        }

        [Fact]
        public void ShouldHaveColor()
        {
            var sut = new Hero();
            Assert.NotNull(sut.Color);
        }

        [Fact]
        public void ShouldHaveSymbol()
        {
            var sut = new Hero();
            Assert.Equal('@',sut.Symbol);
        }

        [Fact]
        public void ShouldHaveLight()
        {
            var sut = new Hero();
            Assert.InRange(sut.LightRadius,1,50);
        }

        [Theory]
        [InlineData(10, 1, 9)]
        [InlineData(10, 0, 10)]
        [InlineData(10, 10, 0)]
        [InlineData(10, 60, -50)]
        [InlineData(10, -60, 10)]
        public void ShouldTakeDamage(int hp, int dam, int result)
        {
            var sut = new Hero(1,1,hp,hp,'@');
            sut.TakeDamage(dam);
            Assert.Equal(result,sut.CurrentHp);
        }
    }
}
