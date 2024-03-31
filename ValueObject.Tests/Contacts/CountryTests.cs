using ValueObjects.Contacts;

namespace ValueObject.Tests.Contacts;

internal class country_should
{
    [Test]
    public void not_be_constructed_when_country_code_is_empty()
    {
        Action currency = () => Country.FromCode(string.Empty);
        currency.Should().Throw<ArgumentException>();
    }

    [Test]
    public void not_be_constructed_when_cointry_code_is_not_valid()
    {
        Action currency = () => Country.FromCode("AA");
        currency.Should().Throw<ArgumentException>();
    }
}
