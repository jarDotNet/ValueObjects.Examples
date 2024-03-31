using ValueObjects.Contacts;

namespace ValueObject.Tests.Contacts;

internal class address_should
{
    [Test]
    public void not_be_constructed_when_street_is_empty()
    {
        Action address = () => Address.From(
            street: "",
            city: "Redmond",
            state: "WA",
            country: Country.FromCode("US"),
            zipCode: "98052"
        ); ;
        address.Should().Throw<ArgumentException>();
    }

    [Test]
    public void be_equal_to_other_address_with_the_same_values()
    {
        var one = Address.From(
            street: "1 Microsoft Way", 
            city: "Redmond", 
            state: "WA", 
            country: Country.FromCode("US"), 
            zipCode: "98052"
        );
        var two = Address.From(
            street: "1 Microsoft Way",
            city: "Redmond",
            state: "WA",
            country: Country.FromCode("US"),
            zipCode: "98052"
        );

        one.Should().Be(two);
    }
    [Test]
    public void be_equal_to_a_cloned_address()
    {
        var one = Address.From(
            street: "1 Microsoft Way",
            city: "Redmond",
            state: "WA",
            country: Country.FromCode("US"),
            zipCode: "98052"
        );
        var two = one.Clone();

        one.Should().Be(two);
    }
}
