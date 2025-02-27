using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;

namespace OdiApp.BusinessLayer.Core
{
    public class Fonksiyonlar
    {

        public static string GenerateNewPassword(int size)
        {
            char[] cr = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPRSTUVYZ".ToCharArray();
            string result = string.Empty;
            Random r = new Random();
            for (int i = 0; i < size; i++)
            {
                result += cr[r.Next(0, cr.Length - 1)].ToString();
            }
            return result;
        }
        public static string convertEnglish(string str)
        {
            str = str.Replace("ı", "i")
                     .Replace("ö", "o")
                     .Replace("ü", "u")
                     .Replace("İ", "I")
                     .Replace("Ö", "O")
                     .Replace("Ü", "U")
                     .Replace("ç", "c")
                     .Replace("Ç", "c")
                     .Replace("ğ", "g")
                     .Replace("Ğ", "G")
                     .Replace("ş", "s")
                     .Replace("Ş", "S");

            return str;
        }
        public static string convertLink(string str)
        {
            str = convertEnglish(str.ToLower());
            str = str.Replace("&", "").Replace(" ", "-").Replace("?", "").Replace(".", "").Replace(",", "").Replace("+", "-");
            return str;

        }

        public static string convertDateTimeListToString(List<DateTime> list)
        {
            List<string> strList = new List<string>();
            foreach (DateTime dt in list)
            {
                strList.Add(dt.ToString());
            }
            return string.Join(",", strList);
        }
        public static List<DateTime> convertStringToDateTimeList(string str)
        {
            List<string> strList = str.Split(',').ToList();
            List<DateTime> dateTimeList = new List<DateTime>();
            foreach (string s in strList)
            {
                dateTimeList.Add(Convert.ToDateTime(s));
            }
            return dateTimeList;
        }

        public static void WriteTXT(string str)
        {
            string dosyaYolu = "errorText.txt";
            using (StreamWriter dosya = new StreamWriter(dosyaYolu))
            {
                dosya.WriteLine(str);
            }
        }

        #region Mail
        public static string HtmlToString(string htmlPath)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(htmlPath))
            {
                body = reader.ReadToEnd();
            }
            return body;
        }
        public static string smtpport = "mail.odiapp.click";
        public static int port = 587;
        public static string mailfrom = "destek@odiapp.click";
        public static string gonderenmailsifre = "Cb31ce2$0";

        public static void MailSender(string body, string konu, string mailTo)
        {
            var fromAddress = new MailAddress(mailfrom);
            var toAddress = new MailAddress(mailTo);
            string subject = konu;
            using (var smtp = new SmtpClient
            {
                Host = smtpport,
                Port = port,
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,

                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, gonderenmailsifre)
            })
            {
                using (var message = new MailMessage(fromAddress, toAddress) { Subject = subject, Body = body })
                {
                    message.IsBodyHtml = true;
                    smtp.Send(message);
                }
            }

        }


        #endregion
        public static void UpdateNonDefaultProperties<T>(T source, T target)
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                var sourceValue = property.GetValue(source);
                var targetValue = property.GetValue(target);

                if (sourceValue != null && !sourceValue.Equals(GetDefaultValue(property.PropertyType)))
                {
                    property.SetValue(target, sourceValue);
                }
            }
        }

        private static object GetDefaultValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}