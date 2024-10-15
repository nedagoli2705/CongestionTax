namespace CongestionTax.Domain.ValueObjects
{
    public class CityTaxOptions
    {
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string CityName { get; set; }
        public decimal MaxTaxPerDay { get; set; }
        public List<TaxRule> TaxRules { get; set; }
    }
}
