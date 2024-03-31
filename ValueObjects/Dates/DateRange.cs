namespace ValueObjects.Dates
{
    public class DateRange : ValueObject
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        private DateRange(DateTime startDate, DateTime? endDate = null)
        {
            if (endDate.HasValue)
            {
                Ensure.Argument.Is(endDate.Value >= startDate, "The end date cannot be earlier than the start date.");
            }

            StartDate = startDate;
            EndDate = endDate ?? DateTime.MaxValue;
        }

        public static DateRange From(DateTime startDate, DateTime? endDate = null)
        {
            return new DateRange(startDate, endDate);
        }

        public bool IsWithinRange(DateTime date)
        {
            return date >= StartDate && date <= EndDate;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return StartDate;
            yield return EndDate;
        }

        public override string ToString()
        {
            var startDisplay = FormatDate(StartDate);
            var endDisplay = FormatDate(EndDate);

            string FormatDate(DateTime date)
            {
                return date.ToShortDateString();
            }

            return $"{startDisplay} to {endDisplay}";
        }
    }
}
