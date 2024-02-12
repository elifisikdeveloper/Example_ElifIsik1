using Example_ElifIsik.Dtos;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text;

namespace Example_ElifIsik.Services
{
    public class TestService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TestService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        //servis kullarak da denemiştim.
        public async Task<bool> AddTestAsync(MaterialModel model, string token)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    throw new ArgumentException("Token cannot be null or empty.");
                }

                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

                using (HttpClient client = new HttpClient(clientHandler))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var jsonContent = JsonConvert.SerializeObject(model);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    using (HttpResponseMessage response = await client.PostAsync("http://testapi.dogruer.com/api/MaterialMaster/SaveMaterialMaster", content))
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();

                        if (response.IsSuccessStatusCode)
                        {
                            return true;
                        }
                        else
                        {
                            throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}. Response content: {responseContent}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the HTTP request: " + ex.Message);
            }
        }

    }
}
