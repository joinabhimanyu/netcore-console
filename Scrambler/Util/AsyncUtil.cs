using Scrambler.Model.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Scrambler.Util
{
    public static class AsyncUtil
    {
        static async Task<T> GetAsync<T>(string path, HttpClient client)
            where T : class
        {
            T result = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<T>();
            }
            return result;
        }
        static async Task<Tuple<Uri, T2>> PostAsync<T1, T2>(string path, T1 input, HttpClient client)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                path, input);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return new Tuple<Uri, T2>(response.Headers.Location, await response.Content.ReadAsAsync<T2>());
        }
        public static async Task<string> RunAsync<TEntity>(string baseUrl,string url) 
            where TEntity : class
        {
            var result = String.Empty;
            using (var client = new HttpClient())
            {
                // Update port # in the following line.
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    string uri = url;
                    dynamic response = await GetAsync<TEntity>(uri, client);
                    if (response != null)
                    {
                        result = response.Description ?? "";
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return result;
        }
    }
}
