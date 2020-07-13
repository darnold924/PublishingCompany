using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ServiceLayer
{
    public class AuthorSvc :IAuthorSvc
    {
        public List<DtoAuthor> GetAll()
        {
            var dtoauthors = new List<DtoAuthor>();

            using (var client = new HttpClient())
            {
                var uri = new Uri("http://localhost/WebAPI/api/author/GetAll");

                var response = client.GetAsync(uri).Result;

                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ToString());

                var responseContent = response.Content;
                var responseString = responseContent.ReadAsStringAsync().Result;

                dynamic authors = JArray.Parse(responseString) as JArray;

                foreach (var obj in authors)
                {
                    DtoAuthor dto = obj.ToObject<DtoAuthor>();

                    dtoauthors.Add(dto);
                }
            }

            return dtoauthors;
        }
        public List<DtoAuthorType> GetAuthorTypes()
        {
            var dtoauthortypes = new List<DtoAuthorType>();

            using (var client = new HttpClient())
            {
                var uri = new Uri("http://localhost/WebAPI/api/author/GetAuthorTypes");

                var response = client.GetAsync(uri).Result;

                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ToString());

                var responseContent = response.Content;
                var responseString = responseContent.ReadAsStringAsync().Result;

                dynamic authortypes = JArray.Parse(responseString) as JArray;

                foreach (var obj in authortypes)
                {
                    DtoAuthorType dto = obj.ToObject<DtoAuthorType>();

                    dtoauthortypes.Add(dto);
                }
            }

            return dtoauthortypes;
        }

        public DtoAuthor  Find(int id)
        {
            DtoAuthor dto;

            using (var client = new HttpClient())
            {
                var uri = new Uri("http://localhost/WebAPI/api/author/Find?id=" + id);
                HttpResponseMessage getResponseMessage = client.GetAsync(uri).Result;

                if (!getResponseMessage.IsSuccessStatusCode)
                    throw new Exception(getResponseMessage.ToString());

                var responsemessage = getResponseMessage.Content.ReadAsStringAsync().Result;

                dynamic author = JsonConvert.DeserializeObject(responsemessage);

                dto = author.ToObject<DtoAuthor>();
            }

            return dto;
        }

        public void Add(DtoAuthor dto)
        {
            using (var client = new HttpClient { BaseAddress = new Uri("http://localhost") })
            {
                string serailizeddto = JsonConvert.SerializeObject(dto);

                var inputMessage = new HttpRequestMessage
                {
                    Content = new StringContent(serailizeddto, Encoding.UTF8, "application/json")
                };

                inputMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage message =
                    client.PostAsync("WebAPI/api/author/Add", inputMessage.Content).Result;

                if (!message.IsSuccessStatusCode)
                    throw new Exception(message.ToString());
            }
        }

        public void Update(DtoAuthor dto)
        {
            using (var client = new HttpClient { BaseAddress = new Uri("http://localhost") })
            {
                string serailizeddto = JsonConvert.SerializeObject(dto);

                var inputMessage = new HttpRequestMessage
                {
                    Content = new StringContent(serailizeddto, Encoding.UTF8, "application/json")
                };

                inputMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage message =
                    client.PostAsync("WebAPI/api/author/Update", inputMessage.Content).Result;

                if (!message.IsSuccessStatusCode)
                    throw new Exception(message.ToString());
            }
        }

        public void Delete(DtoId dto)
        {
            using (var client = new HttpClient { BaseAddress = new Uri("http://localhost") })
            {
                string serailizeddto = JsonConvert.SerializeObject(dto);

                var inputMessage = new HttpRequestMessage
                {
                    Content = new StringContent(serailizeddto, Encoding.UTF8, "application/json")
                };

                inputMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage message =
                    client.PostAsync("WebAPI/api/author/Delete", inputMessage.Content).Result;

                if (!message.IsSuccessStatusCode)
                    throw new Exception(message.ToString());
            }
        }
    }
}
