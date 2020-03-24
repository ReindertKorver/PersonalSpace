using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entities;

namespace PersonalSpaceUI.DAL
{
    public static class BaseConnection
    {
        public static string APIURL = "http://localhost:52664/";
        public static HttpClient client;

        public static string GetAuth(string username, string password)
        {
            return "Basic " + Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));
        }

        public static async Task<T> DoRequest<T>(object obj, HttpMethod method, (string, string)[] headers = null, string addToURL = "")
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(APIURL);
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    client.DefaultRequestHeaders.Add(header.Item1, header.Item2);
                }
            }
            HttpResponseMessage response;
            if (method == HttpMethod.Post)
            {
                response = await client.PostAsync(
                    addToURL, new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json"));
            }
            else if (method == HttpMethod.Put)
            {
                response = await client.PutAsync(
                     addToURL, new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json"));
            }
            else if (method == HttpMethod.Delete)
            {
                response = await client.DeleteAsync(addToURL);
            }
            else
            {
                response = await client.GetAsync(addToURL);
            }
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result);
            }
            else
            {
                string result = await response.Content.ReadAsStringAsync();
                Exception res = JsonConvert.DeserializeObject<Exception>(result);
                throw res;
            }
        }
    }
}