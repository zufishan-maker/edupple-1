using EDUPPLE.INFRASTRUCTURE.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDUPPLE.APPLICATION.Authentication.Models
{
    public class TokenResponseModel
    {

        [JsonProperty(TokenConstants.Parameters.AccessToken, NullValueHandling = NullValueHandling.Ignore)]
        public string AccessToken { get; set; }

        [JsonProperty(TokenConstants.Parameters.TokenType, NullValueHandling = NullValueHandling.Ignore)]
        public string TokenType { get; set; } = "bearer";

        [JsonProperty(TokenConstants.Parameters.ExpiresIn, NullValueHandling = NullValueHandling.Ignore)]
        public long ExpiresIn { get; set; }

        [JsonProperty(TokenConstants.Parameters.RefreshToken, NullValueHandling = NullValueHandling.Ignore)]
        public string RefreshToken { get; set; }
    }
}
