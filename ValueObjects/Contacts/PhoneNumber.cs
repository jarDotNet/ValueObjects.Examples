namespace ValueObjects.Contacts
{
    public class PhoneNumber : ValueObject
    {
        public string Number { get; private set; }

        private PhoneNumber(string number)
        {
            Ensure.Argument.NotNullOrEmpty(number, nameof(number));
            Ensure.Argument.Is(number.Any(char.IsDigit), "The phone number must contain at least one digit.");

            Number = number.Trim();
        }

        public static PhoneNumber From(string number)
        {
            return new PhoneNumber(number);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Number.ToLower();
        }

        public static implicit operator string(PhoneNumber number)
        {
            return number.Number;
        }

        public static explicit operator PhoneNumber(string number)
        {
            return new PhoneNumber(number);
        }
    }
}
