namespace Services.Model
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    ///     Dummy quote provider implementation.
    /// </summary>
    public class NotImplementedQuote : IQuote
    {
        /// <summary>
        ///     Throws <see cref="NotImplementedException"/>
        /// </summary>
        /// <returns>None</returns>
        public async Task<Quotation> GetQuotationAsync()
        {
            throw new NotImplementedException();
        }
    }
}
