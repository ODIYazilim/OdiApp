namespace OdiApp.DTOs.TokenDTOs
{
    //Kullanıcının token bilgilerini tutan sınıf
    public class UserTokenDTO
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
