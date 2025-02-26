using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace OdiApp.DTOs.SharedDTOs.IdentityDTOs.ConnectDTOs
{
    /// <summary>
    /// User login işleminde token'ı almak için gönderilen request modelidir.
    /// </summary>
    public class TokenInputDTO
    {
        [JsonPropertyName("client_id")]
        public string ClientId { get; set; }
        [JsonPropertyName("client_secret")]
        public string ClientSecret { get; set; }
        [JsonPropertyName("grant_type")]
        public string GrantType { get; set; }
        [JsonPropertyName("username")]
        public string Username { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }

        //public string ToFormUrlEncoded()
        //{
        //    return $"client_id={Uri.EscapeDataString(ClientId)}" +
        //           $"&client_secret={Uri.EscapeDataString(ClientSecret)}" +
        //           $"&grant_type={Uri.EscapeDataString(GrantType)}" +
        //           $"&username={Uri.EscapeDataString(Username)}" +
        //           $"&password={Uri.EscapeDataString(Password)}";
        //}

        public string ToFormUrlEncoded()
        {
            var properties = typeof(TokenInputDTO).GetProperties()
                .Where(p => p.GetValue(this) != null)
                .Select(p => $"{Uri.EscapeDataString(p.Name)}={Uri.EscapeDataString(p.GetValue(this).ToString())}");

            return string.Join("&", properties);
        }

        public Dictionary<string, string> ToDictionary()
        {
            var dictionary = new Dictionary<string, string>
    {
        { "client_id", ClientId },
        { "client_secret", ClientSecret },
        { "grant_type", GrantType },
        { "username", Username },
        { "password", Password }
    };

            return dictionary;
        }
    }
}