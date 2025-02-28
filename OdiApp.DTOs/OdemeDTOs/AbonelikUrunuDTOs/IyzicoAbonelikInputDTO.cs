namespace OdiApp.DTOs.OdemeDTOs.AbonelikUrunuDTOs
{
    public class IyzicoAbonelikInputDTO
    {
        public string PricingPlanReferenceCode { get; set; }
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string ExpireYear { get; set; }
        public string ExpireMonth { get; set; }
        public string Cvc { get; set; }
        public string BuyerName { get; set; }
        public string BuyerSurname { get; set; }
        public string BuyerGsmNumber { get; set; }
        public string BuyerEmail { get; set; }
        public string BuyerAddress { get; set; }
    }
}