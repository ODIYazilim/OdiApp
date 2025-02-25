using OdiApp.Entity.Base;

namespace OdiApp.Entity.Token
{
    public class UserRefreshToken : StringBaseModel
    {
        public string UserId { get; set; }
        public string Code { get; set; }
        public DateTime Expration { get; set; }
    }
}
