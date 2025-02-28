using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Model.V2;
using Iyzipay.Model.V2.Subscription;
using OdiApp.DTOs.OdemeDTOs.IyzicoDtos;
using OdiApp.DTOs.SharedDTOs;

namespace OdiApp.BusinessLayer.Services.OdemeLogicServices.IyzicoLogicServices
{
    public interface IIyzicoLogicService
    {
        //Ödeme
        Task<OdiResponse<Payment>> OdemeYapTest(OdemeYapTestInputDTO model);

        //Abonelik ürünü ve planı oluşturma (admin)
        Task<OdiResponse<ResponseData<PlanResource>>> TestAbonelikUrunVePlanOlusturma();

        //Abonelik ürünlerini listeleme
        Task<OdiResponse<ResponsePagingData<ProductResource>>> AbonelikUrunListeleme();

        //Abonelik ürün planları listeleme
        Task<OdiResponse<ResponsePagingData<PlanResource>>> UrunPlanListeleme();

        //Abonelik başlatma
        Task<OdiResponse<ResponseData<SubscriptionCreatedResource>>> AbonelikBaslatma();

        //Abonelik yükseltme NEXT PRIOD
        Task<OdiResponse<IyzipayResourceV2>> AbonelikUpgrade();

        Task<OdiResponse<Card>> KartKaydet();

        Task<OdiResponse<Card>> VarolanKullaniciyaKartEkle();

        Task<OdiResponse<CardList>> KullaniciKartListesi();
    }
}