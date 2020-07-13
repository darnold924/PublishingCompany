using Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ServiceLayer
{
    public class PayRollSvc :IPayRollSvc
    {
        public List<DtoPayroll> GetAll()
        {
            var dtopayrolls = new List<DtoPayroll>();

            using (var client = new HttpClient())
            {
                var uri = new Uri("http://localhost/WebAPI/api/payroll/GetAll");

                var response = client.GetAsync(uri).Result;

                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ToString());

                var responseContent = response.Content;
                var responseString = responseContent.ReadAsStringAsync().Result;

                dynamic payrolls = JArray.Parse(responseString) as JArray;

                foreach (var obj in payrolls)
                {
                    DtoPayroll dto = obj.ToObject<DtoPayroll>();

                    dtopayrolls.Add(dto);
                }
            }

            return dtopayrolls;
        }
        public DtoPayroll Find(int id)
        {
            DtoPayroll dto;

            using (var client = new HttpClient())
            {
                var uri = new Uri("http://localhost/WebAPI/api/payroll/Find?id=" + id);
                HttpResponseMessage getResponseMessage = client.GetAsync(uri).Result;

                if (!getResponseMessage.IsSuccessStatusCode)
                    throw new Exception(getResponseMessage.ToString());

                var responsemessage = getResponseMessage.Content.ReadAsStringAsync().Result;

                dynamic payroll = JsonConvert.DeserializeObject(responsemessage);

                dto = payroll.ToObject<DtoPayroll>();
            }

            return dto;
        }

        public DtoPayroll FindPayRollByAuthorId(int id)
        {
            DtoPayroll dto;

            using (var client = new HttpClient())
            {
                var uri = new Uri("http://localhost/WebAPI/api/payroll/FindPayRollByAuthorId?id=" + id);
                HttpResponseMessage getResponseMessage = client.GetAsync(uri).Result;

                if (!getResponseMessage.IsSuccessStatusCode)
                    throw new Exception(getResponseMessage.ToString());

                var responsemessage = getResponseMessage.Content.ReadAsStringAsync().Result;

                dynamic payroll = JsonConvert.DeserializeObject(responsemessage);

                dto = payroll.ToObject<DtoPayroll>();
            }

            return dto;
        }

        public void Add(DtoPayroll dto)
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
                    client.PostAsync("WebAPI/api/payroll/Add", inputMessage.Content).Result;

                if (!message.IsSuccessStatusCode)
                    throw new Exception(message.ToString());
            }
        }

        public void Update(DtoPayroll dto)
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
                    client.PostAsync("WebAPI/api/payroll/Update", inputMessage.Content).Result;

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
                    client.PostAsync("WebAPI/api/payroll/Delete", inputMessage.Content).Result;

                if (!message.IsSuccessStatusCode)
                    throw new Exception(message.ToString());
            }
        }
    }
}
