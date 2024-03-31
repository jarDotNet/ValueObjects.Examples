using ValueObjects.Dates;

namespace ValueObject.Tests.Dates;

internal class dateRange_should
{
    private readonly DateTime TODAY = DateTime.Today;
    private readonly DateTime TOMORROW = DateTime.Today.AddDays(1);
    private readonly DateTime YESTERDAY = DateTime.Today.AddDays(-1);

    [Test]
    public void not_be_constructed_when_endDate_is_earlier_than_startDate()
    {
        Action range = () => DateRange.From(startDate: TODAY, endDate: YESTERDAY);
        range.Should().Throw<ArgumentException>();
    }

    [Test]
    public void be_in_range_when_a_given_date_is_between_startDate_and_endDate()
    {
        var range = DateRange.From(startDate: YESTERDAY, endDate: TOMORROW);
        range.IsWithinRange(TODAY).Should().BeTrue();
    }

    [Test]
    public void be_in_range_when_a_given_date_is_after_startDate()
    {
        var range = DateRange.From(startDate: TODAY);
        range.IsWithinRange(TOMORROW).Should().BeTrue();
    }

    [Test]
    public void not_be_in_range_when_a_given_date_is_not_between_startDate_and_endDate()
    {
        var range = DateRange.From(startDate: TODAY, endDate: TOMORROW);
        range.IsWithinRange(YESTERDAY).Should().BeFalse();
    }

    [Test]
    public void not_be_in_range_when_a_given_date_is_earlier_than_startDate()
    {
        var range = DateRange.From(startDate: TODAY);
        range.IsWithinRange(YESTERDAY).Should().BeFalse();
    }
}
