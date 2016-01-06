using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
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

        public Task<IList<EmployeeDto>> GetAllEmployees()
        {
            // http://stackoverflow.com/questions/10679214/how-do-you-set-the-content-type-header-for-an-httpclient-request
            ////HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "relativeAddress");
            ////request.Content = new StringContent("{\"name\":\"John Doe\",\"age\":33}",
            ////                                    Encoding.UTF8,
            ////                                    "application/json");

            var response = this.HandleHttpRequestAsync<IList<EmployeeDto>>(HttpMethod.Get);
            return response;
        }

        public Task CreateEmployee(EmployeeDto employee)
        {
            return this.HandleHttpRequestAsync<IList<EmployeeDto>>(HttpMethod.Post, employee);
        }

        private async Task<T> HandleHttpRequestAsync<T>(HttpMethod httpMethod, object parameter = null, [CallerMemberName] string methodName = "")
        {
            var httpRequestMessage = new HttpRequestMessage(httpMethod, methodName);

            if (parameter != null)
            {
                var result = JsonConvert.SerializeObject(parameter);
                httpRequestMessage.Content = new StringContent(result, Encoding.UTF8, "application/json");
            }
           
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

        public Task<IList<DepartmentDto>> GetAllDepartments()
        {
            var response = this.HandleHttpRequestAsync<IList<DepartmentDto>>(HttpMethod.Get);
            return response;
        }

        public Task CreateDepartment(DepartmentDto department)
        {
            return this.HandleHttpRequestAsync<IList<DepartmentDto>>(HttpMethod.Post, department);
        }
    }
}