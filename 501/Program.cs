using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace _501
{
    class Program
    {
        static void Main(string[] args)
        {
            Address address = new Address
            {

                Province = "江苏",
                City = "苏州",
                District = "工业园区",
                Road = "人民路"
            };
            Customer customer = new Customer
            {
                Name = "李四",
                Phone = "",
                CompanyAddress = address,
                ShipAddress = address
            };

            Serialize<Customer>(customer, "customer1.xml",false);
            Serialize<Customer>(customer, "customer2.xml", true);

            Console.Read();
        }


        static void Serialize<T>(T instance , string fileName, bool preserveReference) {
            DataContractSerializer serializer = new DataContractSerializer(typeof(T),null,int.MaxValue,false,preserveReference,null);
            using (XmlWriter writer = new XmlTextWriter(fileName,Encoding.UTF8)) {
                serializer.WriteObject(writer,instance);  
            }
        }

    }

    [DataContract]
    public class Customer {
        [DataMember]
        public string Name { get ; set ; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public Address CompanyAddress { get; set; }
        [DataMember]
        public Address ShipAddress { get; set; }

    }
    [DataContract]
    public class Address
    {
        [DataMember]
        public string Province { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string District { get; set; }
        [DataMember]
        public string Road { get; set; }
    }
}
