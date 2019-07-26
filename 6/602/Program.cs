using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using System.Xml;

namespace _602
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee employee = new Employee
            {

                Id = "123",
                Deptment = "生产",
                Name = "lhl",
                Sex = "M"
            };
            string action = "http://www.lhl.com/addEmployee";
            string ns = "http://www.lhl.com/hr";
            GenerateMessage<Employee>(employee, action, ns, "employee.xml");

            Console.ReadLine();
        }

        public static void GenerateMessage<T>(T typedMessage, string action, string ns, string filename)
        {
            TypedMessageConverter converter = TypedMessageConverter.Create(typeof(T),action,ns);
            using (Message message =converter.ToMessage(typedMessage) ) {
                using (XmlWriter writer = new XmlTextWriter(filename,Encoding.UTF8))
                {
                    message.WriteMessage(writer);
                }
            }


        }
    }

    [MessageContract]
    public class Employee {
        [MessageHeader]
        public string Id { get; set; }
        [MessageBodyMember]
        public string Name { get; set; }
        [MessageBodyMember]
        public string Sex { get; set; }
        [MessageBodyMember]
        public string Deptment { get; set; }
    }
}
