namespace OdiApp.DTOs.TokenDTOs
{
    public class RefreshTokenDTO
    {
        public string RefreshToken { get; set; }

        public override string ToString()
        {
            return RefreshToken;
        }
    }
}
