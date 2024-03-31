using ValueObjects.Contacts;

namespace ValueObject.Tests.Contacts;

internal class phoneNumber_should
{
    private readonly string INVALID_EMPTY_NUMBER = string.Empty;
    private const string INVALID_WITHOUT_DIGITS_NUMBER = "ABCDEFGH";
    private const string VALID_WITH_SPACES_NUMBER = " 123456789 ";

    [Test]
    public void not_be_constructed_when_number_is_empty()
    {
        Action phone = () => PhoneNumber.From(INVALID_EMPTY_NUMBER);
        phone.Should().Throw<ArgumentException>();
    }

    [Test]
    public void not_be_constructed_when_has_no_digits()
    {
        Action phone = () => PhoneNumber.From(INVALID_WITHOUT_DIGITS_NUMBER);
        phone.Should().Throw<ArgumentException>();
    }

    [Test]
    public void trim_white_spaces_of_the_number()
    {
        var phone = PhoneNumber.From(VALID_WITH_SPACES_NUMBER);
        phone.Number.Should().Be(VALID_WITH_SPACES_NUMBER.Trim());
    }

    [TestCase("1ABCDEFGH")]
    [TestCase("ABCD1EFGH")]
    [TestCase("ABCDEFGH1")]
    [TestCase("123456789")]
    public void be_well_constructed_for_valid_numbers(string validNumber)
    {
        var phone = PhoneNumber.From(validNumber);
        phone.Number.Should().Be(validNumber);
    }
}
