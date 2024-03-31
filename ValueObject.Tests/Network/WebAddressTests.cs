using ValueObjects.Network;

namespace ValueObject.Tests.Network;

internal class webAddress_should
{
    private readonly string EMPTY_URL = string.Empty;
    private const string INVALID_URL = "httd://www.bad-url.com";
    private const string VALID_URL = "https://www.correct-url.com";

    [Test]
    public void not_be_constructed_when_url_is_empty()
    {
        Action url = () => WebAddress.FromUrl(EMPTY_URL);
        url.Should().Throw<ArgumentException>();
    }

    [Test]
    public void not_be_constructed_when_url_is_invalid()
    {
        Action url = () => WebAddress.FromUrl(INVALID_URL);
        url.Should().Throw<ArgumentException>();
    }

    [Test]
    public void be_well_constructed_when_url_is_valid()
    {
        var uri = new Uri(VALID_URL);

        var url = WebAddress.FromUrl(uri.AbsoluteUri);
        url.Address.Should().Be(uri.AbsoluteUri);
        url.Scheme.Should().Be(uri.Scheme);
        url.Host.Should().Be(uri.Host);
    }

    [TestCase("http.www.example.com")]
    [TestCase("http:www.example.com")]
    [TestCase("http:/www.example.com")]
    [TestCase("http://www.example.")]
    [TestCase("http://www.example..com")]
    [TestCase("https.www.example.com")]
    [TestCase("https:www.example.com")]
    [TestCase("https:/www.example.com")]
    [TestCase("http:/example.com")]
    [TestCase("https:/example.com")]
    public void not_be_constructed_for_invalid_urls(string invalidUrl)
    {
        Action url = () => WebAddress.FromUrl(invalidUrl);
        url.Should().Throw<ArgumentException>();
    }

    [TestCase("http://www.example.com/")]
    [TestCase("https://www.example.com")]
    [TestCase("http://example.com")]
    [TestCase("https://example.com")]
    [TestCase("www.example.com")]
    public void be_well_constructed_for_valid_urls(string validUrl)
    {
        var url = WebAddress.FromUrl(validUrl);
        url.Address.Should().Contain(validUrl);
    }
}
