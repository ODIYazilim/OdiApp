using Amazon.S3;
using OdiApp.DTOs.SharedDTOs;

namespace Odi.Shared.Services.Interface
{
    public interface IAmazonS3Service
    {
        string GetFolderPath(Dosya dosya);
        string GetUploadPreSignedUrl(string folderPath);
        //string GetPreSignedUrl(string folderPath, string fileName);
        string GetPreSignedUrl(string dosyaYolu);
        IAmazonS3 GetsS3Client();
    }
}
