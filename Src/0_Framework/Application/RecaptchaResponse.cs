using System;
using Newtonsoft.Json;

namespace _0_Framework.Application
{
    public class RecaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("challenge-ts")]
        public DateTimeOffset ChallengeTs { get; set; }
        [JsonProperty("hostname")]
        public string HostName { get; set; }

    }
}