namespace Services.Model
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    ///     Quotation class.
    /// </summary>
    [DataContract]
    public class Quotation
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Quotation"/> class.
        /// </summary>
        /// <param name="updatedAt">Latest update date time</param>
        /// <param name="seller">Seller price</param>
        /// <param name="buyer">Buyer price</param>
        public Quotation(DateTime updatedAt, decimal seller, decimal buyer)
        {
            this.UpdatedAt = updatedAt;
            this.Buyer = buyer;
            this.Seller = seller;
        }

        [DataMember]
        public DateTime UpdatedAt { get; protected set; }

        [DataMember]
        public decimal Buyer { get; protected set; }

        [DataMember]
        public decimal Seller { get; protected set; }
    }
}
