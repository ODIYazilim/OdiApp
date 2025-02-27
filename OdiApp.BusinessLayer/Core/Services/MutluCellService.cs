using System.Text;

namespace OdiApp.BusinessLayer.Core.Services
{
    public class MutluCellService
    {
        private readonly string _start;
        private const string End = "</smspack>";
        private string _body = "";

        public MutluCellService(string username, string password, string org, DateTime date)
        {
            _start = $"<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                     $"<smspack ka=\"{XmlEncode(username)}\" pwd=\"{XmlEncode(password)}\" " +
                     $"org=\"{XmlEncode(org)}\" tarih=\"{FormatDate(date)}\">";
        }

        public void AddSms(string message, string[] numbers)
        {
            var formattedNumbers = string.Join(",", numbers.Select(XmlEncode));
            _body += $"<mesaj><metin>{XmlEncode(message)}</metin><nums>{formattedNumbers}</nums></mesaj>";
        }

        public string GenerateXml()
        {
            if (string.IsNullOrEmpty(_body))
                throw new ArgumentException("SMS paketinizde sms yok!");

            return _start + _body + End;
        }

        public async Task<string> SendAsync()
        {
            string postData = GenerateXml();

            using var clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            using var client = new HttpClient(clientHandler);
            using var content = new StringContent(postData, Encoding.UTF8, "text/xml");

            var response = await client.PostAsync("https://smsgw.mutlucell.com/smsgw-ws/sndblkex", content);
            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<string> GetReportAsync(string username, string password, long id)
        {
            string xml = $"<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                         $"<smsrapor ka=\"{username}\" pwd=\"{password}\" id=\"{id}\" />";

            using var clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            using var client = new HttpClient(clientHandler);
            using var content = new StringContent(xml, Encoding.UTF8, "text/xml");

            var response = await client.PostAsync("https://smsgw.mutlucell.com/smsgw-ws/gtblkrprtex", content);
            return await response.Content.ReadAsStringAsync();
        }

        private static string FormatDate(DateTime date)
        {
            return XmlEncode(date.ToString("yyyy-MM-dd HH:mm"));
        }

        private static string XmlEncode(string value)
        {
            return value.Replace("&", "&amp;")
                        .Replace("<", "&lt;")
                        .Replace(">", "&gt;")
                        .Replace("'", "&apos;")
                        .Replace("\"", "&quot;");
        }
    }
}
