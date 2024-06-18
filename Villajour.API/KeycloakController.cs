using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Villajour.API.Controllers;

namespace Villajour.API
{
    [ApiController]
    [Route("Api/[controller]")]
    public class KeycloakController : ApiControllerBase
    {

        private HttpClient _httpClient;
        private readonly string _baseUrl = "https://keycloak-dev.villajour.fr:8443/admin/realms/";


        [HttpGet("GetClientKeycloakByRole")]
        public async Task<IActionResult> GetUsersByRoleId(string realm, string roleId)
        {
            try
            {
                string accessToken = await GetAccessToken();
                _httpClient = new HttpClient();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var url = $"{_baseUrl}{realm}/roles-by-id/{roleId}/users";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                }

                var usersJson = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<UserInfo>>(usersJson);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("GetToken")]

        public async Task<string> GetAccessToken()
        {
            var client = new HttpClient();
            var tokenEndpoint = "https://keycloak-dev.villajour.fr:8443/realms/master/protocol/openid-connect/token";
            var requestContent = new FormUrlEncodedContent(new[]
            {
        new KeyValuePair<string, string>("client_id", "admin-cli"),
        new KeyValuePair<string, string>("username", "admin"),
        new KeyValuePair<string, string>("password", "a1O}]MwTY^w6bc-M"),
        new KeyValuePair<string, string>("grant_type", "password")
    });

            var response = await client.PostAsync(tokenEndpoint, requestContent);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException("Failed to retrieve access token.");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var tokenData = JsonConvert.DeserializeObject<dynamic>(responseContent);
            return tokenData.access_token;
        }
    }

    public class UserInfo
    {
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
