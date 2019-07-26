using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace _603
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();

            Test1();

            Test2();

            Console.ReadLine();
        }

        private static void Test2()
        {
            Employee employee = new Employee
            {

                Id = "123",
                Deptment = "生产",
                Name = "lhl",
                Sex = "M"
            };
            var session = new XmlBinaryWriterSession();
            XmlDictionary dictionary = new XmlDictionary();
            dictionary.Add("Employee");
            dictionary.Add("Id");
            dictionary.Add("Name");
            dictionary.Add("Gender");
            WriteObject<Employee>(employee, (stream) => XmlDictionaryWriter.CreateBinaryWriter(stream, dictionary, session, false));
        }

        private static void Test1()
        {
            Employee employee = new Employee
            {

                Id = "123",
                Deptment = "生产",
                Name = "lhl",
                Sex = "M"
            };
            WriteObject<Employee>(employee, (stream) => XmlDictionaryWriter.CreateTextWriter(stream, Encoding.UTF8, false));
            //WriteObject<Employee>(employee, (stream) => XmlDictionaryWriter.CreateBinaryWriter(stream, null, null, false));
        }

        private static void Test()
        {
            List<XmlDictionaryString> dictionaryStringList = new List<XmlDictionaryString>();
            XmlDictionary dictionary = new XmlDictionary();

            dictionaryStringList.Add(dictionary.Add("Employee"));
            dictionaryStringList.Add(dictionary.Add("Id"));
            dictionaryStringList.Add(dictionary.Add("Name"));
            dictionaryStringList.Add(dictionary.Add("Gender"));

            Console.WriteLine("{0,-4}{1}", "Key", "Value");
            Console.WriteLine(new string('-', 20));
            foreach (var item in dictionaryStringList)
            {
                Console.WriteLine("{0,-4}{1}", item.Key, item.Value);
            }
        }

        static void WriteObject<T>(T graph, Func<Stream,XmlDictionaryWriter> createWriter) {
            using (MemoryStream stream = new MemoryStream() ) {
                using (XmlDictionaryWriter writer = createWriter(stream) )
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                    serializer.WriteObject(writer,graph);
                }

                long count = stream.Position;
                byte[] bytes = stream.ToArray();
                StreamReader reader = new StreamReader(stream);
                stream.Position = 0;
                string content = reader.ReadToEnd();

                Console.WriteLine("字节数 {0}\n",count);
                Console.WriteLine("binary {0}\n", BitConverter.ToString(bytes));
                Console.WriteLine("text {0}\n", content);
            }
            

        }
    }

    [DataContract(Namespace = "http://www.lhl.com")]
    public class Employee
    {
        [DataMember(Order = 1)]
        public string Id { get; set; }
        [DataMember(Order = 2)]
        public string Name { get; set; }
        [DataMember(Order = 3)]
        public string Sex { get; set; }
        [DataMember(Order = 4 )]
        public string Deptment { get; set; }
    }
}
