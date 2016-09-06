using RLNET;
using Roguicka.Actors;
using Xunit;

namespace Roguicka.Tests.Actors.Tests
{
    public class MonsterTest
    {
        [Fact]
        public void DeathShouldChangeSymbol()
        {
            var sut = new Monster();

            sut.TakeDamage(10);

            Assert.NotEqual('T',sut.Symbol);
        }

        [Theory]
        [InlineData(10, 10, 0)]
        [InlineData(1, 20, -19)]
        [InlineData(10, 1, 9)]
        public void ShouldTakeDamage(int hp, int damage, int result)
        {
            var sut = new Monster(hp, 1, 1, RLColor.Black, 'D', true);

            sut.TakeDamage(damage);

            Assert.Equal(result, sut.CurrentHp);
        }

        [Theory]
        [InlineData(10, 10)]
        [InlineData(1, 20)]
        public void ShouldBeDead(int hp, int damage)
        {
            var sut = new Monster(hp, 1, 1, RLColor.Black, 'D', true);

            sut.TakeDamage(damage);

            Assert.True(sut.IsDead);
        }

        [Fact]
        public void ShouldNotBeDead()
        {
            for (int i = 1; i < 1000; i++)
            {
                var sut = new Monster(i, 1, 1, RLColor.Black, 'D', true);
                Assert.False(sut.IsDead);
            }
        }

        [Theory]
        [InlineData(1, 9, 10)]
        [InlineData(10, 10, 20)]
        public void ShouldHeal(int hp, int heal, int result)
        {
            var sut = new Monster(result, 1, 1, RLColor.Black, 'D', true);

            sut.TakeDamage(result-hp);

            sut.Heal(heal);

            Assert.Equal(result, sut.CurrentHp);
        }

        [Theory]
        [InlineData(1, 11, 10)]
        [InlineData(10, 10000000, 10)]
        public void ShouldNotHealAboveMax(int hp, int heal, int result)
        {
            var sut = new Monster(result, 1, 1, RLColor.Black, 'D', true);

            sut.CurrentHp = hp;

            sut.Heal(heal);

            Assert.Equal(result, sut.CurrentHp);
        }

        [Fact]
        public void DyingShouldUnblock()
        {
            var sut = new Monster();

            sut.TakeDamage(20);

            Assert.False(sut.Blocks);
        }

        [Fact]
        public void ShouldBeAMonster()
        {
            var sut = new Monster();
            Assert.Equal(ActorType.Monster,sut.Type);
        }

        [Fact]
        public void ShouldHaveColor()
        {
            var sut = new Monster();
            Assert.NotNull(sut.Color);
        }

        [Theory]
        [InlineData(10,1,11)]
        [InlineData(10,10,10)]
        [InlineData(1,50,10)]
        [InlineData(1,-1,1)]
        public void ShouldHealCorrectAmount(int hp, int heal, int result)
        {
            var sut = new Monster(result, 1, 1, RLColor.Green, 'M', true) {CurrentHp = hp};
            sut.Heal(heal);
            Assert.Equal(result,sut.CurrentHp);
        }
    }
}
