using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using CrossPlatformLibrary.Tracing;

using Employee.Service.Contracts.DataContracts;

using Guards;

using Newtonsoft.Json;

namespace Employee.Client.Shared.Service
{
    public class EmployeeServiceClient : IEmployeeServiceClient
    {
        private static readonly Uri RequestUrl = new Uri("http://localhost:2004/employeeservice.svc/");
        private readonly ITracer tracer;
        private readonly HttpClient client;

        public EmployeeServiceClient(ITracer tracer)
        {
            Guard.ArgumentNotNull(() => tracer);

            this.tracer = tracer;

            this.client = new HttpClient();
            this.client.BaseAddress = RequestUrl;
            this.client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IList<EmployeeDto>> GetAllEmployees()
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "getallemployees");

            // http://stackoverflow.com/questions/10679214/how-do-you-set-the-content-type-header-for-an-httpclient-request
            ////HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "relativeAddress");
            ////request.Content = new StringContent("{\"name\":\"John Doe\",\"age\":33}",
            ////                                    Encoding.UTF8,
            ////                                    "application/json");

            var response = await this.HandleHttpRequestAsync<IList<EmployeeDto>>(httpRequestMessage);
            return response ?? Enumerable.Empty<EmployeeDto>().ToList();
        }

        private async Task<T> HandleHttpRequestAsync<T>(HttpRequestMessage httpRequestMessage)
        {
            var httpResponseMessage = await this.client.SendAsync(httpRequestMessage);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var json = await httpResponseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }

            this.tracer.Warning("Http request to {0} failed. HttpResponseMessage returned code {1}({2}).",
                httpRequestMessage.RequestUri,
                (int)httpResponseMessage.StatusCode,
                httpResponseMessage.StatusCode);

            return default(T);
        }
    }
}