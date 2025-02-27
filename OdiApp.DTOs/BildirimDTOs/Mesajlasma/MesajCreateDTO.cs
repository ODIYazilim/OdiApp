namespace OdiApp.DTOs.BildirimDTOs.Mesajlasma
{
    public class MesajCreateDTO
    {
        public string Kullanici1Id { get; set; }
        public string Kullanici2Id { get; set; }
        public List<MesajDetayCreateDTO> MesajDetaylar { get; set; }
    }
}