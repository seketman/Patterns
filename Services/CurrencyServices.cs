namespace Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using log4net;
    using Model;

    /// <summary>
    ///     Currency services singleton class.
    /// </summary>
    public class CurrencyServices
    {
        private static volatile CurrencyServices instance;
        private static object syncRoot = new object();

        private readonly ILog log;
        private readonly IEnumerable<Currency> currencies;

        #region Singleton
        /// <summary>
        ///     Prevents a default instance of the <see cref="CurrencyServices"/> class from being created.
        /// </summary>
        private CurrencyServices()
        {
            this.log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

            this.currencies = new List<Currency>
            {
                new Currency("ARS", "Peso", new NotImplementedQuote()),
                new Currency("USD", "Dólar estadounidense", new ProvinceBankDollarQuote()),
                new Currency("BRL", "Real brasileño", new NotImplementedQuote()),
                new Currency("PEN", "Nuevo sol", new NotImplementedQuote())
            };
        }

        /// <summary>
        ///     Gets singleton instance
        /// </summary>
        public static CurrencyServices Inst
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new CurrencyServices();
                        }
                    }
                }

                return instance;
            }
        }
        #endregion

        /// <summary>
        ///     Returns the list of the defined currencies.
        /// </summary>
        /// <returns>List of <see cref="Currency"/></returns>
        public IEnumerable<Currency> GetCurrencies()
        {
            return this.currencies;
        }

        /// <summary>
        ///     Return specific currency.
        /// </summary>
        /// <param name="isoCode">ISO code of the currency to return</param>
        /// <returns>The specified <see cref="Currency"/></returns>
        public Currency GetCurrency(string isoCode)
        {
            return this.currencies.FirstOrDefault(c => c.ISOCode == isoCode);
        }
    }
}
