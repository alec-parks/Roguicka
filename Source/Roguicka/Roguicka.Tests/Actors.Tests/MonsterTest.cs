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
            var sut = new Monster(1, 1, 1, 1, RLColor.Green, 'T');

            sut.TakeDamage(10);

            Assert.NotEqual('T',sut.Symbol);
        }

        [Theory]
        [InlineData(10, 10, 0)]
        [InlineData(1, 20, -19)]
        [InlineData(10, 1, 9)]
        public void ShouldTakeDamage(int hp, int damage, int result)
        {
            var sut = new Monster(hp, hp, 1, 1, RLColor.Black, 'D');

            sut.TakeDamage(damage);

            Assert.Equal(result, sut.CurrentHP);
        }

        [Theory]
        [InlineData(10, 10)]
        [InlineData(1, 20)]
        public void ShouldBeDead(int hp, int damage)
        {
            var sut = new Monster(hp, hp, 1, 1, RLColor.Black, 'D');

            sut.TakeDamage(damage);

            Assert.True(sut.IsDead());
        }

        [Fact]
        public void ShouldNotBeDead()
        {
            for (int i = 1; i < 1000; i++)
            {
                var sut = new Monster(i, i, 1, 1, RLColor.Black, 'D');
                Assert.False(sut.IsDead());
            }
        }

        [Theory]
        [InlineData(1, 9, 10)]
        [InlineData(10, 10, 20)]
        public void ShouldHeal(int hp, int heal, int result)
        {
            var sut = new Monster(hp, result, 1, 1, RLColor.Black, 'D');
            sut.Heal(heal);

            Assert.Equal(result, sut.CurrentHP);
        }

        [Theory]
        [InlineData(1, 11, 10)]
        [InlineData(10, 10000000, 10)]
        public void ShouldNotHealAboveMax(int hp, int heal, int result)
        {
            var sut = new Monster(hp, result, 1, 1, RLColor.Black, 'D');

            sut.Heal(heal);

            Assert.Equal(result, sut.CurrentHP);
        }

        [Fact]
        public void DyingShouldUnblock()
        {
            var sut = new Monster(10, 10, 1, 1, RLColor.Black, 'D');

            sut.TakeDamage(20);

            Assert.False(sut.Blocks);
        }
    }
}
