namespace Services.Model
{
    using System.Threading.Tasks;

    /// <summary>
    ///     Quote provider interface.
    /// </summary>
    public interface IQuote
    {
        /// <summary>
        ///     Returns latest quotation.
        /// </summary>
        /// <returns>Latest <see cref="Quotation"/></returns>
        Task<Quotation> GetQuotationAsync();
    }
}
