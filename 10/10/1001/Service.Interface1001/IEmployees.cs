using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;

namespace Service.Interface1001
{
    [ServiceContract(Namespace = "http://www.lhl.com")]
    public interface IEmployees
    {
        [WebGet(UriTemplate="all",RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        [Description("获取所有员工列表")]
        IEnumerable<Employee> GetAll();

        [WebGet(UriTemplate = "{id}")]
        Employee Get(string id);

        [WebInvoke(UriTemplate = "/", Method="POST" , 
            RequestFormat = WebMessageFormat.Xml ,
            ResponseFormat =  WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Bare)]
        void Create(Employee employee);

        [WebInvoke(UriTemplate = "/", Method = "PUT")]
        void Update(Employee employee);

        [WebInvoke(UriTemplate = "{id}", Method = "DELETE")]
        void Delete(string id);

        [WebInvoke]
        [DisplayMessageFormatter]
        void Op1(Stream stream);

        [WebInvoke(RequestFormat = WebMessageFormat.Json)]
        [DisplayMessageFormatter]
        void Op2(Message stream);

        [WebInvoke]
        [DisplayMessageFormatter]
        void Op3(string[] stream);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare)]
        [DataContractFormat]
        [DisplayMessageFormatter]
        void Op4(string[] args);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare)]
        [XmlSerializerFormat]
        [DisplayMessageFormatter]
        void Op5(string[] args);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare)]
        [XmlSerializerFormat]
        [DisplayMessageFormatter]
        void Op6();

        [WebInvoke(UriTemplate = "Op7/{arg}", BodyStyle = WebMessageBodyStyle.Bare)]
        [DisplayMessageFormatter]
        void Op7(string arg);
    }

    [DataContract(Namespace = "http://www.lhl.com")]
    public class Employee
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Department { get; set; }
        [DataMember]
        public string Grade { get; set; }

        public override string ToString()
        {
            return $"ID : {Id}, Name : {Name}  Department : {Department}  Grade : {Grade}";

        }
    }
}
