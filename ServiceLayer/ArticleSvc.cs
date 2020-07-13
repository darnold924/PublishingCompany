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
    public class ArticleSvc :IArticleSvc
    {
        public List<DtoArticle> GetAll()
        {
            var dtoarticles = new List<DtoArticle>();

            using (var client = new HttpClient())
            {
                var uri = new Uri("http://localhost/WebAPI/api/article/GetAll");

                var response = client.GetAsync(uri).Result;

                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ToString());

                var responseContent = response.Content;
                var responseString = responseContent.ReadAsStringAsync().Result;

                dynamic articles = JArray.Parse(responseString) as JArray;

                foreach (var obj in articles)
                {
                    DtoArticle dto = obj.ToObject<DtoArticle>();

                    dtoarticles.Add(dto);
                }
            }

            return dtoarticles;
        }

        public List<DtoArticle> GetArticlesByAuthorId(int id)
        {
            var dtoarticles = new List<DtoArticle>();

            using (var client = new HttpClient())
            {
                var uri = new Uri("http://localhost/WebAPI/api/article/GetArticlesByAuthorId?id=" + id);

                var response = client.GetAsync(uri).Result;

                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ToString());

                var responseContent = response.Content;
                var responseString = responseContent.ReadAsStringAsync().Result;

                dynamic articles = JArray.Parse(responseString) as JArray;

                foreach (var obj in articles)
                {
                    DtoArticle dto = obj.ToObject<DtoArticle>();

                    dtoarticles.Add(dto);
                }
            }

            return dtoarticles;
        }
        public DtoArticle Find(int id)
        {
            DtoArticle dto;

            using (var client = new HttpClient())
            {
                var uri = new Uri("http://localhost/WebAPI/api/article/Find?id=" + id);
                HttpResponseMessage getResponseMessage = client.GetAsync(uri).Result;

                if (!getResponseMessage.IsSuccessStatusCode)
                    throw new Exception(getResponseMessage.ToString());

                var responsemessage = getResponseMessage.Content.ReadAsStringAsync().Result;

                dynamic article = JsonConvert.DeserializeObject(responsemessage);

                dto = article.ToObject<DtoArticle>();
            }

            return dto;
        }

        public void Add(DtoArticle dto)
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
                    client.PostAsync("WebAPI/api/article/Add", inputMessage.Content).Result;

                if (!message.IsSuccessStatusCode)
                    throw new Exception(message.ToString());
            }
        }

        public void Update(DtoArticle dto)
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
                    client.PostAsync("WebAPI/api/article/Update", inputMessage.Content).Result;

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
                    client.PostAsync("WebAPI/api/article/Delete", inputMessage.Content).Result;

                if (!message.IsSuccessStatusCode)
                    throw new Exception(message.ToString());
            }
        }
    }
}
