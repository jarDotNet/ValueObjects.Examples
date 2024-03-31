namespace ValueObjects.Contacts
{
    public class Country : ValueObject
    {
        private readonly string code;

        private Country(string twoLetterCountryCode)
        {
            Ensure.Argument.NotNullOrEmpty(twoLetterCountryCode, nameof(twoLetterCountryCode));
            Ensure.Argument.Is(Countries.Instance.IsValid(twoLetterCountryCode), $"Invalid country code ({twoLetterCountryCode}).");
            code = twoLetterCountryCode.ToUpperInvariant();
        }

        public static Country FromCode(string twoLetterCountryCode)
        {
            return new Country(twoLetterCountryCode);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return code;
        }

        public static implicit operator string(Country country)
        {
            return country.code;
        }

        public static explicit operator Country(string twoLetterCountryCode)
        {
            return new Country(twoLetterCountryCode);
        }
    }
}
