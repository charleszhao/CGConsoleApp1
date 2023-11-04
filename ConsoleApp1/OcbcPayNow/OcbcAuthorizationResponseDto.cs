using Newtonsoft.Json;

namespace ConsoleApp1.OcbcPayNow
{
    public class OcbcAuthorizationResponseDto
    {
        public bool Success { get; set; }

        public OcbcAuthorizationResultDto Result { get; set; } = null!;
    }

    public class OcbcAuthorizationResultDto
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; } = null!;

        [JsonProperty("scope")]
        public string Scope { get; set; } = null!;

        [JsonProperty("token_type")]
        public string TokenType { get; set; } = null!;

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; } = null!;

        public string ErrorCode { get; set; } = null!;

        public string ErrorMessage { get; set; } = null!;
    }
}
