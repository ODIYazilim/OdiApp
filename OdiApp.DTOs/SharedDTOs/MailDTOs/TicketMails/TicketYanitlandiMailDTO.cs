using OdiApp.DTOs.SharedDTOs.MailDTOs;
using System;

namespace OdiApp.DTOs.SharedDTOs.MailDTOs.TicketMails
{
    public class TicketYanitlandiMailDTO : MailBaseDTO
    {
        public string TicketKonusu { get; set; }
        public string TicketMesaji { get; set; }
        public DateTime TicketYanitTarihi { get; set; }
    }
}