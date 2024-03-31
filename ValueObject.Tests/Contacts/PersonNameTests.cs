using ValueObjects.Contacts;

namespace ValueObject.Tests.Contacts;

internal class personName_should
{
    private readonly string EMPTY_NAME = string.Empty;

    [Test]
    public void not_be_constructed_when_no_name_is_established()
    {
        Action person = () => PersonName.From(firstName: EMPTY_NAME, middleName: EMPTY_NAME, lastName: EMPTY_NAME);
        person.Should().Throw<ArgumentException>();
    }

    [Test]
    public void have_same_displayName_and_fullName_when_only_names_are_established()
    {
        var person = PersonName.From(firstName: "Jon", middleName: "", lastName: "Doe");
        person.DisplayName.Should().Be(person.FullName);
    }

    [Test]
    public void have_different_displayName_and_fullName_when_title_is_established()
    {
        var personWithTitle = PersonName.From(firstName: "Jon", middleName: "", lastName: "Doe", title: "Mr.");
        personWithTitle.DisplayName.Should().NotBe(personWithTitle.FullName);
    }

    [Test]
    public void have_different_displayName_and_fullName_when_suffix_is_established()
    {
        var personWithSuffix = PersonName.From(firstName: "Jon", middleName: "", lastName: "Doe", suffix: "Jr.");
        personWithSuffix.DisplayName.Should().NotBe(personWithSuffix.FullName);
    }
}
