namespace Services.Model
{
    using System;
    using System.Globalization;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    /// <summary>
    ///     Province Bank dollar quote implementation.
    /// </summary>
    public class ProvinceBankDollarQuote : IQuote
    {
        /// <summary>
        ///     HttpClient is intended to be instantiated once and re-used throughout the life of an 
        ///     application. Especially in server applications, creating a new HttpClient instance for 
        ///     every request will exhaust the number of sockets available under heavy loads. This 
        ///     will result in SocketException errors.
        /// </summary>
        static HttpClient httpClient = new HttpClient();

        private const string ProvinceUri = "http://www.bancoprovincia.com.ar/principal/dolar";

        /// <summary>
        ///     Returns the latest Buenos Aires Province Bank dollar quotation.
        /// </summary>
        /// <returns>Latest <see cref="Quotation"/></returns>
        public async Task<Quotation> GetQuotationAsync()
        {
            HttpResponseMessage response = await httpClient.GetAsync(ProvinceUri);
            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return ParseReponseBody(body);
            }

            return null;
        }

        /// <summary>
        ///     Parse Buenos Aires Province Bank dollar quotation service response.
        /// </summary>
        /// <param name="body">Service response body</param>
        /// <returns>Parsed <see cref="Quotation"/></returns>
        private Quotation ParseReponseBody(string body)
        {
            var arrBody = JsonConvert.DeserializeObject<dynamic>(body);

            // Parse buyers price (ex: "15.150")
            decimal buyer = 0;
            string strBuyer = arrBody[0]?.Value;
            if (strBuyer != null)
            {
                buyer = decimal.Parse(strBuyer, CultureInfo.InvariantCulture);
            }

            // Parse sellers price (ex: "15.550")
            decimal seller = 0;
            string strSeller = arrBody[1]?.Value;
            if (strSeller != null)
            {
                seller = decimal.Parse(strSeller, CultureInfo.InvariantCulture);
            }

            // Parse updated date time ("Actualizada al 11/11/2016 15:00")
            DateTime datUpdateAt = DateTime.Now;
            string strUpdatedAt = arrBody[2]?.Value?.ToString();
            if (strUpdatedAt?.Length >= 31)
            {
                strUpdatedAt = strUpdatedAt.Substring(15);
                DateTime.TryParseExact(strUpdatedAt, "dd/MM/yyyy HH:mm", null, DateTimeStyles.None, out datUpdateAt);
            }

            return new Quotation(datUpdateAt, seller, buyer);
        }
    }
}
