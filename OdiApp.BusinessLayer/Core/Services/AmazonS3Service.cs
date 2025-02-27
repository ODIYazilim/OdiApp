using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using OdiApp.BusinessLayer.Core;
using OdiApp.BusinessLayer.Core.Services.Interface;
using OdiApp.DTOs.SharedDTOs;
namespace OdiApp.BusinessLayer.Core.Services
{
    public class AmazonS3Service : IAmazonS3Service
    {
        public string GetFolderPath(Dosya dosya)
        {
            string klasorYolu = "";
            switch (dosya.DosyaTipi)
            {
                case 1: //Profil Fotoğrafı
                    klasorYolu = "KullaniciProfilDosyalari/" + dosya.KullaniciId + "/" + dosya.DosyaAdi;
                    break;
                case 2: //Albüm Fotoğraflari
                    klasorYolu = "KullaniciProfilDosyalari/" + dosya.KullaniciId + "/Fotograflar/" + Fonksiyonlar.convertLink(dosya.AlbumAdi) + "/" + dosya.DosyaAdi;
                    break;
                case 3: //Profil Videolari
                    klasorYolu = "KullaniciProfilDosyalari/" + dosya.KullaniciId + "/Videolar/" + Fonksiyonlar.convertLink(dosya.AlbumAdi) + "/" + dosya.DosyaAdi;
                    break;
                case 4: //Proje Kapak Fotografi
                    klasorYolu = "ProjeDosyalari/KapakFotograflari/" + dosya.DosyaAdi;
                    break;
                case 5: //Proje Ornek Fotograf
                    klasorYolu = "ProjeDosyalari/OrnekFotograflar/" + dosya.DosyaAdi;
                    break;
                case 6://Proje  Senaryo Dosyaları
                    klasorYolu = "ProjeDosyalari/Senaryolar/" + dosya.DosyaAdi;
                    break;
                case 7://Proje  Ses Dosyaları
                    klasorYolu = "ProjeDosyalari/Ses/" + dosya.DosyaAdi;
                    break;
                case 8://Proje  Örnek oyun Video Dosyaları
                    klasorYolu = "ProjeDosyalari/OrnekOyunVideo/" + dosya.DosyaAdi;
                    break;
                case 9://Fotomatik fotoları
                    klasorYolu = "PerformerOdileri/OdiFotograf/" + dosya.DosyaAdi;
                    break;
                case 10://Sesmatik Dosyaları
                    klasorYolu = "PerformerOdileri/OdiSes/" + dosya.DosyaAdi;
                    break;
                case 11://Videomatik Videoları
                    klasorYolu = "PerformerOdileri/OdiVideo/" + dosya.DosyaAdi;
                    break;
                case 12://Mesajlaşmada gönderilen dosyalar
                    klasorYolu = "Mesajlasma/" + dosya.DosyaAdi;
                    break;
                case 13://Callback Senaryolar
                    klasorYolu = "Callback/Senaryolar/" + dosya.DosyaAdi;
                    break;
                case 14: //Ticket Dosyaları
                    klasorYolu = "Ticket/" + dosya.DosyaAdi;
                    break;
                case 15: //Yetenek Videoları
                    klasorYolu = "Yetenek/" + dosya.DosyaAdi;
                    break;
            }

            return klasorYolu;
        }

        /// <summary>
        /// Dosya Uplod için kullanılır
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetUploadPreSignedUrl(string folderPath)
        {

            IAmazonS3 s3Client = GetsS3Client();
            var requestFile = new GetPreSignedUrlRequest
            {
                BucketName = "odiappms",
                Key = folderPath,
                Verb = HttpVerb.PUT,
                Expires = DateTime.UtcNow.AddHours(12)
            };

            string fileUploadUrl = s3Client.GetPreSignedURL(requestFile);
            return fileUploadUrl;
        }

        /// <summary>
        /// Dosya Display için kullanılır
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        //public string GetPreSignedUrl(string folderPath, string fileName)
        //{

        //    IAmazonS3 s3Client = GetsS3Client();
        //    var requestFile = new GetPreSignedUrlRequest
        //    {
        //        BucketName = "odiappms",
        //        Key = folderPath + "/" + fileName,
        //        Verb = HttpVerb.GET,
        //        Expires = DateTime.UtcNow.AddHours(1)
        //    };

        //    string fileUploadUrl = s3Client.GetPreSignedURL(requestFile);
        //    return fileUploadUrl;
        //}
        public string GetPreSignedUrl(string dosyaYolu)
        {

            IAmazonS3 s3Client = GetsS3Client();
            var requestFile = new GetPreSignedUrlRequest
            {
                BucketName = "odiappms",
                Key = dosyaYolu,
                Verb = HttpVerb.GET,
                Expires = DateTime.UtcNow.AddHours(12)
            };

            string fileUploadUrl = s3Client.GetPreSignedURL(requestFile);
            return fileUploadUrl;
        }
        public IAmazonS3 GetsS3Client()
        {
            RegionEndpoint bucketRegion = RegionEndpoint.EUCentral1;
            IAmazonS3 s3Client = new AmazonS3Client(
              "AKIA4QYDI6I2O5S2LRTX",
              "Itd6r61ufVkKnsco5L8HDHn3U+NPTnMeGP0513IK",
              bucketRegion

              );
            return s3Client;
        }
    }
}
