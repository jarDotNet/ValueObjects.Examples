using ValueObjects.Money;

namespace ValueObject.Tests.Money;

internal class currency_should
{

    [Test]
    public void not_be_constructed_when_currency_symbol_is_empty()
    {
        Action currency = () => Currency.FromCode(string.Empty);
        currency.Should().Throw<ArgumentException>();
    }

    [Test]
    public void not_be_constructed_when_currency_symbol_is_not_valid()
    {
        Action currency = () => Currency.FromCode("AAA");
        currency.Should().Throw<ArgumentException>();
    }
}