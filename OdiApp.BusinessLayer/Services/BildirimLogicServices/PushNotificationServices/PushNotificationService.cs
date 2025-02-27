using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.DTOs.BildirimDTOs.OneSignalDTOs;
using OdiApp.DTOs.Enums;
using OdiApp.EntityLayer.BildirimModels;
using RestSharp;
using System.Text.Json;

namespace OdiApp.BusinessLayer.Services.BildirimLogicServices.PushNotificationServices
{
    public class PushNotificationService : IPushNotificationService
    {
        private readonly string APIKEY = "Basic NmY0MTU3MTEtMjJkMi00Y2U4LTk3NGUtYmJmNmQxNzFkNzhi";
        private readonly string APIID = "9296fb85-890c-493a-ad8c-96bc57434b9a";
        private readonly IAmazonS3Service _amazonS3Service;
        public PushNotificationService(IAmazonS3Service amazonS3Service)
        {
            _amazonS3Service = amazonS3Service;
        }

        public async Task<bool> SendPushAll()
        {
            //PushModel push = new PushModel();
            //List<string> externalids = new List<string>();
            //externalids.Add("8fc7fddd-be9b-4a76-bf80-54c7b5be58e4");
            //push.included_segments.external_id = externalids;
            //PushContent pushContent = new PushContent();
            //pushContent.en = "test";
            //pushContent.tr = "deneme";
            //push.target_channel = "push";

            //push.contents = pushContent;
            //push.isAndroid = true;

            //var options = new RestClientOptions("https://onesignal.com");
            //var client = new RestClient(options);
            //var request = new RestRequest("/api/v1/notifications");

            //request.AddHeader("accept", "application/json");
            //request.AddHeader("Content-Type", "application/json");
            //request.AddHeader("Authorization", APIKEY);
            //request.AddJsonBody(JsonSerializer.Serialize(push), false);
            //try
            //{
            //    var response = await client.ExecuteAsync(request);
            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}
            var options = new RestClientOptions("https://onesignal.com")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/api/v1/notifications", Method.Post);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", APIKEY);

            PushModel push = new PushModel();
            List<string> externalids = new List<string>();
            externalids.Add("8fc7fddd-be9b-4a76-bf80-54c7b5be58e4");
            push.include_aliases.external_id = externalids;
            PushContents pushContent = new PushContents();
            pushContent.en = "test";
            pushContent.tr = "deneme";
            push.contents = pushContent;
            push.headings.tr = "TR Baslik";
            push.headings.en = "EN Baslik";
            push.target_channel = "push";
            push.subtitle.tr = "tr Subtitle";
            push.subtitle.en = "en Subtitle";
            push.name = "INTERNAL_CAMPAIGN_NAME";
            List<PushButton> buttons = new List<PushButton>();
            PushButton okButton = new PushButton();
            okButton.id = "ok";
            okButton.text = "Kabul Et";
            okButton.icon = "checkmark";
            okButton.icon_type = "system";
            buttons.Add(okButton);

            PushButton redButton = new PushButton();
            okButton.id = "disc";
            okButton.text = "Reddet";
            okButton.icon = "xmark";
            okButton.icon_type = "system";
            buttons.Add(redButton);

            //push.buttons = buttons;

            push.ios_badgeType = "Increase";
            push.ios_badgeCount = 1;
            push.ios_sound = "default";

            push.chrome_icon = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTEdJx5FSeT-6mNoGh2gjJMUu313u-UmTowPQ&usqp=CAU";
            push.chrome_web_icon = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTEdJx5FSeT-6mNoGh2gjJMUu313u-UmTowPQ&usqp=CAU";
            push.chrome_web_image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTEdJx5FSeT-6mNoGh2gjJMUu313u-UmTowPQ&usqp=CAU";

            push.ios_attachments.image1 = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTEdJx5FSeT-6mNoGh2gjJMUu313u-UmTowPQ&usqp=CAU";
            push.big_picture = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTEdJx5FSeT-6mNoGh2gjJMUu313u-UmTowPQ&usqp=CAU";


            push.contents = pushContent;
            push.app_id = APIID;

            //push.data.displayName=bild
            request.AddJsonBody(JsonSerializer.Serialize(push));
            RestResponse response = await client.ExecuteAsync(request);
            //Console.WriteLine(response.Content);
            return true;

        }

        public Task<bool> SendPushAll(OdiBildirim bildirim)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SendPushClient(OdiBildirim bildirim, List<string> externalIds)
        {
            var options = new RestClientOptions("https://onesignal.com")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/api/v1/notifications", Method.Post);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", APIKEY);

            PushModel push = new PushModel();
            push.include_aliases.external_id = externalIds;
            PushContents pushContent = new PushContents();
            pushContent.en = bildirim.Mesaj;
            pushContent.tr = bildirim.Mesaj;
            push.contents = pushContent;
            push.headings.tr = bildirim.Baslik;
            push.headings.en = bildirim.Baslik;
            push.target_channel = OneSignalTargetChannels.push;
            push.subtitle.tr = bildirim.AltBaslik;
            push.subtitle.en = bildirim.AltBaslik;



            push.name = "INTERNAL_CAMPAIGN_NAME";
            //List<PushButton> buttons = new List<PushButton>();
            //PushButton okButton = new PushButton();
            //okButton.id = "ok";
            //okButton.text = "Kabul Et";
            //okButton.icon = "checkmark";
            //okButton.icon_type = "system";
            //buttons.Add(okButton);

            //PushButton redButton = new PushButton();
            //okButton.id = "disc";
            //okButton.text = "Reddet";
            //okButton.icon = "xmark";
            //okButton.icon_type = "system";
            //buttons.Add(redButton);

            //push.buttons = buttons;

            push.ios_badgeType = "Increase";
            push.ios_badgeCount = 1;
            push.ios_sound = "default";

            push.chrome_icon = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTEdJx5FSeT-6mNoGh2gjJMUu313u-UmTowPQ&usqp=CAU";
            push.chrome_web_icon = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTEdJx5FSeT-6mNoGh2gjJMUu313u-UmTowPQ&usqp=CAU";
            push.chrome_web_image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTEdJx5FSeT-6mNoGh2gjJMUu313u-UmTowPQ&usqp=CAU";

            //push.ios_attachments.image1 = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTEdJx5FSeT-6mNoGh2gjJMUu313u-UmTowPQ&usqp=CAU";
            //push.big_picture = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTEdJx5FSeT-6mNoGh2gjJMUu313u-UmTowPQ&usqp=CAU";

            push.app_id = APIID;
            push.data.notType = "odi push";
            push.data.bildirimTipi = bildirim.BildirimTipi;
            push.data.displayName = bildirim.GonderenKullaniciAdSoyad;
            push.data.avatar = _amazonS3Service.GetPreSignedUrl(bildirim.DosyaYolu);
            request.AddJsonBody(JsonSerializer.Serialize(push));
            RestResponse response = await client.ExecuteAsync(request);
            //Console.WriteLine(response.Content);
            return true;
        }
    }
}
