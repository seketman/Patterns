﻿namespace Services.Model
{
    using System.Runtime.Serialization;
    using System.Threading.Tasks;
    using Utils;

    /// <summary>
    ///     Currency class
    /// </summary>
    [DataContract]
    public class Currency
    {
        private readonly IQuote quote;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Currency"/> class.
        /// </summary>
        /// <param name="isoCode">Currency ISO code</param>
        /// <param name="name">Currency name</param>
        /// <param name="quote">Quote provider</param>
        public Currency(string isoCode, string name, IQuote quote)
        {
            this.ISOCode = Check.NotNull(isoCode, nameof(isoCode));
            this.Name = Check.NotNull(name, nameof(name));
            this.quote = Check.NotNull(quote, nameof(quote));
        }

        [DataMember]
        public string ISOCode { get; protected set; }

        [DataMember]
        public string Name { get; protected set; }

        /// <summary>
        ///     Return latest currency quotation.
        /// </summary>
        /// <returns>Latest <see cref="Quotation"/></returns>
        public Task<Quotation> GetQuotationAsync()
        {
            return this.quote.GetQuotationAsync();
        }
    }
}
