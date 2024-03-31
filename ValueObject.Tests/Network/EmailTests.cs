using ValueObjects.Network;

namespace ValueObject.Tests.Network;

internal class email_should
{
    private readonly string EMPTY_EMAIL = string.Empty;
    private const string INVALID_EMAIL = "invalid@email.x";

    [Test]
    public void not_be_constructed_when_email_is_empty()
    {
        Action email = () => Email.FromAddress(EMPTY_EMAIL);
        email.Should().Throw<ArgumentException>();
    }

    [Test]
    public void not_be_constructed_when_email_is_not_valid()
    {
        Action email = () => Email.FromAddress(INVALID_EMAIL);
        email.Should().Throw<ArgumentException>();
    }

    [Test]
    public void be_well_constructed_when_email_is_valid()
    {
        var user = "username";
        var host = "is-valid.com";
        var address = $"{user}@{host}";

        var email = Email.FromAddress(address);
        email.Address.Should().Be(address);
        email.User.Should().Be(user);
        email.Host.Should().Be(host);
    }

    [TestCase(@"fdsa")]
    [TestCase(@"fdsa@")]
    [TestCase(@"fdsa@fdsa")]
    [TestCase(@"fdsa@fdsa.")]
    [TestCase(@"NotAnEmail")]
    [TestCase(@"@NotAnEmail")]
    [TestCase(@"""test\blah""@example.com")]
    [TestCase("\"test\rblah\"@example.com")]
    [TestCase(@"""test""blah""@example.com")]
    [TestCase(@".wooly@example.com")]
    [TestCase(@"wo..oly@example.com")]
    [TestCase(@"pootietang.@example.com")]
    [TestCase(@".@example.com")]
    [TestCase(@"Ima Fool@example.com")]
    public void not_be_constructed_for_invalid_emails(string invalidEmail)
    {
        Action email = () => Email.FromAddress(invalidEmail);
        email.Should().Throw<ArgumentException>();
    }

    [TestCase(@"someone@somewhere.com")]
    [TestCase(@"someone@somewhere.co.uk")]
    [TestCase(@"someone+tag@somewhere.net")]
    [TestCase(@"futureTLD@somewhere.fooo")]
    [TestCase(@"""test\\blah""@example.com")]
    [TestCase("\"test\\\rblah\"@example.com")]
    [TestCase(@"""test\""blah""@example.com")]
    [TestCase(@"customer/department@example.com")]
    [TestCase(@"$A12345@example.com")]
    [TestCase(@"!def!xyz%abc@example.com")]
    [TestCase(@"_Yosemite.Sam@example.com")]
    [TestCase(@"~@example.com")]
    [TestCase(@"""Austin@Powers""@example.com")]
    [TestCase(@"Ima.Fool@example.com")]
    [TestCase(@"""Ima.Fool""@example.com")]
    [TestCase(@"""Ima Fool""@example.com")]
    public void be_well_constructed_for_valid_emails(string validEmail)
    {
        var email = Email.FromAddress(validEmail);
        email.Address.Should().Be(validEmail);
    }
}
