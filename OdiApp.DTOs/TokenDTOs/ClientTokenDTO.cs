namespace OdiApp.DTOs.TokenDTOs
{
    //Kullanıcı bağımsız Mobil , web vb clientların kullandığı tokenlar için kullanılacak sınıf
    public class ClientTokenDTO
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }

    }
}
