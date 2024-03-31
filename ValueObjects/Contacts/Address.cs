namespace ValueObjects.Contacts
{
    public class Address : ValueObject
    {
        public string Street { get; }
        public string City { get; }
        public string State { get; }
        public Country Country { get; }
        public string ZipCode { get; }

        private Address(string street, string city, string state, Country country, string zipCode)
        {
            Ensure.Argument.NotNullOrEmpty(street, nameof(street)); 
            Ensure.Argument.NotNullOrEmpty(city, nameof(city)); 
            Ensure.Argument.NotNullOrEmpty(state, nameof(state)); 
            Ensure.Argument.NotNull(country, nameof(country));
            Ensure.Argument.NotNullOrEmpty(zipCode, nameof(zipCode));

            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
        }

        public static Address From(string street, string city, string state, Country country, string zipCode) 
        {
            return new Address(street, city, state, country, zipCode);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Street;
            yield return City;
            yield return State;
            yield return Country;
            yield return ZipCode;
        }
    }
}
