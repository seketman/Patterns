namespace APIRest.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;
    using Services;
    using System;

    /// <summary>
    ///     Currencies controller class.
    /// </summary>
    [RoutePrefix("api/currencies")]
    public class CurrenciesController : ApiController
    {
        // GET: api/Currencies
        /// <summary>
        ///     Gets currencies list.
        /// </summary>
        /// <returns>List of <see cref="Currency"/></returns>
        public IHttpActionResult GetCurrencies()
        {
            return Ok(CurrencyServices.Inst.GetCurrencies());
        }

        // GET: api/Currencies/5
        /// <summary>
        ///     Gets specified currency data.
        /// </summary>
        /// <param name="id">ISO code of the currency</param>
        /// <returns>Specified <see cref="Currency"/></returns>
        [Route("{id}")]
        public IHttpActionResult GetCurrency(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException("id is null");

            var currency = CurrencyServices.Inst.GetCurrency(id);
            if (currency == null)
            {
                return BadRequest("Currency not found");
            }

            return Ok(currency);
        }

        // GET: api/Currencies/5/Quotation
        /// <summary>
        ///     Gets specified currency quotation.
        /// </summary>
        /// <param name="id">ISO code of the currency</param>
        /// <returns><see cref="Quotation"/> of the specified currency</returns>
        [Route("{id}/quotation")]
        public async Task<IHttpActionResult> GetQuotation(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException("id is null");

            var currency = CurrencyServices.Inst.GetCurrency(id);
            if (currency == null)
            {
                return BadRequest("Currency not found");
            }

            return Ok(await currency.GetQuotationAsync());
        }
    }
}
