using Service.Interface1001;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;

namespace Client1001
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (ChannelFactory<IEmployees> channelFactory = new ChannelFactory<IEmployees>("employeesService"))
            //{
            //   IEmployees proxy =   channelFactory.CreateChannel();

            //    //Array.ForEach(proxy.GetAll().ToArray(), (employee) => { Console.WriteLine(employee); });
            //    proxy.Create( new Employee {
            //         Id = "005",
            //         Name = "王五",
            //         Grade = "G9",
            //         Department = "型政府"
            //    } );

            //    Array.ForEach(proxy.GetAll().ToArray(), (employee) => { Console.WriteLine(employee); });
            //}

            //EndpointAddress address = new EndpointAddress("http://localhost:3721/employees");
            //Binding binding = new WebHttpBinding();
            //ContractDescription contract = ContractDescription.GetContract(typeof(Service.Interface1001.IEmployees));
            //ServiceEndpoint endpoint = new ServiceEndpoint(contract,binding,address);
            //using (ChannelFactory<IEmployees> channelFactory = new ChannelFactory<IEmployees>(endpoint))
            //{
            //    channelFactory.Endpoint.Behaviors.Add(new WebHttpBehavior());
            //    channelFactory.Open();
            //}
            string eTag;
            GetAllEmployees("", out eTag);

            GetAllEmployees(eTag, out eTag);

            CheckUpdate();
            Console.Read();
        }

        static void GetAllEmployees(string ifNoneMatch, out string eTag)
        {

            eTag = ifNoneMatch;
            Uri address = new Uri("http://localhost:3721/employees/all");
            var request = HttpWebRequest.Create(address);
            if (!string.IsNullOrEmpty(ifNoneMatch))
            {
                request.Headers.Add(HttpRequestHeader.IfNoneMatch, ifNoneMatch);
            }
            request.Method = "Get";
            try
            {
                var response = request.GetResponse() as HttpWebResponse;
                eTag = response.Headers[HttpResponseHeader.ETag];
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    Console.WriteLine(reader.ReadToEnd() + Environment.NewLine);
                }
            }
            catch (WebException ex)
            {
                var response = ex.Response as HttpWebResponse;
                if (null == response)
                    throw;
                if (response.StatusCode == HttpStatusCode.NotModified)
                {
                    Console.WriteLine("未变化");
                    return;
                }
                throw;
            }

        }

        static void CheckUpdate() {

            Uri address = new Uri("http://localhost:3721/employees/001");
            var request = HttpWebRequest.Create(address);
            request.Method = "Get";
            var response = request.GetResponse() as HttpWebResponse;
            string employee;

            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                employee = reader.ReadToEnd();
                Console.WriteLine(employee + Environment.NewLine);
                
            }
            try
            {
                address = new Uri("http://localhost:3721/employees/");
                request = HttpWebRequest.Create(address);
                request.Method = "Put";
                request.ContentType = "application/xml";
                byte[] buffer = Encoding.UTF8.GetBytes(employee);
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                request.Headers.Add(HttpRequestHeader.IfMatch, response.Headers[HttpResponseHeader.ETag]);
                Console.WriteLine("修改员工信息");
                request.GetResponse();
            }
            catch (WebException ex){
                var response1= ex.Response as HttpWebResponse;
                if (null == response1)
                    throw;
                if (response1.StatusCode == HttpStatusCode.PreconditionFailed)
                {
                    Console.WriteLine("服务端数据已经变化");
                }
                else
                    throw;


            }


        }
    }
}
