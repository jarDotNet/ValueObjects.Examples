using System.Text.RegularExpressions;

namespace ValueObjects.Network
{
    public class Email : ValueObject
    {
        public string Address { get; private set; }
        public string User { get; private set; }
        public string Host { get; private set; }

        private Email(string email)
        {
            Ensure.Argument.NotNullOrEmpty(email, nameof(email));
            Ensure.Argument.Is(IsValidEmail(email), $"The email address '{email}' is invalid.");

            var parts = email.Split('@');

            Address = email;
            User = parts[0];
            Host = parts[1];
        }

        public static Email FromAddress(string email)
        {
            return new Email(email);
        }

        /// <see cref="https://haacked.com/archive/2007/08/21/i-knew-how-to-validate-an-email-address-until-i.aspx/"/>
        private static bool IsValidEmail(string email)
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            var isValid = regex.IsMatch(email);

            return isValid;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Address.ToLower();
        }

        public static implicit operator string(Email email)
        {
            return email.Address;
        }

        public static explicit operator Email(string email)
        {
            return new Email(email);
        }
    }
}
