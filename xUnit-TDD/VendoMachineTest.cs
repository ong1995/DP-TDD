using OngTDD.Services;
using Xunit;

namespace xUnit_TDD
{
    public class VendoMachineTest
    {
        private readonly InsertMoney _insertMoney = new InsertMoney();
        private readonly PurchaseBeverage _purchaseBeverage = new PurchaseBeverage();

        [Fact]
        public void Test1()
        {

        }

        [Theory]
        [InlineData(1.25)]
        [InlineData(1.50)]
        [InlineData(10.50)]
        [InlineData(50.50)]
        [InlineData(50)]
        [InlineData(100)]
        [InlineData(0.25)]
        [InlineData(0.50)]
        public void ValidateWholeMoney_ShouldReturnTrue(double amount)
        {
            var result = _insertMoney.ValidateInsertedMoney(amount);

            Assert.True(result);
        }

        [Theory]
        [InlineData(2.25)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(6)]
        [InlineData(101)]
        [InlineData(56)]
        [InlineData(0.26)]
        [InlineData(0.51)]
        public void ValidateWholeMoney_ShouldReturnFalse(double amount)
        {
            var result = _insertMoney.ValidateInsertedMoney(amount);

            Assert.False(result);
        }

        [Theory]
        [InlineData(0.26)]
        [InlineData(0.75)]
        [InlineData(0.08)]
        [InlineData(0.45)]
        [InlineData(0.89)]
        [InlineData(0.3)]
        [InlineData(0.22)]
        [InlineData(0.51)]
        public void ValidateCent_ShouldReturnFalse(double amount)
        {
            var result = _insertMoney.ValidateCent(amount);

            Assert.False(result);
        }

        [Theory]
        [InlineData(0.50)]
        [InlineData(0.25)]
        public void ValidateCent_ShouldReturnTrue(double amount)
        {
            var result = _insertMoney.ValidateCent(amount);

            Assert.True(result);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(50)]
        [InlineData(20)]
        [InlineData(10)]
        [InlineData(5)]
        [InlineData(1)]
        public void ValidatePeso_ShouldReturnTrue(int amount)
        {
            var result = _insertMoney.ValidatePeso(amount);

            Assert.True(result);
        }

        [Theory]
        [InlineData(101)]
        [InlineData(52)]
        [InlineData(23)]
        [InlineData(14)]
        [InlineData(6)]
        [InlineData(2)]
        public void ValidatePeso_ShouldReturnFalse(int amount)
        {
            var result = _insertMoney.ValidatePeso(amount);

            Assert.False(result);
        }
    }
}
