using System.Globalization;

namespace ValueObjects.Contacts
{
    public sealed class Countries
    {
        private static readonly Lazy<Countries> lazy =
            new Lazy<Countries>(() => new Countries());

        private static Dictionary<string, string> map;

        private Countries()
        {
            map = CultureInfo
                .GetCultures(CultureTypes.SpecificCultures)
                .Where(c => !c.IsNeutralCulture)
                .Select(culture => new RegionInfo(culture.Name))
                .Where(ri => ri != null)
                .GroupBy(ri => ri.TwoLetterISORegionName)
                .ToDictionary(x => x.Key, x => x.First().EnglishName);
        }

        public static Countries Instance => lazy.Value;

        public bool IsValid(string twoLetterCountryCode)
        {
            return map.ContainsKey(twoLetterCountryCode);
        }
    }
}
