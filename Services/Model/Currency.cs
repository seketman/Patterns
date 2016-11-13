﻿namespace Services.Model
{
    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    ///     Currency class
    /// </summary>
    [DataContract]
    public class Currency
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Currency"/> class.
        /// </summary>
        /// <param name="isoCode">Currency ISO code</param>
        /// <param name="name">Currency name</param>
        /// <param name="quote">Quote provider</param>
        public Currency(string isoCode, string name, IQuote quote)
        {
            this.ISOCode = isoCode;
            this.Name = name;
            this.Quote = quote;
        }

        [DataMember]
        public string ISOCode { get; protected set; }

        [DataMember]
        public string Name { get; protected set; }

        public IQuote Quote { get; set; }

        /// <summary>
        ///     Return latest currency quotation.
        /// </summary>
        /// <returns>Latest <see cref="Quotation"/></returns>
        public Task<Quotation> GetQuotationAsync()
        {
            return this.Quote.GetQuotationAsync();
        }
    }
}