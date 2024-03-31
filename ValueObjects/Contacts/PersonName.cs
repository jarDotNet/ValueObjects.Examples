using System.Text;

namespace ValueObjects.Contacts
{
    public class PersonName : ValueObject
    {
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string LastName { get; private set; }
        public string Title { get; private set; }
        public string Suffix { get; private set; }

        private PersonName(
            string firstName,
            string middleName,
            string lastName,
            string title = null,
            string suffix = null)
        {
            Ensure.Argument.Is(
                HaveAnyOfTheNamesValue(firstName, middleName, lastName), 
                "At least one name (first, middle or last) is required."
            );

            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Title = title;
            Suffix = suffix;
        }

        public static PersonName From(
            string firstName,
            string middleName,
            string lastName,
            string title = null,
            string suffix = null)
        {
            return new PersonName
            (
               firstName,
               middleName,
               lastName,
               title,
               suffix
            );
        }

        /// <summary>
        /// Gets a display name for presentation purposes (excluding title and suffix)
        /// </summary>
        public string DisplayName
        {
            get
            {
                return Concat(FirstName, MiddleName, LastName);
            }
        }

        /// <summary>
        /// Gets the full name of the person (including title and suffix)
        /// </summary>
        public string FullName
        {
            get
            {
                return Concat
                (
                    Title,
                    FirstName,
                    MiddleName,
                    LastName,
                    Suffix
                );
            }
        }

        /// <summary>
        /// Concatenates an array of words into a single string separated by spaces
        /// </summary>
        /// <param name="words">The words to concatenate</param>
        /// <returns>The concatenated string</returns>
        private string Concat(params string[] words)
        {
            var builder = new StringBuilder();

            foreach (var word in words)
            {
                if (false == string.IsNullOrWhiteSpace(word))
                {
                    builder.Append($"{word} ");
                }
            }

            return builder.ToString().Trim();
        }

        private bool HaveAnyOfTheNamesValue(string firstName, string middleName, string lastName)
        {
            var allNames = new string[] { firstName, middleName, lastName };
            var anyValue = allNames.Any(name => !string.IsNullOrWhiteSpace(name));

            return anyValue;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return FirstName.ToLower();
            yield return MiddleName.ToLower();
            yield return LastName.ToLower();
            yield return Title.ToLower();
            yield return Suffix.ToLower();
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
