using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Model.V2;
using Iyzipay.Model.V2.Subscription;
using Iyzipay.Request;
using Iyzipay.Request.V2.Subscription;
using Microsoft.Extensions.Configuration;


//using Microsoft.Extensions.Options;
using OdiApp.DTOs.OdemeDTOs.IyzicoDtos;
using OdiApp.DTOs.SharedDTOs;

namespace OdiApp.BusinessLayer.Services.OdemeLogicServices.IyzicoLogicServices
{
    public class IyzicoLogicService : IIyzicoLogicService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        //BASEURL
        //SANDBOX   :   https://sandbox-api.iyzipay.com
        //LIVE      :   https://api.iyzipay.com
        private const string IYZICO_BASE_URL = "https://sandbox-api.iyzipay.com";

        //API & SECRET KEY
        //Sandbox kullanımı için başında "sandbox-" olması gerekmektedir.
        private const string IYZICO_API_KEY = "sandbox-NKzMocIFQXj2tIFYGPCo1gZOT756Wigm";
        private const string IYZICO_SECRET_KEY = "sandbox-iz8I7OPcQW5DZamueXXEJ6q0CO9411Yn";


        //Fields
        private static string IyzicoConversationId { get; set; } = "123456789";
        private static string ProductReferenceCode { get; set; } = "";
        private static string Product1PaymentPlanReferenceCode { get; set; } = "";
        private static string Product2PaymentPlanReferenceCode { get; set; } = "";
        private static string SubscriptionReferenceCode { get; set; } = ""; //üye abonelik referans kodu
        private static string CardUserKey { get; set; } = ""; //kartı kaydedilen kullanıcının iyzico tarafından atanan idsi

        public IyzicoLogicService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        //Ortak metod
        private async Task<Options> GetIyzicoOptions()
        {
            Options options = new Options();
            options.ApiKey = IYZICO_API_KEY;
            options.SecretKey = IYZICO_SECRET_KEY;
            options.BaseUrl = IYZICO_BASE_URL;

            await Task.CompletedTask;

            return options;
        }

        public async Task<OdiResponse<Payment>> OdemeYapTest(OdemeYapTestInputDTO model)
        {
            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = IyzicoConversationId;
            request.Price = "1";
            request.PaidPrice = "1.2";
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = "B67832";
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = "John Doe";
            paymentCard.CardNumber = model.CardNumber;
            paymentCard.ExpireMonth = "12";
            paymentCard.ExpireYear = "2030";
            paymentCard.Cvc = "123";
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Id = "BY789";
            buyer.Name = "John";
            buyer.Surname = "Doe";
            buyer.GsmNumber = "+905350000000";
            buyer.Email = "email@email.com";
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            buyer.Ip = "85.34.78.112";
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = "Jane Doe";
            shippingAddress.City = "Istanbul";
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = "Jane Doe";
            billingAddress.City = "Istanbul";
            billingAddress.Country = "Turkey";
            billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem firstBasketItem = new BasketItem();
            firstBasketItem.Id = "BI101";
            firstBasketItem.Name = "Binocular";
            firstBasketItem.Category1 = "Collectibles";
            firstBasketItem.Category2 = "Accessories";
            firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            firstBasketItem.Price = "0.3";
            basketItems.Add(firstBasketItem);

            BasketItem secondBasketItem = new BasketItem();
            secondBasketItem.Id = "BI102";
            secondBasketItem.Name = "Game code";
            secondBasketItem.Category1 = "Game";
            secondBasketItem.Category2 = "Online Game Items";
            secondBasketItem.ItemType = BasketItemType.VIRTUAL.ToString();
            secondBasketItem.Price = "0.5";
            basketItems.Add(secondBasketItem);

            BasketItem thirdBasketItem = new BasketItem();
            thirdBasketItem.Id = "BI103";
            thirdBasketItem.Name = "Usb";
            thirdBasketItem.Category1 = "Electronics";
            thirdBasketItem.Category2 = "Usb / Cable";
            thirdBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            thirdBasketItem.Price = "0.2";
            basketItems.Add(thirdBasketItem);
            request.BasketItems = basketItems;

            Payment payment = Payment.Create(request, await GetIyzicoOptions());

            if (payment.Status == Status.SUCCESS.ToString())
            {
                return OdiResponse<Payment>.Success("Ödeme başarılı", payment, 200);
            }
            else
            {
                return OdiResponse<Payment>.Fail(payment.ErrorMessage, "", 200);
            }
        }

        public async Task<OdiResponse<ResponseData<PlanResource>>> TestAbonelikUrunVePlanOlusturma()
        {
            string randomString = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            CreateProductRequest createProductRequest = new CreateProductRequest
            {
                Description = "ODI Premium",
                Locale = Locale.TR.ToString(),
                ConversationId = IyzicoConversationId,
                Name = $"ODI Premium {randomString}"
            };

            ResponseData<ProductResource> createProductResponse = Product.Create(createProductRequest, await GetIyzicoOptions());

            ProductReferenceCode = createProductResponse.Data.ReferenceCode;

            //Burada sınırsız süre ayda 1 kere ödemeli ürün aboneliği tanımlanıyor.
            CreatePlanRequest request = new CreatePlanRequest()
            {
                Locale = Locale.TR.ToString(),
                Name = $"ODI Premium Aylık Abonelik",
                ConversationId = IyzicoConversationId,
                TrialPeriodDays = 0,
                Price = "5",
                CurrencyCode = Currency.TRY.ToString(),
                PaymentInterval = PaymentInterval.MONTHLY.ToString(),
                //RecurrenceCount = 12, //bu abonelik sonlanmaması için gönderilmeyecek. 
                PaymentIntervalCount = 1,
                PlanPaymentType = PlanPaymentType.RECURRING.ToString(),
                ProductReferenceCode = ProductReferenceCode
            };

            ResponseData<PlanResource> response = Plan.Create(request, await GetIyzicoOptions());

            Product1PaymentPlanReferenceCode = response.Data.ReferenceCode;

            //Upgrade testi için bir tane daha ödeme planı tanımlanıyor.

            request = new CreatePlanRequest()
            {
                Locale = Locale.TR.ToString(),
                Name = $"ODI Premium Aylık Abonelik 2",
                ConversationId = IyzicoConversationId,
                TrialPeriodDays = 0,
                Price = "10",
                CurrencyCode = Currency.TRY.ToString(),
                PaymentInterval = PaymentInterval.MONTHLY.ToString(),
                //RecurrenceCount = 12, //bu abonelik sonlanmaması için gönderilmeyecek. 
                PaymentIntervalCount = 1,
                PlanPaymentType = PlanPaymentType.RECURRING.ToString(),
                ProductReferenceCode = ProductReferenceCode
            };

            response = Plan.Create(request, await GetIyzicoOptions());

            Product2PaymentPlanReferenceCode = response.Data.ReferenceCode;

            return OdiResponse<ResponseData<PlanResource>>.Success("Ürün ve plan oluşturuldu", response, 200);
        }

        public async Task<OdiResponse<ResponsePagingData<ProductResource>>> AbonelikUrunListeleme()
        {
            PagingRequest pagingRequest = new PagingRequest
            {
                Locale = Locale.TR.ToString(),
                ConversationId = IyzicoConversationId,
                Page = 1,
                Count = 10
            };

            ResponsePagingData<ProductResource> response = Product.RetrieveAll(pagingRequest, await GetIyzicoOptions());

            return OdiResponse<ResponsePagingData<ProductResource>>.Success("Ürün listelendi", response, 200);

        }

        public async Task<OdiResponse<ResponsePagingData<PlanResource>>> UrunPlanListeleme()
        {
            RetrieveAllPlanRequest request = new RetrieveAllPlanRequest
            {
                Locale = Locale.TR.ToString(),
                ConversationId = IyzicoConversationId,
                ProductReferenceCode = "a1290092-6e9f-4aad-b691-a92c06b36cf1",
                Count = 10,
                Page = 1
            };

            ResponsePagingData<PlanResource> response = Plan.RetrieveAll(request, await GetIyzicoOptions());
            return OdiResponse<ResponsePagingData<PlanResource>>.Success("Ürün planları listelendi.", response, 200);
        }

        public async Task<OdiResponse<ResponseData<SubscriptionCreatedResource>>> AbonelikBaslatma()
        {
            string randomString = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            SubscriptionInitializeRequest request = new SubscriptionInitializeRequest
            {
                Locale = Locale.TR.ToString(),
                Customer = new CheckoutFormCustomer
                {
                    //Email = $"iyzico-{randomString}@iyzico.com",
                    Email = $"iyzico@iyzico.com",
                    Name = "customer-name",
                    Surname = "customer-surname",
                    BillingAddress = new Address
                    {
                        City = "İstanbul",
                        Country = "Türkiye",
                        Description = "billing-address-description",
                        ContactName = "billing-contact-name",
                        ZipCode = "010101"
                    },
                    ShippingAddress = new Address
                    {
                        City = "İstanbul",
                        Country = "Türkiye",
                        Description = "shipping-address-description",
                        ContactName = "shipping-contact-name",
                        ZipCode = "010102"
                    },

                    GsmNumber = "+905350000000",
                    IdentityNumber = "55555555555"
                },
                PaymentCard = new CardInfo
                {
                    //Kayıtlı kartlar için bu alanlar kullanılır.
                    //CardToken = "card Token",
                    //ConsumerToken = "consumer Token",
                    //UcsToken = "ucs Token",

                    //Yeni kart ile işlem yapılacaksa bu alanlar kullanılır.
                    CardNumber = "5528790000000008",
                    CardHolderName = "iyzico",
                    ExpireMonth = "12",
                    ExpireYear = "2029",
                    Cvc = "123",

                    //Bu alan zorunlu. Kartın kayıt edilip edilmeyeceğini belirler.
                    RegisterConsumerCard = true,
                },
                ConversationId = IyzicoConversationId,
                PricingPlanReferenceCode = Product1PaymentPlanReferenceCode,

                //Abonelik başlangıcında durumu belirtir. ACTIVE veya PENDING alıyor. ACTIVE olursa başlamış oluyor.
                SubscriptionInitialStatus = SubscriptionStatus.ACTIVE.ToString()
            };

            ResponseData<SubscriptionCreatedResource> response = Subscription.Initialize(request, await GetIyzicoOptions());

            SubscriptionReferenceCode = response.Data.ReferenceCode;

            return OdiResponse<ResponseData<SubscriptionCreatedResource>>.Success("Abonelik başlatıldı.", response, 200);
        }

        public async Task<OdiResponse<IyzipayResourceV2>> AbonelikUpgrade()
        {
            UpgradeSubscriptionRequest request = new UpgradeSubscriptionRequest
            {
                Locale = Locale.TR.ToString(),
                ConversationId = IyzicoConversationId,
                SubscriptionReferenceCode = SubscriptionReferenceCode,
                NewPricingPlanReferenceCode = Product2PaymentPlanReferenceCode,
                UseTrial = true,
                ResetRecurrenceCount = true,
                UpgradePeriod = SubscriptionUpgradePeriod.NEXT_PERIOD.ToString()
            };

            IyzipayResourceV2 response = Subscription.Upgrade(request, await GetIyzicoOptions());

            return OdiResponse<IyzipayResourceV2>.Success("Abonelik yükseltildi.", response, 200);
        }

        public async Task<OdiResponse<Card>> KartKaydet()
        {
            CreateCardRequest request = new CreateCardRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = "123456789";
            request.Email = "email@email.com";
            request.ExternalId = "external id";

            CardInformation cardInformation = new CardInformation();
            cardInformation.CardAlias = "card alias";
            cardInformation.CardHolderName = "John Doe";
            cardInformation.CardNumber = "5528790000000008";
            cardInformation.ExpireMonth = "12";
            cardInformation.ExpireYear = "2030";
            request.Card = cardInformation;

            Card card = Card.Create(request, await GetIyzicoOptions());

            CardUserKey = card.CardUserKey;

            return OdiResponse<Card>.Success("Kart kaydedildi.", card, 200);
        }

        public async Task<OdiResponse<Card>> VarolanKullaniciyaKartEkle()
        {
            CreateCardRequest request = new CreateCardRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = "123456789";
            request.CardUserKey = CardUserKey;
            request.ExternalId = "external id";

            CardInformation cardInformation = new CardInformation();
            cardInformation.CardAlias = "card alias";
            cardInformation.CardHolderName = "John Doe";
            cardInformation.CardNumber = "5528790000000008";
            cardInformation.ExpireMonth = "12";
            cardInformation.ExpireYear = "2030";
            request.Card = cardInformation;

            Card card = Card.Create(request, await GetIyzicoOptions());

            CardUserKey = card.CardUserKey;

            return OdiResponse<Card>.Success("Kart kaydedildi.", card, 200);
        }

        public async Task<OdiResponse<CardList>> KullaniciKartListesi()
        {
            RetrieveCardListRequest request = new RetrieveCardListRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = "123456789";
            request.CardUserKey = CardUserKey;

            CardList cardList = CardList.Retrieve(request, await GetIyzicoOptions());

            return OdiResponse<CardList>.Success("Kart kaydedildi.", cardList, 200);
        }
    }
}