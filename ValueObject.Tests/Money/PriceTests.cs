using ValueObjects.Money;

namespace ValueObject.Tests.Money;

internal class price_should
{
    [Test]
    public void not_allow_to_be_constructed_when_amount_is_null()
    {
        Action price = () => _ = new Price(amount: null, new Currency("USD"));
        price.Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void not_allow_to_be_constructed_when_currency_is_null()
    {
        Action price = () => _ = new Price(Amount.FromScalar(1000), currency: null);
        price.Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void be_equal_to_other_price_of_the_same_amount_and_currency()
    {
        var price = Price.From(amount: 1000, currencyCode: "USD");
        price.Should().Be(Price.From(amount: 1000, currencyCode: "USD"));
    }

    [Test]
    public void not_be_equal_to_other_price_of_different_currency()
    {
        var price = Price.From(amount: 1000, currencyCode: "USD");
        price.Should().NotBe(Price.From(amount: 1000, currencyCode: "EUR"));
    }


    [Test]
    public void return_primitiva_amount_and_currency_values_from_desctuctor()
    {
        var amount = 1000;
        var currencyCode = "EUR";
        var price = Price.From(amount, currencyCode);
        var (priceAmount, priceCurrency) = price;
        (priceAmount, priceCurrency).Should().Be((amount, currencyCode));
    }
}
