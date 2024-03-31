namespace ValueObjects.Network
{
    public class WebAddress : ValueObject
    {
        public string Address { get; private set; }
        public string Scheme { get; private set; }
        public string Host { get; private set; }

        private WebAddress(string url)
        {
            Ensure.Argument.NotNullOrEmpty(url, nameof(url));
            Ensure.Argument.Is(IsValudUrl(ref url), $"The website address '{url}' is invalid.");

            var uri = new Uri(url);

            Address = url;
            Scheme = uri.Scheme;
            Host = uri.Host;
        }

        public static WebAddress FromUrl(string url)
        {
            return new WebAddress(url);
        }

        /// <summary>
        /// Checks if a given Url is valid.
        /// Valid URLs should include all of the following “prefixes”: https, http, www
        /// - Url must contain http:// or https://
        /// - Url may contain only one instance of www.
        /// - Url Host name type must be Dns
        /// - Url max length is 100
        /// </summary>
        /// <param name="url">Web site Url</param>
        /// <returns><c>true</c> if the Url is valid, otherwise <c>false</c></returns>
        private static bool IsValudUrl(ref string url)
        {
            if (url.StartsWith("www."))
            {
                url = "http://" + url;
            }

            return Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult)
                     && (uriResult.Scheme == Uri.UriSchemeHttp
                      || uriResult.Scheme == Uri.UriSchemeHttps) 
                     && uriResult.Host.Replace("www.", "").Split('.').Count() > 1 
                     && uriResult.HostNameType == UriHostNameType.Dns 
                     && uriResult.Host.Length > uriResult.Host.LastIndexOf(".") + 1 
                     && 100 >= url.Length;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Address.ToLower();
        }

        public static implicit operator string(WebAddress webAddress)
        {
            return webAddress.Address;
        }

        public static explicit operator WebAddress(string url)
        {
            return new WebAddress(url);
        }
    }
}
