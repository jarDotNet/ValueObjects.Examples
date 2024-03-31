namespace ValueObjects.Money
{
    public class Amount : ValueObject
    {
        private readonly decimal value;

        private Amount(decimal value)
        {
            this.value = value;
        }

        public static Amount FromScalar(decimal value)
        {
            return new Amount(value);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return value;
        }

        public static implicit operator decimal(Amount amount)
        {
            return amount.value;
        }

        public static explicit operator Amount(decimal value)
        {
            return new Amount(value);
        }
    }
}
