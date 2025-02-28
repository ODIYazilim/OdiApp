using AutoMapper;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Model.V2;
using Iyzipay.Model.V2.Subscription;
using Iyzipay.Request;
using Iyzipay.Request.V2.Subscription;
using Microsoft.Extensions.Configuration;
using OdiApp.BusinessLayer.Core.Services;
//using Microsoft.Extensions.Options;
using OdiApp.DataAccessLayer.OdemeDataServices.AbonelikKartlariDataServices;
using OdiApp.DataAccessLayer.OdemeDataServices.AbonelikPaketiAboneOlmaDataServices;
using OdiApp.DataAccessLayer.OdemeDataServices.AbonelikPaketiSatinAlmaDataServices;
using OdiApp.DataAccessLayer.OdemeDataServices.AbonelikUrunuDataServices;
using OdiApp.DataAccessLayer.OdemeDataServices.AbonelikUrunuOdemePlaniDataServices;
using OdiApp.DataAccessLayer.OdemeDataServices.AbonelikYukseltmeTalepDataServices;
using OdiApp.DTOs.Enums;
using OdiApp.DTOs.Enums.AbonelikEnums;
using OdiApp.DTOs.Kullanici;
using OdiApp.DTOs.OdemeDTOs.AbonelikUrunuDTOs;
using OdiApp.DTOs.SharedDTOs;
using OdiApp.DTOs.SharedDTOs.AbonelikUrunuDTOs;
using OdiApp.DTOs.SharedDTOs.PerformerDTOs.YetenekTemsilcisiDTOs;
using OdiApp.EntityLayer.OdemeModels.AbonelikUrunuModels;
using System.Text.Json;

namespace OdiApp.BusinessLayer.Services.OdemeLogicServices.AbonelikUrunuLogicServices
{
    public class AbonelikUrunuLogicService : IAbonelikUrunuLogicService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAbonelikUrunuDataService _abonelikUrunuDataService;
        private readonly IAbonelikUrunuOdemePlaniDataService _abonelikUrunuOdemePlaniDataService;
        private readonly IAbonelikPaketiSatinAlmaDataService _abonelikPaketiSatinAlmaDataService;
        private readonly IAbonelikKartlariDataService _abonelikKartlariDataService;
        private readonly IAbonelikPaketiAboneOlmaDataService _abonelikPaketiAboneOlmaDataService;
        private readonly IAbonelikYukseltmeTalepDataService _abonelikYukseltmeTalepDataService;

        public AbonelikUrunuLogicService(IMapper mapper, IConfiguration configuration, IAbonelikUrunuDataService abonelikUrunuDataService, IHttpClientFactory httpClientFactory, IAbonelikUrunuOdemePlaniDataService abonelikUrunuOdemePlaniDataService, IAbonelikPaketiSatinAlmaDataService abonelikPaketiSatinAlmaDataService, IAbonelikKartlariDataService abonelikKartlariDataService, IAbonelikPaketiAboneOlmaDataService abonelikPaketiAboneOlmaDataService, IAbonelikYukseltmeTalepDataService abonelikYukseltmeTalepDataService)
        {
            _mapper = mapper;
            _configuration = configuration;
            _abonelikUrunuDataService = abonelikUrunuDataService;
            _httpClientFactory = httpClientFactory;
            _abonelikUrunuOdemePlaniDataService = abonelikUrunuOdemePlaniDataService;
            _abonelikPaketiSatinAlmaDataService = abonelikPaketiSatinAlmaDataService;
            _abonelikKartlariDataService = abonelikKartlariDataService;
            _abonelikPaketiAboneOlmaDataService = abonelikPaketiAboneOlmaDataService;
            _abonelikYukseltmeTalepDataService = abonelikYukseltmeTalepDataService;
        }

        //Ortak metod
        private async Task<Iyzipay.Options> GetIyzicoOptions()
        {
            Iyzipay.Options options = new Iyzipay.Options();
            options.ApiKey = "sandbox-NKzMocIFQXj2tIFYGPCo1gZOT756Wigm"; ;
            options.SecretKey = "sandbox-iz8I7OPcQW5DZamueXXEJ6q0CO9411Yn";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            await Task.CompletedTask;

            return options;
        }

        public async Task<OdiResponse<AbonelikUrunu>> OdemeYontemiAbonelikUrunuOlusturma(OdemeYontemiPerformerAbonelikUrunuCreateDTO model, OdiUser user)
        {
            AbonelikUrunu abonelikUrunu = _mapper.Map<AbonelikUrunu>(model);

            DateTime date = DateTime.Now;

            abonelikUrunu.Ekleyen = user.AdSoyad;
            abonelikUrunu.EkleyenId = user.Id;
            abonelikUrunu.EklenmeTarihi = date;

            abonelikUrunu.Guncelleyen = user.AdSoyad;
            abonelikUrunu.GuncelleyenId = user.Id;
            abonelikUrunu.GuncellenmeTarihi = date;

            abonelikUrunu = await _abonelikUrunuDataService.YeniAbonelikUrunu(abonelikUrunu);

            CreateProductRequest createProductRequest = new CreateProductRequest
            {
                Description = $"{abonelikUrunu.KayitGrubu} {abonelikUrunu.KayitTuru} {abonelikUrunu.OdemeYonetimiUrunAdi}",
                Locale = Locale.TR.ToString(),
                ConversationId = abonelikUrunu.Id,
                Name = abonelikUrunu.OdemeYonetimiUrunAdi
            };

            ResponseData<ProductResource> createProductResponse;
            int iyzicoCreateAttempt = 1;
            bool iyzicoCreateSuccess = false;

            do
            {
                createProductResponse = Product.Create(createProductRequest, await GetIyzicoOptions());

                if (createProductResponse != null && createProductResponse.Status == Status.SUCCESS.ToString())
                {
                    iyzicoCreateSuccess = true;
                    break;
                }

                iyzicoCreateAttempt++;

            } while (iyzicoCreateAttempt <= 2);

            if (iyzicoCreateSuccess)
            {
                abonelikUrunu.ReferansCode = createProductResponse?.Data.ReferenceCode;
                abonelikUrunu.Status = createProductResponse?.Status;
                abonelikUrunu.Aktif = true;
            }
            else
            {
                abonelikUrunu.Status = createProductResponse?.Status;
                abonelikUrunu.Aktif = false;
            }

            abonelikUrunu = await _abonelikUrunuDataService.AbonelikUrunuGuncelle(abonelikUrunu);

            return OdiResponse<AbonelikUrunu>.Success("Abonelik ürünü oluşturma tamamlandı.", abonelikUrunu, 200);
        }

        public async Task<OdiResponse<List<AbonelikUrunu>>> AbonelikUrunleriListeleme()
        {
            return OdiResponse<List<AbonelikUrunu>>.Success("Abonelik ürünü listesi getirildi.", await _abonelikUrunuDataService.AbonelikUrunListesiGetir(), 200);
        }

        public async Task<OdiResponse<AbonelikUrunuOdemePlaniOutputDTO>> AbonelikOdemePlaniOlustur(AbonelikUrunuOdemePlaniCreateDTO model, OdiUser user, string jwtToken)
        {
            string urunAdi = "";
            decimal fiyat = 0;
            int odemePeriodu = 0;

            PerformerAbonelikUrunuCreateDTO performerAbonelikUrunuCreateDTO = null;
            YetenekTemsilcisiAbonelikUrunuCreateDTO yetenekTemsilcisiAbonelikUrunuCreateDTO = null;
            YapimAbonelikUrunuCreateDTO yapimAbonelikUrunuCreateDTO = null;

            switch (model.AbonelikTipi)
            {
                case (int)AbonelikTipi.Performer:
                    try
                    {
                        performerAbonelikUrunuCreateDTO = JsonSerializer.Deserialize<PerformerAbonelikUrunuCreateDTO>(model.KullaniciAbonelikUrunu.ToString());
                        urunAdi = performerAbonelikUrunuCreateDTO.UrunAdi;
                        fiyat = performerAbonelikUrunuCreateDTO.KDVliFiyat;
                        odemePeriodu = performerAbonelikUrunuCreateDTO.OdemePeriodu;
                    }
                    catch (Exception)
                    {
                        return OdiResponse<AbonelikUrunuOdemePlaniOutputDTO>.Fail("Ödeme planı oluşturma başarısız.", "KullaniciAbonelikUrunu PerformerAbonelikUrunuCreateDTO türüne dönüştürülemedi.", 400);
                    }

                    break;

                case (int)AbonelikTipi.YetenekTemsilcisi:
                    try
                    {
                        yetenekTemsilcisiAbonelikUrunuCreateDTO = JsonSerializer.Deserialize<YetenekTemsilcisiAbonelikUrunuCreateDTO>(model.KullaniciAbonelikUrunu.ToString());
                        urunAdi = yetenekTemsilcisiAbonelikUrunuCreateDTO.UrunAdi;
                        fiyat = yetenekTemsilcisiAbonelikUrunuCreateDTO.KDVliFiyat;
                        odemePeriodu = yetenekTemsilcisiAbonelikUrunuCreateDTO.OdemePeriodu;
                    }
                    catch (Exception)
                    {
                        return OdiResponse<AbonelikUrunuOdemePlaniOutputDTO>.Fail("Ödeme planı oluşturma başarısız.", "KullaniciAbonelikUrunu YetenekTemsilcisiAbonelikUrunuCreateDTO türüne dönüştürülemedi.", 400);
                    }

                    break;

                case (int)AbonelikTipi.Yapim:
                    try
                    {
                        yapimAbonelikUrunuCreateDTO = JsonSerializer.Deserialize<YapimAbonelikUrunuCreateDTO>(model.KullaniciAbonelikUrunu.ToString());
                        urunAdi = yapimAbonelikUrunuCreateDTO.UrunAdi;
                        fiyat = yapimAbonelikUrunuCreateDTO.KDVliFiyat;
                        odemePeriodu = yapimAbonelikUrunuCreateDTO.OdemePeriodu;
                    }
                    catch (Exception)
                    {
                        return OdiResponse<AbonelikUrunuOdemePlaniOutputDTO>.Fail("Ödeme planı oluşturma başarısız.", "KullaniciAbonelikUrunu YapimAbonelikUrunuCreateDTO türüne dönüştürülemedi.", 400);
                    }

                    break;

                default:
                    return OdiResponse<AbonelikUrunuOdemePlaniOutputDTO>.Fail("Ödeme planı oluşturma başarısız.", "Geçersiz Abonelik Tipi.", 400);
            }

            CreatePlanRequest iyzicoCreatePaymentPlanRequestModel = new CreatePlanRequest()
            {
                Locale = Locale.TR.ToString(),
                Name = urunAdi,
                ConversationId = "",
                TrialPeriodDays = model.DenemeGunSayisi,
                Price = fiyat.ToString().Replace(",", "."),
                CurrencyCode = Currency.TRY.ToString(),
                PaymentInterval = PaymentInterval.MONTHLY.ToString(),
                PaymentIntervalCount = odemePeriodu,
                PlanPaymentType = PlanPaymentType.RECURRING.ToString(),
                ProductReferenceCode = model.ProductReferenceCode
            };

            ResponseData<PlanResource> iyzicoCreatePaymentPlanResponseModel = Plan.Create(iyzicoCreatePaymentPlanRequestModel, await GetIyzicoOptions());

            if (iyzicoCreatePaymentPlanResponseModel != null && iyzicoCreatePaymentPlanResponseModel.Status == Status.SUCCESS.ToString())
            {
                string planReferanceCode = iyzicoCreatePaymentPlanResponseModel.Data.ReferenceCode;

                WebApiRequest webApiRequest = new WebApiRequest(_httpClientFactory);
                OdiResponse<string> apiResult = null;
                string url;

                switch (model.AbonelikTipi)
                {
                    case (int)AbonelikTipi.Performer:
                        url = _configuration.GetSection("GatewayServerURL").Value + "/servis/performer/abonelik-urunu/yeni-performer-abonelik-urunu";

                        performerAbonelikUrunuCreateDTO.ReferenceCode = planReferanceCode;

                        apiResult = await webApiRequest.Post<string, PerformerAbonelikUrunuCreateDTO>(url, jwtToken, performerAbonelikUrunuCreateDTO);

                        break;

                    case (int)AbonelikTipi.YetenekTemsilcisi:
                        url = _configuration.GetSection("GatewayServerURL").Value + "/servis/global/yeni-yetenek-temsilcisi-abonelik-urunu";

                        yetenekTemsilcisiAbonelikUrunuCreateDTO.ReferenceCode = planReferanceCode;

                        apiResult = await webApiRequest.Post<string, YetenekTemsilcisiAbonelikUrunuCreateDTO>(url, jwtToken, yetenekTemsilcisiAbonelikUrunuCreateDTO);

                        break;

                    case (int)AbonelikTipi.Yapim:
                        url = _configuration.GetSection("GatewayServerURL").Value + "/servis/global/yeni-yapim-abonelik-urunu";

                        yapimAbonelikUrunuCreateDTO.ReferenceCode = planReferanceCode;

                        apiResult = await webApiRequest.Post<string, YapimAbonelikUrunuCreateDTO>(url, jwtToken, yapimAbonelikUrunuCreateDTO);

                        break;
                }

                if (apiResult?.IsSuccessful == true)
                {
                    AbonelikUrunuOdemePlani abonelikUrunuOdemePlani = new AbonelikUrunuOdemePlani();
                    abonelikUrunuOdemePlani.AbonelikUrunId = model.AbonelikUrunId;
                    abonelikUrunuOdemePlani.ProductReferenceCode = model.ProductReferenceCode;
                    abonelikUrunuOdemePlani.ReferenceCode = planReferanceCode;
                    abonelikUrunuOdemePlani.AbonelikTipi = model.AbonelikTipi;
                    abonelikUrunuOdemePlani.OdemePeriodu = odemePeriodu;
                    abonelikUrunuOdemePlani.AbonelikFiyati = fiyat;
                    abonelikUrunuOdemePlani.DenemeGunSayisi = model.DenemeGunSayisi;
                    abonelikUrunuOdemePlani.Aktif = true;
                    abonelikUrunuOdemePlani.KullaniciAbonelikUrunId = apiResult.Data;

                    DateTime date = DateTime.Now;

                    abonelikUrunuOdemePlani.EklenmeTarihi = date;
                    abonelikUrunuOdemePlani.Ekleyen = user.AdSoyad;
                    abonelikUrunuOdemePlani.EkleyenId = user.Id;

                    abonelikUrunuOdemePlani.GuncellenmeTarihi = date;
                    abonelikUrunuOdemePlani.Guncelleyen = user.AdSoyad;
                    abonelikUrunuOdemePlani.GuncelleyenId = user.Id;

                    abonelikUrunuOdemePlani = await _abonelikUrunuOdemePlaniDataService.YeniAbonelikUrunuOdemePlani(abonelikUrunuOdemePlani);

                    return OdiResponse<AbonelikUrunuOdemePlaniOutputDTO>.Success("Ödeme planı oluşturma tamamlandı.", _mapper.Map<AbonelikUrunuOdemePlaniOutputDTO>(abonelikUrunuOdemePlani), 200);
                }
                else
                {
                    return OdiResponse<AbonelikUrunuOdemePlaniOutputDTO>.Fail("Ödeme planı oluşturma başarısız.", apiResult?.Errors ?? null, 400);
                }
            }
            else
            {
                return OdiResponse<AbonelikUrunuOdemePlaniOutputDTO>.Fail("Ödeme planı oluşturma başarısız.", $"İyzico ürün ödeme planı oluşturulamadı. İyzico Error Message: {iyzicoCreatePaymentPlanResponseModel?.ErrorMessage}", 400);
            }
        }

        public async Task<OdiResponse<bool>> OdemePlaniSil(ReferenceCodeDTO model, OdiUser user, string jwtToken)
        {
            AbonelikUrunuOdemePlani abonelikUrunuOdemePlani = await _abonelikUrunuOdemePlaniDataService.AbonelikUrunuOdemePlaniGetirByReferenceCode(model.ReferenceCode);

            if (abonelikUrunuOdemePlani != null && abonelikUrunuOdemePlani.Aktif == true)
            {
                DeletePlanRequest request = new DeletePlanRequest
                {
                    Locale = Locale.TR.ToString(),
                    ConversationId = "",
                    PricingPlanReferenceCode = model.ReferenceCode
                };

                IyzipayResourceV2 response = Plan.Delete(request, await GetIyzicoOptions());

                if (response != null && response.Status == Status.SUCCESS.ToString())
                {
                    abonelikUrunuOdemePlani.Aktif = false;

                    abonelikUrunuOdemePlani.GuncellenmeTarihi = DateTime.Now;
                    abonelikUrunuOdemePlani.Guncelleyen = user.AdSoyad;
                    abonelikUrunuOdemePlani.GuncelleyenId = user.Id;

                    await _abonelikUrunuOdemePlaniDataService.AbonelikUrunuOdemePlaniGuncelle(abonelikUrunuOdemePlani);

                    WebApiRequest webApiRequest = new WebApiRequest(_httpClientFactory);
                    OdiResponse<bool> apiResult = null;
                    string url;

                    switch (abonelikUrunuOdemePlani.AbonelikTipi)
                    {
                        case (int)AbonelikTipi.Performer:

                            url = _configuration.GetSection("GatewayServerURL").Value + "/servis/performer/abonelik-urunu/performer-abonelik-urun-durum-guncelle";

                            PerformerAbonelikUrunuIdDTO idDto1 = new PerformerAbonelikUrunuIdDTO
                            {
                                PerformerAbonelikUrunuId = abonelikUrunuOdemePlani.KullaniciAbonelikUrunId
                            };

                            apiResult = await webApiRequest.Post<bool, PerformerAbonelikUrunuIdDTO>(url, jwtToken, idDto1);

                            break;

                        case (int)AbonelikTipi.YetenekTemsilcisi:

                            url = _configuration.GetSection("GatewayServerURL").Value + "/servis/global/yetenek-temsilcisi-abonelik-urun-durum-guncelle";

                            YetenekTemsilcisiAbonelikUrunuIdDTO idDto2 = new YetenekTemsilcisiAbonelikUrunuIdDTO
                            {
                                YetenekTemsilcisiAbonelikUrunuId = abonelikUrunuOdemePlani.KullaniciAbonelikUrunId
                            };

                            apiResult = await webApiRequest.Post<bool, YetenekTemsilcisiAbonelikUrunuIdDTO>(url, jwtToken, idDto2);

                            break;

                        case (int)AbonelikTipi.Yapim:

                            url = _configuration.GetSection("GatewayServerURL").Value + "/servis/global/yapim-abonelik-urun-durum-guncelle";

                            YapimAbonelikUrunuIdDTO idDto3 = new YapimAbonelikUrunuIdDTO
                            {
                                YapimAbonelikUrunuId = abonelikUrunuOdemePlani.KullaniciAbonelikUrunId
                            };

                            apiResult = await webApiRequest.Post<bool, YapimAbonelikUrunuIdDTO>(url, jwtToken, idDto3);

                            break;
                    }

                    if (apiResult?.IsSuccessful == true)
                    {
                        return OdiResponse<bool>.Success("Ödeme planı silindi.", true, 200);
                    }
                    else
                    {
                        return OdiResponse<bool>.Fail("Ödeme planı silinemedi.", apiResult?.Errors ?? null, 400);
                    }
                }
                else
                {
                    return OdiResponse<bool>.Fail("Ödeme planı silme başarısız.", $"İyzico ürün ödeme planı silme başarısız. İyzico Error Message: {response?.ErrorMessage}", 400);
                }
            }
            else
            {
                return OdiResponse<bool>.Fail("Aktif ödeme planı bulunamadı.", "", 404);
            }
        }

        public async Task<OdiResponse<bool>> AbonelikUrunuSil(ReferenceCodeDTO model, OdiUser user)
        {
            AbonelikUrunu abonelikUrunu = await _abonelikUrunuDataService.AbonelikUrunuGetirByReferenceCode(model.ReferenceCode);

            if (abonelikUrunu != null && abonelikUrunu.Aktif == true)
            {
                DeleteProductRequest updateProductRequest = new DeleteProductRequest
                {
                    Locale = Locale.TR.ToString(),
                    ConversationId = "123456789",
                    ProductReferenceCode = model.ReferenceCode
                };

                IyzipayResourceV2 response = Product.Delete(updateProductRequest, await GetIyzicoOptions());

                if (response != null && response.Status == Status.SUCCESS.ToString())
                {
                    abonelikUrunu.Aktif = false;

                    abonelikUrunu.GuncellenmeTarihi = DateTime.Now;
                    abonelikUrunu.Guncelleyen = user.AdSoyad;
                    abonelikUrunu.GuncelleyenId = user.Id;

                    await _abonelikUrunuDataService.AbonelikUrunuGuncelle(abonelikUrunu);

                    return OdiResponse<bool>.Success("Abonelik ürünü silindi.", true, 200);
                }
                else
                {
                    return OdiResponse<bool>.Fail("Abonelik ürünü silme başarısız.", $"İyzico abonelik ürünü silme başarısız. İyzico Error Message: {response?.ErrorMessage}", 400);
                }
            }
            else
            {
                return OdiResponse<bool>.Fail("Aktif abonelik ürünü bulunamadı.", "", 404);
            }
        }

        public async Task<OdiResponse<bool>> AbonelikPaketiSatinAlma(AbonelikPaketiSatinAlmaInputDTO model, OdiUser user, string jwtToken)
        {
            CreatePaymentRequest request = new CreatePaymentRequest();

            request.Locale = Locale.TR.ToString();
            //request.ConversationId = "";
            request.Price = model.SatinAlmaBilgileri.Price;
            request.PaidPrice = model.SatinAlmaBilgileri.PaidPrice;
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            //request.BasketId = "B67832";
            //request.PaymentChannel = PaymentChannel.WEB.ToString();
            //request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = model.SatinAlmaBilgileri.CardHolderName;
            paymentCard.CardNumber = model.SatinAlmaBilgileri.CardNumber;
            paymentCard.ExpireMonth = model.SatinAlmaBilgileri.ExpireMonth;
            paymentCard.ExpireYear = model.SatinAlmaBilgileri.ExpireYear;
            paymentCard.Cvc = model.SatinAlmaBilgileri.Cvc;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Id = model.KullaniciId;
            //buyer.Name = user.AdSoyad.Substring(user.AdSoyad.LastIndexOf(' ') + 1);
            //buyer.Surname = user.AdSoyad.Substring(0, user.AdSoyad.LastIndexOf(' '));
            buyer.Name = model.SatinAlmaBilgileri.BuyerName;
            buyer.Surname = model.SatinAlmaBilgileri.BuyerSurname;
            buyer.GsmNumber = model.SatinAlmaBilgileri.BuyerGsmNumber;
            buyer.Email = model.SatinAlmaBilgileri.BuyerEmail;
            buyer.IdentityNumber = "11111111111";
            buyer.LastLoginDate = DateTime.Now.Subtract(new TimeSpan(1, 5, 13)).ToString("yyyy-MM-dd HH:mm:ss");
            buyer.RegistrationDate = "2010-08-14 23:59:59";
            buyer.RegistrationAddress = model.SatinAlmaBilgileri.BuyerAddress;
            buyer.Ip = "85.34.78.112";
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";
            buyer.ZipCode = "34000";
            request.Buyer = buyer;

            Address address = new Address();
            address.ContactName = model.SatinAlmaBilgileri.BuyerName + " " + model.SatinAlmaBilgileri.BuyerSurname;
            address.City = "Istanbul";
            address.Country = "Turkey";
            address.Description = model.SatinAlmaBilgileri.BuyerAddress;
            address.ZipCode = "34000";
            request.ShippingAddress = address;
            request.BillingAddress = address;

            List<BasketItem> basketItems = new List<BasketItem>();

            BasketItem basketItem = new BasketItem();
            basketItem.Id = model.KullaniciAbonelikUrunId;
            basketItem.Name = "ODI Abonelik Ürünü";
            basketItem.Category1 = "Yapımcı Paketleri";
            basketItem.ItemType = BasketItemType.VIRTUAL.ToString();
            basketItem.Price = model.SatinAlmaBilgileri.Price;
            basketItems.Add(basketItem);
            request.BasketItems = basketItems;

            Payment payment = Payment.Create(request, await GetIyzicoOptions());

            if (payment != null && payment.Status == Status.SUCCESS.ToString())
            {
                WebApiRequest webApiRequest = new WebApiRequest(_httpClientFactory);
                string url = _configuration.GetSection("GatewayServerURL").Value + "/servis/global/yapim-abonelik-olustur";

                YapimAbonelikCreateDTO requestModel = new YapimAbonelikCreateDTO();

                requestModel.YapimKullaniciId = model.KullaniciId;
                requestModel.AbonelikUrunId = model.KullaniciAbonelikUrunId;
                requestModel.AbonelikVeren = (int)YapimAbonelikVeren.SatinAlma;
                requestModel.UcretsizYenile = false;
                requestModel.Yenile = false;
                requestModel.AbonelikBaslangicTarihi = DateTime.Now;
                requestModel.AbonelikBitisTarihi = DateTime.Now.AddMonths(1);
                requestModel.Aktif = true;
                requestModel.SureUzat = true;
                requestModel.SureUzatmaSebebi = YapimAbonelikUzatmaSebepleri.KAY;

                OdiResponse<string> apiResult = await webApiRequest.Post<string, YapimAbonelikCreateDTO>(url, jwtToken, requestModel);

                if (apiResult?.IsSuccessful == true)
                {
                    AbonelikPaketiSatinAlma abonelikPaketiSatinAlma = new AbonelikPaketiSatinAlma();

                    abonelikPaketiSatinAlma.OdemeAlinanMiktar = payment.PaidPrice;
                    abonelikPaketiSatinAlma.PaymentId = payment.PaymentId;
                    abonelikPaketiSatinAlma.CardType = payment.CardType;
                    abonelikPaketiSatinAlma.CardAssociation = payment.CardAssociation;
                    abonelikPaketiSatinAlma.CardFamily = payment.CardFamily;
                    abonelikPaketiSatinAlma.BinNumber = payment.BinNumber;
                    abonelikPaketiSatinAlma.LastFourDigits = payment.LastFourDigits;
                    abonelikPaketiSatinAlma.AbonelikTuru = (int)AbonelikTipi.Yapim;
                    abonelikPaketiSatinAlma.KullaniciAbonelikUrunId = model.KullaniciAbonelikUrunId;
                    abonelikPaketiSatinAlma.KullaniciAbonelikId = apiResult.Data;
                    abonelikPaketiSatinAlma.KullaniciId = model.KullaniciId;

                    DateTime date = DateTime.Now;

                    abonelikPaketiSatinAlma.EklenmeTarihi = date;
                    abonelikPaketiSatinAlma.Ekleyen = user.AdSoyad;
                    abonelikPaketiSatinAlma.EkleyenId = user.Id;

                    abonelikPaketiSatinAlma.GuncellenmeTarihi = date;
                    abonelikPaketiSatinAlma.Guncelleyen = user.AdSoyad;
                    abonelikPaketiSatinAlma.GuncelleyenId = user.Id;

                    await _abonelikPaketiSatinAlmaDataService.YeniAbonelikPaketiSatinAlma(abonelikPaketiSatinAlma);

                    return OdiResponse<bool>.Success("Satın alma tamamlandı.", true, 200);
                }
                else
                {
                    return OdiResponse<bool>.Fail("Satın alma başarısız.", apiResult?.Errors ?? null, 400);
                }
            }
            else
            {
                return OdiResponse<bool>.Fail("Satın alma başarısız.", $"İyzico ürün ödeme başarısız. İyzico Error Message: {payment?.ErrorMessage}", 400);
            }

        }

        public async Task<OdiResponse<bool>> AbonelikPaketiAbonelikBaslatma(AbonelikPaketiAbonelikBaslatmaInputDTO model, OdiUser user, string jwtToken)
        {
            SubscriptionInitializeRequest request = new SubscriptionInitializeRequest();

            request.Locale = Locale.TR.ToString();

            CheckoutFormCustomer customer = new CheckoutFormCustomer();
            customer.Email = model.AboneOlmaBilgileri.BuyerEmail;
            customer.Name = model.AboneOlmaBilgileri.BuyerName;
            customer.Surname = model.AboneOlmaBilgileri.BuyerSurname;
            customer.GsmNumber = model.AboneOlmaBilgileri.BuyerGsmNumber;
            customer.IdentityNumber = "11111111111";

            Address address = new Address();
            address.City = "İstanbul";
            address.Country = "Türkiye";
            address.Description = model.AboneOlmaBilgileri.BuyerAddress;
            address.ContactName = model.AboneOlmaBilgileri.BuyerName + " " + model.AboneOlmaBilgileri.BuyerSurname;
            address.ZipCode = "34000";
            customer.BillingAddress = address;
            customer.ShippingAddress = address;

            request.Customer = customer;

            CardInfo paymentCard = new CardInfo();
            // Kayıtlı kartlar için bu alanlar kullanılır.
            // paymentCard.CardToken = "card Token";
            // paymentCard.ConsumerToken = "consumer Token";
            // paymentCard.UcsToken = "ucs Token";

            // Yeni kart ile işlem yapılacaksa bu alanlar kullanılır.
            paymentCard.CardHolderName = model.AboneOlmaBilgileri.CardHolderName;
            paymentCard.CardNumber = model.AboneOlmaBilgileri.CardNumber;
            paymentCard.ExpireMonth = model.AboneOlmaBilgileri.ExpireMonth;
            paymentCard.ExpireYear = model.AboneOlmaBilgileri.ExpireYear;
            paymentCard.Cvc = model.AboneOlmaBilgileri.Cvc;

            // Bu alan zorunlu. Kartın kayıt edilip edilmeyeceğini belirler.
            paymentCard.RegisterConsumerCard = true;

            request.PaymentCard = paymentCard;

            //request.ConversationId = "";
            request.PricingPlanReferenceCode = model.AboneOlmaBilgileri.PricingPlanReferenceCode;

            // Abonelik başlangıcında durumu belirtir. ACTIVE veya PENDING alıyor. ACTIVE olursa başlamış oluyor.
            request.SubscriptionInitialStatus = SubscriptionStatus.ACTIVE.ToString();

            ResponseData<SubscriptionCreatedResource> response = Subscription.Initialize(request, await GetIyzicoOptions());

            if (response != null && response.Status == Status.SUCCESS.ToString())
            {
                WebApiRequest webApiRequest = new WebApiRequest(_httpClientFactory);
                OdiResponse<string>? apiResult = null;
                string url;

                decimal fiyat = 0;
                int odemePeriodu = 0;

                switch (model.AbonelikOlusturmaBilgileri.AbonelikTipi)
                {
                    case (int)AbonelikTipi.Performer:

                        url = _configuration.GetSection("GatewayServerURL").Value + "/servis/performer/abonelik-urunu/performer-abonelik-urunu-liste";

                        OdiResponse<List<PerformerAbonelikUrunuOutputDTO>> performerUrunListeResult = await webApiRequest.Get<List<PerformerAbonelikUrunuOutputDTO>>(url, jwtToken);

                        if (performerUrunListeResult?.IsSuccessful == true)
                        {
                            PerformerAbonelikUrunuOutputDTO? performerUrun = performerUrunListeResult?.Data?.FirstOrDefault(x => x.ReferenceCode == model.AbonelikOlusturmaBilgileri.PlanReferenceCode && x.Aktif);

                            if (performerUrun != null)
                            {
                                fiyat = performerUrun.KDVliFiyat;
                                odemePeriodu = performerUrun.OdemePeriodu;

                                url = _configuration.GetSection("GatewayServerURL").Value + "/servis/performer/yetenek-temsilcisi/performer-menajer-getir";

                                KullaniciIdDTO menajerGetirRequest = new KullaniciIdDTO();

                                menajerGetirRequest.KullaniciId = model.AbonelikOlusturmaBilgileri.KullaniciId;

                                OdiResponse<PerformerMenajerListItemOutputDTO> menajerGetirResult = await webApiRequest.Post<PerformerMenajerListItemOutputDTO, KullaniciIdDTO>(url, jwtToken, menajerGetirRequest);

                                if (menajerGetirResult?.IsSuccessful == true)
                                {
                                    url = _configuration.GetSection("GatewayServerURL").Value + "/servis/performer/performer-abonelik/performer-abonelik-olustur";

                                    PerformerAbonelikCreateDTO performerRequestModel = new PerformerAbonelikCreateDTO();

                                    performerRequestModel.PerformerId = model.AbonelikOlusturmaBilgileri.KullaniciId;
                                    performerRequestModel.MenajerId = menajerGetirResult.Data.MenajerId;
                                    performerRequestModel.AbonelikUrunId = model.AbonelikOlusturmaBilgileri.KullaniciAbonelikUrunId;
                                    performerRequestModel.KalanFotografSayisi = performerUrun.FotografSayisi;
                                    performerRequestModel.KalanTanitimVideosuSayisi = performerUrun.TanitimVideosuSayisi;
                                    performerRequestModel.KalanShowreelSayisi = performerUrun.ShowreelSayisi;
                                    performerRequestModel.KalanPerformerVideosuSayisi = performerUrun.PerformansVideosuSayisi;
                                    performerRequestModel.AbonelikVeren = (int)YetenekTemsilcisiAbonelikVeren.SatinAlma;
                                    performerRequestModel.UcretsizYenile = false;
                                    performerRequestModel.Yenile = false;
                                    performerRequestModel.AbonelikBaslangicTarihi = DateTime.Now;
                                    performerRequestModel.AbonelikBitisTarihi = DateTime.Now.AddMonths(performerUrun.OdemePeriodu);
                                    performerRequestModel.Aktif = true;
                                    performerRequestModel.SureUzat = true;
                                    performerRequestModel.SureUzatmaSebebi = PerformerAbonelikUzatmaSebepleri.KAY;
                                    performerRequestModel.AbonelikReferenceCode = response.Data.ReferenceCode;
                                    performerRequestModel.KullaniciReferenceCode = response.Data.CustomerReferenceCode;

                                    apiResult = await webApiRequest.Post<string, PerformerAbonelikCreateDTO>(url, jwtToken, performerRequestModel);
                                }
                                else
                                {
                                    return OdiResponse<bool>.Fail("Abone olma başarısız.", apiResult?.Errors ?? null, 400);
                                }
                            }
                            else
                            {
                                return OdiResponse<bool>.Fail("Abone olma başarısız.", "Aktif abonelik ürünü bulunamadı.", 400);
                            }
                        }
                        else
                        {
                            return OdiResponse<bool>.Fail("Abone olma başarısız.", apiResult?.Errors ?? null, 400);
                        }

                        break;

                    case (int)AbonelikTipi.YetenekTemsilcisi:

                        url = _configuration.GetSection("GatewayServerURL").Value + "/servis/global/yetenek-temsilcisi-abonelik-urunu-liste";

                        OdiResponse<List<YetenekTemsilcisiAbonelikUrunuOutputDTO>> yetenekTemsilcisiUrunListeResult = await webApiRequest.Get<List<YetenekTemsilcisiAbonelikUrunuOutputDTO>>(url, jwtToken);

                        if (yetenekTemsilcisiUrunListeResult?.IsSuccessful == true)
                        {
                            YetenekTemsilcisiAbonelikUrunuOutputDTO? yetenekTemsilcisiUrun = yetenekTemsilcisiUrunListeResult?.Data?.FirstOrDefault(x => x.ReferenceCode == model.AbonelikOlusturmaBilgileri.PlanReferenceCode && x.Aktif);

                            if (yetenekTemsilcisiUrun != null)
                            {
                                fiyat = yetenekTemsilcisiUrun.KDVliFiyat;
                                odemePeriodu = yetenekTemsilcisiUrun.OdemePeriodu;

                                url = _configuration.GetSection("GatewayServerURL").Value + "/servis/global/yetenek-temsilcisi-abonelik-olustur";

                                YetenekTemsilcisiAbonelikCreateDTO yetenekTemsilcisiRequestModel = new YetenekTemsilcisiAbonelikCreateDTO();

                                yetenekTemsilcisiRequestModel.YetenekTemsilcisiId = model.AbonelikOlusturmaBilgileri.KullaniciId;
                                yetenekTemsilcisiRequestModel.AbonelikUrunId = model.AbonelikOlusturmaBilgileri.KullaniciAbonelikUrunId;
                                yetenekTemsilcisiRequestModel.KalanPerformerSayisi = yetenekTemsilcisiUrun.PerformerSayisi;
                                yetenekTemsilcisiRequestModel.KalanPremiumSayisi = yetenekTemsilcisiUrun.PremiumProfilSayisi;
                                yetenekTemsilcisiRequestModel.AbonelikVeren = (int)YetenekTemsilcisiAbonelikVeren.SatinAlma;
                                yetenekTemsilcisiRequestModel.UcretsizYenile = false;
                                yetenekTemsilcisiRequestModel.Yenile = false;
                                yetenekTemsilcisiRequestModel.AbonelikBaslangicTarihi = DateTime.Now;
                                yetenekTemsilcisiRequestModel.AbonelikBitisTarihi = DateTime.Now.AddMonths(yetenekTemsilcisiUrun.OdemePeriodu);
                                yetenekTemsilcisiRequestModel.Aktif = true;
                                yetenekTemsilcisiRequestModel.SureUzat = true;
                                yetenekTemsilcisiRequestModel.SureUzatmaSebebi = YetenekTemsilcisiAbonelikUzatmaSebepleri.KAY;
                                yetenekTemsilcisiRequestModel.AbonelikReferenceCode = response.Data.ReferenceCode;
                                yetenekTemsilcisiRequestModel.KullaniciReferenceCode = response.Data.CustomerReferenceCode;

                                apiResult = await webApiRequest.Post<string, YetenekTemsilcisiAbonelikCreateDTO>(url, jwtToken, yetenekTemsilcisiRequestModel);
                            }
                            else
                            {
                                return OdiResponse<bool>.Fail("Abone olma başarısız.", "Aktif abonelik ürünü bulunamadı.", 400);
                            }
                        }
                        else
                        {
                            return OdiResponse<bool>.Fail("Abone olma başarısız.", apiResult?.Errors ?? null, 400);
                        }

                        break;

                    case (int)AbonelikTipi.Yapim:

                        url = _configuration.GetSection("GatewayServerURL").Value + "/servis/global/yapim-abonelik-urunu-liste";

                        OdiResponse<List<YapimAbonelikUrunuOutputDTO>> yapimUrunListeResult = await webApiRequest.Get<List<YapimAbonelikUrunuOutputDTO>>(url, jwtToken);

                        if (yapimUrunListeResult?.IsSuccessful == true)
                        {
                            YapimAbonelikUrunuOutputDTO? yapimUrun = yapimUrunListeResult?.Data?.FirstOrDefault(x => x.ReferenceCode == model.AbonelikOlusturmaBilgileri.PlanReferenceCode && x.Aktif);

                            if (yapimUrun != null)
                            {
                                fiyat = yapimUrun.KDVliFiyat;
                                odemePeriodu = yapimUrun.OdemePeriodu;

                                url = _configuration.GetSection("GatewayServerURL").Value + "/servis/global/yapim-abonelik-olustur";

                                YapimAbonelikCreateDTO yapimRequestModel = new YapimAbonelikCreateDTO();

                                yapimRequestModel.YapimKullaniciId = model.AbonelikOlusturmaBilgileri.KullaniciId;
                                yapimRequestModel.AbonelikUrunId = model.AbonelikOlusturmaBilgileri.KullaniciAbonelikUrunId;
                                yapimRequestModel.AbonelikVeren = (int)YapimAbonelikVeren.SatinAlma;
                                yapimRequestModel.UcretsizYenile = false;
                                yapimRequestModel.Yenile = false;
                                yapimRequestModel.AbonelikBaslangicTarihi = DateTime.Now;
                                yapimRequestModel.AbonelikBitisTarihi = DateTime.Now.AddMonths(yapimUrun.OdemePeriodu);
                                yapimRequestModel.Aktif = true;
                                yapimRequestModel.SureUzat = true;
                                yapimRequestModel.SureUzatmaSebebi = YapimAbonelikUzatmaSebepleri.KAY;
                                yapimRequestModel.AbonelikReferenceCode = response.Data.ReferenceCode;
                                yapimRequestModel.KullaniciReferenceCode = response.Data.CustomerReferenceCode;

                                apiResult = await webApiRequest.Post<string, YapimAbonelikCreateDTO>(url, jwtToken, yapimRequestModel);
                            }
                            else
                            {
                                return OdiResponse<bool>.Fail("Abone olma başarısız.", "Aktif abonelik ürünü bulunamadı.", 400);
                            }
                        }
                        else
                        {
                            return OdiResponse<bool>.Fail("Abone olma başarısız.", apiResult?.Errors ?? null, 400);
                        }

                        break;
                }

                if (apiResult?.IsSuccessful == true)
                {
                    AbonelikKartlari abonelikKartlari = new AbonelikKartlari();

                    DateTime date = DateTime.Now;

                    abonelikKartlari.BinNumber = Convert.ToInt32(model.AboneOlmaBilgileri.CardNumber.Substring(0, 6));
                    abonelikKartlari.LastFourDigit = Convert.ToInt32(model.AboneOlmaBilgileri.CardNumber.Substring(model.AboneOlmaBilgileri.CardNumber.Length - 4, 4));
                    abonelikKartlari.KartKaydiTarihi = date;
                    abonelikKartlari.Aktif = true;

                    abonelikKartlari.EklenmeTarihi = date;
                    abonelikKartlari.Ekleyen = user.AdSoyad;
                    abonelikKartlari.EkleyenId = user.Id;

                    abonelikKartlari.GuncellenmeTarihi = date;
                    abonelikKartlari.Guncelleyen = user.AdSoyad;
                    abonelikKartlari.GuncelleyenId = user.Id;

                    abonelikKartlari = await _abonelikKartlariDataService.YeniAbonelikKartlari(abonelikKartlari);

                    AbonelikPaketiAboneOlma abonelikPaketiAboneOlma = new AbonelikPaketiAboneOlma();

                    abonelikPaketiAboneOlma.KullaniciId = model.AbonelikOlusturmaBilgileri.KullaniciId;
                    abonelikPaketiAboneOlma.KullaniciAbonelikUrunuId = model.AbonelikOlusturmaBilgileri.KullaniciAbonelikUrunId;
                    abonelikPaketiAboneOlma.AbonelikTipi = model.AbonelikOlusturmaBilgileri.AbonelikTipi;
                    abonelikPaketiAboneOlma.KullaniciAbonelikId = apiResult.Data;
                    abonelikPaketiAboneOlma.AbonelikReferanceCode = response.Data.ReferenceCode;
                    abonelikPaketiAboneOlma.PlanReferanceCode = response.Data.PricingPlanReferenceCode;
                    abonelikPaketiAboneOlma.KullaniciReferanceCode = response.Data.CustomerReferenceCode;
                    abonelikPaketiAboneOlma.Fiyat = fiyat;
                    abonelikPaketiAboneOlma.OdemePeriodu = odemePeriodu;
                    abonelikPaketiAboneOlma.AbonelikKartlariId = abonelikKartlari.Id;

                    abonelikPaketiAboneOlma.EklenmeTarihi = date;
                    abonelikPaketiAboneOlma.Ekleyen = user.AdSoyad;
                    abonelikPaketiAboneOlma.EkleyenId = user.Id;

                    abonelikPaketiAboneOlma.GuncellenmeTarihi = date;
                    abonelikPaketiAboneOlma.Guncelleyen = user.AdSoyad;
                    abonelikPaketiAboneOlma.GuncelleyenId = user.Id;

                    abonelikPaketiAboneOlma = await _abonelikPaketiAboneOlmaDataService.YeniAbonelikPaketiAboneOlma(abonelikPaketiAboneOlma);

                    return OdiResponse<bool>.Success("Abone olma başarılı.", true, 200);
                }
                else
                {
                    return OdiResponse<bool>.Fail("Abone olma başarısız.", apiResult?.Errors ?? null, 400);
                }
            }
            else
            {
                return OdiResponse<bool>.Fail("Abone olma başarısız.", $"İyzico abonelik başlatma başarısız. İyzico Error Message: {response?.ErrorMessage}", 400);
            }
        }

        public async Task<OdiResponse<List<PaketAbonelikOdemeOutputDTO>>> PaketAbonelikOdemeListesi(KullaniciIdDTO model, string jwtToken)
        {
            List<AbonelikPaketiAboneOlma> abonelikPaketiAboneOlmaList = await _abonelikPaketiAboneOlmaDataService.AbonelikPaketiAboneOlmaListesiGetirByKullaniciId(model.KullaniciId);

            List<PaketAbonelikOdemeOutputDTO> resultList = new List<PaketAbonelikOdemeOutputDTO>();

            foreach (var item in abonelikPaketiAboneOlmaList)
            {
                PaketAbonelikOdemeOutputDTO dto = new PaketAbonelikOdemeOutputDTO();

                dto.IslemId = item.Id;
                dto.KullaniciId = item.KullaniciId;
                dto.OdenenFiyat = item.Fiyat.ToString();
                dto.OdemePeriyodu = item.OdemePeriodu;
                dto.AbonelikBaslangicTarihi = item.EklenmeTarihi;

                AbonelikKartlari abonelikKartlari = await _abonelikKartlariDataService.AbonelikKartiGetir(item.AbonelikKartlariId);

                dto.KartBinNumber = abonelikKartlari.BinNumber.ToString();
                dto.KartLastFourDigit = abonelikKartlari.LastFourDigit.ToString();

                WebApiRequest webApiRequest = new WebApiRequest(_httpClientFactory);
                OdiResponse<string> apiResult = null;
                string url;

                AbonelikUrunuIdDTO requestModel = new AbonelikUrunuIdDTO();
                requestModel.AbonelikUrunuId = item.KullaniciAbonelikUrunuId;

                switch (item.AbonelikTipi)
                {
                    case (int)AbonelikTipi.Performer:
                        url = _configuration.GetSection("GatewayServerURL").Value + "/servis/performer/abonelik-urunu/performer-abonelik-urunu-isim-getir";

                        apiResult = await webApiRequest.Post<string, AbonelikUrunuIdDTO>(url, jwtToken, requestModel);

                        break;

                    case (int)AbonelikTipi.YetenekTemsilcisi:
                        url = _configuration.GetSection("GatewayServerURL").Value + "/servis/global/yetenek-temsilcisi-abonelik-urunu-isim-getir";

                        apiResult = await webApiRequest.Post<string, AbonelikUrunuIdDTO>(url, jwtToken, requestModel);

                        break;

                    case (int)AbonelikTipi.Yapim:
                        url = _configuration.GetSection("GatewayServerURL").Value + "/servis/global/yapim-abonelik-urunu-isim-getir";

                        apiResult = await webApiRequest.Post<string, AbonelikUrunuIdDTO>(url, jwtToken, requestModel);

                        break;
                }

                if (apiResult?.IsSuccessful == true)
                {
                    dto.KullaniciAbonelikUrunuAdi = apiResult.Data;
                }
                else
                {
                    return OdiResponse<List<PaketAbonelikOdemeOutputDTO>>.Fail("Paket abonelik ödeme listesi alınırken hata oluştu.", apiResult?.Errors ?? null, 400);
                }

                resultList.Add(dto);
            }

            return OdiResponse<List<PaketAbonelikOdemeOutputDTO>>.Success("Paket abonelik ödeme listesi getirildi.", resultList, 200);
        }

        public async Task<OdiResponse<List<PaketSatinAlmaOdemeOutputDTO>>> PaketSatinAlmaOdemeListesi(KullaniciIdDTO model, string jwtToken)
        {
            List<AbonelikPaketiSatinAlma> abonelikPaketiSatinAlmaList = await _abonelikPaketiSatinAlmaDataService.AbonelikUrunListesiGetirByKullaniciId(model.KullaniciId);

            List<PaketSatinAlmaOdemeOutputDTO> resultList = new List<PaketSatinAlmaOdemeOutputDTO>();

            foreach (var item in abonelikPaketiSatinAlmaList)
            {
                PaketSatinAlmaOdemeOutputDTO dto = new PaketSatinAlmaOdemeOutputDTO();

                dto.IslemId = item.Id;
                dto.KullaniciId = item.KullaniciId;
                dto.OdemeAlinanMiktar = item.OdemeAlinanMiktar;
                dto.SatinAlmaTarihi = item.EklenmeTarihi;
                dto.CardType = item.CardType;
                dto.CardAssociation = item.CardAssociation;
                dto.CardFamily = item.CardFamily;
                dto.BinNumber = item.BinNumber;
                dto.LastFourDigits = item.LastFourDigits;

                WebApiRequest webApiRequest = new WebApiRequest(_httpClientFactory);
                OdiResponse<string> apiResult = null;
                string url;

                AbonelikUrunuIdDTO requestModel = new AbonelikUrunuIdDTO();
                requestModel.AbonelikUrunuId = item.KullaniciAbonelikUrunId;

                switch (item.AbonelikTuru)
                {
                    case (int)AbonelikTipi.Performer:
                        url = _configuration.GetSection("GatewayServerURL").Value + "/servis/performer/abonelik-urunu/performer-abonelik-urunu-isim-getir";

                        apiResult = await webApiRequest.Post<string, AbonelikUrunuIdDTO>(url, jwtToken, requestModel);

                        break;

                    case (int)AbonelikTipi.YetenekTemsilcisi:
                        url = _configuration.GetSection("GatewayServerURL").Value + "/servis/global/yetenek-temsilcisi-abonelik-urunu-isim-getir";

                        apiResult = await webApiRequest.Post<string, AbonelikUrunuIdDTO>(url, jwtToken, requestModel);

                        break;

                    case (int)AbonelikTipi.Yapim:
                        url = _configuration.GetSection("GatewayServerURL").Value + "/servis/global/yapim-abonelik-urunu-isim-getir";

                        apiResult = await webApiRequest.Post<string, AbonelikUrunuIdDTO>(url, jwtToken, requestModel);

                        break;
                }

                if (apiResult?.IsSuccessful == true)
                {
                    dto.KullaniciAbonelikUrunuAdi = apiResult.Data;
                }
                else
                {
                    return OdiResponse<List<PaketSatinAlmaOdemeOutputDTO>>.Fail("Paket abonelik ödeme listesi alınırken hata oluştu.", apiResult?.Errors ?? null, 400);
                }

                resultList.Add(dto);
            }

            return OdiResponse<List<PaketSatinAlmaOdemeOutputDTO>>.Success("Paket abonelik ödeme listesi getirildi.", resultList, 200);
        }

        public async Task<OdiResponse<AbonelikKartBilgisiGetirOutputDTO>> AbonelikKartBilgisiGetir(AbonelikReferenceCodeDTO model)
        {
            AbonelikPaketiAboneOlma abonelikPaketiAboneOlma = await _abonelikPaketiAboneOlmaDataService.AbonelikPaketiAboneOlmaGetirByAboneReferenceCode(model.AbonelikReferenceCode);

            AbonelikKartlari abonelikKartlari = await _abonelikKartlariDataService.AbonelikKartiGetir(abonelikPaketiAboneOlma.AbonelikKartlariId);

            AbonelikKartBilgisiGetirOutputDTO dto = new AbonelikKartBilgisiGetirOutputDTO();

            dto.KartBinNumber = abonelikKartlari.BinNumber.ToString();
            dto.KartLastFourDigit = abonelikKartlari.LastFourDigit.ToString();

            return OdiResponse<AbonelikKartBilgisiGetirOutputDTO>.Success("Abonelik kart bilgisi getirildi.", dto, 200);
        }

        public async Task<OdiResponse<AboneligiSonlandirOutputDTO>> AboneligiSonlandir(AboneligiSonlandirInputDTO model, string jwtToken)
        {
            CancelSubscriptionRequest request = new CancelSubscriptionRequest
            {
                Locale = Locale.TR.ToString(),
                SubscriptionReferenceCode = model.AbonelikReferenceCode
            };

            IyzipayResourceV2 response = Subscription.Cancel(request, await GetIyzicoOptions());

            if (response != null && response.Status == Status.SUCCESS.ToString())
            {
                WebApiRequest webApiRequest = new WebApiRequest(_httpClientFactory);
                OdiResponse<AboneligiSonlandirOutputDTO>? apiResult = null;
                string url;

                switch (model.AbonelikTipi)
                {
                    case (int)AbonelikTipi.Performer:
                        url = _configuration.GetSection("GatewayServerURL").Value + "/servis/performer/performer-abonelik/performer-abonelik-bitir";

                        apiResult = await webApiRequest.Post<AboneligiSonlandirOutputDTO, AboneligiSonlandirInputDTO>(url, jwtToken, model);

                        break;

                    case (int)AbonelikTipi.YetenekTemsilcisi:
                        url = _configuration.GetSection("GatewayServerURL").Value + "/servis/global/yetenek-temsilcisi-abonelik-bitir";

                        apiResult = await webApiRequest.Post<AboneligiSonlandirOutputDTO, AboneligiSonlandirInputDTO>(url, jwtToken, model);

                        break;

                    case (int)AbonelikTipi.Yapim:
                        url = _configuration.GetSection("GatewayServerURL").Value + "/servis/global/yapim-abonelik-bitir";

                        apiResult = await webApiRequest.Post<AboneligiSonlandirOutputDTO, AboneligiSonlandirInputDTO>(url, jwtToken, model);

                        break;
                }

                if (apiResult?.IsSuccessful == true)
                {
                    return OdiResponse<AboneligiSonlandirOutputDTO>.Success("Abonelik iptali başarılı.", apiResult.Data, 200);
                }
                else
                {
                    return OdiResponse<AboneligiSonlandirOutputDTO>.Fail("Abonelik iptali başarısız.", apiResult?.Errors ?? null, 400);
                }
            }
            else
            {
                return OdiResponse<AboneligiSonlandirOutputDTO>.Fail("Abonelik iptali başarısız.", $"İyzico abonelik iptali başarısız. İyzico Error Message: {response?.ErrorMessage}", 400);
            }
        }

        public async Task<OdiResponse<AboneligiSonlandirOutputDTO>> AbonelikIptalTalebininAlinmasi(AbonelikYukseltmeTalepCreateDTO model, OdiUser user, string jwtToken)
        {
            AbonelikYukseltmeTalep abonelikYukseltmeTalep = _mapper.Map<AbonelikYukseltmeTalep>(model);

            abonelikYukseltmeTalep.Aktif = true;

            DateTime date = DateTime.Now;

            abonelikYukseltmeTalep.EklenmeTarihi = date;
            abonelikYukseltmeTalep.Ekleyen = user.AdSoyad;
            abonelikYukseltmeTalep.EkleyenId = user.Id;

            abonelikYukseltmeTalep.GuncellenmeTarihi = date;
            abonelikYukseltmeTalep.Guncelleyen = user.AdSoyad;
            abonelikYukseltmeTalep.GuncelleyenId = user.Id;

            abonelikYukseltmeTalep = await _abonelikYukseltmeTalepDataService.YeniAbonelikYukseltmeTalep(abonelikYukseltmeTalep);

            WebApiRequest webApiRequest = new WebApiRequest(_httpClientFactory);
            OdiResponse<bool>? apiResult = null;
            string url;

            AbonelikIdDTO requestModel = new AbonelikIdDTO();

            requestModel.AbonelikId = abonelikYukseltmeTalep.MevcutKullaniciAbonelikId;

            switch (model.AbonelikTipi)
            {
                case (int)AbonelikTipi.Performer:
                    url = _configuration.GetSection("GatewayServerURL").Value + "/servis/performer/performer-abonelik/performer-abonelik-yenilemeyi-kapat";

                    apiResult = await webApiRequest.Post<bool, AbonelikIdDTO>(url, jwtToken, requestModel);

                    break;

                case (int)AbonelikTipi.YetenekTemsilcisi:
                    url = _configuration.GetSection("GatewayServerURL").Value + "/servis/global/yetenek-temsilcisi-abonelik-yenilemeyi-kapat";

                    apiResult = await webApiRequest.Post<bool, AbonelikIdDTO>(url, jwtToken, requestModel);

                    break;

                case (int)AbonelikTipi.Yapim:
                    url = _configuration.GetSection("GatewayServerURL").Value + "/servis/global/yapim-abonelik-yenilemeyi-kapat";

                    apiResult = await webApiRequest.Post<bool, AbonelikIdDTO>(url, jwtToken, requestModel);

                    break;
            }

            if (apiResult?.IsSuccessful == true)
            {
                return OdiResponse<AboneligiSonlandirOutputDTO>.Success("Abonelik iptal talebi alındı.", new AboneligiSonlandirOutputDTO() { AbonelikBitisTarihi = abonelikYukseltmeTalep.MevcutAbonelikBitisTarihi }, 200);
            }
            else
            {
                return OdiResponse<AboneligiSonlandirOutputDTO>.Fail("Abonelik iptal talebi kaydı başarısız.", apiResult?.Errors ?? null, 400);
            }
        }

        public async Task<OdiResponse<bool>> AbonelikYukseltme(AbonelikYukseltmeInputDTO model, OdiUser user, string jwtToken)
        {
            AbonelikYukseltmeTalep abonelikYukseltmeTalep = await _abonelikYukseltmeTalepDataService.AbonelikYukseltmeTalepGetirByKullaniciveAbonelikId(model.KullaniciId, model.KullaniciAbonelikId);

            AboneligiSonlandirInputDTO aboneligiSonlandirInputDTO = new AboneligiSonlandirInputDTO();

            aboneligiSonlandirInputDTO.KullaniciId = model.KullaniciId;
            aboneligiSonlandirInputDTO.AbonelikReferenceCode = abonelikYukseltmeTalep.MevcutAbonelikReferenceCode;
            aboneligiSonlandirInputDTO.AbonelikTipi = abonelikYukseltmeTalep.AbonelikTipi;

            OdiResponse<AboneligiSonlandirOutputDTO> aboneligiSonlandirResponse = await AboneligiSonlandir(aboneligiSonlandirInputDTO, jwtToken);

            if (aboneligiSonlandirResponse.IsSuccessful)
            {
                AbonelikPaketiAbonelikBaslatmaInputDTO abonelikPaketiAbonelikBaslatmaInputDTO = new AbonelikPaketiAbonelikBaslatmaInputDTO();

                //TODO Buraya bakılacak. Abonelik yeniden başlatılırken kart bilgilerini gibi kullanıcı bilgileri gerekli.
                //OdiResponse<bool> abonelikPaketiAbonelikBaslatmaResponse = await AbonelikPaketiAbonelikBaslatma(abonelikPaketiAbonelikBaslatmaInputDTO, user, jwtToken);
                return OdiResponse<bool>.Success("Abonelik yükseltme başarılı.", true, 200);
            }
            else
            {
                return OdiResponse<bool>.Fail("Abonelik yükseltme başarısız.", aboneligiSonlandirResponse?.Errors ?? null, 400);
            }
        }
    }
}