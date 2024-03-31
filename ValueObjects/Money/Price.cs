using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ValueObjects.Money
{
    public class Price : ValueObject
    {
        private readonly Amount amount;
        private readonly Currency currency;

        public Price(Amount amount, Currency currency)
        {
            Ensure.Argument.NotNull(amount, nameof(amount));
            Ensure.Argument.NotNull(currency, nameof(currency));
            this.amount = amount;
            this.currency = currency;
        }

        public static Price From(decimal amount, string currencyCode)
        {
            return new Price(Amount.FromScalar(amount), Currency.FromCode(currencyCode));
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return amount;
            yield return currency;
        }

        public void Deconstruct(out decimal amount, out string currencyCode)
        {
            amount = this.amount;
            currencyCode = (string)currency;
        }
    }
}
