using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NieuweStroom.POC.IT.Infrastructure;

namespace NieuweStroom.POC.IT.IntegrationTest.Helpers
{
    public class Request<TStartup> : IDisposable where TStartup : class
    {
        private readonly HttpClient client;
        private readonly TestServer server;

        public Request()
        {
            var webHostBuilder = new WebHostBuilder().UseStartup<TStartup>().UseConfiguration(ConfigurationSingleton.GetConfiguration());
            this.server = new TestServer(webHostBuilder);
            this.client = server.CreateClient();
        }

        public JwtAuthentication Jwt => new JwtAuthentication(ConfigurationSingleton.GetConfiguration());

        public Task<HttpResponseMessage> Get(string url)
        {
            return client.GetAsync(url);
        }

        public Request<TStartup> AddAuth(string token)
        {
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return this;
        }

        public Task<HttpResponseMessage> Post<T>(string url, T body)
        {
            return client.PostAsJsonAsync<T>(url, body);
        }

        public Task<HttpResponseMessage> Put<T>(string url, T body)
        {
            return client.PutAsJsonAsync<T>(url, body);
        }

        public Task<HttpResponseMessage> Delete(string url)
        {
            return client.DeleteAsync(url);
        }

        public void Dispose()
        {
            client.Dispose();
            server.Dispose();
        }
    }
}